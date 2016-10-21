using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Net;
using System.IO;
using System.Xml;
using System.Runtime.InteropServices;
using System.Net.Sockets;
using System.Threading;
using System.Timers;
using NAudio;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace wintelive
{
    public static class tetraRx
    {
        // interface to listen for UDP packets from tetra-rx instances
        public static SynchronizationContext ctx = null;

        public static UdpClient udpCli = new UdpClient(7379);

        public static void Recv(IAsyncResult res)
        {
            byte[] prebuf = new byte[0];

            try
            {
                IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
                prebuf = udpCli.EndReceive(res, ref sender);
            }
            catch { }

            ctx.Send(delegate
            {
                telive.parseUdp(prebuf);
            }, null);

            try
            {
                udpCli.BeginReceive(new AsyncCallback(Recv), null);
            }
            catch { }
        }

        public static void Start()
        {
            udpCli.BeginReceive(new AsyncCallback(Recv), null);
        }

        public static void Stop()
        {
            udpCli.Close();
        }
    }

    public class acelp
    {

        BufferedWaveProvider wp;
        FileStream fs;

        int fpass = 1;
        ushort usid;
        bool r2play = false;
        bool r2rec = false;

        object playLock, recLock;
        

        [DllImport("libtetradec.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void tetra_decode_init();
        [DllImport("libtetradec.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int tetra_cdec(int fp, short[] inp, short[] outp);
        [DllImport("libtetradec.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int tetra_sdec(short[] inp, short[] outp);

        public acelp(ushort nusid)
        {
            usid = nusid;
            playLock = new object();
            recLock = new object();
            initDll();
        }

        public void initDll()
        {
            fpass = 1;
            tetra_decode_init();
        }

        public static byte[] s2b(short[] shorts)
        {
            int len = shorts.Length * 2;
            byte[] nbytes = new byte[len];
            Buffer.BlockCopy(shorts, 0, nbytes, 0, len);
            return nbytes;
        }

        public static short[] b2s(byte[] bytes)
        {
            int len = (int)Math.Ceiling((double)(bytes.Length / 2));
            short[] nshorts = new short[len];
            Buffer.BlockCopy(bytes, 0, nshorts, 0, bytes.Length);
            return nshorts;
        }

        public byte[] acelp2pcm(byte[] acdata)
        {
            short[] sh = b2s(acdata);

            short[] cdc = new short[276];
            tetra_cdec(fpass, sh, cdc);
            fpass = 0;

            short[] sdc = new short[480];
            tetra_sdec(cdc, sdc);

            return s2b(sdc);

        }

        public void processAcelp(byte[] audio)
        {
            byte[] pcmaudio = acelp2pcm(audio);
            if (pcmaudio.Any(w => w != 0))
            {
                listenInit();
                listen(pcmaudio);

                recordInit();
                record(pcmaudio);
            }
        }

        public void listenInit()
        {
            lock (playLock)
            {
                if (r2play) return;
                wp = new BufferedWaveProvider(new WaveFormat(8000, 16, 1));
                telive.msp.AddMixerInput(wp);
                r2play = true;
            }
        }

        public void listen(byte[] pcm)
        {
            if (wp.BufferedBytes + pcm.Length >= wp.BufferLength || telive.wout.PlaybackState != PlaybackState.Playing)
                telive.wout.Play();
            wp.AddSamples(pcm, 0, pcm.Length);
        }

        public void listenClose()
        {
            lock (playLock)
            {
                if (!r2play) return;
                telive.msp.RemoveMixerInput(wp.ToSampleProvider());
                
                wp = null;
                r2play = false;
            }
        }

        public void recordInit()
        {
            lock (recLock)
            {
                if (r2rec) return;
                string recpath = string.Format(@"c:\a\tetra\rec_{0:yyyyMMdd}_{0:HHmmss}_idx{1}.wav", DateTime.Now, usid);
                bool writeHead = true;
                if (File.Exists(recpath))
                {
                    writeHead = false;
                }
                fs = new FileStream(recpath, FileMode.Append);
                if (writeHead)
                {
                    byte[] wavhead = new byte[] { 0x52, 0x49, 0x46, 0x46, 0xFF, 0xFF, 0xFF, 0xFF, 0x57, 0x41, 0x56, 0x45, 0x66, 0x6D, 0x74, 0x20, 0x10, 0x00, 0x00, 0x00, 0x01, 0x00, 0x01, 0x00, 0x40, 0x1F, 0x00, 0x00, 0x80, 0x3E, 0x00, 0x00, 0x02, 0x00, 0x10, 0x00, 0x64, 0x61, 0x74, 0x61, 0xFF, 0xFF, 0xFF, 0xFF };
                    fs.Write(wavhead, 0, wavhead.Length);
                }
                r2rec = true;
            }
        }

        public void record(byte[] pcm)
        {
            fs.Write(pcm, 0, pcm.Length);
        }

        public void recordClose()
        {
            lock (recLock)
            {
                if (!r2rec) return;
                fs.Close();
                r2rec = false;
            }
        }

    }

    public static class gnuradio
    {
        public static string addr;
        public static double basefreq;
        public static double ppm_corr;
        public static double samp_rate;
        public static double sdr_gain;
        public static double sdr_ifgain;
        public static int receivers;
        public static string title;
        public static double cbw = 12500;
        public static bool baselock = false;

        private static object sendreq(bool method, string varname, object value = null)
        {
            // send XML RPC request
            string kver;
            if (method)
            {
                string typ;
                switch (Type.GetTypeCode(value.GetType()))
                {
                    case TypeCode.Int16:
                    case TypeCode.Int32:
                    case TypeCode.Int64:
                    case TypeCode.UInt16:
                    case TypeCode.UInt32:
                    case TypeCode.UInt64:
                        typ = "int";
                        break;

                    case TypeCode.Double:
                        typ = "double";
                        break;

                    case TypeCode.Boolean:
                        typ = "boolean";
                        break;

                    default:
                        typ = "string";
                        value = value.ToString();
                        break;
                }
                kver = string.Format("<?xml version=\"1.0\"?><methodCall> <methodName>set_{0}</methodName><params><param><value><{1}>{2}</{1}></value></param></params></methodCall>\n", varname, typ, value);
            }
            else
            {
                kver = string.Format("<?xml version=\"1.0\"?><methodCall> <methodName>get_{0}</methodName></methodCall>\n", varname);
            }

            WebClient wc = new WebClient();
            wc.Headers.Add("Content-Type", "text/xml");
            string vystup = Encoding.ASCII.GetString(wc.UploadData(addr, Encoding.ASCII.GetBytes(kver)));

            XmlDocument xd = new XmlDocument();
            xd.LoadXml(vystup);
            XmlNode chyba = xd.SelectSingleNode("/methodResponse/fault");
            object oval = null;
            if (chyba == null)
            {
                // no problem
                if (method)
                    return true;

                XmlNode respn = xd.SelectSingleNode("/methodResponse/params/param/value");
                XmlNode respt = respn.FirstChild;
                string otyp = respt.Name;
                string cont = respt.InnerText;
                switch (otyp)
                {
                    case "string":
                        oval = cont;
                        break;

                    case "int":
                        oval = int.Parse(cont);
                        break;

                    case "double":
                        oval = double.Parse(cont, CultureInfo.InvariantCulture);
                        break;

                    case "boolean":
                        oval = bool.Parse(cont);
                        break;

                }
            }
            else
            {
                if (method)
                    return false;
            }
            return oval;

        }

        public static bool setval(string varname, object varvalue)
        {
            // set GNU Radio value
            return (bool)sendreq(true, varname, varvalue);
        }

        public static object getval(string varname)
        {
            // Read GNU Radio value
            return sendreq(false, varname);
        }

        public static bool baseSet(double nfreq)
        {
            // Set baseband frequency
            bool vysl = setval("freq", nfreq);
            if (vysl)
            {
                basefreq = nfreq;
            }
            return vysl;
        }

        public static bool getSampRate()
        {
            // Get tuner sample rate
            object srate = getval("samp_rate");
            if (srate != null)
            {
                samp_rate = Convert.ToDouble(srate);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool getBaseFreq()
        {
            // Get tuner baseband frequency
            object freqval = getval("freq");
            if (freqval != null)
            {
                basefreq = Convert.ToDouble(freqval);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool getReceivers()
        {
            // Get receiver count
            object rxcount = getval("telive_receiver_channels");
            if (rxcount != null)
            {
                receivers = (int)rxcount;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool getTitle()
        {
            // Get project title
            object titl = getval("telive_receiver_name");
            if (titl != null)
            {
                title = (string)titl;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void init(string naddr, ushort nport)
        {
            // Initialize GNU Radio URL and some data structures
            addr = string.Format("http://{0}:{1}/", naddr, nport);
            getTitle();
            getBaseFreq();
            getSampRate();
            getReceivers();
        }

        public static bool freqIsOver(double qfreq, IEnumerable<double> oldfreqs)
        {
            return (qfreq > oldfreqs.Max());
        }

        public static bool freqIsUnder(double qfreq, IEnumerable<double> oldfreqs)
        {
            return (qfreq < oldfreqs.Min());
        }

        public static bool freqFitsToBase(double nfreq)
        {
            // Does frequency fit into currently tuned baseband?
            // (i.e. can we listen to it without changing the base frequency?)
            double noff = nfreq - basefreq;
            double maxdiff = (samp_rate / 2) - cbw;
            return (Math.Abs(noff) <= maxdiff);
        }

        public static bool freqsFitToBandwidth(IEnumerable<double> freqs)
        {
            // Are all specified frequencies within single bandwidth?
            // (i.e. can we tune them all together?)
            bool fits = false;
            if (freqs.Count() > 0)
            {
                double min = freqs.Min();
                double max = freqs.Max();
                double range = max - min;

                fits = range <= samp_rate - 2 * cbw;
            }
            return fits;
        }

        public static double freqsCenter(IEnumerable<double> freqs)
        {
            // Return exact center of specified frequencies
            if (freqs.Count() > 0)
            {
                return (freqs.Max() + freqs.Min()) / 2;
            }
            // Or nothing if there are none
            return -1;
            
        }

        public static double baseCalc(double nfreq, IEnumerable<double> neededfreqs, bool allowRxLoss)
        {
            // Calculate new baseband frequency that covers all specified frequencies optimally
            // Will return -1 if that's not possible (all freqs don't fit bandwidth)
            double nbase = 0;
            if (neededfreqs.Count() > 0)
            {
                sbyte newpos = 0;
                if (freqIsOver(nfreq, neededfreqs))
                    newpos = 1;
                if (freqIsUnder(nfreq, neededfreqs))
                    newpos = -1;

                neededfreqs = neededfreqs.Concat(new double[] { nfreq });

                // check the list
                if (freqsFitToBandwidth(neededfreqs))
                {
                    // it fits
                    nbase = freqsCenter(neededfreqs);
                    // this also means that newpos = 0, just sayin... :D
                }
                else
                {
                    // not possible without losing some channels
                    if (allowRxLoss)
                    {
                        // this will cut the currently used freuqencies list until
                        // the list + the new freq fit within one baseband
                        do
                        {

                            if (newpos == 1)
                            {
                                double fmin = neededfreqs.Min();
                                neededfreqs = neededfreqs.Where(w => w != fmin);
                            }
                            else
                            if (newpos == -1)
                            {
                                double fmax = neededfreqs.Max();
                                neededfreqs = neededfreqs.Where(w => w != fmax);
                            }

                        } while (!freqsFitToBandwidth(neededfreqs));

                        // ok, now we have a frequency list that was stripped of some border freqencies
                        // and contains the rest AND the new frequency

                        nbase = freqsCenter(neededfreqs);

                    }
                    else
                    {
                        // just return that it's impossible
                        nbase = -1;
                    }
                }
            }
            else
            {
                // spot on the new freq
                nbase = nfreq;
            }
            return nbase;
        }

        public static double lowestValidFreq()
        {
            // gets the lower tunable frequency of current baseband
            // (with proper rounding to 100KHz's)
            double lvf = basefreq - (samp_rate / 2);
            int roundbase = 100000;
            long rnd = (long)lvf / roundbase;
            lvf = rnd * roundbase;

            while (!freqFitsToBase(lvf))
            {
                lvf += cbw;
            }
            return lvf;
        }
    }

    public static class settings
    {
        public static double allScanLow = 420000000;
        public static double allScanHigh = 430000000;
        public static int allScanTime = 2000;

        public static int bandScanTime = 1000;

        public static int timeoutUsiExist = 30000;
        public static int timeoutUsiActive = 10000;
        public static int timeoutUsiPlay = 2000;

        public static int timeoutSsiExist = 20000;

        public static int timeoutRxTuned = 60000;

    }

    public static class telive
    {
        public enum addr_type : ushort
        {
            NULL = 0,
            SSI = 1,
            EVENT_LABEL = 2,
            USSI = 3,
            SMI = 4,
            SSI_EVENT = 5,
            SSI_USAGE = 6,
            SMI_EVENT = 7,
        };
        public enum freq_reason : ushort
        {
            NETINFO = 0,
            FREQINFO = 1,
            DLFREQ = 2,
            SCANNED = 3,
            UNKNOWN = 99
        }
        public enum rx_mode : ushort
        {
            OFF = 0, // receiver is considered disabled
            TUNED = 1, // receiver is tunned to a channel
            ALLSCAN = 2, // ALL receivers are progressively scanning defined range
            BANDSCAN = 3 // receiver is loop-scanning within currently tuned baseband
        }

        public class receiver
        {
            public ushort id { get; }
            public int afc { get; set; }
            public double freq { get { return gnuradio.basefreq + offset; } }
            public double freqFix;
            public double offset;
            public DateTime lastseen { get; set; }
            public DateTime lastburst { get; set; }
            public rx_mode mode;
            public string state
            {
                get
                {
                    switch (mode)
                    {
                        case rx_mode.OFF:
                            return "OFF";

                        case rx_mode.TUNED:
                            return "tuned";

                        case rx_mode.ALLSCAN:
                            return "scan";

                        case rx_mode.BANDSCAN:
                            return "band scan";

                        default:
                            return "unknown";
                    }

                }
            }
            public uint master;

            public receiver(ushort newid)
            {
                id = newid;
                mode = rx_mode.OFF;
            }

            public bool getOffset()
            {
                // get current offset against baseband
                object offval = gnuradio.getval(string.Format("xlate_offset{0}", id));
                if (offval != null)
                {
                    double boffs = Convert.ToDouble(offval);
                    offset = boffs;
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public bool setOffset(double noff)
            {
                // set current offset against baseband
                bool vysl = gnuradio.setval(string.Format("xlate_offset{0}", id), noff);
                if (vysl)
                {
                    offset = noff;
                }
                return vysl;
            }

            public void setAfc(int newafc)
            {
                // just log AFC value
                afc = newafc;
            }

            public void seenBurst()
            {
                // a burst has been received on the receiver
                lastburst = DateTime.Now;
            }

            public void seen()
            {
                // receiver is seen alive (whether tuned to a signal or not)
                lastseen = DateTime.Now;
            }

            public bool tuneOOR()
            {
                // make the receiver quiet by tuning it out of receiving range
                return setOffset(gnuradio.samp_rate);
            }

            public void setMode(rx_mode nmode)
            {
                // set mode of receiver
                mode = nmode;
                if (mode == rx_mode.OFF)
                {
                    lastburst = DateTime.MinValue;
                }
            }

            public bool setFreq(double nfreq)
            {
                // just overlay method for setting offset
                double noff = nfreq - gnuradio.basefreq;
                return setOffset(noff);
            }

            public void updateFix()
            {
                // what we have is what we want
                freqFix = freq;
            }

            public bool applyFix()
            {
                // tune receiver to what we want
                bool vysledek = true;
                if (freq != freqFix)
                {
                    vysledek = setFreq(freqFix);
                }
                return vysledek;
            }

        }
        public class usi
        {
            public ushort id { get; }
            public List<ssi> ssis;
            public string ssilist { get { return string.Join(", ", ssis.Select(w => w.id).ToArray()); } }
            public bool encr { get; set; }
            public bool active { get; set; }
            public bool playing { get; set; }
            public DateTime lastseen { get; set; }
            public DateTime lastplay { get; set; }
            public ushort cid { get; set; }
            public ushort lastrx { get; set; }
            public acelp spl;

            public usi(ushort newid)
            {
                id = newid;
                ssis = new List<ssi>();
                encr = false;
                active = false;
                playing = false;
                spl = new acelp(id);
            }

            public void seen(ushort rx)
            {
                // this USI has been seen on receiver rx
                lastseen = DateTime.Now;
                lastrx = rx;
                activate();
            }

            public void addssi(uint nssi)
            {
                // a SSI has been seen
                if (nssi == 0) return;

                if (!ssis.Any(w => w.id == nssi))
                {
                    ssi nsio = new ssi(nssi);
                    nsio.seen();
                    ssis.Add(nsio);
                }
            }

            public void removessi(uint ossi)
            {
                // ssi is being forgotten
                ssis.RemoveAll(w => w.id == ossi);
            }

            public void addcid(ushort ncid)
            {
                // calledid has been seen
                if (ncid == 0) return;

                cid = ncid;
            }

            public void activate()
            {
                // there is an activity on this USI
                active = true;
            }

            public void play()
            {
                // there is a voice comming from this USI
                playing = true;
                lastplay = DateTime.Now;
            }
            
            public void stop()
            {
                // voice ceased
                playing = false;
                spl.listenClose();
            }

            public void deactivate()
            {
                // USI forgotten
                active = false;
                spl.recordClose();
            }

        }
        public class ssi
        {
            public uint id;
            public DateTime lastseen;
            public ssi(uint nid)
            {
                id = nid;
            }

            public void seen()
            {
                // SSI activity
                lastseen = DateTime.Now;
            }
        }
        public class frequency
        {
            public uint mcc { get; }
            public uint mnc { get; }
            public double dl_freq { get; }
            public double ul_freq { get; }
            public List<uint> la;
            public string lalist { get { return string.Join(", ", la.ToArray()); } }
            public List<ushort> ccode;
            public string cclist { get { return string.Join(", ", ccode.ToArray()); } }
            public freq_reason reason;
            public ushort tunedrx { get; set; }
            public DateTime lastuned;

            public frequency(uint nmcc, uint nmnc, double dlf, double ulf, uint nla, ushort ncc, freq_reason nreason)
            {
                mcc = nmcc;
                mnc = nmnc;
                dl_freq = dlf;
                ul_freq = ulf;
                la = new List<uint> { nla };
                ccode = new List<ushort> { ncc };
                reason = nreason;
                tunedrx = 0;
            }

            public void tuned(ushort rx)
            {
                // frequency has been tuned on receiver
                tunedrx = rx;
                lastuned = DateTime.Now;
            }

            public void detuned()
            {
                // frequency has been detuned form receiver
                tunedrx = 0;
            }

            public void addla(uint nla)
            {
                // detected new LA for this frequency
                if (!la.Contains(nla))
                    la.Add(nla);
            }

            public void addcc(ushort ncc)
            {
                // detected new CCODE for this frequency
                if (!ccode.Contains(ncc))
                    ccode.Add(ncc);
            }

        }
        public class allScanner
        {
            // all receivers scanning defined range
            public double startfreq;
            public double stopfreq;
            private System.Timers.Timer tmrScan = new System.Timers.Timer(settings.allScanTime);
            private object scanLock;

            public allScanner(double lf, double uf)
            {
                startfreq = lf;
                stopfreq = uf;
                scanLock = new object();
            }

            public void start()
            {
                lock (scanLock)
                {
                    for (ushort i = 1; i <= gnuradio.receivers; i++)
                    {
                        receiver rx = rxs.FirstOrDefault(w => w.id == i);
                        rx.setMode(rx_mode.OFF);
                    }
                    double ufreq = startfreq + (gnuradio.receivers - 1) * gnuradio.cbw;
                    double nbase = (ufreq + startfreq) / 2;

                    baseSetSafe(nbase);

                    for (ushort i = 1; i <= gnuradio.receivers; i++)
                    {
                        if (rxTune(i, startfreq + (i - 1) * gnuradio.cbw))
                            rxs.FirstOrDefault(w => w.id == i).setMode(rx_mode.ALLSCAN);
                    }
                }
                tmrScan.Elapsed += new ElapsedEventHandler(tmrScan_tick);
                tmrScan.Enabled = true;

            }

            public void step()
            {
                double nlfreq, nufreq;
                lock (scanLock)
                {
                    double lfreq = rxs.FirstOrDefault(w => w.id == 1).freq;
                    double ufreq = rxs.FirstOrDefault(w => w.id == gnuradio.receivers).freq;
                    nlfreq = lfreq + gnuradio.receivers * gnuradio.cbw;
                    nufreq = ufreq + gnuradio.receivers * gnuradio.cbw;
                    double nbase = (nufreq + nlfreq) / 2;
                    baseSetSafe(nbase);
                }

                if (nufreq >= stopfreq)
                {
                    stop();
                }
            }

            public void stop()
            {
                lock (scanLock)
                {
                    tmrScan.Enabled = false;
                    foreach (receiver rx in rxs.Where(w => w.mode == rx_mode.ALLSCAN))
                    {
                        rx.setMode(rx_mode.OFF);
                    }
                }
            }

            public void tmrScan_tick(object source, ElapsedEventArgs e)
            {
                tetraRx.ctx.Send(delegate
                {
                    step();
                }, null);
            }
        }
        public class bandScanner
        {
            public ushort scanRx;
            private System.Timers.Timer tmrScan = new System.Timers.Timer(settings.bandScanTime);
            private object scanLock;

            public bandScanner(ushort rx)
            {
                scanRx = rx;
                scanLock = new object();
            }

            public void start()
            {
                lock (scanLock)
                {
                    receiver scanner = rxs.FirstOrDefault(w => w.id == scanRx);
                    scanner.mode = rx_mode.BANDSCAN;
                    double startf = gnuradio.lowestValidFreq();
                    rxTune(scanner.id, startf);

                    tmrScan.Elapsed += new ElapsedEventHandler(tmrScan_tick);
                    tmrScan.Enabled = true;
                }
            }

            public void stop()
            {
                lock (scanLock)
                {
                    tmrScan.Enabled = false;
                }
                rxs.FirstOrDefault(w => w.id == scanRx).setMode(rx_mode.OFF);
            }

            public void step()
            {
                lock (scanLock)
                {
                    receiver scanner = rxs.FirstOrDefault(w => w.id == scanRx);

                    double nf = scanner.freq + gnuradio.cbw;

                    if (!gnuradio.freqFitsToBase(nf))
                    {
                        nf = gnuradio.lowestValidFreq();
                    }
                    rxTune(scanner.id, nf);

                }
            }

            public void tmrScan_tick(object source, ElapsedEventArgs e)
            {
                tetraRx.ctx.Send(delegate
                {
                    step();
                }, null);
            }
        }

        public static BindingList<receiver> rxs = new BindingList<receiver>();
        public static BindingList<usi> usis = new BindingList<usi>();
        public static BindingList<frequency> freqs= new BindingList<frequency>();
        public static bandScanner bas;
        public static allScanner als;
        public static System.Timers.Timer janitor = new System.Timers.Timer(500);
        public static frmFreq ff;
        public static frmMain fm;
        public static WaveFormat wf;
        public static MixingSampleProvider msp;
        public static IWavePlayer wout;

        public static void startAll()
        {
            createAllUsis(63);

            rxEnumAll();
            tetraRx.Start();

            janitorStart();

            audioInit();

        }

        public static void createAllUsis(ushort max)
        {
            for (ushort i = 0; i <= max;i++)
            {
                usi newus = new usi(i);
                usis.Add(newus);
            }
        }

        public static void audioInit()
        {
            wf = WaveFormat.CreateIeeeFloatWaveFormat(8000, 1);
            msp = new MixingSampleProvider(wf);
            wout = new WaveOutEvent();
            msp.ReadFully = true;
            wout.Init(msp);
            wout.Play();
        }

        public static void janitorStart()
        {
            janitor.Elapsed += new ElapsedEventHandler(janitorCall);
            janitor.Enabled = true;
        }

        public static void janitorCall(object source, ElapsedEventArgs e)
        {
            tetraRx.ctx.Send(delegate
            {
                // internal structures cleanup
                timeoutUsi();
                timeoutRx();
                timeoutSsi();
                if (ff.chkTuneNew.Checked)
                    tuneAllFree();

                // gui handle
                ff.refreshGrds();
                if (!ff.txtBase.Focused)
                    ff.txtBase.Text = string.Format("{0}", gnuradio.basefreq);
            }, null);
        }

        public static void formsInit(frmMain orig)
        {
            fm = orig;

            ff = new frmFreq();
            ff.Show();
        }

        public static void rxEnumAll()
        {
            for (ushort i = 1; i <= gnuradio.receivers; i++)
            {
                receiver newrx = new receiver(i);
                newrx.getOffset();
                rxs.Add(newrx);
            }
        }

        public static void timeoutUsi()
        {
            DateTime ted = DateTime.Now;

            //List<usi> delQue = new List<usi>();
            foreach (usi ucho in usis)
            {
                if (ucho.playing)
                {
                    // stop anything playing more than 2s without audio signal
                    TimeSpan splay = ted - ucho.lastplay;
                    if (splay.TotalMilliseconds > settings.timeoutUsiPlay)
                        ucho.stop();
                }

                TimeSpan sping = ted - ucho.lastseen;
                if (ucho.active)
                {
                    // deactivate anything without SSI activity for more than 10s
                    if (sping.TotalMilliseconds > settings.timeoutUsiActive)
                        ucho.deactivate();
                }
                else
                {
                    // destroy anything with inactivity for more than 30s
                    if (sping.TotalMilliseconds > settings.timeoutUsiExist)
                    {
                        ucho.lastrx = 0;
                        ucho.cid = 0;
                    }

                }
            }


        }

        public static void timeoutSsi()
        {
            DateTime ted = DateTime.Now;

            foreach (usi ucho in usis.Where(w => !w.active))
            {
                ucho.ssis.RemoveAll(w => (ted - w.lastseen).TotalMilliseconds > settings.timeoutSsiExist);
            }
        }

        public static void timeoutRx()
        {
            DateTime ted = DateTime.Now;

            foreach (receiver rx in rxs.Where(w => w.mode == rx_mode.TUNED))
            {
                if (rx.lastburst == default(DateTime)) continue;

                TimeSpan sburst = ted - rx.lastburst;
                if (sburst.TotalMilliseconds > settings.timeoutRxTuned)
                {
                    rx.setMode(rx_mode.OFF);
                }
            }
        }

        public static void tuneAllFree()
        {
            int freeRxs = rxs.Count(w => w.mode == rx_mode.OFF);

            IEnumerable<double> untuned = freqs.Where(w => w.tunedrx == 0).Select(w => w.dl_freq).Distinct();
            IEnumerable<double> tuned = freqs.Where(w => w.tunedrx != 0).Select(w => w.dl_freq).Distinct();
            int untestedc = untuned.Count();

            if (untestedc > 0 && freeRxs > 0)
            {
                IEnumerable<double> freqstotry = Enumerable.Empty<double>();
                int found = 0;

                if (tuned.Count() > 0)
                {
                    // we already have some tuned freqs
                    double minfr = tuned.Min();
                    double maxfr = tuned.Max();
                    // so first try to tune those, that fit between the tuned range
                    freqstotry = untuned.Where(w => w >= minfr && w <= maxfr).Take(freeRxs);
                    untuned = untuned.Except(freqstotry);
                    found = freqstotry.Count();
                }

                if (found < freeRxs)
                {
                    // then check those who will not require base freq retune
                    IEnumerable<double> smore = untuned.Where(w => gnuradio.freqFitsToBase(w)).Take(freeRxs - found);
                    freqstotry = freqstotry.Concat(smore);
                    untuned = untuned.Except(smore);
                    found = freqstotry.Count();
                }

                // here we have new candidates of maximum exact count of free receivers
                foreach (double frtotune in freqstotry)
                {
                    receiver trx = rxs.FirstOrDefault(w => w.mode == rx_mode.OFF);
                    rxTune(trx.id, frtotune);
                    trx.setMode(rx_mode.TUNED);
                    
                }

                // TODO: something fishy below here, it's tunning to aggresively even if it
                // only should tune if we won't lose tuned channels due to baseband change
                
                // if we still have some more channels to try
                foreach (double frtotune in untuned)
                {
                    receiver frx = rxs.FirstOrDefault(w => w.mode == rx_mode.OFF);
                    if (frx == null)
                    {
                        // ran out of free rxs
                        break;
                    }

                    // safely tune the frequency (e.g. you may shift baseband, but not so much that we lose channels)
                    rxTuneSafe(frx.id, frtotune);
                }
            }
        }

        public static void bandScanStart(ushort rxid)
        {
            if (bas == null)
            {
                bas = new bandScanner(rxid);
                bas.start();
            }
        }

        public static void bandScanStop()
        {
            if (bas != null)
            {
                bas.stop();
                bas = null;
            }
        }

        public static void allScanStart(double lf, double uf)
        {
            als = new allScanner(lf, uf);
            als.start();
        }

        public static void allScanStop()
        {
            als.stop();
            als = null;
        }

        public static bool rxTune(ushort rxid, double nfreq)
        {
            receiver trx = rxs.FirstOrDefault(w => w.id == rxid);
            if (trx.setFreq(nfreq))
            {
                foreach (frequency of in freqs.Where(w => w.tunedrx == rxid))
                {
                    // delete rx reference from prevs requencies
                    of.detuned();
                }
                foreach (frequency of in freqs.Where(w => w.dl_freq == nfreq))
                {
                    // add rx reference from current requenciy
                    of.tuned(rxid);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool rxTuneSafe(ushort rxid, double nfreq, bool force = false)
        {
            receiver trx = rxs.FirstOrDefault(w => w.id == rxid);
            bool vysledek = false;
            if (!gnuradio.freqFitsToBase(nfreq))
            {
                // find other active receivers (except the one we are tuning)
                IEnumerable<receiver> used = rxs.Where(w => w.mode == rx_mode.TUNED && w.id != rxid);
                IEnumerable<double> needed = used.Select(w => w.freq).Distinct();

                // calculate new base that includes everything (or at least as much as possible)
                double nbase = gnuradio.baseCalc(nfreq, needed, force);

                if (nbase != -1)
                {
                    // turn the rx off temporarily
                    trx.setMode(rx_mode.OFF);
                    // set the new base
                    vysledek = baseSetSafe(nbase);
                    // set new rx freq
                    vysledek &= rxTune(rxid, nfreq);
                    // turn it on again
                    trx.setMode(rx_mode.TUNED);
                }
            }
            else
            {
                // the required frequency is withing the baseband, just tune it
                vysledek = rxTune(rxid, nfreq);
                // turn on the rx
                trx.setMode(rx_mode.TUNED);
            }
            return vysledek;

        }

        public static bool rxApplyFix(IEnumerable<receiver> rtr)
        {
            // alter receivers so they stay on their declared freq
            bool vysledek = true;
            foreach (receiver rtrx in rtr)
            {
                if (gnuradio.freqFitsToBase(rtrx.freqFix))
                {
                    // if the receiver is close enough to base then just re-apply the freq
                    vysledek &= rtrx.applyFix();
                }
                else
                {
                    // if it's too far then disable it
                    rtrx.setMode(rx_mode.OFF);
                }
            }
            return vysledek;
        }

        public static void rxUpdateFix(IEnumerable<receiver> rtr)
        {
            // alter receivers so they stay on their declared freq
            foreach (receiver rtrx in rtr)
            {
                // if the receiver is close enough to base then just re-apply the freq
                rtrx.updateFix();
            }
        }

        public static bool baseCenter()
        {
            bool vysledek = true;

            IEnumerable<receiver> used = rxs.Where(w => w.mode == rx_mode.TUNED);
            IEnumerable<double> usedfr = used.Select(w => w.freq).Distinct();

            // calc new optimal frequency
            double optim = gnuradio.freqsCenter(usedfr);
            if (optim != -1 && optim != gnuradio.basefreq)
            {
                // if we got some and it's different from current
                vysledek &= gnuradio.baseSet(optim);
                vysledek &= rxApplyFix(used);
            }

            return vysledek;
        }

        public static bool baseSetSafe(double nbase)
        {
            // set the new base
            gnuradio.baseSet(nbase);
            // change all online channels
            rxApplyFix(rxs.Where(w => w.mode == rx_mode.TUNED));
            // calculate all offline channels
            rxUpdateFix(rxs.Where(w => w.mode != rx_mode.TUNED));

            return true;
        }

        public static void rxChangeModeHelper(ushort rxid)
        {
            receiver rxo = rxs.FirstOrDefault(w => w.id == rxid);
            rx_mode old = rxo.mode;
            switch (old)
            {
                case rx_mode.BANDSCAN:
                    bandScanStop();
                    break;

                case rx_mode.ALLSCAN:
                    allScanStop();
                    break;
            }


        }

        public static bool rxChangeModeScanband(ushort rxid)
        {
            if (rxs.Any(w => w.mode == rx_mode.BANDSCAN))
            {
                // there is already one bandscanner, we don't need more
                return false;
            }
            else {
                rxChangeModeHelper(rxid);
                bandScanStart(rxid);
                return true;
            }
        }

        public static void rxChangeModeScan(double lf, double uf)
        {
            //rxChangeModeHelper()

            als = new allScanner(lf, uf);
            als.start();
        }

        public static void rxChangeModeTuned(ushort rxid)
        {
            rxChangeModeHelper(rxid);
            receiver rxo = rxs.FirstOrDefault(w => w.id == rxid);
            rxo.setMode(rx_mode.TUNED);

            foreach (frequency fr in freqs.Where(w => w.dl_freq == rxo.freq))
            {
                fr.tunedrx = rxid;
            }


        }

        public static void rxChangeModeOff(ushort rxid)
        {
            rxChangeModeHelper(rxid);
            receiver rxo = rxs.FirstOrDefault(w => w.id == rxid);
            rxo.setMode(rx_mode.OFF);

            foreach (frequency fr in freqs.Where(w => w.tunedrx == rxid))
            {
                fr.tunedrx = 0;
            }

        }

        private static void cidaddssi(ushort cid, uint ssi)
        {
            foreach (usi usak in usis.Where(w => w.cid == cid))
            {
                usak.addssi(ssi);
            }
        }

        private static Dictionary<string, string> TETelements(string buf)
        {
            string[] slova = buf.Split(' ');
            Dictionary<string, string> TETstat = new Dictionary<string, string>();
            foreach (string slovo in slova)
            {
                if (slovo.Contains(":"))
                {
                    int dvojtec = slovo.IndexOf(':');
                    string parametr = slovo.Substring(0, dvojtec);
                    string hodnota = slovo.Substring(dvojtec + 1);

                    TETstat.Add(parametr, hodnota);
                }
            }
            return TETstat;

        }

        public static void parseUdp(byte[] prebuf)
        {
            // this gets executed in gui thread
            string buf = Encoding.ASCII.GetString(prebuf);

            if (buf.Contains("TETMON_begin"))
            {
                // control message
                if (buf.Contains("TETMON_end"))
                {
                    parseStat(buf);
                }
                else
                {
                    // error: line starts but doesnt end
                }
            }
            else
            {
                // data payload
                if (buf.Length == 1393)
                {
                    parseTraffic(prebuf);
                }
                else
                {
                    // error: small frame
                }
            }

        }

        private static void parseStat(string buf)
        {
            Dictionary<string, string> TETstat = TETelements(buf);

            ushort rx = 0;
            receiver rxo = null;
            if (TETstat.ContainsKey("RX"))
            {
                rx = ushort.Parse(TETstat["RX"]);
                rxo = rxs.FirstOrDefault(w => w.id == rx);
            }
            if (rxo == null) return;

            rxo.seen();

            string func = (TETstat.ContainsKey("FUNC") ? TETstat["FUNC"] : "");

            Dictionary<rx_mode, List<string>> allowed = new Dictionary<rx_mode, List<string>>();
            allowed.Add(rx_mode.OFF, new List<string> { "AFCVAL" });
            allowed.Add(rx_mode.ALLSCAN, new List<string> { "BURST", "AFCVAL", "NETINFO1", "FREQINFO1", "FREQINFO2" });
            allowed.Add(rx_mode.BANDSCAN, new List<string> { "BURST", "AFCVAL", "NETINFO1", "FREQINFO1", "FREQINFO2" });
            allowed.Add(rx_mode.TUNED, new List<string> { "BURST", "AFCVAL", "NETINFO1", "FREQINFO1", "FREQINFO2", "DSETUPDEC", "DCONNECTDEC", "DTXGRANTDEC", "SDSDEC", "D-SETUP", "D-CONNECT", "D-RELEASE" });

            if (!allowed[rxo.mode].Contains(func))
                return;

            ushort idx;
            usi idxo = null;
            if (TETstat.ContainsKey("IDX"))
            {
                idx = ushort.Parse(TETstat["IDX"]);
                idxo = usis.FirstOrDefault(w => w.id == idx);
                if (rxo.mode == rx_mode.TUNED)
                {
                    idxo.seen(rx);
                }
            }

            uint ssi1 = (TETstat.ContainsKey("SSI") ? uint.Parse(TETstat["SSI"]) : 0);
            uint ssi2 = (TETstat.ContainsKey("SSI2") ? uint.Parse(TETstat["SSI2"]) : 0);
            ushort cid = (TETstat.ContainsKey("CID") ? ushort.Parse(TETstat["CID"]) : (ushort)0);
            addr_type idt = (TETstat.ContainsKey("IDT") ? (addr_type)ushort.Parse(TETstat["IDT"]) : addr_type.NULL);
            uint mcc = (TETstat.ContainsKey("MCC") ? uint.Parse(TETstat["MCC"], NumberStyles.HexNumber) : 0);
            uint mnc = (TETstat.ContainsKey("MNC") ? uint.Parse(TETstat["MNC"], NumberStyles.HexNumber) : 0);
            double dl_freq = (TETstat.ContainsKey("DLF") ? double.Parse(TETstat["DLF"]) : 0);
            double ul_freq = (TETstat.ContainsKey("ULF") ? double.Parse(TETstat["ULF"]) : 0);
            uint la = (TETstat.ContainsKey("LA") ? uint.Parse(TETstat["LA"], NumberStyles.HexNumber) : 0);
            ushort ccode = (TETstat.ContainsKey("CCODE") ? ushort.Parse(TETstat["CCODE"], NumberStyles.HexNumber) : (ushort)0);
            int afc = (TETstat.ContainsKey("AFC") ? int.Parse(TETstat["AFC"]) : 0);

            switch (func)
            {
                case "BURST":
                    rxo.seenBurst();
                    break;

                case "AFCVAL":
                    rxo.setAfc(afc);
                    break;

                case "NETINFO1":
                case "FREQINFO1":
                case "FREQINFO2":
                    if (mcc == 0 || mnc == 0 || dl_freq == 0) break;
                    freq_reason fr = freq_reason.UNKNOWN;
                    switch(func)
                    {
                        case "NETINFO1": fr = freq_reason.NETINFO; break;
                        case "FREQINFO1": fr = freq_reason.FREQINFO; break;
                        case "FREQINFO2": fr = freq_reason.DLFREQ; break;
                    }
                    if (rxo.mode == rx_mode.ALLSCAN || rxo.mode == rx_mode.BANDSCAN)
                    {
                        // note that the channel was discovered during scanning
                        fr = freq_reason.SCANNED;
                    }

                    frequency newfreq = new frequency(mcc, mnc, dl_freq, ul_freq, la, ccode, fr);

                    frequency oldfreq = freqs.FirstOrDefault(w => w.dl_freq == dl_freq && w.ul_freq == ul_freq);

                    receiver trxo = rxs.FirstOrDefault(w => w.freq == dl_freq && w.id == rx);

                    if (oldfreq == null)
                    {
                        if (trxo != null)
                            newfreq.tuned(trxo.id);
                        freqs.Add(newfreq);
                    }
                    else
                    {
                        if (trxo != null)
                            oldfreq.tuned(trxo.id);
                        oldfreq.addla(la);
                        oldfreq.addcc(ccode);
                        oldfreq.reason = fr;
                    }
                    break;

                case "DSETUPDEC":
                    if (idxo != null)
                    {
                        idxo.addssi(ssi1);
                        idxo.addssi(ssi2);
                    }

                    if (cid > 0)
                    {
                        cidaddssi(cid, ssi1);
                        cidaddssi(cid, ssi2);
                    }
                    break;

                case "DCONNECTDEC":
                    if (idxo != null)
                    {
                        idxo.addssi(ssi1);
                        idxo.addssi(ssi2);
                        if (cid > 0)
                        {
                            idxo.addcid(cid);
                        }
                    }

                    break;

                case "DTXGRANTDEC":
                    string txgr = TETstat["TXGRANT"];
                    if (cid > 0 && (txgr == "1" || txgr == "3"))
                    {
                        cidaddssi(cid, ssi1);
                        cidaddssi(cid, ssi2);
                    }
                    break;

                case "SDSDEC":
                    string ssisrc = TETstat["CallingSSI"];
                    string ssitrg = TETstat["CalledSSI"];
                    if (buf.Contains("DATA:"))
                    {
                        int poz = buf.IndexOf("DATA:[")+6;
                        string dat = buf.Substring(poz);
                        poz = dat.IndexOf("]");
                        dat = dat.Substring(0, poz);
                        fm.logMsg(dat);
                        
                    }
                    //string data = TETstat["DATA"];
                    //string lat = TETstat["lat"];
                    //string lon = TETstat["lon"];
                    // pokud contains Test. posli DATA
                    break;

                case "D-SETUP":
                case "D-CONNECT":
                    if (idt == addr_type.SSI_USAGE)
                    {
                        if (idxo != null)
                            idxo.addssi(ssi1);
                    }
                    break;

                case "D-RELEASE":
                    if (idxo != null)
                        idxo.removessi(ssi1);
                    break;

                default:
                    return;
            }

        }

        private static void parseTraffic(byte[] buf)
        {
            byte[] controlb = new byte[12];
            byte[] payload = new byte[1380];
            Array.Copy(buf, 0, controlb, 0, controlb.Length);
            Array.Copy(buf, 13, payload, 0, payload.Length);

            string control = Encoding.ASCII.GetString(controlb);


            Dictionary<string, string> TETstat = TETelements(control);
            ushort rx;
            receiver rxo = null;
            if (TETstat.ContainsKey("RX"))
            {
                rx = ushort.Parse(TETstat["RX"], NumberStyles.HexNumber);
                rxo = rxs.FirstOrDefault(w => w.id == rx);
                if (rxo == null)
                    // rx received but not exists
                    return;
                if (rxo.mode != rx_mode.TUNED)
                    // rx is muted or scanning
                    return;
            }
            else
            {
                // RX not received
                return;
            }

            rxo.seen();

            ushort idx;
            usi idxo = null;
            if (TETstat.ContainsKey("TRA"))
            {
                idx = ushort.Parse(TETstat["TRA"], NumberStyles.HexNumber);
                idxo = usis.FirstOrDefault(w => w.id == idx);
                idxo.seen(rx);
            }
            else
            {
                // no idx at all
                return;
            }

            idxo.play();
            idxo.spl.processAcelp(payload);

            // if ssis !encr

        }

    }

}

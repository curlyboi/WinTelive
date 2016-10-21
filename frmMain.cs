using System;
using System.Windows.Forms;
using System.Threading;

namespace wintelive
{
    public partial class frmMain : Form
    {
        frmFreq ff = new frmFreq();
        short verbosity = 0;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {


        }


        public void logMsg(string str, short verb = 0)
        {
            if (verb >= verbosity)
                txtDebug.Text += string.Format("[{0:HH:mm:ss}] {1}\r\n", DateTime.Now, str);
        }



        private void btnInitGNR_Click(object sender, EventArgs e)
        {
            tetraRx.ctx = SynchronizationContext.Current;

            gnuradio.init(txtHost.Text, ushort.Parse(txtPort.Text));
            telive.startAll();
            grpGnuradio.Enabled = false;
            telive.formsInit(this);


        }

    }
}

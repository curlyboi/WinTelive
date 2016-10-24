using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using wintelive;

namespace wintelive
{
    public partial class frmFreq : Form
    {

        public frmFreq()
        {
            InitializeComponent();
        }

        public void bindFreqs()
        {
            BindingSource bs = new BindingSource(telive.freqs.OrderBy(w => w.dl_freq), null);
            grdFreqs.DataSource = bs;
        }

        public void rebindFreqs()
        {
            int rScroll = grdFreqs.FirstDisplayedScrollingRowIndex;

            int rSel = -1;
            if (grdFreqs.SelectedRows.Count > 0)
                rSel = grdFreqs.SelectedRows[0].Index;

            bindFreqs();

            if (rSel != -1)
            {
                rSel = Math.Min(rSel, grdFreqs.Rows.Count - 1);
                grdFreqs.Rows[rSel].Selected = true;

            }

            rScroll = Math.Min(rScroll, grdFreqs.Rows.Count - 1);
            grdFreqs.FirstDisplayedScrollingRowIndex = rScroll;
        
        }

        public void bindUSIs()
        {
            BindingSource bs = new BindingSource(telive.usis.OrderBy(w => w.id), null);
            grdUSIs.DataSource = bs;
        }

        public void bindRXs()
        {
            BindingSource bs = new BindingSource(telive.rxs.OrderBy(w => w.id), null);
            grdRXs.DataSource = bs;
        }

        private void frmFreq_Load(object sender, EventArgs e)
        {
            bindFreqs();
            bindRXs();
            bindUSIs();
            tmrRefresh.Enabled = true;
        }

        private void grdRXs_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ushort rxid = 0;
            if (e.RowIndex != -1)
            {
                rxid = (ushort)grdRXs[0, e.RowIndex].Value;
            }
            switch (e.ColumnIndex)
            {
                case 2:
                    frmTune ft = new frmTune(rxid, 0);
                    ft.Show();
                    break;

                case 5:
                    frmMode fm = new frmMode(rxid);
                    fm.Show();
                    break;
            }
            
        }

        private void grdFreqs_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            double nfreq = (double)grdFreqs[2, e.RowIndex].Value;
            telive.receiver rx = telive.rxs.FirstOrDefault(w => w.mode == telive.rx_mode.OFF);
            ushort rxid = (rx != null ? rx.id : (ushort)0);
            frmTune ft = new frmTune(rxid, nfreq);
            ft.Show();
        }

        private void txtBase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                double nfreq = double.Parse(txtBase.Text);
                if (!telive.baseSetSafe(nfreq))
                {
                    MessageBox.Show("Baseband frequency could not be changed!", "Baseband change", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }

        }

        private void chkLockBase_CheckedChanged(object sender, EventArgs e)
        {
            txtBase.Enabled = !chkLockBase.Checked;
        }

        private void lblBasefreq_Doubleclick(object sender, EventArgs e)
        {
            DialogResult drs = MessageBox.Show("Center baseband frequency?", "Center baseband", MessageBoxButtons.YesNo);
            if (drs == DialogResult.Yes)
            {
                telive.baseCenter();
            }
        }

        private void btnTuneNew_Click(object sender, EventArgs e)
        {
            telive.tuneAllFree();
        }

        public void grdFreqSelect(double freq)
        {
            grdFreqs.ClearSelection();
            foreach (DataGridViewRow radek in grdFreqs.Rows)
            {
                if ((double)radek.Cells[2].Value == freq)
                {
                    radek.Selected = true;
                    grdFreqs.FirstDisplayedScrollingRowIndex = radek.Index;
                }
            }
        }

        public void grdRXsSelect(ushort rxid)
        {
            grdRXs.ClearSelection();
            grdRXs.Rows[rxid].Selected = true;
            grdRXs.FirstDisplayedScrollingRowIndex = rxid;
        }

        private void tmrRefresh_Tick(object sender, EventArgs e)
        {
            if (!txtBase.Focused)
                txtBase.Text = gnuradio.basefreq.ToString();

            rebindFreqs();
            grdRXs.Invalidate();
            bindUSIs();
            guiDrawGraph();

        }

        private void guiDrawGraph()
        {
            int ix = picFreqGraph.Width;
            int iy = picFreqGraph.Height;
            Bitmap bmp = new Bitmap(ix, iy);
            Graphics gr = Graphics.FromImage(bmp);
            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            int x1, x2;

            Brush bandBrush = new SolidBrush(Color.LightYellow);
            Pen bandCenterPen = new Pen(Color.Orange);
            Brush freqBrush = new SolidBrush(Color.Blue);
            Brush freqSlaveBrush = new SolidBrush(Color.LightBlue);
            Brush rxOffBrush = new SolidBrush(Color.FromArgb(128, 128, 128, 128));
            Brush rxTunedBrush = new SolidBrush(Color.Cyan);
            Brush rxScanBrush = new SolidBrush(Color.FromArgb(128, 255, 0, 0));
            Brush textBrush = new SolidBrush(Color.Black);
            Font rxFont = new Font(FontFamily.GenericSansSerif, 7);

            // baseband rect
            x1 = telive.guiFreqToX(gnuradio.lowRange());
            x2 = telive.guiFreqToX(gnuradio.HighRange()) - x1;
            Rectangle bandRect = new Rectangle(x1, 0, x2, iy - 1);
            gr.FillRectangle(bandBrush, bandRect);
            gr.DrawRectangle(bandCenterPen, bandRect);
            x1 = telive.guiFreqToX(gnuradio.basefreq);
            gr.DrawLine(bandCenterPen, x1, 1, x1, iy - 2);

            // frequencies
            foreach (telive.frequency fre in telive.freqs)
            {
                x1 = telive.guiFreqToX(fre.dl_freq - gnuradio.cbw / 2);
                x2 = telive.guiFreqToX(fre.dl_freq + gnuradio.cbw / 2) - x1;
                Rectangle freqRect = new Rectangle(x1, 10, x2, iy - 12);
                gr.FillRectangle((fre.mcc != 0 && fre.mnc != 0 ? freqBrush : freqSlaveBrush), freqRect);
            }

            // receivers
            foreach (telive.receiver rx in telive.rxs)
            {
                x1 = telive.guiFreqToX(rx.freq - gnuradio.cbw / 2);
                x2 = telive.guiFreqToX(rx.freq + gnuradio.cbw / 2) - x1;
                Rectangle rxRect = new Rectangle(x1, 20, x2, iy - 22);
                Brush thisbrush;
                switch (rx.mode)
                {
                    case telive.rx_mode.OFF: thisbrush = rxOffBrush; break;
                    case telive.rx_mode.TUNED: thisbrush = rxTunedBrush; break;
                    default: thisbrush = rxScanBrush; break;
                }
                gr.FillRectangle(thisbrush, rxRect);
                if (rx.mode == telive.rx_mode.TUNED || rx.mode == telive.rx_mode.BANDSCAN)
                {
                    x1 = telive.guiFreqToX(rx.freq);
                    gr.DrawString(rx.id.ToString(), rxFont, textBrush, x1 - 5, -1);
                }
            }

            picFreqGraph.Image = bmp;
        }

        private void tmrAutotune_Tick(object sender, EventArgs e)
        {
            telive.tuneAllFree();
        }
    }
}

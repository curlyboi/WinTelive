using System;
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
            BindingSource bs = new BindingSource(telive.freqs, null);
            grdFreqs.DataSource = bs;
        }

        public void bindUSIs()
        {
            BindingSource bs = new BindingSource(telive.usis, null);
            grdUSIs.DataSource = bs;
        }

        public void bindRXs()
        {
            BindingSource bs = new BindingSource(telive.rxs, null);
            grdRXs.DataSource = bs;
        }

        public void refreshGrds()
        {
            grdFreqs.Refresh();
            grdRXs.Refresh();
            grdUSIs.Refresh();
        }


        private void frmFreq_Load(object sender, EventArgs e)
        {
            bindFreqs();
            bindRXs();
            bindUSIs();
        }

        private void grdRXs_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 2:
                    ushort rxid = (ushort)grdRXs[0, e.RowIndex].Value;
                    frmTune ft = new frmTune(rxid, 0);
                    ft.Show();
                    break;

                case 5:
                    ushort rxid2 = (ushort)grdRXs[0, e.RowIndex].Value;
                    frmMode fm = new frmMode(rxid2);
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

        private void lblBasefreq_Click(object sender, EventArgs e)
        {
            DialogResult drs = MessageBox.Show("Center baseband frequency?", "Center baseband", MessageBoxButtons.YesNo);
            if (drs == DialogResult.Yes)
            {
                telive.baseCenter();
            }
        }
    }
}

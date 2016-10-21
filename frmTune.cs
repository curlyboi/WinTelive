using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace wintelive
{
    public partial class frmTune : Form
    {
        public frmTune(ushort rxid, double freq)
        {
            InitializeComponent();
            cmbRx.DataSource = telive.rxs;
            cmbRx.DisplayMember = "id";
            cmbRx.ValueMember = "id";
            if (rxid > 0)
            {
                cmbRx.SelectedValue = rxid;
            }
            if (freq > 0)
            {
                txtFreq.Text = string.Format("{0}", freq);
            }
            else
            {
                telive.receiver rxo = telive.rxs.FirstOrDefault(w => w.id == rxid);
                if (rxo != null)
                    txtFreq.Text = string.Format("{0}", rxo.freq);
            }
            
        }

        private void btnTune_Click(object sender, EventArgs e)
        {
            telive.receiver rx = (telive.receiver)cmbRx.SelectedItem;
            if (telive.rxTuneSafe(rx.id, double.Parse(txtFreq.Text), chkForce.Checked))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("Cannot tune receiver!", "Receiver tune", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

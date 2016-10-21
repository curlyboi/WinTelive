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
    public partial class frmMode : Form
    {
        public frmMode(ushort rxid)
        {
            InitializeComponent();
            cmbRx.DataSource = telive.rxs;
            cmbRx.DisplayMember = "id";
            cmbRx.ValueMember = "id";
            if (rxid > 0)
            {
                cmbRx.SelectedValue = rxid;
            }
            cmbMode.DataSource = Enum.GetValues(typeof(telive.rx_mode));
            telive.receiver rxo = telive.rxs.FirstOrDefault(w => w.id == rxid);
            if (rxo != null)
                cmbMode.SelectedItem = rxo.mode;
        }

        private void btnMode_Click(object sender, EventArgs e)
        {
            telive.receiver rx = (telive.receiver)cmbRx.SelectedItem;
            telive.rx_mode mode = (telive.rx_mode)cmbMode.SelectedItem;
            switch (mode)
            {
                case telive.rx_mode.BANDSCAN:
                    if (!telive.rxChangeModeScanband(rx.id))
                    {
                        MessageBox.Show("Some receiver is already in band scanning mode.");
                    }
                    break;

                case telive.rx_mode.ALLSCAN:
                    telive.rxChangeModeScan(settings.allScanLow, settings.allScanHigh);
                    break;

                case telive.rx_mode.OFF:
                    telive.rxChangeModeOff(rx.id);
                    break;

                case telive.rx_mode.TUNED:
                    telive.rxChangeModeTuned(rx.id);
                    break;
            }
            this.Close();
        }
    }
}

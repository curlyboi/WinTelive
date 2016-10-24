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


        private void btnInitGNR_Click(object sender, EventArgs e)
        {
            settings.allScanLow = double.Parse(txtRangeLow.Text);
            settings.allScanHigh = double.Parse(txtRangeHigh.Text);

            gnuradio.init(txtHost.Text, ushort.Parse(txtPort.Text));
            telive.startAll();
            grpGnuradio.Enabled = false;
            telive.ffInit(this);
            telive.fsInit();


        }

    }
}

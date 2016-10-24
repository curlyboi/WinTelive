using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wintelive
{
    public partial class frmSDS : Form
    {
        public frmSDS()
        {
            InitializeComponent();
        }

        public void addMsg(string data)
        {
            txtSDS.AppendText(string.Format("[{0:HH:mm:ss}] {1}{2}", DateTime.Now, data, Environment.NewLine));
        }

    }
}

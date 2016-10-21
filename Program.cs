using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Globalization;
using System.IO;

namespace wintelive
{
    static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }

       

    }
}

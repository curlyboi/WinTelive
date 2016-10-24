namespace wintelive
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpGnuradio = new System.Windows.Forms.GroupBox();
            this.txtUdp = new System.Windows.Forms.TextBox();
            this.lblUdp = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.lblHost = new System.Windows.Forms.Label();
            this.btnInitGNR = new System.Windows.Forms.Button();
            this.txtRangeHigh = new System.Windows.Forms.TextBox();
            this.txtRangeLow = new System.Windows.Forms.TextBox();
            this.lblRange = new System.Windows.Forms.Label();
            this.grpGnuradio.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpGnuradio
            // 
            this.grpGnuradio.Controls.Add(this.txtRangeHigh);
            this.grpGnuradio.Controls.Add(this.txtRangeLow);
            this.grpGnuradio.Controls.Add(this.lblRange);
            this.grpGnuradio.Controls.Add(this.txtUdp);
            this.grpGnuradio.Controls.Add(this.lblUdp);
            this.grpGnuradio.Controls.Add(this.txtPort);
            this.grpGnuradio.Controls.Add(this.txtHost);
            this.grpGnuradio.Controls.Add(this.lblPort);
            this.grpGnuradio.Controls.Add(this.lblHost);
            this.grpGnuradio.Controls.Add(this.btnInitGNR);
            this.grpGnuradio.Location = new System.Drawing.Point(12, 12);
            this.grpGnuradio.Name = "grpGnuradio";
            this.grpGnuradio.Size = new System.Drawing.Size(231, 221);
            this.grpGnuradio.TabIndex = 16;
            this.grpGnuradio.TabStop = false;
            this.grpGnuradio.Text = "GNU Radio";
            // 
            // txtUdp
            // 
            this.txtUdp.Location = new System.Drawing.Point(109, 72);
            this.txtUdp.Name = "txtUdp";
            this.txtUdp.Size = new System.Drawing.Size(100, 20);
            this.txtUdp.TabIndex = 22;
            this.txtUdp.Text = "7379";
            this.txtUdp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblUdp
            // 
            this.lblUdp.AutoSize = true;
            this.lblUdp.Location = new System.Drawing.Point(18, 75);
            this.lblUdp.Name = "lblUdp";
            this.lblUdp.Size = new System.Drawing.Size(78, 13);
            this.lblUdp.TabIndex = 21;
            this.lblUdp.Text = "UDP listen port";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(109, 46);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(100, 20);
            this.txtPort.TabIndex = 20;
            this.txtPort.Text = "42000";
            this.txtPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtHost
            // 
            this.txtHost.Location = new System.Drawing.Point(109, 20);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(100, 20);
            this.txtHost.TabIndex = 19;
            this.txtHost.Text = "127.0.0.1";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(18, 49);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(75, 13);
            this.lblPort.TabIndex = 18;
            this.lblPort.Text = "XML RPC port";
            // 
            // lblHost
            // 
            this.lblHost.AutoSize = true;
            this.lblHost.Location = new System.Drawing.Point(18, 23);
            this.lblHost.Name = "lblHost";
            this.lblHost.Size = new System.Drawing.Size(77, 13);
            this.lblHost.TabIndex = 17;
            this.lblHost.Text = "XML RPC host";
            // 
            // btnInitGNR
            // 
            this.btnInitGNR.Location = new System.Drawing.Point(149, 172);
            this.btnInitGNR.Name = "btnInitGNR";
            this.btnInitGNR.Size = new System.Drawing.Size(60, 28);
            this.btnInitGNR.TabIndex = 16;
            this.btnInitGNR.Text = "Connect";
            this.btnInitGNR.UseVisualStyleBackColor = true;
            this.btnInitGNR.Click += new System.EventHandler(this.btnInitGNR_Click);
            // 
            // txtRangeHigh
            // 
            this.txtRangeHigh.Location = new System.Drawing.Point(109, 137);
            this.txtRangeHigh.Name = "txtRangeHigh";
            this.txtRangeHigh.Size = new System.Drawing.Size(100, 20);
            this.txtRangeHigh.TabIndex = 25;
            this.txtRangeHigh.Text = "430000000";
            this.txtRangeHigh.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtRangeLow
            // 
            this.txtRangeLow.Location = new System.Drawing.Point(109, 111);
            this.txtRangeLow.Name = "txtRangeLow";
            this.txtRangeLow.Size = new System.Drawing.Size(100, 20);
            this.txtRangeLow.TabIndex = 24;
            this.txtRangeLow.Text = "420000000";
            this.txtRangeLow.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblRange
            // 
            this.lblRange.AutoSize = true;
            this.lblRange.Location = new System.Drawing.Point(18, 114);
            this.lblRange.Name = "lblRange";
            this.lblRange.Size = new System.Drawing.Size(90, 13);
            this.lblRange.TabIndex = 23;
            this.lblRange.Text = "Frequency range:";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(260, 248);
            this.Controls.Add(this.grpGnuradio);
            this.Name = "frmMain";
            this.Text = "WinTelive";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.grpGnuradio.ResumeLayout(false);
            this.grpGnuradio.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox grpGnuradio;
        private System.Windows.Forms.TextBox txtUdp;
        private System.Windows.Forms.Label lblUdp;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Label lblHost;
        private System.Windows.Forms.Button btnInitGNR;
        private System.Windows.Forms.TextBox txtRangeHigh;
        private System.Windows.Forms.TextBox txtRangeLow;
        private System.Windows.Forms.Label lblRange;
    }
}


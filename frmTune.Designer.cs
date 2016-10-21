namespace wintelive
{
    partial class frmTune
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
            this.lblRx = new System.Windows.Forms.Label();
            this.lblFreq = new System.Windows.Forms.Label();
            this.txtFreq = new System.Windows.Forms.TextBox();
            this.cmbRx = new System.Windows.Forms.ComboBox();
            this.btnTune = new System.Windows.Forms.Button();
            this.lblHz = new System.Windows.Forms.Label();
            this.chkForce = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblRx
            // 
            this.lblRx.AutoSize = true;
            this.lblRx.Location = new System.Drawing.Point(10, 18);
            this.lblRx.Name = "lblRx";
            this.lblRx.Size = new System.Drawing.Size(53, 13);
            this.lblRx.TabIndex = 0;
            this.lblRx.Text = "Receiver:";
            // 
            // lblFreq
            // 
            this.lblFreq.AutoSize = true;
            this.lblFreq.Location = new System.Drawing.Point(10, 45);
            this.lblFreq.Name = "lblFreq";
            this.lblFreq.Size = new System.Drawing.Size(57, 13);
            this.lblFreq.TabIndex = 1;
            this.lblFreq.Text = "Frequency";
            // 
            // txtFreq
            // 
            this.txtFreq.Location = new System.Drawing.Point(82, 42);
            this.txtFreq.Name = "txtFreq";
            this.txtFreq.Size = new System.Drawing.Size(93, 20);
            this.txtFreq.TabIndex = 2;
            this.txtFreq.Text = "";
            this.txtFreq.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cmbRx
            // 
            this.cmbRx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRx.FormattingEnabled = true;
            this.cmbRx.Location = new System.Drawing.Point(82, 15);
            this.cmbRx.Name = "cmbRx";
            this.cmbRx.Size = new System.Drawing.Size(121, 21);
            this.cmbRx.TabIndex = 3;
            // 
            // btnTune
            // 
            this.btnTune.Location = new System.Drawing.Point(73, 103);
            this.btnTune.Name = "btnTune";
            this.btnTune.Size = new System.Drawing.Size(80, 23);
            this.btnTune.TabIndex = 4;
            this.btnTune.Text = "Tune!";
            this.btnTune.UseVisualStyleBackColor = true;
            this.btnTune.Click += new System.EventHandler(this.btnTune_Click);
            // 
            // lblHz
            // 
            this.lblHz.AutoSize = true;
            this.lblHz.Location = new System.Drawing.Point(181, 45);
            this.lblHz.Name = "lblHz";
            this.lblHz.Size = new System.Drawing.Size(20, 13);
            this.lblHz.TabIndex = 5;
            this.lblHz.Text = "Hz";
            // 
            // chkForce
            // 
            this.chkForce.AutoSize = true;
            this.chkForce.Location = new System.Drawing.Point(13, 73);
            this.chkForce.Name = "chkForce";
            this.chkForce.Size = new System.Drawing.Size(210, 17);
            this.chkForce.TabIndex = 6;
            this.chkForce.Text = "Tune even if baseband need changing";
            this.chkForce.UseVisualStyleBackColor = true;
            // 
            // frmTune
            // 
            this.AcceptButton = this.btnTune;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(228, 138);
            this.Controls.Add(this.chkForce);
            this.Controls.Add(this.lblHz);
            this.Controls.Add(this.btnTune);
            this.Controls.Add(this.cmbRx);
            this.Controls.Add(this.txtFreq);
            this.Controls.Add(this.lblFreq);
            this.Controls.Add(this.lblRx);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTune";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tune receiver";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblRx;
        private System.Windows.Forms.Label lblFreq;
        private System.Windows.Forms.TextBox txtFreq;
        private System.Windows.Forms.ComboBox cmbRx;
        private System.Windows.Forms.Button btnTune;
        private System.Windows.Forms.Label lblHz;
        private System.Windows.Forms.CheckBox chkForce;
    }
}
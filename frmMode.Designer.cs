namespace wintelive
{
    partial class frmMode
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
            this.btnMode = new System.Windows.Forms.Button();
            this.cmbRx = new System.Windows.Forms.ComboBox();
            this.lblMode = new System.Windows.Forms.Label();
            this.lblRx = new System.Windows.Forms.Label();
            this.cmbMode = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnMode
            // 
            this.btnMode.Location = new System.Drawing.Point(68, 87);
            this.btnMode.Name = "btnMode";
            this.btnMode.Size = new System.Drawing.Size(80, 23);
            this.btnMode.TabIndex = 9;
            this.btnMode.Text = "Set!";
            this.btnMode.UseVisualStyleBackColor = true;
            this.btnMode.Click += new System.EventHandler(this.btnMode_Click);
            // 
            // cmbRx
            // 
            this.cmbRx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRx.FormattingEnabled = true;
            this.cmbRx.Location = new System.Drawing.Point(84, 15);
            this.cmbRx.Name = "cmbRx";
            this.cmbRx.Size = new System.Drawing.Size(121, 21);
            this.cmbRx.TabIndex = 8;
            // 
            // lblMode
            // 
            this.lblMode.AutoSize = true;
            this.lblMode.Location = new System.Drawing.Point(12, 45);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(34, 13);
            this.lblMode.TabIndex = 6;
            this.lblMode.Text = "Mode";
            // 
            // lblRx
            // 
            this.lblRx.AutoSize = true;
            this.lblRx.Location = new System.Drawing.Point(12, 18);
            this.lblRx.Name = "lblRx";
            this.lblRx.Size = new System.Drawing.Size(53, 13);
            this.lblRx.TabIndex = 5;
            this.lblRx.Text = "Receiver:";
            // 
            // cmbMode
            // 
            this.cmbMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMode.FormattingEnabled = true;
            this.cmbMode.Location = new System.Drawing.Point(84, 42);
            this.cmbMode.Name = "cmbMode";
            this.cmbMode.Size = new System.Drawing.Size(121, 21);
            this.cmbMode.TabIndex = 10;
            // 
            // frmMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(226, 132);
            this.Controls.Add(this.cmbMode);
            this.Controls.Add(this.btnMode);
            this.Controls.Add(this.cmbRx);
            this.Controls.Add(this.lblMode);
            this.Controls.Add(this.lblRx);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Switch receiver mode";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnMode;
        private System.Windows.Forms.ComboBox cmbRx;
        private System.Windows.Forms.Label lblMode;
        private System.Windows.Forms.Label lblRx;
        private System.Windows.Forms.ComboBox cmbMode;
    }
}
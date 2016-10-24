namespace wintelive
{
    partial class frmSDS
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
            this.txtSDS = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtSDS
            // 
            this.txtSDS.Location = new System.Drawing.Point(12, 12);
            this.txtSDS.Multiline = true;
            this.txtSDS.Name = "txtSDS";
            this.txtSDS.ReadOnly = true;
            this.txtSDS.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSDS.Size = new System.Drawing.Size(588, 356);
            this.txtSDS.TabIndex = 0;
            // 
            // frmSDS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 380);
            this.Controls.Add(this.txtSDS);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSDS";
            this.Text = "SDS Messages";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSDS;
    }
}
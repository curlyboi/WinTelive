namespace wintelive
{
    partial class frmFreq
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grdFreqs = new System.Windows.Forms.DataGridView();
            this.mccDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mncDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dlfreqDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ulfreqDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lalistDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cclistDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tunedrxDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tfreqBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.grdUSIs = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ssilistDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.encrDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.activeDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.playingDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastrxDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.usiBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.grdRXs = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.afcDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.freqDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastseenDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastburstDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.state = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.receiverBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblUSIs = new System.Windows.Forms.Label();
            this.lblRX = new System.Windows.Forms.Label();
            this.lblFreqs = new System.Windows.Forms.Label();
            this.lblHz = new System.Windows.Forms.Label();
            this.lblBasefreq = new System.Windows.Forms.Label();
            this.chkLockBase = new System.Windows.Forms.CheckBox();
            this.chkTuneNew = new System.Windows.Forms.CheckBox();
            this.txtBase = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.grdFreqs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tfreqBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdUSIs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.usiBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdRXs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.receiverBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // grdFreqs
            // 
            this.grdFreqs.AllowUserToAddRows = false;
            this.grdFreqs.AllowUserToDeleteRows = false;
            this.grdFreqs.AllowUserToResizeRows = false;
            this.grdFreqs.AutoGenerateColumns = false;
            this.grdFreqs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdFreqs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdFreqs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.mccDataGridViewTextBoxColumn,
            this.mncDataGridViewTextBoxColumn,
            this.dlfreqDataGridViewTextBoxColumn,
            this.ulfreqDataGridViewTextBoxColumn,
            this.lalistDataGridViewTextBoxColumn,
            this.cclistDataGridViewTextBoxColumn,
            this.tunedrxDataGridViewTextBoxColumn});
            this.grdFreqs.DataSource = this.tfreqBindingSource;
            this.grdFreqs.Location = new System.Drawing.Point(465, 327);
            this.grdFreqs.MultiSelect = false;
            this.grdFreqs.Name = "grdFreqs";
            this.grdFreqs.ReadOnly = true;
            this.grdFreqs.RowHeadersVisible = false;
            this.grdFreqs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.grdFreqs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdFreqs.Size = new System.Drawing.Size(403, 191);
            this.grdFreqs.TabIndex = 2;
            this.grdFreqs.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdFreqs_CellDoubleClick);
            // 
            // mccDataGridViewTextBoxColumn
            // 
            this.mccDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.mccDataGridViewTextBoxColumn.DataPropertyName = "mcc";
            this.mccDataGridViewTextBoxColumn.HeaderText = "MCC";
            this.mccDataGridViewTextBoxColumn.Name = "mccDataGridViewTextBoxColumn";
            this.mccDataGridViewTextBoxColumn.ReadOnly = true;
            this.mccDataGridViewTextBoxColumn.Width = 55;
            // 
            // mncDataGridViewTextBoxColumn
            // 
            this.mncDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.mncDataGridViewTextBoxColumn.DataPropertyName = "mnc";
            this.mncDataGridViewTextBoxColumn.HeaderText = "MNC";
            this.mncDataGridViewTextBoxColumn.Name = "mncDataGridViewTextBoxColumn";
            this.mncDataGridViewTextBoxColumn.ReadOnly = true;
            this.mncDataGridViewTextBoxColumn.Width = 56;
            // 
            // dlfreqDataGridViewTextBoxColumn
            // 
            this.dlfreqDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dlfreqDataGridViewTextBoxColumn.DataPropertyName = "dl_freq";
            this.dlfreqDataGridViewTextBoxColumn.HeaderText = "DLF";
            this.dlfreqDataGridViewTextBoxColumn.Name = "dlfreqDataGridViewTextBoxColumn";
            this.dlfreqDataGridViewTextBoxColumn.ReadOnly = true;
            this.dlfreqDataGridViewTextBoxColumn.Width = 52;
            // 
            // ulfreqDataGridViewTextBoxColumn
            // 
            this.ulfreqDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ulfreqDataGridViewTextBoxColumn.DataPropertyName = "ul_freq";
            this.ulfreqDataGridViewTextBoxColumn.HeaderText = "ULF";
            this.ulfreqDataGridViewTextBoxColumn.Name = "ulfreqDataGridViewTextBoxColumn";
            this.ulfreqDataGridViewTextBoxColumn.ReadOnly = true;
            this.ulfreqDataGridViewTextBoxColumn.Width = 52;
            // 
            // lalistDataGridViewTextBoxColumn
            // 
            this.lalistDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.lalistDataGridViewTextBoxColumn.DataPropertyName = "lalist";
            this.lalistDataGridViewTextBoxColumn.HeaderText = "LAs";
            this.lalistDataGridViewTextBoxColumn.Name = "lalistDataGridViewTextBoxColumn";
            this.lalistDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cclistDataGridViewTextBoxColumn
            // 
            this.cclistDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cclistDataGridViewTextBoxColumn.DataPropertyName = "cclist";
            this.cclistDataGridViewTextBoxColumn.HeaderText = "CCs";
            this.cclistDataGridViewTextBoxColumn.Name = "cclistDataGridViewTextBoxColumn";
            this.cclistDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tunedrxDataGridViewTextBoxColumn
            // 
            this.tunedrxDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.tunedrxDataGridViewTextBoxColumn.DataPropertyName = "tunedrx";
            this.tunedrxDataGridViewTextBoxColumn.HeaderText = "RX";
            this.tunedrxDataGridViewTextBoxColumn.Name = "tunedrxDataGridViewTextBoxColumn";
            this.tunedrxDataGridViewTextBoxColumn.ReadOnly = true;
            this.tunedrxDataGridViewTextBoxColumn.Width = 47;
            // 
            // tfreqBindingSource
            // 
            this.tfreqBindingSource.DataSource = typeof(wintelive.telive.frequency);
            // 
            // grdUSIs
            // 
            this.grdUSIs.AllowUserToAddRows = false;
            this.grdUSIs.AllowUserToDeleteRows = false;
            this.grdUSIs.AllowUserToResizeRows = false;
            this.grdUSIs.AutoGenerateColumns = false;
            this.grdUSIs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdUSIs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdUSIs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn1,
            this.ssilistDataGridViewTextBoxColumn,
            this.encrDataGridViewCheckBoxColumn,
            this.activeDataGridViewCheckBoxColumn,
            this.playingDataGridViewCheckBoxColumn,
            this.cidDataGridViewTextBoxColumn,
            this.lastrxDataGridViewTextBoxColumn});
            this.grdUSIs.DataSource = this.usiBindingSource;
            this.grdUSIs.Location = new System.Drawing.Point(12, 31);
            this.grdUSIs.MultiSelect = false;
            this.grdUSIs.Name = "grdUSIs";
            this.grdUSIs.ReadOnly = true;
            this.grdUSIs.RowHeadersVisible = false;
            this.grdUSIs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.grdUSIs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdUSIs.Size = new System.Drawing.Size(438, 487);
            this.grdUSIs.TabIndex = 0;
            // 
            // idDataGridViewTextBoxColumn1
            // 
            this.idDataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.idDataGridViewTextBoxColumn1.DataPropertyName = "id";
            this.idDataGridViewTextBoxColumn1.HeaderText = "ID";
            this.idDataGridViewTextBoxColumn1.Name = "idDataGridViewTextBoxColumn1";
            this.idDataGridViewTextBoxColumn1.ReadOnly = true;
            this.idDataGridViewTextBoxColumn1.Width = 43;
            // 
            // ssilistDataGridViewTextBoxColumn
            // 
            this.ssilistDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ssilistDataGridViewTextBoxColumn.DataPropertyName = "ssilist";
            this.ssilistDataGridViewTextBoxColumn.HeaderText = "SSIs";
            this.ssilistDataGridViewTextBoxColumn.Name = "ssilistDataGridViewTextBoxColumn";
            this.ssilistDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // encrDataGridViewCheckBoxColumn
            // 
            this.encrDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.encrDataGridViewCheckBoxColumn.DataPropertyName = "encr";
            this.encrDataGridViewCheckBoxColumn.HeaderText = "Encr";
            this.encrDataGridViewCheckBoxColumn.Name = "encrDataGridViewCheckBoxColumn";
            this.encrDataGridViewCheckBoxColumn.ReadOnly = true;
            this.encrDataGridViewCheckBoxColumn.Width = 35;
            // 
            // activeDataGridViewCheckBoxColumn
            // 
            this.activeDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.activeDataGridViewCheckBoxColumn.DataPropertyName = "active";
            this.activeDataGridViewCheckBoxColumn.HeaderText = "Active";
            this.activeDataGridViewCheckBoxColumn.Name = "activeDataGridViewCheckBoxColumn";
            this.activeDataGridViewCheckBoxColumn.ReadOnly = true;
            this.activeDataGridViewCheckBoxColumn.Width = 43;
            // 
            // playingDataGridViewCheckBoxColumn
            // 
            this.playingDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.playingDataGridViewCheckBoxColumn.DataPropertyName = "playing";
            this.playingDataGridViewCheckBoxColumn.HeaderText = "Playing";
            this.playingDataGridViewCheckBoxColumn.Name = "playingDataGridViewCheckBoxColumn";
            this.playingDataGridViewCheckBoxColumn.ReadOnly = true;
            this.playingDataGridViewCheckBoxColumn.Width = 47;
            // 
            // cidDataGridViewTextBoxColumn
            // 
            this.cidDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cidDataGridViewTextBoxColumn.DataPropertyName = "cid";
            this.cidDataGridViewTextBoxColumn.HeaderText = "CalledID";
            this.cidDataGridViewTextBoxColumn.Name = "cidDataGridViewTextBoxColumn";
            this.cidDataGridViewTextBoxColumn.ReadOnly = true;
            this.cidDataGridViewTextBoxColumn.Width = 72;
            // 
            // lastrxDataGridViewTextBoxColumn
            // 
            this.lastrxDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.lastrxDataGridViewTextBoxColumn.DataPropertyName = "lastrx";
            this.lastrxDataGridViewTextBoxColumn.HeaderText = "RX";
            this.lastrxDataGridViewTextBoxColumn.Name = "lastrxDataGridViewTextBoxColumn";
            this.lastrxDataGridViewTextBoxColumn.ReadOnly = true;
            this.lastrxDataGridViewTextBoxColumn.Width = 47;
            // 
            // usiBindingSource
            // 
            this.usiBindingSource.DataSource = typeof(wintelive.telive.usi);
            // 
            // grdRXs
            // 
            this.grdRXs.AllowUserToAddRows = false;
            this.grdRXs.AllowUserToDeleteRows = false;
            this.grdRXs.AllowUserToResizeRows = false;
            this.grdRXs.AutoGenerateColumns = false;
            this.grdRXs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdRXs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdRXs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.afcDataGridViewTextBoxColumn,
            this.freqDataGridViewTextBoxColumn,
            this.lastseenDataGridViewTextBoxColumn,
            this.lastburstDataGridViewTextBoxColumn,
            this.state});
            this.grdRXs.DataSource = this.receiverBindingSource;
            this.grdRXs.Location = new System.Drawing.Point(465, 31);
            this.grdRXs.MultiSelect = false;
            this.grdRXs.Name = "grdRXs";
            this.grdRXs.ReadOnly = true;
            this.grdRXs.RowHeadersVisible = false;
            this.grdRXs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.grdRXs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdRXs.Size = new System.Drawing.Size(403, 252);
            this.grdRXs.TabIndex = 1;
            this.grdRXs.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdRXs_CellDoubleClick);
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.idDataGridViewTextBoxColumn.DataPropertyName = "id";
            this.idDataGridViewTextBoxColumn.HeaderText = "ID";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.ReadOnly = true;
            this.idDataGridViewTextBoxColumn.Width = 43;
            // 
            // afcDataGridViewTextBoxColumn
            // 
            this.afcDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.afcDataGridViewTextBoxColumn.DataPropertyName = "afc";
            this.afcDataGridViewTextBoxColumn.HeaderText = "AFC";
            this.afcDataGridViewTextBoxColumn.Name = "afcDataGridViewTextBoxColumn";
            this.afcDataGridViewTextBoxColumn.ReadOnly = true;
            this.afcDataGridViewTextBoxColumn.Width = 52;
            // 
            // freqDataGridViewTextBoxColumn
            // 
            this.freqDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.freqDataGridViewTextBoxColumn.DataPropertyName = "freq";
            this.freqDataGridViewTextBoxColumn.HeaderText = "Frequency";
            this.freqDataGridViewTextBoxColumn.Name = "freqDataGridViewTextBoxColumn";
            this.freqDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // lastseenDataGridViewTextBoxColumn
            // 
            this.lastseenDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.lastseenDataGridViewTextBoxColumn.DataPropertyName = "lastseen";
            dataGridViewCellStyle3.Format = "HH:mm:ss";
            this.lastseenDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.lastseenDataGridViewTextBoxColumn.HeaderText = "Seen";
            this.lastseenDataGridViewTextBoxColumn.Name = "lastseenDataGridViewTextBoxColumn";
            this.lastseenDataGridViewTextBoxColumn.ReadOnly = true;
            this.lastseenDataGridViewTextBoxColumn.Width = 57;
            // 
            // lastburstDataGridViewTextBoxColumn
            // 
            this.lastburstDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.lastburstDataGridViewTextBoxColumn.DataPropertyName = "lastburst";
            dataGridViewCellStyle4.Format = "HH:mm:ss";
            this.lastburstDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.lastburstDataGridViewTextBoxColumn.HeaderText = "Burst";
            this.lastburstDataGridViewTextBoxColumn.Name = "lastburstDataGridViewTextBoxColumn";
            this.lastburstDataGridViewTextBoxColumn.ReadOnly = true;
            this.lastburstDataGridViewTextBoxColumn.Width = 56;
            // 
            // state
            // 
            this.state.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.state.DataPropertyName = "state";
            this.state.HeaderText = "State";
            this.state.Name = "state";
            this.state.ReadOnly = true;
            this.state.Width = 57;
            // 
            // receiverBindingSource
            // 
            this.receiverBindingSource.DataSource = typeof(wintelive.telive.receiver);
            // 
            // lblUSIs
            // 
            this.lblUSIs.AutoSize = true;
            this.lblUSIs.Location = new System.Drawing.Point(9, 9);
            this.lblUSIs.Name = "lblUSIs";
            this.lblUSIs.Size = new System.Drawing.Size(89, 13);
            this.lblUSIs.TabIndex = 3;
            this.lblUSIs.Text = "Usage Identifiers:";
            // 
            // lblRX
            // 
            this.lblRX.AutoSize = true;
            this.lblRX.Location = new System.Drawing.Point(462, 9);
            this.lblRX.Name = "lblRX";
            this.lblRX.Size = new System.Drawing.Size(58, 13);
            this.lblRX.TabIndex = 4;
            this.lblRX.Text = "Receivers:";
            // 
            // lblFreqs
            // 
            this.lblFreqs.AutoSize = true;
            this.lblFreqs.Location = new System.Drawing.Point(462, 305);
            this.lblFreqs.Name = "lblFreqs";
            this.lblFreqs.Size = new System.Drawing.Size(122, 13);
            this.lblFreqs.TabIndex = 5;
            this.lblFreqs.Text = "Discovered frequencies:";
            // 
            // lblHz
            // 
            this.lblHz.Location = new System.Drawing.Point(792, 9);
            this.lblHz.Name = "lblHz";
            this.lblHz.Size = new System.Drawing.Size(23, 13);
            this.lblHz.TabIndex = 6;
            this.lblHz.Text = "Hz";
            this.lblHz.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblBasefreq
            // 
            this.lblBasefreq.AutoSize = true;
            this.lblBasefreq.Location = new System.Drawing.Point(604, 9);
            this.lblBasefreq.Name = "lblBasefreq";
            this.lblBasefreq.Size = new System.Drawing.Size(108, 13);
            this.lblBasefreq.TabIndex = 7;
            this.lblBasefreq.Text = "Baseband frequency:";
            this.lblBasefreq.DoubleClick += new System.EventHandler(this.lblBasefreq_Click);
            // 
            // chkLockBase
            // 
            this.chkLockBase.AutoSize = true;
            this.chkLockBase.Location = new System.Drawing.Point(821, 8);
            this.chkLockBase.Name = "chkLockBase";
            this.chkLockBase.Size = new System.Drawing.Size(50, 17);
            this.chkLockBase.TabIndex = 8;
            this.chkLockBase.Text = "Lock";
            this.chkLockBase.UseVisualStyleBackColor = true;
            this.chkLockBase.CheckedChanged += new System.EventHandler(this.chkLockBase_CheckedChanged);
            // 
            // chkTuneNew
            // 
            this.chkTuneNew.AutoSize = true;
            this.chkTuneNew.Location = new System.Drawing.Point(654, 304);
            this.chkTuneNew.Name = "chkTuneNew";
            this.chkTuneNew.Size = new System.Drawing.Size(214, 17);
            this.chkTuneNew.TabIndex = 9;
            this.chkTuneNew.Text = "Tune new frequencies on free channels";
            this.chkTuneNew.UseVisualStyleBackColor = true;
            // 
            // txtBase
            // 
            this.txtBase.Location = new System.Drawing.Point(718, 6);
            this.txtBase.Name = "txtBase";
            this.txtBase.Size = new System.Drawing.Size(75, 20);
            this.txtBase.TabIndex = 10;
            this.txtBase.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBase.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBase_KeyDown);
            // 
            // frmFreq
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(883, 530);
            this.Controls.Add(this.txtBase);
            this.Controls.Add(this.chkTuneNew);
            this.Controls.Add(this.chkLockBase);
            this.Controls.Add(this.lblBasefreq);
            this.Controls.Add(this.lblHz);
            this.Controls.Add(this.lblFreqs);
            this.Controls.Add(this.lblRX);
            this.Controls.Add(this.lblUSIs);
            this.Controls.Add(this.grdRXs);
            this.Controls.Add(this.grdUSIs);
            this.Controls.Add(this.grdFreqs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmFreq";
            this.Text = "WinTelive monitor";
            this.Load += new System.EventHandler(this.frmFreq_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdFreqs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tfreqBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdUSIs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.usiBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdRXs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.receiverBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grdFreqs;
        private System.Windows.Forms.DataGridView grdUSIs;
        private System.Windows.Forms.BindingSource usiBindingSource;
        private System.Windows.Forms.DataGridView grdRXs;
        private System.Windows.Forms.BindingSource receiverBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ssilistDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn encrDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn activeDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn playingDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastrxDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource tfreqBindingSource;
        private System.Windows.Forms.Label lblUSIs;
        private System.Windows.Forms.Label lblRX;
        private System.Windows.Forms.Label lblFreqs;
        private System.Windows.Forms.DataGridViewTextBoxColumn mccDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mncDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dlfreqDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ulfreqDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lalistDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cclistDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tunedrxDataGridViewTextBoxColumn;
        private System.Windows.Forms.Label lblBasefreq;
        private System.Windows.Forms.CheckBox chkLockBase;
        public System.Windows.Forms.TextBox txtBase;
        private System.Windows.Forms.Label lblHz;
        public System.Windows.Forms.CheckBox chkTuneNew;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn afcDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn freqDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastseenDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastburstDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn state;
    }
}
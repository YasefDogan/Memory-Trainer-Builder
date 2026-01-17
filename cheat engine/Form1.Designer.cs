namespace cheat_engine
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabScan = new System.Windows.Forms.TabPage();
            this.cbProcesses = new System.Windows.Forms.ComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblProcess = new System.Windows.Forms.Label();
            this.lblValue = new System.Windows.Forms.Label();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.btnScan = new System.Windows.Forms.Button();
            this.lstResults = new System.Windows.Forms.ListBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.progressBarScan = new System.Windows.Forms.ProgressBar();
            this.btnRescan = new System.Windows.Forms.Button();
            this.lblWriteValue = new System.Windows.Forms.Label();
            this.txtWriteValue = new System.Windows.Forms.TextBox();
            this.btnWrite = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.tabUpload = new System.Windows.Forms.TabPage();
            this.lblProcessUpload = new System.Windows.Forms.Label();
            this.cbProcessesUpload = new System.Windows.Forms.ComboBox();
            this.btnRefreshUpload = new System.Windows.Forms.Button();
            this.lblUpload = new System.Windows.Forms.Label();
            this.txtUploadPath = new System.Windows.Forms.TextBox();
            this.btnBrowseUpload = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnLoadAddresses = new System.Windows.Forms.Button();
            this.flpAddressesUpload = new System.Windows.Forms.FlowLayoutPanel();
            this.lblStatusUpload = new System.Windows.Forms.Label();
            this.txtLogUpload = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControlMain.SuspendLayout();
            this.tabScan.SuspendLayout();
            this.tabUpload.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlMain
            // 
            this.tabControlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlMain.Controls.Add(this.tabScan);
            this.tabControlMain.Controls.Add(this.tabUpload);
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(800, 571);
            this.tabControlMain.TabIndex = 0;
            // 
            // tabScan
            // 
            this.tabScan.Controls.Add(this.label1);
            this.tabScan.Controls.Add(this.cbProcesses);
            this.tabScan.Controls.Add(this.btnRefresh);
            this.tabScan.Controls.Add(this.lblProcess);
            this.tabScan.Controls.Add(this.lblValue);
            this.tabScan.Controls.Add(this.txtValue);
            this.tabScan.Controls.Add(this.cbType);
            this.tabScan.Controls.Add(this.btnScan);
            this.tabScan.Controls.Add(this.lstResults);
            this.tabScan.Controls.Add(this.lblStatus);
            this.tabScan.Controls.Add(this.progressBarScan);
            this.tabScan.Controls.Add(this.btnRescan);
            this.tabScan.Controls.Add(this.lblWriteValue);
            this.tabScan.Controls.Add(this.txtWriteValue);
            this.tabScan.Controls.Add(this.btnWrite);
            this.tabScan.Controls.Add(this.btnExport);
            this.tabScan.Controls.Add(this.txtLog);
            this.tabScan.Location = new System.Drawing.Point(4, 25);
            this.tabScan.Name = "tabScan";
            this.tabScan.Size = new System.Drawing.Size(792, 542);
            this.tabScan.TabIndex = 0;
            this.tabScan.Text = "Tarama";
            this.tabScan.UseVisualStyleBackColor = true;
            this.tabScan.Click += new System.EventHandler(this.tabScan_Click);
            // 
            // cbProcesses
            // 
            this.cbProcesses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProcesses.Location = new System.Drawing.Point(12, 30);
            this.cbProcesses.Name = "cbProcesses";
            this.cbProcesses.Size = new System.Drawing.Size(420, 24);
            this.cbProcesses.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(440, 28);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 26);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Yenile";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblProcess
            // 
            this.lblProcess.Location = new System.Drawing.Point(12, 9);
            this.lblProcess.Name = "lblProcess";
            this.lblProcess.Size = new System.Drawing.Size(200, 15);
            this.lblProcess.TabIndex = 2;
            this.lblProcess.Text = "Process seçin:";
            // 
            // lblValue
            // 
            this.lblValue.Location = new System.Drawing.Point(12, 57);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(126, 24);
            this.lblValue.TabIndex = 3;
            this.lblValue.Text = "Aranacak değer:";
            this.lblValue.Click += new System.EventHandler(this.lblValue_Click);
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(12, 80);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(167, 22);
            this.txtValue.TabIndex = 4;
            // 
            // cbType
            // 
            this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType.Items.AddRange(new object[] {
            "Int",
            "Float32",
            "Float64",
            "Bytes (Hex)"});
            this.cbType.Location = new System.Drawing.Point(220, 79);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(150, 24);
            this.cbType.TabIndex = 5;
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(380, 77);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(75, 31);
            this.btnScan.TabIndex = 6;
            this.btnScan.Text = "Ara";
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // lstResults
            // 
            this.lstResults.ItemHeight = 16;
            this.lstResults.Location = new System.Drawing.Point(12, 114);
            this.lstResults.Name = "lstResults";
            this.lstResults.Size = new System.Drawing.Size(760, 260);
            this.lstResults.TabIndex = 7;
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(3, 425);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(150, 23);
            this.lblStatus.TabIndex = 8;
            this.lblStatus.Text = "Hazır";
            this.lblStatus.Click += new System.EventHandler(this.lblStatus_Click);
            // 
            // progressBarScan
            // 
            this.progressBarScan.Location = new System.Drawing.Point(146, 428);
            this.progressBarScan.Name = "progressBarScan";
            this.progressBarScan.Size = new System.Drawing.Size(600, 20);
            this.progressBarScan.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBarScan.TabIndex = 15;
            this.progressBarScan.Click += new System.EventHandler(this.progressBarScan_Click);
            // 
            // btnRescan
            // 
            this.btnRescan.Location = new System.Drawing.Point(12, 380);
            this.btnRescan.Name = "btnRescan";
            this.btnRescan.Size = new System.Drawing.Size(90, 42);
            this.btnRescan.TabIndex = 9;
            this.btnRescan.Text = "Tekrar Ara";
            this.btnRescan.Click += new System.EventHandler(this.btnRescan_Click);
            // 
            // lblWriteValue
            // 
            this.lblWriteValue.Location = new System.Drawing.Point(108, 390);
            this.lblWriteValue.Name = "lblWriteValue";
            this.lblWriteValue.Size = new System.Drawing.Size(120, 32);
            this.lblWriteValue.TabIndex = 10;
            this.lblWriteValue.Text = "Yazılacak değer :";
            this.lblWriteValue.Click += new System.EventHandler(this.lblWriteValue_Click);
            // 
            // txtWriteValue
            // 
            this.txtWriteValue.Location = new System.Drawing.Point(234, 390);
            this.txtWriteValue.Name = "txtWriteValue";
            this.txtWriteValue.Size = new System.Drawing.Size(136, 22);
            this.txtWriteValue.TabIndex = 11;
            // 
            // btnWrite
            // 
            this.btnWrite.Location = new System.Drawing.Point(394, 380);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(90, 42);
            this.btnWrite.TabIndex = 12;
            this.btnWrite.Text = "Değiştir";
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(490, 380);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(90, 42);
            this.btnExport.TabIndex = 13;
            this.btnExport.Text = "Dışa Aktar";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(12, 460);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(760, 74);
            this.txtLog.TabIndex = 14;
            // 
            // tabUpload
            // 
            this.tabUpload.Controls.Add(this.lblProcessUpload);
            this.tabUpload.Controls.Add(this.cbProcessesUpload);
            this.tabUpload.Controls.Add(this.btnRefreshUpload);
            this.tabUpload.Controls.Add(this.lblUpload);
            this.tabUpload.Controls.Add(this.txtUploadPath);
            this.tabUpload.Controls.Add(this.btnBrowseUpload);
            this.tabUpload.Controls.Add(this.btnUpload);
            this.tabUpload.Controls.Add(this.btnLoadAddresses);
            this.tabUpload.Controls.Add(this.flpAddressesUpload);
            this.tabUpload.Controls.Add(this.lblStatusUpload);
            this.tabUpload.Controls.Add(this.txtLogUpload);
            this.tabUpload.Location = new System.Drawing.Point(4, 25);
            this.tabUpload.Name = "tabUpload";
            this.tabUpload.Size = new System.Drawing.Size(792, 542);
            this.tabUpload.TabIndex = 1;
            this.tabUpload.Text = "İçe Aktar";
            this.tabUpload.UseVisualStyleBackColor = true;
            // 
            // lblProcessUpload
            // 
            this.lblProcessUpload.Location = new System.Drawing.Point(12, 9);
            this.lblProcessUpload.Name = "lblProcessUpload";
            this.lblProcessUpload.Size = new System.Drawing.Size(200, 15);
            this.lblProcessUpload.TabIndex = 0;
            this.lblProcessUpload.Text = "Process seçin:";
            // 
            // cbProcessesUpload
            // 
            this.cbProcessesUpload.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProcessesUpload.Location = new System.Drawing.Point(12, 30);
            this.cbProcessesUpload.Name = "cbProcessesUpload";
            this.cbProcessesUpload.Size = new System.Drawing.Size(420, 24);
            this.cbProcessesUpload.TabIndex = 1;
            // 
            // btnRefreshUpload
            // 
            this.btnRefreshUpload.Location = new System.Drawing.Point(440, 28);
            this.btnRefreshUpload.Name = "btnRefreshUpload";
            this.btnRefreshUpload.Size = new System.Drawing.Size(75, 26);
            this.btnRefreshUpload.TabIndex = 2;
            this.btnRefreshUpload.Text = "Yenile";
            this.btnRefreshUpload.Click += new System.EventHandler(this.btnRefreshUpload_Click);
            // 
            // lblUpload
            // 
            this.lblUpload.Location = new System.Drawing.Point(12, 65);
            this.lblUpload.Name = "lblUpload";
            this.lblUpload.Size = new System.Drawing.Size(120, 15);
            this.lblUpload.TabIndex = 3;
            this.lblUpload.Text = "Yüklenecek dosya:";
            // 
            // txtUploadPath
            // 
            this.txtUploadPath.Location = new System.Drawing.Point(12, 85);
            this.txtUploadPath.Name = "txtUploadPath";
            this.txtUploadPath.ReadOnly = true;
            this.txtUploadPath.Size = new System.Drawing.Size(540, 22);
            this.txtUploadPath.TabIndex = 4;
            // 
            // btnBrowseUpload
            // 
            this.btnBrowseUpload.Location = new System.Drawing.Point(560, 83);
            this.btnBrowseUpload.Name = "btnBrowseUpload";
            this.btnBrowseUpload.Size = new System.Drawing.Size(75, 34);
            this.btnBrowseUpload.TabIndex = 5;
            this.btnBrowseUpload.Text = "Gözat...";
            this.btnBrowseUpload.Click += new System.EventHandler(this.btnBrowseUpload_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(645, 83);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(90, 34);
            this.btnUpload.TabIndex = 6;
            this.btnUpload.Text = "Yükle";
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnLoadAddresses
            // 
            this.btnLoadAddresses.Location = new System.Drawing.Point(560, 45);
            this.btnLoadAddresses.Name = "btnLoadAddresses";
            this.btnLoadAddresses.Size = new System.Drawing.Size(175, 35);
            this.btnLoadAddresses.TabIndex = 7;
            this.btnLoadAddresses.Text = "Bulunan Adresleri Aktar";
            this.btnLoadAddresses.Click += new System.EventHandler(this.btnLoadAddresses_Click);
            // 
            // flpAddressesUpload
            // 
            this.flpAddressesUpload.AutoScroll = true;
            this.flpAddressesUpload.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpAddressesUpload.Location = new System.Drawing.Point(8, 123);
            this.flpAddressesUpload.Name = "flpAddressesUpload";
            this.flpAddressesUpload.Size = new System.Drawing.Size(760, 280);
            this.flpAddressesUpload.TabIndex = 8;
            this.flpAddressesUpload.WrapContents = false;
            // 
            // lblStatusUpload
            // 
            this.lblStatusUpload.Location = new System.Drawing.Point(12, 406);
            this.lblStatusUpload.Name = "lblStatusUpload";
            this.lblStatusUpload.Size = new System.Drawing.Size(760, 23);
            this.lblStatusUpload.TabIndex = 9;
            this.lblStatusUpload.Text = "Hazır";
            // 
            // txtLogUpload
            // 
            this.txtLogUpload.Location = new System.Drawing.Point(8, 432);
            this.txtLogUpload.Multiline = true;
            this.txtLogUpload.Name = "txtLogUpload";
            this.txtLogUpload.ReadOnly = true;
            this.txtLogUpload.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLogUpload.Size = new System.Drawing.Size(760, 89);
            this.txtLogUpload.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(220, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 18);
            this.label1.TabIndex = 16;
            this.label1.Text = "Veri Türü:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 571);
            this.Controls.Add(this.tabControlMain);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControlMain.ResumeLayout(false);
            this.tabScan.ResumeLayout(false);
            this.tabScan.PerformLayout();
            this.tabUpload.ResumeLayout(false);
            this.tabUpload.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabScan;
        private System.Windows.Forms.TabPage tabUpload;

        private System.Windows.Forms.ComboBox cbProcesses;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblProcess;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.ListBox lstResults;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnRescan;

        // Write controls
        private System.Windows.Forms.Label lblWriteValue;
        private System.Windows.Forms.TextBox txtWriteValue;
        private System.Windows.Forms.Button btnWrite;

        // Export button
        private System.Windows.Forms.Button btnExport;

        // Log control
        private System.Windows.Forms.TextBox txtLog;

        // Progress bar for scanning
        private System.Windows.Forms.ProgressBar progressBarScan;

        // Upload controls - Process selection
        private System.Windows.Forms.ComboBox cbProcessesUpload;
        private System.Windows.Forms.Button btnRefreshUpload;
        private System.Windows.Forms.Label lblProcessUpload;

        // Upload controls - File upload
        private System.Windows.Forms.Label lblUpload;
        private System.Windows.Forms.TextBox txtUploadPath;
        private System.Windows.Forms.Button btnBrowseUpload;
        private System.Windows.Forms.Button btnUpload;
        
        // Address management
        private System.Windows.Forms.Button btnLoadAddresses;
        private System.Windows.Forms.FlowLayoutPanel flpAddressesUpload;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;

        private System.Windows.Forms.Label lblStatusUpload;
        private System.Windows.Forms.TextBox txtLogUpload;
        private System.Windows.Forms.Label label1;
    }
}


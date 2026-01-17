namespace TestForm
{
    partial class BugReportingDialogTest
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
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kbtnClose = new Krypton.Toolkit.KryptonButton();
            this.kbtnTestEmailConfig = new Krypton.Toolkit.KryptonButton();
            this.kbtnShowExceptionWithBugReporting = new Krypton.Toolkit.KryptonButton();
            this.kbtnShowBugReportWithException = new Krypton.Toolkit.KryptonButton();
            this.kbtnShowBugReport = new Krypton.Toolkit.KryptonButton();
            this.kryptonGroupBox1 = new Krypton.Toolkit.KryptonGroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.kwlblSmtpServer = new Krypton.Toolkit.KryptonWrapLabel();
            this.ktbSmtpServer = new Krypton.Toolkit.KryptonTextBox();
            this.kwlblSmtpPort = new Krypton.Toolkit.KryptonWrapLabel();
            this.ktbSmtpPort = new Krypton.Toolkit.KryptonTextBox();
            this.kchkUseSsl = new Krypton.Toolkit.KryptonCheckBox();
            this.kwlblFromEmail = new Krypton.Toolkit.KryptonWrapLabel();
            this.ktbFromEmail = new Krypton.Toolkit.KryptonTextBox();
            this.kwlblToEmail = new Krypton.Toolkit.KryptonWrapLabel();
            this.ktbToEmail = new Krypton.Toolkit.KryptonTextBox();
            this.kwlblUsername = new Krypton.Toolkit.KryptonWrapLabel();
            this.ktbUsername = new Krypton.Toolkit.KryptonTextBox();
            this.kwlblPassword = new Krypton.Toolkit.KryptonWrapLabel();
            this.ktbPassword = new Krypton.Toolkit.KryptonTextBox();
            this.kwlblTestDescription = new Krypton.Toolkit.KryptonWrapLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).BeginInit();
            this.kryptonGroupBox1.Panel.SuspendLayout();
            this.kryptonGroupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kbtnClose);
            this.kryptonPanel1.Controls.Add(this.kbtnTestEmailConfig);
            this.kryptonPanel1.Controls.Add(this.kbtnShowExceptionWithBugReporting);
            this.kryptonPanel1.Controls.Add(this.kbtnShowBugReportWithException);
            this.kryptonPanel1.Controls.Add(this.kbtnShowBugReport);
            this.kryptonPanel1.Controls.Add(this.kryptonGroupBox1);
            this.kryptonPanel1.Controls.Add(this.kwlblTestDescription);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(600, 584);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kbtnClose
            // 
            this.kbtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnClose.Location = new System.Drawing.Point(513, 549);
            this.kbtnClose.Name = "kbtnClose";
            this.kbtnClose.Size = new System.Drawing.Size(75, 25);
            this.kbtnClose.TabIndex = 6;
            this.kbtnClose.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnClose.Values.Text = "Close";
            this.kbtnClose.Click += new System.EventHandler(this.kbtnClose_Click);
            // 
            // kbtnTestEmailConfig
            // 
            this.kbtnTestEmailConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.kbtnTestEmailConfig.Location = new System.Drawing.Point(12, 549);
            this.kbtnTestEmailConfig.Name = "kbtnTestEmailConfig";
            this.kbtnTestEmailConfig.Size = new System.Drawing.Size(150, 25);
            this.kbtnTestEmailConfig.TabIndex = 5;
            this.kbtnTestEmailConfig.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnTestEmailConfig.Values.Text = "Test Email Configuration";
            this.kbtnTestEmailConfig.Click += new System.EventHandler(this.kbtnTestEmailConfig_Click);
            // 
            // kbtnShowExceptionWithBugReporting
            // 
            this.kbtnShowExceptionWithBugReporting.Location = new System.Drawing.Point(12, 372);
            this.kbtnShowExceptionWithBugReporting.Name = "kbtnShowExceptionWithBugReporting";
            this.kbtnShowExceptionWithBugReporting.Size = new System.Drawing.Size(576, 30);
            this.kbtnShowExceptionWithBugReporting.TabIndex = 4;
            this.kbtnShowExceptionWithBugReporting.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnShowExceptionWithBugReporting.Values.Text = "Show Exception Dialog with Bug Reporting Integration";
            this.kbtnShowExceptionWithBugReporting.Click += new System.EventHandler(this.kbtnShowExceptionWithBugReporting_Click);
            // 
            // kbtnShowBugReportWithException
            // 
            this.kbtnShowBugReportWithException.Location = new System.Drawing.Point(12, 336);
            this.kbtnShowBugReportWithException.Name = "kbtnShowBugReportWithException";
            this.kbtnShowBugReportWithException.Size = new System.Drawing.Size(576, 30);
            this.kbtnShowBugReportWithException.TabIndex = 3;
            this.kbtnShowBugReportWithException.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnShowBugReportWithException.Values.Text = "Show Bug Report Dialog with Exception";
            this.kbtnShowBugReportWithException.Click += new System.EventHandler(this.kbtnShowBugReportWithException_Click);
            // 
            // kbtnShowBugReport
            // 
            this.kbtnShowBugReport.Location = new System.Drawing.Point(12, 300);
            this.kbtnShowBugReport.Name = "kbtnShowBugReport";
            this.kbtnShowBugReport.Size = new System.Drawing.Size(576, 30);
            this.kbtnShowBugReport.TabIndex = 2;
            this.kbtnShowBugReport.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnShowBugReport.Values.Text = "Show Bug Report Dialog (No Exception)";
            this.kbtnShowBugReport.Click += new System.EventHandler(this.kbtnShowBugReport_Click);
            // 
            // kryptonGroupBox1
            // 
            this.kryptonGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonGroupBox1.Location = new System.Drawing.Point(12, 50);
            // 
            // kryptonGroupBox1.Panel
            // 
            this.kryptonGroupBox1.Panel.Controls.Add(this.tableLayoutPanel1);
            this.kryptonGroupBox1.Size = new System.Drawing.Size(576, 244);
            this.kryptonGroupBox1.TabIndex = 1;
            this.kryptonGroupBox1.Values.Heading = "Email Configuration";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.kwlblSmtpServer, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.ktbSmtpServer, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.kwlblSmtpPort, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.ktbSmtpPort, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.kchkUseSsl, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.kwlblFromEmail, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.ktbFromEmail, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.kwlblToEmail, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.ktbToEmail, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.kwlblUsername, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.ktbUsername, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.kwlblPassword, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.ktbPassword, 1, 6);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(572, 220);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // kwlblSmtpServer
            // 
            this.kwlblSmtpServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlblSmtpServer.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.kwlblSmtpServer.Location = new System.Drawing.Point(13, 10);
            this.kwlblSmtpServer.Name = "kwlblSmtpServer";
            this.kwlblSmtpServer.Size = new System.Drawing.Size(114, 30);
            this.kwlblSmtpServer.Text = "SMTP Server:";
            this.kwlblSmtpServer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ktbSmtpServer
            // 
            this.ktbSmtpServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ktbSmtpServer.Location = new System.Drawing.Point(133, 13);
            this.ktbSmtpServer.Name = "ktbSmtpServer";
            this.ktbSmtpServer.Size = new System.Drawing.Size(426, 23);
            this.ktbSmtpServer.TabIndex = 1;
            this.ktbSmtpServer.TextChanged += new System.EventHandler(this.ktbSmtpServer_TextChanged);
            // 
            // kwlblSmtpPort
            // 
            this.kwlblSmtpPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlblSmtpPort.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.kwlblSmtpPort.Location = new System.Drawing.Point(13, 40);
            this.kwlblSmtpPort.Name = "kwlblSmtpPort";
            this.kwlblSmtpPort.Size = new System.Drawing.Size(114, 30);
            this.kwlblSmtpPort.Text = "SMTP Port:";
            this.kwlblSmtpPort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ktbSmtpPort
            // 
            this.ktbSmtpPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ktbSmtpPort.Location = new System.Drawing.Point(133, 43);
            this.ktbSmtpPort.Name = "ktbSmtpPort";
            this.ktbSmtpPort.Size = new System.Drawing.Size(426, 23);
            this.ktbSmtpPort.TabIndex = 2;
            // 
            // kchkUseSsl
            // 
            this.kchkUseSsl.Checked = true;
            this.kchkUseSsl.CheckState = System.Windows.Forms.CheckState.Checked;
            this.kchkUseSsl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kchkUseSsl.Location = new System.Drawing.Point(133, 73);
            this.kchkUseSsl.Name = "kchkUseSsl";
            this.kchkUseSsl.Size = new System.Drawing.Size(426, 19);
            this.kchkUseSsl.TabIndex = 3;
            this.kchkUseSsl.Values.Text = "Use SSL/TLS";
            // 
            // kwlblFromEmail
            // 
            this.kwlblFromEmail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlblFromEmail.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.kwlblFromEmail.Location = new System.Drawing.Point(13, 95);
            this.kwlblFromEmail.Name = "kwlblFromEmail";
            this.kwlblFromEmail.Size = new System.Drawing.Size(114, 30);
            this.kwlblFromEmail.Text = "From Email:";
            this.kwlblFromEmail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ktbFromEmail
            // 
            this.ktbFromEmail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ktbFromEmail.Location = new System.Drawing.Point(133, 98);
            this.ktbFromEmail.Name = "ktbFromEmail";
            this.ktbFromEmail.Size = new System.Drawing.Size(426, 23);
            this.ktbFromEmail.TabIndex = 4;
            // 
            // kwlblToEmail
            // 
            this.kwlblToEmail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlblToEmail.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.kwlblToEmail.Location = new System.Drawing.Point(13, 125);
            this.kwlblToEmail.Name = "kwlblToEmail";
            this.kwlblToEmail.Size = new System.Drawing.Size(114, 30);
            this.kwlblToEmail.Text = "To Email:";
            this.kwlblToEmail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ktbToEmail
            // 
            this.ktbToEmail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ktbToEmail.Location = new System.Drawing.Point(133, 128);
            this.ktbToEmail.Name = "ktbToEmail";
            this.ktbToEmail.Size = new System.Drawing.Size(426, 23);
            this.ktbToEmail.TabIndex = 5;
            this.ktbToEmail.TextChanged += new System.EventHandler(this.ktbToEmail_TextChanged);
            // 
            // kwlblUsername
            // 
            this.kwlblUsername.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlblUsername.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.kwlblUsername.Location = new System.Drawing.Point(13, 155);
            this.kwlblUsername.Name = "kwlblUsername";
            this.kwlblUsername.Size = new System.Drawing.Size(114, 30);
            this.kwlblUsername.Text = "Username:";
            this.kwlblUsername.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ktbUsername
            // 
            this.ktbUsername.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ktbUsername.Location = new System.Drawing.Point(133, 158);
            this.ktbUsername.Name = "ktbUsername";
            this.ktbUsername.Size = new System.Drawing.Size(426, 23);
            this.ktbUsername.TabIndex = 6;
            // 
            // kwlblPassword
            // 
            this.kwlblPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlblPassword.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.kwlblPassword.Location = new System.Drawing.Point(13, 185);
            this.kwlblPassword.Name = "kwlblPassword";
            this.kwlblPassword.Size = new System.Drawing.Size(114, 30);
            this.kwlblPassword.Text = "Password:";
            this.kwlblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ktbPassword
            // 
            this.ktbPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ktbPassword.Location = new System.Drawing.Point(133, 188);
            this.ktbPassword.Name = "ktbPassword";
            this.ktbPassword.PasswordChar = '●';
            this.ktbPassword.Size = new System.Drawing.Size(426, 23);
            this.ktbPassword.TabIndex = 7;
            this.ktbPassword.UseSystemPasswordChar = true;
            // 
            // kwlblTestDescription
            // 
            this.kwlblTestDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kwlblTestDescription.LabelStyle = Krypton.Toolkit.LabelStyle.TitleControl;
            this.kwlblTestDescription.Location = new System.Drawing.Point(12, 12);
            this.kwlblTestDescription.Name = "kwlblTestDescription";
            this.kwlblTestDescription.Size = new System.Drawing.Size(236, 25);
            this.kwlblTestDescription.Text = "Bug Reporting Dialog Test";
            this.kwlblTestDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BugReportingDialogTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 584);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BugReportingDialogTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bug Reporting Dialog Test";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).EndInit();
            this.kryptonGroupBox1.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).EndInit();
            this.kryptonGroupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonPanel kryptonPanel1;
        private KryptonWrapLabel kwlblTestDescription;
        private KryptonGroupBox kryptonGroupBox1;
        private TableLayoutPanel tableLayoutPanel1;
        private KryptonWrapLabel kwlblSmtpServer;
        private KryptonTextBox ktbSmtpServer;
        private KryptonWrapLabel kwlblSmtpPort;
        private KryptonTextBox ktbSmtpPort;
        private KryptonCheckBox kchkUseSsl;
        private KryptonWrapLabel kwlblFromEmail;
        private KryptonTextBox ktbFromEmail;
        private KryptonWrapLabel kwlblToEmail;
        private KryptonTextBox ktbToEmail;
        private KryptonWrapLabel kwlblUsername;
        private KryptonTextBox ktbUsername;
        private KryptonWrapLabel kwlblPassword;
        private KryptonTextBox ktbPassword;
        private KryptonButton kbtnShowBugReport;
        private KryptonButton kbtnShowBugReportWithException;
        private KryptonButton kbtnShowExceptionWithBugReporting;
        private KryptonButton kbtnTestEmailConfig;
        private KryptonButton kbtnClose;
    }
}


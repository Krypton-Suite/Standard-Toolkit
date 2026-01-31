namespace TestForm
{
    partial class SplashScreenExample
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
            this.kbtnShow = new Krypton.Toolkit.KryptonButton();
            this.kbtnCancel = new Krypton.Toolkit.KryptonButton();
            this.kryptonBorderEdge1 = new Krypton.Toolkit.KryptonBorderEdge();
            this.kryptonPanel2 = new Krypton.Toolkit.KryptonPanel();
            this.kcbShowCloseButton = new Krypton.Toolkit.KryptonCheckBox();
            this.kcbShowMinimizeButton = new Krypton.Toolkit.KryptonCheckBox();
            this.ktxtLogo = new Krypton.Toolkit.KryptonTextBox();
            this.bsaBrowseLogo = new Krypton.Toolkit.ButtonSpecAny();
            this.kcmdChosenLogo = new Krypton.Toolkit.KryptonCommand();
            this.knudTimeout = new Krypton.Toolkit.KryptonNumericUpDown();
            this.kryptonLabel3 = new Krypton.Toolkit.KryptonLabel();
            this.kchkShowProgressBarPercentage = new Krypton.Toolkit.KryptonCheckBox();
            this.kchkShowVersion = new Krypton.Toolkit.KryptonCheckBox();
            this.kchkShowProgressBar = new Krypton.Toolkit.KryptonCheckBox();
            this.kchkShowCopyright = new Krypton.Toolkit.KryptonCheckBox();
            this.kryptonLabel2 = new Krypton.Toolkit.KryptonLabel();
            this.ktxtAssembly = new Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            this.kcmdChosenAssembly = new Krypton.Toolkit.KryptonCommand();
            this.kchkShowApplicationName = new Krypton.Toolkit.KryptonCheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kbtnShow);
            this.kryptonPanel1.Controls.Add(this.kbtnCancel);
            this.kryptonPanel1.Controls.Add(this.kryptonBorderEdge1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 192);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kryptonPanel1.Size = new System.Drawing.Size(389, 50);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kbtnShow
            // 
            this.kbtnShow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnShow.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.kbtnShow.Location = new System.Drawing.Point(191, 13);
            this.kbtnShow.Name = "kbtnShow";
            this.kbtnShow.Size = new System.Drawing.Size(90, 25);
            this.kbtnShow.TabIndex = 2;
            this.kbtnShow.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnShow.Values.Text = "&Show";
            this.kbtnShow.Click += new System.EventHandler(this.kbtnShow_Click);
            // 
            // kbtnCancel
            // 
            this.kbtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.kbtnCancel.Location = new System.Drawing.Point(287, 13);
            this.kbtnCancel.Name = "kbtnCancel";
            this.kbtnCancel.Size = new System.Drawing.Size(90, 25);
            this.kbtnCancel.TabIndex = 1;
            this.kbtnCancel.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnCancel.Values.Text = "Cance&l";
            this.kbtnCancel.Values.UseAsADialogButton = true;
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(0, 0);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(389, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.kchkShowApplicationName);
            this.kryptonPanel2.Controls.Add(this.kcbShowCloseButton);
            this.kryptonPanel2.Controls.Add(this.kcbShowMinimizeButton);
            this.kryptonPanel2.Controls.Add(this.ktxtLogo);
            this.kryptonPanel2.Controls.Add(this.knudTimeout);
            this.kryptonPanel2.Controls.Add(this.kryptonLabel3);
            this.kryptonPanel2.Controls.Add(this.kchkShowProgressBarPercentage);
            this.kryptonPanel2.Controls.Add(this.kchkShowVersion);
            this.kryptonPanel2.Controls.Add(this.kchkShowProgressBar);
            this.kryptonPanel2.Controls.Add(this.kchkShowCopyright);
            this.kryptonPanel2.Controls.Add(this.kryptonLabel2);
            this.kryptonPanel2.Controls.Add(this.ktxtAssembly);
            this.kryptonPanel2.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(389, 192);
            this.kryptonPanel2.TabIndex = 1;
            // 
            // kcbShowCloseButton
            // 
            this.kcbShowCloseButton.Location = new System.Drawing.Point(13, 99);
            this.kcbShowCloseButton.Name = "kcbShowCloseButton";
            this.kcbShowCloseButton.Size = new System.Drawing.Size(126, 20);
            this.kcbShowCloseButton.TabIndex = 13;
            this.kcbShowCloseButton.Values.Text = "Show Close Button";
            // 
            // kcbShowMinimizeButton
            // 
            this.kcbShowMinimizeButton.Location = new System.Drawing.Point(145, 99);
            this.kcbShowMinimizeButton.Name = "kcbShowMinimizeButton";
            this.kcbShowMinimizeButton.Size = new System.Drawing.Size(146, 20);
            this.kcbShowMinimizeButton.TabIndex = 12;
            this.kcbShowMinimizeButton.Values.Text = "Show Minimize Button";
            // 
            // ktxtLogo
            // 
            this.ktxtLogo.ButtonSpecs.Add(this.bsaBrowseLogo);
            this.ktxtLogo.Location = new System.Drawing.Point(89, 42);
            this.ktxtLogo.Name = "ktxtLogo";
            this.ktxtLogo.Size = new System.Drawing.Size(288, 24);
            this.ktxtLogo.TabIndex = 11;
            this.ktxtLogo.Text = "kryptonTextBox1";
            // 
            // bsaBrowseLogo
            // 
            this.bsaBrowseLogo.Enabled = Krypton.Toolkit.ButtonEnabled.True;
            this.bsaBrowseLogo.KryptonCommand = this.kcmdChosenLogo;
            this.bsaBrowseLogo.UniqueName = "86ec36b6055b41198bfca216d71b6799";
            // 
            // kcmdChosenLogo
            // 
            this.kcmdChosenLogo.AssignedButtonSpec = this.bsaBrowseLogo;
            this.kcmdChosenLogo.Text = ".&..";
            this.kcmdChosenLogo.Execute += new System.EventHandler(this.kcmdChosenLogo_Execute);
            // 
            // knudTimeout
            // 
            this.knudTimeout.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.knudTimeout.Location = new System.Drawing.Point(89, 153);
            this.knudTimeout.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.knudTimeout.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.knudTimeout.Name = "knudTimeout";
            this.knudTimeout.Size = new System.Drawing.Size(290, 22);
            this.knudTimeout.TabIndex = 10;
            this.knudTimeout.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.LabelStyle = Krypton.Toolkit.LabelStyle.BoldPanel;
            this.kryptonLabel3.Location = new System.Drawing.Point(13, 153);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(62, 20);
            this.kryptonLabel3.TabIndex = 9;
            this.kryptonLabel3.Values.Text = "Timeout:";
            // 
            // kchkShowProgressBarPercentage
            // 
            this.kchkShowProgressBarPercentage.Location = new System.Drawing.Point(13, 127);
            this.kchkShowProgressBarPercentage.Name = "kchkShowProgressBarPercentage";
            this.kchkShowProgressBarPercentage.Size = new System.Drawing.Size(186, 20);
            this.kchkShowProgressBarPercentage.TabIndex = 8;
            this.kchkShowProgressBarPercentage.Values.Text = "Show ProgressBar Percentage";
            // 
            // kchkShowVersion
            // 
            this.kchkShowVersion.Location = new System.Drawing.Point(130, 73);
            this.kchkShowVersion.Name = "kchkShowVersion";
            this.kchkShowVersion.Size = new System.Drawing.Size(98, 20);
            this.kchkShowVersion.TabIndex = 7;
            this.kchkShowVersion.Values.Text = "Show Version";
            // 
            // kchkShowProgressBar
            // 
            this.kchkShowProgressBar.Location = new System.Drawing.Point(234, 73);
            this.kchkShowProgressBar.Name = "kchkShowProgressBar";
            this.kchkShowProgressBar.Size = new System.Drawing.Size(121, 20);
            this.kchkShowProgressBar.TabIndex = 6;
            this.kchkShowProgressBar.Values.Text = "Show ProgressBar";
            // 
            // kchkShowCopyright
            // 
            this.kchkShowCopyright.Location = new System.Drawing.Point(13, 73);
            this.kchkShowCopyright.Name = "kchkShowCopyright";
            this.kchkShowCopyright.Size = new System.Drawing.Size(111, 20);
            this.kchkShowCopyright.TabIndex = 4;
            this.kchkShowCopyright.Values.Text = "Show Copyright";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.LabelStyle = Krypton.Toolkit.LabelStyle.BoldPanel;
            this.kryptonLabel2.Location = new System.Drawing.Point(13, 43);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(43, 20);
            this.kryptonLabel2.TabIndex = 2;
            this.kryptonLabel2.Values.Text = "Logo:";
            // 
            // ktxtAssembly
            // 
            this.ktxtAssembly.Location = new System.Drawing.Point(89, 13);
            this.ktxtAssembly.Name = "ktxtAssembly";
            this.ktxtAssembly.ShowEllipsisButton = true;
            this.ktxtAssembly.Size = new System.Drawing.Size(290, 24);
            this.ktxtAssembly.TabIndex = 1;
            this.ktxtAssembly.Text = "kryptonTextBox1";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.LabelStyle = Krypton.Toolkit.LabelStyle.BoldPanel;
            this.kryptonLabel1.Location = new System.Drawing.Point(13, 13);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(69, 20);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "Assembly:";
            // 
            // kcmdChosenAssembly
            // 
            this.kcmdChosenAssembly.Text = ".&..";
            this.kcmdChosenAssembly.Execute += new System.EventHandler(this.kcmdChosenAssembly_Execute);
            // 
            // kchkShowApplicationName
            // 
            this.kchkShowApplicationName.Location = new System.Drawing.Point(205, 127);
            this.kchkShowApplicationName.Name = "kchkShowApplicationName";
            this.kchkShowApplicationName.Size = new System.Drawing.Size(155, 20);
            this.kchkShowApplicationName.TabIndex = 14;
            this.kchkShowApplicationName.Values.Text = "Show Application Name";
            // 
            // SplashScreenExample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 242);
            this.Controls.Add(this.kryptonPanel2);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "SplashScreenExample";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SplashScreenExample";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            this.kryptonPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonPanel kryptonPanel1;
        private KryptonPanel kryptonPanel2;
        private KryptonBorderEdge kryptonBorderEdge1;
        private KryptonButton kbtnCancel;
        private KryptonButton kbtnShow;
        private KryptonLabel kryptonLabel1;
        private KryptonTextBox ktxtAssembly;
        private KryptonLabel kryptonLabel2;
        private KryptonCheckBox kchkShowCopyright;
        private KryptonCheckBox kchkShowProgressBar;
        private KryptonCheckBox kchkShowVersion;
        private KryptonCheckBox kchkShowProgressBarPercentage;
        private KryptonLabel kryptonLabel3;
        private KryptonNumericUpDown knudTimeout;
        private KryptonCommand kcmdChosenAssembly;
        private KryptonCommand kcmdChosenLogo;
        private KryptonTextBox ktxtLogo;
        private ButtonSpecAny bsaBrowseLogo;
        private KryptonCheckBox kcbShowCloseButton;
        private KryptonCheckBox kcbShowMinimizeButton;
        private KryptonCheckBox kchkShowApplicationName;
    }
}
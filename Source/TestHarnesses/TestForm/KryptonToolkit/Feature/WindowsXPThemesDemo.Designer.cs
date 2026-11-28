namespace TestForm
{
    partial class WindowsXPThemesDemo
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.kryptonManager1 = new Krypton.Toolkit.KryptonManager(this.components);
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.klblInstructions = new Krypton.Toolkit.KryptonLabel();
            this.klblTheme = new Krypton.Toolkit.KryptonLabel();
            this.cmbTheme = new Krypton.Toolkit.KryptonComboBox();
            this.klblStatus = new Krypton.Toolkit.KryptonLabel();
            this.kbtnSample = new Krypton.Toolkit.KryptonButton();
            this.kchkSample = new Krypton.Toolkit.KryptonCheckBox();
            this.krdoSample = new Krypton.Toolkit.KryptonRadioButton();
            this.ktxtSample = new Krypton.Toolkit.KryptonTextBox();
            this.kgrpSample = new Krypton.Toolkit.KryptonGroupBox();
            this.klblGroupBody = new Krypton.Toolkit.KryptonLabel();
            this.kryptonProgressBar1 = new Krypton.Toolkit.KryptonProgressBar();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sampleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTheme)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kgrpSample)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kgrpSample.Panel)).BeginInit();
            this.kgrpSample.Panel.SuspendLayout();
            this.kgrpSample.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            //
            // kryptonPanel1
            //
            this.kryptonPanel1.Controls.Add(this.kgrpSample);
            this.kryptonPanel1.Controls.Add(this.kryptonProgressBar1);
            this.kryptonPanel1.Controls.Add(this.ktxtSample);
            this.kryptonPanel1.Controls.Add(this.krdoSample);
            this.kryptonPanel1.Controls.Add(this.kchkSample);
            this.kryptonPanel1.Controls.Add(this.kbtnSample);
            this.kryptonPanel1.Controls.Add(this.klblStatus);
            this.kryptonPanel1.Controls.Add(this.cmbTheme);
            this.kryptonPanel1.Controls.Add(this.klblTheme);
            this.kryptonPanel1.Controls.Add(this.klblInstructions);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 24);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Padding = new System.Windows.Forms.Padding(16);
            this.kryptonPanel1.Size = new System.Drawing.Size(784, 437);
            this.kryptonPanel1.TabIndex = 0;
            //
            // klblInstructions
            //
            this.klblInstructions.Location = new System.Drawing.Point(19, 19);
            this.klblInstructions.Name = "klblInstructions";
            this.klblInstructions.Size = new System.Drawing.Size(746, 40);
            this.klblInstructions.StateCommon.ShortText.MultiLine = Krypton.Toolkit.InheritBool.True;
            this.klblInstructions.TabIndex = 0;
            this.klblInstructions.Values.Text = "Switch Windows XP Extra themes (Luna / Royale / Zune / Embedded). Check buttons, checks, radios, inputs, group box, progress bar, menu, and form chrome.";
            //
            // klblTheme
            //
            this.klblTheme.Location = new System.Drawing.Point(19, 72);
            this.klblTheme.Name = "klblTheme";
            this.klblTheme.Size = new System.Drawing.Size(48, 20);
            this.klblTheme.TabIndex = 1;
            this.klblTheme.Values.Text = "Theme:";
            //
            // cmbTheme
            //
            this.cmbTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTheme.DropDownWidth = 220;
            this.cmbTheme.Location = new System.Drawing.Point(73, 70);
            this.cmbTheme.Name = "cmbTheme";
            this.cmbTheme.Size = new System.Drawing.Size(220, 21);
            this.cmbTheme.TabIndex = 2;
            this.cmbTheme.SelectedIndexChanged += new System.EventHandler(this.cmbTheme_SelectedIndexChanged);
            //
            // klblStatus
            //
            this.klblStatus.Location = new System.Drawing.Point(19, 104);
            this.klblStatus.Name = "klblStatus";
            this.klblStatus.Size = new System.Drawing.Size(50, 20);
            this.klblStatus.TabIndex = 3;
            this.klblStatus.Values.Text = "Status";
            //
            // kbtnSample
            //
            this.kbtnSample.Location = new System.Drawing.Point(19, 140);
            this.kbtnSample.Name = "kbtnSample";
            this.kbtnSample.Size = new System.Drawing.Size(120, 28);
            this.kbtnSample.TabIndex = 4;
            this.kbtnSample.Values.Text = "Sample Button";
            //
            // kchkSample
            //
            this.kchkSample.Location = new System.Drawing.Point(155, 144);
            this.kchkSample.Name = "kchkSample";
            this.kchkSample.Size = new System.Drawing.Size(120, 20);
            this.kchkSample.TabIndex = 5;
            this.kchkSample.Values.Text = "Sample Check";
            //
            // krdoSample
            //
            this.krdoSample.Checked = true;
            this.krdoSample.Location = new System.Drawing.Point(290, 144);
            this.krdoSample.Name = "krdoSample";
            this.krdoSample.Size = new System.Drawing.Size(130, 20);
            this.krdoSample.TabIndex = 9;
            this.krdoSample.Values.Text = "Sample Radio";
            //
            // ktxtSample
            //
            this.ktxtSample.Location = new System.Drawing.Point(19, 184);
            this.ktxtSample.Name = "ktxtSample";
            this.ktxtSample.Size = new System.Drawing.Size(280, 23);
            this.ktxtSample.TabIndex = 6;
            this.ktxtSample.Text = "Sample text box";
            //
            // kryptonProgressBar1
            //
            this.kryptonProgressBar1.Location = new System.Drawing.Point(19, 224);
            this.kryptonProgressBar1.Name = "kryptonProgressBar1";
            this.kryptonProgressBar1.Size = new System.Drawing.Size(280, 23);
            this.kryptonProgressBar1.TabIndex = 7;
            this.kryptonProgressBar1.Value = 55;
            //
            // kgrpSample
            //
            this.kgrpSample.Location = new System.Drawing.Point(19, 268);
            this.kgrpSample.Name = "kgrpSample";
            this.kgrpSample.Panel.Controls.Add(this.klblGroupBody);
            this.kgrpSample.Size = new System.Drawing.Size(360, 120);
            this.kgrpSample.TabIndex = 8;
            this.kgrpSample.Values.Heading = "Sample Group";
            //
            // klblGroupBody
            //
            this.klblGroupBody.Location = new System.Drawing.Point(12, 12);
            this.klblGroupBody.Name = "klblGroupBody";
            this.klblGroupBody.Size = new System.Drawing.Size(120, 20);
            this.klblGroupBody.TabIndex = 0;
            this.klblGroupBody.Values.Text = "Group body content";
            //
            // menuStrip1
            //
            this.menuStrip1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            //
            // fileToolStripMenuItem
            //
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sampleToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            //
            // sampleToolStripMenuItem
            //
            this.sampleToolStripMenuItem.Name = "sampleToolStripMenuItem";
            this.sampleToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.sampleToolStripMenuItem.Text = "Sample";
            //
            // WindowsXPThemesDemo
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.kryptonPanel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(640, 400);
            this.Name = "WindowsXPThemesDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Windows XP Themes";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.WindowsXPThemesDemo_FormClosed);
            this.Load += new System.EventHandler(this.WindowsXPThemesDemo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTheme)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kgrpSample.Panel)).EndInit();
            this.kgrpSample.Panel.ResumeLayout(false);
            this.kgrpSample.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kgrpSample)).EndInit();
            this.kgrpSample.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private Krypton.Toolkit.KryptonManager kryptonManager1;
        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonLabel klblInstructions;
        private Krypton.Toolkit.KryptonLabel klblTheme;
        private Krypton.Toolkit.KryptonComboBox cmbTheme;
        private Krypton.Toolkit.KryptonLabel klblStatus;
        private Krypton.Toolkit.KryptonButton kbtnSample;
        private Krypton.Toolkit.KryptonCheckBox kchkSample;
        private Krypton.Toolkit.KryptonRadioButton krdoSample;
        private Krypton.Toolkit.KryptonTextBox ktxtSample;
        private Krypton.Toolkit.KryptonGroupBox kgrpSample;
        private Krypton.Toolkit.KryptonLabel klblGroupBody;
        private Krypton.Toolkit.KryptonProgressBar kryptonProgressBar1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sampleToolStripMenuItem;
    }
}
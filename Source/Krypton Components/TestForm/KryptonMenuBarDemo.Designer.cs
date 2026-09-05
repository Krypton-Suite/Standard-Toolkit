namespace TestForm
{
    partial class KryptonMenuBarDemo
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
            this.kryptonMenuBar1 = new Krypton.Toolkit.KryptonMenuBar();
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.tableLayoutPanel1 = new Krypton.Toolkit.KryptonTableLayoutPanel();
            this.klblInstructions = new Krypton.Toolkit.KryptonLabel();
            this.panelButtons = new Krypton.Toolkit.KryptonPanel();
            this.kryptonThemeComboBox1 = new Krypton.Toolkit.KryptonThemeComboBox();
            this.kbtnInsertStandardItems = new Krypton.Toolkit.KryptonButton();
            this.kbtnClearLog = new Krypton.Toolkit.KryptonButton();
            this.kgbKryptonMenuStrip = new Krypton.Toolkit.KryptonGroupBox();
            this.kryptonMenuStrip1 = new Krypton.Toolkit.KryptonMenuStrip();
            this.kgbNativeMenuStrip = new Krypton.Toolkit.KryptonGroupBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.kgbLog = new Krypton.Toolkit.KryptonGroupBox();
            this.klbLog = new Krypton.Toolkit.KryptonListBox();
            this.kryptonStatusStrip1 = new Krypton.Toolkit.KryptonStatusStrip();
            this.klblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelButtons)).BeginInit();
            this.panelButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kgbKryptonMenuStrip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kgbKryptonMenuStrip.Panel)).BeginInit();
            this.kgbKryptonMenuStrip.Panel.SuspendLayout();
            this.kgbKryptonMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kgbNativeMenuStrip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kgbNativeMenuStrip.Panel)).BeginInit();
            this.kgbNativeMenuStrip.Panel.SuspendLayout();
            this.kgbNativeMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kgbLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kgbLog.Panel)).BeginInit();
            this.kgbLog.Panel.SuspendLayout();
            this.kgbLog.SuspendLayout();
            this.kryptonStatusStrip1.SuspendLayout();
            this.SuspendLayout();
            //
            // kryptonMenuBar1
            //
            this.kryptonMenuBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonMenuBar1.Location = new System.Drawing.Point(0, 0);
            this.kryptonMenuBar1.Name = "kryptonMenuBar1";
            this.kryptonMenuBar1.Size = new System.Drawing.Size(784, 24);
            this.kryptonMenuBar1.TabIndex = 0;
            //
            // kryptonPanel1
            //
            this.kryptonPanel1.Controls.Add(this.tableLayoutPanel1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 24);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Padding = new System.Windows.Forms.Padding(8);
            this.kryptonPanel1.Size = new System.Drawing.Size(784, 517);
            this.kryptonPanel1.TabIndex = 1;
            //
            // tableLayoutPanel1
            //
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.klblInstructions, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelButtons, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.kgbKryptonMenuStrip, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.kgbNativeMenuStrip, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.kgbLog, 0, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(8, 8);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(768, 501);
            this.tableLayoutPanel1.TabIndex = 0;
            //
            // klblInstructions
            //
            this.klblInstructions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblInstructions.Location = new System.Drawing.Point(3, 3);
            this.klblInstructions.Name = "klblInstructions";
            this.klblInstructions.Size = new System.Drawing.Size(762, 60);
            this.klblInstructions.TabIndex = 0;
            this.klblInstructions.Values.Text = "KryptonMenuBar (top of this form) is assigned to KryptonForm.MenuBar. Compare with KryptonMenuStrip (ToolStrip path) and native MenuStrip below. Try Alt/F10, Alt+mnemonic, Left/Right while a drop-down is open, Ctrl+N (File > New), and theme changes.";
            //
            // panelButtons
            //
            this.panelButtons.Controls.Add(this.kryptonThemeComboBox1);
            this.panelButtons.Controls.Add(this.kbtnInsertStandardItems);
            this.panelButtons.Controls.Add(this.kbtnClearLog);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelButtons.Location = new System.Drawing.Point(3, 69);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(762, 36);
            this.panelButtons.TabIndex = 1;
            //
            // kryptonThemeComboBox1
            //
            this.kryptonThemeComboBox1.DropDownWidth = 220;
            this.kryptonThemeComboBox1.Location = new System.Drawing.Point(3, 5);
            this.kryptonThemeComboBox1.Name = "kryptonThemeComboBox1";
            this.kryptonThemeComboBox1.Size = new System.Drawing.Size(220, 25);
            this.kryptonThemeComboBox1.TabIndex = 0;
            //
            // kbtnInsertStandardItems
            //
            this.kbtnInsertStandardItems.Location = new System.Drawing.Point(229, 5);
            this.kbtnInsertStandardItems.Name = "kbtnInsertStandardItems";
            this.kbtnInsertStandardItems.Size = new System.Drawing.Size(160, 25);
            this.kbtnInsertStandardItems.TabIndex = 1;
            this.kbtnInsertStandardItems.Values.Text = "Insert Standard Items";
            this.kbtnInsertStandardItems.Click += new System.EventHandler(this.kbtnInsertStandardItems_Click);
            //
            // kbtnClearLog
            //
            this.kbtnClearLog.Location = new System.Drawing.Point(395, 5);
            this.kbtnClearLog.Name = "kbtnClearLog";
            this.kbtnClearLog.Size = new System.Drawing.Size(90, 25);
            this.kbtnClearLog.TabIndex = 2;
            this.kbtnClearLog.Values.Text = "Clear log";
            this.kbtnClearLog.Click += new System.EventHandler(this.kbtnClearLog_Click);
            //
            // kgbKryptonMenuStrip
            //
            this.kgbKryptonMenuStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kgbKryptonMenuStrip.Location = new System.Drawing.Point(3, 111);
            this.kgbKryptonMenuStrip.Name = "kgbKryptonMenuStrip";
            this.kgbKryptonMenuStrip.Size = new System.Drawing.Size(762, 70);
            this.kgbKryptonMenuStrip.TabIndex = 2;
            this.kgbKryptonMenuStrip.Values.Heading = "KryptonMenuStrip (ToolStrip / MainMenuStrip path)";
            this.kgbKryptonMenuStrip.Panel.Controls.Add(this.kryptonMenuStrip1);
            //
            // kryptonMenuStrip1
            //
            this.kryptonMenuStrip1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonMenuStrip1.Location = new System.Drawing.Point(0, 0);
            this.kryptonMenuStrip1.Name = "kryptonMenuStrip1";
            this.kryptonMenuStrip1.Size = new System.Drawing.Size(760, 24);
            this.kryptonMenuStrip1.TabIndex = 0;
            this.kryptonMenuStrip1.Text = "kryptonMenuStrip1";
            //
            // kgbNativeMenuStrip
            //
            this.kgbNativeMenuStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kgbNativeMenuStrip.Location = new System.Drawing.Point(3, 187);
            this.kgbNativeMenuStrip.Name = "kgbNativeMenuStrip";
            this.kgbNativeMenuStrip.Size = new System.Drawing.Size(762, 70);
            this.kgbNativeMenuStrip.TabIndex = 3;
            this.kgbNativeMenuStrip.Values.Heading = "Native WinForms MenuStrip";
            this.kgbNativeMenuStrip.Panel.Controls.Add(this.menuStrip1);
            //
            // menuStrip1
            //
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(760, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            //
            // kgbLog
            //
            this.kgbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kgbLog.Location = new System.Drawing.Point(3, 263);
            this.kgbLog.Name = "kgbLog";
            this.kgbLog.Size = new System.Drawing.Size(762, 235);
            this.kgbLog.TabIndex = 4;
            this.kgbLog.Values.Heading = "Event log";
            this.kgbLog.Panel.Controls.Add(this.klbLog);
            //
            // klbLog
            //
            this.klbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klbLog.Location = new System.Drawing.Point(0, 0);
            this.klbLog.Name = "klbLog";
            this.klbLog.Size = new System.Drawing.Size(760, 210);
            this.klbLog.TabIndex = 0;
            //
            // kryptonStatusStrip1
            //
            this.kryptonStatusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.klblStatus});
            this.kryptonStatusStrip1.Location = new System.Drawing.Point(0, 541);
            this.kryptonStatusStrip1.Name = "kryptonStatusStrip1";
            this.kryptonStatusStrip1.Size = new System.Drawing.Size(784, 22);
            this.kryptonStatusStrip1.TabIndex = 2;
            //
            // klblStatus
            //
            this.klblStatus.Name = "klblStatus";
            this.klblStatus.Size = new System.Drawing.Size(39, 17);
            this.klblStatus.Text = "Ready";
            //
            // KryptonMenuBarDemo
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 563);
            this.Controls.Add(this.kryptonPanel1);
            this.Controls.Add(this.kryptonStatusStrip1);
            this.Controls.Add(this.kryptonMenuBar1);
            this.Name = "KryptonMenuBarDemo";
            this.Text = "KryptonMenuBar (#4242)";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelButtons)).EndInit();
            this.panelButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kgbKryptonMenuStrip.Panel)).EndInit();
            this.kgbKryptonMenuStrip.Panel.ResumeLayout(false);
            this.kgbKryptonMenuStrip.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kgbKryptonMenuStrip)).EndInit();
            this.kgbKryptonMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kgbNativeMenuStrip.Panel)).EndInit();
            this.kgbNativeMenuStrip.Panel.ResumeLayout(false);
            this.kgbNativeMenuStrip.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kgbNativeMenuStrip)).EndInit();
            this.kgbNativeMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kgbLog.Panel)).EndInit();
            this.kgbLog.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kgbLog)).EndInit();
            this.kgbLog.ResumeLayout(false);
            this.kryptonStatusStrip1.ResumeLayout(false);
            this.kryptonStatusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private Krypton.Toolkit.KryptonMenuBar kryptonMenuBar1;
        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonTableLayoutPanel tableLayoutPanel1;
        private Krypton.Toolkit.KryptonLabel klblInstructions;
        private Krypton.Toolkit.KryptonPanel panelButtons;
        private Krypton.Toolkit.KryptonThemeComboBox kryptonThemeComboBox1;
        private Krypton.Toolkit.KryptonButton kbtnInsertStandardItems;
        private Krypton.Toolkit.KryptonButton kbtnClearLog;
        private Krypton.Toolkit.KryptonGroupBox kgbKryptonMenuStrip;
        private Krypton.Toolkit.KryptonMenuStrip kryptonMenuStrip1;
        private Krypton.Toolkit.KryptonGroupBox kgbNativeMenuStrip;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private Krypton.Toolkit.KryptonGroupBox kgbLog;
        private Krypton.Toolkit.KryptonListBox klbLog;
        private Krypton.Toolkit.KryptonStatusStrip kryptonStatusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel klblStatus;
    }
}

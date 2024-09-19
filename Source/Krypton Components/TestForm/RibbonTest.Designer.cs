namespace TestForm
{
    partial class RibbonTest
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
            this.kryptonRibbon1 = new Krypton.Ribbon.KryptonRibbon();
            this.kryptonRibbonTab1 = new Krypton.Ribbon.KryptonRibbonTab();
            this.kryptonRibbonGroup1 = new Krypton.Ribbon.KryptonRibbonGroup();
            this.kryptonRibbonGroupTriple1 = new Krypton.Ribbon.KryptonRibbonGroupTriple();
            this.kryptonRibbonGroupButton1 = new Krypton.Ribbon.KryptonRibbonGroupButton();
            this.kryptonRibbonGroupButton2 = new Krypton.Ribbon.KryptonRibbonGroupButton();
            this.krgbtnTest1715 = new Krypton.Ribbon.KryptonRibbonGroupButton();
            this.kryptonRibbonGroupLines1 = new Krypton.Ribbon.KryptonRibbonGroupLines();
            this.kryptonRibbonGroupButton4 = new Krypton.Ribbon.KryptonRibbonGroupButton();
            this.kryptonRibbonGroupButton5 = new Krypton.Ribbon.KryptonRibbonGroupButton();
            this.kryptonRibbonGroupThemeComboBox1 = new Krypton.Ribbon.KryptonRibbonGroupThemeComboBox();
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonRibbon1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonRibbon1
            // 
            this.kryptonRibbon1.InDesignHelperMode = true;
            this.kryptonRibbon1.Name = "kryptonRibbon1";
            this.kryptonRibbon1.RibbonTabs.AddRange(new Krypton.Ribbon.KryptonRibbonTab[] {
            this.kryptonRibbonTab1});
            this.kryptonRibbon1.SelectedTab = this.kryptonRibbonTab1;
            this.kryptonRibbon1.Size = new System.Drawing.Size(922, 115);
            this.kryptonRibbon1.TabIndex = 0;
            // 
            // kryptonRibbonTab1
            // 
            this.kryptonRibbonTab1.Groups.AddRange(new Krypton.Ribbon.KryptonRibbonGroup[] {
            this.kryptonRibbonGroup1});
            // 
            // kryptonRibbonGroup1
            // 
            this.kryptonRibbonGroup1.Items.AddRange(new Krypton.Ribbon.KryptonRibbonGroupContainer[] {
            this.kryptonRibbonGroupTriple1,
            this.kryptonRibbonGroupLines1});
            // 
            // kryptonRibbonGroupTriple1
            // 
            this.kryptonRibbonGroupTriple1.Items.AddRange(new Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.kryptonRibbonGroupButton1,
            this.kryptonRibbonGroupButton2,
            this.krgbtnTest1715});
            // 
            // krgbtnTest1715
            // 
            this.krgbtnTest1715.TextLine1 = "Test";
            this.krgbtnTest1715.TextLine2 = "#1715";
            this.krgbtnTest1715.Click += new System.EventHandler(this.krgbtnTest1715_Click);
            // 
            // kryptonRibbonGroupLines1
            // 
            this.kryptonRibbonGroupLines1.Items.AddRange(new Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.kryptonRibbonGroupButton4,
            this.kryptonRibbonGroupButton5,
            this.kryptonRibbonGroupThemeComboBox1});
            // 
            // kryptonRibbonGroupThemeComboBox1
            // 
            this.kryptonRibbonGroupThemeComboBox1.DefaultPalette = Krypton.Toolkit.PaletteMode.Office2007Silver;
            this.kryptonRibbonGroupThemeComboBox1.DropDownWidth = 121;
            this.kryptonRibbonGroupThemeComboBox1.FormattingEnabled = false;
            this.kryptonRibbonGroupThemeComboBox1.ItemHeight = 16;
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 115);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(922, 513);
            this.kryptonPanel1.TabIndex = 1;
            // 
            // RibbonTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 628);
            this.CloseBox = false;
            this.Controls.Add(this.kryptonPanel1);
            this.Controls.Add(this.kryptonRibbon1);
            this.Name = "RibbonTest";
            this.Text = "RibbonTest";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonRibbon1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Krypton.Ribbon.KryptonRibbon kryptonRibbon1;
        private Krypton.Ribbon.KryptonRibbonTab kryptonRibbonTab1;
        private Krypton.Ribbon.KryptonRibbonGroup kryptonRibbonGroup1;
        private Krypton.Ribbon.KryptonRibbonGroupTriple kryptonRibbonGroupTriple1;
        private Krypton.Ribbon.KryptonRibbonGroupButton kryptonRibbonGroupButton1;
        private Krypton.Ribbon.KryptonRibbonGroupButton kryptonRibbonGroupButton2;
        private Krypton.Ribbon.KryptonRibbonGroupButton krgbtnTest1715;
        private Krypton.Ribbon.KryptonRibbonGroupLines kryptonRibbonGroupLines1;
        private Krypton.Ribbon.KryptonRibbonGroupButton kryptonRibbonGroupButton4;
        private Krypton.Ribbon.KryptonRibbonGroupButton kryptonRibbonGroupButton5;
        private Krypton.Ribbon.KryptonRibbonGroupThemeComboBox kryptonRibbonGroupThemeComboBox1;
        private KryptonPanel kryptonPanel1;
    }
}
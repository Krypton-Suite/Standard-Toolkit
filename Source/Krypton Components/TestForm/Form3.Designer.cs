namespace TestForm
{
    partial class Form3
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
            this.kryptonRibbonGroupButton3 = new Krypton.Ribbon.KryptonRibbonGroupButton();
            this.kryptonRibbonGroupSeparator1 = new Krypton.Ribbon.KryptonRibbonGroupSeparator();
            this.kryptonRibbonGroupLines1 = new Krypton.Ribbon.KryptonRibbonGroupLines();
            this.kryptonRibbonGroupButton4 = new Krypton.Ribbon.KryptonRibbonGroupButton();
            this.kryptonRibbonGroupButton5 = new Krypton.Ribbon.KryptonRibbonGroupButton();
            this.kryptonRibbonQATButton1 = new Krypton.Ribbon.KryptonRibbonQATButton();
            this.kryptonRibbonQATButton2 = new Krypton.Ribbon.KryptonRibbonQATButton();
            this.kryptonRibbonQATButton3 = new Krypton.Ribbon.KryptonRibbonQATButton();
            this.kryptonRibbonQATButton4 = new Krypton.Ribbon.KryptonRibbonQATButton();
            this.kryptonThemeComboBox1 = new Krypton.Toolkit.KryptonThemeComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonRibbon1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonRibbon1
            // 
            this.kryptonRibbon1.AllowFormIntegrate = true;
            this.kryptonRibbon1.InDesignHelperMode = true;
            this.kryptonRibbon1.Name = "kryptonRibbon1";
            this.kryptonRibbon1.QATButtons.AddRange(new System.ComponentModel.Component[] {
            this.kryptonRibbonQATButton1,
            this.kryptonRibbonQATButton2,
            this.kryptonRibbonQATButton3,
            this.kryptonRibbonQATButton4});
            this.kryptonRibbon1.RibbonTabs.AddRange(new Krypton.Ribbon.KryptonRibbonTab[] {
            this.kryptonRibbonTab1});
            this.kryptonRibbon1.SelectedTab = this.kryptonRibbonTab1;
            this.kryptonRibbon1.Size = new System.Drawing.Size(800, 115);
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
            this.kryptonRibbonGroupSeparator1,
            this.kryptonRibbonGroupLines1});
            // 
            // kryptonRibbonGroupTriple1
            // 
            this.kryptonRibbonGroupTriple1.Items.AddRange(new Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.kryptonRibbonGroupButton1,
            this.kryptonRibbonGroupButton2,
            this.kryptonRibbonGroupButton3});
            // 
            // kryptonRibbonGroupLines1
            // 
            this.kryptonRibbonGroupLines1.Items.AddRange(new Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.kryptonRibbonGroupButton4,
            this.kryptonRibbonGroupButton5});
            // 
            // kryptonThemeComboBox1
            // 
            this.kryptonThemeComboBox1.DropDownWidth = 280;
            this.kryptonThemeComboBox1.IntegralHeight = false;
            this.kryptonThemeComboBox1.Location = new System.Drawing.Point(65, 239);
            this.kryptonThemeComboBox1.Name = "kryptonThemeComboBox1";
            this.kryptonThemeComboBox1.Size = new System.Drawing.Size(280, 21);
            this.kryptonThemeComboBox1.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kryptonThemeComboBox1.TabIndex = 1;
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.kryptonThemeComboBox1);
            this.Controls.Add(this.kryptonRibbon1);
            this.Name = "Form3";
            this.Text = "Form3";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonRibbon1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).EndInit();
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
        private Krypton.Ribbon.KryptonRibbonGroupButton kryptonRibbonGroupButton3;
        private Krypton.Ribbon.KryptonRibbonGroupSeparator kryptonRibbonGroupSeparator1;
        private Krypton.Ribbon.KryptonRibbonGroupLines kryptonRibbonGroupLines1;
        private Krypton.Ribbon.KryptonRibbonGroupButton kryptonRibbonGroupButton4;
        private Krypton.Ribbon.KryptonRibbonGroupButton kryptonRibbonGroupButton5;
        private Krypton.Ribbon.KryptonRibbonQATButton kryptonRibbonQATButton1;
        private Krypton.Ribbon.KryptonRibbonQATButton kryptonRibbonQATButton2;
        private Krypton.Ribbon.KryptonRibbonQATButton kryptonRibbonQATButton3;
        private Krypton.Ribbon.KryptonRibbonQATButton kryptonRibbonQATButton4;
        private Krypton.Toolkit.KryptonThemeComboBox kryptonThemeComboBox1;
    }
}
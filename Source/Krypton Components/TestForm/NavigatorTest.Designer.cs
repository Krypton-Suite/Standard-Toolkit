namespace TestForm
{
    partial class NavigatorTest
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
            this.kryptonRibbon1 = new Krypton.Ribbon.KryptonRibbon();
            this.kryptonRibbonTab1 = new Krypton.Ribbon.KryptonRibbonTab();
            this.checkSetDocMode = new Krypton.Toolkit.KryptonCheckSet(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonRibbon1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkSetDocMode)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonRibbon1
            // 
            this.kryptonRibbon1.InDesignHelperMode = true;
            this.kryptonRibbon1.Name = "kryptonRibbon1";
            this.kryptonRibbon1.RibbonFileAppButton.AppButtonToolTipStyle = Krypton.Toolkit.LabelStyle.SuperTip;
            this.kryptonRibbon1.RibbonTabs.AddRange(new Krypton.Ribbon.KryptonRibbonTab[] {
            this.kryptonRibbonTab1});
            this.kryptonRibbon1.SelectedTab = this.kryptonRibbonTab1;
            this.kryptonRibbon1.Size = new System.Drawing.Size(843, 115);
            this.kryptonRibbon1.TabIndex = 0;
            // 
            // NavigatorTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 683);
            this.CloseBox = false;
            this.Controls.Add(this.kryptonRibbon1);
            this.Name = "NavigatorTest";
            this.Text = "NavigatorTest";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonRibbon1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkSetDocMode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Krypton.Ribbon.KryptonRibbon kryptonRibbon1;
        private Krypton.Ribbon.KryptonRibbonTab kryptonRibbonTab1;
        private KryptonCheckSet checkSetDocMode;
    }
}
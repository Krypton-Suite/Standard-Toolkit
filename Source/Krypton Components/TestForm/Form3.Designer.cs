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
            kryptonRibbon1 = new Krypton.Ribbon.KryptonRibbon();
            kryptonRibbonTab1 = new Krypton.Ribbon.KryptonRibbonTab();
            kryptonRibbonGroup1 = new Krypton.Ribbon.KryptonRibbonGroup();
            kryptonRibbonGroupTriple1 = new Krypton.Ribbon.KryptonRibbonGroupTriple();
            kryptonRibbonGroupButton1 = new Krypton.Ribbon.KryptonRibbonGroupButton();
            kryptonRibbonGroupButton2 = new Krypton.Ribbon.KryptonRibbonGroupButton();
            kryptonRibbonGroupButton3 = new Krypton.Ribbon.KryptonRibbonGroupButton();
            kryptonRibbonGroup2 = new Krypton.Ribbon.KryptonRibbonGroup();
            kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            ((System.ComponentModel.ISupportInitialize)kryptonRibbon1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kryptonPanel1).BeginInit();
            SuspendLayout();
            // 
            // kryptonRibbon1
            // 
            kryptonRibbon1.AllowFormIntegrate = true;
            kryptonRibbon1.InDesignHelperMode = true;
            kryptonRibbon1.Name = "kryptonRibbon1";
            kryptonRibbon1.RibbonTabs.AddRange(new Krypton.Ribbon.KryptonRibbonTab[] { kryptonRibbonTab1 });
            kryptonRibbon1.SelectedContext = null;
            kryptonRibbon1.SelectedTab = kryptonRibbonTab1;
            kryptonRibbon1.Size = new System.Drawing.Size(933, 115);
            kryptonRibbon1.TabIndex = 0;
            // 
            // kryptonRibbonTab1
            // 
            kryptonRibbonTab1.Groups.AddRange(new Krypton.Ribbon.KryptonRibbonGroup[] { kryptonRibbonGroup1, kryptonRibbonGroup2 });
            // 
            // kryptonRibbonGroup1
            // 
            kryptonRibbonGroup1.Items.AddRange(new Krypton.Ribbon.KryptonRibbonGroupContainer[] { kryptonRibbonGroupTriple1 });
            // 
            // kryptonRibbonGroupTriple1
            // 
            kryptonRibbonGroupTriple1.Items.AddRange(new Krypton.Ribbon.KryptonRibbonGroupItem[] { kryptonRibbonGroupButton1, kryptonRibbonGroupButton2, kryptonRibbonGroupButton3 });
            // 
            // kryptonPanel1
            // 
            kryptonPanel1.Location = new System.Drawing.Point(225, 224);
            kryptonPanel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            kryptonPanel1.Name = "kryptonPanel1";
            kryptonPanel1.Size = new System.Drawing.Size(117, 115);
            kryptonPanel1.TabIndex = 1;
            // 
            // Form3
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(933, 519);
            Controls.Add(kryptonPanel1);
            Controls.Add(kryptonRibbon1);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "Form3";
            Text = "Form3";
            Controls.SetChildIndex(kryptonRibbon1, 0);
            Controls.SetChildIndex(kryptonPanel1, 0);
            ((System.ComponentModel.ISupportInitialize)kryptonRibbon1).EndInit();
            ((System.ComponentModel.ISupportInitialize)kryptonPanel1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Krypton.Ribbon.KryptonRibbon kryptonRibbon1;
        private Krypton.Ribbon.KryptonRibbonTab kryptonRibbonTab1;
        private Krypton.Ribbon.KryptonRibbonGroup kryptonRibbonGroup1;
        private Krypton.Ribbon.KryptonRibbonGroupTriple kryptonRibbonGroupTriple1;
        private Krypton.Ribbon.KryptonRibbonGroupButton kryptonRibbonGroupButton1;
        private Krypton.Ribbon.KryptonRibbonGroupButton kryptonRibbonGroupButton2;
        private Krypton.Ribbon.KryptonRibbonGroupButton kryptonRibbonGroupButton3;
        private Krypton.Ribbon.KryptonRibbonGroup kryptonRibbonGroup2;
        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
    }
}
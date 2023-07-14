namespace TestForm
{
    partial class Form5
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
            this.kryptonLanguageManager1 = new Krypton.Toolkit.KryptonLanguageManager();
            this.kryptonIntegratedToolBarManager1 = new Krypton.Toolkit.KryptonIntegratedToolBarManager();
            this.SuspendLayout();
            // 
            // kryptonLanguageManager1
            // 
            this.kryptonLanguageManager1.PaletteContentStyleStrings.ButtonGallery = null;
            this.kryptonLanguageManager1.PaletteContentStyleStrings.GridHeaderRowList = "Grid - RowColumn - List";
            // 
            // kryptonIntegratedToolBarManager1
            // 
            this.kryptonIntegratedToolBarManager1.IntegratedToolBarButtonAlignment = Krypton.Toolkit.PaletteRelativeEdgeAlign.Near;
            this.kryptonIntegratedToolBarManager1.IntegratedToolBarButtonOrientation = Krypton.Toolkit.PaletteButtonOrientation.Auto;
            this.kryptonIntegratedToolBarManager1.ParentForm = this;
            this.kryptonIntegratedToolBarManager1.ShowCopyButton = true;
            this.kryptonIntegratedToolBarManager1.ShowCutButton = true;
            this.kryptonIntegratedToolBarManager1.ShowIntegratedToolBar = false;
            this.kryptonIntegratedToolBarManager1.ShowNewButton = true;
            this.kryptonIntegratedToolBarManager1.ShowOpenButton = true;
            this.kryptonIntegratedToolBarManager1.ShowPageSetupButton = true;
            this.kryptonIntegratedToolBarManager1.ShowPasteButton = true;
            this.kryptonIntegratedToolBarManager1.ShowPrintButton = true;
            this.kryptonIntegratedToolBarManager1.ShowPrintPreviewButton = true;
            this.kryptonIntegratedToolBarManager1.ShowQuickPrintButton = true;
            this.kryptonIntegratedToolBarManager1.ShowRedoButton = true;
            this.kryptonIntegratedToolBarManager1.ShowSaveAllButton = true;
            this.kryptonIntegratedToolBarManager1.ShowSaveAsButton = true;
            this.kryptonIntegratedToolBarManager1.ShowSaveButton = true;
            this.kryptonIntegratedToolBarManager1.ShowUndoButton = true;
            // 
            // Form5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "Form5";
            this.Text = "Form5";
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonLanguageManager kryptonLanguageManager1;
        private Krypton.Toolkit.KryptonIntegratedToolBarManager kryptonIntegratedToolBarManager1;
    }
}
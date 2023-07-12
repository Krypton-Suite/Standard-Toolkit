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
            this.kryptonIntegratedToolBarManager1 = new Krypton.Toolkit.KryptonIntegratedToolBarManager();
            this.kryptonLanguageManager1 = new Krypton.Toolkit.KryptonLanguageManager();
            this.SuspendLayout();
            // 
            // kryptonIntegratedToolBarManager1
            // 
            this.kryptonIntegratedToolBarManager1.ParentForm = this;
            this.kryptonIntegratedToolBarManager1.ShowIntegratedToolBar = true;
            this.kryptonIntegratedToolBarManager1.ShowStandardIntegratedToolBarItems = false;
            this.kryptonIntegratedToolBarManager1.ToolBarButtonAlignment = Krypton.Toolkit.PaletteRelativeEdgeAlign.Far;
            this.kryptonIntegratedToolBarManager1.ToolBarButtonOrientation = Krypton.Toolkit.PaletteButtonOrientation.Auto;
            this.kryptonIntegratedToolBarManager1.ToolBarCommands.CopyButtonCommand = null;
            this.kryptonIntegratedToolBarManager1.ToolBarCommands.CutButtonCommand = null;
            this.kryptonIntegratedToolBarManager1.ToolBarCommands.NewButtonCommand = null;
            this.kryptonIntegratedToolBarManager1.ToolBarCommands.OpenButtonCommand = null;
            this.kryptonIntegratedToolBarManager1.ToolBarCommands.PageSetupButtonCommand = null;
            this.kryptonIntegratedToolBarManager1.ToolBarCommands.PasteButtonCommand = null;
            this.kryptonIntegratedToolBarManager1.ToolBarCommands.PrintButtonCommand = null;
            this.kryptonIntegratedToolBarManager1.ToolBarCommands.PrintPreviewButtonCommand = null;
            this.kryptonIntegratedToolBarManager1.ToolBarCommands.QuickPrintButtonCommand = null;
            this.kryptonIntegratedToolBarManager1.ToolBarCommands.RedoButtonCommand = null;
            this.kryptonIntegratedToolBarManager1.ToolBarCommands.SaveAllButtonCommand = null;
            this.kryptonIntegratedToolBarManager1.ToolBarCommands.SaveAsButtonCommand = null;
            this.kryptonIntegratedToolBarManager1.ToolBarCommands.SaveButtonCommand = null;
            this.kryptonIntegratedToolBarManager1.ToolBarCommands.UndoButtonCommand = null;
            // 
            // kryptonLanguageManager1
            // 
            this.kryptonLanguageManager1.PaletteContentStyleStrings.ButtonGallery = null;
            this.kryptonLanguageManager1.PaletteContentStyleStrings.GridHeaderRowList = "Grid - RowColumn - List";
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

        private Krypton.Toolkit.KryptonIntegratedToolBarManager kryptonIntegratedToolBarManager1;
        private Krypton.Toolkit.KryptonLanguageManager kryptonLanguageManager1;
    }
}
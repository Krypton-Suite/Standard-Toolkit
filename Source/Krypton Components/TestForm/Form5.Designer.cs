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
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonPropertyGrid1 = new Krypton.Toolkit.KryptonPropertyGrid();
            this.kryptonThemeComboBox1 = new Krypton.Toolkit.KryptonThemeComboBox();
            this.kryptonIntegratedToolBarManager1 = new Krypton.Toolkit.KryptonIntegratedToolBarManager();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonLanguageManager1
            // 
            this.kryptonLanguageManager1.PaletteContentStyleStrings.ButtonGallery = null;
            this.kryptonLanguageManager1.PaletteContentStyleStrings.GridHeaderRowList = "Grid - RowColumn - List";
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonPropertyGrid1);
            this.kryptonPanel1.Controls.Add(this.kryptonThemeComboBox1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(800, 450);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonPropertyGrid1
            // 
            this.kryptonPropertyGrid1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.kryptonPropertyGrid1.CategoryForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.kryptonPropertyGrid1.CommandsBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.kryptonPropertyGrid1.CommandsForeColor = System.Drawing.Color.Black;
            this.kryptonPropertyGrid1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.kryptonPropertyGrid1.HelpBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.kryptonPropertyGrid1.HelpForeColor = System.Drawing.Color.Black;
            this.kryptonPropertyGrid1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(196)))), ((int)(((byte)(216)))));
            this.kryptonPropertyGrid1.Location = new System.Drawing.Point(13, 41);
            this.kryptonPropertyGrid1.Name = "kryptonPropertyGrid1";
            this.kryptonPropertyGrid1.SelectedObject = this.kryptonIntegratedToolBarManager1;
            this.kryptonPropertyGrid1.Size = new System.Drawing.Size(344, 397);
            this.kryptonPropertyGrid1.TabIndex = 1;
            this.kryptonPropertyGrid1.ViewBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.kryptonPropertyGrid1.ViewForeColor = System.Drawing.Color.Black;
            // 
            // kryptonThemeComboBox1
            // 
            this.kryptonThemeComboBox1.DropDownWidth = 344;
            this.kryptonThemeComboBox1.IntegralHeight = false;
            this.kryptonThemeComboBox1.Location = new System.Drawing.Point(13, 13);
            this.kryptonThemeComboBox1.Name = "kryptonThemeComboBox1";
            this.kryptonThemeComboBox1.Size = new System.Drawing.Size(344, 21);
            this.kryptonThemeComboBox1.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kryptonThemeComboBox1.TabIndex = 0;
            // 
            // kryptonIntegratedToolBarManager1
            // 
            this.kryptonIntegratedToolBarManager1.IntegratedToolBarButtonAlignment = Krypton.Toolkit.PaletteRelativeEdgeAlign.Near;
            this.kryptonIntegratedToolBarManager1.IntegratedToolBarButtonOrientation = Krypton.Toolkit.PaletteButtonOrientation.FixedTop;
            this.kryptonIntegratedToolBarManager1.ParentForm = this;
            this.kryptonIntegratedToolBarManager1.ShowCopyButton = false;
            this.kryptonIntegratedToolBarManager1.ShowCutButton = false;
            this.kryptonIntegratedToolBarManager1.ShowIntegratedToolBar = false;
            this.kryptonIntegratedToolBarManager1.ShowNewButton = false;
            this.kryptonIntegratedToolBarManager1.ShowOpenButton = false;
            this.kryptonIntegratedToolBarManager1.ShowPageSetupButton = false;
            this.kryptonIntegratedToolBarManager1.ShowPasteButton = false;
            this.kryptonIntegratedToolBarManager1.ShowPrintButton = false;
            this.kryptonIntegratedToolBarManager1.ShowPrintPreviewButton = false;
            this.kryptonIntegratedToolBarManager1.ShowQuickPrintButton = false;
            this.kryptonIntegratedToolBarManager1.ShowRedoButton = false;
            this.kryptonIntegratedToolBarManager1.ShowSaveAllButton = false;
            this.kryptonIntegratedToolBarManager1.ShowSaveAsButton = false;
            this.kryptonIntegratedToolBarManager1.ShowSaveButton = false;
            this.kryptonIntegratedToolBarManager1.ShowUndoButton = false;
            // 
            // Form5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "Form5";
            this.Text = "Form5";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonLanguageManager kryptonLanguageManager1;
        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonThemeComboBox kryptonThemeComboBox1;
        private Krypton.Toolkit.KryptonPropertyGrid kryptonPropertyGrid1;
        private Krypton.Toolkit.KryptonIntegratedToolBarManager kryptonIntegratedToolBarManager1;
    }
}
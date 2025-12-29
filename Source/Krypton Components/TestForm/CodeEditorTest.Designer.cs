namespace TestForm
{
    partial class CodeEditorTest
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
            this.kpnlMain = new Krypton.Toolkit.KryptonPanel();
            this.kceEditor = new Krypton.Utilities.KryptonCodeEditor();
            this.kpnlTop = new Krypton.Toolkit.KryptonPanel();
            this.kpgbOptions = new Krypton.Toolkit.KryptonGroupBox();
            this.kpnlOptions = new Krypton.Toolkit.KryptonPanel();
            this.kchkAutoComplete = new Krypton.Toolkit.KryptonCheckBox();
            this.kchkCodeFolding = new Krypton.Toolkit.KryptonCheckBox();
            this.kchkShowLineNumbers = new Krypton.Toolkit.KryptonCheckBox();
            this.kcmbTheme = new Krypton.Toolkit.KryptonComboBox();
            this.klblTheme = new Krypton.Toolkit.KryptonLabel();
            this.kcmbLanguage = new Krypton.Toolkit.KryptonComboBox();
            this.klblLanguage = new Krypton.Toolkit.KryptonLabel();
            this.kpgbFile = new Krypton.Toolkit.KryptonGroupBox();
            this.kpnlFile = new Krypton.Toolkit.KryptonPanel();
            this.kbtnSaveFile = new Krypton.Toolkit.KryptonButton();
            this.kbtnLoadFile = new Krypton.Toolkit.KryptonButton();
            this.kpnlBottom = new Krypton.Toolkit.KryptonPanel();
            this.kbtnClose = new Krypton.Toolkit.KryptonButton();
            this.kryptonBorderEdge1 = new Krypton.Toolkit.KryptonBorderEdge();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).BeginInit();
            this.kpnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlTop)).BeginInit();
            this.kpnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpgbOptions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kpgbOptions.Panel)).BeginInit();
            this.kpgbOptions.Panel.SuspendLayout();
            this.kpgbOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlOptions)).BeginInit();
            this.kpnlOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbTheme)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbLanguage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kpgbFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kpgbFile.Panel)).BeginInit();
            this.kpgbFile.Panel.SuspendLayout();
            this.kpgbFile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlFile)).BeginInit();
            this.kpnlFile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlBottom)).BeginInit();
            this.kpnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // kpnlMain
            // 
            this.kpnlMain.Controls.Add(this.kceEditor);
            this.kpnlMain.Controls.Add(this.kpnlTop);
            this.kpnlMain.Controls.Add(this.kpnlBottom);
            this.kpnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlMain.Location = new System.Drawing.Point(0, 0);
            this.kpnlMain.Name = "kpnlMain";
            this.kpnlMain.Size = new System.Drawing.Size(857, 630);
            this.kpnlMain.TabIndex = 0;
            // 
            // kceEditor
            // 
            this.kceEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kceEditor.FoldingMarginStateCommon.Back.Color1 = System.Drawing.Color.Red;
            this.kceEditor.FoldingMarginStateCommon.Back.Color2 = System.Drawing.Color.Red;
            this.kceEditor.Location = new System.Drawing.Point(0, 98);
            this.kceEditor.Name = "kceEditor";
            this.kceEditor.Size = new System.Drawing.Size(857, 489);
            this.kceEditor.TabIndex = 1;
            // 
            // kpnlTop
            // 
            this.kpnlTop.Controls.Add(this.kpgbOptions);
            this.kpnlTop.Controls.Add(this.kpgbFile);
            this.kpnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.kpnlTop.Location = new System.Drawing.Point(0, 0);
            this.kpnlTop.Name = "kpnlTop";
            this.kpnlTop.Size = new System.Drawing.Size(857, 98);
            this.kpnlTop.TabIndex = 0;
            // 
            // kpgbOptions
            // 
            this.kpgbOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpgbOptions.Location = new System.Drawing.Point(171, 0);
            // 
            // kpgbOptions.Panel
            // 
            this.kpgbOptions.Panel.Controls.Add(this.kpnlOptions);
            this.kpgbOptions.Size = new System.Drawing.Size(686, 98);
            this.kpgbOptions.TabIndex = 1;
            this.kpgbOptions.Values.Heading = "Editor Options";
            // 
            // kpnlOptions
            // 
            this.kpnlOptions.Controls.Add(this.kchkAutoComplete);
            this.kpnlOptions.Controls.Add(this.kchkCodeFolding);
            this.kpnlOptions.Controls.Add(this.kchkShowLineNumbers);
            this.kpnlOptions.Controls.Add(this.kcmbTheme);
            this.kpnlOptions.Controls.Add(this.klblTheme);
            this.kpnlOptions.Controls.Add(this.kcmbLanguage);
            this.kpnlOptions.Controls.Add(this.klblLanguage);
            this.kpnlOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlOptions.Location = new System.Drawing.Point(0, 0);
            this.kpnlOptions.Name = "kpnlOptions";
            this.kpnlOptions.Size = new System.Drawing.Size(682, 74);
            this.kpnlOptions.TabIndex = 0;
            // 
            // kchkAutoComplete
            // 
            this.kchkAutoComplete.Location = new System.Drawing.Point(514, 13);
            this.kchkAutoComplete.Name = "kchkAutoComplete";
            this.kchkAutoComplete.Size = new System.Drawing.Size(108, 20);
            this.kchkAutoComplete.TabIndex = 4;
            this.kchkAutoComplete.Values.Text = "Auto-Complete";
            this.kchkAutoComplete.CheckedChanged += new System.EventHandler(this.kchkAutoComplete_CheckedChanged);
            // 
            // kchkCodeFolding
            // 
            this.kchkCodeFolding.Location = new System.Drawing.Point(386, 13);
            this.kchkCodeFolding.Name = "kchkCodeFolding";
            this.kchkCodeFolding.Size = new System.Drawing.Size(96, 20);
            this.kchkCodeFolding.TabIndex = 3;
            this.kchkCodeFolding.Values.Text = "Code Folding";
            this.kchkCodeFolding.CheckedChanged += new System.EventHandler(this.kchkCodeFolding_CheckedChanged);
            // 
            // kchkShowLineNumbers
            // 
            this.kchkShowLineNumbers.Checked = true;
            this.kchkShowLineNumbers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.kchkShowLineNumbers.Location = new System.Drawing.Point(257, 13);
            this.kchkShowLineNumbers.Name = "kchkShowLineNumbers";
            this.kchkShowLineNumbers.Size = new System.Drawing.Size(99, 20);
            this.kchkShowLineNumbers.TabIndex = 2;
            this.kchkShowLineNumbers.Values.Text = "Line Numbers";
            this.kchkShowLineNumbers.CheckedChanged += new System.EventHandler(this.kchkShowLineNumbers_CheckedChanged);
            // 
            // kcmbTheme
            // 
            this.kcmbTheme.DropDownWidth = 150;
            this.kcmbTheme.Location = new System.Drawing.Point(86, 39);
            this.kcmbTheme.Name = "kcmbTheme";
            this.kcmbTheme.Size = new System.Drawing.Size(129, 22);
            this.kcmbTheme.TabIndex = 5;
            this.kcmbTheme.SelectedIndexChanged += new System.EventHandler(this.kcmbTheme_SelectedIndexChanged);
            // 
            // klblTheme
            // 
            this.klblTheme.Location = new System.Drawing.Point(9, 41);
            this.klblTheme.Name = "klblTheme";
            this.klblTheme.Size = new System.Drawing.Size(50, 20);
            this.klblTheme.TabIndex = 6;
            this.klblTheme.Values.Text = "Theme:";
            // 
            // kcmbLanguage
            // 
            this.kcmbLanguage.DropDownWidth = 150;
            this.kcmbLanguage.Location = new System.Drawing.Point(86, 13);
            this.kcmbLanguage.Name = "kcmbLanguage";
            this.kcmbLanguage.Size = new System.Drawing.Size(129, 22);
            this.kcmbLanguage.TabIndex = 1;
            this.kcmbLanguage.SelectedIndexChanged += new System.EventHandler(this.kcmbLanguage_SelectedIndexChanged);
            // 
            // klblLanguage
            // 
            this.klblLanguage.Location = new System.Drawing.Point(9, 15);
            this.klblLanguage.Name = "klblLanguage";
            this.klblLanguage.Size = new System.Drawing.Size(67, 20);
            this.klblLanguage.TabIndex = 0;
            this.klblLanguage.Values.Text = "Language:";
            // 
            // kpgbFile
            // 
            this.kpgbFile.Dock = System.Windows.Forms.DockStyle.Left;
            this.kpgbFile.Location = new System.Drawing.Point(0, 0);
            // 
            // kpgbFile.Panel
            // 
            this.kpgbFile.Panel.Controls.Add(this.kpnlFile);
            this.kpgbFile.Size = new System.Drawing.Size(171, 98);
            this.kpgbFile.TabIndex = 0;
            this.kpgbFile.Values.Heading = "File";
            // 
            // kpnlFile
            // 
            this.kpnlFile.Controls.Add(this.kbtnSaveFile);
            this.kpnlFile.Controls.Add(this.kbtnLoadFile);
            this.kpnlFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlFile.Location = new System.Drawing.Point(0, 0);
            this.kpnlFile.Name = "kpnlFile";
            this.kpnlFile.Size = new System.Drawing.Size(167, 74);
            this.kpnlFile.TabIndex = 0;
            // 
            // kbtnSaveFile
            // 
            this.kbtnSaveFile.Location = new System.Drawing.Point(86, 9);
            this.kbtnSaveFile.Name = "kbtnSaveFile";
            this.kbtnSaveFile.Size = new System.Drawing.Size(77, 22);
            this.kbtnSaveFile.TabIndex = 1;
            this.kbtnSaveFile.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnSaveFile.Values.Text = "Save...";
            this.kbtnSaveFile.Click += new System.EventHandler(this.kbtnSaveFile_Click);
            // 
            // kbtnLoadFile
            // 
            this.kbtnLoadFile.Location = new System.Drawing.Point(9, 9);
            this.kbtnLoadFile.Name = "kbtnLoadFile";
            this.kbtnLoadFile.Size = new System.Drawing.Size(77, 22);
            this.kbtnLoadFile.TabIndex = 0;
            this.kbtnLoadFile.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnLoadFile.Values.Text = "Load...";
            this.kbtnLoadFile.Click += new System.EventHandler(this.kbtnLoadFile_Click);
            // 
            // kpnlBottom
            // 
            this.kpnlBottom.Controls.Add(this.kbtnClose);
            this.kpnlBottom.Controls.Add(this.kryptonBorderEdge1);
            this.kpnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kpnlBottom.Location = new System.Drawing.Point(0, 587);
            this.kpnlBottom.Name = "kpnlBottom";
            this.kpnlBottom.Size = new System.Drawing.Size(857, 43);
            this.kpnlBottom.TabIndex = 2;
            // 
            // kbtnClose
            // 
            this.kbtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnClose.Location = new System.Drawing.Point(771, 10);
            this.kbtnClose.Name = "kbtnClose";
            this.kbtnClose.Size = new System.Drawing.Size(77, 22);
            this.kbtnClose.TabIndex = 1;
            this.kbtnClose.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnClose.Values.Text = "Close";
            this.kbtnClose.Click += new System.EventHandler(this.kbtnClose_Click);
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(0, 0);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(857, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // CodeEditorTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 630);
            this.Controls.Add(this.kpnlMain);
            this.MinimumSize = new System.Drawing.Size(688, 525);
            this.Name = "CodeEditorTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Code Editor Test";
            ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).EndInit();
            this.kpnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kpnlTop)).EndInit();
            this.kpnlTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kpgbOptions.Panel)).EndInit();
            this.kpgbOptions.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kpgbOptions)).EndInit();
            this.kpgbOptions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kpnlOptions)).EndInit();
            this.kpnlOptions.ResumeLayout(false);
            this.kpnlOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbTheme)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbLanguage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kpgbFile.Panel)).EndInit();
            this.kpgbFile.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kpgbFile)).EndInit();
            this.kpgbFile.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kpnlFile)).EndInit();
            this.kpnlFile.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kpnlBottom)).EndInit();
            this.kpnlBottom.ResumeLayout(false);
            this.kpnlBottom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kpnlMain;
        private Krypton.Utilities.KryptonCodeEditor kceEditor;
        private Krypton.Toolkit.KryptonPanel kpnlTop;
        private Krypton.Toolkit.KryptonGroupBox kpgbOptions;
        private Krypton.Toolkit.KryptonPanel kpnlOptions;
        private Krypton.Toolkit.KryptonComboBox kcmbLanguage;
        private Krypton.Toolkit.KryptonLabel klblLanguage;
        private Krypton.Toolkit.KryptonComboBox kcmbTheme;
        private Krypton.Toolkit.KryptonLabel klblTheme;
        private Krypton.Toolkit.KryptonCheckBox kchkShowLineNumbers;
        private Krypton.Toolkit.KryptonCheckBox kchkCodeFolding;
        private Krypton.Toolkit.KryptonCheckBox kchkAutoComplete;
        private Krypton.Toolkit.KryptonGroupBox kpgbFile;
        private Krypton.Toolkit.KryptonPanel kpnlFile;
        private Krypton.Toolkit.KryptonButton kbtnLoadFile;
        private Krypton.Toolkit.KryptonButton kbtnSaveFile;
        private Krypton.Toolkit.KryptonPanel kpnlBottom;
        private Krypton.Toolkit.KryptonButton kbtnClose;
        private Krypton.Toolkit.KryptonBorderEdge kryptonBorderEdge1;
    }
}
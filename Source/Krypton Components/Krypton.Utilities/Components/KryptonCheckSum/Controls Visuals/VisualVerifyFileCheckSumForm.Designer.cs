namespace Krypton.Utilities
{
    partial class VisualVerifyFileCheckSumForm
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
            this.ss = new Krypton.Toolkit.KryptonStatusStrip();
            this.tslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.kpbtsiCalculationProgress = new Krypton.Toolkit.KryptonProgressBarToolStripItem();
            this.bgwMD5 = new System.ComponentModel.BackgroundWorker();
            this.bgwSHA1 = new System.ComponentModel.BackgroundWorker();
            this.bgwSHA256 = new System.ComponentModel.BackgroundWorker();
            this.bgwSHA384 = new System.ComponentModel.BackgroundWorker();
            this.bgwSHA512 = new System.ComponentModel.BackgroundWorker();
            this.bgwRIPEMD160 = new System.ComponentModel.BackgroundWorker();
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kbtnVerify = new Krypton.Toolkit.KryptonButton();
            this.kryptonBorderEdge1 = new Krypton.Toolkit.KryptonBorderEdge();
            this.kbtnCancel = new Krypton.Toolkit.KryptonButton();
            this.kryptonPanel2 = new Krypton.Toolkit.KryptonPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.kryptonWrapLabel1 = new Krypton.Toolkit.KryptonWrapLabel();
            this.kryptonWrapLabel2 = new Krypton.Toolkit.KryptonWrapLabel();
            this.ktxtFilePath = new Krypton.Toolkit.KryptonTextBox();
            this.bsaReset = new Krypton.Toolkit.ButtonSpecAny();
            this.bsaBrowse = new Krypton.Toolkit.ButtonSpecAny();
            this.kcmbHashType = new Krypton.Toolkit.KryptonComboBox();
            this.kryptonGroupBox1 = new Krypton.Toolkit.KryptonGroupBox();
            this.kwlHashOutput = new Krypton.Toolkit.KryptonWrapLabel();
            this.kryptonGroupBox2 = new Krypton.Toolkit.KryptonGroupBox();
            this.ktxtVarifyCheckSum = new Krypton.Toolkit.KryptonTextBox();
            this.bsaVerifyReset = new Krypton.Toolkit.ButtonSpecAny();
            this.bsaVerifyBrowse = new Krypton.Toolkit.ButtonSpecAny();
            this.kcmHashVerify = new Krypton.Toolkit.KryptonContextMenu();
            this.kryptonContextMenuItems1 = new Krypton.Toolkit.KryptonContextMenuItems();
            this.kryptonContextMenuItem1 = new Krypton.Toolkit.KryptonContextMenuItem();
            this.kcmdCut = new Krypton.Toolkit.KryptonCommand();
            this.kryptonContextMenuSeparator1 = new Krypton.Toolkit.KryptonContextMenuSeparator();
            this.kryptonContextMenuItem2 = new Krypton.Toolkit.KryptonContextMenuItem();
            this.kcmdCopy = new Krypton.Toolkit.KryptonCommand();
            this.kryptonContextMenuSeparator2 = new Krypton.Toolkit.KryptonContextMenuSeparator();
            this.kryptonContextMenuItem3 = new Krypton.Toolkit.KryptonContextMenuItem();
            this.kcmdPaste = new Krypton.Toolkit.KryptonCommand();
            this.kryptonContextMenuSeparator3 = new Krypton.Toolkit.KryptonContextMenuSeparator();
            this.kryptonContextMenuItem4 = new Krypton.Toolkit.KryptonContextMenuItem();
            this.kcmdLoad = new Krypton.Toolkit.KryptonCommand();
            this.kbtnCalculate = new Krypton.Toolkit.KryptonButton();
            this.ss.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbHashType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).BeginInit();
            this.kryptonGroupBox1.Panel.SuspendLayout();
            this.kryptonGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2.Panel)).BeginInit();
            this.kryptonGroupBox2.Panel.SuspendLayout();
            this.kryptonGroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ss
            // 
            this.ss.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ss.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslStatus,
            this.kpbtsiCalculationProgress});
            this.ss.Location = new System.Drawing.Point(0, 305);
            this.ss.Name = "ss";
            this.ss.ProgressBars = null;
            this.ss.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.ss.Size = new System.Drawing.Size(689, 24);
            this.ss.TabIndex = 4;
            this.ss.Text = "statusStrip1";
            // 
            // tslStatus
            // 
            this.tslStatus.Name = "tslStatus";
            this.tslStatus.Size = new System.Drawing.Size(543, 19);
            this.tslStatus.Spring = true;
            this.tslStatus.Text = "Ready";
            this.tslStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // kpbtsiCalculationProgress
            // 
            this.kpbtsiCalculationProgress.Name = "kpbtsiCalculationProgress";
            this.kpbtsiCalculationProgress.StateCommon.Back.Color1 = System.Drawing.Color.Green;
            this.kpbtsiCalculationProgress.StateDisabled.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.OneNote;
            this.kpbtsiCalculationProgress.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.OneNote;
            this.kpbtsiCalculationProgress.Text = "0%";
            this.kpbtsiCalculationProgress.UseValueAsText = true;
            this.kpbtsiCalculationProgress.Values.Text = "0%";
            this.kpbtsiCalculationProgress.Visible = false;
            // 
            // bgwMD5
            // 
            this.bgwMD5.WorkerReportsProgress = true;
            this.bgwMD5.WorkerSupportsCancellation = true;
            // 
            // bgwSHA1
            // 
            this.bgwSHA1.WorkerReportsProgress = true;
            this.bgwSHA1.WorkerSupportsCancellation = true;
            // 
            // bgwSHA256
            // 
            this.bgwSHA256.WorkerReportsProgress = true;
            this.bgwSHA256.WorkerSupportsCancellation = true;
            // 
            // bgwSHA384
            // 
            this.bgwSHA384.WorkerReportsProgress = true;
            this.bgwSHA384.WorkerSupportsCancellation = true;
            // 
            // bgwSHA512
            // 
            this.bgwSHA512.WorkerReportsProgress = true;
            this.bgwSHA512.WorkerSupportsCancellation = true;
            // 
            // bgwRIPEMD160
            // 
            this.bgwRIPEMD160.WorkerReportsProgress = true;
            this.bgwRIPEMD160.WorkerSupportsCancellation = true;
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kbtnVerify);
            this.kryptonPanel1.Controls.Add(this.kryptonBorderEdge1);
            this.kryptonPanel1.Controls.Add(this.kbtnCancel);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 255);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kryptonPanel1.Size = new System.Drawing.Size(689, 50);
            this.kryptonPanel1.TabIndex = 5;
            // 
            // kbtnVerify
            // 
            this.kbtnVerify.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.kbtnVerify.Enabled = false;
            this.kbtnVerify.Location = new System.Drawing.Point(491, 13);
            this.kbtnVerify.Name = "kbtnVerify";
            this.kbtnVerify.Size = new System.Drawing.Size(90, 25);
            this.kbtnVerify.TabIndex = 2;
            this.kbtnVerify.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnVerify.Values.Text = "&Verify";
            this.kbtnVerify.Click += new System.EventHandler(this.kbtnVerify_Click);
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderSecondary;
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(0, 0);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(689, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // kbtnCancel
            // 
            this.kbtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.kbtnCancel.Location = new System.Drawing.Point(587, 13);
            this.kbtnCancel.Name = "kbtnCancel";
            this.kbtnCancel.Size = new System.Drawing.Size(90, 25);
            this.kbtnCancel.TabIndex = 4;
            this.kbtnCancel.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnCancel.Values.Text = "C&ancel";
            this.kbtnCancel.Click += new System.EventHandler(this.kbtnCalculate_Click);
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.tableLayoutPanel1);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(689, 255);
            this.kryptonPanel2.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.kryptonWrapLabel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.kryptonWrapLabel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.ktxtFilePath, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.kcmbHashType, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.kryptonGroupBox1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.kryptonGroupBox2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.kbtnCalculate, 2, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(689, 255);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // kryptonWrapLabel1
            // 
            this.kryptonWrapLabel1.AutoSize = false;
            this.kryptonWrapLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonWrapLabel1.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.kryptonWrapLabel1.Location = new System.Drawing.Point(3, 0);
            this.kryptonWrapLabel1.Name = "kryptonWrapLabel1";
            this.kryptonWrapLabel1.Size = new System.Drawing.Size(100, 30);
            this.kryptonWrapLabel1.Text = "File Path:";
            this.kryptonWrapLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // kryptonWrapLabel2
            // 
            this.kryptonWrapLabel2.AutoSize = false;
            this.kryptonWrapLabel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonWrapLabel2.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.kryptonWrapLabel2.Location = new System.Drawing.Point(3, 30);
            this.kryptonWrapLabel2.Name = "kryptonWrapLabel2";
            this.kryptonWrapLabel2.Size = new System.Drawing.Size(100, 31);
            this.kryptonWrapLabel2.Text = "Hash Type:";
            this.kryptonWrapLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ktxtFilePath
            // 
            this.ktxtFilePath.ButtonSpecs.Add(this.bsaReset);
            this.ktxtFilePath.ButtonSpecs.Add(this.bsaBrowse);
            this.tableLayoutPanel1.SetColumnSpan(this.ktxtFilePath, 2);
            this.ktxtFilePath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ktxtFilePath.Location = new System.Drawing.Point(109, 3);
            this.ktxtFilePath.Name = "ktxtFilePath";
            this.ktxtFilePath.Size = new System.Drawing.Size(577, 23);
            this.ktxtFilePath.TabIndex = 2;
            this.ktxtFilePath.TextChanged += new System.EventHandler(this.ktxtFilePath_TextChanged);
            // 
            // bsaReset
            // 
            this.bsaReset.Type = Krypton.Toolkit.PaletteButtonSpecStyle.Undo;
            this.bsaReset.UniqueName = "d153dbd03a8c4bedaca0d851db1492dc";
            this.bsaReset.Click += new System.EventHandler(this.bsaReset_Click);
            // 
            // bsaBrowse
            // 
            this.bsaBrowse.ToolTipStyle = Krypton.Toolkit.LabelStyle.SuperTip;
            this.bsaBrowse.Type = Krypton.Toolkit.PaletteButtonSpecStyle.Open;
            this.bsaBrowse.UniqueName = "2efda73a4eff4dea8684b6fe1ad4324f";
            this.bsaBrowse.Click += new System.EventHandler(this.bsaBrowse_Click);
            // 
            // kcmbHashType
            // 
            this.kcmbHashType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbHashType.IntegralHeight = false;
            this.kcmbHashType.Location = new System.Drawing.Point(109, 33);
            this.kcmbHashType.Name = "kcmbHashType";
            this.kcmbHashType.Size = new System.Drawing.Size(200, 22);
            this.kcmbHashType.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kcmbHashType.TabIndex = 3;
            // 
            // kryptonGroupBox1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.kryptonGroupBox1, 3);
            this.kryptonGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonGroupBox1.Location = new System.Drawing.Point(3, 64);
            // 
            // kryptonGroupBox1.Panel
            // 
            this.kryptonGroupBox1.Panel.Controls.Add(this.kwlHashOutput);
            this.kryptonGroupBox1.Size = new System.Drawing.Size(683, 91);
            this.kryptonGroupBox1.TabIndex = 5;
            this.kryptonGroupBox1.Values.Heading = "CheckSum Output";
            // 
            // kwlHashOutput
            // 
            this.kwlHashOutput.AutoSize = false;
            this.kwlHashOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlHashOutput.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            this.kwlHashOutput.Location = new System.Drawing.Point(0, 0);
            this.kwlHashOutput.Name = "kwlHashOutput";
            this.kwlHashOutput.Size = new System.Drawing.Size(679, 67);
            this.kwlHashOutput.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // kryptonGroupBox2
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.kryptonGroupBox2, 3);
            this.kryptonGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonGroupBox2.Location = new System.Drawing.Point(3, 161);
            // 
            // kryptonGroupBox2.Panel
            // 
            this.kryptonGroupBox2.Panel.Controls.Add(this.ktxtVarifyCheckSum);
            this.kryptonGroupBox2.Size = new System.Drawing.Size(683, 91);
            this.kryptonGroupBox2.TabIndex = 6;
            this.kryptonGroupBox2.Values.Heading = "Verify CheckSum";
            // 
            // ktxtVarifyCheckSum
            // 
            this.ktxtVarifyCheckSum.ButtonSpecs.Add(this.bsaVerifyReset);
            this.ktxtVarifyCheckSum.ButtonSpecs.Add(this.bsaVerifyBrowse);
            this.ktxtVarifyCheckSum.KryptonContextMenu = this.kcmHashVerify;
            this.ktxtVarifyCheckSum.Location = new System.Drawing.Point(8, 22);
            this.ktxtVarifyCheckSum.Name = "ktxtVarifyCheckSum";
            this.ktxtVarifyCheckSum.Size = new System.Drawing.Size(664, 23);
            this.ktxtVarifyCheckSum.TabIndex = 0;
            this.ktxtVarifyCheckSum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // bsaVerifyReset
            // 
            this.bsaVerifyReset.Type = Krypton.Toolkit.PaletteButtonSpecStyle.Undo;
            this.bsaVerifyReset.UniqueName = "b92f7a2ea9f441ffb60df6dc3e1d6d88";
            // 
            // bsaVerifyBrowse
            // 
            this.bsaVerifyBrowse.Type = Krypton.Toolkit.PaletteButtonSpecStyle.Open;
            this.bsaVerifyBrowse.UniqueName = "2313891785e94e2c8ff9adc2d90ed55b";
            this.bsaVerifyBrowse.Click += new System.EventHandler(this.bsaVerifyBrowse_Click);
            // 
            // kcmHashVerify
            // 
            this.kcmHashVerify.Items.AddRange(new Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.kryptonContextMenuItems1});
            // 
            // kryptonContextMenuItems1
            // 
            this.kryptonContextMenuItems1.Items.AddRange(new Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.kryptonContextMenuItem1,
            this.kryptonContextMenuSeparator1,
            this.kryptonContextMenuItem2,
            this.kryptonContextMenuSeparator2,
            this.kryptonContextMenuItem3,
            this.kryptonContextMenuSeparator3,
            this.kryptonContextMenuItem4});
            // 
            // kryptonContextMenuItem1
            // 
            this.kryptonContextMenuItem1.KryptonCommand = this.kcmdCut;
            this.kryptonContextMenuItem1.ShortcutKeyDisplayString = "Ctrl + X";
            this.kryptonContextMenuItem1.Text = "&Cut";
            // 
            // kcmdCut
            // 
            this.kcmdCut.Text = "&Cut";
            // 
            // kryptonContextMenuItem2
            // 
            this.kryptonContextMenuItem2.KryptonCommand = this.kcmdCopy;
            this.kryptonContextMenuItem2.ShortcutKeyDisplayString = "Ctrl + C";
            this.kryptonContextMenuItem2.Text = "C&opy";
            // 
            // kcmdCopy
            // 
            this.kcmdCopy.Text = "C&opy";
            // 
            // kryptonContextMenuItem3
            // 
            this.kryptonContextMenuItem3.KryptonCommand = this.kcmdPaste;
            this.kryptonContextMenuItem3.ShortcutKeyDisplayString = "Ctrl + V";
            this.kryptonContextMenuItem3.Text = "&Paste";
            // 
            // kcmdPaste
            // 
            this.kcmdPaste.Text = "P&aste";
            // 
            // kryptonContextMenuItem4
            // 
            this.kryptonContextMenuItem4.KryptonCommand = this.kcmdLoad;
            this.kryptonContextMenuItem4.ShortcutKeyDisplayString = "Ctrl + Shift + O";
            this.kryptonContextMenuItem4.Text = "Load from &File";
            // 
            // kcmdLoad
            // 
            this.kcmdLoad.Text = "Load from &File";
            // 
            // kbtnCalculate
            // 
            this.kbtnCalculate.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.kbtnCalculate.Location = new System.Drawing.Point(315, 33);
            this.kbtnCalculate.Name = "kbtnCalculate";
            this.kbtnCalculate.Size = new System.Drawing.Size(90, 25);
            this.kbtnCalculate.TabIndex = 7;
            this.kbtnCalculate.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnCalculate.Values.Text = "&Calculate";
            this.kbtnCalculate.Click += new System.EventHandler(this.kbtnCalculate_Click);
            // 
            // VisualVerifyFileCheckSumForm
            // 
            this.AcceptButton = this.kbtnVerify;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.kbtnCancel;
            this.ClientSize = new System.Drawing.Size(689, 329);
            this.Controls.Add(this.kryptonPanel2);
            this.Controls.Add(this.kryptonPanel1);
            this.Controls.Add(this.ss);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "VisualVerifyFileCheckSumForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Verify File CheckSum";
            this.Load += new System.EventHandler(this.VisualVerifyFileCheckSumForm_Load);
            this.ss.ResumeLayout(false);
            this.ss.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbHashType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).EndInit();
            this.kryptonGroupBox1.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).EndInit();
            this.kryptonGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2.Panel)).EndInit();
            this.kryptonGroupBox2.Panel.ResumeLayout(false);
            this.kryptonGroupBox2.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2)).EndInit();
            this.kryptonGroupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private KryptonStatusStrip ss;
        private ToolStripStatusLabel tslStatus;
        private BackgroundWorker bgwMD5;
        private BackgroundWorker bgwSHA1;
        private BackgroundWorker bgwSHA256;
        private BackgroundWorker bgwSHA384;
        private BackgroundWorker bgwSHA512;
        private BackgroundWorker bgwRIPEMD160;
        private KryptonPanel kryptonPanel1;
        private KryptonBorderEdge kryptonBorderEdge1;
        private KryptonPanel kryptonPanel2;
        private TableLayoutPanel tableLayoutPanel1;
        private KryptonWrapLabel kryptonWrapLabel1;
        private KryptonWrapLabel kryptonWrapLabel2;
        private KryptonTextBox ktxtFilePath;
        private ButtonSpecAny bsaBrowse;
        private KryptonComboBox kcmbHashType;
        private KryptonButton kbtnCancel;
        private KryptonGroupBox kryptonGroupBox1;
        private KryptonWrapLabel kwlHashOutput;
        private KryptonGroupBox kryptonGroupBox2;
        private KryptonTextBox ktxtVarifyCheckSum;
        private KryptonContextMenu kcmHashVerify;
        private KryptonContextMenuItems kryptonContextMenuItems1;
        private KryptonContextMenuItem kryptonContextMenuItem1;
        private KryptonContextMenuSeparator kryptonContextMenuSeparator1;
        private KryptonContextMenuItem kryptonContextMenuItem2;
        private KryptonContextMenuSeparator kryptonContextMenuSeparator2;
        private KryptonContextMenuItem kryptonContextMenuItem3;
        private KryptonContextMenuSeparator kryptonContextMenuSeparator3;
        private KryptonContextMenuItem kryptonContextMenuItem4;
        private KryptonCommand kcmdCut;
        private KryptonCommand kcmdCopy;
        private KryptonCommand kcmdPaste;
        private KryptonCommand kcmdLoad;
        private KryptonButton kbtnVerify;
        private ButtonSpecAny bsaVerifyBrowse;
        private ButtonSpecAny bsaReset;
        private ButtonSpecAny bsaVerifyReset;
        private KryptonButton kbtnCalculate;
        private KryptonProgressBarToolStripItem kpbtsiCalculationProgress;
    }
}
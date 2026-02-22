namespace Krypton.Utilities
{
    partial class VisualComputeFileCheckSumForm
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
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kbtnSaveToFile = new Krypton.Toolkit.KryptonButton();
            this.kbtnCancel = new Krypton.Toolkit.KryptonButton();
            this.kchkToggleCasing = new Krypton.Toolkit.KryptonCheckBox();
            this.kryptonBorderEdge1 = new Krypton.Toolkit.KryptonBorderEdge();
            this.kryptonPanel2 = new Krypton.Toolkit.KryptonPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.kryptonWrapLabel1 = new Krypton.Toolkit.KryptonWrapLabel();
            this.kryptonWrapLabel2 = new Krypton.Toolkit.KryptonWrapLabel();
            this.ktxtFilePath = new Krypton.Toolkit.KryptonTextBox();
            this.bsaReset = new Krypton.Toolkit.ButtonSpecAny();
            this.bsaBrowse = new Krypton.Toolkit.ButtonSpecAny();
            this.kcmbHashType = new Krypton.Toolkit.KryptonComboBox();
            this.kbtnCalculate = new Krypton.Toolkit.KryptonButton();
            this.kryptonGroupBox1 = new Krypton.Toolkit.KryptonGroupBox();
            this.kwlHashOutput = new Krypton.Toolkit.KryptonWrapLabel();
            this.ss = new Krypton.Toolkit.KryptonStatusStrip();
            this.tslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.kpbtsiCalculationProgress = new Krypton.Toolkit.KryptonProgressBarToolStripItem();
            this.bgwMD5 = new System.ComponentModel.BackgroundWorker();
            this.bgwSHA1 = new System.ComponentModel.BackgroundWorker();
            this.bgwSHA256 = new System.ComponentModel.BackgroundWorker();
            this.bgwSHA384 = new System.ComponentModel.BackgroundWorker();
            this.bgwSHA512 = new System.ComponentModel.BackgroundWorker();
            this.bgwRIPEMD160 = new System.ComponentModel.BackgroundWorker();
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
            this.ss.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kbtnSaveToFile);
            this.kryptonPanel1.Controls.Add(this.kbtnCancel);
            this.kryptonPanel1.Controls.Add(this.kchkToggleCasing);
            this.kryptonPanel1.Controls.Add(this.kryptonBorderEdge1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 212);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kryptonPanel1.Size = new System.Drawing.Size(709, 50);
            this.kryptonPanel1.TabIndex = 1;
            // 
            // kbtnSaveToFile
            // 
            this.kbtnSaveToFile.Enabled = false;
            this.kbtnSaveToFile.Location = new System.Drawing.Point(511, 17);
            this.kbtnSaveToFile.Name = "kbtnSaveToFile";
            this.kbtnSaveToFile.Size = new System.Drawing.Size(90, 25);
            this.kbtnSaveToFile.TabIndex = 3;
            this.kbtnSaveToFile.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnSaveToFile.Values.Text = "Save to &File";
            this.kbtnSaveToFile.Click += new System.EventHandler(this.kbtnSaveToFile_Click);
            // 
            // kbtnCancel
            // 
            this.kbtnCancel.Location = new System.Drawing.Point(607, 17);
            this.kbtnCancel.Name = "kbtnCancel";
            this.kbtnCancel.Size = new System.Drawing.Size(90, 25);
            this.kbtnCancel.TabIndex = 2;
            this.kbtnCancel.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnCancel.Values.Text = "kryptonButton1";
            this.kbtnCancel.Click += new System.EventHandler(this.kbtnCancel_Click);
            // 
            // kchkToggleCasing
            // 
            this.kchkToggleCasing.Location = new System.Drawing.Point(13, 19);
            this.kchkToggleCasing.Name = "kchkToggleCasing";
            this.kchkToggleCasing.Size = new System.Drawing.Size(101, 20);
            this.kchkToggleCasing.TabIndex = 1;
            this.kchkToggleCasing.Values.Text = "Toggle &Casing";
            this.kchkToggleCasing.CheckedChanged += new System.EventHandler(this.kchkToggleCasing_CheckedChanged);
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(0, 0);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(709, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.tableLayoutPanel1);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(709, 212);
            this.kryptonPanel2.TabIndex = 2;
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
            this.tableLayoutPanel1.Controls.Add(this.kbtnCalculate, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.kryptonGroupBox1, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(709, 212);
            this.tableLayoutPanel1.TabIndex = 1;
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
            this.ktxtFilePath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ktxtFilePath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.ktxtFilePath.ButtonSpecs.Add(this.bsaReset);
            this.ktxtFilePath.ButtonSpecs.Add(this.bsaBrowse);
            this.tableLayoutPanel1.SetColumnSpan(this.ktxtFilePath, 2);
            this.ktxtFilePath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ktxtFilePath.Location = new System.Drawing.Point(109, 3);
            this.ktxtFilePath.Name = "ktxtFilePath";
            this.ktxtFilePath.Size = new System.Drawing.Size(597, 23);
            this.ktxtFilePath.TabIndex = 2;
            // 
            // bsaReset
            // 
            this.bsaReset.Type = Krypton.Toolkit.PaletteButtonSpecStyle.Undo;
            this.bsaReset.UniqueName = "f7abb2bddee245e9b00ebbe01ac8b5f2";
            this.bsaReset.Click += new System.EventHandler(this.bsaReset_Click);
            // 
            // bsaBrowse
            // 
            this.bsaBrowse.Image = global::Krypton.Utilities.Properties.Resources.Open;
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
            // kbtnCalculate
            // 
            this.kbtnCalculate.Enabled = false;
            this.kbtnCalculate.Location = new System.Drawing.Point(315, 33);
            this.kbtnCalculate.Name = "kbtnCalculate";
            this.kbtnCalculate.Size = new System.Drawing.Size(90, 25);
            this.kbtnCalculate.TabIndex = 4;
            this.kbtnCalculate.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnCalculate.Values.Text = "&Calculate";
            this.kbtnCalculate.Click += new System.EventHandler(this.kbtnCalculate_Click);
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
            this.kryptonGroupBox1.Size = new System.Drawing.Size(703, 145);
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
            this.kwlHashOutput.Size = new System.Drawing.Size(699, 121);
            this.kwlHashOutput.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ss
            // 
            this.ss.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ss.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslStatus,
            this.kpbtsiCalculationProgress});
            this.ss.Location = new System.Drawing.Point(0, 262);
            this.ss.Name = "ss";
            this.ss.ProgressBars = null;
            this.ss.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.ss.Size = new System.Drawing.Size(709, 22);
            this.ss.TabIndex = 3;
            this.ss.Text = "statusStrip1";
            // 
            // tslStatus
            // 
            this.tslStatus.Name = "tslStatus";
            this.tslStatus.Size = new System.Drawing.Size(694, 17);
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
            this.bgwMD5.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwMD5_DoWork);
            this.bgwMD5.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.Calculation_ProgressChanged);
            this.bgwMD5.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.Calculation_RunWorkerCompleted);
            // 
            // bgwSHA1
            // 
            this.bgwSHA1.WorkerReportsProgress = true;
            this.bgwSHA1.WorkerSupportsCancellation = true;
            this.bgwSHA1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwSHA1_DoWork);
            this.bgwSHA1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.Calculation_ProgressChanged);
            this.bgwSHA1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.Calculation_RunWorkerCompleted);
            // 
            // bgwSHA256
            // 
            this.bgwSHA256.WorkerReportsProgress = true;
            this.bgwSHA256.WorkerSupportsCancellation = true;
            this.bgwSHA256.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwSHA256_DoWork);
            this.bgwSHA256.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.Calculation_ProgressChanged);
            this.bgwSHA256.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.Calculation_RunWorkerCompleted);
            // 
            // bgwSHA384
            // 
            this.bgwSHA384.WorkerReportsProgress = true;
            this.bgwSHA384.WorkerSupportsCancellation = true;
            this.bgwSHA384.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwSHA384_DoWork);
            this.bgwSHA384.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.Calculation_ProgressChanged);
            this.bgwSHA384.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.Calculation_RunWorkerCompleted);
            // 
            // bgwSHA512
            // 
            this.bgwSHA512.WorkerReportsProgress = true;
            this.bgwSHA512.WorkerSupportsCancellation = true;
            this.bgwSHA512.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwSHA512_DoWork);
            this.bgwSHA512.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.Calculation_ProgressChanged);
            this.bgwSHA512.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.Calculation_RunWorkerCompleted);
            // 
            // bgwRIPEMD160
            // 
            this.bgwRIPEMD160.WorkerReportsProgress = true;
            this.bgwRIPEMD160.WorkerSupportsCancellation = true;
            this.bgwRIPEMD160.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwRIPEMD160_DoWork);
            this.bgwRIPEMD160.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.Calculation_ProgressChanged);
            this.bgwRIPEMD160.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.Calculation_RunWorkerCompleted);
            // 
            // VisualComputeFileCheckSumForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 284);
            this.Controls.Add(this.kryptonPanel2);
            this.Controls.Add(this.kryptonPanel1);
            this.Controls.Add(this.ss);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VisualComputeFileCheckSumForm";
            this.Text = "KryptonComputeFileCheckSum";
            this.Load += new System.EventHandler(this.KryptonComputeFileCheckSum_Load);
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
            this.ss.ResumeLayout(false);
            this.ss.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private KryptonPanel kryptonPanel1;
        private KryptonBorderEdge kryptonBorderEdge1;
        private KryptonPanel kryptonPanel2;
        private KryptonCheckBox kchkToggleCasing;
        private KryptonButton kbtnCancel;
        private KryptonButton kbtnSaveToFile;
        private TableLayoutPanel tableLayoutPanel1;
        private KryptonWrapLabel kryptonWrapLabel1;
        private KryptonWrapLabel kryptonWrapLabel2;
        private KryptonTextBox ktxtFilePath;
        private KryptonComboBox kcmbHashType;
        private KryptonButton kbtnCalculate;
        private KryptonGroupBox kryptonGroupBox1;
        private KryptonWrapLabel kwlHashOutput;
        private ButtonSpecAny bsaBrowse;
        private KryptonStatusStrip ss;
        private ToolStripStatusLabel tslStatus;
        private BackgroundWorker bgwMD5;
        private BackgroundWorker bgwSHA1;
        private BackgroundWorker bgwSHA256;
        private BackgroundWorker bgwSHA384;
        private BackgroundWorker bgwSHA512;
        private BackgroundWorker bgwRIPEMD160;
        private ButtonSpecAny bsaReset;
        private KryptonProgressBarToolStripItem kpbtsiCalculationProgress;
    }
}
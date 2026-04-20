using Krypton.Utilities;

namespace TestForm
{
    partial class QRCodeDemo
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
            this._kryptonPanelRoot = new Krypton.Toolkit.KryptonPanel();
            this._splitMain = new System.Windows.Forms.SplitContainer();
            this._kryptonPanelLeft = new Krypton.Toolkit.KryptonPanel();
            this._lblStatus = new Krypton.Toolkit.KryptonLabel();
            this._kryptonGroupExport = new Krypton.Toolkit.KryptonGroupBox();
            this._kbtnStaticApi = new Krypton.Toolkit.KryptonButton();
            this._kbtnCopyImage = new Krypton.Toolkit.KryptonButton();
            this._kbtnSavePng = new Krypton.Toolkit.KryptonButton();
            this._kryptonGroupRender = new Krypton.Toolkit.KryptonGroupBox();
            this._kcbtnLightColor = new Krypton.Toolkit.KryptonColorButton();
            this._kcbtnDarkColor = new Krypton.Toolkit.KryptonColorButton();
            this._klblColors = new Krypton.Toolkit.KryptonLabel();
            this._chkQuietZone = new Krypton.Toolkit.KryptonCheckBox();
            this._numModule = new Krypton.Toolkit.KryptonNumericUpDown();
            this._klblModule = new Krypton.Toolkit.KryptonLabel();
            this._kryptonGroupEcc = new Krypton.Toolkit.KryptonGroupBox();
            this._cmbEcc = new Krypton.Toolkit.KryptonComboBox();
            this._klblEcc = new Krypton.Toolkit.KryptonLabel();
            this._kryptonGroupContent = new Krypton.Toolkit.KryptonGroupBox();
            this._flowSamples = new System.Windows.Forms.FlowLayoutPanel();
            this._kbtnSampleUrl = new Krypton.Toolkit.KryptonButton();
            this._kbtnSampleVCard = new Krypton.Toolkit.KryptonButton();
            this._kbtnSampleUnicode = new Krypton.Toolkit.KryptonButton();
            this._kbtnClear = new Krypton.Toolkit.KryptonButton();
            this._txtContent = new Krypton.Toolkit.KryptonTextBox();
            this._kryptonPanelPreview = new Krypton.Toolkit.KryptonPanel();
            this._kryptonQRCode = new Krypton.Utilities.KryptonQRCode();
            ((System.ComponentModel.ISupportInitialize)(this._kryptonPanelRoot)).BeginInit();
            this._kryptonPanelRoot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._splitMain)).BeginInit();
            this._splitMain.Panel1.SuspendLayout();
            this._splitMain.Panel2.SuspendLayout();
            this._splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._kryptonPanelLeft)).BeginInit();
            this._kryptonPanelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._kryptonGroupExport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._kryptonGroupExport.Panel)).BeginInit();
            this._kryptonGroupExport.Panel.SuspendLayout();
            this._kryptonGroupExport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._kryptonGroupRender)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._kryptonGroupRender.Panel)).BeginInit();
            this._kryptonGroupRender.Panel.SuspendLayout();
            this._kryptonGroupRender.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._kryptonGroupEcc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._kryptonGroupEcc.Panel)).BeginInit();
            this._kryptonGroupEcc.Panel.SuspendLayout();
            this._kryptonGroupEcc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._cmbEcc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._kryptonGroupContent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._kryptonGroupContent.Panel)).BeginInit();
            this._kryptonGroupContent.Panel.SuspendLayout();
            this._kryptonGroupContent.SuspendLayout();
            this._flowSamples.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._kryptonPanelPreview)).BeginInit();
            this._kryptonPanelPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._kryptonQRCode)).BeginInit();
            this.SuspendLayout();
            // 
            // _kryptonPanelRoot
            // 
            this._kryptonPanelRoot.Controls.Add(this._splitMain);
            this._kryptonPanelRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this._kryptonPanelRoot.Location = new System.Drawing.Point(0, 0);
            this._kryptonPanelRoot.Name = "_kryptonPanelRoot";
            this._kryptonPanelRoot.Padding = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this._kryptonPanelRoot.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this._kryptonPanelRoot.Size = new System.Drawing.Size(929, 642);
            this._kryptonPanelRoot.TabIndex = 0;
            // 
            // _splitMain
            // 
            this._splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this._splitMain.Location = new System.Drawing.Point(7, 7);
            this._splitMain.Name = "_splitMain";
            // 
            // _splitMain.Panel1
            // 
            this._splitMain.Panel1.Controls.Add(this._kryptonPanelLeft);
            // 
            // _splitMain.Panel2
            // 
            this._splitMain.Panel2.Controls.Add(this._kryptonPanelPreview);
            this._splitMain.Size = new System.Drawing.Size(915, 628);
            this._splitMain.SplitterDistance = 420;
            this._splitMain.SplitterWidth = 5;
            this._splitMain.TabIndex = 0;
            // 
            // _kryptonPanelLeft
            // 
            this._kryptonPanelLeft.AutoScroll = true;
            this._kryptonPanelLeft.Controls.Add(this._lblStatus);
            this._kryptonPanelLeft.Controls.Add(this._kryptonGroupExport);
            this._kryptonPanelLeft.Controls.Add(this._kryptonGroupRender);
            this._kryptonPanelLeft.Controls.Add(this._kryptonGroupEcc);
            this._kryptonPanelLeft.Controls.Add(this._kryptonGroupContent);
            this._kryptonPanelLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this._kryptonPanelLeft.Location = new System.Drawing.Point(0, 0);
            this._kryptonPanelLeft.Name = "_kryptonPanelLeft";
            this._kryptonPanelLeft.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this._kryptonPanelLeft.Size = new System.Drawing.Size(420, 628);
            this._kryptonPanelLeft.TabIndex = 0;
            // 
            // _lblStatus
            // 
            this._lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._lblStatus.AutoSize = false;
            this._lblStatus.Location = new System.Drawing.Point(7, 569);
            this._lblStatus.Name = "_lblStatus";
            this._lblStatus.Size = new System.Drawing.Size(403, 52);
            this._lblStatus.TabIndex = 4;
            this._lblStatus.Values.Text = "Status";
            // 
            // _kryptonGroupExport
            // 
            this._kryptonGroupExport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._kryptonGroupExport.Location = new System.Drawing.Point(7, 466);
            // 
            // _kryptonGroupExport.Panel
            // 
            this._kryptonGroupExport.Panel.Controls.Add(this._kbtnStaticApi);
            this._kryptonGroupExport.Panel.Controls.Add(this._kbtnCopyImage);
            this._kryptonGroupExport.Panel.Controls.Add(this._kbtnSavePng);
            this._kryptonGroupExport.Size = new System.Drawing.Size(403, 111);
            this._kryptonGroupExport.TabIndex = 3;
            this._kryptonGroupExport.Values.Description = "Save PNG, copy image, or call GenerateBitmap without a control.";
            this._kryptonGroupExport.Values.Heading = "Export & static API";
            // 
            // _kbtnStaticApi
            // 
            this._kbtnStaticApi.Location = new System.Drawing.Point(10, 38);
            this._kbtnStaticApi.Name = "_kbtnStaticApi";
            this._kbtnStaticApi.Size = new System.Drawing.Size(213, 24);
            this._kbtnStaticApi.TabIndex = 2;
            this._kbtnStaticApi.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this._kbtnStaticApi.Values.Text = "Static GenerateBitmap → clipboard";
            // 
            // _kbtnCopyImage
            // 
            this._kbtnCopyImage.Location = new System.Drawing.Point(120, 7);
            this._kbtnCopyImage.Name = "_kbtnCopyImage";
            this._kbtnCopyImage.Size = new System.Drawing.Size(103, 24);
            this._kbtnCopyImage.TabIndex = 1;
            this._kbtnCopyImage.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this._kbtnCopyImage.Values.Text = "Copy image";
            // 
            // _kbtnSavePng
            // 
            this._kbtnSavePng.Location = new System.Drawing.Point(10, 7);
            this._kbtnSavePng.Name = "_kbtnSavePng";
            this._kbtnSavePng.Size = new System.Drawing.Size(103, 24);
            this._kbtnSavePng.TabIndex = 0;
            this._kbtnSavePng.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this._kbtnSavePng.Values.Text = "Save as PNG…";
            // 
            // _kryptonGroupRender
            // 
            this._kryptonGroupRender.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._kryptonGroupRender.Location = new System.Drawing.Point(7, 259);
            // 
            // _kryptonGroupRender.Panel
            // 
            this._kryptonGroupRender.Panel.Controls.Add(this._kcbtnLightColor);
            this._kryptonGroupRender.Panel.Controls.Add(this._kcbtnDarkColor);
            this._kryptonGroupRender.Panel.Controls.Add(this._klblColors);
            this._kryptonGroupRender.Panel.Controls.Add(this._chkQuietZone);
            this._kryptonGroupRender.Panel.Controls.Add(this._numModule);
            this._kryptonGroupRender.Panel.Controls.Add(this._klblModule);
            this._kryptonGroupRender.Size = new System.Drawing.Size(403, 198);
            this._kryptonGroupRender.TabIndex = 2;
            this._kryptonGroupRender.Values.Description = "Module size, quiet zone, and module colors.";
            this._kryptonGroupRender.Values.Heading = "Rendering";
            // 
            // _kcbtnLightColor
            // 
            this._kcbtnLightColor.Location = new System.Drawing.Point(171, 83);
            this._kcbtnLightColor.Name = "_kcbtnLightColor";
            this._kcbtnLightColor.Size = new System.Drawing.Size(154, 26);
            this._kcbtnLightColor.TabIndex = 5;
            // 
            // _kcbtnDarkColor
            // 
            this._kcbtnDarkColor.Location = new System.Drawing.Point(10, 83);
            this._kcbtnDarkColor.Name = "_kcbtnDarkColor";
            this._kcbtnDarkColor.Size = new System.Drawing.Size(154, 26);
            this._kcbtnDarkColor.TabIndex = 4;
            // 
            // _klblColors
            // 
            this._klblColors.Location = new System.Drawing.Point(10, 62);
            this._klblColors.Name = "_klblColors";
            this._klblColors.Size = new System.Drawing.Size(89, 20);
            this._klblColors.TabIndex = 3;
            this._klblColors.Values.Text = "Module colors";
            // 
            // _chkQuietZone
            // 
            this._chkQuietZone.Checked = true;
            this._chkQuietZone.CheckState = System.Windows.Forms.CheckState.Checked;
            this._chkQuietZone.Location = new System.Drawing.Point(10, 35);
            this._chkQuietZone.Name = "_chkQuietZone";
            this._chkQuietZone.Size = new System.Drawing.Size(319, 20);
            this._chkQuietZone.TabIndex = 2;
            this._chkQuietZone.Values.Text = "Quiet zone (4 modules) — recommended for scanners";
            // 
            // _numModule
            // 
            this._numModule.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._numModule.Location = new System.Drawing.Point(120, 5);
            this._numModule.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this._numModule.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._numModule.Name = "_numModule";
            this._numModule.Size = new System.Drawing.Size(69, 22);
            this._numModule.TabIndex = 1;
            this._numModule.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // _klblModule
            // 
            this._klblModule.Location = new System.Drawing.Point(10, 7);
            this._klblModule.Name = "_klblModule";
            this._klblModule.Size = new System.Drawing.Size(100, 20);
            this._klblModule.TabIndex = 0;
            this._klblModule.Values.Text = "Module size (px)";
            // 
            // _kryptonGroupEcc
            // 
            this._kryptonGroupEcc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._kryptonGroupEcc.Location = new System.Drawing.Point(7, 146);
            // 
            // _kryptonGroupEcc.Panel
            // 
            this._kryptonGroupEcc.Panel.Controls.Add(this._cmbEcc);
            this._kryptonGroupEcc.Panel.Controls.Add(this._klblEcc);
            this._kryptonGroupEcc.Size = new System.Drawing.Size(403, 84);
            this._kryptonGroupEcc.TabIndex = 1;
            this._kryptonGroupEcc.Values.Description = "Higher levels recover from damage; payload shrinks.";
            this._kryptonGroupEcc.Values.Heading = "Error correction";
            // 
            // _cmbEcc
            // 
            this._cmbEcc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._cmbEcc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbEcc.DropDownWidth = 360;
            this._cmbEcc.Location = new System.Drawing.Point(10, 24);
            this._cmbEcc.Name = "_cmbEcc";
            this._cmbEcc.Size = new System.Drawing.Size(375, 22);
            this._cmbEcc.TabIndex = 1;
            // 
            // _klblEcc
            // 
            this._klblEcc.Location = new System.Drawing.Point(10, 7);
            this._klblEcc.Name = "_klblEcc";
            this._klblEcc.Size = new System.Drawing.Size(60, 20);
            this._klblEcc.TabIndex = 0;
            this._klblEcc.Values.Text = "ECC level";
            // 
            // _kryptonGroupContent
            // 
            this._kryptonGroupContent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._kryptonGroupContent.Location = new System.Drawing.Point(7, 7);
            // 
            // _kryptonGroupContent.Panel
            // 
            this._kryptonGroupContent.Panel.Controls.Add(this._flowSamples);
            this._kryptonGroupContent.Panel.Controls.Add(this._txtContent);
            this._kryptonGroupContent.Size = new System.Drawing.Size(403, 132);
            this._kryptonGroupContent.TabIndex = 0;
            this._kryptonGroupContent.Values.Description = "UTF-8 byte mode. Change text to update the live preview.";
            this._kryptonGroupContent.Values.Heading = "Content";
            // 
            // _flowSamples
            // 
            this._flowSamples.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._flowSamples.Controls.Add(this._kbtnSampleUrl);
            this._flowSamples.Controls.Add(this._kbtnSampleVCard);
            this._flowSamples.Controls.Add(this._kbtnSampleUnicode);
            this._flowSamples.Controls.Add(this._kbtnClear);
            this._flowSamples.Location = new System.Drawing.Point(7, 61);
            this._flowSamples.Name = "_flowSamples";
            this._flowSamples.Size = new System.Drawing.Size(382, 31);
            this._flowSamples.TabIndex = 1;
            this._flowSamples.WrapContents = false;
            // 
            // _kbtnSampleUrl
            // 
            this._kbtnSampleUrl.AutoSize = true;
            this._kbtnSampleUrl.Location = new System.Drawing.Point(3, 3);
            this._kbtnSampleUrl.Name = "_kbtnSampleUrl";
            this._kbtnSampleUrl.Size = new System.Drawing.Size(30, 22);
            this._kbtnSampleUrl.TabIndex = 0;
            this._kbtnSampleUrl.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this._kbtnSampleUrl.Values.Text = "URL";
            // 
            // _kbtnSampleVCard
            // 
            this._kbtnSampleVCard.AutoSize = true;
            this._kbtnSampleVCard.Location = new System.Drawing.Point(39, 3);
            this._kbtnSampleVCard.Name = "_kbtnSampleVCard";
            this._kbtnSampleVCard.Size = new System.Drawing.Size(40, 22);
            this._kbtnSampleVCard.TabIndex = 1;
            this._kbtnSampleVCard.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this._kbtnSampleVCard.Values.Text = "vCard";
            // 
            // _kbtnSampleUnicode
            // 
            this._kbtnSampleUnicode.AutoSize = true;
            this._kbtnSampleUnicode.Location = new System.Drawing.Point(85, 3);
            this._kbtnSampleUnicode.Name = "_kbtnSampleUnicode";
            this._kbtnSampleUnicode.Size = new System.Drawing.Size(54, 22);
            this._kbtnSampleUnicode.TabIndex = 2;
            this._kbtnSampleUnicode.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this._kbtnSampleUnicode.Values.Text = "Unicode";
            // 
            // _kbtnClear
            // 
            this._kbtnClear.AutoSize = true;
            this._kbtnClear.Location = new System.Drawing.Point(145, 3);
            this._kbtnClear.Name = "_kbtnClear";
            this._kbtnClear.Size = new System.Drawing.Size(36, 22);
            this._kbtnClear.TabIndex = 3;
            this._kbtnClear.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this._kbtnClear.Values.Text = "Clear";
            // 
            // _txtContent
            // 
            this._txtContent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._txtContent.Location = new System.Drawing.Point(10, 7);
            this._txtContent.Multiline = true;
            this._txtContent.Name = "_txtContent";
            this._txtContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._txtContent.Size = new System.Drawing.Size(375, 49);
            this._txtContent.TabIndex = 0;
            // 
            // _kryptonPanelPreview
            // 
            this._kryptonPanelPreview.Controls.Add(this._kryptonQRCode);
            this._kryptonPanelPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this._kryptonPanelPreview.Location = new System.Drawing.Point(0, 0);
            this._kryptonPanelPreview.Name = "_kryptonPanelPreview";
            this._kryptonPanelPreview.Padding = new System.Windows.Forms.Padding(10, 10, 10, 10);
            this._kryptonPanelPreview.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this._kryptonPanelPreview.Size = new System.Drawing.Size(490, 628);
            this._kryptonPanelPreview.TabIndex = 0;
            // 
            // _kryptonQRCode
            // 
            this._kryptonQRCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this._kryptonQRCode.Location = new System.Drawing.Point(10, 10);
            this._kryptonQRCode.MinimumSize = new System.Drawing.Size(103, 104);
            this._kryptonQRCode.Name = "_kryptonQRCode";
            this._kryptonQRCode.Size = new System.Drawing.Size(470, 608);
            this._kryptonQRCode.TabIndex = 0;
            // 
            // QRCodeDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 642);
            this.Controls.Add(this._kryptonPanelRoot);
            this.MinimumSize = new System.Drawing.Size(774, 525);
            this.Name = "QRCodeDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KryptonQRCode — comprehensive demo";
            ((System.ComponentModel.ISupportInitialize)(this._kryptonPanelRoot)).EndInit();
            this._kryptonPanelRoot.ResumeLayout(false);
            this._splitMain.Panel1.ResumeLayout(false);
            this._splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._splitMain)).EndInit();
            this._splitMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._kryptonPanelLeft)).EndInit();
            this._kryptonPanelLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._kryptonGroupExport.Panel)).EndInit();
            this._kryptonGroupExport.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._kryptonGroupExport)).EndInit();
            this._kryptonGroupExport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._kryptonGroupRender.Panel)).EndInit();
            this._kryptonGroupRender.Panel.ResumeLayout(false);
            this._kryptonGroupRender.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._kryptonGroupRender)).EndInit();
            this._kryptonGroupRender.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._kryptonGroupEcc.Panel)).EndInit();
            this._kryptonGroupEcc.Panel.ResumeLayout(false);
            this._kryptonGroupEcc.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._kryptonGroupEcc)).EndInit();
            this._kryptonGroupEcc.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._cmbEcc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._kryptonGroupContent.Panel)).EndInit();
            this._kryptonGroupContent.Panel.ResumeLayout(false);
            this._kryptonGroupContent.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._kryptonGroupContent)).EndInit();
            this._kryptonGroupContent.ResumeLayout(false);
            this._flowSamples.ResumeLayout(false);
            this._flowSamples.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._kryptonPanelPreview)).EndInit();
            this._kryptonPanelPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._kryptonQRCode)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonPanel _kryptonPanelRoot;
        private SplitContainer _splitMain;
        private KryptonPanel _kryptonPanelLeft;
        private KryptonLabel _lblStatus;
        private KryptonGroupBox _kryptonGroupExport;
        private KryptonButton _kbtnStaticApi;
        private KryptonButton _kbtnCopyImage;
        private KryptonButton _kbtnSavePng;
        private KryptonGroupBox _kryptonGroupRender;
        private KryptonColorButton _kcbtnLightColor;
        private KryptonColorButton _kcbtnDarkColor;
        private KryptonLabel _klblColors;
        private KryptonCheckBox _chkQuietZone;
        private KryptonNumericUpDown _numModule;
        private KryptonLabel _klblModule;
        private KryptonGroupBox _kryptonGroupEcc;
        private KryptonComboBox _cmbEcc;
        private KryptonLabel _klblEcc;
        private KryptonGroupBox _kryptonGroupContent;
        private FlowLayoutPanel _flowSamples;
        private KryptonButton _kbtnClear;
        private KryptonButton _kbtnSampleUnicode;
        private KryptonButton _kbtnSampleVCard;
        private KryptonButton _kbtnSampleUrl;
        private KryptonTextBox _txtContent;
        private KryptonPanel _kryptonPanelPreview;
        private KryptonQRCode _kryptonQRCode;
    }
}
#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved. 
 *  
 */
#endregion

namespace TestForm
{
    partial class BasicToastNotificationTest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BasicToastNotificationTest));
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kbtnShow = new Krypton.Toolkit.KryptonButton();
            this.kryptonBorderEdge1 = new Krypton.Toolkit.KryptonBorderEdge();
            this.kryptonPanel2 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonLabel7 = new Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel6 = new Krypton.Toolkit.KryptonLabel();
            this.kcmbToastTitleAlignmentV = new Krypton.Toolkit.KryptonComboBox();
            this.kbtnSampleText = new Krypton.Toolkit.KryptonButton();
            this.kchkShowDoNotShowAgain = new Krypton.Toolkit.KryptonCheckBox();
            this.kchkUseRTL = new Krypton.Toolkit.KryptonCheckBox();
            this.kcbtnBorderColor2 = new Krypton.Toolkit.KryptonColorButton();
            this.kcbtnBorderColor1 = new Krypton.Toolkit.KryptonColorButton();
            this.kchkReportLocation = new Krypton.Toolkit.KryptonCheckBox();
            this.kryptonGroupBox1 = new Krypton.Toolkit.KryptonGroupBox();
            this.klblStartLocationX = new Krypton.Toolkit.KryptonLabel();
            this.kchkSetDefaultLocation = new Krypton.Toolkit.KryptonCheckBox();
            this.klblStartLocation = new Krypton.Toolkit.KryptonLabel();
            this.knudStartLocationY = new Krypton.Toolkit.KryptonNumericUpDown();
            this.knudStartLocationX = new Krypton.Toolkit.KryptonNumericUpDown();
            this.klblStartLocationY = new Krypton.Toolkit.KryptonLabel();
            this.kchkShowProgressBar = new Krypton.Toolkit.KryptonCheckBox();
            this.kcmbToastTitleAlignmentH = new Krypton.Toolkit.KryptonComboBox();
            this.knudCountdownSeconds = new Krypton.Toolkit.KryptonNumericUpDown();
            this.kryptonLabel5 = new Krypton.Toolkit.KryptonLabel();
            this.kbtnTitleFont = new Krypton.Toolkit.KryptonButton();
            this.kbtnContentFont = new Krypton.Toolkit.KryptonButton();
            this.kryptonLabel4 = new Krypton.Toolkit.KryptonLabel();
            this.kchkShowCloseBox = new Krypton.Toolkit.KryptonCheckBox();
            this.kchkTopMost = new Krypton.Toolkit.KryptonCheckBox();
            this.kchkUseFade = new Krypton.Toolkit.KryptonCheckBox();
            this.ktxtCustomToastIconPath = new Krypton.Toolkit.KryptonTextBox();
            this.kcmbToastIcon = new Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel3 = new Krypton.Toolkit.KryptonLabel();
            this.ktxtToastContent = new Krypton.Toolkit.KryptonTextBox();
            this.ktxtToastTitle = new Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel2 = new Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbToastTitleAlignmentV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).BeginInit();
            this.kryptonGroupBox1.Panel.SuspendLayout();
            this.kryptonGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbToastTitleAlignmentH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbToastIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kbtnShow);
            this.kryptonPanel1.Controls.Add(this.kryptonBorderEdge1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 430);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kryptonPanel1.Size = new System.Drawing.Size(894, 50);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kbtnShow
            // 
            this.kbtnShow.Location = new System.Drawing.Point(776, 13);
            this.kbtnShow.Name = "kbtnShow";
            this.kbtnShow.Size = new System.Drawing.Size(90, 25);
            this.kbtnShow.TabIndex = 1;
            this.kbtnShow.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnShow.Values.Text = "Show";
            this.kbtnShow.Click += new System.EventHandler(this.kbtnShow_Click);
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(0, 0);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(894, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.kryptonLabel7);
            this.kryptonPanel2.Controls.Add(this.kryptonLabel6);
            this.kryptonPanel2.Controls.Add(this.kcmbToastTitleAlignmentV);
            this.kryptonPanel2.Controls.Add(this.kbtnSampleText);
            this.kryptonPanel2.Controls.Add(this.kchkShowDoNotShowAgain);
            this.kryptonPanel2.Controls.Add(this.kchkUseRTL);
            this.kryptonPanel2.Controls.Add(this.kcbtnBorderColor2);
            this.kryptonPanel2.Controls.Add(this.kcbtnBorderColor1);
            this.kryptonPanel2.Controls.Add(this.kchkReportLocation);
            this.kryptonPanel2.Controls.Add(this.kryptonGroupBox1);
            this.kryptonPanel2.Controls.Add(this.kchkShowProgressBar);
            this.kryptonPanel2.Controls.Add(this.kcmbToastTitleAlignmentH);
            this.kryptonPanel2.Controls.Add(this.knudCountdownSeconds);
            this.kryptonPanel2.Controls.Add(this.kryptonLabel5);
            this.kryptonPanel2.Controls.Add(this.kbtnTitleFont);
            this.kryptonPanel2.Controls.Add(this.kbtnContentFont);
            this.kryptonPanel2.Controls.Add(this.kryptonLabel4);
            this.kryptonPanel2.Controls.Add(this.kchkShowCloseBox);
            this.kryptonPanel2.Controls.Add(this.kchkTopMost);
            this.kryptonPanel2.Controls.Add(this.kchkUseFade);
            this.kryptonPanel2.Controls.Add(this.ktxtCustomToastIconPath);
            this.kryptonPanel2.Controls.Add(this.kcmbToastIcon);
            this.kryptonPanel2.Controls.Add(this.kryptonLabel3);
            this.kryptonPanel2.Controls.Add(this.ktxtToastContent);
            this.kryptonPanel2.Controls.Add(this.ktxtToastTitle);
            this.kryptonPanel2.Controls.Add(this.kryptonLabel2);
            this.kryptonPanel2.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(894, 430);
            this.kryptonPanel2.TabIndex = 1;
            // 
            // kryptonLabel7
            // 
            this.kryptonLabel7.Location = new System.Drawing.Point(461, 298);
            this.kryptonLabel7.Name = "kryptonLabel7";
            this.kryptonLabel7.Size = new System.Drawing.Size(21, 20);
            this.kryptonLabel7.TabIndex = 31;
            this.kryptonLabel7.Values.Text = "V:";
            // 
            // kryptonLabel6
            // 
            this.kryptonLabel6.Location = new System.Drawing.Point(145, 298);
            this.kryptonLabel6.Name = "kryptonLabel6";
            this.kryptonLabel6.Size = new System.Drawing.Size(22, 20);
            this.kryptonLabel6.TabIndex = 30;
            this.kryptonLabel6.Values.Text = "H:";
            // 
            // kcmbToastTitleAlignmentV
            // 
            this.kcmbToastTitleAlignmentV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbToastTitleAlignmentV.DropDownWidth = 274;
            this.kcmbToastTitleAlignmentV.IntegralHeight = false;
            this.kcmbToastTitleAlignmentV.Location = new System.Drawing.Point(489, 298);
            this.kcmbToastTitleAlignmentV.Name = "kcmbToastTitleAlignmentV";
            this.kcmbToastTitleAlignmentV.Size = new System.Drawing.Size(274, 22);
            this.kcmbToastTitleAlignmentV.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kcmbToastTitleAlignmentV.TabIndex = 29;
            this.kcmbToastTitleAlignmentV.SelectedIndexChanged += new System.EventHandler(this.kcmbToastTitleAlignmentV_SelectedIndexChanged);
            // 
            // kbtnSampleText
            // 
            this.kbtnSampleText.Location = new System.Drawing.Point(13, 66);
            this.kbtnSampleText.Name = "kbtnSampleText";
            this.kbtnSampleText.Size = new System.Drawing.Size(66, 25);
            this.kbtnSampleText.TabIndex = 28;
            this.kbtnSampleText.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnSampleText.Values.Text = "Fill &Text";
            this.kbtnSampleText.Click += new System.EventHandler(this.kbtnSampleText_Click);
            // 
            // kchkShowDoNotShowAgain
            // 
            this.kchkShowDoNotShowAgain.Location = new System.Drawing.Point(503, 197);
            this.kchkShowDoNotShowAgain.Name = "kchkShowDoNotShowAgain";
            this.kchkShowDoNotShowAgain.Size = new System.Drawing.Size(165, 20);
            this.kchkShowDoNotShowAgain.TabIndex = 27;
            this.kchkShowDoNotShowAgain.Values.Text = "Show Do Not Show Again";
            this.kchkShowDoNotShowAgain.CheckedChanged += new System.EventHandler(this.kchkShowDoNotShowAgain_CheckedChanged);
            // 
            // kchkUseRTL
            // 
            this.kchkUseRTL.Location = new System.Drawing.Point(201, 274);
            this.kchkUseRTL.Name = "kchkUseRTL";
            this.kchkUseRTL.Size = new System.Drawing.Size(67, 20);
            this.kchkUseRTL.TabIndex = 26;
            this.kchkUseRTL.Values.Text = "Use RTL";
            this.kchkUseRTL.CheckedChanged += new System.EventHandler(this.kchkUseRTL_CheckedChanged);
            // 
            // kcbtnBorderColor2
            // 
            this.kcbtnBorderColor2.CustomColorPreviewShape = Krypton.Toolkit.KryptonColorButtonCustomColorPreviewShape.Circle;
            this.kcbtnBorderColor2.Location = new System.Drawing.Point(537, 166);
            this.kcbtnBorderColor2.Name = "kcbtnBorderColor2";
            this.kcbtnBorderColor2.Size = new System.Drawing.Size(166, 25);
            this.kcbtnBorderColor2.TabIndex = 25;
            this.kcbtnBorderColor2.Values.Image = ((System.Drawing.Image)(resources.GetObject("kcbtnBorderColor2.Values.Image")));
            this.kcbtnBorderColor2.Values.RoundedCorners = 8;
            this.kcbtnBorderColor2.Values.Text = "Border Color 2";
            this.kcbtnBorderColor2.SelectedColorChanged += new System.EventHandler<Krypton.Toolkit.ColorEventArgs>(this.kcbtnBorderColor2_SelectedColorChanged);
            // 
            // kcbtnBorderColor1
            // 
            this.kcbtnBorderColor1.CustomColorPreviewShape = Krypton.Toolkit.KryptonColorButtonCustomColorPreviewShape.Circle;
            this.kcbtnBorderColor1.Location = new System.Drawing.Point(365, 166);
            this.kcbtnBorderColor1.Name = "kcbtnBorderColor1";
            this.kcbtnBorderColor1.Size = new System.Drawing.Size(166, 25);
            this.kcbtnBorderColor1.TabIndex = 24;
            this.kcbtnBorderColor1.Values.Image = ((System.Drawing.Image)(resources.GetObject("kcbtnBorderColor1.Values.Image")));
            this.kcbtnBorderColor1.Values.RoundedCorners = 8;
            this.kcbtnBorderColor1.Values.Text = "Border Color 1";
            this.kcbtnBorderColor1.SelectedColorChanged += new System.EventHandler<Krypton.Toolkit.ColorEventArgs>(this.kcbtnBorderColor1_SelectedColorChanged);
            // 
            // kchkReportLocation
            // 
            this.kchkReportLocation.Location = new System.Drawing.Point(187, 248);
            this.kchkReportLocation.Name = "kchkReportLocation";
            this.kchkReportLocation.Size = new System.Drawing.Size(110, 20);
            this.kchkReportLocation.TabIndex = 23;
            this.kchkReportLocation.Values.Text = "Report Location";
            this.kchkReportLocation.CheckedChanged += new System.EventHandler(this.kchkReportLocation_CheckedChanged);
            // 
            // kryptonGroupBox1
            // 
            this.kryptonGroupBox1.Location = new System.Drawing.Point(365, 13);
            // 
            // kryptonGroupBox1.Panel
            // 
            this.kryptonGroupBox1.Panel.Controls.Add(this.klblStartLocationX);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kchkSetDefaultLocation);
            this.kryptonGroupBox1.Panel.Controls.Add(this.klblStartLocation);
            this.kryptonGroupBox1.Panel.Controls.Add(this.knudStartLocationY);
            this.kryptonGroupBox1.Panel.Controls.Add(this.knudStartLocationX);
            this.kryptonGroupBox1.Panel.Controls.Add(this.klblStartLocationY);
            this.kryptonGroupBox1.Size = new System.Drawing.Size(443, 146);
            this.kryptonGroupBox1.TabIndex = 22;
            // 
            // klblStartLocationX
            // 
            this.klblStartLocationX.Enabled = false;
            this.klblStartLocationX.Location = new System.Drawing.Point(12, 73);
            this.klblStartLocationX.Name = "klblStartLocationX";
            this.klblStartLocationX.Size = new System.Drawing.Size(20, 20);
            this.klblStartLocationX.TabIndex = 23;
            this.klblStartLocationX.Values.Text = "X:";
            // 
            // kchkSetDefaultLocation
            // 
            this.kchkSetDefaultLocation.Location = new System.Drawing.Point(12, 11);
            this.kchkSetDefaultLocation.Name = "kchkSetDefaultLocation";
            this.kchkSetDefaultLocation.Size = new System.Drawing.Size(133, 20);
            this.kchkSetDefaultLocation.TabIndex = 22;
            this.kchkSetDefaultLocation.Values.Text = "Set Default Location";
            // 
            // klblStartLocation
            // 
            this.klblStartLocation.Enabled = false;
            this.klblStartLocation.Location = new System.Drawing.Point(12, 47);
            this.klblStartLocation.Name = "klblStartLocation";
            this.klblStartLocation.Size = new System.Drawing.Size(89, 20);
            this.klblStartLocation.TabIndex = 18;
            this.klblStartLocation.Values.Text = "Start Location:";
            // 
            // knudStartLocationY
            // 
            this.knudStartLocationY.Enabled = false;
            this.knudStartLocationY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.knudStartLocationY.Location = new System.Drawing.Point(193, 73);
            this.knudStartLocationY.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.knudStartLocationY.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.knudStartLocationY.Name = "knudStartLocationY";
            this.knudStartLocationY.Size = new System.Drawing.Size(120, 22);
            this.knudStartLocationY.TabIndex = 21;
            this.knudStartLocationY.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // knudStartLocationX
            // 
            this.knudStartLocationX.Enabled = false;
            this.knudStartLocationX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.knudStartLocationX.Location = new System.Drawing.Point(38, 73);
            this.knudStartLocationX.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.knudStartLocationX.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.knudStartLocationX.Name = "knudStartLocationX";
            this.knudStartLocationX.Size = new System.Drawing.Size(120, 22);
            this.knudStartLocationX.TabIndex = 19;
            this.knudStartLocationX.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // klblStartLocationY
            // 
            this.klblStartLocationY.Enabled = false;
            this.klblStartLocationY.Location = new System.Drawing.Point(164, 73);
            this.klblStartLocationY.Name = "klblStartLocationY";
            this.klblStartLocationY.Size = new System.Drawing.Size(23, 20);
            this.klblStartLocationY.TabIndex = 20;
            this.klblStartLocationY.Values.Text = " Y:";
            // 
            // kchkShowProgressBar
            // 
            this.kchkShowProgressBar.Location = new System.Drawing.Point(187, 221);
            this.kchkShowProgressBar.Name = "kchkShowProgressBar";
            this.kchkShowProgressBar.Size = new System.Drawing.Size(125, 20);
            this.kchkShowProgressBar.TabIndex = 17;
            this.kchkShowProgressBar.Values.Text = "Show Progress Bar";
            // 
            // kcmbToastTitleAlignmentH
            // 
            this.kcmbToastTitleAlignmentH.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbToastTitleAlignmentH.DropDownWidth = 274;
            this.kcmbToastTitleAlignmentH.IntegralHeight = false;
            this.kcmbToastTitleAlignmentH.Location = new System.Drawing.Point(173, 298);
            this.kcmbToastTitleAlignmentH.Name = "kcmbToastTitleAlignmentH";
            this.kcmbToastTitleAlignmentH.Size = new System.Drawing.Size(274, 22);
            this.kcmbToastTitleAlignmentH.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kcmbToastTitleAlignmentH.TabIndex = 16;
            this.kcmbToastTitleAlignmentH.SelectedIndexChanged += new System.EventHandler(this.kcmbToastTitleAlignmentH_SelectedIndexChanged);
            // 
            // knudCountdownSeconds
            // 
            this.knudCountdownSeconds.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.knudCountdownSeconds.Location = new System.Drawing.Point(192, 366);
            this.knudCountdownSeconds.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.knudCountdownSeconds.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.knudCountdownSeconds.Name = "knudCountdownSeconds";
            this.knudCountdownSeconds.Size = new System.Drawing.Size(120, 22);
            this.knudCountdownSeconds.TabIndex = 15;
            this.knudCountdownSeconds.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.knudCountdownSeconds.ValueChanged += new System.EventHandler(this.knudCountdownSeconds_ValueChanged);
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(13, 366);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(126, 20);
            this.kryptonLabel5.TabIndex = 14;
            this.kryptonLabel5.Values.Text = "Countdown Seconds:";
            // 
            // kbtnTitleFont
            // 
            this.kbtnTitleFont.AutoSize = true;
            this.kbtnTitleFont.Location = new System.Drawing.Point(208, 335);
            this.kbtnTitleFont.Name = "kbtnTitleFont";
            this.kbtnTitleFont.Size = new System.Drawing.Size(117, 25);
            this.kbtnTitleFont.TabIndex = 13;
            this.kbtnTitleFont.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnTitleFont.Values.Text = "Title Font";
            this.kbtnTitleFont.Click += new System.EventHandler(this.kbtnTitleFont_Click);
            // 
            // kbtnContentFont
            // 
            this.kbtnContentFont.AutoSize = true;
            this.kbtnContentFont.Location = new System.Drawing.Point(85, 335);
            this.kbtnContentFont.Name = "kbtnContentFont";
            this.kbtnContentFont.Size = new System.Drawing.Size(117, 25);
            this.kbtnContentFont.TabIndex = 12;
            this.kbtnContentFont.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnContentFont.Values.Text = "Content Font";
            this.kbtnContentFont.Click += new System.EventHandler(this.kbtnContentFont_Click);
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(13, 298);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(96, 20);
            this.kryptonLabel4.TabIndex = 10;
            this.kryptonLabel4.Values.Text = "Title Alignment:";
            // 
            // kchkShowCloseBox
            // 
            this.kchkShowCloseBox.Location = new System.Drawing.Point(85, 273);
            this.kchkShowCloseBox.Name = "kchkShowCloseBox";
            this.kchkShowCloseBox.Size = new System.Drawing.Size(110, 20);
            this.kchkShowCloseBox.TabIndex = 9;
            this.kchkShowCloseBox.Values.Text = "Show Close Box";
            this.kchkShowCloseBox.CheckedChanged += new System.EventHandler(this.kchkShowCloseBox_CheckedChanged);
            // 
            // kchkTopMost
            // 
            this.kchkTopMost.Checked = true;
            this.kchkTopMost.CheckState = System.Windows.Forms.CheckState.Checked;
            this.kchkTopMost.Location = new System.Drawing.Point(85, 247);
            this.kchkTopMost.Name = "kchkTopMost";
            this.kchkTopMost.Size = new System.Drawing.Size(76, 20);
            this.kchkTopMost.TabIndex = 8;
            this.kchkTopMost.Values.Text = "Top Most";
            // 
            // kchkUseFade
            // 
            this.kchkUseFade.Location = new System.Drawing.Point(85, 221);
            this.kchkUseFade.Name = "kchkUseFade";
            this.kchkUseFade.Size = new System.Drawing.Size(73, 20);
            this.kchkUseFade.TabIndex = 7;
            this.kchkUseFade.Values.Text = "Use Fade";
            this.kchkUseFade.CheckedChanged += new System.EventHandler(this.kchkUseFade_CheckedChanged);
            // 
            // ktxtCustomToastIconPath
            // 
            this.ktxtCustomToastIconPath.Enabled = false;
            this.ktxtCustomToastIconPath.Location = new System.Drawing.Point(85, 191);
            this.ktxtCustomToastIconPath.Name = "ktxtCustomToastIconPath";
            this.ktxtCustomToastIconPath.ShowEllipsisButton = true;
            this.ktxtCustomToastIconPath.Size = new System.Drawing.Size(274, 24);
            this.ktxtCustomToastIconPath.TabIndex = 6;
            // 
            // kcmbToastIcon
            // 
            this.kcmbToastIcon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbToastIcon.DropDownWidth = 274;
            this.kcmbToastIcon.IntegralHeight = false;
            this.kcmbToastIcon.Location = new System.Drawing.Point(85, 165);
            this.kcmbToastIcon.Name = "kcmbToastIcon";
            this.kcmbToastIcon.Size = new System.Drawing.Size(274, 22);
            this.kcmbToastIcon.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kcmbToastIcon.TabIndex = 5;
            this.kcmbToastIcon.SelectedIndexChanged += new System.EventHandler(this.kcmbToastIcon_SelectedIndexChanged);
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(13, 165);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(36, 20);
            this.kryptonLabel3.TabIndex = 4;
            this.kryptonLabel3.Values.Text = "Icon:";
            // 
            // ktxtToastContent
            // 
            this.ktxtToastContent.Location = new System.Drawing.Point(85, 41);
            this.ktxtToastContent.Multiline = true;
            this.ktxtToastContent.Name = "ktxtToastContent";
            this.ktxtToastContent.Size = new System.Drawing.Size(274, 118);
            this.ktxtToastContent.TabIndex = 3;
            this.ktxtToastContent.Text = "Put your message here...";
            this.ktxtToastContent.TextChanged += new System.EventHandler(this.ktxtToastContent_TextChanged);
            // 
            // ktxtToastTitle
            // 
            this.ktxtToastTitle.Location = new System.Drawing.Point(85, 13);
            this.ktxtToastTitle.Name = "ktxtToastTitle";
            this.ktxtToastTitle.Size = new System.Drawing.Size(274, 23);
            this.ktxtToastTitle.TabIndex = 2;
            this.ktxtToastTitle.Text = "This is a test";
            this.ktxtToastTitle.TextChanged += new System.EventHandler(this.ktxtToastTitle_TextChanged);
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(13, 39);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(36, 20);
            this.kryptonLabel2.TabIndex = 1;
            this.kryptonLabel2.Values.Text = "Text:";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(13, 13);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(36, 20);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "Title:";
            // 
            // BasicToastNotificationTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 480);
            this.Controls.Add(this.kryptonPanel2);
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "BasicToastNotificationTest";
            this.Text = "ToastNotificationTest";
            this.Load += new System.EventHandler(this.ToastNotificationTest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            this.kryptonPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbToastTitleAlignmentV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).EndInit();
            this.kryptonGroupBox1.Panel.ResumeLayout(false);
            this.kryptonGroupBox1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).EndInit();
            this.kryptonGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kcmbToastTitleAlignmentH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbToastIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonBorderEdge kryptonBorderEdge1;
        private Krypton.Toolkit.KryptonPanel kryptonPanel2;
        private Krypton.Toolkit.KryptonButton kbtnShow;
        private Krypton.Toolkit.KryptonCheckBox kchkUseFade;
        private Krypton.Toolkit.KryptonTextBox ktxtCustomToastIconPath;
        private Krypton.Toolkit.KryptonComboBox kcmbToastIcon;
        private Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private Krypton.Toolkit.KryptonTextBox ktxtToastContent;
        private Krypton.Toolkit.KryptonTextBox ktxtToastTitle;
        private Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private Krypton.Toolkit.KryptonButton kbtnTitleFont;
        private Krypton.Toolkit.KryptonButton kbtnContentFont;
        private Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private Krypton.Toolkit.KryptonCheckBox kchkShowCloseBox;
        private Krypton.Toolkit.KryptonCheckBox kchkTopMost;
        private Krypton.Toolkit.KryptonComboBox kcmbToastTitleAlignmentH;
        private Krypton.Toolkit.KryptonNumericUpDown knudCountdownSeconds;
        private Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private Krypton.Toolkit.KryptonCheckBox kchkShowProgressBar;
        private Krypton.Toolkit.KryptonLabel klblStartLocation;
        private Krypton.Toolkit.KryptonLabel klblStartLocationY;
        private Krypton.Toolkit.KryptonNumericUpDown knudStartLocationX;
        private Krypton.Toolkit.KryptonNumericUpDown knudStartLocationY;
        private Krypton.Toolkit.KryptonGroupBox kryptonGroupBox1;
        private Krypton.Toolkit.KryptonCheckBox kchkSetDefaultLocation;
        private Krypton.Toolkit.KryptonLabel klblStartLocationX;
        private Krypton.Toolkit.KryptonCheckBox kchkReportLocation;
        private Krypton.Toolkit.KryptonColorButton kcbtnBorderColor1;
        private Krypton.Toolkit.KryptonColorButton kcbtnBorderColor2;
        private Krypton.Toolkit.KryptonCheckBox kchkUseRTL;
        private Krypton.Toolkit.KryptonCheckBox kchkShowDoNotShowAgain;
        private KryptonButton kbtnSampleText;
        private KryptonComboBox kcmbToastTitleAlignmentV;
        private KryptonLabel kryptonLabel6;
        private KryptonLabel kryptonLabel7;
    }
}
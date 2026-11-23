namespace TestForm
{
    partial class TouchscreenHighDpiDemo
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
                _dpiMonitorTimer?.Stop();
                _dpiMonitorTimer?.Dispose();
                KryptonManager.GlobalTouchscreenSupportChanged -= OnGlobalTouchscreenSupportChanged;
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
            this.grpControls = new Krypton.Toolkit.KryptonGroupBox();
            this.grpAdvancedControls = new Krypton.Toolkit.KryptonGroupBox();
            this.workspace = new Krypton.Workspace.KryptonWorkspace();
            this.navigator = new Krypton.Navigator.KryptonNavigator();
            this.grpInputControls = new Krypton.Toolkit.KryptonGroupBox();
            this.txtInput = new Krypton.Toolkit.KryptonTextBox();
            this.txtNumeric = new Krypton.Toolkit.KryptonNumericUpDown();
            this.cmbOptions = new Krypton.Toolkit.KryptonComboBox();
            this.grpButtons = new Krypton.Toolkit.KryptonGroupBox();
            this.btnStandard = new Krypton.Toolkit.KryptonButton();
            this.btnPrimary = new Krypton.Toolkit.KryptonButton();
            this.btnSuccess = new Krypton.Toolkit.KryptonButton();
            this.grpCheckboxes = new Krypton.Toolkit.KryptonGroupBox();
            this.chkOption1 = new Krypton.Toolkit.KryptonCheckBox();
            this.chkOption2 = new Krypton.Toolkit.KryptonCheckBox();
            this.chkOption3 = new Krypton.Toolkit.KryptonCheckBox();
            this.grpRadioButtons = new Krypton.Toolkit.KryptonGroupBox();
            this.radioOption1 = new Krypton.Toolkit.KryptonRadioButton();
            this.radioOption2 = new Krypton.Toolkit.KryptonRadioButton();
            this.radioOption3 = new Krypton.Toolkit.KryptonRadioButton();
            this.grpOtherControls = new Krypton.Toolkit.KryptonGroupBox();
            this.progressBar = new Krypton.Toolkit.KryptonProgressBar();
            this.trackBar = new Krypton.Toolkit.KryptonTrackBar();
            this.grpSettings = new Krypton.Toolkit.KryptonGroupBox();
            this.grpDpiInfo = new Krypton.Toolkit.KryptonGroupBox();
            this.lblScalingExample = new Krypton.Toolkit.KryptonLabel();
            this.lblDpiWarning = new Krypton.Toolkit.KryptonLabel();
            this.lblCombinedPerMonitor = new Krypton.Toolkit.KryptonLabel();
            this.lblCombinedPrimary = new Krypton.Toolkit.KryptonLabel();
            this.lblDpiActual = new Krypton.Toolkit.KryptonLabel();
            this.lblDpiPerMonitor = new Krypton.Toolkit.KryptonLabel();
            this.lblDpiPrimary = new Krypton.Toolkit.KryptonLabel();
            this.btnRefreshDpi = new Krypton.Toolkit.KryptonButton();
            this.lblStatus = new Krypton.Toolkit.KryptonLabel();
            this.btnToggle = new Krypton.Toolkit.KryptonButton();
            this.btnApplyPreset75 = new Krypton.Toolkit.KryptonButton();
            this.btnApplyPreset50 = new Krypton.Toolkit.KryptonButton();
            this.btnApplyPreset25 = new Krypton.Toolkit.KryptonButton();
            this.btnResetScale = new Krypton.Toolkit.KryptonButton();
            this.lblScaleValue = new Krypton.Toolkit.KryptonLabel();
            this.trackScaleFactor = new Krypton.Toolkit.KryptonTrackBar();
            this.lblScaleFactor = new Krypton.Toolkit.KryptonLabel();
            this.chkEnableTouchscreen = new Krypton.Toolkit.KryptonCheckBox();
            this.chkEnableFontScaling = new Krypton.Toolkit.KryptonCheckBox();
            this.lblFontScaleFactor = new Krypton.Toolkit.KryptonLabel();
            this.trackFontScaleFactor = new Krypton.Toolkit.KryptonTrackBar();
            this.lblFontScaleValue = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.grpControls)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpControls.Panel)).BeginInit();
            this.grpControls.Panel.SuspendLayout();
            this.grpControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpInputControls)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpInputControls.Panel)).BeginInit();
            this.grpInputControls.Panel.SuspendLayout();
            this.grpInputControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpButtons)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpButtons.Panel)).BeginInit();
            this.grpButtons.Panel.SuspendLayout();
            this.grpButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpCheckboxes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpCheckboxes.Panel)).BeginInit();
            this.grpCheckboxes.Panel.SuspendLayout();
            this.grpCheckboxes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpRadioButtons)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpRadioButtons.Panel)).BeginInit();
            this.grpRadioButtons.Panel.SuspendLayout();
            this.grpRadioButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpOtherControls)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpOtherControls.Panel)).BeginInit();
            this.grpOtherControls.Panel.SuspendLayout();
            this.grpOtherControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpSettings.Panel)).BeginInit();
            this.grpSettings.Panel.SuspendLayout();
            this.grpSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpDpiInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpDpiInfo.Panel)).BeginInit();
            this.grpDpiInfo.Panel.SuspendLayout();
            this.grpDpiInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpControls
            // 
            this.grpControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpControls.Location = new System.Drawing.Point(0, 0);
            this.grpControls.Name = "grpControls";
            this.grpControls.Size = new System.Drawing.Size(1000, 500);
            this.grpControls.Panel.AutoScroll = true;
            this.grpControls.TabIndex = 0;
            this.grpControls.Values.Heading = "Control Examples (These scale with High DPI + Touchscreen support)";
            // 
            // grpControls.Panel
            // 
            this.grpControls.Panel.Controls.Add(this.grpAdvancedControls);
            this.grpControls.Panel.Controls.Add(this.grpOtherControls);
            this.grpControls.Panel.Controls.Add(this.grpRadioButtons);
            this.grpControls.Panel.Controls.Add(this.grpCheckboxes);
            this.grpControls.Panel.Controls.Add(this.grpButtons);
            this.grpControls.Panel.Controls.Add(this.grpInputControls);
            // 
            // grpInputControls
            // 
            this.grpInputControls.Location = new System.Drawing.Point(15, 15);
            this.grpInputControls.Name = "grpInputControls";
            this.grpInputControls.Size = new System.Drawing.Size(400, 120);
            this.grpInputControls.TabIndex = 0;
            this.grpInputControls.Values.Heading = "Input Controls";
            // 
            // grpInputControls.Panel
            // 
            this.grpInputControls.Panel.Controls.Add(this.cmbOptions);
            this.grpInputControls.Panel.Controls.Add(this.txtNumeric);
            this.grpInputControls.Panel.Controls.Add(this.txtInput);
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(15, 20);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(370, 27);
            this.txtInput.TabIndex = 0;
            // 
            // txtNumeric
            // 
            this.txtNumeric.Location = new System.Drawing.Point(15, 55);
            this.txtNumeric.Name = "txtNumeric";
            this.txtNumeric.Size = new System.Drawing.Size(180, 27);
            this.txtNumeric.TabIndex = 1;
            // 
            // cmbOptions
            // 
            this.cmbOptions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOptions.DropDownWidth = 370;
            this.cmbOptions.Location = new System.Drawing.Point(205, 55);
            this.cmbOptions.Name = "cmbOptions";
            this.cmbOptions.Size = new System.Drawing.Size(180, 27);
            this.cmbOptions.TabIndex = 2;
            // 
            // grpButtons
            // 
            this.grpButtons.Location = new System.Drawing.Point(430, 15);
            this.grpButtons.Name = "grpButtons";
            this.grpButtons.Size = new System.Drawing.Size(430, 120);
            this.grpButtons.TabIndex = 1;
            this.grpButtons.Values.Heading = "Buttons";
            // 
            // grpButtons.Panel
            // 
            this.grpButtons.Panel.Controls.Add(this.btnSuccess);
            this.grpButtons.Panel.Controls.Add(this.btnPrimary);
            this.grpButtons.Panel.Controls.Add(this.btnStandard);
            // 
            // btnStandard
            // 
            this.btnStandard.Location = new System.Drawing.Point(15, 20);
            this.btnStandard.Name = "btnStandard";
            this.btnStandard.Size = new System.Drawing.Size(120, 35);
            this.btnStandard.TabIndex = 0;
            this.btnStandard.Values.Text = "Standard";
            // 
            // btnPrimary
            // 
            this.btnPrimary.Location = new System.Drawing.Point(150, 20);
            this.btnPrimary.Name = "btnPrimary";
            this.btnPrimary.Size = new System.Drawing.Size(120, 35);
            this.btnPrimary.TabIndex = 1;
            this.btnPrimary.Values.Text = "Primary";
            // 
            // btnSuccess
            // 
            this.btnSuccess.Location = new System.Drawing.Point(285, 20);
            this.btnSuccess.Name = "btnSuccess";
            this.btnSuccess.Size = new System.Drawing.Size(120, 35);
            this.btnSuccess.TabIndex = 2;
            this.btnSuccess.Values.Text = "Success";
            // 
            // grpCheckboxes
            // 
            this.grpCheckboxes.Location = new System.Drawing.Point(15, 150);
            this.grpCheckboxes.Name = "grpCheckboxes";
            this.grpCheckboxes.Size = new System.Drawing.Size(400, 100);
            this.grpCheckboxes.TabIndex = 2;
            this.grpCheckboxes.Values.Heading = "Checkboxes";
            // 
            // grpCheckboxes.Panel
            // 
            this.grpCheckboxes.Panel.Controls.Add(this.chkOption3);
            this.grpCheckboxes.Panel.Controls.Add(this.chkOption2);
            this.grpCheckboxes.Panel.Controls.Add(this.chkOption1);
            // 
            // chkOption1
            // 
            this.chkOption1.Location = new System.Drawing.Point(15, 20);
            this.chkOption1.Name = "chkOption1";
            this.chkOption1.Size = new System.Drawing.Size(100, 20);
            this.chkOption1.TabIndex = 0;
            this.chkOption1.Values.Text = "Option 1";
            // 
            // chkOption2
            // 
            this.chkOption2.Location = new System.Drawing.Point(15, 50);
            this.chkOption2.Name = "chkOption2";
            this.chkOption2.Size = new System.Drawing.Size(100, 20);
            this.chkOption2.TabIndex = 1;
            this.chkOption2.Values.Text = "Option 2";
            // 
            // chkOption3
            // 
            this.chkOption3.Location = new System.Drawing.Point(150, 20);
            this.chkOption3.Name = "chkOption3";
            this.chkOption3.Size = new System.Drawing.Size(100, 20);
            this.chkOption3.TabIndex = 2;
            this.chkOption3.Values.Text = "Option 3";
            // 
            // grpRadioButtons
            // 
            this.grpRadioButtons.Location = new System.Drawing.Point(430, 150);
            this.grpRadioButtons.Name = "grpRadioButtons";
            this.grpRadioButtons.Size = new System.Drawing.Size(430, 100);
            this.grpRadioButtons.TabIndex = 3;
            this.grpRadioButtons.Values.Heading = "Radio Buttons";
            // 
            // grpRadioButtons.Panel
            // 
            this.grpRadioButtons.Panel.Controls.Add(this.radioOption3);
            this.grpRadioButtons.Panel.Controls.Add(this.radioOption2);
            this.grpRadioButtons.Panel.Controls.Add(this.radioOption1);
            // 
            // radioOption1
            // 
            this.radioOption1.Location = new System.Drawing.Point(15, 20);
            this.radioOption1.Name = "radioOption1";
            this.radioOption1.Size = new System.Drawing.Size(120, 20);
            this.radioOption1.TabIndex = 0;
            this.radioOption1.Values.Text = "Radio Option A";
            // 
            // radioOption2
            // 
            this.radioOption2.Location = new System.Drawing.Point(15, 50);
            this.radioOption2.Name = "radioOption2";
            this.radioOption2.Size = new System.Drawing.Size(120, 20);
            this.radioOption2.TabIndex = 1;
            this.radioOption2.Values.Text = "Radio Option B";
            // 
            // radioOption3
            // 
            this.radioOption3.Location = new System.Drawing.Point(150, 20);
            this.radioOption3.Name = "radioOption3";
            this.radioOption3.Size = new System.Drawing.Size(120, 20);
            this.radioOption3.TabIndex = 2;
            this.radioOption3.Values.Text = "Radio Option C";
            // 
            // grpOtherControls
            // 
            this.grpOtherControls.Location = new System.Drawing.Point(15, 265);
            this.grpOtherControls.Name = "grpOtherControls";
            this.grpOtherControls.Size = new System.Drawing.Size(845, 100);
            this.grpOtherControls.TabIndex = 4;
            this.grpOtherControls.Values.Heading = "Other Controls";
            // 
            // grpOtherControls.Panel
            // 
            this.grpOtherControls.Panel.Controls.Add(this.trackBar);
            this.grpOtherControls.Panel.Controls.Add(this.progressBar);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(15, 20);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(400, 25);
            this.progressBar.TabIndex = 0;
            this.progressBar.Value = 65;
            // 
            // trackBar
            // 
            this.trackBar.Location = new System.Drawing.Point(15, 55);
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(400, 45);
            this.trackBar.TabIndex = 1;
            this.trackBar.TickFrequency = 10;
            // 
            // grpAdvancedControls
            // 
            this.grpAdvancedControls.Location = new System.Drawing.Point(15, 380);
            this.grpAdvancedControls.Name = "grpAdvancedControls";
            this.grpAdvancedControls.Size = new System.Drawing.Size(845, 100);
            this.grpAdvancedControls.TabIndex = 5;
            this.grpAdvancedControls.Values.Heading = "Advanced Controls (Navigator & Workspace - Per-Monitor DPI aware)";
            // 
            // grpAdvancedControls.Panel
            // 
            this.grpAdvancedControls.Panel.Controls.Add(this.workspace);
            this.grpAdvancedControls.Panel.Controls.Add(this.navigator);
            // 
            // navigator
            // 
            this.navigator.Bar.BarFirstItemInset = 0;
            this.navigator.Bar.BarLastItemInset = 0;
            this.navigator.Bar.BarMinimumHeight = 0;
            this.navigator.Bar.TabStyle = Krypton.Toolkit.TabStyle.StandardProfile;
            this.navigator.Button.ButtonDisplayLogic = Krypton.Navigator.ButtonDisplayLogic.None;
            this.navigator.Button.ContextButtonAction = Krypton.Navigator.ContextButtonAction.None;
            this.navigator.Dock = System.Windows.Forms.DockStyle.Left;
            this.navigator.Location = new System.Drawing.Point(15, 20);
            this.navigator.Name = "navigator";
            this.navigator.NavigatorMode = Krypton.Navigator.NavigatorMode.BarTabGroup;
            this.navigator.Size = new System.Drawing.Size(250, 60);
            this.navigator.TabIndex = 0;
            // 
            // workspace
            // 
            this.workspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.workspace.Location = new System.Drawing.Point(265, 20);
            this.workspace.Name = "workspace";
            this.workspace.Size = new System.Drawing.Size(565, 60);
            this.workspace.TabIndex = 1;
            // 
            // grpSettings
            // 
            this.grpSettings.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpSettings.Location = new System.Drawing.Point(0, 500);
            this.grpSettings.Name = "grpSettings";
            this.grpSettings.Size = new System.Drawing.Size(1000, 500);
            this.grpSettings.TabIndex = 1;
            this.grpSettings.Values.Heading = "Touchscreen + High DPI Settings";
            // 
            // grpSettings.Panel
            // 
            this.grpSettings.Panel.Controls.Add(this.grpDpiInfo);
            this.grpSettings.Panel.Controls.Add(this.lblStatus);
            this.grpSettings.Panel.Controls.Add(this.btnToggle);
            this.grpSettings.Panel.Controls.Add(this.btnApplyPreset75);
            this.grpSettings.Panel.Controls.Add(this.btnApplyPreset50);
            this.grpSettings.Panel.Controls.Add(this.btnApplyPreset25);
            this.grpSettings.Panel.Controls.Add(this.btnResetScale);
            this.grpSettings.Panel.Controls.Add(this.lblScaleValue);
            this.grpSettings.Panel.Controls.Add(this.trackScaleFactor);
            this.grpSettings.Panel.Controls.Add(this.lblScaleFactor);
            this.grpSettings.Panel.Controls.Add(this.chkEnableTouchscreen);
            this.grpSettings.Panel.Controls.Add(this.chkEnableFontScaling);
            this.grpSettings.Panel.Controls.Add(this.lblFontScaleFactor);
            this.grpSettings.Panel.Controls.Add(this.trackFontScaleFactor);
            this.grpSettings.Panel.Controls.Add(this.lblFontScaleValue);
            // 
            // chkEnableTouchscreen
            // 
            this.chkEnableTouchscreen.Location = new System.Drawing.Point(15, 20);
            this.chkEnableTouchscreen.Name = "chkEnableTouchscreen";
            this.chkEnableTouchscreen.Size = new System.Drawing.Size(200, 20);
            this.chkEnableTouchscreen.TabIndex = 0;
            this.chkEnableTouchscreen.Values.Text = "Enable Touchscreen Support";
            // 
            // lblScaleFactor
            // 
            this.lblScaleFactor.Location = new System.Drawing.Point(15, 50);
            this.lblScaleFactor.Name = "lblScaleFactor";
            this.lblScaleFactor.Size = new System.Drawing.Size(200, 20);
            this.lblScaleFactor.TabIndex = 1;
            this.lblScaleFactor.Values.Text = "Scale Factor (1.0x - 3.0x):";
            // 
            // trackScaleFactor
            // 
            this.trackScaleFactor.Location = new System.Drawing.Point(15, 75);
            this.trackScaleFactor.Maximum = 200;
            this.trackScaleFactor.Minimum = 0;
            this.trackScaleFactor.Name = "trackScaleFactor";
            this.trackScaleFactor.Size = new System.Drawing.Size(400, 45);
            this.trackScaleFactor.TabIndex = 2;
            this.trackScaleFactor.TickFrequency = 25;
            this.trackScaleFactor.Value = 25;
            // 
            // lblScaleValue
            // 
            this.lblScaleValue.Location = new System.Drawing.Point(430, 75);
            this.lblScaleValue.Name = "lblScaleValue";
            this.lblScaleValue.Size = new System.Drawing.Size(200, 20);
            this.lblScaleValue.TabIndex = 3;
            this.lblScaleValue.Values.Text = "1.25x (25.0% larger)";
            // 
            // btnResetScale
            // 
            this.btnResetScale.Location = new System.Drawing.Point(650, 75);
            this.btnResetScale.Name = "btnResetScale";
            this.btnResetScale.Size = new System.Drawing.Size(100, 35);
            this.btnResetScale.TabIndex = 4;
            this.btnResetScale.Values.Text = "Reset (1.25x)";
            // 
            // btnApplyPreset25
            // 
            this.btnApplyPreset25.Location = new System.Drawing.Point(15, 130);
            this.btnApplyPreset25.Name = "btnApplyPreset25";
            this.btnApplyPreset25.Size = new System.Drawing.Size(120, 35);
            this.btnApplyPreset25.TabIndex = 5;
            this.btnApplyPreset25.Values.Text = "Preset: 25%";
            // 
            // btnApplyPreset50
            // 
            this.btnApplyPreset50.Location = new System.Drawing.Point(150, 130);
            this.btnApplyPreset50.Name = "btnApplyPreset50";
            this.btnApplyPreset50.Size = new System.Drawing.Size(120, 35);
            this.btnApplyPreset50.TabIndex = 6;
            this.btnApplyPreset50.Values.Text = "Preset: 50%";
            // 
            // btnApplyPreset75
            // 
            this.btnApplyPreset75.Location = new System.Drawing.Point(285, 130);
            this.btnApplyPreset75.Name = "btnApplyPreset75";
            this.btnApplyPreset75.Size = new System.Drawing.Size(120, 35);
            this.btnApplyPreset75.TabIndex = 7;
            this.btnApplyPreset75.Values.Text = "Preset: 75%";
            // 
            // btnToggle
            // 
            this.btnToggle.Location = new System.Drawing.Point(430, 130);
            this.btnToggle.Name = "btnToggle";
            this.btnToggle.Size = new System.Drawing.Size(200, 35);
            this.btnToggle.TabIndex = 8;
            this.btnToggle.Values.Text = "Toggle Support";
            // 
            // chkEnableFontScaling
            // 
            this.chkEnableFontScaling.Location = new System.Drawing.Point(15, 180);
            this.chkEnableFontScaling.Name = "chkEnableFontScaling";
            this.chkEnableFontScaling.Size = new System.Drawing.Size(200, 20);
            this.chkEnableFontScaling.TabIndex = 9;
            this.chkEnableFontScaling.Values.Text = "Enable Font Scaling";
            // 
            // lblFontScaleFactor
            // 
            this.lblFontScaleFactor.Location = new System.Drawing.Point(15, 210);
            this.lblFontScaleFactor.Name = "lblFontScaleFactor";
            this.lblFontScaleFactor.Size = new System.Drawing.Size(200, 20);
            this.lblFontScaleFactor.TabIndex = 10;
            this.lblFontScaleFactor.Values.Text = "Font Scale Factor (1.0x - 3.0x):";
            // 
            // trackFontScaleFactor
            // 
            this.trackFontScaleFactor.Location = new System.Drawing.Point(15, 235);
            this.trackFontScaleFactor.Maximum = 200;
            this.trackFontScaleFactor.Minimum = 0;
            this.trackFontScaleFactor.Name = "trackFontScaleFactor";
            this.trackFontScaleFactor.Size = new System.Drawing.Size(400, 45);
            this.trackFontScaleFactor.TabIndex = 11;
            this.trackFontScaleFactor.TickFrequency = 25;
            this.trackFontScaleFactor.Value = 25;
            // 
            // lblFontScaleValue
            // 
            this.lblFontScaleValue.Location = new System.Drawing.Point(430, 235);
            this.lblFontScaleValue.Name = "lblFontScaleValue";
            this.lblFontScaleValue.Size = new System.Drawing.Size(200, 20);
            this.lblFontScaleValue.TabIndex = 12;
            this.lblFontScaleValue.Values.Text = "1.25x (25.0% larger)";
            // 
            // grpDpiInfo
            // 
            this.grpDpiInfo.Location = new System.Drawing.Point(15, 290);
            this.grpDpiInfo.Name = "grpDpiInfo";
            this.grpDpiInfo.Size = new System.Drawing.Size(970, 190);
            this.grpDpiInfo.TabIndex = 13;
            this.grpDpiInfo.Values.Heading = "High DPI Information (Per-Monitor DPI Awareness)";
            // 
            // grpDpiInfo.Panel
            // 
            this.grpDpiInfo.Panel.Controls.Add(this.lblScalingExample);
            this.grpDpiInfo.Panel.Controls.Add(this.lblDpiWarning);
            this.grpDpiInfo.Panel.Controls.Add(this.lblCombinedPerMonitor);
            this.grpDpiInfo.Panel.Controls.Add(this.lblCombinedPrimary);
            this.grpDpiInfo.Panel.Controls.Add(this.lblDpiActual);
            this.grpDpiInfo.Panel.Controls.Add(this.lblDpiPerMonitor);
            this.grpDpiInfo.Panel.Controls.Add(this.lblDpiPrimary);
            this.grpDpiInfo.Panel.Controls.Add(this.btnRefreshDpi);
            // 
            // btnRefreshDpi
            // 
            this.btnRefreshDpi.Location = new System.Drawing.Point(850, 20);
            this.btnRefreshDpi.Name = "btnRefreshDpi";
            this.btnRefreshDpi.Size = new System.Drawing.Size(100, 35);
            this.btnRefreshDpi.TabIndex = 0;
            this.btnRefreshDpi.Values.Text = "Refresh DPI";
            // 
            // lblDpiPrimary
            // 
            this.lblDpiPrimary.Location = new System.Drawing.Point(15, 20);
            this.lblDpiPrimary.Name = "lblDpiPrimary";
            this.lblDpiPrimary.Size = new System.Drawing.Size(400, 20);
            this.lblDpiPrimary.TabIndex = 1;
            this.lblDpiPrimary.Values.Text = "Primary Monitor DPI: 1.00x (1.00x, 1.00y)";
            // 
            // lblDpiPerMonitor
            // 
            this.lblDpiPerMonitor.Location = new System.Drawing.Point(15, 50);
            this.lblDpiPerMonitor.Name = "lblDpiPerMonitor";
            this.lblDpiPerMonitor.Size = new System.Drawing.Size(400, 20);
            this.lblDpiPerMonitor.TabIndex = 2;
            this.lblDpiPerMonitor.Values.Text = "Per-Monitor DPI: 1.00x (1.00x, 1.00y)";
            // 
            // lblDpiActual
            // 
            this.lblDpiActual.Location = new System.Drawing.Point(15, 80);
            this.lblDpiActual.Name = "lblDpiActual";
            this.lblDpiActual.Size = new System.Drawing.Size(400, 20);
            this.lblDpiActual.TabIndex = 3;
            this.lblDpiActual.Values.Text = "Windows API DPI: Not available";
            // 
            // lblCombinedPrimary
            // 
            this.lblCombinedPrimary.Location = new System.Drawing.Point(430, 20);
            this.lblCombinedPrimary.Name = "lblCombinedPrimary";
            this.lblCombinedPrimary.Size = new System.Drawing.Size(400, 20);
            this.lblCombinedPrimary.TabIndex = 4;
            this.lblCombinedPrimary.Values.Text = "Combined (Primary): 1.00x (1.00x, 1.00y)";
            // 
            // lblCombinedPerMonitor
            // 
            this.lblCombinedPerMonitor.Location = new System.Drawing.Point(430, 50);
            this.lblCombinedPerMonitor.Name = "lblCombinedPerMonitor";
            this.lblCombinedPerMonitor.Size = new System.Drawing.Size(400, 20);
            this.lblCombinedPerMonitor.TabIndex = 5;
            this.lblCombinedPerMonitor.Values.Text = "Combined (Per-Monitor): 1.00x (1.00x, 1.00y)";
            // 
            // lblDpiWarning
            // 
            this.lblDpiWarning.Location = new System.Drawing.Point(15, 110);
            this.lblDpiWarning.Name = "lblDpiWarning";
            this.lblDpiWarning.Size = new System.Drawing.Size(815, 20);
            this.lblDpiWarning.TabIndex = 6;
            this.lblDpiWarning.Values.Text = "✓ Single monitor or matching DPI";
            // 
            // lblScalingExample
            // 
            this.lblScalingExample.Location = new System.Drawing.Point(15, 140);
            this.lblScalingExample.Name = "lblScalingExample";
            this.lblScalingExample.Size = new System.Drawing.Size(815, 40);
            this.lblScalingExample.TabIndex = 7;
            this.lblScalingExample.Values.Text = "Scaling Example (base=100px): DPI only=100px, DPI+Touchscreen=100px, Per-Monitor=100px";
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(650, 20);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(335, 50);
            this.lblStatus.TabIndex = 14;
            this.lblStatus.Values.Text = "Status: Disabled";
            // 
            // TouchscreenHighDpiDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 1000);
            this.Controls.Add(this.grpControls);
            this.Controls.Add(this.grpSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MinimumSize = new System.Drawing.Size(1020, 1020);
            this.Name = "TouchscreenHighDpiDemo";
            this.Text = "Touchscreen + High DPI Scaling Demo (Issue #2844)";
            ((System.ComponentModel.ISupportInitialize)(this.grpControls)).EndInit();
            this.grpControls.Panel.ResumeLayout(false);
            this.grpControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpInputControls)).EndInit();
            this.grpInputControls.Panel.ResumeLayout(false);
            this.grpInputControls.Panel.PerformLayout();
            this.grpInputControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpButtons)).EndInit();
            this.grpButtons.Panel.ResumeLayout(false);
            this.grpButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpCheckboxes)).EndInit();
            this.grpCheckboxes.Panel.ResumeLayout(false);
            this.grpCheckboxes.Panel.PerformLayout();
            this.grpCheckboxes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpRadioButtons)).EndInit();
            this.grpRadioButtons.Panel.ResumeLayout(false);
            this.grpRadioButtons.Panel.PerformLayout();
            this.grpRadioButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpOtherControls)).EndInit();
            this.grpOtherControls.Panel.ResumeLayout(false);
            this.grpOtherControls.Panel.PerformLayout();
            this.grpOtherControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpAdvancedControls)).EndInit();
            this.grpAdvancedControls.Panel.ResumeLayout(false);
            this.grpAdvancedControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpSettings)).EndInit();
            this.grpSettings.Panel.ResumeLayout(false);
            this.grpSettings.Panel.PerformLayout();
            this.grpSettings.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpDpiInfo)).EndInit();
            this.grpDpiInfo.Panel.ResumeLayout(false);
            this.grpDpiInfo.Panel.PerformLayout();
            this.grpDpiInfo.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonGroupBox grpControls;
        private Krypton.Toolkit.KryptonGroupBox grpAdvancedControls;
        private Krypton.Navigator.KryptonNavigator navigator;
        private Krypton.Workspace.KryptonWorkspace workspace;
        private Krypton.Toolkit.KryptonGroupBox grpInputControls;
        private Krypton.Toolkit.KryptonTextBox txtInput;
        private Krypton.Toolkit.KryptonNumericUpDown txtNumeric;
        private Krypton.Toolkit.KryptonComboBox cmbOptions;
        private Krypton.Toolkit.KryptonGroupBox grpButtons;
        private Krypton.Toolkit.KryptonButton btnStandard;
        private Krypton.Toolkit.KryptonButton btnPrimary;
        private Krypton.Toolkit.KryptonButton btnSuccess;
        private Krypton.Toolkit.KryptonGroupBox grpCheckboxes;
        private Krypton.Toolkit.KryptonCheckBox chkOption1;
        private Krypton.Toolkit.KryptonCheckBox chkOption2;
        private Krypton.Toolkit.KryptonCheckBox chkOption3;
        private Krypton.Toolkit.KryptonGroupBox grpRadioButtons;
        private Krypton.Toolkit.KryptonRadioButton radioOption1;
        private Krypton.Toolkit.KryptonRadioButton radioOption2;
        private Krypton.Toolkit.KryptonRadioButton radioOption3;
        private Krypton.Toolkit.KryptonGroupBox grpOtherControls;
        private Krypton.Toolkit.KryptonProgressBar progressBar;
        private Krypton.Toolkit.KryptonTrackBar trackBar;
        private Krypton.Toolkit.KryptonGroupBox grpSettings;
        private Krypton.Toolkit.KryptonCheckBox chkEnableTouchscreen;
        private Krypton.Toolkit.KryptonLabel lblScaleFactor;
        private Krypton.Toolkit.KryptonTrackBar trackScaleFactor;
        private Krypton.Toolkit.KryptonLabel lblScaleValue;
        private Krypton.Toolkit.KryptonButton btnResetScale;
        private Krypton.Toolkit.KryptonButton btnApplyPreset25;
        private Krypton.Toolkit.KryptonButton btnApplyPreset50;
        private Krypton.Toolkit.KryptonButton btnApplyPreset75;
        private Krypton.Toolkit.KryptonButton btnToggle;
        private Krypton.Toolkit.KryptonLabel lblStatus;
        private Krypton.Toolkit.KryptonCheckBox chkEnableFontScaling;
        private Krypton.Toolkit.KryptonLabel lblFontScaleFactor;
        private Krypton.Toolkit.KryptonTrackBar trackFontScaleFactor;
        private Krypton.Toolkit.KryptonLabel lblFontScaleValue;
        private Krypton.Toolkit.KryptonGroupBox grpDpiInfo;
        private Krypton.Toolkit.KryptonButton btnRefreshDpi;
        private Krypton.Toolkit.KryptonLabel lblDpiPrimary;
        private Krypton.Toolkit.KryptonLabel lblDpiPerMonitor;
        private Krypton.Toolkit.KryptonLabel lblDpiActual;
        private Krypton.Toolkit.KryptonLabel lblCombinedPrimary;
        private Krypton.Toolkit.KryptonLabel lblCombinedPerMonitor;
        private Krypton.Toolkit.KryptonLabel lblDpiWarning;
        private Krypton.Toolkit.KryptonLabel lblScalingExample;
    }
}
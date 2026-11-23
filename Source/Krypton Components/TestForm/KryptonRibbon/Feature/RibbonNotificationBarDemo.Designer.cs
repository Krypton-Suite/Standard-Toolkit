namespace TestForm
{
	partial class RibbonNotificationBarDemo
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
			this.kryptonRibbon = new Krypton.Ribbon.KryptonRibbon();
			this.kryptonPanel = new Krypton.Toolkit.KryptonPanel();
			this.kryptonGroupBoxCustomization = new Krypton.Toolkit.KryptonGroupBox();
			this.numHeight = new Krypton.Toolkit.KryptonNumericUpDown();
			this.kryptonLabel8 = new Krypton.Toolkit.KryptonLabel();
			this.numAutoDismiss = new Krypton.Toolkit.KryptonNumericUpDown();
			this.kryptonLabel7 = new Krypton.Toolkit.KryptonLabel();
			this.txtButtonTexts = new Krypton.Toolkit.KryptonTextBox();
			this.kryptonLabel6 = new Krypton.Toolkit.KryptonLabel();
			this.chkShowActionButtons = new Krypton.Toolkit.KryptonCheckBox();
			this.chkShowCloseButton = new Krypton.Toolkit.KryptonCheckBox();
			this.chkShowIcon = new Krypton.Toolkit.KryptonCheckBox();
			this.txtTitleText = new Krypton.Toolkit.KryptonTextBox();
			this.kryptonLabel5 = new Krypton.Toolkit.KryptonLabel();
			this.txtMessageText = new Krypton.Toolkit.KryptonTextBox();
			this.kryptonLabel4 = new Krypton.Toolkit.KryptonLabel();
			this.cmbNotificationType = new Krypton.Toolkit.KryptonComboBox();
			this.kryptonLabel3 = new Krypton.Toolkit.KryptonLabel();
			this.btnApplyCustomization = new Krypton.Toolkit.KryptonButton();
			this.kryptonGroupBoxAdvanced = new Krypton.Toolkit.KryptonGroupBox();
			this.btnNotificationQueue = new Krypton.Toolkit.KryptonButton();
			this.btnDynamicUpdate = new Krypton.Toolkit.KryptonButton();
			this.btnProgressNotification = new Krypton.Toolkit.KryptonButton();
			this.kryptonGroupBoxCustom = new Krypton.Toolkit.KryptonGroupBox();
			this.btnCustomNoButtons = new Krypton.Toolkit.KryptonButton();
			this.btnCustomColors = new Krypton.Toolkit.KryptonButton();
			this.kryptonGroupBoxSuccess = new Krypton.Toolkit.KryptonGroupBox();
			this.btnSuccessAutoDismiss = new Krypton.Toolkit.KryptonButton();
			this.btnSuccessBasic = new Krypton.Toolkit.KryptonButton();
			this.kryptonGroupBoxError = new Krypton.Toolkit.KryptonGroupBox();
			this.btnErrorMultipleActions = new Krypton.Toolkit.KryptonButton();
			this.btnErrorBasic = new Krypton.Toolkit.KryptonButton();
			this.kryptonGroupBoxWarning = new Krypton.Toolkit.KryptonGroupBox();
			this.btnWarningAutoDismiss = new Krypton.Toolkit.KryptonButton();
			this.btnWarningOfficeStyle = new Krypton.Toolkit.KryptonButton();
			this.kryptonGroupBoxInformation = new Krypton.Toolkit.KryptonGroupBox();
			this.btnInfoWithTitle = new Krypton.Toolkit.KryptonButton();
			this.btnInfoBasic = new Krypton.Toolkit.KryptonButton();
			this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
			this.lblStatus = new Krypton.Toolkit.KryptonLabel();
			this.btnHideNotification = new Krypton.Toolkit.KryptonButton();
			((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).BeginInit();
			this.kryptonPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxCustomization)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxCustomization.Panel)).BeginInit();
			this.kryptonGroupBoxCustomization.Panel.SuspendLayout();
			this.kryptonGroupBoxCustomization.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cmbNotificationType)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxAdvanced)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxAdvanced.Panel)).BeginInit();
			this.kryptonGroupBoxAdvanced.Panel.SuspendLayout();
			this.kryptonGroupBoxAdvanced.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxCustom)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxCustom.Panel)).BeginInit();
			this.kryptonGroupBoxCustom.Panel.SuspendLayout();
			this.kryptonGroupBoxCustom.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxSuccess)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxSuccess.Panel)).BeginInit();
			this.kryptonGroupBoxSuccess.Panel.SuspendLayout();
			this.kryptonGroupBoxSuccess.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxError)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxError.Panel)).BeginInit();
			this.kryptonGroupBoxError.Panel.SuspendLayout();
			this.kryptonGroupBoxError.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxWarning)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxWarning.Panel)).BeginInit();
			this.kryptonGroupBoxWarning.Panel.SuspendLayout();
			this.kryptonGroupBoxWarning.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxInformation)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxInformation.Panel)).BeginInit();
			this.kryptonGroupBoxInformation.Panel.SuspendLayout();
			this.kryptonGroupBoxInformation.SuspendLayout();
			this.SuspendLayout();
			// 
			// kryptonRibbon
			// 
			this.kryptonRibbon.Name = "kryptonRibbon";
			this.kryptonRibbon.Size = new System.Drawing.Size(1200, 116);
			// 
			// kryptonPanel
			// 
			this.kryptonPanel.Controls.Add(this.btnHideNotification);
			this.kryptonPanel.Controls.Add(this.lblStatus);
			this.kryptonPanel.Controls.Add(this.kryptonLabel1);
			this.kryptonPanel.Controls.Add(this.kryptonGroupBoxCustomization);
			this.kryptonPanel.Controls.Add(this.kryptonGroupBoxAdvanced);
			this.kryptonPanel.Controls.Add(this.kryptonGroupBoxCustom);
			this.kryptonPanel.Controls.Add(this.kryptonGroupBoxSuccess);
			this.kryptonPanel.Controls.Add(this.kryptonGroupBoxError);
			this.kryptonPanel.Controls.Add(this.kryptonGroupBoxWarning);
			this.kryptonPanel.Controls.Add(this.kryptonGroupBoxInformation);
			this.kryptonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.kryptonPanel.Location = new System.Drawing.Point(0, 116);
			this.kryptonPanel.Name = "kryptonPanel";
			this.kryptonPanel.Padding = new System.Windows.Forms.Padding(10);
			this.kryptonPanel.Size = new System.Drawing.Size(1200, 634);
			this.kryptonPanel.TabIndex = 1;
			// 
			// kryptonGroupBoxCustomization
			// 
			this.kryptonGroupBoxCustomization.GroupBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
			this.kryptonGroupBoxCustomization.GroupBorderStyle = Krypton.Toolkit.PaletteBorderStyle.ButtonStandalone;
			this.kryptonGroupBoxCustomization.Location = new System.Drawing.Point(800, 13);
			this.kryptonGroupBoxCustomization.Name = "kryptonGroupBoxCustomization";
			// 
			// kryptonGroupBoxCustomization.Panel
			// 
			this.kryptonGroupBoxCustomization.Panel.Controls.Add(this.numHeight);
			this.kryptonGroupBoxCustomization.Panel.Controls.Add(this.kryptonLabel8);
			this.kryptonGroupBoxCustomization.Panel.Controls.Add(this.numAutoDismiss);
			this.kryptonGroupBoxCustomization.Panel.Controls.Add(this.kryptonLabel7);
			this.kryptonGroupBoxCustomization.Panel.Controls.Add(this.txtButtonTexts);
			this.kryptonGroupBoxCustomization.Panel.Controls.Add(this.kryptonLabel6);
			this.kryptonGroupBoxCustomization.Panel.Controls.Add(this.chkShowActionButtons);
			this.kryptonGroupBoxCustomization.Panel.Controls.Add(this.chkShowCloseButton);
			this.kryptonGroupBoxCustomization.Panel.Controls.Add(this.chkShowIcon);
			this.kryptonGroupBoxCustomization.Panel.Controls.Add(this.txtTitleText);
			this.kryptonGroupBoxCustomization.Panel.Controls.Add(this.kryptonLabel5);
			this.kryptonGroupBoxCustomization.Panel.Controls.Add(this.txtMessageText);
			this.kryptonGroupBoxCustomization.Panel.Controls.Add(this.kryptonLabel4);
			this.kryptonGroupBoxCustomization.Panel.Controls.Add(this.cmbNotificationType);
			this.kryptonGroupBoxCustomization.Panel.Controls.Add(this.kryptonLabel3);
			this.kryptonGroupBoxCustomization.Panel.Controls.Add(this.btnApplyCustomization);
			this.kryptonGroupBoxCustomization.Size = new System.Drawing.Size(380, 580);
			this.kryptonGroupBoxCustomization.TabIndex = 6;
			this.kryptonGroupBoxCustomization.Values.Heading = "Customization";
			// 
			// numHeight
			// 
			this.numHeight.Location = new System.Drawing.Point(150, 320);
			this.numHeight.Maximum = new decimal(new int[] {
			200,
			0,
			0,
			0});
			this.numHeight.Name = "numHeight";
			this.numHeight.Size = new System.Drawing.Size(200, 22);
			this.numHeight.TabIndex = 15;
			this.numHeight.Value = new decimal(new int[] {
			0,
			0,
			0,
			0});
			// 
			// kryptonLabel8
			// 
			this.kryptonLabel8.Location = new System.Drawing.Point(10, 320);
			this.kryptonLabel8.Name = "kryptonLabel8";
			this.kryptonLabel8.Size = new System.Drawing.Size(134, 20);
			this.kryptonLabel8.TabIndex = 14;
			this.kryptonLabel8.Values.Text = "Height (0=auto):";
			// 
			// numAutoDismiss
			// 
			this.numAutoDismiss.Location = new System.Drawing.Point(150, 290);
			this.numAutoDismiss.Maximum = new decimal(new int[] {
			60,
			0,
			0,
			0});
			this.numAutoDismiss.Name = "numAutoDismiss";
			this.numAutoDismiss.Size = new System.Drawing.Size(200, 22);
			this.numAutoDismiss.TabIndex = 13;
			// 
			// kryptonLabel7
			// 
			this.kryptonLabel7.Location = new System.Drawing.Point(10, 290);
			this.kryptonLabel7.Name = "kryptonLabel7";
			this.kryptonLabel7.Size = new System.Drawing.Size(134, 20);
			this.kryptonLabel7.TabIndex = 12;
			this.kryptonLabel7.Values.Text = "Auto-dismiss (sec):";
			// 
			// txtButtonTexts
			// 
			this.txtButtonTexts.Location = new System.Drawing.Point(150, 250);
			this.txtButtonTexts.Name = "txtButtonTexts";
			this.txtButtonTexts.Size = new System.Drawing.Size(200, 23);
			this.txtButtonTexts.TabIndex = 11;
			this.txtButtonTexts.Text = "OK, Cancel";
			// 
			// kryptonLabel6
			// 
			this.kryptonLabel6.Location = new System.Drawing.Point(10, 250);
			this.kryptonLabel6.Name = "kryptonLabel6";
			this.kryptonLabel6.Size = new System.Drawing.Size(134, 20);
			this.kryptonLabel6.TabIndex = 10;
			this.kryptonLabel6.Values.Text = "Button Texts (comma):";
			// 
			// chkShowActionButtons
			// 
			this.chkShowActionButtons.Checked = true;
			this.chkShowActionButtons.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkShowActionButtons.Location = new System.Drawing.Point(150, 220);
			this.chkShowActionButtons.Name = "chkShowActionButtons";
			this.chkShowActionButtons.Size = new System.Drawing.Size(150, 20);
			this.chkShowActionButtons.TabIndex = 9;
			this.chkShowActionButtons.Values.Text = "Show Action Buttons";
			// 
			// chkShowCloseButton
			// 
			this.chkShowCloseButton.Checked = true;
			this.chkShowCloseButton.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkShowCloseButton.Location = new System.Drawing.Point(150, 190);
			this.chkShowCloseButton.Name = "chkShowCloseButton";
			this.chkShowCloseButton.Size = new System.Drawing.Size(150, 20);
			this.chkShowCloseButton.TabIndex = 8;
			this.chkShowCloseButton.Values.Text = "Show Close Button";
			// 
			// chkShowIcon
			// 
			this.chkShowIcon.Checked = true;
			this.chkShowIcon.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkShowIcon.Location = new System.Drawing.Point(150, 160);
			this.chkShowIcon.Name = "chkShowIcon";
			this.chkShowIcon.Size = new System.Drawing.Size(150, 20);
			this.chkShowIcon.TabIndex = 7;
			this.chkShowIcon.Values.Text = "Show Icon";
			// 
			// txtTitleText
			// 
			this.txtTitleText.Location = new System.Drawing.Point(150, 100);
			this.txtTitleText.Name = "txtTitleText";
			this.txtTitleText.Size = new System.Drawing.Size(200, 23);
			this.txtTitleText.TabIndex = 6;
			// 
			// kryptonLabel5
			// 
			this.kryptonLabel5.Location = new System.Drawing.Point(10, 100);
			this.kryptonLabel5.Name = "kryptonLabel5";
			this.kryptonLabel5.Size = new System.Drawing.Size(134, 20);
			this.kryptonLabel5.TabIndex = 5;
			this.kryptonLabel5.Values.Text = "Title Text:";
			// 
			// txtMessageText
			// 
			this.txtMessageText.Location = new System.Drawing.Point(150, 70);
			this.txtMessageText.Name = "txtMessageText";
			this.txtMessageText.Size = new System.Drawing.Size(200, 23);
			this.txtMessageText.TabIndex = 4;
			this.txtMessageText.Text = "This is a custom notification message.";
			// 
			// kryptonLabel4
			// 
			this.kryptonLabel4.Location = new System.Drawing.Point(10, 70);
			this.kryptonLabel4.Name = "kryptonLabel4";
			this.kryptonLabel4.Size = new System.Drawing.Size(134, 20);
			this.kryptonLabel4.TabIndex = 3;
			this.kryptonLabel4.Values.Text = "Message Text:";
			// 
			// cmbNotificationType
			// 
			this.cmbNotificationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbNotificationType.DropDownWidth = 200;
			this.cmbNotificationType.Items.AddRange(new object[] {
			Krypton.Ribbon.RibbonNotificationBarType.Information,
			Krypton.Ribbon.RibbonNotificationBarType.Warning,
			Krypton.Ribbon.RibbonNotificationBarType.Error,
			Krypton.Ribbon.RibbonNotificationBarType.Success,
			Krypton.Ribbon.RibbonNotificationBarType.Custom});
			this.cmbNotificationType.Location = new System.Drawing.Point(150, 40);
			this.cmbNotificationType.Name = "cmbNotificationType";
			this.cmbNotificationType.Size = new System.Drawing.Size(200, 21);
			this.cmbNotificationType.TabIndex = 2;
			// 
			// kryptonLabel3
			// 
			this.kryptonLabel3.Location = new System.Drawing.Point(10, 40);
			this.kryptonLabel3.Name = "kryptonLabel3";
			this.kryptonLabel3.Size = new System.Drawing.Size(134, 20);
			this.kryptonLabel3.TabIndex = 1;
			this.kryptonLabel3.Values.Text = "Notification Type:";
			// 
			// btnApplyCustomization
			// 
			this.btnApplyCustomization.Location = new System.Drawing.Point(10, 360);
			this.btnApplyCustomization.Name = "btnApplyCustomization";
			this.btnApplyCustomization.Size = new System.Drawing.Size(340, 25);
			this.btnApplyCustomization.TabIndex = 0;
			this.btnApplyCustomization.Values.Text = "Apply & Show Notification";
			// 
			// kryptonGroupBoxAdvanced
			// 
			this.kryptonGroupBoxAdvanced.GroupBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
			this.kryptonGroupBoxAdvanced.GroupBorderStyle = Krypton.Toolkit.PaletteBorderStyle.ButtonStandalone;
			this.kryptonGroupBoxAdvanced.Location = new System.Drawing.Point(400, 400);
			this.kryptonGroupBoxAdvanced.Name = "kryptonGroupBoxAdvanced";
			// 
			// kryptonGroupBoxAdvanced.Panel
			// 
			this.kryptonGroupBoxAdvanced.Panel.Controls.Add(this.btnNotificationQueue);
			this.kryptonGroupBoxAdvanced.Panel.Controls.Add(this.btnDynamicUpdate);
			this.kryptonGroupBoxAdvanced.Panel.Controls.Add(this.btnProgressNotification);
			this.kryptonGroupBoxAdvanced.Size = new System.Drawing.Size(380, 193);
			this.kryptonGroupBoxAdvanced.TabIndex = 5;
			this.kryptonGroupBoxAdvanced.Values.Heading = "Advanced Examples";
			// 
			// btnNotificationQueue
			// 
			this.btnNotificationQueue.Location = new System.Drawing.Point(10, 100);
			this.btnNotificationQueue.Name = "btnNotificationQueue";
			this.btnNotificationQueue.Size = new System.Drawing.Size(360, 35);
			this.btnNotificationQueue.TabIndex = 2;
			this.btnNotificationQueue.Values.Text = "Notification Queue";
			// 
			// btnDynamicUpdate
			// 
			this.btnDynamicUpdate.Location = new System.Drawing.Point(10, 55);
			this.btnDynamicUpdate.Name = "btnDynamicUpdate";
			this.btnDynamicUpdate.Size = new System.Drawing.Size(360, 35);
			this.btnDynamicUpdate.TabIndex = 1;
			this.btnDynamicUpdate.Values.Text = "Dynamic Update";
			// 
			// btnProgressNotification
			// 
			this.btnProgressNotification.Location = new System.Drawing.Point(10, 10);
			this.btnProgressNotification.Name = "btnProgressNotification";
			this.btnProgressNotification.Size = new System.Drawing.Size(360, 35);
			this.btnProgressNotification.TabIndex = 0;
			this.btnProgressNotification.Values.Text = "Progress Notification";
			// 
			// kryptonGroupBoxCustom
			// 
			this.kryptonGroupBoxCustom.GroupBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
			this.kryptonGroupBoxCustom.GroupBorderStyle = Krypton.Toolkit.PaletteBorderStyle.ButtonStandalone;
			this.kryptonGroupBoxCustom.Location = new System.Drawing.Point(400, 200);
			this.kryptonGroupBoxCustom.Name = "kryptonGroupBoxCustom";
			// 
			// kryptonGroupBoxCustom.Panel
			// 
			this.kryptonGroupBoxCustom.Panel.Controls.Add(this.btnCustomNoButtons);
			this.kryptonGroupBoxCustom.Panel.Controls.Add(this.btnCustomColors);
			this.kryptonGroupBoxCustom.Size = new System.Drawing.Size(380, 193);
			this.kryptonGroupBoxCustom.TabIndex = 4;
			this.kryptonGroupBoxCustom.Values.Heading = "Custom Examples";
			// 
			// btnCustomNoButtons
			// 
			this.btnCustomNoButtons.Location = new System.Drawing.Point(10, 55);
			this.btnCustomNoButtons.Name = "btnCustomNoButtons";
			this.btnCustomNoButtons.Size = new System.Drawing.Size(360, 35);
			this.btnCustomNoButtons.TabIndex = 1;
			this.btnCustomNoButtons.Values.Text = "No Buttons (Auto-dismiss)";
			// 
			// btnCustomColors
			// 
			this.btnCustomColors.Location = new System.Drawing.Point(10, 10);
			this.btnCustomColors.Name = "btnCustomColors";
			this.btnCustomColors.Size = new System.Drawing.Size(360, 35);
			this.btnCustomColors.TabIndex = 0;
			this.btnCustomColors.Values.Text = "Custom Colors";
			// 
			// kryptonGroupBoxSuccess
			// 
			this.kryptonGroupBoxSuccess.GroupBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
			this.kryptonGroupBoxSuccess.GroupBorderStyle = Krypton.Toolkit.PaletteBorderStyle.ButtonStandalone;
			this.kryptonGroupBoxSuccess.Location = new System.Drawing.Point(10, 400);
			this.kryptonGroupBoxSuccess.Name = "kryptonGroupBoxSuccess";
			// 
			// kryptonGroupBoxSuccess.Panel
			// 
			this.kryptonGroupBoxSuccess.Panel.Controls.Add(this.btnSuccessAutoDismiss);
			this.kryptonGroupBoxSuccess.Panel.Controls.Add(this.btnSuccessBasic);
			this.kryptonGroupBoxSuccess.Size = new System.Drawing.Size(380, 193);
			this.kryptonGroupBoxSuccess.TabIndex = 3;
			this.kryptonGroupBoxSuccess.Values.Heading = "Success Examples";
			// 
			// btnSuccessAutoDismiss
			// 
			this.btnSuccessAutoDismiss.Location = new System.Drawing.Point(10, 55);
			this.btnSuccessAutoDismiss.Name = "btnSuccessAutoDismiss";
			this.btnSuccessAutoDismiss.Size = new System.Drawing.Size(360, 35);
			this.btnSuccessAutoDismiss.TabIndex = 1;
			this.btnSuccessAutoDismiss.Values.Text = "Success with Auto-dismiss";
			// 
			// btnSuccessBasic
			// 
			this.btnSuccessBasic.Location = new System.Drawing.Point(10, 10);
			this.btnSuccessBasic.Name = "btnSuccessBasic";
			this.btnSuccessBasic.Size = new System.Drawing.Size(360, 35);
			this.btnSuccessBasic.TabIndex = 0;
			this.btnSuccessBasic.Values.Text = "Basic Success";
			// 
			// kryptonGroupBoxError
			// 
			this.kryptonGroupBoxError.GroupBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
			this.kryptonGroupBoxError.GroupBorderStyle = Krypton.Toolkit.PaletteBorderStyle.ButtonStandalone;
			this.kryptonGroupBoxError.Location = new System.Drawing.Point(400, 10);
			this.kryptonGroupBoxError.Name = "kryptonGroupBoxError";
			// 
			// kryptonGroupBoxError.Panel
			// 
			this.kryptonGroupBoxError.Panel.Controls.Add(this.btnErrorMultipleActions);
			this.kryptonGroupBoxError.Panel.Controls.Add(this.btnErrorBasic);
			this.kryptonGroupBoxError.Size = new System.Drawing.Size(380, 183);
			this.kryptonGroupBoxError.TabIndex = 2;
			this.kryptonGroupBoxError.Values.Heading = "Error Examples";
			// 
			// btnErrorMultipleActions
			// 
			this.btnErrorMultipleActions.Location = new System.Drawing.Point(10, 55);
			this.btnErrorMultipleActions.Name = "btnErrorMultipleActions";
			this.btnErrorMultipleActions.Size = new System.Drawing.Size(360, 35);
			this.btnErrorMultipleActions.TabIndex = 1;
			this.btnErrorMultipleActions.Values.Text = "Error with Multiple Actions";
			// 
			// btnErrorBasic
			// 
			this.btnErrorBasic.Location = new System.Drawing.Point(10, 10);
			this.btnErrorBasic.Name = "btnErrorBasic";
			this.btnErrorBasic.Size = new System.Drawing.Size(360, 35);
			this.btnErrorBasic.TabIndex = 0;
			this.btnErrorBasic.Values.Text = "Basic Error";
			// 
			// kryptonGroupBoxWarning
			// 
			this.kryptonGroupBoxWarning.GroupBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
			this.kryptonGroupBoxWarning.GroupBorderStyle = Krypton.Toolkit.PaletteBorderStyle.ButtonStandalone;
			this.kryptonGroupBoxWarning.Location = new System.Drawing.Point(10, 200);
			this.kryptonGroupBoxWarning.Name = "kryptonGroupBoxWarning";
			// 
			// kryptonGroupBoxWarning.Panel
			// 
			this.kryptonGroupBoxWarning.Panel.Controls.Add(this.btnWarningAutoDismiss);
			this.kryptonGroupBoxWarning.Panel.Controls.Add(this.btnWarningOfficeStyle);
			this.kryptonGroupBoxWarning.Size = new System.Drawing.Size(380, 193);
			this.kryptonGroupBoxWarning.TabIndex = 1;
			this.kryptonGroupBoxWarning.Values.Heading = "Warning Examples";
			// 
			// btnWarningAutoDismiss
			// 
			this.btnWarningAutoDismiss.Location = new System.Drawing.Point(10, 55);
			this.btnWarningAutoDismiss.Name = "btnWarningAutoDismiss";
			this.btnWarningAutoDismiss.Size = new System.Drawing.Size(360, 35);
			this.btnWarningAutoDismiss.TabIndex = 1;
			this.btnWarningAutoDismiss.Values.Text = "Warning with Auto-dismiss";
			// 
			// btnWarningOfficeStyle
			// 
			this.btnWarningOfficeStyle.Location = new System.Drawing.Point(10, 10);
			this.btnWarningOfficeStyle.Name = "btnWarningOfficeStyle";
			this.btnWarningOfficeStyle.Size = new System.Drawing.Size(360, 35);
			this.btnWarningOfficeStyle.TabIndex = 0;
			this.btnWarningOfficeStyle.Values.Text = "Office-Style Update Warning";
			// 
			// kryptonGroupBoxInformation
			// 
			this.kryptonGroupBoxInformation.GroupBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
			this.kryptonGroupBoxInformation.GroupBorderStyle = Krypton.Toolkit.PaletteBorderStyle.ButtonStandalone;
			this.kryptonGroupBoxInformation.Location = new System.Drawing.Point(10, 10);
			this.kryptonGroupBoxInformation.Name = "kryptonGroupBoxInformation";
			// 
			// kryptonGroupBoxInformation.Panel
			// 
			this.kryptonGroupBoxInformation.Panel.Controls.Add(this.btnInfoWithTitle);
			this.kryptonGroupBoxInformation.Panel.Controls.Add(this.btnInfoBasic);
			this.kryptonGroupBoxInformation.Size = new System.Drawing.Size(380, 183);
			this.kryptonGroupBoxInformation.TabIndex = 0;
			this.kryptonGroupBoxInformation.Values.Heading = "Information Examples";
			// 
			// btnInfoWithTitle
			// 
			this.btnInfoWithTitle.Location = new System.Drawing.Point(10, 55);
			this.btnInfoWithTitle.Name = "btnInfoWithTitle";
			this.btnInfoWithTitle.Size = new System.Drawing.Size(360, 35);
			this.btnInfoWithTitle.TabIndex = 1;
			this.btnInfoWithTitle.Values.Text = "Information with Title";
			// 
			// btnInfoBasic
			// 
			this.btnInfoBasic.Location = new System.Drawing.Point(10, 10);
			this.btnInfoBasic.Name = "btnInfoBasic";
			this.btnInfoBasic.Size = new System.Drawing.Size(360, 35);
			this.btnInfoBasic.TabIndex = 0;
			this.btnInfoBasic.Values.Text = "Basic Information";
			// 
			// kryptonLabel1
			// 
			this.kryptonLabel1.Location = new System.Drawing.Point(10, 600);
			this.kryptonLabel1.Name = "kryptonLabel1";
			this.kryptonLabel1.Size = new System.Drawing.Size(100, 20);
			this.kryptonLabel1.TabIndex = 7;
			this.kryptonLabel1.Values.Text = "Status:";
			// 
			// lblStatus
			// 
			this.lblStatus.Location = new System.Drawing.Point(116, 600);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(500, 20);
			this.lblStatus.TabIndex = 8;
			this.lblStatus.Values.Text = "Ready. Click a button to show a notification.";
			// 
			// btnHideNotification
			// 
			this.btnHideNotification.Location = new System.Drawing.Point(1100, 600);
			this.btnHideNotification.Name = "btnHideNotification";
			this.btnHideNotification.Size = new System.Drawing.Size(90, 25);
			this.btnHideNotification.TabIndex = 9;
			this.btnHideNotification.Values.Text = "Hide";
			// 
			// RibbonNotificationBarDemo
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1200, 750);
			this.Controls.Add(this.kryptonPanel);
			this.Controls.Add(this.kryptonRibbon);
			this.Name = "RibbonNotificationBarDemo";
			this.Text = "Ribbon Notification Bar Demo";
			((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).EndInit();
			this.kryptonPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxCustomization.Panel)).EndInit();
            this.kryptonGroupBoxCustomization.Panel.ResumeLayout(false);
            this.kryptonGroupBoxCustomization.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxCustomization)).EndInit();
            this.kryptonGroupBoxCustomization.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbNotificationType)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxAdvanced.Panel)).EndInit();
			this.kryptonGroupBoxAdvanced.Panel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxAdvanced)).EndInit();
			this.kryptonGroupBoxAdvanced.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxCustom.Panel)).EndInit();
			this.kryptonGroupBoxCustom.Panel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxCustom)).EndInit();
			this.kryptonGroupBoxCustom.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxSuccess.Panel)).EndInit();
			this.kryptonGroupBoxSuccess.Panel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxSuccess)).EndInit();
			this.kryptonGroupBoxSuccess.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxError.Panel)).EndInit();
			this.kryptonGroupBoxError.Panel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxError)).EndInit();
			this.kryptonGroupBoxError.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxWarning.Panel)).EndInit();
			this.kryptonGroupBoxWarning.Panel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxWarning)).EndInit();
			this.kryptonGroupBoxWarning.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxInformation.Panel)).EndInit();
			this.kryptonGroupBoxInformation.Panel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxInformation)).EndInit();
			this.kryptonGroupBoxInformation.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Krypton.Ribbon.KryptonRibbon kryptonRibbon;
		private Krypton.Toolkit.KryptonPanel kryptonPanel;
		private Krypton.Toolkit.KryptonGroupBox kryptonGroupBoxInformation;
		private Krypton.Toolkit.KryptonButton btnInfoBasic;
		private Krypton.Toolkit.KryptonButton btnInfoWithTitle;
		private Krypton.Toolkit.KryptonGroupBox kryptonGroupBoxWarning;
		private Krypton.Toolkit.KryptonButton btnWarningOfficeStyle;
		private Krypton.Toolkit.KryptonButton btnWarningAutoDismiss;
		private Krypton.Toolkit.KryptonGroupBox kryptonGroupBoxError;
		private Krypton.Toolkit.KryptonButton btnErrorBasic;
		private Krypton.Toolkit.KryptonButton btnErrorMultipleActions;
		private Krypton.Toolkit.KryptonGroupBox kryptonGroupBoxSuccess;
		private Krypton.Toolkit.KryptonButton btnSuccessBasic;
		private Krypton.Toolkit.KryptonButton btnSuccessAutoDismiss;
		private Krypton.Toolkit.KryptonGroupBox kryptonGroupBoxCustom;
		private Krypton.Toolkit.KryptonButton btnCustomColors;
		private Krypton.Toolkit.KryptonButton btnCustomNoButtons;
		private Krypton.Toolkit.KryptonGroupBox kryptonGroupBoxAdvanced;
		private Krypton.Toolkit.KryptonButton btnProgressNotification;
		private Krypton.Toolkit.KryptonButton btnDynamicUpdate;
		private Krypton.Toolkit.KryptonButton btnNotificationQueue;
		private Krypton.Toolkit.KryptonGroupBox kryptonGroupBoxCustomization;
		private Krypton.Toolkit.KryptonButton btnApplyCustomization;
		private Krypton.Toolkit.KryptonComboBox cmbNotificationType;
		private Krypton.Toolkit.KryptonLabel kryptonLabel3;
		private Krypton.Toolkit.KryptonTextBox txtMessageText;
		private Krypton.Toolkit.KryptonLabel kryptonLabel4;
		private Krypton.Toolkit.KryptonTextBox txtTitleText;
		private Krypton.Toolkit.KryptonLabel kryptonLabel5;
		private Krypton.Toolkit.KryptonCheckBox chkShowIcon;
		private Krypton.Toolkit.KryptonCheckBox chkShowCloseButton;
		private Krypton.Toolkit.KryptonCheckBox chkShowActionButtons;
		private Krypton.Toolkit.KryptonTextBox txtButtonTexts;
		private Krypton.Toolkit.KryptonLabel kryptonLabel6;
		private Krypton.Toolkit.KryptonNumericUpDown numAutoDismiss;
		private Krypton.Toolkit.KryptonLabel kryptonLabel7;
		private Krypton.Toolkit.KryptonNumericUpDown numHeight;
		private Krypton.Toolkit.KryptonLabel kryptonLabel8;
		private Krypton.Toolkit.KryptonLabel kryptonLabel1;
		private Krypton.Toolkit.KryptonLabel lblStatus;
		private Krypton.Toolkit.KryptonButton btnHideNotification;
	}
}
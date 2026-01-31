namespace TestForm
{
    partial class ControlboxTouchscreenDemo
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
            this.grpMain = new Krypton.Toolkit.KryptonGroupBox();
            this.grpInfo = new Krypton.Toolkit.KryptonGroupBox();
            this.lblInfo = new Krypton.Toolkit.KryptonLabel();
            this.grpDemo = new Krypton.Toolkit.KryptonGroupBox();
            this.btnShowSystemMenu = new Krypton.Toolkit.KryptonButton();
            this.btnShowContextMenu = new Krypton.Toolkit.KryptonButton();
            this.grpSettings = new Krypton.Toolkit.KryptonGroupBox();
            this.btnToggle = new Krypton.Toolkit.KryptonButton();
            this.btnApplyPreset75 = new Krypton.Toolkit.KryptonButton();
            this.btnApplyPreset50 = new Krypton.Toolkit.KryptonButton();
            this.btnApplyPreset25 = new Krypton.Toolkit.KryptonButton();
            this.btnResetScale = new Krypton.Toolkit.KryptonButton();
            this.lblScaleValue = new Krypton.Toolkit.KryptonLabel();
            this.trackScaleFactor = new Krypton.Toolkit.KryptonTrackBar();
            this.lblScaleFactor = new Krypton.Toolkit.KryptonLabel();
            this.chkEnableTouchscreen = new Krypton.Toolkit.KryptonCheckBox();
            this.lblStatus = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.grpMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpMain.Panel)).BeginInit();
            this.grpMain.Panel.SuspendLayout();
            this.grpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpInfo.Panel)).BeginInit();
            this.grpInfo.Panel.SuspendLayout();
            this.grpInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpDemo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpDemo.Panel)).BeginInit();
            this.grpDemo.Panel.SuspendLayout();
            this.grpDemo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpSettings.Panel)).BeginInit();
            this.grpSettings.Panel.SuspendLayout();
            this.grpSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpMain
            // 
            this.grpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpMain.Location = new System.Drawing.Point(0, 0);
            this.grpMain.Name = "grpMain";
            this.grpMain.Size = new System.Drawing.Size(900, 600);
            this.grpMain.Panel.AutoScroll = true;
            this.grpMain.TabIndex = 0;
            this.grpMain.Values.Heading = "Controlbox & Context Menu Touchscreen Support Demo";
            // 
            // grpMain.Panel
            // 
            this.grpMain.Panel.Controls.Add(this.grpInfo);
            this.grpMain.Panel.Controls.Add(this.grpDemo);
            this.grpMain.Panel.Controls.Add(this.grpSettings);
            // 
            // grpInfo
            // 
            this.grpInfo.Location = new System.Drawing.Point(15, 15);
            this.grpInfo.Name = "grpInfo";
            this.grpInfo.Size = new System.Drawing.Size(870, 120);
            this.grpInfo.TabIndex = 0;
            this.grpInfo.Values.Heading = "Information";
            // 
            // grpInfo.Panel
            // 
            this.grpInfo.Panel.Controls.Add(this.lblInfo);
            // 
            // lblInfo
            // 
            this.lblInfo.Location = new System.Drawing.Point(15, 20);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(840, 80);
            this.lblInfo.TabIndex = 0;
            this.lblInfo.Values.Text = "Information will be displayed here";
            // 
            // grpDemo
            // 
            this.grpDemo.Location = new System.Drawing.Point(15, 150);
            this.grpDemo.Name = "grpDemo";
            this.grpDemo.Size = new System.Drawing.Size(870, 100);
            this.grpDemo.TabIndex = 1;
            this.grpDemo.Values.Heading = "Demo Controls";
            // 
            // grpDemo.Panel
            // 
            this.grpDemo.Panel.Controls.Add(this.btnShowSystemMenu);
            this.grpDemo.Panel.Controls.Add(this.btnShowContextMenu);
            // 
            // btnShowContextMenu
            // 
            this.btnShowContextMenu.Location = new System.Drawing.Point(15, 20);
            this.btnShowContextMenu.Name = "btnShowContextMenu";
            this.btnShowContextMenu.Size = new System.Drawing.Size(200, 50);
            this.btnShowContextMenu.TabIndex = 0;
            this.btnShowContextMenu.Values.Text = "Show Context Menu";
            // 
            // btnShowSystemMenu
            // 
            this.btnShowSystemMenu.Location = new System.Drawing.Point(230, 20);
            this.btnShowSystemMenu.Name = "btnShowSystemMenu";
            this.btnShowSystemMenu.Size = new System.Drawing.Size(200, 50);
            this.btnShowSystemMenu.TabIndex = 1;
            this.btnShowSystemMenu.Values.Text = "Show System Menu";
            // 
            // grpSettings
            // 
            this.grpSettings.Location = new System.Drawing.Point(15, 265);
            this.grpSettings.Name = "grpSettings";
            this.grpSettings.Size = new System.Drawing.Size(870, 300);
            this.grpSettings.TabIndex = 2;
            this.grpSettings.Values.Heading = "Touchscreen Support Settings";
            // 
            // grpSettings.Panel
            // 
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
            this.lblScaleFactor.Location = new System.Drawing.Point(15, 55);
            this.lblScaleFactor.Name = "lblScaleFactor";
            this.lblScaleFactor.Size = new System.Drawing.Size(200, 20);
            this.lblScaleFactor.TabIndex = 1;
            this.lblScaleFactor.Values.Text = "Scale Factor:";
            // 
            // trackScaleFactor
            // 
            this.trackScaleFactor.Location = new System.Drawing.Point(15, 80);
            this.trackScaleFactor.Maximum = 200;
            this.trackScaleFactor.Minimum = 100;
            this.trackScaleFactor.Name = "trackScaleFactor";
            this.trackScaleFactor.Size = new System.Drawing.Size(600, 45);
            this.trackScaleFactor.TabIndex = 2;
            this.trackScaleFactor.TickFrequency = 25;
            this.trackScaleFactor.Value = 125;
            // 
            // lblScaleValue
            // 
            this.lblScaleValue.Location = new System.Drawing.Point(630, 80);
            this.lblScaleValue.Name = "lblScaleValue";
            this.lblScaleValue.Size = new System.Drawing.Size(200, 20);
            this.lblScaleValue.TabIndex = 3;
            this.lblScaleValue.Values.Text = "1.25x (25.0% larger)";
            // 
            // btnResetScale
            // 
            this.btnResetScale.Location = new System.Drawing.Point(15, 140);
            this.btnResetScale.Name = "btnResetScale";
            this.btnResetScale.Size = new System.Drawing.Size(150, 35);
            this.btnResetScale.TabIndex = 4;
            this.btnResetScale.Values.Text = "Reset to Default";
            // 
            // btnApplyPreset25
            // 
            this.btnApplyPreset25.Location = new System.Drawing.Point(180, 140);
            this.btnApplyPreset25.Name = "btnApplyPreset25";
            this.btnApplyPreset25.Size = new System.Drawing.Size(150, 35);
            this.btnApplyPreset25.TabIndex = 5;
            this.btnApplyPreset25.Values.Text = "25% Larger";
            // 
            // btnApplyPreset50
            // 
            this.btnApplyPreset50.Location = new System.Drawing.Point(345, 140);
            this.btnApplyPreset50.Name = "btnApplyPreset50";
            this.btnApplyPreset50.Size = new System.Drawing.Size(150, 35);
            this.btnApplyPreset50.TabIndex = 6;
            this.btnApplyPreset50.Values.Text = "50% Larger";
            // 
            // btnApplyPreset75
            // 
            this.btnApplyPreset75.Location = new System.Drawing.Point(510, 140);
            this.btnApplyPreset75.Name = "btnApplyPreset75";
            this.btnApplyPreset75.Size = new System.Drawing.Size(150, 35);
            this.btnApplyPreset75.TabIndex = 7;
            this.btnApplyPreset75.Values.Text = "75% Larger";
            // 
            // btnToggle
            // 
            this.btnToggle.ButtonStyle = Krypton.Toolkit.ButtonStyle.Command;
            this.btnToggle.Location = new System.Drawing.Point(15, 190);
            this.btnToggle.Name = "btnToggle";
            this.btnToggle.Size = new System.Drawing.Size(200, 40);
            this.btnToggle.TabIndex = 8;
            this.btnToggle.Values.Text = "Toggle Touchscreen Support";
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(15, 250);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(840, 30);
            this.lblStatus.TabIndex = 9;
            this.lblStatus.Values.Text = "Status information will be displayed here";
            // 
            // ControlboxTouchscreenDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.grpMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.Name = "ControlboxTouchscreenDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Controlbox & Context Menu Touchscreen Support Demo (Issue #2925)";
            ((System.ComponentModel.ISupportInitialize)(this.grpMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpMain.Panel)).EndInit();
            this.grpMain.Panel.ResumeLayout(false);
            this.grpMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpInfo.Panel)).EndInit();
            this.grpInfo.Panel.ResumeLayout(false);
            this.grpInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpDemo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpDemo.Panel)).EndInit();
            this.grpDemo.Panel.ResumeLayout(false);
            this.grpDemo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpSettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpSettings.Panel)).EndInit();
            this.grpSettings.Panel.ResumeLayout(false);
            this.grpSettings.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonGroupBox grpMain;
        private Krypton.Toolkit.KryptonGroupBox grpInfo;
        private Krypton.Toolkit.KryptonLabel lblInfo;
        private Krypton.Toolkit.KryptonGroupBox grpDemo;
        private Krypton.Toolkit.KryptonButton btnShowContextMenu;
        private Krypton.Toolkit.KryptonButton btnShowSystemMenu;
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
    }
}

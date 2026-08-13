namespace TestForm
{
    partial class MessageBoxExtendedLifetimeDemo
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
            this.klblResult = new Krypton.Toolkit.KryptonLabel();
            this.flpPresets = new System.Windows.Forms.FlowLayoutPanel();
            this.kbtnShow = new Krypton.Toolkit.KryptonButton();
            this.kbtnFadeOnly = new Krypton.Toolkit.KryptonButton();
            this.kbtnTimeoutNoClose = new Krypton.Toolkit.KryptonButton();
            this.kbtnAutoCloseOk = new Krypton.Toolkit.KryptonButton();
            this.kbtnAutoCloseButton2 = new Krypton.Toolkit.KryptonButton();
            this.kbtnFadeAndTimeout = new Krypton.Toolkit.KryptonButton();
            this.kbtnRtlTimeout = new Krypton.Toolkit.KryptonButton();
            this.kbtnShowOverload = new Krypton.Toolkit.KryptonButton();
            this.kbtnShowAsync = new Krypton.Toolkit.KryptonButton();
            this.kchkRtl = new Krypton.Toolkit.KryptonCheckBox();
            this.kcmbButtons = new Krypton.Toolkit.KryptonComboBox();
            this.klblButtons = new Krypton.Toolkit.KryptonLabel();
            this.kcmbTimeOutResult = new Krypton.Toolkit.KryptonComboBox();
            this.klblTimeOutResult = new Krypton.Toolkit.KryptonLabel();
            this.kcmbTimeOutAction = new Krypton.Toolkit.KryptonComboBox();
            this.klblTimeOutAction = new Krypton.Toolkit.KryptonLabel();
            this.knudTimeOut = new Krypton.Toolkit.KryptonNumericUpDown();
            this.klblTimeOut = new Krypton.Toolkit.KryptonLabel();
            this.kcmbAutoClose = new Krypton.Toolkit.KryptonComboBox();
            this.klblAutoClose = new Krypton.Toolkit.KryptonLabel();
            this.kchkUseTimeOut = new Krypton.Toolkit.KryptonCheckBox();
            this.kcmbFadeSpeed = new Krypton.Toolkit.KryptonComboBox();
            this.klblFadeSpeed = new Krypton.Toolkit.KryptonLabel();
            this.kchkUseFade = new Krypton.Toolkit.KryptonCheckBox();
            this.ktxtMessage = new Krypton.Toolkit.KryptonTextBox();
            this.klblMessage = new Krypton.Toolkit.KryptonLabel();
            this.ktxtCaption = new Krypton.Toolkit.KryptonTextBox();
            this.klblCaption = new Krypton.Toolkit.KryptonLabel();
            this.klblInstructions = new Krypton.Toolkit.KryptonWrapLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).BeginInit();
            this.kpnlMain.SuspendLayout();
            this.flpPresets.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbButtons)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbTimeOutResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbTimeOutAction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbAutoClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbFadeSpeed)).BeginInit();
            this.SuspendLayout();
            // 
            // kpnlMain
            // 
            this.kpnlMain.Controls.Add(this.klblResult);
            this.kpnlMain.Controls.Add(this.flpPresets);
            this.kpnlMain.Controls.Add(this.kchkRtl);
            this.kpnlMain.Controls.Add(this.kcmbButtons);
            this.kpnlMain.Controls.Add(this.klblButtons);
            this.kpnlMain.Controls.Add(this.kcmbTimeOutResult);
            this.kpnlMain.Controls.Add(this.klblTimeOutResult);
            this.kpnlMain.Controls.Add(this.kcmbTimeOutAction);
            this.kpnlMain.Controls.Add(this.klblTimeOutAction);
            this.kpnlMain.Controls.Add(this.knudTimeOut);
            this.kpnlMain.Controls.Add(this.klblTimeOut);
            this.kpnlMain.Controls.Add(this.kcmbAutoClose);
            this.kpnlMain.Controls.Add(this.klblAutoClose);
            this.kpnlMain.Controls.Add(this.kchkUseTimeOut);
            this.kpnlMain.Controls.Add(this.kcmbFadeSpeed);
            this.kpnlMain.Controls.Add(this.klblFadeSpeed);
            this.kpnlMain.Controls.Add(this.kchkUseFade);
            this.kpnlMain.Controls.Add(this.ktxtMessage);
            this.kpnlMain.Controls.Add(this.klblMessage);
            this.kpnlMain.Controls.Add(this.ktxtCaption);
            this.kpnlMain.Controls.Add(this.klblCaption);
            this.kpnlMain.Controls.Add(this.klblInstructions);
            this.kpnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlMain.Location = new System.Drawing.Point(0, 0);
            this.kpnlMain.Name = "kpnlMain";
            this.kpnlMain.Padding = new System.Windows.Forms.Padding(12);
            this.kpnlMain.Size = new System.Drawing.Size(724, 641);
            this.kpnlMain.TabIndex = 0;
            // 
            // klblInstructions
            // 
            this.klblInstructions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.klblInstructions.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.klblInstructions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.klblInstructions.Location = new System.Drawing.Point(12, 12);
            this.klblInstructions.Name = "klblInstructions";
            this.klblInstructions.Size = new System.Drawing.Size(700, 52);
            this.klblInstructions.Text = "Issue #4188: configure fade, caption timeout, and auto-close, then Show. Presets " +
    "cover fade-only, display-only countdown, auto-close with a DialogResult, auto-cl" +
    "ose via button two, fade+timeout, RTL, the existing Show() timeout overload, and ShowAsync(data).";
            // 
            // klblCaption
            // 
            this.klblCaption.Location = new System.Drawing.Point(12, 72);
            this.klblCaption.Name = "klblCaption";
            this.klblCaption.Size = new System.Drawing.Size(60, 20);
            this.klblCaption.TabIndex = 1;
            this.klblCaption.Values.Text = "Caption:";
            // 
            // ktxtCaption
            // 
            this.ktxtCaption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ktxtCaption.Location = new System.Drawing.Point(160, 72);
            this.ktxtCaption.Name = "ktxtCaption";
            this.ktxtCaption.Size = new System.Drawing.Size(552, 23);
            this.ktxtCaption.TabIndex = 2;
            // 
            // klblMessage
            // 
            this.klblMessage.Location = new System.Drawing.Point(12, 104);
            this.klblMessage.Name = "klblMessage";
            this.klblMessage.Size = new System.Drawing.Size(64, 20);
            this.klblMessage.TabIndex = 3;
            this.klblMessage.Values.Text = "Message:";
            // 
            // ktxtMessage
            // 
            this.ktxtMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ktxtMessage.Location = new System.Drawing.Point(160, 104);
            this.ktxtMessage.Multiline = true;
            this.ktxtMessage.Name = "ktxtMessage";
            this.ktxtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ktxtMessage.Size = new System.Drawing.Size(552, 72);
            this.ktxtMessage.TabIndex = 4;
            // 
            // kchkUseFade
            // 
            this.kchkUseFade.Location = new System.Drawing.Point(160, 188);
            this.kchkUseFade.Name = "kchkUseFade";
            this.kchkUseFade.Size = new System.Drawing.Size(80, 20);
            this.kchkUseFade.TabIndex = 5;
            this.kchkUseFade.Values.Text = "Use fade";
            // 
            // klblFadeSpeed
            // 
            this.klblFadeSpeed.Location = new System.Drawing.Point(280, 188);
            this.klblFadeSpeed.Name = "klblFadeSpeed";
            this.klblFadeSpeed.Size = new System.Drawing.Size(74, 20);
            this.klblFadeSpeed.TabIndex = 6;
            this.klblFadeSpeed.Values.Text = "Fade speed:";
            // 
            // kcmbFadeSpeed
            // 
            this.kcmbFadeSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbFadeSpeed.DropDownWidth = 160;
            this.kcmbFadeSpeed.IntegralHeight = false;
            this.kcmbFadeSpeed.Location = new System.Drawing.Point(360, 186);
            this.kcmbFadeSpeed.Name = "kcmbFadeSpeed";
            this.kcmbFadeSpeed.Size = new System.Drawing.Size(160, 22);
            this.kcmbFadeSpeed.TabIndex = 7;
            // 
            // kchkUseTimeOut
            // 
            this.kchkUseTimeOut.Location = new System.Drawing.Point(160, 220);
            this.kchkUseTimeOut.Name = "kchkUseTimeOut";
            this.kchkUseTimeOut.Size = new System.Drawing.Size(176, 20);
            this.kchkUseTimeOut.TabIndex = 8;
            this.kchkUseTimeOut.Values.Text = "Show caption countdown";
            // 
            // klblAutoClose
            // 
            this.klblAutoClose.Location = new System.Drawing.Point(12, 252);
            this.klblAutoClose.Name = "klblAutoClose";
            this.klblAutoClose.Size = new System.Drawing.Size(70, 20);
            this.klblAutoClose.TabIndex = 9;
            this.klblAutoClose.Values.Text = "Auto-close:";
            // 
            // kcmbAutoClose
            // 
            this.kcmbAutoClose.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbAutoClose.DropDownWidth = 160;
            this.kcmbAutoClose.IntegralHeight = false;
            this.kcmbAutoClose.Location = new System.Drawing.Point(160, 250);
            this.kcmbAutoClose.Name = "kcmbAutoClose";
            this.kcmbAutoClose.Size = new System.Drawing.Size(160, 22);
            this.kcmbAutoClose.TabIndex = 10;
            // 
            // klblTimeOut
            // 
            this.klblTimeOut.Location = new System.Drawing.Point(340, 252);
            this.klblTimeOut.Name = "klblTimeOut";
            this.klblTimeOut.Size = new System.Drawing.Size(90, 20);
            this.klblTimeOut.TabIndex = 11;
            this.klblTimeOut.Values.Text = "Timeout (sec):";
            // 
            // knudTimeOut
            // 
            this.knudTimeOut.Location = new System.Drawing.Point(436, 250);
            this.knudTimeOut.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.knudTimeOut.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.knudTimeOut.Name = "knudTimeOut";
            this.knudTimeOut.Size = new System.Drawing.Size(80, 22);
            this.knudTimeOut.TabIndex = 12;
            this.knudTimeOut.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // klblTimeOutAction
            // 
            this.klblTimeOutAction.Location = new System.Drawing.Point(12, 284);
            this.klblTimeOutAction.Name = "klblTimeOutAction";
            this.klblTimeOutAction.Size = new System.Drawing.Size(92, 20);
            this.klblTimeOutAction.TabIndex = 13;
            this.klblTimeOutAction.Values.Text = "Timeout action:";
            // 
            // kcmbTimeOutAction
            // 
            this.kcmbTimeOutAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbTimeOutAction.DropDownWidth = 160;
            this.kcmbTimeOutAction.IntegralHeight = false;
            this.kcmbTimeOutAction.Location = new System.Drawing.Point(160, 282);
            this.kcmbTimeOutAction.Name = "kcmbTimeOutAction";
            this.kcmbTimeOutAction.Size = new System.Drawing.Size(160, 22);
            this.kcmbTimeOutAction.TabIndex = 14;
            // 
            // klblTimeOutResult
            // 
            this.klblTimeOutResult.Location = new System.Drawing.Point(340, 284);
            this.klblTimeOutResult.Name = "klblTimeOutResult";
            this.klblTimeOutResult.Size = new System.Drawing.Size(90, 20);
            this.klblTimeOutResult.TabIndex = 15;
            this.klblTimeOutResult.Values.Text = "Timeout result:";
            // 
            // kcmbTimeOutResult
            // 
            this.kcmbTimeOutResult.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbTimeOutResult.DropDownWidth = 160;
            this.kcmbTimeOutResult.IntegralHeight = false;
            this.kcmbTimeOutResult.Location = new System.Drawing.Point(436, 282);
            this.kcmbTimeOutResult.Name = "kcmbTimeOutResult";
            this.kcmbTimeOutResult.Size = new System.Drawing.Size(160, 22);
            this.kcmbTimeOutResult.TabIndex = 16;
            // 
            // klblButtons
            // 
            this.klblButtons.Location = new System.Drawing.Point(12, 316);
            this.klblButtons.Name = "klblButtons";
            this.klblButtons.Size = new System.Drawing.Size(56, 20);
            this.klblButtons.TabIndex = 17;
            this.klblButtons.Values.Text = "Buttons:";
            // 
            // kcmbButtons
            // 
            this.kcmbButtons.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbButtons.DropDownWidth = 160;
            this.kcmbButtons.IntegralHeight = false;
            this.kcmbButtons.Location = new System.Drawing.Point(160, 314);
            this.kcmbButtons.Name = "kcmbButtons";
            this.kcmbButtons.Size = new System.Drawing.Size(160, 22);
            this.kcmbButtons.TabIndex = 18;
            // 
            // kchkRtl
            // 
            this.kchkRtl.Location = new System.Drawing.Point(340, 316);
            this.kchkRtl.Name = "kchkRtl";
            this.kchkRtl.Size = new System.Drawing.Size(200, 20);
            this.kchkRtl.TabIndex = 19;
            this.kchkRtl.Values.Text = "RTL (MessageBoxOptions)";
            // 
            // flpPresets
            // 
            this.flpPresets.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpPresets.Controls.Add(this.kbtnShow);
            this.flpPresets.Controls.Add(this.kbtnFadeOnly);
            this.flpPresets.Controls.Add(this.kbtnTimeoutNoClose);
            this.flpPresets.Controls.Add(this.kbtnAutoCloseOk);
            this.flpPresets.Controls.Add(this.kbtnAutoCloseButton2);
            this.flpPresets.Controls.Add(this.kbtnFadeAndTimeout);
            this.flpPresets.Controls.Add(this.kbtnRtlTimeout);
            this.flpPresets.Controls.Add(this.kbtnShowOverload);
            this.flpPresets.Controls.Add(this.kbtnShowAsync);
            this.flpPresets.Location = new System.Drawing.Point(12, 352);
            this.flpPresets.Name = "flpPresets";
            this.flpPresets.Size = new System.Drawing.Size(700, 220);
            this.flpPresets.TabIndex = 20;
            // 
            // kbtnShow
            // 
            this.kbtnShow.Location = new System.Drawing.Point(3, 3);
            this.kbtnShow.Name = "kbtnShow";
            this.kbtnShow.Size = new System.Drawing.Size(160, 32);
            this.kbtnShow.TabIndex = 0;
            this.kbtnShow.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnShow.Values.Text = "Show (current options)";
            this.kbtnShow.Click += new System.EventHandler(this.kbtnShow_Click);
            // 
            // kbtnFadeOnly
            // 
            this.kbtnFadeOnly.Location = new System.Drawing.Point(169, 3);
            this.kbtnFadeOnly.Name = "kbtnFadeOnly";
            this.kbtnFadeOnly.Size = new System.Drawing.Size(160, 32);
            this.kbtnFadeOnly.TabIndex = 1;
            this.kbtnFadeOnly.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnFadeOnly.Values.Text = "Preset: fade only";
            this.kbtnFadeOnly.Click += new System.EventHandler(this.kbtnFadeOnly_Click);
            // 
            // kbtnTimeoutNoClose
            // 
            this.kbtnTimeoutNoClose.Location = new System.Drawing.Point(335, 3);
            this.kbtnTimeoutNoClose.Name = "kbtnTimeoutNoClose";
            this.kbtnTimeoutNoClose.Size = new System.Drawing.Size(180, 32);
            this.kbtnTimeoutNoClose.TabIndex = 2;
            this.kbtnTimeoutNoClose.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnTimeoutNoClose.Values.Text = "Preset: countdown only";
            this.kbtnTimeoutNoClose.Click += new System.EventHandler(this.kbtnTimeoutNoClose_Click);
            // 
            // kbtnAutoCloseOk
            // 
            this.kbtnAutoCloseOk.Location = new System.Drawing.Point(521, 3);
            this.kbtnAutoCloseOk.Name = "kbtnAutoCloseOk";
            this.kbtnAutoCloseOk.Size = new System.Drawing.Size(160, 32);
            this.kbtnAutoCloseOk.TabIndex = 3;
            this.kbtnAutoCloseOk.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnAutoCloseOk.Values.Text = "Preset: auto-close OK";
            this.kbtnAutoCloseOk.Click += new System.EventHandler(this.kbtnAutoCloseOk_Click);
            // 
            // kbtnAutoCloseButton2
            // 
            this.kbtnAutoCloseButton2.Location = new System.Drawing.Point(3, 41);
            this.kbtnAutoCloseButton2.Name = "kbtnAutoCloseButton2";
            this.kbtnAutoCloseButton2.Size = new System.Drawing.Size(180, 32);
            this.kbtnAutoCloseButton2.TabIndex = 4;
            this.kbtnAutoCloseButton2.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnAutoCloseButton2.Values.Text = "Preset: click button 2";
            this.kbtnAutoCloseButton2.Click += new System.EventHandler(this.kbtnAutoCloseButton2_Click);
            // 
            // kbtnFadeAndTimeout
            // 
            this.kbtnFadeAndTimeout.Location = new System.Drawing.Point(189, 41);
            this.kbtnFadeAndTimeout.Name = "kbtnFadeAndTimeout";
            this.kbtnFadeAndTimeout.Size = new System.Drawing.Size(180, 32);
            this.kbtnFadeAndTimeout.TabIndex = 5;
            this.kbtnFadeAndTimeout.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnFadeAndTimeout.Values.Text = "Preset: fade + timeout";
            this.kbtnFadeAndTimeout.Click += new System.EventHandler(this.kbtnFadeAndTimeout_Click);
            // 
            // kbtnRtlTimeout
            // 
            this.kbtnRtlTimeout.Location = new System.Drawing.Point(375, 41);
            this.kbtnRtlTimeout.Name = "kbtnRtlTimeout";
            this.kbtnRtlTimeout.Size = new System.Drawing.Size(160, 32);
            this.kbtnRtlTimeout.TabIndex = 6;
            this.kbtnRtlTimeout.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnRtlTimeout.Values.Text = "Preset: RTL timeout";
            this.kbtnRtlTimeout.Click += new System.EventHandler(this.kbtnRtlTimeout_Click);
            // 
            // kbtnShowOverload
            // 
            this.kbtnShowOverload.Location = new System.Drawing.Point(3, 79);
            this.kbtnShowOverload.Name = "kbtnShowOverload";
            this.kbtnShowOverload.Size = new System.Drawing.Size(200, 32);
            this.kbtnShowOverload.TabIndex = 7;
            this.kbtnShowOverload.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnShowOverload.Values.Text = "Show() timeout overload";
            this.kbtnShowOverload.Click += new System.EventHandler(this.kbtnShowOverload_Click);
            // 
            // kbtnShowAsync
            // 
            this.kbtnShowAsync.Location = new System.Drawing.Point(209, 79);
            this.kbtnShowAsync.Name = "kbtnShowAsync";
            this.kbtnShowAsync.Size = new System.Drawing.Size(160, 32);
            this.kbtnShowAsync.TabIndex = 8;
            this.kbtnShowAsync.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnShowAsync.Values.Text = "ShowAsync(data)";
            this.kbtnShowAsync.Click += new System.EventHandler(this.kbtnShowAsync_Click);
            // 
            // klblResult
            // 
            this.klblResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.klblResult.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.klblResult.Location = new System.Drawing.Point(12, 584);
            this.klblResult.Name = "klblResult";
            this.klblResult.Size = new System.Drawing.Size(83, 20);
            this.klblResult.TabIndex = 21;
            this.klblResult.Values.Text = "Last result: -";
            // 
            // MessageBoxExtendedLifetimeDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 641);
            this.Controls.Add(this.kpnlMain);
            this.MinimumSize = new System.Drawing.Size(640, 560);
            this.Name = "MessageBoxExtendedLifetimeDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "4188 MessageBox Extended Fade / Timeout";
            ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).EndInit();
            this.kpnlMain.ResumeLayout(false);
            this.kpnlMain.PerformLayout();
            this.flpPresets.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kcmbButtons)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbTimeOutResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbTimeOutAction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbAutoClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbFadeSpeed)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kpnlMain;
        private Krypton.Toolkit.KryptonWrapLabel klblInstructions;
        private Krypton.Toolkit.KryptonLabel klblCaption;
        private Krypton.Toolkit.KryptonTextBox ktxtCaption;
        private Krypton.Toolkit.KryptonLabel klblMessage;
        private Krypton.Toolkit.KryptonTextBox ktxtMessage;
        private Krypton.Toolkit.KryptonCheckBox kchkUseFade;
        private Krypton.Toolkit.KryptonLabel klblFadeSpeed;
        private Krypton.Toolkit.KryptonComboBox kcmbFadeSpeed;
        private Krypton.Toolkit.KryptonCheckBox kchkUseTimeOut;
        private Krypton.Toolkit.KryptonLabel klblAutoClose;
        private Krypton.Toolkit.KryptonComboBox kcmbAutoClose;
        private Krypton.Toolkit.KryptonLabel klblTimeOut;
        private Krypton.Toolkit.KryptonNumericUpDown knudTimeOut;
        private Krypton.Toolkit.KryptonLabel klblTimeOutAction;
        private Krypton.Toolkit.KryptonComboBox kcmbTimeOutAction;
        private Krypton.Toolkit.KryptonLabel klblTimeOutResult;
        private Krypton.Toolkit.KryptonComboBox kcmbTimeOutResult;
        private Krypton.Toolkit.KryptonLabel klblButtons;
        private Krypton.Toolkit.KryptonComboBox kcmbButtons;
        private Krypton.Toolkit.KryptonCheckBox kchkRtl;
        private System.Windows.Forms.FlowLayoutPanel flpPresets;
        private Krypton.Toolkit.KryptonButton kbtnShow;
        private Krypton.Toolkit.KryptonButton kbtnFadeOnly;
        private Krypton.Toolkit.KryptonButton kbtnTimeoutNoClose;
        private Krypton.Toolkit.KryptonButton kbtnAutoCloseOk;
        private Krypton.Toolkit.KryptonButton kbtnAutoCloseButton2;
        private Krypton.Toolkit.KryptonButton kbtnFadeAndTimeout;
        private Krypton.Toolkit.KryptonButton kbtnRtlTimeout;
        private Krypton.Toolkit.KryptonButton kbtnShowOverload;
        private Krypton.Toolkit.KryptonButton kbtnShowAsync;
        private Krypton.Toolkit.KryptonLabel klblResult;
    }
}

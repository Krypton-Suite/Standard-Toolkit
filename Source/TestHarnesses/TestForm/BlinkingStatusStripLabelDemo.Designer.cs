namespace TestForm;

partial class BlinkingStatusStripLabelDemo
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        this.kpnlMain = new Krypton.Toolkit.KryptonPanel();
        this.klblInstructions = new Krypton.Toolkit.KryptonWrapLabel();
        this.tlpOptions = new System.Windows.Forms.TableLayoutPanel();
        this.klblMode = new Krypton.Toolkit.KryptonLabel();
        this.kcmbMode = new Krypton.Toolkit.KryptonComboBox();
        this.klblTarget = new Krypton.Toolkit.KryptonLabel();
        this.kcmbTarget = new Krypton.Toolkit.KryptonComboBox();
        this.klblVisibilityStyle = new Krypton.Toolkit.KryptonLabel();
        this.kcmbVisibilityStyle = new Krypton.Toolkit.KryptonComboBox();
        this.klblInterval = new Krypton.Toolkit.KryptonLabel();
        this.knudInterval = new Krypton.Toolkit.KryptonNumericUpDown();
        this.klblSoftCycle = new Krypton.Toolkit.KryptonLabel();
        this.knudSoftCycle = new Krypton.Toolkit.KryptonNumericUpDown();
        this.klblSoftTick = new Krypton.Toolkit.KryptonLabel();
        this.knudSoftTick = new Krypton.Toolkit.KryptonNumericUpDown();
        this.klblDuration = new Krypton.Toolkit.KryptonLabel();
        this.knudDuration = new Krypton.Toolkit.KryptonNumericUpDown();
        this.klblMaxCount = new Krypton.Toolkit.KryptonLabel();
        this.knudMaxCount = new Krypton.Toolkit.KryptonNumericUpDown();
        this.klblSessionDuration = new Krypton.Toolkit.KryptonLabel();
        this.knudSessionDuration = new Krypton.Toolkit.KryptonNumericUpDown();
        this.klblColorOne = new Krypton.Toolkit.KryptonLabel();
        this.kbtnColorOne = new Krypton.Toolkit.KryptonColorButton();
        this.klblColorTwo = new Krypton.Toolkit.KryptonLabel();
        this.kbtnColorTwo = new Krypton.Toolkit.KryptonColorButton();
        this.klblTextColor = new Krypton.Toolkit.KryptonLabel();
        this.kbtnTextColor = new Krypton.Toolkit.KryptonColorButton();
        this.flpFlags = new System.Windows.Forms.FlowLayoutPanel();
        this.kchkBlinkEnabled = new Krypton.Toolkit.KryptonCheckBox();
        this.kchkUseBlinkTextColor = new Krypton.Toolkit.KryptonCheckBox();
        this.kchkPauseOnMouseOver = new Krypton.Toolkit.KryptonCheckBox();
        this.kchkRestoreOnStop = new Krypton.Toolkit.KryptonCheckBox();
        this.kchkBlinkOnlyWhenVisible = new Krypton.Toolkit.KryptonCheckBox();
        this.flpButtons = new System.Windows.Forms.FlowLayoutPanel();
        this.kbtnStart = new Krypton.Toolkit.KryptonButton();
        this.kbtnStartDuration = new Krypton.Toolkit.KryptonButton();
        this.kbtnStop = new Krypton.Toolkit.KryptonButton();
        this.klblRuntime = new Krypton.Toolkit.KryptonLabel();
        this.klblStatus = new Krypton.Toolkit.KryptonLabel();
        this.statusStrip = new System.Windows.Forms.StatusStrip();
        this._blinkingLabel = new Krypton.Toolkit.Utilities.KryptonBlinkingToolStripStatusLabel();
        this._normalLabel = new System.Windows.Forms.ToolStripStatusLabel();
        ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).BeginInit();
        this.kpnlMain.SuspendLayout();
        this.tlpOptions.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.kcmbMode)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.kcmbTarget)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.kcmbVisibilityStyle)).BeginInit();
        this.flpFlags.SuspendLayout();
        this.flpButtons.SuspendLayout();
        this.statusStrip.SuspendLayout();
        this.SuspendLayout();
        // 
        // kpnlMain
        // 
        this.kpnlMain.Controls.Add(this.klblStatus);
        this.kpnlMain.Controls.Add(this.klblRuntime);
        this.kpnlMain.Controls.Add(this.flpButtons);
        this.kpnlMain.Controls.Add(this.flpFlags);
        this.kpnlMain.Controls.Add(this.tlpOptions);
        this.kpnlMain.Controls.Add(this.klblInstructions);
        this.kpnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
        this.kpnlMain.Location = new System.Drawing.Point(0, 0);
        this.kpnlMain.Name = "kpnlMain";
        this.kpnlMain.Padding = new System.Windows.Forms.Padding(12);
        this.kpnlMain.Size = new System.Drawing.Size(820, 480);
        this.kpnlMain.TabIndex = 0;
        // 
        // klblInstructions
        // 
        this.klblInstructions.AutoSize = false;
        this.klblInstructions.Dock = System.Windows.Forms.DockStyle.Top;
        this.klblInstructions.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.klblInstructions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
        this.klblInstructions.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
        this.klblInstructions.Location = new System.Drawing.Point(12, 12);
        this.klblInstructions.Name = "klblInstructions";
        this.klblInstructions.Size = new System.Drawing.Size(796, 56);
        this.klblInstructions.Text = "Compare the blinking status label (left) with a normal ToolStripStatusLabel (righ" +
    "t). Try Hard / Soft / Visibility modes, adjust intervals, colours, PauseOnMouseO" +
    "ver, MaxBlinkCount, and duration. Soft interpolates colours; Visibility HideText" +
    " avoids StatusStrip reflow.";
        this.klblInstructions.TextAlign = System.Drawing.ContentAlignment.TopLeft;
        // 
        // tlpOptions
        // 
        this.tlpOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
        | System.Windows.Forms.AnchorStyles.Right)));
        this.tlpOptions.ColumnCount = 4;
        this.tlpOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
        this.tlpOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
        this.tlpOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
        this.tlpOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
        this.tlpOptions.Controls.Add(this.klblMode, 0, 0);
        this.tlpOptions.Controls.Add(this.kcmbMode, 1, 0);
        this.tlpOptions.Controls.Add(this.klblTarget, 2, 0);
        this.tlpOptions.Controls.Add(this.kcmbTarget, 3, 0);
        this.tlpOptions.Controls.Add(this.klblVisibilityStyle, 0, 1);
        this.tlpOptions.Controls.Add(this.kcmbVisibilityStyle, 1, 1);
        this.tlpOptions.Controls.Add(this.klblInterval, 2, 1);
        this.tlpOptions.Controls.Add(this.knudInterval, 3, 1);
        this.tlpOptions.Controls.Add(this.klblSoftCycle, 0, 2);
        this.tlpOptions.Controls.Add(this.knudSoftCycle, 1, 2);
        this.tlpOptions.Controls.Add(this.klblSoftTick, 2, 2);
        this.tlpOptions.Controls.Add(this.knudSoftTick, 3, 2);
        this.tlpOptions.Controls.Add(this.klblDuration, 0, 3);
        this.tlpOptions.Controls.Add(this.knudDuration, 1, 3);
        this.tlpOptions.Controls.Add(this.klblMaxCount, 2, 3);
        this.tlpOptions.Controls.Add(this.knudMaxCount, 3, 3);
        this.tlpOptions.Controls.Add(this.klblSessionDuration, 0, 4);
        this.tlpOptions.Controls.Add(this.knudSessionDuration, 1, 4);
        this.tlpOptions.Controls.Add(this.klblColorOne, 0, 5);
        this.tlpOptions.Controls.Add(this.kbtnColorOne, 1, 5);
        this.tlpOptions.Controls.Add(this.klblColorTwo, 2, 5);
        this.tlpOptions.Controls.Add(this.kbtnColorTwo, 3, 5);
        this.tlpOptions.Controls.Add(this.klblTextColor, 0, 6);
        this.tlpOptions.Controls.Add(this.kbtnTextColor, 1, 6);
        this.tlpOptions.Location = new System.Drawing.Point(12, 76);
        this.tlpOptions.Name = "tlpOptions";
        this.tlpOptions.RowCount = 7;
        this.tlpOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
        this.tlpOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
        this.tlpOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
        this.tlpOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
        this.tlpOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
        this.tlpOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
        this.tlpOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
        this.tlpOptions.Size = new System.Drawing.Size(796, 224);
        this.tlpOptions.TabIndex = 1;
        // 
        // klblMode
        // 
        this.klblMode.Values.Text = "BlinkMode";
        // 
        // kcmbMode
        // 
        this.kcmbMode.Dock = System.Windows.Forms.DockStyle.Fill;
        this.kcmbMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.kcmbMode.Items.AddRange(new object[] {
            "Hard",
            "Soft",
            "Visibility"});
        this.kcmbMode.SelectedIndex = 0;
        // 
        // klblTarget
        // 
        this.klblTarget.Values.Text = "BlinkTarget";
        // 
        // kcmbTarget
        // 
        this.kcmbTarget.Dock = System.Windows.Forms.DockStyle.Fill;
        this.kcmbTarget.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.kcmbTarget.Items.AddRange(new object[] {
            "Foreground",
            "Background",
            "Both"});
        this.kcmbTarget.SelectedIndex = 0;
        // 
        // klblVisibilityStyle
        // 
        this.klblVisibilityStyle.Values.Text = "VisibilityStyle";
        // 
        // kcmbVisibilityStyle
        // 
        this.kcmbVisibilityStyle.Dock = System.Windows.Forms.DockStyle.Fill;
        this.kcmbVisibilityStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.kcmbVisibilityStyle.Items.AddRange(new object[] {
            "HideText",
            "ToggleVisible"});
        this.kcmbVisibilityStyle.SelectedIndex = 0;
        // 
        // klblInterval
        // 
        this.klblInterval.Values.Text = "BlinkInterval";
        // 
        // knudInterval
        // 
        this.knudInterval.Dock = System.Windows.Forms.DockStyle.Fill;
        this.knudInterval.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
        this.knudInterval.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
        this.knudInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
        this.knudInterval.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
        // 
        // klblSoftCycle
        // 
        this.klblSoftCycle.Values.Text = "SoftCycle (ms)";
        // 
        // knudSoftCycle
        // 
        this.knudSoftCycle.Dock = System.Windows.Forms.DockStyle.Fill;
        this.knudSoftCycle.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
        this.knudSoftCycle.Maximum = new decimal(new int[] {
            30000,
            0,
            0,
            0});
        this.knudSoftCycle.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
        this.knudSoftCycle.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
        // 
        // klblSoftTick
        // 
        this.klblSoftTick.Values.Text = "SoftTick (ms)";
        // 
        // knudSoftTick
        // 
        this.knudSoftTick.Dock = System.Windows.Forms.DockStyle.Fill;
        this.knudSoftTick.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
        this.knudSoftTick.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
        this.knudSoftTick.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
        // 
        // klblDuration
        // 
        this.klblDuration.Values.Text = "BlinkDuration";
        // 
        // knudDuration
        // 
        this.knudDuration.Dock = System.Windows.Forms.DockStyle.Fill;
        this.knudDuration.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
        this.knudDuration.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
        this.knudDuration.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
        // 
        // klblMaxCount
        // 
        this.klblMaxCount.Values.Text = "MaxBlinkCount";
        // 
        // knudMaxCount
        // 
        this.knudMaxCount.Dock = System.Windows.Forms.DockStyle.Fill;
        this.knudMaxCount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
        this.knudMaxCount.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
        // 
        // klblSessionDuration
        // 
        this.klblSessionDuration.Values.Text = "StartBlink(ms)";
        // 
        // knudSessionDuration
        // 
        this.knudSessionDuration.Dock = System.Windows.Forms.DockStyle.Fill;
        this.knudSessionDuration.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
        this.knudSessionDuration.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
        this.knudSessionDuration.Value = new decimal(new int[] {
            3000,
            0,
            0,
            0});
        // 
        // klblColorOne
        // 
        this.klblColorOne.Values.Text = "BlinkColorOne";
        // 
        // kbtnColorOne
        // 
        this.kbtnColorOne.Dock = System.Windows.Forms.DockStyle.Fill;
        this.kbtnColorOne.SelectedColor = System.Drawing.Color.White;
        this.kbtnColorOne.Values.Text = "Color One";
        // 
        // klblColorTwo
        // 
        this.klblColorTwo.Values.Text = "BlinkColorTwo";
        // 
        // kbtnColorTwo
        // 
        this.kbtnColorTwo.Dock = System.Windows.Forms.DockStyle.Fill;
        this.kbtnColorTwo.SelectedColor = System.Drawing.Color.Black;
        this.kbtnColorTwo.Values.Text = "Color Two";
        // 
        // klblTextColor
        // 
        this.klblTextColor.Values.Text = "BlinkTextColor";
        // 
        // kbtnTextColor
        // 
        this.kbtnTextColor.Dock = System.Windows.Forms.DockStyle.Fill;
        this.kbtnTextColor.SelectedColor = System.Drawing.Color.Red;
        this.kbtnTextColor.Values.Text = "Text Color";
        // 
        // flpFlags
        // 
        this.flpFlags.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
        | System.Windows.Forms.AnchorStyles.Right)));
        this.flpFlags.Controls.Add(this.kchkBlinkEnabled);
        this.flpFlags.Controls.Add(this.kchkUseBlinkTextColor);
        this.flpFlags.Controls.Add(this.kchkPauseOnMouseOver);
        this.flpFlags.Controls.Add(this.kchkRestoreOnStop);
        this.flpFlags.Controls.Add(this.kchkBlinkOnlyWhenVisible);
        this.flpFlags.Location = new System.Drawing.Point(12, 310);
        this.flpFlags.Name = "flpFlags";
        this.flpFlags.Size = new System.Drawing.Size(796, 36);
        this.flpFlags.TabIndex = 2;
        // 
        // kchkBlinkEnabled
        // 
        this.kchkBlinkEnabled.Location = new System.Drawing.Point(3, 3);
        this.kchkBlinkEnabled.Name = "kchkBlinkEnabled";
        this.kchkBlinkEnabled.Size = new System.Drawing.Size(110, 20);
        this.kchkBlinkEnabled.Values.Text = "BlinkEnabled";
        // 
        // kchkUseBlinkTextColor
        // 
        this.kchkUseBlinkTextColor.Checked = true;
        this.kchkUseBlinkTextColor.CheckState = System.Windows.Forms.CheckState.Checked;
        this.kchkUseBlinkTextColor.Location = new System.Drawing.Point(119, 3);
        this.kchkUseBlinkTextColor.Name = "kchkUseBlinkTextColor";
        this.kchkUseBlinkTextColor.Size = new System.Drawing.Size(140, 20);
        this.kchkUseBlinkTextColor.Values.Text = "UseBlinkTextColor";
        // 
        // kchkPauseOnMouseOver
        // 
        this.kchkPauseOnMouseOver.Location = new System.Drawing.Point(265, 3);
        this.kchkPauseOnMouseOver.Name = "kchkPauseOnMouseOver";
        this.kchkPauseOnMouseOver.Size = new System.Drawing.Size(140, 20);
        this.kchkPauseOnMouseOver.Values.Text = "PauseOnMouseOver";
        // 
        // kchkRestoreOnStop
        // 
        this.kchkRestoreOnStop.Checked = true;
        this.kchkRestoreOnStop.CheckState = System.Windows.Forms.CheckState.Checked;
        this.kchkRestoreOnStop.Location = new System.Drawing.Point(411, 3);
        this.kchkRestoreOnStop.Name = "kchkRestoreOnStop";
        this.kchkRestoreOnStop.Size = new System.Drawing.Size(160, 20);
        this.kchkRestoreOnStop.Values.Text = "RestoreAppearanceOnStop";
        // 
        // kchkBlinkOnlyWhenVisible
        // 
        this.kchkBlinkOnlyWhenVisible.Checked = true;
        this.kchkBlinkOnlyWhenVisible.CheckState = System.Windows.Forms.CheckState.Checked;
        this.kchkBlinkOnlyWhenVisible.Location = new System.Drawing.Point(577, 3);
        this.kchkBlinkOnlyWhenVisible.Name = "kchkBlinkOnlyWhenVisible";
        this.kchkBlinkOnlyWhenVisible.Size = new System.Drawing.Size(160, 20);
        this.kchkBlinkOnlyWhenVisible.Values.Text = "BlinkOnlyWhenVisible";
        // 
        // flpButtons
        // 
        this.flpButtons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
        | System.Windows.Forms.AnchorStyles.Right)));
        this.flpButtons.Controls.Add(this.kbtnStart);
        this.flpButtons.Controls.Add(this.kbtnStartDuration);
        this.flpButtons.Controls.Add(this.kbtnStop);
        this.flpButtons.Location = new System.Drawing.Point(12, 352);
        this.flpButtons.Name = "flpButtons";
        this.flpButtons.Size = new System.Drawing.Size(796, 40);
        this.flpButtons.TabIndex = 3;
        // 
        // kbtnStart
        // 
        this.kbtnStart.Location = new System.Drawing.Point(3, 3);
        this.kbtnStart.Name = "kbtnStart";
        this.kbtnStart.Size = new System.Drawing.Size(120, 32);
        this.kbtnStart.Values.Text = "StartBlink()";
        // 
        // kbtnStartDuration
        // 
        this.kbtnStartDuration.Location = new System.Drawing.Point(129, 3);
        this.kbtnStartDuration.Name = "kbtnStartDuration";
        this.kbtnStartDuration.Size = new System.Drawing.Size(160, 32);
        this.kbtnStartDuration.Values.Text = "StartBlink(duration)";
        // 
        // kbtnStop
        // 
        this.kbtnStop.Location = new System.Drawing.Point(295, 3);
        this.kbtnStop.Name = "kbtnStop";
        this.kbtnStop.Size = new System.Drawing.Size(120, 32);
        this.kbtnStop.Values.Text = "StopBlink()";
        // 
        // klblRuntime
        // 
        this.klblRuntime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
        | System.Windows.Forms.AnchorStyles.Right)));
        this.klblRuntime.Location = new System.Drawing.Point(15, 400);
        this.klblRuntime.Name = "klblRuntime";
        this.klblRuntime.Size = new System.Drawing.Size(790, 20);
        this.klblRuntime.Values.Text = "IsBlinking=False";
        // 
        // klblStatus
        // 
        this.klblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
        | System.Windows.Forms.AnchorStyles.Right)));
        this.klblStatus.Location = new System.Drawing.Point(15, 426);
        this.klblStatus.Name = "klblStatus";
        this.klblStatus.Size = new System.Drawing.Size(790, 20);
        this.klblStatus.Values.Text = "Status";
        // 
        // statusStrip
        // 
        this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._blinkingLabel,
            this._normalLabel});
        this.statusStrip.Location = new System.Drawing.Point(0, 480);
        this.statusStrip.Name = "statusStrip";
        this.statusStrip.Size = new System.Drawing.Size(820, 22);
        this.statusStrip.TabIndex = 1;
        this.statusStrip.Text = "statusStrip";
        // 
        // _blinkingLabel
        // 
        this._blinkingLabel.Name = "_blinkingLabel";
        this._blinkingLabel.Size = new System.Drawing.Size(150, 17);
        this._blinkingLabel.Spring = true;
        this._blinkingLabel.Text = "Blinking status label";
        this._blinkingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // _normalLabel
        // 
        this._normalLabel.Name = "_normalLabel";
        this._normalLabel.Size = new System.Drawing.Size(655, 17);
        this._normalLabel.Spring = true;
        this._normalLabel.Text = "Normal ToolStripStatusLabel";
        this._normalLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // BlinkingStatusStripLabelDemo
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(820, 502);
        this.Controls.Add(this.kpnlMain);
        this.Controls.Add(this.statusStrip);
        this.Name = "BlinkingStatusStripLabelDemo";
        this.Text = "Blinking Status Strip Label Demo";
        ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).EndInit();
        this.kpnlMain.ResumeLayout(false);
        this.kpnlMain.PerformLayout();
        this.tlpOptions.ResumeLayout(false);
        this.tlpOptions.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.kcmbMode)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.kcmbTarget)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.kcmbVisibilityStyle)).EndInit();
        this.flpFlags.ResumeLayout(false);
        this.flpFlags.PerformLayout();
        this.flpButtons.ResumeLayout(false);
        this.statusStrip.ResumeLayout(false);
        this.statusStrip.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #endregion

    private Krypton.Toolkit.KryptonPanel kpnlMain;
    private Krypton.Toolkit.KryptonWrapLabel klblInstructions;
    private System.Windows.Forms.TableLayoutPanel tlpOptions;
    private Krypton.Toolkit.KryptonLabel klblMode;
    private Krypton.Toolkit.KryptonComboBox kcmbMode;
    private Krypton.Toolkit.KryptonLabel klblTarget;
    private Krypton.Toolkit.KryptonComboBox kcmbTarget;
    private Krypton.Toolkit.KryptonLabel klblVisibilityStyle;
    private Krypton.Toolkit.KryptonComboBox kcmbVisibilityStyle;
    private Krypton.Toolkit.KryptonLabel klblInterval;
    private Krypton.Toolkit.KryptonNumericUpDown knudInterval;
    private Krypton.Toolkit.KryptonLabel klblSoftCycle;
    private Krypton.Toolkit.KryptonNumericUpDown knudSoftCycle;
    private Krypton.Toolkit.KryptonLabel klblSoftTick;
    private Krypton.Toolkit.KryptonNumericUpDown knudSoftTick;
    private Krypton.Toolkit.KryptonLabel klblDuration;
    private Krypton.Toolkit.KryptonNumericUpDown knudDuration;
    private Krypton.Toolkit.KryptonLabel klblMaxCount;
    private Krypton.Toolkit.KryptonNumericUpDown knudMaxCount;
    private Krypton.Toolkit.KryptonLabel klblSessionDuration;
    private Krypton.Toolkit.KryptonNumericUpDown knudSessionDuration;
    private Krypton.Toolkit.KryptonLabel klblColorOne;
    private Krypton.Toolkit.KryptonColorButton kbtnColorOne;
    private Krypton.Toolkit.KryptonLabel klblColorTwo;
    private Krypton.Toolkit.KryptonColorButton kbtnColorTwo;
    private Krypton.Toolkit.KryptonLabel klblTextColor;
    private Krypton.Toolkit.KryptonColorButton kbtnTextColor;
    private System.Windows.Forms.FlowLayoutPanel flpFlags;
    private Krypton.Toolkit.KryptonCheckBox kchkBlinkEnabled;
    private Krypton.Toolkit.KryptonCheckBox kchkUseBlinkTextColor;
    private Krypton.Toolkit.KryptonCheckBox kchkPauseOnMouseOver;
    private Krypton.Toolkit.KryptonCheckBox kchkRestoreOnStop;
    private Krypton.Toolkit.KryptonCheckBox kchkBlinkOnlyWhenVisible;
    private System.Windows.Forms.FlowLayoutPanel flpButtons;
    private Krypton.Toolkit.KryptonButton kbtnStart;
    private Krypton.Toolkit.KryptonButton kbtnStartDuration;
    private Krypton.Toolkit.KryptonButton kbtnStop;
    private Krypton.Toolkit.KryptonLabel klblRuntime;
    private Krypton.Toolkit.KryptonLabel klblStatus;
    private System.Windows.Forms.StatusStrip statusStrip;
    private Krypton.Toolkit.Utilities.KryptonBlinkingToolStripStatusLabel _blinkingLabel;
    private System.Windows.Forms.ToolStripStatusLabel _normalLabel;
}

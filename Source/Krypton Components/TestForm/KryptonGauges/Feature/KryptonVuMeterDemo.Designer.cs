#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, tobitege, Lesandro, KamaniAR, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

partial class KryptonVuMeterDemo
{
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.klblInstructions = new Krypton.Toolkit.KryptonLabel();
        this.klblAnalog = new Krypton.Toolkit.KryptonLabel();
        this.kvumAnalog = new Krypton.Gauges.KryptonVuMeter();
        this.klblSegments = new Krypton.Toolkit.KryptonLabel();
        this.kvumSegments = new Krypton.Gauges.KryptonVuMeter();
        this.klblDisabled = new Krypton.Toolkit.KryptonLabel();
        this.kvumDisabled = new Krypton.Gauges.KryptonVuMeter();
        this.klblLevel = new Krypton.Toolkit.KryptonLabel();
        this.ktrkLevel = new Krypton.Toolkit.KryptonTrackBar();
        this.kchkPeak = new Krypton.Toolkit.KryptonCheckBox();
        this.kchkCapture = new Krypton.Toolkit.KryptonCheckBox();
        this.klblChannel = new Krypton.Toolkit.KryptonLabel();
        this.kcmbChannel = new Krypton.Toolkit.KryptonComboBox();
        this.klblOrientation = new Krypton.Toolkit.KryptonLabel();
        this.kcmbOrientation = new Krypton.Toolkit.KryptonComboBox();
        this.klblTheme = new Krypton.Toolkit.KryptonLabel();
        this.ktheme = new Krypton.Toolkit.KryptonThemeComboBox();
        this.kbtnSilence = new Krypton.Toolkit.KryptonButton();
        this.kbtnZero = new Krypton.Toolkit.KryptonButton();
        this.kpgMeter = new Krypton.Toolkit.KryptonPropertyGrid();
        ((System.ComponentModel.ISupportInitialize)this.kcmbChannel).BeginInit();
        ((System.ComponentModel.ISupportInitialize)this.kcmbOrientation).BeginInit();
        ((System.ComponentModel.ISupportInitialize)this.ktheme).BeginInit();
        this.SuspendLayout();
        // 
        // klblInstructions
        // 
        this.klblInstructions.Location = new System.Drawing.Point(12, 12);
        this.klblInstructions.Name = "klblInstructions";
        this.klblInstructions.Size = new System.Drawing.Size(1040, 52);
        this.klblInstructions.TabIndex = 0;
        this.klblInstructions.Values.Text = "Drag the level to move both meters. Colours follow the theme. StateCommon, StateNormal, and StateDisabled override them. The disabled meter uses the disabled palette state.";
        // 
        // klblAnalog
        // 
        this.klblAnalog.Location = new System.Drawing.Point(16, 72);
        this.klblAnalog.Name = "klblAnalog";
        this.klblAnalog.Size = new System.Drawing.Size(120, 20);
        this.klblAnalog.TabIndex = 1;
        this.klblAnalog.Values.Text = "Analog";
        // 
        // kvumAnalog
        // 
        this.kvumAnalog.Location = new System.Drawing.Point(16, 96);
        this.kvumAnalog.Name = "kvumAnalog";
        this.kvumAnalog.Size = new System.Drawing.Size(330, 180);
        this.kvumAnalog.TabIndex = 2;
        this.kvumAnalog.ValueChanged += new System.EventHandler<Krypton.Gauges.VuMeterValueChangedEventArgs>(this.kvumAnalog_ValueChanged);
        this.kvumAnalog.CaptureFailed += new System.EventHandler<Krypton.Gauges.VuMeterCaptureFailedEventArgs>(this.kvumAnalog_CaptureFailed);
        // 
        // klblSegments
        // 
        this.klblSegments.Location = new System.Drawing.Point(364, 72);
        this.klblSegments.Name = "klblSegments";
        this.klblSegments.Size = new System.Drawing.Size(140, 20);
        this.klblSegments.TabIndex = 3;
        this.klblSegments.Values.Text = "Segments";
        // 
        // kvumSegments
        // 
        this.kvumSegments.Location = new System.Drawing.Point(364, 96);
        this.kvumSegments.Name = "kvumSegments";
        this.kvumSegments.Size = new System.Drawing.Size(330, 180);
        this.kvumSegments.TabIndex = 4;
        this.kvumSegments.Values.Style = Krypton.Gauges.VuMeterStyle.Segments;
        // 
        // klblDisabled
        // 
        this.klblDisabled.Location = new System.Drawing.Point(712, 72);
        this.klblDisabled.Name = "klblDisabled";
        this.klblDisabled.Size = new System.Drawing.Size(180, 20);
        this.klblDisabled.TabIndex = 5;
        this.klblDisabled.Values.Text = "Disabled analog at 0 VU";
        // 
        // kvumDisabled
        // 
        this.kvumDisabled.Enabled = false;
        this.kvumDisabled.Location = new System.Drawing.Point(712, 96);
        this.kvumDisabled.Name = "kvumDisabled";
        this.kvumDisabled.Size = new System.Drawing.Size(330, 180);
        this.kvumDisabled.TabIndex = 6;
        // 
        // klblLevel
        // 
        this.klblLevel.Location = new System.Drawing.Point(16, 288);
        this.klblLevel.Name = "klblLevel";
        this.klblLevel.Size = new System.Drawing.Size(160, 20);
        this.klblLevel.TabIndex = 7;
        this.klblLevel.Values.Text = "Target -20.0 VU";
        // 
        // ktrkLevel
        // 
        this.ktrkLevel.Location = new System.Drawing.Point(16, 312);
        this.ktrkLevel.Maximum = 3;
        this.ktrkLevel.Minimum = -20;
        this.ktrkLevel.Name = "ktrkLevel";
        this.ktrkLevel.Size = new System.Drawing.Size(330, 32);
        this.ktrkLevel.TabIndex = 8;
        this.ktrkLevel.Value = -20;
        this.ktrkLevel.ValueChanged += new System.EventHandler(this.ktrkLevel_ValueChanged);
        // 
        // kchkPeak
        // 
        this.kchkPeak.Location = new System.Drawing.Point(364, 312);
        this.kchkPeak.Name = "kchkPeak";
        this.kchkPeak.Size = new System.Drawing.Size(100, 24);
        this.kchkPeak.TabIndex = 9;
        this.kchkPeak.Values.Text = "Peak hold";
        this.kchkPeak.CheckedChanged += new System.EventHandler(this.kchkPeak_CheckedChanged);
        // 
        // kchkCapture
        // 
        this.kchkCapture.Location = new System.Drawing.Point(480, 312);
        this.kchkCapture.Name = "kchkCapture";
        this.kchkCapture.Size = new System.Drawing.Size(160, 24);
        this.kchkCapture.TabIndex = 10;
        this.kchkCapture.Values.Text = "Capture microphone";
        this.kchkCapture.CheckedChanged += new System.EventHandler(this.kchkCapture_CheckedChanged);
        // 
        // klblChannel
        // 
        this.klblChannel.Location = new System.Drawing.Point(660, 288);
        this.klblChannel.Name = "klblChannel";
        this.klblChannel.Size = new System.Drawing.Size(80, 20);
        this.klblChannel.TabIndex = 11;
        this.klblChannel.Values.Text = "Channel";
        // 
        // kcmbChannel
        // 
        this.kcmbChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.kcmbChannel.DropDownWidth = 140;
        this.kcmbChannel.Enabled = false;
        this.kcmbChannel.Location = new System.Drawing.Point(660, 312);
        this.kcmbChannel.Name = "kcmbChannel";
        this.kcmbChannel.Size = new System.Drawing.Size(140, 22);
        this.kcmbChannel.TabIndex = 12;
        this.kcmbChannel.SelectedIndexChanged += new System.EventHandler(this.kcmbChannel_SelectedIndexChanged);
        // 
        // klblOrientation
        // 
        this.klblOrientation.Location = new System.Drawing.Point(816, 288);
        this.klblOrientation.Name = "klblOrientation";
        this.klblOrientation.Size = new System.Drawing.Size(140, 20);
        this.klblOrientation.TabIndex = 13;
        this.klblOrientation.Values.Text = "Segment direction";
        // 
        // kcmbOrientation
        // 
        this.kcmbOrientation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.kcmbOrientation.DropDownWidth = 140;
        this.kcmbOrientation.Location = new System.Drawing.Point(816, 312);
        this.kcmbOrientation.Name = "kcmbOrientation";
        this.kcmbOrientation.Size = new System.Drawing.Size(140, 22);
        this.kcmbOrientation.TabIndex = 14;
        this.kcmbOrientation.SelectedIndexChanged += new System.EventHandler(this.kcmbOrientation_SelectedIndexChanged);
        // 
        // klblTheme
        // 
        this.klblTheme.Location = new System.Drawing.Point(16, 356);
        this.klblTheme.Name = "klblTheme";
        this.klblTheme.Size = new System.Drawing.Size(80, 20);
        this.klblTheme.TabIndex = 15;
        this.klblTheme.Values.Text = "Theme";
        // 
        // ktheme
        // 
        this.ktheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.ktheme.DropDownWidth = 280;
        this.ktheme.Location = new System.Drawing.Point(16, 380);
        this.ktheme.Name = "ktheme";
        this.ktheme.Size = new System.Drawing.Size(280, 22);
        this.ktheme.TabIndex = 16;
        // 
        // kbtnSilence
        // 
        this.kbtnSilence.Location = new System.Drawing.Point(312, 376);
        this.kbtnSilence.Name = "kbtnSilence";
        this.kbtnSilence.Size = new System.Drawing.Size(110, 28);
        this.kbtnSilence.TabIndex = 17;
        this.kbtnSilence.Values.Text = "Silence";
        this.kbtnSilence.Click += new System.EventHandler(this.kbtnSilence_Click);
        // 
        // kbtnZero
        // 
        this.kbtnZero.Location = new System.Drawing.Point(428, 376);
        this.kbtnZero.Name = "kbtnZero";
        this.kbtnZero.Size = new System.Drawing.Size(110, 28);
        this.kbtnZero.TabIndex = 18;
        this.kbtnZero.Values.Text = "0 VU";
        this.kbtnZero.Click += new System.EventHandler(this.kbtnZero_Click);
        // 
        // kpgMeter
        // 
        this.kpgMeter.Location = new System.Drawing.Point(16, 420);
        this.kpgMeter.Name = "kpgMeter";
        this.kpgMeter.Size = new System.Drawing.Size(1026, 300);
        this.kpgMeter.TabIndex = 19;
        // 
        // KryptonVuMeterDemo
        // 
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(1064, 736);
        this.Controls.Add(this.kpgMeter);
        this.Controls.Add(this.kbtnZero);
        this.Controls.Add(this.kbtnSilence);
        this.Controls.Add(this.ktheme);
        this.Controls.Add(this.klblTheme);
        this.Controls.Add(this.kcmbOrientation);
        this.Controls.Add(this.klblOrientation);
        this.Controls.Add(this.kcmbChannel);
        this.Controls.Add(this.klblChannel);
        this.Controls.Add(this.kchkCapture);
        this.Controls.Add(this.kchkPeak);
        this.Controls.Add(this.ktrkLevel);
        this.Controls.Add(this.klblLevel);
        this.Controls.Add(this.kvumDisabled);
        this.Controls.Add(this.klblDisabled);
        this.Controls.Add(this.kvumSegments);
        this.Controls.Add(this.klblSegments);
        this.Controls.Add(this.kvumAnalog);
        this.Controls.Add(this.klblAnalog);
        this.Controls.Add(this.klblInstructions);
        this.Name = "KryptonVuMeterDemo";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "VU Meter (#4655)";
        this.Load += new System.EventHandler(this.KryptonVuMeterDemo_Load);
        ((System.ComponentModel.ISupportInitialize)this.kcmbChannel).EndInit();
        ((System.ComponentModel.ISupportInitialize)this.kcmbOrientation).EndInit();
        ((System.ComponentModel.ISupportInitialize)this.ktheme).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private Krypton.Toolkit.KryptonLabel klblInstructions;
    private Krypton.Toolkit.KryptonLabel klblAnalog;
    private Krypton.Gauges.KryptonVuMeter kvumAnalog;
    private Krypton.Toolkit.KryptonLabel klblSegments;
    private Krypton.Gauges.KryptonVuMeter kvumSegments;
    private Krypton.Toolkit.KryptonLabel klblDisabled;
    private Krypton.Gauges.KryptonVuMeter kvumDisabled;
    private Krypton.Toolkit.KryptonLabel klblLevel;
    private Krypton.Toolkit.KryptonTrackBar ktrkLevel;
    private Krypton.Toolkit.KryptonCheckBox kchkPeak;
    private Krypton.Toolkit.KryptonCheckBox kchkCapture;
    private Krypton.Toolkit.KryptonLabel klblChannel;
    private Krypton.Toolkit.KryptonComboBox kcmbChannel;
    private Krypton.Toolkit.KryptonLabel klblOrientation;
    private Krypton.Toolkit.KryptonComboBox kcmbOrientation;
    private Krypton.Toolkit.KryptonLabel klblTheme;
    private Krypton.Toolkit.KryptonThemeComboBox ktheme;
    private Krypton.Toolkit.KryptonButton kbtnSilence;
    private Krypton.Toolkit.KryptonButton kbtnZero;
    private Krypton.Toolkit.KryptonPropertyGrid kpgMeter;
}

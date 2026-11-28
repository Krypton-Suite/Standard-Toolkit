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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KryptonVuMeterDemo));
            this.klblInstructions = new Krypton.Toolkit.KryptonLabel();
            this.klblAnalog = new Krypton.Toolkit.KryptonLabel();
            this.kvumAnalog = new Krypton.Gauges.KryptonVuMeter();
            this.klblSegments = new Krypton.Toolkit.KryptonLabel();
            this.kvumSegments = new Krypton.Gauges.KryptonVuMeter();
            this.klblDisabled = new Krypton.Toolkit.KryptonLabel();
            this.kvumDisabled = new Krypton.Gauges.KryptonVuMeter();
            this.klblLeft = new Krypton.Toolkit.KryptonLabel();
            this.kvumLeft = new Krypton.Gauges.KryptonVuMeter();
            this.klblRight = new Krypton.Toolkit.KryptonLabel();
            this.kvumRight = new Krypton.Gauges.KryptonVuMeter();
            this.klblLevel = new Krypton.Toolkit.KryptonLabel();
            this.ktrkLevel = new Krypton.Toolkit.KryptonTrackBar();
            this.klblBass = new Krypton.Toolkit.KryptonLabel();
            this.ktrkBass = new Krypton.Toolkit.KryptonTrackBar();
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
            this.kchkMusic = new Krypton.Toolkit.KryptonCheckBox();
            this.kchkStereo = new Krypton.Toolkit.KryptonCheckBox();
            this.klblLength = new Krypton.Toolkit.KryptonLabel();
            this.kcmbLength = new Krypton.Toolkit.KryptonComboBox();
            this.klblVerses = new Krypton.Toolkit.KryptonLabel();
            this.kcmbVerses = new Krypton.Toolkit.KryptonComboBox();
            this.klblBars = new Krypton.Toolkit.KryptonLabel();
            this.kcmbBars = new Krypton.Toolkit.KryptonComboBox();
            this.kpgMeter = new Krypton.Toolkit.KryptonPropertyGrid();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbChannel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbOrientation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ktheme)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbVerses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbBars)).BeginInit();
            this.SuspendLayout();
            // 
            // klblInstructions
            // 
            this.klblInstructions.Location = new System.Drawing.Point(12, 12);
            this.klblInstructions.Name = "klblInstructions";
            this.klblInstructions.Size = new System.Drawing.Size(1030, 58);
            this.klblInstructions.TabIndex = 0;
            this.klblInstructions.Values.Text = resources.GetString("klblInstructions.Values.Text");
            // 
            // klblAnalog
            // 
            this.klblAnalog.Location = new System.Drawing.Point(16, 72);
            this.klblAnalog.Name = "klblAnalog";
            this.klblAnalog.Size = new System.Drawing.Size(49, 20);
            this.klblAnalog.TabIndex = 1;
            this.klblAnalog.Values.Text = "Analog";
            // 
            // kvumAnalog
            // 
            this.kvumAnalog.Location = new System.Drawing.Point(16, 96);
            this.kvumAnalog.Name = "kvumAnalog";
            this.kvumAnalog.Size = new System.Drawing.Size(330, 180);
            this.kvumAnalog.TabIndex = 2;
            this.kvumAnalog.TabStop = false;
            this.kvumAnalog.ValueChanged += new System.EventHandler<Krypton.Gauges.VuMeterValueChangedEventArgs>(this.kvumAnalog_ValueChanged);
            this.kvumAnalog.CaptureFailed += new System.EventHandler<Krypton.Gauges.VuMeterCaptureFailedEventArgs>(this.kvumAnalog_CaptureFailed);
            // 
            // klblSegments
            // 
            this.klblSegments.Location = new System.Drawing.Point(364, 72);
            this.klblSegments.Name = "klblSegments";
            this.klblSegments.Size = new System.Drawing.Size(64, 20);
            this.klblSegments.TabIndex = 3;
            this.klblSegments.Values.Text = "Segments";
            // 
            // kvumSegments
            // 
            this.kvumSegments.Location = new System.Drawing.Point(364, 96);
            this.kvumSegments.Name = "kvumSegments";
            this.kvumSegments.Size = new System.Drawing.Size(330, 180);
            this.kvumSegments.TabIndex = 4;
            this.kvumSegments.TabStop = false;
            this.kvumSegments.Values.Style = Krypton.Gauges.VuMeterStyle.Segments;
            // 
            // klblDisabled
            // 
            this.klblDisabled.Location = new System.Drawing.Point(712, 72);
            this.klblDisabled.Name = "klblDisabled";
            this.klblDisabled.Size = new System.Drawing.Size(142, 20);
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
            this.kvumDisabled.TabStop = false;
            // 
            // klblLeft
            // 
            this.klblLeft.Location = new System.Drawing.Point(16, 286);
            this.klblLeft.Name = "klblLeft";
            this.klblLeft.Size = new System.Drawing.Size(80, 20);
            this.klblLeft.TabIndex = 22;
            this.klblLeft.Values.Text = "Left";
            // 
            // kvumLeft
            // 
            this.kvumLeft.Location = new System.Drawing.Point(16, 308);
            this.kvumLeft.Name = "kvumLeft";
            this.kvumLeft.Size = new System.Drawing.Size(508, 170);
            this.kvumLeft.TabIndex = 23;
            this.kvumLeft.TabStop = false;
            // 
            // klblRight
            // 
            this.klblRight.Location = new System.Drawing.Point(540, 286);
            this.klblRight.Name = "klblRight";
            this.klblRight.Size = new System.Drawing.Size(80, 20);
            this.klblRight.TabIndex = 24;
            this.klblRight.Values.Text = "Right";
            // 
            // kvumRight
            // 
            this.kvumRight.Location = new System.Drawing.Point(540, 308);
            this.kvumRight.Name = "kvumRight";
            this.kvumRight.Size = new System.Drawing.Size(508, 170);
            this.kvumRight.TabIndex = 25;
            this.kvumRight.TabStop = false;
            // 
            // klblLevel
            // 
            this.klblLevel.Location = new System.Drawing.Point(16, 488);
            this.klblLevel.Name = "klblLevel";
            this.klblLevel.Size = new System.Drawing.Size(480, 20);
            this.klblLevel.TabIndex = 7;
            this.klblLevel.Values.Text = "Target -20.0 VU";
            // 
            // ktrkLevel
            // 
            this.ktrkLevel.Location = new System.Drawing.Point(16, 512);
            this.ktrkLevel.Maximum = 3;
            this.ktrkLevel.Minimum = -20;
            this.ktrkLevel.Name = "ktrkLevel";
            this.ktrkLevel.Size = new System.Drawing.Size(150, 35);
            this.ktrkLevel.TabIndex = 8;
            this.ktrkLevel.Value = -20;
            this.ktrkLevel.ValueChanged += new System.EventHandler(this.ktrkLevel_ValueChanged);
            // 
            // klblBass
            // 
            this.klblBass.Location = new System.Drawing.Point(180, 488);
            this.klblBass.Name = "klblBass";
            this.klblBass.Size = new System.Drawing.Size(160, 20);
            this.klblBass.TabIndex = 28;
            this.klblBass.Values.Text = "Bass 50";
            // 
            // ktrkBass
            // 
            this.ktrkBass.Location = new System.Drawing.Point(180, 512);
            this.ktrkBass.Maximum = 100;
            this.ktrkBass.Name = "ktrkBass";
            this.ktrkBass.Size = new System.Drawing.Size(160, 35);
            this.ktrkBass.TabIndex = 29;
            this.ktrkBass.TickFrequency = 25;
            this.ktrkBass.Value = 50;
            this.ktrkBass.ValueChanged += new System.EventHandler(this.ktrkBass_ValueChanged);
            this.ktrkBass.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ktrkBass_MouseUp);
            // 
            // kchkPeak
            // 
            this.kchkPeak.Location = new System.Drawing.Point(364, 512);
            this.kchkPeak.Name = "kchkPeak";
            this.kchkPeak.Size = new System.Drawing.Size(77, 20);
            this.kchkPeak.TabIndex = 9;
            this.kchkPeak.Values.Text = "Peak hold";
            this.kchkPeak.CheckedChanged += new System.EventHandler(this.kchkPeak_CheckedChanged);
            // 
            // kchkCapture
            // 
            this.kchkCapture.Location = new System.Drawing.Point(480, 512);
            this.kchkCapture.Name = "kchkCapture";
            this.kchkCapture.Size = new System.Drawing.Size(136, 20);
            this.kchkCapture.TabIndex = 10;
            this.kchkCapture.Values.Text = "Capture microphone";
            this.kchkCapture.CheckedChanged += new System.EventHandler(this.kchkCapture_CheckedChanged);
            // 
            // klblChannel
            // 
            this.klblChannel.Location = new System.Drawing.Point(660, 488);
            this.klblChannel.Name = "klblChannel";
            this.klblChannel.Size = new System.Drawing.Size(55, 20);
            this.klblChannel.TabIndex = 11;
            this.klblChannel.Values.Text = "Channel";
            // 
            // kcmbChannel
            // 
            this.kcmbChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbChannel.DropDownWidth = 140;
            this.kcmbChannel.Enabled = false;
            this.kcmbChannel.Location = new System.Drawing.Point(660, 512);
            this.kcmbChannel.Name = "kcmbChannel";
            this.kcmbChannel.Size = new System.Drawing.Size(140, 22);
            this.kcmbChannel.TabIndex = 12;
            this.kcmbChannel.SelectedIndexChanged += new System.EventHandler(this.kcmbChannel_SelectedIndexChanged);
            // 
            // klblOrientation
            // 
            this.klblOrientation.Location = new System.Drawing.Point(816, 488);
            this.klblOrientation.Name = "klblOrientation";
            this.klblOrientation.Size = new System.Drawing.Size(111, 20);
            this.klblOrientation.TabIndex = 13;
            this.klblOrientation.Values.Text = "Segment direction";
            // 
            // kcmbOrientation
            // 
            this.kcmbOrientation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbOrientation.DropDownWidth = 140;
            this.kcmbOrientation.Location = new System.Drawing.Point(816, 512);
            this.kcmbOrientation.Name = "kcmbOrientation";
            this.kcmbOrientation.Size = new System.Drawing.Size(140, 22);
            this.kcmbOrientation.TabIndex = 14;
            this.kcmbOrientation.SelectedIndexChanged += new System.EventHandler(this.kcmbOrientation_SelectedIndexChanged);
            // 
            // klblTheme
            // 
            this.klblTheme.Location = new System.Drawing.Point(16, 556);
            this.klblTheme.Name = "klblTheme";
            this.klblTheme.Size = new System.Drawing.Size(48, 20);
            this.klblTheme.TabIndex = 15;
            this.klblTheme.Values.Text = "Theme";
            // 
            // ktheme
            // 
            this.ktheme.DropDownWidth = 280;
            this.ktheme.Location = new System.Drawing.Point(16, 580);
            this.ktheme.Name = "ktheme";
            this.ktheme.Size = new System.Drawing.Size(280, 22);
            this.ktheme.TabIndex = 16;
            // 
            // kbtnSilence
            // 
            this.kbtnSilence.Location = new System.Drawing.Point(312, 576);
            this.kbtnSilence.Name = "kbtnSilence";
            this.kbtnSilence.Size = new System.Drawing.Size(110, 28);
            this.kbtnSilence.TabIndex = 17;
            this.kbtnSilence.Values.Text = "Silence";
            this.kbtnSilence.Click += new System.EventHandler(this.kbtnSilence_Click);
            // 
            // kbtnZero
            // 
            this.kbtnZero.Location = new System.Drawing.Point(428, 576);
            this.kbtnZero.Name = "kbtnZero";
            this.kbtnZero.Size = new System.Drawing.Size(110, 28);
            this.kbtnZero.TabIndex = 18;
            this.kbtnZero.Values.Text = "0 VU";
            this.kbtnZero.Click += new System.EventHandler(this.kbtnZero_Click);
            // 
            // kchkMusic
            // 
            this.kchkMusic.Location = new System.Drawing.Point(548, 578);
            this.kchkMusic.Name = "kchkMusic";
            this.kchkMusic.Size = new System.Drawing.Size(81, 20);
            this.kchkMusic.TabIndex = 20;
            this.kchkMusic.Values.Text = "Play music";
            this.kchkMusic.CheckedChanged += new System.EventHandler(this.kchkMusic_CheckedChanged);
            // 
            // kchkStereo
            // 
            this.kchkStereo.Location = new System.Drawing.Point(708, 578);
            this.kchkStereo.Name = "kchkStereo";
            this.kchkStereo.Size = new System.Drawing.Size(78, 20);
            this.kchkStereo.TabIndex = 21;
            this.kchkStereo.Values.Text = "Stereo file";
            this.kchkStereo.CheckedChanged += new System.EventHandler(this.kchkStereo_CheckedChanged);
            // 
            // klblLength
            // 
            this.klblLength.Location = new System.Drawing.Point(830, 556);
            this.klblLength.Name = "klblLength";
            this.klblLength.Size = new System.Drawing.Size(48, 20);
            this.klblLength.TabIndex = 26;
            this.klblLength.Values.Text = "Length";
            // 
            // kcmbLength
            // 
            this.kcmbLength.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbLength.DropDownWidth = 140;
            this.kcmbLength.Location = new System.Drawing.Point(830, 578);
            this.kcmbLength.Name = "kcmbLength";
            this.kcmbLength.Size = new System.Drawing.Size(140, 22);
            this.kcmbLength.TabIndex = 27;
            this.kcmbLength.SelectedIndexChanged += new System.EventHandler(this.kcmbLength_SelectedIndexChanged);
            // 
            // klblVerses
            // 
            this.klblVerses.Location = new System.Drawing.Point(16, 612);
            this.klblVerses.Name = "klblVerses";
            this.klblVerses.Size = new System.Drawing.Size(48, 20);
            this.klblVerses.TabIndex = 30;
            this.klblVerses.Values.Text = "Verses";
            // 
            // kcmbVerses
            // 
            this.kcmbVerses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbVerses.DropDownWidth = 80;
            this.kcmbVerses.Location = new System.Drawing.Point(16, 632);
            this.kcmbVerses.Name = "kcmbVerses";
            this.kcmbVerses.Size = new System.Drawing.Size(80, 22);
            this.kcmbVerses.TabIndex = 31;
            this.kcmbVerses.SelectedIndexChanged += new System.EventHandler(this.kcmbStructure_SelectedIndexChanged);
            // 
            // klblBars
            // 
            this.klblBars.Location = new System.Drawing.Point(112, 612);
            this.klblBars.Name = "klblBars";
            this.klblBars.Size = new System.Drawing.Size(88, 20);
            this.klblBars.TabIndex = 32;
            this.klblBars.Values.Text = "Bars / verse";
            // 
            // kcmbBars
            // 
            this.kcmbBars.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbBars.DropDownWidth = 80;
            this.kcmbBars.Location = new System.Drawing.Point(112, 632);
            this.kcmbBars.Name = "kcmbBars";
            this.kcmbBars.Size = new System.Drawing.Size(80, 22);
            this.kcmbBars.TabIndex = 33;
            this.kcmbBars.SelectedIndexChanged += new System.EventHandler(this.kcmbStructure_SelectedIndexChanged);
            // 
            // kpgMeter
            // 
            this.kpgMeter.Location = new System.Drawing.Point(16, 664);
            this.kpgMeter.Name = "kpgMeter";
            this.kpgMeter.Padding = new System.Windows.Forms.Padding(1);
            this.kpgMeter.Size = new System.Drawing.Size(1026, 206);
            this.kpgMeter.TabIndex = 19;
            // 
            // KryptonVuMeterDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 890);
            this.Controls.Add(this.kcmbBars);
            this.Controls.Add(this.klblBars);
            this.Controls.Add(this.kcmbVerses);
            this.Controls.Add(this.klblVerses);
            this.Controls.Add(this.kpgMeter);
            this.Controls.Add(this.kcmbLength);
            this.Controls.Add(this.klblLength);
            this.Controls.Add(this.kchkStereo);
            this.Controls.Add(this.kchkMusic);
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
            this.Controls.Add(this.ktrkBass);
            this.Controls.Add(this.klblBass);
            this.Controls.Add(this.ktrkLevel);
            this.Controls.Add(this.klblLevel);
            this.Controls.Add(this.kvumRight);
            this.Controls.Add(this.klblRight);
            this.Controls.Add(this.kvumLeft);
            this.Controls.Add(this.klblLeft);
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
            ((System.ComponentModel.ISupportInitialize)(this.kcmbChannel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbOrientation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ktheme)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbVerses)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbBars)).EndInit();
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
    private Krypton.Toolkit.KryptonLabel klblLeft;
    private Krypton.Gauges.KryptonVuMeter kvumLeft;
    private Krypton.Toolkit.KryptonLabel klblRight;
    private Krypton.Gauges.KryptonVuMeter kvumRight;
    private Krypton.Toolkit.KryptonLabel klblLevel;
    private Krypton.Toolkit.KryptonTrackBar ktrkLevel;
    private Krypton.Toolkit.KryptonLabel klblBass;
    private Krypton.Toolkit.KryptonTrackBar ktrkBass;
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
    private Krypton.Toolkit.KryptonCheckBox kchkMusic;
    private Krypton.Toolkit.KryptonCheckBox kchkStereo;
    private Krypton.Toolkit.KryptonLabel klblLength;
    private Krypton.Toolkit.KryptonComboBox kcmbLength;
    private Krypton.Toolkit.KryptonLabel klblVerses;
    private Krypton.Toolkit.KryptonComboBox kcmbVerses;
    private Krypton.Toolkit.KryptonLabel klblBars;
    private Krypton.Toolkit.KryptonComboBox kcmbBars;
    private Krypton.Toolkit.KryptonPropertyGrid kpgMeter;
}

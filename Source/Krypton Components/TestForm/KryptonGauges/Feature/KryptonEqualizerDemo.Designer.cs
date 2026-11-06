#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, tobitege, Lesandro, KamaniAR, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

partial class KryptonEqualizerDemo
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
        this.klblBars = new Krypton.Toolkit.KryptonLabel();
        this.keqBars = new Krypton.Gauges.KryptonEqualizer();
        this.klblSegments = new Krypton.Toolkit.KryptonLabel();
        this.keqSegments = new Krypton.Gauges.KryptonEqualizer();
        this.klblDisabled = new Krypton.Toolkit.KryptonLabel();
        this.keqDisabled = new Krypton.Gauges.KryptonEqualizer();
        this.kchkSimulate = new Krypton.Toolkit.KryptonCheckBox();
        this.kchkMusic = new Krypton.Toolkit.KryptonCheckBox();
        this.kchkCapture = new Krypton.Toolkit.KryptonCheckBox();
        this.kchkPeak = new Krypton.Toolkit.KryptonCheckBox();
        this.kchkInteractive = new Krypton.Toolkit.KryptonCheckBox();
        this.kchkSegments = new Krypton.Toolkit.KryptonCheckBox();
        this.klblChannel = new Krypton.Toolkit.KryptonLabel();
        this.kcmbChannel = new Krypton.Toolkit.KryptonComboBox();
        this.klblOrientation = new Krypton.Toolkit.KryptonLabel();
        this.kcmbOrientation = new Krypton.Toolkit.KryptonComboBox();
        this.klblBands = new Krypton.Toolkit.KryptonLabel();
        this.kcmbBands = new Krypton.Toolkit.KryptonComboBox();
        this.klblTheme = new Krypton.Toolkit.KryptonLabel();
        this.ktheme = new Krypton.Toolkit.KryptonThemeComboBox();
        this.kbtnResetGains = new Krypton.Toolkit.KryptonButton();
        this.kbtnSilence = new Krypton.Toolkit.KryptonButton();
        this.klblStatus = new Krypton.Toolkit.KryptonLabel();
        this.kpgEqualizer = new Krypton.Toolkit.KryptonPropertyGrid();
        ((System.ComponentModel.ISupportInitialize)(this.kcmbChannel)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.kcmbOrientation)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.kcmbBands)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.ktheme)).BeginInit();
        this.SuspendLayout();
        // 
        // klblInstructions
        // 
        this.klblInstructions.Location = new System.Drawing.Point(12, 12);
        this.klblInstructions.Name = "klblInstructions";
        this.klblInstructions.Size = new System.Drawing.Size(980, 40);
        this.klblInstructions.TabIndex = 0;
        this.klblInstructions.Values.Text = "Issue #4660: drag gain thumbs, Simulate for synthetic levels, Play music for the" +
    " shared generated tune (FFT into bands), or Capture the microphone. Capture and" +
    " music update levels only — gains stay interactive.";
        // 
        // klblBars
        // 
        this.klblBars.Location = new System.Drawing.Point(12, 60);
        this.klblBars.Name = "klblBars";
        this.klblBars.Size = new System.Drawing.Size(120, 20);
        this.klblBars.TabIndex = 1;
        this.klblBars.Values.Text = "Bars (interactive)";
        // 
        // keqBars
        // 
        this.keqBars.Location = new System.Drawing.Point(12, 86);
        this.keqBars.Name = "keqBars";
        this.keqBars.Size = new System.Drawing.Size(520, 180);
        this.keqBars.TabIndex = 2;
        this.keqBars.GainChanged += new System.EventHandler<Krypton.Gauges.EqualizerGainChangedEventArgs>(this.keqBars_GainChanged);
        this.keqBars.CaptureFailed += new System.EventHandler<Krypton.Gauges.EqualizerCaptureFailedEventArgs>(this.keqBars_CaptureFailed);
        // 
        // klblSegments
        // 
        this.klblSegments.Location = new System.Drawing.Point(550, 60);
        this.klblSegments.Name = "klblSegments";
        this.klblSegments.Size = new System.Drawing.Size(80, 20);
        this.klblSegments.TabIndex = 3;
        this.klblSegments.Values.Text = "Segments";
        // 
        // keqSegments
        // 
        this.keqSegments.Location = new System.Drawing.Point(550, 86);
        this.keqSegments.Name = "keqSegments";
        this.keqSegments.Size = new System.Drawing.Size(280, 180);
        this.keqSegments.TabIndex = 4;
        this.keqSegments.Values.Style = Krypton.Gauges.EqualizerStyle.Segments;
        // 
        // klblDisabled
        // 
        this.klblDisabled.Location = new System.Drawing.Point(848, 60);
        this.klblDisabled.Name = "klblDisabled";
        this.klblDisabled.Size = new System.Drawing.Size(60, 20);
        this.klblDisabled.TabIndex = 5;
        this.klblDisabled.Values.Text = "Disabled";
        // 
        // keqDisabled
        // 
        this.keqDisabled.Enabled = false;
        this.keqDisabled.Location = new System.Drawing.Point(848, 86);
        this.keqDisabled.Name = "keqDisabled";
        this.keqDisabled.Size = new System.Drawing.Size(220, 180);
        this.keqDisabled.TabIndex = 6;
        // 
        // kchkSimulate
        // 
        this.kchkSimulate.Location = new System.Drawing.Point(12, 280);
        this.kchkSimulate.Name = "kchkSimulate";
        this.kchkSimulate.Size = new System.Drawing.Size(80, 20);
        this.kchkSimulate.TabIndex = 7;
        this.kchkSimulate.Values.Text = "Simulate";
        this.kchkSimulate.CheckedChanged += new System.EventHandler(this.kchkSimulate_CheckedChanged);
        // 
        // kchkMusic
        // 
        this.kchkMusic.Location = new System.Drawing.Point(100, 280);
        this.kchkMusic.Name = "kchkMusic";
        this.kchkMusic.Size = new System.Drawing.Size(100, 20);
        this.kchkMusic.TabIndex = 8;
        this.kchkMusic.Values.Text = "Play music";
        this.kchkMusic.CheckedChanged += new System.EventHandler(this.kchkMusic_CheckedChanged);
        // 
        // kchkCapture
        // 
        this.kchkCapture.Location = new System.Drawing.Point(210, 280);
        this.kchkCapture.Name = "kchkCapture";
        this.kchkCapture.Size = new System.Drawing.Size(80, 20);
        this.kchkCapture.TabIndex = 9;
        this.kchkCapture.Values.Text = "Capture";
        this.kchkCapture.CheckedChanged += new System.EventHandler(this.kchkCapture_CheckedChanged);
        // 
        // kchkPeak
        // 
        this.kchkPeak.Location = new System.Drawing.Point(300, 280);
        this.kchkPeak.Name = "kchkPeak";
        this.kchkPeak.Size = new System.Drawing.Size(90, 20);
        this.kchkPeak.TabIndex = 10;
        this.kchkPeak.Values.Text = "Peak hold";
        this.kchkPeak.CheckedChanged += new System.EventHandler(this.kchkPeak_CheckedChanged);
        // 
        // kchkInteractive
        // 
        this.kchkInteractive.Checked = true;
        this.kchkInteractive.CheckState = System.Windows.Forms.CheckState.Checked;
        this.kchkInteractive.Location = new System.Drawing.Point(400, 280);
        this.kchkInteractive.Name = "kchkInteractive";
        this.kchkInteractive.Size = new System.Drawing.Size(90, 20);
        this.kchkInteractive.TabIndex = 11;
        this.kchkInteractive.Values.Text = "Interactive";
        this.kchkInteractive.CheckedChanged += new System.EventHandler(this.kchkInteractive_CheckedChanged);
        // 
        // kchkSegments
        // 
        this.kchkSegments.Location = new System.Drawing.Point(500, 280);
        this.kchkSegments.Name = "kchkSegments";
        this.kchkSegments.Size = new System.Drawing.Size(140, 20);
        this.kchkSegments.TabIndex = 12;
        this.kchkSegments.Values.Text = "Bars as segments";
        this.kchkSegments.CheckedChanged += new System.EventHandler(this.kchkSegments_CheckedChanged);
        // 
        // klblChannel
        // 
        this.klblChannel.Location = new System.Drawing.Point(12, 314);
        this.klblChannel.Name = "klblChannel";
        this.klblChannel.Size = new System.Drawing.Size(55, 20);
        this.klblChannel.TabIndex = 12;
        this.klblChannel.Values.Text = "Channel";
        // 
        // kcmbChannel
        // 
        this.kcmbChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.kcmbChannel.DropDownWidth = 121;
        this.kcmbChannel.Location = new System.Drawing.Point(80, 312);
        this.kcmbChannel.Name = "kcmbChannel";
        this.kcmbChannel.Size = new System.Drawing.Size(100, 22);
        this.kcmbChannel.TabIndex = 13;
        this.kcmbChannel.SelectedIndexChanged += new System.EventHandler(this.kcmbChannel_SelectedIndexChanged);
        // 
        // klblOrientation
        // 
        this.klblOrientation.Location = new System.Drawing.Point(200, 314);
        this.klblOrientation.Name = "klblOrientation";
        this.klblOrientation.Size = new System.Drawing.Size(70, 20);
        this.klblOrientation.TabIndex = 14;
        this.klblOrientation.Values.Text = "Orientation";
        // 
        // kcmbOrientation
        // 
        this.kcmbOrientation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.kcmbOrientation.DropDownWidth = 121;
        this.kcmbOrientation.Location = new System.Drawing.Point(280, 312);
        this.kcmbOrientation.Name = "kcmbOrientation";
        this.kcmbOrientation.Size = new System.Drawing.Size(110, 22);
        this.kcmbOrientation.TabIndex = 15;
        this.kcmbOrientation.SelectedIndexChanged += new System.EventHandler(this.kcmbOrientation_SelectedIndexChanged);
        // 
        // klblBands
        // 
        this.klblBands.Location = new System.Drawing.Point(410, 314);
        this.klblBands.Name = "klblBands";
        this.klblBands.Size = new System.Drawing.Size(40, 20);
        this.klblBands.TabIndex = 16;
        this.klblBands.Values.Text = "Bands";
        // 
        // kcmbBands
        // 
        this.kcmbBands.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.kcmbBands.DropDownWidth = 80;
        this.kcmbBands.Location = new System.Drawing.Point(460, 312);
        this.kcmbBands.Name = "kcmbBands";
        this.kcmbBands.Size = new System.Drawing.Size(70, 22);
        this.kcmbBands.TabIndex = 17;
        this.kcmbBands.SelectedIndexChanged += new System.EventHandler(this.kcmbBands_SelectedIndexChanged);
        // 
        // klblTheme
        // 
        this.klblTheme.Location = new System.Drawing.Point(550, 314);
        this.klblTheme.Name = "klblTheme";
        this.klblTheme.Size = new System.Drawing.Size(45, 20);
        this.klblTheme.TabIndex = 18;
        this.klblTheme.Values.Text = "Theme";
        // 
        // ktheme
        // 
        this.ktheme.DropDownWidth = 220;
        this.ktheme.Location = new System.Drawing.Point(605, 312);
        this.ktheme.Name = "ktheme";
        this.ktheme.Size = new System.Drawing.Size(220, 22);
        this.ktheme.TabIndex = 19;
        // 
        // kbtnResetGains
        // 
        this.kbtnResetGains.Location = new System.Drawing.Point(12, 350);
        this.kbtnResetGains.Name = "kbtnResetGains";
        this.kbtnResetGains.Size = new System.Drawing.Size(110, 28);
        this.kbtnResetGains.TabIndex = 20;
        this.kbtnResetGains.Values.Text = "Reset gains";
        this.kbtnResetGains.Click += new System.EventHandler(this.kbtnResetGains_Click);
        // 
        // kbtnSilence
        // 
        this.kbtnSilence.Location = new System.Drawing.Point(130, 350);
        this.kbtnSilence.Name = "kbtnSilence";
        this.kbtnSilence.Size = new System.Drawing.Size(90, 28);
        this.kbtnSilence.TabIndex = 21;
        this.kbtnSilence.Values.Text = "Silence";
        this.kbtnSilence.Click += new System.EventHandler(this.kbtnSilence_Click);
        // 
        // klblStatus
        // 
        this.klblStatus.Location = new System.Drawing.Point(240, 354);
        this.klblStatus.Name = "klblStatus";
        this.klblStatus.Size = new System.Drawing.Size(600, 20);
        this.klblStatus.TabIndex = 22;
        this.klblStatus.Values.Text = "Status";
        // 
        // kpgEqualizer
        // 
        this.kpgEqualizer.Location = new System.Drawing.Point(12, 392);
        this.kpgEqualizer.Name = "kpgEqualizer";
        this.kpgEqualizer.Size = new System.Drawing.Size(1056, 240);
        this.kpgEqualizer.TabIndex = 23;
        // 
        // KryptonEqualizerDemo
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(1080, 644);
        this.Controls.Add(this.kpgEqualizer);
        this.Controls.Add(this.klblStatus);
        this.Controls.Add(this.kbtnSilence);
        this.Controls.Add(this.kbtnResetGains);
        this.Controls.Add(this.ktheme);
        this.Controls.Add(this.klblTheme);
        this.Controls.Add(this.kcmbBands);
        this.Controls.Add(this.klblBands);
        this.Controls.Add(this.kcmbOrientation);
        this.Controls.Add(this.klblOrientation);
        this.Controls.Add(this.kcmbChannel);
        this.Controls.Add(this.klblChannel);
        this.Controls.Add(this.kchkSegments);
        this.Controls.Add(this.kchkInteractive);
        this.Controls.Add(this.kchkPeak);
        this.Controls.Add(this.kchkCapture);
        this.Controls.Add(this.kchkMusic);
        this.Controls.Add(this.kchkSimulate);
        this.Controls.Add(this.keqDisabled);
        this.Controls.Add(this.klblDisabled);
        this.Controls.Add(this.keqSegments);
        this.Controls.Add(this.klblSegments);
        this.Controls.Add(this.keqBars);
        this.Controls.Add(this.klblBars);
        this.Controls.Add(this.klblInstructions);
        this.Name = "KryptonEqualizerDemo";
        this.Text = "Krypton Equalizer (#4660)";
        this.Load += new System.EventHandler(this.KryptonEqualizerDemo_Load);
        this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.KryptonEqualizerDemo_FormClosed);
        ((System.ComponentModel.ISupportInitialize)(this.kcmbChannel)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.kcmbOrientation)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.kcmbBands)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.ktheme)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private Krypton.Toolkit.KryptonLabel klblInstructions;
    private Krypton.Toolkit.KryptonLabel klblBars;
    private Krypton.Gauges.KryptonEqualizer keqBars;
    private Krypton.Toolkit.KryptonLabel klblSegments;
    private Krypton.Gauges.KryptonEqualizer keqSegments;
    private Krypton.Toolkit.KryptonLabel klblDisabled;
    private Krypton.Gauges.KryptonEqualizer keqDisabled;
    private Krypton.Toolkit.KryptonCheckBox kchkSimulate;
    private Krypton.Toolkit.KryptonCheckBox kchkMusic;
    private Krypton.Toolkit.KryptonCheckBox kchkCapture;
    private Krypton.Toolkit.KryptonCheckBox kchkPeak;
    private Krypton.Toolkit.KryptonCheckBox kchkInteractive;
    private Krypton.Toolkit.KryptonCheckBox kchkSegments;
    private Krypton.Toolkit.KryptonLabel klblChannel;
    private Krypton.Toolkit.KryptonComboBox kcmbChannel;
    private Krypton.Toolkit.KryptonLabel klblOrientation;
    private Krypton.Toolkit.KryptonComboBox kcmbOrientation;
    private Krypton.Toolkit.KryptonLabel klblBands;
    private Krypton.Toolkit.KryptonComboBox kcmbBands;
    private Krypton.Toolkit.KryptonLabel klblTheme;
    private Krypton.Toolkit.KryptonThemeComboBox ktheme;
    private Krypton.Toolkit.KryptonButton kbtnResetGains;
    private Krypton.Toolkit.KryptonButton kbtnSilence;
    private Krypton.Toolkit.KryptonLabel klblStatus;
    private Krypton.Toolkit.KryptonPropertyGrid kpgEqualizer;
}

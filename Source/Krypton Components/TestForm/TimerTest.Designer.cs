#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2026 - 2026. All rights reserved. 
 *  
 */
#endregion

namespace TestForm
{
    partial class TimerTest
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
            this.components = new System.ComponentModel.Container();
            this.kpnlMain = new Krypton.Toolkit.KryptonPanel();
            this.kscMain = new Krypton.Toolkit.KryptonSplitContainer();
            this.kpnlLeft = new Krypton.Toolkit.KryptonPanel();
            this.kpgbConfiguration = new Krypton.Toolkit.KryptonGroupBox();
            this.kpnlConfig = new Krypton.Toolkit.KryptonPanel();
            this.kcmbPaletteMode = new Krypton.Toolkit.KryptonComboBox();
            this.klblPaletteMode = new Krypton.Toolkit.KryptonLabel();
            this.kchkUpdateProgressBar = new Krypton.Toolkit.KryptonCheckBox();
            this.ktxtInterval = new Krypton.Toolkit.KryptonTextBox();
            this.klblIntervalInput = new Krypton.Toolkit.KryptonLabel();
            this.knudInterval = new Krypton.Toolkit.KryptonNumericUpDown();
            this.klblInterval = new Krypton.Toolkit.KryptonLabel();
            this.kpgbStatus = new Krypton.Toolkit.KryptonGroupBox();
            this.kpnlStatus = new Krypton.Toolkit.KryptonPanel();
            this.kprogressBar = new Krypton.Toolkit.KryptonProgressBar();
            this.klblLastTick = new Krypton.Toolkit.KryptonLabel();
            this.klblElapsedTime = new Krypton.Toolkit.KryptonLabel();
            this.klblTickCount = new Krypton.Toolkit.KryptonLabel();
            this.klblEnabled = new Krypton.Toolkit.KryptonLabel();
            this.klblIntervalStatus = new Krypton.Toolkit.KryptonLabel();
            this.klblStatus = new Krypton.Toolkit.KryptonLabel();
            this.kpgbControls = new Krypton.Toolkit.KryptonGroupBox();
            this.kpnlControls = new Krypton.Toolkit.KryptonPanel();
            this.kbtnClearEvents = new Krypton.Toolkit.KryptonButton();
            this.kbtnReset = new Krypton.Toolkit.KryptonButton();
            this.kbtnStop = new Krypton.Toolkit.KryptonButton();
            this.kbtnStart = new Krypton.Toolkit.KryptonButton();
            this.kpnlRight = new Krypton.Toolkit.KryptonPanel();
            this.kpgbEvents = new Krypton.Toolkit.KryptonGroupBox();
            this.kpnlEvents = new Krypton.Toolkit.KryptonPanel();
            this.klstEvents = new Krypton.Toolkit.KryptonListBox();
            this.kpnlBottom = new Krypton.Toolkit.KryptonPanel();
            this.kbtnClose = new Krypton.Toolkit.KryptonButton();
            this.kryptonBorderEdge1 = new Krypton.Toolkit.KryptonBorderEdge();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).BeginInit();
            this.kpnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kscMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kscMain.Panel1)).BeginInit();
            this.kscMain.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kscMain.Panel2)).BeginInit();
            this.kscMain.Panel2.SuspendLayout();
            this.kscMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlLeft)).BeginInit();
            this.kpnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpgbConfiguration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kpgbConfiguration.Panel)).BeginInit();
            this.kpgbConfiguration.Panel.SuspendLayout();
            this.kpgbConfiguration.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlConfig)).BeginInit();
            this.kpnlConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbPaletteMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kpgbStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kpgbStatus.Panel)).BeginInit();
            this.kpgbStatus.Panel.SuspendLayout();
            this.kpgbStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlStatus)).BeginInit();
            this.kpnlStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpgbControls)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kpgbControls.Panel)).BeginInit();
            this.kpgbControls.Panel.SuspendLayout();
            this.kpgbControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlControls)).BeginInit();
            this.kpnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlRight)).BeginInit();
            this.kpnlRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpgbEvents)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kpgbEvents.Panel)).BeginInit();
            this.kpgbEvents.Panel.SuspendLayout();
            this.kpgbEvents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlEvents)).BeginInit();
            this.kpnlEvents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlBottom)).BeginInit();
            this.kpnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // kpnlMain
            // 
            this.kpnlMain.Controls.Add(this.kscMain);
            this.kpnlMain.Controls.Add(this.kpnlBottom);
            this.kpnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlMain.Location = new System.Drawing.Point(0, 0);
            this.kpnlMain.Name = "kpnlMain";
            this.kpnlMain.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelClient;
            this.kpnlMain.Size = new System.Drawing.Size(900, 600);
            this.kpnlMain.TabIndex = 0;
            // 
            // kscMain
            // 
            this.kscMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kscMain.Location = new System.Drawing.Point(0, 0);
            this.kscMain.Name = "kscMain";
            this.kscMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // kscMain.Panel1
            // 
            this.kscMain.Panel1.Controls.Add(this.kpnlLeft);
            // 
            // kscMain.Panel2
            // 
            this.kscMain.Panel2.Controls.Add(this.kpnlRight);
            this.kscMain.Size = new System.Drawing.Size(900, 550);
            this.kscMain.SplitterDistance = 250;
            this.kscMain.TabIndex = 0;
            // 
            // kpnlLeft
            // 
            this.kpnlLeft.Controls.Add(this.kpgbConfiguration);
            this.kpnlLeft.Controls.Add(this.kpgbStatus);
            this.kpnlLeft.Controls.Add(this.kpgbControls);
            this.kpnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlLeft.Location = new System.Drawing.Point(0, 0);
            this.kpnlLeft.Name = "kpnlLeft";
            this.kpnlLeft.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelClient;
            this.kpnlLeft.Size = new System.Drawing.Size(900, 250);
            this.kpnlLeft.TabIndex = 0;
            // 
            // kpgbConfiguration
            // 
            this.kpgbConfiguration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpgbConfiguration.Location = new System.Drawing.Point(0, 0);
            this.kpgbConfiguration.Name = "kpgbConfiguration";
            // 
            // kpgbConfiguration.Panel
            // 
            this.kpgbConfiguration.Panel.Controls.Add(this.kpnlConfig);
            this.kpgbConfiguration.Size = new System.Drawing.Size(900, 120);
            this.kpgbConfiguration.TabIndex = 0;
            this.kpgbConfiguration.Values.Heading = "Configuration";
            // 
            // kpnlConfig
            // 
            this.kpnlConfig.Controls.Add(this.kcmbPaletteMode);
            this.kpnlConfig.Controls.Add(this.klblPaletteMode);
            this.kpnlConfig.Controls.Add(this.kchkUpdateProgressBar);
            this.kpnlConfig.Controls.Add(this.ktxtInterval);
            this.kpnlConfig.Controls.Add(this.klblIntervalInput);
            this.kpnlConfig.Controls.Add(this.knudInterval);
            this.kpnlConfig.Controls.Add(this.klblInterval);
            this.kpnlConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlConfig.Location = new System.Drawing.Point(0, 0);
            this.kpnlConfig.Name = "kpnlConfig";
            this.kpnlConfig.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelClient;
            this.kpnlConfig.Size = new System.Drawing.Size(896, 95);
            this.kpnlConfig.TabIndex = 0;
            // 
            // kcmbPaletteMode
            // 
            this.kcmbPaletteMode.DropDownWidth = 200;
            this.kcmbPaletteMode.Location = new System.Drawing.Point(120, 60);
            this.kcmbPaletteMode.Name = "kcmbPaletteMode";
            this.kcmbPaletteMode.Size = new System.Drawing.Size(200, 25);
            this.kcmbPaletteMode.TabIndex = 6;
            this.kcmbPaletteMode.SelectedIndexChanged += new System.EventHandler(this.kcmbPaletteMode_SelectedIndexChanged);
            // 
            // klblPaletteMode
            // 
            this.klblPaletteMode.Location = new System.Drawing.Point(10, 62);
            this.klblPaletteMode.Name = "klblPaletteMode";
            this.klblPaletteMode.Size = new System.Drawing.Size(100, 23);
            this.klblPaletteMode.TabIndex = 5;
            this.klblPaletteMode.Values.Text = "Palette Mode:";
            // 
            // kchkUpdateProgressBar
            // 
            this.kchkUpdateProgressBar.Location = new System.Drawing.Point(400, 30);
            this.kchkUpdateProgressBar.Name = "kchkUpdateProgressBar";
            this.kchkUpdateProgressBar.Size = new System.Drawing.Size(200, 23);
            this.kchkUpdateProgressBar.TabIndex = 4;
            this.kchkUpdateProgressBar.Values.Text = "Update Progress Bar";
            this.kchkUpdateProgressBar.CheckedChanged += new System.EventHandler(this.kchkUpdateProgressBar_CheckedChanged);
            // 
            // ktxtInterval
            // 
            this.ktxtInterval.Location = new System.Drawing.Point(250, 30);
            this.ktxtInterval.Name = "ktxtInterval";
            this.ktxtInterval.Size = new System.Drawing.Size(100, 25);
            this.ktxtInterval.TabIndex = 3;
            this.ktxtInterval.Text = "1000";
            this.ktxtInterval.TextChanged += new System.EventHandler(this.ktxtInterval_TextChanged);
            // 
            // klblIntervalInput
            // 
            this.klblIntervalInput.Location = new System.Drawing.Point(200, 32);
            this.klblIntervalInput.Name = "klblIntervalInput";
            this.klblIntervalInput.Size = new System.Drawing.Size(50, 23);
            this.klblIntervalInput.TabIndex = 2;
            this.klblIntervalInput.Values.Text = "Text:";
            // 
            // knudInterval
            // 
            this.knudInterval.Location = new System.Drawing.Point(120, 30);
            this.knudInterval.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.knudInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.knudInterval.Name = "knudInterval";
            this.knudInterval.Size = new System.Drawing.Size(80, 25);
            this.knudInterval.TabIndex = 1;
            this.knudInterval.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.knudInterval.ValueChanged += new System.EventHandler(this.knudInterval_ValueChanged);
            // 
            // klblInterval
            // 
            this.klblInterval.Location = new System.Drawing.Point(10, 32);
            this.klblInterval.Name = "klblInterval";
            this.klblInterval.Size = new System.Drawing.Size(100, 23);
            this.klblInterval.TabIndex = 0;
            this.klblInterval.Values.Text = "Interval (ms):";
            // 
            // kpgbStatus
            // 
            this.kpgbStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kpgbStatus.Location = new System.Drawing.Point(0, 120);
            this.kpgbStatus.Name = "kpgbStatus";
            // 
            // kpgbStatus.Panel
            // 
            this.kpgbStatus.Panel.Controls.Add(this.kpnlStatus);
            this.kpgbStatus.Size = new System.Drawing.Size(900, 80);
            this.kpgbStatus.TabIndex = 1;
            this.kpgbStatus.Values.Heading = "Status";
            // 
            // kpnlStatus
            // 
            this.kpnlStatus.Controls.Add(this.kprogressBar);
            this.kpnlStatus.Controls.Add(this.klblLastTick);
            this.kpnlStatus.Controls.Add(this.klblElapsedTime);
            this.kpnlStatus.Controls.Add(this.klblTickCount);
            this.kpnlStatus.Controls.Add(this.klblEnabled);
            this.kpnlStatus.Controls.Add(this.klblIntervalStatus);
            this.kpnlStatus.Controls.Add(this.klblStatus);
            this.kpnlStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlStatus.Location = new System.Drawing.Point(0, 0);
            this.kpnlStatus.Name = "kpnlStatus";
            this.kpnlStatus.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelClient;
            this.kpnlStatus.Size = new System.Drawing.Size(896, 55);
            this.kpnlStatus.TabIndex = 0;
            // 
            // kprogressBar
            // 
            this.kprogressBar.Location = new System.Drawing.Point(500, 30);
            this.kprogressBar.Name = "kprogressBar";
            this.kprogressBar.Size = new System.Drawing.Size(200, 20);
            this.kprogressBar.TabIndex = 6;
            this.kprogressBar.Visible = false;
            // 
            // klblLastTick
            // 
            this.klblLastTick.Location = new System.Drawing.Point(500, 5);
            this.klblLastTick.Name = "klblLastTick";
            this.klblLastTick.Size = new System.Drawing.Size(200, 23);
            this.klblLastTick.TabIndex = 5;
            this.klblLastTick.Values.Text = "Last Tick: --";
            // 
            // klblElapsedTime
            // 
            this.klblElapsedTime.Location = new System.Drawing.Point(250, 30);
            this.klblElapsedTime.Name = "klblElapsedTime";
            this.klblElapsedTime.Size = new System.Drawing.Size(200, 23);
            this.klblElapsedTime.TabIndex = 4;
            this.klblElapsedTime.Values.Text = "Elapsed Time: 00:00:00.000";
            // 
            // klblTickCount
            // 
            this.klblTickCount.Location = new System.Drawing.Point(250, 5);
            this.klblTickCount.Name = "klblTickCount";
            this.klblTickCount.Size = new System.Drawing.Size(200, 23);
            this.klblTickCount.TabIndex = 3;
            this.klblTickCount.Values.Text = "Tick Count: 0";
            // 
            // klblEnabled
            // 
            this.klblEnabled.Location = new System.Drawing.Point(10, 30);
            this.klblEnabled.Name = "klblEnabled";
            this.klblEnabled.Size = new System.Drawing.Size(200, 23);
            this.klblEnabled.TabIndex = 2;
            this.klblEnabled.Values.Text = "Enabled: False";
            // 
            // klblIntervalStatus
            // 
            this.klblIntervalStatus.Location = new System.Drawing.Point(10, 5);
            this.klblIntervalStatus.Name = "klblIntervalStatus";
            this.klblIntervalStatus.Size = new System.Drawing.Size(200, 23);
            this.klblIntervalStatus.TabIndex = 1;
            this.klblIntervalStatus.Values.Text = "Interval: 1000 ms";
            // 
            // klblStatus
            // 
            // 
            // kpgbControls
            // 
            this.kpgbControls.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kpgbControls.Location = new System.Drawing.Point(0, 200);
            this.kpgbControls.Name = "kpgbControls";
            // 
            // kpgbControls.Panel
            // 
            this.kpgbControls.Panel.Controls.Add(this.kpnlControls);
            this.kpgbControls.Size = new System.Drawing.Size(900, 50);
            this.kpgbControls.TabIndex = 2;
            this.kpgbControls.Values.Heading = "Controls";
            // 
            // kpnlControls
            // 
            this.kpnlControls.Controls.Add(this.kbtnClearEvents);
            this.kpnlControls.Controls.Add(this.kbtnReset);
            this.kpnlControls.Controls.Add(this.kbtnStop);
            this.kpnlControls.Controls.Add(this.kbtnStart);
            this.kpnlControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlControls.Location = new System.Drawing.Point(0, 0);
            this.kpnlControls.Name = "kpnlControls";
            this.kpnlControls.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelClient;
            this.kpnlControls.Size = new System.Drawing.Size(896, 25);
            this.kpnlControls.TabIndex = 0;
            // 
            // kbtnClearEvents
            // 
            this.kbtnClearEvents.Location = new System.Drawing.Point(300, 0);
            this.kbtnClearEvents.Name = "kbtnClearEvents";
            this.kbtnClearEvents.Size = new System.Drawing.Size(100, 25);
            this.kbtnClearEvents.TabIndex = 3;
            this.kbtnClearEvents.Values.Text = "Clear Events";
            this.kbtnClearEvents.Click += new System.EventHandler(this.kbtnClearEvents_Click);
            // 
            // kbtnReset
            // 
            this.kbtnReset.Location = new System.Drawing.Point(200, 0);
            this.kbtnReset.Name = "kbtnReset";
            this.kbtnReset.Size = new System.Drawing.Size(100, 25);
            this.kbtnReset.TabIndex = 2;
            this.kbtnReset.Values.Text = "Reset";
            this.kbtnReset.Click += new System.EventHandler(this.kbtnReset_Click);
            // 
            // kbtnStop
            // 
            this.kbtnStop.Location = new System.Drawing.Point(100, 0);
            this.kbtnStop.Name = "kbtnStop";
            this.kbtnStop.Size = new System.Drawing.Size(100, 25);
            this.kbtnStop.TabIndex = 1;
            this.kbtnStop.Values.Text = "Stop";
            this.kbtnStop.Click += new System.EventHandler(this.kbtnStop_Click);
            // 
            // kbtnStart
            // 
            this.kbtnStart.Location = new System.Drawing.Point(0, 0);
            this.kbtnStart.Name = "kbtnStart";
            this.kbtnStart.Size = new System.Drawing.Size(100, 25);
            this.kbtnStart.TabIndex = 0;
            this.kbtnStart.Values.Text = "Start";
            this.kbtnStart.Click += new System.EventHandler(this.kbtnStart_Click);
            // 
            // kpnlRight
            // 
            this.kpnlRight.Controls.Add(this.kpgbEvents);
            this.kpnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlRight.Location = new System.Drawing.Point(0, 0);
            this.kpnlRight.Name = "kpnlRight";
            this.kpnlRight.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelClient;
            this.kpnlRight.Size = new System.Drawing.Size(900, 296);
            this.kpnlRight.TabIndex = 0;
            // 
            // kpgbEvents
            // 
            this.kpgbEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpgbEvents.Location = new System.Drawing.Point(0, 0);
            this.kpgbEvents.Name = "kpgbEvents";
            // 
            // kpgbEvents.Panel
            // 
            this.kpgbEvents.Panel.Controls.Add(this.kpnlEvents);
            this.kpgbEvents.Size = new System.Drawing.Size(900, 296);
            this.kpgbEvents.TabIndex = 0;
            this.kpgbEvents.Values.Heading = "Timer Events";
            // 
            // kpnlEvents
            // 
            this.kpnlEvents.Controls.Add(this.klstEvents);
            this.kpnlEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlEvents.Location = new System.Drawing.Point(0, 0);
            this.kpnlEvents.Name = "kpnlEvents";
            this.kpnlEvents.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelClient;
            this.kpnlEvents.Size = new System.Drawing.Size(896, 271);
            this.kpnlEvents.TabIndex = 0;
            // 
            // klstEvents
            // 
            this.klstEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klstEvents.Location = new System.Drawing.Point(0, 0);
            this.klstEvents.Name = "klstEvents";
            this.klstEvents.Size = new System.Drawing.Size(896, 271);
            this.klstEvents.TabIndex = 0;
            // 
            // kpnlBottom
            // 
            this.kpnlBottom.Controls.Add(this.kbtnClose);
            this.kpnlBottom.Controls.Add(this.kryptonBorderEdge1);
            this.kpnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kpnlBottom.Location = new System.Drawing.Point(0, 550);
            this.kpnlBottom.Name = "kpnlBottom";
            this.kpnlBottom.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelClient;
            this.kpnlBottom.Size = new System.Drawing.Size(900, 50);
            this.kpnlBottom.TabIndex = 1;
            // 
            // kbtnClose
            // 
            this.kbtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnClose.Location = new System.Drawing.Point(800, 12);
            this.kbtnClose.Name = "kbtnClose";
            this.kbtnClose.Size = new System.Drawing.Size(90, 25);
            this.kbtnClose.TabIndex = 1;
            this.kbtnClose.Values.Text = "Close";
            this.kbtnClose.Click += new System.EventHandler(this.kbtnClose_Click);
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(0, 0);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(900, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // TimerTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.kpnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.MinimumSize = new System.Drawing.Size(700, 500);
            this.Name = "TimerTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KryptonTimer Test";
            ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).EndInit();
            this.kpnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kscMain.Panel1)).EndInit();
            this.kscMain.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kscMain.Panel2)).EndInit();
            this.kscMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kscMain)).EndInit();
            this.kscMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kpnlLeft)).EndInit();
            this.kpnlLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kpgbConfiguration.Panel)).EndInit();
            this.kpgbConfiguration.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kpgbConfiguration)).EndInit();
            this.kpgbConfiguration.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kpnlConfig)).EndInit();
            this.kpnlConfig.ResumeLayout(false);
            this.kpnlConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbPaletteMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kpgbStatus.Panel)).EndInit();
            this.kpgbStatus.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kpgbStatus)).EndInit();
            this.kpgbStatus.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kpnlStatus)).EndInit();
            this.kpnlStatus.ResumeLayout(false);
            this.kpnlStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpgbControls.Panel)).EndInit();
            this.kpgbControls.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kpgbControls)).EndInit();
            this.kpgbControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kpnlControls)).EndInit();
            this.kpnlControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kpnlRight)).EndInit();
            this.kpnlRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kpgbEvents.Panel)).EndInit();
            this.kpgbEvents.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kpgbEvents)).EndInit();
            this.kpgbEvents.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kpnlEvents)).EndInit();
            this.kpnlEvents.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kpnlBottom)).EndInit();
            this.kpnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kpnlMain;
        private Krypton.Toolkit.KryptonSplitContainer kscMain;
        private Krypton.Toolkit.KryptonPanel kpnlLeft;
        private Krypton.Toolkit.KryptonGroupBox kpgbConfiguration;
        private Krypton.Toolkit.KryptonPanel kpnlConfig;
        private Krypton.Toolkit.KryptonNumericUpDown knudInterval;
        private Krypton.Toolkit.KryptonLabel klblInterval;
        private Krypton.Toolkit.KryptonTextBox ktxtInterval;
        private Krypton.Toolkit.KryptonLabel klblIntervalInput;
        private Krypton.Toolkit.KryptonCheckBox kchkUpdateProgressBar;
        private Krypton.Toolkit.KryptonComboBox kcmbPaletteMode;
        private Krypton.Toolkit.KryptonLabel klblPaletteMode;
        private Krypton.Toolkit.KryptonGroupBox kpgbStatus;
        private Krypton.Toolkit.KryptonPanel kpnlStatus;
        private Krypton.Toolkit.KryptonLabel klblStatus;
        private Krypton.Toolkit.KryptonLabel klblIntervalStatus;
        private Krypton.Toolkit.KryptonLabel klblEnabled;
        private Krypton.Toolkit.KryptonLabel klblTickCount;
        private Krypton.Toolkit.KryptonLabel klblElapsedTime;
        private Krypton.Toolkit.KryptonLabel klblLastTick;
        private Krypton.Toolkit.KryptonProgressBar kprogressBar;
        private Krypton.Toolkit.KryptonGroupBox kpgbControls;
        private Krypton.Toolkit.KryptonPanel kpnlControls;
        private Krypton.Toolkit.KryptonButton kbtnStart;
        private Krypton.Toolkit.KryptonButton kbtnStop;
        private Krypton.Toolkit.KryptonButton kbtnReset;
        private Krypton.Toolkit.KryptonButton kbtnClearEvents;
        private Krypton.Toolkit.KryptonPanel kpnlRight;
        private Krypton.Toolkit.KryptonGroupBox kpgbEvents;
        private Krypton.Toolkit.KryptonPanel kpnlEvents;
        private Krypton.Toolkit.KryptonListBox klstEvents;
        private Krypton.Toolkit.KryptonPanel kpnlBottom;
        private Krypton.Toolkit.KryptonButton kbtnClose;
        private Krypton.Toolkit.KryptonBorderEdge kryptonBorderEdge1;
    }
}
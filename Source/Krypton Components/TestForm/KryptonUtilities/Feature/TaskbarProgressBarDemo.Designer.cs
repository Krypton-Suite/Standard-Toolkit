namespace TestForm
{
    partial class TaskbarProgressBarDemo
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
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonGroupBox1 = new Krypton.Toolkit.KryptonGroupBox();
            this.kryptonProgressBar1 = new Krypton.Toolkit.KryptonProgressBar();
            this.kryptonTrackBar1 = new Krypton.Toolkit.KryptonTrackBar();
            this.klblValue = new Krypton.Toolkit.KryptonLabel();
            this.klblStatus = new Krypton.Toolkit.KryptonLabel();
            this.kryptonGroupBox2 = new Krypton.Toolkit.KryptonGroupBox();
            this.kchkEnableTaskbar = new Krypton.Toolkit.KryptonCheckBox();
            this.kryptonGroupBox3 = new Krypton.Toolkit.KryptonGroupBox();
            this.kbtnStartSimulation = new Krypton.Toolkit.KryptonButton();
            this.kbtnStopSimulation = new Krypton.Toolkit.KryptonButton();
            this.kryptonGroupBox4 = new Krypton.Toolkit.KryptonGroupBox();
            this.kryptonLabel2 = new Krypton.Toolkit.KryptonLabel();
            this.kcmbStyle = new Krypton.Toolkit.KryptonComboBox();
            this.kryptonGroupBox5 = new Krypton.Toolkit.KryptonGroupBox();
            this.kryptonLabel3 = new Krypton.Toolkit.KryptonLabel();
            this.kcmbState = new Krypton.Toolkit.KryptonComboBox();
            this.kbtnSetError = new Krypton.Toolkit.KryptonButton();
            this.kbtnSetPaused = new Krypton.Toolkit.KryptonButton();
            this.kbtnSetNormal = new Krypton.Toolkit.KryptonButton();
            this.kbtnClearProgress = new Krypton.Toolkit.KryptonButton();
            this.kryptonGroupBox6 = new Krypton.Toolkit.KryptonGroupBox();
            this.kryptonLabel4 = new Krypton.Toolkit.KryptonLabel();
            this.knudMinimum = new Krypton.Toolkit.KryptonNumericUpDown();
            this.kryptonLabel5 = new Krypton.Toolkit.KryptonLabel();
            this.knudMaximum = new Krypton.Toolkit.KryptonNumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).BeginInit();
            this.kryptonGroupBox1.Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2)).BeginInit();
            this.kryptonGroupBox2.Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox3)).BeginInit();
            this.kryptonGroupBox3.Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox4)).BeginInit();
            this.kryptonGroupBox4.Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbStyle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox5)).BeginInit();
            this.kryptonGroupBox5.Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox6)).BeginInit();
            this.kryptonGroupBox6.Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonGroupBox6);
            this.kryptonPanel1.Controls.Add(this.kryptonGroupBox5);
            this.kryptonPanel1.Controls.Add(this.kryptonGroupBox4);
            this.kryptonPanel1.Controls.Add(this.kryptonGroupBox3);
            this.kryptonPanel1.Controls.Add(this.kryptonGroupBox2);
            this.kryptonPanel1.Controls.Add(this.kryptonGroupBox1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(680, 620);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonGroupBox1 — Progress bar, track bar, value and status labels
            // 
            this.kryptonGroupBox1.Location = new System.Drawing.Point(12, 12);
            this.kryptonGroupBox1.Name = "kryptonGroupBox1";
            this.kryptonGroupBox1.Size = new System.Drawing.Size(656, 140);
            this.kryptonGroupBox1.TabIndex = 0;
            this.kryptonGroupBox1.Values.Heading = "Progress Bar";
            // Progress bar
            this.kryptonProgressBar1.Location = new System.Drawing.Point(10, 10);
            this.kryptonProgressBar1.Name = "kryptonProgressBar1";
            this.kryptonProgressBar1.Size = new System.Drawing.Size(626, 26);
            this.kryptonProgressBar1.TabIndex = 0;
            // Track bar
            this.kryptonTrackBar1.Location = new System.Drawing.Point(10, 44);
            this.kryptonTrackBar1.Name = "kryptonTrackBar1";
            this.kryptonTrackBar1.Size = new System.Drawing.Size(626, 38);
            this.kryptonTrackBar1.TabIndex = 1;
            this.kryptonTrackBar1.TickStyle = System.Windows.Forms.TickStyle.BottomRight;
            // Value label
            this.klblValue.Location = new System.Drawing.Point(10, 88);
            this.klblValue.Name = "klblValue";
            this.klblValue.Size = new System.Drawing.Size(200, 20);
            this.klblValue.TabIndex = 2;
            this.klblValue.Values.Text = "Value: 0 / 100";
            // Status label
            this.klblStatus.Location = new System.Drawing.Point(220, 88);
            this.klblStatus.Name = "klblStatus";
            this.klblStatus.Size = new System.Drawing.Size(416, 20);
            this.klblStatus.TabIndex = 3;
            this.klblStatus.Values.Text = "Taskbar: OFF";
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonProgressBar1);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonTrackBar1);
            this.kryptonGroupBox1.Panel.Controls.Add(this.klblValue);
            this.kryptonGroupBox1.Panel.Controls.Add(this.klblStatus);
            // 
            // kryptonGroupBox2 — Enable/Disable toggle
            // 
            this.kryptonGroupBox2.Location = new System.Drawing.Point(12, 162);
            this.kryptonGroupBox2.Name = "kryptonGroupBox2";
            this.kryptonGroupBox2.Size = new System.Drawing.Size(320, 68);
            this.kryptonGroupBox2.TabIndex = 1;
            this.kryptonGroupBox2.Values.Heading = "Scenario 1 — Enable / Disable";
            this.kchkEnableTaskbar.Location = new System.Drawing.Point(10, 10);
            this.kchkEnableTaskbar.Name = "kchkEnableTaskbar";
            this.kchkEnableTaskbar.Size = new System.Drawing.Size(280, 20);
            this.kchkEnableTaskbar.TabIndex = 0;
            this.kchkEnableTaskbar.Values.Text = "UseTaskbarProgress (synchronise with taskbar)";
            this.kchkEnableTaskbar.CheckedChanged += new System.EventHandler(this.kchkEnableTaskbar_CheckedChanged);
            this.kryptonGroupBox2.Panel.Controls.Add(this.kchkEnableTaskbar);
            // 
            // kryptonGroupBox3 — Simulated download
            // 
            this.kryptonGroupBox3.Location = new System.Drawing.Point(348, 162);
            this.kryptonGroupBox3.Name = "kryptonGroupBox3";
            this.kryptonGroupBox3.Size = new System.Drawing.Size(320, 68);
            this.kryptonGroupBox3.TabIndex = 2;
            this.kryptonGroupBox3.Values.Heading = "Scenario 2 — Simulated Download";
            this.kbtnStartSimulation.Location = new System.Drawing.Point(10, 10);
            this.kbtnStartSimulation.Name = "kbtnStartSimulation";
            this.kbtnStartSimulation.Size = new System.Drawing.Size(140, 30);
            this.kbtnStartSimulation.TabIndex = 0;
            this.kbtnStartSimulation.Values.Text = "Start";
            this.kbtnStartSimulation.Click += new System.EventHandler(this.kbtnStartSimulation_Click);
            this.kbtnStopSimulation.Location = new System.Drawing.Point(160, 10);
            this.kbtnStopSimulation.Name = "kbtnStopSimulation";
            this.kbtnStopSimulation.Size = new System.Drawing.Size(140, 30);
            this.kbtnStopSimulation.TabIndex = 1;
            this.kbtnStopSimulation.Values.Text = "Stop";
            this.kbtnStopSimulation.Click += new System.EventHandler(this.kbtnStopSimulation_Click);
            this.kryptonGroupBox3.Panel.Controls.Add(this.kbtnStartSimulation);
            this.kryptonGroupBox3.Panel.Controls.Add(this.kbtnStopSimulation);
            // 
            // kryptonGroupBox4 — ProgressBarStyle
            // 
            this.kryptonGroupBox4.Location = new System.Drawing.Point(12, 240);
            this.kryptonGroupBox4.Name = "kryptonGroupBox4";
            this.kryptonGroupBox4.Size = new System.Drawing.Size(320, 68);
            this.kryptonGroupBox4.TabIndex = 3;
            this.kryptonGroupBox4.Values.Heading = "Scenario 4 — ProgressBarStyle";
            this.kryptonLabel2.Location = new System.Drawing.Point(10, 14);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(42, 20);
            this.kryptonLabel2.TabIndex = 0;
            this.kryptonLabel2.Values.Text = "Style:";
            this.kcmbStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbStyle.Location = new System.Drawing.Point(58, 10);
            this.kcmbStyle.Name = "kcmbStyle";
            this.kcmbStyle.Size = new System.Drawing.Size(242, 22);
            this.kcmbStyle.TabIndex = 1;
            this.kcmbStyle.SelectedIndexChanged += new System.EventHandler(this.kcmbStyle_SelectedIndexChanged);
            this.kryptonGroupBox4.Panel.Controls.Add(this.kryptonLabel2);
            this.kryptonGroupBox4.Panel.Controls.Add(this.kcmbStyle);
            // 
            // kryptonGroupBox5 — TaskbarProgressState
            // 
            this.kryptonGroupBox5.Location = new System.Drawing.Point(348, 240);
            this.kryptonGroupBox5.Name = "kryptonGroupBox5";
            this.kryptonGroupBox5.Size = new System.Drawing.Size(320, 170);
            this.kryptonGroupBox5.TabIndex = 4;
            this.kryptonGroupBox5.Values.Heading = "Scenario 5 — TaskbarProgressState";
            this.kryptonLabel3.Location = new System.Drawing.Point(10, 14);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(42, 20);
            this.kryptonLabel3.TabIndex = 0;
            this.kryptonLabel3.Values.Text = "State:";
            this.kcmbState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbState.Location = new System.Drawing.Point(58, 10);
            this.kcmbState.Name = "kcmbState";
            this.kcmbState.Size = new System.Drawing.Size(242, 22);
            this.kcmbState.TabIndex = 1;
            this.kcmbState.SelectedIndexChanged += new System.EventHandler(this.kcmbState_SelectedIndexChanged);
            this.kbtnSetNormal.Location = new System.Drawing.Point(10, 42);
            this.kbtnSetNormal.Name = "kbtnSetNormal";
            this.kbtnSetNormal.Size = new System.Drawing.Size(138, 30);
            this.kbtnSetNormal.TabIndex = 2;
            this.kbtnSetNormal.Values.Text = "Normal (green)";
            this.kbtnSetNormal.Click += new System.EventHandler(this.kbtnSetNormal_Click);
            this.kbtnSetPaused.Location = new System.Drawing.Point(162, 42);
            this.kbtnSetPaused.Name = "kbtnSetPaused";
            this.kbtnSetPaused.Size = new System.Drawing.Size(138, 30);
            this.kbtnSetPaused.TabIndex = 3;
            this.kbtnSetPaused.Values.Text = "Paused (yellow)";
            this.kbtnSetPaused.Click += new System.EventHandler(this.kbtnSetPaused_Click);
            this.kbtnSetError.Location = new System.Drawing.Point(10, 82);
            this.kbtnSetError.Name = "kbtnSetError";
            this.kbtnSetError.Size = new System.Drawing.Size(138, 30);
            this.kbtnSetError.TabIndex = 4;
            this.kbtnSetError.Values.Text = "Error (red)";
            this.kbtnSetError.Click += new System.EventHandler(this.kbtnSetError_Click);
            this.kbtnClearProgress.Location = new System.Drawing.Point(162, 82);
            this.kbtnClearProgress.Name = "kbtnClearProgress";
            this.kbtnClearProgress.Size = new System.Drawing.Size(138, 30);
            this.kbtnClearProgress.TabIndex = 5;
            this.kbtnClearProgress.Values.Text = "Clear (disable)";
            this.kbtnClearProgress.Click += new System.EventHandler(this.kbtnClearProgress_Click);
            this.kryptonGroupBox5.Panel.Controls.Add(this.kryptonLabel3);
            this.kryptonGroupBox5.Panel.Controls.Add(this.kcmbState);
            this.kryptonGroupBox5.Panel.Controls.Add(this.kbtnSetNormal);
            this.kryptonGroupBox5.Panel.Controls.Add(this.kbtnSetPaused);
            this.kryptonGroupBox5.Panel.Controls.Add(this.kbtnSetError);
            this.kryptonGroupBox5.Panel.Controls.Add(this.kbtnClearProgress);
            // 
            // kryptonGroupBox6 — Min/Max range
            // 
            this.kryptonGroupBox6.Location = new System.Drawing.Point(12, 420);
            this.kryptonGroupBox6.Name = "kryptonGroupBox6";
            this.kryptonGroupBox6.Size = new System.Drawing.Size(320, 68);
            this.kryptonGroupBox6.TabIndex = 5;
            this.kryptonGroupBox6.Values.Heading = "Scenario 6 — Minimum / Maximum Range";
            this.kryptonLabel4.Location = new System.Drawing.Point(10, 14);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(28, 20);
            this.kryptonLabel4.TabIndex = 0;
            this.kryptonLabel4.Values.Text = "Min:";
            this.knudMinimum.Location = new System.Drawing.Point(44, 10);
            this.knudMinimum.Minimum = 0;
            this.knudMinimum.Maximum = 9999;
            this.knudMinimum.Name = "knudMinimum";
            this.knudMinimum.Size = new System.Drawing.Size(80, 22);
            this.knudMinimum.TabIndex = 1;
            this.knudMinimum.Value = 0;
            this.knudMinimum.ValueChanged += new System.EventHandler(this.knudMinimum_ValueChanged);
            this.kryptonLabel5.Location = new System.Drawing.Point(138, 14);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(32, 20);
            this.kryptonLabel5.TabIndex = 2;
            this.kryptonLabel5.Values.Text = "Max:";
            this.knudMaximum.Location = new System.Drawing.Point(176, 10);
            this.knudMaximum.Minimum = 1;
            this.knudMaximum.Maximum = 9999;
            this.knudMaximum.Name = "knudMaximum";
            this.knudMaximum.Size = new System.Drawing.Size(80, 22);
            this.knudMaximum.TabIndex = 3;
            this.knudMaximum.Value = 100;
            this.knudMaximum.ValueChanged += new System.EventHandler(this.knudMaximum_ValueChanged);
            this.kryptonGroupBox6.Panel.Controls.Add(this.kryptonLabel4);
            this.kryptonGroupBox6.Panel.Controls.Add(this.knudMinimum);
            this.kryptonGroupBox6.Panel.Controls.Add(this.kryptonLabel5);
            this.kryptonGroupBox6.Panel.Controls.Add(this.knudMaximum);
            // 
            // TaskbarProgressBarDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 620);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "TaskbarProgressBarDemo";
            this.Text = "Taskbar Progress Bar Demo (Issue #2890)";
            this.Load += new System.EventHandler(this.TaskbarProgressBarDemo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).EndInit();
            this.kryptonGroupBox1.Panel.ResumeLayout(false);
            this.kryptonGroupBox1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2)).EndInit();
            this.kryptonGroupBox2.Panel.ResumeLayout(false);
            this.kryptonGroupBox2.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox3)).EndInit();
            this.kryptonGroupBox3.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox4)).EndInit();
            this.kryptonGroupBox4.Panel.ResumeLayout(false);
            this.kryptonGroupBox4.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbStyle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox5)).EndInit();
            this.kryptonGroupBox5.Panel.ResumeLayout(false);
            this.kryptonGroupBox5.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox6)).EndInit();
            this.kryptonGroupBox6.Panel.ResumeLayout(false);
            this.kryptonGroupBox6.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonGroupBox kryptonGroupBox1;
        private Krypton.Toolkit.KryptonProgressBar kryptonProgressBar1;
        private Krypton.Toolkit.KryptonTrackBar kryptonTrackBar1;
        private Krypton.Toolkit.KryptonLabel klblValue;
        private Krypton.Toolkit.KryptonLabel klblStatus;
        private Krypton.Toolkit.KryptonGroupBox kryptonGroupBox2;
        private Krypton.Toolkit.KryptonCheckBox kchkEnableTaskbar;
        private Krypton.Toolkit.KryptonGroupBox kryptonGroupBox3;
        private Krypton.Toolkit.KryptonButton kbtnStartSimulation;
        private Krypton.Toolkit.KryptonButton kbtnStopSimulation;
        private Krypton.Toolkit.KryptonGroupBox kryptonGroupBox4;
        private Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private Krypton.Toolkit.KryptonComboBox kcmbStyle;
        private Krypton.Toolkit.KryptonGroupBox kryptonGroupBox5;
        private Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private Krypton.Toolkit.KryptonComboBox kcmbState;
        private Krypton.Toolkit.KryptonButton kbtnSetNormal;
        private Krypton.Toolkit.KryptonButton kbtnSetPaused;
        private Krypton.Toolkit.KryptonButton kbtnSetError;
        private Krypton.Toolkit.KryptonButton kbtnClearProgress;
        private Krypton.Toolkit.KryptonGroupBox kryptonGroupBox6;
        private Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private Krypton.Toolkit.KryptonNumericUpDown knudMinimum;
        private Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private Krypton.Toolkit.KryptonNumericUpDown knudMaximum;
    }
}
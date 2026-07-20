#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm
{
    partial class KryptonLoggerDemo
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
            this.pnlHeader = new Krypton.Toolkit.KryptonPanel();
            this.lblInstructions = new Krypton.Toolkit.KryptonLabel();
            this.grpEnvironment = new Krypton.Toolkit.KryptonGroupBox();
            this.lblActivePath = new Krypton.Toolkit.KryptonLabel();
            this.lblDefaultPath = new Krypton.Toolkit.KryptonLabel();
            this.lblKryptonLogWm = new Krypton.Toolkit.KryptonLabel();
            this.lblKryptonLogPath = new Krypton.Toolkit.KryptonLabel();
            this.lblKryptonLog = new Krypton.Toolkit.KryptonLabel();
            this.grpActions = new Krypton.Toolkit.KryptonGroupBox();
            this.kryptonThemeComboBox1 = new Krypton.Toolkit.KryptonThemeComboBox();
            this.lblTheme = new Krypton.Toolkit.KryptonLabel();
            this.chkCustomLogger = new Krypton.Toolkit.KryptonCheckBox();
            this.btnClearOutput = new Krypton.Toolkit.KryptonButton();
            this.btnReadLogFile = new Krypton.Toolkit.KryptonButton();
            this.btnParallelStress = new Krypton.Toolkit.KryptonButton();
            this.btnWriteLogOutput = new Krypton.Toolkit.KryptonButton();
            this.btnWriteKryptonLogger = new Krypton.Toolkit.KryptonButton();
            this.grpOutput = new Krypton.Toolkit.KryptonGroupBox();
            this.txtOutput = new Krypton.Toolkit.KryptonTextBox();
            this.lblStatus = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).BeginInit();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpEnvironment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpEnvironment.Panel)).BeginInit();
            this.grpEnvironment.Panel.SuspendLayout();
            this.grpEnvironment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpActions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpActions.Panel)).BeginInit();
            this.grpActions.Panel.SuspendLayout();
            this.grpActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpOutput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpOutput.Panel)).BeginInit();
            this.grpOutput.Panel.SuspendLayout();
            this.grpOutput.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlHeader
            //
            this.pnlHeader.Controls.Add(this.lblInstructions);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(12);
            this.pnlHeader.Size = new System.Drawing.Size(784, 72);
            this.pnlHeader.TabIndex = 0;
            //
            // lblInstructions
            //
            this.lblInstructions.AutoSize = false;
            this.lblInstructions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInstructions.Location = new System.Drawing.Point(12, 12);
            this.lblInstructions.Name = "lblInstructions";
            this.lblInstructions.Size = new System.Drawing.Size(760, 48);
            this.lblInstructions.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblInstructions.StateCommon.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.lblInstructions.TabIndex = 0;
            this.lblInstructions.Values.Text = "Issue #3856: exercise KryptonLogger and CommonHelper.LogOutput. Set KRYPTON_LOG=1 or KRYPTON_LOG_PATH before launching TestForm for file output. Switch theme to generate [WM] trace lines when file logging is enabled.";
            //
            // grpEnvironment
            //
            this.grpEnvironment.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpEnvironment.Location = new System.Drawing.Point(0, 72);
            this.grpEnvironment.Name = "grpEnvironment";
            this.grpEnvironment.Size = new System.Drawing.Size(784, 130);
            this.grpEnvironment.TabIndex = 1;
            this.grpEnvironment.Values.Heading = "Environment (read at process start)";
            //
            // grpEnvironment.Panel
            //
            this.grpEnvironment.Panel.Controls.Add(this.lblActivePath);
            this.grpEnvironment.Panel.Controls.Add(this.lblDefaultPath);
            this.grpEnvironment.Panel.Controls.Add(this.lblKryptonLogWm);
            this.grpEnvironment.Panel.Controls.Add(this.lblKryptonLogPath);
            this.grpEnvironment.Panel.Controls.Add(this.lblKryptonLog);
            //
            // lblKryptonLog
            //
            this.lblKryptonLog.Location = new System.Drawing.Point(15, 10);
            this.lblKryptonLog.Name = "lblKryptonLog";
            this.lblKryptonLog.Size = new System.Drawing.Size(740, 20);
            this.lblKryptonLog.TabIndex = 0;
            this.lblKryptonLog.Values.Text = "KRYPTON_LOG = (not set)";
            //
            // lblKryptonLogPath
            //
            this.lblKryptonLogPath.Location = new System.Drawing.Point(15, 32);
            this.lblKryptonLogPath.Name = "lblKryptonLogPath";
            this.lblKryptonLogPath.Size = new System.Drawing.Size(740, 20);
            this.lblKryptonLogPath.TabIndex = 1;
            this.lblKryptonLogPath.Values.Text = "KRYPTON_LOG_PATH = (not set)";
            //
            // lblKryptonLogWm
            //
            this.lblKryptonLogWm.Location = new System.Drawing.Point(15, 54);
            this.lblKryptonLogWm.Name = "lblKryptonLogWm";
            this.lblKryptonLogWm.Size = new System.Drawing.Size(740, 20);
            this.lblKryptonLogWm.TabIndex = 2;
            this.lblKryptonLogWm.Values.Text = "KRYPTON_LOG_WM = (not set)";
            //
            // lblDefaultPath
            //
            this.lblDefaultPath.Location = new System.Drawing.Point(15, 76);
            this.lblDefaultPath.Name = "lblDefaultPath";
            this.lblDefaultPath.Size = new System.Drawing.Size(740, 20);
            this.lblDefaultPath.TabIndex = 3;
            this.lblDefaultPath.Values.Text = "Default file when KRYPTON_LOG is enabled:";
            //
            // lblActivePath
            //
            this.lblActivePath.Location = new System.Drawing.Point(15, 98);
            this.lblActivePath.Name = "lblActivePath";
            this.lblActivePath.Size = new System.Drawing.Size(740, 20);
            this.lblActivePath.TabIndex = 4;
            this.lblActivePath.Values.Text = "Active log file:";
            //
            // grpActions
            //
            this.grpActions.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpActions.Location = new System.Drawing.Point(0, 202);
            this.grpActions.Name = "grpActions";
            this.grpActions.Size = new System.Drawing.Size(784, 118);
            this.grpActions.TabIndex = 2;
            this.grpActions.Values.Heading = "Actions";
            //
            // grpActions.Panel
            //
            this.grpActions.Panel.Controls.Add(this.kryptonThemeComboBox1);
            this.grpActions.Panel.Controls.Add(this.lblTheme);
            this.grpActions.Panel.Controls.Add(this.chkCustomLogger);
            this.grpActions.Panel.Controls.Add(this.btnClearOutput);
            this.grpActions.Panel.Controls.Add(this.btnReadLogFile);
            this.grpActions.Panel.Controls.Add(this.btnParallelStress);
            this.grpActions.Panel.Controls.Add(this.btnWriteLogOutput);
            this.grpActions.Panel.Controls.Add(this.btnWriteKryptonLogger);
            //
            // btnWriteKryptonLogger
            //
            this.btnWriteKryptonLogger.Location = new System.Drawing.Point(15, 15);
            this.btnWriteKryptonLogger.Name = "btnWriteKryptonLogger";
            this.btnWriteKryptonLogger.Size = new System.Drawing.Size(150, 28);
            this.btnWriteKryptonLogger.TabIndex = 0;
            this.btnWriteKryptonLogger.Values.Text = "KryptonLogger.Write";
            this.btnWriteKryptonLogger.Click += new System.EventHandler(this.OnWriteKryptonLoggerClick);
            //
            // btnWriteLogOutput
            //
            this.btnWriteLogOutput.Location = new System.Drawing.Point(175, 15);
            this.btnWriteLogOutput.Name = "btnWriteLogOutput";
            this.btnWriteLogOutput.Size = new System.Drawing.Size(170, 28);
            this.btnWriteLogOutput.TabIndex = 1;
            this.btnWriteLogOutput.Values.Text = "CommonHelper.LogOutput";
            this.btnWriteLogOutput.Click += new System.EventHandler(this.OnWriteLogOutputClick);
            //
            // btnParallelStress
            //
            this.btnParallelStress.Location = new System.Drawing.Point(355, 15);
            this.btnParallelStress.Name = "btnParallelStress";
            this.btnParallelStress.Size = new System.Drawing.Size(150, 28);
            this.btnParallelStress.TabIndex = 2;
            this.btnParallelStress.Values.Text = "Parallel stress (100)";
            this.btnParallelStress.Click += new System.EventHandler(this.OnParallelStressClick);
            //
            // btnReadLogFile
            //
            this.btnReadLogFile.Location = new System.Drawing.Point(515, 15);
            this.btnReadLogFile.Name = "btnReadLogFile";
            this.btnReadLogFile.Size = new System.Drawing.Size(120, 28);
            this.btnReadLogFile.TabIndex = 3;
            this.btnReadLogFile.Values.Text = "Reload log file";
            this.btnReadLogFile.Click += new System.EventHandler(this.OnReadLogFileClick);
            //
            // btnClearOutput
            //
            this.btnClearOutput.Location = new System.Drawing.Point(645, 15);
            this.btnClearOutput.Name = "btnClearOutput";
            this.btnClearOutput.Size = new System.Drawing.Size(110, 28);
            this.btnClearOutput.TabIndex = 4;
            this.btnClearOutput.Values.Text = "Clear output";
            this.btnClearOutput.Click += new System.EventHandler(this.OnClearOutputClick);
            //
            // chkCustomLogger
            //
            this.chkCustomLogger.Location = new System.Drawing.Point(15, 55);
            this.chkCustomLogger.Name = "chkCustomLogger";
            this.chkCustomLogger.Size = new System.Drawing.Size(330, 20);
            this.chkCustomLogger.TabIndex = 5;
            this.chkCustomLogger.Values.Text = "Use custom IKryptonLogger (in-memory output only)";
            this.chkCustomLogger.CheckedChanged += new System.EventHandler(this.OnCustomLoggerCheckedChanged);
            //
            // lblTheme
            //
            this.lblTheme.Location = new System.Drawing.Point(360, 55);
            this.lblTheme.Name = "lblTheme";
            this.lblTheme.Size = new System.Drawing.Size(50, 20);
            this.lblTheme.TabIndex = 6;
            this.lblTheme.Values.Text = "Theme:";
            //
            // kryptonThemeComboBox1
            //
            this.kryptonThemeComboBox1.DropDownWidth = 250;
            this.kryptonThemeComboBox1.Location = new System.Drawing.Point(416, 53);
            this.kryptonThemeComboBox1.Name = "kryptonThemeComboBox1";
            this.kryptonThemeComboBox1.Size = new System.Drawing.Size(220, 22);
            this.kryptonThemeComboBox1.TabIndex = 7;
            this.kryptonThemeComboBox1.SelectedIndexChanged += new System.EventHandler(this.OnThemeChanged);
            //
            // grpOutput
            //
            this.grpOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpOutput.Location = new System.Drawing.Point(0, 320);
            this.grpOutput.Name = "grpOutput";
            this.grpOutput.Size = new System.Drawing.Size(784, 261);
            this.grpOutput.TabIndex = 3;
            this.grpOutput.Values.Heading = "Demo output";
            //
            // grpOutput.Panel
            //
            this.grpOutput.Panel.Controls.Add(this.txtOutput);
            //
            // txtOutput
            //
            this.txtOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOutput.Location = new System.Drawing.Point(0, 0);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOutput.Size = new System.Drawing.Size(780, 236);
            this.txtOutput.TabIndex = 0;
            //
            // lblStatus
            //
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblStatus.Location = new System.Drawing.Point(0, 581);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.lblStatus.Size = new System.Drawing.Size(784, 32);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Values.Text = "Ready.";
            //
            // KryptonLoggerDemo
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 613);
            this.Controls.Add(this.grpOutput);
            this.Controls.Add(this.grpActions);
            this.Controls.Add(this.grpEnvironment);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.lblStatus);
            this.MinimumSize = new System.Drawing.Size(700, 500);
            this.Name = "KryptonLoggerDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Krypton Logger Demo (Issue #3856)";
            ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).EndInit();
            this.pnlHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpEnvironment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpEnvironment.Panel)).EndInit();
            this.grpEnvironment.Panel.ResumeLayout(false);
            this.grpEnvironment.Panel.PerformLayout();
            this.grpEnvironment.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpActions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpActions.Panel)).EndInit();
            this.grpActions.Panel.ResumeLayout(false);
            this.grpActions.Panel.PerformLayout();
            this.grpActions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpOutput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpOutput.Panel)).EndInit();
            this.grpOutput.Panel.ResumeLayout(false);
            this.grpOutput.Panel.PerformLayout();
            this.grpOutput.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.KryptonPanel pnlHeader;
        private Krypton.Toolkit.KryptonLabel lblInstructions;
        private Krypton.Toolkit.KryptonGroupBox grpEnvironment;
        private Krypton.Toolkit.KryptonLabel lblKryptonLog;
        private Krypton.Toolkit.KryptonLabel lblKryptonLogPath;
        private Krypton.Toolkit.KryptonLabel lblKryptonLogWm;
        private Krypton.Toolkit.KryptonLabel lblDefaultPath;
        private Krypton.Toolkit.KryptonLabel lblActivePath;
        private Krypton.Toolkit.KryptonGroupBox grpActions;
        private Krypton.Toolkit.KryptonButton btnWriteKryptonLogger;
        private Krypton.Toolkit.KryptonButton btnWriteLogOutput;
        private Krypton.Toolkit.KryptonButton btnParallelStress;
        private Krypton.Toolkit.KryptonButton btnReadLogFile;
        private Krypton.Toolkit.KryptonButton btnClearOutput;
        private Krypton.Toolkit.KryptonCheckBox chkCustomLogger;
        private Krypton.Toolkit.KryptonLabel lblTheme;
        private Krypton.Toolkit.KryptonThemeComboBox kryptonThemeComboBox1;
        private Krypton.Toolkit.KryptonGroupBox grpOutput;
        private Krypton.Toolkit.KryptonTextBox txtOutput;
        private Krypton.Toolkit.KryptonLabel lblStatus;
    }
}

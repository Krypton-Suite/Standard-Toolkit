namespace TestForm
{
    partial class ApplicationUpdaterDemo
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

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.kryptonPanel = new Krypton.Toolkit.KryptonPanel();
            this.table = new System.Windows.Forms.TableLayoutPanel();
            this.lblInfo = new Krypton.Toolkit.KryptonWrapLabel();
            this.flowOptions = new System.Windows.Forms.FlowLayoutPanel();
            this.lblMode = new Krypton.Toolkit.KryptonLabel();
            this.cmbMode = new Krypton.Toolkit.KryptonComboBox();
            this.lblSeverity = new Krypton.Toolkit.KryptonLabel();
            this.cmbSeverity = new Krypton.Toolkit.KryptonComboBox();
            this.lblKind = new Krypton.Toolkit.KryptonLabel();
            this.cmbKind = new Krypton.Toolkit.KryptonComboBox();
            this.lblMaxDays = new Krypton.Toolkit.KryptonLabel();
            this.nudMaxDays = new Krypton.Toolkit.KryptonNumericUpDown();
            this.chkAllowCritical = new Krypton.Toolkit.KryptonCheckBox();
            this.chkBannerPulse = new Krypton.Toolkit.KryptonCheckBox();
            this.chkSpeedGraph = new Krypton.Toolkit.KryptonCheckBox();
            this.flowButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCheckNow = new Krypton.Toolkit.KryptonButton();
            this.btnScheduled = new Krypton.Toolkit.KryptonButton();
            this.btnReset = new Krypton.Toolkit.KryptonButton();
            this.btnReleaseNotes = new Krypton.Toolkit.KryptonButton();
            this.btnGitHub = new Krypton.Toolkit.KryptonButton();
            this.flowGitHub = new System.Windows.Forms.FlowLayoutPanel();
            this.lblOwner = new Krypton.Toolkit.KryptonLabel();
            this.txtOwner = new Krypton.Toolkit.KryptonTextBox();
            this.lblRepository = new Krypton.Toolkit.KryptonLabel();
            this.txtRepository = new Krypton.Toolkit.KryptonTextBox();
            this.txtLog = new Krypton.Toolkit.KryptonTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).BeginInit();
            this.kryptonPanel.SuspendLayout();
            this.table.SuspendLayout();
            this.flowOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSeverity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbKind)).BeginInit();
            this.flowButtons.SuspendLayout();
            this.flowGitHub.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel
            // 
            this.kryptonPanel.Controls.Add(this.table);
            this.kryptonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel.Name = "kryptonPanel";
            this.kryptonPanel.Padding = new System.Windows.Forms.Padding(12);
            this.kryptonPanel.Size = new System.Drawing.Size(860, 620);
            this.kryptonPanel.TabIndex = 0;
            // 
            // table
            // 
            this.table.ColumnCount = 1;
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.table.Controls.Add(this.lblInfo, 0, 0);
            this.table.Controls.Add(this.flowOptions, 0, 1);
            this.table.Controls.Add(this.flowButtons, 0, 2);
            this.table.Controls.Add(this.flowGitHub, 0, 3);
            this.table.Controls.Add(this.txtLog, 0, 4);
            this.table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.table.Location = new System.Drawing.Point(12, 12);
            this.table.Name = "table";
            this.table.RowCount = 5;
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 108F));
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.table.Size = new System.Drawing.Size(836, 596);
            this.table.TabIndex = 0;
            // 
            // lblInfo
            // 
            this.lblInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInfo.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(836, 108);
            this.lblInfo.Text = "Local manifest lists 1.5.0 and 9.9.9 and this demo reports 1.0.0. Check now ignores the one-day interval and an active postpone, and stays quiet for a skipped version. Scheduled check honours both. The prompt shows the newest version. Release notes opens those notes in their own window, and Update now installs 9.9.9. Remind later asks for a date or skips this version. Max days is written as maximumPostponeDays. Critical hides Remind later unless Allow critical postpone is checked, and hides Skip unless the manifest sets allowSkip. allowResume and allowRetry can hide Resume and Restart. With the current version set to 9.9.9, release notes lists every feed package and Close. Automatic applies Optional and Recommended without a prompt. The demo does not exit. A zip extracts into the temp install folder. GitHub check uses the owner and repository, then returns to the local manifest.";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // flowOptions
            // 
            this.flowOptions.Controls.Add(this.lblMode);
            this.flowOptions.Controls.Add(this.cmbMode);
            this.flowOptions.Controls.Add(this.lblSeverity);
            this.flowOptions.Controls.Add(this.cmbSeverity);
            this.flowOptions.Controls.Add(this.lblKind);
            this.flowOptions.Controls.Add(this.cmbKind);
            this.flowOptions.Controls.Add(this.lblMaxDays);
            this.flowOptions.Controls.Add(this.nudMaxDays);
            this.flowOptions.Controls.Add(this.chkAllowCritical);
            this.flowOptions.Controls.Add(this.chkBannerPulse);
            this.flowOptions.Controls.Add(this.chkSpeedGraph);
            this.flowOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowOptions.Name = "flowOptions";
            this.flowOptions.WrapContents = true;
            // 
            // lblMode
            // 
            this.lblMode.Name = "lblMode";
            this.lblMode.Values.Text = "Mode";
            // 
            // cmbMode
            // 
            this.cmbMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMode.DropDownWidth = 140;
            this.cmbMode.Name = "cmbMode";
            this.cmbMode.Size = new System.Drawing.Size(140, 22);
            // 
            // lblSeverity
            // 
            this.lblSeverity.Name = "lblSeverity";
            this.lblSeverity.Values.Text = "Severity";
            // 
            // cmbSeverity
            // 
            this.cmbSeverity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSeverity.DropDownWidth = 140;
            this.cmbSeverity.Name = "cmbSeverity";
            this.cmbSeverity.Size = new System.Drawing.Size(140, 22);
            // 
            // lblKind
            // 
            this.lblKind.Name = "lblKind";
            this.lblKind.Values.Text = "Package";
            // 
            // cmbKind
            // 
            this.cmbKind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKind.DropDownWidth = 140;
            this.cmbKind.Name = "cmbKind";
            this.cmbKind.Size = new System.Drawing.Size(140, 22);
            // 
            // lblMaxDays
            // 
            this.lblMaxDays.Name = "lblMaxDays";
            this.lblMaxDays.Values.Text = "Max days";
            // 
            // nudMaxDays
            // 
            this.nudMaxDays.Maximum = new decimal(new int[] { 3650, 0, 0, 0 });
            this.nudMaxDays.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudMaxDays.Name = "nudMaxDays";
            this.nudMaxDays.Size = new System.Drawing.Size(70, 24);
            this.nudMaxDays.Value = new decimal(new int[] { 14, 0, 0, 0 });
            // 
            // chkAllowCritical
            // 
            this.chkAllowCritical.Name = "chkAllowCritical";
            this.chkAllowCritical.Size = new System.Drawing.Size(180, 24);
            this.chkAllowCritical.Values.Text = "Allow critical postpone";
            // 
            // chkBannerPulse
            // 
            this.chkBannerPulse.Checked = true;
            this.chkBannerPulse.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBannerPulse.Name = "chkBannerPulse";
            this.chkBannerPulse.Size = new System.Drawing.Size(200, 24);
            this.chkBannerPulse.Values.Text = "Banner and pulse on 9.9.9";
            // 
            // chkSpeedGraph
            // 
            this.chkSpeedGraph.Name = "chkSpeedGraph";
            this.chkSpeedGraph.Size = new System.Drawing.Size(220, 24);
            this.chkSpeedGraph.Values.Text = "Transfer progress panel on 9.9.9";
            // 
            // flowButtons
            // 
            this.flowButtons.Controls.Add(this.btnCheckNow);
            this.flowButtons.Controls.Add(this.btnScheduled);
            this.flowButtons.Controls.Add(this.btnReset);
            this.flowButtons.Controls.Add(this.btnReleaseNotes);
            this.flowButtons.Controls.Add(this.btnGitHub);
            this.flowButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowButtons.Name = "flowButtons";
            this.flowButtons.WrapContents = false;
            // 
            // btnCheckNow
            // 
            this.btnCheckNow.Name = "btnCheckNow";
            this.btnCheckNow.Size = new System.Drawing.Size(120, 28);
            this.btnCheckNow.Values.Text = "Check now";
            // 
            // btnScheduled
            // 
            this.btnScheduled.Name = "btnScheduled";
            this.btnScheduled.Size = new System.Drawing.Size(140, 28);
            this.btnScheduled.Values.Text = "Scheduled check";
            // 
            // btnReset
            // 
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(110, 28);
            this.btnReset.Values.Text = "Reset state";
            // 
            // btnReleaseNotes
            // 
            this.btnReleaseNotes.Name = "btnReleaseNotes";
            this.btnReleaseNotes.Size = new System.Drawing.Size(120, 28);
            this.btnReleaseNotes.Values.Text = "Release notes";
            // 
            // btnGitHub
            // 
            this.btnGitHub.Name = "btnGitHub";
            this.btnGitHub.Size = new System.Drawing.Size(130, 28);
            this.btnGitHub.Values.Text = "GitHub check";
            // 
            // flowGitHub
            // 
            this.flowGitHub.Controls.Add(this.lblOwner);
            this.flowGitHub.Controls.Add(this.txtOwner);
            this.flowGitHub.Controls.Add(this.lblRepository);
            this.flowGitHub.Controls.Add(this.txtRepository);
            this.flowGitHub.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowGitHub.Name = "flowGitHub";
            this.flowGitHub.WrapContents = false;
            // 
            // lblOwner
            // 
            this.lblOwner.Name = "lblOwner";
            this.lblOwner.Values.Text = "Owner";
            // 
            // txtOwner
            // 
            this.txtOwner.Name = "txtOwner";
            this.txtOwner.Size = new System.Drawing.Size(180, 23);
            // 
            // lblRepository
            // 
            this.lblRepository.Name = "lblRepository";
            this.lblRepository.Values.Text = "Repository";
            // 
            // txtRepository
            // 
            this.txtRepository.Name = "txtRepository";
            this.txtRepository.Size = new System.Drawing.Size(220, 23);
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            // 
            // ApplicationUpdaterDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 620);
            this.Controls.Add(this.kryptonPanel);
            this.Name = "ApplicationUpdaterDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Application Updater (#4669)";
            this.Load += new System.EventHandler(this.ApplicationUpdaterDemo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).EndInit();
            this.kryptonPanel.ResumeLayout(false);
            this.table.ResumeLayout(false);
            this.table.PerformLayout();
            this.flowOptions.ResumeLayout(false);
            this.flowOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSeverity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbKind)).EndInit();
            this.flowButtons.ResumeLayout(false);
            this.flowGitHub.ResumeLayout(false);
            this.flowGitHub.PerformLayout();
            this.ResumeLayout(false);
        }

        private Krypton.Toolkit.KryptonPanel kryptonPanel;
        private System.Windows.Forms.TableLayoutPanel table;
        private Krypton.Toolkit.KryptonWrapLabel lblInfo;
        private System.Windows.Forms.FlowLayoutPanel flowOptions;
        private Krypton.Toolkit.KryptonLabel lblMode;
        private Krypton.Toolkit.KryptonComboBox cmbMode;
        private Krypton.Toolkit.KryptonLabel lblSeverity;
        private Krypton.Toolkit.KryptonComboBox cmbSeverity;
        private Krypton.Toolkit.KryptonLabel lblKind;
        private Krypton.Toolkit.KryptonComboBox cmbKind;
        private Krypton.Toolkit.KryptonLabel lblMaxDays;
        private Krypton.Toolkit.KryptonNumericUpDown nudMaxDays;
        private Krypton.Toolkit.KryptonCheckBox chkAllowCritical;
        private Krypton.Toolkit.KryptonCheckBox chkBannerPulse;
        private Krypton.Toolkit.KryptonCheckBox chkSpeedGraph;
        private System.Windows.Forms.FlowLayoutPanel flowButtons;
        private Krypton.Toolkit.KryptonButton btnCheckNow;
        private Krypton.Toolkit.KryptonButton btnScheduled;
        private Krypton.Toolkit.KryptonButton btnReset;
        private Krypton.Toolkit.KryptonButton btnReleaseNotes;
        private Krypton.Toolkit.KryptonButton btnGitHub;
        private System.Windows.Forms.FlowLayoutPanel flowGitHub;
        private Krypton.Toolkit.KryptonLabel lblOwner;
        private Krypton.Toolkit.KryptonTextBox txtOwner;
        private Krypton.Toolkit.KryptonLabel lblRepository;
        private Krypton.Toolkit.KryptonTextBox txtRepository;
        private Krypton.Toolkit.KryptonTextBox txtLog;
    }
}

namespace TestForm
{
    partial class TransferProgressPanelDemo
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
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.tableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.kwlblInfo = new Krypton.Toolkit.KryptonWrapLabel();
            this.flowToolbar = new System.Windows.Forms.FlowLayoutPanel();
            this.klblTheme = new Krypton.Toolkit.KryptonLabel();
            this.kcmbTheme = new Krypton.Toolkit.KryptonThemeComboBox();
            this.btnStart = new Krypton.Toolkit.KryptonButton();
            this.btnPause = new Krypton.Toolkit.KryptonButton();
            this.btnResume = new Krypton.Toolkit.KryptonButton();
            this.btnClear = new Krypton.Toolkit.KryptonButton();
            this.chkUnknownTotal = new Krypton.Toolkit.KryptonCheckBox();
            this.chkShowProgress = new Krypton.Toolkit.KryptonCheckBox();
            this.klblMode = new Krypton.Toolkit.KryptonLabel();
            this.kcmbMode = new Krypton.Toolkit.KryptonComboBox();
            this.klblLayout = new Krypton.Toolkit.KryptonLabel();
            this.kcmbLayout = new Krypton.Toolkit.KryptonComboBox();
            this.transferProgressPanel = new Krypton.Toolkit.Utilities.KryptonTransferProgressPanel();
            this.kwlblStatus = new Krypton.Toolkit.KryptonWrapLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.tableLayout.SuspendLayout();
            this.flowToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbTheme)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbLayout)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.tableLayout);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Padding = new System.Windows.Forms.Padding(12);
            this.kryptonPanel1.Size = new System.Drawing.Size(720, 320);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // tableLayout
            // 
            this.tableLayout.ColumnCount = 1;
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayout.Controls.Add(this.kwlblInfo, 0, 0);
            this.tableLayout.Controls.Add(this.flowToolbar, 0, 1);
            this.tableLayout.Controls.Add(this.transferProgressPanel, 0, 2);
            this.tableLayout.Controls.Add(this.kwlblStatus, 0, 3);
            this.tableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayout.Location = new System.Drawing.Point(12, 12);
            this.tableLayout.Name = "tableLayout";
            this.tableLayout.RowCount = 4;
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayout.Size = new System.Drawing.Size(696, 296);
            this.tableLayout.TabIndex = 0;
            // 
            // kwlblInfo
            // 
            this.kwlblInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlblInfo.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.kwlblInfo.Location = new System.Drawing.Point(3, 3);
            this.kwlblInfo.Name = "kwlblInfo";
            this.kwlblInfo.Size = new System.Drawing.Size(690, 50);
            this.kwlblInfo.Text = "KryptonTransferProgressPanel: SideBySide (speed + progress charts) or DualSeries (one chart, two series). Toggle Show progress, sample mode, and chart layout. Unknown total uses Auto→bytes.";
            // 
            // flowToolbar
            // 
            this.flowToolbar.Controls.Add(this.klblTheme);
            this.flowToolbar.Controls.Add(this.kcmbTheme);
            this.flowToolbar.Controls.Add(this.btnStart);
            this.flowToolbar.Controls.Add(this.btnPause);
            this.flowToolbar.Controls.Add(this.btnResume);
            this.flowToolbar.Controls.Add(this.btnClear);
            this.flowToolbar.Controls.Add(this.chkUnknownTotal);
            this.flowToolbar.Controls.Add(this.chkShowProgress);
            this.flowToolbar.Controls.Add(this.klblMode);
            this.flowToolbar.Controls.Add(this.kcmbMode);
            this.flowToolbar.Controls.Add(this.klblLayout);
            this.flowToolbar.Controls.Add(this.kcmbLayout);
            this.flowToolbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowToolbar.Location = new System.Drawing.Point(3, 59);
            this.flowToolbar.Name = "flowToolbar";
            this.flowToolbar.Size = new System.Drawing.Size(690, 38);
            this.flowToolbar.TabIndex = 1;
            // 
            // klblTheme
            // 
            this.klblTheme.Location = new System.Drawing.Point(3, 8);
            this.klblTheme.Name = "klblTheme";
            this.klblTheme.Size = new System.Drawing.Size(45, 20);
            this.klblTheme.TabIndex = 0;
            this.klblTheme.Values.Text = "Theme";
            // 
            // kcmbTheme
            // 
            this.kcmbTheme.DropDownWidth = 220;
            this.kcmbTheme.Location = new System.Drawing.Point(54, 5);
            this.kcmbTheme.Name = "kcmbTheme";
            this.kcmbTheme.Size = new System.Drawing.Size(180, 22);
            this.kcmbTheme.TabIndex = 1;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(240, 3);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(72, 28);
            this.btnStart.TabIndex = 2;
            this.btnStart.Values.Text = "Start";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnPause
            // 
            this.btnPause.Enabled = false;
            this.btnPause.Location = new System.Drawing.Point(318, 3);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(72, 28);
            this.btnPause.TabIndex = 3;
            this.btnPause.Values.Text = "Pause";
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnResume
            // 
            this.btnResume.Enabled = false;
            this.btnResume.Location = new System.Drawing.Point(396, 3);
            this.btnResume.Name = "btnResume";
            this.btnResume.Size = new System.Drawing.Size(72, 28);
            this.btnResume.TabIndex = 4;
            this.btnResume.Values.Text = "Resume";
            this.btnResume.Click += new System.EventHandler(this.btnResume_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(474, 3);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(72, 28);
            this.btnClear.TabIndex = 5;
            this.btnClear.Values.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // chkUnknownTotal
            // 
            this.chkUnknownTotal.Location = new System.Drawing.Point(552, 6);
            this.chkUnknownTotal.Name = "chkUnknownTotal";
            this.chkUnknownTotal.Size = new System.Drawing.Size(120, 20);
            this.chkUnknownTotal.TabIndex = 6;
            this.chkUnknownTotal.Values.Text = "Unknown total";
            this.chkUnknownTotal.CheckedChanged += new System.EventHandler(this.chkUnknownTotal_CheckedChanged);
            // 
            // chkShowProgress
            // 
            this.chkShowProgress.Checked = true;
            this.chkShowProgress.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowProgress.Location = new System.Drawing.Point(678, 6);
            this.chkShowProgress.Name = "chkShowProgress";
            this.chkShowProgress.Size = new System.Drawing.Size(110, 20);
            this.chkShowProgress.TabIndex = 7;
            this.chkShowProgress.Values.Text = "Show progress";
            this.chkShowProgress.CheckedChanged += new System.EventHandler(this.chkShowProgress_CheckedChanged);
            // 
            // klblMode
            // 
            this.klblMode.Location = new System.Drawing.Point(794, 6);
            this.klblMode.Name = "klblMode";
            this.klblMode.Size = new System.Drawing.Size(40, 20);
            this.klblMode.TabIndex = 8;
            this.klblMode.Values.Text = "Mode";
            // 
            // kcmbMode
            // 
            this.kcmbMode.DropDownWidth = 100;
            this.kcmbMode.Location = new System.Drawing.Point(840, 5);
            this.kcmbMode.Name = "kcmbMode";
            this.kcmbMode.Size = new System.Drawing.Size(100, 22);
            this.kcmbMode.TabIndex = 9;
            this.kcmbMode.SelectedIndexChanged += new System.EventHandler(this.kcmbMode_SelectedIndexChanged);
            // 
            // klblLayout
            // 
            this.klblLayout.Location = new System.Drawing.Point(946, 6);
            this.klblLayout.Name = "klblLayout";
            this.klblLayout.Size = new System.Drawing.Size(44, 20);
            this.klblLayout.TabIndex = 10;
            this.klblLayout.Values.Text = "Layout";
            // 
            // kcmbLayout
            // 
            this.kcmbLayout.DropDownWidth = 120;
            this.kcmbLayout.Location = new System.Drawing.Point(996, 5);
            this.kcmbLayout.Name = "kcmbLayout";
            this.kcmbLayout.Size = new System.Drawing.Size(110, 22);
            this.kcmbLayout.TabIndex = 11;
            this.kcmbLayout.SelectedIndexChanged += new System.EventHandler(this.kcmbLayout_SelectedIndexChanged);
            // 
            // transferProgressPanel
            // 
            this.transferProgressPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.transferProgressPanel.Location = new System.Drawing.Point(3, 103);
            this.transferProgressPanel.Name = "transferProgressPanel";
            this.transferProgressPanel.Size = new System.Drawing.Size(690, 154);
            this.transferProgressPanel.TabIndex = 2;
            this.transferProgressPanel.TabStop = false;
            // 
            // kwlblStatus
            // 
            this.kwlblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlblStatus.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.kwlblStatus.Location = new System.Drawing.Point(3, 263);
            this.kwlblStatus.Name = "kwlblStatus";
            this.kwlblStatus.Size = new System.Drawing.Size(690, 30);
            this.kwlblStatus.Text = "Ready.";
            // 
            // TransferProgressPanelDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 320);
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "TransferProgressPanelDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Transfer Progress Panel";
            this.Load += new System.EventHandler(this.TransferProgressPanelDemo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.tableLayout.ResumeLayout(false);
            this.tableLayout.PerformLayout();
            this.flowToolbar.ResumeLayout(false);
            this.flowToolbar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbTheme)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbLayout)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayout;
        private Krypton.Toolkit.KryptonWrapLabel kwlblInfo;
        private System.Windows.Forms.FlowLayoutPanel flowToolbar;
        private Krypton.Toolkit.KryptonLabel klblTheme;
        private Krypton.Toolkit.KryptonThemeComboBox kcmbTheme;
        private Krypton.Toolkit.KryptonButton btnStart;
        private Krypton.Toolkit.KryptonButton btnPause;
        private Krypton.Toolkit.KryptonButton btnResume;
        private Krypton.Toolkit.KryptonButton btnClear;
        private Krypton.Toolkit.KryptonCheckBox chkUnknownTotal;
        private Krypton.Toolkit.KryptonCheckBox chkShowProgress;
        private Krypton.Toolkit.KryptonLabel klblMode;
        private Krypton.Toolkit.KryptonComboBox kcmbMode;
        private Krypton.Toolkit.KryptonLabel klblLayout;
        private Krypton.Toolkit.KryptonComboBox kcmbLayout;
        private Krypton.Toolkit.Utilities.KryptonTransferProgressPanel transferProgressPanel;
        private Krypton.Toolkit.KryptonWrapLabel kwlblStatus;
    }
}

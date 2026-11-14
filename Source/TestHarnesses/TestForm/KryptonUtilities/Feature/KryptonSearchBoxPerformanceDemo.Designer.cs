namespace TestForm
{
    partial class KryptonSearchBoxPerformanceDemo
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.kpnlMain = new Krypton.Toolkit.KryptonPanel();
            this.tableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.kwlblInfo = new Krypton.Toolkit.KryptonWrapLabel();
            this.flowToolbar = new System.Windows.Forms.FlowLayoutPanel();
            this.klblTheme = new Krypton.Toolkit.KryptonLabel();
            this.kcmbTheme = new Krypton.Toolkit.KryptonThemeComboBox();
            this.klblDelay = new Krypton.Toolkit.KryptonLabel();
            this.nudDelay = new Krypton.Toolkit.KryptonNumericUpDown();
            this.ksbPerf = new Krypton.Toolkit.Utilities.KryptonSearchBox();
            this.klblStatus = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).BeginInit();
            this.kpnlMain.SuspendLayout();
            this.tableLayout.SuspendLayout();
            this.flowToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbTheme)).BeginInit();
            this.SuspendLayout();
            //
            // kpnlMain
            //
            this.kpnlMain.Controls.Add(this.tableLayout);
            this.kpnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlMain.Location = new System.Drawing.Point(0, 0);
            this.kpnlMain.Name = "kpnlMain";
            this.kpnlMain.Padding = new System.Windows.Forms.Padding(12);
            this.kpnlMain.Size = new System.Drawing.Size(640, 280);
            this.kpnlMain.TabIndex = 0;
            //
            // tableLayout
            //
            this.tableLayout.ColumnCount = 1;
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayout.Controls.Add(this.kwlblInfo, 0, 0);
            this.tableLayout.Controls.Add(this.flowToolbar, 0, 1);
            this.tableLayout.Controls.Add(this.ksbPerf, 0, 2);
            this.tableLayout.Controls.Add(this.klblStatus, 0, 3);
            this.tableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayout.Location = new System.Drawing.Point(12, 12);
            this.tableLayout.Name = "tableLayout";
            this.tableLayout.RowCount = 4;
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayout.Size = new System.Drawing.Size(616, 256);
            this.tableLayout.TabIndex = 0;
            //
            // kwlblInfo
            //
            this.kwlblInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlblInfo.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.kwlblInfo.Location = new System.Drawing.Point(3, 3);
            this.kwlblInfo.Name = "kwlblInfo";
            this.kwlblInfo.Size = new System.Drawing.Size(610, 66);
            this.kwlblInfo.Text = "Issue #4580: KryptonSearchBox with 5,000 suggestions. Type quickly (e.g. \"item 12\"), use Up/Down and Enter or click a suggestion. Typing, dropdown, and selection should stay responsive. Adjust SuggestionUpdateDelay (0 = immediate).";
            //
            // flowToolbar
            //
            this.flowToolbar.AutoSize = true;
            this.flowToolbar.Controls.Add(this.klblTheme);
            this.flowToolbar.Controls.Add(this.kcmbTheme);
            this.flowToolbar.Controls.Add(this.klblDelay);
            this.flowToolbar.Controls.Add(this.nudDelay);
            this.flowToolbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowToolbar.Location = new System.Drawing.Point(0, 72);
            this.flowToolbar.Margin = new System.Windows.Forms.Padding(0);
            this.flowToolbar.Name = "flowToolbar";
            this.flowToolbar.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.flowToolbar.Size = new System.Drawing.Size(616, 40);
            this.flowToolbar.TabIndex = 1;
            this.flowToolbar.WrapContents = false;
            //
            // klblTheme
            //
            this.klblTheme.Location = new System.Drawing.Point(3, 7);
            this.klblTheme.Name = "klblTheme";
            this.klblTheme.Size = new System.Drawing.Size(48, 20);
            this.klblTheme.TabIndex = 0;
            this.klblTheme.Values.Text = "Theme:";
            //
            // kcmbTheme
            //
            this.kcmbTheme.DropDownWidth = 220;
            this.kcmbTheme.Location = new System.Drawing.Point(57, 7);
            this.kcmbTheme.Name = "kcmbTheme";
            this.kcmbTheme.Size = new System.Drawing.Size(220, 21);
            this.kcmbTheme.TabIndex = 1;
            //
            // klblDelay
            //
            this.klblDelay.Location = new System.Drawing.Point(283, 7);
            this.klblDelay.Name = "klblDelay";
            this.klblDelay.Size = new System.Drawing.Size(120, 20);
            this.klblDelay.TabIndex = 2;
            this.klblDelay.Values.Text = "Update delay (ms):";
            //
            // nudDelay
            //
            this.nudDelay.Location = new System.Drawing.Point(409, 7);
            this.nudDelay.Maximum = new decimal(new int[] { 500, 0, 0, 0 });
            this.nudDelay.Name = "nudDelay";
            this.nudDelay.Size = new System.Drawing.Size(64, 22);
            this.nudDelay.TabIndex = 3;
            this.nudDelay.Value = new decimal(new int[] { 75, 0, 0, 0 });
            this.nudDelay.ValueChanged += new System.EventHandler(this.nudDelay_ValueChanged);
            //
            // ksbPerf
            //
            this.ksbPerf.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ksbPerf.Location = new System.Drawing.Point(3, 115);
            this.ksbPerf.Name = "ksbPerf";
            this.ksbPerf.Size = new System.Drawing.Size(610, 30);
            this.ksbPerf.TabIndex = 2;
            this.ksbPerf.SuggestionSelected += new System.EventHandler<Krypton.Toolkit.Utilities.SuggestionSelectedEventArgs>(this.ksbPerf_SuggestionSelected);
            this.ksbPerf.Search += new System.EventHandler<Krypton.Toolkit.Utilities.SearchEventArgs>(this.ksbPerf_Search);
            //
            // klblStatus
            //
            this.klblStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.klblStatus.Location = new System.Drawing.Point(3, 151);
            this.klblStatus.Name = "klblStatus";
            this.klblStatus.Size = new System.Drawing.Size(610, 20);
            this.klblStatus.TabIndex = 3;
            this.klblStatus.Values.Text = "Status";
            //
            // KryptonSearchBoxPerformanceDemo
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 280);
            this.Controls.Add(this.kpnlMain);
            this.Name = "KryptonSearchBoxPerformanceDemo";
            this.Text = "KryptonSearchBox Performance (#4580)";
            this.Load += new System.EventHandler(this.KryptonSearchBoxPerformanceDemo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).EndInit();
            this.kpnlMain.ResumeLayout(false);
            this.tableLayout.ResumeLayout(false);
            this.tableLayout.PerformLayout();
            this.flowToolbar.ResumeLayout(false);
            this.flowToolbar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbTheme)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kpnlMain;
        private System.Windows.Forms.TableLayoutPanel tableLayout;
        private Krypton.Toolkit.KryptonWrapLabel kwlblInfo;
        private System.Windows.Forms.FlowLayoutPanel flowToolbar;
        private Krypton.Toolkit.KryptonLabel klblTheme;
        private Krypton.Toolkit.KryptonThemeComboBox kcmbTheme;
        private Krypton.Toolkit.KryptonLabel klblDelay;
        private Krypton.Toolkit.KryptonNumericUpDown nudDelay;
        private Krypton.Toolkit.Utilities.KryptonSearchBox ksbPerf;
        private Krypton.Toolkit.KryptonLabel klblStatus;
    }
}
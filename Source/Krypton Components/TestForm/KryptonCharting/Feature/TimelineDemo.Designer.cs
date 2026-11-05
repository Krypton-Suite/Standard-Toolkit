namespace TestForm
{
    partial class TimelineDemo
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
            this.klblOrientation = new Krypton.Toolkit.KryptonLabel();
            this.cmbOrientation = new Krypton.Toolkit.KryptonComboBox();
            this.klblLayout = new Krypton.Toolkit.KryptonLabel();
            this.cmbLayout = new Krypton.Toolkit.KryptonComboBox();
            this.klblTheme = new Krypton.Toolkit.KryptonLabel();
            this.kcmbTheme = new Krypton.Toolkit.KryptonThemeComboBox();
            this.klblDateStyle = new Krypton.Toolkit.KryptonLabel();
            this.cmbDateStyle = new Krypton.Toolkit.KryptonComboBox();
            this.kchkCustomColours = new Krypton.Toolkit.KryptonCheckBox();
            this.klblSteps = new Krypton.Toolkit.KryptonLabel();
            this.timeline = new Krypton.Charting.KryptonTimeline();
            this.klblDates = new Krypton.Toolkit.KryptonLabel();
            this.dateTimeline = new Krypton.Charting.KryptonDateTimeline();
            this.kwlblStatus = new Krypton.Toolkit.KryptonWrapLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.tableLayout.SuspendLayout();
            this.flowToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOrientation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLayout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbTheme)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDateStyle)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.tableLayout);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Padding = new System.Windows.Forms.Padding(12);
            this.kryptonPanel1.Size = new System.Drawing.Size(1080, 760);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // tableLayout
            // 
            this.tableLayout.ColumnCount = 1;
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayout.Controls.Add(this.kwlblInfo, 0, 0);
            this.tableLayout.Controls.Add(this.flowToolbar, 0, 1);
            this.tableLayout.Controls.Add(this.klblSteps, 0, 2);
            this.tableLayout.Controls.Add(this.timeline, 0, 3);
            this.tableLayout.Controls.Add(this.klblDates, 0, 4);
            this.tableLayout.Controls.Add(this.dateTimeline, 0, 5);
            this.tableLayout.Controls.Add(this.kwlblStatus, 0, 6);
            this.tableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayout.Location = new System.Drawing.Point(12, 12);
            this.tableLayout.Name = "tableLayout";
            this.tableLayout.RowCount = 7;
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 58F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayout.Size = new System.Drawing.Size(1056, 736);
            this.tableLayout.TabIndex = 0;
            // 
            // kwlblInfo
            // 
            this.kwlblInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlblInfo.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.kwlblInfo.Location = new System.Drawing.Point(3, 3);
            this.kwlblInfo.Name = "kwlblInfo";
            this.kwlblInfo.Size = new System.Drawing.Size(1050, 58);
            this.kwlblInfo.Text = "Step timeline: switch orientation and layout, or drag a step to reorder it. Each item has a Style: Normal, Hatched, Completed, Delayed, or Cancelled. Custom colours edits ItemStyles (stripe, hatch, and mark) on both timelines. Date timeline: Chart draws row bars; Vis stacks boxes above the axis. Drag an item to move its dates (Chart also changes row). Ctrl+wheel zooms. Drag empty space to pan.";
            // 
            // flowToolbar
            // 
            this.flowToolbar.Controls.Add(this.klblOrientation);
            this.flowToolbar.Controls.Add(this.cmbOrientation);
            this.flowToolbar.Controls.Add(this.klblLayout);
            this.flowToolbar.Controls.Add(this.cmbLayout);
            this.flowToolbar.Controls.Add(this.klblTheme);
            this.flowToolbar.Controls.Add(this.kcmbTheme);
            this.flowToolbar.Controls.Add(this.klblDateStyle);
            this.flowToolbar.Controls.Add(this.cmbDateStyle);
            this.flowToolbar.Controls.Add(this.kchkCustomColours);
            this.flowToolbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowToolbar.Location = new System.Drawing.Point(3, 67);
            this.flowToolbar.Name = "flowToolbar";
            this.flowToolbar.Size = new System.Drawing.Size(1050, 34);
            this.flowToolbar.TabIndex = 1;
            // 
            // klblOrientation
            // 
            this.klblOrientation.Location = new System.Drawing.Point(3, 8);
            this.klblOrientation.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.klblOrientation.Name = "klblOrientation";
            this.klblOrientation.Size = new System.Drawing.Size(74, 20);
            this.klblOrientation.Values.Text = "Orientation:";
            // 
            // cmbOrientation
            // 
            this.cmbOrientation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOrientation.DropDownWidth = 140;
            this.cmbOrientation.Location = new System.Drawing.Point(83, 5);
            this.cmbOrientation.Margin = new System.Windows.Forms.Padding(3, 5, 12, 3);
            this.cmbOrientation.Name = "cmbOrientation";
            this.cmbOrientation.Size = new System.Drawing.Size(140, 22);
            this.cmbOrientation.TabIndex = 0;
            this.cmbOrientation.SelectedIndexChanged += new System.EventHandler(this.cmbOrientation_SelectedIndexChanged);
            // 
            // klblLayout
            // 
            this.klblLayout.Location = new System.Drawing.Point(238, 8);
            this.klblLayout.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.klblLayout.Name = "klblLayout";
            this.klblLayout.Size = new System.Drawing.Size(46, 20);
            this.klblLayout.Values.Text = "Layout:";
            // 
            // cmbLayout
            // 
            this.cmbLayout.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLayout.DropDownWidth = 140;
            this.cmbLayout.Location = new System.Drawing.Point(290, 5);
            this.cmbLayout.Margin = new System.Windows.Forms.Padding(3, 5, 12, 3);
            this.cmbLayout.Name = "cmbLayout";
            this.cmbLayout.Size = new System.Drawing.Size(140, 22);
            this.cmbLayout.TabIndex = 1;
            this.cmbLayout.SelectedIndexChanged += new System.EventHandler(this.cmbLayout_SelectedIndexChanged);
            // 
            // klblTheme
            // 
            this.klblTheme.Location = new System.Drawing.Point(445, 8);
            this.klblTheme.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.klblTheme.Name = "klblTheme";
            this.klblTheme.Size = new System.Drawing.Size(48, 20);
            this.klblTheme.Values.Text = "Theme:";
            // 
            // kcmbTheme
            // 
            this.kcmbTheme.DropDownWidth = 220;
            this.kcmbTheme.Location = new System.Drawing.Point(499, 5);
            this.kcmbTheme.Margin = new System.Windows.Forms.Padding(3, 5, 12, 3);
            this.kcmbTheme.Name = "kcmbTheme";
            this.kcmbTheme.Size = new System.Drawing.Size(220, 21);
            this.kcmbTheme.TabIndex = 2;
            this.kcmbTheme.SelectedIndexChanged += new System.EventHandler(this.kcmbTheme_SelectedIndexChanged);
            // 
            // klblDateStyle
            // 
            this.klblDateStyle.Location = new System.Drawing.Point(734, 8);
            this.klblDateStyle.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.klblDateStyle.Name = "klblDateStyle";
            this.klblDateStyle.Size = new System.Drawing.Size(72, 20);
            this.klblDateStyle.Values.Text = "Date style:";
            // 
            // cmbDateStyle
            // 
            this.cmbDateStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDateStyle.DropDownWidth = 90;
            this.cmbDateStyle.Location = new System.Drawing.Point(812, 5);
            this.cmbDateStyle.Margin = new System.Windows.Forms.Padding(3, 5, 12, 3);
            this.cmbDateStyle.Name = "cmbDateStyle";
            this.cmbDateStyle.Size = new System.Drawing.Size(90, 22);
            this.cmbDateStyle.TabIndex = 3;
            this.cmbDateStyle.SelectedIndexChanged += new System.EventHandler(this.cmbDateStyle_SelectedIndexChanged);
            // 
            // kchkCustomColours
            // 
            this.kchkCustomColours.Location = new System.Drawing.Point(3, 5);
            this.kchkCustomColours.Margin = new System.Windows.Forms.Padding(12, 6, 3, 3);
            this.kchkCustomColours.Name = "kchkCustomColours";
            this.kchkCustomColours.Size = new System.Drawing.Size(113, 20);
            this.kchkCustomColours.TabIndex = 4;
            this.kchkCustomColours.Values.Text = "Custom colours";
            this.kchkCustomColours.CheckedChanged += new System.EventHandler(this.kchkCustomColours_CheckedChanged);
            // 
            // klblSteps
            // 
            this.klblSteps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblSteps.Location = new System.Drawing.Point(3, 107);
            this.klblSteps.Name = "klblSteps";
            this.klblSteps.Size = new System.Drawing.Size(1050, 18);
            this.klblSteps.Values.Text = "Step timeline";
            // 
            // timeline
            // 
            this.timeline.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timeline.Location = new System.Drawing.Point(3, 131);
            this.timeline.Name = "timeline";
            this.timeline.Size = new System.Drawing.Size(1050, 220);
            this.timeline.TabIndex = 3;
            this.timeline.ItemClick += new System.EventHandler<Krypton.Charting.KryptonTimelineItemEventArgs>(this.timeline_ItemClick);
            // 
            // klblDates
            // 
            this.klblDates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblDates.Location = new System.Drawing.Point(3, 357);
            this.klblDates.Name = "klblDates";
            this.klblDates.Size = new System.Drawing.Size(1050, 18);
            this.klblDates.Values.Text = "Date timeline (chart rows, or vis.js stacked boxes)";
            // 
            // dateTimeline
            // 
            this.dateTimeline.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dateTimeline.Location = new System.Drawing.Point(3, 381);
            this.dateTimeline.Name = "dateTimeline";
            this.dateTimeline.Size = new System.Drawing.Size(1050, 280);
            this.dateTimeline.TabIndex = 4;
            this.dateTimeline.ItemClick += new System.EventHandler<Krypton.Charting.KryptonDateTimelineItemEventArgs>(this.dateTimeline_ItemClick);
            // 
            // kwlblStatus
            // 
            this.kwlblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlblStatus.Location = new System.Drawing.Point(3, 667);
            this.kwlblStatus.Name = "kwlblStatus";
            this.kwlblStatus.Size = new System.Drawing.Size(1050, 30);
            this.kwlblStatus.Text = "Ready.";
            // 
            // TimelineDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 760);
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "TimelineDemo";
            this.Text = "Timeline (#4218)";
            this.Load += new System.EventHandler(this.TimelineDemo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.tableLayout.ResumeLayout(false);
            this.tableLayout.PerformLayout();
            this.flowToolbar.ResumeLayout(false);
            this.flowToolbar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOrientation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLayout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbTheme)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDateStyle)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayout;
        private Krypton.Toolkit.KryptonWrapLabel kwlblInfo;
        private System.Windows.Forms.FlowLayoutPanel flowToolbar;
        private Krypton.Toolkit.KryptonLabel klblOrientation;
        private Krypton.Toolkit.KryptonComboBox cmbOrientation;
        private Krypton.Toolkit.KryptonLabel klblLayout;
        private Krypton.Toolkit.KryptonComboBox cmbLayout;
        private Krypton.Toolkit.KryptonLabel klblTheme;
        private Krypton.Toolkit.KryptonThemeComboBox kcmbTheme;
        private Krypton.Toolkit.KryptonLabel klblDateStyle;
        private Krypton.Toolkit.KryptonComboBox cmbDateStyle;
        private Krypton.Toolkit.KryptonCheckBox kchkCustomColours;
        private Krypton.Toolkit.KryptonLabel klblSteps;
        private Krypton.Charting.KryptonTimeline timeline;
        private Krypton.Toolkit.KryptonLabel klblDates;
        private Krypton.Charting.KryptonDateTimeline dateTimeline;
        private Krypton.Toolkit.KryptonWrapLabel kwlblStatus;
    }
}

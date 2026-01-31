namespace TestForm
{
    partial class RTLControlsTest
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
            this.grpRtlToggle = new Krypton.Toolkit.KryptonGroupBox();
            this.lblRtlStatus = new Krypton.Toolkit.KryptonLabel();
            this.btnToggleRtl = new Krypton.Toolkit.KryptonButton();
            this.lblExample1 = new Krypton.Toolkit.KryptonLabel();
            this.grpCalendars = new Krypton.Toolkit.KryptonGroupBox();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.calendarLtr = new Krypton.Toolkit.KryptonMonthCalendar();
            this.lblExample2 = new Krypton.Toolkit.KryptonLabel();
            this.calendarRtl = new Krypton.Toolkit.KryptonMonthCalendar();
            this.lblExample3 = new Krypton.Toolkit.KryptonLabel();
            this.calendarMultiMonth = new Krypton.Toolkit.KryptonMonthCalendar();
            this.lblExample4 = new Krypton.Toolkit.KryptonLabel();
            this.calendarFeatures = new Krypton.Toolkit.KryptonMonthCalendar();
            this.lblExample5 = new Krypton.Toolkit.KryptonLabel();
            this.btnApplyToAll = new Krypton.Toolkit.KryptonButton();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.lblPropertyGrid = new Krypton.Toolkit.KryptonLabel();
            this.lblStatus = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.grpRtlToggle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpRtlToggle.Panel)).BeginInit();
            this.grpRtlToggle.Panel.SuspendLayout();
            this.grpRtlToggle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpCalendars)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpCalendars.Panel)).BeginInit();
            this.grpCalendars.Panel.SuspendLayout();
            this.grpCalendars.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpRtlToggle
            // 
            this.grpRtlToggle.Location = new System.Drawing.Point(12, 12);
            this.grpRtlToggle.Name = "grpRtlToggle";
            this.grpRtlToggle.Size = new System.Drawing.Size(780, 100);
            this.grpRtlToggle.TabIndex = 0;
            this.grpRtlToggle.Values.Heading = "RTL Toggle Example";
            // 
            // grpRtlToggle.Panel
            // 
            this.grpRtlToggle.Panel.Controls.Add(this.lblRtlStatus);
            this.grpRtlToggle.Panel.Controls.Add(this.btnToggleRtl);
            this.grpRtlToggle.Panel.Controls.Add(this.lblExample1);
            // 
            // lblRtlStatus
            // 
            this.lblRtlStatus.Location = new System.Drawing.Point(200, 50);
            this.lblRtlStatus.Name = "lblRtlStatus";
            this.lblRtlStatus.Size = new System.Drawing.Size(200, 20);
            this.lblRtlStatus.TabIndex = 2;
            this.lblRtlStatus.Values.Text = "RTL Layout: Disabled";
            // 
            // btnToggleRtl
            // 
            this.btnToggleRtl.Location = new System.Drawing.Point(15, 45);
            this.btnToggleRtl.Name = "btnToggleRtl";
            this.btnToggleRtl.Size = new System.Drawing.Size(160, 35);
            this.btnToggleRtl.TabIndex = 1;
            this.btnToggleRtl.Values.Text = "Toggle RTL";
            // 
            // lblExample1
            // 
            this.lblExample1.Location = new System.Drawing.Point(15, 20);
            this.lblExample1.Name = "lblExample1";
            this.lblExample1.Size = new System.Drawing.Size(750, 20);
            this.lblExample1.TabIndex = 0;
            this.lblExample1.Values.Text = "Example 1: Toggle RTL layout";
            // 
            // grpCalendars
            // 
            this.grpCalendars.Location = new System.Drawing.Point(12, 118);
            this.grpCalendars.Name = "grpCalendars";
            this.grpCalendars.Size = new System.Drawing.Size(780, 500);
            this.grpCalendars.TabIndex = 1;
            this.grpCalendars.Values.Heading = "Calendar Examples";
            // 
            // grpCalendars.Panel
            // 
            this.grpCalendars.Panel.Controls.Add(this.tableLayoutPanel);
            this.grpCalendars.Panel.Controls.Add(this.btnApplyToAll);
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.Controls.Add(this.calendarLtr, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.lblExample2, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.calendarRtl, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.lblExample3, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.calendarMultiMonth, 0, 3);
            this.tableLayoutPanel.Controls.Add(this.lblExample4, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.calendarFeatures, 1, 3);
            this.tableLayoutPanel.Location = new System.Drawing.Point(15, 15);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 5;
            this.tableLayoutPanel.Controls.Add(this.lblExample5, 0, 4);
            this.tableLayoutPanel.SetColumnSpan(this.lblExample5, 2);
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(750, 400);
            this.tableLayoutPanel.TabIndex = 3;
            // 
            // calendarLtr
            // 
            this.calendarLtr.Location = new System.Drawing.Point(3, 28);
            this.calendarLtr.Name = "calendarLtr";
            this.calendarLtr.Size = new System.Drawing.Size(372, 172);
            this.calendarLtr.TabIndex = 0;
            // 
            // lblExample2
            // 
            this.lblExample2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblExample2.Location = new System.Drawing.Point(3, 0);
            this.lblExample2.Name = "lblExample2";
            this.lblExample2.Size = new System.Drawing.Size(372, 25);
            this.lblExample2.TabIndex = 1;
            this.lblExample2.Values.Text = "Example 2: LTR calendar (toggleable)";
            // 
            // calendarRtl
            // 
            this.calendarRtl.Location = new System.Drawing.Point(378, 28);
            this.calendarRtl.Name = "calendarRtl";
            this.calendarRtl.Size = new System.Drawing.Size(369, 172);
            this.calendarRtl.TabIndex = 2;
            // 
            // lblExample3
            // 
            this.lblExample3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblExample3.Location = new System.Drawing.Point(378, 0);
            this.lblExample3.Name = "lblExample3";
            this.lblExample3.Size = new System.Drawing.Size(369, 25);
            this.lblExample3.TabIndex = 3;
            this.lblExample3.Values.Text = "Example 3: Multi-month calendar (2x2)";
            // 
            // calendarMultiMonth
            // 
            this.calendarMultiMonth.CalendarDimensions = new System.Drawing.Size(2, 2);
            this.calendarMultiMonth.Location = new System.Drawing.Point(3, 231);
            this.calendarMultiMonth.Name = "calendarMultiMonth";
            this.calendarMultiMonth.Size = new System.Drawing.Size(372, 126);
            this.calendarMultiMonth.TabIndex = 4;
            // 
            // lblExample4
            // 
            this.lblExample4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblExample4.Location = new System.Drawing.Point(3, 203);
            this.lblExample4.Name = "lblExample4";
            this.lblExample4.Size = new System.Drawing.Size(372, 25);
            this.lblExample4.TabIndex = 5;
            this.lblExample4.Values.Text = "Example 4: Calendar with week numbers";
            // 
            // calendarFeatures
            // 
            this.calendarFeatures.Location = new System.Drawing.Point(378, 231);
            this.calendarFeatures.Name = "calendarFeatures";
            this.calendarFeatures.ShowWeekNumbers = true;
            this.calendarFeatures.Size = new System.Drawing.Size(369, 126);
            this.calendarFeatures.TabIndex = 6;
            // 
            // lblExample5
            // 
            this.lblExample5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblExample5.Location = new System.Drawing.Point(3, 363);
            this.lblExample5.Name = "lblExample5";
            this.lblExample5.Size = new System.Drawing.Size(744, 34);
            this.lblExample5.TabIndex = 7;
            this.lblExample5.Values.Text = "Example 5: Global RTL Support - All VisualSimpleBase controls inherit RTL support";
            // 
            // btnApplyToAll
            // 
            this.btnApplyToAll.Location = new System.Drawing.Point(15, 425);
            this.btnApplyToAll.Name = "btnApplyToAll";
            this.btnApplyToAll.Size = new System.Drawing.Size(200, 35);
            this.btnApplyToAll.TabIndex = 2;
            this.btnApplyToAll.Values.Text = "Apply RTL to All Calendars";
            this.btnApplyToAll.Click += new System.EventHandler(this.BtnApplyToAll_Click);
            // 
            // propertyGrid
            // 
            this.propertyGrid.Location = new System.Drawing.Point(810, 60);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(280, 480);
            this.propertyGrid.TabIndex = 2;
            this.propertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.PropertyGrid_PropertyValueChanged);
            // 
            // lblPropertyGrid
            // 
            this.lblPropertyGrid.Location = new System.Drawing.Point(810, 35);
            this.lblPropertyGrid.Name = "lblPropertyGrid";
            this.lblPropertyGrid.Size = new System.Drawing.Size(280, 20);
            this.lblPropertyGrid.TabIndex = 3;
            this.lblPropertyGrid.Values.Text = "Properties (Select calendar in left panel)";
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(12, 625);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(1078, 20);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Values.Text = "Status: Ready";
            // 
            // MonthCalendarRtlTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1102, 655);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblPropertyGrid);
            this.Controls.Add(this.propertyGrid);
            this.Controls.Add(this.grpCalendars);
            this.Controls.Add(this.grpRtlToggle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = System.Drawing.SystemIcons.Application;
            this.MaximizeBox = false;
            this.Name = "MonthCalendarRtlTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Month Calendar RTL Test";
            ((System.ComponentModel.ISupportInitialize)(this.grpRtlToggle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpRtlToggle.Panel)).EndInit();
            this.grpRtlToggle.Panel.ResumeLayout(false);
            this.grpRtlToggle.Panel.PerformLayout();
            this.grpRtlToggle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpCalendars)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpCalendars.Panel)).EndInit();
            this.grpCalendars.Panel.ResumeLayout(false);
            this.grpCalendars.ResumeLayout(false);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonGroupBox grpRtlToggle;
        private Krypton.Toolkit.KryptonLabel lblExample1;
        private Krypton.Toolkit.KryptonButton btnToggleRtl;
        private Krypton.Toolkit.KryptonLabel lblRtlStatus;
        private Krypton.Toolkit.KryptonGroupBox grpCalendars;
        private Krypton.Toolkit.KryptonMonthCalendar calendarLtr;
        private Krypton.Toolkit.KryptonMonthCalendar calendarRtl;
        private Krypton.Toolkit.KryptonMonthCalendar calendarMultiMonth;
        private Krypton.Toolkit.KryptonMonthCalendar calendarFeatures;
        private Krypton.Toolkit.KryptonLabel lblExample2;
        private Krypton.Toolkit.KryptonLabel lblExample3;
        private Krypton.Toolkit.KryptonLabel lblExample4;
        private Krypton.Toolkit.KryptonLabel lblExample5;
        private Krypton.Toolkit.KryptonButton btnApplyToAll;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private Krypton.Toolkit.KryptonLabel lblPropertyGrid;
        private Krypton.Toolkit.KryptonLabel lblStatus;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
    }
}
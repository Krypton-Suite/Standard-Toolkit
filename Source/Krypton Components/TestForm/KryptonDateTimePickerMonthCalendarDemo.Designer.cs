namespace TestForm
{
    partial class KryptonDateTimePickerMonthCalendarDemo
    {
        private System.ComponentModel.IContainer? components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KryptonDateTimePickerMonthCalendarDemo));
            this.grpMain = new Krypton.Toolkit.KryptonGroupBox();
            this.grpPickColor = new Krypton.Toolkit.KryptonGroupBox();
            this.dtpPickColor = new Krypton.Toolkit.KryptonDateTimePicker();
            this.btnPickColor = new Krypton.Toolkit.KryptonColorButton();
            this.btnUseThemeDefault = new Krypton.Toolkit.KryptonButton();
            this.btnApplyToAll = new Krypton.Toolkit.KryptonButton();
            this.grpPresets = new Krypton.Toolkit.KryptonGroupBox();
            this.dtpDarkGray = new Krypton.Toolkit.KryptonDateTimePicker();
            this.dtpSoftBlue = new Krypton.Toolkit.KryptonDateTimePicker();
            this.dtpLightYellow = new Krypton.Toolkit.KryptonDateTimePicker();
            this.dtpDarkSlate = new Krypton.Toolkit.KryptonDateTimePicker();
            this.grpThemeDefault = new Krypton.Toolkit.KryptonGroupBox();
            this.dtpThemeDefault = new Krypton.Toolkit.KryptonDateTimePicker();
            this.pnlControls = new Krypton.Toolkit.KryptonPanel();
            this.lblStatus = new Krypton.Toolkit.KryptonLabel();
            this.lblDescription = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.grpMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpMain.Panel)).BeginInit();
            this.grpMain.Panel.SuspendLayout();
            this.grpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpPickColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpPickColor.Panel)).BeginInit();
            this.grpPickColor.Panel.SuspendLayout();
            this.grpPickColor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpPresets)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpPresets.Panel)).BeginInit();
            this.grpPresets.Panel.SuspendLayout();
            this.grpPresets.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpThemeDefault)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpThemeDefault.Panel)).BeginInit();
            this.grpThemeDefault.Panel.SuspendLayout();
            this.grpThemeDefault.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlControls)).BeginInit();
            this.pnlControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpMain
            // 
            this.grpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpMain.Location = new System.Drawing.Point(0, 130);
            // 
            // grpMain.Panel
            // 
            this.grpMain.Panel.AutoScroll = true;
            this.grpMain.Panel.Controls.Add(this.grpPickColor);
            this.grpMain.Panel.Controls.Add(this.grpPresets);
            this.grpMain.Panel.Controls.Add(this.grpThemeDefault);
            this.grpMain.Size = new System.Drawing.Size(884, 420);
            this.grpMain.TabIndex = 1;
            this.grpMain.Values.Heading = "KryptonDateTimePicker – Month calendar background (Issue #1827)";
            // 
            // grpPickColor
            // 
            this.grpPickColor.Location = new System.Drawing.Point(15, 240);
            // 
            // grpPickColor.Panel
            // 
            this.grpPickColor.Panel.Controls.Add(this.dtpPickColor);
            this.grpPickColor.Panel.Controls.Add(this.btnPickColor);
            this.grpPickColor.Panel.Controls.Add(this.btnUseThemeDefault);
            this.grpPickColor.Panel.Controls.Add(this.btnApplyToAll);
            this.grpPickColor.Size = new System.Drawing.Size(550, 136);
            this.grpPickColor.TabIndex = 2;
            this.grpPickColor.Values.Heading = "Pick a color – Use the color button to set the calendar background for the date p" +
    "icker";
            // 
            // dtpPickColor
            // 
            this.dtpPickColor.Location = new System.Drawing.Point(15, 35);
            this.dtpPickColor.Name = "dtpPickColor";
            this.dtpPickColor.Size = new System.Drawing.Size(250, 21);
            this.dtpPickColor.TabIndex = 0;
            // 
            // btnPickColor
            // 
            this.btnPickColor.CustomColorPreviewShape = Krypton.Toolkit.KryptonColorButtonCustomColorPreviewShape.Circle;
            this.btnPickColor.Location = new System.Drawing.Point(280, 32);
            this.btnPickColor.Name = "btnPickColor";
            this.btnPickColor.Size = new System.Drawing.Size(120, 30);
            this.btnPickColor.TabIndex = 1;
            this.btnPickColor.Values.Image = ((System.Drawing.Image)(resources.GetObject("btnPickColor.Values.Image")));
            this.btnPickColor.Values.RoundedCorners = 8;
            this.btnPickColor.Values.Text = "Calendar color";
            // 
            // btnUseThemeDefault
            // 
            this.btnUseThemeDefault.Location = new System.Drawing.Point(415, 32);
            this.btnUseThemeDefault.Name = "btnUseThemeDefault";
            this.btnUseThemeDefault.Size = new System.Drawing.Size(120, 30);
            this.btnUseThemeDefault.TabIndex = 2;
            this.btnUseThemeDefault.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnUseThemeDefault.Values.Text = "Use theme default";
            // 
            // btnApplyToAll
            // 
            this.btnApplyToAll.Location = new System.Drawing.Point(15, 70);
            this.btnApplyToAll.Name = "btnApplyToAll";
            this.btnApplyToAll.Size = new System.Drawing.Size(180, 28);
            this.btnApplyToAll.TabIndex = 3;
            this.btnApplyToAll.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnApplyToAll.Values.Text = "Apply current color to all";
            // 
            // grpPresets
            // 
            this.grpPresets.Location = new System.Drawing.Point(15, 105);
            // 
            // grpPresets.Panel
            // 
            this.grpPresets.Panel.Controls.Add(this.dtpDarkGray);
            this.grpPresets.Panel.Controls.Add(this.dtpSoftBlue);
            this.grpPresets.Panel.Controls.Add(this.dtpLightYellow);
            this.grpPresets.Panel.Controls.Add(this.dtpDarkSlate);
            this.grpPresets.Size = new System.Drawing.Size(850, 120);
            this.grpPresets.TabIndex = 1;
            this.grpPresets.Values.Heading = "Preset calendar backgrounds – Open drop-down to see the calendar body color";
            // 
            // dtpDarkGray
            // 
            this.dtpDarkGray.Location = new System.Drawing.Point(15, 35);
            this.dtpDarkGray.Name = "dtpDarkGray";
            this.dtpDarkGray.Size = new System.Drawing.Size(200, 21);
            this.dtpDarkGray.TabIndex = 0;
            // 
            // dtpSoftBlue
            // 
            this.dtpSoftBlue.Location = new System.Drawing.Point(225, 35);
            this.dtpSoftBlue.Name = "dtpSoftBlue";
            this.dtpSoftBlue.Size = new System.Drawing.Size(200, 21);
            this.dtpSoftBlue.TabIndex = 1;
            // 
            // dtpLightYellow
            // 
            this.dtpLightYellow.Location = new System.Drawing.Point(435, 35);
            this.dtpLightYellow.Name = "dtpLightYellow";
            this.dtpLightYellow.Size = new System.Drawing.Size(200, 21);
            this.dtpLightYellow.TabIndex = 2;
            // 
            // dtpDarkSlate
            // 
            this.dtpDarkSlate.Location = new System.Drawing.Point(645, 35);
            this.dtpDarkSlate.Name = "dtpDarkSlate";
            this.dtpDarkSlate.Size = new System.Drawing.Size(200, 21);
            this.dtpDarkSlate.TabIndex = 3;
            // 
            // grpThemeDefault
            // 
            this.grpThemeDefault.Location = new System.Drawing.Point(15, 15);
            // 
            // grpThemeDefault.Panel
            // 
            this.grpThemeDefault.Panel.Controls.Add(this.dtpThemeDefault);
            this.grpThemeDefault.Size = new System.Drawing.Size(400, 75);
            this.grpThemeDefault.TabIndex = 0;
            this.grpThemeDefault.Values.Heading = "Theme default – CalendarBackColor = Empty (uses palette/theme)";
            // 
            // dtpThemeDefault
            // 
            this.dtpThemeDefault.Location = new System.Drawing.Point(15, 30);
            this.dtpThemeDefault.Name = "dtpThemeDefault";
            this.dtpThemeDefault.Size = new System.Drawing.Size(350, 21);
            this.dtpThemeDefault.TabIndex = 0;
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.lblStatus);
            this.pnlControls.Controls.Add(this.lblDescription);
            this.pnlControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlControls.Location = new System.Drawing.Point(0, 0);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(884, 130);
            this.pnlControls.TabIndex = 0;
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(15, 65);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(410, 25);
            this.lblStatus.StateCommon.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Values.Text = "Open any date picker drop-down to see the month calendar background.";
            // 
            // lblDescription
            // 
            this.lblDescription.Location = new System.Drawing.Point(15, 15);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(1437, 25);
            this.lblDescription.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDescription.StateCommon.ShortText.MultiLine = Krypton.Toolkit.InheritBool.True;
            this.lblDescription.StateCommon.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.lblDescription.TabIndex = 0;
            this.lblDescription.Values.Text = resources.GetString("lblDescription.Values.Text");
            // 
            // KryptonDateTimePickerMonthCalendarDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 550);
            this.Controls.Add(this.grpMain);
            this.Controls.Add(this.pnlControls);
            this.Name = "KryptonDateTimePickerMonthCalendarDemo";
            this.Text = "KryptonDateTimePicker Month Calendar Background Demo (Issue #1827)";
            ((System.ComponentModel.ISupportInitialize)(this.grpMain.Panel)).EndInit();
            this.grpMain.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpMain)).EndInit();
            this.grpMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpPickColor.Panel)).EndInit();
            this.grpPickColor.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpPickColor)).EndInit();
            this.grpPickColor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpPresets.Panel)).EndInit();
            this.grpPresets.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpPresets)).EndInit();
            this.grpPresets.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpThemeDefault.Panel)).EndInit();
            this.grpThemeDefault.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpThemeDefault)).EndInit();
            this.grpThemeDefault.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlControls)).EndInit();
            this.pnlControls.ResumeLayout(false);
            this.pnlControls.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonGroupBox grpMain;
        private Krypton.Toolkit.KryptonGroupBox grpThemeDefault;
        private Krypton.Toolkit.KryptonDateTimePicker dtpThemeDefault;
        private Krypton.Toolkit.KryptonGroupBox grpPresets;
        private Krypton.Toolkit.KryptonDateTimePicker dtpDarkGray;
        private Krypton.Toolkit.KryptonDateTimePicker dtpSoftBlue;
        private Krypton.Toolkit.KryptonDateTimePicker dtpLightYellow;
        private Krypton.Toolkit.KryptonDateTimePicker dtpDarkSlate;
        private Krypton.Toolkit.KryptonGroupBox grpPickColor;
        private Krypton.Toolkit.KryptonDateTimePicker dtpPickColor;
        private Krypton.Toolkit.KryptonColorButton btnPickColor;
        private Krypton.Toolkit.KryptonButton btnUseThemeDefault;
        private Krypton.Toolkit.KryptonButton btnApplyToAll;
        private Krypton.Toolkit.KryptonPanel pnlControls;
        private Krypton.Toolkit.KryptonLabel lblDescription;
        private Krypton.Toolkit.KryptonLabel lblStatus;
    }
}
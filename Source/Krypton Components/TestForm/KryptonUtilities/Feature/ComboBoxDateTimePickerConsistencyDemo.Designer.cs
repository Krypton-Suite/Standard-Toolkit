namespace TestForm
{
    partial class ComboBoxDateTimePickerConsistencyDemo
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
            this.grpMain = new Krypton.Toolkit.KryptonGroupBox();
            this.grpContentVariations = new Krypton.Toolkit.KryptonGroupBox();
            this.dtpDateOnly = new Krypton.Toolkit.KryptonDateTimePicker();
            this.cmbNumbers = new Krypton.Toolkit.KryptonComboBox();
            this.dtpCustomFormat = new Krypton.Toolkit.KryptonDateTimePicker();
            this.cmbLongText = new Krypton.Toolkit.KryptonComboBox();
            this.grpStyleVariations = new Krypton.Toolkit.KryptonGroupBox();
            this.dtpStandalone = new Krypton.Toolkit.KryptonDateTimePicker();
            this.cmbStandalone = new Krypton.Toolkit.KryptonComboBox();
            this.dtpInputControl = new Krypton.Toolkit.KryptonDateTimePicker();
            this.cmbInputControl = new Krypton.Toolkit.KryptonComboBox();
            this.grpTallControls = new Krypton.Toolkit.KryptonGroupBox();
            this.dtpTall = new Krypton.Toolkit.KryptonDateTimePicker();
            this.cmbTall = new Krypton.Toolkit.KryptonComboBox();
            this.grpStandardHeight = new Krypton.Toolkit.KryptonGroupBox();
            this.dtpStandard = new Krypton.Toolkit.KryptonDateTimePicker();
            this.cmbStandard = new Krypton.Toolkit.KryptonComboBox();
            this.pnlControls = new Krypton.Toolkit.KryptonPanel();
            this.btnRefresh = new Krypton.Toolkit.KryptonButton();
            this.btnToggleStyle = new Krypton.Toolkit.KryptonButton();
            this.btnResetHeight = new Krypton.Toolkit.KryptonButton();
            this.btnDecreaseHeight = new Krypton.Toolkit.KryptonButton();
            this.btnIncreaseHeight = new Krypton.Toolkit.KryptonButton();
            this.lblStatus = new Krypton.Toolkit.KryptonLabel();
            this.lblDescription = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.grpMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpMain.Panel)).BeginInit();
            this.grpMain.Panel.SuspendLayout();
            this.grpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpContentVariations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpContentVariations.Panel)).BeginInit();
            this.grpContentVariations.Panel.SuspendLayout();
            this.grpContentVariations.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpStyleVariations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpStyleVariations.Panel)).BeginInit();
            this.grpStyleVariations.Panel.SuspendLayout();
            this.grpStyleVariations.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpTallControls)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpTallControls.Panel)).BeginInit();
            this.grpTallControls.Panel.SuspendLayout();
            this.grpTallControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpStandardHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpStandardHeight.Panel)).BeginInit();
            this.grpStandardHeight.Panel.SuspendLayout();
            this.grpStandardHeight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlControls)).BeginInit();
            this.pnlControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpMain
            // 
            this.grpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpMain.Location = new System.Drawing.Point(0, 120);
            this.grpMain.Name = "grpMain";
            this.grpMain.Size = new System.Drawing.Size(1200, 630);
            this.grpMain.Panel.AutoScroll = true;
            this.grpMain.TabIndex = 1;
            this.grpMain.Values.Heading = "ComboBox and DateTimePicker Consistency Demo (Issue #1651)";
            // 
            // grpMain.Panel
            // 
            this.grpMain.Panel.Controls.Add(this.grpContentVariations);
            this.grpMain.Panel.Controls.Add(this.grpStyleVariations);
            this.grpMain.Panel.Controls.Add(this.grpTallControls);
            this.grpMain.Panel.Controls.Add(this.grpStandardHeight);
            // 
            // grpStandardHeight
            // 
            this.grpStandardHeight.Location = new System.Drawing.Point(15, 15);
            this.grpStandardHeight.Name = "grpStandardHeight";
            this.grpStandardHeight.Size = new System.Drawing.Size(570, 100);
            this.grpStandardHeight.TabIndex = 0;
            this.grpStandardHeight.Values.Heading = "Standard Height Controls (25px) - Drop-down buttons should stretch to full height";
            // 
            // grpStandardHeight.Panel
            // 
            this.grpStandardHeight.Panel.Controls.Add(this.dtpStandard);
            this.grpStandardHeight.Panel.Controls.Add(this.cmbStandard);
            // 
            // cmbStandard
            // 
            this.cmbStandard.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cmbStandard.DropDownWidth = 250;
            this.cmbStandard.Location = new System.Drawing.Point(15, 30);
            this.cmbStandard.Name = "cmbStandard";
            this.cmbStandard.Size = new System.Drawing.Size(250, 25);
            this.cmbStandard.TabIndex = 0;
            // 
            // dtpStandard
            // 
            this.dtpStandard.Location = new System.Drawing.Point(285, 30);
            this.dtpStandard.Name = "dtpStandard";
            this.dtpStandard.Size = new System.Drawing.Size(250, 25);
            this.dtpStandard.TabIndex = 1;
            // 
            // grpTallControls
            // 
            this.grpTallControls.Location = new System.Drawing.Point(600, 15);
            this.grpTallControls.Name = "grpTallControls";
            this.grpTallControls.Size = new System.Drawing.Size(570, 100);
            this.grpTallControls.TabIndex = 1;
            this.grpTallControls.Values.Heading = "Tall Controls (60px) - Drop-down buttons should stretch to full height";
            // 
            // grpTallControls.Panel
            // 
            this.grpTallControls.Panel.Controls.Add(this.dtpTall);
            this.grpTallControls.Panel.Controls.Add(this.cmbTall);
            // 
            // cmbTall
            // 
            this.cmbTall.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cmbTall.DropDownWidth = 250;
            this.cmbTall.Location = new System.Drawing.Point(15, 30);
            this.cmbTall.Name = "cmbTall";
            this.cmbTall.Size = new System.Drawing.Size(250, 60);
            this.cmbTall.TabIndex = 0;
            // 
            // dtpTall
            // 
            this.dtpTall.Location = new System.Drawing.Point(285, 30);
            this.dtpTall.Name = "dtpTall";
            this.dtpTall.Size = new System.Drawing.Size(250, 60);
            this.dtpTall.TabIndex = 1;
            // 
            // grpStyleVariations
            // 
            this.grpStyleVariations.Location = new System.Drawing.Point(15, 130);
            this.grpStyleVariations.Name = "grpStyleVariations";
            this.grpStyleVariations.Size = new System.Drawing.Size(570, 150);
            this.grpStyleVariations.TabIndex = 2;
            this.grpStyleVariations.Values.Heading = "Style Variations - Both should maintain consistent drop-down button height";
            // 
            // grpStyleVariations.Panel
            // 
            this.grpStyleVariations.Panel.Controls.Add(this.dtpStandalone);
            this.grpStyleVariations.Panel.Controls.Add(this.cmbStandalone);
            this.grpStyleVariations.Panel.Controls.Add(this.dtpInputControl);
            this.grpStyleVariations.Panel.Controls.Add(this.cmbInputControl);
            // 
            // cmbInputControl
            // 
            this.cmbInputControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cmbInputControl.DropDownWidth = 250;
            this.cmbInputControl.Location = new System.Drawing.Point(15, 30);
            this.cmbInputControl.Name = "cmbInputControl";
            this.cmbInputControl.Size = new System.Drawing.Size(250, 25);
            this.cmbInputControl.TabIndex = 0;
            // 
            // dtpInputControl
            // 
            this.dtpInputControl.Location = new System.Drawing.Point(285, 30);
            this.dtpInputControl.Name = "dtpInputControl";
            this.dtpInputControl.Size = new System.Drawing.Size(250, 25);
            this.dtpInputControl.TabIndex = 1;
            // 
            // cmbStandalone
            // 
            this.cmbStandalone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cmbStandalone.DropDownWidth = 250;
            this.cmbStandalone.Location = new System.Drawing.Point(15, 70);
            this.cmbStandalone.Name = "cmbStandalone";
            this.cmbStandalone.Size = new System.Drawing.Size(250, 25);
            this.cmbStandalone.TabIndex = 2;
            // 
            // dtpStandalone
            // 
            this.dtpStandalone.Location = new System.Drawing.Point(285, 70);
            this.dtpStandalone.Name = "dtpStandalone";
            this.dtpStandalone.Size = new System.Drawing.Size(250, 25);
            this.dtpStandalone.TabIndex = 3;
            // 
            // grpContentVariations
            // 
            this.grpContentVariations.Location = new System.Drawing.Point(600, 130);
            this.grpContentVariations.Name = "grpContentVariations";
            this.grpContentVariations.Size = new System.Drawing.Size(570, 150);
            this.grpContentVariations.TabIndex = 3;
            this.grpContentVariations.Values.Heading = "Content Variations - Text should be centered vertically";
            // 
            // grpContentVariations.Panel
            // 
            this.grpContentVariations.Panel.Controls.Add(this.dtpDateOnly);
            this.grpContentVariations.Panel.Controls.Add(this.cmbNumbers);
            this.grpContentVariations.Panel.Controls.Add(this.dtpCustomFormat);
            this.grpContentVariations.Panel.Controls.Add(this.cmbLongText);
            // 
            // cmbLongText
            // 
            this.cmbLongText.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cmbLongText.DropDownWidth = 250;
            this.cmbLongText.Location = new System.Drawing.Point(15, 30);
            this.cmbLongText.Name = "cmbLongText";
            this.cmbLongText.Size = new System.Drawing.Size(250, 25);
            this.cmbLongText.TabIndex = 0;
            // 
            // dtpCustomFormat
            // 
            this.dtpCustomFormat.Location = new System.Drawing.Point(285, 30);
            this.dtpCustomFormat.Name = "dtpCustomFormat";
            this.dtpCustomFormat.Size = new System.Drawing.Size(250, 25);
            this.dtpCustomFormat.TabIndex = 1;
            // 
            // cmbNumbers
            // 
            this.cmbNumbers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cmbNumbers.DropDownWidth = 250;
            this.cmbNumbers.Location = new System.Drawing.Point(15, 70);
            this.cmbNumbers.Name = "cmbNumbers";
            this.cmbNumbers.Size = new System.Drawing.Size(250, 25);
            this.cmbNumbers.TabIndex = 2;
            // 
            // dtpDateOnly
            // 
            this.dtpDateOnly.Location = new System.Drawing.Point(285, 70);
            this.dtpDateOnly.Name = "dtpDateOnly";
            this.dtpDateOnly.Size = new System.Drawing.Size(250, 25);
            this.dtpDateOnly.TabIndex = 3;
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.btnRefresh);
            this.pnlControls.Controls.Add(this.btnToggleStyle);
            this.pnlControls.Controls.Add(this.btnResetHeight);
            this.pnlControls.Controls.Add(this.btnDecreaseHeight);
            this.pnlControls.Controls.Add(this.btnIncreaseHeight);
            this.pnlControls.Controls.Add(this.lblStatus);
            this.pnlControls.Controls.Add(this.lblDescription);
            this.pnlControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlControls.Location = new System.Drawing.Point(0, 0);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(1200, 120);
            this.pnlControls.TabIndex = 0;
            // 
            // lblDescription
            // 
            this.lblDescription.Location = new System.Drawing.Point(15, 15);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(1170, 30);
            this.lblDescription.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDescription.StateCommon.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.lblDescription.TabIndex = 0;
            this.lblDescription.Values.Text = "This demo verifies that KComboBox drop-down buttons stretch to full height like KDateTimePicker, and that text is properly centered (Issue #1651).";
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(15, 50);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(800, 20);
            this.lblStatus.StateCommon.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Values.Text = "Status: Ready";
            // 
            // btnIncreaseHeight
            // 
            this.btnIncreaseHeight.Location = new System.Drawing.Point(15, 75);
            this.btnIncreaseHeight.Name = "btnIncreaseHeight";
            this.btnIncreaseHeight.Size = new System.Drawing.Size(140, 30);
            this.btnIncreaseHeight.TabIndex = 2;
            this.btnIncreaseHeight.Values.Text = "Increase Height";
            // 
            // btnDecreaseHeight
            // 
            this.btnDecreaseHeight.Location = new System.Drawing.Point(165, 75);
            this.btnDecreaseHeight.Name = "btnDecreaseHeight";
            this.btnDecreaseHeight.Size = new System.Drawing.Size(140, 30);
            this.btnDecreaseHeight.TabIndex = 3;
            this.btnDecreaseHeight.Values.Text = "Decrease Height";
            // 
            // btnResetHeight
            // 
            this.btnResetHeight.Location = new System.Drawing.Point(315, 75);
            this.btnResetHeight.Name = "btnResetHeight";
            this.btnResetHeight.Size = new System.Drawing.Size(140, 30);
            this.btnResetHeight.TabIndex = 4;
            this.btnResetHeight.Values.Text = "Reset Height";
            // 
            // btnToggleStyle
            // 
            this.btnToggleStyle.Location = new System.Drawing.Point(465, 75);
            this.btnToggleStyle.Name = "btnToggleStyle";
            this.btnToggleStyle.Size = new System.Drawing.Size(140, 30);
            this.btnToggleStyle.TabIndex = 5;
            this.btnToggleStyle.Values.Text = "Toggle Style";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(615, 75);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(140, 30);
            this.btnRefresh.TabIndex = 6;
            this.btnRefresh.Values.Text = "Refresh";
            // 
            // ComboBoxDateTimePickerConsistencyDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 750);
            this.Controls.Add(this.grpMain);
            this.Controls.Add(this.pnlControls);
            this.Name = "ComboBoxDateTimePickerConsistencyDemo";
            this.Text = "ComboBox and DateTimePicker Consistency Demo (Issue #1651)";
            ((System.ComponentModel.ISupportInitialize)(this.grpMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpMain.Panel)).EndInit();
            this.grpMain.Panel.ResumeLayout(false);
            this.grpMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpContentVariations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpContentVariations.Panel)).EndInit();
            this.grpContentVariations.Panel.ResumeLayout(false);
            this.grpContentVariations.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpStyleVariations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpStyleVariations.Panel)).EndInit();
            this.grpStyleVariations.Panel.ResumeLayout(false);
            this.grpStyleVariations.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpTallControls)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpTallControls.Panel)).EndInit();
            this.grpTallControls.Panel.ResumeLayout(false);
            this.grpTallControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpStandardHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpStandardHeight.Panel)).EndInit();
            this.grpStandardHeight.Panel.ResumeLayout(false);
            this.grpStandardHeight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlControls)).EndInit();
            this.pnlControls.ResumeLayout(false);
            this.pnlControls.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonGroupBox grpMain;
        private Krypton.Toolkit.KryptonGroupBox grpStandardHeight;
        private Krypton.Toolkit.KryptonComboBox cmbStandard;
        private Krypton.Toolkit.KryptonDateTimePicker dtpStandard;
        private Krypton.Toolkit.KryptonGroupBox grpTallControls;
        private Krypton.Toolkit.KryptonComboBox cmbTall;
        private Krypton.Toolkit.KryptonDateTimePicker dtpTall;
        private Krypton.Toolkit.KryptonGroupBox grpStyleVariations;
        private Krypton.Toolkit.KryptonComboBox cmbInputControl;
        private Krypton.Toolkit.KryptonDateTimePicker dtpInputControl;
        private Krypton.Toolkit.KryptonComboBox cmbStandalone;
        private Krypton.Toolkit.KryptonDateTimePicker dtpStandalone;
        private Krypton.Toolkit.KryptonGroupBox grpContentVariations;
        private Krypton.Toolkit.KryptonComboBox cmbLongText;
        private Krypton.Toolkit.KryptonDateTimePicker dtpCustomFormat;
        private Krypton.Toolkit.KryptonComboBox cmbNumbers;
        private Krypton.Toolkit.KryptonDateTimePicker dtpDateOnly;
        private Krypton.Toolkit.KryptonPanel pnlControls;
        private Krypton.Toolkit.KryptonLabel lblDescription;
        private Krypton.Toolkit.KryptonLabel lblStatus;
        private Krypton.Toolkit.KryptonButton btnIncreaseHeight;
        private Krypton.Toolkit.KryptonButton btnDecreaseHeight;
        private Krypton.Toolkit.KryptonButton btnResetHeight;
        private Krypton.Toolkit.KryptonButton btnToggleStyle;
        private Krypton.Toolkit.KryptonButton btnRefresh;
    }
}
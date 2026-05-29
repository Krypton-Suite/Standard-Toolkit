#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2024 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm
{
    partial class ButtonTextTrackingExample
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
            this.components = new System.ComponentModel.Container();
            this.kryptonManager1 = new Krypton.Toolkit.KryptonManager(this.components);
            //
            // kryptonManager1
            //
            this.kryptonManager1.GlobalPaletteMode = Krypton.Toolkit.PaletteMode.Microsoft365BlackDarkMode;
            //
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.klblStatus = new Krypton.Toolkit.KryptonLabel();
            this.kbtnReset = new Krypton.Toolkit.KryptonButton();
            this.kbtnApplyColor = new Krypton.Toolkit.KryptonButton();
            this.kcbtnTrackingColor = new Krypton.Toolkit.KryptonColorButton();
            this.klblCustomColor = new Krypton.Toolkit.KryptonLabel();
            this.kryptonSeparator1 = new Krypton.Toolkit.KryptonSeparator();
            this.kchkChecked = new Krypton.Toolkit.KryptonCheckButton();
            this.kbtnColor = new Krypton.Toolkit.KryptonColorButton();
            this.kbtnStandalone = new Krypton.Toolkit.KryptonButton();
            this.klblControls = new Krypton.Toolkit.KryptonLabel();
            this.klblDescription = new Krypton.Toolkit.KryptonLabel();
            this.klblTheme = new Krypton.Toolkit.KryptonLabel();
            this.kryptonThemeComboBox1 = new Krypton.Toolkit.KryptonThemeComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).BeginInit();
            this.SuspendLayout();
            //
            // kryptonPanel1
            //
            this.kryptonPanel1.Controls.Add(this.klblStatus);
            this.kryptonPanel1.Controls.Add(this.kbtnReset);
            this.kryptonPanel1.Controls.Add(this.kbtnApplyColor);
            this.kryptonPanel1.Controls.Add(this.kcbtnTrackingColor);
            this.kryptonPanel1.Controls.Add(this.klblCustomColor);
            this.kryptonPanel1.Controls.Add(this.kryptonSeparator1);
            this.kryptonPanel1.Controls.Add(this.kchkChecked);
            this.kryptonPanel1.Controls.Add(this.kbtnColor);
            this.kryptonPanel1.Controls.Add(this.kbtnStandalone);
            this.kryptonPanel1.Controls.Add(this.klblControls);
            this.kryptonPanel1.Controls.Add(this.klblDescription);
            this.kryptonPanel1.Controls.Add(this.klblTheme);
            this.kryptonPanel1.Controls.Add(this.kryptonThemeComboBox1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Padding = new System.Windows.Forms.Padding(12);
            this.kryptonPanel1.Size = new System.Drawing.Size(584, 361);
            this.kryptonPanel1.TabIndex = 0;
            //
            // klblTheme
            //
            this.klblTheme.Location = new System.Drawing.Point(15, 15);
            this.klblTheme.Name = "klblTheme";
            this.klblTheme.Size = new System.Drawing.Size(45, 20);
            this.klblTheme.TabIndex = 0;
            this.klblTheme.Values.Text = "Theme:";
            //
            // kryptonThemeComboBox1
            //
            this.kryptonThemeComboBox1.DropDownWidth = 250;
            this.kryptonThemeComboBox1.IntegralHeight = false;
            this.kryptonThemeComboBox1.Location = new System.Drawing.Point(66, 13);
            this.kryptonThemeComboBox1.Name = "kryptonThemeComboBox1";
            this.kryptonThemeComboBox1.Size = new System.Drawing.Size(250, 22);
            this.kryptonThemeComboBox1.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kryptonThemeComboBox1.TabIndex = 1;
            this.kryptonThemeComboBox1.SelectedIndexChanged += new System.EventHandler(this.kryptonThemeComboBox1_SelectedIndexChanged);
            //
            // klblDescription
            //
            this.klblDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.klblDescription.Location = new System.Drawing.Point(15, 45);
            this.klblDescription.Name = "klblDescription";
            this.klblDescription.Size = new System.Drawing.Size(554, 40);
            this.klblDescription.StateCommon.ShortText.MultiLine = Krypton.Toolkit.InheritBool.True;
            this.klblDescription.StateCommon.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.klblDescription.TabIndex = 2;
            this.klblDescription.Values.Text = "Hover over the buttons to see the tracking (hover) text color.";
            //
            // klblControls
            //
            this.klblControls.LabelStyle = Krypton.Toolkit.LabelStyle.BoldPanel;
            this.klblControls.Location = new System.Drawing.Point(15, 95);
            this.klblControls.Name = "klblControls";
            this.klblControls.Size = new System.Drawing.Size(180, 20);
            this.klblControls.TabIndex = 3;
            this.klblControls.Values.Text = "Controls (hover to see tracking):";
            //
            // kbtnStandalone
            //
            this.kbtnStandalone.Location = new System.Drawing.Point(15, 125);
            this.kbtnStandalone.Name = "kbtnStandalone";
            this.kbtnStandalone.Size = new System.Drawing.Size(120, 28);
            this.kbtnStandalone.TabIndex = 4;
            this.kbtnStandalone.Values.Text = "KryptonButton";
            //
            // kbtnColor
            //
            this.kbtnColor.Location = new System.Drawing.Point(145, 125);
            this.kbtnColor.Name = "kbtnColor";
            this.kbtnColor.Size = new System.Drawing.Size(120, 28);
            this.kbtnColor.TabIndex = 5;
            this.kbtnColor.Values.Text = "Color Button";
            //
            // kchkChecked
            //
            this.kchkChecked.Location = new System.Drawing.Point(275, 125);
            this.kchkChecked.Name = "kchkChecked";
            this.kchkChecked.Size = new System.Drawing.Size(120, 28);
            this.kchkChecked.TabIndex = 6;
            this.kchkChecked.Values.Text = "Check Button";
            //
            // kryptonSeparator1
            //
            this.kryptonSeparator1.Location = new System.Drawing.Point(15, 168);
            this.kryptonSeparator1.Name = "kryptonSeparator1";
            this.kryptonSeparator1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.kryptonSeparator1.SeparatorStyle = Krypton.Toolkit.SeparatorStyle.LowProfile;
            this.kryptonSeparator1.Size = new System.Drawing.Size(554, 10);
            this.kryptonSeparator1.TabIndex = 7;
            //
            // klblCustomColor
            //
            this.klblCustomColor.LabelStyle = Krypton.Toolkit.LabelStyle.BoldPanel;
            this.klblCustomColor.Location = new System.Drawing.Point(15, 188);
            this.klblCustomColor.Name = "klblCustomColor";
            this.klblCustomColor.Size = new System.Drawing.Size(280, 20);
            this.klblCustomColor.TabIndex = 8;
            this.klblCustomColor.Values.Text = "Custom tracking color (SchemeBaseColors.ButtonTextTracking):";
            //
            // kcbtnTrackingColor
            //
            this.kcbtnTrackingColor.Location = new System.Drawing.Point(15, 215);
            this.kcbtnTrackingColor.Name = "kcbtnTrackingColor";
            this.kcbtnTrackingColor.SelectedColor = System.Drawing.Color.Empty;
            this.kcbtnTrackingColor.Size = new System.Drawing.Size(120, 28);
            this.kcbtnTrackingColor.TabIndex = 9;
            this.kcbtnTrackingColor.Values.Text = "Pick Color";
            this.kcbtnTrackingColor.SelectedColorChanged += new System.EventHandler<Krypton.Toolkit.ColorEventArgs>(this.kcbtnTrackingColor_SelectedColorChanged);
            //
            // kbtnApplyColor
            //
            this.kbtnApplyColor.Location = new System.Drawing.Point(145, 215);
            this.kbtnApplyColor.Name = "kbtnApplyColor";
            this.kbtnApplyColor.Size = new System.Drawing.Size(90, 28);
            this.kbtnApplyColor.TabIndex = 10;
            this.kbtnApplyColor.Values.Text = "Apply";
            this.kbtnApplyColor.Click += new System.EventHandler(this.kbtnApplyColor_Click);
            //
            // kbtnReset
            //
            this.kbtnReset.Location = new System.Drawing.Point(245, 215);
            this.kbtnReset.Name = "kbtnReset";
            this.kbtnReset.Size = new System.Drawing.Size(90, 28);
            this.kbtnReset.TabIndex = 11;
            this.kbtnReset.Values.Text = "Reset";
            this.kbtnReset.Click += new System.EventHandler(this.kbtnReset_Click);
            //
            // klblStatus
            //
            this.klblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.klblStatus.Location = new System.Drawing.Point(15, 255);
            this.klblStatus.Name = "klblStatus";
            this.klblStatus.Size = new System.Drawing.Size(554, 20);
            this.klblStatus.StateCommon.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.klblStatus.TabIndex = 12;
            this.klblStatus.Values.Text = "Use the color picker to set a custom tracking text color, or Reset to use theme default.";
            //
            // ButtonTextTrackingExample
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "ButtonTextTrackingExample";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Button Text Tracking Example (Issue #1326)";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.KryptonManager kryptonManager1;
        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonThemeComboBox kryptonThemeComboBox1;
        private Krypton.Toolkit.KryptonLabel klblTheme;
        private Krypton.Toolkit.KryptonLabel klblDescription;
        private Krypton.Toolkit.KryptonLabel klblControls;
        private Krypton.Toolkit.KryptonButton kbtnStandalone;
        private Krypton.Toolkit.KryptonColorButton kbtnColor;
        private Krypton.Toolkit.KryptonCheckButton kchkChecked;
        private Krypton.Toolkit.KryptonSeparator kryptonSeparator1;
        private Krypton.Toolkit.KryptonLabel klblCustomColor;
        private Krypton.Toolkit.KryptonColorButton kcbtnTrackingColor;
        private Krypton.Toolkit.KryptonButton kbtnApplyColor;
        private Krypton.Toolkit.KryptonButton kbtnReset;
        private Krypton.Toolkit.KryptonLabel klblStatus;
    }
}

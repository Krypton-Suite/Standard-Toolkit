#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp) & Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm
{
    partial class Bug3025KryptonLabelAutoSizeDemo
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
            this.kryptonPanelMain = new Krypton.Toolkit.KryptonPanel();
            this.kryptonThemeComboBox1 = new Krypton.Toolkit.KryptonThemeComboBox();
            this.lblInstruction = new Krypton.Toolkit.KryptonWrapLabel();
            this.lblSectionAutoSize = new Krypton.Toolkit.KryptonLabel();
            this.kryptonLabelShort = new Krypton.Toolkit.KryptonLabel();
            this.kryptonLabelLong = new Krypton.Toolkit.KryptonLabel();
            this.kryptonLabelNumber = new Krypton.Toolkit.KryptonLabel();
            this.lblSectionNoAutoSize = new Krypton.Toolkit.KryptonLabel();
            this.kryptonLabelFixedSize = new Krypton.Toolkit.KryptonLabel();
            this.lblSectionStyles = new Krypton.Toolkit.KryptonLabel();
            this.kryptonLabelNormalPanel = new Krypton.Toolkit.KryptonLabel();
            this.kryptonLabelBoldPanel = new Krypton.Toolkit.KryptonLabel();
            this.kryptonLabelTitlePanel = new Krypton.Toolkit.KryptonLabel();
            this.kryptonLabelCaptionPanel = new Krypton.Toolkit.KryptonLabel();
            this.lblSectionWithImage = new Krypton.Toolkit.KryptonLabel();
            this.kryptonLabelWithImage = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelMain)).BeginInit();
            this.kryptonPanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).BeginInit();
            this.SuspendLayout();
            //
            // kryptonPanelMain
            //
            this.kryptonPanelMain.Controls.Add(this.kryptonLabelWithImage);
            this.kryptonPanelMain.Controls.Add(this.lblSectionWithImage);
            this.kryptonPanelMain.Controls.Add(this.kryptonLabelCaptionPanel);
            this.kryptonPanelMain.Controls.Add(this.kryptonLabelTitlePanel);
            this.kryptonPanelMain.Controls.Add(this.kryptonLabelBoldPanel);
            this.kryptonPanelMain.Controls.Add(this.kryptonLabelNormalPanel);
            this.kryptonPanelMain.Controls.Add(this.lblSectionStyles);
            this.kryptonPanelMain.Controls.Add(this.kryptonLabelFixedSize);
            this.kryptonPanelMain.Controls.Add(this.lblSectionNoAutoSize);
            this.kryptonPanelMain.Controls.Add(this.kryptonLabelNumber);
            this.kryptonPanelMain.Controls.Add(this.kryptonLabelLong);
            this.kryptonPanelMain.Controls.Add(this.kryptonLabelShort);
            this.kryptonPanelMain.Controls.Add(this.lblSectionAutoSize);
            this.kryptonPanelMain.Controls.Add(this.lblInstruction);
            this.kryptonPanelMain.Controls.Add(this.kryptonThemeComboBox1);
            this.kryptonPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanelMain.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanelMain.Name = "kryptonPanelMain";
            this.kryptonPanelMain.Padding = new System.Windows.Forms.Padding(12);
            this.kryptonPanelMain.Size = new System.Drawing.Size(584, 461);
            this.kryptonPanelMain.TabIndex = 0;
            //
            // kryptonThemeComboBox1
            //
            this.kryptonThemeComboBox1.DefaultPalette = Krypton.Toolkit.PaletteMode.Global;
            this.kryptonThemeComboBox1.DropDownWidth = 200;
            this.kryptonThemeComboBox1.IntegralHeight = false;
            this.kryptonThemeComboBox1.Location = new System.Drawing.Point(12, 12);
            this.kryptonThemeComboBox1.Name = "kryptonThemeComboBox1";
            this.kryptonThemeComboBox1.Size = new System.Drawing.Size(200, 22);
            this.kryptonThemeComboBox1.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kryptonThemeComboBox1.TabIndex = 0;
            //
            // lblInstruction
            //
            this.lblInstruction.AutoSize = true;
            this.lblInstruction.Location = new System.Drawing.Point(15, 44);
            this.lblInstruction.Name = "lblInstruction";
            this.lblInstruction.Size = new System.Drawing.Size(520, 38);
            this.lblInstruction.Text = "Issue #3025: KryptonLabel with AutoSize = true now resizes to fit its text when " +
    "placed in the Designer (click-drag). All labels below have AutoSize = true unless " +
    "marked otherwise. Change theme to verify sizing in different palettes.";
            //
            // lblSectionAutoSize
            //
            this.lblSectionAutoSize.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            this.lblSectionAutoSize.Location = new System.Drawing.Point(15, 95);
            this.lblSectionAutoSize.Name = "lblSectionAutoSize";
            this.lblSectionAutoSize.Size = new System.Drawing.Size(180, 27);
            this.lblSectionAutoSize.TabIndex = 2;
            this.lblSectionAutoSize.Values.Text = "AutoSize = true (default)";
            //
            // kryptonLabelShort
            //
            this.kryptonLabelShort.Location = new System.Drawing.Point(15, 128);
            this.kryptonLabelShort.Name = "kryptonLabelShort";
            this.kryptonLabelShort.Size = new System.Drawing.Size(45, 20);
            this.kryptonLabelShort.TabIndex = 3;
            this.kryptonLabelShort.Values.Text = "Short";
            //
            // kryptonLabelLong
            //
            this.kryptonLabelLong.Location = new System.Drawing.Point(15, 154);
            this.kryptonLabelLong.Name = "kryptonLabelLong";
            this.kryptonLabelLong.Size = new System.Drawing.Size(280, 20);
            this.kryptonLabelLong.TabIndex = 4;
            this.kryptonLabelLong.Values.Text = "This is a longer label that auto-sizes to fit this text.";
            //
            // kryptonLabelNumber
            //
            this.kryptonLabelNumber.Location = new System.Drawing.Point(15, 180);
            this.kryptonLabelNumber.Name = "kryptonLabelNumber";
            this.kryptonLabelNumber.Size = new System.Drawing.Size(27, 20);
            this.kryptonLabelNumber.TabIndex = 5;
            this.kryptonLabelNumber.Values.Text = "42";
            //
            // lblSectionNoAutoSize
            //
            this.lblSectionNoAutoSize.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            this.lblSectionNoAutoSize.Location = new System.Drawing.Point(15, 213);
            this.lblSectionNoAutoSize.Name = "lblSectionNoAutoSize";
            this.lblSectionNoAutoSize.Size = new System.Drawing.Size(195, 27);
            this.lblSectionNoAutoSize.TabIndex = 6;
            this.lblSectionNoAutoSize.Values.Text = "AutoSize = false (fixed size)";
            //
            // kryptonLabelFixedSize
            //
            this.kryptonLabelFixedSize.AutoSize = false;
            this.kryptonLabelFixedSize.Location = new System.Drawing.Point(15, 246);
            this.kryptonLabelFixedSize.Name = "kryptonLabelFixedSize";
            this.kryptonLabelFixedSize.Size = new System.Drawing.Size(200, 25);
            this.kryptonLabelFixedSize.TabIndex = 7;
            this.kryptonLabelFixedSize.Values.Text = "Fixed 200×25";
            //
            // lblSectionStyles
            //
            this.lblSectionStyles.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            this.lblSectionStyles.Location = new System.Drawing.Point(15, 284);
            this.lblSectionStyles.Name = "lblSectionStyles";
            this.lblSectionStyles.Size = new System.Drawing.Size(95, 27);
            this.lblSectionStyles.TabIndex = 8;
            this.lblSectionStyles.Values.Text = "Label styles";
            //
            // kryptonLabelNormalPanel
            //
            this.kryptonLabelNormalPanel.LabelStyle = Krypton.Toolkit.LabelStyle.NormalPanel;
            this.kryptonLabelNormalPanel.Location = new System.Drawing.Point(15, 317);
            this.kryptonLabelNormalPanel.Name = "kryptonLabelNormalPanel";
            this.kryptonLabelNormalPanel.Size = new System.Drawing.Size(82, 20);
            this.kryptonLabelNormalPanel.TabIndex = 9;
            this.kryptonLabelNormalPanel.Values.Text = "NormalPanel";
            //
            // kryptonLabelBoldPanel
            //
            this.kryptonLabelBoldPanel.LabelStyle = Krypton.Toolkit.LabelStyle.BoldPanel;
            this.kryptonLabelBoldPanel.Location = new System.Drawing.Point(15, 343);
            this.kryptonLabelBoldPanel.Name = "kryptonLabelBoldPanel";
            this.kryptonLabelBoldPanel.Size = new System.Drawing.Size(70, 20);
            this.kryptonLabelBoldPanel.TabIndex = 10;
            this.kryptonLabelBoldPanel.Values.Text = "BoldPanel";
            //
            // kryptonLabelTitlePanel
            //
            this.kryptonLabelTitlePanel.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            this.kryptonLabelTitlePanel.Location = new System.Drawing.Point(15, 369);
            this.kryptonLabelTitlePanel.Name = "kryptonLabelTitlePanel";
            this.kryptonLabelTitlePanel.Size = new System.Drawing.Size(72, 20);
            this.kryptonLabelTitlePanel.TabIndex = 11;
            this.kryptonLabelTitlePanel.Values.Text = "TitlePanel";
            //
            // kryptonLabelCaptionPanel
            //
            this.kryptonLabelCaptionPanel.LabelStyle = Krypton.Toolkit.LabelStyle.GroupBoxCaption;
            this.kryptonLabelCaptionPanel.Location = new System.Drawing.Point(15, 395);
            this.kryptonLabelCaptionPanel.Name = "kryptonLabelCaptionPanel";
            this.kryptonLabelCaptionPanel.Size = new System.Drawing.Size(85, 20);
            this.kryptonLabelCaptionPanel.TabIndex = 12;
            this.kryptonLabelCaptionPanel.Values.Text = "CaptionPanel";
            //
            // lblSectionWithImage
            //
            this.lblSectionWithImage.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            this.lblSectionWithImage.Location = new System.Drawing.Point(320, 95);
            this.lblSectionWithImage.Name = "lblSectionWithImage";
            this.lblSectionWithImage.Size = new System.Drawing.Size(115, 27);
            this.lblSectionWithImage.TabIndex = 13;
            this.lblSectionWithImage.Values.Text = "Text + image";
            //
            // kryptonLabelWithImage
            //
            this.kryptonLabelWithImage.Location = new System.Drawing.Point(320, 128);
            this.kryptonLabelWithImage.Name = "kryptonLabelWithImage";
            this.kryptonLabelWithImage.Size = new System.Drawing.Size(120, 20);
            this.kryptonLabelWithImage.TabIndex = 14;
            this.kryptonLabelWithImage.Values.Text = "Label with image";
            //
            // Bug3025KryptonLabelAutoSizeDemo
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 461);
            this.Controls.Add(this.kryptonPanelMain);
            this.Name = "Bug3025KryptonLabelAutoSizeDemo";
            this.Text = "Issue #3025: KryptonLabel AutoSize Demo";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelMain)).EndInit();
            this.kryptonPanelMain.ResumeLayout(false);
            this.kryptonPanelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanelMain;
        private Krypton.Toolkit.KryptonThemeComboBox kryptonThemeComboBox1;
        private Krypton.Toolkit.KryptonWrapLabel lblInstruction;
        private Krypton.Toolkit.KryptonLabel lblSectionAutoSize;
        private Krypton.Toolkit.KryptonLabel kryptonLabelShort;
        private Krypton.Toolkit.KryptonLabel kryptonLabelLong;
        private Krypton.Toolkit.KryptonLabel kryptonLabelNumber;
        private Krypton.Toolkit.KryptonLabel lblSectionNoAutoSize;
        private Krypton.Toolkit.KryptonLabel kryptonLabelFixedSize;
        private Krypton.Toolkit.KryptonLabel lblSectionStyles;
        private Krypton.Toolkit.KryptonLabel kryptonLabelNormalPanel;
        private Krypton.Toolkit.KryptonLabel kryptonLabelBoldPanel;
        private Krypton.Toolkit.KryptonLabel kryptonLabelTitlePanel;
        private Krypton.Toolkit.KryptonLabel kryptonLabelCaptionPanel;
        private Krypton.Toolkit.KryptonLabel lblSectionWithImage;
        private Krypton.Toolkit.KryptonLabel kryptonLabelWithImage;
    }
}

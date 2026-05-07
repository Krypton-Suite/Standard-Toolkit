#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

namespace TestForm
{
    partial class ProgressBarTest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgressBarTest));
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kchkShowTextShadow = new Krypton.Toolkit.KryptonCheckBox();
            this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            this.kcbtnProgressBarColour = new Krypton.Toolkit.KryptonColorButton();
            this.kcmbProgressBarStyle = new Krypton.Toolkit.KryptonComboBox();
            this.klblColorStyle = new Krypton.Toolkit.KryptonLabel();
            this.kcmbColorStyle = new Krypton.Toolkit.KryptonComboBox();
            this.kchkShowTextBackdrop = new Krypton.Toolkit.KryptonCheckBox();
            this.kcbtnBackdropColor = new Krypton.Toolkit.KryptonColorButton();
            this.kchkUseProgressValueAsText = new Krypton.Toolkit.KryptonCheckBox();
            this.ktrkProgressValues = new Krypton.Toolkit.KryptonTrackBar();
            this.kryptonProgressBarVert2 = new Krypton.Toolkit.KryptonProgressBar();
            this.kryptonProgressBarVert1 = new Krypton.Toolkit.KryptonProgressBar();
            this.kryptonProgressBar2 = new Krypton.Toolkit.KryptonProgressBar();
            this.kryptonProgressBar1 = new Krypton.Toolkit.KryptonProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbProgressBarStyle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbColorStyle)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kchkShowTextShadow);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel1.Controls.Add(this.kcbtnProgressBarColour);
            this.kryptonPanel1.Controls.Add(this.kcmbProgressBarStyle);
            this.kryptonPanel1.Controls.Add(this.klblColorStyle);
            this.kryptonPanel1.Controls.Add(this.kcmbColorStyle);
            this.kryptonPanel1.Controls.Add(this.kchkShowTextBackdrop);
            this.kryptonPanel1.Controls.Add(this.kcbtnBackdropColor);
            this.kryptonPanel1.Controls.Add(this.kchkUseProgressValueAsText);
            this.kryptonPanel1.Controls.Add(this.ktrkProgressValues);
            this.kryptonPanel1.Controls.Add(this.kryptonProgressBarVert2);
            this.kryptonPanel1.Controls.Add(this.kryptonProgressBarVert1);
            this.kryptonPanel1.Controls.Add(this.kryptonProgressBar2);
            this.kryptonPanel1.Controls.Add(this.kryptonProgressBar1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(569, 259);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kchkShowTextShadow
            // 
            this.kchkShowTextShadow.Location = new System.Drawing.Point(197, 191);
            this.kchkShowTextShadow.Name = "kchkShowTextShadow";
            this.kchkShowTextShadow.Size = new System.Drawing.Size(122, 20);
            this.kchkShowTextShadow.TabIndex = 24;
            this.kchkShowTextShadow.Values.Text = "Show text shadow";
            this.kchkShowTextShadow.CheckedChanged += new System.EventHandler(this.kchkShowTextShadow_CheckedChanged);
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(197, 120);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(87, 20);
            this.kryptonLabel1.TabIndex = 21;
            this.kryptonLabel1.Values.Text = "Progress Style";
            // 
            // kcbtnProgressBarColour
            // 
            this.kcbtnProgressBarColour.CustomColorPreviewShape = Krypton.Toolkit.KryptonColorButtonCustomColorPreviewShape.Circle;
            this.kcbtnProgressBarColour.Location = new System.Drawing.Point(13, 143);
            this.kcbtnProgressBarColour.Name = "kcbtnProgressBarColour";
            this.kcbtnProgressBarColour.SelectedColor = System.Drawing.Color.Green;
            this.kcbtnProgressBarColour.Size = new System.Drawing.Size(165, 25);
            this.kcbtnProgressBarColour.TabIndex = 16;
            this.kcbtnProgressBarColour.Values.Image = ((System.Drawing.Image)(resources.GetObject("kcbtnProgressBarColour.Values.Image")));
            this.kcbtnProgressBarColour.Values.RoundedCorners = 8;
            this.kcbtnProgressBarColour.Values.Text = "ProgressBar Colour";
            this.kcbtnProgressBarColour.SelectedColorChanged += new System.EventHandler<Krypton.Toolkit.ColorEventArgs>(this.kcbtnProgressBarColour_SelectedColorChanged);
            // 
            // kcmbProgressBarStyle
            // 
            this.kcmbProgressBarStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbProgressBarStyle.DropDownWidth = 261;
            this.kcmbProgressBarStyle.IntegralHeight = false;
            this.kcmbProgressBarStyle.Location = new System.Drawing.Point(290, 117);
            this.kcmbProgressBarStyle.Name = "kcmbProgressBarStyle";
            this.kcmbProgressBarStyle.Size = new System.Drawing.Size(179, 22);
            this.kcmbProgressBarStyle.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kcmbProgressBarStyle.TabIndex = 15;
            this.kcmbProgressBarStyle.SelectedIndexChanged += new System.EventHandler(this.kcmbProgressBarStyle_SelectedIndexChanged);
            // 
            // klblColorStyle
            // 
            this.klblColorStyle.Location = new System.Drawing.Point(197, 146);
            this.klblColorStyle.Name = "klblColorStyle";
            this.klblColorStyle.Size = new System.Drawing.Size(70, 20);
            this.klblColorStyle.TabIndex = 17;
            this.klblColorStyle.Values.Text = "Color Style";
            // 
            // kcmbColorStyle
            // 
            this.kcmbColorStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbColorStyle.DropDownWidth = 179;
            this.kcmbColorStyle.IntegralHeight = false;
            this.kcmbColorStyle.Location = new System.Drawing.Point(290, 146);
            this.kcmbColorStyle.Name = "kcmbColorStyle";
            this.kcmbColorStyle.Size = new System.Drawing.Size(179, 22);
            this.kcmbColorStyle.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kcmbColorStyle.TabIndex = 18;
            this.kcmbColorStyle.SelectedIndexChanged += new System.EventHandler(this.kcmbColorStyle_SelectedIndexChanged);
            // 
            // kchkShowTextBackdrop
            // 
            this.kchkShowTextBackdrop.Location = new System.Drawing.Point(17, 191);
            this.kchkShowTextBackdrop.Name = "kchkShowTextBackdrop";
            this.kchkShowTextBackdrop.Size = new System.Drawing.Size(132, 20);
            this.kchkShowTextBackdrop.TabIndex = 19;
            this.kchkShowTextBackdrop.Values.Text = "Show text backdrop";
            this.kchkShowTextBackdrop.CheckedChanged += new System.EventHandler(this.kchkShowTextBackdrop_CheckedChanged);
            // 
            // kcbtnBackdropColor
            // 
            this.kcbtnBackdropColor.CustomColorPreviewShape = Krypton.Toolkit.KryptonColorButtonCustomColorPreviewShape.Circle;
            this.kcbtnBackdropColor.Location = new System.Drawing.Point(12, 217);
            this.kcbtnBackdropColor.Name = "kcbtnBackdropColor";
            this.kcbtnBackdropColor.SelectedColor = System.Drawing.Color.WhiteSmoke;
            this.kcbtnBackdropColor.Size = new System.Drawing.Size(179, 25);
            this.kcbtnBackdropColor.TabIndex = 20;
            this.kcbtnBackdropColor.Values.Image = ((System.Drawing.Image)(resources.GetObject("kcbtnBackdropColor.Values.Image")));
            this.kcbtnBackdropColor.Values.RoundedCorners = 8;
            this.kcbtnBackdropColor.Values.Text = "Text Backdrop Colour";
            this.kcbtnBackdropColor.SelectedColorChanged += new System.EventHandler<Krypton.Toolkit.ColorEventArgs>(this.kcbtnBackdropColor_SelectedColorChanged);
            // 
            // kchkUseProgressValueAsText
            // 
            this.kchkUseProgressValueAsText.Checked = true;
            this.kchkUseProgressValueAsText.CheckState = System.Windows.Forms.CheckState.Checked;
            this.kchkUseProgressValueAsText.Location = new System.Drawing.Point(17, 116);
            this.kchkUseProgressValueAsText.Name = "kchkUseProgressValueAsText";
            this.kchkUseProgressValueAsText.Size = new System.Drawing.Size(165, 20);
            this.kchkUseProgressValueAsText.TabIndex = 14;
            this.kchkUseProgressValueAsText.Values.Text = "Use progress value as text";
            this.kchkUseProgressValueAsText.CheckedChanged += new System.EventHandler(this.kchkUseProgressValueAsText_CheckedChanged);
            // 
            // ktrkProgressValues
            // 
            this.ktrkProgressValues.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ktrkProgressValues.Location = new System.Drawing.Point(13, 77);
            this.ktrkProgressValues.Maximum = 100;
            this.ktrkProgressValues.Name = "ktrkProgressValues";
            this.ktrkProgressValues.Size = new System.Drawing.Size(470, 33);
            this.ktrkProgressValues.TabIndex = 13;
            this.ktrkProgressValues.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.ktrkProgressValues.ValueChanged += new System.EventHandler(this.ktrkProgressValues_ValueChanged);
            // 
            // kryptonProgressBarVert2
            // 
            this.kryptonProgressBarVert2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonProgressBarVert2.Enabled = false;
            this.kryptonProgressBarVert2.Location = new System.Drawing.Point(530, 13);
            this.kryptonProgressBarVert2.Name = "kryptonProgressBarVert2";
            this.kryptonProgressBarVert2.Orientation = Krypton.Toolkit.VisualOrientation.Right;
            this.kryptonProgressBarVert2.Size = new System.Drawing.Size(30, 234);
            this.kryptonProgressBarVert2.StateCommon.Back.Color1 = System.Drawing.Color.Green;
            this.kryptonProgressBarVert2.StateDisabled.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.OneNote;
            this.kryptonProgressBarVert2.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.OneNote;
            this.kryptonProgressBarVert2.TabIndex = 23;
            this.kryptonProgressBarVert2.Text = "75%";
            this.kryptonProgressBarVert2.TextBackdropColor = System.Drawing.Color.Empty;
            this.kryptonProgressBarVert2.TextShadowColor = System.Drawing.Color.Empty;
            this.kryptonProgressBarVert2.UseValueAsText = true;
            this.kryptonProgressBarVert2.Value = 75;
            this.kryptonProgressBarVert2.Values.Text = "75%";
            // 
            // kryptonProgressBarVert1
            // 
            this.kryptonProgressBarVert1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonProgressBarVert1.Location = new System.Drawing.Point(494, 13);
            this.kryptonProgressBarVert1.Name = "kryptonProgressBarVert1";
            this.kryptonProgressBarVert1.Orientation = Krypton.Toolkit.VisualOrientation.Right;
            this.kryptonProgressBarVert1.Size = new System.Drawing.Size(30, 234);
            this.kryptonProgressBarVert1.StateCommon.Back.Color1 = System.Drawing.Color.Green;
            this.kryptonProgressBarVert1.StateDisabled.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.OneNote;
            this.kryptonProgressBarVert1.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.OneNote;
            this.kryptonProgressBarVert1.TabIndex = 22;
            this.kryptonProgressBarVert1.Text = "75%";
            this.kryptonProgressBarVert1.TextBackdropColor = System.Drawing.Color.Empty;
            this.kryptonProgressBarVert1.TextShadowColor = System.Drawing.Color.Empty;
            this.kryptonProgressBarVert1.UseValueAsText = true;
            this.kryptonProgressBarVert1.Value = 75;
            this.kryptonProgressBarVert1.Values.Text = "75%";
            // 
            // kryptonProgressBar2
            // 
            this.kryptonProgressBar2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonProgressBar2.Enabled = false;
            this.kryptonProgressBar2.Location = new System.Drawing.Point(13, 45);
            this.kryptonProgressBar2.Name = "kryptonProgressBar2";
            this.kryptonProgressBar2.Size = new System.Drawing.Size(470, 26);
            this.kryptonProgressBar2.StateCommon.Back.Color1 = System.Drawing.Color.Green;
            this.kryptonProgressBar2.StateDisabled.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.OneNote;
            this.kryptonProgressBar2.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.OneNote;
            this.kryptonProgressBar2.TabIndex = 1;
            this.kryptonProgressBar2.Text = "75%";
            this.kryptonProgressBar2.TextBackdropColor = System.Drawing.Color.Empty;
            this.kryptonProgressBar2.TextShadowColor = System.Drawing.Color.Empty;
            this.kryptonProgressBar2.UseValueAsText = true;
            this.kryptonProgressBar2.Value = 75;
            this.kryptonProgressBar2.Values.Text = "75%";
            // 
            // kryptonProgressBar1
            // 
            this.kryptonProgressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonProgressBar1.Location = new System.Drawing.Point(13, 13);
            this.kryptonProgressBar1.Name = "kryptonProgressBar1";
            this.kryptonProgressBar1.Size = new System.Drawing.Size(470, 26);
            this.kryptonProgressBar1.StateCommon.Back.Color1 = System.Drawing.Color.Green;
            this.kryptonProgressBar1.StateDisabled.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.OneNote;
            this.kryptonProgressBar1.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.OneNote;
            this.kryptonProgressBar1.TabIndex = 0;
            this.kryptonProgressBar1.Text = "75%";
            this.kryptonProgressBar1.TextBackdropColor = System.Drawing.Color.Empty;
            this.kryptonProgressBar1.TextShadowColor = System.Drawing.Color.Empty;
            this.kryptonProgressBar1.UseValueAsText = true;
            this.kryptonProgressBar1.Value = 75;
            this.kryptonProgressBar1.Values.Text = "75%";
            // 
            // ProgressBarTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 259);
            this.Controls.Add(this.kryptonPanel1);
            this.Location = new System.Drawing.Point(0, 0);
            this.MinimumSize = new System.Drawing.Size(500, 290);
            this.Name = "ProgressBarTest";
            this.Text = "ProgressBarTest";
            this.Load += new System.EventHandler(this.ProgressBarTest_Load);
            this.Controls.SetChildIndex(this.kryptonPanel1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbProgressBarStyle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbColorStyle)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonProgressBar kryptonProgressBar1;
        private Krypton.Toolkit.KryptonProgressBar kryptonProgressBar2;
        private Krypton.Toolkit.KryptonCheckBox kchkUseProgressValueAsText;
        private Krypton.Toolkit.KryptonTrackBar ktrkProgressValues;
        private Krypton.Toolkit.KryptonComboBox kcmbProgressBarStyle;
        private Krypton.Toolkit.KryptonColorButton kcbtnProgressBarColour;
        private Krypton.Toolkit.KryptonLabel klblColorStyle;
        private Krypton.Toolkit.KryptonComboBox kcmbColorStyle;
        private Krypton.Toolkit.KryptonCheckBox kchkShowTextBackdrop;
        private Krypton.Toolkit.KryptonColorButton kcbtnBackdropColor;
        private KryptonLabel kryptonLabel1;
        private Krypton.Toolkit.KryptonProgressBar kryptonProgressBarVert1;
        private Krypton.Toolkit.KryptonProgressBar kryptonProgressBarVert2;
        private KryptonCheckBox kchkShowTextShadow;
    }
}
#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved. 
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
            this.kcbtnProgressBarColour = new Krypton.Toolkit.KryptonColorButton();
            this.kcmbProgressBarStyle = new Krypton.Toolkit.KryptonComboBox();
            this.kchkUseProgressValueAsText = new Krypton.Toolkit.KryptonCheckBox();
            this.ktrkProgressValues = new Krypton.Toolkit.KryptonTrackBar();
            this.kryptonProgressBar2 = new Krypton.Toolkit.KryptonProgressBar();
            this.kryptonProgressBar1 = new Krypton.Toolkit.KryptonProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbProgressBarStyle)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kcbtnProgressBarColour);
            this.kryptonPanel1.Controls.Add(this.kcmbProgressBarStyle);
            this.kryptonPanel1.Controls.Add(this.kchkUseProgressValueAsText);
            this.kryptonPanel1.Controls.Add(this.ktrkProgressValues);
            this.kryptonPanel1.Controls.Add(this.kryptonProgressBar2);
            this.kryptonPanel1.Controls.Add(this.kryptonProgressBar1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(485, 177);
            this.kryptonPanel1.TabIndex = 0;
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
            this.kcmbProgressBarStyle.Location = new System.Drawing.Point(208, 117);
            this.kcmbProgressBarStyle.Name = "kcmbProgressBarStyle";
            this.kcmbProgressBarStyle.Size = new System.Drawing.Size(261, 22);
            this.kcmbProgressBarStyle.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kcmbProgressBarStyle.TabIndex = 15;
            this.kcmbProgressBarStyle.SelectedIndexChanged += new System.EventHandler(this.kcmbProgressBarStyle_SelectedIndexChanged);
            // 
            // kchkUseProgressValueAsText
            // 
            this.kchkUseProgressValueAsText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.kchkUseProgressValueAsText.Location = new System.Drawing.Point(17, 116);
            this.kchkUseProgressValueAsText.Name = "kchkUseProgressValueAsText";
            this.kchkUseProgressValueAsText.Size = new System.Drawing.Size(165, 20);
            this.kchkUseProgressValueAsText.TabIndex = 14;
            this.kchkUseProgressValueAsText.Values.Text = "Use progress value as text";
            this.kchkUseProgressValueAsText.CheckedChanged += new System.EventHandler(this.kchkUseProgressValueAsText_CheckedChanged);
            // 
            // ktrkProgressValues
            // 
            this.ktrkProgressValues.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ktrkProgressValues.Location = new System.Drawing.Point(17, 77);
            this.ktrkProgressValues.Maximum = 100;
            this.ktrkProgressValues.Name = "ktrkProgressValues";
            this.ktrkProgressValues.Size = new System.Drawing.Size(456, 33);
            this.ktrkProgressValues.TabIndex = 13;
            this.ktrkProgressValues.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.ktrkProgressValues.ValueChanged += new System.EventHandler(this.ktrkProgressValues_ValueChanged);
            // 
            // kryptonProgressBar2
            // 
            this.kryptonProgressBar2.Enabled = false;
            this.kryptonProgressBar2.Location = new System.Drawing.Point(13, 45);
            this.kryptonProgressBar2.Name = "kryptonProgressBar2";
            this.kryptonProgressBar2.Size = new System.Drawing.Size(456, 26);
            this.kryptonProgressBar2.StateCommon.Back.Color1 = System.Drawing.Color.Green;
            this.kryptonProgressBar2.StateDisabled.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.OneNote;
            this.kryptonProgressBar2.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.OneNote;
            this.kryptonProgressBar2.TabIndex = 1;
            this.kryptonProgressBar2.Values.Text = "";
            // 
            // kryptonProgressBar1
            // 
            this.kryptonProgressBar1.Location = new System.Drawing.Point(13, 13);
            this.kryptonProgressBar1.Name = "kryptonProgressBar1";
            this.kryptonProgressBar1.Size = new System.Drawing.Size(456, 26);
            this.kryptonProgressBar1.StateCommon.Back.Color1 = System.Drawing.Color.Green;
            this.kryptonProgressBar1.StateDisabled.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.OneNote;
            this.kryptonProgressBar1.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.OneNote;
            this.kryptonProgressBar1.TabIndex = 0;
            this.kryptonProgressBar1.Values.Text = "";
            // 
            // ProgressBarTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 177);
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "ProgressBarTest";
            this.Text = "ProgressBarTest";
            this.Load += new System.EventHandler(this.ProgressBarTest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbProgressBarStyle)).EndInit();
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
    }
}
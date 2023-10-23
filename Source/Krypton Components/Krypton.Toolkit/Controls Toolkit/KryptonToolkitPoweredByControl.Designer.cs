#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2023. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit
{
    partial class KryptonToolkitPoweredByControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            kryptonGroupBox1 = new KryptonGroupBox();
            tlpnlContent = new TableLayoutPanel();
            klwlblDetails = new KryptonLinkWrapLabel();
            kpbxLogo = new KryptonPictureBox();
            ktcmbCurrentTheme = new KryptonThemeComboBox();
            kwlblCurrentTheme = new KryptonWrapLabel();
            ((ISupportInitialize)kryptonGroupBox1).BeginInit();
            ((ISupportInitialize)kryptonGroupBox1.Panel).BeginInit();
            kryptonGroupBox1.Panel.SuspendLayout();
            tlpnlContent.SuspendLayout();
            ((ISupportInitialize)kpbxLogo).BeginInit();
            ((ISupportInitialize)ktcmbCurrentTheme).BeginInit();
            SuspendLayout();
            // 
            // kryptonGroupBox1
            // 
            kryptonGroupBox1.Dock = DockStyle.Fill;
            kryptonGroupBox1.Location = new Point(0, 0);
            kryptonGroupBox1.Name = "kryptonGroupBox1";
            // 
            // 
            // 
            kryptonGroupBox1.Panel.Controls.Add(tlpnlContent);
            kryptonGroupBox1.Size = new Size(659, 301);
            kryptonGroupBox1.TabIndex = 0;
            kryptonGroupBox1.Values.Heading = "Powered by Krypton Toolkit";
            // 
            // tlpnlContent
            // 
            tlpnlContent.BackColor = Color.Transparent;
            tlpnlContent.ColumnCount = 2;
            tlpnlContent.ColumnStyles.Add(new ColumnStyle());
            tlpnlContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpnlContent.Controls.Add(klwlblDetails, 1, 0);
            tlpnlContent.Controls.Add(kpbxLogo, 0, 0);
            tlpnlContent.Controls.Add(ktcmbCurrentTheme, 1, 2);
            tlpnlContent.Controls.Add(kwlblCurrentTheme, 1, 1);
            tlpnlContent.Dock = DockStyle.Fill;
            tlpnlContent.Location = new Point(0, 0);
            tlpnlContent.Name = "tlpnlContent";
            tlpnlContent.RowCount = 3;
            tlpnlContent.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpnlContent.RowStyles.Add(new RowStyle());
            tlpnlContent.RowStyles.Add(new RowStyle());
            tlpnlContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlpnlContent.Size = new Size(655, 277);
            tlpnlContent.TabIndex = 0;
            // 
            // klwlblDetails
            // 
            klwlblDetails.Dock = DockStyle.Fill;
            klwlblDetails.Font = new Font("Segoe UI", 9F);
            klwlblDetails.ForeColor = Color.FromArgb(30, 57, 91);
            klwlblDetails.LabelStyle = LabelStyle.AlternateControl;
            klwlblDetails.LinkArea = new LinkArea(134, 144);
            klwlblDetails.Location = new Point(64, 4);
            klwlblDetails.Margin = new Padding(4);
            klwlblDetails.Name = "klwlblDetails";
            klwlblDetails.Size = new Size(587, 217);
            klwlblDetails.Text = "Some of the components used in this application are part of the Krypton Standard Toolkit. \r\n\r\nLicense: BSD-3-Clause\r\n\r\nTo learn more, click here.";
            klwlblDetails.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            klwlblDetails.UseCompatibleTextRendering = true;
            klwlblDetails.LinkClicked += klwlblDetails_LinkClicked;
            // 
            // kpbxLogo
            // 
            kpbxLogo.Dock = DockStyle.Fill;
            kpbxLogo.Location = new Point(8, 4);
            kpbxLogo.Margin = new Padding(8, 4, 4, 4);
            kpbxLogo.Name = "kpbxLogo";
            kpbxLogo.Size = new Size(48, 217);
            kpbxLogo.SizeMode = PictureBoxSizeMode.CenterImage;
            kpbxLogo.TabIndex = 5;
            kpbxLogo.TabStop = false;
            // 
            // ktcmbCurrentTheme
            // 
            ktcmbCurrentTheme.Dock = DockStyle.Fill;
            ktcmbCurrentTheme.DropDownWidth = 573;
            ktcmbCurrentTheme.IntegralHeight = false;
            ktcmbCurrentTheme.Location = new Point(64, 252);
            ktcmbCurrentTheme.Margin = new Padding(4);
            ktcmbCurrentTheme.Name = "ktcmbCurrentTheme";
            ktcmbCurrentTheme.Size = new Size(587, 21);
            ktcmbCurrentTheme.StateCommon.ComboBox.Content.TextH = PaletteRelativeAlign.Near;
            ktcmbCurrentTheme.TabIndex = 4;
            // 
            // kwlblCurrentTheme
            // 
            kwlblCurrentTheme.Dock = DockStyle.Fill;
            kwlblCurrentTheme.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            kwlblCurrentTheme.ForeColor = Color.FromArgb(30, 57, 91);
            kwlblCurrentTheme.LabelStyle = LabelStyle.BoldControl;
            kwlblCurrentTheme.Location = new Point(64, 229);
            kwlblCurrentTheme.Margin = new Padding(4);
            kwlblCurrentTheme.Name = "kwlblCurrentTheme";
            kwlblCurrentTheme.Size = new Size(587, 15);
            kwlblCurrentTheme.Text = "Current Theme:";
            kwlblCurrentTheme.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // KryptonToolkitPoweredByControl
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(kryptonGroupBox1);
            Name = "KryptonToolkitPoweredByControl";
            Size = new Size(659, 301);
            ((ISupportInitialize)kryptonGroupBox1.Panel).EndInit();
            kryptonGroupBox1.Panel.ResumeLayout(false);
            ((ISupportInitialize)kryptonGroupBox1).EndInit();
            tlpnlContent.ResumeLayout(false);
            tlpnlContent.PerformLayout();
            ((ISupportInitialize)kpbxLogo).EndInit();
            ((ISupportInitialize)ktcmbCurrentTheme).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private KryptonGroupBox kryptonGroupBox1;
        private TableLayoutPanel tlpnlContent;
        private KryptonLinkWrapLabel klwlblDetails;
        private KryptonWrapLabel kwlblCurrentTheme;
        private KryptonThemeComboBox ktcmbCurrentTheme;
        private KryptonPictureBox kpbxLogo;
    }
}

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
    partial class CustomPaletteTest
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomPaletteTest));
            this.kryptonManager1 = new Krypton.Toolkit.KryptonManager(this.components);
            this.kryptonCustomPaletteBase1 = new Krypton.Toolkit.KryptonCustomPaletteBase(this.components);
            this.kryptonThemeComboBox1 = new Krypton.Toolkit.KryptonThemeComboBox();
            this.kryptonScrollBar1 = new Krypton.Toolkit.KryptonScrollBar();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonManager1
            // 
            this.kryptonManager1.BaseFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonManager1.GlobalPaletteMode = Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            // 
            // kryptonCustomPaletteBase1
            // 
            this.kryptonCustomPaletteBase1.BaseFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonCustomPaletteBase1.BasePaletteType = Krypton.Toolkit.BasePaletteType.Custom;
            this.kryptonCustomPaletteBase1.ThemeName = null;
            this.kryptonCustomPaletteBase1.UseThemeFormChromeBorderWidth = Krypton.Toolkit.InheritBool.True;
            // 
            // kryptonThemeComboBox1
            // 
            this.kryptonThemeComboBox1.DefaultPalette = Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.kryptonThemeComboBox1.DropDownWidth = 301;
            this.kryptonThemeComboBox1.IntegralHeight = false;
            this.kryptonThemeComboBox1.Location = new System.Drawing.Point(239, 217);
            this.kryptonThemeComboBox1.Name = "kryptonThemeComboBox1";
            this.kryptonThemeComboBox1.Size = new System.Drawing.Size(301, 22);
            this.kryptonThemeComboBox1.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kryptonThemeComboBox1.TabIndex = 0;
            // 
            // kryptonScrollBar1
            // 
            this.kryptonScrollBar1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(140)))), ((int)(((byte)(201)))));
            this.kryptonScrollBar1.DisabledBorderColor = System.Drawing.Color.Gray;
            this.kryptonScrollBar1.Location = new System.Drawing.Point(66, 12);
            this.kryptonScrollBar1.Name = "kryptonScrollBar1";
            this.kryptonScrollBar1.Opacity = 1D;
            this.kryptonScrollBar1.ScrollBarWidth = 19;
            this.kryptonScrollBar1.Size = new System.Drawing.Size(19, 404);
            this.kryptonScrollBar1.TabIndex = 1;
            this.kryptonScrollBar1.Text = "kryptonScrollBar1";
            // 
            // CustomPaletteTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 453);
            this.Controls.Add(this.kryptonScrollBar1);
            this.Controls.Add(this.kryptonThemeComboBox1);
            this.Name = "CustomPaletteTest";
            this.Text = "CustomPaletteTest";
            this.UseThemeFormChromeBorderWidth = false;
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonManager kryptonManager1;
        private Krypton.Toolkit.KryptonCustomPaletteBase kryptonCustomPaletteBase1;
        private Krypton.Toolkit.KryptonThemeComboBox kryptonThemeComboBox1;
        private Krypton.Toolkit.KryptonScrollBar kryptonScrollBar1;
    }
}
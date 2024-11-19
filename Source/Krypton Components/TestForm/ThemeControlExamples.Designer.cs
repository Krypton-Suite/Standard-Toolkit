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
    partial class ThemeControlExamples
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
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kbtnThemeBrowser = new Krypton.Toolkit.KryptonButton();
            this.kryptonLabel2 = new Krypton.Toolkit.KryptonLabel();
            this.kryptonThemeComboBox1 = new Krypton.Toolkit.KryptonThemeComboBox();
            this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            this.kryptonThemeListBox1 = new Krypton.Toolkit.KryptonThemeListBox();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonThemeListBox1);
            this.kryptonPanel1.Controls.Add(this.kbtnThemeBrowser);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel2);
            this.kryptonPanel1.Controls.Add(this.kryptonThemeComboBox1);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(800, 450);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kbtnThemeBrowser
            // 
            this.kbtnThemeBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.kbtnThemeBrowser.Location = new System.Drawing.Point(13, 413);
            this.kbtnThemeBrowser.Name = "kbtnThemeBrowser";
            this.kbtnThemeBrowser.Size = new System.Drawing.Size(173, 25);
            this.kbtnThemeBrowser.TabIndex = 3;
            this.kbtnThemeBrowser.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnThemeBrowser.Values.Text = "Theme Browser";
            this.kbtnThemeBrowser.Click += new System.EventHandler(this.kbtnThemeBrowser_Click);
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.LabelStyle = Krypton.Toolkit.LabelStyle.BoldPanel;
            this.kryptonLabel2.Location = new System.Drawing.Point(13, 40);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(99, 22);
            this.kryptonLabel2.TabIndex = 2;
            this.kryptonLabel2.Values.Text = "Theme ListBox:";
            // 
            // kryptonThemeComboBox1
            // 
            this.kryptonThemeComboBox1.DefaultPalette = Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.kryptonThemeComboBox1.DropDownWidth = 334;
            this.kryptonThemeComboBox1.IntegralHeight = false;
            this.kryptonThemeComboBox1.Location = new System.Drawing.Point(141, 13);
            this.kryptonThemeComboBox1.Name = "kryptonThemeComboBox1";
            this.kryptonThemeComboBox1.Size = new System.Drawing.Size(334, 21);
            this.kryptonThemeComboBox1.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kryptonThemeComboBox1.TabIndex = 1;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.LabelStyle = Krypton.Toolkit.LabelStyle.BoldPanel;
            this.kryptonLabel1.Location = new System.Drawing.Point(13, 13);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(121, 22);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "Theme ComboBox:";
            // 
            // kryptonThemeListBox1
            // 
            this.kryptonThemeListBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.kryptonThemeListBox1.DefaultPalette = Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.kryptonThemeListBox1.Location = new System.Drawing.Point(141, 40);
            this.kryptonThemeListBox1.Name = "kryptonThemeListBox1";
            this.kryptonThemeListBox1.Size = new System.Drawing.Size(334, 356);
            this.kryptonThemeListBox1.TabIndex = 4;
            // 
            // ThemeControlExamples
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "ThemeControlExamples";
            this.Text = "ThemeControlExamples";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonPanel kryptonPanel1;
        private KryptonLabel kryptonLabel1;
        private KryptonThemeComboBox kryptonThemeComboBox1;
        private KryptonLabel kryptonLabel2;
           private KryptonButton kbtnThemeBrowser;
        private KryptonThemeListBox kryptonThemeListBox1;
    }
}
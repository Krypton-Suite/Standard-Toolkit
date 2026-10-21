#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm
{
    partial class FormBorderTest
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
            this.kryptonLabel2 = new Krypton.Toolkit.KryptonLabel();
            this.kryptonThemeComboBox1 = new Krypton.Toolkit.KryptonThemeComboBox();
            this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            this.kcmbGripMode = new Krypton.Toolkit.KryptonComboBox();
            this.klblGripMode = new Krypton.Toolkit.KryptonLabel();
            this.kcmbBorderStyle = new Krypton.Toolkit.KryptonComboBox();
            this.kbtnExit = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbGripMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbBorderStyle)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonLabel2);
            this.kryptonPanel1.Controls.Add(this.kryptonThemeComboBox1);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel1.Controls.Add(this.kcmbGripMode);
            this.kryptonPanel1.Controls.Add(this.klblGripMode);
            this.kryptonPanel1.Controls.Add(this.kcmbBorderStyle);
            this.kryptonPanel1.Controls.Add(this.kbtnExit);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(441, 198);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(19, 112);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(50, 20);
            this.kryptonLabel2.TabIndex = 4;
            this.kryptonLabel2.Values.Text = "Theme:";
            // 
            // kryptonThemeComboBox1
            // 
            this.kryptonThemeComboBox1.DropDownWidth = 270;
            this.kryptonThemeComboBox1.Location = new System.Drawing.Point(130, 110);
            this.kryptonThemeComboBox1.Name = "kryptonThemeComboBox1";
            this.kryptonThemeComboBox1.Size = new System.Drawing.Size(270, 22);
            this.kryptonThemeComboBox1.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kryptonThemeComboBox1.TabIndex = 5;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(19, 32);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(79, 20);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "Border Style:";
            // 
            // kcmbGripMode
            // 
            this.kcmbGripMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbGripMode.DropDownWidth = 121;
            this.kcmbGripMode.IntegralHeight = false;
            this.kcmbGripMode.Location = new System.Drawing.Point(130, 69);
            this.kcmbGripMode.Name = "kcmbGripMode";
            this.kcmbGripMode.Size = new System.Drawing.Size(270, 22);
            this.kcmbGripMode.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kcmbGripMode.TabIndex = 3;
            this.kcmbGripMode.SelectedIndexChanged += new System.EventHandler(this.kcmbGripMode_SelectedIndexChanged);
            // 
            // klblGripMode
            // 
            this.klblGripMode.Location = new System.Drawing.Point(19, 71);
            this.klblGripMode.Name = "klblGripMode";
            this.klblGripMode.Size = new System.Drawing.Size(91, 20);
            this.klblGripMode.TabIndex = 2;
            this.klblGripMode.Values.Text = "Size Grip Style:";
            // 
            // kcmbBorderStyle
            // 
            this.kcmbBorderStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbBorderStyle.DropDownWidth = 121;
            this.kcmbBorderStyle.IntegralHeight = false;
            this.kcmbBorderStyle.Location = new System.Drawing.Point(130, 30);
            this.kcmbBorderStyle.Name = "kcmbBorderStyle";
            this.kcmbBorderStyle.Size = new System.Drawing.Size(270, 22);
            this.kcmbBorderStyle.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kcmbBorderStyle.TabIndex = 1;
            this.kcmbBorderStyle.SelectedIndexChanged += new System.EventHandler(this.kcmbBorderStyle_SelectedIndexChanged);
            // 
            // kbtnExit
            // 
            this.kbtnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.kbtnExit.Location = new System.Drawing.Point(312, 148);
            this.kbtnExit.Name = "kbtnExit";
            this.kbtnExit.Size = new System.Drawing.Size(90, 25);
            this.kbtnExit.TabIndex = 6;
            this.kbtnExit.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnExit.Values.Text = "Exit";
            this.kbtnExit.Click += new System.EventHandler(this.kbtnExit_Click);
            // 
            // FormBorderTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.kbtnExit;
            this.ClientSize = new System.Drawing.Size(441, 198);
            this.Controls.Add(this.kryptonPanel1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "FormBorderTest";
            this.Text = "Form Border Test";
            this.Load += new System.EventHandler(this.FormBorderTest_Load);
            this.Controls.SetChildIndex(this.kryptonPanel1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbGripMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbBorderStyle)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonButton kbtnExit;
        private Krypton.Toolkit.KryptonComboBox kcmbBorderStyle;
        private Krypton.Toolkit.KryptonLabel klblGripMode;
        private Krypton.Toolkit.KryptonComboBox kcmbGripMode;
        private KryptonLabel kryptonLabel1;
        private KryptonLabel kryptonLabel2;
        private KryptonThemeComboBox kryptonThemeComboBox1;
    }
}
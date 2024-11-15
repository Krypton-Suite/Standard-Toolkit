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
            this.kbtnExit = new Krypton.Toolkit.KryptonButton();
            this.kcmbBorderStyle = new Krypton.Toolkit.KryptonComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbBorderStyle)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kcmbBorderStyle);
            this.kryptonPanel1.Controls.Add(this.kbtnExit);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(804, 454);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kbtnExit
            // 
            this.kbtnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.kbtnExit.Location = new System.Drawing.Point(702, 417);
            this.kbtnExit.Name = "kbtnExit";
            this.kbtnExit.Size = new System.Drawing.Size(90, 25);
            this.kbtnExit.TabIndex = 39;
            this.kbtnExit.Values.Text = "Exit";
            this.kbtnExit.Click += new System.EventHandler(this.kbtnExit_Click);
            // 
            // kcmbBorderStyle
            // 
            this.kcmbBorderStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbBorderStyle.DropDownWidth = 121;
            this.kcmbBorderStyle.IntegralHeight = false;
            this.kcmbBorderStyle.Location = new System.Drawing.Point(159, 107);
            this.kcmbBorderStyle.Name = "kcmbBorderStyle";
            this.kcmbBorderStyle.Size = new System.Drawing.Size(270, 21);
            this.kcmbBorderStyle.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kcmbBorderStyle.TabIndex = 40;
            this.kcmbBorderStyle.SelectedIndexChanged += new System.EventHandler(this.kcmbBorderStyle_SelectedIndexChanged);
            // 
            // FormBorderTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.kbtnExit;
            this.ClientSize = new System.Drawing.Size(804, 454);
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "FormBorderTest";
            this.Text = "FormBorderTest";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormBorderTest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kcmbBorderStyle)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonButton kbtnExit;
        private Krypton.Toolkit.KryptonComboBox kcmbBorderStyle;
    }
}
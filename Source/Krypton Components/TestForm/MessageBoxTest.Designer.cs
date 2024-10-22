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
    partial class MessageBoxTest
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
            this.kryptonCheckBox1 = new Krypton.Toolkit.KryptonCheckBox();
            this.kryptonButton11 = new Krypton.Toolkit.KryptonButton();
            this.kbtnTestMessagebox = new Krypton.Toolkit.KryptonButton();
            this.kcmdMessageboxTest = new Krypton.Toolkit.KryptonCommand();
            this.kbtnCustomMessageBox = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kbtnCustomMessageBox);
            this.kryptonPanel1.Controls.Add(this.kryptonCheckBox1);
            this.kryptonPanel1.Controls.Add(this.kryptonButton11);
            this.kryptonPanel1.Controls.Add(this.kbtnTestMessagebox);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(272, 142);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonCheckBox1
            // 
            this.kryptonCheckBox1.Checked = true;
            this.kryptonCheckBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.kryptonCheckBox1.Location = new System.Drawing.Point(12, 105);
            this.kryptonCheckBox1.Name = "kryptonCheckBox1";
            this.kryptonCheckBox1.Size = new System.Drawing.Size(126, 20);
            this.kryptonCheckBox1.TabIndex = 41;
            this.kryptonCheckBox1.Values.Text = "Show Close Button";
            // 
            // kryptonButton11
            // 
            this.kryptonButton11.Location = new System.Drawing.Point(12, 43);
            this.kryptonButton11.Name = "kryptonButton11";
            this.kryptonButton11.Size = new System.Drawing.Size(245, 25);
            this.kryptonButton11.TabIndex = 40;
            this.kryptonButton11.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kryptonButton11.Values.Text = "Test Messagebox (no text)";
            this.kryptonButton11.Click += new System.EventHandler(this.kryptonButton11_Click);
            // 
            // kbtnTestMessagebox
            // 
            this.kbtnTestMessagebox.Location = new System.Drawing.Point(12, 12);
            this.kbtnTestMessagebox.Name = "kbtnTestMessagebox";
            this.kbtnTestMessagebox.Size = new System.Drawing.Size(245, 25);
            this.kbtnTestMessagebox.TabIndex = 39;
            this.kbtnTestMessagebox.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnTestMessagebox.Values.Text = "Test Messagebox";
            this.kbtnTestMessagebox.Click += new System.EventHandler(this.kbtnTestMessagebox_Click);
            // 
            // kcmdMessageboxTest
            // 
            this.kcmdMessageboxTest.Text = "kryptonCommand1";
            this.kcmdMessageboxTest.Execute += new System.EventHandler(this.kcmdMessageboxTest_Execute);
            // 
            // kbtnCustomMessageBox
            // 
            this.kbtnCustomMessageBox.Location = new System.Drawing.Point(12, 74);
            this.kbtnCustomMessageBox.Name = "kbtnCustomMessageBox";
            this.kbtnCustomMessageBox.Size = new System.Drawing.Size(245, 25);
            this.kbtnCustomMessageBox.TabIndex = 42;
            this.kbtnCustomMessageBox.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnCustomMessageBox.Values.Text = "Test Messagebox (custom)";
            this.kbtnCustomMessageBox.Click += new System.EventHandler(this.kbtnCustomMessageBox_Click);
            // 
            // MessageBoxTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(272, 142);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "MessageBoxTest";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MessageBoxTest";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonCheckBox kryptonCheckBox1;
        private Krypton.Toolkit.KryptonButton kryptonButton11;
        private Krypton.Toolkit.KryptonButton kbtnTestMessagebox;
        private Krypton.Toolkit.KryptonCommand kcmdMessageboxTest;
        private KryptonButton kbtnCustomMessageBox;
    }
}
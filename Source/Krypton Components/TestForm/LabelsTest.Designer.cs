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
    partial class LabelsTest
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
            this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            this.kryptonLinkLabel1 = new Krypton.Toolkit.KryptonLinkLabel();
            this.kryptonLinkWrapLabel1 = new Krypton.Toolkit.KryptonLinkWrapLabel();
            this.kryptonLinkWrapLabel2 = new Krypton.Toolkit.KryptonLinkWrapLabel();
            this.kryptonWrapLabel1 = new Krypton.Toolkit.KryptonWrapLabel();
            this.kryptonWrapLabel2 = new Krypton.Toolkit.KryptonWrapLabel();
            this.SuspendLayout();
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(13, 13);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(88, 20);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "kryptonLabel1";
            // 
            // kryptonLinkLabel1
            // 
            this.kryptonLinkLabel1.Location = new System.Drawing.Point(13, 40);
            this.kryptonLinkLabel1.Name = "kryptonLinkLabel1";
            this.kryptonLinkLabel1.Size = new System.Drawing.Size(110, 20);
            this.kryptonLinkLabel1.TabIndex = 1;
            this.kryptonLinkLabel1.Values.Text = "kryptonLinkLabel1";
            // 
            // kryptonLinkWrapLabel1
            // 
            this.kryptonLinkWrapLabel1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.kryptonLinkWrapLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.kryptonLinkWrapLabel1.LabelStyle = Krypton.Toolkit.LabelStyle.AlternateControl;
            this.kryptonLinkWrapLabel1.Location = new System.Drawing.Point(13, 67);
            this.kryptonLinkWrapLabel1.Name = "kryptonLinkWrapLabel1";
            this.kryptonLinkWrapLabel1.Size = new System.Drawing.Size(132, 15);
            this.kryptonLinkWrapLabel1.Text = "kryptonLinkWrapLabel1";
            // 
            // kryptonLinkWrapLabel2
            // 
            this.kryptonLinkWrapLabel2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.kryptonLinkWrapLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.kryptonLinkWrapLabel2.LabelStyle = Krypton.Toolkit.LabelStyle.AlternateControl;
            this.kryptonLinkWrapLabel2.Location = new System.Drawing.Point(13, 86);
            this.kryptonLinkWrapLabel2.Name = "kryptonLinkWrapLabel2";
            this.kryptonLinkWrapLabel2.Size = new System.Drawing.Size(132, 15);
            this.kryptonLinkWrapLabel2.Text = "kryptonLinkWrapLabel2";
            // 
            // kryptonWrapLabel1
            // 
            this.kryptonWrapLabel1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.kryptonWrapLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.kryptonWrapLabel1.LabelStyle = Krypton.Toolkit.LabelStyle.AlternateControl;
            this.kryptonWrapLabel1.Location = new System.Drawing.Point(13, 135);
            this.kryptonWrapLabel1.Name = "kryptonWrapLabel1";
            this.kryptonWrapLabel1.Size = new System.Drawing.Size(110, 15);
            this.kryptonWrapLabel1.Text = "kryptonWrapLabel1";
            // 
            // kryptonWrapLabel2
            // 
            this.kryptonWrapLabel2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.kryptonWrapLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.kryptonWrapLabel2.LabelStyle = Krypton.Toolkit.LabelStyle.AlternateControl;
            this.kryptonWrapLabel2.Location = new System.Drawing.Point(13, 179);
            this.kryptonWrapLabel2.Name = "kryptonWrapLabel2";
            this.kryptonWrapLabel2.Size = new System.Drawing.Size(149, 105);
            this.kryptonWrapLabel2.Text = "This is a KryptonWrapLabel\r\n\r\nwith\r\n\r\nmultiple\r\n\r\nlines.";
            // 
            // LabelsTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.kryptonWrapLabel2);
            this.Controls.Add(this.kryptonWrapLabel1);
            this.Controls.Add(this.kryptonLinkWrapLabel2);
            this.Controls.Add(this.kryptonLinkWrapLabel1);
            this.Controls.Add(this.kryptonLinkLabel1);
            this.Controls.Add(this.kryptonLabel1);
            this.Name = "LabelsTest";
            this.Text = "LabelsTest";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private KryptonLabel kryptonLabel1;
        private KryptonLinkLabel kryptonLinkLabel1;
        private KryptonLinkWrapLabel kryptonLinkWrapLabel1;
        private KryptonLinkWrapLabel kryptonLinkWrapLabel2;
        private KryptonWrapLabel kryptonWrapLabel1;
        private KryptonWrapLabel kryptonWrapLabel2;
    }
}
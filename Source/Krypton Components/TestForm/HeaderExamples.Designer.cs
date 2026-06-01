#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2024 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm
{
    partial class HeaderExamples
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
            this.kryptonHeaderTop = new Krypton.Toolkit.KryptonHeader();
            this.kryptonHeaderBottom = new Krypton.Toolkit.KryptonHeader();
            this.labelInstructions = new System.Windows.Forms.Label();
            this.SuspendLayout();
            //
            // kryptonHeaderTop
            //
            this.kryptonHeaderTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonHeaderTop.Location = new System.Drawing.Point(0, 0);
            this.kryptonHeaderTop.Name = "kryptonHeaderTop";
            this.kryptonHeaderTop.Size = new System.Drawing.Size(800, 31);
            this.kryptonHeaderTop.TabIndex = 0;
            this.kryptonHeaderTop.Values.Description = "AutoSize = true, Dock = Top";
            this.kryptonHeaderTop.Values.Heading = "Top docked header";
            //
            // kryptonHeaderBottom
            //
            this.kryptonHeaderBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonHeaderBottom.Location = new System.Drawing.Point(0, 419);
            this.kryptonHeaderBottom.Name = "kryptonHeaderBottom";
            this.kryptonHeaderBottom.Size = new System.Drawing.Size(800, 31);
            this.kryptonHeaderBottom.TabIndex = 1;
            this.kryptonHeaderBottom.Values.Description = "AutoSize = true, Dock = Bottom";
            this.kryptonHeaderBottom.Values.Heading = "Bottom docked header";
            //
            // labelInstructions
            //
            this.labelInstructions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelInstructions.Location = new System.Drawing.Point(0, 31);
            this.labelInstructions.Name = "labelInstructions";
            this.labelInstructions.Size = new System.Drawing.Size(800, 388);
            this.labelInstructions.TabIndex = 2;
            this.labelInstructions.Text = "Resize this form. Both KryptonHeader controls should keep the full client width while AutoSize keeps their preferred height.";
            this.labelInstructions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // HeaderExamples
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelInstructions);
            this.Controls.Add(this.kryptonHeaderBottom);
            this.Controls.Add(this.kryptonHeaderTop);
            this.Name = "HeaderExamples";
            this.Text = "HeaderExamples";
            this.ResumeLayout(false);

        }

        #endregion
        private KryptonHeader kryptonHeaderTop;
        private KryptonHeader kryptonHeaderBottom;
        private System.Windows.Forms.Label labelInstructions;
    }
}
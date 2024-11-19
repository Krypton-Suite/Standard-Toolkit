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
    partial class FadeFormTest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FadeFormTest));
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.cbtnShowImage = new Krypton.Toolkit.KryptonCheckButton();
            this.kryptonPictureBox1 = new Krypton.Toolkit.KryptonPictureBox();
            this.label2 = new Krypton.Toolkit.KryptonLabel();
            this.kryptonPropertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.lblOpacity = new Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            this.btnFadeIn = new Krypton.Toolkit.KryptonButton();
            this.btnFadeOut = new Krypton.Toolkit.KryptonButton();
            this.nudOpacityFraction = new System.Windows.Forms.NumericUpDown();
            this.nudSleepDelay = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOpacityFraction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSleepDelay)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.cbtnShowImage);
            this.kryptonPanel1.Controls.Add(this.kryptonPictureBox1);
            this.kryptonPanel1.Controls.Add(this.label2);
            this.kryptonPanel1.Controls.Add(this.kryptonPropertyGrid1);
            this.kryptonPanel1.Controls.Add(this.lblOpacity);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel1.Controls.Add(this.btnFadeIn);
            this.kryptonPanel1.Controls.Add(this.btnFadeOut);
            this.kryptonPanel1.Controls.Add(this.nudOpacityFraction);
            this.kryptonPanel1.Controls.Add(this.nudSleepDelay);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(1092, 676);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // cbtnShowImage
            // 
            this.cbtnShowImage.Location = new System.Drawing.Point(928, 44);
            this.cbtnShowImage.Name = "cbtnShowImage";
            this.cbtnShowImage.Size = new System.Drawing.Size(125, 25);
            this.cbtnShowImage.TabIndex = 8;
            this.cbtnShowImage.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.cbtnShowImage.Values.Text = "Show Image";
            this.cbtnShowImage.Click += new System.EventHandler(this.cbtnShowImage_Click);
            // 
            // kryptonPictureBox1
            // 
            this.kryptonPictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("kryptonPictureBox1.Image")));
            this.kryptonPictureBox1.Location = new System.Drawing.Point(365, 75);
            this.kryptonPictureBox1.Name = "kryptonPictureBox1";
            this.kryptonPictureBox1.Size = new System.Drawing.Size(715, 589);
            this.kryptonPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.kryptonPictureBox1.TabIndex = 7;
            this.kryptonPictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(734, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(188, 20);
            this.label2.TabIndex = 6;
            this.label2.Values.Text = "Opacity (In)(De)crement Fraction";
            // 
            // kryptonPropertyGrid1
            // 
            this.kryptonPropertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.kryptonPropertyGrid1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.kryptonPropertyGrid1.CategoryForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.kryptonPropertyGrid1.CommandsBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.kryptonPropertyGrid1.CommandsForeColor = System.Drawing.Color.White;
            this.kryptonPropertyGrid1.DisabledItemForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.kryptonPropertyGrid1.Font = new System.Drawing.Font("Lucida Console", 10.8F);
            this.kryptonPropertyGrid1.HelpBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.kryptonPropertyGrid1.HelpForeColor = System.Drawing.Color.White;
            this.kryptonPropertyGrid1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.kryptonPropertyGrid1.Location = new System.Drawing.Point(13, 13);
            this.kryptonPropertyGrid1.Name = "kryptonPropertyGrid1";
            this.kryptonPropertyGrid1.SelectedObject = this;
            this.kryptonPropertyGrid1.Size = new System.Drawing.Size(332, 651);
            this.kryptonPropertyGrid1.TabIndex = 0;
            this.kryptonPropertyGrid1.ViewBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.kryptonPropertyGrid1.ViewForeColor = System.Drawing.Color.White;
            // 
            // lblOpacity
            // 
            this.lblOpacity.Location = new System.Drawing.Point(928, 12);
            this.lblOpacity.Name = "lblOpacity";
            this.lblOpacity.Size = new System.Drawing.Size(6, 2);
            this.lblOpacity.TabIndex = 4;
            this.lblOpacity.Values.Text = "";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(552, 13);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(115, 20);
            this.kryptonLabel1.TabIndex = 4;
            this.kryptonLabel1.Values.Text = "Thread.Sleep Delay";
            // 
            // btnFadeIn
            // 
            this.btnFadeIn.Location = new System.Drawing.Point(365, 44);
            this.btnFadeIn.Name = "btnFadeIn";
            this.btnFadeIn.Size = new System.Drawing.Size(172, 25);
            this.btnFadeIn.TabIndex = 1;
            this.btnFadeIn.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnFadeIn.Values.Text = "Fade In";
            this.btnFadeIn.Click += new System.EventHandler(this.btnFadeIn_Click);
            // 
            // btnFadeOut
            // 
            this.btnFadeOut.Location = new System.Drawing.Point(365, 13);
            this.btnFadeOut.Name = "btnFadeOut";
            this.btnFadeOut.Size = new System.Drawing.Size(172, 25);
            this.btnFadeOut.TabIndex = 1;
            this.btnFadeOut.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnFadeOut.Values.Text = "Fade Out";
            this.btnFadeOut.Click += new System.EventHandler(this.btnFadeOut_Click);
            // 
            // nudOpacityFraction
            // 
            this.nudOpacityFraction.Location = new System.Drawing.Point(734, 49);
            this.nudOpacityFraction.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudOpacityFraction.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudOpacityFraction.Name = "nudOpacityFraction";
            this.nudOpacityFraction.Size = new System.Drawing.Size(172, 20);
            this.nudOpacityFraction.TabIndex = 3;
            this.nudOpacityFraction.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nudSleepDelay
            // 
            this.nudSleepDelay.Location = new System.Drawing.Point(552, 49);
            this.nudSleepDelay.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudSleepDelay.Name = "nudSleepDelay";
            this.nudSleepDelay.Size = new System.Drawing.Size(172, 20);
            this.nudSleepDelay.TabIndex = 2;
            this.nudSleepDelay.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // FadeFormTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1092, 676);
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "FadeFormTest";
            this.Text = "FadeFormTest";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FadeFormTest_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOpacityFraction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSleepDelay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private System.Windows.Forms.PropertyGrid kryptonPropertyGrid1;
        private KryptonButton btnFadeOut;
        private KryptonLabel label2;
        private KryptonLabel kryptonLabel1;
        private NumericUpDown nudSleepDelay;
        private NumericUpDown nudOpacityFraction;
        private KryptonButton btnFadeIn;
        private KryptonPictureBox kryptonPictureBox1;
        private KryptonCheckButton cbtnShowImage;
        private KryptonLabel lblOpacity;
    }
}
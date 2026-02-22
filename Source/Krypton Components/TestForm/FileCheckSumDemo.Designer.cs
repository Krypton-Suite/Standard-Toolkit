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
    partial class FileCheckSumDemo
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kbtnVerifyFileCheckSum = new Krypton.Toolkit.KryptonButton();
            this.kbtnComputeFileCheckSum = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            //
            // kryptonPanel1
            //
            this.kryptonPanel1.Controls.Add(this.kbtnVerifyFileCheckSum);
            this.kryptonPanel1.Controls.Add(this.kbtnComputeFileCheckSum);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(334, 101);
            this.kryptonPanel1.TabIndex = 0;
            //
            // kbtnVerifyFileCheckSum
            //
            this.kbtnVerifyFileCheckSum.Location = new System.Drawing.Point(12, 43);
            this.kbtnVerifyFileCheckSum.Name = "kbtnVerifyFileCheckSum";
            this.kbtnVerifyFileCheckSum.Size = new System.Drawing.Size(310, 35);
            this.kbtnVerifyFileCheckSum.TabIndex = 1;
            this.kbtnVerifyFileCheckSum.Values.Text = "Verify file checksum...";
            this.kbtnVerifyFileCheckSum.Click += new System.EventHandler(this.kbtnVerifyFileCheckSum_Click);
            //
            // kbtnComputeFileCheckSum
            //
            this.kbtnComputeFileCheckSum.Location = new System.Drawing.Point(12, 12);
            this.kbtnComputeFileCheckSum.Name = "kbtnComputeFileCheckSum";
            this.kbtnComputeFileCheckSum.Size = new System.Drawing.Size(310, 25);
            this.kbtnComputeFileCheckSum.TabIndex = 0;
            this.kbtnComputeFileCheckSum.Values.Text = "Compute file checksum...";
            this.kbtnComputeFileCheckSum.Click += new System.EventHandler(this.kbtnComputeFileCheckSum_Click);
            //
            // FileCheckSumDemo
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 101);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FileCheckSumDemo";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "File checksum utilities";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonButton kbtnComputeFileCheckSum;
        private Krypton.Toolkit.KryptonButton kbtnVerifyFileCheckSum;
    }
}

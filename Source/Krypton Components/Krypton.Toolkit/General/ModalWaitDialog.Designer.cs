#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    partial class ModalWaitDialog
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
            // Prevent dangling reference by remove all event references
            Application.RemoveMessageFilter(this);

            if (disposing && (components != null))
                components.Dispose();

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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.kpbModalProgress = new Krypton.Toolkit.KryptonProgressBar();
            this.kwlMessage = new Krypton.Toolkit.KryptonWrapLabel();
            this.internalKryptonLoadingCircle1 = new Krypton.Toolkit.InternalKryptonLoadingCircle();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.tableLayoutPanel1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(360, 98);
            this.kryptonPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.kwlMessage, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.kpbModalProgress, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.internalKryptonLoadingCircle1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(360, 98);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // kpbModalProgress
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.kpbModalProgress, 2);
            this.kpbModalProgress.Location = new System.Drawing.Point(3, 75);
            this.kpbModalProgress.Name = "kpbModalProgress";
            this.kpbModalProgress.Size = new System.Drawing.Size(354, 20);
            this.kpbModalProgress.StateNormal.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.kpbModalProgress.TabIndex = 12;
            this.kpbModalProgress.TextBackdropColor = System.Drawing.Color.Empty;
            this.kpbModalProgress.TextShadowColor = System.Drawing.Color.Empty;
            // 
            // kwlMessage
            // 
            this.kwlMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlMessage.Location = new System.Drawing.Point(137, 0);
            this.kwlMessage.Name = "kwlMessage";
            this.kwlMessage.Size = new System.Drawing.Size(220, 72);
            this.kwlMessage.Text = "Please wait for operation to complete.";
            this.kwlMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // internalKryptonLoadingCircle1
            // 
            this.internalKryptonLoadingCircle1.CircleValues.Color = System.Drawing.Color.Empty;
            this.internalKryptonLoadingCircle1.CircleValues.InnerCircleRadius = 8;
            this.internalKryptonLoadingCircle1.CircleValues.NumberSpoke = 24;
            this.internalKryptonLoadingCircle1.CircleValues.OuterCircleRadius = 9;
            this.internalKryptonLoadingCircle1.CircleValues.RotationSpeed = 100;
            this.internalKryptonLoadingCircle1.CircleValues.SpokeThickness = 4;
            this.internalKryptonLoadingCircle1.CircleValues.StylePreset = Krypton.Toolkit.InternalLoadingCircleStylePresets.IE7;
            this.internalKryptonLoadingCircle1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.internalKryptonLoadingCircle1.Location = new System.Drawing.Point(3, 3);
            this.internalKryptonLoadingCircle1.Name = "internalKryptonLoadingCircle1";
            this.internalKryptonLoadingCircle1.Size = new System.Drawing.Size(128, 66);
            this.internalKryptonLoadingCircle1.TabIndex = 13;
            this.internalKryptonLoadingCircle1.Text = "internalKryptonLoadingCircle1";
            // 
            // ModalWaitDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 98);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(304, 89);
            this.Name = "ModalWaitDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Processing";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private KryptonPanel kryptonPanel1;
        private KryptonProgressBar kpbModalProgress;
        private TableLayoutPanel tableLayoutPanel1;
        private KryptonWrapLabel kwlMessage;
        private InternalKryptonLoadingCircle internalKryptonLoadingCircle1;
    }
}
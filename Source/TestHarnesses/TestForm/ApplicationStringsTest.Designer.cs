#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm
{
    partial class ApplicationStringsTest
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
            this.components = new System.ComponentModel.Container();
            this.kryptonCustomStringsManager1 = new Krypton.Toolkit.Utilities.KryptonCustomStringsManager(this.components);
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kbtnResetTyped = new Krypton.Toolkit.KryptonButton();
            this.kbtnUpdateTyped = new Krypton.Toolkit.KryptonButton();
            this.kbtnReset = new Krypton.Toolkit.KryptonButton();
            this.kbtnUpdate = new Krypton.Toolkit.KryptonButton();
            this.kryptonBorderEdge1 = new Krypton.Toolkit.KryptonBorderEdge();
            this.kryptonPanel2 = new Krypton.Toolkit.KryptonPanel();
            this.kbtnTypedDemo = new Krypton.Toolkit.KryptonButton();
            this.ktxtTypedValue = new Krypton.Toolkit.KryptonTextBox();
            this.kryptonWrapLabel3 = new Krypton.Toolkit.KryptonWrapLabel();
            this.kbtnDemo = new Krypton.Toolkit.KryptonButton();
            this.ktxtValue = new Krypton.Toolkit.KryptonTextBox();
            this.kryptonWrapLabel2 = new Krypton.Toolkit.KryptonWrapLabel();
            this.kryptonWrapLabel1 = new Krypton.Toolkit.KryptonWrapLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kbtnResetTyped);
            this.kryptonPanel1.Controls.Add(this.kbtnUpdateTyped);
            this.kryptonPanel1.Controls.Add(this.kbtnReset);
            this.kryptonPanel1.Controls.Add(this.kbtnUpdate);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 286);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kryptonPanel1.Size = new System.Drawing.Size(484, 50);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kbtnResetTyped
            // 
            this.kbtnResetTyped.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnResetTyped.Location = new System.Drawing.Point(72, 13);
            this.kbtnResetTyped.Name = "kbtnResetTyped";
            this.kbtnResetTyped.Size = new System.Drawing.Size(90, 25);
            this.kbtnResetTyped.TabIndex = 3;
            this.kbtnResetTyped.Values.Text = "Reset Typed";
            this.kbtnResetTyped.Click += new System.EventHandler(this.kbtnResetTyped_Click);
            // 
            // kbtnUpdateTyped
            // 
            this.kbtnUpdateTyped.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnUpdateTyped.Location = new System.Drawing.Point(168, 13);
            this.kbtnUpdateTyped.Name = "kbtnUpdateTyped";
            this.kbtnUpdateTyped.Size = new System.Drawing.Size(100, 25);
            this.kbtnUpdateTyped.TabIndex = 2;
            this.kbtnUpdateTyped.Values.Text = "Update Typed";
            this.kbtnUpdateTyped.Click += new System.EventHandler(this.kbtnUpdateTyped_Click);
            // 
            // kbtnReset
            // 
            this.kbtnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnReset.Location = new System.Drawing.Point(276, 13);
            this.kbtnReset.Name = "kbtnReset";
            this.kbtnReset.Size = new System.Drawing.Size(90, 25);
            this.kbtnReset.TabIndex = 1;
            this.kbtnReset.Values.Text = "Reset Dict";
            this.kbtnReset.Click += new System.EventHandler(this.kbtnReset_Click);
            // 
            // kbtnUpdate
            // 
            this.kbtnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnUpdate.Location = new System.Drawing.Point(372, 13);
            this.kbtnUpdate.Name = "kbtnUpdate";
            this.kbtnUpdate.Size = new System.Drawing.Size(100, 25);
            this.kbtnUpdate.TabIndex = 0;
            this.kbtnUpdate.Values.Text = "Update Dict";
            this.kbtnUpdate.Click += new System.EventHandler(this.kbtnUpdate_Click);
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(0, 285);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(484, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.kbtnTypedDemo);
            this.kryptonPanel2.Controls.Add(this.ktxtTypedValue);
            this.kryptonPanel2.Controls.Add(this.kryptonWrapLabel3);
            this.kryptonPanel2.Controls.Add(this.kbtnDemo);
            this.kryptonPanel2.Controls.Add(this.ktxtValue);
            this.kryptonPanel2.Controls.Add(this.kryptonWrapLabel2);
            this.kryptonPanel2.Controls.Add(this.kryptonWrapLabel1);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(484, 285);
            this.kryptonPanel2.TabIndex = 1;
            // 
            // kbtnTypedDemo
            // 
            this.kbtnTypedDemo.Location = new System.Drawing.Point(13, 243);
            this.kbtnTypedDemo.Name = "kbtnTypedDemo";
            this.kbtnTypedDemo.Size = new System.Drawing.Size(120, 25);
            this.kbtnTypedDemo.TabIndex = 6;
            this.kbtnTypedDemo.Values.Text = "SaveDraft";
            // 
            // ktxtTypedValue
            // 
            this.ktxtTypedValue.Location = new System.Drawing.Point(13, 206);
            this.ktxtTypedValue.Name = "ktxtTypedValue";
            this.ktxtTypedValue.Size = new System.Drawing.Size(459, 23);
            this.ktxtTypedValue.TabIndex = 5;
            // 
            // kryptonWrapLabel3
            // 
            this.kryptonWrapLabel3.AutoSize = false;
            this.kryptonWrapLabel3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.kryptonWrapLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.kryptonWrapLabel3.Location = new System.Drawing.Point(13, 163);
            this.kryptonWrapLabel3.Name = "kryptonWrapLabel3";
            this.kryptonWrapLabel3.Size = new System.Drawing.Size(459, 37);
            this.kryptonWrapLabel3.Text = "Phase 2: register a strongly-typed string set with KryptonCustomStrings.RegisterStringSet and retrieve it with GetStringSet<T>().";
            // 
            // kbtnDemo
            // 
            this.kbtnDemo.Location = new System.Drawing.Point(13, 118);
            this.kbtnDemo.Name = "kbtnDemo";
            this.kbtnDemo.Size = new System.Drawing.Size(120, 25);
            this.kbtnDemo.TabIndex = 2;
            this.kbtnDemo.Values.Text = "SaveDraft";
            // 
            // ktxtValue
            // 
            this.ktxtValue.Location = new System.Drawing.Point(13, 81);
            this.ktxtValue.Name = "ktxtValue";
            this.ktxtValue.Size = new System.Drawing.Size(459, 23);
            this.ktxtValue.TabIndex = 1;
            // 
            // kryptonWrapLabel2
            // 
            this.kryptonWrapLabel2.AutoSize = false;
            this.kryptonWrapLabel2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.kryptonWrapLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.kryptonWrapLabel2.Location = new System.Drawing.Point(13, 56);
            this.kryptonWrapLabel2.Name = "kryptonWrapLabel2";
            this.kryptonWrapLabel2.Size = new System.Drawing.Size(459, 19);
            this.kryptonWrapLabel2.Text = "Phase 1: key/value strings via KryptonCustomStringsManager or KryptonCustomStrings.Values.";
            // 
            // kryptonWrapLabel1
            // 
            this.kryptonWrapLabel1.AutoSize = false;
            this.kryptonWrapLabel1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.kryptonWrapLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.kryptonWrapLabel1.Location = new System.Drawing.Point(13, 13);
            this.kryptonWrapLabel1.Name = "kryptonWrapLabel1";
            this.kryptonWrapLabel1.Size = new System.Drawing.Size(459, 37);
            this.kryptonWrapLabel1.Text = "Issue #3757: drop KryptonCustomStringsManager on a form and edit CustomStrings in the designer, or use KryptonCustomStrings at runtime.";
            // 
            // ApplicationStringsTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 336);
            this.Controls.Add(this.kryptonPanel2);
            this.Controls.Add(this.kryptonBorderEdge1);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ApplicationStringsTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Custom Strings Test";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            this.kryptonPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonButton kbtnUpdate;
        private Krypton.Toolkit.KryptonButton kbtnReset;
        private Krypton.Toolkit.KryptonBorderEdge kryptonBorderEdge1;
        private Krypton.Toolkit.KryptonPanel kryptonPanel2;
        private Krypton.Toolkit.KryptonWrapLabel kryptonWrapLabel1;
        private Krypton.Toolkit.KryptonTextBox ktxtValue;
        private Krypton.Toolkit.KryptonButton kbtnDemo;
        private Krypton.Toolkit.KryptonWrapLabel kryptonWrapLabel2;
        private Krypton.Toolkit.KryptonWrapLabel kryptonWrapLabel3;
        private Krypton.Toolkit.KryptonTextBox ktxtTypedValue;
        private Krypton.Toolkit.KryptonButton kbtnTypedDemo;
        private Krypton.Toolkit.KryptonButton kbtnUpdateTyped;
        private Krypton.Toolkit.KryptonButton kbtnResetTyped;
        private Krypton.Toolkit.Utilities.KryptonCustomStringsManager kryptonCustomStringsManager1;
    }
}

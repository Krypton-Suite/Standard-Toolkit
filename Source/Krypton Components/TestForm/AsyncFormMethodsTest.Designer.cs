#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm
{
    partial class AsyncFormMethodsTest
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
            this.klblResult = new Krypton.Toolkit.KryptonLabel();
            this.kbtnTaskDialogShowDialogAsync = new Krypton.Toolkit.KryptonButton();
            this.kbtnMessageBoxShowAsync = new Krypton.Toolkit.KryptonButton();
            this.kbtnShowDialogAsync = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.klblResult);
            this.kryptonPanel1.Controls.Add(this.kbtnTaskDialogShowDialogAsync);
            this.kryptonPanel1.Controls.Add(this.kbtnMessageBoxShowAsync);
            this.kryptonPanel1.Controls.Add(this.kbtnShowDialogAsync);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(420, 160);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // klblResult
            // 
            this.klblResult.Location = new System.Drawing.Point(12, 120);
            this.klblResult.Name = "klblResult";
            this.klblResult.Size = new System.Drawing.Size(52, 20);
            this.klblResult.TabIndex = 3;
            this.klblResult.Values.Text = "Result:";
            // 
            // kbtnTaskDialogShowDialogAsync
            // 
            this.kbtnTaskDialogShowDialogAsync.Location = new System.Drawing.Point(12, 74);
            this.kbtnTaskDialogShowDialogAsync.Name = "kbtnTaskDialogShowDialogAsync";
            this.kbtnTaskDialogShowDialogAsync.Size = new System.Drawing.Size(390, 30);
            this.kbtnTaskDialogShowDialogAsync.TabIndex = 2;
            this.kbtnTaskDialogShowDialogAsync.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnTaskDialogShowDialogAsync.Values.Text = "KryptonTaskDialog.ShowDialogAsync";
            this.kbtnTaskDialogShowDialogAsync.Click += new System.EventHandler(this.kbtnTaskDialogShowDialogAsync_Click);
            // 
            // kbtnMessageBoxShowAsync
            // 
            this.kbtnMessageBoxShowAsync.Location = new System.Drawing.Point(12, 43);
            this.kbtnMessageBoxShowAsync.Name = "kbtnMessageBoxShowAsync";
            this.kbtnMessageBoxShowAsync.Size = new System.Drawing.Size(390, 30);
            this.kbtnMessageBoxShowAsync.TabIndex = 1;
            this.kbtnMessageBoxShowAsync.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnMessageBoxShowAsync.Values.Text = "KryptonMessageBox.ShowAsync";
            this.kbtnMessageBoxShowAsync.Click += new System.EventHandler(this.kbtnMessageBoxShowAsync_Click);
            // 
            // kbtnShowDialogAsync
            // 
            this.kbtnShowDialogAsync.Location = new System.Drawing.Point(12, 12);
            this.kbtnShowDialogAsync.Name = "kbtnShowDialogAsync";
            this.kbtnShowDialogAsync.Size = new System.Drawing.Size(390, 30);
            this.kbtnShowDialogAsync.TabIndex = 0;
            this.kbtnShowDialogAsync.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnShowDialogAsync.Values.Text = "KryptonForm.ShowDialogAsync";
            this.kbtnShowDialogAsync.Click += new System.EventHandler(this.kbtnShowDialogAsync_Click);
            // 
            // AsyncFormMethodsTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 160);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AsyncFormMethodsTest";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Async Form Methods (#4177)";
            this.Controls.SetChildIndex(this.kryptonPanel1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonButton kbtnShowDialogAsync;
        private Krypton.Toolkit.KryptonButton kbtnMessageBoxShowAsync;
        private Krypton.Toolkit.KryptonButton kbtnTaskDialogShowDialogAsync;
        private Krypton.Toolkit.KryptonLabel klblResult;
    }
}

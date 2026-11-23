#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

partial class VisualSwitchTranslationsCultureForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null!;

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
            this.klblCulture = new Krypton.Toolkit.KryptonLabel();
            this.kcmbCulture = new Krypton.Toolkit.KryptonComboBox();
            this.klblDirectory = new Krypton.Toolkit.KryptonLabel();
            this.ktxtDirectory = new Krypton.Toolkit.KryptonTextBox();
            this.kbtnBrowse = new Krypton.Toolkit.KryptonButton();
            this.kbtnOk = new Krypton.Toolkit.KryptonButton();
            this.kbtnCancel = new Krypton.Toolkit.KryptonButton();
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonPanel2 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonBorderEdge1 = new Krypton.Toolkit.KryptonBorderEdge();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbCulture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // klblCulture
            // 
            this.klblCulture.Location = new System.Drawing.Point(12, 12);
            this.klblCulture.Name = "klblCulture";
            this.klblCulture.Size = new System.Drawing.Size(53, 20);
            this.klblCulture.TabIndex = 0;
            this.klblCulture.Values.Text = "Culture:";
            // 
            // kcmbCulture
            // 
            this.kcmbCulture.Location = new System.Drawing.Point(120, 8);
            this.kcmbCulture.Name = "kcmbCulture";
            this.kcmbCulture.Size = new System.Drawing.Size(320, 22);
            this.kcmbCulture.TabIndex = 1;
            // 
            // klblDirectory
            // 
            this.klblDirectory.Location = new System.Drawing.Point(12, 46);
            this.klblDirectory.Name = "klblDirectory";
            this.klblDirectory.Size = new System.Drawing.Size(63, 20);
            this.klblDirectory.TabIndex = 2;
            this.klblDirectory.Values.Text = "Directory:";
            // 
            // ktxtDirectory
            // 
            this.ktxtDirectory.Location = new System.Drawing.Point(120, 42);
            this.ktxtDirectory.Name = "ktxtDirectory";
            this.ktxtDirectory.Size = new System.Drawing.Size(250, 23);
            this.ktxtDirectory.TabIndex = 3;
            // 
            // kbtnBrowse
            // 
            this.kbtnBrowse.Location = new System.Drawing.Point(376, 40);
            this.kbtnBrowse.Name = "kbtnBrowse";
            this.kbtnBrowse.Size = new System.Drawing.Size(64, 25);
            this.kbtnBrowse.TabIndex = 4;
            this.kbtnBrowse.Values.Text = "Browse...";
            this.kbtnBrowse.Click += new System.EventHandler(this.kbtnBrowse_Click);
            // 
            // kbtnOk
            // 
            this.kbtnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.kbtnOk.Location = new System.Drawing.Point(292, 13);
            this.kbtnOk.Name = "kbtnOk";
            this.kbtnOk.Size = new System.Drawing.Size(75, 25);
            this.kbtnOk.TabIndex = 5;
            this.kbtnOk.Values.Text = "OK";
            // 
            // kbtnCancel
            // 
            this.kbtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.kbtnCancel.Location = new System.Drawing.Point(373, 13);
            this.kbtnCancel.Name = "kbtnCancel";
            this.kbtnCancel.Size = new System.Drawing.Size(75, 25);
            this.kbtnCancel.TabIndex = 6;
            this.kbtnCancel.Values.Text = "Cancel";
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonBorderEdge1);
            this.kryptonPanel1.Controls.Add(this.kbtnCancel);
            this.kryptonPanel1.Controls.Add(this.kbtnOk);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 75);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kryptonPanel1.Size = new System.Drawing.Size(460, 50);
            this.kryptonPanel1.TabIndex = 8;
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.klblCulture);
            this.kryptonPanel2.Controls.Add(this.kcmbCulture);
            this.kryptonPanel2.Controls.Add(this.kbtnBrowse);
            this.kryptonPanel2.Controls.Add(this.klblDirectory);
            this.kryptonPanel2.Controls.Add(this.ktxtDirectory);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(460, 75);
            this.kryptonPanel2.TabIndex = 10;
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(0, 0);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(460, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // VisualSwitchTranslationsCultureForm
            // 
            this.AcceptButton = this.kbtnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.kbtnCancel;
            this.ClientSize = new System.Drawing.Size(460, 125);
            this.Controls.Add(this.kryptonPanel2);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VisualSwitchTranslationsCultureForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Switch Translations Culture";
            ((System.ComponentModel.ISupportInitialize)(this.kcmbCulture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            this.kryptonPanel2.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion

    private Krypton.Toolkit.KryptonLabel klblCulture;
    private Krypton.Toolkit.KryptonComboBox kcmbCulture;
    private Krypton.Toolkit.KryptonLabel klblDirectory;
    private Krypton.Toolkit.KryptonTextBox ktxtDirectory;
    private Krypton.Toolkit.KryptonButton kbtnBrowse;
    private Krypton.Toolkit.KryptonButton kbtnOk;
    private Krypton.Toolkit.KryptonButton kbtnCancel;
    private KryptonPanel kryptonPanel1;
    private KryptonBorderEdge kryptonBorderEdge1;
    private KryptonPanel kryptonPanel2;
}

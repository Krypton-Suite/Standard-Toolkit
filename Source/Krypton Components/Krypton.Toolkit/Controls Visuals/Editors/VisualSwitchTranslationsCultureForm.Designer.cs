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
        ((System.ComponentModel.ISupportInitialize)(this.kcmbCulture)).BeginInit();
        this.SuspendLayout();
        // 
        // klblCulture
        // 
        this.klblCulture.Location = new System.Drawing.Point(12, 18);
        this.klblCulture.Name = "klblCulture";
        this.klblCulture.Size = new System.Drawing.Size(55, 20);
        this.klblCulture.TabIndex = 0;
        this.klblCulture.Values.Text = "Culture:";
        // 
        // kcmbCulture
        // 
        this.kcmbCulture.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
        this.kcmbCulture.Location = new System.Drawing.Point(120, 14);
        this.kcmbCulture.Name = "kcmbCulture";
        this.kcmbCulture.Size = new System.Drawing.Size(320, 22);
        this.kcmbCulture.TabIndex = 1;
        // 
        // klblDirectory
        // 
        this.klblDirectory.Location = new System.Drawing.Point(12, 52);
        this.klblDirectory.Name = "klblDirectory";
        this.klblDirectory.Size = new System.Drawing.Size(64, 20);
        this.klblDirectory.TabIndex = 2;
        this.klblDirectory.Values.Text = "Directory:";
        // 
        // ktxtDirectory
        // 
        this.ktxtDirectory.Location = new System.Drawing.Point(120, 48);
        this.ktxtDirectory.Name = "ktxtDirectory";
        this.ktxtDirectory.Size = new System.Drawing.Size(250, 23);
        this.ktxtDirectory.TabIndex = 3;
        // 
        // kbtnBrowse
        // 
        this.kbtnBrowse.Location = new System.Drawing.Point(376, 46);
        this.kbtnBrowse.Name = "kbtnBrowse";
        this.kbtnBrowse.Size = new System.Drawing.Size(64, 25);
        this.kbtnBrowse.TabIndex = 4;
        this.kbtnBrowse.Values.Text = "Browse...";
        this.kbtnBrowse.Click += new System.EventHandler(this.kbtnBrowse_Click);
        // 
        // kbtnOk
        // 
        this.kbtnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
        this.kbtnOk.Location = new System.Drawing.Point(284, 100);
        this.kbtnOk.Name = "kbtnOk";
        this.kbtnOk.Size = new System.Drawing.Size(75, 25);
        this.kbtnOk.TabIndex = 5;
        this.kbtnOk.Values.Text = "OK";
        // 
        // kbtnCancel
        // 
        this.kbtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.kbtnCancel.Location = new System.Drawing.Point(365, 100);
        this.kbtnCancel.Name = "kbtnCancel";
        this.kbtnCancel.Size = new System.Drawing.Size(75, 25);
        this.kbtnCancel.TabIndex = 6;
        this.kbtnCancel.Values.Text = "Cancel";
        // 
        // VisualSwitchTranslationsCultureForm
        // 
        this.AcceptButton = this.kbtnOk;
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.CancelButton = this.kbtnCancel;
        this.ClientSize = new System.Drawing.Size(460, 150);
        this.Controls.Add(this.kbtnCancel);
        this.Controls.Add(this.kbtnOk);
        this.Controls.Add(this.kbtnBrowse);
        this.Controls.Add(this.ktxtDirectory);
        this.Controls.Add(this.klblDirectory);
        this.Controls.Add(this.kcmbCulture);
        this.Controls.Add(this.klblCulture);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "VisualSwitchTranslationsCultureForm";
        this.ShowInTaskbar = false;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "Switch Translations Culture";
        ((System.ComponentModel.ISupportInitialize)(this.kcmbCulture)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #endregion

    private Krypton.Toolkit.KryptonLabel klblCulture;
    private Krypton.Toolkit.KryptonComboBox kcmbCulture;
    private Krypton.Toolkit.KryptonLabel klblDirectory;
    private Krypton.Toolkit.KryptonTextBox ktxtDirectory;
    private Krypton.Toolkit.KryptonButton kbtnBrowse;
    private Krypton.Toolkit.KryptonButton kbtnOk;
    private Krypton.Toolkit.KryptonButton kbtnCancel;
}

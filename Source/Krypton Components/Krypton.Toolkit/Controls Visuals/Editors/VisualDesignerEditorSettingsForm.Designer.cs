#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

partial class VisualDesignerEditorSettingsForm
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
            this.lblTheme = new Krypton.Toolkit.KryptonLabel();
            this.kcmbTheme = new Krypton.Toolkit.KryptonComboBox();
            this.lblDescription = new Krypton.Toolkit.KryptonWrapLabel();
            this.kbtnClear = new Krypton.Toolkit.KryptonButton();
            this.kbtnOpenFolder = new Krypton.Toolkit.KryptonButton();
            this.kbtnOk = new Krypton.Toolkit.KryptonButton();
            this.kbtnCancel = new Krypton.Toolkit.KryptonButton();
            this.kpnlButtons = new Krypton.Toolkit.KryptonPanel();
            this.kryptonBorderEdge1 = new Krypton.Toolkit.KryptonBorderEdge();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbTheme)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlButtons)).BeginInit();
            this.kpnlButtons.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTheme
            // 
            this.lblTheme.Location = new System.Drawing.Point(12, 12);
            this.lblTheme.Name = "lblTheme";
            this.lblTheme.Size = new System.Drawing.Size(160, 20);
            this.lblTheme.TabIndex = 0;
            this.lblTheme.Values.Text = "Theme for designer editors:";
            // 
            // kcmbTheme
            // 
            this.kcmbTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbTheme.Location = new System.Drawing.Point(12, 40);
            this.kcmbTheme.Name = "kcmbTheme";
            this.kcmbTheme.Size = new System.Drawing.Size(552, 22);
            this.kcmbTheme.TabIndex = 1;
            // 
            // lblDescription
            // 
            this.lblDescription.LabelStyle = Krypton.Toolkit.LabelStyle.AlternateControl;
            this.lblDescription.Location = new System.Drawing.Point(12, 80);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(584, 15);
            this.lblDescription.Text = "Applies only to Krypton designer dialogs. Does not change the edited control or t" +
    "he application global theme.";
            // 
            // kbtnClear
            // 
            this.kbtnClear.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.kbtnClear.Location = new System.Drawing.Point(10, 10);
            this.kbtnClear.Margin = new System.Windows.Forms.Padding(10);
            this.kbtnClear.Name = "kbtnClear";
            this.kbtnClear.Size = new System.Drawing.Size(140, 28);
            this.kbtnClear.TabIndex = 3;
            this.kbtnClear.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnClear.Values.Text = "Clear preference";
            // 
            // kbtnOpenFolder
            // 
            this.kbtnOpenFolder.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.kbtnOpenFolder.Location = new System.Drawing.Point(170, 10);
            this.kbtnOpenFolder.Margin = new System.Windows.Forms.Padding(10);
            this.kbtnOpenFolder.Name = "kbtnOpenFolder";
            this.kbtnOpenFolder.Size = new System.Drawing.Size(130, 28);
            this.kbtnOpenFolder.TabIndex = 4;
            this.kbtnOpenFolder.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnOpenFolder.Values.Text = "Open folder...";
            // 
            // kbtnOk
            // 
            this.kbtnOk.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.kbtnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.kbtnOk.Location = new System.Drawing.Point(371, 10);
            this.kbtnOk.Margin = new System.Windows.Forms.Padding(10);
            this.kbtnOk.Name = "kbtnOk";
            this.kbtnOk.Size = new System.Drawing.Size(104, 28);
            this.kbtnOk.TabIndex = 5;
            this.kbtnOk.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnOk.Values.Text = "OK";
            // 
            // kbtnCancel
            // 
            this.kbtnCancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.kbtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.kbtnCancel.Location = new System.Drawing.Point(495, 10);
            this.kbtnCancel.Margin = new System.Windows.Forms.Padding(10);
            this.kbtnCancel.Name = "kbtnCancel";
            this.kbtnCancel.Size = new System.Drawing.Size(104, 28);
            this.kbtnCancel.TabIndex = 6;
            this.kbtnCancel.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnCancel.Values.Text = "Cancel";
            // 
            // kpnlButtons
            // 
            this.kpnlButtons.Controls.Add(this.tableLayoutPanel1);
            this.kpnlButtons.Controls.Add(this.kryptonBorderEdge1);
            this.kpnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kpnlButtons.Location = new System.Drawing.Point(0, 206);
            this.kpnlButtons.Name = "kpnlButtons";
            this.kpnlButtons.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kpnlButtons.Size = new System.Drawing.Size(609, 50);
            this.kpnlButtons.TabIndex = 9;
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(0, 0);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(609, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.kbtnClear, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.kbtnOk, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.kbtnCancel, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.kbtnOpenFolder, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(609, 49);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.lblTheme);
            this.kryptonPanel1.Controls.Add(this.kcmbTheme);
            this.kryptonPanel1.Controls.Add(this.lblDescription);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(609, 206);
            this.kryptonPanel1.TabIndex = 12;
            // 
            // VisualDesignerEditorSettingsForm
            // 
            this.AcceptButton = this.kbtnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.kbtnCancel;
            this.ClientSize = new System.Drawing.Size(609, 256);
            this.Controls.Add(this.kryptonPanel1);
            this.Controls.Add(this.kpnlButtons);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VisualDesignerEditorSettingsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Designer Editor Settings";
            ((System.ComponentModel.ISupportInitialize)(this.kcmbTheme)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlButtons)).EndInit();
            this.kpnlButtons.ResumeLayout(false);
            this.kpnlButtons.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion

    private KryptonLabel lblTheme;
    private KryptonComboBox kcmbTheme;
    private KryptonWrapLabel lblDescription;
    private KryptonButton kbtnClear;
    private KryptonButton kbtnOpenFolder;
    private KryptonButton kbtnOk;
    private KryptonButton kbtnCancel;
    private KryptonPanel kpnlButtons;
    private KryptonBorderEdge kryptonBorderEdge1;
    private TableLayoutPanel tableLayoutPanel1;
    private KryptonPanel kryptonPanel1;
}

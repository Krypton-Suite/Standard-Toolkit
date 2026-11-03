#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

internal partial class VisualApplicationUpdatePromptForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
            this.kryptonPanel = new Krypton.Toolkit.KryptonPanel();
            this.table = new System.Windows.Forms.TableLayoutPanel();
            this.lblHeading = new Krypton.Toolkit.KryptonLabel();
            this.lblCurrent = new Krypton.Toolkit.KryptonLabel();
            this.lblNew = new Krypton.Toolkit.KryptonLabel();
            this.lblSeverity = new Krypton.Toolkit.KryptonLabel();
            this.lblMandatory = new Krypton.Toolkit.KryptonLabel();
            this.btnUpdate = new Krypton.Toolkit.KryptonButton();
            this.btnNotes = new Krypton.Toolkit.KryptonButton();
            this.btnRemind = new Krypton.Toolkit.KryptonButton();
            this.btnSkip = new Krypton.Toolkit.KryptonButton();
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonBorderEdge1 = new Krypton.Toolkit.KryptonBorderEdge();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).BeginInit();
            this.kryptonPanel.SuspendLayout();
            this.table.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel
            // 
            this.kryptonPanel.Controls.Add(this.table);
            this.kryptonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel.Name = "kryptonPanel";
            this.kryptonPanel.Padding = new System.Windows.Forms.Padding(12);
            this.kryptonPanel.Size = new System.Drawing.Size(650, 180);
            this.kryptonPanel.TabIndex = 0;
            // 
            // table
            // 
            this.table.BackColor = System.Drawing.Color.Transparent;
            this.table.ColumnCount = 1;
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.table.Controls.Add(this.lblHeading, 0, 0);
            this.table.Controls.Add(this.lblCurrent, 0, 1);
            this.table.Controls.Add(this.lblNew, 0, 2);
            this.table.Controls.Add(this.lblSeverity, 0, 3);
            this.table.Controls.Add(this.lblMandatory, 0, 4);
            this.table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.table.Location = new System.Drawing.Point(12, 12);
            this.table.Name = "table";
            this.table.RowCount = 5;
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.table.Size = new System.Drawing.Size(626, 156);
            this.table.TabIndex = 0;
            // 
            // lblHeading
            // 
            this.lblHeading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeading.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            this.lblHeading.Location = new System.Drawing.Point(3, 3);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(620, 29);
            this.lblHeading.TabIndex = 0;
            this.lblHeading.Values.Text = "A new version is available.";
            // 
            // lblCurrent
            // 
            this.lblCurrent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCurrent.Location = new System.Drawing.Point(3, 38);
            this.lblCurrent.Name = "lblCurrent";
            this.lblCurrent.Size = new System.Drawing.Size(620, 20);
            this.lblCurrent.TabIndex = 1;
            this.lblCurrent.Values.Text = "Current version:";
            // 
            // lblNew
            // 
            this.lblNew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNew.Location = new System.Drawing.Point(3, 64);
            this.lblNew.Name = "lblNew";
            this.lblNew.Size = new System.Drawing.Size(620, 20);
            this.lblNew.TabIndex = 2;
            this.lblNew.Values.Text = "New version:";
            // 
            // lblSeverity
            // 
            this.lblSeverity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSeverity.Location = new System.Drawing.Point(3, 90);
            this.lblSeverity.Name = "lblSeverity";
            this.lblSeverity.Size = new System.Drawing.Size(620, 20);
            this.lblSeverity.TabIndex = 3;
            this.lblSeverity.Values.Text = "Severity:";
            // 
            // lblMandatory
            // 
            this.lblMandatory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMandatory.Location = new System.Drawing.Point(3, 116);
            this.lblMandatory.Name = "lblMandatory";
            this.lblMandatory.Size = new System.Drawing.Size(620, 37);
            this.lblMandatory.TabIndex = 4;
            this.lblMandatory.Values.Text = "This update is required.";
            this.lblMandatory.Visible = false;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(32, 9);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(0, 8, 12, 0);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(120, 32);
            this.btnUpdate.TabIndex = 0;
            this.btnUpdate.Values.Text = "Update now";
            // 
            // btnNotes
            // 
            this.btnNotes.Location = new System.Drawing.Point(164, 9);
            this.btnNotes.Margin = new System.Windows.Forms.Padding(0, 8, 12, 0);
            this.btnNotes.Name = "btnNotes";
            this.btnNotes.Size = new System.Drawing.Size(140, 32);
            this.btnNotes.TabIndex = 1;
            this.btnNotes.Values.Text = "Release notes";
            // 
            // btnRemind
            // 
            this.btnRemind.Location = new System.Drawing.Point(316, 9);
            this.btnRemind.Margin = new System.Windows.Forms.Padding(0, 8, 12, 0);
            this.btnRemind.Name = "btnRemind";
            this.btnRemind.Size = new System.Drawing.Size(160, 32);
            this.btnRemind.TabIndex = 2;
            this.btnRemind.Values.Text = "Remind me later";
            // 
            // btnSkip
            // 
            this.btnSkip.Location = new System.Drawing.Point(488, 9);
            this.btnSkip.Margin = new System.Windows.Forms.Padding(0, 8, 12, 0);
            this.btnSkip.Name = "btnSkip";
            this.btnSkip.Size = new System.Drawing.Size(150, 32);
            this.btnSkip.TabIndex = 3;
            this.btnSkip.Values.Text = "Skip this version";
            this.btnSkip.Visible = false;
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.btnUpdate);
            this.kryptonPanel1.Controls.Add(this.kryptonBorderEdge1);
            this.kryptonPanel1.Controls.Add(this.btnNotes);
            this.kryptonPanel1.Controls.Add(this.btnSkip);
            this.kryptonPanel1.Controls.Add(this.btnRemind);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 180);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kryptonPanel1.Size = new System.Drawing.Size(650, 50);
            this.kryptonPanel1.TabIndex = 2;
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(0, 0);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(650, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // VisualApplicationUpdatePromptForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 230);
            this.Controls.Add(this.kryptonPanel);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VisualApplicationUpdatePromptForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Update available";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).EndInit();
            this.kryptonPanel.ResumeLayout(false);
            this.table.ResumeLayout(false);
            this.table.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.ResumeLayout(false);

    }

    private Krypton.Toolkit.KryptonPanel kryptonPanel;
    private System.Windows.Forms.TableLayoutPanel table;
    private Krypton.Toolkit.KryptonLabel lblHeading;
    private Krypton.Toolkit.KryptonLabel lblCurrent;
    private Krypton.Toolkit.KryptonLabel lblNew;
    private Krypton.Toolkit.KryptonLabel lblSeverity;
    private Krypton.Toolkit.KryptonLabel lblMandatory;
    private Krypton.Toolkit.KryptonButton btnUpdate;
    private Krypton.Toolkit.KryptonButton btnNotes;
    private Krypton.Toolkit.KryptonButton btnRemind;
    private Krypton.Toolkit.KryptonButton btnSkip;
    private KryptonPanel kryptonPanel1;
    private KryptonBorderEdge kryptonBorderEdge1;
}

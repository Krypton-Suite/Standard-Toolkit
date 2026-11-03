#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

internal partial class VisualApplicationUpdateNotesForm
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
            this.txtNotes = new Krypton.Toolkit.KryptonTextBox();
            this.buttonPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.btnUpdate = new Krypton.Toolkit.KryptonButton();
            this.btnRemind = new Krypton.Toolkit.KryptonButton();
            this.btnSkip = new Krypton.Toolkit.KryptonButton();
            this.btnClose = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).BeginInit();
            this.kryptonPanel.SuspendLayout();
            this.table.SuspendLayout();
            this.buttonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel
            // 
            this.kryptonPanel.Controls.Add(this.table);
            this.kryptonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel.Name = "kryptonPanel";
            this.kryptonPanel.Padding = new System.Windows.Forms.Padding(12);
            this.kryptonPanel.Size = new System.Drawing.Size(640, 420);
            this.kryptonPanel.TabIndex = 0;
            // 
            // table
            // 
            this.table.BackColor = System.Drawing.Color.Transparent;
            this.table.ColumnCount = 1;
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.table.Controls.Add(this.lblHeading, 0, 0);
            this.table.Controls.Add(this.lblCurrent, 0, 1);
            this.table.Controls.Add(this.txtNotes, 0, 2);
            this.table.Controls.Add(this.buttonPanel, 0, 3);
            this.table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.table.Location = new System.Drawing.Point(12, 12);
            this.table.Name = "table";
            this.table.RowCount = 4;
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.table.Size = new System.Drawing.Size(616, 396);
            this.table.TabIndex = 0;
            // 
            // lblHeading
            // 
            this.lblHeading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeading.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            this.lblHeading.Location = new System.Drawing.Point(3, 3);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(610, 29);
            this.lblHeading.TabIndex = 0;
            this.lblHeading.Values.Text = "Release notes";
            // 
            // lblCurrent
            // 
            this.lblCurrent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCurrent.Location = new System.Drawing.Point(3, 38);
            this.lblCurrent.Name = "lblCurrent";
            this.lblCurrent.Size = new System.Drawing.Size(610, 20);
            this.lblCurrent.TabIndex = 1;
            this.lblCurrent.Values.Text = "Current version:";
            // 
            // txtNotes
            // 
            this.txtNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNotes.HideSelection = true;
            this.txtNotes.Location = new System.Drawing.Point(3, 64);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.ReadOnly = true;
            this.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNotes.Size = new System.Drawing.Size(610, 274);
            this.txtNotes.TabIndex = 2;
            this.txtNotes.TabStop = false;
            // 
            // buttonPanel
            // 
            this.buttonPanel.AutoSize = false;
            this.buttonPanel.Controls.Add(this.btnUpdate);
            this.buttonPanel.Controls.Add(this.btnRemind);
            this.buttonPanel.Controls.Add(this.btnSkip);
            this.buttonPanel.Controls.Add(this.btnClose);
            this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonPanel.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.buttonPanel.Location = new System.Drawing.Point(0, 344);
            this.buttonPanel.Margin = new System.Windows.Forms.Padding(0);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Padding = new System.Windows.Forms.Padding(0);
            this.buttonPanel.Size = new System.Drawing.Size(616, 52);
            this.buttonPanel.TabIndex = 3;
            this.buttonPanel.WrapContents = false;
            // 
            // btnUpdate
            // 
            this.btnUpdate.AutoSize = false;
            this.btnUpdate.Location = new System.Drawing.Point(0, 8);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(0, 8, 12, 0);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(120, 32);
            this.btnUpdate.TabIndex = 0;
            this.btnUpdate.Values.Text = "Update now";
            this.btnUpdate.Visible = false;
            // 
            // btnRemind
            // 
            this.btnRemind.AutoSize = false;
            this.btnRemind.Location = new System.Drawing.Point(132, 8);
            this.btnRemind.Margin = new System.Windows.Forms.Padding(0, 8, 12, 0);
            this.btnRemind.Name = "btnRemind";
            this.btnRemind.Size = new System.Drawing.Size(160, 32);
            this.btnRemind.TabIndex = 1;
            this.btnRemind.Values.Text = "Remind me later";
            this.btnRemind.Visible = false;
            // 
            // btnSkip
            // 
            this.btnSkip.AutoSize = false;
            this.btnSkip.Location = new System.Drawing.Point(292, 8);
            this.btnSkip.Margin = new System.Windows.Forms.Padding(0, 8, 12, 0);
            this.btnSkip.Name = "btnSkip";
            this.btnSkip.Size = new System.Drawing.Size(150, 32);
            this.btnSkip.TabIndex = 2;
            this.btnSkip.Values.Text = "Skip this version";
            this.btnSkip.Visible = false;
            // 
            // btnClose
            // 
            this.btnClose.AutoSize = false;
            this.btnClose.Location = new System.Drawing.Point(304, 8);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0, 8, 12, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 32);
            this.btnClose.TabIndex = 2;
            this.btnClose.Values.Text = "Close";
            // 
            // VisualApplicationUpdateNotesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 420);
            this.Controls.Add(this.kryptonPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VisualApplicationUpdateNotesForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Release notes";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).EndInit();
            this.kryptonPanel.ResumeLayout(false);
            this.table.ResumeLayout(false);
            this.table.PerformLayout();
            this.buttonPanel.ResumeLayout(false);
            this.ResumeLayout(false);

    }

    private Krypton.Toolkit.KryptonPanel kryptonPanel;
    private System.Windows.Forms.TableLayoutPanel table;
    private Krypton.Toolkit.KryptonLabel lblHeading;
    private Krypton.Toolkit.KryptonLabel lblCurrent;
    private Krypton.Toolkit.KryptonTextBox txtNotes;
    private System.Windows.Forms.FlowLayoutPanel buttonPanel;
    private Krypton.Toolkit.KryptonButton btnUpdate;
    private Krypton.Toolkit.KryptonButton btnRemind;
    private Krypton.Toolkit.KryptonButton btnSkip;
    private Krypton.Toolkit.KryptonButton btnClose;
}

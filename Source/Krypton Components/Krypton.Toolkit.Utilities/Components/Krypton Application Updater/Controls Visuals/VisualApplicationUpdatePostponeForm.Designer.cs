#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

internal partial class VisualApplicationUpdatePostponeForm
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
            this.dayPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.lblPrompt = new Krypton.Toolkit.KryptonLabel();
            this.dtpRemind = new Krypton.Toolkit.KryptonDateTimePicker();
            this.buttonPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.btnRemind = new Krypton.Toolkit.KryptonButton();
            this.btnSkip = new Krypton.Toolkit.KryptonButton();
            this.btnCancel = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).BeginInit();
            this.kryptonPanel.SuspendLayout();
            this.table.SuspendLayout();
            this.dayPanel.SuspendLayout();
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
            this.kryptonPanel.Size = new System.Drawing.Size(520, 168);
            this.kryptonPanel.TabIndex = 0;
            // 
            // table
            // 
            this.table.BackColor = System.Drawing.Color.Transparent;
            this.table.ColumnCount = 1;
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.table.Controls.Add(this.dayPanel, 0, 0);
            this.table.Controls.Add(this.buttonPanel, 0, 1);
            this.table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.table.Location = new System.Drawing.Point(12, 12);
            this.table.Name = "table";
            this.table.RowCount = 2;
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.table.Size = new System.Drawing.Size(496, 144);
            this.table.TabIndex = 0;
            // 
            // dayPanel
            // 
            this.dayPanel.AutoSize = false;
            this.dayPanel.Controls.Add(this.lblPrompt);
            this.dayPanel.Controls.Add(this.dtpRemind);
            this.dayPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dayPanel.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.dayPanel.Location = new System.Drawing.Point(0, 0);
            this.dayPanel.Margin = new System.Windows.Forms.Padding(0);
            this.dayPanel.Name = "dayPanel";
            this.dayPanel.Padding = new System.Windows.Forms.Padding(0, 16, 0, 0);
            this.dayPanel.Size = new System.Drawing.Size(496, 92);
            this.dayPanel.TabIndex = 0;
            this.dayPanel.WrapContents = false;
            // 
            // lblPrompt
            // 
            this.lblPrompt.Location = new System.Drawing.Point(0, 16);
            this.lblPrompt.Margin = new System.Windows.Forms.Padding(0, 4, 12, 0);
            this.lblPrompt.Name = "lblPrompt";
            this.lblPrompt.Size = new System.Drawing.Size(88, 20);
            this.lblPrompt.TabIndex = 0;
            this.lblPrompt.Values.Text = "Remind me on";
            // 
            // dtpRemind
            // 
            this.dtpRemind.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpRemind.Location = new System.Drawing.Point(108, 16);
            this.dtpRemind.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.dtpRemind.Name = "dtpRemind";
            this.dtpRemind.Size = new System.Drawing.Size(200, 26);
            this.dtpRemind.TabIndex = 1;
            // 
            // buttonPanel
            // 
            this.buttonPanel.AutoSize = false;
            this.buttonPanel.Controls.Add(this.btnRemind);
            this.buttonPanel.Controls.Add(this.btnSkip);
            this.buttonPanel.Controls.Add(this.btnCancel);
            this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonPanel.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.buttonPanel.Location = new System.Drawing.Point(0, 92);
            this.buttonPanel.Margin = new System.Windows.Forms.Padding(0);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Padding = new System.Windows.Forms.Padding(0);
            this.buttonPanel.Size = new System.Drawing.Size(496, 52);
            this.buttonPanel.TabIndex = 1;
            this.buttonPanel.WrapContents = false;
            // 
            // btnRemind
            // 
            this.btnRemind.AutoSize = false;
            this.btnRemind.Location = new System.Drawing.Point(0, 8);
            this.btnRemind.Margin = new System.Windows.Forms.Padding(0, 8, 12, 0);
            this.btnRemind.Name = "btnRemind";
            this.btnRemind.Size = new System.Drawing.Size(160, 32);
            this.btnRemind.TabIndex = 0;
            this.btnRemind.Values.Text = "Remind me later";
            // 
            // btnSkip
            // 
            this.btnSkip.AutoSize = false;
            this.btnSkip.Location = new System.Drawing.Point(172, 8);
            this.btnSkip.Margin = new System.Windows.Forms.Padding(0, 8, 12, 0);
            this.btnSkip.Name = "btnSkip";
            this.btnSkip.Size = new System.Drawing.Size(150, 32);
            this.btnSkip.TabIndex = 1;
            this.btnSkip.Values.Text = "Skip this version";
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = false;
            this.btnCancel.Location = new System.Drawing.Point(334, 8);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0, 8, 12, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 32);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Values.Text = "Cancel";
            // 
            // VisualApplicationUpdatePostponeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 168);
            this.Controls.Add(this.kryptonPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VisualApplicationUpdatePostponeForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Remind me later";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).EndInit();
            this.kryptonPanel.ResumeLayout(false);
            this.table.ResumeLayout(false);
            this.dayPanel.ResumeLayout(false);
            this.dayPanel.PerformLayout();
            this.buttonPanel.ResumeLayout(false);
            this.ResumeLayout(false);

    }

    private Krypton.Toolkit.KryptonPanel kryptonPanel;
    private System.Windows.Forms.TableLayoutPanel table;
    private System.Windows.Forms.FlowLayoutPanel dayPanel;
    private Krypton.Toolkit.KryptonLabel lblPrompt;
    private Krypton.Toolkit.KryptonDateTimePicker dtpRemind;
    private System.Windows.Forms.FlowLayoutPanel buttonPanel;
    private Krypton.Toolkit.KryptonButton btnRemind;
    private Krypton.Toolkit.KryptonButton btnSkip;
    private Krypton.Toolkit.KryptonButton btnCancel;
}

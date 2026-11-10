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
            this.btnRemind = new Krypton.Toolkit.KryptonButton();
            this.btnSkip = new Krypton.Toolkit.KryptonButton();
            this.btnCancel = new Krypton.Toolkit.KryptonButton();
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonBorderEdge1 = new Krypton.Toolkit.KryptonBorderEdge();
            this.kryptonPanel2 = new Krypton.Toolkit.KryptonPanel();
            this.table = new System.Windows.Forms.TableLayoutPanel();
            this.dayPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.lblPrompt = new Krypton.Toolkit.KryptonLabel();
            this.dtpRemind = new Krypton.Toolkit.KryptonDateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            this.table.SuspendLayout();
            this.dayPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRemind
            // 
            this.btnRemind.Location = new System.Drawing.Point(74, 9);
            this.btnRemind.Margin = new System.Windows.Forms.Padding(0, 8, 12, 0);
            this.btnRemind.Name = "btnRemind";
            this.btnRemind.Size = new System.Drawing.Size(160, 32);
            this.btnRemind.TabIndex = 0;
            this.btnRemind.Values.Text = "Remind me later";
            // 
            // btnSkip
            // 
            this.btnSkip.Location = new System.Drawing.Point(246, 9);
            this.btnSkip.Margin = new System.Windows.Forms.Padding(0, 8, 12, 0);
            this.btnSkip.Name = "btnSkip";
            this.btnSkip.Size = new System.Drawing.Size(150, 32);
            this.btnSkip.TabIndex = 1;
            this.btnSkip.Values.Text = "Skip this version";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(408, 9);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0, 8, 12, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 32);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Values.Text = "Cancel";
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.btnRemind);
            this.kryptonPanel1.Controls.Add(this.kryptonBorderEdge1);
            this.kryptonPanel1.Controls.Add(this.btnSkip);
            this.kryptonPanel1.Controls.Add(this.btnCancel);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 118);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kryptonPanel1.Size = new System.Drawing.Size(520, 50);
            this.kryptonPanel1.TabIndex = 2;
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(0, 0);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(520, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.table);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(520, 118);
            this.kryptonPanel2.TabIndex = 3;
            // 
            // table
            // 
            this.table.BackColor = System.Drawing.Color.Transparent;
            this.table.ColumnCount = 1;
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.table.Controls.Add(this.dayPanel, 0, 0);
            this.table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.table.Location = new System.Drawing.Point(0, 0);
            this.table.Name = "table";
            this.table.RowCount = 1;
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.table.Size = new System.Drawing.Size(520, 118);
            this.table.TabIndex = 1;
            // 
            // dayPanel
            // 
            this.dayPanel.Controls.Add(this.lblPrompt);
            this.dayPanel.Controls.Add(this.dtpRemind);
            this.dayPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dayPanel.Location = new System.Drawing.Point(0, 0);
            this.dayPanel.Margin = new System.Windows.Forms.Padding(0);
            this.dayPanel.Name = "dayPanel";
            this.dayPanel.Padding = new System.Windows.Forms.Padding(0, 16, 0, 0);
            this.dayPanel.Size = new System.Drawing.Size(520, 118);
            this.dayPanel.TabIndex = 0;
            this.dayPanel.WrapContents = false;
            // 
            // lblPrompt
            // 
            this.lblPrompt.Location = new System.Drawing.Point(0, 20);
            this.lblPrompt.Margin = new System.Windows.Forms.Padding(0, 4, 12, 0);
            this.lblPrompt.Name = "lblPrompt";
            this.lblPrompt.Size = new System.Drawing.Size(90, 20);
            this.lblPrompt.TabIndex = 0;
            this.lblPrompt.Values.Text = "Remind me on";
            // 
            // dtpRemind
            // 
            this.dtpRemind.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpRemind.Location = new System.Drawing.Point(102, 16);
            this.dtpRemind.Margin = new System.Windows.Forms.Padding(0);
            this.dtpRemind.Name = "dtpRemind";
            this.dtpRemind.Size = new System.Drawing.Size(200, 21);
            this.dtpRemind.TabIndex = 1;
            // 
            // VisualApplicationUpdatePostponeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 168);
            this.Controls.Add(this.kryptonPanel2);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VisualApplicationUpdatePostponeForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Remind me later";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            this.table.ResumeLayout(false);
            this.dayPanel.ResumeLayout(false);
            this.dayPanel.PerformLayout();
            this.ResumeLayout(false);

    }
    private Krypton.Toolkit.KryptonButton btnRemind;
    private Krypton.Toolkit.KryptonButton btnSkip;
    private Krypton.Toolkit.KryptonButton btnCancel;
    private KryptonPanel kryptonPanel1;
    private KryptonBorderEdge kryptonBorderEdge1;
    private KryptonPanel kryptonPanel2;
    private TableLayoutPanel table;
    private FlowLayoutPanel dayPanel;
    private KryptonLabel lblPrompt;
    private KryptonDateTimePicker dtpRemind;
}

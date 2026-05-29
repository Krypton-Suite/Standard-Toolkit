#region BSD License
/*
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2017 - 2026. All rights reserved.
 */
#endregion

namespace TestForm;

partial class RibbonMdiDemo
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
        this.toolStrip = new System.Windows.Forms.ToolStrip();
        this.btnAddResizable = new System.Windows.Forms.ToolStripButton();
        this.btnAddNoResize = new System.Windows.Forms.ToolStripButton();
        this.btnOpenMaximized = new System.Windows.Forms.ToolStripButton();
        this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
        this.btnCloseAll = new System.Windows.Forms.ToolStripButton();
        this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
        this.btnTileHorizontal = new System.Windows.Forms.ToolStripButton();
        this.btnTileVertical = new System.Windows.Forms.ToolStripButton();
        this.btnCascade = new System.Windows.Forms.ToolStripButton();
        this.statusStrip = new System.Windows.Forms.StatusStrip();
        this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
        this.toolStrip.SuspendLayout();
        this.statusStrip.SuspendLayout();
        this.SuspendLayout();
        //
        // toolStrip
        //
        this.toolStrip.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddResizable,
            this.btnAddNoResize,
            this.btnOpenMaximized,
            this.toolStripSeparator1,
            this.btnCloseAll,
            this.toolStripSeparator2,
            this.btnTileHorizontal,
            this.btnTileVertical,
            this.btnCascade});
        this.toolStrip.Dock = System.Windows.Forms.DockStyle.Top;
        this.toolStrip.Location = new System.Drawing.Point(0, 0);
        this.toolStrip.Name = "toolStrip";
        this.toolStrip.Size = new System.Drawing.Size(884, 25);
        this.toolStrip.TabIndex = 0;
        this.toolStrip.Text = "toolStrip";
        //
        // btnAddResizable
        //
        this.btnAddResizable.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
        this.btnAddResizable.Name = "btnAddResizable";
        this.btnAddResizable.Size = new System.Drawing.Size(120, 22);
        this.btnAddResizable.Text = "Add Resizable Child";
        this.btnAddResizable.Click += this.BtnAddResizable_Click;
        //
        // btnAddNoResize
        //
        this.btnAddNoResize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
        this.btnAddNoResize.Name = "btnAddNoResize";
        this.btnAddNoResize.Size = new System.Drawing.Size(115, 22);
        this.btnAddNoResize.Text = "Add No-Resize Child";
        this.btnAddNoResize.Click += this.BtnAddNoResize_Click;
        //
        // btnOpenMaximized
        //
        this.btnOpenMaximized.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
        this.btnOpenMaximized.Name = "btnOpenMaximized";
        this.btnOpenMaximized.Size = new System.Drawing.Size(100, 22);
        this.btnOpenMaximized.Text = "Open Maximized";
        this.btnOpenMaximized.Click += this.BtnOpenMaximized_Click;
        //
        // toolStripSeparator1
        //
        this.toolStripSeparator1.Name = "toolStripSeparator1";
        this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
        //
        // btnCloseAll
        //
        this.btnCloseAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
        this.btnCloseAll.Name = "btnCloseAll";
        this.btnCloseAll.Size = new System.Drawing.Size(63, 22);
        this.btnCloseAll.Text = "Close All";
        this.btnCloseAll.Click += this.BtnCloseAll_Click;
        //
        // toolStripSeparator2
        //
        this.toolStripSeparator2.Name = "toolStripSeparator2";
        this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
        //
        // btnTileHorizontal
        //
        this.btnTileHorizontal.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
        this.btnTileHorizontal.Name = "btnTileHorizontal";
        this.btnTileHorizontal.Size = new System.Drawing.Size(96, 22);
        this.btnTileHorizontal.Text = "Tile Horizontally";
        this.btnTileHorizontal.Click += this.BtnTileHorizontal_Click;
        //
        // btnTileVertical
        //
        this.btnTileVertical.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
        this.btnTileVertical.Name = "btnTileVertical";
        this.btnTileVertical.Size = new System.Drawing.Size(79, 22);
        this.btnTileVertical.Text = "Tile Vertically";
        this.btnTileVertical.Click += this.BtnTileVertical_Click;
        //
        // btnCascade
        //
        this.btnCascade.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
        this.btnCascade.Name = "btnCascade";
        this.btnCascade.Size = new System.Drawing.Size(55, 22);
        this.btnCascade.Text = "Cascade";
        this.btnCascade.Click += this.BtnCascade_Click;
        //
        // statusStrip
        //
        this.statusStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
        this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.lblStatus });
        this.statusStrip.Location = new System.Drawing.Point(0, 439);
        this.statusStrip.Name = "statusStrip";
        this.statusStrip.Size = new System.Drawing.Size(884, 22);
        this.statusStrip.TabIndex = 1;
        this.statusStrip.Text = "statusStrip";
        //
        // lblStatus
        //
        this.lblStatus.Name = "lblStatus";
        this.lblStatus.Size = new System.Drawing.Size(669, 17);
        this.lblStatus.Spring = true;
        this.lblStatus.Text = "Issue #2921: Verify single ribbon (no double tabs), close/min/max/QAT click areas aligned with buttons, no double tabs after closing all MDI children.";
        this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        //
        // RibbonMdiDemo
        //
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(884, 461);
        this.Controls.Add(this.toolStrip);
        this.Controls.Add(this.statusStrip);
        this.IsMdiContainer = true;
        this.Name = "RibbonMdiDemo";
        this.Text = "Ribbon MDI Demo (Issue #2921) - Double Ribbon, Caption Hit-Test, QAT";
        this.toolStrip.ResumeLayout(false);
        this.toolStrip.PerformLayout();
        this.statusStrip.ResumeLayout(false);
        this.statusStrip.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #endregion

    private System.Windows.Forms.ToolStrip toolStrip;
    private System.Windows.Forms.StatusStrip statusStrip;
    private System.Windows.Forms.ToolStripStatusLabel lblStatus;
    private System.Windows.Forms.ToolStripButton btnAddResizable;
    private System.Windows.Forms.ToolStripButton btnAddNoResize;
    private System.Windows.Forms.ToolStripButton btnOpenMaximized;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripButton btnCloseAll;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripButton btnTileHorizontal;
    private System.Windows.Forms.ToolStripButton btnTileVertical;
    private System.Windows.Forms.ToolStripButton btnCascade;
}

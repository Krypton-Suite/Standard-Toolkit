#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Navigator.Utilities;
using Krypton.Workspace;

namespace TestForm;

partial class DocumentGroupsDemo
{
    private System.ComponentModel.IContainer components = null!;

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
        components = new Container();
        kryptonPanel1 = new KryptonPanel();
        klblInstructions = new KryptonLabel();
        klblStatus = new KryptonLabel();
        kryptonPanel2 = new KryptonPanel();
        btnAddPage = new KryptonButton();
        btnSplitHorizontal = new KryptonButton();
        btnSplitVertical = new KryptonButton();
        btnMovePage = new KryptonButton();
        btnCloseEmpty = new KryptonButton();
        btnSaveLayout = new KryptonButton();
        btnLoadLayout = new KryptonButton();
        kryptonWorkspace1 = new KryptonWorkspace();
        kryptonNavigatorFormIntegrator1 = new KryptonNavigatorFormIntegrator(components);
        ((ISupportInitialize)kryptonPanel1).BeginInit();
        kryptonPanel1.SuspendLayout();
        ((ISupportInitialize)kryptonPanel2).BeginInit();
        kryptonPanel2.SuspendLayout();
        ((ISupportInitialize)kryptonWorkspace1).BeginInit();
        SuspendLayout();
        //
        // kryptonPanel1
        //
        kryptonPanel1.Controls.Add(klblStatus);
        kryptonPanel1.Controls.Add(klblInstructions);
        kryptonPanel1.Dock = DockStyle.Bottom;
        kryptonPanel1.Location = new Point(0, 451);
        kryptonPanel1.Name = "kryptonPanel1";
        kryptonPanel1.Padding = new Padding(8);
        kryptonPanel1.Size = new Size(900, 72);
        kryptonPanel1.TabIndex = 2;
        //
        // klblInstructions
        //
        klblInstructions.Dock = DockStyle.Top;
        klblInstructions.Location = new Point(8, 8);
        klblInstructions.Name = "klblInstructions";
        klblInstructions.Size = new Size(884, 20);
        klblInstructions.TabIndex = 0;
        klblInstructions.Values.Text = "IDE document groups: each workspace cell is a tab group. CaptionIntegrated hosts one caption strip per cell. Split / Move use KryptonDocumentGroupHelper.";
        //
        // klblStatus
        //
        klblStatus.Dock = DockStyle.Fill;
        klblStatus.Location = new Point(8, 28);
        klblStatus.Name = "klblStatus";
        klblStatus.Size = new Size(884, 36);
        klblStatus.TabIndex = 1;
        klblStatus.Values.Text = "Status";
        //
        // kryptonPanel2
        //
        kryptonPanel2.Controls.Add(btnLoadLayout);
        kryptonPanel2.Controls.Add(btnSaveLayout);
        kryptonPanel2.Controls.Add(btnCloseEmpty);
        kryptonPanel2.Controls.Add(btnMovePage);
        kryptonPanel2.Controls.Add(btnSplitVertical);
        kryptonPanel2.Controls.Add(btnSplitHorizontal);
        kryptonPanel2.Controls.Add(btnAddPage);
        kryptonPanel2.Dock = DockStyle.Top;
        kryptonPanel2.Location = new Point(0, 0);
        kryptonPanel2.Name = "kryptonPanel2";
        kryptonPanel2.Padding = new Padding(8);
        kryptonPanel2.Size = new Size(900, 48);
        kryptonPanel2.TabIndex = 0;
        //
        // btnAddPage
        //
        btnAddPage.Location = new Point(11, 10);
        btnAddPage.Name = "btnAddPage";
        btnAddPage.Size = new Size(100, 28);
        btnAddPage.TabIndex = 0;
        btnAddPage.Values.Text = "Add page";
        btnAddPage.Click += BtnAddPage_Click;
        //
        // btnSplitHorizontal
        //
        btnSplitHorizontal.Location = new Point(120, 10);
        btnSplitHorizontal.Name = "btnSplitHorizontal";
        btnSplitHorizontal.Size = new Size(120, 28);
        btnSplitHorizontal.TabIndex = 1;
        btnSplitHorizontal.Values.Text = "Split side-by-side";
        btnSplitHorizontal.Click += BtnSplitHorizontal_Click;
        //
        // btnSplitVertical
        //
        btnSplitVertical.Location = new Point(250, 10);
        btnSplitVertical.Name = "btnSplitVertical";
        btnSplitVertical.Size = new Size(120, 28);
        btnSplitVertical.TabIndex = 2;
        btnSplitVertical.Values.Text = "Split stacked";
        btnSplitVertical.Click += BtnSplitVertical_Click;
        //
        // btnMovePage
        //
        btnMovePage.Location = new Point(380, 10);
        btnMovePage.Name = "btnMovePage";
        btnMovePage.Size = new Size(140, 28);
        btnMovePage.TabIndex = 3;
        btnMovePage.Values.Text = "Move page to new cell";
        btnMovePage.Click += BtnMovePage_Click;
        //
        // btnCloseEmpty
        //
        btnCloseEmpty.Location = new Point(530, 10);
        btnCloseEmpty.Name = "btnCloseEmpty";
        btnCloseEmpty.Size = new Size(120, 28);
        btnCloseEmpty.TabIndex = 4;
        btnCloseEmpty.Values.Text = "Close empty cells";
        btnCloseEmpty.Click += BtnCloseEmpty_Click;
        //
        // btnSaveLayout
        //
        btnSaveLayout.Location = new Point(660, 10);
        btnSaveLayout.Name = "btnSaveLayout";
        btnSaveLayout.Size = new Size(100, 28);
        btnSaveLayout.TabIndex = 5;
        btnSaveLayout.Values.Text = "Save layout";
        btnSaveLayout.Click += BtnSaveLayout_Click;
        //
        // btnLoadLayout
        //
        btnLoadLayout.Location = new Point(770, 10);
        btnLoadLayout.Name = "btnLoadLayout";
        btnLoadLayout.Size = new Size(100, 28);
        btnLoadLayout.TabIndex = 6;
        btnLoadLayout.Values.Text = "Load layout";
        btnLoadLayout.Click += BtnLoadLayout_Click;
        //
        // kryptonWorkspace1
        //
        kryptonWorkspace1.Dock = DockStyle.Fill;
        kryptonWorkspace1.Location = new Point(0, 48);
        kryptonWorkspace1.Name = "kryptonWorkspace1";
        kryptonWorkspace1.Size = new Size(900, 403);
        kryptonWorkspace1.TabIndex = 1;
        //
        // DocumentGroupsDemo
        //
        AutoScaleDimensions = new SizeF(6F, 13F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(900, 523);
        Controls.Add(kryptonWorkspace1);
        Controls.Add(kryptonPanel2);
        Controls.Add(kryptonPanel1);
        MinimumSize = new Size(720, 400);
        Name = "DocumentGroupsDemo";
        Text = "Document Groups (Workspace + Multi-strip Caption)";
        ((ISupportInitialize)kryptonPanel1).EndInit();
        kryptonPanel1.ResumeLayout(false);
        kryptonPanel1.PerformLayout();
        ((ISupportInitialize)kryptonPanel2).EndInit();
        kryptonPanel2.ResumeLayout(false);
        ((ISupportInitialize)kryptonWorkspace1).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private KryptonPanel kryptonPanel1;
    private KryptonPanel kryptonPanel2;
    private KryptonLabel klblInstructions;
    private KryptonLabel klblStatus;
    private KryptonButton btnAddPage;
    private KryptonButton btnSplitHorizontal;
    private KryptonButton btnSplitVertical;
    private KryptonButton btnMovePage;
    private KryptonButton btnCloseEmpty;
    private KryptonButton btnSaveLayout;
    private KryptonButton btnLoadLayout;
    private KryptonWorkspace kryptonWorkspace1;
    private KryptonNavigatorFormIntegrator kryptonNavigatorFormIntegrator1;
}

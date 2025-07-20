#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

using Krypton.Navigator;
using Krypton.Workspace;

namespace Krypton.Toolkit.Suite.Core.Standard.Toolkit.TestForm;

public partial class WorkspaceTest : KryptonForm
{
    public WorkspaceTest()
    {
        InitializeComponent();
        InitializeWorkspace();
    }

    private void InitializeWorkspace()
    {
        // Create a workspace with multiple cells for testing RTL layout
        var workspace = new KryptonWorkspace();
        workspace.Dock = DockStyle.Fill;
        
        // Create a root sequence with horizontal orientation
        var rootSequence = new KryptonWorkspaceSequence(Orientation.Horizontal);
        
        // Create multiple cells with different content
        var cell1 = new KryptonWorkspaceCell();
        var cell2 = new KryptonWorkspaceCell();
        var cell3 = new KryptonWorkspaceCell();
        
        // Add pages to cells
        var page1 = new KryptonPage { Text = "Cell 1 - Left", TextTitle = "Left Cell" };
        var page2 = new KryptonPage { Text = "Cell 2 - Center", TextTitle = "Center Cell" };
        var page3 = new KryptonPage { Text = "Cell 3 - Right", TextTitle = "Right Cell" };
        
        cell1.Pages.Add(page1);
        cell2.Pages.Add(page2);
        cell3.Pages.Add(page3);
        
        // Add cells to the sequence
        rootSequence.Children!.Add(cell1);
        rootSequence.Children.Add(cell2);
        rootSequence.Children.Add(cell3);
        
        // Set the root sequence to define the structure of the workspace
        workspace.Root.Children?.Add(rootSequence);
        
        // Add workspace to the form
        Controls.Add(workspace);
        
        // Store reference to workspace for RTL testing
        _workspace = workspace;
    }

    private KryptonWorkspace? _workspace;

    private void btnToggleRtl_Click(object sender, EventArgs e)
    {
        // Toggle RTL mode
        RightToLeft = RightToLeft == RightToLeft.Yes ? RightToLeft.No : RightToLeft.Yes;
        RightToLeftLayout = !RightToLeftLayout;
        
        // Update workspace RTL settings
        if (_workspace != null)
        {
            _workspace.RightToLeft = RightToLeft;
        }
        
        // Update button text
        btnToggleRtl.Text = $"RTL: {(RightToLeft == RightToLeft.Yes ? "ON" : "OFF")}";
        
        // Update status label
        lblStatus.Text = $"RTL Mode: {(RightToLeft == RightToLeft.Yes ? "Enabled" : "Disabled")} - Test KryptonWorkspace RTL Support";
    }

    private void InitializeComponent()
    {
        this.btnToggleRtl = new KryptonButton();
        this.lblStatus = new KryptonLabel();
        this.SuspendLayout();
        // 
        // btnToggleRtl
        // 
        this.btnToggleRtl.Location = new System.Drawing.Point(12, 12);
        this.btnToggleRtl.Name = "btnToggleRtl";
        this.btnToggleRtl.Size = new System.Drawing.Size(100, 30);
        this.btnToggleRtl.TabIndex = 0;
        this.btnToggleRtl.Values.Text = "RTL: OFF";
        this.btnToggleRtl.Click += new System.EventHandler(this.btnToggleRtl_Click);
        // 
        // lblStatus
        // 
        this.lblStatus.Location = new System.Drawing.Point(118, 12);
        this.lblStatus.Name = "lblStatus";
        this.lblStatus.Size = new System.Drawing.Size(400, 30);
        this.lblStatus.TabIndex = 1;
        this.lblStatus.Values.Text = "RTL Mode: Disabled - Test KryptonWorkspace RTL Support";
        // 
        // WorkspaceTest
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 600);
        this.Controls.Add(this.lblStatus);
        this.Controls.Add(this.btnToggleRtl);
        this.Name = "WorkspaceTest";
        this.Text = "KryptonWorkspace RTL Test";
        this.RightToLeftLayout = false;
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private KryptonButton btnToggleRtl;
    private KryptonLabel lblStatus;
} 
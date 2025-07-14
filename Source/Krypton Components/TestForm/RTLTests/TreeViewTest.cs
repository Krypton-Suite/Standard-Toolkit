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

namespace Krypton.Toolkit.Suite.Core.Standard.Toolkit.TestForm;

public partial class TreeViewTest : KryptonForm
{
    private KryptonTreeView kryptonTreeView1;
    private KryptonLabel klblRtlStatus;
    private KryptonButton kbtnToggleRTL;
    private KryptonPanel kryptonPanel1;

    public TreeViewTest()
    {
        InitializeComponent();
        InitializeTreeView();
    }

    private void InitializeTreeView()
    {
        // Create sample tree structure
        var rootNode1 = new TreeNode("Root Node 1");
        rootNode1.Nodes.Add(new TreeNode("Child Node 1.1"));
        rootNode1.Nodes.Add(new TreeNode("Child Node 1.2"));
        var subNode = new TreeNode("Child Node 1.3");
        subNode.Nodes.Add(new TreeNode("Grandchild Node 1.3.1"));
        subNode.Nodes.Add(new TreeNode("Grandchild Node 1.3.2"));
        rootNode1.Nodes.Add(subNode);

        var rootNode2 = new TreeNode("Root Node 2");
        rootNode2.Nodes.Add(new TreeNode("Child Node 2.1"));
        rootNode2.Nodes.Add(new TreeNode("Child Node 2.2"));

        var rootNode3 = new TreeNode("Root Node 3");
        rootNode3.Nodes.Add(new TreeNode("Child Node 3.1"));
        rootNode3.Nodes.Add(new TreeNode("Child Node 3.2"));
        rootNode3.Nodes.Add(new TreeNode("Child Node 3.3"));

        // Add nodes to TreeView
        kryptonTreeView1.Nodes.Add(rootNode1);
        kryptonTreeView1.Nodes.Add(rootNode2);
        kryptonTreeView1.Nodes.Add(rootNode3);

        // Configure TreeView for better RTL testing
        kryptonTreeView1.ShowLines = true;
        kryptonTreeView1.ShowPlusMinus = true;
        kryptonTreeView1.FullRowSelect = true;
        kryptonTreeView1.HideSelection = false;

        // Expand the first node to show the structure
        rootNode1.Expand();
        subNode.Expand();
    }

    private void kbtnToggleRTL_Click(object sender, EventArgs e)
    {
        // Toggle RTL settings
        RightToLeft = RightToLeft == RightToLeft.Yes ? RightToLeft.No : RightToLeft.Yes;
        RightToLeftLayout = !RightToLeftLayout;

        // Update display
        UpdateRtlDisplay();
    }

    private void UpdateRtlDisplay()
    {
        // Update button text
        kbtnToggleRTL.Text = $"RTL: {(RightToLeft == RightToLeft.Yes ? "ON" : "OFF")}";
        
        // Update label to show RTL state
        klblRtlStatus.Values.Text = $"TreeView RTL Test - Mode: {(RightToLeft == RightToLeft.Yes ? "RTL" : "LTR")}";
        
        // Update form title
        Text = $"TreeView RTL Test - {(RightToLeft == RightToLeft.Yes ? "RTL" : "LTR")} Mode";
    }

    private void InitializeComponent()
    {
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonTreeView1 = new Krypton.Toolkit.KryptonTreeView();
            this.klblRtlStatus = new Krypton.Toolkit.KryptonLabel();
            this.kbtnToggleRTL = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.klblRtlStatus);
            this.kryptonPanel1.Controls.Add(this.kbtnToggleRTL);
            this.kryptonPanel1.Controls.Add(this.kryptonTreeView1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(1053, 473);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonTreeView1
            // 
            this.kryptonTreeView1.Location = new System.Drawing.Point(12, 12);
            this.kryptonTreeView1.Name = "kryptonTreeView1";
            this.kryptonTreeView1.Size = new System.Drawing.Size(361, 449);
            this.kryptonTreeView1.TabIndex = 0;
            // 
            // klblRtlStatus
            // 
            this.klblRtlStatus.Location = new System.Drawing.Point(612, 224);
            this.klblRtlStatus.Name = "klblRtlStatus";
            this.klblRtlStatus.Size = new System.Drawing.Size(88, 20);
            this.klblRtlStatus.TabIndex = 16;
            this.klblRtlStatus.Values.Text = "kryptonLabel1";
            // 
            // kbtnToggleRTL
            // 
            this.kbtnToggleRTL.Location = new System.Drawing.Point(379, 224);
            this.kbtnToggleRTL.Name = "kbtnToggleRTL";
            this.kbtnToggleRTL.Size = new System.Drawing.Size(226, 25);
            this.kbtnToggleRTL.TabIndex = 15;
            this.kbtnToggleRTL.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnToggleRTL.Values.Text = "Toggle RTL";
            this.kbtnToggleRTL.Click += new System.EventHandler(this.kbtnToggleRTL_Click);
            // 
            // TreeViewTest
            // 
            this.ClientSize = new System.Drawing.Size(1053, 473);
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "TreeViewTest";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.ResumeLayout(false);

    }
}
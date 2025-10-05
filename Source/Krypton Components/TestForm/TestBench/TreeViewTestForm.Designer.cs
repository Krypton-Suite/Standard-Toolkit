namespace TestForm
{
    partial class TreeViewTestForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Node3");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Node4");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Node5");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Node1", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Node7");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Node9");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Node8", new System.Windows.Forms.TreeNode[] {
            treeNode6});
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Node6", new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode7});
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Node2", new System.Windows.Forms.TreeNode[] {
            treeNode8});
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Node0", new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode9});
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Node1");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Node2");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Node3");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Node0", new System.Windows.Forms.TreeNode[] {
            treeNode11,
            treeNode12,
            treeNode13});
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Node5");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Node7");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Node9");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Node8", new System.Windows.Forms.TreeNode[] {
            treeNode17});
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("Node6", new System.Windows.Forms.TreeNode[] {
            treeNode16,
            treeNode18});
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("Node4", new System.Windows.Forms.TreeNode[] {
            treeNode15,
            treeNode19});
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kbtnOk = new Krypton.Toolkit.KryptonButton();
            this.kryptonBorderEdge1 = new Krypton.Toolkit.KryptonBorderEdge();
            this.kryptonPanel2 = new Krypton.Toolkit.KryptonPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.kryptonPropertyGrid1 = new Krypton.Toolkit.KryptonPropertyGrid();
            this.kryptonTreeView1 = new Krypton.Toolkit.KryptonTreeView();
            this.kryptonPropertyGrid2 = new Krypton.Toolkit.KryptonPropertyGrid();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kbtnOk);
            this.kryptonPanel1.Controls.Add(this.kryptonBorderEdge1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 537);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kryptonPanel1.Size = new System.Drawing.Size(1061, 50);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kbtnOk
            // 
            this.kbtnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.kbtnOk.Location = new System.Drawing.Point(959, 13);
            this.kbtnOk.Name = "kbtnOk";
            this.kbtnOk.Size = new System.Drawing.Size(90, 25);
            this.kbtnOk.TabIndex = 1;
            this.kbtnOk.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnOk.Values.Text = "O&K";
            this.kbtnOk.Values.UseAsADialogButton = true;
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(0, 0);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(1061, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.tableLayoutPanel1);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(1061, 537);
            this.kryptonPanel2.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.treeView1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.kryptonPropertyGrid1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.kryptonTreeView1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.kryptonPropertyGrid2, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1061, 537);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(3, 3);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "Node3";
            treeNode1.Text = "Node3";
            treeNode2.Name = "Node4";
            treeNode2.Text = "Node4";
            treeNode3.Name = "Node5";
            treeNode3.Text = "Node5";
            treeNode4.Name = "Node1";
            treeNode4.Text = "Node1";
            treeNode5.Name = "Node7";
            treeNode5.Text = "Node7";
            treeNode6.Name = "Node9";
            treeNode6.Text = "Node9";
            treeNode7.Name = "Node8";
            treeNode7.Text = "Node8";
            treeNode8.Name = "Node6";
            treeNode8.Text = "Node6";
            treeNode9.Name = "Node2";
            treeNode9.Text = "Node2";
            treeNode10.Name = "Node0";
            treeNode10.Text = "Node0";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode10});
            this.treeView1.Size = new System.Drawing.Size(150, 531);
            this.treeView1.TabIndex = 0;
            // 
            // kryptonPropertyGrid1
            // 
            this.kryptonPropertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPropertyGrid1.Location = new System.Drawing.Point(161, 5);
            this.kryptonPropertyGrid1.Margin = new System.Windows.Forms.Padding(5);
            this.kryptonPropertyGrid1.Name = "kryptonPropertyGrid1";
            this.kryptonPropertyGrid1.Padding = new System.Windows.Forms.Padding(1);
            this.kryptonPropertyGrid1.SelectedObject = this.treeView1;
            this.kryptonPropertyGrid1.Size = new System.Drawing.Size(362, 527);
            this.kryptonPropertyGrid1.TabIndex = 1;
            this.kryptonPropertyGrid1.Text = "kryptonPropertyGrid1";
            // 
            // kryptonTreeView1
            // 
            this.kryptonTreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonTreeView1.Location = new System.Drawing.Point(533, 5);
            this.kryptonTreeView1.Margin = new System.Windows.Forms.Padding(5);
            this.kryptonTreeView1.Name = "kryptonTreeView1";
            treeNode11.Name = "Node1";
            treeNode11.Text = "Node1";
            treeNode12.Name = "Node2";
            treeNode12.Text = "Node2";
            treeNode13.Name = "Node3";
            treeNode13.Text = "Node3";
            treeNode14.Name = "Node0";
            treeNode14.Text = "Node0";
            treeNode15.Name = "Node5";
            treeNode15.Text = "Node5";
            treeNode16.Name = "Node7";
            treeNode16.Text = "Node7";
            treeNode17.Name = "Node9";
            treeNode17.Text = "Node9";
            treeNode18.Name = "Node8";
            treeNode18.Text = "Node8";
            treeNode19.Name = "Node6";
            treeNode19.Text = "Node6";
            treeNode20.Name = "Node4";
            treeNode20.Text = "Node4";
            this.kryptonTreeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode14,
            treeNode20});
            this.kryptonTreeView1.Size = new System.Drawing.Size(150, 527);
            this.kryptonTreeView1.TabIndex = 2;
            // 
            // kryptonPropertyGrid2
            // 
            this.kryptonPropertyGrid2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPropertyGrid2.Location = new System.Drawing.Point(693, 5);
            this.kryptonPropertyGrid2.Margin = new System.Windows.Forms.Padding(5);
            this.kryptonPropertyGrid2.Name = "kryptonPropertyGrid2";
            this.kryptonPropertyGrid2.Padding = new System.Windows.Forms.Padding(1);
            this.kryptonPropertyGrid2.SelectedObject = this.kryptonTreeView1;
            this.kryptonPropertyGrid2.Size = new System.Drawing.Size(363, 527);
            this.kryptonPropertyGrid2.TabIndex = 3;
            this.kryptonPropertyGrid2.Text = "kryptonPropertyGrid2";
            // 
            // TreeViewTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1061, 587);
            this.Controls.Add(this.kryptonPanel2);
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "TreeViewTestForm";
            this.Text = "TreeViewTestForm";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonPanel kryptonPanel1;
        private KryptonPanel kryptonPanel2;
        private TableLayoutPanel tableLayoutPanel1;
        private TreeView treeView1;
        private KryptonPropertyGrid kryptonPropertyGrid1;
        private KryptonTreeView kryptonTreeView1;
        private KryptonPropertyGrid kryptonPropertyGrid2;
        private KryptonBorderEdge kryptonBorderEdge1;
        private KryptonButton kbtnOk;
    }
}
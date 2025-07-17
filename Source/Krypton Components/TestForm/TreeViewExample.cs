#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved. 
 *  
 */
#endregion

namespace TestForm;

public partial class TreeViewExample : KryptonForm
{
    private int _next = 1;

    private Random _random = new Random();

    public TreeViewExample()
    {
        InitializeComponent();

        buttonAppend_Click(this, EventArgs.Empty);
        buttonInsert_Click(this, EventArgs.Empty);
        buttonInsert_Click(this, EventArgs.Empty);
        buttonInsert_Click(this, EventArgs.Empty);
        ktvTest.SelectedNode = null;
        buttonAppend_Click(this, EventArgs.Empty);
        buttonInsert_Click(this, EventArgs.Empty);
        buttonInsert_Click(this, EventArgs.Empty);
        ktvTest.SelectedNode = null;
        buttonAppend_Click(this, EventArgs.Empty);
        buttonInsert_Click(this, EventArgs.Empty);
        ktvTest.SelectedNode = null;
        buttonAppend_Click(this, EventArgs.Empty);
        buttonAppend_Click(this, EventArgs.Empty);
    }

    private KryptonTreeNode CreateNewItem()
    {
        var item = new KryptonTreeNode
        {
            Text = $@"Item {_next++}",
            ImageIndex = _random.Next(imageList.Images.Count - 1)
        };
        item.SelectedImageIndex = item.ImageIndex;
        return item;
    }

    private void kbtnToggleNodeCheckBox_Click(object sender, EventArgs e)
    {
        if (ktvTest.SelectedNode is KryptonTreeNode node)
        {
            node.IsCheckBoxVisible = !node.IsCheckBoxVisible;
        }
    }

    private void ktvTest_BeforeCheck(object sender, TreeViewCancelEventArgs e)
    {
        if (ktvTest.SelectedNode is KryptonTreeNode node)
        {
            e.Cancel = !node.IsCheckBoxVisible;
        }
    }

    private void buttonAppend_Click(object sender, EventArgs e)
    {
        TreeNode node = CreateNewItem();

        ktvTest.Nodes.Add(node);

        ktvTest.SelectedNode = node;
    }

    private void buttonInsert_Click(object sender, EventArgs e)
    {
        if (ktvTest.SelectedNode != null)
        {
            ktvTest.SelectedNode.Nodes.Add(CreateNewItem());
            ktvTest.SelectedNode.Expand();
        }
        else
        {
            buttonAppend_Click(sender, EventArgs.Empty);
        }
    }

    private void buttonRemove_Click(object sender, EventArgs e)
    {
        if (ktvTest.SelectedNode != null)
        {
            if (ktvTest.SelectedNode.Parent != null)
            {
                ktvTest.SelectedNode.Parent.Nodes.Remove(ktvTest.SelectedNode);
            }
            else
            {
                ktvTest.Nodes.Remove(ktvTest.SelectedNode);
            }
        }
    }

    private void buttonClear_Click(object sender, EventArgs e)
    {
        ktvTest.Nodes.Clear();
    }

    private void kbtnClose_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void kbtnTreeViewTest_Click(object sender, EventArgs e)
    {
        var treeViewTest = new TreeViewTestForm();

        treeViewTest.ShowDialog(this);
    }
}
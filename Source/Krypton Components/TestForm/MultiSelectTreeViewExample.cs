#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

public partial class MultiSelectTreeViewExample : KryptonForm
{
    public MultiSelectTreeViewExample()
    {
        InitializeComponent();
        PopulateTree();
        UpdateSelectionLabel();
    }

    private void PopulateTree()
    {
        kmstvDemo.Nodes.Clear();

        for (var i = 1; i <= 3; i++)
        {
            var parent = new TreeNode($@"Parent {i}");
            kmstvDemo.Nodes.Add(parent);

            for (var j = 1; j <= 4; j++)
            {
                parent.Nodes.Add(new TreeNode($@"Child {i}.{j}"));
            }

            parent.Expand();
        }
    }

    private void UpdateSelectionLabel()
    {
        klblSelection.Text = kmstvDemo.SelectedNodes.Count == 0
            ? @"Selected: (none)"
            : $@"Selected ({kmstvDemo.SelectedNodes.Count}): {string.Join(@", ", kmstvDemo.SelectedNodes.Select(node => node.Text))}";
    }

    private void kmstvDemo_SelectedNodesChanged(object? sender, EventArgs e) => UpdateSelectionLabel();

    private void kbtnClearSelection_Click(object? sender, EventArgs e)
    {
        kmstvDemo.ClearSelectedNodes();
        UpdateSelectionLabel();
    }

    private void kbtnSelectAll_Click(object? sender, EventArgs e)
    {
        kmstvDemo.SelectAllVisibleNodes();
        UpdateSelectionLabel();
    }

    private void kbtnClose_Click(object? sender, EventArgs e) => Close();
}

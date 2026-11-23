#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Toolkit;
using Krypton.Toolkit.Utilities;

namespace TestForm;

/// <summary>
/// Demonstrates <see cref="KryptonTreeComboBox"/> &#8211; a ComboBox-style control whose drop-down
/// hosts a hierarchical tree. Implements feature request #3444.
/// </summary>
public sealed class KryptonTreeComboBoxDemo : KryptonForm
{
    private readonly KryptonTreeComboBox _leafCombo;
    private readonly KryptonTreeComboBox _breadcrumbCombo;
    private readonly KryptonTreeComboBox _anyNodeCombo;
    private readonly KryptonLabel _statusLabel;

    public KryptonTreeComboBoxDemo()
    {
        Text = @"KryptonTreeComboBox Demo (Issue #3444)";
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = new Size(760, 380);
        Padding = new Padding(12);

        var tlp = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 4,
            ColumnStyles =
            {
                new ColumnStyle(SizeType.Absolute, 200),
                new ColumnStyle(SizeType.Percent, 100)
            }
        };
        tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
        tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
        tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
        tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        tlp.Controls.Add(new KryptonLabel { Text = @"Leaf + full path:" }, 0, 0);
        _leafCombo = new KryptonTreeComboBox
        {
            Width = 360,
            DisplayMode = KryptonTreeComboBoxDisplayMode.FullPath,
            SelectMode = KryptonTreeComboBoxSelectMode.LeafOnly
        };
        PopulateLocationTree(_leafCombo.Nodes);
        _leafCombo.SelectedNodeChanged += OnSelectionChanged;
        tlp.Controls.Add(_leafCombo, 1, 0);

        tlp.Controls.Add(new KryptonLabel { Text = @"Breadcrumb:" }, 0, 1);
        _breadcrumbCombo = new KryptonTreeComboBox
        {
            Width = 360,
            DisplayMode = KryptonTreeComboBoxDisplayMode.Breadcrumb,
            BreadcrumbSeparator = @" › ",
            SelectMode = KryptonTreeComboBoxSelectMode.LeafOnly
        };
        PopulateLocationTree(_breadcrumbCombo.Nodes);
        _breadcrumbCombo.SelectedNodeChanged += OnSelectionChanged;
        tlp.Controls.Add(_breadcrumbCombo, 1, 1);

        tlp.Controls.Add(new KryptonLabel { Text = @"Any node (click):" }, 0, 2);
        _anyNodeCombo = new KryptonTreeComboBox
        {
            Width = 360,
            DisplayMode = KryptonTreeComboBoxDisplayMode.Breadcrumb,
            SelectMode = KryptonTreeComboBoxSelectMode.AnyNode,
            CommitOnNodeClick = true
        };
        PopulateLocationTree(_anyNodeCombo.Nodes);
        _anyNodeCombo.SelectedNodeChanged += OnSelectionChanged;
        tlp.Controls.Add(_anyNodeCombo, 1, 2);

        _statusLabel = new KryptonLabel
        {
            Text = @"Open a drop-down (F4 / Alt+Down). Double-click a leaf, press Enter, or use single-click on the third combo. Parent nodes can be picked only in the third row.",
            Dock = DockStyle.Top
        };
        var statusHost = new KryptonPanel { Dock = DockStyle.Fill, Padding = new Padding(0, 12, 0, 0) };
        statusHost.Controls.Add(_statusLabel);
        tlp.Controls.Add(statusHost, 0, 3);
        tlp.SetColumnSpan(statusHost, 2);

        Controls.Add(tlp);
    }

    private void OnSelectionChanged(object? sender, EventArgs e)
    {
        if (sender is not KryptonTreeComboBox combo)
        {
            return;
        }

        var node = combo.SelectedNode;
        string which = ReferenceEquals(combo, _leafCombo) ? "Leaf/FullPath"
                     : ReferenceEquals(combo, _breadcrumbCombo) ? "Breadcrumb"
                     : "AnyNode";
        _statusLabel.Text = node == null
            ? $@"[{which}] (no selection)"
            : $@"[{which}] Text='{combo.Text}', Node='{node.Text}', Tag={node.Tag ?? "(null)"}";
    }

    private static void PopulateLocationTree(TreeNodeCollection nodes)
    {
        nodes.Clear();
        TreeNode continents = BuildNode("Continents",
            BuildNode("Europe",
                BuildNode("United Kingdom", tag: "UK"),
                BuildNode("Germany", tag: "DE"),
                BuildNode("France", tag: "FR")),
            BuildNode("North America",
                BuildNode("USA", tag: "US"),
                BuildNode("Canada", tag: "CA"),
                BuildNode("Mexico", tag: "MX")),
            BuildNode("Asia",
                BuildNode("Japan", tag: "JP"),
                BuildNode("South Korea", tag: "KR"),
                BuildNode("India", tag: "IN")));

        nodes.Add(continents);
        continents.Expand();
        foreach (TreeNode region in continents.Nodes)
        {
            region.Expand();
        }
    }

    private static TreeNode BuildNode(string text, object? tag = null, params TreeNode[] children)
    {
        var node = new TreeNode(text) { Tag = tag };
        if (children.Length > 0)
        {
            node.Nodes.AddRange(children);
        }

        return node;
    }
}

#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Reproduces issue #4326: a freshly constructed <see cref="KryptonTreeView"/> must accept
/// <see cref="KryptonTreeView.MultiSelect"/> = <c>false</c> in the property grid.
/// </summary>
public sealed class Bug4326TreeViewMultiSelectDemo : KryptonForm
{
    private readonly KryptonWrapLabel _lblInfo;
    private readonly KryptonWrapLabel _lblStatus;
    private readonly KryptonTreeView _treeFresh;
    private readonly KryptonTreeView _treeCheckBoxes;
    private readonly TreeView _treeNative;
    private readonly KryptonPropertyGrid _propertyGrid;
    private readonly KryptonComboBox _comboTarget;

    public Bug4326TreeViewMultiSelectDemo()
    {
        Text = @"Bug #4326 - TreeView MultiSelect";
        StartPosition = FormStartPosition.CenterScreen;
        Size = new Size(980, 620);
        MinimumSize = new Size(860, 520);

        _lblInfo = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = 88,
            Text =
                @"How to test issue #4326:" + Environment.NewLine +
                @"1) Fresh KryptonTreeView starts with MultiSelect = False and CheckBoxes = False." + Environment.NewLine +
                @"2) In the property grid, set MultiSelect to True, then back to False — it must stay False." + Environment.NewLine +
                @"3) Switch to ""Krypton (CheckBoxes)"", turn CheckBoxes on, then set MultiSelect to False — it must stay False (not snap back to True)."
        };

        _lblStatus = new KryptonWrapLabel
        {
            Dock = DockStyle.Bottom,
            AutoSize = false,
            Height = 40,
            Text = @"Status"
        };

        var buttons = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            Height = 40,
            Padding = new Padding(8, 6, 8, 0),
            WrapContents = false
        };

        var btnFalse = new KryptonButton { Text = @"Set MultiSelect = False", AutoSize = true };
        btnFalse.Click += (_, _) => SetSelectedMultiSelect(false);
        var btnTrue = new KryptonButton { Text = @"Set MultiSelect = True", AutoSize = true };
        btnTrue.Click += (_, _) => SetSelectedMultiSelect(true);
        buttons.Controls.Add(btnFalse);
        buttons.Controls.Add(btnTrue);

        var treesHost = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 3,
            RowCount = 2,
            Padding = new Padding(8)
        };
        treesHost.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3f));
        treesHost.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3f));
        treesHost.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.4f));
        treesHost.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        treesHost.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));

        _treeFresh = CreateKryptonTree(@"Root A", @"Child A1", @"Child A2");
        _treeCheckBoxes = CreateKryptonTree(@"Root B", @"Child B1", @"Child B2");
        _treeCheckBoxes.CheckBoxes = true;
        _treeNative = CreateNativeTree();

        treesHost.Controls.Add(CreateCaption(@"Krypton (fresh)"), 0, 0);
        treesHost.Controls.Add(CreateCaption(@"Krypton (CheckBoxes)"), 1, 0);
        treesHost.Controls.Add(CreateCaption(@"Native TreeView"), 2, 0);
        treesHost.Controls.Add(_treeFresh, 0, 1);
        treesHost.Controls.Add(_treeCheckBoxes, 1, 1);
        treesHost.Controls.Add(_treeNative, 2, 1);

        _propertyGrid = new KryptonPropertyGrid
        {
            Dock = DockStyle.Fill,
            SelectedObject = _treeFresh
        };
        _propertyGrid.PropertyValueChanged += (_, _) => UpdateStatus();

        _comboTarget = new KryptonComboBox
        {
            Dock = DockStyle.Top,
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        _comboTarget.Items.AddRange(new object[] { @"Krypton (fresh)", @"Krypton (CheckBoxes)", @"Native TreeView" });
        _comboTarget.SelectedIndex = 0;
        _comboTarget.SelectedIndexChanged += OnTargetChanged;

        var gridHost = new KryptonPanel
        {
            Dock = DockStyle.Right,
            Width = 360
        };
        gridHost.Controls.Add(_propertyGrid);
        gridHost.Controls.Add(_comboTarget);

        var body = new KryptonPanel { Dock = DockStyle.Fill };
        body.Controls.Add(treesHost);

        Controls.Add(body);
        Controls.Add(gridHost);
        Controls.Add(buttons);
        Controls.Add(_lblInfo);
        Controls.Add(_lblStatus);

        UpdateStatus();
    }

    private void OnTargetChanged(object? sender, EventArgs e)
    {
        _propertyGrid.SelectedObject = _comboTarget.SelectedIndex switch
        {
            1 => _treeCheckBoxes,
            2 => _treeNative,
            _ => _treeFresh
        };
        UpdateStatus();
    }

    private void SetSelectedMultiSelect(bool value)
    {
        if (_propertyGrid.SelectedObject is KryptonTreeView tree)
        {
            tree.MultiSelect = value;
            _propertyGrid.Refresh();
            UpdateStatus();
        }
    }

    private void UpdateStatus()
    {
        if (_propertyGrid.SelectedObject is KryptonTreeView tree)
        {
            bool descriptorValue = GetDescriptorValue(tree, nameof(KryptonTreeView.MultiSelect));
            _lblStatus.Text =
                $@"{tree.Name}: MultiSelect={tree.MultiSelect} (descriptor={descriptorValue}), CheckBoxes={tree.CheckBoxes}";
            return;
        }

        if (_propertyGrid.SelectedObject is TreeView native)
        {
            _lblStatus.Text = $@"{native.Name}: native TreeView has no MultiSelect; CheckBoxes={native.CheckBoxes}";
        }
    }

    private static bool GetDescriptorValue(object component, string propertyName)
    {
        PropertyDescriptor? descriptor = TypeDescriptor.GetProperties(component)[propertyName];
        return descriptor?.GetValue(component) is true;
    }

    private static KryptonLabel CreateCaption(string text) =>
        new()
        {
            Text = text,
            Dock = DockStyle.Fill,
            LabelStyle = LabelStyle.BoldControl
        };

    private static KryptonTreeView CreateKryptonTree(string rootText, string child1, string child2)
    {
        var tree = new KryptonTreeView
        {
            Dock = DockStyle.Fill,
            Name = rootText.Replace(" ", string.Empty)
        };
        TreeNode root = tree.Nodes.Add(rootText);
        root.Nodes.Add(child1);
        root.Nodes.Add(child2);
        root.Expand();
        return tree;
    }

    private static TreeView CreateNativeTree()
    {
        var tree = new TreeView
        {
            Dock = DockStyle.Fill,
            Name = @"NativeTreeView"
        };
        TreeNode root = tree.Nodes.Add(@"Native root");
        root.Nodes.Add(@"Native child 1");
        root.Nodes.Add(@"Native child 2");
        root.Expand();
        return tree;
    }
}

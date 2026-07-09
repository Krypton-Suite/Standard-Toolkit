#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Krypton-themed drop-down for selecting data sources and members at design time.
/// </summary>
internal partial class KryptonDesignerDesignBindingPicker : UserControl
{
    #region Instance Fields
    private readonly bool _showDataSources;
    private readonly bool _showDataMembers;
    private readonly bool _selectListMembers;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonDesignerDesignBindingPicker"/> class.
    /// </summary>
    public KryptonDesignerDesignBindingPicker(
        ITypeDescriptorContext context,
        IServiceProvider provider,
        bool showDataSources,
        bool showDataMembers,
        bool selectListMembers,
        object? rootDataSource,
        string? currentDataMember,
        object? currentDataSource = null)
    {
        _showDataSources = showDataSources;
        _showDataMembers = showDataMembers;
        _selectListMembers = selectListMembers;

        InitializeComponent();

        if (_showDataSources)
        {
            BuildDataSourceTree(context, provider, currentDataSource);
        }
        else
        {
            BuildDataMemberTree(rootDataSource, currentDataMember);
        }
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets the selected data member path.
    /// </summary>
    public string? SelectedDataMember =>
        _treeView.SelectedNode?.Tag as string;

    /// <summary>
    /// Gets the selected data source instance.
    /// </summary>
    public object? SelectedDataSource =>
        _treeView.SelectedNode?.Tag;
    #endregion

    #region Implementation
    private void BuildDataSourceTree(ITypeDescriptorContext context, IServiceProvider provider, object? currentDataSource)
    {
        _treeView.Nodes.Clear();
        var noneNode = new TreeNode(@"(none)") { Tag = null };
        _treeView.Nodes.Add(noneNode);

        if (provider.GetService(typeof(IDesignerHost)) is IDesignerHost host)
        {
            foreach (IComponent component in host.Container.Components)
            {
                if (component.Site?.Name is not { } name || !IsSupportedDataSource(component))
                {
                    continue;
                }

                var node = new TreeNode(name) { Tag = component };
                _treeView.Nodes.Add(node);
                if (ReferenceEquals(component, currentDataSource))
                {
                    _treeView.SelectedNode = node;
                }
            }
        }

        if (_treeView.SelectedNode is null)
        {
            _treeView.SelectedNode = noneNode;
        }
    }

    private void BuildDataMemberTree(object? rootDataSource, string? currentDataMember)
    {
        _treeView.Nodes.Clear();
        var noneNode = new TreeNode(@"(none)") { Tag = string.Empty };
        _treeView.Nodes.Add(noneNode);

        if (rootDataSource is not null)
        {
            PopulateMemberNodes(_treeView.Nodes, rootDataSource, string.Empty);
        }

        SelectNodeByMemberPath(currentDataMember ?? string.Empty);
        if (_treeView.SelectedNode is null)
        {
            _treeView.SelectedNode = noneNode;
        }
    }

    private void PopulateMemberNodes(TreeNodeCollection nodes, object dataSource, string prefix)
    {
        foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(dataSource))
        {
            if (!property.IsBrowsable)
            {
                continue;
            }

            var path = string.IsNullOrEmpty(prefix) ? property.Name : $"{prefix}.{property.Name}";
            var isList = typeof(IList).IsAssignableFrom(property.PropertyType) && property.PropertyType != typeof(string);
            var isComplex = property.PropertyType.IsClass && property.PropertyType != typeof(string);

            if (_selectListMembers && isList)
            {
                nodes.Add(new TreeNode(property.Name) { Tag = path });
            }
            else if (!_selectListMembers && !isList)
            {
                var node = new TreeNode(property.Name) { Tag = path };
                nodes.Add(node);

                if (isComplex)
                {
                    try
                    {
                        var child = property.GetValue(dataSource);
                        if (child is not null)
                        {
                            PopulateMemberNodes(node.Nodes, child, path);
                        }
                    }
                    catch (Exception)
                    {
                        // Ignore design-time binding failures.
                    }
                }
            }
        }
    }

    private void SelectNodeByMemberPath(string? memberPath)
    {
        if (string.IsNullOrEmpty(memberPath))
        {
            return;
        }

        foreach (TreeNode node in _treeView.Nodes)
        {
            var match = FindNode(node, memberPath!);
            if (match is not null)
            {
                _treeView.SelectedNode = match;
                return;
            }
        }
    }

    private static TreeNode? FindNode(TreeNode node, string memberPath)
    {
        if (string.Equals(node.Tag as string, memberPath, StringComparison.Ordinal))
        {
            return node;
        }

        foreach (TreeNode child in node.Nodes)
        {
            var match = FindNode(child, memberPath);
            if (match is not null)
            {
                return match;
            }
        }

        return null;
    }

    private static bool IsSupportedDataSource(object component) =>
        component is BindingSource or IList or System.Data.DataSet or System.Data.DataTable or System.Data.DataView
            or System.Data.DataColumn;

    private void UpdateHelp(TreeNode? node)
    {
        if (node is null)
        {
            _helpLabel.Values.Text = @"Select a binding.";
            return;
        }

        _helpLabel.Values.Text = _showDataSources
            ? $@"Data source: {node.Text}"
            : $@"Data member: {node.Tag}";
    }
    #endregion
}

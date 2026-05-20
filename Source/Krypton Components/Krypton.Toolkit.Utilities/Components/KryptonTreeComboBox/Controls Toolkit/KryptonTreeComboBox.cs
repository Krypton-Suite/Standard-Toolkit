#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Provides a ComboBox-style control whose drop-down hosts a hierarchical <see cref="KryptonTreeView"/>.
/// Implements feature request
/// <a href="https://github.com/Krypton-Suite/Standard-Toolkit/issues/3444">#3444</a>.
/// </summary>
/// <remarks>
/// Built on <see cref="KryptonComboBoxUserControl"/> with a fixed tree drop-down. Supports grouped
/// (parent/child) nodes, optional check boxes, images, and several display formats for the editor
/// text (leaf text, full path, or breadcrumb).
/// </remarks>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonComboBox), "ToolboxBitmaps.KryptonComboBox.bmp")]
[DefaultEvent(nameof(SelectedNodeChanged))]
[DefaultProperty(nameof(Nodes))]
[Designer(typeof(KryptonTreeComboBoxDesigner))]
[DesignerCategory(@"code")]
[Description(@"A ComboBox-style control whose drop-down hosts a hierarchical tree view.")]
public class KryptonTreeComboBox : KryptonComboBoxUserControl
{
    #region Static Fields

    private const int DefaultDropDownWidth = 280;
    private const int DefaultDropDownHeight = 240;
    private const string DefaultPathSeparator = @"\";
    private const string DefaultBreadcrumbSeparator = @" > ";

    #endregion

    #region Instance Fields

    private readonly KryptonTreeComboBoxDropDown _dropDown;
    private TreeNode? _selectedNode;
    private KryptonTreeComboBoxDisplayMode _displayMode = KryptonTreeComboBoxDisplayMode.LeafText;
    private KryptonTreeComboBoxSelectMode _selectMode = KryptonTreeComboBoxSelectMode.LeafOnly;
    private string _pathSeparator = DefaultPathSeparator;
    private string _breadcrumbSeparator = DefaultBreadcrumbSeparator;
    private bool _commitOnNodeClick;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when the selected tree node changes (after a value is committed from the drop-down).
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when the selected tree node changes.")]
    public event EventHandler? SelectedNodeChanged;

    /// <summary>
    /// Occurs after the tree selection changes while the drop-down is open. Forwards the inner
    /// <see cref="KryptonTreeView.AfterSelect"/> event.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs after the tree selection changes while the drop-down is open.")]
    public event TreeViewEventHandler? AfterSelect;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonTreeComboBox"/> class.
    /// </summary>
    public KryptonTreeComboBox()
    {
        ReadOnlyEditor = true;
        DropDownResizable = true;
        DropDownWidth = DefaultDropDownWidth;
        DropDownHeight = DefaultDropDownHeight;

        _dropDown = new KryptonTreeComboBoxDropDown(this);
        _dropDown.TreeView.AfterSelect += OnInnerTreeAfterSelect;
        base.DropContent = _dropDown;

        ValueCommitted += OnDropDownValueCommitted;
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            ValueCommitted -= OnDropDownValueCommitted;
            _dropDown.TreeView.AfterSelect -= OnInnerTreeAfterSelect;
        }

        base.Dispose(disposing);
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets the collection of tree nodes displayed in the drop-down.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The collection of tree nodes displayed in the drop-down.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Editor(@"System.Windows.Forms.Design.TreeNodeCollectionEditor", typeof(UITypeEditor))]
    public TreeNodeCollection Nodes => _dropDown.TreeView.Nodes;

    /// <summary>
    /// Gets the hosted <see cref="KryptonTreeView"/>. Do not reparent this control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonTreeView TreeView => _dropDown.TreeView;

    /// <summary>
    /// Gets or sets the currently selected tree node.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public TreeNode? SelectedNode
    {
        get => _selectedNode;
        set
        {
            if (_selectedNode == value)
            {
                return;
            }

            _selectedNode = value;
            _dropDown.TreeView.SelectedNode = value;

            if (value == null)
            {
                Text = string.Empty;
            }
            else
            {
                Text = FormatNodeDisplay(value);
            }

            OnSelectedNodeChanged(EventArgs.Empty);
        }
    }

    /// <summary>
    /// Gets or sets how the committed node is formatted in the editor.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"How the committed node is formatted in the editor.")]
    [DefaultValue(KryptonTreeComboBoxDisplayMode.LeafText)]
    public KryptonTreeComboBoxDisplayMode DisplayMode
    {
        get => _displayMode;
        set => _displayMode = value;
    }

    /// <summary>
    /// Gets or sets which nodes may be selected from the drop-down.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Which nodes may be selected from the drop-down.")]
    [DefaultValue(KryptonTreeComboBoxSelectMode.LeafOnly)]
    public KryptonTreeComboBoxSelectMode SelectMode
    {
        get => _selectMode;
        set => _selectMode = value;
    }

    /// <summary>
    /// Gets or sets the separator used when <see cref="DisplayMode"/> is
    /// <see cref="KryptonTreeComboBoxDisplayMode.FullPath"/>.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Separator used for FullPath display mode.")]
    [DefaultValue(DefaultPathSeparator)]
    public string PathSeparator
    {
        get => _pathSeparator;
        set => _pathSeparator = value ?? DefaultPathSeparator;
    }

    /// <summary>
    /// Gets or sets the separator used when <see cref="DisplayMode"/> is
    /// <see cref="KryptonTreeComboBoxDisplayMode.Breadcrumb"/>.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Separator used for Breadcrumb display mode.")]
    [DefaultValue(DefaultBreadcrumbSeparator)]
    public string BreadcrumbSeparator
    {
        get => _breadcrumbSeparator;
        set => _breadcrumbSeparator = value ?? DefaultBreadcrumbSeparator;
    }

    /// <summary>
    /// Gets or sets a value indicating whether a single click on a selectable node commits the
    /// selection (in addition to double-click and Enter).
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"When true, a single click on a selectable node commits the selection.")]
    [DefaultValue(false)]
    public bool CommitOnNodeClick
    {
        get => _commitOnNodeClick;
        set => _commitOnNodeClick = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether check boxes are shown next to the tree nodes.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Whether check boxes are shown next to the tree nodes.")]
    [DefaultValue(false)]
    public bool CheckBoxes
    {
        get => _dropDown.TreeView.CheckBoxes;
        set => _dropDown.TreeView.CheckBoxes = value;
    }

    /// <summary>
    /// Gets or sets the <see cref="ImageList"/> used to display node images.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The ImageList used to display node images.")]
    [DefaultValue(null)]
    public ImageList? ImageList
    {
        get => _dropDown.TreeView.ImageList;
        set => _dropDown.TreeView.ImageList = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether lines are shown between sibling nodes.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Whether lines are shown between sibling nodes.")]
    [DefaultValue(true)]
    public bool ShowLines
    {
        get => _dropDown.TreeView.ShowLines;
        set => _dropDown.TreeView.ShowLines = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether lines are shown between tree nodes and the root.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Whether lines are shown between tree nodes and the root.")]
    [DefaultValue(true)]
    public bool ShowRootLines
    {
        get => _dropDown.TreeView.ShowRootLines;
        set => _dropDown.TreeView.ShowRootLines = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether plus/minus glyphs are shown next to parent nodes.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Whether plus/minus glyphs are shown next to parent nodes.")]
    [DefaultValue(true)]
    public bool ShowPlusMinus
    {
        get => _dropDown.TreeView.ShowPlusMinus;
        set => _dropDown.TreeView.ShowPlusMinus = value;
    }

    /// <summary>
    /// Hidden. The tree drop-down is fixed for this control.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Control? DropContent
    {
        get => base.DropContent;
        set => base.DropContent = value;
    }

    /// <summary>
    /// Hidden. Filter-as-you-type is not supported for tree combo boxes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(false)]
    public new bool AutoOpenOnType
    {
        get => base.AutoOpenOnType;
        set => base.AutoOpenOnType = value;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Returns a value indicating whether the specified node may be committed for the current
    /// <see cref="SelectMode"/>.
    /// </summary>
    /// <param name="node">The candidate node.</param>
    /// <returns><see langword="true"/> when the node may be selected.</returns>
    public bool CanSelectNode(TreeNode node)
    {
        if (node == null)
        {
            return false;
        }

        return _selectMode == KryptonTreeComboBoxSelectMode.AnyNode || node.Nodes.Count == 0;
    }

    /// <summary>
    /// Formats the specified node for display in the editor according to <see cref="DisplayMode"/>.
    /// </summary>
    /// <param name="node">The node to format.</param>
    /// <returns>Formatted display text.</returns>
    public string FormatNodeDisplay(TreeNode node)
    {
        switch (_displayMode)
        {
            case KryptonTreeComboBoxDisplayMode.FullPath:
                return FormatFullPath(node);
            case KryptonTreeComboBoxDisplayMode.Breadcrumb:
                return FormatBreadcrumb(node);
            default:
                return node.Text;
        }
    }

    #endregion

    #region Protected

    /// <summary>
    /// Raises the <see cref="SelectedNodeChanged"/> event.
    /// </summary>
    /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
    protected virtual void OnSelectedNodeChanged(EventArgs e) => SelectedNodeChanged?.Invoke(this, e);

    #endregion

    #region Implementation

    private void OnDropDownValueCommitted(object? sender, KryptonDropDownCommitEventArgs e)
    {
        _selectedNode = e.Value as TreeNode;
        OnSelectedNodeChanged(EventArgs.Empty);
    }

    private void OnInnerTreeAfterSelect(object? sender, TreeViewEventArgs e) => AfterSelect?.Invoke(this, e);

    private string FormatFullPath(TreeNode node)
    {
        string fullPath = node.FullPath;
        if (_pathSeparator.Length == 1 && _pathSeparator[0] == '\\')
        {
            return fullPath;
        }

        return fullPath.Replace('\\', _pathSeparator[0]);
    }

    private string FormatBreadcrumb(TreeNode node)
    {
        if (string.IsNullOrEmpty(_breadcrumbSeparator))
        {
            return node.Text;
        }

        var parts = new List<string>();
        TreeNode? current = node;
        while (current != null)
        {
            parts.Insert(0, current.Text);
            current = current.Parent;
        }

        return string.Join(_breadcrumbSeparator, parts);
    }

    #endregion
}

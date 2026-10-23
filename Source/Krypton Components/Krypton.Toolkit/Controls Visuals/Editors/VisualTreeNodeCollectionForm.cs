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
/// Krypton-themed tree-node collection editor dialog.
/// </summary>
internal partial class VisualTreeNodeCollectionForm : VisualDesignerCollectionForm
{
    #region Instance Fields
    private static readonly object NextNodeKey = new();
    private TreeNode? _selectedNode;
    private int _nextNode;
    private int _initialNextNode;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the <see cref="VisualTreeNodeCollectionForm"/> class for the WinForms designer.
    /// </summary>
    public VisualTreeNodeCollectionForm()
        : base()
    {
        InitializeComponent();
        ConfigureDesignerChrome();
    }

    /// <summary>
    /// Initialize a new instance of the <see cref="VisualTreeNodeCollectionForm"/> class.
    /// </summary>
    /// <param name="editor">Owning collection editor.</param>
    public VisualTreeNodeCollectionForm(KryptonDesignerCollectionEditor editor)
        : base(editor)
    {
        InitializeComponent();
        ConfigureDesignerChrome();

        Text = @"TreeNode Collection Editor";
        ControlBox = false;
        ApplyButtonSizes();
        ClientSize = KryptonDesignerEditorDpi.Scale(this, new Size(720, 480));
        MinimumSize = KryptonDesignerEditorDpi.Scale(this, new Size(620, 380));

        _treeView.AfterSelect += (_, e) =>
        {
            _selectedNode = e.Node;
            UpdatePropertyGrid();
            UpdateButtons();
        };
    }
    #endregion

    #region Implementation
    private void ConfigureDesignerChrome()
    {
        InternalDesignerEditorFormChrome.Apply(this, kpnlContent, kpnlButtonBar);
        kpnlButtonBar.OkButton.Values.Text = KryptonManager.Strings.GeneralStrings.OK;
        kpnlButtonBar.CancelButton.Values.Text = KryptonManager.Strings.GeneralStrings.Cancel;
        kpnlButtonBar.OkButton.Click += OnOkClick;
    }

    private void ApplyButtonSizes()
    {
        var buttonSize = KryptonDesignerEditorDpi.Scale(this, new Size(112, 28));
        foreach (var button in new[]
                 {
                     _buttonAddRoot, _buttonAddChild, _buttonDelete, _buttonMoveUp, _buttonMoveDown
                 })
        {
            button.AutoSize = false;
            button.Size = buttonSize;
            button.MinimumSize = buttonSize;
        }
    }

    /// <inheritdoc />
    protected override void OnEditValueChanged()
    {
        if (EditValue is null || Items is null)
        {
            return;
        }

        _nextNode = ReadNextNode();
        _initialNextNode = _nextNode;
        _treeView.Nodes.Clear();

        foreach (TreeNode item in Items)
        {
            _treeView.Nodes.Add(CloneNode(item));
        }

        _propertyGrid.Site = new KryptonDesignerPropertyGridSite(Context, _propertyGrid);
        ApplyOwnerPaletteFromContext();
        CopyImageListsFromDesignTreeView();

        if (_treeView.Nodes.Count > 0)
        {
            _treeView.SelectedNode = _treeView.Nodes[0];
        }

        UpdateButtons();
        UpdatePropertyGrid();
        _treeView.Focus();
    }

    private void CopyImageListsFromDesignTreeView()
    {
        if (Context?.Instance is not TreeView designTreeView)
        {
            return;
        }

        _treeView.ImageList = designTreeView.ImageList;
        _treeView.ImageIndex = designTreeView.ImageIndex;
        _treeView.SelectedImageIndex = designTreeView.SelectedImageIndex;
        _treeView.StateImageList = designTreeView.StateImageList;
        _treeView.CheckBoxes = designTreeView.CheckBoxes;
    }

    private TreeNode CloneNode(TreeNode source)
    {
        var clone = (TreeNode)source.Clone();
        foreach (TreeNode child in source.Nodes)
        {
            clone.Nodes.Add(CloneNode(child));
        }

        return clone;
    }

    private void AddNode(TreeNode? parent)
    {
        var name = $"Node{_nextNode++}";
        var node = new TreeNode(name) { Name = name, Text = name };
        if (parent is null)
        {
            _treeView.Nodes.Add(node);
        }
        else
        {
            parent.Expand();
            parent.Nodes.Add(node);
        }

        _treeView.SelectedNode = node;
        WriteNextNode();
        UpdateButtons();
    }

    private void DeleteSelectedNode()
    {
        if (_selectedNode is null)
        {
            return;
        }

        var node = _selectedNode;
        _selectedNode = null;
        node.Remove();
        UpdatePropertyGrid();
        UpdateButtons();
    }

    private void MoveSelectedNode(bool up)
    {
        if (_selectedNode?.Parent is TreeNode parent)
        {
            var index = parent.Nodes.IndexOf(_selectedNode);
            var newIndex = up ? index - 1 : index + 1;
            if (newIndex >= 0 && newIndex < parent.Nodes.Count)
            {
                parent.Nodes.RemoveAt(index);
                parent.Nodes.Insert(newIndex, _selectedNode);
            }
        }
        else if (_selectedNode is not null)
        {
            var index = _treeView.Nodes.IndexOf(_selectedNode);
            var newIndex = up ? index - 1 : index + 1;
            if (newIndex >= 0 && newIndex < _treeView.Nodes.Count)
            {
                _treeView.Nodes.RemoveAt(index);
                _treeView.Nodes.Insert(newIndex, _selectedNode);
            }
        }

        _treeView.SelectedNode = _selectedNode;
    }

    private void UpdatePropertyGrid()
    {
        if (_selectedNode is not null)
        {
            _propertiesLabel.Values.Text = $@"{(_selectedNode.Name)} properties";
            _propertyGrid.SelectedObject = _selectedNode;
        }
        else
        {
            _propertiesLabel.Values.Text = @"Properties:";
            _propertyGrid.SelectedObject = null;
        }
    }

    private void UpdateButtons()
    {
        var hasSelection = _selectedNode is not null;
        _buttonAddChild.Enabled = hasSelection;
        _buttonDelete.Enabled = hasSelection;
        _buttonMoveUp.Enabled = hasSelection && _selectedNode!.Index > 0;
        _buttonMoveDown.Enabled = hasSelection && _selectedNode!.NextNode is not null;
    }

    private void OnOkClick(object? sender, EventArgs e)
    {
        var roots = new object[_treeView.Nodes.Count];
        for (var i = 0; i < roots.Length; i++)
        {
            roots[i] = CloneNode(_treeView.Nodes[i]);
        }

        Items = roots;
        CommitDesignerItems();
        WriteNextNode();
        Context?.OnComponentChanged();
    }

    private int ReadNextNode()
    {
        if (Context?.Instance is not TreeView treeView || treeView.Site is null)
        {
            return 0;
        }

        if (treeView.Site.GetService(typeof(IDictionaryService)) is IDictionaryService dictionary)
        {
            return dictionary.GetValue(NextNodeKey) is int value ? value : 0;
        }

        return 0;
    }

    private void WriteNextNode()
    {
        if (Context?.Instance is not TreeView treeView || treeView.Site is null)
        {
            return;
        }

        if (treeView.Site.GetService(typeof(IDictionaryService)) is IDictionaryService dictionary)
        {
            dictionary.SetValue(NextNodeKey, _nextNode);
        }
    }
    #endregion
}

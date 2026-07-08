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
/// Krypton-themed designer editor for <see cref="TreeNodeCollection"/>.
/// </summary>
public sealed class KryptonDesignerTreeNodeCollectionEditor : KryptonDesignerCollectionEditor
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonDesignerTreeNodeCollectionEditor"/> class.
    /// </summary>
    public KryptonDesignerTreeNodeCollectionEditor()
        : base(typeof(TreeNode))
    {
    }
    #endregion

    #region Protected
    /// <inheritdoc />
    protected override KryptonDesignerCollectionForm CreateKryptonDesignerCollectionForm() =>
        new KryptonDesignerTreeNodeCollectionForm(this);
    #endregion
}

/// <summary>
/// Krypton-themed tree-node collection editor dialog.
/// </summary>
internal sealed class KryptonDesignerTreeNodeCollectionForm : KryptonDesignerCollectionForm
{
    #region Instance Fields
    private static readonly object NextNodeKey = new();
    private readonly KryptonTreeView _treeView;
    private readonly KryptonPropertyGrid _propertyGrid;
    private readonly KryptonLabel _propertiesLabel;
    private readonly KryptonButton _buttonAddRoot;
    private readonly KryptonButton _buttonAddChild;
    private readonly KryptonButton _buttonDelete;
    private readonly KryptonButton _buttonMoveUp;
    private readonly KryptonButton _buttonMoveDown;
    private readonly KryptonButton _buttonOk;
    private readonly KryptonButton _buttonCancel;
    private TreeNode? _selectedNode;
    private int _nextNode;
    private int _initialNextNode;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonDesignerTreeNodeCollectionForm"/> class.
    /// </summary>
    /// <param name="editor">Owning collection editor.</param>
    public KryptonDesignerTreeNodeCollectionForm(KryptonDesignerCollectionEditor editor)
        : base(editor)
    {
        Text = @"TreeNode Collection Editor";
        ControlBox = false;
        ClientSize = KryptonDesignerEditorDpi.Scale(this, new Size(720, 480));
        MinimumSize = KryptonDesignerEditorDpi.Scale(this, new Size(620, 380));

        _treeView = new KryptonTreeView { Dock = DockStyle.Fill, HideSelection = false, AllowDrop = true };
        _propertyGrid = new KryptonPropertyGrid { Dock = DockStyle.Fill };
        _propertiesLabel = new KryptonLabel { AutoSize = true, Values = { Text = @"Properties:" } };
        _buttonAddRoot = CreateButton(@"Add Root", (_, _) => AddNode(null));
        _buttonAddChild = CreateButton(@"Add Child", (_, _) => AddNode(_selectedNode));
        _buttonDelete = CreateButton(@"Delete", (_, _) => DeleteSelectedNode());
        _buttonMoveUp = CreateButton(@"Move Up", (_, _) => MoveSelectedNode(up: true));
        _buttonMoveDown = CreateButton(@"Move Down", (_, _) => MoveSelectedNode(up: false));
        _buttonOk = CreateButton(KryptonManager.Strings.GeneralStrings.OK, OnOkClick);
        _buttonOk.DialogResult = DialogResult.OK;
        _buttonCancel = CreateButton(KryptonManager.Strings.GeneralStrings.Cancel, null);
        _buttonCancel.DialogResult = DialogResult.Cancel;

        InitializeLayout();

        _treeView.AfterSelect += (_, e) =>
        {
            _selectedNode = e.Node;
            UpdatePropertyGrid();
            UpdateButtons();
        };

        AcceptButton = _buttonOk;
        CancelButton = _buttonCancel;
    }
    #endregion

    #region Implementation
    private static KryptonButton CreateButton(string text, EventHandler? click)
    {
        var button = new KryptonButton
        {
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            Values = { Text = text }
        };
        if (click is not null)
        {
            button.Click += click;
        }

        return button;
    }

    private void InitializeLayout()
    {
        var membersLabel = new KryptonLabel { AutoSize = true, Values = { Text = @"Members:" } };
        var membersPanel = new KryptonPanel { Dock = DockStyle.Fill };
        membersPanel.Controls.Add(_treeView);
        membersPanel.Controls.Add(membersLabel);
        membersLabel.Dock = DockStyle.Top;

        var propertiesPanel = new KryptonPanel { Dock = DockStyle.Fill };
        propertiesPanel.Controls.Add(_propertyGrid);
        propertiesPanel.Controls.Add(_propertiesLabel);
        _propertiesLabel.Dock = DockStyle.Top;

        var navPanel = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink
        };
        navPanel.Controls.Add(_buttonAddRoot);
        navPanel.Controls.Add(_buttonAddChild);
        navPanel.Controls.Add(_buttonDelete);
        navPanel.Controls.Add(_buttonMoveUp);
        navPanel.Controls.Add(_buttonMoveDown);

        var okCancelPanel = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.RightToLeft
        };
        okCancelPanel.Controls.Add(_buttonCancel);
        okCancelPanel.Controls.Add(_buttonOk);

        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 3,
            Padding = new Padding(KryptonDesignerEditorDpi.Scale(this, 9)),
            RowCount = 3
        };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        layout.RowStyles.Add(new RowStyle());
        layout.RowStyles.Add(new RowStyle());
        layout.Controls.Add(membersPanel, 0, 0);
        layout.Controls.Add(navPanel, 1, 0);
        layout.Controls.Add(propertiesPanel, 2, 0);
        layout.Controls.Add(okCancelPanel, 2, 2);
        layout.SetRowSpan(membersPanel, 2);
        layout.SetRowSpan(navPanel, 2);
        layout.SetRowSpan(propertiesPanel, 2);

        Controls.Add(layout);
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

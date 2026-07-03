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
/// Provides a Krypton-styled tree view with extended multi-selection support.
/// Implements feature request
/// <a href="https://github.com/Krypton-Suite/Standard-Toolkit/issues/3837">#3837</a>.
/// </summary>
/// <remarks>
/// Supports Ctrl+click toggle, Shift+click range selection, rubber-band drag selection,
/// optional check-box selection, and a <see cref="SelectedNodes"/> collection.
/// </remarks>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonTreeView), "ToolboxBitmaps.KryptonTreeView.bmp")]
[DefaultEvent(nameof(SelectedNodesChanged))]
[DefaultProperty(nameof(Nodes))]
[Designer(typeof(KryptonMultiSelectTreeViewDesigner))]
[DesignerCategory(@"code")]
[Description(@"Displays a hierarchical tree with Krypton styling and extended multi-selection.")]
[Docking(DockingBehavior.Ask)]
public class KryptonMultiSelectTreeView : KryptonTreeView
{
    #region Static Fields

    private const int RubberBandDragThreshold = 4;

    #endregion

    #region Instance Fields

    private readonly HashSet<TreeNode> _selectedNodeSet = new();
    private readonly List<TreeNode> _selectedNodesList = new();
    private readonly ReadOnlyCollection<TreeNode> _selectedNodes;
    private TreeNode? _selectionAnchor;
    private bool _suppressSelectionHandling;
    private bool _syncingCheckState;
    private bool _rubberBandActive;
    private bool _rubberBandPending;
    private Point _mouseDownPoint;
    private Point _rubberBandStart;
    private Point _rubberBandEnd;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when the <see cref="SelectedNodes"/> collection changes.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when the SelectedNodes collection changes.")]
    public event EventHandler? SelectedNodesChanged;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonMultiSelectTreeView"/> class.
    /// </summary>
    public KryptonMultiSelectTreeView()
    {
        _selectedNodes = new ReadOnlyCollection<TreeNode>(_selectedNodesList);

        FullRowSelect = true;

        BeforeSelect += OnMultiSelectBeforeSelect;
        AfterCheck += OnMultiSelectAfterCheck;
        NodeMouseClick += OnMultiSelectNodeMouseClick;

        TreeView.MouseDown += OnTreeViewMouseDown;
        TreeView.MouseMove += OnTreeViewMouseMove;
        TreeView.MouseUp += OnTreeViewMouseUp;
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets the currently selected nodes.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ReadOnlyCollection<TreeNode> SelectedNodes => _selectedNodes;

    /// <summary>
    /// Gets or sets a value indicating whether rubber-band drag selection is enabled.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether click-drag rubber-band selection is enabled.")]
    [DefaultValue(true)]
    public bool EnableRubberBandSelection { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether check-box changes update <see cref="SelectedNodes"/>.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether checking or unchecking a node updates SelectedNodes.")]
    [DefaultValue(true)]
    public bool SyncCheckBoxesToSelection { get; set; } = true;

    /// <summary>
    /// Hides the base <see cref="KryptonTreeView.MultiSelect"/> checkbox-toggle behavior.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new bool MultiSelect
    {
        get => false;
        set
        {
            // Intentionally ignored; this control manages its own selection model.
        }
    }

    /// <summary>
    /// Replaces the current selection with the supplied nodes.
    /// </summary>
    /// <param name="nodes">Nodes to select.</param>
    public void SetSelectedNodes(IEnumerable<TreeNode> nodes)
    {
        if (nodes is null)
        {
            throw new ArgumentNullException(nameof(nodes));
        }

        ApplySelection(nodes, extend: false, focusNode: null);
    }

    /// <summary>
    /// Clears the current selection.
    /// </summary>
    public void ClearSelectedNodes() => ApplySelection(Array.Empty<TreeNode>(), extend: false, focusNode: null);

    /// <summary>
    /// Selects all visible nodes.
    /// </summary>
    public void SelectAllVisibleNodes()
    {
        ApplySelection(TreeViewMultiSelectHelper.EnumerateVisibleNodes(Nodes), extend: false, focusNode: SelectedNode);
    }

    #endregion

    #region Protected

    /// <inheritdoc />
    protected override bool IsNodeMultiSelected(TreeNode node) => _selectedNodeSet.Contains(node);

    /// <inheritdoc />
    protected override void OnPaint(PaintEventArgs? e)
    {
        base.OnPaint(e);
        if (e is not null)
        {
            DrawRubberBandOverlay(e.Graphics);
        }
    }

    /// <summary>
    /// Raises the <see cref="SelectedNodesChanged"/> event.
    /// </summary>
    /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
    protected virtual void OnSelectedNodesChanged(EventArgs e) => SelectedNodesChanged?.Invoke(this, e);

    #endregion

    #region Implementation

    private void OnMultiSelectBeforeSelect(object? sender, TreeViewCancelEventArgs e)
    {
        if (_suppressSelectionHandling || _rubberBandActive)
        {
            e.Cancel = true;
            return;
        }

        if (_rubberBandPending)
        {
            e.Cancel = true;
            return;
        }

        if (e.Node is null)
        {
            return;
        }

        var modifiers = Control.ModifierKeys;

        if ((modifiers & Keys.Control) == Keys.Control)
        {
            e.Cancel = true;
            ToggleNodeSelection(e.Node);
            FocusSelectedNode(e.Node);
            _selectionAnchor = e.Node;
            return;
        }

        if ((modifiers & Keys.Shift) == Keys.Shift)
        {
            TreeNode? anchor = _selectionAnchor ?? SelectedNode;
            if (anchor is not null)
            {
                e.Cancel = true;
                SelectRange(anchor, e.Node);
                FocusSelectedNode(e.Node);
                _selectionAnchor = anchor;
                return;
            }
        }

        ApplySelection(new[] { e.Node }, extend: false, focusNode: e.Node);
        _selectionAnchor = e.Node;
    }

    private void OnMultiSelectNodeMouseClick(object? sender, TreeNodeMouseClickEventArgs e)
    {
        if (_rubberBandActive || _rubberBandPending)
        {
            return;
        }

        if (e.Button == MouseButtons.Left)
        {
            _selectionAnchor = e.Node;
        }
    }

    private void OnMultiSelectAfterCheck(object? sender, TreeViewEventArgs e)
    {
        if (_syncingCheckState || !SyncCheckBoxesToSelection || e.Node is null)
        {
            return;
        }

        if (e.Node.Checked)
        {
            AddNodeToSelection(e.Node, extend: true);
        }
        else
        {
            RemoveNodeFromSelection(e.Node);
        }
    }

    private void OnTreeViewMouseDown(object? sender, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Left)
        {
            return;
        }

        var hit = TreeView.HitTest(e.Location);
        if (hit.Location == TreeViewHitTestLocations.PlusMinus
            || hit.Location == TreeViewHitTestLocations.StateImage)
        {
            return;
        }

        if (EnableRubberBandSelection)
        {
            var modifiers = Control.ModifierKeys;
            if ((modifiers & (Keys.Control | Keys.Shift)) == Keys.None)
            {
                _mouseDownPoint = e.Location;
                _rubberBandPending = true;
                _rubberBandStart = e.Location;
                _rubberBandEnd = e.Location;
            }
        }
    }

    private void OnTreeViewMouseMove(object? sender, MouseEventArgs e)
    {
        if (!_rubberBandPending && !_rubberBandActive)
        {
            return;
        }

        if ((Control.MouseButtons & MouseButtons.Left) != MouseButtons.Left)
        {
            return;
        }

        if (_rubberBandPending)
        {
            var dx = Math.Abs(e.X - _mouseDownPoint.X);
            var dy = Math.Abs(e.Y - _mouseDownPoint.Y);
            if (dx < RubberBandDragThreshold && dy < RubberBandDragThreshold)
            {
                return;
            }

            _rubberBandPending = false;
            _rubberBandActive = true;
            CaptureRubberBandMouse();
        }

        _rubberBandEnd = e.Location;
        TreeView.Invalidate();
        Invalidate();
    }

    private void OnTreeViewMouseUp(object? sender, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Left)
        {
            return;
        }

        if (_rubberBandActive)
        {
            CompleteRubberBandSelection();
        }
        else if (_rubberBandPending)
        {
            ApplyDeferredClickSelection(e.Location);
        }

        _rubberBandPending = false;
        _rubberBandActive = false;
        ReleaseRubberBandMouse();
        TreeView.Invalidate();
        Invalidate();
    }

    private void ApplyDeferredClickSelection(Point location)
    {
        TreeViewHitTestInfo hit = TreeView.HitTest(location);
        if (hit.Node is null
            || hit.Location == TreeViewHitTestLocations.PlusMinus
            || hit.Location == TreeViewHitTestLocations.StateImage)
        {
            return;
        }

        var modifiers = Control.ModifierKeys;

        if ((modifiers & Keys.Control) == Keys.Control)
        {
            ToggleNodeSelection(hit.Node);
            FocusSelectedNode(hit.Node);
            _selectionAnchor = hit.Node;
            return;
        }

        if ((modifiers & Keys.Shift) == Keys.Shift)
        {
            TreeNode? anchor = _selectionAnchor ?? SelectedNode;
            if (anchor is not null)
            {
                SelectRange(anchor, hit.Node);
                FocusSelectedNode(hit.Node);
                return;
            }
        }

        ApplySelection(new[] { hit.Node }, extend: false, focusNode: hit.Node);
        _selectionAnchor = hit.Node;
    }

    private void DrawRubberBandOverlay(Graphics graphics)
    {
        if (!_rubberBandActive)
        {
            return;
        }

        Point start = PointToClient(TreeView.PointToScreen(_rubberBandStart));
        Point end = PointToClient(TreeView.PointToScreen(_rubberBandEnd));
        Rectangle band = TreeViewMultiSelectHelper.NormalizeRectangle(start, end);
        if (band.Width <= 0 && band.Height <= 0)
        {
            return;
        }

        Color fillColor = Color.FromArgb(48, SystemColors.Highlight);
        using var fillBrush = new SolidBrush(fillColor);
        using var borderPen = new Pen(SystemColors.Highlight, 1f) { DashStyle = DashStyle.Dot };
        graphics.FillRectangle(fillBrush, band);
        graphics.DrawRectangle(borderPen, band);
    }

    private void CompleteRubberBandSelection()
    {
        Rectangle band = TreeViewMultiSelectHelper.NormalizeRectangle(_rubberBandStart, _rubberBandEnd);
        if (band.Width <= 0 && band.Height <= 0)
        {
            return;
        }

        var hits = new List<TreeNode>();
        foreach (TreeNode node in TreeViewMultiSelectHelper.EnumerateVisibleNodes(Nodes))
        {
            Rectangle bounds = TreeViewMultiSelectHelper.GetNodeBounds(TreeView, node);
            if (!bounds.IsEmpty && bounds.IntersectsWith(band))
            {
                hits.Add(node);
            }
        }

        bool extend = (Control.ModifierKeys & Keys.Control) == Keys.Control;
        TreeNode? focusNode = hits.Count > 0 ? hits[hits.Count - 1] : null;
        ApplySelection(hits, extend, focusNode);
        _selectionAnchor = focusNode;
    }

    private void ToggleNodeSelection(TreeNode node)
    {
        if (_selectedNodeSet.Contains(node))
        {
            RemoveNodeFromSelection(node);
        }
        else
        {
            AddNodeToSelection(node, extend: true);
        }
    }

    private void SelectRange(TreeNode anchor, TreeNode end)
    {
        var visible = TreeViewMultiSelectHelper.EnumerateVisibleNodes(Nodes).ToList();
        var startIndex = visible.IndexOf(anchor);
        var endIndex = visible.IndexOf(end);

        if (startIndex < 0 || endIndex < 0)
        {
            ApplySelection(new[] { end }, extend: false, focusNode: end);
            return;
        }

        if (startIndex > endIndex)
        {
            (startIndex, endIndex) = (endIndex, startIndex);
        }

        var range = visible.GetRange(startIndex, endIndex - startIndex + 1);
        ApplySelection(range, extend: false, focusNode: end);
    }

    private void ApplySelection(IEnumerable<TreeNode> nodes, bool extend, TreeNode? focusNode)
    {
        PruneInvalidNodes();

        var incoming = nodes.Where(node => node is not null).ToList();
        HashSet<TreeNode> next;

        if (extend)
        {
            next = new HashSet<TreeNode>(_selectedNodeSet);
            foreach (TreeNode node in incoming)
            {
                next.Add(node);
            }
        }
        else
        {
            next = new HashSet<TreeNode>(incoming);
        }

        if (next.SetEquals(_selectedNodeSet))
        {
            if (focusNode is not null && !ReferenceEquals(SelectedNode, focusNode))
            {
                FocusSelectedNode(focusNode);
            }

            SyncCheckStatesFromSelection();
            return;
        }

        _selectedNodeSet.Clear();
        foreach (TreeNode node in next)
        {
            _selectedNodeSet.Add(node);
        }

        _selectedNodesList.Clear();
        foreach (TreeNode node in TreeViewMultiSelectHelper.EnumerateVisibleNodes(Nodes))
        {
            if (_selectedNodeSet.Contains(node))
            {
                _selectedNodesList.Add(node);
            }
        }

        foreach (TreeNode node in _selectedNodeSet)
        {
            if (!_selectedNodesList.Contains(node))
            {
                _selectedNodesList.Add(node);
            }
        }

        if (focusNode is not null)
        {
            FocusSelectedNode(focusNode);
            _selectionAnchor ??= focusNode;
        }

        SyncCheckStatesFromSelection();
        PerformNeedPaint(true);

        OnSelectedNodesChanged(EventArgs.Empty);
    }

    private void AddNodeToSelection(TreeNode node, bool extend)
    {
        if (extend)
        {
            if (_selectedNodeSet.Contains(node))
            {
                return;
            }

            ApplySelection(new[] { node }, extend: true, focusNode: node);
            return;
        }

        ApplySelection(new[] { node }, extend: false, focusNode: node);
    }

    private void RemoveNodeFromSelection(TreeNode node)
    {
        if (!_selectedNodeSet.Contains(node))
        {
            return;
        }

        var remaining = _selectedNodesList.Where(existing => !ReferenceEquals(existing, node));
        TreeNode? focusNode = ReferenceEquals(SelectedNode, node)
            ? remaining.FirstOrDefault()
            : SelectedNode;
        ApplySelection(remaining, extend: false, focusNode: focusNode);
    }

    private void FocusSelectedNode(TreeNode node)
    {
        if (ReferenceEquals(SelectedNode, node))
        {
            TreeView.Invalidate();
            return;
        }

        try
        {
            _suppressSelectionHandling = true;
            SelectedNode = node;
        }
        finally
        {
            _suppressSelectionHandling = false;
        }

        TreeView.Invalidate();
    }

    private void SyncCheckStatesFromSelection()
    {
        if (!CheckBoxes || !SyncCheckBoxesToSelection)
        {
            return;
        }

        TreeView.BeginUpdate();
        try
        {
            _syncingCheckState = true;
            foreach (TreeNode node in TreeViewMultiSelectHelper.EnumerateAllNodes(Nodes))
            {
                bool shouldBeChecked = _selectedNodeSet.Contains(node);
                if (node.Checked != shouldBeChecked)
                {
                    node.Checked = shouldBeChecked;
                }
            }
        }
        finally
        {
            _syncingCheckState = false;
            TreeView.EndUpdate();
        }

        TreeView.Invalidate();
    }

    private void PruneInvalidNodes()
    {
        if (_selectedNodeSet.Count == 0)
        {
            return;
        }

        var invalid = _selectedNodeSet.Where(node => node.TreeView is null).ToList();
        if (invalid.Count == 0)
        {
            return;
        }

        foreach (TreeNode node in invalid)
        {
            _selectedNodeSet.Remove(node);
            _selectedNodesList.Remove(node);
        }
    }

    private void CaptureRubberBandMouse()
    {
        if (!TreeView.Capture)
        {
            TreeView.Capture = true;
        }
    }

    private void ReleaseRubberBandMouse()
    {
        if (TreeView.Capture)
        {
            TreeView.Capture = false;
        }
    }

    #endregion
}

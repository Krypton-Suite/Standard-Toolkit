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
/// Drop-down content for <see cref="KryptonTreeComboBox"/> hosting a <see cref="KryptonTreeView"/>.
/// </summary>
internal sealed class KryptonTreeComboBoxDropDown : UserControl, IKryptonDropDownUserControl
{
    #region Instance Fields

    private readonly KryptonTreeComboBox _owner;
    private readonly KryptonTreeView _treeView;

    #endregion

    #region Events

    public event EventHandler<KryptonDropDownCommitEventArgs>? CommitValue;
    public event EventHandler? RequestClose;

    #endregion

    #region Identity

    public KryptonTreeComboBoxDropDown(KryptonTreeComboBox owner)
    {
        _owner = owner;
        _treeView = new KryptonTreeView { Dock = DockStyle.Fill };
        _treeView.NodeMouseClick += OnNodeMouseClick;
        _treeView.NodeMouseDoubleClick += OnNodeMouseDoubleClick;
        _treeView.KeyDown += OnTreeKeyDown;
        Controls.Add(_treeView);
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets the hosted tree view. Do not reparent this control.
    /// </summary>
    public KryptonTreeView TreeView => _treeView;

    #endregion

    #region IKryptonDropDownUserControl

    public Size GetPreferredDropSize(Size proposedSize)
    {
        if (proposedSize.IsEmpty)
        {
            return Size.Empty;
        }

        return proposedSize;
    }

    public void OnDropDownOpening(object owner)
    {
        if (_owner.SelectedNode != null)
        {
            _treeView.SelectedNode = _owner.SelectedNode;
            _owner.SelectedNode.EnsureVisible();
        }
    }

    public void OnDropDownOpened(object owner) => _treeView.Focus();

    public void OnDropDownClosing(object owner, ref bool cancel)
    {
    }

    public void OnDropDownClosed(object owner)
    {
    }

    #endregion

    #region Implementation

    private void OnNodeMouseClick(object? sender, TreeNodeMouseClickEventArgs e)
    {
        if (_owner.CommitOnNodeClick
            && e.Button == MouseButtons.Left
            && e.Node != null
            && _owner.CanSelectNode(e.Node))
        {
            CommitNode(e.Node);
        }
    }

    private void OnNodeMouseDoubleClick(object? sender, TreeNodeMouseClickEventArgs e)
    {
        if (e.Node != null && _owner.CanSelectNode(e.Node))
        {
            CommitNode(e.Node);
        }
    }

    private void OnTreeKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter && _treeView.SelectedNode is TreeNode selected && _owner.CanSelectNode(selected))
        {
            CommitNode(selected);
            e.Handled = true;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Escape)
        {
            RequestClose?.Invoke(this, EventArgs.Empty);
            e.Handled = true;
            e.SuppressKeyPress = true;
        }
    }

    private void CommitNode(TreeNode node)
    {
        string displayText = _owner.FormatNodeDisplay(node);
        CommitValue?.Invoke(this, new KryptonDropDownCommitEventArgs(node, displayText));
    }

    #endregion
}

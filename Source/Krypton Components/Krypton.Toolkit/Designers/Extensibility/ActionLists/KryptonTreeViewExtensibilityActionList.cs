#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, tobitege, Lesandro & KamaniAR et al. 2025 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Action list for the KryptonTreeView control using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonTreeViewExtensibilityActionList : KryptonExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonTreeView? _treeView;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonTreeViewExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonTreeViewExtensibilityActionList(ControlDesigner owner)
        : base(owner)
    {
        _treeView = (KryptonTreeView?)owner.Component;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets and sets a value indicating whether checkboxes are displayed next to tree nodes.
    /// </summary>
    public bool CheckBoxes
    {
        get => _treeView?.CheckBoxes ?? false;
        set => SetPropertyValue(_treeView!, nameof(CheckBoxes), CheckBoxes, value, v => _treeView!.CheckBoxes = v);
    }

    /// <summary>
    /// Gets and sets a value indicating whether lines are drawn between tree nodes.
    /// </summary>
    public bool ShowLines
    {
        get => _treeView?.ShowLines ?? true;
        set => SetPropertyValue(_treeView!, nameof(ShowLines), ShowLines, value, v => _treeView!.ShowLines = v);
    }

    /// <summary>
    /// Gets and sets a value indicating whether plus-sign and minus-sign buttons are displayed next to tree nodes.
    /// </summary>
    public bool ShowPlusMinus
    {
        get => _treeView?.ShowPlusMinus ?? true;
        set => SetPropertyValue(_treeView!, nameof(ShowPlusMinus), ShowPlusMinus, value, v => _treeView!.ShowPlusMinus = v);
    }

    /// <summary>
    /// Gets and sets a value indicating whether lines are drawn between the tree nodes that are at the root of the tree view.
    /// </summary>
    public bool ShowRootLines
    {
        get => _treeView?.ShowRootLines ?? true;
        set => SetPropertyValue(_treeView!, nameof(ShowRootLines), ShowRootLines, value, v => _treeView!.ShowRootLines = v);
    }

    /// <summary>
    /// Gets and sets a value indicating whether the selection highlight spans the width of the tree view control.
    /// </summary>
    public bool FullRowSelect
    {
        get => _treeView?.FullRowSelect ?? false;
        set => SetPropertyValue(_treeView!, nameof(FullRowSelect), FullRowSelect, value, v => _treeView!.FullRowSelect = v);
    }

    /// <summary>
    /// Gets and sets a value indicating whether the tree view control has scroll bars.
    /// </summary>
    public bool Scrollable
    {
        get => _treeView?.Scrollable ?? true;
        set => SetPropertyValue(_treeView!, nameof(Scrollable), Scrollable, value, v => _treeView!.Scrollable = v);
    }
    #endregion

    #region Public Override
    /// <summary>
    /// Returns the collection of DesignerActionItem objects contained in the list.
    /// </summary>
    /// <returns>A DesignerActionItem array that contains the items in this list.</returns>
    public override DesignerActionItemCollection GetSortedActionItems()
    {
        var actions = new DesignerActionItemCollection();

        // This can be null when deleting a control instance at design time
        if (_treeView != null)
        {
            // Add the list of TreeView specific actions
            actions.Add(new DesignerActionHeaderItem(@"Appearance"));
            actions.Add(new DesignerActionPropertyItem(nameof(CheckBoxes), @"Check Boxes", @"Appearance", @"Show checkboxes"));
            actions.Add(new DesignerActionPropertyItem(nameof(ShowLines), @"Show Lines", @"Appearance", @"Show lines between nodes"));
            actions.Add(new DesignerActionPropertyItem(nameof(ShowPlusMinus), @"Show Plus/Minus", @"Appearance", @"Show plus/minus buttons"));
            actions.Add(new DesignerActionPropertyItem(nameof(ShowRootLines), @"Show Root Lines", @"Appearance", @"Show root lines"));
            actions.Add(new DesignerActionPropertyItem(nameof(FullRowSelect), @"Full Row Select", @"Appearance", @"Full row selection"));
            actions.Add(new DesignerActionHeaderItem(@"Behavior"));
            actions.Add(new DesignerActionPropertyItem(nameof(Scrollable), @"Scrollable", @"Behavior", @"Enable scrolling"));
        }

        return actions;
    }
    #endregion
}

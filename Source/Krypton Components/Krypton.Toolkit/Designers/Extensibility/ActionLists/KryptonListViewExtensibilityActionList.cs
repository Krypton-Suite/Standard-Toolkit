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
/// Action list for the KryptonListView control using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonListViewExtensibilityActionList : KryptonExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonListView? _listView;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonListViewExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonListViewExtensibilityActionList(ControlDesigner owner)
        : base(owner)
    {
        _listView = (KryptonListView?)owner.Component;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets and sets the view mode.
    /// </summary>
    public View View
    {
        get => _listView?.View ?? View.LargeIcon;
        set => SetPropertyValue(_listView!, nameof(View), View, value, v => _listView!.View = v);
    }

    /// <summary>
    /// Gets and sets whether checkboxes are shown.
    /// </summary>
    public bool CheckBoxes
    {
        get => _listView?.CheckBoxes ?? false;
        set => SetPropertyValue(_listView!, nameof(CheckBoxes), CheckBoxes, value, v => _listView!.CheckBoxes = v);
    }

    /// <summary>
    /// Gets and sets whether full row select is enabled.
    /// </summary>
    public bool FullRowSelect
    {
        get => _listView?.FullRowSelect ?? false;
        set => SetPropertyValue(_listView!, nameof(FullRowSelect), FullRowSelect, value, v => _listView!.FullRowSelect = v);
    }

    /// <summary>
    /// Gets and sets whether grid lines are shown.
    /// </summary>
    public bool GridLines
    {
        get => _listView?.GridLines ?? false;
        set => SetPropertyValue(_listView!, nameof(GridLines), GridLines, value, v => _listView!.GridLines = v);
    }

    /// <summary>
    /// Gets and sets whether hot tracking is enabled.
    /// </summary>
    public bool HotTracking
    {
        get => _listView?.HotTracking ?? false;
        set => SetPropertyValue(_listView!, nameof(HotTracking), HotTracking, value, v => _listView!.HotTracking = v);
    }

    /// <summary>
    /// Gets and sets whether hover selection is enabled.
    /// </summary>
    public bool HoverSelection
    {
        get => _listView?.HoverSelection ?? false;
        set => SetPropertyValue(_listView!, nameof(HoverSelection), HoverSelection, value, v => _listView!.HoverSelection = v);
    }

    /// <summary>
    /// Gets and sets whether label edit is allowed.
    /// </summary>
    public bool LabelEdit
    {
        get => _listView?.LabelEdit ?? false;
        set => SetPropertyValue(_listView!, nameof(LabelEdit), LabelEdit, value, v => _listView!.LabelEdit = v);
    }

    /// <summary>
    /// Gets and sets whether multi select is enabled.
    /// </summary>
    public bool MultiSelect
    {
        get => _listView?.MultiSelect ?? true;
        set => SetPropertyValue(_listView!, nameof(MultiSelect), MultiSelect, value, v => _listView!.MultiSelect = v);
    }

    /// <summary>
    /// Gets and sets the sorting order.
    /// </summary>
    public SortOrder Sorting
    {
        get => _listView?.Sorting ?? SortOrder.None;
        set => SetPropertyValue(_listView!, nameof(Sorting), Sorting, value, v => _listView!.Sorting = v);
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
        if (_listView != null)
        {
            // Add the list of ListView specific actions
            actions.Add(new DesignerActionHeaderItem(@"Appearance"));
            actions.Add(new DesignerActionPropertyItem(nameof(View), @"View", @"Appearance", @"View mode"));
            actions.Add(new DesignerActionPropertyItem(nameof(CheckBoxes), @"Check Boxes", @"Appearance", @"Show checkboxes"));
            actions.Add(new DesignerActionPropertyItem(nameof(FullRowSelect), @"Full Row Select", @"Appearance", @"Full row selection"));
            actions.Add(new DesignerActionPropertyItem(nameof(GridLines), @"Grid Lines", @"Appearance", @"Show grid lines"));
            actions.Add(new DesignerActionHeaderItem(@"Behavior"));
            actions.Add(new DesignerActionPropertyItem(nameof(HotTracking), @"Hot Tracking", @"Behavior", @"Hot tracking"));
            actions.Add(new DesignerActionPropertyItem(nameof(HoverSelection), @"Hover Selection", @"Behavior", @"Hover selection"));
            actions.Add(new DesignerActionPropertyItem(nameof(LabelEdit), @"Label Edit", @"Behavior", @"Allow label editing"));
            actions.Add(new DesignerActionPropertyItem(nameof(MultiSelect), @"Multi Select", @"Behavior", @"Multiple selection"));
            actions.Add(new DesignerActionPropertyItem(nameof(Sorting), @"Sorting", @"Behavior", @"Sorting order"));
        }

        return actions;
    }
    #endregion
}

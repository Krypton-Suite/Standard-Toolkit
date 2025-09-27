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
/// Action list for the KryptonSplitContainer control using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonSplitContainerExtensibilityActionList : KryptonExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonSplitContainer? _splitContainer;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonSplitContainerExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonSplitContainerExtensibilityActionList(ControlDesigner owner)
        : base(owner)
    {
        _splitContainer = (KryptonSplitContainer?)owner.Component;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets and sets the orientation of the splitter.
    /// </summary>
    public Orientation Orientation
    {
        get => _splitContainer?.Orientation ?? Orientation.Vertical;
        set => SetPropertyValue(_splitContainer!, nameof(Orientation), Orientation, value, v => _splitContainer!.Orientation = v);
    }

    /// <summary>
    /// Gets and sets the distance of the splitter from the left or top edge.
    /// </summary>
    public int SplitterDistance
    {
        get => _splitContainer?.SplitterDistance ?? 50;
        set => SetPropertyValue(_splitContainer!, nameof(SplitterDistance), SplitterDistance, value, v => _splitContainer!.SplitterDistance = v);
    }

    /// <summary>
    /// Gets and sets the minimum size of panel 1.
    /// </summary>
    public int Panel1MinSize
    {
        get => _splitContainer?.Panel1MinSize ?? 0;
        set => SetPropertyValue(_splitContainer!, nameof(Panel1MinSize), Panel1MinSize, value, v => _splitContainer!.Panel1MinSize = v);
    }

    /// <summary>
    /// Gets and sets the minimum size of panel 2.
    /// </summary>
    public int Panel2MinSize
    {
        get => _splitContainer?.Panel2MinSize ?? 0;
        set => SetPropertyValue(_splitContainer!, nameof(Panel2MinSize), Panel2MinSize, value, v => _splitContainer!.Panel2MinSize = v);
    }

    /// <summary>
    /// Gets and sets whether the splitter is fixed.
    /// </summary>
    public bool IsSplitterFixed
    {
        get => _splitContainer?.IsSplitterFixed ?? false;
        set => SetPropertyValue(_splitContainer!, nameof(IsSplitterFixed), IsSplitterFixed, value, v => _splitContainer!.IsSplitterFixed = v);
    }

    /// <summary>
    /// Gets and sets the width of the splitter.
    /// </summary>
    public int SplitterWidth
    {
        get => _splitContainer?.SplitterWidth ?? 4;
        set => SetPropertyValue(_splitContainer!, nameof(SplitterWidth), SplitterWidth, value, v => _splitContainer!.SplitterWidth = v);
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
        if (_splitContainer != null)
        {
            // Add the list of SplitContainer specific actions
            actions.Add(new DesignerActionHeaderItem(@"Appearance"));
            actions.Add(new DesignerActionPropertyItem(nameof(Orientation), @"Orientation", @"Appearance", @"Splitter orientation"));
            actions.Add(new DesignerActionPropertyItem(nameof(SplitterWidth), @"Splitter Width", @"Appearance", @"Splitter width"));
            actions.Add(new DesignerActionHeaderItem(@"Layout"));
            actions.Add(new DesignerActionPropertyItem(nameof(SplitterDistance), @"Splitter Distance", @"Layout", @"Splitter position"));
            actions.Add(new DesignerActionPropertyItem(nameof(Panel1MinSize), @"Panel1 Min Size", @"Layout", @"Panel1 minimum size"));
            actions.Add(new DesignerActionPropertyItem(nameof(Panel2MinSize), @"Panel2 Min Size", @"Layout", @"Panel2 minimum size"));
            actions.Add(new DesignerActionPropertyItem(nameof(IsSplitterFixed), @"Is Splitter Fixed", @"Layout", @"Splitter is fixed"));
        }

        return actions;
    }
    #endregion
}

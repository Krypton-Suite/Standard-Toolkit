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
/// Action list for the KryptonScrollBar control using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonScrollBarExtensibilityActionList : KryptonExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonScrollBar? _scrollBar;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonScrollBarExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonScrollBarExtensibilityActionList(ControlDesigner owner)
        : base(owner)
    {
        _scrollBar = (KryptonScrollBar?)owner.Component;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets and sets the orientation of the scroll bar.
    /// </summary>
    public ScrollBarOrientation Orientation
    {
        get => _scrollBar?.Orientation ?? ScrollBarOrientation.Vertical;
        set => SetPropertyValue(_scrollBar!, nameof(Orientation), Orientation, value, v => _scrollBar!.Orientation = v);
    }

    /// <summary>
    /// Gets and sets the minimum value.
    /// </summary>
    public int Minimum
    {
        get => _scrollBar?.Minimum ?? 0;
        set => SetPropertyValue(_scrollBar!, nameof(Minimum), Minimum, value, v => _scrollBar!.Minimum = v);
    }

    /// <summary>
    /// Gets and sets the maximum value.
    /// </summary>
    public int Maximum
    {
        get => _scrollBar?.Maximum ?? 100;
        set => SetPropertyValue(_scrollBar!, nameof(Maximum), Maximum, value, v => _scrollBar!.Maximum = v);
    }

    /// <summary>
    /// Gets and sets the current value.
    /// </summary>
    public int Value
    {
        get => _scrollBar?.Value ?? 0;
        set => SetPropertyValue(_scrollBar!, nameof(Value), Value, value, v => _scrollBar!.Value = v);
    }

    /// <summary>
    /// Gets and sets the small change amount.
    /// </summary>
    public int SmallChange
    {
        get => _scrollBar?.SmallChange ?? 1;
        set => SetPropertyValue(_scrollBar!, nameof(SmallChange), SmallChange, value, v => _scrollBar!.SmallChange = v);
    }

    /// <summary>
    /// Gets and sets the large change amount.
    /// </summary>
    public int LargeChange
    {
        get => _scrollBar?.LargeChange ?? 10;
        set => SetPropertyValue(_scrollBar!, nameof(LargeChange), LargeChange, value, v => _scrollBar!.LargeChange = v);
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
        if (_scrollBar != null)
        {
            // Add the list of ScrollBar specific actions
            actions.Add(new DesignerActionHeaderItem(@"Appearance"));
            actions.Add(new DesignerActionPropertyItem(nameof(Orientation), @"Orientation", @"Appearance", @"Scroll bar orientation"));
            actions.Add(new DesignerActionHeaderItem(@"Values"));
            actions.Add(new DesignerActionPropertyItem(nameof(Minimum), @"Minimum", @"Values", @"Minimum value"));
            actions.Add(new DesignerActionPropertyItem(nameof(Maximum), @"Maximum", @"Values", @"Maximum value"));
            actions.Add(new DesignerActionPropertyItem(nameof(Value), @"Value", @"Values", @"Current value"));
            actions.Add(new DesignerActionPropertyItem(nameof(SmallChange), @"Small Change", @"Values", @"Small change amount"));
            actions.Add(new DesignerActionPropertyItem(nameof(LargeChange), @"Large Change", @"Values", @"Large change amount"));
        }

        return actions;
    }
    #endregion
}

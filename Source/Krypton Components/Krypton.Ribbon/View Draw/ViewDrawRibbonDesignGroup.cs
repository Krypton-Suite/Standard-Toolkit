#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Draws an design time only for adding a new group to the selected tab.
/// </summary>
internal class ViewDrawRibbonDesignGroup : ViewDrawRibbonDesignBase
{
    private readonly Padding _padding; // = new(5, 0, 0, 1);

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawRibbonDesignGroup class.
    /// </summary>
    /// <param name="ribbon">Reference to owning ribbon control.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public ViewDrawRibbonDesignGroup(KryptonRibbon ribbon,
        NeedPaintHandler needPaint)
        : base(ribbon, needPaint) =>
        _padding = new Padding((int)(5 * FactorDpiX), 0, 0, (int)(1 * FactorDpiY));

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $@"ViewDrawRibbonDesignGroup:{Id}";

    #endregion

    #region Protected
    /// <summary>
    /// Gets the short text used as the main ribbon title.
    /// </summary>
    /// <returns>Title string.</returns>
    public override string GetShortText() => @"Group";

    /// <summary>
    /// Gets the padding to use when calculating the preferred size.
    /// </summary>
    protected override Padding PreferredPadding => _padding;

    /// <summary>
    /// Gets the padding to use when laying out the view.
    /// </summary>
    protected override Padding LayoutPadding => Padding.Empty;

    /// <summary>
    /// Gets the padding to shrink the client area by when laying out.
    /// </summary>
    protected override Padding OuterPadding => _padding;

    /// <summary>
    /// Raises the Click event.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected override void OnClick(object? sender, EventArgs e) => Ribbon.SelectedTab?.OnDesignTimeAddGroup();
    #endregion
}
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
/// Draws an design time only for adding a new tab to the ribbon.
/// </summary>
internal class ViewDrawRibbonDesignTab : ViewDrawRibbonDesignBase
{
    private readonly Padding _padding; // = new(2, 4, 2, 0);

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawRibbonDesignTab class.
    /// </summary>
    /// <param name="ribbon">Reference to owning ribbon control.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public ViewDrawRibbonDesignTab(KryptonRibbon ribbon,
        NeedPaintHandler needPaint)
        : base(ribbon, needPaint) =>
        _padding = new Padding((int)(2 * FactorDpiX), (int)(4 * FactorDpiY), (int)(2 * FactorDpiX), 0);

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $@"ViewDrawRibbonDesignTab:{Id}";

    #endregion

    #region Protected
    /// <summary>
    /// Gets the short text used as the main ribbon title.
    /// </summary>
    /// <returns>Title string.</returns>
    public override string GetShortText() => @"Tab";

    /// <summary>
    /// Gets the padding to use when calculating the preferred size.
    /// </summary>
    protected override Padding PreferredPadding => _padding;

    /// <summary>
    /// Gets the padding to use when laying out the view.
    /// </summary>
    protected override Padding LayoutPadding => _padding;

    /// <summary>
    /// Gets the padding to shrink the client area by when laying out.
    /// </summary>
    protected override Padding OuterPadding => Padding.Empty;

    /// <summary>
    /// Raises the Click event.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected override void OnClick(object? sender, EventArgs e) =>
        // Ask the ribbon to add a new tab at design time
        Ribbon.OnDesignTimeAddTab();
    #endregion
}
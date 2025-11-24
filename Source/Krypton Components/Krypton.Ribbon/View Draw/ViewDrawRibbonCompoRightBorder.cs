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
/// Allocate a spacer for the right side of a window that prevents layout over the min/max/close buttons.
/// </summary>
internal class ViewDrawRibbonCompoRightBorder : ViewLeaf
{
    #region Instance Fields

    // Note: Do we need _width?
    // private int _width;
    private readonly int _spacingGap; // = 10;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawRibbonCompoRightBorder class.
    /// </summary>
    public ViewDrawRibbonCompoRightBorder() => _spacingGap = (int)(10 * FactorDpiX);

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $@"ViewDrawRibbonCompoRightBorder:{Id}";

    #endregion

    #region CompOwnerForm
    /// <summary>
    /// Gets and sets the owner form to use when compositing.
    /// </summary>
    public VisualForm CompOwnerForm { get; set; }

    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize(ViewLayoutContext context)
    {
        var preferredSize = Size.Empty;

        return preferredSize;
    }

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        // Start with all the provided space
        ClientRectangle = context!.DisplayRectangle;
    }
    #endregion
}
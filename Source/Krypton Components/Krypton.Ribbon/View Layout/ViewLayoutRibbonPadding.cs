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
/// View element adds padding to the provided drawing area.
/// </summary>
internal class ViewLayoutRibbonPadding : ViewComposite
{
    #region Instance Fields
    private Padding _preferredPadding;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewLayoutRibbonPadding class.
    /// </summary>
    /// <param name="preferredPadding">Padding to use when calculating space.</param>
    public ViewLayoutRibbonPadding(Padding preferredPadding) => _preferredPadding = preferredPadding;

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewLayoutRibbonPadding:{Id}";

    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize(ViewLayoutContext context)
    {
        // Get the preferred size of the contained content
        Size preferredSize = base.GetPreferredSize(context);

        // Add on the padding we need around edges
        return new Size(preferredSize.Width + _preferredPadding.Horizontal,
            preferredSize.Height + _preferredPadding.Vertical);
    }

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout(ViewLayoutContext context)
    {
        // Validate incoming reference
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        // We take on all the available display area
        ClientRectangle = context.DisplayRectangle;

        // Find the rectangle for the child elements by applying padding
        context.DisplayRectangle = CommonHelper.ApplyPadding(Orientation.Horizontal, ClientRectangle, _preferredPadding);

        // Let base perform actual layout process of child elements
        base.Layout(context);

        // Put back the original display value now we have finished
        context.DisplayRectangle = ClientRectangle;
    }
    #endregion
}
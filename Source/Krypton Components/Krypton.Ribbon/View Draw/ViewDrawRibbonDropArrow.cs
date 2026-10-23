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
/// Draws a drop arrow used in various ribbon controls.
/// </summary>
internal class ViewDrawRibbonDropArrow : ViewLeaf
{
    #region Instance Fields
    private readonly KryptonRibbon _ribbon;
    private readonly Size _arrowSize; // = new(5, 4);
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawRibbonDropArrow class.
    /// </summary>
    /// <param name="ribbon">Reference to owning control instance.</param>
    public ViewDrawRibbonDropArrow([DisallowNull] KryptonRibbon ribbon)
    {
        Debug.Assert(ribbon != null);
        _ribbon = ribbon!;
        _arrowSize = new Size((int)(5 * FactorDpiX), (int)(4 * FactorDpiY));
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $@"ViewDrawRibbonDropArrow:{Id}";

    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize(ViewLayoutContext context) => _arrowSize;

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        // We take on all the available display area
        ClientRectangle = context!.DisplayRectangle;
    }
    #endregion

    #region Paint
    /// <summary>
    /// Perform rendering before child elements are rendered.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public override void RenderBefore([DisallowNull] RenderContext context)
    {
        if (context.Renderer is null)
        {
            throw new ArgumentNullException(nameof(context.Renderer));
        }


        // Use renderer to draw the drop arrow in the provided space
        context.Renderer.RenderGlyph.DrawRibbonDropArrow(_ribbon.RibbonShape,
            context,
            ClientRectangle,
            _ribbon.StateCommon.RibbonGeneral,
            State);
    }
    #endregion
}
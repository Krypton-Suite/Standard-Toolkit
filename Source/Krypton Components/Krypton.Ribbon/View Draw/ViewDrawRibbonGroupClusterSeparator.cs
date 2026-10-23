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
/// Draws a short vertical cluster separator.
/// </summary>
internal class ViewDrawRibbonGroupClusterSeparator : ViewLeaf
{
    #region Instance Fields
    private readonly Size _preferredSize; // = new(1, 4);
    private readonly KryptonRibbon _ribbon;
    private readonly bool _start;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawRibbonGroupClusterSeparator class.
    /// </summary>
    /// <param name="ribbon">Reference to owning ribbon control.</param>
    /// <param name="start">Is this is cluster start separator.</param>
    public ViewDrawRibbonGroupClusterSeparator([DisallowNull] KryptonRibbon? ribbon, bool start)
    {
        Debug.Assert(ribbon is not null);

        _ribbon = ribbon ?? throw new ArgumentNullException(nameof(ribbon ));
        _start = start;
        _preferredSize = new Size((int)(1 * FactorDpiX), (int)(4 * FactorDpiY));
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $@"ViewDrawRibbonGroupClusterSeparator:{Id}";

    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize(ViewLayoutContext context) => _preferredSize;

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context is not null);

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

        Rectangle drawRect = ClientRectangle;

        if (_start)
        {
            drawRect.X -= 4;
        }

        drawRect.Width += 4;

        context.Renderer.RenderGlyph.DrawRibbonGroupSeparator(_ribbon.RibbonShape, 
            context,
            drawRect, 
            _ribbon.StateCommon.RibbonGeneral, 
            State);
    }
    #endregion
}
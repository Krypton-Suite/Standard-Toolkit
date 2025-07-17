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
/// Draws a border edge but with a lighter inside area.
/// </summary>
internal class ViewDrawRibbonGroupClusterEdge : ViewDrawBorderEdge
{
    #region Instance Fields
    private readonly KryptonRibbon _ribbon;
    private readonly PaletteBorderEdge _palette;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawRibbonGroupClusterEdge class.
    /// </summary>
    /// <param name="ribbon">Reference to owning ribbon..</param>
    /// <param name="palette">Palette source for drawing details.</param>
    public ViewDrawRibbonGroupClusterEdge([DisallowNull] KryptonRibbon? ribbon,
        [DisallowNull] PaletteBorderEdge? palette)
        : base(palette, Orientation.Vertical)
    {
        Debug.Assert(ribbon != null);
        Debug.Assert(palette != null);

        _ribbon = ribbon ?? throw new ArgumentNullException(nameof(ribbon ));
        _palette = palette ?? throw new ArgumentNullException(nameof(palette));
    }
    #endregion

    #region Paint
    /// <summary>
    /// Perform rendering before child elements are rendered.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public override void RenderBefore([DisallowNull]RenderContext context)
    {
        if (context.Renderer is null)
        {
            throw new ArgumentNullException(nameof(context.Renderer));
        }

        // Let base class perform standard drawing first
        base.RenderBefore(context);

        var drawRect = new Rectangle(ClientLocation.X, ClientLocation.Y + ClientWidth, ClientWidth,
            ClientHeight - (ClientWidth * 2));

        context.Renderer.RenderRibbon.DrawRibbonClusterEdge(_ribbon.RibbonShape, context, drawRect, _palette, State);
    }
    #endregion
}
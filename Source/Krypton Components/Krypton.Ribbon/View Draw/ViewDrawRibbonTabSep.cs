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
/// Draws a separator between ribbon tabs.
/// </summary>
internal class ViewDrawRibbonTabSep : ViewLayoutRibbonSeparator
{
    #region Static Fields

    private const int SEP_WIDTH = 4;    // Passed to base class for DPI modification
    private static readonly Color _lighten1 = Color.FromArgb(128, Color.White);
    private static readonly Blend _fadeBlend;
    #endregion

    #region Instance Fields
    private readonly IPaletteRibbonGeneral _palette;

    #endregion

    #region Identity
    static ViewDrawRibbonTabSep()
    {
        _fadeBlend = new Blend
        {
            Factors = [0.0f, 1.0f, 1.0f],
            Positions = [0.0f, 0.33f, 1.0f]
        };
    }

    /// <summary>
    /// Initialize a new instance of the ViewDrawRibbonTabSep class.
    /// </summary>
    /// <param name="palette">Source for palette values.</param>
    public ViewDrawRibbonTabSep([DisallowNull] IPaletteRibbonGeneral palette)
        : base(SEP_WIDTH, true)
    {
        Debug.Assert(palette != null);
        _palette = palette!;
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewDrawRibbonTabSep:{Id}";

    #endregion

    #region Draw
    /// <summary>
    /// Gets and sets a value indicating if the tab separator should draw.
    /// </summary>
    public bool Draw { get; set; }

    #endregion

    #region Paint
    /// <summary>
    /// Perform rendering before child elements are rendered.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public override void RenderBefore(RenderContext context)
    {
        if (Draw)
        {
            var rectF = new RectangleF(ClientLocation.X, ClientLocation.Y - 0.5f, ClientWidth, ClientHeight + 1);
            using var sepBrush = new LinearGradientBrush(rectF, Color.Transparent,
                _palette.GetRibbonTabSeparatorColor(PaletteState.Normal), 90f);
            sepBrush.Blend = _fadeBlend;

            switch (_palette.GetRibbonShape())
            {
                default:
                case PaletteRibbonShape.Office2007:
                case PaletteRibbonShape.Office2013:
                case PaletteRibbonShape.Microsoft365:
                case PaletteRibbonShape.VisualStudio:
                    context.Graphics.FillRectangle(sepBrush, ClientLocation.X + 2, ClientLocation.Y, 1, ClientHeight - 1);
                    break;
                case PaletteRibbonShape.Office2010:
                case PaletteRibbonShape.VisualStudio2010:
                    context.Graphics.FillRectangle(sepBrush, ClientLocation.X + 1, ClientLocation.Y, 1, ClientHeight - 1);

                    using (var sepLightBrush = new LinearGradientBrush(rectF, Color.Transparent, _lighten1, 90f))
                    {
                        context.Graphics.FillRectangle(sepLightBrush, ClientLocation.X + 2, ClientLocation.Y, 1, ClientHeight - 1);
                    }
                    break;
            }
        }
    }
    #endregion
}
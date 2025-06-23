#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Extends the professional renderer to provide Office2010 style additions.
/// </summary>
public class RenderOffice2010 : RenderProfessional
{
    #region Static Fields

    private const float BORDER_PERCENT = 0.6f;
    private const float WHITE_PERCENT = 0.4f;
    private static readonly Blend _ribbonGroup5Blend;
    private static readonly Blend _ribbonGroup6Blend;
    private static readonly Blend _ribbonGroup7Blend;
    #endregion

    #region Identity
    static RenderOffice2010()
    {
        _ribbonGroup5Blend = new Blend
        {
            Factors = [0.0f, 0.0f, 1.0f],
            Positions = [0.0f, 0.5f, 1.0f]
        };

        _ribbonGroup6Blend = new Blend
        {
            Factors = [0.0f, 0.0f, 0.75f, 1.0f],
            Positions = [0.0f, 0.1f, 0.45f, 1.0f]
        };

        _ribbonGroup7Blend = new Blend
        {
            Factors = [0.0f, 1.0f, 1.0f, 0.0f],
            Positions = [0.0f, 0.15f, 0.85f, 1.0f]
        };
    }
    #endregion

    #region RenderRibbon Overrides
    /// <summary>
    /// Perform drawing of a ribbon cluster edge.
    /// </summary>
    /// <param name="shape">Ribbon shape.</param>
    /// <param name="context">Render context.</param>
    /// <param name="displayRect">Display area available for drawing.</param>
    /// <param name="paletteBack">Palette used for recovering drawing details.</param>
    /// <param name="state">State associated with rendering.</param>
    public override void DrawRibbonClusterEdge(PaletteRibbonShape shape,
        [DisallowNull] RenderContext context,
        Rectangle displayRect,
        [DisallowNull] IPaletteBack paletteBack,
        PaletteState state)
    {
        Debug.Assert(context != null);
        Debug.Assert(paletteBack != null);

        if (paletteBack is null)
        {
            throw new ArgumentNullException(nameof(paletteBack));
        }

        // Get the first border color
        Color borderColor = paletteBack.GetBackColor1(state);

        // We want to lighten it by merging with white
        Color lightColor = CommonHelper.MergeColors(borderColor, BORDER_PERCENT,
            Color.White, WHITE_PERCENT);

        // Draw inside of the border edge in a lighter version of the border
        using var drawBrush = new SolidBrush(lightColor);
        context!.Graphics.FillRectangle(drawBrush, displayRect);
    }

    #endregion

    #region IRenderer Overrides

    /// <summary>
    /// Gets a renderer for drawing the toolstrips.
    /// </summary>
    /// <param name="colorPalette">Color palette to use when rendering toolstrip.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public override ToolStripRenderer RenderToolStrip([DisallowNull] PaletteBase? colorPalette)
    {
        Debug.Assert(colorPalette != null);

        // Validate incoming parameter
        if (colorPalette == null)
        {
            throw new ArgumentNullException(nameof(colorPalette));
        }

        // Use the professional renderer but pull colors from the palette
        var renderer = new KryptonOffice2010Renderer(colorPalette.ColorTable)
        {

            // Setup the need to use rounded corners
            RoundedEdges = colorPalette.ColorTable.UseRoundedEdges != InheritBool.False
        };

        return renderer;
    }
    #endregion

    #region Implementation

    /// <summary>
    /// Internal rendering method.
    /// </summary>
    protected override IDisposable? DrawRibbonTabContext(RenderContext context,
        Rectangle rect,
        IPaletteRibbonGeneral paletteGeneral,
        IPaletteRibbonBack paletteBack,
        IDisposable? memento)
    {
        if (rect is { Width: > 0, Height: > 0 })
        {
            Color c1 = paletteGeneral.GetRibbonTabSeparatorContextColor(PaletteState.Normal);
            Color c2 = paletteBack.GetRibbonBackColor5(PaletteState.ContextCheckedNormal);

            var generate = true;
            MementoRibbonTabContextOffice2010 cache;

            // Access a cache instance and decide if cache resources need generating
            if (memento is MementoRibbonTabContextOffice2010 contextOffice2010)
            {
                cache = contextOffice2010;
                generate = !cache.UseCachedValues(rect, c1, c2);
            }
            else
            {
                memento?.Dispose();

                cache = new MementoRibbonTabContextOffice2010(rect, c1, c2);
                memento = cache;
            }

            // Do we need to generate the contents of the cache?
            if (generate)
            {
                // Dispose of existing values
                cache.Dispose();

                cache.BorderOuterPen = new Pen(c1);
                cache.BorderInnerPen = new Pen(CommonHelper.MergeColors(Color.Black, 0.1f, c2, 0.9f));
                cache.TopBrush = new SolidBrush(c2);
                Color lightC2 = ControlPaint.Light(c2);
                cache.BottomBrush = new LinearGradientBrush(new RectangleF(rect.X - 1, rect.Y, rect.Width + 2, rect.Height + 1),
                    Color.FromArgb(128, lightC2), Color.FromArgb(64, lightC2), 90f);
            }

            // Draw the left and right borders
            context.Graphics.DrawLine(cache.BorderOuterPen!, rect.X, rect.Y, rect.X, rect.Bottom);
            context.Graphics.DrawLine(cache.BorderInnerPen!, rect.X + 1, rect.Y, rect.X + 1, rect.Bottom - 1);
            context.Graphics.DrawLine(cache.BorderOuterPen!, rect.Right - 1, rect.Y, rect.Right - 1, rect.Bottom - 1);
            context.Graphics.DrawLine(cache.BorderInnerPen!, rect.Right - 2, rect.Y, rect.Right - 2, rect.Bottom - 1);
            
            // Draw the solid block of colour at the top
            context.Graphics.FillRectangle(cache.TopBrush!, rect.X + 2, rect.Y, rect.Width - 4, 4);

            // Draw the gradient to the bottom
            context.Graphics.FillRectangle(cache.BottomBrush!, rect.X + 2, rect.Y + 4, rect.Width - 4, rect.Height - 4);
        }

        return memento;
    }
    #endregion
}
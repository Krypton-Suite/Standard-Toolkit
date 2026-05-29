#region BSD License
/*
 *   BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>Extends the professional renderer to provide Visual Studio 2010 with Office 2007 style additions.</summary>
public class RenderVisualStudio2010With2007 : RenderProfessional
{
    #region Static Fields

    private const float BORDER_PERCENT = 0.6f;
    private const float WHITE_PERCENT = 0.4f;
    private static readonly Blend _ribbonGroup5Blend;
    private static readonly Blend _ribbonGroup6Blend;
    private static readonly Blend _ribbonGroup7Blend;
    #endregion

    #region Identity
    static RenderVisualStudio2010With2007()
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
        Debug.Assert(context is not null);
        Debug.Assert(paletteBack is not null);

        if (paletteBack is null)
        {
            throw new ArgumentNullException(nameof(paletteBack));
        }

        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        // Get the first border color
        Color borderColor = paletteBack.GetBackColor1(state);

        // We want to lighten it by merging with white
        Color lightColor = CommonHelper.MergeColors(borderColor, BORDER_PERCENT,
            Color.White, WHITE_PERCENT);

        // Draw inside of the border edge in a lighter version of the border
        using var drawBrush = new SolidBrush(lightColor);
        context.Graphics.FillRectangle(drawBrush, displayRect);
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
        var renderer = new KryptonOffice2007Renderer(colorPalette.ColorTable)
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
            MementoRibbonTabContextOffice cache;

            // Access a cache instance and decide if cache resources need generating
            if (memento is MementoRibbonTabContextOffice office)
            {
                cache = office;
                generate = !cache.UseCachedValues(rect, c1, c2);
            }
            else
            {
                memento?.Dispose();

                cache = new MementoRibbonTabContextOffice(rect, c1, c2);
                memento = cache;
            }

            // Do we need to generate the contents of the cache?
            if (generate)
            {
                // Dispose of existing values
                cache.Dispose();

                var borderRect = new Rectangle(rect.X - 1, rect.Y - 1, rect.Width + 2, rect.Height + 2);
                cache.FillRect = new Rectangle(rect.X + 1, rect.Y, rect.Width - 2, rect.Height - 1);

                var borderBrush = new LinearGradientBrush(borderRect, c1, Color.Transparent, 270f)
                {
                    Blend = _ribbonGroup5Blend
                };
                cache.BorderPen = new Pen(borderBrush);

                var underlineBrush =
                    new LinearGradientBrush(borderRect, Color.Transparent, Color.FromArgb(200, c2), 0f)
                    {
                        Blend = _ribbonGroup7Blend
                    };
                cache.UnderlinePen = new Pen(underlineBrush);

                cache.FillBrush = new LinearGradientBrush(borderRect, Color.FromArgb(106, c2), Color.Transparent, 270f)
                {
                    Blend = _ribbonGroup6Blend
                };
            }

            // Draw the left and right border lines
            context.Graphics.DrawLine(cache.BorderPen!, rect.X, rect.Y, rect.X, rect.Bottom - 1);
            context.Graphics.DrawLine(cache.BorderPen!, rect.Right - 1, rect.Y, rect.Right - 1, rect.Bottom - 1);

            // Fill the inner area with a gradient context specific color
            context.Graphics.FillRectangle(cache.FillBrush!, cache.FillRect);

            // Overdraw the brighter line at bottom
            context.Graphics.DrawLine(cache.UnderlinePen!, rect.X + 1, rect.Bottom - 2, rect.Right - 2, rect.Bottom - 2);
        }

        return memento;
    }
    #endregion
}
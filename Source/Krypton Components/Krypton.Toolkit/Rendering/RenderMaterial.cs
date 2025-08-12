#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2025 - 2025. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Renderer targeting a flat, dense Material visual language.
/// </summary>
/// <seealso cref="RenderOffice2010" />
public sealed class RenderMaterial : RenderOffice2010
{
    #region Constructor
    static RenderMaterial()
    {
    }
    #endregion

    #region RenderRibbon Overrides
    /// <inheritdoc />
    public override void DrawRibbonClusterEdge(PaletteRibbonShape shape,
        [DisallowNull] RenderContext context,
        Rectangle displayRect,
        [DisallowNull] IPaletteBack paletteBack,
        PaletteState state)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (paletteBack == null)
        {
            throw new ArgumentNullException(nameof(paletteBack));
        }

        using var drawBrush = new SolidBrush(paletteBack.GetBackColor1(state));
        context.Graphics.FillRectangle(drawBrush, displayRect);
    }
    #endregion

    #region IRenderer Overrides
    /// <inheritdoc />
    public override ToolStripRenderer RenderToolStrip([DisallowNull] PaletteBase? colorPalette)
    {
        if (colorPalette == null)
        {
            throw new ArgumentNullException(nameof(colorPalette));
        }

        var renderer = new KryptonOffice2010Renderer(colorPalette.ColorTable)
        {
            RoundedEdges = colorPalette.ColorTable.UseRoundedEdges != InheritBool.False
        };

        return renderer;
    }
    #endregion

    #region RenderStandardBorder
    /// <inheritdoc />
    public override Padding GetBorderDisplayPadding(IPaletteBorder? palette, PaletteState state, VisualOrientation orientation)
    {
        // Prefer minimal interior insets; fall back to base for overrides or unknowns
        if (CommonHelper.IsOverrideState(state))
        {
            return base.GetBorderDisplayPadding(palette, state, orientation);
        }

        return new Padding(1);
    }
    #endregion
}

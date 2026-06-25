#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Decorates an input control view by painting an optional glowing border after the child is rendered.
/// </summary>
internal sealed class ViewDecoratorInputGlow : ViewDecorator
{
    #region Instance Fields
    private readonly IInputGlowingBorderProvider _provider;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDecoratorInputGlow class.
    /// </summary>
    /// <param name="provider">Glowing border provider.</param>
    /// <param name="child">Decorated view.</param>
    public ViewDecoratorInputGlow(IInputGlowingBorderProvider provider, ViewBase child)
        : base(child)
    {
        _provider = provider;
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        $"ViewDecoratorInputGlow:{Id}";
    #endregion

    #region Paint
    /// <summary>
    /// Perform a render of the elements.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public override void Render(RenderContext context)
    {
        base.Render(context);

        if (!_provider.ShouldDrawGlowingBorder())
        {
            return;
        }

        Rectangle bounds = ClientRectangle;
        if (bounds.Width <= 0 || bounds.Height <= 0)
        {
            return;
        }

        InputGlowingBorderValues values = _provider.Values;
        IPaletteTriple tripleState = _provider.GetGlowingBorderTripleState();
        PaletteState state = _provider.GetGlowingBorderState();
        IPaletteBorder? paletteBorder = tripleState.PaletteBorder;

        if (paletteBorder == null)
        {
            return;
        }

        InputGlowBorderRenderer.Draw(context,
            bounds,
            paletteBorder,
            state,
            _provider.AnimationPhase,
            values.Animate,
            values.Style,
            values.Colors.Color1,
            values.Colors.Color2,
            values.Colors.HighlightColor);
    }
    #endregion
}

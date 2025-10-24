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
/// Redirect border based on the incoming state of the request.
/// </summary>
public class PaletteRedirectBorderEdge : PaletteRedirect
{
    #region Instance Fields
    private PaletteBorderEdge? _disabled;
    private PaletteBorderEdge? _normal;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteRedirectBorderEdge class.
    /// </summary>
    /// <param name="target">Initial palette target for redirection.</param>
    public PaletteRedirectBorderEdge(PaletteBase target)
        : this(target, null, null)
    {
    }

    /// <summary>
    /// Initialize a new instance of the PaletteRedirectBorderEdge class.
    /// </summary>
    /// <param name="target">Initial palette target for redirection.</param>
    /// <param name="disabled">Redirection for disabled state requests.</param>
    /// <param name="normal">Redirection for normal state requests.</param>
    public PaletteRedirectBorderEdge(PaletteBase target,
        PaletteBorderEdge? disabled,
        PaletteBorderEdge? normal)
        : base(target)
    {
        // Remember state specific inheritance
        _disabled = disabled;
        _normal = normal;
    }
    #endregion

    #region SetRedirectStates
    /// <summary>
    /// Set the redirection states.
    /// </summary>
    /// <param name="disabled">Redirection for disabled state requests.</param>
    /// <param name="normal">Redirection for normal state requests.</param>
    public virtual void SetRedirectStates(PaletteBorderEdge disabled,
        PaletteBorderEdge normal)
    {
        _disabled = disabled;
        _normal = normal;
    }
    #endregion

    #region ResetRedirectStates
    /// <summary>
    /// Reset the redirection states to null.
    /// </summary>
    public virtual void ResetRedirectStates()
    {
        _disabled = null;
        _normal = null;
    }
    #endregion

    #region Border
    /// <summary>
    /// Gets a value indicating if border should be drawn.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public override InheritBool GetBorderDraw(PaletteBorderStyle style, PaletteState state)
    {
        PaletteBorderEdge? inherit = GetInherit(state);

        return inherit?.GetBackDraw(state) ?? Target?.GetBorderDraw(style, state) ?? InheritBool.Inherit;
    }

    /// <summary>
    /// Gets a value indicating which borders to draw.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteDrawBorders value.</returns>
    public override PaletteDrawBorders GetBorderDrawBorders(PaletteBorderStyle style, PaletteState state) => Target?.GetBorderDrawBorders(style, state) ?? PaletteDrawBorders.Inherit;

    /// <summary>
    /// Gets the graphics drawing hint for the border.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteGraphicsHint value.</returns>
    public override PaletteGraphicsHint GetBorderGraphicsHint(PaletteBorderStyle style, PaletteState state)
    {
        PaletteBorderEdge? inherit = GetInherit(state);

        return inherit?.GetBackGraphicsHint(state) ?? Target?.GetBorderGraphicsHint(style, state) ?? PaletteGraphicsHint.Inherit;
    }

    /// <summary>
    /// Gets the first border color.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetBorderColor1(PaletteBorderStyle style, PaletteState state)
    {
        PaletteBorderEdge? inherit = GetInherit(state);

        return inherit?.GetBackColor1(state) ?? Target?.GetBorderColor1(style, state) ?? GlobalStaticValues.EMPTY_COLOR;
    }

    /// <summary>
    /// Gets the second border color.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetBorderColor2(PaletteBorderStyle style, PaletteState state)
    {
        PaletteBorderEdge? inherit = GetInherit(state);

        return inherit?.GetBackColor2(state) ?? Target?.GetBorderColor2(style, state) ?? GlobalStaticValues.EMPTY_COLOR;
    }

    /// <summary>
    /// Gets the color border drawing style.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public override PaletteColorStyle GetBorderColorStyle(PaletteBorderStyle style, PaletteState state)
    {
        PaletteBorderEdge? inherit = GetInherit(state);

        return inherit?.GetBackColorStyle(state) ?? Target?.GetBorderColorStyle(style, state) ?? PaletteColorStyle.Inherit;
    }

    /// <summary>
    /// Gets the color border alignment.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public override PaletteRectangleAlign GetBorderColorAlign(PaletteBorderStyle style, PaletteState state)
    {
        PaletteBorderEdge? inherit = GetInherit(state);

        return inherit?.GetBackColorAlign(state) ?? Target?.GetBorderColorAlign(style, state) ?? PaletteRectangleAlign.Inherit;
    }

    /// <summary>
    /// Gets the color border angle.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public override float GetBorderColorAngle(PaletteBorderStyle style, PaletteState state)
    {
        PaletteBorderEdge? inherit = GetInherit(state);

        return inherit?.GetBackColorAngle(state) ?? Target?.GetBorderColorAngle(style, state) ?? 0.0f;
    }

    /// <summary>
    /// Gets the border width.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Integer width.</returns>
    public override int GetBorderWidth(PaletteBorderStyle style, PaletteState state)
    {
        PaletteBorderEdge? inherit = GetInherit(state);

        return inherit?.GetBorderWidth(state) ?? Target?.GetBorderWidth(style, state) ?? 0;
    }

    /// <summary>
    /// Gets the border corner rounding.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Float rounding.</returns>
    public override float GetBorderRounding(PaletteBorderStyle style, PaletteState state) => Target?.GetBorderRounding(style, state) ?? 0.0f;

    /// <summary>
    /// Gets a border image.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public override Image? GetBorderImage(PaletteBorderStyle style, PaletteState state)
    {
        PaletteBorderEdge? inherit = GetInherit(state);

        return inherit?.GetBackImage(state) ?? Target?.GetBorderImage(style, state);
    }

    /// <summary>
    /// Gets the border image style.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public override PaletteImageStyle GetBorderImageStyle(PaletteBorderStyle style, PaletteState state)
    {
        PaletteBorderEdge? inherit = GetInherit(state);

        return inherit?.GetBackImageStyle(state) ?? Target?.GetBorderImageStyle(style, state) ?? PaletteImageStyle.Inherit;
    }

    /// <summary>
    /// Gets the image border alignment.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public override PaletteRectangleAlign GetBorderImageAlign(PaletteBorderStyle style, PaletteState state)
    {
        PaletteBorderEdge? inherit = GetInherit(state);

        return inherit?.GetBackImageAlign(state) ?? Target?.GetBorderImageAlign(style, state) ?? PaletteRectangleAlign.Inherit;
    }
    #endregion

    #region Implementation
    private PaletteBorderEdge? GetInherit(PaletteState state)
    {
        switch (state)
        {
            case PaletteState.Disabled:
                return _disabled;
            case PaletteState.Normal:
                return _normal;
            default:
                // Should never happen!
                Debug.Assert(false);
                throw DebugTools.NotImplemented(state.ToString());
        }
    }
    #endregion
}
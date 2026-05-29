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
public class PaletteRedirectBorder : PaletteRedirect
{
    #region Instance Fields
    private IPaletteBorder? _disabled;
    private IPaletteBorder? _normal;
    private IPaletteBorder? _pressed;
    private IPaletteBorder? _tracking;
    private IPaletteBorder? _checkedNormal;
    private IPaletteBorder? _checkedPressed;
    private IPaletteBorder? _checkedTracking;
    private IPaletteBorder? _focusOverride;
    private IPaletteBorder? _normalDefaultOverride;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteRedirectBorder class.
    /// </summary>
    /// <param name="target">Initial palette target for redirection.</param>
    public PaletteRedirectBorder(PaletteBase target)
        : this(target, null, null, null, null, null, null, null, null, null)
    {
    }

    /// <summary>
    /// Initialize a new instance of the PaletteRedirectBorder class.
    /// </summary>
    /// <param name="target">Initial palette target for redirection.</param>
    /// <param name="disabled">Redirection for disabled state requests.</param>
    /// <param name="normal">Redirection for normal state requests.</param>
    public PaletteRedirectBorder(PaletteBase target,
        IPaletteBorder disabled,
        IPaletteBorder normal)
        : this(target, disabled, normal, null, null, null, null, null, null, null)
    {
    }

    /// <summary>
    /// Initialize a new instance of the PaletteRedirectBorder class.
    /// </summary>
    /// <param name="target">Initial palette target for redirection.</param>
    /// <param name="disabled">Redirection for disabled state requests.</param>
    /// <param name="normal">Redirection for normal state requests.</param>
    /// <param name="pressed">Redirection for pressed state requests.</param>
    /// <param name="tracking">Redirection for tracking state requests.</param>
    public PaletteRedirectBorder(PaletteBase target,
        IPaletteBorder disabled,
        IPaletteBorder normal,
        IPaletteBorder pressed,
        IPaletteBorder tracking)
        : this(target, disabled, normal, pressed, tracking, null, null, null, null, null)
    {
    }

    /// <summary>
    /// Initialize a new instance of the PaletteRedirectBorder class.
    /// </summary>
    /// <param name="target">Initial palette target for redirection.</param>
    /// <param name="disabled">Redirection for disabled state requests.</param>
    /// <param name="normal">Redirection for normal state requests.</param>
    /// <param name="pressed">Redirection for pressed state requests.</param>
    /// <param name="tracking">Redirection for tracking state requests.</param>
    /// <param name="checkedNormal">Redirection for checked normal state requests.</param>
    /// <param name="checkedPressed">Redirection for checked pressed state requests.</param>
    /// <param name="checkedTracking">Redirection for checked tracking state requests.</param>
    /// <param name="focusOverride">Redirection for focus override state requests.</param>
    /// <param name="normalDefaultOverride">Redirection for normal default override state requests.</param>
    public PaletteRedirectBorder(PaletteBase? target,
        IPaletteBorder? disabled,
        IPaletteBorder? normal,
        IPaletteBorder? pressed,
        IPaletteBorder? tracking,
        IPaletteBorder? checkedNormal,
        IPaletteBorder? checkedPressed,
        IPaletteBorder? checkedTracking,
        IPaletteBorder? focusOverride,
        IPaletteBorder? normalDefaultOverride)
        : base(target)
    {
        // Remember state specific inheritance
        _disabled = disabled;
        _normal = normal;
        _pressed = pressed;
        _tracking = tracking;
        _checkedNormal = checkedNormal;
        _checkedPressed = checkedPressed;
        _checkedTracking = checkedTracking;
        _focusOverride = focusOverride;
        _normalDefaultOverride = normalDefaultOverride;
    }
    #endregion

    #region SetRedirectStates
    /// <summary>
    /// Set the redirection states.
    /// </summary>
    /// <param name="disabled">Redirection for disabled state requests.</param>
    /// <param name="normal">Redirection for normal state requests.</param>
    public virtual void SetRedirectStates(IPaletteBorder disabled,
        IPaletteBorder normal)
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
        _pressed = null;
        _tracking = null;
        _checkedNormal = null;
        _checkedPressed = null;
        _checkedTracking = null;
        _focusOverride = null;
        _normalDefaultOverride = null;
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
        IPaletteBorder? inherit = GetInherit(state);

        return inherit?.GetBorderDraw(state) ?? Target!.GetBorderDraw(style, state);
    }

    /// <summary>
    /// Gets a value indicating which borders to draw.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteDrawBorders value.</returns>
    public override PaletteDrawBorders GetBorderDrawBorders(PaletteBorderStyle style, PaletteState state)
    {
        IPaletteBorder? inherit = GetInherit(state);

        return inherit?.GetBorderDrawBorders(state) ?? Target!.GetBorderDrawBorders(style, state);
    }

    /// <summary>
    /// Gets the graphics drawing hint for the border.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteGraphicsHint value.</returns>
    public override PaletteGraphicsHint GetBorderGraphicsHint(PaletteBorderStyle style, PaletteState state)
    {
        IPaletteBorder? inherit = GetInherit(state);

        return inherit?.GetBorderGraphicsHint(state) ?? Target!.GetBorderGraphicsHint(style, state);
    }

    /// <summary>
    /// Gets the first border color.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetBorderColor1(PaletteBorderStyle style, PaletteState state)
    {
        IPaletteBorder? inherit = GetInherit(state);

        return inherit?.GetBorderColor1(state) ?? Target!.GetBorderColor1(style, state);
    }

    /// <summary>
    /// Gets the second border color.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetBorderColor2(PaletteBorderStyle style, PaletteState state)
    {
        IPaletteBorder? inherit = GetInherit(state);

        return inherit?.GetBorderColor2(state) ?? Target!.GetBorderColor2(style, state);
    }

    /// <summary>
    /// Gets the color border drawing style.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public override PaletteColorStyle GetBorderColorStyle(PaletteBorderStyle style, PaletteState state)
    {
        IPaletteBorder? inherit = GetInherit(state);

        return inherit?.GetBorderColorStyle(state) ?? Target!.GetBorderColorStyle(style, state);
    }

    /// <summary>
    /// Gets the color border alignment.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public override PaletteRectangleAlign GetBorderColorAlign(PaletteBorderStyle style, PaletteState state)
    {
        IPaletteBorder? inherit = GetInherit(state);

        return inherit?.GetBorderColorAlign(state) ?? Target!.GetBorderColorAlign(style, state);
    }

    /// <summary>
    /// Gets the color border angle.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public override float GetBorderColorAngle(PaletteBorderStyle style, PaletteState state)
    {
        IPaletteBorder? inherit = GetInherit(state);

        return inherit?.GetBorderColorAngle(state) ?? Target!.GetBorderColorAngle(style, state);
    }

    /// <summary>
    /// Gets the border width.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Integer width.</returns>
    public override int GetBorderWidth(PaletteBorderStyle style, PaletteState state)
    {
        IPaletteBorder? inherit = GetInherit(state);

        return inherit?.GetBorderWidth(state) ?? Target!.GetBorderWidth(style, state);
    }

    /// <summary>
    /// Gets the border corner rounding.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Float rounding.</returns>
    public override float GetBorderRounding(PaletteBorderStyle style, PaletteState state)
    {
        IPaletteBorder? inherit = GetInherit(state);

        return inherit?.GetBorderRounding(state) ?? Target!.GetBorderRounding(style, state);
    }

    /// <summary>
    /// Gets a border image.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public override Image? GetBorderImage(PaletteBorderStyle style, PaletteState state)
    {
        IPaletteBorder? inherit = GetInherit(state);

        return inherit?.GetBorderImage(state) ?? Target!.GetBorderImage(style, state);
    }

    /// <summary>
    /// Gets the border image style.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public override PaletteImageStyle GetBorderImageStyle(PaletteBorderStyle style, PaletteState state)
    {
        IPaletteBorder? inherit = GetInherit(state);

        return inherit?.GetBorderImageStyle(state) ?? Target!.GetBorderImageStyle(style, state);
    }

    /// <summary>
    /// Gets the image border alignment.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public override PaletteRectangleAlign GetBorderImageAlign(PaletteBorderStyle style, PaletteState state)
    {
        IPaletteBorder? inherit = GetInherit(state);

        return inherit?.GetBorderImageAlign(state) ?? Target!.GetBorderImageAlign(style, state);
    }
    #endregion

    #region Implementation
    private IPaletteBorder? GetInherit(PaletteState state)
    {
        switch (state)
        {
            case PaletteState.Disabled:
                return _disabled;
            case PaletteState.Normal:
                return _normal;
            case PaletteState.Pressed:
                return _pressed;
            case PaletteState.Tracking:
                return _tracking;
            case PaletteState.CheckedNormal:
                return _checkedNormal;
            case PaletteState.CheckedPressed:
                return _checkedPressed;
            case PaletteState.CheckedTracking:
                return _checkedTracking;
            case PaletteState.FocusOverride:
                return _focusOverride;
            case PaletteState.NormalDefaultOverride:
                return _normalDefaultOverride;
            default:
                // Should never happen!
                Debug.Assert(false);
                throw DebugTools.NotImplemented(state.ToString());
        }
    }
    #endregion
}
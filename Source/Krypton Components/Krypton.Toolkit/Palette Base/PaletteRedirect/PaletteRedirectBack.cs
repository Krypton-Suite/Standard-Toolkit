#region BSD License
/*
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Redirect background based on the incoming state of the request.
/// </summary>
public class PaletteRedirectBack : PaletteRedirect
{
    #region Instance Fields
    private IPaletteBack? _disabled;
    private IPaletteBack? _normal;
    private IPaletteBack? _pressed;
    private IPaletteBack? _tracking;
    private IPaletteBack? _checkedNormal;
    private IPaletteBack? _checkedPressed;
    private IPaletteBack? _checkedTracking;
    private IPaletteBack? _focusOverride;
    private IPaletteBack? _normalDefaultOverride;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteRedirectBack class.
    /// </summary>
    /// <param name="target">Initial palette target for redirection.</param>
    public PaletteRedirectBack(PaletteBase target)
        : this(target, null, null, null, null, null, null, null, null, null)
    {
    }

    /// <summary>
    /// Initialize a new instance of the PaletteRedirectBack class.
    /// </summary>
    /// <param name="target">Initial palette target for redirection.</param>
    /// <param name="disabled">Redirection for disabled state requests.</param>
    /// <param name="normal">Redirection for normal state requests.</param>
    public PaletteRedirectBack(PaletteBase target,
        IPaletteBack disabled,
        IPaletteBack normal)
        : this(target, disabled, normal, null, null, null, null, null, null, null)
    {
    }

    /// <summary>
    /// Initialize a new instance of the PaletteRedirectBack class.
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
    public PaletteRedirectBack(PaletteBase target,
        IPaletteBack? disabled,
        IPaletteBack? normal,
        IPaletteBack? pressed,
        IPaletteBack? tracking,
        IPaletteBack? checkedNormal,
        IPaletteBack? checkedPressed,
        IPaletteBack? checkedTracking,
        IPaletteBack? focusOverride,
        IPaletteBack? normalDefaultOverride)
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
    public virtual void SetRedirectStates(IPaletteBack disabled,
        IPaletteBack normal)
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

    #region Back
    /// <summary>
    /// Gets a value indicating if background should be drawn.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public override InheritBool GetBackDraw(PaletteBackStyle style, PaletteState state)
    {
        IPaletteBack? inherit = GetInherit(state);

        return inherit?.GetBackDraw(state) ?? Target!.GetBackDraw(style, state);
    }

    /// <summary>
    /// Gets the graphics drawing hint for the background.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteGraphicsHint value.</returns>
    public override PaletteGraphicsHint GetBackGraphicsHint(PaletteBackStyle style, PaletteState state)
    {
        IPaletteBack? inherit = GetInherit(state);

        return inherit?.GetBackGraphicsHint(state) ?? Target!.GetBackGraphicsHint(style, state);
    }

    /// <summary>
    /// Gets the first background color.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetBackColor1(PaletteBackStyle style, PaletteState state)
    {
        IPaletteBack? inherit = GetInherit(state);

        return inherit?.GetBackColor1(state) ?? Target!.GetBackColor1(style, state);
    }

    /// <summary>
    /// Gets the second back color.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetBackColor2(PaletteBackStyle style, PaletteState state)
    {
        IPaletteBack? inherit = GetInherit(state);

        return inherit?.GetBackColor2(state) ?? Target!.GetBackColor2(style, state);
    }

    /// <summary>
    /// Gets the color background drawing style.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public override PaletteColorStyle GetBackColorStyle(PaletteBackStyle style, PaletteState state)
    {
        IPaletteBack? inherit = GetInherit(state);

        return inherit?.GetBackColorStyle(state) ?? Target!.GetBackColorStyle(style, state);
    }

    /// <summary>
    /// Gets the color alignment.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public override PaletteRectangleAlign GetBackColorAlign(PaletteBackStyle style, PaletteState state)
    {
        IPaletteBack? inherit = GetInherit(state);

        return inherit?.GetBackColorAlign(state) ?? Target!.GetBackColorAlign(style, state);
    }

    /// <summary>
    /// Gets the color background angle.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public override float GetBackColorAngle(PaletteBackStyle style, PaletteState state)
    {
        IPaletteBack? inherit = GetInherit(state);

        return inherit?.GetBackColorAngle(state) ?? Target!.GetBackColorAngle(style, state);
    }

    /// <summary>
    /// Gets a background image.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public override Image? GetBackImage(PaletteBackStyle style, PaletteState state)
    {
        IPaletteBack? inherit = GetInherit(state);

        return inherit?.GetBackImage(state) ?? Target!.GetBackImage(style, state);
    }

    /// <summary>
    /// Gets the background image style.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public override PaletteImageStyle GetBackImageStyle(PaletteBackStyle style, PaletteState state)
    {
        IPaletteBack? inherit = GetInherit(state);

        return inherit?.GetBackImageStyle(state) ?? Target!.GetBackImageStyle(style, state);
    }

    /// <summary>
    /// Gets the image alignment.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public override PaletteRectangleAlign GetBackImageAlign(PaletteBackStyle style, PaletteState state)
    {
        IPaletteBack? inherit = GetInherit(state);

        return inherit?.GetBackImageAlign(state) ?? Target!.GetBackImageAlign(style, state);
    }
    #endregion

    #region Implementation
    private IPaletteBack? GetInherit(PaletteState state)
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
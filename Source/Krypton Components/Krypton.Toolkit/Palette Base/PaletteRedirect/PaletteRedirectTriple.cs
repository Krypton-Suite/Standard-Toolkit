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
/// Redirect back/border/content based on the incoming state of the request.
/// </summary>
public class PaletteRedirectTriple : PaletteRedirect
{
    #region Instance Fields
    private IPaletteTriple? _disabled;
    private IPaletteTriple? _normal;
    private IPaletteTriple? _pressed;
    private IPaletteTriple? _tracking;
    private IPaletteTriple? _checkedNormal;
    private IPaletteTriple? _checkedPressed;
    private IPaletteTriple? _checkedTracking;
    private IPaletteTriple? _focusOverride;
    private IPaletteTriple? _normalDefaultOverride;
    private readonly IPaletteTriple? _contextNormal;
    private readonly IPaletteTriple? _contextPressed;
    private readonly IPaletteTriple? _contextTracking;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteRedirectTriple class.
    /// </summary>
    public PaletteRedirectTriple()
        : this(null, null, null, null, null, null, null, null, null, null)
    {
    }

    /// <summary>
    /// Initialize a new instance of the PaletteRedirectTriple class.
    /// </summary>
    /// <param name="target">Initial palette target for redirection.</param>
    public PaletteRedirectTriple(PaletteBase target)
        : this(target, null, null, null, null, null, null, null, null, null)
    {
    }

    /// <summary>
    /// Initialize a new instance of the PaletteRedirectTriple class.
    /// </summary>
    /// <param name="target">Initial palette target for redirection.</param>
    /// <param name="disabled">Redirection for disabled state requests.</param>
    /// <param name="normal">Redirection for normal state requests.</param>
    public PaletteRedirectTriple(PaletteBase target,
        IPaletteTriple? disabled,
        IPaletteTriple? normal)
        : this(target, disabled, normal, null, null, null, null, null, null, null)
    {
    }

    /// <summary>
    /// Initialize a new instance of the PaletteRedirectTriple class.
    /// </summary>
    /// <param name="target">Initial palette target for redirection.</param>
    /// <param name="disabled">Redirection for disabled state requests.</param>
    /// <param name="normal">Redirection for normal state requests.</param>
    /// <param name="tracking">Redirection for tracking state requests.</param>
    public PaletteRedirectTriple(PaletteBase target,
        IPaletteTriple? disabled,
        IPaletteTriple? normal,
        IPaletteTriple? tracking)
        : this(target, disabled, normal, null, tracking, null, null, null, null, null)
    {
    }

    /// <summary>
    /// Initialize a new instance of the PaletteRedirectTriple class.
    /// </summary>
    /// <param name="target">Initial palette target for redirection.</param>
    /// <param name="disabled">Redirection for disabled state requests.</param>
    /// <param name="normal">Redirection for normal state requests.</param>
    /// <param name="tracking">Redirection for tracking state requests.</param>
    /// <param name="pressed">Redirection for pressed state requests.</param>
    /// <param name="contextNormal">Redirection for context normal state requests.</param>
    /// <param name="contextPressed">Redirection for context pressed state requests.</param>
    /// <param name="contextTracking">Redirection for context tracking state requests.</param>
    public PaletteRedirectTriple(PaletteBase target,
        IPaletteTriple? disabled,
        IPaletteTriple? normal,
        IPaletteTriple? tracking,
        IPaletteTriple? pressed,
        IPaletteTriple? contextNormal,
        IPaletteTriple? contextPressed,
        IPaletteTriple? contextTracking)
        : this(target, disabled, normal, pressed, tracking, null, null, null, null, null)
    {
        _contextNormal = contextNormal;
        _contextPressed = contextPressed;
        _contextTracking = contextTracking;
    }

    /// <summary>
    /// Initialize a new instance of the PaletteRedirectTriple class.
    /// </summary>
    /// <param name="target">Initial palette target for redirection.</param>
    /// <param name="disabled">Redirection for disabled state requests.</param>
    /// <param name="normal">Redirection for normal state requests.</param>
    /// <param name="pressed">Redirection for pressed state requests.</param>
    /// <param name="tracking">Redirection for tracking state requests.</param>
    /// <param name="selected">Redirection for all checked states.</param>
    /// <param name="focusOverride">Redirection for focus override state requests.</param>
    public PaletteRedirectTriple(PaletteBase target,
        IPaletteTriple? disabled,
        IPaletteTriple? normal,
        IPaletteTriple? pressed,
        IPaletteTriple? tracking,
        IPaletteTriple? selected,
        IPaletteTriple? focusOverride)
        : this(target, disabled, normal, pressed, tracking, selected,
            selected, selected, focusOverride, null)
    {
    }

    /// <summary>
    /// Initialize a new instance of the PaletteRedirectTriple class.
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
    public PaletteRedirectTriple(PaletteBase? target,
        IPaletteTriple? disabled,
        IPaletteTriple? normal,
        IPaletteTriple? pressed,
        IPaletteTriple? tracking,
        IPaletteTriple? checkedNormal,
        IPaletteTriple? checkedPressed,
        IPaletteTriple? checkedTracking,
        IPaletteTriple? focusOverride,
        IPaletteTriple? normalDefaultOverride)
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
    public virtual void SetRedirectStates(IPaletteTriple disabled,
        IPaletteTriple normal)
    {
        _disabled = disabled;
        _normal = normal;
    }

    /// <summary>
    /// Set the redirection states.
    /// </summary>
    /// <param name="disabled">Redirection for disabled state requests.</param>
    /// <param name="normal">Redirection for normal state requests.</param>
    /// <param name="pressed">Redirection for pressed state requests.</param>
    /// <param name="tracking">Redirection for tracking state requests.</param>
    /// <param name="checkedNormal">Redirection for checked normal state requests.</param>
    /// <param name="checkedPressed">Redirection for checked pressed state requests.</param>
    /// <param name="checkedTracking">Redirection for checked tracking state requests.</param>
    /// <param name="focusOverride">Redirection for focus override state requests.</param>
    /// <param name="normalDefaultOverride">Redirection for normal default override state requests.</param>
    public virtual void SetRedirectStates(IPaletteTriple disabled,
        IPaletteTriple normal,
        IPaletteTriple pressed,
        IPaletteTriple tracking,
        IPaletteTriple checkedNormal,
        IPaletteTriple checkedPressed,
        IPaletteTriple checkedTracking,
        IPaletteTriple focusOverride,
        IPaletteTriple normalDefaultOverride)
    {
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
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null 
            ? inherit.PaletteBack.GetBackDraw(state) 
            : Target!.GetBackDraw(style, state);
    }

    /// <summary>
    /// Gets the graphics drawing hint for the background.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteGraphicsHint value.</returns>
    public override PaletteGraphicsHint GetBackGraphicsHint(PaletteBackStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null 
            ? inherit.PaletteBack.GetBackGraphicsHint(state) 
            : Target!.GetBackGraphicsHint(style, state);
    }

    /// <summary>
    /// Gets the first background color.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetBackColor1(PaletteBackStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteBack.GetBackColor1(state)
            : Target!.GetBackColor1(style, state);
    }

    /// <summary>
    /// Gets the second back color.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetBackColor2(PaletteBackStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteBack.GetBackColor2(state)
            : Target!.GetBackColor2(style, state);
    }

    /// <summary>
    /// Gets the color background drawing style.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public override PaletteColorStyle GetBackColorStyle(PaletteBackStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteBack.GetBackColorStyle(state)
            : Target!.GetBackColorStyle(style, state);
    }

    /// <summary>
    /// Gets the color alignment.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public override PaletteRectangleAlign GetBackColorAlign(PaletteBackStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteBack.GetBackColorAlign(state)
            : Target!.GetBackColorAlign(style, state);
    }

    /// <summary>
    /// Gets the color background angle.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public override float GetBackColorAngle(PaletteBackStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteBack.GetBackColorAngle(state) 
            : Target!.GetBackColorAngle(style, state);
    }

    /// <summary>
    /// Gets a background image.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public override Image? GetBackImage(PaletteBackStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteBack.GetBackImage(state)
            : Target!.GetBackImage(style, state);
    }

    /// <summary>
    /// Gets the background image style.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public override PaletteImageStyle GetBackImageStyle(PaletteBackStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteBack.GetBackImageStyle(state)
            : Target!.GetBackImageStyle(style, state);
    }

    /// <summary>
    /// Gets the image alignment.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public override PaletteRectangleAlign GetBackImageAlign(PaletteBackStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteBack.GetBackImageAlign(state)
            : Target!.GetBackImageAlign(style, state);
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
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteBorder!.GetBorderDraw(state)
            : Target!.GetBorderDraw(style, state);
    }

    /// <summary>
    /// Gets a value indicating which borders to draw.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteDrawBorders value.</returns>
    public override PaletteDrawBorders GetBorderDrawBorders(PaletteBorderStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteBorder!.GetBorderDrawBorders(state)
            : Target!.GetBorderDrawBorders(style, state);
    }

    /// <summary>
    /// Gets the graphics drawing hint for the border.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteGraphicsHint value.</returns>
    public override PaletteGraphicsHint GetBorderGraphicsHint(PaletteBorderStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteBorder!.GetBorderGraphicsHint(state)
            : Target!.GetBorderGraphicsHint(style, state);
    }

    /// <summary>
    /// Gets the first border color.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetBorderColor1(PaletteBorderStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteBorder!.GetBorderColor1(state)
            : Target!.GetBorderColor1(style, state);
    }

    /// <summary>
    /// Gets the second border color.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetBorderColor2(PaletteBorderStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteBorder!.GetBorderColor2(state)
            : Target!.GetBorderColor2(style, state);
    }

    /// <summary>
    /// Gets the color border drawing style.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public override PaletteColorStyle GetBorderColorStyle(PaletteBorderStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteBorder!.GetBorderColorStyle(state)
            : Target!.GetBorderColorStyle(style, state);
    }

    /// <summary>
    /// Gets the color border alignment.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public override PaletteRectangleAlign GetBorderColorAlign(PaletteBorderStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteBorder!.GetBorderColorAlign(state)
            : Target!.GetBorderColorAlign(style, state);
    }

    /// <summary>
    /// Gets the color border angle.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public override float GetBorderColorAngle(PaletteBorderStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteBorder!.GetBorderColorAngle(state)
            : Target!.GetBorderColorAngle(style, state);
    }

    /// <summary>
    /// Gets the border width.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Integer width.</returns>
    public override int GetBorderWidth(PaletteBorderStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteBorder!.GetBorderWidth(state)
            : Target!.GetBorderWidth(style, state);
    }

    /// <summary>
    /// Gets the border corner rounding.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Float rounding.</returns>
    public override float GetBorderRounding(PaletteBorderStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteBorder!.GetBorderRounding(state)
            : Target!.GetBorderRounding(style, state);
    }

    /// <summary>
    /// Gets a border image.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public override Image? GetBorderImage(PaletteBorderStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteBorder!.GetBorderImage(state)
            : Target!.GetBorderImage(style, state);
    }

    /// <summary>
    /// Gets the border image style.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public override PaletteImageStyle GetBorderImageStyle(PaletteBorderStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteBorder!.GetBorderImageStyle(state)
            : Target!.GetBorderImageStyle(style, state);
    }

    /// <summary>
    /// Gets the image border alignment.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public override PaletteRectangleAlign GetBorderImageAlign(PaletteBorderStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteBorder!.GetBorderImageAlign(state)
            : Target!.GetBorderImageAlign(style, state);
    }
    #endregion

    #region Content
    /// <summary>
    /// Gets a value indicating if content should be drawn.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public override InheritBool GetContentDraw(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteContent!.GetContentDraw(state)
            : Target!.GetContentDraw(style, state);
    }

    /// <summary>
    /// Gets a value indicating if content should be drawn with focus indication.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public override InheritBool GetContentDrawFocus(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteContent!.GetContentDrawFocus(state)
            : Target!.GetContentDrawFocus(style, state);
    }

    /// <summary>
    /// Gets the horizontal relative alignment of the image.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentImageH(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteContent!.GetContentImageH(state)
            : Target!.GetContentImageH(style, state);
    }

    /// <summary>
    /// Gets the vertical relative alignment of the image.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentImageV(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteContent!.GetContentImageV(state)
            : Target!.GetContentImageV(style, state);
    }

    /// <summary>
    /// Gets the effect applied to drawing of the image.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteImageEffect value.</returns>
    public override PaletteImageEffect GetContentImageEffect(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteContent!.GetContentImageEffect(state)
            : Target!.GetContentImageEffect(style, state);
    }

    /// <summary>
    /// Gets the image color to remap into another color.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentImageColorMap(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteContent!.GetContentImageColorMap(state)
            : Target!.GetContentImageColorMap(style, state);
    }

    /// <summary>
    /// Gets the color to use in place of the image map color.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentImageColorTo(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteContent!.GetContentImageColorTo(state)
            : Target!.GetContentImageColorTo(style, state);
    }

    /// <summary>
    /// Gets the font for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public override Font? GetContentShortTextFont(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteContent!.GetContentShortTextFont(state)
            : Target!.GetContentShortTextFont(style, state);
    }

    /// <summary>
    /// Gets the rendering hint for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextHint value.</returns>
    public override PaletteTextHint GetContentShortTextHint(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteContent!.GetContentShortTextHint(state) 
            : Target!.GetContentShortTextHint(style, state);
    }

    /// <summary>
    /// Gets the prefix drawing setting for short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextPrefix value.</returns>
    public override PaletteTextHotkeyPrefix GetContentShortTextPrefix(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteContent!.GetContentShortTextPrefix(state)
            : Target!.GetContentShortTextPrefix(style, state);
    }

    /// <summary>
    /// Gets the flag indicating if multiline text is allowed for short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public override InheritBool GetContentShortTextMultiLine(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteContent!.GetContentShortTextMultiLine(state)
            : Target!.GetContentShortTextMultiLine(style, state);
    }

    /// <summary>
    /// Gets the text trimming to use for short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextTrim value.</returns>
    public override PaletteTextTrim GetContentShortTextTrim(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteContent!.GetContentShortTextTrim(state)
            : Target!.GetContentShortTextTrim(style, state);
    }

    /// <summary>
    /// Gets the horizontal relative alignment of the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentShortTextH(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteContent!.GetContentShortTextH(state)
            : Target!.GetContentShortTextH(style, state);
    }

    /// <summary>
    /// Gets the vertical relative alignment of the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentShortTextV(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteContent!.GetContentShortTextV(state)
            : Target!.GetContentShortTextV(style, state);
    }

    /// <summary>
    /// Gets the horizontal relative alignment of multiline short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentShortTextMultiLineH(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteContent!.GetContentShortTextMultiLineH(state)
            : Target!.GetContentShortTextMultiLineH(style, state);
    }

    /// <summary>
    /// Gets the first back color for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentShortTextColor1(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteContent!.GetContentShortTextColor1(state)
            : Target!.GetContentShortTextColor1(style, state);
    }

    /// <summary>
    /// Gets the second back color for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentShortTextColor2(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteContent!.GetContentShortTextColor2(state)
            : Target!.GetContentShortTextColor2(style, state);
    }

    /// <summary>
    /// Gets the color drawing style for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public override PaletteColorStyle GetContentShortTextColorStyle(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteContent!.GetContentShortTextColorStyle(state)
            : Target!.GetContentShortTextColorStyle(style, state);
    }

    /// <summary>
    /// Gets the color alignment for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public override PaletteRectangleAlign GetContentShortTextColorAlign(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteContent!.GetContentShortTextColorAlign(state)
            : Target!.GetContentShortTextColorAlign(style, state);
    }

    /// <summary>
    /// Gets the color background angle for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public override float GetContentShortTextColorAngle(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteContent!.GetContentShortTextColorAngle(state)
            : Target!.GetContentShortTextColorAngle(style, state);
    }

    /// <summary>
    /// Gets a background image for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public override Image? GetContentShortTextImage(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteContent!.GetContentShortTextImage(state)
            : Target!.GetContentShortTextImage(style, state);
    }

    /// <summary>
    /// Gets the background image style.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public override PaletteImageStyle GetContentShortTextImageStyle(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteContent!.GetContentShortTextImageStyle(state)
            : Target!.GetContentShortTextImageStyle(style, state);
    }

    /// <summary>
    /// Gets the image alignment for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public override PaletteRectangleAlign GetContentShortTextImageAlign(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteContent!.GetContentShortTextImageAlign(state)
            : Target!.GetContentShortTextImageAlign(style, state);
    }

    /// <summary>
    /// Gets the font for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public override Font? GetContentLongTextFont(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteContent!.GetContentLongTextFont(state)
            : Target!.GetContentLongTextFont(style, state);
    }

    /// <summary>
    /// Gets the rendering hint for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextHint value.</returns>
    public override PaletteTextHint GetContentLongTextHint(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteContent!.GetContentLongTextHint(state)
            : Target!.GetContentLongTextHint(style, state);
    }

    /// <summary>
    /// Gets the flag indicating if multiline text is allowed for long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public override InheritBool GetContentLongTextMultiLine(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteContent!.GetContentLongTextMultiLine(state)
            : Target!.GetContentLongTextMultiLine(style, state);
    }

    /// <summary>
    /// Gets the text trimming to use for long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextTrim value.</returns>
    public override PaletteTextTrim GetContentLongTextTrim(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null 
            ? inherit.PaletteContent!.GetContentLongTextTrim(state) 
            : Target!.GetContentLongTextTrim(style, state);
    }

    /// <summary>
    /// Gets the prefix drawing setting for long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextPrefix value.</returns>
    public override PaletteTextHotkeyPrefix GetContentLongTextPrefix(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteContent!.GetContentLongTextPrefix(state)
            : Target!.GetContentLongTextPrefix(style, state);
    }

    /// <summary>
    /// Gets the horizontal relative alignment of the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentLongTextH(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteContent!.GetContentLongTextH(state)
            : Target!.GetContentLongTextH(style, state);
    }

    /// <summary>
    /// Gets the vertical relative alignment of the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentLongTextV(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteContent!.GetContentLongTextV(state)
            : Target!.GetContentLongTextV(style, state);
    }

    /// <summary>
    /// Gets the horizontal relative alignment of multiline long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentLongTextMultiLineH(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteContent!.GetContentLongTextMultiLineH(state)
            : Target!.GetContentLongTextMultiLineH(style, state);
    }

    /// <summary>
    /// Gets the first back color for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentLongTextColor1(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteContent!.GetContentLongTextColor1(state)
            : Target!.GetContentLongTextColor1(style, state);
    }

    /// <summary>
    /// Gets the second back color for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentLongTextColor2(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteContent!.GetContentLongTextColor2(state)
            : Target!.GetContentLongTextColor2(style, state);
    }

    /// <summary>
    /// Gets the color drawing style for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public override PaletteColorStyle GetContentLongTextColorStyle(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteContent!.GetContentLongTextColorStyle(state)
            : Target!.GetContentLongTextColorStyle(style, state);
    }

    /// <summary>
    /// Gets the color alignment for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public override PaletteRectangleAlign GetContentLongTextColorAlign(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteContent!.GetContentLongTextColorAlign(state)
            : Target!.GetContentLongTextColorAlign(style, state);
    }

    /// <summary>
    /// Gets the color background angle for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public override float GetContentLongTextColorAngle(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteContent!.GetContentLongTextColorAngle(state)
            : Target!.GetContentLongTextColorAngle(style, state);
    }

    /// <summary>
    /// Gets a background image for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public override Image? GetContentLongTextImage(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteContent!.GetContentLongTextImage(state)
            : Target!.GetContentLongTextImage(style, state);
    }

    /// <summary>
    /// Gets the background image style for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public override PaletteImageStyle GetContentLongTextImageStyle(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteContent!.GetContentLongTextImageStyle(state)
            : Target!.GetContentLongTextImageStyle(style, state);
    }

    /// <summary>
    /// Gets the image alignment for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public override PaletteRectangleAlign GetContentLongTextImageAlign(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteContent!.GetContentLongTextImageAlign(state)
            : Target!.GetContentLongTextImageAlign(style, state);
    }

    /// <summary>
    /// Gets the padding between the border and content drawing.
    /// </summary>
    /// <param name="owningForm"></param>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Padding value.</returns>
    public override Padding GetBorderContentPadding(KryptonForm? owningForm, PaletteContentStyle style,
        PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null
            ? inherit.PaletteContent!.GetBorderContentPadding(owningForm, state)
            : Target!.GetBorderContentPadding(owningForm, style, state);
    }

    /// <summary>
    /// Gets the padding between adjacent content items.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Integer value.</returns>
    public override int GetContentAdjacentGap(PaletteContentStyle style, PaletteState state)
    {
        IPaletteTriple? inherit = GetInherit(state);

        return inherit != null 
            ? inherit.PaletteContent!.GetContentAdjacentGap(state) 
            : Target!.GetContentAdjacentGap(style, state);
    }
    #endregion

    #region Implementation
    private IPaletteTriple? GetInherit(PaletteState state)
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
            case PaletteState.ContextNormal:
                return _contextNormal;
            case PaletteState.ContextPressed:
                return _contextPressed;
            case PaletteState.ContextTracking:
                return _contextTracking;
            default:
                // Should never happen!
                Debug.Assert(false);
                throw DebugTools.NotImplemented(state.ToString());
        }
    }
    #endregion
}
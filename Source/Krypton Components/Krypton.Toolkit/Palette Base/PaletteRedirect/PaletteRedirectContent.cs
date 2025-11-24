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
/// Redirect content based on the incoming state of the request.
/// </summary>
public class PaletteRedirectContent : PaletteRedirect
{
    #region Instance Fields
    private IPaletteContent? _disabled;
    private IPaletteContent? _normal;
    private IPaletteContent? _pressed;
    private IPaletteContent? _tracking;
    private IPaletteContent? _checkedNormal;
    private IPaletteContent? _checkedPressed;
    private IPaletteContent? _checkedTracking;
    private IPaletteContent? _focusOverride;
    private IPaletteContent? _normalDefaultOverride;
    private IPaletteContent? _linkVisitedOverride;
    private IPaletteContent? _linkNotVisitedOverride;
    private IPaletteContent? _linkPressedOverride;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteRedirectContent class.
    /// </summary>
    public PaletteRedirectContent()
        : this(null, null, null, null, null, null, null, null, null, null, null, null, null)
    {
    }

    /// <summary>
    /// Initialize a new instance of the PaletteRedirectContent class.
    /// </summary>
    /// <param name="target">Initial palette target for redirection.</param>
    /// <param name="disabled">Redirection for disabled state requests.</param>
    /// <param name="normal">Redirection for normal state requests.</param>
    public PaletteRedirectContent(PaletteBase target,
        IPaletteContent disabled,
        IPaletteContent normal)
        : this(target, disabled, normal, null, null, null, null, null, null, null, null, null, null)
    {
    }

    /// <summary>
    /// Initialize a new instance of the PaletteRedirectContent class.
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
    /// <param name="linkVisitedOverride">Redirection for link visited override state requests.</param>
    /// <param name="linkNotVisitedOverride">Redirection for link not visited override state requests.</param>
    /// <param name="linkPressedOverride">Redirection for link pressed override state requests.</param>
    public PaletteRedirectContent(PaletteBase? target,
        IPaletteContent? disabled,
        IPaletteContent? normal,
        IPaletteContent? pressed,
        IPaletteContent? tracking,
        IPaletteContent? checkedNormal,
        IPaletteContent? checkedPressed,
        IPaletteContent? checkedTracking,
        IPaletteContent? focusOverride,
        IPaletteContent? normalDefaultOverride,
        IPaletteContent? linkVisitedOverride,
        IPaletteContent? linkNotVisitedOverride,
        IPaletteContent? linkPressedOverride)
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
        _linkVisitedOverride = linkVisitedOverride;
        _linkNotVisitedOverride = linkNotVisitedOverride;
        _linkPressedOverride = linkPressedOverride;
    }
    #endregion

    #region SetRedirectStates
    /// <summary>
    /// Set the redirection states.
    /// </summary>
    /// <param name="disabled">Redirection for disabled state requests.</param>
    /// <param name="normal">Redirection for normal state requests.</param>
    public virtual void SetRedirectStates(IPaletteContent disabled,
        IPaletteContent normal)
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
    /// <param name="linkVisitedOverride">Redirection for link visited override state requests.</param>
    /// <param name="linkNotVisitedOverride">Redirection for link not visited override state requests.</param>
    /// <param name="linkPressedOverride">Redirection for link pressed override state requests.</param>
    public virtual void SetRedirectStates(IPaletteContent disabled,
        IPaletteContent normal,
        IPaletteContent pressed,
        IPaletteContent tracking,
        IPaletteContent checkedNormal,
        IPaletteContent checkedPressed,
        IPaletteContent checkedTracking,
        IPaletteContent focusOverride,
        IPaletteContent normalDefaultOverride,
        IPaletteContent linkVisitedOverride,
        IPaletteContent linkNotVisitedOverride,
        IPaletteContent linkPressedOverride)
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
        _linkVisitedOverride = linkVisitedOverride;
        _linkNotVisitedOverride = linkNotVisitedOverride;
        _linkPressedOverride = linkPressedOverride;
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
        _linkVisitedOverride = null;
        _linkNotVisitedOverride = null;
        _linkPressedOverride = null;
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
        IPaletteContent? inherit = GetInherit(state);

        return inherit?.GetContentDraw(state) ?? Target!.GetContentDraw(style, state);
    }

    /// <summary>
    /// Gets a value indicating if content should be drawn with focus indication.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public override InheritBool GetContentDrawFocus(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetInherit(state);

        return inherit?.GetContentDrawFocus(state) ?? Target!.GetContentDrawFocus(style, state);
    }

    /// <summary>
    /// Gets the horizontal relative alignment of the image.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentImageH(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetInherit(state);

        return inherit?.GetContentImageH(state) ?? Target!.GetContentImageH(style, state);
    }

    /// <summary>
    /// Gets the vertical relative alignment of the image.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentImageV(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetInherit(state);

        return inherit?.GetContentImageV(state) ?? Target!.GetContentImageV(style, state);
    }

    /// <summary>
    /// Gets the effect applied to drawing of the image.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteImageEffect value.</returns>
    public override PaletteImageEffect GetContentImageEffect(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetInherit(state);

        return inherit?.GetContentImageEffect(state) ?? Target!.GetContentImageEffect(style, state);
    }

    /// <summary>
    /// Gets the font for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public override Font? GetContentShortTextFont(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetInherit(state);

        return inherit?.GetContentShortTextFont(state) ?? Target!.GetContentShortTextFont(style, state);
    }

    /// <summary>
    /// Gets the rendering hint for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextHint value.</returns>
    public override PaletteTextHint GetContentShortTextHint(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetInherit(state);

        return inherit?.GetContentShortTextHint(state) ?? Target!.GetContentShortTextHint(style, state);
    }

    /// <summary>
    /// Gets the prefix drawing setting for short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextPrefix value.</returns>
    public override PaletteTextHotkeyPrefix GetContentShortTextPrefix(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetInherit(state);

        return inherit?.GetContentShortTextPrefix(state) ?? Target!.GetContentShortTextPrefix(style, state);
    }

    /// <summary>
    /// Gets the flag indicating if multiline text is allowed for short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public override InheritBool GetContentShortTextMultiLine(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetInherit(state);

        return inherit?.GetContentShortTextMultiLine(state) ?? Target!.GetContentShortTextMultiLine(style, state);
    }

    /// <summary>
    /// Gets the text trimming to use for short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextTrim value.</returns>
    public override PaletteTextTrim GetContentShortTextTrim(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetInherit(state);

        return inherit?.GetContentShortTextTrim(state) ?? Target!.GetContentShortTextTrim(style, state);
    }

    /// <summary>
    /// Gets the horizontal relative alignment of the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentShortTextH(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetInherit(state);

        return inherit?.GetContentShortTextH(state) ?? Target!.GetContentShortTextH(style, state);
    }

    /// <summary>
    /// Gets the vertical relative alignment of the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentShortTextV(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetInherit(state);

        return inherit?.GetContentShortTextV(state) ?? Target!.GetContentShortTextV(style, state);
    }

    /// <summary>
    /// Gets the horizontal relative alignment of multiline short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentShortTextMultiLineH(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetInherit(state);

        return inherit?.GetContentShortTextMultiLineH(state) ?? Target!.GetContentShortTextMultiLineH(style, state);
    }

    /// <summary>
    /// Gets the first back color for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentShortTextColor1(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetInherit(state);

        return inherit?.GetContentShortTextColor1(state) ?? Target!.GetContentShortTextColor1(style, state);
    }

    /// <summary>
    /// Gets the second back color for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentShortTextColor2(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetInherit(state);

        return inherit?.GetContentShortTextColor2(state) ?? Target!.GetContentShortTextColor2(style, state);
    }

    /// <summary>
    /// Gets the color drawing style for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public override PaletteColorStyle GetContentShortTextColorStyle(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetInherit(state);

        return inherit?.GetContentShortTextColorStyle(state) ?? Target!.GetContentShortTextColorStyle(style, state);
    }

    /// <summary>
    /// Gets the color alignment for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public override PaletteRectangleAlign GetContentShortTextColorAlign(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetInherit(state);

        return inherit?.GetContentShortTextColorAlign(state) ?? Target!.GetContentShortTextColorAlign(style, state);
    }

    /// <summary>
    /// Gets the color background angle for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public override float GetContentShortTextColorAngle(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetInherit(state);

        return inherit?.GetContentShortTextColorAngle(state) ?? Target!.GetContentShortTextColorAngle(style, state);
    }

    /// <summary>
    /// Gets a background image for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public override Image? GetContentShortTextImage(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetInherit(state);

        return inherit?.GetContentShortTextImage(state) ?? Target!.GetContentShortTextImage(style, state);
    }

    /// <summary>
    /// Gets the background image style.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public override PaletteImageStyle GetContentShortTextImageStyle(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetInherit(state);

        return inherit?.GetContentShortTextImageStyle(state) ?? Target!.GetContentShortTextImageStyle(style, state);
    }

    /// <summary>
    /// Gets the image alignment for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public override PaletteRectangleAlign GetContentShortTextImageAlign(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetInherit(state);

        return inherit?.GetContentShortTextImageAlign(state) ?? Target!.GetContentShortTextImageAlign(style, state);
    }

    /// <summary>
    /// Gets the font for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public override Font? GetContentLongTextFont(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetInherit(state);

        return inherit?.GetContentLongTextFont(state) ?? Target!.GetContentLongTextFont(style, state);
    }

    /// <summary>
    /// Gets the rendering hint for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextHint value.</returns>
    public override PaletteTextHint GetContentLongTextHint(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetInherit(state);

        return inherit?.GetContentLongTextHint(state) ?? Target!.GetContentLongTextHint(style, state);
    }

    /// <summary>
    /// Gets the flag indicating if multiline text is allowed for long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public override InheritBool GetContentLongTextMultiLine(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetInherit(state);

        return inherit?.GetContentLongTextMultiLine(state) ?? Target!.GetContentLongTextMultiLine(style, state);
    }

    /// <summary>
    /// Gets the text trimming to use for long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextTrim value.</returns>
    public override PaletteTextTrim GetContentLongTextTrim(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetInherit(state);

        return inherit?.GetContentLongTextTrim(state) ?? Target!.GetContentLongTextTrim(style, state);
    }

    /// <summary>
    /// Gets the prefix drawing setting for long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextPrefix value.</returns>
    public override PaletteTextHotkeyPrefix GetContentLongTextPrefix(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetInherit(state);

        return inherit?.GetContentLongTextPrefix(state) ?? Target!.GetContentLongTextPrefix(style, state);
    }

    /// <summary>
    /// Gets the horizontal relative alignment of the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentLongTextH(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetInherit(state);

        return inherit?.GetContentLongTextH(state) ?? Target!.GetContentLongTextH(style, state);
    }

    /// <summary>
    /// Gets the vertical relative alignment of the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentLongTextV(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetInherit(state);

        return inherit?.GetContentLongTextV(state) ?? Target!.GetContentLongTextV(style, state);
    }

    /// <summary>
    /// Gets the horizontal relative alignment of multiline long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentLongTextMultiLineH(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetInherit(state);

        return inherit?.GetContentLongTextMultiLineH(state) ?? Target!.GetContentLongTextMultiLineH(style, state);
    }

    /// <summary>
    /// Gets the first back color for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentLongTextColor1(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetInherit(state);

        return inherit?.GetContentLongTextColor1(state) ?? Target!.GetContentLongTextColor1(style, state);
    }

    /// <summary>
    /// Gets the second back color for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentLongTextColor2(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetInherit(state);

        return inherit?.GetContentLongTextColor2(state) ?? Target!.GetContentLongTextColor2(style, state);
    }

    /// <summary>
    /// Gets the color drawing style for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public override PaletteColorStyle GetContentLongTextColorStyle(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetInherit(state);

        return inherit?.GetContentLongTextColorStyle(state) ?? Target!.GetContentLongTextColorStyle(style, state);
    }

    /// <summary>
    /// Gets the color alignment for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public override PaletteRectangleAlign GetContentLongTextColorAlign(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetInherit(state);

        return inherit?.GetContentLongTextColorAlign(state) ?? Target!.GetContentLongTextColorAlign(style, state);
    }

    /// <summary>
    /// Gets a background image for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public override Image? GetContentLongTextImage(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetInherit(state);

        return inherit?.GetContentLongTextImage(state) ?? Target!.GetContentLongTextImage(style, state);
    }

    /// <summary>
    /// Gets the background image style for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public override PaletteImageStyle GetContentLongTextImageStyle(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetInherit(state);

        return inherit?.GetContentLongTextImageStyle(state) ?? Target!.GetContentLongTextImageStyle(style, state);
    }

    /// <summary>
    /// Gets the image alignment for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public override PaletteRectangleAlign GetContentLongTextImageAlign(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetInherit(state);

        return inherit?.GetContentLongTextImageAlign(state) ?? Target!.GetContentLongTextImageAlign(style, state);
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
        IPaletteContent? inherit = GetInherit(state);

        return inherit?.GetBorderContentPadding(owningForm, state) 
               ?? Target!.GetBorderContentPadding(owningForm, style, state);
    }

    /// <summary>
    /// Gets the padding between adjacent content items.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Integer value.</returns>
    public override int GetContentAdjacentGap(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetInherit(state);

        return inherit?.GetContentAdjacentGap(state) 
               ?? Target!.GetContentAdjacentGap(style, state);
    }
    #endregion

    #region Implementation
    private IPaletteContent? GetInherit(PaletteState state)
    {
        switch (state)
        {
            case PaletteState.Disabled:
                return _disabled;
            case PaletteState.Normal:
            case PaletteState.ContextNormal:    // From the TreeGrid
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
            case PaletteState.LinkVisitedOverride:
                return _linkVisitedOverride;
            case PaletteState.LinkNotVisitedOverride:
                return _linkNotVisitedOverride;
            case PaletteState.LinkPressedOverride:
                return _linkPressedOverride;
            default:
                // Should never happen!
                Debug.Assert(false);
                throw DebugTools.NotImplemented(state.ToString());
        }
    }
    #endregion
}
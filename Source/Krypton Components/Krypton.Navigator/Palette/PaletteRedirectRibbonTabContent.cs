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

namespace Krypton.Navigator;

/// <summary>
/// Redirect ribbon tab values based on the incoming state of the request.
/// </summary>
public class PaletteRedirectRibbonTabContent : PaletteRedirect
{
    #region Instance Fields
    private IPaletteRibbonBack? _disabledBack;
    private IPaletteRibbonBack? _normalBack;
    private IPaletteRibbonBack? _pressedBack;
    private IPaletteRibbonBack? _trackingBack;
    private IPaletteRibbonBack? _selectedBack;
    private IPaletteRibbonBack? _focusOverrideBack;
    private IPaletteRibbonText? _disabledText;
    private IPaletteRibbonText? _normalText;
    private IPaletteRibbonText? _pressedText;
    private IPaletteRibbonText? _trackingText;
    private IPaletteRibbonText? _selectedText;
    private IPaletteRibbonText? _focusOverrideText;
    private IPaletteContent? _disabledContent;
    private IPaletteContent? _normalContent;
    private IPaletteContent? _pressedContent;
    private IPaletteContent? _trackingContent;
    private IPaletteContent? _selectedContent;
    private IPaletteContent? _focusOverrideContent;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteRedirectRibbonDouble class.
    /// </summary>
    /// <param name="target">Initial palette target for redirection.</param>
    public PaletteRedirectRibbonTabContent(PaletteBase target)
        : this(target,
            null, null, null, null, null, null,
            null, null, null, null, null, null,
            null, null, null, null, null, null)
    {
    }

    /// <summary>
    /// Initialize a new instance of the PaletteRedirectRibbonDouble class.
    /// </summary>
    /// <param name="target">Initial palette target for redirection.</param>
    /// <param name="disabledBack">Redirection for back disabled state requests.</param>
    /// <param name="normalBack">Redirection for back normal state requests.</param>
    /// <param name="pressedBack">Redirection for back pressed state requests.</param>
    /// <param name="trackingBack">Redirection for back tracking state requests.</param>
    /// <param name="selectedBack">Redirection for selected states requests.</param>
    /// <param name="focusOverrideBack">Redirection for back focus override state requests.</param>
    /// <param name="disabledText">Redirection for text disabled state requests.</param>
    /// <param name="normalText">Redirection for text normal state requests.</param>
    /// <param name="pressedText">Redirection for text pressed state requests.</param>
    /// <param name="trackingText">Redirection for text tracking state requests.</param>
    /// <param name="selectedText">Redirection for text selected states requests.</param>
    /// <param name="focusOverrideText">Redirection for text focus override state requests.</param>
    /// <param name="disabledContent">Redirection for content disabled state requests.</param>
    /// <param name="normalContent">Redirection for content normal state requests.</param>
    /// <param name="pressedContent">Redirection for content pressed state requests.</param>
    /// <param name="trackingContent">Redirection for content tracking state requests.</param>
    /// <param name="selectedContent">Redirection for content selected states requests.</param>
    /// <param name="focusOverrideContent">Redirection for content focus override state requests.</param>
    public PaletteRedirectRibbonTabContent(PaletteBase target,
        IPaletteRibbonBack? disabledBack,
        IPaletteRibbonBack? normalBack,
        IPaletteRibbonBack? pressedBack,
        IPaletteRibbonBack? trackingBack,
        IPaletteRibbonBack? selectedBack,
        IPaletteRibbonBack? focusOverrideBack,
        IPaletteRibbonText? disabledText,
        IPaletteRibbonText? normalText,
        IPaletteRibbonText? pressedText,
        IPaletteRibbonText? trackingText,
        IPaletteRibbonText? selectedText,
        IPaletteRibbonText? focusOverrideText,
        IPaletteContent? disabledContent,
        IPaletteContent? normalContent,
        IPaletteContent? pressedContent,
        IPaletteContent? trackingContent,
        IPaletteContent? selectedContent,
        IPaletteContent? focusOverrideContent)
        : base(target)
    {
        // Remember state specific inheritance
        _disabledBack = disabledBack;
        _normalBack = normalBack;
        _pressedBack = pressedBack;
        _trackingBack = trackingBack;
        _selectedBack = selectedBack;
        _focusOverrideBack = focusOverrideBack;
        _disabledText = disabledText;
        _normalText = normalText;
        _pressedText = pressedText;
        _trackingText = trackingText;
        _selectedText = selectedText;
        _focusOverrideText = focusOverrideText;
        _disabledContent = disabledContent;
        _normalContent = normalContent;
        _pressedContent = pressedContent;
        _trackingContent = trackingContent;
        _selectedContent = selectedContent;
        _focusOverrideContent = focusOverrideContent;
    }
    #endregion

    #region SetRedirectStates
    /// <summary>
    /// Set the redirection states.
    /// </summary>
    /// <param name="disabled">Redirection for disabled state requests.</param>
    /// <param name="normal">Redirection for normal state requests.</param>
    /// <param name="pressed">Redirection for pressed state requests.</param>
    /// <param name="tracking">Redirection for tracking state requests.</param>
    /// <param name="selected">Redirection for selected states requests.</param>
    /// <param name="focusOverride">Redirection for focus override state requests.</param>
    public virtual void SetRedirectStates(PaletteRibbonTabContent disabled,
        PaletteRibbonTabContent normal,
        PaletteRibbonTabContent pressed,
        PaletteRibbonTabContent tracking,
        PaletteRibbonTabContent selected,
        PaletteRibbonTabContentRedirect focusOverride)
    {
        _disabledBack = disabled.TabDraw;
        _disabledText = disabled.TabDraw;
        _normalBack = normal.TabDraw;
        _normalText = normal.TabDraw;
        _pressedBack = pressed.TabDraw;
        _pressedText = pressed.TabDraw;
        _trackingBack = tracking.TabDraw;
        _trackingText = tracking.TabDraw;
        _selectedBack = selected.TabDraw;
        _selectedText = selected.TabDraw;
        _focusOverrideBack = focusOverride.TabDraw;
        _focusOverrideText = focusOverride.TabDraw;
        _disabledContent = disabled.Content;
        _normalContent = normal.Content;
        _pressedContent = pressed.Content;
        _trackingContent = tracking.Content;
        _selectedContent = selected.Content;
        _focusOverrideContent = focusOverride.Content;
    }
    #endregion

    #region ResetRedirectStates
    /// <summary>
    /// Reset the redirection states to null.
    /// </summary>
    public virtual void ResetRedirectStates()
    {
        _disabledBack = null;
        _normalBack = null;
        _pressedBack = null;
        _trackingBack = null;
        _selectedBack = null;
        _focusOverrideBack = null;
        _disabledText = null;
        _normalText = null;
        _pressedText = null;
        _trackingText = null;
        _selectedText = null;
        _focusOverrideText = null;
        _disabledContent = null;
        _normalContent = null;
        _pressedContent = null;
        _trackingContent = null;
        _selectedContent = null;
        _focusOverrideContent = null;
    }
    #endregion

    #region RibbonBack
    /// <summary>
    /// Gets the background drawing style for the ribbon item.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <param name="style">Style of the ribbon back style requested.</param>
    /// <returns>Color value.</returns>
    public override PaletteRibbonColorStyle GetRibbonBackColorStyle(PaletteRibbonBackStyle style, PaletteState state)
    {
        IPaletteRibbonBack? inherit = GetBackInherit(state);

        return inherit?.GetRibbonBackColorStyle(state) ?? Target!.GetRibbonBackColorStyle(style, state);
    }

    /// <summary>
    /// Gets the first background color for the ribbon item.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <param name="style">Style of the ribbon color requested.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonBackColor1(PaletteRibbonBackStyle style, PaletteState state)
    {
        IPaletteRibbonBack? inherit = GetBackInherit(state);

        return inherit?.GetRibbonBackColor1(state) ?? Target!.GetRibbonBackColor1(style, state);
    }

    /// <summary>
    /// Gets the second background color for the ribbon item.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <param name="style">Style of the ribbon color requested.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonBackColor2(PaletteRibbonBackStyle style, PaletteState state)
    {
        IPaletteRibbonBack? inherit = GetBackInherit(state);

        return inherit?.GetRibbonBackColor2(state) ?? Target!.GetRibbonBackColor2(style, state);
    }

    /// <summary>
    /// Gets the third background color for the ribbon item.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <param name="style">Style of the ribbon color requested.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonBackColor3(PaletteRibbonBackStyle style, PaletteState state)
    {
        IPaletteRibbonBack? inherit = GetBackInherit(state);

        return inherit?.GetRibbonBackColor3(state) ?? Target!.GetRibbonBackColor3(style, state);
    }

    /// <summary>
    /// Gets the fourth background color for the ribbon item.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <param name="style">Style of the ribbon color requested.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonBackColor4(PaletteRibbonBackStyle style, PaletteState state)
    {
        IPaletteRibbonBack? inherit = GetBackInherit(state);

        return inherit?.GetRibbonBackColor4(state) ?? Target!.GetRibbonBackColor4(style, state);
    }

    /// <summary>
    /// Gets the fifth background color for the ribbon item.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <param name="style">Style of the ribbon color requested.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonBackColor5(PaletteRibbonBackStyle style, PaletteState state)
    {
        IPaletteRibbonBack? inherit = GetBackInherit(state);

        return inherit?.GetRibbonBackColor5(state) ?? Target!.GetRibbonBackColor5(style, state);
    }
    #endregion

    #region RibbonText
    /// <summary>
    /// Gets the tab color for the item text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <param name="style">Style of the ribbon color requested.</param>
    /// <returns>Color value.</returns>
    public override Color GetRibbonTextColor(PaletteRibbonTextStyle style, PaletteState state)
    {
        IPaletteRibbonText? inherit = GetTextInherit(state);

        return inherit?.GetRibbonTextColor(state) ?? Target!.GetRibbonTextColor(style, state);
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
        IPaletteContent? inherit = GetContentInherit(state);

        return inherit?.GetContentDraw(state) 
               ?? Target!.GetContentDraw(style, state);
    }

    /// <summary>
    /// Gets a value indicating if content should be drawn with focus indication.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public override InheritBool GetContentDrawFocus(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetContentInherit(state);

        return inherit?.GetContentDrawFocus(state) 
               ?? Target!.GetContentDrawFocus(style, state);
    }

    /// <summary>
    /// Gets the horizontal relative alignment of the image.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentImageH(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetContentInherit(state);

        return inherit?.GetContentImageH(state) 
               ?? Target!.GetContentImageH(style, state);
    }

    /// <summary>
    /// Gets the vertical relative alignment of the image.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentImageV(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetContentInherit(state);

        return inherit?.GetContentImageV(state) 
               ?? Target!.GetContentImageV(style, state);
    }

    /// <summary>
    /// Gets the effect applied to drawing of the image.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteImageEffect value.</returns>
    public override PaletteImageEffect GetContentImageEffect(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetContentInherit(state);

        return inherit?.GetContentImageEffect(state) 
               ?? Target!.GetContentImageEffect(style, state);
    }

    /// <summary>
    /// Gets the font for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public override Font GetContentShortTextFont(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetContentInherit(state);

        return inherit?.GetContentShortTextFont(state) 
               ?? Target?.GetContentShortTextFont(style, state) 
               ?? throw new NullReferenceException("The result of GetContentShortTextFont() cannot be null.");
    }

    /// <summary>
    /// Gets the rendering hint for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextHint value.</returns>
    public override PaletteTextHint GetContentShortTextHint(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetContentInherit(state);

        return inherit?.GetContentShortTextHint(state) 
               ?? Target!.GetContentShortTextHint(style, state);
    }

    /// <summary>
    /// Gets the prefix drawing setting for short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextPrefix value.</returns>
    public override PaletteTextHotkeyPrefix GetContentShortTextPrefix(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetContentInherit(state);

        return inherit?.GetContentShortTextPrefix(state) 
               ?? Target!.GetContentShortTextPrefix(style, state);
    }

    /// <summary>
    /// Gets the flag indicating if multiline text is allowed for short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public override InheritBool GetContentShortTextMultiLine(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetContentInherit(state);

        return inherit?.GetContentShortTextMultiLine(state) 
               ?? Target!.GetContentShortTextMultiLine(style, state);
    }

    /// <summary>
    /// Gets the text trimming to use for short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextTrim value.</returns>
    public override PaletteTextTrim GetContentShortTextTrim(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetContentInherit(state);

        return inherit?.GetContentShortTextTrim(state) 
               ?? Target!.GetContentShortTextTrim(style, state);
    }

    /// <summary>
    /// Gets the horizontal relative alignment of the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentShortTextH(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetContentInherit(state);

        return inherit?.GetContentShortTextH(state) 
               ?? Target!.GetContentShortTextH(style, state);
    }

    /// <summary>
    /// Gets the vertical relative alignment of the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentShortTextV(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetContentInherit(state);

        return inherit?.GetContentShortTextV(state) 
               ?? Target!.GetContentShortTextV(style, state);
    }

    /// <summary>
    /// Gets the horizontal relative alignment of multiline short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentShortTextMultiLineH(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetContentInherit(state);

        return inherit?.GetContentShortTextMultiLineH(state) 
               ?? Target!.GetContentShortTextMultiLineH(style, state);
    }

    /// <summary>
    /// Gets the first back color for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentShortTextColor1(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetContentInherit(state);

        return inherit?.GetContentShortTextColor1(state) 
               ?? Target!.GetContentShortTextColor1(style, state);
    }

    /// <summary>
    /// Gets the second back color for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentShortTextColor2(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetContentInherit(state);

        return inherit?.GetContentShortTextColor2(state) 
               ?? Target!.GetContentShortTextColor2(style, state);
    }

    /// <summary>
    /// Gets the color drawing style for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public override PaletteColorStyle GetContentShortTextColorStyle(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetContentInherit(state);

        return inherit?.GetContentShortTextColorStyle(state) 
               ?? Target!.GetContentShortTextColorStyle(style, state);
    }

    /// <summary>
    /// Gets the color alignment for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public override PaletteRectangleAlign GetContentShortTextColorAlign(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetContentInherit(state);

        return inherit?.GetContentShortTextColorAlign(state) 
               ?? Target!.GetContentShortTextColorAlign(style, state);
    }

    /// <summary>
    /// Gets the color background angle for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public override float GetContentShortTextColorAngle(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetContentInherit(state);

        return inherit?.GetContentShortTextColorAngle(state) 
               ?? Target!.GetContentShortTextColorAngle(style, state);
    }

    /// <summary>
    /// Gets a background image for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public override Image? GetContentShortTextImage(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetContentInherit(state);

        return inherit?.GetContentShortTextImage(state) 
               ?? Target!.GetContentShortTextImage(style, state);
    }

    /// <summary>
    /// Gets the background image style.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public override PaletteImageStyle GetContentShortTextImageStyle(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetContentInherit(state);

        return inherit?.GetContentShortTextImageStyle(state) 
               ?? Target!.GetContentShortTextImageStyle(style, state);
    }

    /// <summary>
    /// Gets the image alignment for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public override PaletteRectangleAlign GetContentShortTextImageAlign(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetContentInherit(state);

        return inherit?.GetContentShortTextImageAlign(state) 
               ?? Target!.GetContentShortTextImageAlign(style, state);
    }

    /// <summary>
    /// Gets the font for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public override Font GetContentLongTextFont(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetContentInherit(state);

        return inherit?.GetContentLongTextFont(state) 
               ?? Target?.GetContentLongTextFont(style, state) 
               ?? throw new NullReferenceException("The result of GetContentLongTextFont() cannot be null.");
    }

    /// <summary>
    /// Gets the rendering hint for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextHint value.</returns>
    public override PaletteTextHint GetContentLongTextHint(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetContentInherit(state);

        return inherit?.GetContentLongTextHint(state) 
               ?? Target!.GetContentLongTextHint(style, state);
    }

    /// <summary>
    /// Gets the flag indicating if multiline text is allowed for long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public override InheritBool GetContentLongTextMultiLine(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetContentInherit(state);

        return inherit?.GetContentLongTextMultiLine(state) 
               ?? Target!.GetContentLongTextMultiLine(style, state);
    }

    /// <summary>
    /// Gets the text trimming to use for long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextTrim value.</returns>
    public override PaletteTextTrim GetContentLongTextTrim(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetContentInherit(state);

        return inherit?.GetContentLongTextTrim(state) 
               ?? Target!.GetContentLongTextTrim(style, state);
    }

    /// <summary>
    /// Gets the prefix drawing setting for long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextPrefix value.</returns>
    public override PaletteTextHotkeyPrefix GetContentLongTextPrefix(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetContentInherit(state);

        return inherit?.GetContentLongTextPrefix(state) 
               ?? Target!.GetContentLongTextPrefix(style, state);
    }

    /// <summary>
    /// Gets the horizontal relative alignment of the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentLongTextH(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetContentInherit(state);

        return inherit?.GetContentLongTextH(state) 
               ?? Target!.GetContentLongTextH(style, state);
    }

    /// <summary>
    /// Gets the vertical relative alignment of the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentLongTextV(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetContentInherit(state);

        return inherit?.GetContentLongTextV(state) 
               ?? Target!.GetContentLongTextV(style, state);
    }

    /// <summary>
    /// Gets the horizontal relative alignment of multiline long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentLongTextMultiLineH(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetContentInherit(state);

        return inherit?.GetContentLongTextMultiLineH(state) 
               ?? Target!.GetContentLongTextMultiLineH(style, state);
    }

    /// <summary>
    /// Gets the first back color for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentLongTextColor1(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetContentInherit(state);

        return inherit?.GetContentLongTextColor1(state) 
               ?? Target!.GetContentLongTextColor1(style, state);
    }

    /// <summary>
    /// Gets the second back color for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentLongTextColor2(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetContentInherit(state);

        return inherit?.GetContentLongTextColor2(state) 
               ?? Target!.GetContentLongTextColor2(style, state);
    }

    /// <summary>
    /// Gets the color drawing style for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public override PaletteColorStyle GetContentLongTextColorStyle(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetContentInherit(state);

        return inherit?.GetContentLongTextColorStyle(state) 
               ?? Target!.GetContentLongTextColorStyle(style, state);
    }

    /// <summary>
    /// Gets the color alignment for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public override PaletteRectangleAlign GetContentLongTextColorAlign(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetContentInherit(state);

        return inherit?.GetContentLongTextColorAlign(state) 
               ?? Target!.GetContentLongTextColorAlign(style, state);
    }

    /// <summary>
    /// Gets a background image for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public override Image? GetContentLongTextImage(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetContentInherit(state);

        return inherit?.GetContentLongTextImage(state) 
               ?? Target!.GetContentLongTextImage(style, state);
    }

    /// <summary>
    /// Gets the background image style for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public override PaletteImageStyle GetContentLongTextImageStyle(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetContentInherit(state);

        return inherit?.GetContentLongTextImageStyle(state) 
               ?? Target!.GetContentLongTextImageStyle(style, state);
    }

    /// <summary>
    /// Gets the image alignment for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public override PaletteRectangleAlign GetContentLongTextImageAlign(PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetContentInherit(state);

        return inherit?.GetContentLongTextImageAlign(state) 
               ?? Target!.GetContentLongTextImageAlign(style, state);
    }

    /// <summary>
    /// Gets the padding between the border and content drawing.
    /// </summary>
    /// <param name="owningForm"></param>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Padding value.</returns>
    public override Padding GetBorderContentPadding(KryptonForm? owningForm, PaletteContentStyle style, PaletteState state)
    {
        IPaletteContent? inherit = GetContentInherit(state);

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
        IPaletteContent? inherit = GetContentInherit(state);

        return inherit?.GetContentAdjacentGap(state) 
               ?? Target!.GetContentAdjacentGap(style, state);
    }
    #endregion

    #region Implementation
    private IPaletteRibbonBack? GetBackInherit(PaletteState state)
    {
        switch (state)
        {
            case PaletteState.Disabled:
                return _disabledBack;
            case PaletteState.Normal:
                return _normalBack;
            case PaletteState.Pressed:
                return _pressedBack;
            case PaletteState.Tracking:
                return _trackingBack;
            case PaletteState.CheckedNormal:
            case PaletteState.CheckedPressed:
            case PaletteState.CheckedTracking:
                return _selectedBack;
            case PaletteState.FocusOverride:
                return _focusOverrideBack;
            default:
                // Should never happen!
                Debug.Assert(false);
                throw DebugTools.NotImplemented(state.ToString());
        }
    }

    private IPaletteRibbonText? GetTextInherit(PaletteState state)
    {
        switch (state)
        {
            case PaletteState.Disabled:
                return _disabledText;
            case PaletteState.Normal:
                return _normalText;
            case PaletteState.Pressed:
                return _pressedText;
            case PaletteState.Tracking:
                return _trackingText;
            case PaletteState.CheckedNormal:
            case PaletteState.CheckedPressed:
            case PaletteState.CheckedTracking:
                return _selectedText;
            case PaletteState.FocusOverride:
                return _focusOverrideText;
            default:
                // Should never happen!
                Debug.Assert(false);
                throw DebugTools.NotImplemented(state.ToString());
        }
    }

    private IPaletteContent? GetContentInherit(PaletteState state)
    {
        switch (state)
        {
            case PaletteState.Disabled:
                return _disabledContent;
            case PaletteState.Normal:
                return _normalContent;
            case PaletteState.Pressed:
                return _pressedContent;
            case PaletteState.Tracking:
                return _trackingContent;
            case PaletteState.CheckedNormal:
            case PaletteState.CheckedPressed:
            case PaletteState.CheckedTracking:
                return _selectedContent;
            case PaletteState.FocusOverride:
                return _focusOverrideContent;
            default:
                // Should never happen!
                Debug.Assert(false);
                throw DebugTools.NotImplemented(state.ToString());
        }
    }
    #endregion
}
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
/// Redirect all content requests directly to the palette instance.
/// </summary>
public class PaletteContentToPalette : IPaletteContent
{
    #region Instance Fields
    private readonly PaletteBase _palette;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteContentToPalette class.
    /// </summary>
    /// <param name="palette">Source for getting all values.</param>
    /// <param name="style">Style of values required.</param>
    public PaletteContentToPalette(PaletteBase palette, PaletteContentStyle style)
    {
        // Remember source palette
        _palette = palette;
        ContentStyle = style;
    }
    #endregion

    #region ContentStyle
    /// <summary>
    /// Gets and sets the fixed content style.
    /// </summary>
    public PaletteContentStyle ContentStyle { get; set; }

    #endregion

    #region GetContentStyle
    /// <summary>
    /// Gets the style appropriate for this content.
    /// </summary>
    /// <returns>Content style.</returns>
    public PaletteContentStyle GetContentStyle() => ContentStyle;

    #endregion

    #region Draw
    /// <summary>
    /// Gets the actual content draw value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public InheritBool GetContentDraw(PaletteState state) => _palette.GetContentDraw(ContentStyle, state);

    #endregion

    #region DrawFocus
    /// <summary>
    /// Gets the actual content draw with focus value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public InheritBool GetContentDrawFocus(PaletteState state) => _palette.GetContentDrawFocus(ContentStyle, state);

    #endregion

    #region Image
    /// <summary>
    /// Gets the actual content image horizontal alignment value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public PaletteRelativeAlign GetContentImageH(PaletteState state) => _palette.GetContentImageH(ContentStyle, state);

    /// <summary>
    /// Gets the actual content image vertical alignment value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public PaletteRelativeAlign GetContentImageV(PaletteState state) => _palette.GetContentImageV(ContentStyle, state);

    /// <summary>
    /// Gets the actual image drawing effect value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteImageEffect value.</returns>
    public PaletteImageEffect GetContentImageEffect(PaletteState state) => _palette.GetContentImageEffect(ContentStyle, state);

    /// <summary>
    /// Gets the image color to remap into another color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetContentImageColorMap(PaletteState state) => _palette.GetContentImageColorMap(ContentStyle, state);

    /// <summary>
    /// Gets the color to use in place of the image map color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetContentImageColorTo(PaletteState state) => _palette.GetContentImageColorTo(ContentStyle, state);

    #endregion

    #region ShortText
    /// <summary>
    /// Gets the actual content short text font value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public Font? GetContentShortTextFont(PaletteState state) => _palette.GetContentShortTextFont(ContentStyle, state);

    /// <summary>
    /// Gets the font for the short text by generating a new font instance.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public Font? GetContentShortTextNewFont(PaletteState state) => _palette.GetContentShortTextNewFont(ContentStyle, state);

    /// <summary>
    /// Gets the actual text rendering hint for short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextHint value.</returns>
    public PaletteTextHint GetContentShortTextHint(PaletteState state) => _palette.GetContentShortTextHint(ContentStyle, state);

    /// <summary>
    /// Gets the prefix drawing setting for short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextPrefix value.</returns>
    public PaletteTextHotkeyPrefix GetContentShortTextPrefix(PaletteState state) => _palette.GetContentShortTextPrefix(ContentStyle, state);

    /// <summary>
    /// Gets the actual text trimming for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextTrim value.</returns>
    public PaletteTextTrim GetContentShortTextTrim(PaletteState state) => _palette.GetContentShortTextTrim(ContentStyle, state);

    /// <summary>
    /// Gets the actual content short text horizontal alignment value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public PaletteRelativeAlign GetContentShortTextH(PaletteState state) => _palette.GetContentShortTextH(ContentStyle, state);

    /// <summary>
    /// Gets the actual content short text vertical alignment value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public PaletteRelativeAlign GetContentShortTextV(PaletteState state) => _palette.GetContentShortTextV(ContentStyle, state);

    /// <summary>
    /// Gets the actual content short text horizontal multiline alignment value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public PaletteRelativeAlign GetContentShortTextMultiLineH(PaletteState state) => _palette.GetContentShortTextMultiLineH(ContentStyle, state);

    /// <summary>
    /// Gets the flag indicating if multiline text is allowed for short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public InheritBool GetContentShortTextMultiLine(PaletteState state) => _palette.GetContentShortTextMultiLine(ContentStyle, state);

    /// <summary>
    /// Gets the first color for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetContentShortTextColor1(PaletteState state) => _palette.GetContentShortTextColor1(ContentStyle, state);

    /// <summary>
    /// Gets the second back color for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetContentShortTextColor2(PaletteState state) => _palette.GetContentShortTextColor2(ContentStyle, state);

    /// <summary>
    /// Gets the color drawing style for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public PaletteColorStyle GetContentShortTextColorStyle(PaletteState state) => _palette.GetContentShortTextColorStyle(ContentStyle, state);

    /// <summary>
    /// Gets the color alignment style for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public PaletteRectangleAlign GetContentShortTextColorAlign(PaletteState state) => _palette.GetContentShortTextColorAlign(ContentStyle, state);

    /// <summary>
    /// Gets the color angle for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public float GetContentShortTextColorAngle(PaletteState state) => _palette.GetContentShortTextColorAngle(ContentStyle, state);

    /// <summary>
    /// Gets an image for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public Image? GetContentShortTextImage(PaletteState state) => _palette.GetContentShortTextImage(ContentStyle, state);

    /// <summary>
    /// Gets the image style for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public PaletteImageStyle GetContentShortTextImageStyle(PaletteState state) => _palette.GetContentShortTextImageStyle(ContentStyle, state);

    /// <summary>
    /// Gets the image alignment style for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public PaletteRectangleAlign GetContentShortTextImageAlign(PaletteState state) => _palette.GetContentShortTextImageAlign(ContentStyle, state);

    #endregion

    #region LongText
    /// <summary>
    /// Gets the actual content long text font value.
    /// </summary>
    /// <returns>Font value.</returns>
    /// <param name="state">Palette value should be applicable to this state.</param>
    public Font? GetContentLongTextFont(PaletteState state) => _palette.GetContentLongTextFont(ContentStyle, state);

    /// <summary>
    /// Gets the font for the long text by generating a new font instance.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public Font? GetContentLongTextNewFont(PaletteState state) => _palette.GetContentLongTextNewFont(ContentStyle, state);

    /// <summary>
    /// Gets the actual text rendering hint for long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextHint value.</returns>
    public PaletteTextHint GetContentLongTextHint(PaletteState state) => _palette.GetContentLongTextHint(ContentStyle, state);

    /// <summary>
    /// Gets the prefix drawing setting for long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextPrefix value.</returns>
    public PaletteTextHotkeyPrefix GetContentLongTextPrefix(PaletteState state) => _palette.GetContentLongTextPrefix(ContentStyle, state);

    /// <summary>
    /// Gets the actual text trimming for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextTrim value.</returns>
    public PaletteTextTrim GetContentLongTextTrim(PaletteState state) => _palette.GetContentLongTextTrim(ContentStyle, state);

    /// <summary>
    /// Gets the actual content long text horizontal alignment value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public PaletteRelativeAlign GetContentLongTextH(PaletteState state) => _palette.GetContentLongTextH(ContentStyle, state);

    /// <summary>
    /// Gets the actual content long text vertical alignment value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public PaletteRelativeAlign GetContentLongTextV(PaletteState state) => _palette.GetContentLongTextV(ContentStyle, state);

    /// <summary>
    /// Gets the actual content long text horizontal multiline alignment value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public PaletteRelativeAlign GetContentLongTextMultiLineH(PaletteState state) => _palette.GetContentLongTextMultiLineH(ContentStyle, state);

    /// <summary>
    /// Gets the flag indicating if multiline text is allowed for long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public InheritBool GetContentLongTextMultiLine(PaletteState state) => _palette.GetContentLongTextMultiLine(ContentStyle, state);

    /// <summary>
    /// Gets the first color for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetContentLongTextColor1(PaletteState state) => _palette.GetContentLongTextColor1(ContentStyle, state);

    /// <summary>
    /// Gets the second back color for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetContentLongTextColor2(PaletteState state) => _palette.GetContentLongTextColor2(ContentStyle, state);

    /// <summary>
    /// Gets the color drawing style for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public PaletteColorStyle GetContentLongTextColorStyle(PaletteState state) => _palette.GetContentLongTextColorStyle(ContentStyle, state);

    /// <summary>
    /// Gets the color alignment style for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public PaletteRectangleAlign GetContentLongTextColorAlign(PaletteState state) => _palette.GetContentLongTextColorAlign(ContentStyle, state);

    /// <summary>
    /// Gets the color angle for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public float GetContentLongTextColorAngle(PaletteState state) => _palette.GetContentLongTextColorAngle(ContentStyle, state);

    /// <summary>
    /// Gets an image for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public Image? GetContentLongTextImage(PaletteState state) => _palette.GetContentLongTextImage(ContentStyle, state);

    /// <summary>
    /// Gets the image style for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public PaletteImageStyle GetContentLongTextImageStyle(PaletteState state) => _palette.GetContentLongTextImageStyle(ContentStyle, state);

    /// <summary>
    /// Gets the image alignment style for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public PaletteRectangleAlign GetContentLongTextImageAlign(PaletteState state) => _palette.GetContentLongTextImageAlign(ContentStyle, state);

    #endregion

    #region Padding

    /// <summary>
    /// Gets the actual padding between the border and content drawing.
    /// </summary>
    /// <param name="owningForm"></param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Padding value.</returns>
    public Padding GetBorderContentPadding(KryptonForm? owningForm, PaletteState state) => _palette.GetBorderContentPadding(owningForm, ContentStyle, state);

    #endregion

    #region AdjacentGap
    /// <summary>
    /// Gets the actual padding between adjacent content items.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Integer value.</returns>
    public int GetContentAdjacentGap(PaletteState state) => _palette.GetContentAdjacentGap(ContentStyle, state);

    #endregion
}
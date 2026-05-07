#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

namespace Krypton.Ribbon;

internal class QATButtonToContent : IPaletteContent
{
    #region Instance Fields
    private readonly IQuickAccessToolbarButton _qatButton;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the RibbonTabToContent class.
    /// </summary>
    /// <param name="qatButton">Source for button values.</param>
    public QATButtonToContent([DisallowNull] IQuickAccessToolbarButton qatButton)
    {
        Debug.Assert(qatButton is not null);

        _qatButton = qatButton ?? throw new ArgumentNullException(nameof(qatButton));
    }
    #endregion

    #region IPaletteContent
    /// <summary>
    /// Gets a value indicating if content should be drawn.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public InheritBool GetContentDraw(PaletteState state) => InheritBool.True;

    /// <summary>
    /// Gets a value indicating if content should be drawn with focus indication.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public InheritBool GetContentDrawFocus(PaletteState state) => InheritBool.False;

    /// <summary>
    /// Gets the horizontal relative alignment of the image.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public PaletteRelativeAlign GetContentImageH(PaletteState state) => PaletteRelativeAlign.Center;

    /// <summary>
    /// Gets the vertical relative alignment of the image.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public PaletteRelativeAlign GetContentImageV(PaletteState state) => PaletteRelativeAlign.Center;

    /// <summary>
    /// Gets the effect applied to drawing of the image.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteImageEffect value.</returns>
    public PaletteImageEffect GetContentImageEffect(PaletteState state) => state != PaletteState.Disabled
        ? PaletteImageEffect.Normal
        : PaletteImageEffect.Disabled;

    /// <summary>
    /// Gets the image color to remap into another color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetContentImageColorMap(PaletteState state) => Color.Empty;

    /// <summary>
    /// Gets the color to use in place of the image map color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetContentImageColorTo(PaletteState state) => Color.Empty;

    /// <summary>
    /// Gets the font for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public Font? GetContentShortTextFont(PaletteState state) => null;

    /// <summary>
    /// Gets the font for the short text by generating a new font instance.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public Font? GetContentShortTextNewFont(PaletteState state) => null;

    /// <summary>
    /// Gets the rendering hint for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextHint value.</returns>
    public PaletteTextHint GetContentShortTextHint(PaletteState state) => PaletteTextHint.ClearTypeGridFit;

    /// <summary>
    /// Gets the prefix drawing setting for short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextPrefix value.</returns>
    public PaletteTextHotkeyPrefix GetContentShortTextPrefix(PaletteState state) => PaletteTextHotkeyPrefix.None;

    /// <summary>
    /// Gets the flag indicating if multiline text is allowed for short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public InheritBool GetContentShortTextMultiLine(PaletteState state) => InheritBool.False;

    /// <summary>
    /// Gets the text trimming to use for short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextTrim value.</returns>
    public PaletteTextTrim GetContentShortTextTrim(PaletteState state) => PaletteTextTrim.Character;

    /// <summary>
    /// Gets the horizontal relative alignment of the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public PaletteRelativeAlign GetContentShortTextH(PaletteState state) => PaletteRelativeAlign.Center;

    /// <summary>
    /// Gets the vertical relative alignment of the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public PaletteRelativeAlign GetContentShortTextV(PaletteState state) => PaletteRelativeAlign.Center;

    /// <summary>
    /// Gets the horizontal relative alignment of multiline short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public PaletteRelativeAlign GetContentShortTextMultiLineH(PaletteState state) => PaletteRelativeAlign.Near;

    /// <summary>
    /// Gets the first back color for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetContentShortTextColor1(PaletteState state) => Color.Empty;

    /// <summary>
    /// Gets the second back color for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetContentShortTextColor2(PaletteState state) => Color.Empty;

    /// <summary>
    /// Gets the color drawing style for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public PaletteColorStyle GetContentShortTextColorStyle(PaletteState state) => PaletteColorStyle.Solid;

    /// <summary>
    /// Gets the color alignment for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public PaletteRectangleAlign GetContentShortTextColorAlign(PaletteState state) => PaletteRectangleAlign.Local;

    /// <summary>
    /// Gets the color background angle for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public float GetContentShortTextColorAngle(PaletteState state) => 0f;

    /// <summary>
    /// Gets a background image for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public Image? GetContentShortTextImage(PaletteState state) => null;

    /// <summary>
    /// Gets the background image style.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public PaletteImageStyle GetContentShortTextImageStyle(PaletteState state) => PaletteImageStyle.Stretch;

    /// <summary>
    /// Gets the image alignment for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public PaletteRectangleAlign GetContentShortTextImageAlign(PaletteState state) => PaletteRectangleAlign.Local;

    /// <summary>
    /// Gets the font for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public Font? GetContentLongTextFont(PaletteState state) => null;

    /// <summary>
    /// Gets the font for the long text by generating a new font instance.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public Font? GetContentLongTextNewFont(PaletteState state) => null;

    /// <summary>
    /// Gets the rendering hint for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextHint value.</returns>
    public PaletteTextHint GetContentLongTextHint(PaletteState state) => PaletteTextHint.ClearTypeGridFit;

    /// <summary>
    /// Gets the flag indicating if multiline text is allowed for long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public InheritBool GetContentLongTextMultiLine(PaletteState state) => InheritBool.False;

    /// <summary>
    /// Gets the text trimming to use for long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextTrim value.</returns>
    public PaletteTextTrim GetContentLongTextTrim(PaletteState state) => PaletteTextTrim.Character;

    /// <summary>
    /// Gets the prefix drawing setting for long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextPrefix value.</returns>
    public PaletteTextHotkeyPrefix GetContentLongTextPrefix(PaletteState state) => PaletteTextHotkeyPrefix.None;

    /// <summary>
    /// Gets the horizontal relative alignment of the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public PaletteRelativeAlign GetContentLongTextH(PaletteState state) => PaletteRelativeAlign.Center;

    /// <summary>
    /// Gets the vertical relative alignment of the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public PaletteRelativeAlign GetContentLongTextV(PaletteState state) => PaletteRelativeAlign.Center;

    /// <summary>
    /// Gets the horizontal relative alignment of multiline long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public PaletteRelativeAlign GetContentLongTextMultiLineH(PaletteState state) => PaletteRelativeAlign.Near;

    /// <summary>
    /// Gets the first back color for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetContentLongTextColor1(PaletteState state) => Color.Empty;

    /// <summary>
    /// Gets the second back color for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetContentLongTextColor2(PaletteState state) => Color.Empty;

    /// <summary>
    /// Gets the color drawing style for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public PaletteColorStyle GetContentLongTextColorStyle(PaletteState state) => PaletteColorStyle.Solid;

    /// <summary>
    /// Gets the color alignment for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public PaletteRectangleAlign GetContentLongTextColorAlign(PaletteState state) => PaletteRectangleAlign.Local;

    /// <summary>
    /// Gets the color background angle for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public float GetContentLongTextColorAngle(PaletteState state) => 0f;

    /// <summary>
    /// Gets a background image for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public Image? GetContentLongTextImage(PaletteState state) => null;

    /// <summary>
    /// Gets the background image style for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public PaletteImageStyle GetContentLongTextImageStyle(PaletteState state) => PaletteImageStyle.Stretch;

    /// <summary>
    /// Gets the image alignment for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public PaletteRectangleAlign GetContentLongTextImageAlign(PaletteState state) => PaletteRectangleAlign.Local;

    /// <summary>
    /// Gets the padding between the border and content drawing.
    /// </summary>
    /// <param name="owningForm"></param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Padding value.</returns>
    public Padding GetBorderContentPadding(KryptonForm? owningForm, PaletteState state) => Padding.Empty;

    /// <summary>
    /// Gets the padding between adjacent content items.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Integer value.</returns>
    public int GetContentAdjacentGap(PaletteState state) => 3;

    /// <summary>
    /// Gets the style appropriate for this content.
    /// </summary>
    /// <returns>Content style.</returns>
    public PaletteContentStyle GetContentStyle() => PaletteContentStyle.LabelNormalPanel;

    #endregion
}
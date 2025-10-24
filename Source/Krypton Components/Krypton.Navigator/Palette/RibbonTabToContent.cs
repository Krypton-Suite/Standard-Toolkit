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

internal class RibbonTabToContent : IPaletteContent
{
    #region Instance Fields
    private readonly IPaletteRibbonGeneral _ribbonGeneral;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the RibbonTabToContent class.
    /// </summary>
    /// <param name="ribbonGeneral">Source for general ribbon settings.</param>
    /// <param name="ribbonTabText">Source for ribbon tab settings.</param>
    /// <param name="content">Source for content settings.</param>
    public RibbonTabToContent([DisallowNull] IPaletteRibbonGeneral ribbonGeneral,
        [DisallowNull] IPaletteRibbonText ribbonTabText,
        [DisallowNull] IPaletteContent content)
    {
        Debug.Assert(ribbonGeneral != null);
        Debug.Assert(ribbonTabText != null);
        Debug.Assert(content != null);

        _ribbonGeneral = ribbonGeneral ?? throw new ArgumentNullException(nameof(ribbonGeneral));
        PaletteRibbonText = ribbonTabText ?? throw new ArgumentNullException(nameof(ribbonTabText));
        PaletteContent = content ?? throw new ArgumentNullException(nameof(content));
    }
    #endregion

    #region PaletteRibbonText
    /// <summary>
    /// Gets and sets the ribbon tab text palette to use.
    /// </summary>
    public IPaletteRibbonText PaletteRibbonText { get; set; }

    #endregion

    #region PaletteContent
    /// <summary>
    /// Gets and sets the ribbon tab content palette to use.
    /// </summary>
    public IPaletteContent? PaletteContent { get; set; }

    #endregion

    #region IPaletteContent
    /// <summary>
    /// Gets a value indicating if content should be drawn.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public InheritBool GetContentDraw(PaletteState state) => PaletteContent!.GetContentDraw(state);

    /// <summary>
    /// Gets a value indicating if content should be drawn with focus indication.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public InheritBool GetContentDrawFocus(PaletteState state) => PaletteContent!.GetContentDrawFocus(state);

    /// <summary>
    /// Gets the horizontal relative alignment of the image.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public PaletteRelativeAlign GetContentImageH(PaletteState state) => PaletteContent!.GetContentImageH(state);

    /// <summary>
    /// Gets the vertical relative alignment of the image.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public PaletteRelativeAlign GetContentImageV(PaletteState state) => PaletteContent!.GetContentImageV(state);

    /// <summary>
    /// Gets the effect applied to drawing of the image.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteImageEffect value.</returns>
    public PaletteImageEffect GetContentImageEffect(PaletteState state) => PaletteContent!.GetContentImageEffect(state);

    /// <summary>
    /// Gets the image color to remap into another color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetContentImageColorMap(PaletteState state) => PaletteContent!.GetContentImageColorMap(state);

    /// <summary>
    /// Gets the color to use in place of the image map color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetContentImageColorTo(PaletteState state) => PaletteContent!.GetContentImageColorTo(state);

    /// <summary>
    /// Gets the font for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public Font GetContentShortTextFont(PaletteState state) => _ribbonGeneral.GetRibbonTextFont(state);

    /// <summary>
    /// Gets the font for the short text by generating a new font instance.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public Font GetContentShortTextNewFont(PaletteState state) => _ribbonGeneral.GetRibbonTextFont(state);

    /// <summary>
    /// Gets the rendering hint for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextHint value.</returns>
    public PaletteTextHint GetContentShortTextHint(PaletteState state) => _ribbonGeneral.GetRibbonTextHint(state);

    /// <summary>
    /// Gets the prefix drawing setting for short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextPrefix value.</returns>
    public PaletteTextHotkeyPrefix GetContentShortTextPrefix(PaletteState state) => PaletteContent!.GetContentShortTextPrefix(state);

    /// <summary>
    /// Gets the flag indicating if multiline text is allowed for short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public InheritBool GetContentShortTextMultiLine(PaletteState state) => PaletteContent!.GetContentShortTextMultiLine(state);

    /// <summary>
    /// Gets the text trimming to use for short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextTrim value.</returns>
    public virtual PaletteTextTrim GetContentShortTextTrim(PaletteState state) => PaletteContent!.GetContentShortTextTrim(state);

    /// <summary>
    /// Gets the horizontal relative alignment of the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public virtual PaletteRelativeAlign GetContentShortTextH(PaletteState state) => PaletteContent!.GetContentShortTextH(state);

    /// <summary>
    /// Gets the vertical relative alignment of the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public PaletteRelativeAlign GetContentShortTextV(PaletteState state) => PaletteContent!.GetContentShortTextV(state);

    /// <summary>
    /// Gets the horizontal relative alignment of multiline short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public PaletteRelativeAlign GetContentShortTextMultiLineH(PaletteState state) => PaletteContent!.GetContentShortTextMultiLineH(state);

    /// <summary>
    /// Gets the first back color for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public virtual Color GetContentShortTextColor1(PaletteState state) => PaletteRibbonText.GetRibbonTextColor(state);

    /// <summary>
    /// Gets the second back color for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public virtual Color GetContentShortTextColor2(PaletteState state) => PaletteRibbonText.GetRibbonTextColor(state);

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
    public Font GetContentLongTextFont(PaletteState state) => _ribbonGeneral.GetRibbonTextFont(state);

    /// <summary>
    /// Gets the font for the long text by generating a new font instance.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public Font GetContentLongTextNewFont(PaletteState state) => _ribbonGeneral.GetRibbonTextFont(state);

    /// <summary>
    /// Gets the rendering hint for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextHint value.</returns>
    public PaletteTextHint GetContentLongTextHint(PaletteState state) => _ribbonGeneral.GetRibbonTextHint(state);

    /// <summary>
    /// Gets the flag indicating if multiline text is allowed for long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public InheritBool GetContentLongTextMultiLine(PaletteState state) => PaletteContent!.GetContentLongTextMultiLine(state);

    /// <summary>
    /// Gets the text trimming to use for long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextTrim value.</returns>
    public virtual PaletteTextTrim GetContentLongTextTrim(PaletteState state) => PaletteContent!.GetContentLongTextTrim(state);

    /// <summary>
    /// Gets the prefix drawing setting for long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextPrefix value.</returns>
    public PaletteTextHotkeyPrefix GetContentLongTextPrefix(PaletteState state) => PaletteContent!.GetContentLongTextPrefix(state);

    /// <summary>
    /// Gets the horizontal relative alignment of the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public PaletteRelativeAlign GetContentLongTextH(PaletteState state) => PaletteContent!.GetContentLongTextH(state);

    /// <summary>
    /// Gets the vertical relative alignment of the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public PaletteRelativeAlign GetContentLongTextV(PaletteState state) => PaletteContent!.GetContentLongTextV(state);

    /// <summary>
    /// Gets the horizontal relative alignment of multiline long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public PaletteRelativeAlign GetContentLongTextMultiLineH(PaletteState state) => PaletteContent!.GetContentLongTextMultiLineH(state);

    /// <summary>
    /// Gets the first back color for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public virtual Color GetContentLongTextColor1(PaletteState state) => PaletteRibbonText.GetRibbonTextColor(state);

    /// <summary>
    /// Gets the second back color for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public virtual Color GetContentLongTextColor2(PaletteState state) => PaletteRibbonText.GetRibbonTextColor(state);

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
    public Padding GetBorderContentPadding(KryptonForm? owningForm, PaletteState state) => PaletteContent!.GetBorderContentPadding(owningForm, state);

    /// <summary>
    /// Gets the padding between adjacent content items.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Integer value.</returns>
    public int GetContentAdjacentGap(PaletteState state) => PaletteContent!.GetContentAdjacentGap(state);

    /// <summary>
    /// Gets the style appropriate for this content.
    /// </summary>
    /// <returns>Content style.</returns>
    public PaletteContentStyle GetContentStyle() => PaletteContentStyle.LabelNormalPanel;

    #endregion
}
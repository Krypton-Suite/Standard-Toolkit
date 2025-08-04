#region BSD License
/*
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2017 - 2025. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Provides base class implementation for palettes.
/// </summary>
[ToolboxItem(false)]
public abstract class PaletteBase : Component
{
    #region Instance Fields

    /// <summary>
    /// Direct indexed access to the palette's backing color array.
    /// </summary>
    protected abstract Color[] SchemeColors { get; }
    internal Color[] GetSchemeColors() => SchemeColors;

    private Padding? _inputControlPadding;
    private PaletteDragFeedback _dragFeedback;
    private Image[] _toolBarImages;

    private readonly Font _defaultFontStyle = new Font("Segoe UI", 9f, FontStyle.Regular);

    #region Font Stuff

    private Font _baseFont; // = _defaultFontStyle;
    public Font? BoldFont;
    public Font? ItalicFont;
    public Font? Header1ShortFont;
    public Font? Header2ShortFont;
    public Font? Header1LongFont;
    public Font? Header2LongFont;
    public Font? SuperToolFont;
    public Font? HeaderFormFont;
    public Font? ButtonFont;
    public Font? ButtonFontNavigatorStack;
    public Font? ButtonFontNavigatorMini;
    public Font? TabFontNormal;
    public Font? TabFontSelected;
    public Font? RibbonTabFont;
    public Font? GridFont;
    public Font? CalendarFont;
    public Font? CalendarBoldFont;
    public Font? RibbonTabContextFont;

    #endregion

    #endregion Instance Fields

    #region Events
    /// <summary>
    /// Occurs when a palette change requires a repaint.
    /// </summary>
    public event EventHandler<PaletteLayoutEventArgs>? PalettePaint;

    /// <summary>
    /// Occurs when the UseThemeFormChromeBorderWidth setting changes.
    /// </summary>
    public event EventHandler? UseThemeFormChromeBorderWidthChanged;

    /// <summary>
    /// Occurs when the BasePalette/BasePaletteMode setting changes.
    /// </summary>
    public event EventHandler? BasePaletteChanged;

    /// <summary>
    /// Occurs when the BaseRenderer/BaseRendererMode setting changes.
    /// </summary>
    public event EventHandler? BaseRendererChanged;

    /// <summary>
    /// Occurs when a button spec change occurs.
    /// </summary>
    public event EventHandler? ButtonSpecChanged;

    /// <summary>
    /// Occurs when a single scheme color is changed.
    /// </summary>
    public event EventHandler<SchemeColorChangedEventArgs>? SchemeColorChanged;
    #endregion

    #region Identity
    /// <summary>Initializes a new instance of the <see cref="PaletteBase" /> class.</summary>
    protected PaletteBase()
    {
        // We need to notice when system color settings change
        SystemEvents.UserPreferenceChanged += OnUserPreferenceChanged;

        // Inherit means we need to calculate the value next time it is requested
        _dragFeedback = PaletteDragFeedback.Inherit;

        BaseFont = _defaultFontStyle;
        ThemeName = @"PaletteBase"; // DisallowNull !
    }
    #endregion

    #region UseThemeFormChromeBorderWidth
    private InheritBool _allowFormChrome = InheritBool.True;

    /// <summary>
    /// Gets or sets a value indicating if KryptonForm instances should UseThemeFormChromeBorderWidth.
    /// </summary>
    /// <returns>InheritBool value.</returns>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Should KryptonForm instances UseThemeFormChromeBorderWidth.")]
    [DefaultValue(InheritBool.Inherit)]
    public virtual InheritBool UseThemeFormChromeBorderWidth
    {
        get => _allowFormChrome;

        set
        {
            if (_allowFormChrome != value)
            {
                _allowFormChrome = value;
                OnUseThemeFormChromeBorderWidthChanged(this, EventArgs.Empty);
            }
        }
    }
    private void ResetUseThemeFormChromeBorderWidth() => UseThemeFormChromeBorderWidth = InheritBool.True;
    private bool ShouldSerializeUseThemeFormChromeBorderWidth() => UseThemeFormChromeBorderWidth != InheritBool.True;
    #endregion

    #region Renderer
    /// <summary>
    /// Gets the renderer to use for this palette.
    /// </summary>
    /// <returns>Renderer to use for drawing palette settings.</returns>
    public abstract IRenderer GetRenderer();
    #endregion

    #region Back
    /// <summary>
    /// Gets a value indicating if background should be drawn.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public abstract InheritBool GetBackDraw(PaletteBackStyle style, PaletteState state);

    /// <summary>
    /// Gets the graphics drawing hint for the background.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteGraphicsHint value.</returns>
    public abstract PaletteGraphicsHint GetBackGraphicsHint(PaletteBackStyle style, PaletteState state);

    /// <summary>
    /// Gets the first background color.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetBackColor1(PaletteBackStyle style, PaletteState state);

    /// <summary>
    /// Gets the second back color.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetBackColor2(PaletteBackStyle style, PaletteState state);

    /// <summary>
    /// Gets the color background drawing style.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public abstract PaletteColorStyle GetBackColorStyle(PaletteBackStyle style, PaletteState state);

    /// <summary>
    /// Gets the color alignment.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public abstract PaletteRectangleAlign GetBackColorAlign(PaletteBackStyle style, PaletteState state);

    /// <summary>
    /// Gets the color background angle.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public abstract float GetBackColorAngle(PaletteBackStyle style, PaletteState state);

    /// <summary>
    /// Gets a background image.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public abstract Image? GetBackImage(PaletteBackStyle style, PaletteState state);

    /// <summary>
    /// Gets the background image style.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public abstract PaletteImageStyle GetBackImageStyle(PaletteBackStyle style, PaletteState state);

    /// <summary>
    /// Gets the image alignment.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public abstract PaletteRectangleAlign GetBackImageAlign(PaletteBackStyle style, PaletteState state);
    #endregion

    #region Border
    /// <summary>
    /// Gets a value indicating if border should be drawn.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public abstract InheritBool GetBorderDraw(PaletteBorderStyle style, PaletteState state);

    /// <summary>
    /// Gets a value indicating which borders to draw.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteDrawBorders value.</returns>
    public abstract PaletteDrawBorders GetBorderDrawBorders(PaletteBorderStyle style, PaletteState state);

    /// <summary>
    /// Gets the graphics drawing hint for the border.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteGraphicsHint value.</returns>
    public abstract PaletteGraphicsHint GetBorderGraphicsHint(PaletteBorderStyle style, PaletteState state);

    /// <summary>
    /// Gets the first border color.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetBorderColor1(PaletteBorderStyle style, PaletteState state);

    /// <summary>
    /// Gets the second border color.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetBorderColor2(PaletteBorderStyle style, PaletteState state);

    /// <summary>
    /// Gets the color border drawing style.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public abstract PaletteColorStyle GetBorderColorStyle(PaletteBorderStyle style, PaletteState state);

    /// <summary>
    /// Gets the color border alignment.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public abstract PaletteRectangleAlign GetBorderColorAlign(PaletteBorderStyle style, PaletteState state);

    /// <summary>
    /// Gets the color border angle.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public abstract float GetBorderColorAngle(PaletteBorderStyle style, PaletteState state);

    /// <summary>
    /// Gets the border width.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Integer width.</returns>
    public abstract int GetBorderWidth(PaletteBorderStyle style, PaletteState state);

    /// <summary>
    /// Gets the border corner rounding.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Float rounding.</returns>
    public abstract float GetBorderRounding(PaletteBorderStyle style, PaletteState state);

    /// <summary>
    /// Gets a border image.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public abstract Image? GetBorderImage(PaletteBorderStyle style, PaletteState state);

    /// <summary>
    /// Gets the border image style.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public abstract PaletteImageStyle GetBorderImageStyle(PaletteBorderStyle style, PaletteState state);

    /// <summary>
    /// Gets the image border alignment.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public abstract PaletteRectangleAlign GetBorderImageAlign(PaletteBorderStyle style, PaletteState state);
    #endregion

    #region Content
    /// <summary>
    /// Gets a value indicating if content should be drawn.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public abstract InheritBool GetContentDraw(PaletteContentStyle style, PaletteState state);

    /// <summary>
    /// Gets a value indicating if content should be drawn with focus indication.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public abstract InheritBool GetContentDrawFocus(PaletteContentStyle style, PaletteState state);

    /// <summary>
    /// Gets the horizontal relative alignment of the image.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public abstract PaletteRelativeAlign GetContentImageH(PaletteContentStyle style, PaletteState state);

    /// <summary>
    /// Gets the vertical relative alignment of the image.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public abstract PaletteRelativeAlign GetContentImageV(PaletteContentStyle style, PaletteState state);

    /// <summary>
    /// Gets the effect applied to drawing of the image.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteImageEffect value.</returns>
    public abstract PaletteImageEffect GetContentImageEffect(PaletteContentStyle style, PaletteState state);

    /// <summary>
    /// Gets the image color to remap into another color.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetContentImageColorMap(PaletteContentStyle style, PaletteState state);

    /// <summary>
    /// Gets the color to use in place of the image map color.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetContentImageColorTo(PaletteContentStyle style, PaletteState state);

    /// <summary>
    /// Gets the image color that should be transparent.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetContentImageColorTransparent(PaletteContentStyle style, PaletteState state);

    /// <summary>
    /// Gets the font for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public abstract Font? GetContentShortTextFont(PaletteContentStyle style, PaletteState state);

    /// <summary>
    /// Gets the font for the short text by generating a new font instance.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public abstract Font? GetContentShortTextNewFont(PaletteContentStyle style, PaletteState state);

    /// <summary>
    /// Gets the rendering hint for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextHint value.</returns>
    public abstract PaletteTextHint GetContentShortTextHint(PaletteContentStyle style, PaletteState state);

    /// <summary>
    /// Gets the prefix drawing setting for short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextPrefix value.</returns>
    public abstract PaletteTextHotkeyPrefix GetContentShortTextPrefix(PaletteContentStyle style, PaletteState state);

    /// <summary>
    /// Gets the flag indicating if multiline text is allowed for short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public abstract InheritBool GetContentShortTextMultiLine(PaletteContentStyle style, PaletteState state);

    /// <summary>
    /// Gets the text trimming to use for short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextTrim value.</returns>
    public abstract PaletteTextTrim GetContentShortTextTrim(PaletteContentStyle style, PaletteState state);

    /// <summary>
    /// Gets the horizontal relative alignment of the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public abstract PaletteRelativeAlign GetContentShortTextH(PaletteContentStyle style, PaletteState state);

    /// <summary>
    /// Gets the vertical relative alignment of the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public abstract PaletteRelativeAlign GetContentShortTextV(PaletteContentStyle style, PaletteState state);

    /// <summary>
    /// Gets the horizontal relative alignment of multiline short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public abstract PaletteRelativeAlign GetContentShortTextMultiLineH(PaletteContentStyle style, PaletteState state);

    /// <summary>
    /// Gets the first back color for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetContentShortTextColor1(PaletteContentStyle style, PaletteState state);

    /// <summary>
    /// Gets the second back color for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetContentShortTextColor2(PaletteContentStyle style, PaletteState state);

    /// <summary>
    /// Gets the color drawing style for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public abstract PaletteColorStyle GetContentShortTextColorStyle(PaletteContentStyle style, PaletteState state);

    /// <summary>
    /// Gets the color alignment for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public abstract PaletteRectangleAlign GetContentShortTextColorAlign(PaletteContentStyle style, PaletteState state);

    /// <summary>
    /// Gets the color background angle for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public abstract float GetContentShortTextColorAngle(PaletteContentStyle style, PaletteState state);

    /// <summary>
    /// Gets a background image for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public abstract Image? GetContentShortTextImage(PaletteContentStyle style, PaletteState state);

    /// <summary>
    /// Gets the background image style.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public abstract PaletteImageStyle GetContentShortTextImageStyle(PaletteContentStyle style, PaletteState state);

    /// <summary>
    /// Gets the image alignment for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public abstract PaletteRectangleAlign GetContentShortTextImageAlign(PaletteContentStyle style, PaletteState state);

    /// <summary>
    /// Gets the font for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public abstract Font? GetContentLongTextFont(PaletteContentStyle style, PaletteState state);

    /// <summary>
    /// Gets the font for the long text by generating a new font instance.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public abstract Font? GetContentLongTextNewFont(PaletteContentStyle style, PaletteState state);

    /// <summary>
    /// Gets the rendering hint for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextHint value.</returns>
    public abstract PaletteTextHint GetContentLongTextHint(PaletteContentStyle style, PaletteState state);

    /// <summary>
    /// Gets the prefix drawing setting for long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextPrefix value.</returns>
    public abstract PaletteTextHotkeyPrefix GetContentLongTextPrefix(PaletteContentStyle style, PaletteState state);

    /// <summary>
    /// Gets the flag indicating if multiline text is allowed for long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public abstract InheritBool GetContentLongTextMultiLine(PaletteContentStyle style, PaletteState state);

    /// <summary>
    /// Gets the text trimming to use for long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextTrim value.</returns>
    public abstract PaletteTextTrim GetContentLongTextTrim(PaletteContentStyle style, PaletteState state);

    /// <summary>
    /// Gets the horizontal relative alignment of the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public abstract PaletteRelativeAlign GetContentLongTextH(PaletteContentStyle style, PaletteState state);

    /// <summary>
    /// Gets the vertical relative alignment of the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public abstract PaletteRelativeAlign GetContentLongTextV(PaletteContentStyle style, PaletteState state);

    /// <summary>
    /// Gets the horizontal relative alignment of multiline long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public abstract PaletteRelativeAlign GetContentLongTextMultiLineH(PaletteContentStyle style, PaletteState state);

    /// <summary>
    /// Gets the first back color for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetContentLongTextColor1(PaletteContentStyle style, PaletteState state);

    /// <summary>
    /// Gets the second back color for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetContentLongTextColor2(PaletteContentStyle style, PaletteState state);

    /// <summary>
    /// Gets the color drawing style for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public abstract PaletteColorStyle GetContentLongTextColorStyle(PaletteContentStyle style, PaletteState state);

    /// <summary>
    /// Gets the color alignment for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public abstract PaletteRectangleAlign GetContentLongTextColorAlign(PaletteContentStyle style, PaletteState state);

    /// <summary>
    /// Gets the color background angle for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public abstract float GetContentLongTextColorAngle(PaletteContentStyle style, PaletteState state);

    /// <summary>
    /// Gets a background image for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public abstract Image? GetContentLongTextImage(PaletteContentStyle style, PaletteState state);

    /// <summary>
    /// Gets the background image style for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public abstract PaletteImageStyle GetContentLongTextImageStyle(PaletteContentStyle style, PaletteState state);

    /// <summary>
    /// Gets the image alignment for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public abstract PaletteRectangleAlign GetContentLongTextImageAlign(PaletteContentStyle style, PaletteState state);

    /// <summary>
    /// Gets the padding between the border and content drawing.
    /// </summary>
    /// <param name="owningForm"></param>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Padding value.</returns>
    public abstract Padding GetBorderContentPadding(KryptonForm? owningForm, PaletteContentStyle style,
        PaletteState state);

    /// <summary>
    /// Gets the padding between adjacent content items.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Integer value.</returns>
    public abstract int GetContentAdjacentGap(PaletteContentStyle style, PaletteState state);
    #endregion

    #region Metric

    /// <summary>
    /// Gets an integer metric value.
    /// </summary>
    /// <param name="owningForm"></param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <param name="metric">Requested metric.</param>
    /// <returns>Integer value.</returns>
    public abstract int GetMetricInt(KryptonForm? owningForm, PaletteState state, PaletteMetricInt metric);

    /// <summary>
    /// Gets a boolean metric value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <param name="metric">Requested metric.</param>
    /// <returns>InheritBool value.</returns>
    public abstract InheritBool GetMetricBool(PaletteState state, PaletteMetricBool metric);

    /// <summary>
    /// Gets a padding metric value.
    /// </summary>
    /// <param name="owningForm"></param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <param name="metric">Requested metric.</param>
    /// <returns>Padding value.</returns>
    public abstract Padding GetMetricPadding(KryptonForm? owningForm, PaletteState state, PaletteMetricPadding metric);

    #endregion

    #region Images

    /// <summary>
    /// Gets a tree view image appropriate for the provided state.
    /// </summary>
    /// <param name="expanded">Is the node expanded</param>
    /// <returns>Appropriate image for drawing; otherwise null.</returns>
    public abstract Image? GetTreeViewImage(bool expanded);

    /// <summary>
    /// Gets a check box image appropriate for the provided state.
    /// </summary>
    /// <param name="enabled">Is the check box enabled.</param>
    /// <param name="checkState">Is the check box checked/unchecked/indeterminate.</param>
    /// <param name="tracking">Is the check box being hot tracked.</param>
    /// <param name="pressed">Is the check box being pressed.</param>
    /// <returns>Appropriate image for drawing; otherwise null.</returns>
    public abstract Image? GetCheckBoxImage(bool enabled, CheckState checkState, bool tracking, bool pressed);

    /// <summary>
    /// Gets a check box image appropriate for the provided state.
    /// </summary>
    /// <param name="enabled">Is the radio button enabled.</param>
    /// <param name="checkState">Is the radio button checked.</param>
    /// <param name="tracking">Is the radio button being hot tracked.</param>
    /// <param name="pressed">Is the radio button being pressed.</param>
    /// <returns>Appropriate image for drawing; otherwise null.</returns>
    public abstract Image? GetRadioButtonImage(bool enabled, bool checkState, bool tracking, bool pressed);

    /// <summary>
    /// Gets a checked image appropriate for a context menu item.
    /// </summary>
    /// <returns>Appropriate image for drawing; otherwise null.</returns>
    public abstract Image? GetContextMenuCheckedImage();

    /// <summary>
    /// Gets a indeterminate image appropriate for a context menu item.
    /// </summary>
    /// <returns>Appropriate image for drawing; otherwise null.</returns>
    public abstract Image? GetContextMenuIndeterminateImage();

    /// <summary>
    /// Gets an image indicating a sub-menu on a context menu item.
    /// </summary>
    /// <returns>Appropriate image for drawing; otherwise null.</returns>
    public abstract Image? GetContextMenuSubMenuImage();

    /// <summary>
    /// Gets a check box image appropriate for the provided state.
    /// </summary>
    /// <param name="button">Enum of the button to fetch.</param>
    /// <param name="state">State of the button to fetch.</param>
    /// <returns>Appropriate image for drawing; otherwise null.</returns>
    public abstract Image? GetGalleryButtonImage(PaletteRibbonGalleryButton button, PaletteState state);
    #endregion

    #region ButtonSpec

    /// <summary>
    /// Gets the icon to display for the button.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>Icon value.</returns>
    public virtual Icon? GetButtonSpecIcon(PaletteButtonSpecStyle style)
    {
        switch (style)
        {
            case PaletteButtonSpecStyle.Generic:
            case PaletteButtonSpecStyle.Close:
            case PaletteButtonSpecStyle.Context:
            case PaletteButtonSpecStyle.Next:
            case PaletteButtonSpecStyle.Previous:
            case PaletteButtonSpecStyle.ArrowLeft:
            case PaletteButtonSpecStyle.ArrowRight:
            case PaletteButtonSpecStyle.ArrowUp:
            case PaletteButtonSpecStyle.ArrowDown:
            case PaletteButtonSpecStyle.DropDown:
            case PaletteButtonSpecStyle.PinVertical:
            case PaletteButtonSpecStyle.PinHorizontal:
            case PaletteButtonSpecStyle.FormClose:
            case PaletteButtonSpecStyle.FormMin:
            case PaletteButtonSpecStyle.FormMax:
            case PaletteButtonSpecStyle.FormRestore:
            case PaletteButtonSpecStyle.FormHelp:
            case PaletteButtonSpecStyle.PendantClose:
            case PaletteButtonSpecStyle.PendantMin:
            case PaletteButtonSpecStyle.PendantRestore:
            case PaletteButtonSpecStyle.WorkspaceMaximize:
            case PaletteButtonSpecStyle.WorkspaceRestore:
            case PaletteButtonSpecStyle.RibbonMinimize:
            case PaletteButtonSpecStyle.RibbonExpand:
                return null;
            case PaletteButtonSpecStyle.New:
            case PaletteButtonSpecStyle.Open:
            case PaletteButtonSpecStyle.SaveAll:
            case PaletteButtonSpecStyle.SaveAs:
            case PaletteButtonSpecStyle.Save:
            case PaletteButtonSpecStyle.Cut:
            case PaletteButtonSpecStyle.Copy:
            case PaletteButtonSpecStyle.Paste:
            case PaletteButtonSpecStyle.Undo:
            case PaletteButtonSpecStyle.Redo:
            case PaletteButtonSpecStyle.PageSetup:
            case PaletteButtonSpecStyle.PrintPreview:
            case PaletteButtonSpecStyle.Print:
            case PaletteButtonSpecStyle.QuickPrint:
            default:
                // Should never happen!
                Debug.Assert(false);
                throw DebugTools.NotImplemented(style.ToString());
        }
    }

    /// <summary>
    /// Gets the image to display for the button.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <param name="state">State for which image is required.</param>
    /// <returns>Image value.</returns>
    public abstract Image? GetButtonSpecImage(PaletteButtonSpecStyle style, PaletteState state);

    /// <summary>
    /// Gets the image transparent color.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>Color value.</returns>
    public virtual Color GetButtonSpecImageTransparentColor(PaletteButtonSpecStyle style)
    {
        switch (style)
        {
            case PaletteButtonSpecStyle.Generic:
                return GlobalStaticValues.EMPTY_COLOR;
            case PaletteButtonSpecStyle.Close:
            case PaletteButtonSpecStyle.Context:
            case PaletteButtonSpecStyle.Next:
            case PaletteButtonSpecStyle.Previous:
            case PaletteButtonSpecStyle.ArrowLeft:
            case PaletteButtonSpecStyle.ArrowRight:
            case PaletteButtonSpecStyle.ArrowUp:
            case PaletteButtonSpecStyle.ArrowDown:
            case PaletteButtonSpecStyle.DropDown:
            case PaletteButtonSpecStyle.PinVertical:
            case PaletteButtonSpecStyle.PinHorizontal:
            case PaletteButtonSpecStyle.FormClose:
            case PaletteButtonSpecStyle.FormMin:
            case PaletteButtonSpecStyle.FormMax:
            case PaletteButtonSpecStyle.FormRestore:
            case PaletteButtonSpecStyle.FormHelp:
            case PaletteButtonSpecStyle.PendantClose:
            case PaletteButtonSpecStyle.PendantMin:
            case PaletteButtonSpecStyle.PendantRestore:
            case PaletteButtonSpecStyle.WorkspaceMaximize:
            case PaletteButtonSpecStyle.WorkspaceRestore:
            case PaletteButtonSpecStyle.RibbonMinimize:
            case PaletteButtonSpecStyle.RibbonExpand:
                return GlobalStaticValues.TRANSPARENCY_KEY_COLOR;
            case PaletteButtonSpecStyle.New:
            case PaletteButtonSpecStyle.Open:
            case PaletteButtonSpecStyle.SaveAll:
            case PaletteButtonSpecStyle.SaveAs:
            case PaletteButtonSpecStyle.Save:
            case PaletteButtonSpecStyle.Cut:
            case PaletteButtonSpecStyle.Copy:
            case PaletteButtonSpecStyle.Paste:
            case PaletteButtonSpecStyle.Undo:
            case PaletteButtonSpecStyle.Redo:
            case PaletteButtonSpecStyle.PageSetup:
            case PaletteButtonSpecStyle.PrintPreview:
            case PaletteButtonSpecStyle.Print:
            case PaletteButtonSpecStyle.QuickPrint:
                return GlobalStaticValues.EMPTY_COLOR;
            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(style.ToString());
                return GlobalStaticValues.EMPTY_COLOR;
        }
    }

    /// <summary>
    /// Gets the short text to display for the button.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>String value.</returns>
    public virtual string GetButtonSpecShortText(PaletteButtonSpecStyle style)
    {
        switch (style)
        {
            case PaletteButtonSpecStyle.Generic:
            case PaletteButtonSpecStyle.Close:
            case PaletteButtonSpecStyle.Context:
            case PaletteButtonSpecStyle.Next:
            case PaletteButtonSpecStyle.Previous:
            case PaletteButtonSpecStyle.ArrowLeft:
            case PaletteButtonSpecStyle.ArrowRight:
            case PaletteButtonSpecStyle.ArrowUp:
            case PaletteButtonSpecStyle.ArrowDown:
            case PaletteButtonSpecStyle.DropDown:
            case PaletteButtonSpecStyle.PinVertical:
            case PaletteButtonSpecStyle.PinHorizontal:
            case PaletteButtonSpecStyle.FormClose:
            case PaletteButtonSpecStyle.FormMin:
            case PaletteButtonSpecStyle.FormMax:
            case PaletteButtonSpecStyle.FormRestore:
            case PaletteButtonSpecStyle.FormHelp:
            case PaletteButtonSpecStyle.PendantClose:
            case PaletteButtonSpecStyle.PendantMin:
            case PaletteButtonSpecStyle.PendantRestore:
            case PaletteButtonSpecStyle.WorkspaceMaximize:
            case PaletteButtonSpecStyle.WorkspaceRestore:
            case PaletteButtonSpecStyle.RibbonMinimize:
            case PaletteButtonSpecStyle.RibbonExpand:
            case PaletteButtonSpecStyle.New:
            case PaletteButtonSpecStyle.Open:
            case PaletteButtonSpecStyle.SaveAll:
            case PaletteButtonSpecStyle.SaveAs:
            case PaletteButtonSpecStyle.Save:
            case PaletteButtonSpecStyle.Cut:
            case PaletteButtonSpecStyle.Copy:
            case PaletteButtonSpecStyle.Paste:
            case PaletteButtonSpecStyle.Undo:
            case PaletteButtonSpecStyle.Redo:
            case PaletteButtonSpecStyle.PageSetup:
            case PaletteButtonSpecStyle.PrintPreview:
            case PaletteButtonSpecStyle.Print:
            case PaletteButtonSpecStyle.QuickPrint:
                return string.Empty;
            default:
                // Should never happen!
                Debug.Assert(false);
                throw DebugTools.NotImplemented(style.ToString());
        }
    }

    /// <summary>
    /// Gets the long text to display for the button.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>String value.</returns>
    public virtual string GetButtonSpecLongText(PaletteButtonSpecStyle style)
    {
        switch (style)
        {
            case PaletteButtonSpecStyle.Generic:
            case PaletteButtonSpecStyle.Close:
            case PaletteButtonSpecStyle.Context:
            case PaletteButtonSpecStyle.Next:
            case PaletteButtonSpecStyle.Previous:
            case PaletteButtonSpecStyle.ArrowLeft:
            case PaletteButtonSpecStyle.ArrowRight:
            case PaletteButtonSpecStyle.ArrowUp:
            case PaletteButtonSpecStyle.ArrowDown:
            case PaletteButtonSpecStyle.DropDown:
            case PaletteButtonSpecStyle.PinVertical:
            case PaletteButtonSpecStyle.PinHorizontal:
            case PaletteButtonSpecStyle.FormClose:
            case PaletteButtonSpecStyle.FormMin:
            case PaletteButtonSpecStyle.FormMax:
            case PaletteButtonSpecStyle.FormRestore:
            case PaletteButtonSpecStyle.FormHelp:
            case PaletteButtonSpecStyle.PendantClose:
            case PaletteButtonSpecStyle.PendantMin:
            case PaletteButtonSpecStyle.PendantRestore:
            case PaletteButtonSpecStyle.WorkspaceMaximize:
            case PaletteButtonSpecStyle.WorkspaceRestore:
            case PaletteButtonSpecStyle.RibbonMinimize:
            case PaletteButtonSpecStyle.RibbonExpand:
            case PaletteButtonSpecStyle.New:
            case PaletteButtonSpecStyle.Open:
            case PaletteButtonSpecStyle.SaveAll:
            case PaletteButtonSpecStyle.SaveAs:
            case PaletteButtonSpecStyle.Save:
            case PaletteButtonSpecStyle.Cut:
            case PaletteButtonSpecStyle.Copy:
            case PaletteButtonSpecStyle.Paste:
            case PaletteButtonSpecStyle.Undo:
            case PaletteButtonSpecStyle.Redo:
            case PaletteButtonSpecStyle.PageSetup:
            case PaletteButtonSpecStyle.PrintPreview:
            case PaletteButtonSpecStyle.Print:
            case PaletteButtonSpecStyle.QuickPrint:
                return string.Empty;
            default:
                // Should never happen!
                Debug.Assert(false);
                throw DebugTools.NotImplemented(style.ToString());
        }
    }

    /// <summary>
    /// Gets the tooltip title text to display for the button.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>String value.</returns>
    public virtual string GetButtonSpecToolTipTitle(PaletteButtonSpecStyle style)
    {
        switch (style)
        {
            // TODO: Use LanguageManager for strings
            case PaletteButtonSpecStyle.Close:
            case PaletteButtonSpecStyle.PendantClose:
            case PaletteButtonSpecStyle.FormClose:
                return "Close";
            case PaletteButtonSpecStyle.Context:
                return "Select";
            case PaletteButtonSpecStyle.Next:
                return "Next";
            case PaletteButtonSpecStyle.Previous:
                return "Previous";
            case PaletteButtonSpecStyle.FormMin:
            case PaletteButtonSpecStyle.PendantMin:
                return "Minimize";
            case PaletteButtonSpecStyle.FormMax:
                return "Maximize";
            case PaletteButtonSpecStyle.PendantRestore:
            case PaletteButtonSpecStyle.FormRestore:
                return "Restore";
            case PaletteButtonSpecStyle.FormHelp:
                return nameof(Help);
            case PaletteButtonSpecStyle.RibbonMinimize:
                return "Minimize";
            case PaletteButtonSpecStyle.RibbonExpand:
                return "Expand";
            case PaletteButtonSpecStyle.PinVertical:
            case PaletteButtonSpecStyle.PinHorizontal:
            case PaletteButtonSpecStyle.Generic:
            case PaletteButtonSpecStyle.ArrowLeft:
            case PaletteButtonSpecStyle.ArrowRight:
            case PaletteButtonSpecStyle.ArrowUp:
            case PaletteButtonSpecStyle.ArrowDown:
            case PaletteButtonSpecStyle.DropDown:
            case PaletteButtonSpecStyle.New:
            case PaletteButtonSpecStyle.Open:
            case PaletteButtonSpecStyle.SaveAll:
            case PaletteButtonSpecStyle.SaveAs:
            case PaletteButtonSpecStyle.Save:
            case PaletteButtonSpecStyle.Cut:
            case PaletteButtonSpecStyle.Copy:
            case PaletteButtonSpecStyle.Paste:
            case PaletteButtonSpecStyle.Undo:
            case PaletteButtonSpecStyle.Redo:
            case PaletteButtonSpecStyle.PageSetup:
            case PaletteButtonSpecStyle.PrintPreview:
            case PaletteButtonSpecStyle.Print:
            case PaletteButtonSpecStyle.QuickPrint:
                return string.Empty;
            default:
                // Should never happen!
                Debug.Assert(false);
                throw DebugTools.NotImplemented(style.ToString());
        }
    }

    /// <summary>
    /// Gets the color to remap from the image to the container foreground.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>Color value.</returns>
    public virtual Color GetButtonSpecColorMap(PaletteButtonSpecStyle style)
    {
        switch (style)
        {
            case PaletteButtonSpecStyle.FormClose:
            case PaletteButtonSpecStyle.FormMin:
            case PaletteButtonSpecStyle.FormMax:
            case PaletteButtonSpecStyle.FormRestore:
            case PaletteButtonSpecStyle.FormHelp:
            case PaletteButtonSpecStyle.PendantClose:
            case PaletteButtonSpecStyle.PendantMin:
            case PaletteButtonSpecStyle.PendantRestore:
            case PaletteButtonSpecStyle.Generic:
            case PaletteButtonSpecStyle.New:
            case PaletteButtonSpecStyle.Open:
            case PaletteButtonSpecStyle.SaveAll:
            case PaletteButtonSpecStyle.SaveAs:
            case PaletteButtonSpecStyle.Save:
            case PaletteButtonSpecStyle.Cut:
            case PaletteButtonSpecStyle.Copy:
            case PaletteButtonSpecStyle.Paste:
            case PaletteButtonSpecStyle.Undo:
            case PaletteButtonSpecStyle.Redo:
            case PaletteButtonSpecStyle.PageSetup:
            case PaletteButtonSpecStyle.PrintPreview:
            case PaletteButtonSpecStyle.Print:
            case PaletteButtonSpecStyle.QuickPrint:
                return GlobalStaticValues.EMPTY_COLOR;
            case PaletteButtonSpecStyle.Close:
            case PaletteButtonSpecStyle.Context:
            case PaletteButtonSpecStyle.Next:
            case PaletteButtonSpecStyle.Previous:
            case PaletteButtonSpecStyle.ArrowLeft:
            case PaletteButtonSpecStyle.ArrowRight:
            case PaletteButtonSpecStyle.ArrowUp:
            case PaletteButtonSpecStyle.ArrowDown:
            case PaletteButtonSpecStyle.DropDown:
            case PaletteButtonSpecStyle.PinVertical:
            case PaletteButtonSpecStyle.PinHorizontal:
            case PaletteButtonSpecStyle.WorkspaceMaximize:
            case PaletteButtonSpecStyle.WorkspaceRestore:
            case PaletteButtonSpecStyle.RibbonMinimize:
            case PaletteButtonSpecStyle.RibbonExpand:
                return Color.Black;
            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(style.ToString());
                return GlobalStaticValues.EMPTY_COLOR;
        }
    }

    /// <summary>
    /// Gets the color to remap to transparent.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>Color value.</returns>
    public virtual Color GetButtonSpecColorTransparent(PaletteButtonSpecStyle style)
    {
        switch (style)
        {
            case PaletteButtonSpecStyle.Generic:
            case PaletteButtonSpecStyle.New:
            case PaletteButtonSpecStyle.Open:
            case PaletteButtonSpecStyle.SaveAll:
            case PaletteButtonSpecStyle.SaveAs:
            case PaletteButtonSpecStyle.Save:
            case PaletteButtonSpecStyle.Cut:
            case PaletteButtonSpecStyle.Copy:
            case PaletteButtonSpecStyle.Paste:
            case PaletteButtonSpecStyle.Undo:
            case PaletteButtonSpecStyle.Redo:
            case PaletteButtonSpecStyle.PageSetup:
            case PaletteButtonSpecStyle.PrintPreview:
            case PaletteButtonSpecStyle.Print:
            case PaletteButtonSpecStyle.QuickPrint:
                return GlobalStaticValues.EMPTY_COLOR;
            case PaletteButtonSpecStyle.Close:
            case PaletteButtonSpecStyle.Context:
            case PaletteButtonSpecStyle.Next:
            case PaletteButtonSpecStyle.Previous:
            case PaletteButtonSpecStyle.ArrowLeft:
            case PaletteButtonSpecStyle.ArrowRight:
            case PaletteButtonSpecStyle.ArrowUp:
            case PaletteButtonSpecStyle.ArrowDown:
            case PaletteButtonSpecStyle.DropDown:
            case PaletteButtonSpecStyle.PinVertical:
            case PaletteButtonSpecStyle.PinHorizontal:
            case PaletteButtonSpecStyle.FormClose:
            case PaletteButtonSpecStyle.FormMin:
            case PaletteButtonSpecStyle.FormMax:
            case PaletteButtonSpecStyle.FormRestore:
            case PaletteButtonSpecStyle.FormHelp:
            case PaletteButtonSpecStyle.PendantClose:
            case PaletteButtonSpecStyle.PendantMin:
            case PaletteButtonSpecStyle.PendantRestore:
            case PaletteButtonSpecStyle.WorkspaceMaximize:
            case PaletteButtonSpecStyle.WorkspaceRestore:
            case PaletteButtonSpecStyle.RibbonMinimize:
            case PaletteButtonSpecStyle.RibbonExpand:
                return GlobalStaticValues.TRANSPARENCY_KEY_COLOR;
            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(style.ToString());
                return GlobalStaticValues.EMPTY_COLOR;
        }
    }

    /// <summary>
    /// Gets the button style used for drawing the button.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>PaletteButtonStyle value.</returns>
    public virtual PaletteButtonStyle GetButtonSpecStyle(PaletteButtonSpecStyle style)
    {
        switch (style)
        {
            case PaletteButtonSpecStyle.FormMin:
            case PaletteButtonSpecStyle.FormMax:
            case PaletteButtonSpecStyle.FormRestore:
            case PaletteButtonSpecStyle.FormHelp:
                return PaletteButtonStyle.Form;
            case PaletteButtonSpecStyle.FormClose:
                return PaletteButtonStyle.FormClose;
            case PaletteButtonSpecStyle.Generic:
            case PaletteButtonSpecStyle.Close:
            case PaletteButtonSpecStyle.Context:
            case PaletteButtonSpecStyle.Next:
            case PaletteButtonSpecStyle.Previous:
            case PaletteButtonSpecStyle.ArrowLeft:
            case PaletteButtonSpecStyle.ArrowRight:
            case PaletteButtonSpecStyle.ArrowUp:
            case PaletteButtonSpecStyle.ArrowDown:
            case PaletteButtonSpecStyle.DropDown:
            case PaletteButtonSpecStyle.PinVertical:
            case PaletteButtonSpecStyle.PinHorizontal:
            case PaletteButtonSpecStyle.PendantClose:
            case PaletteButtonSpecStyle.PendantMin:
            case PaletteButtonSpecStyle.PendantRestore:
            case PaletteButtonSpecStyle.WorkspaceMaximize:
            case PaletteButtonSpecStyle.WorkspaceRestore:
            case PaletteButtonSpecStyle.RibbonMinimize:
            case PaletteButtonSpecStyle.RibbonExpand:
            case PaletteButtonSpecStyle.New:
            case PaletteButtonSpecStyle.Open:
            case PaletteButtonSpecStyle.SaveAll:
            case PaletteButtonSpecStyle.SaveAs:
            case PaletteButtonSpecStyle.Save:
            case PaletteButtonSpecStyle.Cut:
            case PaletteButtonSpecStyle.Copy:
            case PaletteButtonSpecStyle.Paste:
            case PaletteButtonSpecStyle.Undo:
            case PaletteButtonSpecStyle.Redo:
            case PaletteButtonSpecStyle.PageSetup:
            case PaletteButtonSpecStyle.PrintPreview:
            case PaletteButtonSpecStyle.Print:
            case PaletteButtonSpecStyle.QuickPrint:
                return PaletteButtonStyle.ButtonSpec;
            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(style.ToString());
                return PaletteButtonStyle.ButtonSpec;
        }
    }

    /// <summary>
    /// Get the location for the button.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>HeaderLocation value.</returns>
    public virtual HeaderLocation GetButtonSpecLocation(PaletteButtonSpecStyle style)
    {
        switch (style)
        {
            case PaletteButtonSpecStyle.Generic:
            case PaletteButtonSpecStyle.Close:
            case PaletteButtonSpecStyle.Context:
            case PaletteButtonSpecStyle.Next:
            case PaletteButtonSpecStyle.Previous:
            case PaletteButtonSpecStyle.ArrowLeft:
            case PaletteButtonSpecStyle.ArrowRight:
            case PaletteButtonSpecStyle.ArrowUp:
            case PaletteButtonSpecStyle.ArrowDown:
            case PaletteButtonSpecStyle.DropDown:
            case PaletteButtonSpecStyle.PinVertical:
            case PaletteButtonSpecStyle.PinHorizontal:
            case PaletteButtonSpecStyle.FormClose:
            case PaletteButtonSpecStyle.FormMin:
            case PaletteButtonSpecStyle.FormMax:
            case PaletteButtonSpecStyle.FormRestore:
            case PaletteButtonSpecStyle.FormHelp:
            case PaletteButtonSpecStyle.PendantClose:
            case PaletteButtonSpecStyle.PendantMin:
            case PaletteButtonSpecStyle.PendantRestore:
            case PaletteButtonSpecStyle.WorkspaceMaximize:
            case PaletteButtonSpecStyle.WorkspaceRestore:
            case PaletteButtonSpecStyle.RibbonMinimize:
            case PaletteButtonSpecStyle.RibbonExpand:
            case PaletteButtonSpecStyle.New:
            case PaletteButtonSpecStyle.Open:
            case PaletteButtonSpecStyle.SaveAll:
            case PaletteButtonSpecStyle.SaveAs:
            case PaletteButtonSpecStyle.Save:
            case PaletteButtonSpecStyle.Cut:
            case PaletteButtonSpecStyle.Copy:
            case PaletteButtonSpecStyle.Paste:
            case PaletteButtonSpecStyle.Undo:
            case PaletteButtonSpecStyle.Redo:
            case PaletteButtonSpecStyle.PageSetup:
            case PaletteButtonSpecStyle.PrintPreview:
            case PaletteButtonSpecStyle.Print:
            case PaletteButtonSpecStyle.QuickPrint:
                return HeaderLocation.PrimaryHeader;
            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(style.ToString());
                return HeaderLocation.PrimaryHeader;
        }
    }

    /// <summary>
    /// Gets the edge to position the button against.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>PaletteRelativeEdgeAlign value.</returns>
    public virtual PaletteRelativeEdgeAlign GetButtonSpecEdge(PaletteButtonSpecStyle style)
    {
        switch (style)
        {
            case PaletteButtonSpecStyle.Generic:
            case PaletteButtonSpecStyle.Close:
            case PaletteButtonSpecStyle.Context:
            case PaletteButtonSpecStyle.Next:
            case PaletteButtonSpecStyle.Previous:
            case PaletteButtonSpecStyle.ArrowLeft:
            case PaletteButtonSpecStyle.ArrowRight:
            case PaletteButtonSpecStyle.ArrowUp:
            case PaletteButtonSpecStyle.ArrowDown:
            case PaletteButtonSpecStyle.DropDown:
            case PaletteButtonSpecStyle.PinVertical:
            case PaletteButtonSpecStyle.PinHorizontal:
            case PaletteButtonSpecStyle.FormClose:
            case PaletteButtonSpecStyle.FormMin:
            case PaletteButtonSpecStyle.FormMax:
            case PaletteButtonSpecStyle.FormRestore:
            case PaletteButtonSpecStyle.FormHelp:
            case PaletteButtonSpecStyle.PendantClose:
            case PaletteButtonSpecStyle.PendantMin:
            case PaletteButtonSpecStyle.PendantRestore:
            case PaletteButtonSpecStyle.WorkspaceMaximize:
            case PaletteButtonSpecStyle.WorkspaceRestore:
            case PaletteButtonSpecStyle.RibbonMinimize:
            case PaletteButtonSpecStyle.RibbonExpand:
            case PaletteButtonSpecStyle.New:
            case PaletteButtonSpecStyle.Open:
            case PaletteButtonSpecStyle.SaveAll:
            case PaletteButtonSpecStyle.SaveAs:
            case PaletteButtonSpecStyle.Save:
            case PaletteButtonSpecStyle.Cut:
            case PaletteButtonSpecStyle.Copy:
            case PaletteButtonSpecStyle.Paste:
            case PaletteButtonSpecStyle.Undo:
            case PaletteButtonSpecStyle.Redo:
            case PaletteButtonSpecStyle.PageSetup:
            case PaletteButtonSpecStyle.PrintPreview:
            case PaletteButtonSpecStyle.Print:
            case PaletteButtonSpecStyle.QuickPrint:
                return PaletteRelativeEdgeAlign.Far;
            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(style.ToString());
                return PaletteRelativeEdgeAlign.Far;
        }
    }

    /// <summary>
    /// Gets the button orientation.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <returns>PaletteButtonOrientation value.</returns>
    public virtual PaletteButtonOrientation GetButtonSpecOrientation(PaletteButtonSpecStyle style)
    {
        switch (style)
        {
            case PaletteButtonSpecStyle.Close:
            case PaletteButtonSpecStyle.Context:
            case PaletteButtonSpecStyle.ArrowLeft:
            case PaletteButtonSpecStyle.ArrowRight:
            case PaletteButtonSpecStyle.ArrowUp:
            case PaletteButtonSpecStyle.ArrowDown:
            case PaletteButtonSpecStyle.DropDown:
            case PaletteButtonSpecStyle.PinVertical:
            case PaletteButtonSpecStyle.PinHorizontal:
            case PaletteButtonSpecStyle.FormClose:
            case PaletteButtonSpecStyle.FormMin:
            case PaletteButtonSpecStyle.FormMax:
            case PaletteButtonSpecStyle.FormRestore:
            case PaletteButtonSpecStyle.FormHelp:
            case PaletteButtonSpecStyle.PendantClose:
            case PaletteButtonSpecStyle.PendantMin:
            case PaletteButtonSpecStyle.PendantRestore:
            case PaletteButtonSpecStyle.WorkspaceMaximize:
            case PaletteButtonSpecStyle.WorkspaceRestore:
            case PaletteButtonSpecStyle.RibbonMinimize:
            case PaletteButtonSpecStyle.RibbonExpand:
            case PaletteButtonSpecStyle.New:
            case PaletteButtonSpecStyle.Open:
            case PaletteButtonSpecStyle.SaveAll:
            case PaletteButtonSpecStyle.SaveAs:
            case PaletteButtonSpecStyle.Save:
            case PaletteButtonSpecStyle.Cut:
            case PaletteButtonSpecStyle.Copy:
            case PaletteButtonSpecStyle.Paste:
            case PaletteButtonSpecStyle.Undo:
            case PaletteButtonSpecStyle.Redo:
            case PaletteButtonSpecStyle.PageSetup:
            case PaletteButtonSpecStyle.PrintPreview:
            case PaletteButtonSpecStyle.Print:
            case PaletteButtonSpecStyle.QuickPrint:
                return PaletteButtonOrientation.FixedTop;
            case PaletteButtonSpecStyle.Generic:
            case PaletteButtonSpecStyle.Next:
            case PaletteButtonSpecStyle.Previous:
                return PaletteButtonOrientation.Auto;
            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(style.ToString());
                return PaletteButtonOrientation.Auto;
        }
    }

    #endregion

    #region RibbonGeneral
    /// <summary>
    /// Gets the ribbon shape that should be used.
    /// </summary>
    /// <returns>Ribbon shape value.</returns>
    public abstract PaletteRibbonShape GetRibbonShape();

    /// <summary>
    /// Gets the text alignment for the ribbon context text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public abstract PaletteRelativeAlign GetRibbonContextTextAlign(PaletteState state);

    /// <summary>
    /// Gets the font for the ribbon context text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public abstract Font GetRibbonContextTextFont(PaletteState state);

    /// <summary>
    /// Gets the color for the ribbon context text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public abstract Color GetRibbonContextTextColor(PaletteState state);

    /// <summary>
    /// Gets the dark disabled color used for ribbon glyphs.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetRibbonDisabledDark(PaletteState state);

    /// <summary>
    /// Gets the light disabled color used for ribbon glyphs.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetRibbonDisabledLight(PaletteState state);

    /// <summary>
    /// Gets the color for the drop arrow light.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetRibbonDropArrowLight(PaletteState state);

    /// <summary>
    /// Gets the color for the drop arrow dark.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetRibbonDropArrowDark(PaletteState state);

    /// <summary>
    /// Gets the color for the dialog launcher dark.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetRibbonGroupDialogDark(PaletteState state);

    /// <summary>
    /// Gets the color for the dialog launcher light.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetRibbonGroupDialogLight(PaletteState state);

    /// <summary>
    /// Gets the color for the group separator dark.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetRibbonGroupSeparatorDark(PaletteState state);

    /// <summary>
    /// Gets the color for the group separator light.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetRibbonGroupSeparatorLight(PaletteState state);

    /// <summary>
    /// Gets the color for the minimize bar dark.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetRibbonMinimizeBarDark(PaletteState state);

    /// <summary>
    /// Gets the color for the minimize bar light.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetRibbonMinimizeBarLight(PaletteState state);

    /// <summary>
    /// Gets the gradient dark rafting color for the tab background.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetRibbonTabRowBackgroundGradientRaftingDark(PaletteState state);

    /// <summary>
    /// Gets the gradient light rafting color for the tab background.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetRibbonTabRowBackgroundGradientRaftingLight(PaletteState state);

    /// <summary>
    /// Gets the solid color for the tab background.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetRibbonTabRowBackgroundSolidColor(PaletteState state);

    /// <summary>
    /// Gets the color for the tab separator.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetRibbonTabSeparatorColor(PaletteState state);

    /// <summary>
    /// Gets the color for the tab context separators.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetRibbonTabSeparatorContextColor(PaletteState state);

    /// <summary>
    /// Gets the font for the ribbon text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public abstract Font GetRibbonTextFont(PaletteState state);

    /// <summary>
    /// Gets the rendering hint for the ribbon font.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextHint value.</returns>
    public abstract PaletteTextHint GetRibbonTextHint(PaletteState state);

    /// <summary>
    /// Gets the color for the extra QAT button dark content color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetRibbonQATButtonDark(PaletteState state);

    /// <summary>
    /// Gets the color for the extra QAT button light content color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetRibbonQATButtonLight(PaletteState state);

    /// <summary>Gets the ribbon tab row gradient first color.</summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>The gradient first color.</returns>
    public abstract Color GetRibbonTabRowGradientColor1(PaletteState state);

    /// <summary>Gets the ribbon app button dark color.</summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>The app button dark color.</returns>
    public abstract Color GetRibbonFileAppTabBottomColor(PaletteState state);

    /// <summary>Gets the ribbon app button light color.</summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>The app button light color.</returns>
    public abstract Color GetRibbonFileAppTabTopColor(PaletteState state);

    /// <summary>Gets the ribbon app button text color.</summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>The app button text color.</returns>
    public abstract Color GetRibbonFileAppTabTextColor(PaletteState state);

    /// <summary>Gets the ribbon tab row gradient rafting angle.</summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>The gradient rafting angle.</returns>
    public abstract float GetRibbonTabRowGradientRaftingAngle(PaletteState state);
    #endregion

    #region RibbonBack
    /// <summary>
    /// Gets the method used to draw the background of a ribbon item.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteRibbonBackStyle value.</returns>
    public abstract PaletteRibbonColorStyle GetRibbonBackColorStyle(PaletteRibbonBackStyle style, PaletteState state);

    /// <summary>
    /// Gets the first background color for the ribbon item.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetRibbonBackColor1(PaletteRibbonBackStyle style, PaletteState state);

    /// <summary>
    /// Gets the second background color for the ribbon item.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetRibbonBackColor2(PaletteRibbonBackStyle style, PaletteState state);

    /// <summary>
    /// Gets the third background color for the ribbon item.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetRibbonBackColor3(PaletteRibbonBackStyle style, PaletteState state);

    /// <summary>
    /// Gets the fourth background color for the ribbon item.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetRibbonBackColor4(PaletteRibbonBackStyle style, PaletteState state);

    /// <summary>
    /// Gets the fifth background color for the ribbon item.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetRibbonBackColor5(PaletteRibbonBackStyle style, PaletteState state);
    #endregion

    #region RibbonText

    /// <summary>
    /// Gets the tab color for the item text.
    /// </summary>
    /// <param name="style">Text style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetRibbonTextColor(PaletteRibbonTextStyle style, PaletteState state);

    #endregion

    #region ElementColor
    /// <summary>
    /// Gets the first element color.
    /// </summary>
    /// <param name="element">Element for which color is required.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetElementColor1(PaletteElement element, PaletteState state);

    /// <summary>
    /// Gets the second element color.
    /// </summary>
    /// <param name="element">Element for which color is required.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetElementColor2(PaletteElement element, PaletteState state);

    /// <summary>
    /// Gets the third element color.
    /// </summary>
    /// <param name="element">Element for which color is required.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetElementColor3(PaletteElement element, PaletteState state);

    /// <summary>
    /// Gets the fourth element color.
    /// </summary>
    /// <param name="element">Element for which color is required.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetElementColor4(PaletteElement element, PaletteState state);

    /// <summary>
    /// Gets the fifth element color.
    /// </summary>
    /// <param name="element">Element for which color is required.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public abstract Color GetElementColor5(PaletteElement element, PaletteState state);
    #endregion

    #region DragDrop
    /// <summary>
    /// Gets the feedback drawing method used.
    /// </summary>
    /// <returns>Feedback enumeration value.</returns>
    public virtual PaletteDragFeedback GetDragDropFeedback()
    {
        // Do we need to calculate the feedback value again?
        if (_dragFeedback == PaletteDragFeedback.Inherit)
        {
            // We default to using rounded feedback on Vista upwards and square on earlier versions
            _dragFeedback = Environment.OSVersion.Version.Major >= 6 ? PaletteDragFeedback.Rounded : PaletteDragFeedback.Square;

            // If trying to use rounded feedback...
            if (_dragFeedback == PaletteDragFeedback.Rounded)
            {
                // Rounded feedback uses a per-pixel alpha blending and so we need to be on a machine that supports
                // more than 256 colors and also allows the layered windows feature. If not then revert to squares
                if ((OSFeature.Feature.GetVersionPresent(OSFeature.LayeredWindows) == null) || (CommonHelper.ColorDepth() <= 8))
                {
                    _dragFeedback = PaletteDragFeedback.Square;
                }
            }
        }

        return _dragFeedback;
    }

    /// <summary>
    /// Gets the background color for a solid drag drop area.
    /// </summary>
    /// <returns>Color value.</returns>
    public virtual Color GetDragDropSolidBack() => SystemColors.ActiveCaption;

    /// <summary>
    /// Gets the border color for a solid drag drop area.
    /// </summary>
    /// <returns>Color value.</returns>
    public virtual Color GetDragDropSolidBorder() => SystemColors.Control;

    /// <summary>
    /// Gets the opacity of the solid area.
    /// </summary>
    /// <returns>Opacity ranging from 0 to 1.</returns>
    public virtual float GetDragDropSolidOpacity() => 0.37f;

    /// <summary>
    /// Gets the background color for the docking indicators area.
    /// </summary>
    /// <returns>Color value.</returns>
    public virtual Color GetDragDropDockBack() => Color.FromArgb(228, 228, 228);

    /// <summary>
    /// Gets the border color for the docking indicators area.
    /// </summary>
    /// <returns>Color value.</returns>
    public virtual Color GetDragDropDockBorder() => Color.FromArgb(181, 181, 181);

    /// <summary>
    /// Gets the active color for docking indicators.
    /// </summary>
    /// <returns>Color value.</returns>
    public virtual Color GetDragDropDockActive() => SystemColors.ActiveCaption;

    /// <summary>
    /// Gets the inactive color for docking indicators.
    /// </summary>
    /// <returns>Color value.</returns>
    public virtual Color GetDragDropDockInactive() => SystemColors.InactiveCaption;

    #endregion

    #region GetPaletteTextHotkeyPrefix

    /// <summary>Gets the palette text hot key prefix.</summary>
    /// <returns>The palette text hot key prefix.</returns>
    public virtual PaletteTextHotkeyPrefix GetPaletteTextHotKeyPrefix() =>
        GetContentShortTextPrefix(PaletteContentStyle.LabelNormalControl, PaletteState.Normal);

    #endregion

    #region Public

    /// <summary>Gets or sets the base palette font.</summary>
    /// <value>The base palette font.</value>
    [Description(@"Gets or sets the base palette font.")]
    [DisallowNull]
    public Font BaseFont
    {
        get => _baseFont;

        set
        {
            _baseFont = value;
            DefineFonts();
            // Call an event to force repaint style things
            OnPalettePaint(this, new PaletteLayoutEventArgs(true, false));
        }
    }
    internal void ResetBaseFont() => BaseFont = _defaultFontStyle;
    internal bool ShouldSerializeBaseFont() => !Equals(BaseFont, _defaultFontStyle);

    /// <summary>Gets or sets the name of the theme.</summary>
    /// <value>The name of the theme.</value>
    [Description(@"Gets or sets the name of the theme.")]
    [DisallowNull]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden)]
    public string ThemeName { get; set; }

    /// <summary>Gets or sets the type of the base palette.</summary>
    /// <value>The type of the base palette.</value>
    [Description(@"Gets or sets the type of the base palette.")]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden)]
    public BasePaletteType BasePaletteType { get; set; }

    #endregion

    #region ToolBar Images

    public virtual Image[] ToolBarImages(Image[] toolBarImages)
    {
        _toolBarImages = toolBarImages;

        return _toolBarImages;
    }

    #endregion

    #region Protected

    /// <summary>
    /// Update the fonts to reflect system or user defined changes.
    /// </summary>
    protected virtual void DefineFonts()
    {
        // Release resources
        DisposeFonts();

        var baseFontName = BaseFont.Name;
        var baseFontSize = BaseFont.Size;

        Header1ShortFont = new Font(baseFontName, baseFontSize + 4.5f, FontStyle.Bold);
        Header2ShortFont = new Font(baseFontName, baseFontSize, FontStyle.Regular);
        HeaderFormFont = new Font(baseFontName, SystemFonts.CaptionFont!.SizeInPoints, FontStyle.Regular);
        Header1LongFont = new Font(baseFontName, baseFontSize + 1.5f, FontStyle.Regular);
        Header2LongFont = new Font(baseFontName, baseFontSize, FontStyle.Regular);
        ButtonFont = new Font(baseFontName, baseFontSize, FontStyle.Regular);
        ButtonFontNavigatorStack = new Font(ButtonFont, FontStyle.Bold);
        ButtonFontNavigatorMini = new Font(baseFontName, baseFontSize + 3.5f, FontStyle.Bold);
        TabFontNormal = new Font(baseFontName, baseFontSize, FontStyle.Regular);
        TabFontSelected = new Font(TabFontNormal, FontStyle.Bold);
        RibbonTabFont = new Font(baseFontName, baseFontSize, FontStyle.Regular);
        GridFont = new Font(baseFontName, baseFontSize, FontStyle.Regular);
        SuperToolFont = new Font(baseFontName, baseFontSize, FontStyle.Bold);
        CalendarFont = new Font(baseFontName, baseFontSize, FontStyle.Regular);
        CalendarBoldFont = new Font(baseFontName, baseFontSize, FontStyle.Bold);
        BoldFont = new Font(baseFontName, baseFontSize, FontStyle.Bold);
        ItalicFont = new Font(baseFontName, baseFontSize, FontStyle.Italic);
        RibbonTabContextFont = new Font(RibbonTabFont, FontStyle.Bold);
    }

    protected virtual void DisposeFonts()
    {
        Header1ShortFont?.Dispose();
        Header2ShortFont?.Dispose();
        Header1LongFont?.Dispose();
        Header2LongFont?.Dispose();
        HeaderFormFont?.Dispose();
        ButtonFont?.Dispose();
        ButtonFontNavigatorStack?.Dispose();
        ButtonFontNavigatorMini?.Dispose();
        TabFontNormal?.Dispose();
        TabFontSelected?.Dispose();
        RibbonTabFont?.Dispose();
        GridFont?.Dispose();
        CalendarFont?.Dispose();
        CalendarBoldFont?.Dispose();
        SuperToolFont?.Dispose();
        BoldFont?.Dispose();
        ItalicFont?.Dispose();
        RibbonTabContextFont?.Dispose();

        Header1ShortFont = null;
        Header2ShortFont = null;
        Header1LongFont = null;
        Header2LongFont = null;
        HeaderFormFont = null;
        ButtonFont = null;
        ButtonFontNavigatorStack = null;
        ButtonFontNavigatorMini = null;
        TabFontNormal = null;
        TabFontSelected = null;
        RibbonTabFont = null;
        GridFont = null;
        CalendarFont = null;
        CalendarBoldFont = null;
        SuperToolFont = null;
        BoldFont = null;
        ItalicFont = null;
        RibbonTabContextFont = null;
    }

    #endregion

    #region ColorTable
    /// <summary>
    /// Gets access to the color table instance.
    /// </summary>
    public abstract KryptonColorTable ColorTable { get; }
    #endregion

    #region Static Color Routines
    /// <summary>
    /// Merge two colors together using relative percentages.
    /// </summary>
    /// <param name="color1">First color.</param>
    /// <param name="percent1">Percentage of first color to use.</param>
    /// <param name="color2">Second color.</param>
    /// <param name="percent2">Percentage of second color to use.</param>
    /// <returns>Merged color.</returns>
    public static Color MergeColors(Color color1, float percent1,
        Color color2, float percent2) =>
        CommonHelper.MergeColors(color1, percent1, color2, percent2);

    /// <summary>
    /// Merge three colors together using relative percentages.
    /// </summary>
    /// <param name="color1">First color.</param>
    /// <param name="percent1">Percentage of first color to use.</param>
    /// <param name="color2">Second color.</param>
    /// <param name="percent2">Percentage of second color to use.</param>
    /// <param name="color3">Third color.</param>
    /// <param name="percent3">Percentage of third color to use.</param>
    /// <returns>Merged color.</returns>
    public static Color MergeColors(Color color1, float percent1,
        Color color2, float percent2,
        Color color3, float percent3) =>
        CommonHelper.MergeColors(color1, percent1,
            color2, percent2,
            color3, percent3);

    /// <summary>
    /// Create a faded version of provided color.
    /// </summary>
    /// <param name="baseColor">Starting color.</param>
    /// <returns>Faded version of parameter color.</returns>
    public static Color FadedColor(Color baseColor)
    {
        // Convert to HSL space
        ColorHSL hsl = new ColorHSL(baseColor)
        {
            // Remove saturation and fix luminance
            Saturation = 0.0f,
            Luminance = 0.55f
        };

        return hsl.Color;
    }
    #endregion

    #region InputControlPadding
    /// <summary>
    /// Gets the input control padding needed to add a border to a borderless input control.
    /// </summary>
    protected virtual Padding InputControlPadding
    {
        get
        {
            if (!_inputControlPadding.HasValue)
            {
                // Find size of a input control with and without a border
                var tb = new TextBox
                {
                    BorderStyle = BorderStyle.None
                };
                Size sn = tb.GetPreferredSize(new Size(int.MaxValue, int.MaxValue));
                tb.BorderStyle = BorderStyle.FixedSingle;
                Size ss = tb.GetPreferredSize(new Size(int.MaxValue, int.MaxValue));

                // Always subtract one from top and bottom edges to account for border placed there later by Krypton
                var inputControlPadding = new Padding(0);
                var xDiff = Math.Max(0, ss.Width - sn.Width);
                var yDiff = Math.Max(0, ss.Height - sn.Height - 2);

                // If on Vista or upwards
                if (Environment.OSVersion.Version.Major == 6)
                {
                    // Under Aero we need to reduce by 2, otherwise in Classic and reduce by 1
                    if (PI.IsAppThemed() && PI.IsThemeActive())
                    {
                        yDiff = Math.Max(0, yDiff - 3);
                    }
                    else
                    {
                        yDiff = Math.Max(0, yDiff - 2);
                    }
                }

                // Allocate the difference between the border edges
                if (xDiff > 0)
                {
                    inputControlPadding.Left = xDiff / 2;
                    inputControlPadding.Right = xDiff - inputControlPadding.Left;
                }

                if (yDiff > 0)
                {
                    inputControlPadding.Top = yDiff / 2;
                    inputControlPadding.Bottom = yDiff - inputControlPadding.Top;
                }

                _inputControlPadding = inputControlPadding;
            }

            return _inputControlPadding.Value;
        }
    }
    #endregion

    #region OnUserPreferenceChanged
    public void UserPreferenceChanged() =>
        OnUserPreferenceChanged(this, new UserPreferenceChangedEventArgs(UserPreferenceCategory.General));

    /// <summary>
    /// Handle a change in the user preferences.
    /// </summary>
    /// <param name="sender">Source of event.</param>
    /// <param name="e">Event data.</param>
    protected virtual void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
    {
        _inputControlPadding = null;
        _dragFeedback = PaletteDragFeedback.Inherit;
    }
    #endregion

    #region OnPalettePaint

    /// <summary>
    /// Raises the PalettePaint event.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An PaletteLayoutEventArgs containing event data.</param>
    protected virtual void OnPalettePaint(object sender, PaletteLayoutEventArgs e) =>
        // https://github.com/Krypton-Suite/Standard-Toolkit/issues/1023#issuecomment-1588810368
        PalettePaint?.Invoke(this, e);

    #endregion

    #region OnUseThemeFormChromeBorderWidthChanged

    /// <summary>
    /// Raises the UseThemeFormChromeBorderWidthChanged event.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An EventArgs containing event data.</param>
    protected virtual void OnUseThemeFormChromeBorderWidthChanged(object sender, EventArgs e) => UseThemeFormChromeBorderWidthChanged?.Invoke(this, e);

    #endregion

    #region OnBasePaletteChanged

    /// <summary>
    /// Raises the BasePaletteChanged event.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An EventArgs containing event data.</param>
    protected virtual void OnBasePaletteChanged(object sender, EventArgs e) => BasePaletteChanged?.Invoke(this, e);

    #endregion

    #region OnBaseRendererChanged
    /// <summary>
    /// Raises the BaseRendererChanged event.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An EventArgs containing event data.</param>
    protected virtual void OnBaseRendererChanged(object sender, EventArgs e) => BaseRendererChanged?.Invoke(this, e);

    #endregion

    #region OnButtonSpecChanged
    /// <summary>
    /// Raises the ButtonSpecChanged event.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An EventArgs containing event data.</param>
    protected virtual void OnButtonSpecChanged(object sender, EventArgs e) => ButtonSpecChanged?.Invoke(this, e);

    #endregion

    #region IDisposable Implementation
    /// <summary>
    /// Releases the unmanaged resources used by the PaletteBase and optionally releases the managed resources.
    /// </summary>
    /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Detach from static SystemEvents to prevent memory leaks
            SystemEvents.UserPreferenceChanged -= OnUserPreferenceChanged;

            // Dispose any font resources we created
            DisposeFonts();
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Palette Helpers

    private readonly object _colorLock = new();

    /// <summary>
    /// Resets <see cref="ColorTable"/> to be updated on next paint.
    /// </summary>
    protected virtual void InvalidateColorTable()
    {
        // Default implementation uses reflection as fallback
        var tableField = GetType().GetField("_table", BindingFlags.Instance | BindingFlags.NonPublic)
                         ?? GetType().GetField("Table", BindingFlags.Instance | BindingFlags.NonPublic);

        if (tableField != null)
        {
            tableField.SetValue(this, null);
            return;
        }

        // Try property approach
        var tableProp = GetType().GetProperty("ColorTable", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        if (tableProp != null && tableProp.CanWrite)
        {
            tableProp.SetValue(this, null);
        }
    }

    /// <summary>
    /// Updates scheme colors and invalidates the color table.
    /// </summary>
    protected void UpdateColorTable()
    {
        InvalidateColorTable();
    }

    /// <summary>
    /// Discovers and returns every static <see cref="Color"/> field defined on this palette instance,
    /// regardless of its declared visibility.
    /// The method inspects the actual runtime type of the palette (including all base types)
    /// and uses reflection with <see cref="BindingFlags.Static"/>,
    /// <see cref="BindingFlags.NonPublic"/>, <see cref="BindingFlags.Public"/>,
    /// and <see cref="BindingFlags.FlattenHierarchy"/> to locate all fields whose element type is <see cref="Color"/>.
    /// </summary>
    /// <returns>
    /// An <see cref="IReadOnlyDictionary{String,Color}"/> where each key is the exact name of a static <see cref="Color"/>
    /// field (including private, internal, protected and public), and each value is the corresponding <see cref="Color"/> instance
    /// as initialized by the type’s static constructor.
    /// This allows harvesting every palette color array in a fully automated, deterministic manner.
    /// </returns>
    public IReadOnlyDictionary<string, Color[]> GetStaticColorArrays()
    {
        // Look at this palette’s exact runtime type
        var type   = GetType();
        var result = new Dictionary<string, Color[]>();

        // Grab every static field (public or non-public) on this type or its base types
        var fields = type.GetFields(BindingFlags.Static
                                | BindingFlags.NonPublic
                                | BindingFlags.Public
                                | BindingFlags.FlattenHierarchy);

        // Filter down to only Color[] fields
        foreach (var f in fields)
        {
            if (f.FieldType == typeof(Color[]))
            {
                // Read the array (private or not) and store it
                var array = (Color[])f.GetValue(null)!;
                result[f.Name] = array;
            }
        }

        return result;
    }

    /// <summary>
    /// Copies <paramref name="source"/> into <see cref="SchemeColors"/> and invalidates the color table.
    /// <param name="source">Color array with all values.</param>
    /// </summary>
    public virtual void CopySchemeColors(Color[] source)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        lock (_colorLock)
        {
            Array.Copy(source, SchemeColors, Math.Min(source.Length, SchemeColors.Length));
            InvalidateColorTable();
        }

        OnPalettePaint(this, new PaletteLayoutEventArgs(true, true));
    }

    // Thread-safe single-color setter
    public virtual void SetSchemeColor(SchemeBaseColors colorIndex, Color newColor)
    {
        lock (_colorLock)
        {
            if (SchemeColors[(int)colorIndex] == newColor)
                return; // no change
            SchemeColors[(int)colorIndex] = newColor;
            InvalidateColorTable();
        }
        OnSchemeColorChanged(colorIndex, newColor);
        SchemeColorChanged?.Invoke(this, new SchemeColorChangedEventArgs(colorIndex, newColor));
        OnPalettePaint(this, new PaletteLayoutEventArgs(true, true));
    }

    // Thread-safe single-color getter
    public virtual Color GetSchemeColor(SchemeBaseColors colorIndex)
    {
        lock (_colorLock)
        {
            return SchemeColors[(int)colorIndex];
        }
    }

    // Thread-safe batch update
    public virtual void UpdateSchemeColors(Dictionary<SchemeBaseColors, Color> colorUpdates)
    {
        if (colorUpdates is null) throw new ArgumentNullException(nameof(colorUpdates));
        foreach (var kv in colorUpdates)
            SetSchemeColor(kv.Key, kv.Value); // reuses events + paint
    }

    // Thread-safe full-scheme replacement
    public void ApplyScheme(KryptonColorSchemeBase newScheme)
    {
        if (newScheme is null)
        {
            throw new ArgumentNullException(nameof(newScheme));
        }

        lock (_colorLock)
        {
            Array.Copy(newScheme.ToArray(), SchemeColors, SchemeColors.Length);
            InvalidateColorTable();
        }
        // notify each index has changed
        foreach (SchemeBaseColors idx in Enum.GetValues(typeof(SchemeBaseColors)))
        {
            SchemeColorChanged?.Invoke(this, new SchemeColorChangedEventArgs(idx, SchemeColors[(int)idx]));
        }
        OnPalettePaint(this, new PaletteLayoutEventArgs(true, true));
    }

    #endregion Palette Helpers

    /// <summary>Hook for derived families to rebuild caches when a color changes.</summary>
    protected virtual void OnSchemeColorChanged(SchemeBaseColors index, Color newColor) { }
}

/// <summary>
/// Data for when one scheme color changes.
/// </summary>
public sealed class SchemeColorChangedEventArgs : EventArgs
{
    public SchemeBaseColors Index { get; }
    public Color NewColor { get; }

    public SchemeColorChangedEventArgs(SchemeBaseColors index, Color newColor)
    {
        Index = index;
        NewColor = newColor;
    }
}
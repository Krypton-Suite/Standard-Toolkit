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
/// Implement storage for palette content details.
/// </summary>
public class PaletteContent : Storage,
    IPaletteContent
{
    #region Internal Classes
    private class InternalStorage
    {
        public InheritBool ContentDraw;
        public InheritBool ContentDrawFocus;
        public Padding ContentPadding;
        public int ContentAdjacentGap;

        /// <summary>
        /// Initialize a new instance of the InternalStorage structure.
        /// </summary>
        public InternalStorage()
        {
            // Set to default values
            ContentDraw = InheritBool.Inherit;
            ContentDrawFocus = InheritBool.Inherit;
            ContentPadding = CommonHelper.InheritPadding;
            ContentAdjacentGap = -1;
        }

        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        public bool IsDefault => (ContentDraw == InheritBool.Inherit) &&
                                 (ContentDrawFocus == InheritBool.Inherit) &&
                                 ContentPadding.Equals(CommonHelper.InheritPadding) &&
                                 (ContentAdjacentGap == -1);
    }
    #endregion

    #region Instance Fields
    private InternalStorage? _storage;
    private readonly PaletteContentImage _image;
    private readonly PaletteContentText _shortText;
    private readonly PaletteContentText _longText;
    private IPaletteContent _inherit;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when a property has changed value.
    /// </summary>
    [Browsable(false)]  // SKC: Probably a special case for not exposing this event in the designer....
    [EditorBrowsable(EditorBrowsableState.Never)]
    public event PropertyChangedEventHandler? PropertyChanged;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteContent class.
    /// </summary>
    /// <param name="inherit">Source for inheriting defaulted values.</param>
    public PaletteContent(IPaletteContent inherit)
        : this(inherit, null)
    {
    }

    /// <summary>
    /// Initialize a new instance of the PaletteContent class.
    /// </summary>
    /// <param name="inherit">Source for inheriting defaulted values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteContent([DisallowNull] IPaletteContent inherit,
        NeedPaintHandler? needPaint)
    {
        Debug.Assert(inherit != null);

        // Remember inheritance
        _inherit = inherit!;

        // Store the provided paint notification delegate
        NeedPaint = needPaint;

        // Create the content storage for sub objects
        _image = new PaletteContentImage(needPaint);
        _shortText = new PaletteContentText(needPaint);
        _longText = new PaletteContentText(needPaint);
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => _image.IsDefault &&
                                      _shortText.IsDefault &&
                                      _longText.IsDefault &&
                                      ((_storage == null) || _storage.IsDefault);

    #endregion

    #region SetInherit
    /// <summary>
    /// Sets the inheritance parent.
    /// </summary>
    public void SetInherit(IPaletteContent inherit) => _inherit = inherit;
    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    /// <param name="state">Palette state to use when populating.</param>
    public virtual void PopulateFromBase(PaletteState state)
    {
        // Get the values and set into storage
        Draw = GetContentDraw(state);
        DrawFocus = GetContentDrawFocus(state);
        Image!.ImageH = GetContentImageH(state);
        Image.ImageV = GetContentImageV(state);
        Image.Effect = GetContentImageEffect(state);
        Image.ImageColorMap = GetContentImageColorMap(state);
        Image.ImageColorTo = GetContentImageColorTo(state);
        ShortText.Font = GetContentShortTextFont(state);
        ShortText.Hint = GetContentShortTextHint(state);
        ShortText.Prefix = GetContentShortTextPrefix(state);
        ShortText.Trim = GetContentShortTextTrim(state);
        ShortText.TextH = GetContentShortTextH(state);
        ShortText.TextV = GetContentShortTextV(state);
        ShortText.MultiLineH = GetContentShortTextMultiLineH(state);
        ShortText.MultiLine = GetContentShortTextMultiLine(state);
        ShortText.Color1 = GetContentShortTextColor1(state);
        ShortText.Color2 = GetContentShortTextColor2(state);
        ShortText.ColorStyle = GetContentShortTextColorStyle(state);
        ShortText.ColorAlign = GetContentShortTextColorAlign(state);
        ShortText.ColorAngle = GetContentShortTextColorAngle(state);
        ShortText.Image = GetContentShortTextImage(state);
        ShortText.ImageStyle = GetContentShortTextImageStyle(state);
        ShortText.ImageAlign = GetContentShortTextImageAlign(state);
        LongText.Font = GetContentLongTextFont(state);
        LongText.Hint = GetContentLongTextHint(state);
        LongText.Prefix = GetContentLongTextPrefix(state);
        LongText.Trim = GetContentLongTextTrim(state);
        LongText.TextH = GetContentLongTextH(state);
        LongText.TextV = GetContentLongTextV(state);
        LongText.MultiLineH = GetContentLongTextMultiLineH(state);
        LongText.MultiLine = GetContentLongTextMultiLine(state);
        LongText.Color1 = GetContentLongTextColor1(state);
        LongText.Color2 = GetContentLongTextColor2(state);
        LongText.ColorStyle = GetContentLongTextColorStyle(state);
        LongText.ColorAlign = GetContentLongTextColorAlign(state);
        LongText.ColorAngle = GetContentLongTextColorAngle(state);
        LongText.Image = GetContentLongTextImage(state);
        LongText.ImageStyle = GetContentLongTextImageStyle(state);
        LongText.ImageAlign = GetContentLongTextImageAlign(state);
        Padding = GetBorderContentPadding(null, state);
        AdjacentGap = GetContentAdjacentGap(state);
    }
    #endregion

    #region Draw
    /// <summary>
    /// Gets a value indicating if content should be drawn.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Should content be drawn.")]
    [DefaultValue(InheritBool.Inherit)]
    [RefreshProperties(RefreshProperties.All)]
    public virtual InheritBool Draw
    {
        get => _storage?.ContentDraw ?? InheritBool.Inherit;

        set
        {
            if (_storage != null)
            {
                if (_storage.ContentDraw != value)
                {
                    _storage.ContentDraw = value;
                    OnPropertyChanged(nameof(Draw));
                    PerformNeedPaint();
                }
            }
            else
            {
                if (value != InheritBool.Inherit)
                {
                    _storage = new InternalStorage
                    {
                        ContentDraw = value
                    };
                    OnPropertyChanged(nameof(Draw));
                    PerformNeedPaint();
                }
            }
        }
    }

    /// <summary>
    /// Gets the actual content draw value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public InheritBool GetContentDraw(PaletteState state) =>
        Draw != InheritBool.Inherit ? Draw : _inherit.GetContentDraw(state);

    #endregion

    #region DrawFocus
    /// <summary>
    /// Gets a value indicating if content should be drawn with focus indication.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Should content be drawn with focus indication..")]
    [DefaultValue(InheritBool.Inherit)]
    [RefreshProperties(RefreshProperties.All)]
    public virtual InheritBool DrawFocus
    {
        get => _storage?.ContentDrawFocus ?? InheritBool.Inherit;

        set
        {
            if (_storage != null)
            {
                if (_storage.ContentDrawFocus != value)
                {
                    _storage.ContentDrawFocus = value;
                    OnPropertyChanged(nameof(DrawFocus));
                    PerformNeedPaint();
                }
            }
            else
            {
                if (value != InheritBool.Inherit)
                {
                    _storage = new InternalStorage
                    {
                        ContentDrawFocus = value
                    };
                    OnPropertyChanged(nameof(DrawFocus));
                    PerformNeedPaint();
                }
            }
        }
    }

    /// <summary>
    /// Gets the actual content draw with focus value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public InheritBool GetContentDrawFocus(PaletteState state) =>
        DrawFocus != InheritBool.Inherit ? DrawFocus : _inherit.GetContentDrawFocus(state);

    #endregion

    #region Image
    /// <summary>
    /// Gets access to the image palette details.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining image appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteContentImage? Image => _image;

    private bool ShouldSerializeImage() => !_image.IsDefault;

    /// <summary>
    /// Gets the actual content image horizontal alignment value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public PaletteRelativeAlign GetContentImageH(PaletteState state) =>
        _image.ImageH != PaletteRelativeAlign.Inherit ? _image.ImageH : _inherit.GetContentImageH(state);

    /// <summary>
    /// Gets the actual content image vertical alignment value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public PaletteRelativeAlign GetContentImageV(PaletteState state) =>
        _image.ImageV != PaletteRelativeAlign.Inherit ? _image.ImageV : _inherit.GetContentImageV(state);

    /// <summary>
    /// Gets the actual image drawing effect value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteImageEffect value.</returns>
    public PaletteImageEffect GetContentImageEffect(PaletteState state) =>
        _image.Effect != PaletteImageEffect.Inherit ? _image.Effect : _inherit.GetContentImageEffect(state);

    /// <summary>
    /// Gets the image color to remap into another color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetContentImageColorMap(PaletteState state) => _image.ImageColorMap != GlobalStaticValues.EMPTY_COLOR
        ? _image.ImageColorMap
        : _inherit.GetContentImageColorMap(state);

    /// <summary>
    /// Gets the color to use in place of the image map color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetContentImageColorTo(PaletteState state) => _image.ImageColorTo != GlobalStaticValues.EMPTY_COLOR
        ? _image.ImageColorTo
        : _inherit.GetContentImageColorTo(state);

    #endregion

    #region ShortText
    /// <summary>
    /// Gets access to the short text palette details.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining short text appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteContentText ShortText => _shortText;

    private bool ShouldSerializeShortText() => !_shortText.IsDefault;

    /// <summary>
    /// Gets the actual content short text font value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public Font? GetContentShortTextFont(PaletteState state) => _shortText.Font ?? _inherit.GetContentShortTextFont(state);

    /// <summary>
    /// Gets the font for the short text by generating a new font instance.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public Font? GetContentShortTextNewFont(PaletteState state) => _shortText.Font ?? _inherit.GetContentShortTextNewFont(state);

    /// <summary>
    /// Gets the actual text rendering hint for short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextHint value.</returns>
    public PaletteTextHint GetContentShortTextHint(PaletteState state) => _shortText.Hint != PaletteTextHint.Inherit
        ? _shortText.Hint
        : _inherit.GetContentShortTextHint(state);

    /// <summary>
    /// Gets the prefix drawing setting for short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextPrefix value.</returns>
    public PaletteTextHotkeyPrefix GetContentShortTextPrefix(PaletteState state) =>
        _shortText.Prefix != PaletteTextHotkeyPrefix.Inherit
            ? _shortText.Prefix
            : _inherit.GetContentShortTextPrefix(state);

    /// <summary>
    /// Gets the actual text trimming for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextTrim value.</returns>
    public PaletteTextTrim GetContentShortTextTrim(PaletteState state) =>
        _shortText.Trim != PaletteTextTrim.Inherit
            ? _shortText.Trim
            : _inherit.GetContentShortTextTrim(state);

    /// <summary>
    /// Gets the actual content short text horizontal alignment value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public PaletteRelativeAlign GetContentShortTextH(PaletteState state) =>
        _shortText.TextH != PaletteRelativeAlign.Inherit ? _shortText.TextH : _inherit.GetContentShortTextH(state);

    /// <summary>
    /// Gets the actual content short text vertical alignment value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public PaletteRelativeAlign GetContentShortTextV(PaletteState state) =>
        _shortText.TextV != PaletteRelativeAlign.Inherit ? _shortText.TextV : _inherit.GetContentShortTextV(state);

    /// <summary>
    /// Gets the actual content short text horizontal multiline alignment value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public PaletteRelativeAlign GetContentShortTextMultiLineH(PaletteState state) =>
        _shortText.MultiLineH != PaletteRelativeAlign.Inherit
            ? _shortText.MultiLineH
            : _inherit.GetContentShortTextMultiLineH(state);

    /// <summary>
    /// Gets the flag indicating if multiline text is allowed for short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public InheritBool GetContentShortTextMultiLine(PaletteState state) =>
        _shortText.MultiLine != InheritBool.Inherit
            ? _shortText.MultiLine
            : _inherit.GetContentShortTextMultiLine(state);

    /// <summary>
    /// Gets the first color for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetContentShortTextColor1(PaletteState state) =>
        ShortText.Color1 != GlobalStaticValues.EMPTY_COLOR
            ? ShortText.Color1
            : _inherit.GetContentShortTextColor1(state);

    /// <summary>
    /// Gets the second back color for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetContentShortTextColor2(PaletteState state) => ShortText.Color2 != GlobalStaticValues.EMPTY_COLOR
        ? ShortText.Color2
        : _inherit.GetContentShortTextColor2(state);

    /// <summary>
    /// Gets the color drawing style for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public PaletteColorStyle GetContentShortTextColorStyle(PaletteState state) =>
        ShortText.ColorStyle != PaletteColorStyle.Inherit
            ? ShortText.ColorStyle
            : _inherit.GetContentShortTextColorStyle(state);

    /// <summary>
    /// Gets the color alignment style for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public PaletteRectangleAlign GetContentShortTextColorAlign(PaletteState state) =>
        ShortText.ColorAlign != PaletteRectangleAlign.Inherit
            ? ShortText.ColorAlign
            : _inherit.GetContentShortTextColorAlign(state);

    /// <summary>
    /// Gets the color angle for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public float GetContentShortTextColorAngle(PaletteState state) => ShortText.ColorAngle != -1f
        ? ShortText.ColorAngle
        : _inherit.GetContentShortTextColorAngle(state);

    /// <summary>
    /// Gets an image for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public Image? GetContentShortTextImage(PaletteState state) => ShortText.Image ?? _inherit.GetContentShortTextImage(state);

    /// <summary>
    /// Gets the image style for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public PaletteImageStyle GetContentShortTextImageStyle(PaletteState state) =>
        ShortText.ImageStyle != PaletteImageStyle.Inherit
            ? ShortText.ImageStyle
            : _inherit.GetContentShortTextImageStyle(state);

    /// <summary>
    /// Gets the image alignment style for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public PaletteRectangleAlign GetContentShortTextImageAlign(PaletteState state) =>
        ShortText.ImageAlign != PaletteRectangleAlign.Inherit
            ? ShortText.ImageAlign
            : _inherit.GetContentShortTextImageAlign(state);

    #endregion

    #region LongText
    /// <summary>
    /// Gets access to the long text palette details.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining long text appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteContentText LongText => _longText;

    private bool ShouldSerializeLongText() => !_longText.IsDefault;

    /// <summary>
    /// Gets the actual content long text font value.
    /// </summary>
    /// <returns>Font value.</returns>
    /// <param name="state">Palette value should be applicable to this state.</param>
    public Font? GetContentLongTextFont(PaletteState state) => _longText.Font ?? _inherit.GetContentLongTextFont(state);

    /// <summary>
    /// Gets the font for the long text by generating a new font instance.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public Font? GetContentLongTextNewFont(PaletteState state) => _longText.Font ?? _inherit.GetContentLongTextNewFont(state);

    /// <summary>
    /// Gets the actual text rendering hint for long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextHint value.</returns>
    public PaletteTextHint GetContentLongTextHint(PaletteState state) => _longText.Hint != PaletteTextHint.Inherit
        ? _longText.Hint
        : _inherit.GetContentLongTextHint(state);

    /// <summary>
    /// Gets the prefix drawing setting for long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextPrefix value.</returns>
    public PaletteTextHotkeyPrefix GetContentLongTextPrefix(PaletteState state) =>
        _longText.Prefix != PaletteTextHotkeyPrefix.Inherit
            ? _longText.Prefix
            : _inherit.GetContentLongTextPrefix(state);

    /// <summary>
    /// Gets the actual text trimming for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextTrim value.</returns>
    public PaletteTextTrim GetContentLongTextTrim(PaletteState state) => _longText.Trim != PaletteTextTrim.Inherit
        ? _longText.Trim
        : _inherit.GetContentLongTextTrim(state);

    /// <summary>
    /// Gets the actual content long text horizontal alignment value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public PaletteRelativeAlign GetContentLongTextH(PaletteState state) =>
        _longText.TextH != PaletteRelativeAlign.Inherit ? _longText.TextH : _inherit.GetContentLongTextH(state);

    /// <summary>
    /// Gets the actual content long text vertical alignment value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public PaletteRelativeAlign GetContentLongTextV(PaletteState state) =>
        _longText.TextV != PaletteRelativeAlign.Inherit
            ? _longText.TextV
            : _inherit.GetContentLongTextV(state);

    /// <summary>
    /// Gets the actual content long text horizontal multiline alignment value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public PaletteRelativeAlign GetContentLongTextMultiLineH(PaletteState state) =>
        _longText.MultiLineH != PaletteRelativeAlign.Inherit
            ? _longText.MultiLineH
            : _inherit.GetContentLongTextMultiLineH(state);

    /// <summary>
    /// Gets the flag indicating if multiline text is allowed for long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public InheritBool GetContentLongTextMultiLine(PaletteState state) =>
        _longText.MultiLine != InheritBool.Inherit
            ? _longText.MultiLine
            : _inherit.GetContentLongTextMultiLine(state);

    /// <summary>
    /// Gets the first color for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetContentLongTextColor1(PaletteState state) =>
        LongText.Color1 != GlobalStaticValues.EMPTY_COLOR
            ? LongText.Color1
            : _inherit.GetContentLongTextColor1(state);

    /// <summary>
    /// Gets the second back color for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetContentLongTextColor2(PaletteState state) =>
        LongText.Color2 != GlobalStaticValues.EMPTY_COLOR
            ? LongText.Color2
            : _inherit.GetContentLongTextColor2(state);

    /// <summary>
    /// Gets the color drawing style for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public PaletteColorStyle GetContentLongTextColorStyle(PaletteState state) =>
        LongText.ColorStyle != PaletteColorStyle.Inherit
            ? LongText.ColorStyle
            : _inherit.GetContentLongTextColorStyle(state);

    /// <summary>
    /// Gets the color alignment style for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public PaletteRectangleAlign GetContentLongTextColorAlign(PaletteState state) =>
        LongText.ColorAlign != PaletteRectangleAlign.Inherit
            ? LongText.ColorAlign
            : _inherit.GetContentLongTextColorAlign(state);

    /// <summary>
    /// Gets the color angle for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public float GetContentLongTextColorAngle(PaletteState state) =>
        LongText.ColorAngle != -1f
            ? LongText.ColorAngle
            : _inherit.GetContentLongTextColorAngle(state);

    /// <summary>
    /// Gets an image for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public Image? GetContentLongTextImage(PaletteState state) => LongText.Image ?? _inherit.GetContentLongTextImage(state);

    /// <summary>
    /// Gets the image style for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public PaletteImageStyle GetContentLongTextImageStyle(PaletteState state) =>
        LongText.ImageStyle != PaletteImageStyle.Inherit
            ? LongText.ImageStyle
            : _inherit.GetContentLongTextImageStyle(state);

    /// <summary>
    /// Gets the image alignment style for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public PaletteRectangleAlign GetContentLongTextImageAlign(PaletteState state) =>
        LongText.ImageAlign != PaletteRectangleAlign.Inherit
            ? LongText.ImageAlign
            : _inherit.GetContentLongTextImageAlign(state);

    #endregion

    #region Padding
    /// <summary>
    /// Gets the padding between the border and content drawing.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Padding between the border and content drawing.")]
    [DefaultValue(typeof(Padding), "-1,-1,-1,-1")]
    [RefreshProperties(RefreshProperties.All)]
    public virtual Padding Padding
    {
        get => _storage?.ContentPadding ?? CommonHelper.InheritPadding;

        set
        {
            if (_storage != null)
            {
                if (!value.Equals(_storage.ContentPadding))
                {
                    _storage.ContentPadding = value;
                    OnPropertyChanged(nameof(Padding));
                    PerformNeedPaint(true);
                }
            }
            else
            {
                if (!value.Equals(CommonHelper.InheritPadding))
                {
                    _storage = new InternalStorage
                    {
                        ContentPadding = value
                    };
                    OnPropertyChanged(nameof(Padding));
                    PerformNeedPaint(true);
                }
            }
        }
    }

    /// <summary>
    /// Reset the Padding to the default value.
    /// </summary>
    public void ResetPadding() => Padding = CommonHelper.InheritPadding;

    /// <summary>
    /// Gets the actual padding between the border and content drawing.
    /// </summary>
    /// <param name="owningForm"></param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Padding value.</returns>
    public Padding GetBorderContentPadding(KryptonForm? owningForm, PaletteState state)
    {
        // Initialize the padding from inherited values
        Padding paddingInherit = _inherit.GetBorderContentPadding(owningForm, state);
        Padding paddingThis = Padding;

        // Override with specified values
        if (paddingThis.Left != -1)
        {
            paddingInherit.Left = paddingThis.Left;
        }

        if (paddingThis.Right != -1)
        {
            paddingInherit.Right = paddingThis.Right;
        }

        if (paddingThis.Top != -1)
        {
            paddingInherit.Top = paddingThis.Top;
        }

        if (paddingThis.Bottom != -1)
        {
            paddingInherit.Bottom = paddingThis.Bottom;
        }

        return paddingInherit;
    }
    #endregion

    #region AdjacentGap
    /// <summary>
    /// Gets the padding between adjacent content items.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Spacing gap between adjacent content items.")]
    [DefaultValue(-1)]
    [RefreshProperties(RefreshProperties.All)]
    public virtual int AdjacentGap
    {
        get => _storage?.ContentAdjacentGap ?? -1;

        set
        {
            if (_storage != null)
            {
                if (_storage.ContentAdjacentGap != value)
                {
                    _storage.ContentAdjacentGap = value;
                    OnPropertyChanged(nameof(AdjacentGap));
                    PerformNeedPaint(true);
                }
            }
            else
            {
                if (value != -1)
                {
                    _storage = new InternalStorage
                    {
                        ContentAdjacentGap = value
                    };
                    OnPropertyChanged(nameof(AdjacentGap));
                    PerformNeedPaint(true);
                }
            }
        }
    }

    /// <summary>
    /// Reset the AdjacentGap to the default value.
    /// </summary>
    public void ResetAdjacentGap() => AdjacentGap = -1;

    /// <summary>
    /// Gets the actual padding between adjacent content items.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Integer value.</returns>
    public int GetContentAdjacentGap(PaletteState state) => AdjacentGap != -1 ? AdjacentGap : _inherit.GetContentAdjacentGap(state);

    #endregion

    #region ContentStyle
    /// <summary>
    /// Gets the style appropriate for this content.
    /// </summary>
    /// <returns>Content style.</returns>
    public PaletteContentStyle GetContentStyle() => _inherit.GetContentStyle();
    #endregion

    #region Protected
    /// <summary>
    /// Raises the PropertyChanged event.
    /// </summary>
    /// <param name="property">Name of the property changed.</param>
    protected virtual void OnPropertyChanged(string property) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    #endregion
}
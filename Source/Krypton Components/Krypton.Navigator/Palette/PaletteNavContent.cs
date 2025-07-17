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

// ReSharper disable CompareOfFloatsByEqualityOperator
namespace Krypton.Navigator;

/// <summary>
/// Implement storage for palette content details.
/// </summary>
public class PaletteNavContent : Storage,
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
    private IPaletteContent? _inherit;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteNavContent class.
    /// </summary>
    /// <param name="inherit">Source for inheriting defaulted values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteNavContent([DisallowNull] IPaletteContent inherit,
        NeedPaintHandler needPaint)
    {
        Debug.Assert(inherit != null);

        // Remember inheritance
        _inherit = inherit;

        // Store the provided paint notification delegate
        NeedPaint = needPaint;

        // Create the content storage for sub objects
        Image = new PaletteContentImage(needPaint);
        ShortText = new PaletteNavContentText(needPaint);
        LongText = new PaletteNavContentText(needPaint);
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => (Image!.IsDefault &&
        ShortText.IsDefault &&
        LongText.IsDefault &&
        _storage == null || _storage!.IsDefault);

    #endregion

    #region SetInherit
    /// <summary>
    /// Sets the inheritance parent.
    /// </summary>
    public void SetInherit(IPaletteContent? inherit) => _inherit = inherit;
    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    /// <param name="state">Palette state to use when populating.</param>
    public void PopulateFromBase(PaletteState state)
    {
        // Get the values and set into storage
        Draw = GetContentDraw(state);
        DrawFocus = GetContentDrawFocus(state);
        if (Image != null)
        {
            Image.ImageH = GetContentImageH(state);
            Image.ImageV = GetContentImageV(state);
            Image.Effect = GetContentImageEffect(state);
            Image.ImageColorMap = GetContentImageColorMap(state);
            Image.ImageColorTo = GetContentImageColorTo(state);
        }

        ShortText.Prefix = GetContentShortTextPrefix(state);
        ShortText.Trim = GetContentShortTextTrim(state);
        ShortText.TextH = GetContentShortTextH(state);
        ShortText.TextV = GetContentShortTextV(state);
        ShortText.MultiLineH = GetContentShortTextMultiLineH(state);
        ShortText.MultiLine = GetContentShortTextMultiLine(state);
        LongText.Prefix = GetContentLongTextPrefix(state);
        LongText.Trim = GetContentLongTextTrim(state);
        LongText.TextH = GetContentLongTextH(state);
        LongText.TextV = GetContentLongTextV(state);
        LongText.MultiLineH = GetContentLongTextMultiLineH(state);
        LongText.MultiLine = GetContentLongTextMultiLine(state);
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
    //[DefaultValue(typeof(InheritBool), "Inherit")]
    [RefreshProperties(RefreshProperties.All)]
    public InheritBool Draw
    {
        get => _storage?.ContentDraw ?? InheritBool.Inherit;

        set
        {
            if (_storage != null)
            {
                if (_storage.ContentDraw != value)
                {
                    _storage.ContentDraw = value;
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
        Draw != InheritBool.Inherit ? Draw : _inherit!.GetContentDraw(state);
    #endregion

    #region DrawFocus
    /// <summary>
    /// Gets a value indicating if content should be drawn with focus indication.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Should content be drawn with focus indication..")]
    //[DefaultValue(typeof(InheritBool), "Inherit")]
    [RefreshProperties(RefreshProperties.All)]
    public InheritBool DrawFocus
    {
        get => _storage?.ContentDrawFocus ?? InheritBool.Inherit;

        set
        {
            if (_storage != null)
            {
                if (_storage.ContentDrawFocus != value)
                {
                    _storage.ContentDrawFocus = value;
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
        DrawFocus != InheritBool.Inherit ? DrawFocus : _inherit!.GetContentDrawFocus(state);
    #endregion

    #region Image
    /// <summary>
    /// Gets access to the image palette details.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining image appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteContentImage? Image { get; }

    private bool ShouldSerializeImage() => !Image!.IsDefault;

    /// <summary>
    /// Gets the actual content image horizontal alignment value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public PaletteRelativeAlign GetContentImageH(PaletteState state) => Image!.ImageH != PaletteRelativeAlign.Inherit
        ? Image.ImageH
        : _inherit!.GetContentImageH(state);

    /// <summary>
    /// Gets the actual content image vertical alignment value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public PaletteRelativeAlign GetContentImageV(PaletteState state) => Image!.ImageV != PaletteRelativeAlign.Inherit
        ? Image.ImageV
        : _inherit!.GetContentImageV(state);

    /// <summary>
    /// Gets the actual image drawing effect value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteImageEffect value.</returns>
    public PaletteImageEffect GetContentImageEffect(PaletteState state) =>
        Image!.Effect != PaletteImageEffect.Inherit ? Image.Effect : _inherit!.GetContentImageEffect(state);

    /// <summary>
    /// Gets the image color to remap into another color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetContentImageColorMap(PaletteState state) => Image!.ImageColorMap != Color.Empty
        ? Image.ImageColorMap
        : _inherit!.GetContentImageColorMap(state);

    /// <summary>
    /// Gets the color to use in place of the image map color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetContentImageColorTo(PaletteState state) => Image!.ImageColorTo != Color.Empty
        ? Image.ImageColorTo
        : _inherit!.GetContentImageColorTo(state);

    #endregion

    #region ShortText
    /// <summary>
    /// Gets access to the short text palette details.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining short text appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteNavContentText ShortText { get; }

    private bool ShouldSerializeShortText() => !ShortText.IsDefault;

    /// <summary>
    /// Gets the actual content short text font value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public Font? GetContentShortTextFont(PaletteState state) =>
        ShortText.Font ?? _inherit!.GetContentShortTextFont(state);

    /// <summary>
    /// Gets the font for the short text by generating a new font instance.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public Font? GetContentShortTextNewFont(PaletteState state) =>
        ShortText.Font ?? _inherit!.GetContentShortTextNewFont(state);

    /// <summary>
    /// Gets the actual text rendering hint for short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextHint value.</returns>
    public PaletteTextHint GetContentShortTextHint(PaletteState state) => ShortText.Hint != PaletteTextHint.Inherit
        ? ShortText.Hint
        : _inherit!.GetContentShortTextHint(state);

    /// <summary>
    /// Gets the prefix drawing setting for short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextPrefix value.</returns>
    public PaletteTextHotkeyPrefix GetContentShortTextPrefix(PaletteState state) =>
        ShortText.Prefix != PaletteTextHotkeyPrefix.Inherit
            ? ShortText.Prefix
            : _inherit!.GetContentShortTextPrefix(state);

    /// <summary>
    /// Gets the actual text trimming for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextTrim value.</returns>
    public PaletteTextTrim GetContentShortTextTrim(PaletteState state) => ShortText.Trim != PaletteTextTrim.Inherit
        ? ShortText.Trim
        : _inherit!.GetContentShortTextTrim(state);

    /// <summary>
    /// Gets the actual content short text horizontal alignment value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public PaletteRelativeAlign GetContentShortTextH(PaletteState state) =>
        ShortText.TextH != PaletteRelativeAlign.Inherit ? ShortText.TextH : _inherit!.GetContentShortTextH(state);

    /// <summary>
    /// Gets the actual content short text vertical alignment value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public PaletteRelativeAlign GetContentShortTextV(PaletteState state) =>
        ShortText.TextV != PaletteRelativeAlign.Inherit ? ShortText.TextV : _inherit!.GetContentShortTextV(state);

    /// <summary>
    /// Gets the actual content short text horizontal multiline alignment value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public PaletteRelativeAlign GetContentShortTextMultiLineH(PaletteState state) =>
        ShortText.MultiLineH != PaletteRelativeAlign.Inherit
            ? ShortText.MultiLineH
            : _inherit!.GetContentShortTextMultiLineH(state);

    /// <summary>
    /// Gets the flag indicating if multiline text is allowed for short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public InheritBool GetContentShortTextMultiLine(PaletteState state) =>
        ShortText.MultiLine != InheritBool.Inherit
            ? ShortText.MultiLine
            : _inherit!.GetContentShortTextMultiLine(state);

    /// <summary>
    /// Gets the first color for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetContentShortTextColor1(PaletteState state) => ShortText.Color1 != Color.Empty
        ? ShortText.Color1
        : _inherit!.GetContentShortTextColor1(state);

    /// <summary>
    /// Gets the second back color for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetContentShortTextColor2(PaletteState state) => ShortText.Color2 != Color.Empty
        ? ShortText.Color2
        : _inherit!.GetContentShortTextColor2(state);

    /// <summary>
    /// Gets the color drawing style for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public PaletteColorStyle GetContentShortTextColorStyle(PaletteState state) =>
        ShortText.ColorStyle != PaletteColorStyle.Inherit
            ? ShortText.ColorStyle
            : _inherit!.GetContentShortTextColorStyle(state);

    /// <summary>
    /// Gets the color alignment style for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public PaletteRectangleAlign GetContentShortTextColorAlign(PaletteState state) =>
        ShortText.ColorAlign != PaletteRectangleAlign.Inherit
            ? ShortText.ColorAlign
            : _inherit!.GetContentShortTextColorAlign(state);

    /// <summary>
    /// Gets the color angle for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public float GetContentShortTextColorAngle(PaletteState state) => ShortText.ColorAngle != -1
        ? ShortText.ColorAngle
        : _inherit!.GetContentShortTextColorAngle(state);

    /// <summary>
    /// Gets an image for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public Image? GetContentShortTextImage(PaletteState state) =>
        ShortText.Image ?? _inherit!.GetContentShortTextImage(state);

    /// <summary>
    /// Gets the image style for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public PaletteImageStyle GetContentShortTextImageStyle(PaletteState state) =>
        ShortText.ImageStyle != PaletteImageStyle.Inherit
            ? ShortText.ImageStyle
            : _inherit!.GetContentShortTextImageStyle(state);

    /// <summary>
    /// Gets the image alignment style for the short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public PaletteRectangleAlign GetContentShortTextImageAlign(PaletteState state) =>
        ShortText.ImageAlign != PaletteRectangleAlign.Inherit
            ? ShortText.ImageAlign
            : _inherit!.GetContentShortTextImageAlign(state);

    #endregion

    #region LongText
    /// <summary>
    /// Gets access to the long text palette details.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining long text appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteNavContentText LongText { get; }

    private bool ShouldSerializeLongText() => !LongText.IsDefault;

    /// <summary>
    /// Gets the actual content long text font value.
    /// </summary>
    /// <returns>Font value.</returns>
    /// <param name="state">Palette value should be applicable to this state.</param>
    public Font? GetContentLongTextFont(PaletteState state) =>
        LongText.Font ?? _inherit!.GetContentLongTextFont(state);

    /// <summary>
    /// Gets the font for the long text by generating a new font instance.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public Font? GetContentLongTextNewFont(PaletteState state) =>
        LongText.Font ?? _inherit!.GetContentLongTextNewFont(state);

    /// <summary>
    /// Gets the actual text rendering hint for long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextHint value.</returns>
    public PaletteTextHint GetContentLongTextHint(PaletteState state) => LongText.Hint != PaletteTextHint.Inherit
        ? LongText.Hint
        : _inherit!.GetContentLongTextHint(state);

    /// <summary>
    /// Gets the prefix drawing setting for long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextPrefix value.</returns>
    public PaletteTextHotkeyPrefix GetContentLongTextPrefix(PaletteState state) =>
        LongText.Prefix != PaletteTextHotkeyPrefix.Inherit
            ? LongText.Prefix
            : _inherit!.GetContentLongTextPrefix(state);

    /// <summary>
    /// Gets the actual text trimming for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextTrim value.</returns>
    public PaletteTextTrim GetContentLongTextTrim(PaletteState state) => LongText.Trim != PaletteTextTrim.Inherit
        ? LongText.Trim
        : _inherit!.GetContentLongTextTrim(state);

    /// <summary>
    /// Gets the actual content long text horizontal alignment value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public PaletteRelativeAlign GetContentLongTextH(PaletteState state) =>
        LongText.TextH != PaletteRelativeAlign.Inherit ? LongText.TextH : _inherit!.GetContentLongTextH(state);

    /// <summary>
    /// Gets the actual content long text vertical alignment value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public PaletteRelativeAlign GetContentLongTextV(PaletteState state) =>
        LongText.TextV != PaletteRelativeAlign.Inherit ? LongText.TextV : _inherit!.GetContentLongTextV(state);

    /// <summary>
    /// Gets the actual content long text horizontal multiline alignment value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public PaletteRelativeAlign GetContentLongTextMultiLineH(PaletteState state) =>
        LongText.MultiLineH != PaletteRelativeAlign.Inherit
            ? LongText.MultiLineH
            : _inherit!.GetContentLongTextMultiLineH(state);

    /// <summary>
    /// Gets the flag indicating if multiline text is allowed for long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public InheritBool GetContentLongTextMultiLine(PaletteState state) => LongText.MultiLine != InheritBool.Inherit
        ? LongText.MultiLine
        : _inherit!.GetContentLongTextMultiLine(state);

    /// <summary>
    /// Gets the first color for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetContentLongTextColor1(PaletteState state) => LongText.Color1 != Color.Empty
        ? LongText.Color1
        : _inherit!.GetContentLongTextColor1(state);

    /// <summary>
    /// Gets the second back color for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetContentLongTextColor2(PaletteState state) => LongText.Color2 != Color.Empty
        ? LongText.Color2
        : _inherit!.GetContentLongTextColor2(state);

    /// <summary>
    /// Gets the color drawing style for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public PaletteColorStyle GetContentLongTextColorStyle(PaletteState state) =>
        LongText.ColorStyle != PaletteColorStyle.Inherit
            ? LongText.ColorStyle
            : _inherit!.GetContentLongTextColorStyle(state);

    /// <summary>
    /// Gets the color alignment style for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public PaletteRectangleAlign GetContentLongTextColorAlign(PaletteState state) =>
        LongText.ColorAlign != PaletteRectangleAlign.Inherit
            ? LongText.ColorAlign
            : _inherit!.GetContentLongTextColorAlign(state);

    /// <summary>
    /// Gets the color angle for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public float GetContentLongTextColorAngle(PaletteState state) => LongText.ColorAngle != -1
        ? LongText.ColorAngle
        : _inherit!.GetContentLongTextColorAngle(state);

    /// <summary>
    /// Gets an image for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public Image? GetContentLongTextImage(PaletteState state) =>
        LongText.Image ?? _inherit!.GetContentLongTextImage(state);

    /// <summary>
    /// Gets the image style for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public PaletteImageStyle GetContentLongTextImageStyle(PaletteState state) =>
        LongText.ImageStyle != PaletteImageStyle.Inherit
            ? LongText.ImageStyle
            : _inherit!.GetContentLongTextImageStyle(state);

    /// <summary>
    /// Gets the image alignment style for the long text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public PaletteRectangleAlign GetContentLongTextImageAlign(PaletteState state) =>
        LongText.ImageAlign != PaletteRectangleAlign.Inherit
            ? LongText.ImageAlign
            : _inherit!.GetContentLongTextImageAlign(state);

    #endregion

    #region Padding
    /// <summary>
    /// Gets the padding between the border and content drawing.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Padding between the border and content drawing.")]
    //[DefaultValue(typeof(Padding), "-1,-1,-1,-1")]
    [RefreshProperties(RefreshProperties.All)]
    public Padding Padding
    {
        get => _storage?.ContentPadding ?? CommonHelper.InheritPadding;

        set
        {
            if (_storage != null)
            {
                if (!value.Equals(_storage.ContentPadding))
                {
                    _storage.ContentPadding = value;
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
        Padding paddingInherit = _inherit!.GetBorderContentPadding(owningForm, state);
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
    public int AdjacentGap
    {
        get => _storage?.ContentAdjacentGap ?? -1;

        set
        {
            if (_storage != null)
            {
                if (_storage.ContentAdjacentGap != value)
                {
                    _storage.ContentAdjacentGap = value;
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
    public int GetContentAdjacentGap(PaletteState state) =>
        AdjacentGap != -1 ? AdjacentGap : _inherit!.GetContentAdjacentGap(state);

    #endregion

    #region ContentStyle
    /// <summary>
    /// Gets the style appropriate for this content.
    /// </summary>
    /// <returns>Content style.</returns>
    public PaletteContentStyle GetContentStyle() => _inherit!.GetContentStyle();

    #endregion
}
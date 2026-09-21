#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Localisable strings for <see cref="KryptonScreenColorPicker"/> and <see cref="KryptonColorPicker"/>.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class KryptonScreenColorPickerStrings : GlobalId
{
    private const string DEFAULT_OVERLAY_INSTRUCTIONS =
        @"Click to pick  ·  Esc or right-click to cancel  ·  Wheel or +/- zooms  ·  Ctrl+wheel or [ ] resizes  ·  F12 copies screenshot";
    private const string DEFAULT_OVERLAY_TITLE = @"Screen colour picker";
    private const string DEFAULT_FLYOUT_STYLE_KRYPTON = @"Krypton";
    private const string DEFAULT_FLYOUT_STYLE_CLASSIC = @"Classic (PowerToys)";
    private const string DEFAULT_HEX_DISPLAY_NAME = @"Hex";
    private const string DEFAULT_HEX_ALPHA_DISPLAY_NAME = @"Hex (alpha)";
    private const string DEFAULT_HEX_INTEGER_DISPLAY_NAME = @"Hex integer";
    private const string DEFAULT_RGB_DISPLAY_NAME = @"RGB";
    private const string DEFAULT_RGBA_DISPLAY_NAME = @"RGBA";
    private const string DEFAULT_HSL_DISPLAY_NAME = @"HSL";
    private const string DEFAULT_HSV_DISPLAY_NAME = @"HSV";
    private const string DEFAULT_CMYK_DISPLAY_NAME = @"CMYK";
    private const string DEFAULT_DECIMAL_DISPLAY_NAME = @"Decimal";
    private const string DEFAULT_VECTOR_DISPLAY_NAME = @"Vector";
    private const string DEFAULT_KNOWN_NAME_DISPLAY_NAME = @"Known name";
    private const string DEFAULT_CUSTOM_COLOR_NAME = @"Custom";
    private const string DEFAULT_MAGNIFIER_META_FORMAT = @"{0}x  ·  {1} src px";
    private const string DEFAULT_HEX_VALUE_FORMAT = @"#{0:X2}{1:X2}{2:X2}";
    private const string DEFAULT_HEX_ALPHA_VALUE_FORMAT = @"#{0:X2}{1:X2}{2:X2}{3:X2}";
    private const string DEFAULT_HEX_INTEGER_VALUE_FORMAT = @"0x{0:X2}{1:X2}{2:X2}";
    private const string DEFAULT_RGB_VALUE_FORMAT = @"RGB({0}, {1}, {2})";
    private const string DEFAULT_RGBA_VALUE_FORMAT = @"RGBA({0}, {1}, {2}, {3})";
    private const string DEFAULT_HSL_VALUE_FORMAT = @"HSL({0:0}, {1:0}%, {2:0}%)";
    private const string DEFAULT_HSV_VALUE_FORMAT = @"HSV({0:0}, {1:0}%, {2:0}%)";
    private const string DEFAULT_CMYK_VALUE_FORMAT = @"CMYK({0:0}%, {1:0}%, {2:0}%, {3:0}%)";
    private const string DEFAULT_VECTOR_VALUE_FORMAT = @"{0:0.###}, {1:0.###}, {2:0.###}";

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonScreenColorPickerStrings"/> class.
    /// </summary>
    public KryptonScreenColorPickerStrings() => Reset();

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? @"Modified" : string.Empty;

    /// <summary>
    /// Gets a value indicating whether all strings are at their defaults.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsDefault =>
        OverlayInstructions == DEFAULT_OVERLAY_INSTRUCTIONS
        && OverlayTitle == DEFAULT_OVERLAY_TITLE
        && FlyoutStyleKrypton == DEFAULT_FLYOUT_STYLE_KRYPTON
        && FlyoutStyleClassic == DEFAULT_FLYOUT_STYLE_CLASSIC
        && HexDisplayName == DEFAULT_HEX_DISPLAY_NAME
        && HexAlphaDisplayName == DEFAULT_HEX_ALPHA_DISPLAY_NAME
        && HexIntegerDisplayName == DEFAULT_HEX_INTEGER_DISPLAY_NAME
        && RgbDisplayName == DEFAULT_RGB_DISPLAY_NAME
        && RgbaDisplayName == DEFAULT_RGBA_DISPLAY_NAME
        && HslDisplayName == DEFAULT_HSL_DISPLAY_NAME
        && HsvDisplayName == DEFAULT_HSV_DISPLAY_NAME
        && CmykDisplayName == DEFAULT_CMYK_DISPLAY_NAME
        && DecimalDisplayName == DEFAULT_DECIMAL_DISPLAY_NAME
        && VectorDisplayName == DEFAULT_VECTOR_DISPLAY_NAME
        && KnownNameDisplayName == DEFAULT_KNOWN_NAME_DISPLAY_NAME
        && CustomColorName == DEFAULT_CUSTOM_COLOR_NAME
        && MagnifierMetaFormat == DEFAULT_MAGNIFIER_META_FORMAT
        && HexValueFormat == DEFAULT_HEX_VALUE_FORMAT
        && HexAlphaValueFormat == DEFAULT_HEX_ALPHA_VALUE_FORMAT
        && HexIntegerValueFormat == DEFAULT_HEX_INTEGER_VALUE_FORMAT
        && RgbValueFormat == DEFAULT_RGB_VALUE_FORMAT
        && RgbaValueFormat == DEFAULT_RGBA_VALUE_FORMAT
        && HslValueFormat == DEFAULT_HSL_VALUE_FORMAT
        && HsvValueFormat == DEFAULT_HSV_VALUE_FORMAT
        && CmykValueFormat == DEFAULT_CMYK_VALUE_FORMAT
        && VectorValueFormat == DEFAULT_VECTOR_VALUE_FORMAT;

    /// <summary>
    /// Resets all strings to their English defaults.
    /// </summary>
    public void Reset()
    {
        OverlayInstructions = DEFAULT_OVERLAY_INSTRUCTIONS;
        OverlayTitle = DEFAULT_OVERLAY_TITLE;
        FlyoutStyleKrypton = DEFAULT_FLYOUT_STYLE_KRYPTON;
        FlyoutStyleClassic = DEFAULT_FLYOUT_STYLE_CLASSIC;
        HexDisplayName = DEFAULT_HEX_DISPLAY_NAME;
        HexAlphaDisplayName = DEFAULT_HEX_ALPHA_DISPLAY_NAME;
        HexIntegerDisplayName = DEFAULT_HEX_INTEGER_DISPLAY_NAME;
        RgbDisplayName = DEFAULT_RGB_DISPLAY_NAME;
        RgbaDisplayName = DEFAULT_RGBA_DISPLAY_NAME;
        HslDisplayName = DEFAULT_HSL_DISPLAY_NAME;
        HsvDisplayName = DEFAULT_HSV_DISPLAY_NAME;
        CmykDisplayName = DEFAULT_CMYK_DISPLAY_NAME;
        DecimalDisplayName = DEFAULT_DECIMAL_DISPLAY_NAME;
        VectorDisplayName = DEFAULT_VECTOR_DISPLAY_NAME;
        KnownNameDisplayName = DEFAULT_KNOWN_NAME_DISPLAY_NAME;
        CustomColorName = DEFAULT_CUSTOM_COLOR_NAME;
        MagnifierMetaFormat = DEFAULT_MAGNIFIER_META_FORMAT;
        HexValueFormat = DEFAULT_HEX_VALUE_FORMAT;
        HexAlphaValueFormat = DEFAULT_HEX_ALPHA_VALUE_FORMAT;
        HexIntegerValueFormat = DEFAULT_HEX_INTEGER_VALUE_FORMAT;
        RgbValueFormat = DEFAULT_RGB_VALUE_FORMAT;
        RgbaValueFormat = DEFAULT_RGBA_VALUE_FORMAT;
        HslValueFormat = DEFAULT_HSL_VALUE_FORMAT;
        HsvValueFormat = DEFAULT_HSV_VALUE_FORMAT;
        CmykValueFormat = DEFAULT_CMYK_VALUE_FORMAT;
        VectorValueFormat = DEFAULT_VECTOR_VALUE_FORMAT;
    }

    /// <summary>Gets or sets the instruction banner shown while picking.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Instruction banner shown on the screen overlay.")]
    [DefaultValue(DEFAULT_OVERLAY_INSTRUCTIONS)]
    public string OverlayInstructions { get; set; } = DEFAULT_OVERLAY_INSTRUCTIONS;

    /// <summary>Gets or sets the overlay window title.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Window title of the screen overlay.")]
    [DefaultValue(DEFAULT_OVERLAY_TITLE)]
    public string OverlayTitle { get; set; } = DEFAULT_OVERLAY_TITLE;

    /// <summary>Gets or sets the display name for Krypton flyout chrome.</summary>
    [Localizable(true)]
    [Category(@"Flyout")]
    [Description(@"Combo label for the themed Krypton flyout.")]
    [DefaultValue(DEFAULT_FLYOUT_STYLE_KRYPTON)]
    public string FlyoutStyleKrypton { get; set; } = DEFAULT_FLYOUT_STYLE_KRYPTON;

    /// <summary>Gets or sets the display name for Classic flyout chrome.</summary>
    [Localizable(true)]
    [Category(@"Flyout")]
    [Description(@"Combo label for the Classic (PowerToys) flyout.")]
    [DefaultValue(DEFAULT_FLYOUT_STYLE_CLASSIC)]
    public string FlyoutStyleClassic { get; set; } = DEFAULT_FLYOUT_STYLE_CLASSIC;

    /// <summary>Gets or sets the Hex format list label.</summary>
    [Localizable(true)]
    [Category(@"Formats")]
    [DefaultValue(DEFAULT_HEX_DISPLAY_NAME)]
    public string HexDisplayName { get; set; } = DEFAULT_HEX_DISPLAY_NAME;

    /// <summary>Gets or sets the Hex (alpha) format list label.</summary>
    [Localizable(true)]
    [Category(@"Formats")]
    [DefaultValue(DEFAULT_HEX_ALPHA_DISPLAY_NAME)]
    public string HexAlphaDisplayName { get; set; } = DEFAULT_HEX_ALPHA_DISPLAY_NAME;

    /// <summary>Gets or sets the Hex integer format list label.</summary>
    [Localizable(true)]
    [Category(@"Formats")]
    [DefaultValue(DEFAULT_HEX_INTEGER_DISPLAY_NAME)]
    public string HexIntegerDisplayName { get; set; } = DEFAULT_HEX_INTEGER_DISPLAY_NAME;

    /// <summary>Gets or sets the RGB format list label.</summary>
    [Localizable(true)]
    [Category(@"Formats")]
    [DefaultValue(DEFAULT_RGB_DISPLAY_NAME)]
    public string RgbDisplayName { get; set; } = DEFAULT_RGB_DISPLAY_NAME;

    /// <summary>Gets or sets the RGBA format list label.</summary>
    [Localizable(true)]
    [Category(@"Formats")]
    [DefaultValue(DEFAULT_RGBA_DISPLAY_NAME)]
    public string RgbaDisplayName { get; set; } = DEFAULT_RGBA_DISPLAY_NAME;

    /// <summary>Gets or sets the HSL format list label.</summary>
    [Localizable(true)]
    [Category(@"Formats")]
    [DefaultValue(DEFAULT_HSL_DISPLAY_NAME)]
    public string HslDisplayName { get; set; } = DEFAULT_HSL_DISPLAY_NAME;

    /// <summary>Gets or sets the HSV format list label.</summary>
    [Localizable(true)]
    [Category(@"Formats")]
    [DefaultValue(DEFAULT_HSV_DISPLAY_NAME)]
    public string HsvDisplayName { get; set; } = DEFAULT_HSV_DISPLAY_NAME;

    /// <summary>Gets or sets the CMYK format list label.</summary>
    [Localizable(true)]
    [Category(@"Formats")]
    [DefaultValue(DEFAULT_CMYK_DISPLAY_NAME)]
    public string CmykDisplayName { get; set; } = DEFAULT_CMYK_DISPLAY_NAME;

    /// <summary>Gets or sets the Decimal format list label.</summary>
    [Localizable(true)]
    [Category(@"Formats")]
    [DefaultValue(DEFAULT_DECIMAL_DISPLAY_NAME)]
    public string DecimalDisplayName { get; set; } = DEFAULT_DECIMAL_DISPLAY_NAME;

    /// <summary>Gets or sets the Vector format list label.</summary>
    [Localizable(true)]
    [Category(@"Formats")]
    [DefaultValue(DEFAULT_VECTOR_DISPLAY_NAME)]
    public string VectorDisplayName { get; set; } = DEFAULT_VECTOR_DISPLAY_NAME;

    /// <summary>Gets or sets the Known name format list label.</summary>
    [Localizable(true)]
    [Category(@"Formats")]
    [DefaultValue(DEFAULT_KNOWN_NAME_DISPLAY_NAME)]
    public string KnownNameDisplayName { get; set; } = DEFAULT_KNOWN_NAME_DISPLAY_NAME;

    /// <summary>Gets or sets the label used when no web colour name matches.</summary>
    [Localizable(true)]
    [Category(@"Formats")]
    [Description(@"Shown when the sampled colour is not a named web colour.")]
    [DefaultValue(DEFAULT_CUSTOM_COLOR_NAME)]
    public string CustomColorName { get; set; } = DEFAULT_CUSTOM_COLOR_NAME;

    /// <summary>Gets or sets the magnifier meta line. <c>{0}</c> is zoom, <c>{1}</c> is source pixels.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"Format for zoom and source-pixel count. {0} = zoom, {1} = source pixels.")]
    [DefaultValue(DEFAULT_MAGNIFIER_META_FORMAT)]
    public string MagnifierMetaFormat { get; set; } = DEFAULT_MAGNIFIER_META_FORMAT;

    /// <summary>Gets or sets the RGB hex value format. <c>{0}</c>–<c>{2}</c> are R, G, B.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [DefaultValue(DEFAULT_HEX_VALUE_FORMAT)]
    public string HexValueFormat { get; set; } = DEFAULT_HEX_VALUE_FORMAT;

    /// <summary>Gets or sets the ARGB hex value format. <c>{0}</c>–<c>{3}</c> are A, R, G, B.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [DefaultValue(DEFAULT_HEX_ALPHA_VALUE_FORMAT)]
    public string HexAlphaValueFormat { get; set; } = DEFAULT_HEX_ALPHA_VALUE_FORMAT;

    /// <summary>Gets or sets the hex integer value format. <c>{0}</c>–<c>{2}</c> are R, G, B.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [DefaultValue(DEFAULT_HEX_INTEGER_VALUE_FORMAT)]
    public string HexIntegerValueFormat { get; set; } = DEFAULT_HEX_INTEGER_VALUE_FORMAT;

    /// <summary>Gets or sets the RGB value format. <c>{0}</c>–<c>{2}</c> are R, G, B.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [DefaultValue(DEFAULT_RGB_VALUE_FORMAT)]
    public string RgbValueFormat { get; set; } = DEFAULT_RGB_VALUE_FORMAT;

    /// <summary>Gets or sets the RGBA value format. <c>{0}</c>–<c>{3}</c> are R, G, B, A.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [DefaultValue(DEFAULT_RGBA_VALUE_FORMAT)]
    public string RgbaValueFormat { get; set; } = DEFAULT_RGBA_VALUE_FORMAT;

    /// <summary>Gets or sets the HSL value format. <c>{0}</c> hue, <c>{1}</c> saturation %, <c>{2}</c> lightness %.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [DefaultValue(DEFAULT_HSL_VALUE_FORMAT)]
    public string HslValueFormat { get; set; } = DEFAULT_HSL_VALUE_FORMAT;

    /// <summary>Gets or sets the HSV value format. <c>{0}</c> hue, <c>{1}</c> saturation %, <c>{2}</c> value %.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [DefaultValue(DEFAULT_HSV_VALUE_FORMAT)]
    public string HsvValueFormat { get; set; } = DEFAULT_HSV_VALUE_FORMAT;

    /// <summary>Gets or sets the CMYK value format. <c>{0}</c>–<c>{3}</c> are C, M, Y, K percentages.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [DefaultValue(DEFAULT_CMYK_VALUE_FORMAT)]
    public string CmykValueFormat { get; set; } = DEFAULT_CMYK_VALUE_FORMAT;

    /// <summary>Gets or sets the unit RGB vector format. <c>{0}</c>–<c>{2}</c> are R, G, B in 0–1.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [DefaultValue(DEFAULT_VECTOR_VALUE_FORMAT)]
    public string VectorValueFormat { get; set; } = DEFAULT_VECTOR_VALUE_FORMAT;

    /// <summary>
    /// Display name for a single colour format flag.
    /// </summary>
    /// <param name="format">A single format flag.</param>
    /// <returns>The localised list label, or the enum name when <paramref name="format"/> is not a defined flag.</returns>
    public string GetFormatDisplayName(KryptonScreenColorPickerColorFormat format)
    {
        switch (format)
        {
            case KryptonScreenColorPickerColorFormat.Hex:
                return HexDisplayName;
            case KryptonScreenColorPickerColorFormat.HexAlpha:
                return HexAlphaDisplayName;
            case KryptonScreenColorPickerColorFormat.HexInteger:
                return HexIntegerDisplayName;
            case KryptonScreenColorPickerColorFormat.Rgb:
                return RgbDisplayName;
            case KryptonScreenColorPickerColorFormat.Rgba:
                return RgbaDisplayName;
            case KryptonScreenColorPickerColorFormat.Hsl:
                return HslDisplayName;
            case KryptonScreenColorPickerColorFormat.Hsv:
                return HsvDisplayName;
            case KryptonScreenColorPickerColorFormat.Cmyk:
                return CmykDisplayName;
            case KryptonScreenColorPickerColorFormat.Decimal:
                return DecimalDisplayName;
            case KryptonScreenColorPickerColorFormat.Vector:
                return VectorDisplayName;
            case KryptonScreenColorPickerColorFormat.KnownName:
                return KnownNameDisplayName;
            default:
                return format.ToString();
        }
    }

    internal string FormatTemplate(string format, string fallback, params object[] args)
    {
        string template = string.IsNullOrEmpty(format) ? fallback : format;
        return string.Format(CultureInfo.CurrentCulture, template, args);
    }

    internal string FormatMagnifierMeta(int zoom, int sourcePixels) =>
        FormatTemplate(MagnifierMetaFormat, DEFAULT_MAGNIFIER_META_FORMAT, zoom, sourcePixels);

    internal string FormatHex(Color color) =>
        FormatTemplate(HexValueFormat, DEFAULT_HEX_VALUE_FORMAT, color.R, color.G, color.B);

    internal string FormatHexAlpha(Color color) =>
        FormatTemplate(HexAlphaValueFormat, DEFAULT_HEX_ALPHA_VALUE_FORMAT, color.A, color.R, color.G, color.B);

    internal string FormatHexInteger(Color color) =>
        FormatTemplate(HexIntegerValueFormat, DEFAULT_HEX_INTEGER_VALUE_FORMAT, color.R, color.G, color.B);

    internal string FormatRgb(Color color) =>
        FormatTemplate(RgbValueFormat, DEFAULT_RGB_VALUE_FORMAT, color.R, color.G, color.B);

    internal string FormatRgba(Color color) =>
        FormatTemplate(RgbaValueFormat, DEFAULT_RGBA_VALUE_FORMAT, color.R, color.G, color.B, color.A);

    internal string FormatHsl(float hue, float saturationPercent, float lightnessPercent) =>
        FormatTemplate(HslValueFormat, DEFAULT_HSL_VALUE_FORMAT, hue, saturationPercent, lightnessPercent);

    internal string FormatHsv(float hue, float saturationPercent, float valuePercent) =>
        FormatTemplate(HsvValueFormat, DEFAULT_HSV_VALUE_FORMAT, hue, saturationPercent, valuePercent);

    internal string FormatCmyk(float cyanPercent, float magentaPercent, float yellowPercent, float blackPercent) =>
        FormatTemplate(CmykValueFormat, DEFAULT_CMYK_VALUE_FORMAT,
            cyanPercent, magentaPercent, yellowPercent, blackPercent);

    internal string FormatVector(float r, float g, float b) =>
        FormatTemplate(VectorValueFormat, DEFAULT_VECTOR_VALUE_FORMAT, r, g, b);
}

#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Themes;

/// <summary>
/// Sparkle renderer with accessibility colours (#4168).
/// </summary>
public class PaletteSparkleDeuteranopia : PaletteSparkleBase
{
    private static readonly ImageList _checkBoxList;
    private static readonly Image?[] _radioButtonArray;

    private static readonly Color[] _appButtonNormal =
    [
        Color.FromArgb(243, 245, 248),
        Color.FromArgb(214, 220, 231),
        Color.FromArgb(188, 198, 211),
        Color.FromArgb(254, 254, 255),
        Color.FromArgb(206, 213, 225)
    ];

    private static readonly Color[] _appButtonTrack =
    [
        Color.FromArgb(215, 239, 245),
        Color.FromArgb(146, 214, 238),
        Color.FromArgb(60, 155, 201),
        Color.FromArgb(93, 201, 248),
        Color.FromArgb(25, 168, 238)
    ];

    private static readonly Color[] _appButtonPressed =
    [
        Color.FromArgb(196, 227, 235),
        Color.FromArgb(149, 198, 228),
        Color.FromArgb(7, 97, 166),
        Color.FromArgb(57, 155, 242),
        Color.FromArgb(9, 136, 236)
    ];

    private static readonly Color[] _ribbonGroupCollapsedBorderContextTracking =
    [
        Color.FromArgb(128, 168, 184, 196),
        Color.FromArgb(168, 184, 196),
        Color.FromArgb(48, 255, 255, 255),
        Color.FromArgb(192, 207, 220)
    ];

    private static readonly Color[] _sparkleColors =
    [
        Color.FromArgb(99, 108, 135),
        Color.FromArgb(86, 94, 118),
        Color.FromArgb(72, 81, 102),
        Color.FromArgb(45, 45, 45),
        Color.FromArgb(27, 31, 38),
        Color.FromArgb(20, 21, 23),
        Color.FromArgb(19, 37, 61),
        Color.FromArgb(60, 129, 206),
        Color.FromArgb(13, 30, 52),
        Color.FromArgb(125, 205, 248),
        Color.FromArgb(28, 66, 160),
        Color.FromArgb(87, 198, 239),
        Color.FromArgb(14, 65, 204),
        Color.FromArgb(112, 212, 255),
        Color.FromArgb(27, 65, 160),
        Color.FromArgb(51, 153, 255),
        Color.FromArgb(29, 89, 131),
        Color.FromArgb(164, 225, 236, 244),
        Color.FromArgb(164, 181, 215, 231),
        Color.FromArgb(164, 91, 187, 230),
        Color.FromArgb(220, 229, 244),
        Color.FromArgb(185, 191, 230),
        Color.FromArgb(57, 66, 102),
        Color.FromArgb(57, 175, 250),
        Color.FromArgb(177, 219, 242),
        Color.FromArgb(180, 218, 242),
        Color.FromArgb(145, 198, 228),
        Color.FromArgb(148, 197, 228),
        Color.FromArgb(190, 190, 190),
        Color.FromArgb(79, 180, 239),
        Color.FromArgb(48, 89, 146),
        Color.FromArgb(85, 132, 196),
        Color.FromArgb(209, 220, 235),
        Color.FromArgb(202, 211, 222),
        Color.FromArgb(176, 196, 222),
        Color.FromArgb(82, 120, 213),
        Color.FromArgb(72, 110, 213),
        Color.FromArgb(10, 20, 255)
    ];

    static PaletteSparkleDeuteranopia()
    {
        _checkBoxList = new ImageList { ImageSize = new Size(13, 13), ColorDepth = ColorDepth.Depth24Bit };
        _checkBoxList.Images.AddStrip(CheckBoxStripResources.CheckBoxStripSparkle);
        _radioButtonArray =
        [
            SparkleRadioButtonImageResources.RadioButtonSparkleD,
            SparkleRadioButtonImageResources.RadioButtonSparkleN,
            SparkleRadioButtonImageResources.RadioButtonSparkleT,
            SparkleRadioButtonImageResources.RadioButtonSparkleP,
            SparkleRadioButtonImageResources.RadioButtonSparkleDC,
            SparkleRadioButtonImageResources.RadioButtonSparkleNC,
            SparkleRadioButtonImageResources.RadioButtonSparkleTC,
            SparkleRadioButtonImageResources.RadioButtonSparklePC
        ];
    }

    /// <summary>
    /// Initialize a new instance of the <see cref="PaletteSparkleDeuteranopia"/> class.
    /// </summary>
    public PaletteSparkleDeuteranopia()
        : base(new PaletteDeuteranopia_BaseScheme(), _sparkleColors, _appButtonNormal, _appButtonTrack, _appButtonPressed,
            _ribbonGroupCollapsedBorderContextTracking, _checkBoxList, _radioButtonArray)
    {
        ThemeName = nameof(PaletteSparkleDeuteranopia);
        AccessibilitySparkleAccents.ApplyDeuteranopia(_sparkleColors, _appButtonTrack, _appButtonPressed);
    }

    /// <inheritdoc />
    public override Color GetContentShortTextColor1(PaletteContentStyle style, PaletteState state)
    {
        Color? text = AccessibilityContentContrast.TryGetDeuteranopiaText(style, state);
        return text ?? base.GetContentShortTextColor1(style, state);
    }

    /// <inheritdoc />
    public override Color GetContentShortTextColor2(PaletteContentStyle style, PaletteState state)
    {
        Color? text = AccessibilityContentContrast.TryGetDeuteranopiaText(style, state);
        return text ?? base.GetContentShortTextColor2(style, state);
    }

    /// <inheritdoc />
    public override Color GetContentLongTextColor1(PaletteContentStyle style, PaletteState state)
    {
        Color? text = AccessibilityContentContrast.TryGetDeuteranopiaText(style, state);
        return text ?? base.GetContentLongTextColor1(style, state);
    }

    /// <inheritdoc />
    public override Color GetContentLongTextColor2(PaletteContentStyle style, PaletteState state)
    {
        Color? text = AccessibilityContentContrast.TryGetDeuteranopiaText(style, state);
        return text ?? base.GetContentLongTextColor2(style, state);
    }
}
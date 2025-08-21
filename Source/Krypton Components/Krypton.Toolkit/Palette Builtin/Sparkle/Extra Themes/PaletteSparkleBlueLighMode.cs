#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, et al. 2017 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Provides a fixed blue variation on the sparkle appearance.
/// </summary>
public class PaletteSparkleBlueLightMode : PaletteSparkleBase
{
    #region Static Fields
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
        Color.FromArgb(99, 108, 135),        // 0 _colorDark99
        Color.FromArgb(86, 94, 118),         // 1 _colorDark86
        Color.FromArgb(72, 81, 102),         // 2 _colorDark72
        Color.FromArgb(45, 45, 45),          // 3 _colorDark45
        Color.FromArgb(27, 31, 38),          // 4 _colorDark27
        Color.FromArgb(20, 21, 23),          // 5 _colorDark20
        Color.FromArgb(19, 37, 61),          // 6 _buttonTrackBack1
        Color.FromArgb(60, 129, 206),        // 7 _buttonTrackBack2
        Color.FromArgb(13, 30, 52),          // 8 _buttonPressBack1
        Color.FromArgb(125, 205, 248),       // 9 _buttonPressBack2
        Color.FromArgb(28, 66, 160),         // 10 _buttonCheckBack1
        Color.FromArgb(87, 198, 239),        // 11 _buttonCheckBack2
        Color.FromArgb(14, 65, 204),         // 12 _buttonCheckTrackBack1
        Color.FromArgb(112, 212, 255),       // 13 _buttonCheckTrackBack2
        Color.FromArgb(27, 65, 160),         // 14 _buttonCheckPressBack1
        Color.FromArgb(51, 153, 255),        // 15 _colorBlue
        Color.FromArgb(29, 89, 131),         // 16 _menuItemHeading
        Color.FromArgb(164, 225, 236, 244),  // 17 _menuItemTrackBack1
        Color.FromArgb(164, 181, 215, 231),  // 18 _menuItemTrackBack2
        Color.FromArgb(164, 91, 187, 230),   // 19 _menuItemTrackBorder
        Color.FromArgb(220, 229, 244),       // 20 _menuItemCheckedBack
        Color.FromArgb(185, 191, 230),       // 21 _menuItemCheckedBorder
        Color.FromArgb(57, 66, 102),         // 22 _buttonBack2
        Color.FromArgb(57, 175, 250),        // 23 _buttonDefaultBack
        Color.FromArgb(177, 219, 242),       // 24 _gridHeaderTracking1
        Color.FromArgb(180, 218, 242),       // 25 _gridHeaderTracking2
        Color.FromArgb(145, 198, 228),       // 26 _gridHeaderPressed1
        Color.FromArgb(148, 197, 228),       // 27 _gridHeaderPressed2
        Color.FromArgb(190, 190, 190),       // 28 _gridCellBorder
        Color.FromArgb(79, 180, 239),        // 29 _tabCheckedNormal
        Color.FromArgb(48, 89, 146),         // 30 _ribbonFrameBorder1
        Color.FromArgb(85, 132, 196),        // 31 _ribbonFrameBorder1
        Color.FromArgb(209, 220, 235),       // 32 _ribbonFrameBack1
        Color.FromArgb(202, 211, 222),       // 33 _ribbonFrameBack2
        Color.FromArgb(176, 196, 222),       // 34 _ribbonFrameBack3
        Color.FromArgb(82, 120, 213),        // 35 _ribbonFrameBack3
        Color.FromArgb(72, 110, 213),        // 36 _contextCheckedTabFill
        Color.FromArgb(10, 20, 255) // 37 _focusTabFill
    ];

    #endregion

    #region Identity
    static PaletteSparkleBlueLightMode()
    {
        _checkBoxList = new ImageList
        {
            ImageSize = new Size(13, 13),
            ColorDepth = ColorDepth.Depth24Bit
        };
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
    /// Initialize a new instance of the PaletteSparkleBlueLightMode class.
    /// </summary>
    public PaletteSparkleBlueLightMode()
        : base(
        new PaletteSparkleBlueLighMode_BaseScheme(),
        _sparkleColors,
        _appButtonNormal,
        _appButtonTrack,
        _appButtonPressed,
        _ribbonGroupCollapsedBorderContextTracking,
        _checkBoxList,
        _radioButtonArray)
    {
        ThemeName = nameof(PaletteSparkleBlueLightMode);
    }
    #endregion
}
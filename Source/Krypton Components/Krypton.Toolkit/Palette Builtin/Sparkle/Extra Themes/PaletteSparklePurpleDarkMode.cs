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
/// Provides a fixed purple variation on the sparkle appearance.
/// </summary>
public class PaletteSparklePurpleDarkMode : PaletteSparkleBase
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
        Color.FromArgb(239, 215, 245),
        Color.FromArgb(214, 146, 238),
        Color.FromArgb(155, 60, 201),
        Color.FromArgb(201, 93, 248),
        Color.FromArgb(168, 25, 238)
    ];

    private static readonly Color[] _appButtonPressed =
    [
        Color.FromArgb(227, 196, 235),
        Color.FromArgb(198, 149, 228),
        Color.FromArgb(97, 7, 166),
        Color.FromArgb(155, 57, 242),
        Color.FromArgb(136, 9, 236)
    ];

    private static readonly Color[] _ribbonGroupCollapsedBorderContextTracking =
    [
        Color.FromArgb(128, 184, 168, 196),
        Color.FromArgb(184, 168, 196),
        Color.FromArgb(48, 255, 255, 255),
        Color.FromArgb(207, 192, 220)
    ];

    private static readonly Color[] _sparkleColors =
    [
        Color.FromArgb(99, 108, 135),        // 0 _colorDark99
        Color.FromArgb(86, 94, 118),         // 1 _colorDark86
        Color.FromArgb(72, 81, 102),         // 2 _colorDark72
        Color.FromArgb(45, 45, 45),          // 3 _colorDark45
        Color.FromArgb(27, 31, 38),          // 4 _colorDark27
        Color.FromArgb(20, 21, 23),          // 5 _colorDark20
        Color.FromArgb(37, 19, 61),          // 6 _buttonTrackBack1
        Color.FromArgb(129, 60, 206),        // 7 _buttonTrackBack2
        Color.FromArgb(30, 13, 52),          // 8 _buttonPressBack1
        Color.FromArgb(205, 125, 248),       // 9 _buttonPressBack2
        Color.FromArgb(66, 28, 160),         // 10 _buttonCheckBack1
        Color.FromArgb(198, 87, 239),        // 11 _buttonCheckBack2
        Color.FromArgb(65, 14, 204),         // 12 _buttonCheckTrackBack1
        Color.FromArgb(212, 112, 255),       // 13 _buttonCheckTrackBack2
        Color.FromArgb(65, 27, 160),         // 14 _buttonCheckPressBack1
        Color.FromArgb(153, 51, 255),        // 15 _colorBlue
        Color.FromArgb(89, 29, 131),         // 16 _menuItemHeading
        Color.FromArgb(164, 236, 225, 244),  // 17 _menuItemTrackBack1
        Color.FromArgb(164, 215, 181, 231),  // 18 _menuItemTrackBack2
        Color.FromArgb(164, 187, 91, 230),   // 19 _menuItemTrackBorder
        Color.FromArgb(239, 230, 244),       // 20 _menuItemCheckedBack
        Color.FromArgb(196, 190, 230),       // 21 _menuItemCheckedBorder
        Color.FromArgb(57, 66, 102),         // 22 _buttonBack2
        Color.FromArgb(175, 57, 250),        // 23 _buttonDefaultBack
        Color.FromArgb(219, 177, 242),       // 24 _gridHeaderTracking1
        Color.FromArgb(218, 180, 242),       // 25 _gridHeaderTracking2
        Color.FromArgb(198, 145, 228),       // 26 _gridHeaderPressed1
        Color.FromArgb(197, 148, 228),       // 27 _gridHeaderPressed2
        Color.FromArgb(190, 190, 190),       // 28 _gridCellBorder
        Color.FromArgb(180, 79, 239),        // 29 _tabCheckedNormal
        Color.FromArgb(89, 48, 146),         // 30 _ribbonFrameBorder1
        Color.FromArgb(132, 85, 196),        // 31 _ribbonFrameBorder1
        Color.FromArgb(220, 209, 235),       // 32 _ribbonFrameBack1
        Color.FromArgb(211, 202, 222),       // 33 _ribbonFrameBack2
        Color.FromArgb(196, 176, 222),       // 34 _ribbonFrameBack3
        Color.FromArgb(120, 82, 213),        // 35 _ribbonFrameBack3
        Color.FromArgb(110, 72, 213),        // 36 _contextCheckedTabFill
        Color.FromArgb(20, 10, 255) // 37 _focusTabFill
    ];

    #endregion

    #region Identity
    static PaletteSparklePurpleDarkMode()
    {
        _checkBoxList = new ImageList
        {
            ImageSize = new Size(13, 13),
            ColorDepth = ColorDepth.Depth24Bit
        };
        _checkBoxList.Images.AddStrip(CheckBoxStripResources.CheckBoxStripSparklePurple);
        _radioButtonArray =
        [
            SparkleRadioButtonImageResources.RadioButtonSparkleD,
            SparkleRadioButtonImageResources.RadioButtonSparkleN,
            SparkleRadioButtonImageResources.RadioButtonSparklePurpleT,
            SparkleRadioButtonImageResources.RadioButtonSparklePurpleP,
            SparkleRadioButtonImageResources.RadioButtonSparkleDC,
            SparkleRadioButtonImageResources.RadioButtonSparklePurpleNC,
            SparkleRadioButtonImageResources.RadioButtonSparklePurpleTC,
            SparkleRadioButtonImageResources.RadioButtonSparklePurplePC
        ];
    }

    /// <summary>
    /// Initialize a new instance of the PaletteSparklePurpleDarkMode class.
    /// </summary>
    public PaletteSparklePurpleDarkMode()
        : base(
        new PaletteSparklePurpleDarkMode_BaseScheme(),
        _sparkleColors,
        _appButtonNormal,
        _appButtonTrack,
        _appButtonPressed,
        _ribbonGroupCollapsedBorderContextTracking,
        _checkBoxList,
        _radioButtonArray)
    {
        ThemeName = nameof(PaletteSparklePurpleDarkMode);
    }
    #endregion
}
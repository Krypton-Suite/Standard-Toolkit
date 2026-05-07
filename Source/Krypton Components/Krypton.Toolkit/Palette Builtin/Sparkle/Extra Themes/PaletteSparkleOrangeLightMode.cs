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
/// Provides a fixed orange variation on the sparkle appearance.
/// </summary>
public class PaletteSparkleOrangeLightMode : PaletteSparkleBase
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
        Color.FromArgb(245, 239, 215),
        Color.FromArgb(238, 214, 146),
        Color.FromArgb(201, 155, 60),
        Color.FromArgb(248, 201, 93),
        Color.FromArgb(238, 168, 25)
    ];

    private static readonly Color[] _appButtonPressed =
    [
        Color.FromArgb(235, 227, 196),
        Color.FromArgb(228, 198, 149),
        Color.FromArgb(166, 97, 7),
        Color.FromArgb(242, 155, 57),
        Color.FromArgb(236, 136, 9)
    ];

    private static readonly Color[] _ribbonGroupCollapsedBorderContextTracking =
    [
        Color.FromArgb(128, 196, 184, 168),
        Color.FromArgb(196, 184, 169),
        Color.FromArgb(48, 255, 255, 255),
        Color.FromArgb(220, 207, 192)
    ];

    private static readonly Color[] _sparkleColors =
    [
        Color.FromArgb(99, 108, 135),        // 0 _colorDark99
        Color.FromArgb(86, 94, 118),         // 1 _colorDark86
        Color.FromArgb(72, 81, 102),         // 2 _colorDark72
        Color.FromArgb(45, 45, 45),          // 3 _colorDark45
        Color.FromArgb(27, 31, 38),          // 4 _colorDark27
        Color.FromArgb(20, 21, 23),          // 5 _colorDark20
        Color.FromArgb(61, 37, 19),          // 6 _buttonTrackBack1
        Color.FromArgb(206, 129, 60),        // 7 _buttonTrackBack2
        Color.FromArgb(52, 30, 13),          // 8 _buttonPressBack1
        Color.FromArgb(248, 205, 125),       // 9 _buttonPressBack2
        Color.FromArgb(160, 66, 28),         // 10 _buttonCheckBack1
        Color.FromArgb(239, 198, 87),        // 11 _buttonCheckBack2
        Color.FromArgb(204, 65, 14),         // 12 _buttonCheckTrackBack1
        Color.FromArgb(255, 212, 112),       // 13 _buttonCheckTrackBack2
        Color.FromArgb(160, 65, 27),         // 14 _buttonCheckPressBack1
        Color.FromArgb(240, 153, 51),        // 15 _colorBlue
        Color.FromArgb(171, 91, 91),         // 16 _menuItemHeading
        Color.FromArgb(225, 244, 236, 225),  // 17 _menuItemTrackBack1
        Color.FromArgb(225, 231, 215, 181),  // 18 _menuItemTrackBack2
        Color.FromArgb(225, 230, 187, 91),   // 19 _menuItemTrackBorder
        Color.FromArgb(244, 239, 230),       // 20 _menuItemCheckedBack
        Color.FromArgb(230, 191, 185),       // 21 _menuItemCheckedBorder
        Color.FromArgb( 80,  80,  80),       // 22 _buttonBack2
        Color.FromArgb(250, 175, 57),        // 23 _buttonDefaultBack
        Color.FromArgb(242, 219, 177),       // 24 _gridHeaderTracking1
        Color.FromArgb(242, 218, 180),       // 25 _gridHeaderTracking2
        Color.FromArgb(228, 198, 145),       // 26 _gridHeaderPressed1
        Color.FromArgb(228, 197, 148),       // 27 _gridHeaderPressed2
        Color.FromArgb(190, 190, 190),       // 28 _gridCellBorder
        Color.FromArgb(239, 180, 79),        // 29 _tabCheckedNormal
        Color.FromArgb(146, 89, 48),         // 30 _ribbonFrameBorder1
        Color.FromArgb(196, 132, 85),        // 31 _ribbonFrameBorder1
        Color.FromArgb(235, 220, 209),       // 32 _ribbonFrameBack1
        Color.FromArgb(222, 211, 202),       // 33 _ribbonFrameBack2
        Color.FromArgb(222, 196, 176),       // 34 _ribbonFrameBack3
        Color.FromArgb(213, 120, 82),        // 35 _ribbonFrameBack4
        Color.FromArgb(213, 110, 72),        // 36 _contextCheckedTabFill
        Color.FromArgb(255, 20, 10) // 37 _focusTabFill
    ];

    #endregion

    #region Identity
    static PaletteSparkleOrangeLightMode()
    {
        _checkBoxList = new ImageList
        {
            ImageSize = new Size(13, 13),
            ColorDepth = ColorDepth.Depth24Bit
        };
        _checkBoxList.Images.AddStrip(CheckBoxStripResources.CheckBoxStripSparkleOrange);
        _radioButtonArray =
        [
            SparkleRadioButtonImageResources.RadioButtonSparkleD,
            SparkleRadioButtonImageResources.RadioButtonSparkleN,
            SparkleRadioButtonImageResources.RadioButtonSparkleOrangeT,
            SparkleRadioButtonImageResources.RadioButtonSparkleOrangeP,
            SparkleRadioButtonImageResources.RadioButtonSparkleDC,
            SparkleRadioButtonImageResources.RadioButtonSparkleOrangeNC,
            SparkleRadioButtonImageResources.RadioButtonSparkleOrangeTC,
            SparkleRadioButtonImageResources.RadioButtonSparkleOrangePC
        ];
    }

    /// <summary>
    /// Initialize a new instance of the PaletteSparkleOrangeLightMode class.
    /// </summary>
    public PaletteSparkleOrangeLightMode()
        : base(
        new PaletteSparkleOrangeLightMode_BaseScheme(),
        _sparkleColors,
        _appButtonNormal,
        _appButtonTrack,
        _appButtonPressed,
        _ribbonGroupCollapsedBorderContextTracking,
        _checkBoxList,
        _radioButtonArray)
    {
        ThemeName = nameof(PaletteSparkleOrangeLightMode);
    }
    #endregion
}
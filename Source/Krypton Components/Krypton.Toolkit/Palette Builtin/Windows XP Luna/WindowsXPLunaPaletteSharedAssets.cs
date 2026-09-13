#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Shared checkbox, radio, gallery, and form-button assets for Windows XP Luna palette variants.
/// </summary>
internal static class WindowsXPLunaPaletteSharedAssets
{
    internal static readonly ImageList BlueCheckBoxList;

    internal static readonly ImageList BlueGalleryButtonList;

    internal static readonly Image?[] BlueRadioButtonArray;

    internal static readonly ImageList SilverCheckBoxList;

    internal static readonly ImageList SilverGalleryButtonList;

    internal static readonly Image?[] SilverRadioButtonArray;

    internal static readonly Image? BlueContextMenuSubMenu = GenericImageResources.BlueContextMenuSub;

    internal static readonly Image? SilverContextMenuSubMenu = GenericImageResources.SilverContextMenuSub;

    internal static readonly ImageList BlackCheckBoxList;

    internal static readonly ImageList BlackGalleryButtonList;

    internal static readonly Image?[] BlackRadioButtonArray;

    internal static readonly Image? BlackContextMenuSubMenu = GenericImageResources.BlackContextMenuSub;

    static WindowsXPLunaPaletteSharedAssets()
    {
        BlueCheckBoxList = new ImageList
        {
            ImageSize = new Size(13, 13),
            ColorDepth = ColorDepth.Depth24Bit
        };
        BlueCheckBoxList.Images.AddStrip(CheckBoxStripResources.CheckBoxStrip2007Blue);

        BlueGalleryButtonList = new ImageList
        {
            ImageSize = new Size(13, 7),
            ColorDepth = ColorDepth.Depth24Bit,
            TransparentColor = GlobalStaticVariables.TRANSPARENCY_KEY_COLOR
        };
        BlueGalleryButtonList.Images.AddStrip(GalleryImageResources.GalleryBlue);

        BlueRadioButtonArray =
        [
            Office2007RadioButtonImageResources.RadioButton2007BlueD,
            Office2007RadioButtonImageResources.RadioButton2007BlueN,
            Office2007RadioButtonImageResources.RadioButton2007BlueT,
            Office2007RadioButtonImageResources.RadioButton2007BlueP,
            Office2007RadioButtonImageResources.RadioButton2007BlueDC,
            Office2007RadioButtonImageResources.RadioButton2007BlueNC,
            Office2007RadioButtonImageResources.RadioButton2007BlueTC,
            Office2007RadioButtonImageResources.RadioButton2007BluePC
        ];

        SilverCheckBoxList = new ImageList
        {
            ImageSize = new Size(13, 13),
            ColorDepth = ColorDepth.Depth24Bit
        };
        SilverCheckBoxList.Images.AddStrip(CheckBoxStripResources.CheckBoxStrip2007Silver);

        SilverGalleryButtonList = new ImageList
        {
            ImageSize = new Size(13, 7),
            ColorDepth = ColorDepth.Depth24Bit,
            TransparentColor = GlobalStaticVariables.TRANSPARENCY_KEY_COLOR
        };
        SilverGalleryButtonList.Images.AddStrip(GalleryImageResources.GallerySilverBlack);

        SilverRadioButtonArray =
        [
            Office2007RadioButtonImageResources.RadioButton2007BlueD,
            Office2007RadioButtonImageResources.RadioButton2007SilverN,
            Office2007RadioButtonImageResources.RadioButton2007SilverT,
            Office2007RadioButtonImageResources.RadioButton2007SilverP,
            Office2007RadioButtonImageResources.RadioButton2007BlueDC,
            Office2007RadioButtonImageResources.RadioButton2007SilverNC,
            Office2007RadioButtonImageResources.RadioButton2007SilverTC,
            Office2007RadioButtonImageResources.RadioButton2007SilverPC
        ];

        BlackCheckBoxList = new ImageList
        {
            ImageSize = new Size(13, 13),
            ColorDepth = ColorDepth.Depth24Bit
        };
        BlackCheckBoxList.Images.AddStrip(CheckBoxStripResources.CheckBoxStrip2007Black);

        BlackGalleryButtonList = new ImageList
        {
            ImageSize = new Size(13, 7),
            ColorDepth = ColorDepth.Depth24Bit,
            TransparentColor = GlobalStaticVariables.TRANSPARENCY_KEY_COLOR
        };
        BlackGalleryButtonList.Images.AddStrip(GalleryImageResources.GallerySilverBlack);

        BlackRadioButtonArray =
        [
            Office2007RadioButtonImageResources.RadioButton2007BlueD,
            Office2007RadioButtonImageResources.RadioButton2007BlackN,
            Office2007RadioButtonImageResources.RadioButton2007BlackT,
            Office2007RadioButtonImageResources.RadioButton2007BlackP,
            Office2007RadioButtonImageResources.RadioButton2007BlueDC,
            Office2007RadioButtonImageResources.RadioButton2007BlackNC,
            Office2007RadioButtonImageResources.RadioButton2007BlackTC,
            Office2007RadioButtonImageResources.RadioButton2007BlackPC
        ];
    }

    private static readonly Size FormButtonSize = new Size(21, 21);

    private static readonly LunaFormButtonSet BlueFormButtons =
        new LunaFormButtonSet(WindowsXPLunaFormButtonGlyphFactory.LunaChromeVariant.Blue);

    private static readonly LunaFormButtonSet OliveFormButtons =
        new LunaFormButtonSet(WindowsXPLunaFormButtonGlyphFactory.LunaChromeVariant.Olive);

    private static readonly LunaFormButtonSet SilverFormButtons =
        new LunaFormButtonSet(WindowsXPLunaFormButtonGlyphFactory.LunaChromeVariant.Silver);

    private static readonly LunaFormButtonSet DarkFormButtons =
        new LunaFormButtonSet(WindowsXPLunaFormButtonGlyphFactory.LunaChromeVariant.Dark);

    private static readonly Image? FormHelpNormal = Office2007ControlBoxResources.Office2007HelpIconNormal;

    private static readonly Image? FormHelpDisabled = Office2007ControlBoxResources.Office2007HelpIconDisabled;

    private static readonly Image? FormHelpActive = Office2007ControlBoxResources.Office2007HelpIconHover;

    private static readonly Image? FormHelpPressed = Office2007ControlBoxResources.Office2007HelpIconPressed;

    internal static Image? GetBlueFormButtonImage(PaletteButtonSpecStyle style, PaletteState state) =>
        GetLunaFormButtonImage(BlueFormButtons, style, state);

    internal static Image? GetOliveFormButtonImage(PaletteButtonSpecStyle style, PaletteState state) =>
        GetLunaFormButtonImage(OliveFormButtons, style, state);

    internal static Image? GetSilverFormButtonImage(PaletteButtonSpecStyle style, PaletteState state) =>
        GetLunaFormButtonImage(SilverFormButtons, style, state);

    internal static Image? GetBlackFormButtonImage(PaletteButtonSpecStyle style, PaletteState state) =>
        GetLunaFormButtonImage(DarkFormButtons, style, state);

    private static Image? GetLunaFormButtonImage(LunaFormButtonSet set, PaletteButtonSpecStyle style, PaletteState state) =>
        style switch
        {
            PaletteButtonSpecStyle.FormClose => set.Get(WindowsXPLunaFormButtonGlyphFactory.CaptionButtonKind.Close, state),
            PaletteButtonSpecStyle.FormMin => set.Get(WindowsXPLunaFormButtonGlyphFactory.CaptionButtonKind.Minimize, state),
            PaletteButtonSpecStyle.FormMax => set.Get(WindowsXPLunaFormButtonGlyphFactory.CaptionButtonKind.Maximize, state),
            PaletteButtonSpecStyle.FormRestore => set.Get(WindowsXPLunaFormButtonGlyphFactory.CaptionButtonKind.Restore, state),
            PaletteButtonSpecStyle.FormHelp => state switch
            {
                PaletteState.Tracking => FormHelpActive,
                PaletteState.Pressed => FormHelpPressed,
                PaletteState.Disabled => FormHelpDisabled,
                _ => FormHelpNormal
            },
            _ => null
        };

    private sealed class LunaFormButtonSet
    {
        private readonly WindowsXPLunaFormButtonGlyphFactory.LunaChromeVariant _variant;
        private readonly Image? _closeNormal;
        private readonly Image? _closeDisabled;
        private readonly Image? _closeActive;
        private readonly Image? _closePressed;
        private readonly Image? _minNormal;
        private readonly Image? _minDisabled;
        private readonly Image? _minActive;
        private readonly Image? _minPressed;
        private readonly Image? _maxNormal;
        private readonly Image? _maxDisabled;
        private readonly Image? _maxActive;
        private readonly Image? _maxPressed;
        private readonly Image? _restoreNormal;
        private readonly Image? _restoreDisabled;
        private readonly Image? _restoreActive;
        private readonly Image? _restorePressed;

        internal LunaFormButtonSet(WindowsXPLunaFormButtonGlyphFactory.LunaChromeVariant variant)
        {
            _variant = variant;
            _closeNormal = Create(WindowsXPLunaFormButtonGlyphFactory.CaptionButtonKind.Close, PaletteState.Normal);
            _closeDisabled = Create(WindowsXPLunaFormButtonGlyphFactory.CaptionButtonKind.Close, PaletteState.Disabled);
            _closeActive = Create(WindowsXPLunaFormButtonGlyphFactory.CaptionButtonKind.Close, PaletteState.Tracking);
            _closePressed = Create(WindowsXPLunaFormButtonGlyphFactory.CaptionButtonKind.Close, PaletteState.Pressed);
            _minNormal = Create(WindowsXPLunaFormButtonGlyphFactory.CaptionButtonKind.Minimize, PaletteState.Normal);
            _minDisabled = Create(WindowsXPLunaFormButtonGlyphFactory.CaptionButtonKind.Minimize, PaletteState.Disabled);
            _minActive = Create(WindowsXPLunaFormButtonGlyphFactory.CaptionButtonKind.Minimize, PaletteState.Tracking);
            _minPressed = Create(WindowsXPLunaFormButtonGlyphFactory.CaptionButtonKind.Minimize, PaletteState.Pressed);
            _maxNormal = Create(WindowsXPLunaFormButtonGlyphFactory.CaptionButtonKind.Maximize, PaletteState.Normal);
            _maxDisabled = Create(WindowsXPLunaFormButtonGlyphFactory.CaptionButtonKind.Maximize, PaletteState.Disabled);
            _maxActive = Create(WindowsXPLunaFormButtonGlyphFactory.CaptionButtonKind.Maximize, PaletteState.Tracking);
            _maxPressed = Create(WindowsXPLunaFormButtonGlyphFactory.CaptionButtonKind.Maximize, PaletteState.Pressed);
            _restoreNormal = Create(WindowsXPLunaFormButtonGlyphFactory.CaptionButtonKind.Restore, PaletteState.Normal);
            _restoreDisabled = Create(WindowsXPLunaFormButtonGlyphFactory.CaptionButtonKind.Restore, PaletteState.Disabled);
            _restoreActive = Create(WindowsXPLunaFormButtonGlyphFactory.CaptionButtonKind.Restore, PaletteState.Tracking);
            _restorePressed = Create(WindowsXPLunaFormButtonGlyphFactory.CaptionButtonKind.Restore, PaletteState.Pressed);
        }

        internal Image? Get(WindowsXPLunaFormButtonGlyphFactory.CaptionButtonKind kind, PaletteState state) =>
            kind switch
            {
                WindowsXPLunaFormButtonGlyphFactory.CaptionButtonKind.Close => state switch
                {
                    PaletteState.Disabled => _closeDisabled,
                    PaletteState.Tracking or PaletteState.CheckedTracking => _closeActive,
                    PaletteState.Pressed or PaletteState.CheckedPressed => _closePressed,
                    _ => _closeNormal
                },
                WindowsXPLunaFormButtonGlyphFactory.CaptionButtonKind.Minimize => state switch
                {
                    PaletteState.Disabled => _minDisabled,
                    PaletteState.Tracking or PaletteState.CheckedTracking => _minActive,
                    PaletteState.Pressed or PaletteState.CheckedPressed => _minPressed,
                    _ => _minNormal
                },
                WindowsXPLunaFormButtonGlyphFactory.CaptionButtonKind.Maximize => state switch
                {
                    PaletteState.Disabled => _maxDisabled,
                    PaletteState.Tracking or PaletteState.CheckedTracking => _maxActive,
                    PaletteState.Pressed or PaletteState.CheckedPressed => _maxPressed,
                    _ => _maxNormal
                },
                WindowsXPLunaFormButtonGlyphFactory.CaptionButtonKind.Restore => state switch
                {
                    PaletteState.Disabled => _restoreDisabled,
                    PaletteState.Tracking or PaletteState.CheckedTracking => _restoreActive,
                    PaletteState.Pressed or PaletteState.CheckedPressed => _restorePressed,
                    _ => _restoreNormal
                },
                _ => null
            };

        private Image? Create(WindowsXPLunaFormButtonGlyphFactory.CaptionButtonKind kind, PaletteState state) =>
            WindowsXPLunaFormButtonGlyphFactory.Create(kind, state, FormButtonSize, _variant);
    }
}

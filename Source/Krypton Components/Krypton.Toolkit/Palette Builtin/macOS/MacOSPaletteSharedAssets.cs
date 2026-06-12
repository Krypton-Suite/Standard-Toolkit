#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Traffic-light form button images for macOS palette variants (light and dark title bars).
/// </summary>
internal static class MacOSPaletteSharedAssets
{
    private static readonly Size FormButtonSize = new Size(18, 18);

    private static readonly TrafficLightSet Light = CreateSet(isDarkSurface: false);
    private static readonly TrafficLightSet Dark = CreateSet(isDarkSurface: true);

    private static readonly Image? FormHelpNormal;
    private static readonly Image? FormHelpDisabled;
    private static readonly Image? FormHelpActive;
    private static readonly Image? FormHelpPressed;

    static MacOSPaletteSharedAssets()
    {
        var helpForward = new PaletteMicrosoft365White();
        FormHelpNormal = helpForward.GetButtonSpecImage(PaletteButtonSpecStyle.FormHelp, PaletteState.Normal);
        FormHelpDisabled = helpForward.GetButtonSpecImage(PaletteButtonSpecStyle.FormHelp, PaletteState.Disabled);
        FormHelpActive = helpForward.GetButtonSpecImage(PaletteButtonSpecStyle.FormHelp, PaletteState.Tracking);
        FormHelpPressed = helpForward.GetButtonSpecImage(PaletteButtonSpecStyle.FormHelp, PaletteState.Pressed);
    }

    internal static Image? GetFormButtonImage(PaletteButtonSpecStyle style, PaletteState state, bool isDarkSurface)
    {
        TrafficLightSet set = isDarkSurface ? Dark : Light;

        return style switch
        {
            PaletteButtonSpecStyle.FormClose => set.Get(MacOSFormButtonGlyphFactory.TrafficLightKind.Close, state),
            PaletteButtonSpecStyle.FormMin => set.Get(MacOSFormButtonGlyphFactory.TrafficLightKind.Minimize, state),
            PaletteButtonSpecStyle.FormMax => set.Get(MacOSFormButtonGlyphFactory.TrafficLightKind.Zoom, state),
            PaletteButtonSpecStyle.FormRestore => set.Get(MacOSFormButtonGlyphFactory.TrafficLightKind.Zoom, state),
            PaletteButtonSpecStyle.FormHelp => state switch
            {
                PaletteState.Tracking => FormHelpActive,
                PaletteState.Pressed => FormHelpPressed,
                PaletteState.Normal => FormHelpNormal,
                _ => FormHelpDisabled
            },
            _ => null
        };
    }

    private static TrafficLightSet CreateSet(bool isDarkSurface)
    {
        return new TrafficLightSet(isDarkSurface, FormButtonSize);
    }

    private sealed class TrafficLightSet
    {
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

        internal TrafficLightSet(bool isDarkSurface, Size size)
        {
            _closeNormal = MacOSFormButtonGlyphFactory.CreateTrafficLight(
                MacOSFormButtonGlyphFactory.TrafficLightKind.Close, PaletteState.Normal, size, isDarkSurface);
            _closeDisabled = MacOSFormButtonGlyphFactory.CreateTrafficLight(
                MacOSFormButtonGlyphFactory.TrafficLightKind.Close, PaletteState.Disabled, size, isDarkSurface);
            _closeActive = MacOSFormButtonGlyphFactory.CreateTrafficLight(
                MacOSFormButtonGlyphFactory.TrafficLightKind.Close, PaletteState.Tracking, size, isDarkSurface);
            _closePressed = MacOSFormButtonGlyphFactory.CreateTrafficLight(
                MacOSFormButtonGlyphFactory.TrafficLightKind.Close, PaletteState.Pressed, size, isDarkSurface);

            _minNormal = MacOSFormButtonGlyphFactory.CreateTrafficLight(
                MacOSFormButtonGlyphFactory.TrafficLightKind.Minimize, PaletteState.Normal, size, isDarkSurface);
            _minDisabled = MacOSFormButtonGlyphFactory.CreateTrafficLight(
                MacOSFormButtonGlyphFactory.TrafficLightKind.Minimize, PaletteState.Disabled, size, isDarkSurface);
            _minActive = MacOSFormButtonGlyphFactory.CreateTrafficLight(
                MacOSFormButtonGlyphFactory.TrafficLightKind.Minimize, PaletteState.Tracking, size, isDarkSurface);
            _minPressed = MacOSFormButtonGlyphFactory.CreateTrafficLight(
                MacOSFormButtonGlyphFactory.TrafficLightKind.Minimize, PaletteState.Pressed, size, isDarkSurface);

            _maxNormal = MacOSFormButtonGlyphFactory.CreateTrafficLight(
                MacOSFormButtonGlyphFactory.TrafficLightKind.Zoom, PaletteState.Normal, size, isDarkSurface);
            _maxDisabled = MacOSFormButtonGlyphFactory.CreateTrafficLight(
                MacOSFormButtonGlyphFactory.TrafficLightKind.Zoom, PaletteState.Disabled, size, isDarkSurface);
            _maxActive = MacOSFormButtonGlyphFactory.CreateTrafficLight(
                MacOSFormButtonGlyphFactory.TrafficLightKind.Zoom, PaletteState.Tracking, size, isDarkSurface);
            _maxPressed = MacOSFormButtonGlyphFactory.CreateTrafficLight(
                MacOSFormButtonGlyphFactory.TrafficLightKind.Zoom, PaletteState.Pressed, size, isDarkSurface);
        }

        internal Image? Get(MacOSFormButtonGlyphFactory.TrafficLightKind kind, PaletteState state) => kind switch
        {
            MacOSFormButtonGlyphFactory.TrafficLightKind.Close => state switch
            {
                PaletteState.Tracking => _closeActive,
                PaletteState.Normal => _closeNormal,
                PaletteState.Pressed => _closePressed,
                _ => _closeDisabled
            },
            MacOSFormButtonGlyphFactory.TrafficLightKind.Minimize => state switch
            {
                PaletteState.Normal => _minNormal,
                PaletteState.Tracking => _minActive,
                PaletteState.Pressed => _minPressed,
                _ => _minDisabled
            },
            MacOSFormButtonGlyphFactory.TrafficLightKind.Zoom => state switch
            {
                PaletteState.Normal => _maxNormal,
                PaletteState.Tracking => _maxActive,
                PaletteState.Pressed => _maxPressed,
                _ => _maxDisabled
            },
            _ => null
        };
    }
}
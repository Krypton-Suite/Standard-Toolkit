#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Themes;

/// <summary>Windows XP Zune accent colours mapped onto the Royale semantic palette surface.</summary>
internal static class WindowsXPZuneColors
{
    internal static readonly Color Accent = Color.FromArgb(230, 126, 34);
    internal static readonly Color AccentDark = Color.FromArgb(116, 68, 38);
    internal static readonly Color Surface = Color.FromArgb(238, 230, 218);
    internal static readonly Color SurfaceDark = Color.FromArgb(64, 52, 45);
    internal static readonly Color Text = Color.FromArgb(35, 31, 28);
    internal static readonly Color DisabledText = Color.FromArgb(150, 140, 130);

    /// <summary>Royale-compatible semantic palette for Zune.</summary>
    public static RoyalePalette Palette { get; } = new RoyalePalette("Zune",
        WindowsXPStyleColorFactory.CreateSemanticDictionary(
            edgeLight: Color.FromArgb(250, 245, 238),
            edgeHighlight: Color.White,
            edgeShadow: Color.FromArgb(180, 165, 150),
            edgeDarkShadow: SurfaceDark,
            edgeFill: Surface,
            text: Text,
            disabledText: DisabledText,
            buttonFace: Surface,
            buttonBorder: AccentDark,
            buttonTracking: Accent,
            buttonChecked: AccentDark,
            controlDisabled: Color.FromArgb(220, 210, 200),
            editFill: Color.White,
            controlBorder: Color.FromArgb(180, 165, 150),
            captionFill: AccentDark,
            groupBoxText: AccentDark,
            toolbarFill: Surface,
            toolbarText: Text,
            dialogFill: Surface,
            dialogBorder: Color.FromArgb(180, 165, 150)));
}

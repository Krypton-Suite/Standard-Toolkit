#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Themes;

/// <summary>Exact colour definitions extracted from Embedded.msstyles TEXTFILE resources.</summary>
internal static class EmbeddedColors
{
    internal static readonly Color EdgeLight = Color.FromArgb(220, 223, 228);
    internal static readonly Color EdgeHighlight = Color.FromArgb(245, 245, 245);
    internal static readonly Color EdgeShadow = Color.FromArgb(214, 213, 217);
    internal static readonly Color EdgeDarkShadow = Color.FromArgb(133, 135, 140);
    internal static readonly Color EdgeFill = Color.FromArgb(242, 244, 250);
    internal static readonly Color Background = Color.FromArgb(0, 0, 64);
    internal static readonly Color Control = Color.FromArgb(235, 233, 237);
    internal static readonly Color EditFill = Color.FromArgb(255, 255, 255);
    internal static readonly Color Text = Color.FromArgb(127, 124, 115);
    internal static readonly Color DisabledText = Color.FromArgb(181, 180, 184);
    internal static readonly Color ButtonFace = Color.FromArgb(235, 233, 237);
    internal static readonly Color ButtonTracking = Color.FromArgb(62, 157, 232);
    internal static readonly Color ButtonPressed = Color.FromArgb(62, 157, 232);
    internal static readonly Color CaptionFill = Color.FromArgb(62, 157, 232);
    internal static readonly Color CaptionText = Color.FromArgb(143, 182, 227);
    internal static readonly Color InactiveCaption = Color.FromArgb(111, 161, 217);
    internal static readonly Color Highlight = Color.FromArgb(62, 157, 232);
    internal static readonly Color HighlightText = Color.FromArgb(255, 255, 255);
    internal static readonly Color Menu = Color.FromArgb(255, 255, 255);
    internal static readonly Color MenuBar = Color.FromArgb(235, 233, 237);

    /// <summary>All RGB definitions keyed by source INI and property name.</summary>
    internal static readonly IReadOnlyDictionary<string, Color> Definitions = new Dictionary<string, Color>
    {
        ["EXTRALARGEDEFAULT_INI:EdgeLightColor"] = Color.FromArgb(220, 223, 228),
        ["EXTRALARGEDEFAULT_INI:EdgeHighLightColor"] = Color.FromArgb(245, 245, 245),
        ["EXTRALARGEDEFAULT_INI:EdgeShadowColor"] = Color.FromArgb(214, 213, 217),
        ["EXTRALARGEDEFAULT_INI:EdgeDkShadowColor"] = Color.FromArgb(133, 135, 140),
        ["EXTRALARGEDEFAULT_INI:EdgeFillColor"] = Color.FromArgb(242, 244, 250),
        ["EXTRALARGEDEFAULT_INI:Window"] = Color.FromArgb(255, 255, 255),
        ["EXTRALARGEDEFAULT_INI:MenuBar"] = Color.FromArgb(235, 233, 237),
        ["EXTRALARGEDEFAULT_INI:Menu"] = Color.FromArgb(255, 255, 255),
        ["EXTRALARGEDEFAULT_INI:Background"] = Color.FromArgb(0, 0, 64),
        ["EXTRALARGEDEFAULT_INI:Btnface"] = Color.FromArgb(235, 233, 237),
        ["EXTRALARGEDEFAULT_INI:Highlight"] = Color.FromArgb(62, 157, 232),
        ["EXTRALARGEDEFAULT_INI:ActiveCaption"] = Color.FromArgb(62, 157, 232),
        ["EXTRALARGEDEFAULT_INI:CaptionText"] = Color.FromArgb(143, 182, 227),
        ["EXTRALARGEDEFAULT_INI:InactiveCaption"] = Color.FromArgb(111, 161, 217),
        ["EXTRALARGEDEFAULT_INI:InactiveCaptionText"] = Color.FromArgb(255, 255, 255),
        ["EXTRALARGEDEFAULT_INI:GradientActiveCaption"] = Color.FromArgb(112, 177, 235),
        ["EXTRALARGEDEFAULT_INI:GradientInactiveCaption"] = Color.FromArgb(131, 183, 227),
        ["EXTRALARGEDEFAULT_INI:HighlightText"] = Color.FromArgb(255, 255, 255),
        ["EXTRALARGEDEFAULT_INI:MenuHilight"] = Color.FromArgb(62, 157, 232),
        ["EXTRALARGEDEFAULT_INI:BtnShadow"] = Color.FromArgb(181, 180, 184),
        ["EXTRALARGEDEFAULT_INI:GrayText"] = Color.FromArgb(181, 180, 184),
        ["EXTRALARGEDEFAULT_INI:BtnHighlight"] = Color.FromArgb(255, 255, 255),
        ["EXTRALARGEDEFAULT_INI:DkShadow3d"] = Color.FromArgb(133, 135, 140),
        ["EXTRALARGEDEFAULT_INI:Light3d"] = Color.FromArgb(220, 223, 228),
        ["EXTRALARGEDEFAULT_INI:TextColor"] = Color.FromArgb(127, 124, 115),
        ["EXTRALARGEDEFAULT_INI:FillColorHint"] = Color.FromArgb(202, 198, 175),
        ["EXTRALARGEDEFAULT_INI:BorderColorHint"] = Color.FromArgb(168, 191, 234),
        ["EXTRALARGEDEFAULT_INI:AccentColorHint"] = Color.FromArgb(255, 121, 95),
        ["EXTRALARGEDEFAULT_INI:FillColor"] = Color.FromArgb(2, 72, 178),
        ["EXTRALARGEDEFAULT_INI:BorderColor"] = Color.FromArgb(255, 255, 255),
        ["EXTRALARGEDEFAULT_INI:TransparentColor"] = Color.FromArgb(255, 0, 0),
        ["EXTRALARGEDEFAULT_INI:transparentcolor"] = Color.FromArgb(242, 241, 231),
        ["EXTRALARGEDEFAULT_INI:fillColor"] = Color.FromArgb(235, 235, 228),
        ["EXTRALARGEDEFAULT_INI:borderColor"] = Color.FromArgb(235, 235, 228),
        ["EXTRALARGEDEFAULT_INI:color"] = Color.FromArgb(181, 180, 184),
        ["EXTRALARGEDEFAULT_INI:TextShadowColor"] = Color.FromArgb(10, 24, 131),
        ["EXTRALARGEDEFAULT_INI:textshadowcolor"] = Color.FromArgb(69, 76, 16),
        ["EXTRALARGEDEFAULT_INI:HotTracking"] = Color.FromArgb(255, 255, 255),
        ["EXTRALARGEDEFAULT_INI:FillcolorHint"] = Color.FromArgb(254, 234, 182),
        ["EXTRALARGEDEFAULT_INI:GradientColor1"] = Color.FromArgb(97, 174, 232),
        ["EXTRALARGEDEFAULT_INI:GradientColor2"] = Color.FromArgb(84, 145, 191),
        ["LARGEFONTSDEFAULT_INI:EdgeLightColor"] = Color.FromArgb(220, 223, 228),
        ["LARGEFONTSDEFAULT_INI:EdgeHighLightColor"] = Color.FromArgb(245, 245, 245),
        ["LARGEFONTSDEFAULT_INI:EdgeShadowColor"] = Color.FromArgb(214, 213, 217),
        ["LARGEFONTSDEFAULT_INI:EdgeDkShadowColor"] = Color.FromArgb(133, 135, 140),
        ["LARGEFONTSDEFAULT_INI:EdgeFillColor"] = Color.FromArgb(242, 244, 250),
        ["LARGEFONTSDEFAULT_INI:Window"] = Color.FromArgb(255, 255, 255),
        ["LARGEFONTSDEFAULT_INI:MenuBar"] = Color.FromArgb(235, 233, 237),
        ["LARGEFONTSDEFAULT_INI:Menu"] = Color.FromArgb(255, 255, 255),
        ["LARGEFONTSDEFAULT_INI:Background"] = Color.FromArgb(0, 0, 64),
        ["LARGEFONTSDEFAULT_INI:Btnface"] = Color.FromArgb(235, 233, 237),
        ["LARGEFONTSDEFAULT_INI:Highlight"] = Color.FromArgb(62, 157, 232),
        ["LARGEFONTSDEFAULT_INI:ActiveCaption"] = Color.FromArgb(62, 157, 232),
        ["LARGEFONTSDEFAULT_INI:CaptionText"] = Color.FromArgb(143, 182, 227),
        ["LARGEFONTSDEFAULT_INI:InactiveCaption"] = Color.FromArgb(111, 161, 217),
        ["LARGEFONTSDEFAULT_INI:InactiveCaptionText"] = Color.FromArgb(255, 255, 255),
        ["LARGEFONTSDEFAULT_INI:GradientActiveCaption"] = Color.FromArgb(112, 177, 235),
        ["LARGEFONTSDEFAULT_INI:GradientInactiveCaption"] = Color.FromArgb(131, 183, 227),
        ["LARGEFONTSDEFAULT_INI:HighlightText"] = Color.FromArgb(255, 255, 255),
        ["LARGEFONTSDEFAULT_INI:MenuHilight"] = Color.FromArgb(62, 157, 232),
        ["LARGEFONTSDEFAULT_INI:BtnShadow"] = Color.FromArgb(181, 180, 184),
        ["LARGEFONTSDEFAULT_INI:GrayText"] = Color.FromArgb(181, 180, 184),
        ["LARGEFONTSDEFAULT_INI:BtnHighlight"] = Color.FromArgb(255, 255, 255),
        ["LARGEFONTSDEFAULT_INI:DkShadow3d"] = Color.FromArgb(133, 135, 140),
        ["LARGEFONTSDEFAULT_INI:Light3d"] = Color.FromArgb(220, 223, 228),
        ["LARGEFONTSDEFAULT_INI:TextColor"] = Color.FromArgb(127, 124, 115),
        ["LARGEFONTSDEFAULT_INI:FillColorHint"] = Color.FromArgb(202, 198, 175),
        ["LARGEFONTSDEFAULT_INI:BorderColorHint"] = Color.FromArgb(168, 191, 234),
        ["LARGEFONTSDEFAULT_INI:AccentColorHint"] = Color.FromArgb(255, 121, 95),
        ["LARGEFONTSDEFAULT_INI:FillColor"] = Color.FromArgb(2, 72, 178),
        ["LARGEFONTSDEFAULT_INI:BorderColor"] = Color.FromArgb(255, 255, 255),
        ["LARGEFONTSDEFAULT_INI:TransparentColor"] = Color.FromArgb(255, 0, 0),
        ["LARGEFONTSDEFAULT_INI:transparentcolor"] = Color.FromArgb(242, 241, 231),
        ["LARGEFONTSDEFAULT_INI:fillColor"] = Color.FromArgb(235, 235, 228),
        ["LARGEFONTSDEFAULT_INI:borderColor"] = Color.FromArgb(235, 235, 228),
        ["LARGEFONTSDEFAULT_INI:color"] = Color.FromArgb(181, 180, 184),
        ["LARGEFONTSDEFAULT_INI:TextShadowColor"] = Color.FromArgb(10, 24, 131),
        ["LARGEFONTSDEFAULT_INI:textshadowcolor"] = Color.FromArgb(69, 76, 16),
        ["LARGEFONTSDEFAULT_INI:HotTracking"] = Color.FromArgb(255, 255, 255),
        ["LARGEFONTSDEFAULT_INI:FillcolorHint"] = Color.FromArgb(254, 234, 182),
        ["LARGEFONTSDEFAULT_INI:GradientColor1"] = Color.FromArgb(97, 174, 232),
        ["LARGEFONTSDEFAULT_INI:GradientColor2"] = Color.FromArgb(84, 145, 191),
        ["NORMALDEFAULT_INI:EdgeLightColor"] = Color.FromArgb(220, 223, 228),
        ["NORMALDEFAULT_INI:EdgeHighLightColor"] = Color.FromArgb(245, 245, 245),
        ["NORMALDEFAULT_INI:EdgeShadowColor"] = Color.FromArgb(214, 213, 217),
        ["NORMALDEFAULT_INI:EdgeDkShadowColor"] = Color.FromArgb(133, 135, 140),
        ["NORMALDEFAULT_INI:EdgeFillColor"] = Color.FromArgb(242, 244, 250),
        ["NORMALDEFAULT_INI:Window"] = Color.FromArgb(255, 255, 255),
        ["NORMALDEFAULT_INI:MenuBar"] = Color.FromArgb(235, 233, 237),
        ["NORMALDEFAULT_INI:Menu"] = Color.FromArgb(255, 255, 255),
        ["NORMALDEFAULT_INI:Background"] = Color.FromArgb(0, 0, 64),
        ["NORMALDEFAULT_INI:Btnface"] = Color.FromArgb(235, 233, 237),
        ["NORMALDEFAULT_INI:Highlight"] = Color.FromArgb(62, 157, 232),
        ["NORMALDEFAULT_INI:ActiveCaption"] = Color.FromArgb(62, 157, 232),
        ["NORMALDEFAULT_INI:CaptionText"] = Color.FromArgb(143, 182, 227),
        ["NORMALDEFAULT_INI:InactiveCaption"] = Color.FromArgb(111, 161, 217),
        ["NORMALDEFAULT_INI:InactiveCaptionText"] = Color.FromArgb(255, 255, 255),
        ["NORMALDEFAULT_INI:GradientActiveCaption"] = Color.FromArgb(112, 177, 235),
        ["NORMALDEFAULT_INI:GradientInactiveCaption"] = Color.FromArgb(131, 183, 227),
        ["NORMALDEFAULT_INI:HighlightText"] = Color.FromArgb(255, 255, 255),
        ["NORMALDEFAULT_INI:MenuHilight"] = Color.FromArgb(62, 157, 232),
        ["NORMALDEFAULT_INI:BtnShadow"] = Color.FromArgb(181, 180, 184),
        ["NORMALDEFAULT_INI:GrayText"] = Color.FromArgb(181, 180, 184),
        ["NORMALDEFAULT_INI:BtnHighlight"] = Color.FromArgb(255, 255, 255),
        ["NORMALDEFAULT_INI:DkShadow3d"] = Color.FromArgb(133, 135, 140),
        ["NORMALDEFAULT_INI:Light3d"] = Color.FromArgb(220, 223, 228),
        ["NORMALDEFAULT_INI:TextColor"] = Color.FromArgb(127, 124, 115),
        ["NORMALDEFAULT_INI:FillColorHint"] = Color.FromArgb(202, 198, 175),
        ["NORMALDEFAULT_INI:BorderColorHint"] = Color.FromArgb(168, 191, 234),
        ["NORMALDEFAULT_INI:AccentColorHint"] = Color.FromArgb(255, 121, 95),
        ["NORMALDEFAULT_INI:FillColor"] = Color.FromArgb(2, 72, 178),
        ["NORMALDEFAULT_INI:BorderColor"] = Color.FromArgb(255, 255, 255),
        ["NORMALDEFAULT_INI:TransparentColor"] = Color.FromArgb(255, 0, 0),
        ["NORMALDEFAULT_INI:transparentcolor"] = Color.FromArgb(242, 241, 231),
        ["NORMALDEFAULT_INI:fillColor"] = Color.FromArgb(235, 235, 228),
        ["NORMALDEFAULT_INI:borderColor"] = Color.FromArgb(235, 235, 228),
        ["NORMALDEFAULT_INI:color"] = Color.FromArgb(181, 180, 184),
        ["NORMALDEFAULT_INI:TextShadowColor"] = Color.FromArgb(10, 24, 131),
        ["NORMALDEFAULT_INI:textshadowcolor"] = Color.FromArgb(69, 76, 16),
        ["NORMALDEFAULT_INI:HotTracking"] = Color.FromArgb(255, 255, 255),
        ["NORMALDEFAULT_INI:FillcolorHint"] = Color.FromArgb(254, 234, 182),
        ["NORMALDEFAULT_INI:GradientColor1"] = Color.FromArgb(97, 174, 232),
        ["NORMALDEFAULT_INI:GradientColor2"] = Color.FromArgb(84, 145, 191),
    };

    /// <summary>Luna-compatible semantic palette built from Embedded colours.</summary>
    public static LunaPalette Palette { get; } = new LunaPalette("Embedded",
        WindowsXPStyleColorFactory.CreateSemanticDictionary(
            edgeLight: EdgeLight,
            edgeHighlight: EdgeHighlight,
            edgeShadow: EdgeShadow,
            edgeDarkShadow: EdgeDarkShadow,
            edgeFill: EdgeFill,
            text: Text,
            disabledText: DisabledText,
            buttonFace: ButtonFace,
            buttonBorder: Color.FromArgb(0, 60, 116),
            buttonTracking: ButtonTracking,
            buttonChecked: ButtonPressed,
            controlDisabled: Color.FromArgb(214, 213, 217),
            editFill: EditFill,
            controlBorder: EdgeShadow,
            captionFill: CaptionFill,
            groupBoxText: Highlight,
            toolbarFill: MenuBar,
            toolbarText: Text,
            dialogFill: Control,
            dialogBorder: EdgeShadow));
}

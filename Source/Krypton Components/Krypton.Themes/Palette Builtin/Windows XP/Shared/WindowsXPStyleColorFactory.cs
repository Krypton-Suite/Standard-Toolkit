#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Themes;

/// <summary>
/// Builds Luna/Royale semantic colour dictionaries for XP sibling themes that only expose a compact colour set.
/// </summary>
internal static class WindowsXPStyleColorFactory
{
    internal static Dictionary<string, Color> CreateSemanticDictionary(
        Color edgeLight,
        Color edgeHighlight,
        Color edgeShadow,
        Color edgeDarkShadow,
        Color edgeFill,
        Color text,
        Color disabledText,
        Color buttonFace,
        Color buttonBorder,
        Color buttonTracking,
        Color buttonChecked,
        Color controlDisabled,
        Color editFill,
        Color controlBorder,
        Color captionFill,
        Color groupBoxText,
        Color toolbarFill,
        Color toolbarText,
        Color dialogFill,
        Color dialogBorder)
    {
        return new Dictionary<string, Color>(StringComparer.OrdinalIgnoreCase)
        {
            ["globals.EdgeLightColor"] = edgeLight,
            ["globals.EdgeHighLightColor"] = edgeHighlight,
            ["globals.EdgeShadowColor"] = edgeShadow,
            ["globals.EdgeDkShadowColor"] = edgeDarkShadow,
            ["globals.EdgeFillColor"] = edgeFill,
            ["button.pushbutton.TextColor"] = text,
            ["button.pushbutton(disabled).TextColor"] = disabledText,
            ["button.pushbutton.FillColorHint"] = buttonFace,
            ["button.pushbutton.BorderColorHint"] = buttonBorder,
            ["button.pushbutton.AccentColorHint"] = buttonTracking,
            ["button.pushbutton(defaulted).AccentColorHint"] = buttonChecked,
            ["button.groupbox.TextColor"] = groupBoxText,
            ["edit.edittext(Disabled).FillColor"] = controlDisabled,
            ["edit.FillColor"] = editFill,
            ["edit.BorderColor"] = controlBorder,
            ["Listbox.BorderColor"] = controlBorder,
            ["Listbox.FillColor"] = editFill,
            ["ListView.BorderColor"] = controlBorder,
            ["ListView.FillColor"] = editFill,
            ["TreeView.BorderColor"] = controlBorder,
            ["TreeView.FillColor"] = editFill,
            ["Toolbar.FillColor"] = toolbarFill,
            ["Toolbar.TextColor"] = toolbarText,
            ["Window.dialog.FillColor"] = dialogFill,
            ["Window.dialog.BorderColor"] = dialogBorder,
            ["Window.TextShadowColor"] = edgeShadow,
            ["ExplorerBar.NormalGroupBackground.FillColor"] = edgeFill,
            ["ExplorerBar.NormalGroupBackground.BorderColor"] = edgeShadow,
            ["ExplorerBar.NormalGroupBackground.TextColor"] = text,
            ["ExplorerBar.SpecialGroupHead.TextColor"] = Color.White,
            ["ExplorerBar.SpecialGroupHead.FillColor"] = captionFill,
            ["Window.Caption.FillColorHint"] = captionFill
        };
    }
}

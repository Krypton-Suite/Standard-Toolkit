#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Menu and tool-strip colours for Luna, taken from the palette instead of the host Professional table.
/// </summary>
internal sealed class KryptonWindowsXPLunaColorTable : KryptonColorTable
{
    internal KryptonWindowsXPLunaColorTable(PaletteBase palette)
        : base(palette)
    {
    }

    /// <summary>Gets the Luna menu-selection text colour.</summary>
    internal Color HighlightText =>
        Palette.GetContentShortTextColor1(PaletteContentStyle.ButtonListItem, PaletteState.Tracking);

    /// <inheritdoc />
    public override Color MenuItemSelected =>
        Palette.GetBackColor1(PaletteBackStyle.ContextMenuItemHighlight, PaletteState.Tracking);

    /// <inheritdoc />
    public override Color MenuItemSelectedGradientBegin => MenuItemSelected;

    /// <inheritdoc />
    public override Color MenuItemSelectedGradientEnd => MenuItemSelected;

    /// <inheritdoc />
    public override Color MenuItemPressedGradientBegin => MenuStripGradientBegin;

    /// <inheritdoc />
    public override Color MenuItemPressedGradientMiddle => MenuStripGradientBegin;

    /// <inheritdoc />
    public override Color MenuItemPressedGradientEnd => MenuStripGradientEnd;

    /// <inheritdoc />
    public override Color MenuItemBorder => MenuItemSelected;

    /// <inheritdoc />
    public override Color MenuStripGradientBegin =>
        Palette.GetBackColor1(PaletteBackStyle.PanelClient, PaletteState.Normal);

    /// <inheritdoc />
    public override Color MenuStripGradientEnd => MenuStripGradientBegin;

    /// <inheritdoc />
    public override Color ToolStripGradientBegin =>
        Palette.GetBackColor1(PaletteBackStyle.PanelClient, PaletteState.Normal);

    /// <inheritdoc />
    public override Color ToolStripGradientMiddle => ToolStripGradientBegin;

    /// <inheritdoc />
    public override Color ToolStripGradientEnd => ToolStripGradientBegin;

    /// <inheritdoc />
    public override Color ToolStripBorder =>
        Palette.GetBorderColor1(PaletteBorderStyle.ControlGroupBox, PaletteState.Normal);

    /// <inheritdoc />
    public override Color SeparatorDark => ToolStripBorder;

    /// <inheritdoc />
    public override Color SeparatorLight =>
        Palette.GetBorderColor2(PaletteBorderStyle.ButtonStandalone, PaletteState.Normal);

    /// <inheritdoc />
    public override Color MenuItemText =>
        Palette.GetContentShortTextColor1(PaletteContentStyle.LabelNormalPanel, PaletteState.Normal);

    /// <inheritdoc />
    public override Color MenuStripText => MenuItemText;
}

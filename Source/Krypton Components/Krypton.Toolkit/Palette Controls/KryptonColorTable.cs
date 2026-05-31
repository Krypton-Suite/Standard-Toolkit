#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Extend the ProfessionalColorTable with some Krypton specific properties.
/// </summary>
public class KryptonColorTable : ProfessionalColorTable
{
    #region Identity
    /// <summary>
    /// Creates a new instance of the KryptonColorTable class.
    /// </summary>
    /// <param name="palette">Reference to associated palette.</param>
    public KryptonColorTable(PaletteBase palette) => Palette = palette;

    #endregion

    #region Palette
    /// <summary>
    /// Gets the associated palette instance.
    /// </summary>
    public PaletteBase Palette { get; }

    #endregion

    #region RoundedEdges
    /// <summary>
    /// Gets a value indicating if rounded edges are required.
    /// </summary>
    public virtual InheritBool UseRoundedEdges => InheritBool.True;

    #endregion

    #region MenuItemText
    /// <summary>
    /// Gets the text color used on the menu items.
    /// </summary>
    public virtual Color MenuItemText => SystemColors.MenuText;

    #endregion

    #region MenuStripText
    /// <summary>
    /// Gets the text color used on the menu strip.
    /// </summary>
    public virtual Color MenuStripText => SystemColors.MenuText;

    #endregion

    #region ToolStripText
    /// <summary>
    /// Gets the text color used on the tool strip.
    /// </summary>
    public virtual Color ToolStripText => SystemColors.MenuText;

    #endregion

    #region StatusStripText
    /// <summary>
    /// Gets the text color used on the status strip.
    /// </summary>
    public virtual Color StatusStripText => SystemColors.MenuText;

    #endregion

    #region ContextMenu
    /// <summary>
    /// Gets the palette colour used for context menu backgrounds.
    /// </summary>
    public Color ContextMenuBack => GetBackColor(PaletteBackStyle.ContextMenuInner, PaletteState.Normal, ToolStripDropDownBackground);

    /// <summary>
    /// Gets the palette colour used for context menu borders.
    /// </summary>
    public Color ContextMenuBorder => GetBorderColor(PaletteBorderStyle.ContextMenuOuter, PaletteState.Normal, MenuBorder);

    /// <summary>
    /// Gets the palette colour used for context menu image columns.
    /// </summary>
    public Color ContextMenuImageColumnBack => GetBackColor(PaletteBackStyle.ContextMenuItemImageColumn, PaletteState.Normal, ImageMarginGradientBegin);

    /// <summary>
    /// Gets the palette colour used for context menu image column borders.
    /// </summary>
    public Color ContextMenuImageColumnBorder => GetBorderColor(PaletteBorderStyle.ContextMenuItemImageColumn, PaletteState.Normal, ImageMarginGradientMiddle);

    /// <summary>
    /// Gets the palette colour used for checked context menu item image backgrounds.
    /// </summary>
    public Color ContextMenuItemImageBack => GetBackColor(PaletteBackStyle.ContextMenuItemImage, PaletteState.CheckedNormal, CheckBackground);

    /// <summary>
    /// Gets the palette colour used for checked context menu item image borders.
    /// </summary>
    public Color ContextMenuItemImageBorder => GetBorderColor(PaletteBorderStyle.ContextMenuItemImage, PaletteState.CheckedNormal, CommonHelper.WhitenColor(ContextMenuItemImageBack, 1.05f, 1.52f, 2.75f));

    /// <summary>
    /// Gets the palette colour used for checked context menu item glyphs.
    /// </summary>
    public Color ContextMenuItemImageText => GetContentColor(PaletteContentStyle.ContextMenuItemImage, PaletteState.CheckedNormal, CommonHelper.WhitenColor(ContextMenuItemImageBack, 3.86f, 3.02f, 1.07f));

    /// <summary>
    /// Gets the palette colour used for context menu item text.
    /// </summary>
    public Color ContextMenuItemText => GetContentColor(PaletteContentStyle.ContextMenuItemTextStandard, PaletteState.Normal, MenuItemText);

    /// <summary>
    /// Gets the palette colour used for disabled context menu item text.
    /// </summary>
    public Color ContextMenuItemDisabledText => GetContentColor(PaletteContentStyle.ContextMenuItemTextStandard, PaletteState.Disabled, SystemColors.GrayText);

    /// <summary>
    /// Gets the first palette colour used for highlighted context menu item backgrounds.
    /// </summary>
    public Color ContextMenuItemHighlightBack1 => GetBackColor(PaletteBackStyle.ContextMenuItemHighlight, PaletteState.Tracking, MenuItemSelectedGradientBegin);

    /// <summary>
    /// Gets the second palette colour used for highlighted context menu item backgrounds.
    /// </summary>
    public Color ContextMenuItemHighlightBack2 => GetBackColor2(PaletteBackStyle.ContextMenuItemHighlight, PaletteState.Tracking, ContextMenuItemHighlightBack1);

    /// <summary>
    /// Gets the palette colour used for highlighted context menu item borders.
    /// </summary>
    public Color ContextMenuItemHighlightBorder => GetBorderColor(PaletteBorderStyle.ContextMenuItemHighlight, PaletteState.Tracking, MenuItemBorder);

    #endregion

    #region MenuStripFont
    /// <summary>
    /// Gets the font used on the menu strip.
    /// </summary>
    public virtual Font MenuStripFont => SystemInformation.MenuFont;

    #endregion

    #region ToolStripFont
    /// <summary>
    /// Gets the font used on the tool strip.
    /// </summary>
    public virtual Font ToolStripFont => SystemInformation.MenuFont;

    #endregion

    #region StatusStripFont
    /// <summary>
    /// Gets the font used on the status strip.
    /// </summary>
    public virtual Font StatusStripFont => SystemInformation.MenuFont;

    #endregion

    #region Implementation
    private Color GetBackColor(PaletteBackStyle style, PaletteState state, Color fallback) =>
        ResolveColor(Palette.GetBackColor1(style, state), fallback);

    private Color GetBackColor2(PaletteBackStyle style, PaletteState state, Color fallback) =>
        ResolveColor(Palette.GetBackColor2(style, state), fallback);

    private Color GetBorderColor(PaletteBorderStyle style, PaletteState state, Color fallback) =>
        ResolveColor(Palette.GetBorderColor1(style, state), fallback);

    private Color GetContentColor(PaletteContentStyle style, PaletteState state, Color fallback) =>
        ResolveColor(Palette.GetContentShortTextColor1(style, state), fallback);

    private static Color ResolveColor(Color color, Color fallback) =>
        (color == GlobalStaticValues.EMPTY_COLOR) || color.IsEmpty ? fallback : color;

    #endregion
}

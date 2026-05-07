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
}
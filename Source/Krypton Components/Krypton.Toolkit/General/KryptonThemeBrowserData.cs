#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2024 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>A structure that contains basic information for <see cref="VisualThemeBrowserForm"/>.</summary>
public struct KryptonThemeBrowserData
{
    #region Public

    /// <summary>Gets or sets the show import button.</summary>
    /// <value>The show import button.</value>
    public bool? ShowImportButton { get; set; }

    /// <summary>Gets or sets the show silent option.</summary>
    /// <value>The show silent option.</value>
    public bool? ShowSilentOption { get; set; }

    /// <summary>Gets or sets the start position.</summary>
    /// <value>The start position.</value>
    public FormStartPosition? StartPosition { get; set; }

    /// <summary>Gets or sets the start index.</summary>
    /// <value>The start index.</value>
    /// <remarks>
    /// Prefer <see cref="DefaultPalette"/> when selecting by theme. When both are set,
    /// <see cref="DefaultPalette"/> takes precedence.
    /// </remarks>
    public int? StartIndex { get; set; }

    /// <summary>Gets or sets the default palette mode to select when the theme browser opens.</summary>
    /// <value>The default <see cref="PaletteMode"/>, or <c>null</c> to fall back to <see cref="StartIndex"/>.</value>
    /// <remarks>
    /// When set to <see cref="PaletteMode.Global"/>, the browser selects the current
    /// <see cref="KryptonManager.GlobalPaletteMode"/> when that mode is a concrete theme.
    /// </remarks>
    public PaletteMode? DefaultPalette { get; set; }

    /// <summary>Gets or sets the window title.</summary>
    /// <value>The window title.</value>
    public string? WindowTitle { get; set; }

    /// <summary>Gets or sets the use RTL layout of the <see cref="KryptonThemeBrowser"/> UI.</summary>
    /// <value>The use RTL layout in an <see cref="KryptonThemeBrowser"/>.</value>
    public KryptonUseRTLLayout UseRtlLayout { get; set; }

    /// <summary>
    /// Gets or sets whether extra (non-core) catalogued palettes appear in the browser list.
    /// </summary>
    /// <value><see langword="null"/> or <see langword="true"/> lists extras when they are discovered.</value>
    public bool? ShowExtraThemes { get; set; }

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="KryptonThemeBrowserData" /> struct.</summary>
    public KryptonThemeBrowserData()
    {
        UseRtlLayout = KryptonUseRTLLayout.No;
    }

    #endregion
}
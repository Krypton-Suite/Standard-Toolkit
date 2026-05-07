#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
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
    public int? StartIndex { get; set; }

    /// <summary>Gets or sets the window title.</summary>
    /// <value>The window title.</value>
    public string? WindowTitle { get; set; }

    // ToDo: Add default palette mode option in V100

    /// <summary>Gets or sets the use RTL layout of the <see cref="KryptonThemeBrowser"/> UI.</summary>
    /// <value>The use RTL layout in an <see cref="KryptonThemeBrowser"/>.</value>
    public KryptonUseRTLLayout UseRtlLayout { get; set; }

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="KryptonThemeBrowserData" /> struct.</summary>
    public KryptonThemeBrowserData()
    {
        UseRtlLayout = KryptonUseRTLLayout.No;
    }

    #endregion
}
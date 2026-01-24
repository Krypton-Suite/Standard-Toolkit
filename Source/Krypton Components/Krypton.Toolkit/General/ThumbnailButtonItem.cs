#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Represents a button shown in the taskbar thumbnail toolbar (e.g. play, pause, next).
/// </summary>
public class ThumbnailButtonItem
{
    #region Public

    /// <summary>
    /// Gets or sets the application-defined button ID. Must be unique within the toolbar.
    /// This ID is passed to the ThumbnailButtonClick event when the button is clicked.
    /// </summary>
    [CLSCompliant(false)]
    public uint Id { get; set; }

    /// <summary>
    /// Gets or sets the icon displayed on the button. Should be 32-bit, typically 16x16 or system icon size.
    /// </summary>
    public Icon? Icon { get; set; }

    /// <summary>
    /// Gets or sets the tooltip text shown when the mouse hovers over the button.
    /// </summary>
    public string Tooltip { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets whether the button is enabled. Default is true.
    /// </summary>
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// Gets or sets whether the button is hidden. Default is false.
    /// </summary>
    public bool Hidden { get; set; }

    #endregion
}

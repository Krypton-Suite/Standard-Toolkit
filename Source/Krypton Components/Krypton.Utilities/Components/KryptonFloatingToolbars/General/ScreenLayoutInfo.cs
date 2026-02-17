#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

/// <summary>
/// Contains layout information for a screen.
/// </summary>
public class ScreenLayoutInfo
{
    /// <summary>
    /// Gets or sets the device name of the screen.
    /// </summary>
    public string DeviceName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the bounds of the screen.
    /// </summary>
    public Rectangle Bounds { get; set; }

    /// <summary>
    /// Gets or sets the working area of the screen (excluding taskbar).
    /// </summary>
    public Rectangle WorkingArea { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this is the primary screen.
    /// </summary>
    public bool IsPrimary { get; set; }

    /// <summary>
    /// Gets or sets the bits per pixel of the screen.
    /// </summary>
    public int BitsPerPixel { get; set; }
}
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
/// Specifies the visual style of a floating window.
/// </summary>
public enum FloatingWindowStyle
{
    /// <summary>
    /// Default style with standard window chrome.
    /// </summary>
    Default = 0,

    /// <summary>
    /// Minimal style with no border or title bar.
    /// </summary>
    Minimal = 1,

    /// <summary>
    /// Tool window style with smaller title bar.
    /// </summary>
    ToolWindow = 2,

    /// <summary>
    /// Custom style allowing full customization.
    /// </summary>
    Custom = 3
}

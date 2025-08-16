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
/// Defines how UAC shield icons should handle theme changes.
/// </summary>
public enum ShieldIconThemeMode
{
    /// <summary>
    /// Automatically detect and adapt to the current theme.
    /// </summary>
    Automatic = 0,

    /// <summary>
    /// Force light theme appearance regardless of system theme.
    /// </summary>
    Light = 1,

    /// <summary>
    /// Force dark theme appearance regardless of system theme.
    /// </summary>
    Dark = 2,

    /// <summary>
    /// Use the system's default theme behavior.
    /// </summary>
    System = 3
}

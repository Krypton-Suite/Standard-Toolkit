#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Utilities;

/// <summary>
/// Extension methods for integrating Font Awesome icons with Krypton Docking components.
/// Note: Docking components use KryptonPage instances for pages, so use NavigatorExtensions 
/// (SetFontAwesomeIcons on KryptonPage) for setting icons on docking pages.
/// </summary>
public static class DockingExtensions
{
    // Docking components use KryptonPage instances directly, so icon support
    // is provided through NavigatorExtensions.SetFontAwesomeIcons().
    // This class is provided for future extensibility if Docking-specific
    // icon properties are added.
}
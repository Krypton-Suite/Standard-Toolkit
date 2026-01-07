#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Marker class for the permanent "Close" button in the navigation list.
/// </summary>
internal class BackstageCloseItem
{
    /// <summary>
    /// Gets the display text for the Close button.
    /// </summary>
    public string Text => KryptonManager.Strings.GeneralStrings.Close;
}

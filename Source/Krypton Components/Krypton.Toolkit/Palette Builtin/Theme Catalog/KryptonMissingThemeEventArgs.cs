#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Data for <see cref="KryptonThemeCatalog.MissingThemeFallback"/> when an extra palette is not loaded.
/// </summary>
public sealed class KryptonMissingThemeEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonMissingThemeEventArgs"/> class.
    /// </summary>
    /// <param name="requestedMode">The extra <see cref="PaletteMode"/> that had no implementation.</param>
    /// <param name="fallbackMode">The core mode used instead.</param>
    public KryptonMissingThemeEventArgs(PaletteMode requestedMode, PaletteMode fallbackMode)
    {
        RequestedMode = requestedMode;
        FallbackMode = fallbackMode;
    }

    /// <summary>
    /// Gets the extra mode the application asked for.
    /// </summary>
    public PaletteMode RequestedMode { get; }

    /// <summary>
    /// Gets the core mode used to paint (Microsoft 365 Blue).
    /// </summary>
    public PaletteMode FallbackMode { get; }
}

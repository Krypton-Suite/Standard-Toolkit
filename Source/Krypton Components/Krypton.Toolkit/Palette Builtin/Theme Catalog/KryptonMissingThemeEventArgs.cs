#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
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
        : this(requestedMode, fallbackMode, null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonMissingThemeEventArgs"/> class with a descriptive explanation.
    /// </summary>
    /// <param name="requestedMode">The extra <see cref="PaletteMode"/> that had no implementation.</param>
    /// <param name="fallbackMode">The core mode used instead.</param>
    /// <param name="reason">The descriptive explanation of why the theme reverted.</param>
    public KryptonMissingThemeEventArgs(PaletteMode requestedMode, PaletteMode fallbackMode, string? reason)
    {
        RequestedMode = requestedMode;
        FallbackMode = fallbackMode;
        Reason = reason ?? $"The requested theme '{requestedMode}' requires the 'Krypton.Themes' assembly ('Krypton.Themes.dll'), which is not loaded or could not be found. The theme has reverted to '{fallbackMode}'.";
    }

    /// <summary>
    /// Gets the extra mode the application asked for.
    /// </summary>
    public PaletteMode RequestedMode { get; }

    /// <summary>
    /// Gets the core mode used to paint (Microsoft 365 Blue).
    /// </summary>
    public PaletteMode FallbackMode { get; }

    /// <summary>
    /// Gets the explanation of why the theme reverted to the fallback palette.
    /// </summary>
    public string Reason { get; }

    /// <summary>
    /// Gets or sets whether the missing theme fallback has been handled by the subscriber.
    /// When <see langword="true"/>, the default warning dialog is suppressed.
    /// </summary>
    public bool Handled { get; set; }
}

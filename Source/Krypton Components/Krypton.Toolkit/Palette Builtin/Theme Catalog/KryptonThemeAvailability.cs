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
/// Lets the application hide builtin themes from selectors without removing <see cref="PaletteMode"/> values.
/// </summary>
/// <remarks>
/// Disabled themes remain usable from code via <see cref="KryptonManager.GlobalPaletteMode"/> and
/// <see cref="KryptonManager.GetPaletteForMode"/>. Selectors consult <see cref="IsSelectable"/>.
/// </remarks>
public static class KryptonThemeAvailability
{
    private static readonly object _sync = new object();
    private static readonly HashSet<PaletteMode> _disabledModes = new HashSet<PaletteMode>();
    private static readonly HashSet<string> _disabledFamilies = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
    private static readonly HashSet<string> _disabledExtraFamilies = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
    private static bool _allowCustomThemes = true;

    /// <summary>
    /// Occurs when enablement changes. Theme selectors should rebuild.
    /// </summary>
    public static event EventHandler? AvailabilityChanged;

    /// <summary>
    /// Gets or sets whether registered XML/custom themes appear in selectors. Defaults to <see langword="true"/>.
    /// </summary>
    public static bool AllowCustomThemes
    {
        get => _allowCustomThemes;
        set
        {
            if (_allowCustomThemes == value)
            {
                return;
            }

            _allowCustomThemes = value;
            OnChanged();
        }
    }

    /// <summary>
    /// Enables or disables a single builtin mode in theme selectors.
    /// </summary>
    /// <param name="mode">Palette mode.</param>
    /// <param name="enabled"><see langword="false"/> hides the mode from selectors.</param>
    public static void SetEnabled(PaletteMode mode, bool enabled)
    {
        if (mode == PaletteMode.Global || mode == PaletteMode.Custom)
        {
            return;
        }

        lock (_sync)
        {
            if (enabled)
            {
                _disabledModes.Remove(mode);
            }
            else
            {
                _disabledModes.Add(mode);
            }
        }

        OnChanged();
    }

    /// <summary>
    /// Enables or disables every catalogued theme in <paramref name="family"/>.
    /// </summary>
    /// <param name="family">Family key from <see cref="KryptonThemeFamilies"/>.</param>
    /// <param name="enabled"><see langword="false"/> hides the family from selectors.</param>
    public static void SetFamilyEnabled(string family, bool enabled) => SetFamilyEnabled(family, enabled, extraOnly: false);

    /// <summary>
    /// Enables or disables themes in <paramref name="family"/>.
    /// </summary>
    /// <param name="family">Family key from <see cref="KryptonThemeFamilies"/>.</param>
    /// <param name="enabled"><see langword="false"/> hides matching themes from selectors.</param>
    /// <param name="extraOnly">When <see langword="true"/>, core palettes in the family stay listed.</param>
    public static void SetFamilyEnabled(string family, bool enabled, bool extraOnly)
    {
        if (string.IsNullOrWhiteSpace(family))
        {
            return;
        }

        lock (_sync)
        {
            var target = extraOnly ? _disabledExtraFamilies : _disabledFamilies;
            if (enabled)
            {
                target.Remove(family);
            }
            else
            {
                target.Add(family);
            }
        }

        OnChanged();
    }

    /// <summary>
    /// Returns whether selectors should list <paramref name="mode"/>.
    /// </summary>
    /// <param name="mode">Palette mode.</param>
    /// <returns><see langword="true"/> when the implementation is available and not disabled.</returns>
    public static bool IsSelectable(PaletteMode mode)
    {
        if (mode == PaletteMode.Custom)
        {
            return _allowCustomThemes;
        }

        if (mode == PaletteMode.Global)
        {
            return false;
        }

        if (!KryptonThemeCatalog.IsImplementationAvailable(mode))
        {
            return false;
        }

        lock (_sync)
        {
            if (_disabledModes.Contains(mode))
            {
                return false;
            }

            var family = KryptonThemeCatalog.GetFamily(mode);
            if (family != null && _disabledFamilies.Contains(family))
            {
                return false;
            }

            if (family != null
                && _disabledExtraFamilies.Contains(family)
                && !KryptonThemeCatalog.IsCoreMode(mode))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Clears all per-mode and per-family disables and restores custom-theme listing.
    /// </summary>
    public static void Reset()
    {
        lock (_sync)
        {
            _disabledModes.Clear();
            _disabledFamilies.Clear();
            _disabledExtraFamilies.Clear();
            _allowCustomThemes = true;
        }

        OnChanged();
    }

    private static void OnChanged()
    {
        AvailabilityChanged?.Invoke(null, EventArgs.Empty);
        ThemeManager.NotifyThemeListChanged();
    }
}

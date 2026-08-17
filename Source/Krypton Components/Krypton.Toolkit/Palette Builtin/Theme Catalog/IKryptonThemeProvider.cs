#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Supplies builtin palette implementations for registration with <see cref="KryptonThemeCatalog"/>.
/// </summary>
/// <remarks>
/// The core provider lives in <c>Krypton.Toolkit</c>. Additional palettes are supplied by
/// <c>Krypton.Themes</c> via <see cref="KryptonThemeProviderAttribute"/> and
/// <see cref="KryptonManager.AutoDiscoverThemes"/>. Third-party assemblies can use the same
/// attribute. Extra providers cannot add new <see cref="PaletteMode"/> values; they can only
/// implement modes that are not already registered. Pass
/// <see cref="KryptonThemeDescriptor.Family"/> and <see cref="KryptonThemeDescriptor.ChromeKind"/>
/// explicitly; the five-argument constructor guesses both from the mode name.
/// </remarks>
public interface IKryptonThemeProvider
{
    /// <summary>
    /// Gets the palettes this provider can construct.
    /// </summary>
    /// <returns>Descriptors; must not be <see langword="null"/>.</returns>
    IReadOnlyList<KryptonThemeDescriptor> GetThemes();
}

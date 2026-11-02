#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026. All rights reserved.
 *
 */
#endregion

using System;
using System.Collections.Generic;
using Krypton.Toolkit;

[assembly: KryptonThemeProvider(typeof(ThemeProviderSample.SampleThemeProvider))]

namespace ThemeProviderSample;

/// <summary>
/// Example extra-assembly provider. Modes already registered by Toolkit or Themes are skipped.
/// </summary>
public sealed class SampleThemeProvider : IKryptonThemeProvider
{
    /// <inheritdoc />
    /// <remarks>
    /// Return descriptors with explicit family and <see cref="KryptonThemeChromeKind"/>.
    /// Existing Toolkit/Themes modes are skipped. New <see cref="PaletteMode"/> values cannot be invented here.
    /// </remarks>
    public IReadOnlyList<KryptonThemeDescriptor> GetThemes() => Array.Empty<KryptonThemeDescriptor>();
}

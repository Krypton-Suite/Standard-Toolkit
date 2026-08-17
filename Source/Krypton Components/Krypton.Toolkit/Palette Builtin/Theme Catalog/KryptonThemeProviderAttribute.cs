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
/// Marks an assembly as supplying extra builtin palettes for <see cref="KryptonManager.AutoDiscoverThemes"/>.
/// </summary>
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
public sealed class KryptonThemeProviderAttribute : Attribute
{
    /// <summary>
    /// Initializes the attribute.
    /// </summary>
    /// <param name="providerType">A type that implements <see cref="IKryptonThemeProvider"/> and has a public parameterless constructor.</param>
    public KryptonThemeProviderAttribute(Type providerType) =>
        ProviderType = providerType ?? throw new ArgumentNullException(nameof(providerType));

    /// <summary>
    /// Gets the provider type.
    /// </summary>
    public Type ProviderType { get; }
}

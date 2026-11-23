#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Supplies update packages to <see cref="KryptonApplicationUpdater"/>.
/// Assign an implementation to <see cref="KryptonApplicationUpdater.Provider"/> to replace the built-in JSON and GitHub feeds.
/// </summary>
public interface IKryptonUpdateProvider
{
    /// <summary>Contacts the feed and returns every package it describes.</summary>
    /// <param name="cancellationToken">Cancels the request.</param>
    /// <returns>The catalog. The component keeps only the newest version newer than the current one.</returns>
    /// <exception cref="KryptonUpdateException">The feed could not be read or did not match the manifest contract.</exception>
    Task<KryptonUpdateCatalog> CheckAsync(CancellationToken cancellationToken);
}

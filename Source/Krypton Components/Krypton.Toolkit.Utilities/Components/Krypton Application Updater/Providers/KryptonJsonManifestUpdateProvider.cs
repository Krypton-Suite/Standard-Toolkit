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
/// Reads a JSON update manifest from an HTTPS address or a local file.
/// A local <c>packageUrl</c> is allowed so an offline feed can point at a local installer or zip.
/// An <c>http</c> address is rejected.
/// </summary>
public sealed class KryptonJsonManifestUpdateProvider : IKryptonUpdateProvider
{
    private readonly string _feedUrl;

    /// <summary>Initializes the provider.</summary>
    /// <param name="feedUrl">HTTPS manifest URL, <c>file://</c> URL, or local path.</param>
    public KryptonJsonManifestUpdateProvider(string feedUrl) =>
        _feedUrl = feedUrl ?? string.Empty;

    /// <inheritdoc />
    public async Task<KryptonUpdateCatalog> CheckAsync(CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(_feedUrl))
        {
            KryptonUpdateThrowHelper.Throw("Set FeedUrl to an https manifest or a local manifest file.");
        }

        var json = await KryptonUpdateClient.ReadTextAsync(_feedUrl, null, null, cancellationToken).ConfigureAwait(false);
        return KryptonUpdateManifestParser.Parse(json);
    }
}

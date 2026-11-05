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
/// Reads the latest GitHub release.
/// A release asset named <c>krypton-update.json</c> is the manifest (package addresses may be asset names on that release).
/// Otherwise one <c>.exe</c>, <c>.msi</c>, or <c>.zip</c> asset is used, and its <c>digest</c> must be <c>sha256:</c>.
/// Nothing is applied when neither hash is present.
/// </summary>
public sealed class KryptonGitHubReleaseUpdateProvider : IKryptonUpdateProvider
{
    private readonly string _owner;
    private readonly string _repository;
    private readonly string? _assetName;
    private readonly KryptonUpdateSeverity _defaultSeverity;
    private readonly string? _token;

    /// <summary>Initializes the provider.</summary>
    /// <param name="owner">Repository owner.</param>
    /// <param name="repository">Repository name.</param>
    /// <param name="assetName">Exact asset name when the release has no <c>krypton-update.json</c>. Null uses the single package asset.</param>
    /// <param name="defaultSeverity">Severity used when the release itself is mapped to a package.</param>
    /// <param name="token">Optional token for a private repository. Not written by the designer.</param>
    public KryptonGitHubReleaseUpdateProvider(string owner, string repository, string? assetName, KryptonUpdateSeverity defaultSeverity, string? token)
    {
        _owner = owner ?? string.Empty;
        _repository = repository ?? string.Empty;
        _assetName = null;
        if (assetName != null && !string.IsNullOrWhiteSpace(assetName))
        {
            _assetName = assetName.Trim();
        }
        _defaultSeverity = defaultSeverity;
        _token = string.IsNullOrWhiteSpace(token) ? null : token;
    }

    /// <inheritdoc />
    public async Task<KryptonUpdateCatalog> CheckAsync(CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(_owner) || string.IsNullOrWhiteSpace(_repository))
        {
            throw new KryptonUpdateException("Set GitHubOwner and GitHubRepository.");
        }

        var api = "https://api.github.com/repos/" + Uri.EscapeDataString(_owner.Trim()) + "/" + Uri.EscapeDataString(_repository.Trim()) + "/releases/latest";
        string json;
        try
        {
            json = await KryptonUpdateClient.ReadTextAsync(api, _token, "application/vnd.github+json", cancellationToken).ConfigureAwait(false);
        }
        catch (KryptonUpdateException exception) when (exception.Message.Contains("404"))
        {
            throw new KryptonUpdateException("No published GitHub release was found.", exception);
        }

        var release = GitHubRelease.Parse(json);
        var manifest = FindAsset(release.Assets, "krypton-update.json");
        if (manifest != null)
        {
            var manifestJson = await KryptonUpdateClient.ReadTextAsync(manifest.DownloadUrl, _token, null, cancellationToken).ConfigureAwait(false);
            var catalog = KryptonUpdateManifestParser.Parse(manifestJson);
            return new KryptonUpdateCatalog(ResolveManifestPackages(catalog.Packages, release.Assets));
        }

        var asset = SelectPackageAsset(release.Assets);
        var hash = HashFromDigest(asset.Digest);
        if (hash == null || string.IsNullOrWhiteSpace(hash))
        {
            throw new KryptonUpdateException("The release asset has no SHA-256 digest and the release has no krypton-update.json manifest.");
        }

        if (!KryptonUpdateVersion.TryParse(release.TagName, out var version))
        {
            throw new KryptonUpdateException("The release tag is not a System.Version value.");
        }

        var kind = asset.Name.EndsWith(".zip", StringComparison.OrdinalIgnoreCase)
            ? KryptonUpdatePackageKind.Archive
            : KryptonUpdatePackageKind.Installer;
        var package = new KryptonUpdatePackage(version, _defaultSeverity, kind, asset.DownloadUrl, hash, null, release.Body, null, release.PublishedUtc);
        return new KryptonUpdateCatalog(new[] { package });
    }

    private static List<KryptonUpdatePackage> ResolveManifestPackages(IReadOnlyList<KryptonUpdatePackage> packages, List<GitHubAsset> assets)
    {
        var resolved = new List<KryptonUpdatePackage>(packages.Count);
        foreach (var package in packages)
        {
            if (KryptonUpdateClient.IsHttps(package.PackageUrl) || KryptonUpdateClient.TryGetLocalPath(package.PackageUrl, out _))
            {
                resolved.Add(package);
                continue;
            }

            var asset = FindAsset(assets, package.PackageUrl);
            if (asset == null)
            {
                throw new KryptonUpdateException("The manifest packageUrl '" + package.PackageUrl + "' is not an https address or a release asset name.");
            }

            var hash = string.IsNullOrWhiteSpace(package.Sha256) ? HashFromDigest(asset.Digest) : package.Sha256;
            if (hash == null || string.IsNullOrWhiteSpace(hash))
            {
                throw new KryptonUpdateException("The manifest package is missing sha256.");
            }

            resolved.Add(new KryptonUpdatePackage(
                package.Version,
                package.Severity,
                package.PackageKind,
                asset.DownloadUrl,
                hash,
                package.CertificateThumbprint,
                package.ReleaseNotes,
                package.InstallerArguments,
                package.ReleasedUtc,
                package.MaximumPostponeDays == KryptonUpdatePackage.DefaultMaximumPostponeDays ? (int?)null : package.MaximumPostponeDays,
                package.AllowPostponeSetting,
                package.AllowSkipSetting,
                package.AllowResumeSetting,
                package.AllowRetrySetting,
                package.Banner,
                package.Pulse));
        }

        return resolved;
    }

    private GitHubAsset SelectPackageAsset(List<GitHubAsset> assets)
    {
        if (_assetName != null)
        {
            var named = FindAsset(assets, _assetName);
            if (named == null)
            {
                throw new KryptonUpdateException("The release does not contain asset '" + _assetName + "'.");
            }

            return named;
        }

        GitHubAsset? match = null;
        foreach (var asset in assets)
        {
            if (!IsPackageFile(asset.Name))
            {
                continue;
            }

            if (match != null)
            {
                throw new KryptonUpdateException("The release has more than one installer or zip. Set GitHubAssetName.");
            }

            match = asset;
        }

        if (match == null)
        {
            throw new KryptonUpdateException("The release has no .exe, .msi, or .zip asset.");
        }

        return match;
    }

    private static GitHubAsset? FindAsset(List<GitHubAsset> assets, string name)
    {
        foreach (var asset in assets)
        {
            if (string.Equals(asset.Name, name, StringComparison.OrdinalIgnoreCase))
            {
                return asset;
            }
        }

        return null;
    }

    private static bool IsPackageFile(string name) =>
        name.EndsWith(".exe", StringComparison.OrdinalIgnoreCase)
        || name.EndsWith(".msi", StringComparison.OrdinalIgnoreCase)
        || name.EndsWith(".zip", StringComparison.OrdinalIgnoreCase);

    private static string? HashFromDigest(string? digest)
    {
        if (digest == null || string.IsNullOrWhiteSpace(digest))
        {
            return null;
        }

        const string prefix = "sha256:";
        if (!digest.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }

        return digest.Substring(prefix.Length).Trim();
    }

    private sealed class GitHubAsset
    {
        public GitHubAsset(string name, string downloadUrl, string? digest)
        {
            Name = name;
            DownloadUrl = downloadUrl;
            Digest = digest;
        }

        public string Name { get; }

        public string DownloadUrl { get; }

        public string? Digest { get; }
    }

    private sealed class GitHubRelease
    {
        public string? TagName { get; set; }

        public string? Body { get; set; }

        public DateTime? PublishedUtc { get; set; }

        public List<GitHubAsset> Assets { get; } = new List<GitHubAsset>();

        public static GitHubRelease Parse(string json)
        {
#if NET8_0_OR_GREATER
            return ParseDocument(json);
#else
            return ParseSerializer(json);
#endif
        }

#if NET8_0_OR_GREATER
        private static GitHubRelease ParseDocument(string json)
        {
            using var document = JsonDocument.Parse(json);
            var root = document.RootElement;
            var release = new GitHubRelease
            {
                TagName = root.TryGetProperty("tag_name", out var tag) ? tag.GetString() : null,
                Body = root.TryGetProperty("body", out var body) && body.ValueKind == JsonValueKind.String ? body.GetString() : null
            };
            if (root.TryGetProperty("published_at", out var published) && published.ValueKind == JsonValueKind.String
                && DateTime.TryParse(published.GetString(), CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var publishedAt))
            {
                release.PublishedUtc = publishedAt.ToUniversalTime();
            }

            if (root.TryGetProperty("assets", out var assets) && assets.ValueKind == JsonValueKind.Array)
            {
                foreach (var asset in assets.EnumerateArray())
                {
                    var name = asset.TryGetProperty("name", out var nameElement) ? nameElement.GetString() : null;
                    var url = asset.TryGetProperty("browser_download_url", out var urlElement) ? urlElement.GetString() : null;
                    var digest = asset.TryGetProperty("digest", out var digestElement) && digestElement.ValueKind == JsonValueKind.String
                        ? digestElement.GetString()
                        : null;
                    if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(url))
                    {
                        release.Assets.Add(new GitHubAsset(name, url, digest));
                    }
                }
            }

            return release;
        }
#else
        private static GitHubRelease ParseSerializer(string json)
        {
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer { MaxJsonLength = int.MaxValue };
            var data = serializer.DeserializeObject(json) as Dictionary<string, object>;
            if (data == null)
            {
                throw new KryptonUpdateException("The GitHub release response was not a JSON object.");
            }

            var release = new GitHubRelease
            {
                TagName = GetString(data, "tag_name"),
                Body = GetString(data, "body")
            };
            var published = GetString(data, "published_at");
            if (!string.IsNullOrWhiteSpace(published)
                && DateTime.TryParse(published, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var publishedAt))
            {
                release.PublishedUtc = publishedAt.ToUniversalTime();
            }

            if (data.TryGetValue("assets", out var assets))
            {
                foreach (var item in Enumerate(assets))
                {
                    var name = GetString(item, "name");
                    var url = GetString(item, "browser_download_url");
                    if (name != null && url != null && !string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(url))
                    {
                        release.Assets.Add(new GitHubAsset(name, url, GetString(item, "digest")));
                    }
                }
            }

            return release;
        }

        private static string? GetString(Dictionary<string, object> data, string name)
        {
            if (!data.TryGetValue(name, out var value) || value == null)
            {
                return null;
            }

            return Convert.ToString(value, CultureInfo.InvariantCulture);
        }

        private static List<Dictionary<string, object>> Enumerate(object value)
        {
            var list = new List<Dictionary<string, object>>();
            if (value is object[] array)
            {
                foreach (var item in array)
                {
                    if (item is Dictionary<string, object> dictionary)
                    {
                        list.Add(dictionary);
                    }
                }
            }
            else if (value is ArrayList arrayList)
            {
                foreach (var item in arrayList)
                {
                    if (item is Dictionary<string, object> dictionary)
                    {
                        list.Add(dictionary);
                    }
                }
            }

            return list;
        }
#endif
    }
}

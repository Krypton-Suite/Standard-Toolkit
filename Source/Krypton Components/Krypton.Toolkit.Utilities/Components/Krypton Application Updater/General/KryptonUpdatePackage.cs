#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>One downloadable update described by a feed.</summary>
public sealed class KryptonUpdatePackage
{
    /// <summary>Day limit used when the manifest omits <c>maximumPostponeDays</c>.</summary>
    public const int DefaultMaximumPostponeDays = 365;

    private readonly bool? _allowPostpone;
    private readonly bool? _allowSkip;
    private readonly bool? _allowResume;
    private readonly bool? _allowRetry;

    /// <summary>Initializes a package. <paramref name="sha256"/> is required before the package can be applied.</summary>
    /// <param name="version">Package version. Compared with <see cref="Version"/> ordering.</param>
    /// <param name="severity">How the prompt and automatic mode treat the package.</param>
    /// <param name="packageKind">Installer or zip archive.</param>
    /// <param name="packageUrl">HTTPS address or a local file path.</param>
    /// <param name="sha256">64 hex characters. Compared in constant time after download.</param>
    /// <param name="certificateThumbprint">When set, the file must be Authenticode-signed with this thumbprint.</param>
    /// <param name="releaseNotes">Text shown in the prompt. May be null.</param>
    /// <param name="installerArguments">Arguments passed to an installer. Ignored for a zip.</param>
    /// <param name="releasedUtc">Published time from the feed, when present.</param>
    /// <param name="maximumPostponeDays">Latest remind-later date, in days from today. Null uses <see cref="DefaultMaximumPostponeDays"/>.</param>
    /// <param name="allowPostpone">When null, a critical package cannot be postponed and every other severity can. When set, that choice wins.</param>
    /// <param name="allowSkip">When null, a critical package cannot be skipped and every other severity can. When set, that choice wins.</param>
    /// <param name="allowResume">When null, a partial download can be resumed. When false, Resume is hidden.</param>
    /// <param name="allowRetry">When null, a failed download can be started again. When false, Restart is hidden.</param>
    /// <param name="banner">Optional prompt and notes banner. Null when the manifest omits it.</param>
    /// <param name="pulse">Optional form chrome glow. Null when the manifest omits it.</param>
    public KryptonUpdatePackage(
        Version version,
        KryptonUpdateSeverity severity,
        KryptonUpdatePackageKind packageKind,
        string packageUrl,
        string sha256,
        string? certificateThumbprint,
        string? releaseNotes,
        string? installerArguments,
        DateTime? releasedUtc,
        int? maximumPostponeDays = null,
        bool? allowPostpone = null,
        bool? allowSkip = null,
        bool? allowResume = null,
        bool? allowRetry = null,
        KryptonUpdateBanner? banner = null,
        KryptonUpdatePulse? pulse = null)
    {
        ThrowHelper.ThrowIfNull(version);
        if (maximumPostponeDays.HasValue && maximumPostponeDays.Value < 1)
        {
            ThrowHelper.ThrowArgumentOutOfRangeException(nameof(maximumPostponeDays), maximumPostponeDays.Value, "maximumPostponeDays must be at least 1.");
        }

        Version = version;
        Severity = severity;
        PackageKind = packageKind;
        PackageUrl = packageUrl ?? string.Empty;
        Sha256 = sha256 ?? string.Empty;
        CertificateThumbprint = null;
        if (certificateThumbprint != null && !string.IsNullOrWhiteSpace(certificateThumbprint))
        {
            CertificateThumbprint = certificateThumbprint.Trim();
        }
        ReleaseNotes = releaseNotes;
        InstallerArguments = installerArguments;
        ReleasedUtc = releasedUtc;
        MaximumPostponeDays = maximumPostponeDays ?? DefaultMaximumPostponeDays;
        _allowPostpone = allowPostpone;
        _allowSkip = allowSkip;
        _allowResume = allowResume;
        _allowRetry = allowRetry;
        Banner = banner;
        Pulse = pulse;
    }

    /// <summary>Gets the package version.</summary>
    public Version Version { get; }

    /// <summary>Gets how strongly the update is recommended.</summary>
    public KryptonUpdateSeverity Severity { get; }

    /// <summary>Gets whether the file is an installer or a zip archive.</summary>
    public KryptonUpdatePackageKind PackageKind { get; }

    /// <summary>Gets the HTTPS address or local path of the file.</summary>
    public string PackageUrl { get; }

    /// <summary>Gets the expected SHA-256 hash as hex.</summary>
    public string Sha256 { get; }

    /// <summary>Gets the optional Authenticode thumbprint.</summary>
    public string? CertificateThumbprint { get; }

    /// <summary>Gets the release notes, or null when the feed did not supply any.</summary>
    public string? ReleaseNotes { get; }

    /// <summary>Gets installer arguments, or null.</summary>
    public string? InstallerArguments { get; }

    /// <summary>Gets the published time in UTC, or null.</summary>
    public DateTime? ReleasedUtc { get; }

    /// <summary>Gets a value indicating that the severity is <see cref="KryptonUpdateSeverity.Critical"/>.</summary>
    public bool IsMandatory => Severity == KryptonUpdateSeverity.Critical;

    /// <summary>Gets the latest remind-later date as a number of days from today.</summary>
    public int MaximumPostponeDays { get; }

    /// <summary>
    /// Gets whether Remind later is offered.
    /// A critical package is false unless the manifest sets <c>allowPostpone</c> to true.
    /// </summary>
    public bool AllowsPostpone => _allowPostpone ?? Severity != KryptonUpdateSeverity.Critical;

    /// <summary>
    /// Gets whether Skip this version is offered.
    /// A critical package is false unless the manifest sets <c>allowSkip</c> to true.
    /// </summary>
    public bool AllowsSkip => _allowSkip ?? Severity != KryptonUpdateSeverity.Critical;

    /// <summary>Gets whether a partial download offers Resume. Omitted manifest values allow it.</summary>
    public bool AllowsResume => _allowResume ?? true;

    /// <summary>Gets whether a failed download offers Restart. Omitted manifest values allow it.</summary>
    public bool AllowsRetry => _allowRetry ?? true;

    /// <summary>Gets the manifest <c>allowPostpone</c> value, or null when the file omitted it.</summary>
    internal bool? AllowPostponeSetting => _allowPostpone;

    /// <summary>Gets the manifest <c>allowSkip</c> value, or null when the file omitted it.</summary>
    internal bool? AllowSkipSetting => _allowSkip;

    /// <summary>Gets the manifest <c>allowResume</c> value, or null when the file omitted it.</summary>
    internal bool? AllowResumeSetting => _allowResume;

    /// <summary>Gets the manifest <c>allowRetry</c> value, or null when the file omitted it.</summary>
    internal bool? AllowRetrySetting => _allowRetry;

    /// <summary>Gets the optional prompt and notes banner, or null when the manifest omits it.</summary>
    public KryptonUpdateBanner? Banner { get; }

    /// <summary>Gets the optional form chrome glow, or null when the manifest omits it.</summary>
    public KryptonUpdatePulse? Pulse { get; }

    /// <summary>Returns a copy that downloads from <paramref name="packageUrl"/>.</summary>
    /// <param name="packageUrl">Resolved HTTPS address or local path.</param>
    /// <returns>A package with the same metadata and the new address.</returns>
    public KryptonUpdatePackage WithPackageUrl(string packageUrl) =>
        new KryptonUpdatePackage(
            Version,
            Severity,
            PackageKind,
            packageUrl,
            Sha256,
            CertificateThumbprint,
            ReleaseNotes,
            InstallerArguments,
            ReleasedUtc,
            MaximumPostponeDays == DefaultMaximumPostponeDays ? (int?)null : MaximumPostponeDays,
            _allowPostpone,
            _allowSkip,
            _allowResume,
            _allowRetry,
            Banner,
            Pulse);
}

/// <summary>Packages returned by one feed check.</summary>
public sealed class KryptonUpdateCatalog
{
    /// <summary>Initializes a catalog.</summary>
    /// <param name="packages">Packages from the feed. Null is treated as an empty list.</param>
    public KryptonUpdateCatalog(IReadOnlyList<KryptonUpdatePackage>? packages) =>
        Packages = packages ?? Array.Empty<KryptonUpdatePackage>();

    /// <summary>Gets the packages in feed order.</summary>
    public IReadOnlyList<KryptonUpdatePackage> Packages { get; }

    /// <summary>Selects the newest package whose version is greater than <paramref name="current"/>.</summary>
    /// <param name="current">Version installed now.</param>
    /// <returns>The newest newer package, or null when the feed is not newer.</returns>
    public KryptonUpdatePackage? SelectNewerThan(Version current)
    {
        ThrowHelper.ThrowIfNull(current);

        KryptonUpdatePackage? best = null;
        foreach (var package in Packages)
        {
            if (package.Version <= current)
            {
                continue;
            }

            if (best == null || package.Version > best.Version)
            {
                best = package;
            }
        }

        return best;
    }

    /// <summary>Selects every package whose version is greater than <paramref name="current"/>, highest version first.</summary>
    /// <param name="current">Version installed now.</param>
    /// <returns>The newer packages. Equal versions keep their feed order.</returns>
    public IReadOnlyList<KryptonUpdatePackage> SelectAllNewerThan(Version current)
    {
        ThrowHelper.ThrowIfNull(current);
        return OrderByVersion(Packages, current);
    }

    /// <summary>Returns <paramref name="packages"/> highest version first. Equal versions keep their original order.</summary>
    /// <param name="packages">Feed packages.</param>
    /// <param name="newerThan">When set, packages at or below this version are left out.</param>
    /// <returns>The ordered packages.</returns>
    internal static IReadOnlyList<KryptonUpdatePackage> OrderByVersion(IReadOnlyList<KryptonUpdatePackage> packages, Version? newerThan)
    {
        var indexed = new List<KeyValuePair<int, KryptonUpdatePackage>>();
        for (var index = 0; index < packages.Count; index++)
        {
            var package = packages[index];
            if (newerThan != null && package.Version <= newerThan)
            {
                continue;
            }

            indexed.Add(new KeyValuePair<int, KryptonUpdatePackage>(index, package));
        }

        indexed.Sort(CompareNewestFirst);
        var ordered = new List<KryptonUpdatePackage>(indexed.Count);
        foreach (var entry in indexed)
        {
            ordered.Add(entry.Value);
        }

        return ordered;
    }

    private static int CompareNewestFirst(KeyValuePair<int, KryptonUpdatePackage> left, KeyValuePair<int, KryptonUpdatePackage> right)
    {
        var byVersion = right.Value.Version.CompareTo(left.Value.Version);
        return byVersion != 0 ? byVersion : left.Key.CompareTo(right.Key);
    }
}

/// <summary>Result of a check, including a failure that was reported through <see cref="KryptonApplicationUpdater.Failed"/>.</summary>
public sealed class KryptonUpdateCheckResult
{
    private KryptonUpdateCheckResult(KryptonUpdateCheckStatus status, KryptonUpdatePackage? package, Exception? error)
    {
        Status = status;
        Package = package;
        Error = error;
    }

    /// <summary>Gets the outcome.</summary>
    public KryptonUpdateCheckStatus Status { get; }

    /// <summary>Gets the package the check selected, when there was one.</summary>
    public KryptonUpdatePackage? Package { get; }

    /// <summary>Gets the exception when <see cref="Status"/> is <see cref="KryptonUpdateCheckStatus.Failed"/>.</summary>
    public Exception? Error { get; }

    /// <summary>Creates a result that is not a failure.</summary>
    /// <param name="status">Outcome other than <see cref="KryptonUpdateCheckStatus.Failed"/>.</param>
    /// <param name="package">Package involved, or null.</param>
    /// <returns>The result.</returns>
    public static KryptonUpdateCheckResult Create(KryptonUpdateCheckStatus status, KryptonUpdatePackage? package = null) =>
        new KryptonUpdateCheckResult(status, package, null);

    /// <summary>Creates a failed result.</summary>
    /// <param name="error">Exception already raised through <see cref="KryptonApplicationUpdater.Failed"/> by the component.</param>
    /// <returns>The result.</returns>
    public static KryptonUpdateCheckResult Failure(Exception error) =>
        new KryptonUpdateCheckResult(KryptonUpdateCheckStatus.Failed, null, error);
}

/// <summary>Parses feed versions. A leading <c>v</c> is ignored. Prerelease labels are not part of <see cref="Version"/>.</summary>
public static class KryptonUpdateVersion
{
    /// <summary>Parses <paramref name="text"/> into a <see cref="Version"/>.</summary>
    /// <param name="text">Version text such as <c>1.2.3</c> or <c>v1.2.3</c>.</param>
    /// <param name="version">The parsed version when the method returns true.</param>
    /// <returns>True when <paramref name="text"/> is a <see cref="Version"/>.</returns>
    public static bool TryParse(string? text, out Version version)
    {
        version = new Version(0, 0, 0, 0);
        if (text == null || string.IsNullOrWhiteSpace(text))
        {
            return false;
        }

        text = text.Trim();
        if (text.Length > 1 && (text[0] == 'v' || text[0] == 'V'))
        {
            text = text.Substring(1);
        }

        if (!Version.TryParse(text, out Version? parsed) || parsed == null)
        {
            return false;
        }

        version = parsed;
        return true;
    }
}

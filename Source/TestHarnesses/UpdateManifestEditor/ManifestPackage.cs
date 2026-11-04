#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace UpdateManifestEditor
{
    /// <summary>One package row in the manifest editor. <see cref="PackageFile"/> is not written to the JSON.</summary>
    internal sealed class ManifestPackage
    {
        public string Version { get; set; } = string.Empty;

        public string Severity { get; set; } = "recommended";

        public string PackageKind { get; set; } = "installer";

        public string PackageFile { get; set; } = string.Empty;

        public string PackageUrl { get; set; } = string.Empty;

        public string Sha256 { get; set; } = string.Empty;

        public string CertificateThumbprint { get; set; } = string.Empty;

        public string ReleaseNotes { get; set; } = string.Empty;

        public string InstallerArguments { get; set; } = string.Empty;

        public string ReleasedUtc { get; set; } = string.Empty;

        /// <summary>Empty omits <c>maximumPostponeDays</c> and the updater uses 365.</summary>
        public string MaximumPostponeDays { get; set; } = string.Empty;

        /// <summary>Empty omits <c>allowPostpone</c>. <c>true</c> or <c>false</c> is written as a JSON boolean.</summary>
        public string AllowPostpone { get; set; } = string.Empty;

        /// <summary>Empty omits <c>allowSkip</c>.</summary>
        public string AllowSkip { get; set; } = string.Empty;

        /// <summary>Empty omits <c>allowResume</c>.</summary>
        public string AllowResume { get; set; } = string.Empty;

        /// <summary>Empty omits <c>allowRetry</c>. False hides Restart.</summary>
        public string AllowRetry { get; set; } = string.Empty;

        public override string ToString()
        {
            var version = string.IsNullOrWhiteSpace(Version) ? "(new package)" : Version.Trim();
            var kind = string.IsNullOrWhiteSpace(PackageKind) ? "installer" : PackageKind.Trim();
            return version + "  " + kind;
        }
    }
}

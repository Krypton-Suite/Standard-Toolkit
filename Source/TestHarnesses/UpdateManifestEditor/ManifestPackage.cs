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

        public string BannerText { get; set; } = string.Empty;

        public string BannerBackColor { get; set; } = string.Empty;

        public string BannerBackColor2 { get; set; } = string.Empty;

        public string BannerForeColor { get; set; } = string.Empty;

        public string BannerImageUrl { get; set; } = string.Empty;

        /// <summary>Empty omits <c>pulse</c>. <c>true</c> or <c>false</c> writes enable.</summary>
        public string PulseEnable { get; set; } = string.Empty;

        public string PulseColor { get; set; } = string.Empty;

        /// <summary>Empty defaults to All when pulse is written.</summary>
        public string PulseStyle { get; set; } = string.Empty;

        public string PulseSpeed { get; set; } = string.Empty;

        /// <summary>Empty omits <c>progressDisplay</c>. Values: <c>progressBar</c>, <c>transferSpeedGraph</c>.</summary>
        public string ProgressDisplay { get; set; } = string.Empty;

        public override string ToString()
        {
            var version = string.IsNullOrWhiteSpace(Version) ? "(new package)" : Version.Trim();
            var kind = string.IsNullOrWhiteSpace(PackageKind) ? "installer" : PackageKind.Trim();
            return version + "  " + kind;
        }
    }
}

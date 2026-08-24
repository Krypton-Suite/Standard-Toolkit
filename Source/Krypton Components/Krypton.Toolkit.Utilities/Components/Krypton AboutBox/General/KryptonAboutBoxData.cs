#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2023 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Identity and chrome for <see cref="KryptonAboutBox"/>.
/// Empty members are filled from <see cref="CurrentAssembly"/> attributes and file version info.
/// </summary>
public struct KryptonAboutBoxData
{
    #region Public

    /// <summary>Gets or sets whether the Toolkit Information page is shown.</summary>
    public bool? ShowToolkitInformation { get; set; }

    /// <summary>
    /// Gets or sets whether the System Information button is shown.
    /// When null, <see cref="KryptonAboutToolkitData.ShowSystemInformationButton"/> is used (default true).
    /// </summary>
    public bool? ShowSystemInformationButton { get; set; }

    /// <summary>
    /// Gets or sets the assembly whose attributes and file version supply About identity.
    /// When null, the entry assembly is used.
    /// </summary>
    public Assembly? CurrentAssembly { get; set; }

    /// <summary>Gets or sets whether the build date uses the full culture-specific format (<c>F</c>) rather than general (<c>G</c>).</summary>
    public bool? UseFullBuiltOnDate { get; set; }

    /// <summary>Gets or sets the header image.</summary>
    [Editor(typeof(KryptonDesignerImageEditor), typeof(UITypeEditor))]
    public Image? HeaderImage { get; set; }

    /// <summary>Gets or sets the main image.</summary>
    public Image? MainImage { get; set; }

    /// <summary>
    /// Gets or sets an optional overlay (badge) drawn on top of <see cref="MainImage"/>.
    /// When <see cref="KryptonOverlayImage.Image"/> is null, no overlay is applied.
    /// </summary>
    public KryptonOverlayImage MainImageOverlay { get; set; }

    /// <summary>Gets or sets the application name. When empty, product/title attributes are used.</summary>
    public string? ApplicationName { get; set; }

    /// <summary>Gets or sets an optional version override. When empty, informational/file version is used.</summary>
    public string? Version { get; set; }

    /// <summary>Gets or sets an optional copyright override.</summary>
    public string? Copyright { get; set; }

    /// <summary>Gets or sets an optional company override.</summary>
    public string? Company { get; set; }

    /// <summary>Gets or sets an optional description override shown on the Description page.</summary>
    public string? Description { get; set; }

    /// <summary>Gets or sets the use RTL layout of the <see cref="KryptonAboutBox"/> UI.</summary>
    public KryptonUseRTLLayout UseRtlLayout { get; set; }

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="KryptonAboutBoxData"/> struct.</summary>
    public KryptonAboutBoxData()
    {
        UseRtlLayout = KryptonUseRTLLayout.No;
    }

    #endregion
}

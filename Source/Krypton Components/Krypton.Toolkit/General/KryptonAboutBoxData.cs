#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>A structure that contains basic information for <see cref="VisualAboutBoxForm"/>.</summary>
public struct KryptonAboutBoxData
{
    #region Public

    /// <summary>Gets or sets the show toolkit information.</summary>
    /// <value>The show toolkit information.</value>
    public bool? ShowToolkitInformation { get; set; }

    /// <summary>Gets or sets the current assembly.</summary>
    /// <value>The current assembly.</value>
    public Assembly CurrentAssembly { get; set; }

    /// <summary>Gets or sets the use full built on date.</summary>
    /// <value>The use full built on date.</value>
    public bool? UseFullBuiltOnDate { get; set; }

    /// <summary>Gets or sets the header image.</summary>
    /// <value>The header image.</value>
    public Image? HeaderImage { get; set; } //= GenericImageResources.InformationSmall;

    /// <summary>Gets or sets the main image.</summary>
    /// <value>The main image.</value>
    public Image? MainImage { get; set; }

    /// <summary>Gets or sets the name of the application.</summary>
    /// <value>The name of the application.</value>
    public string ApplicationName { get; set; }

    /// <summary>Gets or sets the use RTL layout of the <see cref="KryptonAboutBox"/> UI.</summary>
    /// <value>The use RTL layout in an <see cref="KryptonAboutBox"/>.</value>
    public KryptonUseRTLLayout UseRtlLayout { get; set; }

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="KryptonAboutBoxData" /> struct.</summary>
    public KryptonAboutBoxData()
    {
        UseRtlLayout = KryptonUseRTLLayout.No;
    }

    #endregion
}
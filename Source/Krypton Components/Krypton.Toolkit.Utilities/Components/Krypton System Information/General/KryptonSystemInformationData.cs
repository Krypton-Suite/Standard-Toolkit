#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Options for displaying <see cref="KryptonSystemInformation"/>.
/// </summary>
public struct KryptonSystemInformationData
{
    /// <summary>
    /// Gets or sets the category identifier to select when the dialog opens.
    /// Use values from <see cref="SystemInformationCategoryId"/>.
    /// </summary>
    public string? InitialCategoryId { get; set; }

    /// <summary>
    /// Gets or sets whether the File menu offers launching native Windows System Information (<c>MSInfo32.exe</c>).
    /// Defaults to <see langword="true"/>.
    /// </summary>
    public bool? ShowWindowsSystemInformation { get; set; }

    /// <summary>
    /// Gets or sets whether the form uses a right-to-left layout.
    /// </summary>
    public KryptonUseRTLLayout UseRtlLayout { get; set; }

    /// <summary>
    /// Gets or sets whether Loaded Modules enumerates every process (slow). Default is the current process only.
    /// </summary>
    public bool? EnumerateAllProcessModules { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonSystemInformationData"/> struct.
    /// </summary>
    public KryptonSystemInformationData()
    {
        ShowWindowsSystemInformation = true;
        UseRtlLayout = KryptonUseRTLLayout.No;
        InitialCategoryId = SystemInformationCategoryId.SystemSummary;
        EnumerateAllProcessModules = false;
    }
}

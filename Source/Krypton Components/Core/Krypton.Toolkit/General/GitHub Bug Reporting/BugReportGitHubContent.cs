#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, tobitege, Lesandro, KamaniAR, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Content for a GitHub bug report, matching the repository's bug report issue template.
/// </summary>
/// <remarks>
/// Field IDs and structure align with .github/ISSUE_TEMPLATE/bug_report.yml.
/// </remarks>
public class BugReportGitHubContent
{
    #region Identity

    /// <summary>
    /// Initializes a new instance of the <see cref="BugReportGitHubContent"/> class.
    /// </summary>
    public BugReportGitHubContent()
    {
        Summary = string.Empty;
        Description = string.Empty;
        StepsToReproduce = string.Empty;
        ExpectedBehavior = string.Empty;
        ActualBehavior = string.Empty;
        OperatingSystem = string.Empty;
        OsVersion = string.Empty;
        FrameworkVersion = string.Empty;
        ToolkitVersion = string.Empty;
        AdditionalInformation = string.Empty;
        AreasAffected = string.Empty;
    }

    #endregion

    #region Public

    /// <summary>
    /// A brief summary of the bug (used as the issue title).
    /// </summary>
    public string Summary { get; set; }

    /// <summary>
    /// Describe the bug in detail.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// List the steps to reproduce the bug.
    /// </summary>
    public string StepsToReproduce { get; set; }

    /// <summary>
    /// What you expected to happen.
    /// </summary>
    public string ExpectedBehavior { get; set; }

    /// <summary>
    /// What actually happened.
    /// </summary>
    public string ActualBehavior { get; set; }

    /// <summary>
    /// Operating system (e.g. Windows 11).
    /// </summary>
    public string OperatingSystem { get; set; }

    /// <summary>
    /// OS version (e.g. 10.0.26200).
    /// </summary>
    public string OsVersion { get; set; }

    /// <summary>
    /// Framework/.NET version (e.g. 4.8.1 or 8 - 10).
    /// </summary>
    public string FrameworkVersion { get; set; }

    /// <summary>
    /// The version of the Krypton Toolkit (e.g. 100.25.12.365).
    /// </summary>
    public string ToolkitVersion { get; set; }

    /// <summary>
    /// Any other information that might be helpful (screenshots, logs, etc.).
    /// </summary>
    public string AdditionalInformation { get; set; }

    /// <summary>
    /// Area affected: Docking, Navigator, Ribbon, Toolkit, or Workspace.
    /// </summary>
    public string AreasAffected { get; set; }

    #endregion
}

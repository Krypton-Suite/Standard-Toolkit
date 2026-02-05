#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, tobitege, Lesandro, KamaniAR, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

/// <summary>
/// Content for a GitHub bug report, matching the repository's bug report issue template.
/// </summary>
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

    #endregion
}

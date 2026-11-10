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
/// Result of an attempt to create a GitHub bug report issue.
/// </summary>
public readonly struct BugReportGitHubResult
{
    private BugReportGitHubResult(bool success, string? issueUrl, string? errorMessage)
    {
        Success = success;
        IssueUrl = issueUrl;
        ErrorMessage = errorMessage;
    }

    /// <summary>
    /// Gets a value indicating whether the issue was created successfully.
    /// </summary>
    public bool Success { get; }

    /// <summary>
    /// Gets the URL of the created issue when successful.
    /// </summary>
    public string? IssueUrl { get; }

    /// <summary>
    /// Gets the error message when the operation failed.
    /// </summary>
    public string? ErrorMessage { get; }

    /// <summary>
    /// Creates a successful result with the created issue URL.
    /// </summary>
    public static BugReportGitHubResult SuccessResult(string? issueUrl) =>
        new BugReportGitHubResult(true, issueUrl, null);

    /// <summary>
    /// Creates a failed result with the given error message.
    /// </summary>
    public static BugReportGitHubResult Failure(string errorMessage) =>
        new BugReportGitHubResult(false, null, errorMessage ?? string.Empty);
}
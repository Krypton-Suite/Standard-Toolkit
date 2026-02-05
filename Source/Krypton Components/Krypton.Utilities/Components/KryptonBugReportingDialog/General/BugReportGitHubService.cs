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
/// Service for creating bug report issues on GitHub via the REST API.
/// </summary>
/// <remarks>
/// Public API in Krypton.Utilities that delegates to the implementation in Krypton.Toolkit.
/// The public version does not follow any specific issue template (e.g. bug_report.yml); use title and body for a generic issue.
/// </remarks>
public class BugReportGitHubService
{
    private readonly Krypton.Toolkit.BugReportGitHubService _toolkitService = new Krypton.Toolkit.BugReportGitHubService();

    /// <summary>
    /// Creates a GitHub issue with the given title and body. Does not follow any repository-specific issue template.
    /// </summary>
    /// <param name="config">GitHub repository and authentication configuration. Must be valid.</param>
    /// <param name="title">The issue title.</param>
    /// <param name="body">The issue body (markdown). Sent as-is.</param>
    /// <returns>A result with the created issue URL on success, or an error message on failure.</returns>
    public BugReportGitHubResult CreateIssue(BugReportGitHubConfig config, string title, string body)
    {
        var toolkitResult = _toolkitService.CreateIssue(ToToolkitConfig(config), title ?? string.Empty, body ?? string.Empty);
        return FromToolkitResult(toolkitResult);
    }

    /// <summary>
    /// Creates a GitHub issue with the given title and body asynchronously. Does not follow any repository-specific issue template.
    /// </summary>
    /// <param name="config">GitHub repository and authentication configuration. Must be valid.</param>
    /// <param name="title">The issue title.</param>
    /// <param name="body">The issue body (markdown). Sent as-is.</param>
    /// <returns>A result with the created issue URL on success, or an error message on failure.</returns>
    public async Task<BugReportGitHubResult> CreateIssueAsync(BugReportGitHubConfig config, string title, string body)
    {
        var toolkitResult = await _toolkitService.CreateIssueAsync(ToToolkitConfig(config), title ?? string.Empty, body ?? string.Empty).ConfigureAwait(false);
        return FromToolkitResult(toolkitResult);
    }

    /// <summary>
    /// Creates a bug report issue on GitHub using the provided config and content (template-based; for internal/Standard-Toolkit use).
    /// </summary>
    /// <param name="config">GitHub repository and authentication configuration. Must be valid.</param>
    /// <param name="content">The bug report content matching the issue template.</param>
    /// <returns>A result with the created issue URL on success, or an error message on failure.</returns>
    public BugReportGitHubResult CreateIssue(BugReportGitHubConfig config, BugReportGitHubContent content)
    {
        var toolkitResult = _toolkitService.CreateIssue(ToToolkitConfig(config), ToToolkitContent(content));
        return FromToolkitResult(toolkitResult);
    }

    /// <summary>
    /// Creates a bug report issue on GitHub asynchronously (template-based; for internal/Standard-Toolkit use).
    /// </summary>
    /// <param name="config">GitHub repository and authentication configuration. Must be valid.</param>
    /// <param name="content">The bug report content matching the issue template.</param>
    /// <returns>A result with the created issue URL on success, or an error message on failure.</returns>
    public async Task<BugReportGitHubResult> CreateIssueAsync(BugReportGitHubConfig config, BugReportGitHubContent content)
    {
        var toolkitResult = await _toolkitService.CreateIssueAsync(ToToolkitConfig(config), ToToolkitContent(content)).ConfigureAwait(false);
        return FromToolkitResult(toolkitResult);
    }

    private static Krypton.Toolkit.BugReportGitHubConfig ToToolkitConfig(BugReportGitHubConfig config)
    {
        return new Krypton.Toolkit.BugReportGitHubConfig
        {
            Owner = config.Owner,
            RepositoryName = config.RepositoryName,
            PersonalAccessToken = config.PersonalAccessToken
        };
    }

    private static Krypton.Toolkit.BugReportGitHubContent ToToolkitContent(BugReportGitHubContent content)
    {
        return new Krypton.Toolkit.BugReportGitHubContent
        {
            Summary = content.Summary,
            Description = content.Description,
            StepsToReproduce = content.StepsToReproduce,
            ExpectedBehavior = content.ExpectedBehavior,
            ActualBehavior = content.ActualBehavior,
            OperatingSystem = content.OperatingSystem,
            OsVersion = content.OsVersion
        };
    }

    private static BugReportGitHubResult FromToolkitResult(Krypton.Toolkit.BugReportGitHubResult toolkitResult)
    {
        return toolkitResult.Success
            ? BugReportGitHubResult.SuccessResult(toolkitResult.IssueUrl)
            : BugReportGitHubResult.FailureResult(toolkitResult.ErrorMessage ?? string.Empty);
    }
}

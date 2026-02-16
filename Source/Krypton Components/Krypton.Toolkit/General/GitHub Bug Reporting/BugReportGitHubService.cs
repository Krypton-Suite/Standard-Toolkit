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
/// Service for creating bug report issues on GitHub via the REST API.
/// </summary>
/// <remarks>
/// Builds the issue body to match the repository's bug report template (.github/ISSUE_TEMPLATE/bug_report.yml).
/// </remarks>
public class BugReportGitHubService
{
    #region Constants

    private const string GitHubApiBase = "https://api.github.com";
    private const string AcceptHeader = "application/vnd.github+json";
    private const string ApiVersionHeader = "2022-11-28";
    private const string UserAgent = "Krypton-Standard-Toolkit-BugReport/1.0";

    #endregion

    #region Identity

    /// <summary>
    /// Initializes a new instance of the <see cref="BugReportGitHubService"/> class.
    /// </summary>
    public BugReportGitHubService()
    {
    }

    #endregion

    #region Public

    /// <summary>
    /// Creates a bug report issue on GitHub using the provided config and content.
    /// </summary>
    /// <param name="config">GitHub repository and authentication configuration. Must be valid.</param>
    /// <param name="content">The bug report content matching the issue template.</param>
    /// <returns>A result with the created issue URL on success, or an error message on failure.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="config"/> or <paramref name="content"/> is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown when <paramref name="config"/> is not valid.</exception>
    public BugReportGitHubResult CreateIssue(BugReportGitHubConfig config, BugReportGitHubContent content)
    {
        if (config == null)
        {
            throw new ArgumentNullException(nameof(config));
        }

        if (content == null)
        {
            throw new ArgumentNullException(nameof(content));
        }

        if (!config.IsValid)
        {
            throw new InvalidOperationException("GitHub configuration is incomplete. Set Owner, RepositoryName, and PersonalAccessToken.");
        }

        if (string.IsNullOrWhiteSpace(content.Summary))
        {
            return BugReportGitHubResult.Failure("Summary (title) is required.");
        }

        try
        {
            var body = BuildIssueBody(content);
            var json = BuildCreateIssueJson(content.Summary.Trim(), body);
            var url = $"{GitHubApiBase}/repos/{config.Owner.Trim()}/{config.RepositoryName.Trim()}/issues";

            using var client = CreateHttpClient(config.PersonalAccessToken);
            using var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            using var response = client.SendAsync(request).GetAwaiter().GetResult();

            if (response.IsSuccessStatusCode)
            {
                var responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var issueUrl = ExtractIssueUrlFromResponse(responseBody);
                return BugReportGitHubResult.SuccessResult(issueUrl ?? response.Headers.Location?.ToString() ?? url);
            }

            var errorBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var message = $"GitHub API returned {(int)response.StatusCode}: {response.ReasonPhrase}";
            if (!string.IsNullOrWhiteSpace(errorBody))
            {
                var detail = ExtractErrorMessage(errorBody);
                if (!string.IsNullOrEmpty(detail))
                {
                    message += " — " + detail;
                }
            }

            return BugReportGitHubResult.Failure(message);
        }
        catch (HttpRequestException ex)
        {
            KryptonExceptionDialog.Show(ex);

            return BugReportGitHubResult.Failure($"Request failed: {ex.Message}");
        }
        catch (TaskCanceledException ex)
        {
            KryptonExceptionDialog.Show(ex);

            return BugReportGitHubResult.Failure("Request was cancelled or timed out.");
        }
        catch (Exception ex)
        {
            KryptonExceptionDialog.Show(ex);

            return BugReportGitHubResult.Failure(ex.Message);
        }
    }

    /// <summary>
    /// Creates a bug report issue on GitHub asynchronously.
    /// </summary>
    /// <param name="config">GitHub repository and authentication configuration. Must be valid.</param>
    /// <param name="content">The bug report content matching the issue template.</param>
    /// <returns>A result with the created issue URL on success, or an error message on failure.</returns>
    public async Task<BugReportGitHubResult> CreateIssueAsync(BugReportGitHubConfig config, BugReportGitHubContent content)
    {
        if (config == null)
        {
            throw new ArgumentNullException(nameof(config));
        }

        if (content == null)
        {
            throw new ArgumentNullException(nameof(content));
        }

        if (!config.IsValid)
        {
            throw new InvalidOperationException("GitHub configuration is incomplete. Set Owner, RepositoryName, and PersonalAccessToken.");
        }

        if (string.IsNullOrWhiteSpace(content.Summary))
        {
            return BugReportGitHubResult.Failure("Summary (title) is required.");
        }

        try
        {
            var body = BuildIssueBody(content);
            var json = BuildCreateIssueJson(content.Summary.Trim(), body);
            var url = $"{GitHubApiBase}/repos/{config.Owner.Trim()}/{config.RepositoryName.Trim()}/issues";

            using var client = CreateHttpClient(config.PersonalAccessToken);
            using var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            using var response = await client.SendAsync(request).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var issueUrl = ExtractIssueUrlFromResponse(responseBody);
                return BugReportGitHubResult.SuccessResult(issueUrl ?? response.Headers.Location?.ToString() ?? url);
            }

            var errorBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var message = $"GitHub API returned {(int)response.StatusCode}: {response.ReasonPhrase}";
            if (!string.IsNullOrWhiteSpace(errorBody))
            {
                var detail = ExtractErrorMessage(errorBody);
                if (!string.IsNullOrEmpty(detail))
                {
                    message += " — " + detail;
                }
            }

            return BugReportGitHubResult.Failure(message);
        }
        catch (HttpRequestException ex)
        {
            KryptonExceptionDialog.Show(ex);

            return BugReportGitHubResult.Failure($"Request failed: {ex.Message}");
        }
        catch (TaskCanceledException ex)
        {
            KryptonExceptionDialog.Show(ex);

            return BugReportGitHubResult.Failure("Request was cancelled or timed out.");
        }
        catch (Exception ex)
        {
            KryptonExceptionDialog.Show(ex);

            return BugReportGitHubResult.Failure(ex.Message);
        }
    }

    /// <summary>
    /// Creates a GitHub issue with the given title and body (no template). Use for repositories that do not follow a specific issue template.
    /// </summary>
    /// <param name="config">GitHub repository and authentication configuration. Must be valid.</param>
    /// <param name="title">The issue title.</param>
    /// <param name="body">The issue body (markdown). Sent as-is.</param>
    /// <returns>A result with the created issue URL on success, or an error message on failure.</returns>
    public BugReportGitHubResult CreateIssue(BugReportGitHubConfig config, string title, string? body)
    {
        if (config == null)
        {
            throw new ArgumentNullException(nameof(config));
        }

        if (!config.IsValid)
        {
            throw new InvalidOperationException("GitHub configuration is incomplete. Set Owner, RepositoryName, and PersonalAccessToken.");
        }

        if (string.IsNullOrWhiteSpace(title))
        {
            return BugReportGitHubResult.Failure("Title is required.");
        }

        var bodyText = body ?? string.Empty;

        try
        {
            var json = BuildCreateIssueJson(title.Trim(), bodyText);
            var url = $"{GitHubApiBase}/repos/{config.Owner.Trim()}/{config.RepositoryName.Trim()}/issues";

            using var client = CreateHttpClient(config.PersonalAccessToken);
            using var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            using var response = client.SendAsync(request).GetAwaiter().GetResult();

            if (response.IsSuccessStatusCode)
            {
                var responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var issueUrl = ExtractIssueUrlFromResponse(responseBody);
                return BugReportGitHubResult.SuccessResult(issueUrl ?? response.Headers.Location?.ToString() ?? url);
            }

            var errorBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var message = $"GitHub API returned {(int)response.StatusCode}: {response.ReasonPhrase}";
            if (!string.IsNullOrWhiteSpace(errorBody))
            {
                var detail = ExtractErrorMessage(errorBody);
                if (!string.IsNullOrEmpty(detail))
                {
                    message += " — " + detail;
                }
            }

            return BugReportGitHubResult.Failure(message);
        }
        catch (HttpRequestException ex)
        {
            KryptonExceptionDialog.Show(ex);

            return BugReportGitHubResult.Failure($"Request failed: {ex.Message}");
        }
        catch (TaskCanceledException ex)
        {
            KryptonExceptionDialog.Show(ex);

            return BugReportGitHubResult.Failure("Request was cancelled or timed out.");
        }
        catch (Exception ex)
        {
            KryptonExceptionDialog.Show(ex);

            return BugReportGitHubResult.Failure(ex.Message);
        }
    }

    /// <summary>
    /// Creates a GitHub issue with the given title and body asynchronously (no template).
    /// </summary>
    /// <param name="config">GitHub repository and authentication configuration. Must be valid.</param>
    /// <param name="title">The issue title.</param>
    /// <param name="body">The issue body (markdown). Sent as-is.</param>
    /// <returns>A result with the created issue URL on success, or an error message on failure.</returns>
    public async Task<BugReportGitHubResult> CreateIssueAsync(BugReportGitHubConfig config, string title, string? body)
    {
        if (config == null)
        {
            throw new ArgumentNullException(nameof(config));
        }

        if (!config.IsValid)
        {
            throw new InvalidOperationException("GitHub configuration is incomplete. Set Owner, RepositoryName, and PersonalAccessToken.");
        }

        if (string.IsNullOrWhiteSpace(title))
        {
            return BugReportGitHubResult.Failure("Title is required.");
        }

        var bodyText = body ?? string.Empty;

        try
        {
            var json = BuildCreateIssueJson(title.Trim(), bodyText);
            var url = $"{GitHubApiBase}/repos/{config.Owner.Trim()}/{config.RepositoryName.Trim()}/issues";

            using var client = CreateHttpClient(config.PersonalAccessToken);
            using var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            using var response = await client.SendAsync(request).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var issueUrl = ExtractIssueUrlFromResponse(responseBody);
                return BugReportGitHubResult.SuccessResult(issueUrl ?? response.Headers.Location?.ToString() ?? url);
            }

            var errorBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var message = $"GitHub API returned {(int)response.StatusCode}: {response.ReasonPhrase}";
            if (!string.IsNullOrWhiteSpace(errorBody))
            {
                var detail = ExtractErrorMessage(errorBody);
                if (!string.IsNullOrEmpty(detail))
                {
                    message += " — " + detail;
                }
            }

            return BugReportGitHubResult.Failure(message);
        }
        catch (HttpRequestException ex)
        {
            KryptonExceptionDialog.Show(ex);

            return BugReportGitHubResult.Failure($"Request failed: {ex.Message}");
        }
        catch (TaskCanceledException ex)
        {
            KryptonExceptionDialog.Show(ex);

            return BugReportGitHubResult.Failure("Request was cancelled or timed out.");
        }
        catch (Exception ex)
        {
            KryptonExceptionDialog.Show(ex);

            return BugReportGitHubResult.Failure(ex.Message);
        }
    }

    #endregion

    #region Private

    /// <summary>
    /// Builds the issue body markdown from the template content, matching bug_report.yml sections.
    /// </summary>
    private static string BuildIssueBody(BugReportGitHubContent content)
    {
        var sb = new StringBuilder();

        sb.AppendLine("Thanks for taking the time to report an issue! Please fill out the template below to help us understand and address the problem. Please make sure to [search for existing issues](https://github.com/Krypton-Suite/Standard-Toolkit/issues) before filing a new one!");
        sb.AppendLine();
        sb.AppendLine("---");
        sb.AppendLine();

        AppendSection(sb, "**Description**", content.Description);
        AppendSection(sb, "**Steps to Reproduce**", content.StepsToReproduce);
        AppendSection(sb, "**Expected Behavior**", content.ExpectedBehavior);
        AppendSection(sb, "**Actual Behavior**", content.ActualBehavior);
        AppendSection(sb, "**Operating System**", content.OperatingSystem);
        AppendSection(sb, "**OS Version**", content.OsVersion);
        AppendSection(sb, "**Framework/.NET Version**", content.FrameworkVersion);
        AppendSection(sb, "**Toolkit Version**", content.ToolkitVersion);
        AppendSection(sb, "**Additional Information**", content.AdditionalInformation);
        AppendSection(sb, "**Areas Affected**", content.AreasAffected);

        return sb.ToString();
    }

    private static void AppendSection(StringBuilder sb, string heading, string value)
    {
        sb.AppendLine(heading);
        sb.AppendLine();
        sb.AppendLine(string.IsNullOrWhiteSpace(value) ? "_Not provided_" : value.Trim());
        sb.AppendLine();
    }

    private static string BuildCreateIssueJson(string title, string body)
    {
        var escapedTitle = EscapeJsonString(title);
        var escapedBody = EscapeJsonString(body);
        return $@"{{""title"":""{escapedTitle}"",""body"":""{escapedBody}"",""labels"":[""bug""]}}";
    }

    private static string EscapeJsonString(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return string.Empty;
        }

        var sb = new StringBuilder(value.Length);
        foreach (var c in value)
        {
            switch (c)
            {
                case '\\':
                    sb.Append(@"\\");
                    break;
                case '"':
                    sb.Append(@"\""");
                    break;
                case '\n':
                    sb.Append(@"\n");
                    break;
                case '\r':
                    sb.Append(@"\r");
                    break;
                case '\t':
                    sb.Append(@"\t");
                    break;
                default:
                    if (c < ' ')
                    {
                        sb.AppendFormat(@"\u{0:x4}", (int)c);
                    }
                    else
                    {
                        sb.Append(c);
                    }

                    break;
            }
        }

        return sb.ToString();
    }

    private static HttpClient CreateHttpClient(string token)
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Accept", AcceptHeader);
        client.DefaultRequestHeaders.Add("X-GitHub-Api-Version", ApiVersionHeader);
        client.DefaultRequestHeaders.Add("User-Agent", UserAgent);
        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.Trim());
        return client;
    }

    private static string? ExtractIssueUrlFromResponse(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            return null;
        }

        // GitHub returns { "html_url": "https://github.com/owner/repo/issues/123", ... }
        const string marker = "\"html_url\":\"";
        var start = json.IndexOf(marker, StringComparison.Ordinal);
        if (start < 0)
        {
            return null;
        }

        start += marker.Length;
        var end = json.IndexOf('"', start);
        if (end < 0)
        {
            return null;
        }

        return json.Substring(start, end - start).Replace("\\/", "/");
    }

    private static string? ExtractErrorMessage(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            return null;
        }

        // GitHub error: { "message": "Validation Failed", "errors": [...] }
        const string marker = "\"message\":\"";
        var start = json.IndexOf(marker, StringComparison.Ordinal);
        if (start < 0)
        {
            return null;
        }

        start += marker.Length;
        var end = json.IndexOf('"', start);
        if (end < 0)
        {
            return null;
        }

        return json.Substring(start, end - start).Replace("\\/", "/");
    }

    #endregion
}

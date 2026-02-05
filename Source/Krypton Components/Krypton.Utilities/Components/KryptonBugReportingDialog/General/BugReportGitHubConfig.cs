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
/// Configuration for creating bug reports on GitHub via the Issues API.
/// </summary>
/// <remarks>
/// Use a Personal Access Token (PAT) with <c>repo</c> or <c>public_repo</c> scope.
/// Store the token securely (e.g. user config or environment variable); do not commit to source control.
/// </remarks>
public class BugReportGitHubConfig
{
    #region Identity

    /// <summary>
    /// Initializes a new instance of the <see cref="BugReportGitHubConfig"/> class.
    /// </summary>
    public BugReportGitHubConfig()
    {
        Owner = string.Empty;
        RepositoryName = string.Empty;
        PersonalAccessToken = string.Empty;
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the GitHub repository owner (e.g. Krypton-Suite).
    /// </summary>
    public string Owner { get; set; }

    /// <summary>
    /// Gets or sets the repository name (e.g. Standard-Toolkit).
    /// </summary>
    public string RepositoryName { get; set; }

    /// <summary>
    /// Gets or sets the Personal Access Token used to authenticate with the GitHub API.
    /// </summary>
    /// <remarks>
    /// Create a token at GitHub → Settings → Developer settings → Personal access tokens.
    /// Required scope: <c>repo</c> (or <c>public_repo</c> for public repositories only).
    /// </remarks>
    public string PersonalAccessToken { get; set; }

    /// <summary>
    /// Gets a value indicating whether the configuration has the minimum required values to create an issue.
    /// </summary>
    public bool IsValid =>
        !string.IsNullOrWhiteSpace(Owner) &&
        !string.IsNullOrWhiteSpace(RepositoryName) &&
        !string.IsNullOrWhiteSpace(PersonalAccessToken);

    #endregion
}

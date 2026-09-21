#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Optional extras for <see cref="KryptonExceptionDialog.Show(Exception, KryptonExceptionDialogOptions)"/>.
/// Existing <c>Show</c> overloads are unchanged.
/// </summary>
public sealed class KryptonExceptionDialogOptions
{
    /// <summary>Gets or sets the highlight colour. When null, the dialog default is used.</summary>
    public Color? HighlightColor { get; set; }

    /// <summary>Gets or sets whether the copy button is shown.</summary>
    public bool? ShowCopyButton { get; set; }

    /// <summary>Gets or sets whether the search box is shown.</summary>
    public bool? ShowSearchBox { get; set; }

    /// <summary>Gets or sets the callback invoked by the Report Bug button.</summary>
    public Action<Exception>? BugReportCallback { get; set; }

    /// <summary>Gets or sets the GitHub secret used to decrypt the issue-report config.</summary>
    public SecureString? GitHubSecretKey { get; set; }

    /// <summary>Gets or sets the encrypted GitHub config path. When null, the default path is used.</summary>
    public string? GitHubConfigPath { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether recent <see cref="KryptonLog"/> events are appended
    /// to copied exception details.
    /// </summary>
    public bool IncludeRecentLog { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether a View Log button is shown. When null, the button
    /// is shown if <see cref="IncludeRecentLog"/> is true and a memory sink is configured.
    /// </summary>
    public bool? ShowViewLogButton { get; set; }

    /// <summary>Gets or sets how many recent log events to append when copying. Defaults to 50.</summary>
    public int RecentLogLineCount { get; set; } = 50;
}

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
/// Public API for the GitHub issue report dialog.
/// </summary>
/// <remarks>
/// <para>
/// This dialog loads GitHub configuration (Owner, RepositoryName, PersonalAccessToken) from an encrypted file.
/// The user is not shown any GitHub configuration fields — only issue content (Title, Description).
/// </para>
/// <para>
/// <b>Developer setup:</b>
/// Use <see cref="BugReportGitHubConfigEncryption.SaveEncryptedConfig(BugReportGitHubConfig, string, System.Security.SecureString)"/> to create the encrypted config file,
/// then ship it with your application. At runtime, provide the same secret key to <see cref="Show(IWin32Window?, SecureString?)"/>.
/// </para>
/// </remarks>
[ToolboxItem(false)]
[DesignerCategory(@"code")]
public static class KryptonGitHubIssueReportDialog
{
    /// <summary>
    /// Displays the GitHub issue report dialog using configuration loaded from the default encrypted config file.
    /// </summary>
    /// <param name="owner">Optional parent window. Can be null.</param>
    /// <param name="secretKey">The secret key used to decrypt the configuration file.</param>
    /// <returns>DialogResult.OK if the issue was created successfully; otherwise, DialogResult.Cancel.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="secretKey"/> is null or empty.</exception>
    public static DialogResult Show(IWin32Window? owner, SecureString? secretKey) => Show(owner, secretKey, null);

    /// <summary>
    /// Displays the GitHub issue report dialog using configuration loaded from the specified encrypted config file.
    /// </summary>
    /// <param name="owner">Optional parent window. Can be null.</param>
    /// <param name="secretKey">The secret key used to decrypt the configuration file.</param>
    /// <param name="configFilePath">Path to the encrypted config file. If null, uses the default path.</param>
    /// <returns>DialogResult.OK if the issue was created successfully; otherwise, DialogResult.Cancel.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="secretKey"/> is null or empty.</exception>
    public static DialogResult Show(IWin32Window? owner, SecureString? secretKey, string? configFilePath) => Show(owner, secretKey, configFilePath, null);

    /// <summary>
    /// Displays the GitHub issue report dialog with configuration from the specified encrypted config file and optional pre-filled description.
    /// </summary>
    /// <param name="owner">Optional parent window. Can be null.</param>
    /// <param name="secretKey">The secret key used to decrypt the configuration file.</param>
    /// <param name="configFilePath">Path to the encrypted config file. If null, uses the default path.</param>
    /// <param name="initialDescription">Optional pre-filled text for the issue description (e.g. exception details from KryptonExceptionDialog).</param>
    /// <returns>DialogResult.OK if the issue was created successfully; otherwise, DialogResult.Cancel.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="secretKey"/> is null or empty.</exception>
    public static DialogResult Show(IWin32Window? owner, SecureString? secretKey, string? configFilePath, string? initialDescription)
    {
        if (secretKey == null || secretKey.Length == 0)
        {
            throw new ArgumentNullException(nameof(secretKey));
        }

        var filePath = configFilePath ?? BugReportGitHubConfigEncryption.GetDefaultConfigFilePath();

        if (!BugReportGitHubConfigEncryption.TryLoadEncryptedConfig(filePath, secretKey!, out var config) || config == null)
        {
            KryptonMessageBox.Show(
                "Failed to load GitHub configuration. The encrypted config file may be missing, corrupted, or the secret key is incorrect.",
                "Configuration Error",
                KryptonMessageBoxButtons.OK,
                KryptonMessageBoxIcon.Error);

            return DialogResult.Cancel;
        }

        using var dialog = new VisualGitHubIssueReportForm(config, initialDescription);
        return dialog.ShowDialog(owner);
    }

    /// <summary>
    /// Displays the GitHub issue report dialog with an explicitly provided configuration (no file loading).
    /// </summary>
    /// <param name="owner">Optional parent window. Can be null.</param>
    /// <param name="config">The GitHub configuration to use. Must be valid (Owner, RepositoryName, and PersonalAccessToken required).</param>
    /// <returns>DialogResult.OK if the issue was created successfully; otherwise, DialogResult.Cancel.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="config"/> is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown when <paramref name="config"/> is not valid.</exception>
    public static DialogResult Show(IWin32Window? owner, BugReportGitHubConfig? config) => Show(owner, config, null);

    /// <summary>
    /// Displays the GitHub issue report dialog with an explicitly provided configuration and optional pre-filled description.
    /// </summary>
    /// <param name="owner">Optional parent window. Can be null.</param>
    /// <param name="config">The GitHub configuration to use. Must be valid (Owner, RepositoryName, and PersonalAccessToken required).</param>
    /// <param name="initialDescription">Optional pre-filled text for the issue description (e.g. exception details).</param>
    /// <returns>DialogResult.OK if the issue was created successfully; otherwise, DialogResult.Cancel.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="config"/> is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown when <paramref name="config"/> is not valid.</exception>
    public static DialogResult Show(IWin32Window? owner, BugReportGitHubConfig? config, string? initialDescription)
    {
        if (config == null)
        {
            throw new ArgumentNullException(nameof(config));
        }

        if (!config.IsValid)
        {
            throw new InvalidOperationException("Config must have Owner, RepositoryName, and PersonalAccessToken set.");
        }

        using var dialog = new VisualGitHubIssueReportForm(config, initialDescription);
        return dialog.ShowDialog(owner);
    }
}


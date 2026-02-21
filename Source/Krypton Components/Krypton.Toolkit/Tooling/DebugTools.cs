#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Allow Krypton to be improved by getting help from users.
/// </summary>
/// <remarks>
/// <para>
/// To enable the integrated GitHub issue dialog for <see cref="NotImplemented"/>, call <see cref="ConfigureGitHubReporting"/>
/// at application startup with the secret key for your encrypted config file.
/// </para>
/// <para>
/// If not configured, clicking "Yes" on the "Not Implemented" prompt will open the GitHub new issue page in the browser.
/// </para>
/// </remarks>
public static class DebugTools
{
    #region Static Fields

    private static SecureString? _secretKey;
    private static string? _configFilePath;

    #endregion

    #region Public

    /// <summary>
    /// Configures DebugTools to use the integrated GitHub issue report dialog.
    /// Call this at application startup.
    /// </summary>
    /// <param name="secretKey">The secret key for decrypting the GitHub config file.</param>
    /// <param name="configFilePath">Optional path to the encrypted config file. If null, uses the default path.</param>
    public static void ConfigureGitHubReporting(SecureString secretKey, string? configFilePath = null)
    {
        _secretKey = secretKey;
        _configFilePath = configFilePath;
    }

    /// <summary>
    /// Clears the GitHub reporting configuration, reverting to browser-based issue creation.
    /// </summary>
    public static void ClearGitHubReporting()
    {
        _secretKey = null;
        _configFilePath = null;
    }

    /// <summary>
    /// Gets a value indicating whether GitHub reporting is configured.
    /// </summary>
    public static bool IsGitHubReportingConfigured => _secretKey != null && _secretKey.Length > 0;

    /// <summary>
    /// Allow Krypton to be improved by getting help from users.
    /// </summary>
    /// <remarks>
    /// If <see cref="ConfigureGitHubReporting"/> has been called, shows the integrated GitHub issue dialog.
    /// Otherwise, opens the GitHub new issue page in the browser.
    /// </remarks>
    public static Exception NotImplemented(string? outOfRange,
        [CallerFilePath] string callingFilePath = "",
        [CallerLineNumber] int lineNumber = 0,
        [CallerMemberName] string? callingMethod = "")
    {
        // Do not use `KryptonMessageBox` as this will cause palette's to go into recurrent loop
        if (DialogResult.Yes == MessageBox.Show(
                @$"If you are seeing this message, please submit a new bug report here.\n\nAdditional details:-\nMethod Signature: {callingMethod}\nFunction: {callingMethod}\nFile: {callingFilePath}\nLine Number: {lineNumber}",
                @"Not Implemented - Please submit ?", MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2))
        {
            if (IsGitHubReportingConfigured)
            {
                var context = $"**Not Implemented context**\r\nMethod: {callingMethod}\r\nFile: {callingFilePath}\r\nLine: {lineNumber}";
                KryptonGitHubIssueReportDialog.Show(Form.ActiveForm, _secretKey!, _configFilePath, context);
            }
            else
            {
                // Fallback: open GitHub new issue page in browser
                Process.Start(new ProcessStartInfo
                {
                    FileName = @"https://github.com/Krypton-Suite/Standard-Toolkit/issues/new/choose",
                    UseShellExecute = true
                });
            }
        }

        return new ArgumentOutOfRangeException(outOfRange)
        {
            Source = callingMethod
        };
    }

    #endregion
}

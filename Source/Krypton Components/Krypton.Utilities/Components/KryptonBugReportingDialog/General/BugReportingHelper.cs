#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

/// <summary>
/// Helper class for integrating bug reporting with exception dialogs.
/// </summary>
public static class BugReportingHelper
{
    /// <summary>
    /// Shows an exception dialog with bug reporting capability.
    /// </summary>
    /// <param name="exception">The exception to display.</param>
    /// <param name="emailConfig">The email configuration for bug reporting.</param>
    /// <param name="highlightColor">Optional highlight color for the exception dialog.</param>
    /// <param name="showCopyButton">Optional flag to show the copy button.</param>
    /// <param name="showSearchBox">Optional flag to show the search box.</param>
    public static void ShowExceptionWithBugReporting(Exception exception, BugReportEmailConfig emailConfig, 
        Color? highlightColor = null, bool? showCopyButton = null, bool? showSearchBox = null)
    {
        Krypton.Toolkit.KryptonExceptionDialog.Show(exception, highlightColor, showCopyButton, showSearchBox, 
            ex => KryptonBugReportingDialog.Show(ex, emailConfig));
    }
}


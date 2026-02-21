#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

/// <summary>The public interface to the <see cref="VisualBugReportingDialogForm"/> class.</summary>
[ToolboxItem(false)]
[DesignerCategory(@"code")]
public static class KryptonBugReportingDialog
{
    #region Public

    /// <summary>
    /// Displays the bug reporting dialog to allow the user to report a bug.
    /// </summary>
    /// <param name="exception">Optional exception to include in the bug report.</param>
    /// <param name="emailConfig">The email configuration for sending the bug report.</param>
    /// <returns>DialogResult.OK if the bug report was sent successfully; otherwise, DialogResult.Cancel.</returns>
    public static DialogResult Show(Exception? exception, BugReportEmailConfig emailConfig)
    {
        if (emailConfig == null)
        {
            throw new ArgumentNullException(nameof(emailConfig));
        }

        using var dialog = new VisualBugReportingDialogForm(exception, emailConfig);
        return dialog.ShowDialog();
    }

    /// <summary>
    /// Displays the bug reporting dialog to allow the user to report a bug.
    /// </summary>
    /// <param name="emailConfig">The email configuration for sending the bug report.</param>
    /// <returns>DialogResult.OK if the bug report was sent successfully; otherwise, DialogResult.Cancel.</returns>
    public static DialogResult Show(BugReportEmailConfig emailConfig) => Show(null, emailConfig);

    #endregion
}


#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

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
            ThrowHelper.ThrowArgumentNullException(nameof(emailConfig));
        }

        using var dialog = new VisualBugReportingDialogForm(exception, emailConfig);
        return dialog.ShowDialog();
    }

    /// <summary>
    /// Displays the bug reporting dialog, optionally attaching a recent application log excerpt.
    /// </summary>
    /// <param name="exception">Optional exception to include in the bug report.</param>
    /// <param name="emailConfig">The email configuration for sending the bug report.</param>
    /// <param name="includeApplicationLog">When true, attaches a log excerpt from <see cref="KryptonLog"/> if available.</param>
    public static DialogResult Show(Exception? exception, BugReportEmailConfig emailConfig, bool includeApplicationLog)
    {
        if (emailConfig == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(emailConfig));
        }

        using var dialog = new VisualBugReportingDialogForm(exception, emailConfig, includeApplicationLog);
        return dialog.ShowDialog();
    }

    /// <summary>
    /// Displays the bug reporting dialog to allow the user to report a bug.
    /// </summary>
    /// <param name="emailConfig">The email configuration for sending the bug report.</param>
    /// <returns>DialogResult.OK if the bug report was sent successfully; otherwise, DialogResult.Cancel.</returns>
    public static DialogResult Show(BugReportEmailConfig emailConfig) => Show(null, emailConfig);

    /// <summary>Displays the bug reporting dialog asynchronously.</summary>
    public static Task<DialogResult> ShowAsync(Exception? exception, BugReportEmailConfig emailConfig)
    {
        if (emailConfig == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(emailConfig));
        }

        return ShowCoreAsync(exception, emailConfig);
    }

    /// <summary>Displays the bug reporting dialog asynchronously.</summary>
    public static Task<DialogResult> ShowAsync(BugReportEmailConfig emailConfig) => ShowAsync(null, emailConfig);

    /// <summary>Displays the bug reporting dialog asynchronously, optionally attaching a log excerpt.</summary>
    public static Task<DialogResult> ShowAsync(Exception? exception, BugReportEmailConfig emailConfig, bool includeApplicationLog)
    {
        if (emailConfig == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(emailConfig));
        }

        return ShowCoreAsync(exception, emailConfig, includeApplicationLog);
    }

    private static async Task<DialogResult> ShowCoreAsync(Exception? exception, BugReportEmailConfig emailConfig, bool includeApplicationLog = false)
    {
        using var dialog = new VisualBugReportingDialogForm(exception, emailConfig, includeApplicationLog);
        // Await required so using does not dispose the form before the dialog completes.
        return await KryptonFormAsync.ShowDialogAsync(dialog).ConfigureAwait(false);
    }


    #endregion
}


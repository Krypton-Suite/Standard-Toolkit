#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

internal partial class VisualExceptionDialogForm : KryptonForm
{
    #region Instance Fields

    private readonly bool? _showCopyButton;

    private readonly bool? _showSearchBox;

    private readonly bool? _showSubmitBugReportButton;

    private readonly bool? _showReportBugToGitHubButton;

    private readonly Color? _highlightColor;

    private readonly Exception? _exception;

    private readonly Action<Exception>? _bugReportCallback;

    private readonly string? _gitHubSecretKey;

    private readonly string? _gitHubConfigFilePath;

    private readonly List<KryptonTreeNode> _originalNodes = new List<KryptonTreeNode>();

    #endregion

    #region Identity

    public VisualExceptionDialogForm(bool? showCopyButton, bool? showSearchBox, bool? showSubmitBugReportButton, Color? highlightColor, Exception exception, Action<Exception>? bugReportCallback = null, bool? showReportBugToGitHubButton = null, string? gitHubSecretKey = null, string? gitHubConfigFilePath = null)
    {
        InitializeComponent();

        SetInheritedControlOverride();

        _showCopyButton = showCopyButton ?? false;

        _showSearchBox = showSearchBox ?? false;

        _showSubmitBugReportButton = showSubmitBugReportButton ?? false;

        _showReportBugToGitHubButton = showReportBugToGitHubButton ?? false;

        _gitHubSecretKey = string.IsNullOrWhiteSpace(gitHubSecretKey) ? null : gitHubSecretKey;

        _gitHubConfigFilePath = gitHubConfigFilePath;

        _highlightColor = highlightColor ?? Color.LightYellow;

        _exception = exception;

        _bugReportCallback = bugReportCallback;

        // Set highlight color
        isbSearchArea.HighlightColor = (Color)_highlightColor;

        SetupUI();
    }

    #endregion

    #region Implementation

    private void SetupUI()
    {
        Text = KryptonManager.Strings.ExceptionDialogStrings.WindowTitle;
        kwlblExceptionDetails.Text = KryptonManager.Strings.ExceptionDialogStrings.ExceptionDetailsHeader;
        kwlblExceptionOutline.Text = KryptonManager.Strings.ExceptionDialogStrings.ExceptionOutlineHeader;
        kbtnCopy.Text = KryptonManager.Strings.ExceptionDialogStrings.CopyDetailsButtonText;
        kbtnReportBug.Text = KryptonManager.Strings.ExceptionDialogStrings.ReportBugButtonText;
        kbtnReportBugToGitHub.Text = KryptonManager.Strings.ExceptionDialogStrings.ReportBugOnGitHubButtonText;
        kbtnOk.Text = KryptonManager.Strings.GeneralStrings.OK;
        kbtnCopy.Visible = _showCopyButton ?? true;
        kbtnCopy.Visible = _showSubmitBugReportButton ?? true;
        isbSearchArea.ShowSearchFeatures = _showSearchBox ?? true;

        if (_bugReportCallback != null && _exception != null)
        {
            kbtnReportBug.Visible = true;
            kbtnReportBug.Text = KryptonManager.Strings.BugReportingDialogStrings.ReportBugButtonText;
            kbtnReportBug.Click += KbtnReportBug_Click;
        }
        else
        {
            kbtnReportBug.Visible = false;
        }

        var showReportToGitHubButton = (_bugReportCallback != null || _gitHubSecretKey != null) && _exception != null;

        if (showReportToGitHubButton) 
        {
            kbtnReportBugToGitHub.Visible = true;
            kbtnReportBugToGitHub.Text = KryptonManager.Strings.ExceptionDialogStrings.ReportBugOnGitHubButtonText;
            kbtnReportBugToGitHub.Click += KbtnReportBugToGitHub_Click;
        }
        else
        {
            kbtnReportBugToGitHub.Visible = false;
        }

        isbSearchArea.SearchBox.CueHint.CueHintText = KryptonManager.Strings.ExceptionDialogStrings.SearchBoxCueText;
        if (_exception is not null)
        {
            isbSearchArea.Populate(_exception);

            foreach (KryptonTreeNode node in isbSearchArea.Tree.Nodes)
            {
                _originalNodes.Add((KryptonTreeNode)node.Clone());
            }
        }

        if (GeneralToolkitUtilities.GetCurrentScreenSize() == new Point(1080, 720))
        {
            GeneralToolkitUtilities.AdjustFormDimensions(this, 900, 650);
        }
        else
        {
            GeneralToolkitUtilities.AdjustFormDimensions(this, 1108, 687);
        }
    }

    private string? FormatExceptionDetails(Exception exception) =>
        // Format exception details
        $"{KryptonManager.Strings.ExceptionDialogStrings.Type}: {exception.GetType().Name}\n" +
        $"{KryptonManager.Strings.ExceptionDialogStrings.Message}: {exception.Message}\n\n" +
        $"{KryptonManager.Strings.ExceptionDialogStrings.StackTrace}:\n{exception.StackTrace}\n\n" +
        $"{KryptonManager.Strings.ExceptionDialogStrings.InnerException}:\n{(exception.InnerException != null ? exception.InnerException.Message : $"{KryptonManager.Strings.ExceptionDialogStrings.None}")}\n";

    private void kbtnCopy_Click(object sender, EventArgs e) => Clipboard.SetText(krtbExceptionDetails.Text);

    private void kbtnOk_Click(object sender, EventArgs e) => DialogResult = DialogResult.OK;

    private void isbSearchArea_NodeSelected(object sender, TreeViewEventArgs e)
    {
        var selectedException = isbSearchArea.SelectedException;

        if (e.Node!.Text == KryptonManager.Strings.ExceptionDialogStrings.InnerException ||
            e.Node.Text == KryptonManager.Strings.ExceptionDialogStrings.StackTrace)
        {
            krtbExceptionDetails.Text = KryptonManager.Strings.ExceptionDialogStrings.MoreDetails;
        }
        else
        {
            krtbExceptionDetails.Text = selectedException != null
                ? FormatExceptionDetails(selectedException)
                : e.Node.Text;
        }
    }

    private void krtbExceptionDetails_TextChanged(object sender, EventArgs e) => kbtnCopy.Enabled = !string.IsNullOrEmpty(krtbExceptionDetails.Text);

    private void KbtnReportBug_Click(object? sender, EventArgs e)
    {
        if (_exception != null && _bugReportCallback != null)
        {
            _bugReportCallback(_exception);
        }
    }

    private void KbtnReportBugToGitHub_Click(object? sender, EventArgs e)
    {
        if (_exception == null)
        {
            return;
        }

        if (_gitHubSecretKey != null)
        {
            var filePath = _gitHubConfigFilePath ?? BugReportGitHubConfigEncryption.GetDefaultConfigFilePath();
            if (!BugReportGitHubConfigEncryption.TryLoadEncryptedConfig(filePath, _gitHubSecretKey, out var config) || config == null)
            {
                KryptonMessageBox.Show(
                    "Failed to load GitHub configuration. The encrypted config file may be missing, corrupted, or the secret key is incorrect.",
                    "Configuration Error",
                    KryptonMessageBoxButtons.OK,
                    KryptonMessageBoxIcon.Error);
                return;
            }

            var additionalInfo = FormatExceptionForGitHub(_exception);
            KryptonGitHubIssueReportDialog.Show(this, config, additionalInfo);
        }
        else if (_bugReportCallback != null)
        {
            _bugReportCallback(_exception);
        }
    }

    private static string FormatExceptionForGitHub(Exception exception)
    {
        var sb = new StringBuilder();
        sb.AppendLine("**Exception from KryptonExceptionDialog**");
        sb.AppendLine();
        sb.AppendLine($"**Type:** {exception.GetType().FullName}");
        sb.AppendLine($"**Message:** {exception.Message}");
        sb.AppendLine();
        sb.AppendLine("**Stack Trace:**");
        sb.AppendLine("```");
        sb.AppendLine(exception.StackTrace ?? "(none)");
        sb.AppendLine("```");
        if (exception.InnerException != null)
        {
            sb.AppendLine();
            sb.AppendLine("**Inner Exception:**");
            sb.AppendLine($"Type: {exception.InnerException.GetType().FullName}");
            sb.AppendLine($"Message: {exception.InnerException.Message}");
        }
        return sb.ToString();
    }

    #endregion

    #region Show

    /// <summary>
    /// Displays a dialog that presents the specified exception to the user, with optional features for highlighting,
    /// copying details, submitting a bug report, and searching within the dialog.
    /// </summary>
    /// <param name="exception">The exception to display in the dialog. Provides details about the error that occurred.</param>
    /// <param name="highlightColor">An optional color used to highlight exception details in the dialog for improved visibility. If null, no
    /// highlighting is applied.</param>
    /// <param name="showCopyButton">Indicates whether a button is shown to allow the user to copy exception details to the clipboard. If null, the
    /// default behavior is used.</param>
    /// <param name="showSubmitBugReportButton">Indicates whether a button is shown to submit a bug report related to the exception. If null, the default
    /// behavior is used.</param>
    /// <param name="showSearchBox">Indicates whether a search box is included in the dialog for user queries. If null, the default behavior is
    /// used.</param>
    /// <param name="bugReportCallback">An optional callback that is invoked when the user submits a bug report. Receives the exception as a parameter.</param>
    /// <param name="showReportBugToGitHubButton">Indicates whether a button is shown to submit a bug report directly to GitHub. If null, the default
    /// <param name="gitHubSecretKey">An optional secret key used for authenticating with GitHub when submitting a bug report. If null, GitHub
    /// integration is not used.</param>
    /// <param name="gitHubConfigFilePath">An optional file path to a GitHub configuration file used when submitting a bug report. If null, the default
    /// configuration is used.</param>
    internal static void Show(Exception exception, Color? highlightColor, bool? showCopyButton, bool? showSubmitBugReportButton, bool? showSearchBox, Action<Exception>? bugReportCallback = null, bool? showReportBugToGitHubButton = null, string? gitHubSecretKey = null, string? gitHubConfigFilePath = null)
    {
        using var ved = new VisualExceptionDialogForm(showCopyButton, showSearchBox, showSubmitBugReportButton, highlightColor, exception, bugReportCallback, showReportBugToGitHubButton, gitHubSecretKey, gitHubConfigFilePath);

        ved.ShowDialog();
    }

    #endregion
}
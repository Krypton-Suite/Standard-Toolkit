#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, tobitege, Lesandro, KamaniAR, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Form for creating a GitHub issue with title and body. Does not follow any repository-specific issue template.
/// GitHub configuration (Owner, Repository, PAT) is loaded from an encrypted file — not shown to the user.
/// </summary>
internal partial class VisualGitHubIssueReportForm : KryptonForm
{
    private readonly BugReportGitHubService _githubService = new BugReportGitHubService();
    private readonly KryptonErrorProvider _errorProvider;
    private readonly BugReportGitHubConfig _config;

    /// <summary>
    /// Initializes the form with the provided GitHub configuration and optional initial description text.
    /// </summary>
    /// <param name="config">The GitHub configuration (Owner, RepositoryName, PersonalAccessToken). Must be valid.</param>
    /// <param name="initialDescription">Optional pre-filled text for the issue description (e.g. exception details).</param>
    public VisualGitHubIssueReportForm(BugReportGitHubConfig config, string? initialDescription = null)
    {
        if (config == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(config));
        }

        if (!config.IsValid)
        {
            ThrowHelper.ThrowInvalidOperationException("Config must have Owner, RepositoryName, and PersonalAccessToken set.");
        }

        _config = config;

        InitializeComponent();
        ApplyStrings();

        if (!string.IsNullOrEmpty(initialDescription))
        {
            krtbDescription.Text = initialDescription;
        }

        _errorProvider = new KryptonErrorProvider
        {
            ContainerControl = this,
            BlinkStyle = KryptonErrorBlinkStyle.BlinkIfDifferentError
        };
    }

    private static KryptonBugReportingDialogStrings Strings => KryptonBugReportingDialog.Strings;

    private void ApplyStrings()
    {
        var strings = Strings;
        Text = strings.GitHubWindowTitle;
        kwlblSummary.Text = strings.GitHubTitleLabel;
        kwlblDescription.Text = strings.GitHubDescriptionLabel;
        kbtnCreate.Values.Text = strings.GitHubCreateButton;
        kbtnCancel.Values.Text = strings.Cancel;
    }

    private bool ValidateInput()
    {
        _errorProvider.Clear();

        var valid = true;

        if (string.IsNullOrWhiteSpace(ktbSummary.Text))
        {
            _errorProvider.SetError(ktbSummary, Strings.GitHubTitleRequired);
            valid = false;
        }

        if (string.IsNullOrWhiteSpace(krtbDescription.Text))
        {
            _errorProvider.SetError(krtbDescription, Strings.GitHubDescriptionRequired);
            valid = false;
        }

        return valid;
    }

    private void kbtnCreate_Click(object sender, EventArgs e)
    {
        if (!ValidateInput())
        {
            return;
        }

        kbtnCreate.Enabled = false;
        kbtnCreate.Values.Text = Strings.GitHubCreating;
        Application.DoEvents();

        try
        {
            var result = _githubService.CreateIssue(_config, ktbSummary.Text.Trim(), krtbDescription.Text.Trim());

            if (result.Success)
            {
                if (!string.IsNullOrWhiteSpace(result.IssueUrl))
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = result.IssueUrl,
                        UseShellExecute = true
                    });
                }

                KryptonMessageBox.Show(
                    Strings.GitHubCreatedSuccess,
                    Strings.GitHubSuccessTitle,
                    KryptonMessageBoxButtons.OK,
                    KryptonMessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                KryptonMessageBox.Show(
                    result.ErrorMessage ?? Strings.GitHubCreateFailed,
                    Strings.GitHubCreateFailedTitle,
                    KryptonMessageBoxButtons.OK,
                    KryptonMessageBoxIcon.Error);
            }
        }
        finally
        {
            kbtnCreate.Enabled = true;
            kbtnCreate.Values.Text = Strings.GitHubCreateButton;
        }
    }

    private void kbtnCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        _errorProvider?.Clear();
        _errorProvider?.Dispose();
        base.OnFormClosed(e);
    }
}

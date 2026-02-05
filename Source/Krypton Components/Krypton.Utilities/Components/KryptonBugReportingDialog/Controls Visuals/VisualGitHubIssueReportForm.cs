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
            throw new ArgumentNullException(nameof(config));
        }

        if (!config.IsValid)
        {
            throw new InvalidOperationException("Config must have Owner, RepositoryName, and PersonalAccessToken set.");
        }

        _config = config;

        InitializeComponent();

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

    private bool ValidateInput()
    {
        _errorProvider.Clear();

        var valid = true;

        if (string.IsNullOrWhiteSpace(ktbSummary.Text))
        {
            _errorProvider.SetError(ktbSummary, "Title is required.");
            valid = false;
        }

        if (string.IsNullOrWhiteSpace(krtbDescription.Text))
        {
            _errorProvider.SetError(krtbDescription, "Description is required.");
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
        kbtnCreate.Values.Text = "Creating...";
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
                    "Bug report created successfully.",
                    "Success",
                    KryptonMessageBoxButtons.OK,
                    KryptonMessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                KryptonMessageBox.Show(
                    result.ErrorMessage ?? "Failed to create issue.",
                    "Create Issue Failed",
                    KryptonMessageBoxButtons.OK,
                    KryptonMessageBoxIcon.Error);
            }
        }
        finally
        {
            kbtnCreate.Enabled = true;
            kbtnCreate.Values.Text = "Create on GitHub";
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

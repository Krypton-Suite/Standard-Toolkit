#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, tobitege, Lesandro, KamaniAR, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Internal form for creating a bug report issue on the repository's GitHub issue tracker.
/// Fields match .github/ISSUE_TEMPLATE/bug_report.yml.
/// GitHub configuration (Owner, Repository, PAT) is loaded from an encrypted file — not shown to the user.
/// </summary>
internal partial class VisualGitHubIssueReportForm : KryptonForm
{
    private readonly BugReportGitHubService _githubService = new BugReportGitHubService();
    private readonly KryptonErrorProvider _errorProvider;
    private readonly BugReportGitHubConfig _config;
    private readonly string? _additionalInfoContext;

    /// <summary>
    /// Initializes the form with the provided GitHub configuration.
    /// </summary>
    /// <param name="config">The GitHub configuration (Owner, RepositoryName, PersonalAccessToken). Must be valid.</param>
    /// <param name="additionalInfoContext">Optional text to pre-fill the Additional Information field.</param>
    public VisualGitHubIssueReportForm(BugReportGitHubConfig config, string? additionalInfoContext = null)
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
        _additionalInfoContext = additionalInfoContext;

        InitializeComponent();

        _errorProvider = new KryptonErrorProvider
        {
            ContainerControl = this,
            BlinkStyle = KryptonErrorBlinkStyle.BlinkIfDifferentError
        };

        LoadDefaults();
    }

    private void LoadDefaults()
    {
        kcmbAreasAffected.SelectedIndex = -1;

        if (!string.IsNullOrEmpty(_additionalInfoContext))
        {
            krtbAdditionalInfo.Text = _additionalInfoContext;
        }

        try
        {
            if (string.IsNullOrWhiteSpace(ktbOs.Text))
            {
                ktbOs.Text = "Windows";
            }

            if (string.IsNullOrWhiteSpace(ktbOsVersion.Text))
            {
                ktbOsVersion.Text = Environment.OSVersion.Version.ToString();
            }

            if (string.IsNullOrWhiteSpace(ktbFrameworkVersion.Text))
            {
                var ver = Environment.Version;
                ktbFrameworkVersion.Text = $"{ver.Major}.{ver.Minor}";
            }
        }
        catch
        {
            // Ignore
        }
    }

    private BugReportGitHubContent GetContent()
    {
        return new BugReportGitHubContent
        {
            Summary = ktbSummary.Text?.Trim() ?? string.Empty,
            Description = krtbDescription.Text?.Trim() ?? string.Empty,
            StepsToReproduce = krtbStepsToReproduce.Text?.Trim() ?? string.Empty,
            ExpectedBehavior = krtbExpectedBehavior.Text?.Trim() ?? string.Empty,
            ActualBehavior = krtbActualBehavior.Text?.Trim() ?? string.Empty,
            OperatingSystem = ktbOs.Text?.Trim() ?? string.Empty,
            OsVersion = ktbOsVersion.Text?.Trim() ?? string.Empty,
            FrameworkVersion = ktbFrameworkVersion.Text?.Trim() ?? string.Empty,
            ToolkitVersion = ktbToolkitVersion.Text?.Trim() ?? string.Empty,
            AdditionalInformation = krtbAdditionalInfo.Text?.Trim() ?? string.Empty,
            AreasAffected = kcmbAreasAffected.SelectedItem?.ToString() ?? string.Empty
        };
    }

    private bool ValidateInput()
    {
        _errorProvider.Clear();

        var valid = true;

        if (string.IsNullOrWhiteSpace(ktbSummary.Text))
        {
            _errorProvider.SetError(ktbSummary, "Summary is required.");
            valid = false;
        }

        if (string.IsNullOrWhiteSpace(krtbDescription.Text))
        {
            _errorProvider.SetError(krtbDescription, "Description is required.");
            valid = false;
        }

        if (string.IsNullOrWhiteSpace(krtbStepsToReproduce.Text))
        {
            _errorProvider.SetError(krtbStepsToReproduce, "Steps to reproduce are required.");
            valid = false;
        }

        if (string.IsNullOrWhiteSpace(krtbExpectedBehavior.Text))
        {
            _errorProvider.SetError(krtbExpectedBehavior, "Expected behavior is required.");
            valid = false;
        }

        if (string.IsNullOrWhiteSpace(krtbActualBehavior.Text))
        {
            _errorProvider.SetError(krtbActualBehavior, "Actual behavior is required.");
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

        var content = GetContent();

        kbtnCreate.Enabled = false;
        kbtnCreate.Values.Text = "Creating...";
        Application.DoEvents();

        try
        {
            var result = _githubService.CreateIssue(_config, content);

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

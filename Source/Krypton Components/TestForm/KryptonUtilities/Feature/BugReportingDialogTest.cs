#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Utilities;

namespace TestForm;

public partial class BugReportingDialogTest : KryptonForm
{
    private readonly KryptonErrorProvider _errorProvider;

    public BugReportingDialogTest()
    {
        InitializeComponent();
        
        _errorProvider = new KryptonErrorProvider
        {
            ContainerControl = this,
            BlinkStyle = KryptonErrorBlinkStyle.BlinkIfDifferentError
        };
        
        LoadDefaultEmailConfig();
    }

    private void LoadDefaultEmailConfig()
    {
        ktbSmtpServer.Text = "smtp.example.com";
        ktbSmtpPort.Text = "587";
        kchkUseSsl.Checked = true;
        ktbFromEmail.Text = "app@example.com";
        ktbToEmail.Text = "bugs@example.com";
        ktbUsername.Text = "app@example.com";
        ktbPassword.Text = string.Empty;
    }

    private BugReportEmailConfig GetEmailConfig()
    {
        if (!int.TryParse(ktbSmtpPort.Text, out var port))
        {
            port = 587;
        }

        return new BugReportEmailConfig
        {
            SmtpServer = ktbSmtpServer.Text,
            SmtpPort = port,
            UseSsl = kchkUseSsl.Checked,
            FromEmail = ktbFromEmail.Text,
            ToEmail = ktbToEmail.Text,
            Username = ktbUsername.Text,
            Password = ktbPassword.Text
        };
    }

    private bool ValidateEmailConfig()
    {
        bool isValid = true;

        if (string.IsNullOrWhiteSpace(ktbSmtpServer.Text))
        {
            _errorProvider.SetError(ktbSmtpServer, "Please enter SMTP server address.");
            isValid = false;
        }
        else
        {
            _errorProvider.SetError(ktbSmtpServer, string.Empty);
        }

        if (string.IsNullOrWhiteSpace(ktbToEmail.Text))
        {
            _errorProvider.SetError(ktbToEmail, "Please enter recipient email address.");
            isValid = false;
        }
        else
        {
            _errorProvider.SetError(ktbToEmail, string.Empty);
        }

        return isValid;
    }

    private void ValidateSmtpServer()
    {
        if (string.IsNullOrWhiteSpace(ktbSmtpServer.Text))
        {
            _errorProvider.SetError(ktbSmtpServer, "Please enter SMTP server address.");
        }
        else
        {
            _errorProvider.SetError(ktbSmtpServer, string.Empty);
        }
    }

    private void ValidateToEmail()
    {
        if (string.IsNullOrWhiteSpace(ktbToEmail.Text))
        {
            _errorProvider.SetError(ktbToEmail, "Please enter recipient email address.");
        }
        else
        {
            _errorProvider.SetError(ktbToEmail, string.Empty);
        }
    }

    private void kbtnShowBugReport_Click(object sender, EventArgs e)
    {
        if (!ValidateEmailConfig())
        {
            return;
        }

        var emailConfig = GetEmailConfig();
        var result = KryptonBugReportingDialog.Show(emailConfig);
        
        if (result == DialogResult.OK)
        {
            MessageBox.Show("Bug report sent successfully!", "Success", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    private void kbtnShowBugReportWithException_Click(object sender, EventArgs e)
    {
        if (!ValidateEmailConfig())
        {
            return;
        }

        try
        {
            throw new InvalidOperationException("This is a test exception for bug reporting.");
        }
        catch (Exception ex)
        {
            var emailConfig = GetEmailConfig();
            var result = KryptonBugReportingDialog.Show(ex, emailConfig);
            
            if (result == DialogResult.OK)
            {
                MessageBox.Show("Bug report with exception sent successfully!", "Success", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }

    private void kbtnShowExceptionWithBugReporting_Click(object sender, EventArgs e)
    {
        if (!ValidateEmailConfig())
        {
            return;
        }

        try
        {
            throw new ArgumentOutOfRangeException("testParameter", 100, 
                "This is a test exception to demonstrate integrated bug reporting.");
        }
        catch (Exception ex)
        {
            var emailConfig = GetEmailConfig();
            BugReportingHelper.ShowExceptionWithBugReporting(
                ex, 
                emailConfig,
                highlightColor: Color.Orange,
                showCopyButton: true,
                showSearchBox: true
            );
        }
    }

    private void kbtnTestEmailConfig_Click(object sender, EventArgs e)
    {
        if (!ValidateEmailConfig())
        {
            return;
        }

        var emailConfig = GetEmailConfig();
        var service = new BugReportEmailService();
        
        kbtnTestEmailConfig.Enabled = false;
        kbtnTestEmailConfig.Text = "Testing...";
        Application.DoEvents();

        try
        {
            var success = service.SendBugReport(
                emailConfig,
                "Test Email - Bug Reporting Configuration",
                $"This is a test email sent at {DateTime.Now:yyyy-MM-dd HH:mm:ss}\n\n" +
                "If you receive this email, your SMTP configuration is working correctly.",
                null
            );

            if (success)
            {
                MessageBox.Show(
                    "Email configuration test successful!\n\n" +
                    "Please check the recipient email inbox to confirm receipt.",
                    "Test Successful",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            else
            {
                MessageBox.Show(
                    "Email configuration test failed.\n\n" +
                    "Please check:\n" +
                    "- SMTP server address and port\n" +
                    "- Username and password (if authentication is required)\n" +
                    "- SSL/TLS settings\n" +
                    "- Firewall and network connectivity",
                    "Test Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Error testing email configuration:\n\n{ex.Message}",
                "Test Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }
        finally
        {
            kbtnTestEmailConfig.Enabled = true;
            kbtnTestEmailConfig.Text = "Test Email Configuration";
        }
    }

    private void kbtnClose_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void ktbSmtpServer_TextChanged(object sender, EventArgs e)
    {
        ValidateSmtpServer();
    }

    private void ktbToEmail_TextChanged(object sender, EventArgs e)
    {
        ValidateToEmail();
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        _errorProvider?.Clear();
        _errorProvider?.Dispose();
        base.OnFormClosed(e);
    }
}


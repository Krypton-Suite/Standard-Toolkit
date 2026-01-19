#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

using System.Net;
using System.Net.Mail;

namespace Krypton.Utilities;

/// <summary>
/// Service class for sending bug report emails.
/// </summary>
public class BugReportEmailService
{
    #region Identity

    /// <summary>
    /// Initializes a new instance of the <see cref="BugReportEmailService"/> class.
    /// </summary>
    public BugReportEmailService()
    {
    }

    #endregion

    #region Public

    /// <summary>
    /// Sends a bug report email.
    /// </summary>
    /// <param name="config">The email configuration.</param>
    /// <param name="subject">The email subject.</param>
    /// <param name="body">The email body.</param>
    /// <param name="attachments">Optional list of file paths to attach.</param>
    /// <returns>True if the email was sent successfully; otherwise, false.</returns>
    public bool SendBugReport(BugReportEmailConfig config, string subject, string body, List<string>? attachments = null)
    {
        if (config == null)
        {
            throw new ArgumentNullException(nameof(config));
        }

        if (string.IsNullOrWhiteSpace(config.SmtpServer))
        {
            throw new InvalidOperationException("SMTP server is not configured.");
        }

        if (string.IsNullOrWhiteSpace(config.ToEmail))
        {
            throw new InvalidOperationException("Recipient email address is not configured.");
        }

        try
        {
            using var message = new MailMessage();
            message.From = new MailAddress(config.FromEmail);
            message.To.Add(config.ToEmail);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = false;

            if (attachments != null)
            {
                foreach (var attachmentPath in attachments)
                {
                    if (File.Exists(attachmentPath))
                    {
                        message.Attachments.Add(new Attachment(attachmentPath));
                    }
                }
            }

            using var client = new SmtpClient(config.SmtpServer, config.SmtpPort);
            client.EnableSsl = config.UseSsl;

            if (config.RequiresAuthentication)
            {
                client.Credentials = new NetworkCredential(config.Username, config.Password);
            }

            client.Send(message);
            return true;
        }
        catch
        {
            return false;
        }
    }

    #endregion
}


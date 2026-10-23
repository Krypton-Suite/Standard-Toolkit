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

namespace Krypton.Toolkit.Utilities;

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
            ThrowHelper.ThrowArgumentNullException(nameof(config));
        }

        if (string.IsNullOrWhiteSpace(config.SmtpServer))
        {
            ThrowHelper.ThrowInvalidOperationException("SMTP server is not configured.");
        }

        if (string.IsNullOrWhiteSpace(config.ToEmail))
        {
            ThrowHelper.ThrowInvalidOperationException("Recipient email address is not configured.");
        }

        try
        {
            using var message = new MailMessage();
            message.From = new MailAddress(config.FromEmail);
            message.To.Add(config.ToEmail);
            message.Subject = subject;
            // SMTP Username/Password authenticate the client only; they are never copied into the message.
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

    #region Internal

    /// <summary>
    /// Builds the plain-text body transmitted with a bug-report email.
    /// </summary>
    /// <param name="reporterEmail">Address of the person submitting the report.</param>
    /// <param name="bugDescription">User-authored description of the problem.</param>
    /// <param name="stepsToReproduce">User-authored reproduction steps.</param>
    /// <param name="exception">Optional exception. Only the type name and message are included.</param>
    /// <param name="attachmentPaths">Optional attachment paths; only file names are listed in the body.</param>
    /// <returns>The formatted email body.</returns>
    /// <remarks>
    /// SMTP credentials are not parameters and must never be included. Exception content is limited
    /// to type and message so stack traces and inner-exception chains are not transmitted.
    /// </remarks>
    internal static string CreateTransmittedBody(
        string reporterEmail,
        string bugDescription,
        string stepsToReproduce,
        Exception? exception,
        string[]? attachmentPaths)
    {
        var strings = KryptonBugReportingDialog.Strings;
        var sb = new StringBuilder(strings.EmailHeading);
        sb.AppendLine();
        sb.AppendLine(@"==========");
        sb.AppendLine();
        sb.AppendLine(KryptonBugReportingDialogStrings.Format(strings.ReportedByFormat, reporterEmail));
        sb.AppendLine(KryptonBugReportingDialogStrings.Format(strings.DateFormat, DateTimeOffset.Now.ToString("o")));
        sb.AppendLine();
        sb.AppendLine(strings.BugDescription);
        sb.AppendLine(@"----------------");
        sb.AppendLine(bugDescription);
        sb.AppendLine();
        sb.AppendLine(strings.StepsToReproduce);
        sb.AppendLine(@"-------------------");
        sb.AppendLine(stepsToReproduce);
        sb.AppendLine();

        if (exception != null)
        {
            sb.AppendLine(strings.EmailExceptionHeader);
            sb.AppendLine(@"-----------------");
            sb.AppendLine(KryptonBugReportingDialogStrings.Format(strings.ExceptionTypeFormat, exception.GetType().Name));
            sb.AppendLine(KryptonBugReportingDialogStrings.Format(strings.ExceptionMessageFormat, exception.Message));
            sb.AppendLine();
        }

        if (attachmentPaths != null && attachmentPaths.Length > 0)
        {
            sb.AppendLine(strings.Attachments);
            sb.AppendLine(@"-----------");
            foreach (var path in attachmentPaths)
            {
                sb.AppendLine(Path.GetFileName(path));
            }
        }

        return sb.ToString();
    }

    #endregion
}


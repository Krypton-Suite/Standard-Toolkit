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
/// Configuration class for bug report email settings.
/// </summary>
public class BugReportEmailConfig
{
    #region Identity

    /// <summary>
    /// Initializes a new instance of the <see cref="BugReportEmailConfig"/> class.
    /// </summary>
    public BugReportEmailConfig()
    {
        SmtpServer = string.Empty;
        SmtpPort = 587;
        UseSsl = true;
        FromEmail = string.Empty;
        ToEmail = string.Empty;
        Username = string.Empty;
        Password = string.Empty;
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the SMTP server address.
    /// </summary>
    public string SmtpServer { get; set; }

    /// <summary>
    /// Gets or sets the SMTP server port.
    /// </summary>
    public int SmtpPort { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to use SSL/TLS.
    /// </summary>
    public bool UseSsl { get; set; }

    /// <summary>
    /// Gets or sets the sender email address.
    /// </summary>
    public string FromEmail { get; set; }

    /// <summary>
    /// Gets or sets the recipient email address.
    /// </summary>
    public string ToEmail { get; set; }

    /// <summary>
    /// Gets or sets the SMTP username (if authentication is required).
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Gets or sets the SMTP password (if authentication is required).
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether SMTP authentication is required.
    /// </summary>
    public bool RequiresAuthentication => !string.IsNullOrWhiteSpace(Username);

    #endregion
}


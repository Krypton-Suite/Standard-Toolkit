#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>Exposes a general set of strings that are used within the Krypton bug reporting dialog, and are localisable.</summary>
/// <seealso cref="GlobalId" />
[TypeConverter(typeof(ExpandableObjectConverter))]
public class KryptonBugReportingDialogStrings : GlobalId
{
    #region Static Values

    private const string DEFAULT_BUG_REPORTING_DIALOG_WINDOW_TITLE = @"Report Bug";
    private const string DEFAULT_BUG_REPORTING_DIALOG_EMAIL_ADDRESS = @"Email Address:";
    private const string DEFAULT_BUG_REPORTING_DIALOG_BUG_DESCRIPTION = @"Bug Description:";
    private const string DEFAULT_BUG_REPORTING_DIALOG_STEPS_TO_REPRODUCE = @"Steps to Reproduce:";
    private const string DEFAULT_BUG_REPORTING_DIALOG_ATTACHMENTS = @"Attachments:";
    private const string DEFAULT_BUG_REPORTING_DIALOG_ADD_SCREENSHOT = @"Add Screenshot";
    private const string DEFAULT_BUG_REPORTING_DIALOG_ADD_FILE = @"Add File";
    private const string DEFAULT_BUG_REPORTING_DIALOG_REMOVE = @"Remove";
    private const string DEFAULT_BUG_REPORTING_DIALOG_SEND = @"Send Report";
    private const string DEFAULT_BUG_REPORTING_DIALOG_CANCEL = @"Cancel";
    private const string DEFAULT_BUG_REPORTING_DIALOG_SENDING = @"Sending...";
    private const string DEFAULT_BUG_REPORTING_DIALOG_SUCCESS_TITLE = @"Report Sent";
    private const string DEFAULT_BUG_REPORTING_DIALOG_SUCCESS_MESSAGE = @"Your bug report has been sent successfully. Thank you for your feedback!";
    private const string DEFAULT_BUG_REPORTING_DIALOG_ERROR_TITLE = @"Error Sending Report";
    private const string DEFAULT_BUG_REPORTING_DIALOG_ERROR_MESSAGE = @"An error occurred while sending the bug report. Please try again later.";
    private const string DEFAULT_BUG_REPORTING_DIALOG_INVALID_EMAIL = @"Please enter a valid email address.";
    private const string DEFAULT_BUG_REPORTING_DIALOG_REQUIRED_FIELDS = @"Please fill in all required fields.";
    private const string DEFAULT_GENERIC_ERROR_TITLE = @"Error";
    private const string DEFAULT_OPEN_ATTACHMENT_TOOLTIP_FORMAT = @"Double-click to open: {0}";
    private const string DEFAULT_UNABLE_TO_OPEN_FILE_FORMAT = @"Unable to open file: {0}";
    private const string DEFAULT_TEMPORARY_FILE_NOT_FOUND_FORMAT = @"Temporary file not found: {0}";
    private const string DEFAULT_FAILED_TO_CAPTURE_SCREENSHOT_FORMAT = @"Failed to capture screenshot: {0}";
    private const string DEFAULT_SELECT_FILES_TO_ATTACH = @"Select Files to Attach";
    private const string DEFAULT_EMAIL_CONFIGURATION_NOT_SET = @"Email configuration is not set. Please configure the recipient email address.";
    private const string DEFAULT_EMAIL_SUBJECT_FORMAT = @"Bug Report - {0}";
    private const string DEFAULT_EMAIL_HEADING = @"Bug Report";
    private const string DEFAULT_REPORTED_BY_FORMAT = @"Reported by: {0}";
    private const string DEFAULT_DATE_FORMAT = @"Date: {0}";
    private const string DEFAULT_EMAIL_EXCEPTION_HEADER = @"Exception Details:";
    private const string DEFAULT_EXCEPTION_TYPE_FORMAT = @"Exception Type: {0}";
    private const string DEFAULT_EXCEPTION_MESSAGE_FORMAT = @"Message: {0}";
    private const string DEFAULT_INNER_EXCEPTION_HEADER = @"Inner Exception:";
    private const string DEFAULT_GITHUB_WINDOW_TITLE = @"Create Issue on GitHub";
    private const string DEFAULT_GITHUB_TITLE_LABEL = @"Title:";
    private const string DEFAULT_GITHUB_DESCRIPTION_LABEL = @"Description:";
    private const string DEFAULT_GITHUB_TITLE_REQUIRED = @"Title is required.";
    private const string DEFAULT_GITHUB_DESCRIPTION_REQUIRED = @"Description is required.";
    private const string DEFAULT_GITHUB_CREATING = @"Creating...";
    private const string DEFAULT_GITHUB_CREATE_BUTTON = @"Create on GitHub";
    private const string DEFAULT_GITHUB_CREATED_SUCCESS = @"Bug report created successfully.";
    private const string DEFAULT_GITHUB_SUCCESS_TITLE = @"Success";
    private const string DEFAULT_GITHUB_CREATE_FAILED_TITLE = @"Create Issue Failed";
    private const string DEFAULT_GITHUB_CREATE_FAILED = @"Failed to create issue.";
    private const string DEFAULT_GITHUB_CONFIG_LOAD_FAILED = @"Failed to load GitHub configuration. The encrypted config file may be missing, corrupted, or the secret key is incorrect.";
    private const string DEFAULT_GITHUB_CONFIG_ERROR_TITLE = @"Configuration Error";

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="KryptonBugReportingDialogStrings" /> class.</summary>
    public KryptonBugReportingDialogStrings() => Reset();

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? "Modified" : string.Empty;

    #endregion

    #region IsDefault

    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsDefault => WindowTitle.Equals(DEFAULT_BUG_REPORTING_DIALOG_WINDOW_TITLE) &&
                             EmailAddress.Equals(DEFAULT_BUG_REPORTING_DIALOG_EMAIL_ADDRESS) &&
                             BugDescription.Equals(DEFAULT_BUG_REPORTING_DIALOG_BUG_DESCRIPTION) &&
                             StepsToReproduce.Equals(DEFAULT_BUG_REPORTING_DIALOG_STEPS_TO_REPRODUCE) &&
                             Attachments.Equals(DEFAULT_BUG_REPORTING_DIALOG_ATTACHMENTS) &&
                             AddScreenshot.Equals(DEFAULT_BUG_REPORTING_DIALOG_ADD_SCREENSHOT) &&
                             AddFile.Equals(DEFAULT_BUG_REPORTING_DIALOG_ADD_FILE) &&
                             Remove.Equals(DEFAULT_BUG_REPORTING_DIALOG_REMOVE) &&
                             Send.Equals(DEFAULT_BUG_REPORTING_DIALOG_SEND) &&
                             Cancel.Equals(DEFAULT_BUG_REPORTING_DIALOG_CANCEL) &&
                             Sending.Equals(DEFAULT_BUG_REPORTING_DIALOG_SENDING) &&
                             SuccessTitle.Equals(DEFAULT_BUG_REPORTING_DIALOG_SUCCESS_TITLE) &&
                             SuccessMessage.Equals(DEFAULT_BUG_REPORTING_DIALOG_SUCCESS_MESSAGE) &&
                             ErrorTitle.Equals(DEFAULT_BUG_REPORTING_DIALOG_ERROR_TITLE) &&
                             ErrorMessage.Equals(DEFAULT_BUG_REPORTING_DIALOG_ERROR_MESSAGE) &&
                             InvalidEmail.Equals(DEFAULT_BUG_REPORTING_DIALOG_INVALID_EMAIL) &&
                             RequiredFields.Equals(DEFAULT_BUG_REPORTING_DIALOG_REQUIRED_FIELDS) &&
                             GenericErrorTitle.Equals(DEFAULT_GENERIC_ERROR_TITLE) &&
                             OpenAttachmentTooltipFormat.Equals(DEFAULT_OPEN_ATTACHMENT_TOOLTIP_FORMAT) &&
                             UnableToOpenFileFormat.Equals(DEFAULT_UNABLE_TO_OPEN_FILE_FORMAT) &&
                             TemporaryFileNotFoundFormat.Equals(DEFAULT_TEMPORARY_FILE_NOT_FOUND_FORMAT) &&
                             FailedToCaptureScreenshotFormat.Equals(DEFAULT_FAILED_TO_CAPTURE_SCREENSHOT_FORMAT) &&
                             SelectFilesToAttach.Equals(DEFAULT_SELECT_FILES_TO_ATTACH) &&
                             EmailConfigurationNotSet.Equals(DEFAULT_EMAIL_CONFIGURATION_NOT_SET) &&
                             EmailSubjectFormat.Equals(DEFAULT_EMAIL_SUBJECT_FORMAT) &&
                             EmailHeading.Equals(DEFAULT_EMAIL_HEADING) &&
                             ReportedByFormat.Equals(DEFAULT_REPORTED_BY_FORMAT) &&
                             DateFormat.Equals(DEFAULT_DATE_FORMAT) &&
                             EmailExceptionHeader.Equals(DEFAULT_EMAIL_EXCEPTION_HEADER) &&
                             ExceptionTypeFormat.Equals(DEFAULT_EXCEPTION_TYPE_FORMAT) &&
                             ExceptionMessageFormat.Equals(DEFAULT_EXCEPTION_MESSAGE_FORMAT) &&
                             InnerExceptionHeader.Equals(DEFAULT_INNER_EXCEPTION_HEADER) &&
                             GitHubWindowTitle.Equals(DEFAULT_GITHUB_WINDOW_TITLE) &&
                             GitHubTitleLabel.Equals(DEFAULT_GITHUB_TITLE_LABEL) &&
                             GitHubDescriptionLabel.Equals(DEFAULT_GITHUB_DESCRIPTION_LABEL) &&
                             GitHubTitleRequired.Equals(DEFAULT_GITHUB_TITLE_REQUIRED) &&
                             GitHubDescriptionRequired.Equals(DEFAULT_GITHUB_DESCRIPTION_REQUIRED) &&
                             GitHubCreating.Equals(DEFAULT_GITHUB_CREATING) &&
                             GitHubCreateButton.Equals(DEFAULT_GITHUB_CREATE_BUTTON) &&
                             GitHubCreatedSuccess.Equals(DEFAULT_GITHUB_CREATED_SUCCESS) &&
                             GitHubSuccessTitle.Equals(DEFAULT_GITHUB_SUCCESS_TITLE) &&
                             GitHubCreateFailedTitle.Equals(DEFAULT_GITHUB_CREATE_FAILED_TITLE) &&
                             GitHubCreateFailed.Equals(DEFAULT_GITHUB_CREATE_FAILED) &&
                             GitHubConfigLoadFailed.Equals(DEFAULT_GITHUB_CONFIG_LOAD_FAILED) &&
                             GitHubConfigErrorTitle.Equals(DEFAULT_GITHUB_CONFIG_ERROR_TITLE);

    #endregion

    #region Public

    /// <summary>Gets or sets the window title for the bug reporting dialog.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The window title for the bug reporting dialog.")]
    [DefaultValue(DEFAULT_BUG_REPORTING_DIALOG_WINDOW_TITLE)]
    [RefreshProperties(RefreshProperties.All)]
    public string WindowTitle { get; set; }

    /// <summary>Gets or sets the email address label.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The email address label.")]
    [DefaultValue(DEFAULT_BUG_REPORTING_DIALOG_EMAIL_ADDRESS)]
    [RefreshProperties(RefreshProperties.All)]
    public string EmailAddress { get; set; }

    /// <summary>Gets or sets the bug description label.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The bug description label.")]
    [DefaultValue(DEFAULT_BUG_REPORTING_DIALOG_BUG_DESCRIPTION)]
    [RefreshProperties(RefreshProperties.All)]
    public string BugDescription { get; set; }

    /// <summary>Gets or sets the steps to reproduce label.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The steps to reproduce label.")]
    [DefaultValue(DEFAULT_BUG_REPORTING_DIALOG_STEPS_TO_REPRODUCE)]
    [RefreshProperties(RefreshProperties.All)]
    public string StepsToReproduce { get; set; }

    /// <summary>Gets or sets the attachments label.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The attachments label.")]
    [DefaultValue(DEFAULT_BUG_REPORTING_DIALOG_ATTACHMENTS)]
    [RefreshProperties(RefreshProperties.All)]
    public string Attachments { get; set; }

    /// <summary>Gets or sets the add screenshot button text.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The add screenshot button text.")]
    [DefaultValue(DEFAULT_BUG_REPORTING_DIALOG_ADD_SCREENSHOT)]
    [RefreshProperties(RefreshProperties.All)]
    public string AddScreenshot { get; set; }

    /// <summary>Gets or sets the add file button text.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The add file button text.")]
    [DefaultValue(DEFAULT_BUG_REPORTING_DIALOG_ADD_FILE)]
    [RefreshProperties(RefreshProperties.All)]
    public string AddFile { get; set; }

    /// <summary>Gets or sets the remove button text.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The remove button text.")]
    [DefaultValue(DEFAULT_BUG_REPORTING_DIALOG_REMOVE)]
    [RefreshProperties(RefreshProperties.All)]
    public string Remove { get; set; }

    /// <summary>Gets or sets the send button text.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The send button text.")]
    [DefaultValue(DEFAULT_BUG_REPORTING_DIALOG_SEND)]
    [RefreshProperties(RefreshProperties.All)]
    public string Send { get; set; }

    /// <summary>Gets or sets the cancel button text.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The cancel button text.")]
    [DefaultValue(DEFAULT_BUG_REPORTING_DIALOG_CANCEL)]
    [RefreshProperties(RefreshProperties.All)]
    public string Cancel { get; set; }

    /// <summary>Gets or sets the sending status text.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The sending status text.")]
    [DefaultValue(DEFAULT_BUG_REPORTING_DIALOG_SENDING)]
    [RefreshProperties(RefreshProperties.All)]
    public string Sending { get; set; }

    /// <summary>Gets or sets the success dialog title.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The success dialog title.")]
    [DefaultValue(DEFAULT_BUG_REPORTING_DIALOG_SUCCESS_TITLE)]
    [RefreshProperties(RefreshProperties.All)]
    public string SuccessTitle { get; set; }

    /// <summary>Gets or sets the success dialog message.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The success dialog message.")]
    [DefaultValue(DEFAULT_BUG_REPORTING_DIALOG_SUCCESS_MESSAGE)]
    [RefreshProperties(RefreshProperties.All)]
    public string SuccessMessage { get; set; }

    /// <summary>Gets or sets the error dialog title used when sending a report fails.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The error dialog title used when sending a report fails.")]
    [DefaultValue(DEFAULT_BUG_REPORTING_DIALOG_ERROR_TITLE)]
    [RefreshProperties(RefreshProperties.All)]
    public string ErrorTitle { get; set; }

    /// <summary>Gets or sets the error dialog message used when sending a report fails.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The error dialog message used when sending a report fails.")]
    [DefaultValue(DEFAULT_BUG_REPORTING_DIALOG_ERROR_MESSAGE)]
    [RefreshProperties(RefreshProperties.All)]
    public string ErrorMessage { get; set; }

    /// <summary>Gets or sets the invalid email error message.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The invalid email error message.")]
    [DefaultValue(DEFAULT_BUG_REPORTING_DIALOG_INVALID_EMAIL)]
    [RefreshProperties(RefreshProperties.All)]
    public string InvalidEmail { get; set; }

    /// <summary>Gets or sets the required fields error message.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The required fields error message.")]
    [DefaultValue(DEFAULT_BUG_REPORTING_DIALOG_REQUIRED_FIELDS)]
    [RefreshProperties(RefreshProperties.All)]
    public string RequiredFields { get; set; }

    /// <summary>Gets or sets the generic error caption used for file and screenshot failures.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The generic error caption used for file and screenshot failures.")]
    [DefaultValue(DEFAULT_GENERIC_ERROR_TITLE)]
    [RefreshProperties(RefreshProperties.All)]
    public string GenericErrorTitle { get; set; }

    /// <summary>Gets or sets the attachment tooltip format. Use <c>{0}</c> for the file name.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The attachment tooltip format. Use {0} for the file name.")]
    [DefaultValue(DEFAULT_OPEN_ATTACHMENT_TOOLTIP_FORMAT)]
    [RefreshProperties(RefreshProperties.All)]
    public string OpenAttachmentTooltipFormat { get; set; }

    /// <summary>Gets or sets the unable-to-open-file message format. Use <c>{0}</c> for the exception message.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The unable-to-open-file message format. Use {0} for the exception message.")]
    [DefaultValue(DEFAULT_UNABLE_TO_OPEN_FILE_FORMAT)]
    [RefreshProperties(RefreshProperties.All)]
    public string UnableToOpenFileFormat { get; set; }

    /// <summary>Gets or sets the temporary-file-not-found message format. Use <c>{0}</c> for the exception message.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The temporary-file-not-found message format. Use {0} for the exception message.")]
    [DefaultValue(DEFAULT_TEMPORARY_FILE_NOT_FOUND_FORMAT)]
    [RefreshProperties(RefreshProperties.All)]
    public string TemporaryFileNotFoundFormat { get; set; }

    /// <summary>Gets or sets the failed-to-capture-screenshot message format. Use <c>{0}</c> for the exception message.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The failed-to-capture-screenshot message format. Use {0} for the exception message.")]
    [DefaultValue(DEFAULT_FAILED_TO_CAPTURE_SCREENSHOT_FORMAT)]
    [RefreshProperties(RefreshProperties.All)]
    public string FailedToCaptureScreenshotFormat { get; set; }

    /// <summary>Gets or sets the add-file dialog title.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The add-file dialog title.")]
    [DefaultValue(DEFAULT_SELECT_FILES_TO_ATTACH)]
    [RefreshProperties(RefreshProperties.All)]
    public string SelectFilesToAttach { get; set; }

    /// <summary>Gets or sets the message shown when email configuration is missing.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The message shown when email configuration is missing.")]
    [DefaultValue(DEFAULT_EMAIL_CONFIGURATION_NOT_SET)]
    [RefreshProperties(RefreshProperties.All)]
    public string EmailConfigurationNotSet { get; set; }

    /// <summary>Gets or sets the email subject format. Use <c>{0}</c> for the timestamp.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The email subject format. Use {0} for the timestamp.")]
    [DefaultValue(DEFAULT_EMAIL_SUBJECT_FORMAT)]
    [RefreshProperties(RefreshProperties.All)]
    public string EmailSubjectFormat { get; set; }

    /// <summary>Gets or sets the email body heading.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The email body heading.")]
    [DefaultValue(DEFAULT_EMAIL_HEADING)]
    [RefreshProperties(RefreshProperties.All)]
    public string EmailHeading { get; set; }

    /// <summary>Gets or sets the reported-by line format. Use <c>{0}</c> for the reporter address.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The reported-by line format. Use {0} for the reporter address.")]
    [DefaultValue(DEFAULT_REPORTED_BY_FORMAT)]
    [RefreshProperties(RefreshProperties.All)]
    public string ReportedByFormat { get; set; }

    /// <summary>Gets or sets the date line format. Use <c>{0}</c> for the timestamp.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The date line format. Use {0} for the timestamp.")]
    [DefaultValue(DEFAULT_DATE_FORMAT)]
    [RefreshProperties(RefreshProperties.All)]
    public string DateFormat { get; set; }

    /// <summary>Gets or sets the exception-details section heading in the email body.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The exception-details section heading in the email body.")]
    [DefaultValue(DEFAULT_EMAIL_EXCEPTION_HEADER)]
    [RefreshProperties(RefreshProperties.All)]
    public string EmailExceptionHeader { get; set; }

    /// <summary>Gets or sets the exception type line format. Use <c>{0}</c> for the type name.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The exception type line format. Use {0} for the type name.")]
    [DefaultValue(DEFAULT_EXCEPTION_TYPE_FORMAT)]
    [RefreshProperties(RefreshProperties.All)]
    public string ExceptionTypeFormat { get; set; }

    /// <summary>Gets or sets the exception message line format. Use <c>{0}</c> for the message.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The exception message line format. Use {0} for the message.")]
    [DefaultValue(DEFAULT_EXCEPTION_MESSAGE_FORMAT)]
    [RefreshProperties(RefreshProperties.All)]
    public string ExceptionMessageFormat { get; set; }

    /// <summary>Gets or sets the inner-exception heading.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The inner-exception heading.")]
    [DefaultValue(DEFAULT_INNER_EXCEPTION_HEADER)]
    [RefreshProperties(RefreshProperties.All)]
    public string InnerExceptionHeader { get; set; }

    /// <summary>Gets or sets the GitHub issue dialog window title.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The GitHub issue dialog window title.")]
    [DefaultValue(DEFAULT_GITHUB_WINDOW_TITLE)]
    [RefreshProperties(RefreshProperties.All)]
    public string GitHubWindowTitle { get; set; }

    /// <summary>Gets or sets the GitHub issue title label.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The GitHub issue title label.")]
    [DefaultValue(DEFAULT_GITHUB_TITLE_LABEL)]
    [RefreshProperties(RefreshProperties.All)]
    public string GitHubTitleLabel { get; set; }

    /// <summary>Gets or sets the GitHub issue description label.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The GitHub issue description label.")]
    [DefaultValue(DEFAULT_GITHUB_DESCRIPTION_LABEL)]
    [RefreshProperties(RefreshProperties.All)]
    public string GitHubDescriptionLabel { get; set; }

    /// <summary>Gets or sets the GitHub title-required validation message.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The GitHub title-required validation message.")]
    [DefaultValue(DEFAULT_GITHUB_TITLE_REQUIRED)]
    [RefreshProperties(RefreshProperties.All)]
    public string GitHubTitleRequired { get; set; }

    /// <summary>Gets or sets the GitHub description-required validation message.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The GitHub description-required validation message.")]
    [DefaultValue(DEFAULT_GITHUB_DESCRIPTION_REQUIRED)]
    [RefreshProperties(RefreshProperties.All)]
    public string GitHubDescriptionRequired { get; set; }

    /// <summary>Gets or sets the GitHub creating status text.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The GitHub creating status text.")]
    [DefaultValue(DEFAULT_GITHUB_CREATING)]
    [RefreshProperties(RefreshProperties.All)]
    public string GitHubCreating { get; set; }

    /// <summary>Gets or sets the GitHub create button text.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The GitHub create button text.")]
    [DefaultValue(DEFAULT_GITHUB_CREATE_BUTTON)]
    [RefreshProperties(RefreshProperties.All)]
    public string GitHubCreateButton { get; set; }

    /// <summary>Gets or sets the GitHub issue-created success message.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The GitHub issue-created success message.")]
    [DefaultValue(DEFAULT_GITHUB_CREATED_SUCCESS)]
    [RefreshProperties(RefreshProperties.All)]
    public string GitHubCreatedSuccess { get; set; }

    /// <summary>Gets or sets the GitHub success dialog title.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The GitHub success dialog title.")]
    [DefaultValue(DEFAULT_GITHUB_SUCCESS_TITLE)]
    [RefreshProperties(RefreshProperties.All)]
    public string GitHubSuccessTitle { get; set; }

    /// <summary>Gets or sets the GitHub create-failed dialog title.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The GitHub create-failed dialog title.")]
    [DefaultValue(DEFAULT_GITHUB_CREATE_FAILED_TITLE)]
    [RefreshProperties(RefreshProperties.All)]
    public string GitHubCreateFailedTitle { get; set; }

    /// <summary>Gets or sets the fallback GitHub create-failed message.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The fallback GitHub create-failed message.")]
    [DefaultValue(DEFAULT_GITHUB_CREATE_FAILED)]
    [RefreshProperties(RefreshProperties.All)]
    public string GitHubCreateFailed { get; set; }

    /// <summary>Gets or sets the message shown when the encrypted GitHub config cannot be loaded.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The message shown when the encrypted GitHub config cannot be loaded.")]
    [DefaultValue(DEFAULT_GITHUB_CONFIG_LOAD_FAILED)]
    [RefreshProperties(RefreshProperties.All)]
    public string GitHubConfigLoadFailed { get; set; }

    /// <summary>Gets or sets the GitHub configuration-error dialog title.</summary>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The GitHub configuration-error dialog title.")]
    [DefaultValue(DEFAULT_GITHUB_CONFIG_ERROR_TITLE)]
    [RefreshProperties(RefreshProperties.All)]
    public string GitHubConfigErrorTitle { get; set; }

    #endregion

    #region Implementation

    /// <summary>Resets the strings.</summary>
    public void Reset()
    {
        WindowTitle = DEFAULT_BUG_REPORTING_DIALOG_WINDOW_TITLE;
        EmailAddress = DEFAULT_BUG_REPORTING_DIALOG_EMAIL_ADDRESS;
        BugDescription = DEFAULT_BUG_REPORTING_DIALOG_BUG_DESCRIPTION;
        StepsToReproduce = DEFAULT_BUG_REPORTING_DIALOG_STEPS_TO_REPRODUCE;
        Attachments = DEFAULT_BUG_REPORTING_DIALOG_ATTACHMENTS;
        AddScreenshot = DEFAULT_BUG_REPORTING_DIALOG_ADD_SCREENSHOT;
        AddFile = DEFAULT_BUG_REPORTING_DIALOG_ADD_FILE;
        Remove = DEFAULT_BUG_REPORTING_DIALOG_REMOVE;
        Send = DEFAULT_BUG_REPORTING_DIALOG_SEND;
        Cancel = DEFAULT_BUG_REPORTING_DIALOG_CANCEL;
        Sending = DEFAULT_BUG_REPORTING_DIALOG_SENDING;
        SuccessTitle = DEFAULT_BUG_REPORTING_DIALOG_SUCCESS_TITLE;
        SuccessMessage = DEFAULT_BUG_REPORTING_DIALOG_SUCCESS_MESSAGE;
        ErrorTitle = DEFAULT_BUG_REPORTING_DIALOG_ERROR_TITLE;
        ErrorMessage = DEFAULT_BUG_REPORTING_DIALOG_ERROR_MESSAGE;
        InvalidEmail = DEFAULT_BUG_REPORTING_DIALOG_INVALID_EMAIL;
        RequiredFields = DEFAULT_BUG_REPORTING_DIALOG_REQUIRED_FIELDS;
        GenericErrorTitle = DEFAULT_GENERIC_ERROR_TITLE;
        OpenAttachmentTooltipFormat = DEFAULT_OPEN_ATTACHMENT_TOOLTIP_FORMAT;
        UnableToOpenFileFormat = DEFAULT_UNABLE_TO_OPEN_FILE_FORMAT;
        TemporaryFileNotFoundFormat = DEFAULT_TEMPORARY_FILE_NOT_FOUND_FORMAT;
        FailedToCaptureScreenshotFormat = DEFAULT_FAILED_TO_CAPTURE_SCREENSHOT_FORMAT;
        SelectFilesToAttach = DEFAULT_SELECT_FILES_TO_ATTACH;
        EmailConfigurationNotSet = DEFAULT_EMAIL_CONFIGURATION_NOT_SET;
        EmailSubjectFormat = DEFAULT_EMAIL_SUBJECT_FORMAT;
        EmailHeading = DEFAULT_EMAIL_HEADING;
        ReportedByFormat = DEFAULT_REPORTED_BY_FORMAT;
        DateFormat = DEFAULT_DATE_FORMAT;
        EmailExceptionHeader = DEFAULT_EMAIL_EXCEPTION_HEADER;
        ExceptionTypeFormat = DEFAULT_EXCEPTION_TYPE_FORMAT;
        ExceptionMessageFormat = DEFAULT_EXCEPTION_MESSAGE_FORMAT;
        InnerExceptionHeader = DEFAULT_INNER_EXCEPTION_HEADER;
        GitHubWindowTitle = DEFAULT_GITHUB_WINDOW_TITLE;
        GitHubTitleLabel = DEFAULT_GITHUB_TITLE_LABEL;
        GitHubDescriptionLabel = DEFAULT_GITHUB_DESCRIPTION_LABEL;
        GitHubTitleRequired = DEFAULT_GITHUB_TITLE_REQUIRED;
        GitHubDescriptionRequired = DEFAULT_GITHUB_DESCRIPTION_REQUIRED;
        GitHubCreating = DEFAULT_GITHUB_CREATING;
        GitHubCreateButton = DEFAULT_GITHUB_CREATE_BUTTON;
        GitHubCreatedSuccess = DEFAULT_GITHUB_CREATED_SUCCESS;
        GitHubSuccessTitle = DEFAULT_GITHUB_SUCCESS_TITLE;
        GitHubCreateFailedTitle = DEFAULT_GITHUB_CREATE_FAILED_TITLE;
        GitHubCreateFailed = DEFAULT_GITHUB_CREATE_FAILED;
        GitHubConfigLoadFailed = DEFAULT_GITHUB_CONFIG_LOAD_FAILED;
        GitHubConfigErrorTitle = DEFAULT_GITHUB_CONFIG_ERROR_TITLE;
    }

    /// <summary>Formats <paramref name="format"/> using the current UI culture.</summary>
    /// <param name="format">A composite format string.</param>
    /// <param name="args">Format arguments.</param>
    internal static string Format(string format, params object[] args) =>
        string.Format(System.Globalization.CultureInfo.CurrentCulture, format, args);

    #endregion
}

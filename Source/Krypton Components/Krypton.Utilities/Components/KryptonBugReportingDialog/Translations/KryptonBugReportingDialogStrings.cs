#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

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

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="KryptonBugReportingDialogStrings" /> class.</summary>
    public KryptonBugReportingDialogStrings()
    {
        Reset();
    }

    /// <summary>Converts to string.</summary>
    /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
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
                             RequiredFields.Equals(DEFAULT_BUG_REPORTING_DIALOG_REQUIRED_FIELDS);

    #endregion

    #region Public

    /// <summary>Gets or sets the window title for the bug reporting dialog.</summary>
    /// <value>The window title.</value>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The window title for the bug reporting dialog.")]
    [DefaultValue(DEFAULT_BUG_REPORTING_DIALOG_WINDOW_TITLE)]
    [RefreshProperties(RefreshProperties.All)]
    public string WindowTitle { get; set; }

    /// <summary>Gets or sets the email address label.</summary>
    /// <value>The email address label.</value>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The email address label.")]
    [DefaultValue(DEFAULT_BUG_REPORTING_DIALOG_EMAIL_ADDRESS)]
    [RefreshProperties(RefreshProperties.All)]
    public string EmailAddress { get; set; }

    /// <summary>Gets or sets the bug description label.</summary>
    /// <value>The bug description label.</value>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The bug description label.")]
    [DefaultValue(DEFAULT_BUG_REPORTING_DIALOG_BUG_DESCRIPTION)]
    [RefreshProperties(RefreshProperties.All)]
    public string BugDescription { get; set; }

    /// <summary>Gets or sets the steps to reproduce label.</summary>
    /// <value>The steps to reproduce label.</value>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The steps to reproduce label.")]
    [DefaultValue(DEFAULT_BUG_REPORTING_DIALOG_STEPS_TO_REPRODUCE)]
    [RefreshProperties(RefreshProperties.All)]
    public string StepsToReproduce { get; set; }

    /// <summary>Gets or sets the attachments label.</summary>
    /// <value>The attachments label.</value>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The attachments label.")]
    [DefaultValue(DEFAULT_BUG_REPORTING_DIALOG_ATTACHMENTS)]
    [RefreshProperties(RefreshProperties.All)]
    public string Attachments { get; set; }

    /// <summary>Gets or sets the add screenshot button text.</summary>
    /// <value>The add screenshot button text.</value>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The add screenshot button text.")]
    [DefaultValue(DEFAULT_BUG_REPORTING_DIALOG_ADD_SCREENSHOT)]
    [RefreshProperties(RefreshProperties.All)]
    public string AddScreenshot { get; set; }

    /// <summary>Gets or sets the add file button text.</summary>
    /// <value>The add file button text.</value>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The add file button text.")]
    [DefaultValue(DEFAULT_BUG_REPORTING_DIALOG_ADD_FILE)]
    [RefreshProperties(RefreshProperties.All)]
    public string AddFile { get; set; }

    /// <summary>Gets or sets the remove button text.</summary>
    /// <value>The remove button text.</value>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The remove button text.")]
    [DefaultValue(DEFAULT_BUG_REPORTING_DIALOG_REMOVE)]
    [RefreshProperties(RefreshProperties.All)]
    public string Remove { get; set; }

    /// <summary>Gets or sets the send button text.</summary>
    /// <value>The send button text.</value>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The send button text.")]
    [DefaultValue(DEFAULT_BUG_REPORTING_DIALOG_SEND)]
    [RefreshProperties(RefreshProperties.All)]
    public string Send { get; set; }

    /// <summary>Gets or sets the cancel button text.</summary>
    /// <value>The cancel button text.</value>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The cancel button text.")]
    [DefaultValue(DEFAULT_BUG_REPORTING_DIALOG_CANCEL)]
    [RefreshProperties(RefreshProperties.All)]
    public string Cancel { get; set; }

    /// <summary>Gets or sets the sending status text.</summary>
    /// <value>The sending status text.</value>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The sending status text.")]
    [DefaultValue(DEFAULT_BUG_REPORTING_DIALOG_SENDING)]
    [RefreshProperties(RefreshProperties.All)]
    public string Sending { get; set; }

    /// <summary>Gets or sets the success dialog title.</summary>
    /// <value>The success dialog title.</value>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The success dialog title.")]
    [DefaultValue(DEFAULT_BUG_REPORTING_DIALOG_SUCCESS_TITLE)]
    [RefreshProperties(RefreshProperties.All)]
    public string SuccessTitle { get; set; }

    /// <summary>Gets or sets the success dialog message.</summary>
    /// <value>The success dialog message.</value>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The success dialog message.")]
    [DefaultValue(DEFAULT_BUG_REPORTING_DIALOG_SUCCESS_MESSAGE)]
    [RefreshProperties(RefreshProperties.All)]
    public string SuccessMessage { get; set; }

    /// <summary>Gets or sets the error dialog title.</summary>
    /// <value>The error dialog title.</value>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The error dialog title.")]
    [DefaultValue(DEFAULT_BUG_REPORTING_DIALOG_ERROR_TITLE)]
    [RefreshProperties(RefreshProperties.All)]
    public string ErrorTitle { get; set; }

    /// <summary>Gets or sets the error dialog message.</summary>
    /// <value>The error dialog message.</value>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The error dialog message.")]
    [DefaultValue(DEFAULT_BUG_REPORTING_DIALOG_ERROR_MESSAGE)]
    [RefreshProperties(RefreshProperties.All)]
    public string ErrorMessage { get; set; }

    /// <summary>Gets or sets the invalid email error message.</summary>
    /// <value>The invalid email error message.</value>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The invalid email error message.")]
    [DefaultValue(DEFAULT_BUG_REPORTING_DIALOG_INVALID_EMAIL)]
    [RefreshProperties(RefreshProperties.All)]
    public string InvalidEmail { get; set; }

    /// <summary>Gets or sets the required fields error message.</summary>
    /// <value>The required fields error message.</value>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The required fields error message.")]
    [DefaultValue(DEFAULT_BUG_REPORTING_DIALOG_REQUIRED_FIELDS)]
    [RefreshProperties(RefreshProperties.All)]
    public string RequiredFields { get; set; }

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
    }

    #endregion
}


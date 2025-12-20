#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

// Used only in Visual Bug Reporting Dialog
using System.Net.Mail;

namespace Krypton.Utilities;

public partial class VisualBugReportingDialogForm : KryptonForm
{
    #region Instance Fields

    private readonly Exception? _exception;
    private readonly BugReportEmailConfig? _emailConfig;
    private readonly List<string> _attachmentPaths = new List<string>();
    private readonly BugReportEmailService _emailService = new BugReportEmailService();
    private readonly Dictionary<string, Bitmap> _thumbnailCache = new Dictionary<string, Bitmap>();
    private readonly KryptonErrorProvider _errorProvider;

    #endregion

    #region Identity

    /// <summary>
    /// Initializes a new instance of the <see cref="VisualBugReportingDialogForm"/> class with the specified exception and email configuration.
    /// </summary>
    /// <param name="exception">The exception to be reported, or null if no exception is associated with the bug report.</param>
    /// <param name="emailConfig">The configuration settings for sending the bug report via email, or null to use default settings.</param>
    public VisualBugReportingDialogForm(Exception? exception, BugReportEmailConfig? emailConfig)
    {
        InitializeComponent();

        SetInheritedControlOverride();

        _exception = exception;
        _emailConfig = emailConfig;

        _errorProvider = new KryptonErrorProvider
        {
            ContainerControl = this,
            BlinkStyle = KryptonErrorBlinkStyle.BlinkIfDifferentError,
            IconAlignment = KryptonErrorIconAlignment.MiddleLeft
        };

        SetupUI();
    }

    #endregion

    #region Implementation

    private static readonly KryptonBugReportingDialogStrings _defaultStrings = new KryptonBugReportingDialogStrings();

    /// <summary>
    /// Initializes the user interface elements with default text and updates their state based on the current context.
    /// </summary>
    /// <remarks>Call this method to refresh the UI labels, button text, and control states, typically after
    /// changing localization resources or relevant data. This method also populates the bug description field with
    /// exception details if an exception is present.</remarks>
    private void SetupUI()
    {
        var strings = _defaultStrings;

        Text = strings.WindowTitle;
        kwlblEmailAddress.Text = strings.EmailAddress;
        kwlblBugDescription.Text = strings.BugDescription;
        kwlblStepsToReproduce.Text = strings.StepsToReproduce;
        kwlblAttachments.Text = strings.Attachments;
        kbtnAddScreenshot.Text = strings.AddScreenshot;
        kbtnAddFile.Text = strings.AddFile;
        kbtnRemove.Text = strings.Remove;
        kbtnSend.Text = strings.Send;
        kbtnCancel.Text = strings.Cancel;

        if (_exception != null)
        {
            krtbBugDescription.Text = FormatExceptionDetails(_exception);
        }

        UpdateAttachmentList();
        UpdateSendButtonState();
    }

    /// <summary>
    /// Formats detailed information about the specified exception into a readable string.
    /// </summary>
    /// <param name="exception">The exception to format. Cannot be null.</param>
    /// <returns>A string containing the exception type, message, stack trace, and inner exception details, if present.</returns>
    private string FormatExceptionDetails(Exception exception)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Exception Type: {exception.GetType().Name}");
        sb.AppendLine($"Message: {exception.Message}");
        sb.AppendLine();
        sb.AppendLine("Stack Trace:");
        sb.AppendLine(exception.StackTrace ?? "N/A");
        if (exception.InnerException != null)
        {
            sb.AppendLine();
            sb.AppendLine("Inner Exception:");
            sb.AppendLine(exception.InnerException.Message);
        }
        return sb.ToString();
    }

    /// <summary>
    /// Refreshes the list of attachment thumbnails displayed in the user interface to reflect the current set of attachment paths.
    /// </summary>
    /// <remarks>This method clears the existing attachment controls and recreates them based on the current
    /// collection of attachment paths. It also updates the enabled state of the remove button depending on whether any
    /// attachments are present.</remarks>
    private void UpdateAttachmentList()
    {
        flpAttachments.Controls.Clear();

        foreach (var path in _attachmentPaths)
        {
            var thumbnailPanel = CreateThumbnailPanel(path);
            flpAttachments.Controls.Add(thumbnailPanel);
        }

        kbtnRemove.Enabled = _attachmentPaths.Count > 0;
    }

    /// <summary>
    /// Creates a panel control that displays a thumbnail preview, file name, and a remove button for the specified file.
    /// </summary>
    /// <remarks>Double-clicking the panel, thumbnail image, or file name label opens the associated file
    /// using the default application. The remove button removes the file from the attachment list. The panel's Tag
    /// property is set to the provided file path.</remarks>
    /// <param name="filePath">The full path to the file to display in the thumbnail panel. Cannot be null or empty.</param>
    /// <returns>A Panel control containing the file's thumbnail image, file name label, and a button to remove the attachment.</returns>
    private Panel CreateThumbnailPanel(string filePath)
    {
        var panel = new KryptonPanel
        {
            Size = new Size(140, 160),
            Margin = new Padding(5),
            BorderStyle = BorderStyle.FixedSingle,
            BackColor = Color.White,
            Tag = filePath,
            Cursor = Cursors.Hand
        };

        var pictureBox = new PictureBox
        {
            Size = new Size(128, 128),
            Location = new Point(5, 5),
            SizeMode = PictureBoxSizeMode.Zoom,
            BorderStyle = BorderStyle.None,
            Cursor = Cursors.Hand
        };

        var fileName = Path.GetFileName(filePath);
        var label = new KryptonLabel
        {
            Text = fileName.Length > 18 ? fileName.Substring(0, 15) + "..." : fileName,
            Location = new Point(5, 136),
            Size = new Size(130, 20),
            AutoSize = false,
            Cursor = Cursors.Hand
        };
        label.StateCommon.ShortText.TextH = PaletteRelativeAlign.Near;
        label.StateCommon.ShortText.TextV = PaletteRelativeAlign.Near;

        var removeButton = new KryptonButton
        {
            Text = "×",
            Size = new Size(20, 20),
            Location = new Point(115, 2),
            ButtonStyle = ButtonStyle.LowProfile,
            Cursor = Cursors.Hand,
            TabStop = false
        };
        removeButton.StateCommon.Back.Color1 = Color.Red;
        removeButton.StateCommon.Back.Color2 = Color.Red;
        removeButton.StateCommon.Content.ShortText.Color1 = Color.White;
        removeButton.StateCommon.Content.ShortText.Font = new Font("Arial", 12, FontStyle.Bold);
        removeButton.StateCommon.Border.Draw = InheritBool.False;
        removeButton.StateTracking.Back.Color1 = Color.DarkRed;
        removeButton.StateTracking.Back.Color2 = Color.DarkRed;
        removeButton.StatePressed.Back.Color1 = Color.DarkRed;
        removeButton.StatePressed.Back.Color2 = Color.DarkRed;
        removeButton.Click += (s, e) => RemoveAttachment(filePath);

        pictureBox.DoubleClick += (s, e) => OpenAttachment(filePath);
        label.DoubleClick += (s, e) => OpenAttachment(filePath);
        panel.DoubleClick += (s, e) => OpenAttachment(filePath);

        pictureBox.MouseEnter += (s, e) => panel.BackColor = Color.LightGray;
        pictureBox.MouseLeave += (s, e) => panel.BackColor = Color.White;
        label.MouseEnter += (s, e) => panel.BackColor = Color.LightGray;
        label.MouseLeave += (s, e) => panel.BackColor = Color.White;

        var toolTip = new ToolTip();
        toolTip.SetToolTip(panel, $"Double-click to open: {fileName}");
        toolTip.SetToolTip(pictureBox, $"Double-click to open: {fileName}");
        toolTip.SetToolTip(label, $"Double-click to open: {fileName}");

        panel.Controls.Add(pictureBox);
        panel.Controls.Add(label);
        panel.Controls.Add(removeButton);

        LoadThumbnail(pictureBox, filePath);

        return panel;
    }

    /// <summary>
    /// Attempts to open the specified file using the default application associated with its file type.
    /// </summary>
    /// <remarks>If the file does not exist, no action is taken. If the file cannot be opened, an error
    /// message is displayed to the user.</remarks>
    /// <param name="filePath">The full path to the file to open. Must refer to an existing file.</param>
    private void OpenAttachment(string filePath)
    {
        if (File.Exists(filePath))
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show($"Unable to open file: {ex.Message}", "Error",
                    KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
            }
        }
    }

    /// <summary>
    /// Loads a thumbnail image from the specified file path and assigns it to the provided PictureBox control.
    /// </summary>
    /// <remarks>If a cached thumbnail is available for the specified file path, it is used; otherwise, a new
    /// thumbnail is generated and cached for future use. If the thumbnail cannot be loaded or an error occurs, the
    /// PictureBox image is set to null.</remarks>
    /// <param name="pictureBox">The PictureBox control to display the loaded thumbnail image. Cannot be null.</param>
    /// <param name="filePath">The file system path of the image for which to load a thumbnail. Cannot be null or empty.</param>
    private void LoadThumbnail(PictureBox pictureBox, string filePath)
    {
        try
        {
            Bitmap? thumbnail = null;

            if (_thumbnailCache.TryGetValue(filePath, out var cachedThumbnail))
            {
                thumbnail = cachedThumbnail;
            }
            else
            {
                thumbnail = ThumbnailHelper.GenerateThumbnail(filePath, 128);
                if (thumbnail != null)
                {
                    _thumbnailCache[filePath] = thumbnail;
                }
            }

            pictureBox.Image = thumbnail ?? null;
        }
        catch
        {
            pictureBox.Image = null;
        }
    }

    /// <summary>
    /// Removes the specified attachment and its associated resources from the collection.
    /// </summary>
    /// <remarks>If the attachment file is located in the system's temporary directory, the file is deleted
    /// from disk. Any cached thumbnail associated with the attachment is also disposed and removed. No action is taken
    /// if the specified file path does not exist in the collection.</remarks>
    /// <param name="filePath">The full file path of the attachment to remove. Must not be null or empty.</param>
    private void RemoveAttachment(string filePath)
    {
        if (_attachmentPaths.Contains(filePath))
        {
            if (filePath.StartsWith(Path.GetTempPath()))
            {
                try
                {
                    File.Delete(filePath);
                }
                catch (FileNotFoundException e)
                {
                    KryptonMessageBox.Show($"Temporary file not found: {e.Message}", "Error",
                        KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
                }
            }

            if (_thumbnailCache.TryGetValue(filePath, out var thumbnail))
            {
                thumbnail.Dispose();
                _thumbnailCache.Remove(filePath);
            }

            _attachmentPaths.Remove(filePath);
            UpdateAttachmentList();
        }
    }

    /// <summary>
    /// Updates the enabled state of the Send button based on the validity of the email address, bug description, and email configuration.
    /// </summary>
    /// <remarks>The Send button is enabled only when both the email address and bug description fields are
    /// not empty, contain no validation errors, and a valid email configuration with a non-empty recipient address is
    /// present.</remarks>
    private void UpdateSendButtonState()
    {
        bool hasErrors = !string.IsNullOrEmpty(_errorProvider.GetError(ktbEmailAddress)) ||
                         !string.IsNullOrEmpty(_errorProvider.GetError(krtbBugDescription));

        kbtnSend.Enabled = !string.IsNullOrWhiteSpace(ktbEmailAddress.Text) &&
                          !string.IsNullOrWhiteSpace(krtbBugDescription.Text) &&
                          !hasErrors &&
                          _emailConfig != null &&
                          !string.IsNullOrWhiteSpace(_emailConfig.ToEmail);
    }

    /// <summary>
    /// Captures a screenshot of the primary screen and adds it as an attachment for the current bug report.
    /// </summary>
    /// <remarks>The screenshot is saved as a PNG file in the system's temporary directory and is
    /// automatically added to the list of attachments. If the primary screen cannot be determined, a default resolution
    /// of 1920x1080 is used. An error message is displayed if the screenshot cannot be captured or saved.</remarks>
    private void CaptureScreenshot()
    {
        try
        {
            var screenBounds = Screen.PrimaryScreen?.Bounds ?? new Rectangle(0, 0, 1920, 1080);
            var bmp = new Bitmap(screenBounds.Width, screenBounds.Height);
            using (var g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(screenBounds.Left, screenBounds.Top, 0, 0, bmp.Size);
            }

            var tempPath = Path.Combine(Path.GetTempPath(), $"BugReport_Screenshot_{DateTime.Now:yyyyMMdd_HHmmss}.png");
            bmp.Save(tempPath, ImageFormat.Png);
            bmp.Dispose();

            _attachmentPaths.Add(tempPath);
            UpdateAttachmentList();
        }
        catch (Exception ex)
        {
            KryptonMessageBox.Show($"Failed to capture screenshot: {ex.Message}", "Error", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// Displays a file selection dialog that allows the user to select one or more files to attach. Adds the selected files to the attachment list if they are not already present.
    /// </summary>
    /// <remarks>This method opens a standard file dialog with multi-selection enabled. Only files that are
    /// not already in the attachment list are added. The attachment list is updated after new files are added. This
    /// method is intended to be called from a UI context and requires a valid window handle for dialog
    /// ownership.</remarks>
    private void AddFile()
    {
        using var dialog = new OpenFileDialog();
        dialog.Multiselect = true;
        dialog.Title = "Select Files to Attach";

        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            foreach (var fileName in dialog.FileNames)
            {
                if (!_attachmentPaths.Contains(fileName))
                {
                    _attachmentPaths.Add(fileName);
                }
            }
            UpdateAttachmentList();
        }
    }

    /// <summary>
    /// Validates the user input fields for email address and bug description, and checks that the email configuration is set.
    /// </summary>
    /// <remarks>If validation fails, error messages are displayed next to the relevant input fields or as a
    /// message box. This method is typically called before submitting or processing user input to ensure all required
    /// information is present and correctly formatted.</remarks>
    /// <returns>true if all required fields are valid and the email configuration is set; otherwise, false.</returns>
    private bool ValidateInput()
    {
        var strings = _defaultStrings;
        bool isValid = true;

        if (string.IsNullOrWhiteSpace(ktbEmailAddress.Text))
        {
            _errorProvider.SetError(ktbEmailAddress, strings.RequiredFields);
            isValid = false;
        }
        else if (!IsValidEmail(ktbEmailAddress.Text))
        {
            _errorProvider.SetError(ktbEmailAddress, strings.InvalidEmail);
            isValid = false;
        }
        else
        {
            _errorProvider.SetError(ktbEmailAddress, string.Empty);
        }

        if (string.IsNullOrWhiteSpace(krtbBugDescription.Text))
        {
            _errorProvider.SetError(krtbBugDescription, strings.RequiredFields);
            isValid = false;
        }
        else
        {
            _errorProvider.SetError(krtbBugDescription, string.Empty);
        }

        if (_emailConfig == null || string.IsNullOrWhiteSpace(_emailConfig.ToEmail))
        {
            KryptonMessageBox.Show("Email configuration is not set. Please configure the recipient email address.", strings.ErrorTitle, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
            isValid = false;
        }

        return isValid;
    }

    /// <summary>
    /// Validates the email address entered by the user and updates the associated error provider and send button state.
    /// </summary>
    /// <remarks>Displays an error message if the email address field is empty or contains an invalid email
    /// address format. Clears the error if the input is valid. This method is typically called in response to user
    /// input events to provide immediate feedback.</remarks>
    private void ValidateEmailAddress()
    {
        var strings = _defaultStrings;

        if (string.IsNullOrWhiteSpace(ktbEmailAddress.Text))
        {
            _errorProvider.SetError(ktbEmailAddress, strings.RequiredFields);
        }
        else if (!IsValidEmail(ktbEmailAddress.Text))
        {
            _errorProvider.SetError(ktbEmailAddress, strings.InvalidEmail);
        }
        else
        {
            _errorProvider.SetError(ktbEmailAddress, string.Empty);
        }

        UpdateSendButtonState();
    }

    /// <summary>
    /// Validates the bug description input and updates the associated error state.
    /// </summary>
    /// <remarks>If the bug description field is empty or contains only whitespace, an error message is
    /// displayed to prompt the user to provide a value. Otherwise, any existing error is cleared. This method also
    /// updates the enabled state of the send button based on the current validation result.</remarks>
    private void ValidateBugDescription()
    {
        var strings = _defaultStrings;

        _errorProvider.SetError(krtbBugDescription,
            string.IsNullOrWhiteSpace(krtbBugDescription.Text) ? strings.RequiredFields : string.Empty);

        UpdateSendButtonState();
    }

    /// <summary>
    /// Determines whether the specified string is a valid email address format.
    /// </summary>
    /// <remarks>This method checks the format of the email address but does not verify that the address
    /// exists or is reachable.</remarks>
    /// <param name="email">The email address to validate. Cannot be null.</param>
    /// <returns>true if the specified string is a valid email address format; otherwise, false.</returns>
    private static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Sends a bug report using the configured email service and displays a status message to the user.
    /// </summary>
    /// <remarks>This method validates the input before attempting to send the bug report. It disables the
    /// Send button during the operation to prevent duplicate submissions and provides user feedback based on the
    /// result. If the bug report is sent successfully, the dialog result is set to OK; otherwise, an error message is
    /// displayed. This method is intended to be called in response to a user action, such as clicking a Send button in
    /// a bug report form.</remarks>
    private void SendBugReport()
    {
        if (!ValidateInput())
        {
            return;
        }

        var strings = _defaultStrings;

        kbtnSend.Enabled = false;
        kbtnSend.Text = strings.Sending;
        Application.DoEvents();

        try
        {
            var subject = $"Bug Report - {DateTime.Now:yyyy-MM-dd HH:mm:ss}";
            var body = BuildEmailBody();

            var success = _emailService.SendBugReport(_emailConfig!, subject, body, _attachmentPaths);

            if (success)
            {
                KryptonMessageBox.Show(strings.SuccessMessage, strings.SuccessTitle, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
            }
            else
            {
                KryptonMessageBox.Show(strings.ErrorMessage, strings.ErrorTitle, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
            }
        }
        catch (Exception ex)
        {
            KryptonMessageBox.Show($"{strings.ErrorMessage}\n\n{ex.Message}", strings.ErrorTitle, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
        }
        finally
        {
            kbtnSend.Enabled = true;
            kbtnSend.Text = strings.Send;
        }
    }

    /// <summary>
    /// Builds the body text for a bug report email using the current form data.
    /// </summary>
    /// <remarks>The returned email body includes all relevant details entered by the user at the time of
    /// invocation. Exception details and attachments are included only if available.</remarks>
    /// <returns>A string containing the formatted bug report, including reporter information, bug description, steps to
    /// reproduce, exception details if present, and a list of attachments.</returns>
    private string BuildEmailBody()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Bug Report");
        sb.AppendLine("==========");
        sb.AppendLine();
        sb.AppendLine($"Reported by: {ktbEmailAddress.Text}");
        sb.AppendLine($"Date: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        sb.AppendLine();
        sb.AppendLine("Bug Description:");
        sb.AppendLine("----------------");
        sb.AppendLine(krtbBugDescription.Text);
        sb.AppendLine();
        sb.AppendLine("Steps to Reproduce:");
        sb.AppendLine("-------------------");
        sb.AppendLine(krtbStepsToReproduce.Text);
        sb.AppendLine();

        if (_exception != null)
        {
            sb.AppendLine("Exception Details:");
            sb.AppendLine("-----------------");
            sb.AppendLine(FormatExceptionDetails(_exception));
            sb.AppendLine();
        }

        if (_attachmentPaths.Count > 0)
        {
            sb.AppendLine("Attachments:");
            sb.AppendLine("-----------");
            foreach (var path in _attachmentPaths)
            {
                sb.AppendLine(Path.GetFileName(path));
            }
        }

        return sb.ToString();
    }

    private void ktbEmailAddress_TextChanged(object sender, EventArgs e) => ValidateEmailAddress();

    private void krtbBugDescription_TextChanged(object sender, EventArgs e) => ValidateBugDescription();

    private void kbtnAddScreenshot_Click(object sender, EventArgs e) => CaptureScreenshot();

    private void kbtnAddFile_Click(object sender, EventArgs e) => AddFile();

    private void kbtnRemove_Click(object sender, EventArgs e)
    {
        if (_attachmentPaths.Count > 0)
        {
            RemoveAttachment(_attachmentPaths[0]);
        }
    }

    private void kbtnSend_Click(object sender, EventArgs e) => SendBugReport();

    private void kbtnCancel_Click(object sender, EventArgs e) => DialogResult = DialogResult.Cancel;

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        _errorProvider?.Clear();
        _errorProvider?.Dispose();

        foreach (var thumbnail in _thumbnailCache.Values)
        {
            thumbnail?.Dispose();
        }
        _thumbnailCache.Clear();

        foreach (var path in _attachmentPaths)
        {
            if (path.StartsWith(Path.GetTempPath()))
            {
                try
                {
                    File.Delete(path);
                }
                catch (FileNotFoundException ex)
                {
                    KryptonMessageBox.Show($"Temporary file not found: {ex.Message}", "Error",
                        KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
                }
            }
        }
        base.OnFormClosed(e);
    }

    #endregion
}

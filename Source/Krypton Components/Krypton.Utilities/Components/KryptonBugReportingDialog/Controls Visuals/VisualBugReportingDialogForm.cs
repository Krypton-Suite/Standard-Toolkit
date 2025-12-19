#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

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

    #endregion

    #region Identity

    public VisualBugReportingDialogForm(Exception? exception, BugReportEmailConfig? emailConfig)
    {
        InitializeComponent();

        SetInheritedControlOverride();

        _exception = exception;
        _emailConfig = emailConfig;

        SetupUI();
    }

    #endregion

    #region Implementation

    private static readonly KryptonBugReportingDialogStrings _defaultStrings = new KryptonBugReportingDialogStrings();

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

    private Panel CreateThumbnailPanel(string filePath)
    {
        var panel = new KryptonPanel
        {
            Size = new Size(140, 160),
            Margin = new Padding(5),
            BorderStyle = BorderStyle.FixedSingle,
            BackColor = Color.White,
            Tag = filePath
        };

        var pictureBox = new PictureBox
        {
            Size = new Size(128, 128),
            Location = new Point(5, 5),
            SizeMode = PictureBoxSizeMode.Zoom,
            BorderStyle = BorderStyle.None
        };

        var fileName = Path.GetFileName(filePath);
        var label = new KryptonLabel
        {
            Text = fileName.Length > 18 ? fileName.Substring(0, 15) + "..." : fileName,
            Location = new Point(5, 136),
            Size = new Size(130, 20),
            AutoSize = false
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

        pictureBox.MouseEnter += (s, e) => panel.BackColor = Color.LightGray;
        pictureBox.MouseLeave += (s, e) => panel.BackColor = Color.White;

        panel.Controls.Add(pictureBox);
        panel.Controls.Add(label);
        panel.Controls.Add(removeButton);

        LoadThumbnail(pictureBox, filePath);

        return panel;
    }

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

            if (thumbnail != null)
            {
                pictureBox.Image = thumbnail;
            }
            else
            {
                pictureBox.Image = null;
            }
        }
        catch
        {
            pictureBox.Image = null;
        }
    }

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
                catch
                {
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

    private void UpdateSendButtonState()
    {
        kbtnSend.Enabled = !string.IsNullOrWhiteSpace(ktbEmailAddress.Text) &&
                          !string.IsNullOrWhiteSpace(krtbBugDescription.Text) &&
                          _emailConfig != null &&
                          !string.IsNullOrWhiteSpace(_emailConfig.ToEmail);
    }

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
            MessageBox.Show($"Failed to capture screenshot: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

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


    private bool ValidateInput()
    {
        var strings = _defaultStrings;

        if (string.IsNullOrWhiteSpace(ktbEmailAddress.Text))
        {
            MessageBox.Show(strings.RequiredFields, strings.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        if (!IsValidEmail(ktbEmailAddress.Text))
        {
            MessageBox.Show(strings.InvalidEmail, strings.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        if (string.IsNullOrWhiteSpace(krtbBugDescription.Text))
        {
            MessageBox.Show(strings.RequiredFields, strings.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        if (_emailConfig == null || string.IsNullOrWhiteSpace(_emailConfig.ToEmail))
        {
            MessageBox.Show("Email configuration is not set. Please configure the recipient email address.", strings.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        return true;
    }

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
                MessageBox.Show(strings.SuccessMessage, strings.SuccessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show(strings.ErrorMessage, strings.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"{strings.ErrorMessage}\n\n{ex.Message}", strings.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            kbtnSend.Enabled = true;
            kbtnSend.Text = strings.Send;
        }
    }

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

    private void ktbEmailAddress_TextChanged(object sender, EventArgs e) => UpdateSendButtonState();

    private void krtbBugDescription_TextChanged(object sender, EventArgs e) => UpdateSendButtonState();

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
                catch
                {
                }
            }
        }
        base.OnFormClosed(e);
    }

    #endregion
}


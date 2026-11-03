#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Shows download progress. Cancel stays available while bytes are moving.
/// A folded footer shows the file name, size, speed, and time remaining.
/// After the transfer stops early, the window stays open for resume, restart, or close.
/// </summary>
internal partial class VisualApplicationUpdateProgressForm : KryptonForm
{
    private const char EXPAND_GLYPH = '\u25BC';
    private const char COLLAPSE_GLYPH = '\u25B2';
    private const int DETAILS_HEIGHT = 96;

    private bool _allowClose;
    private bool _detailsExpanded;
    private int _collapsedClientHeight;
    private TaskCompletionSource<KryptonUpdateDownloadRecovery>? _recovery;
    private KryptonApplicationUpdaterStrings _strings = new KryptonApplicationUpdaterStrings();
    private string _fileName = string.Empty;
    private bool _hasSample;
    private long _lastBytes;
    private DateTime _lastSampleUtc;
    private double _bytesPerSecond;

    /// <summary>Initializes the progress window.</summary>
    public VisualApplicationUpdateProgressForm()
    {
        InitializeComponent();
        _collapsedClientHeight = ClientSize.Height;
        btnDetails.Click += OnDetails;
        btnCancel.Click += OnCancel;
        btnResume.Click += OnResume;
        btnRestart.Click += OnRestart;
        btnClose.Click += OnCloseRecovery;
    }

    /// <summary>Raised when the user cancels the download.</summary>
    internal event EventHandler? CancelClicked;

    /// <summary>Gets a value indicating that the user cancelled the download.</summary>
    internal bool CancelRequested { get; private set; }

    /// <summary>Applies localised labels and shows Cancel for the first transfer.</summary>
    /// <param name="strings">Prompt text.</param>
    internal void Bind(KryptonApplicationUpdaterStrings strings)
    {
        _strings = strings ?? new KryptonApplicationUpdaterStrings();
        Text = _strings.WindowTitle;
        btnCancel.Text = _strings.Cancel;
        btnResume.Text = _strings.Resume;
        btnRestart.Text = _strings.Restart;
        btnClose.Text = _strings.Close;
        ApplyDetailsButton();
        SetFileName(_fileName);
        BeginTransfer(_strings.Downloading);
    }

    /// <summary>Sets the package file name shown in the details footer.</summary>
    /// <param name="name">File name, without a directory.</param>
    internal void SetFileName(string name)
    {
        _fileName = name ?? string.Empty;
        if (IsDisposed)
        {
            return;
        }

        lblFile.Values.Text = _strings.FileName + " " + _fileName;
    }

    /// <summary>Shows Cancel and clears a previous recovery choice.</summary>
    /// <param name="status">Status line for this attempt.</param>
    internal void BeginTransfer(string status)
    {
        CancelRequested = false;
        _recovery = null;
        btnResume.Visible = false;
        btnRestart.Visible = false;
        btnClose.Visible = false;
        btnCancel.Visible = true;
        btnCancel.Enabled = true;
        ResetRate();
        SetStatus(status);
        SetBar(0, 100);
        PaintDetails(0, null);
    }

    /// <summary>Shows Resume when <paramref name="canResume"/> is true, Restart when <paramref name="canRetry"/> is true, and Close. The window stays open.</summary>
    /// <param name="message">Why the transfer stopped.</param>
    /// <param name="canResume">True when a partial file is still on disk and the manifest allows resume.</param>
    /// <param name="canRetry">True when the manifest allows the download to start again.</param>
    /// <param name="strings">Button captions.</param>
    /// <returns>The button the user chooses.</returns>
    internal Task<KryptonUpdateDownloadRecovery> ShowRecovery(string message, bool canResume, bool canRetry, KryptonApplicationUpdaterStrings strings)
    {
        var source = new TaskCompletionSource<KryptonUpdateDownloadRecovery>();
        _recovery = source;
        btnResume.Text = strings.Resume;
        btnRestart.Text = strings.Restart;
        btnClose.Text = strings.Close;
        btnCancel.Visible = false;
        btnResume.Visible = canResume;
        btnResume.Enabled = true;
        btnRestart.Visible = canRetry;
        btnRestart.Enabled = true;
        btnClose.Visible = true;
        btnClose.Enabled = true;
        SetStatus(message);
        return source.Task;
    }

    /// <summary>Closes the window from code. A user close during a transfer cancels instead.</summary>
    internal void Dismiss()
    {
        _allowClose = true;
        if (IsDisposed)
        {
            return;
        }

        Close();
        Dispose();
    }

    /// <summary>Sets the status line.</summary>
    /// <param name="text">Status text.</param>
    internal void SetStatus(string text)
    {
        if (IsDisposed)
        {
            return;
        }

        if (InvokeRequired)
        {
            BeginInvoke(new Action(() => SetStatus(text)));
            return;
        }

        lblStatus.Values.Text = text ?? string.Empty;
    }

    /// <summary>Enables or disables cancel. Cancel stays disabled once verification has started.</summary>
    /// <param name="enabled">True while the file is still copying.</param>
    internal void SetCancelEnabled(bool enabled)
    {
        if (IsDisposed)
        {
            return;
        }

        if (InvokeRequired)
        {
            BeginInvoke(new Action(() => SetCancelEnabled(enabled)));
            return;
        }

        btnCancel.Enabled = enabled;
    }

    /// <summary>Updates the bar. An unknown total uses a marquee.</summary>
    /// <param name="received">Bytes written.</param>
    /// <param name="total">Content length, or null.</param>
    internal void SetProgress(long received, long? total)
    {
        if (IsDisposed)
        {
            return;
        }

        if (InvokeRequired)
        {
            BeginInvoke(new Action(() => SetProgress(received, total)));
            return;
        }

        SetBar(received, total);
        UpdateDetails(received, total);
    }

    private void SetBar(long received, long? total)
    {
        if (total.HasValue && total.Value > 0)
        {
            if (progressDownload.Style != ProgressBarStyle.Continuous)
            {
                progressDownload.Style = ProgressBarStyle.Continuous;
            }

            var percent = (int)Math.Min(100, received * 100 / total.Value);
            progressDownload.Value = percent;
            return;
        }

        if (progressDownload.Style != ProgressBarStyle.Marquee)
        {
            progressDownload.Style = ProgressBarStyle.Marquee;
        }
    }

    private void OnDetails(object? sender, EventArgs e)
    {
        _detailsExpanded = !_detailsExpanded;
        var height = ScaledDetailsHeight();
        table.RowStyles[3].Height = _detailsExpanded ? height : 0;
        detailsPanel.Visible = _detailsExpanded;
        ClientSize = new Size(ClientSize.Width, _collapsedClientHeight + (_detailsExpanded ? height : 0));
        ApplyDetailsButton();
    }

    private void ApplyDetailsButton()
    {
        var caption = _detailsExpanded ? _strings.HideDetails : _strings.Details;
        var glyph = _detailsExpanded ? COLLAPSE_GLYPH : EXPAND_GLYPH;
        btnDetails.Text = caption + " " + glyph;
    }

    private int ScaledDetailsHeight()
    {
        var dpi = DeviceDpi;
        if (dpi <= 0)
        {
            dpi = 96;
        }

        return DETAILS_HEIGHT * dpi / 96;
    }

    private void ResetRate()
    {
        _hasSample = false;
        _lastBytes = 0;
        _bytesPerSecond = 0;
    }

    private void UpdateDetails(long received, long? total)
    {
        var now = DateTime.UtcNow;
        if (!_hasSample)
        {
            _hasSample = true;
            _lastBytes = received;
            _lastSampleUtc = now;
        }
        else
        {
            var elapsed = (now - _lastSampleUtc).TotalSeconds;
            if (elapsed >= 0.25d)
            {
                var delta = received - _lastBytes;
                if (delta >= 0)
                {
                    _bytesPerSecond = delta / elapsed;
                }

                _lastBytes = received;
                _lastSampleUtc = now;
            }
        }

        PaintDetails(received, total);
    }

    private void PaintDetails(long received, long? total)
    {
        var size = FormatBytes(received);
        if (total.HasValue && total.Value > 0)
        {
            size = size + " " + _strings.DownloadOf + " " + FormatBytes(total.Value);
        }

        lblDownloaded.Values.Text = _strings.Downloaded + " " + size;
        lblSpeed.Values.Text = _strings.Speed + " " + (_bytesPerSecond >= 1d ? FormatBytes((long)_bytesPerSecond) + "/s" : _strings.Unknown);
        lblRemaining.Values.Text = _strings.Remaining + " " + FormatRemaining(received, total);
    }

    private string FormatRemaining(long received, long? total)
    {
        if (!total.HasValue || total.Value <= received || _bytesPerSecond < 1d)
        {
            return _strings.Unknown;
        }

        var seconds = (long)((total.Value - received) / _bytesPerSecond);
        if (seconds < 60)
        {
            return seconds.ToString(CultureInfo.CurrentCulture) + " s";
        }

        if (seconds < 3600)
        {
            return (seconds / 60).ToString(CultureInfo.CurrentCulture) + " min";
        }

        var hours = seconds / 3600;
        var minutes = (seconds % 3600) / 60;
        return hours.ToString(CultureInfo.CurrentCulture) + " h " + minutes.ToString(CultureInfo.CurrentCulture) + " min";
    }

    private static string FormatBytes(long bytes)
    {
        var culture = CultureInfo.CurrentCulture;
        if (bytes < 1024)
        {
            return bytes.ToString(culture) + " B";
        }

        if (bytes < 1024 * 1024)
        {
            return (bytes / 1024d).ToString("0.0", culture) + " KB";
        }

        if (bytes < 1024L * 1024 * 1024)
        {
            return (bytes / 1024d / 1024d).ToString("0.0", culture) + " MB";
        }

        return (bytes / 1024d / 1024d / 1024d).ToString("0.0", culture) + " GB";
    }

    /// <inheritdoc />
    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        if (_allowClose)
        {
            base.OnFormClosing(e);
            return;
        }

        if (_recovery != null)
        {
            CompleteRecovery(KryptonUpdateDownloadRecovery.Close);
            base.OnFormClosing(e);
            return;
        }

        e.Cancel = true;
        base.OnFormClosing(e);
        if (!CancelRequested)
        {
            OnCancel(this, EventArgs.Empty);
        }
    }

    private void OnCancel(object? sender, EventArgs e)
    {
        CancelRequested = true;
        btnCancel.Enabled = false;
        CancelClicked?.Invoke(this, EventArgs.Empty);
    }

    private void OnResume(object? sender, EventArgs e) => CompleteRecovery(KryptonUpdateDownloadRecovery.Resume);

    private void OnRestart(object? sender, EventArgs e) => CompleteRecovery(KryptonUpdateDownloadRecovery.Restart);

    private void OnCloseRecovery(object? sender, EventArgs e) => CompleteRecovery(KryptonUpdateDownloadRecovery.Close);

    private void CompleteRecovery(KryptonUpdateDownloadRecovery choice)
    {
        var source = _recovery;
        _recovery = null;
        if (source != null)
        {
            source.TrySetResult(choice);
        }
    }
}

#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Dialogs;

/// <summary>
/// Progress window for a file operation. Shown when the shell calls <see cref="IOperationsProgressDialog.StartProgressDialog"/>.
/// </summary>
internal sealed partial class VisualFileOperationProgressForm : KryptonForm
{
    private const int CollapsedHeightWithBanner = 300;
    private const int ExpandedHeightWithBanner = 380;
    private const int BannerHeight = 56;
    private const int ProgressTop = 88;
    private const int DetailsCollapsedTop = 120;
    private const int ErrorCollapsedTop = 164;
    private const int GraphHeight = 110;

    private readonly FileOperationProgressState _state;
    private readonly KryptonFileOperationStrings _strings;
    private readonly IWin32Window? _owner;
    private readonly bool _showBanner;
    private bool _allowClose;
    private bool _shown;
    private bool _detailsExpanded;

    /// <summary>
    /// Creates the form. Callers must read <see cref="Control.Handle"/> on the UI thread before starting the worker.
    /// </summary>
    internal VisualFileOperationProgressForm(KryptonFileOperationStrings strings, FileOperationProgressState state, IWin32Window? owner, bool showBanner = true)
    {
        _strings = strings;
        _state = state;
        _owner = owner;
        _showBanner = showBanner;
        InitializeComponent();
        _btnPause.Text = strings.Pause;
        _btnCancel.Text = strings.Cancel;
        _btnPause.Visible = false;
        _picBanner.Image = SystemIcons.Information.ToBitmap();
        _lblBanner.Text = strings.Calculating;
        Text = strings.Calculating;
        _btnMoreDetails.Text = strings.MoreDetails;
        _speedGraph.Values.Capacity = 60;
        _speedGraph.Values.SpeedLabelFormat = strings.SpeedLabel;
        _speedGraph.Visible = false;
        ApplyBanner(showBanner);
        _btnPause.Click += OnPause;
        _btnCancel.Click += OnCancel;
        _btnMoreDetails.Click += OnMoreDetails;
    }

    private int CollapsedHeight => _showBanner ? CollapsedHeightWithBanner : CollapsedHeightWithBanner - BannerHeight;

    private int ExpandedHeight => _showBanner ? ExpandedHeightWithBanner : ExpandedHeightWithBanner - BannerHeight;

    private void ApplyBanner(bool showBanner)
    {
        _pnlBanner.Visible = showBanner;
        if (!showBanner)
        {
            ClientSize = new Size(ClientSize.Width, CollapsedHeight);
        }
    }

    /// <summary>Pause and cancel flag read by the shell on the worker thread.</summary>
    internal FileOperationProgressState State => _state;

    /// <summary>Text the progress dialog is using.</summary>
    internal KryptonFileOperationStrings Strings => _strings;

    /// <summary>True when the speed graph replaces the progress bar.</summary>
    internal bool DetailsExpanded => _detailsExpanded;

    /// <summary>
    /// Swaps the progress bar and speed graph. Used by tests and by the More details button.
    /// </summary>
    internal void SetDetailsExpanded(bool expanded)
    {
        if (_detailsExpanded == expanded || IsDisposed)
        {
            return;
        }

        _detailsExpanded = expanded;
        _progress.Visible = !expanded;
        _speedGraph.Visible = expanded;
        _btnMoreDetails.Text = expanded ? _strings.FewerDetails : _strings.MoreDetails;
        ClientSize = new Size(ClientSize.Width, expanded ? ExpandedHeight : CollapsedHeight);

        // Graph takes the progress-bar slot; details/error shift down while expanded.
        // Sample history is kept when collapsing so re-expand still shows recent data.
        if (expanded)
        {
            _speedGraph.Location = new Point(12, ProgressTop);
            _speedGraph.Size = new Size(_pnlContent.ClientSize.Width - 24, GraphHeight);
            int detailsTop = ProgressTop + GraphHeight + 12;
            _lblDetails.Location = new Point(_lblDetails.Left, detailsTop);
            _lblError.Location = new Point(_lblError.Left, detailsTop + _lblDetails.Height + 4);
        }
        else
        {
            _lblDetails.Location = new Point(_lblDetails.Left, DetailsCollapsedTop);
            _lblError.Location = new Point(_lblError.Left, ErrorCollapsedTop);
        }
    }

    /// <summary>
    /// Posts a progress snapshot to the UI thread.
    /// </summary>
    internal void Post(ProgressDisplay display)
    {
        Post(() => Apply(display));
    }

    /// <summary>
    /// Shows the shell message for <paramref name="hresult"/> and raises <paramref name="handler"/> on the UI thread.
    /// </summary>
    internal void ReportError(string path, int hresult, FileErrorHandler handler)
    {
        ReportError(path, hresult, Describe(hresult), handler);
    }

    /// <summary>
    /// Shows <paramref name="message"/> and raises <paramref name="handler"/> on the UI thread.
    /// </summary>
    internal void ReportError(string path, int hresult, string message, FileErrorHandler handler)
    {
        _state.AnyError = true;
        string safePath = path ?? string.Empty;
        Post(() =>
        {
            _lblError.Text = message;
            _lblError.Visible = true;
            handler(safePath, hresult, message);
        });
    }

    /// <summary>Allows the form to close after the worker has finished.</summary>
    internal void AllowClose()
    {
        _allowClose = true;
        if (!IsDisposed)
        {
            Close();
        }
    }

    /// <inheritdoc />
    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        if (!_allowClose && e.CloseReason == CloseReason.UserClosing)
        {
            e.Cancel = true;
            _state.Status = (int)PDOPSTATUS.CANCELLED;
            return;
        }

        base.OnFormClosing(e);
    }

    private void Apply(ProgressDisplay display)
    {
        if (IsDisposed)
        {
            return;
        }

        Text = display.Caption;
        _lblBanner.Text = display.Caption;
        _lblItem.Text = display.Item;
        _lblFrom.Text = display.Source.Length == 0 ? string.Empty : _strings.FromLabel + " " + display.Source;
        _lblTo.Text = display.Target.Length == 0 ? string.Empty : _strings.ToLabel + " " + display.Target;
        _lblDetails.Text = display.Detail;
        _btnPause.Visible = display.AllowPause;
        if (display.Marquee)
        {
            _progress.Style = ProgressBarStyle.Marquee;
        }
        else
        {
            _progress.Style = ProgressBarStyle.Continuous;
            int value = display.Percent;
            if (value < _progress.Minimum)
            {
                value = _progress.Minimum;
            }

            if (value > _progress.Maximum)
            {
                value = _progress.Maximum;
            }

            _progress.Value = value;
        }

        if (_detailsExpanded && !display.Marquee)
        {
            _speedGraph.AddSample(display.BytesPerSecond);
        }

        if (display.Show)
        {
            ShowIfNeeded();
        }
    }

    private void ShowIfNeeded()
    {
        if (_shown || IsDisposed)
        {
            return;
        }

        _shown = true;
        if (_owner != null)
        {
            Show(_owner);
        }
        else
        {
            Show();
        }
    }

    private void Post(Action action)
    {
        if (IsDisposed || !IsHandleCreated)
        {
            return;
        }

        try
        {
            if (InvokeRequired)
            {
                BeginInvoke(action);
            }
            else
            {
                action();
            }
        }
        catch (InvalidOperationException)
        {
        }
    }

    private void OnMoreDetails(object? sender, EventArgs e) => SetDetailsExpanded(!_detailsExpanded);

    private void OnPause(object? sender, EventArgs e)
    {
        if (_state.Status == (int)PDOPSTATUS.PAUSED)
        {
            _state.Status = (int)PDOPSTATUS.RUNNING;
            _btnPause.Text = _strings.Pause;
            _speedGraph.Resume();
        }
        else
        {
            _state.Status = (int)PDOPSTATUS.PAUSED;
            _btnPause.Text = _strings.Resume;
            _speedGraph.Pause();
        }
    }

    private void OnCancel(object? sender, EventArgs e)
    {
        _state.Status = (int)PDOPSTATUS.CANCELLED;
    }

    private static string Describe(int hresult)
    {
        try
        {
            Exception? ex = Marshal.GetExceptionForHR(hresult);
            if (ex != null && !string.IsNullOrEmpty(ex.Message))
            {
                return ex.Message;
            }
        }
        catch (ArgumentException)
        {
        }

        return "0x" + hresult.ToString("X8", CultureInfo.InvariantCulture);
    }
}

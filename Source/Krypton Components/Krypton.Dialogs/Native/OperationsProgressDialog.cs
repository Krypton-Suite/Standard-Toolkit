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
/// Values the progress form paints. Copied on the worker so the UI thread never touches shell objects.
/// </summary>
internal sealed class ProgressDisplay
{
    /// <summary>Form and heading text.</summary>
    internal string Caption = string.Empty;

    /// <summary>Current item display name.</summary>
    internal string Item = string.Empty;

    /// <summary>Source location.</summary>
    internal string Source = string.Empty;

    /// <summary>Destination location.</summary>
    internal string Target = string.Empty;

    /// <summary>Second line under the bar.</summary>
    internal string Detail = string.Empty;

    /// <summary>0-100 when <see cref="Marquee"/> is false.</summary>
    internal int Percent;

    /// <summary>True for preflight and indeterminate mode.</summary>
    internal bool Marquee;

    /// <summary>True when <see cref="IOperationsProgressDialog.StartProgressDialog"/> has been called.</summary>
    internal bool Show;

    /// <summary>True when the shell passed <see cref="OPPROGDLGF.ENABLEPAUSE"/>.</summary>
    internal bool AllowPause;

    /// <summary>Estimated transfer rate in bytes per second.</summary>
    internal double BytesPerSecond;
}

/// <summary>
/// COM progress dialog. Created on the worker STA so the shell calls it directly.
/// UI updates are posted with <c>BeginInvoke</c> so the shell thread never blocks on the form.
/// </summary>
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.None)]
internal sealed class OperationsProgressDialog : IOperationsProgressDialog
{
    private readonly VisualFileOperationProgressForm _form;
    private readonly KryptonFileOperationStrings _strings;
    private readonly Stopwatch _timer = new Stopwatch();

    private string _caption = string.Empty;
    private string _item = string.Empty;
    private string _source = string.Empty;
    private string _target = string.Empty;
    private bool _marquee = true;
    private bool _started;
    private bool _allowPause;
    private long _lastTicks;
    private ulong _lastSize;
    private double _bytesPerSecond;
    private ulong _remainingMs;
    private ulong _size;
    private ulong _totalSize;
    private ulong _items;
    private ulong _totalItems;
    private ulong _points;
    private ulong _totalPoints;

    /// <summary>
    /// Creates a dialog bound to a form that already has a handle on the UI thread.
    /// </summary>
    internal OperationsProgressDialog(VisualFileOperationProgressForm form, KryptonFileOperationStrings strings)
    {
        _form = form;
        _strings = strings;
        _caption = strings.Calculating;
    }

    /// <inheritdoc />
    public void StartProgressDialog(IntPtr hwndOwner, OPPROGDLGF flags)
    {
        try
        {
            _allowPause = (flags & OPPROGDLGF.ENABLEPAUSE) != 0;
            _started = true;
            _marquee = true;
            _timer.Restart();
            Publish();
        }
        catch (InvalidOperationException)
        {
        }
    }

    /// <inheritdoc />
    public void StopProgressDialog()
    {
        _started = false;
    }

    /// <inheritdoc />
    public void SetOperation(SPACTION action)
    {
        _caption = CaptionFor(action);
        Publish();
    }

    /// <inheritdoc />
    public void SetMode(PDMODE mode)
    {
        if ((mode & PDMODE.UNDOING) != 0)
        {
            _caption = _strings.Undoing;
        }
        else if ((mode & PDMODE.PREFLIGHT) != 0 || (mode & PDMODE.INDETERMINATE) != 0)
        {
            _caption = _strings.Calculating;
        }

        _marquee = (mode & (PDMODE.PREFLIGHT | PDMODE.INDETERMINATE)) != 0;
        Publish();
    }

    /// <inheritdoc />
    public void UpdateProgress(ulong ullPointsCurrent, ulong ullPointsTotal, ulong ullSizeCurrent, ulong ullSizeTotal, ulong ullItemsCurrent, ulong ullItemsTotal)
    {
        NoteSize(ullSizeCurrent, ullSizeTotal);
        _marquee = ullSizeTotal == 0 && ullItemsTotal == 0 && ullPointsTotal == 0;
        Publish(ullSizeCurrent, ullSizeTotal, ullItemsCurrent, ullItemsTotal, ullPointsCurrent, ullPointsTotal);
    }

    /// <inheritdoc />
    public void UpdateLocations(IShellItem psiSource, IShellItem psiTarget, IShellItem psiItem)
    {
        NotePaths(
            FirstName(psiSource),
            FirstName(psiTarget),
            ShellNative.DisplayName(psiItem, SIGDN.NORMALDISPLAY));
    }

    /// <summary>
    /// Fills any location the shell left blank. Empty values do not clear a name already shown.
    /// </summary>
    internal void NotePaths(string source, string target, string item)
    {
        if (!string.IsNullOrEmpty(source))
        {
            _source = source;
        }

        if (!string.IsNullOrEmpty(target))
        {
            _target = target;
        }

        if (!string.IsNullOrEmpty(item))
        {
            _item = item;
        }

        Publish();
    }

    private static string FirstName(IShellItem item)
    {
        string path = ShellNative.DisplayName(item, SIGDN.FILESYSPATH);
        return path.Length == 0 ? ShellNative.DisplayName(item, SIGDN.NORMALDISPLAY) : path;
    }

    /// <inheritdoc />
    public void ResetTimer()
    {
        _timer.Restart();
        _lastTicks = 0;
        _lastSize = 0;
        _bytesPerSecond = 0;
        _remainingMs = 0;
    }

    /// <inheritdoc />
    public void PauseTimer()
    {
        if (_timer.IsRunning)
        {
            _timer.Stop();
        }
    }

    /// <inheritdoc />
    public void ResumeTimer()
    {
        if (!_timer.IsRunning)
        {
            _timer.Start();
        }
    }

    /// <inheritdoc />
    public void GetMilliseconds(out ulong pullElapsed, out ulong pullRemaining)
    {
        pullElapsed = (ulong)Math.Max(0, _timer.ElapsedMilliseconds);
        pullRemaining = _remainingMs;
    }

    /// <inheritdoc />
    public PDOPSTATUS GetOperationStatus()
    {
        int status = _form.State.Status;
        if (status < (int)PDOPSTATUS.RUNNING || status > (int)PDOPSTATUS.ERRORS)
        {
            return PDOPSTATUS.RUNNING;
        }

        return (PDOPSTATUS)status;
    }

    private void NoteSize(ulong current, ulong total)
    {
        long now = _timer.ElapsedMilliseconds;
        if (!_timer.IsRunning)
        {
            _lastTicks = now;
            _lastSize = current;
            return;
        }

        if (_lastTicks == 0)
        {
            _lastTicks = now;
            _lastSize = current;
            return;
        }

        // Keep the sample window open. Progress callbacks arrive faster than 200ms, and resetting
        // the baseline on each one never lets a rate accumulate.
        if (current < _lastSize)
        {
            _lastTicks = now;
            _lastSize = current;
            return;
        }

        if (now <= _lastTicks + 200)
        {
            return;
        }

        double seconds = (now - _lastTicks) / 1000.0;
        double instant = (current - _lastSize) / seconds;
        _bytesPerSecond = _bytesPerSecond <= 0 ? instant : (_bytesPerSecond * 0.6) + (instant * 0.4);
        _lastTicks = now;
        _lastSize = current;
        if (_bytesPerSecond > 1 && total > current)
        {
            _remainingMs = (ulong)((total - current) / _bytesPerSecond * 1000.0);
        }
        else
        {
            _remainingMs = 0;
        }
    }

    private void Publish()
    {
        Publish(_size, _totalSize, _items, _totalItems, _points, _totalPoints);
    }

    private void Publish(ulong size, ulong totalSize, ulong items, ulong totalItems, ulong points, ulong totalPoints)
    {
        _size = size;
        _totalSize = totalSize;
        _items = items;
        _totalItems = totalItems;
        _points = points;
        _totalPoints = totalPoints;
        int percent = 0;
        if (totalSize > 0)
        {
            percent = (int)Math.Min(100, size * 100 / totalSize);
        }
        else if (totalItems > 0)
        {
            percent = (int)Math.Min(100, items * 100 / totalItems);
        }
        else if (totalPoints > 0)
        {
            percent = (int)Math.Min(100, points * 100 / totalPoints);
        }

        string detail = _marquee
            ? _strings.Calculating
            : BuildDetail(size, totalSize, items, totalItems);

        var display = new ProgressDisplay
        {
            Caption = _caption,
            Item = _item,
            Source = _source,
            Target = _target,
            Detail = detail,
            Percent = percent,
            Marquee = _marquee,
            Show = _started,
            AllowPause = _allowPause,
            BytesPerSecond = _bytesPerSecond
        };

        _form.Post(display);
    }

    private string BuildDetail(ulong size, ulong totalSize, ulong items, ulong totalItems)
    {
        string itemText = string.Format(CultureInfo.CurrentCulture, _strings.ItemsOf, items, totalItems);
        string byteText = string.Format(
            CultureInfo.CurrentCulture,
            _strings.BytesOf,
            FormatBytes(size),
            FormatBytes(totalSize));
        string speedText = string.Format(
            CultureInfo.CurrentCulture,
            _strings.SpeedPerSecond,
            FormatBytes((ulong)Math.Max(0, _bytesPerSecond)));
        string timeText = _remainingMs == 0
            ? string.Empty
            : string.Format(CultureInfo.CurrentCulture, _strings.TimeRemaining, FormatDuration(_remainingMs));

        if (timeText.Length == 0)
        {
            return itemText + ", " + byteText + ", " + speedText;
        }

        return itemText + ", " + byteText + ", " + speedText + ", " + timeText;
    }

    private string CaptionFor(SPACTION action)
    {
        switch (action)
        {
            case SPACTION.COPYING:
                return _strings.Copying;
            case SPACTION.MOVING:
                return _strings.Moving;
            case SPACTION.RECYCLING:
                return _strings.Recycling;
            case SPACTION.DELETING:
                return _strings.Deleting;
            case SPACTION.RENAMING:
                return _strings.Renaming;
            case SPACTION.COPY_MOVING:
                return _strings.CopyingAndMoving;
            case SPACTION.CALCULATING:
                return _strings.Calculating;
            default:
                return _caption.Length == 0 ? _strings.Calculating : _caption;
        }
    }

    private static string FormatBytes(ulong bytes)
    {
        string[] units = { "B", "KB", "MB", "GB", "TB" };
        double value = bytes;
        int unit = 0;
        while (value >= 1024 && unit < units.Length - 1)
        {
            value /= 1024;
            unit++;
        }

        if (unit == 0)
        {
            return bytes.ToString(CultureInfo.CurrentCulture) + " B";
        }

        return value.ToString("0.##", CultureInfo.CurrentCulture) + " " + units[unit];
    }

    private static string FormatDuration(ulong milliseconds)
    {
        var span = TimeSpan.FromMilliseconds(milliseconds);
        if (span.TotalHours >= 1)
        {
            return string.Format(CultureInfo.CurrentCulture, "{0}:{1:00}:{2:00}", (int)span.TotalHours, span.Minutes, span.Seconds);
        }

        return string.Format(CultureInfo.CurrentCulture, "{0}:{1:00}", (int)span.TotalMinutes, span.Seconds);
    }
}

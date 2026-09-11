#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

internal partial class VisualKryptonLogViewerForm : KryptonForm
{
    private readonly SynchronizationContext? _syncContext;
    private KryptonLogEvent[] _events = Array.Empty<KryptonLogEvent>();
    private int _reloadQueued;

    public VisualKryptonLogViewerForm()
    {
        InitializeComponent();
        _syncContext = SynchronizationContext.Current;
        ApplyStrings();
        PopulateLevelFilter();
        Reload();

        var memory = KryptonLog.Memory;
        if (memory != null)
        {
            memory.EventsChanged += OnMemoryEventsChanged;
        }
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        var memory = KryptonLog.Memory;
        if (memory != null)
        {
            memory.EventsChanged -= OnMemoryEventsChanged;
        }

        base.OnFormClosed(e);
    }

    private void ApplyStrings()
    {
        var strings = KryptonLogViewer.Strings;
        Text = strings.WindowTitle;
        kwlblLevel.Text = strings.Level;
        kwlblCategory.Text = strings.Category;
        kwlblSearch.Text = strings.Search;
        kchkLiveTail.Text = strings.LiveTail;
        kbtnExport.Text = strings.Export;
        kbtnClose.Text = strings.Close;
        colTime.Text = strings.ColumnTime;
        colLevel.Text = strings.ColumnLevel;
        colCategory.Text = strings.ColumnCategory;
        colMessage.Text = strings.ColumnMessage;
    }

    private void PopulateLevelFilter()
    {
        kcmbLevel.Items.Clear();
        kcmbLevel.Items.Add(KryptonLogViewer.Strings.AllLevels);
        foreach (KryptonLogLevel level in Enum.GetValues(typeof(KryptonLogLevel)))
        {
            kcmbLevel.Items.Add(level.ToString());
        }

        kcmbLevel.SelectedIndex = 0;
    }

    private void OnMemoryEventsChanged(object? sender, EventArgs e)
    {
        if (!kchkLiveTail.Checked)
        {
            return;
        }

        if (Interlocked.Exchange(ref _reloadQueued, 1) != 0)
        {
            return;
        }

        var context = _syncContext;
        if (context != null)
        {
            context.Post(_ =>
            {
                Volatile.Write(ref _reloadQueued, 0);
                if (!IsDisposed)
                {
                    Reload();
                }
            }, null);
            return;
        }

        Volatile.Write(ref _reloadQueued, 0);
    }

    private void Reload()
    {
        var memory = KryptonLog.Memory;
        if (memory == null)
        {
            _events = Array.Empty<KryptonLogEvent>();
            lvEvents.VirtualListSize = 0;
            kwlblStatus.Text = KryptonLogViewer.Strings.NoMemorySink;
            return;
        }

        var snapshot = memory.Snapshot();
        var minLevel = ResolveMinLevel();
        var category = ktxtCategory.Text?.Trim() ?? string.Empty;
        var search = ktxtSearch.Text?.Trim() ?? string.Empty;
        _events = snapshot.Where(evt => Matches(evt, minLevel, category, search)).ToArray();
        lvEvents.VirtualListSize = _events.Length;
        lvEvents.Refresh();
        kwlblStatus.Text = $"{_events.Length} / {snapshot.Length}";
        if (kchkLiveTail.Checked && _events.Length > 0)
        {
            lvEvents.EnsureVisible(_events.Length - 1);
        }
    }

    private KryptonLogLevel? ResolveMinLevel()
    {
        if (kcmbLevel.SelectedIndex <= 0)
        {
            return null;
        }

        return Enum.TryParse(kcmbLevel.Text, out KryptonLogLevel level) ? level : null;
    }

    private static bool Matches(KryptonLogEvent evt, KryptonLogLevel? minLevel, string category, string search)
    {
        if (minLevel.HasValue && evt.Level < minLevel.Value)
        {
            return false;
        }

        if (category.Length > 0 && evt.Category.IndexOf(category, StringComparison.OrdinalIgnoreCase) < 0)
        {
            return false;
        }

        return search.Length == 0 || evt.Message.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0;
    }

    private void lvEvents_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
    {
        if (e.ItemIndex < 0 || e.ItemIndex >= _events.Length)
        {
            e.Item = new ListViewItem();
            return;
        }

        var evt = _events[e.ItemIndex];
        var item = new ListViewItem(evt.Timestamp.ToString("HH:mm:ss.fff", CultureInfo.InvariantCulture));
        item.SubItems.Add(evt.Level.ToString());
        item.SubItems.Add(evt.Category);
        item.SubItems.Add(evt.Message);
        e.Item = item;
    }

    private void FilterChanged(object? sender, EventArgs e) => Reload();

    private void kbtnExport_Click(object? sender, EventArgs e)
    {
        var strings = KryptonLogViewer.Strings;
        using var dialog = new SaveFileDialog
        {
            Title = strings.ExportTitle,
            Filter = strings.ExportFilter,
            FileName = $"Krypton-{DateTime.Now:yyyyMMdd-HHmmss}.log"
        };

        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        var layout = KryptonLogLayout.Default;
        var sb = new StringBuilder();
        foreach (var evt in _events)
        {
            sb.Append(layout.Render(evt));
            if (sb.Length == 0 || sb[sb.Length - 1] != '\n')
            {
                sb.AppendLine();
            }
        }

        File.WriteAllText(dialog.FileName, KryptonLogProtect.Protect(sb.ToString()), Encoding.UTF8);
    }

    private void kbtnClose_Click(object? sender, EventArgs e) => DialogResult = DialogResult.OK;
}

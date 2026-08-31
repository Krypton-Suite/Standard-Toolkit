#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Demo for Issue #3847: <see cref="KryptonListView"/> virtual mode parity with native <see cref="ListView"/>.
/// </summary>
public partial class Feature3847ListViewVirtualModeDemo : KryptonForm
{
    private const int DefaultRowCount = 10000;
    private const int GrowShrinkStep = 1000;

    private readonly string[] _categories = { "Alpha", "Beta", "Gamma", "Delta", "Epsilon" };
    private VirtualRow[] _rows = Array.Empty<VirtualRow>();
    private int _cacheStart = -1;
    private int _cacheEnd = -1;
    private int _retrieveCount;
    private int _cacheCount;

    public Feature3847ListViewVirtualModeDemo()
    {
        InitializeComponent();
    }

    private void Feature3847ListViewVirtualModeDemo_Load(object? sender, EventArgs e)
    {
        ConfigureListView(lvNative);
        ConfigureListView(klvKrypton);
        BindVirtualList(lvNative);
        BindVirtualList(klvKrypton);
        RebuildRows(DefaultRowCount);
        ApplyVirtualSize();
        UpdateStatus();
    }

    private static void ConfigureListView(ListView listView)
    {
        listView.View = View.Details;
        listView.FullRowSelect = true;
        listView.HideSelection = false;
        listView.GridLines = true;
        listView.MultiSelect = true;
        listView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
        if (listView.Columns.Count == 0)
        {
            listView.Columns.Add("Index", 70);
            listView.Columns.Add("Name", 160);
            listView.Columns.Add("Category", 100);
            listView.Columns.Add("Value", 90);
        }
    }

    private static void ConfigureListView(KryptonListView listView)
    {
        listView.View = View.Details;
        listView.FullRowSelect = true;
        listView.HideSelection = false;
        listView.GridLines = true;
        listView.MultiSelect = true;
        listView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
        if (listView.Columns.Count == 0)
        {
            listView.Columns.Add("Index", 70);
            listView.Columns.Add("Name", 160);
            listView.Columns.Add("Category", 100);
            listView.Columns.Add("Value", 90);
        }
    }

    private void BindVirtualList(ListView listView)
    {
        listView.RetrieveVirtualItem += OnRetrieveVirtualItem;
        listView.CacheVirtualItems += OnCacheVirtualItems;
        listView.SearchForVirtualItem += OnSearchForVirtualItem;
        listView.VirtualItemsSelectionRangeChanged += OnVirtualItemsSelectionRangeChanged;
        listView.SelectedIndexChanged += OnSelectedIndexChanged;
        listView.VirtualMode = true;
    }

    private void BindVirtualList(KryptonListView listView)
    {
        listView.RetrieveVirtualItem += OnRetrieveVirtualItem;
        listView.CacheVirtualItems += OnCacheVirtualItems;
        listView.SearchForVirtualItem += OnSearchForVirtualItem;
        listView.VirtualItemsSelectionRangeChanged += OnVirtualItemsSelectionRangeChanged;
        listView.SelectedIndexChanged += OnSelectedIndexChanged;
        listView.VirtualMode = true;
    }

    private void RebuildRows(int count)
    {
        if (count < 0)
        {
            count = 0;
        }

        var rows = new VirtualRow[count];
        for (var i = 0; i < count; i++)
        {
            rows[i] = new VirtualRow(
                i,
                $"Item {i:00000}",
                _categories[i % _categories.Length],
                (i * 17 % 1000).ToString("000"));
        }

        _rows = rows;
        _cacheStart = -1;
        _cacheEnd = -1;
        _retrieveCount = 0;
        _cacheCount = 0;
    }

    private void ApplyVirtualSize()
    {
        var size = _rows.Length;
        lvNative.VirtualListSize = size;
        klvKrypton.VirtualListSize = size;
        lvNative.Refresh();
        klvKrypton.Refresh();
    }

    private void OnRetrieveVirtualItem(object? sender, RetrieveVirtualItemEventArgs e)
    {
        _retrieveCount++;
        e.Item = CreateItem(e.ItemIndex);
    }

    private void OnCacheVirtualItems(object? sender, CacheVirtualItemsEventArgs e)
    {
        _cacheCount++;
        _cacheStart = e.StartIndex;
        _cacheEnd = e.EndIndex;
        UpdateStatus();
    }

    private void OnSearchForVirtualItem(object? sender, SearchForVirtualItemEventArgs e)
    {
        e.Index = -1;
        if (string.IsNullOrEmpty(e.Text) || _rows.Length == 0)
        {
            return;
        }

        var start = e.StartIndex;
        if (start < 0 || start >= _rows.Length)
        {
            start = 0;
        }

        for (var i = 0; i < _rows.Length; i++)
        {
            var index = (start + i) % _rows.Length;
            var row = _rows[index];
            var match = e.IsPrefixSearch
                ? row.Name.StartsWith(e.Text, StringComparison.CurrentCultureIgnoreCase)
                : row.Name.IndexOf(e.Text, StringComparison.CurrentCultureIgnoreCase) >= 0
                  || row.Category.IndexOf(e.Text, StringComparison.CurrentCultureIgnoreCase) >= 0
                  || row.Value.IndexOf(e.Text, StringComparison.CurrentCultureIgnoreCase) >= 0;
            if (match)
            {
                e.Index = index;
                return;
            }
        }
    }

    private void OnVirtualItemsSelectionRangeChanged(object? sender, ListViewVirtualItemsSelectionRangeChangedEventArgs e) =>
        UpdateStatus();

    private void OnSelectedIndexChanged(object? sender, EventArgs e) => UpdateStatus();

    private ListViewItem CreateItem(int index)
    {
        if (index < 0 || index >= _rows.Length)
        {
            return new ListViewItem();
        }

        var row = _rows[index];
        var item = new ListViewItem(row.Index.ToString());
        item.SubItems.Add(row.Name);
        item.SubItems.Add(row.Category);
        item.SubItems.Add(row.Value);
        return item;
    }

    private void kbtnFilter_Click(object? sender, EventArgs e)
    {
        var filter = ktxtFilter.Text?.Trim() ?? string.Empty;
        if (filter.Length == 0)
        {
            RebuildRows(DefaultRowCount);
        }
        else
        {
            RebuildRows(DefaultRowCount);
            _rows = Array.FindAll(_rows, row =>
                row.Name.IndexOf(filter, StringComparison.CurrentCultureIgnoreCase) >= 0
                || row.Category.IndexOf(filter, StringComparison.CurrentCultureIgnoreCase) >= 0
                || row.Value.IndexOf(filter, StringComparison.CurrentCultureIgnoreCase) >= 0);
        }

        ApplyVirtualSize();
        UpdateStatus();
    }

    private void kbtnFind_Click(object? sender, EventArgs e)
    {
        var text = ktxtFind.Text ?? string.Empty;
        var native = lvNative.FindItemWithText(text, true, 0, true);
        var krypton = klvKrypton.FindItemWithText(text, true, 0, true);
        if (native != null)
        {
            lvNative.SelectedIndices.Clear();
            lvNative.SelectedIndices.Add(native.Index);
            lvNative.EnsureVisible(native.Index);
        }

        if (krypton != null)
        {
            klvKrypton.SelectedIndices.Clear();
            klvKrypton.SelectedIndices.Add(krypton.Index);
            klvKrypton.EnsureVisible(krypton.Index);
        }

        UpdateStatus($"Find '{text}': native={(native?.Index.ToString() ?? "none")}, krypton={(krypton?.Index.ToString() ?? "none")}");
    }

    private void kbtnGrow_Click(object? sender, EventArgs e)
    {
        RebuildRows(_rows.Length + GrowShrinkStep);
        ApplyVirtualSize();
        UpdateStatus();
    }

    private void kbtnShrink_Click(object? sender, EventArgs e)
    {
        var next = _rows.Length - GrowShrinkStep;
        RebuildRows(next < 0 ? 0 : next);
        ApplyVirtualSize();
        UpdateStatus();
    }

    private void kbtnReset_Click(object? sender, EventArgs e)
    {
        ktxtFilter.Text = string.Empty;
        ktxtFind.Text = string.Empty;
        RebuildRows(DefaultRowCount);
        ApplyVirtualSize();
        UpdateStatus();
    }

    private void UpdateStatus(string? extra = null)
    {
        var nativeSelected = lvNative.SelectedIndices.Count;
        var kryptonSelected = klvKrypton.SelectedIndices.Count;
        var cache = _cacheStart < 0 ? "none" : $"{_cacheStart}–{_cacheEnd}";
        var message =
            $"Rows={_rows.Length}; retrieve={_retrieveCount}; cache events={_cacheCount} ({cache}); " +
            $"selected native={nativeSelected}, krypton={kryptonSelected}.";
        if (!string.IsNullOrEmpty(extra))
        {
            message += " " + extra;
        }

        klblStatus.Values.Text = message;
    }

    private sealed class VirtualRow
    {
        internal VirtualRow(int index, string name, string category, string value)
        {
            Index = index;
            Name = name;
            Category = category;
            Value = value;
        }

        internal int Index { get; }
        internal string Name { get; }
        internal string Category { get; }
        internal string Value { get; }
    }
}

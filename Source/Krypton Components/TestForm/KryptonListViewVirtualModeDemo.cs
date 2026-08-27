#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Manual repro for issue #3847: <see cref="KryptonListView"/> virtual mode
/// (<see cref="KryptonListView.VirtualMode"/>, <see cref="KryptonListView.VirtualListSize"/>,
/// <see cref="KryptonListView.RetrieveVirtualItem"/>, <see cref="KryptonListView.CacheVirtualItems"/>,
/// <see cref="KryptonListView.SearchForVirtualItem"/>).
/// </summary>
public sealed class KryptonListViewVirtualModeDemo : KryptonForm
{
    private const int ItemCount = 100000;

    private readonly Dictionary<int, ListViewItem> _cache = new();
    private readonly KryptonListView _listView;
    private readonly KryptonLabel _status;
    private readonly KryptonTextBox _searchBox;
    private int _cacheStart = -1;
    private int _cacheEnd = -1;
    private int _lastRetrieveIndex = -1;

    public KryptonListViewVirtualModeDemo()
    {
        Text = @"Issue #3847 — KryptonListView Virtual Mode";
        StartPosition = FormStartPosition.CenterScreen;
        Size = new Size(920, 620);
        MinimumSize = new Size(640, 420);

        var instructions = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = 92,
            Padding = new Padding(12, 8, 12, 4),
            Text =
                "Issue #3847: KryptonListView now exposes VirtualMode / VirtualListSize and forwards RetrieveVirtualItem, " +
                "CacheVirtualItems, and SearchForVirtualItem." + Environment.NewLine +
                "Scroll the 100,000-item list, select a range, type in the list to search, use Find, and change the theme. " +
                "The list must stay responsive and restyle visible rows without enumerating every virtual item."
        };

        _listView = new KryptonListView
        {
            Dock = DockStyle.Fill,
            View = View.Details,
            FullRowSelect = true,
            HideSelection = false,
            GridLines = true,
            MultiSelect = true
        };
        _listView.Columns.Add("Name", 220);
        _listView.Columns.Add("Parity", 80);
        _listView.Columns.Add("Index", 80);
        _listView.RetrieveVirtualItem += OnRetrieveVirtualItem;
        _listView.CacheVirtualItems += OnCacheVirtualItems;
        _listView.SearchForVirtualItem += OnSearchForVirtualItem;
        _listView.SelectedIndexChanged += (_, _) => UpdateStatus();
        _listView.VirtualItemsSelectionRangeChanged += (_, _) => UpdateStatus();
        _listView.VirtualMode = true;
        _listView.VirtualListSize = ItemCount;

        _status = new KryptonLabel
        {
            Dock = DockStyle.Fill,
            AutoSize = false
        };

        _searchBox = new KryptonTextBox
        {
            Dock = DockStyle.Fill,
            CueHint = { CueHintText = @"Find (e.g. Item 12345)" }
        };
        _searchBox.KeyDown += OnSearchKeyDown;

        var findButton = new KryptonButton
        {
            Dock = DockStyle.Fill,
            Values = { Text = @"Find" }
        };
        findButton.Click += (_, _) => FindTypedItem();

        var themeCombo = new KryptonThemeComboBox
        {
            Dock = DockStyle.Fill
        };

        var chrome = new TableLayoutPanel
        {
            Dock = DockStyle.Bottom,
            AutoSize = true,
            Padding = new Padding(12, 4, 12, 12),
            ColumnCount = 4,
            RowCount = 2
        };
        chrome.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        chrome.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 280F));
        chrome.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 88F));
        chrome.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 220F));
        chrome.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        chrome.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        chrome.Controls.Add(_status, 0, 0);
        chrome.SetColumnSpan(_status, 4);
        chrome.Controls.Add(new KryptonLabel
        {
            Dock = DockStyle.Fill,
            AutoSize = false,
            Values = { Text = @"Type-ahead works in the list; Find uses SearchForVirtualItem." }
        }, 0, 1);
        chrome.Controls.Add(_searchBox, 1, 1);
        chrome.Controls.Add(findButton, 2, 1);
        chrome.Controls.Add(themeCombo, 3, 1);

        Controls.Add(_listView);
        Controls.Add(chrome);
        Controls.Add(instructions);

        UpdateStatus();
    }

    private void OnCacheVirtualItems(object? sender, CacheVirtualItemsEventArgs e)
    {
        if (e.StartIndex >= _cacheStart && e.EndIndex <= _cacheEnd && _cache.Count > 0)
        {
            UpdateStatus();
            return;
        }

        _cache.Clear();
        _cacheStart = e.StartIndex;
        _cacheEnd = e.EndIndex;
        for (int i = e.StartIndex; i <= e.EndIndex; i++)
        {
            _cache[i] = CreateItem(i);
        }

        UpdateStatus();
    }

    private void OnRetrieveVirtualItem(object? sender, RetrieveVirtualItemEventArgs e)
    {
        _lastRetrieveIndex = e.ItemIndex;
        if (!_cache.TryGetValue(e.ItemIndex, out ListViewItem? item))
        {
            item = CreateItem(e.ItemIndex);
        }

        e.Item = item;
    }

    private void OnSearchForVirtualItem(object? sender, SearchForVirtualItemEventArgs e)
    {
        if (!e.IsTextSearch || string.IsNullOrEmpty(e.Text))
        {
            return;
        }

        int start = Math.Max(0, e.StartIndex);
        for (int i = start; i < ItemCount; i++)
        {
            if (ItemTextMatches(i, e.Text, e.IsPrefixSearch))
            {
                e.Index = i;
                return;
            }
        }

        for (int i = 0; i < start; i++)
        {
            if (ItemTextMatches(i, e.Text, e.IsPrefixSearch))
            {
                e.Index = i;
                return;
            }
        }
    }

    private void OnSearchKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            FindTypedItem();
        }
    }

    private void FindTypedItem()
    {
        string text = _searchBox.Text.Trim();
        if (text.Length == 0)
        {
            return;
        }

        ListViewItem? match = _listView.FindItemWithText(text, false, 0, true);
        if (match == null)
        {
            _status.Values.Text = $@"No match for '{text}'.";
            return;
        }

        _listView.SelectedIndices.Clear();
        _listView.SelectedIndices.Add(match.Index);
        _listView.EnsureVisible(match.Index);
        _listView.Focus();
        UpdateStatus();
    }

    private void UpdateStatus()
    {
        string cache = _cacheStart < 0
            ? "none"
            : $"{_cacheStart}–{_cacheEnd} ({_cache.Count} items)";
        int selected = _listView.SelectedIndices.Count;
        _status.Values.Text =
            $"VirtualListSize={ItemCount:N0}; last retrieve={_lastRetrieveIndex}; cache={cache}; selected={selected}.";
    }

    private static bool ItemTextMatches(int index, string text, bool prefix)
    {
        string itemText = FormatName(index);
        return prefix
            ? itemText.StartsWith(text, StringComparison.CurrentCultureIgnoreCase)
            : string.Equals(itemText, text, StringComparison.CurrentCultureIgnoreCase);
    }

    private static ListViewItem CreateItem(int index) =>
        new(new[]
        {
            FormatName(index),
            (index % 2) == 0 ? "Even" : "Odd",
            index.ToString(CultureInfo.InvariantCulture)
        });

    private static string FormatName(int index) => $"Item {index}";
}

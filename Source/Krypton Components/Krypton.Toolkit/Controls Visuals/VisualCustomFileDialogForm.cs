#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal sealed partial class VisualCustomFileDialogForm : KryptonForm
{
    private sealed class NavigationTarget
    {
        public NavigationTarget(string text, string path)
        {
            Text = text;
            Path = path;
        }

        public string Text { get; }

        public string Path { get; }
    }

    private sealed class FileEntry
    {
        public FileEntry(string path, bool isDirectory, long length, DateTime lastWriteTime)
        {
            Path = path;
            IsDirectory = isDirectory;
            Length = length;
            LastWriteTime = lastWriteTime;
        }

        public string Path { get; }

        public bool IsDirectory { get; }

        public long Length { get; }

        public DateTime LastWriteTime { get; }

        public string Name => System.IO.Path.GetFileName(Path.TrimEnd(System.IO.Path.DirectorySeparatorChar, System.IO.Path.AltDirectorySeparatorChar));
    }

    private sealed class FilterEntry
    {
        public FilterEntry(string displayName, string rawPattern)
        {
            DisplayName = displayName;
            RawPattern = rawPattern;
            Patterns = rawPattern.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(pattern => pattern.Trim())
                .Where(pattern => pattern.Length > 0)
                .ToArray();
        }

        public string DisplayName { get; }

        public string RawPattern { get; }

        public string[] Patterns { get; }

        public override string ToString() => DisplayName;
    }

    private sealed class ViewModeEntry
    {
        public ViewModeEntry(string displayName, View view)
        {
            DisplayName = displayName;
            View = view;
        }

        public string DisplayName { get; }

        public View View { get; }

        public override string ToString() => DisplayName;
    }

    private sealed class DateModifiedFilterEntry
    {
        public DateModifiedFilterEntry(string displayName, DateModifiedFilter filter)
        {
            DisplayName = displayName;
            Filter = filter;
        }

        public string DisplayName { get; }

        public DateModifiedFilter Filter { get; }

        public override string ToString() => DisplayName;
    }

    private sealed class DirectoryLoadResult
    {
        public List<FileEntry> Entries { get; } = [];

        public string? ErrorMessage { get; set; }
    }

    private enum ShellIconKind
    {
        Place,
        Drive,
        Folder,
        File,
        PlacesRoot,
        DrivesRoot
    }

    private enum DateModifiedFilter
    {
        AnyTime,
        Today,
        Yesterday,
        ThisWeek,
        LastWeek,
        ThisMonth,
        LastMonth,
        ThisYear,
        LastYear
    }

    private const string PLACEHOLDER_NODE_NAME = @"__KryptonExpansionPlaceholder";
    private const int MAXIMUM_AUTOCOMPLETE_SUGGESTIONS = 12;
    private const int MAXIMUM_SEARCH_HISTORY_ITEMS = 20;

    private static KryptonCustomFileDialogStrings DialogStrings => KryptonManager.Strings.CustomFileDialogStrings;

    private static readonly object _searchHistorySync = new object();
    private static readonly List<string> _searchHistory = [];
    private readonly KryptonDialogProviderContext _context;
    private readonly KryptonDialogOptions _options;
    private readonly List<FilterEntry> _filters;
    private readonly List<string> _navigationHistory;
    private readonly Dictionary<string, int> _shellIconCache;
    private readonly System.Windows.Forms.Timer _addressSuggestionTimer;
    private VisualCustomFileDialogSuggestionPopup? _addressSuggestionPopup;
    private VisualCustomFileDialogSuggestionPopup? _searchSuggestionPopup;
    private KryptonDialogResult _providerResult;
    private string _currentPath;
    private List<FileEntry> _loadedEntries;
    private ImageList? _shellSmallImageList;
    private ImageList? _shellLargeImageList;
    private KryptonContextMenuItems? _viewMenuItems;
    private KryptonContextMenu? _breadcrumbContextMenu;
    private KryptonContextMenuItem? _deleteHistoryMenuItem;
    private KryptonComboBox? _dateModifiedComboBox;
    private int _loadGeneration;
    private bool _initialLoadQueued;
    private int _historyIndex;
    private bool _navigatingHistory;
    private bool _updatingBreadcrumbs;
    private bool _suppressAddressEdit;
    private bool _committingAddressEdit;
    private bool _updatingNavigationSelection;
    private int _addressSuggestionGeneration;
    private float _dpiFactorX = 1f;
    private float _dpiFactorY = 1f;

    public VisualCustomFileDialogForm(KryptonDialogProviderContext context)
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        UpdateStyles();

        _context = context;
        _options = context.Options;
        _filters = ParseFilters(_options.Filter);
        _providerResult = new KryptonDialogResult
        {
            DialogResult = DialogResult.Cancel
        };
        _currentPath = ResolveStartingPath();
        _loadedEntries = [];
        _navigationHistory = [];
        _shellIconCache = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        _historyIndex = -1;

        InitializeComponent();
        _addressSuggestionTimer = new System.Windows.Forms.Timer
        {
            Interval = 180
        };
        _addressSuggestionTimer.Tick += OnAddressSuggestionTimerTick;
        InitializeShellIcons();
        InitializeViewModes();
        InitializeBreadcrumbContextMenu();
        InitializeDateModifiedFilter();
        ApplyNavigationButtonGlyphs();
        RefreshDpiFactors();
        ApplyDialogLayout(applyClientSize: true);
        ApplyDialogOptions();
        Shown += OnDialogShown;
        FormClosed += OnDialogFormClosed;
#if !NET462
        DpiChanged += OnDialogDpiChanged;
#endif
    }

    private void InitializeShellIcons()
    {
        DisposeShellIcons();

        _shellSmallImageList = new ImageList
        {
            ColorDepth = ColorDepth.Depth32Bit,
            ImageSize = SystemInformation.SmallIconSize
        };
        _shellLargeImageList = new ImageList
        {
            ColorDepth = ColorDepth.Depth32Bit,
            ImageSize = SystemInformation.IconSize
        };

        _shellIconCache.Clear();

        // Seed a default folder icon before the tree is populated.
        GetShellIconIndex(null, ShellIconKind.Folder);
        _fileList.SmallImageList = _shellSmallImageList;
        _fileList.LargeImageList = _shellLargeImageList;
        _navigationTree.ImageList = _shellSmallImageList;
    }

    private void DisposeShellIcons()
    {
        if (_navigationTree.ImageList != null)
        {
            _navigationTree.ImageList = null;
        }

        _fileList.SmallImageList = null;
        _fileList.LargeImageList = null;

        _shellSmallImageList?.Dispose();
        _shellLargeImageList?.Dispose();
        _shellSmallImageList = null;
        _shellLargeImageList = null;
    }

    private void ApplyDialogLayout(bool applyClientSize)
    {
        if (applyClientSize)
        {
            ClientSize = ScaleSize(946, 533);
        }

        MinimumSize = ScaleSize(679, 495);

        _rootPanel.Padding = ScalePadding(10);

        _chromeLayout.RowStyles.Clear();
        _chromeLayout.RowCount = 3;
        _chromeLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, ScaleY(36F)));
        _chromeLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _chromeLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        _navigationLayout.AutoSize = false;
        _navigationLayout.Dock = DockStyle.Fill;
        _navigationLayout.Margin = Padding.Empty;
        _navigationLayout.ColumnStyles.Clear();
        _navigationLayout.ColumnCount = 8;
        _navigationLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, ScaleX(34F)));
        _navigationLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, ScaleX(34F)));
        _navigationLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, ScaleX(34F)));
        _navigationLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, ScaleX(78F)));
        _navigationLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _navigationLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, ScaleX(126F)));
        if (_options.ShowDateModifiedFilter)
        {
            _navigationLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, ScaleX(150F)));
            _navigationLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, ScaleX(180F)));
        }
        else
        {
            _navigationLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, ScaleX(58F)));
            _navigationLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, ScaleX(180F)));
        }

        ConfigureToolbarButton(_backButton, ScaleSize(30, 28));
        ConfigureToolbarButton(_forwardButton, ScaleSize(30, 28));
        ConfigureToolbarButton(_upButton, ScaleSize(30, 28));
        ConfigureToolbarButton(_refreshButton, ScaleSize(74, 28));
        _refreshButton.Values.Text = DialogStrings.Refresh;

        _addressHost.Dock = DockStyle.Fill;
        _addressHost.Margin = ScalePadding(4, 2, 4, 2);
        _addressHost.Padding = Padding.Empty;
        _addressHost.BackColor = Color.Transparent;
        _addressHost.MinimumSize = ScaleSize(160, 28);
        _addressBar.Dock = DockStyle.Fill;
        _addressBar.AutoSize = false;
        _addressEditBox.Dock = DockStyle.Fill;

        ConfigureToolbarButton(_viewButton, ScaleSize(120, 28));
        _viewButton.ShowSplitOption = true;
        _viewButton.Values.ShowSplitOption = true;

        if (_dateModifiedComboBox != null)
        {
            // Search cue is enough; the former Search label column hosts the date-modified filter.
            _searchLabel.Visible = false;
            _dateModifiedComboBox.Dock = DockStyle.Fill;
            _dateModifiedComboBox.Margin = ScalePadding(4, 2, 2, 2);
            _dateModifiedComboBox.MinimumSize = ScaleSize(140, 28);
        }
        else
        {
            _searchLabel.Visible = true;
            _searchLabel.Anchor = AnchorStyles.Left;
            _searchLabel.Margin = ScalePadding(4, 0, 2, 0);
            _searchLabel.Values.Text = DialogStrings.SearchLabel;
        }

        _searchTextBox.Dock = DockStyle.Fill;
        _searchTextBox.Margin = ScalePadding(0, 2, 0, 2);

        _splitContainer.Margin = ScalePadding(0, 0, 0, 8);
        _splitContainer.Panel1MinSize = ScaleX(180);
        _splitContainer.Panel2MinSize = ScaleX(280);
        if (_splitContainer.Width > ScaleX(500))
        {
            _splitContainer.SplitterDistance = Math.Max(ScaleX(180), Math.Min(ScaleX(280), _splitContainer.Width / 3));
        }
        else
        {
            _splitContainer.SplitterDistance = ScaleX(240);
        }

        _navigationTree.Dock = DockStyle.Fill;
        _navigationTree.Margin = Padding.Empty;
        _fileList.Dock = DockStyle.Fill;
        _fileList.Margin = Padding.Empty;

        _bottomLayout.AutoSize = true;
        _bottomLayout.Margin = Padding.Empty;
        _bottomLayout.Padding = ScalePadding(0, 4, 0, 0);
        _bottomLayout.ColumnStyles.Clear();
        _bottomLayout.ColumnCount = 4;
        _bottomLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _bottomLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _bottomLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _bottomLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _bottomLayout.RowStyles.Clear();
        _bottomLayout.RowCount = 3;
        _bottomLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _bottomLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _bottomLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, ScaleY(24F)));

        _fileNameLabel.Anchor = AnchorStyles.Left;
        _fileNameLabel.Margin = ScalePadding(0, 4, 8, 4);
        _fileNameTextBox.Dock = DockStyle.Fill;
        _fileNameTextBox.Margin = ScalePadding(0, 2, 0, 2);
        _filterLabel.Anchor = AnchorStyles.Left;
        _filterLabel.Margin = ScalePadding(0, 4, 8, 4);
        _filterComboBox.Dock = DockStyle.Fill;
        _filterComboBox.Margin = ScalePadding(0, 2, 8, 2);

        ConfigureToolbarButton(_acceptButton, ScaleSize(110, 28));
        ConfigureToolbarButton(_cancelButton, ScaleSize(90, 28));
        _acceptButton.Margin = ScalePadding(8, 2, 4, 2);
        _cancelButton.Margin = ScalePadding(0, 2, 0, 2);

        _statusLabel.Dock = DockStyle.Fill;
        _statusLabel.Margin = ScalePadding(0, 4, 0, 0);
        _statusLabel.AutoSize = false;

        if (_fileList.View == View.Tile)
        {
            _fileList.TileSize = ScaleSize(216, 48);
        }

        ApplyScaledColumnWidths();
    }

    private void ApplyScaledColumnWidths()
    {
        _columnName.Width = ScaleX(280);
        _columnType.Width = ScaleX(120);
        _columnModified.Width = ScaleX(180);
        _columnSize.Width = ScaleX(120);
    }

    private int ScaleX(int value) => (int)Math.Round(value * _dpiFactorX);

    private int ScaleY(int value) => (int)Math.Round(value * _dpiFactorY);

    private float ScaleX(float value) => value * _dpiFactorX;

    private float ScaleY(float value) => value * _dpiFactorY;

    /// <summary>
    /// Caches the DPI factors for the monitor hosting the dialog. Returns <c>true</c> when they changed,
    /// so scaled layout is only re-applied when it would actually differ.
    /// </summary>
    private bool RefreshDpiFactors()
    {
        var factorX = Math.Max(FactorDpiX, 0.1f);
        var factorY = Math.Max(FactorDpiY, 0.1f);

        if (IsHandleCreated)
        {
            // FactorDpi* is seeded from the primary monitor before the handle exists.
            factorX = Math.Max(KryptonManager.GetDpiFactorX(Handle), 0.1f);
            factorY = Math.Max(KryptonManager.GetDpiFactorY(Handle), 0.1f);
        }

        if (Math.Abs(factorX - _dpiFactorX) < 0.01f && Math.Abs(factorY - _dpiFactorY) < 0.01f)
        {
            return false;
        }

        _dpiFactorX = factorX;
        _dpiFactorY = factorY;
        return true;
    }

    /// <summary>
    /// Raises the HandleCreated event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);

        // Re-scale before the window is shown when it lands on a monitor with a different DPI
        // than the one used while constructing; doing it later would resize a visible dialog.
        if (RefreshDpiFactors())
        {
            ApplyDialogLayout(applyClientSize: true);
            InitializeShellIcons();
        }
    }

    private Size ScaleSize(int width, int height) => new Size(ScaleX(width), ScaleY(height));

    private Padding ScalePadding(int all) => new Padding(ScaleX(all), ScaleY(all), ScaleX(all), ScaleY(all));

    private Padding ScalePadding(int left, int top, int right, int bottom) =>
        new Padding(ScaleX(left), ScaleY(top), ScaleX(right), ScaleY(bottom));

#if !NET462
    private void OnDialogDpiChanged(object? sender, DpiChangedEventArgs e)
    {
        // VisualForm already refreshed FactorDpi* and WinForms has scaled the window itself.
        // Re-apply the absolute layout metrics and rebuild shell icons for the new DPI.
        if (RefreshDpiFactors())
        {
            ApplyDialogLayout(applyClientSize: false);
            RebuildShellIconsForDpi();
        }
    }
#endif

    private void RebuildShellIconsForDpi()
    {
        var hadTree = _navigationTree.Nodes.Count > 0;
        InitializeShellIcons();
        if (hadTree)
        {
            BuildNavigationTree();
            SelectNavigationNode(_currentPath);
            ApplyEntryFilter(suspendUpdates: false);
        }
    }

    private static void ConfigureToolbarButton(KryptonButton button, Size size)
    {
        button.Anchor = AnchorStyles.None;
        button.AutoSize = false;
        button.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        button.Size = size;
        button.MinimumSize = size;
        button.Margin = new Padding(1);
        button.Dock = DockStyle.None;
    }

    private void InitializeViewModes()
    {
        _viewMenuItems = new KryptonContextMenuItems();
        foreach (var mode in GetViewModes())
        {
            var item = new KryptonContextMenuItem(mode.DisplayName)
            {
                Tag = mode.View
            };
            item.Click += OnViewMenuItemClick;
            _viewMenuItems.Items.Add(item);
        }

        var viewContextMenu = new KryptonContextMenu();
        viewContextMenu.Items.Add(_viewMenuItems);
        _viewButton.KryptonContextMenu = viewContextMenu;
        ApplyViewMode(View.Details);
    }

    private void InitializeBreadcrumbContextMenu()
    {
        var strings = DialogStrings;
        var copyAddressItem = new KryptonContextMenuItem(strings.CopyAddress, OnCopyAddress);
        var copyAddressAsTextItem = new KryptonContextMenuItem(strings.CopyAddressAsText, OnCopyAddressAsText);
        var editAddressItem = new KryptonContextMenuItem(strings.EditAddress, OnEditAddress);
        _deleteHistoryMenuItem = new KryptonContextMenuItem(strings.DeleteHistory, OnDeleteAddressHistory);

        var items = new KryptonContextMenuItems
        {
            ImageColumn = false
        };
        items.Items.Add(copyAddressItem);
        items.Items.Add(copyAddressAsTextItem);
        items.Items.Add(editAddressItem);
        items.Items.Add(new KryptonContextMenuSeparator());
        items.Items.Add(_deleteHistoryMenuItem);

        _breadcrumbContextMenu = new KryptonContextMenu();
        _breadcrumbContextMenu.Items.Add(items);
    }

    private void InitializeDateModifiedFilter()
    {
        if (!_options.ShowDateModifiedFilter)
        {
            return;
        }

        var strings = DialogStrings;
        _dateModifiedComboBox = new KryptonComboBox
        {
            DropDownStyle = ComboBoxStyle.DropDownList,
            IntegralHeight = false
        };
        _dateModifiedComboBox.Items.AddRange(
        [
            new DateModifiedFilterEntry(strings.DateModifiedAnyTime, DateModifiedFilter.AnyTime),
            new DateModifiedFilterEntry(strings.DateModifiedToday, DateModifiedFilter.Today),
            new DateModifiedFilterEntry(strings.DateModifiedYesterday, DateModifiedFilter.Yesterday),
            new DateModifiedFilterEntry(strings.DateModifiedThisWeek, DateModifiedFilter.ThisWeek),
            new DateModifiedFilterEntry(strings.DateModifiedLastWeek, DateModifiedFilter.LastWeek),
            new DateModifiedFilterEntry(strings.DateModifiedThisMonth, DateModifiedFilter.ThisMonth),
            new DateModifiedFilterEntry(strings.DateModifiedLastMonth, DateModifiedFilter.LastMonth),
            new DateModifiedFilterEntry(strings.DateModifiedThisYear, DateModifiedFilter.ThisYear),
            new DateModifiedFilterEntry(strings.DateModifiedLastYear, DateModifiedFilter.LastYear)
        ]);
        _dateModifiedComboBox.SelectedIndex = 0;
        _dateModifiedComboBox.SelectedIndexChanged += OnDateModifiedFilterChanged;
        _dateModifiedComboBox.AccessibleName = strings.DateModified;
        _dateModifiedComboBox.ToolTipValues.EnableToolTips = true;
        _dateModifiedComboBox.ToolTipValues.Heading = strings.DateModified;

        // Reuse the Search label column so the toolbar stays at eight columns.
        _navigationLayout.Controls.Remove(_searchLabel);
        _navigationLayout.Controls.Add(_dateModifiedComboBox, 6, 0);
        _navigationLayout.SetCellPosition(_searchTextBox, new TableLayoutPanelCellPosition(7, 0));
    }

    private ViewModeEntry[] GetViewModes()
    {
        var strings = DialogStrings;
        return
        [
            new ViewModeEntry(strings.ViewDetails, View.Details),
            new ViewModeEntry(strings.ViewLargeIcons, View.LargeIcon),
            new ViewModeEntry(strings.ViewSmallIcons, View.SmallIcon),
            new ViewModeEntry(strings.ViewList, View.List),
            new ViewModeEntry(strings.ViewTiles, View.Tile)
        ];
    }

    private void OnViewButtonClick(object? sender, EventArgs e)
    {
        var modes = GetViewModes();
        var currentIndex = Array.FindIndex(modes, mode => mode.View == _fileList.View);
        if (currentIndex < 0)
        {
            currentIndex = 0;
        }

        var nextMode = modes[(currentIndex + 1) % modes.Length];
        ApplyViewMode(nextMode.View);
    }

    private void OnViewMenuItemClick(object? sender, EventArgs e)
    {
        if (sender is KryptonContextMenuItem { Tag: View view })
        {
            ApplyViewMode(view);
        }
    }

    private void ApplyViewMode(View view)
    {
        _fileList.View = view;
        _fileList.FullRowSelect = view == View.Details;
        if (view == View.Tile)
        {
            _fileList.TileSize = ScaleSize(216, 48);
        }

        var modes = GetViewModes();
        var selectedMode = modes.FirstOrDefault(mode => mode.View == view) ?? modes[0];
        _viewButton.Values.Text = selectedMode.DisplayName;
        var viewCaption = string.Format(CultureInfo.CurrentCulture, DialogStrings.ViewPrefix, selectedMode.DisplayName);
        _viewButton.ToolTipValues.Heading = viewCaption;
        _viewButton.AccessibleName = viewCaption;

        if (_viewMenuItems == null)
        {
            return;
        }

        foreach (KryptonContextMenuItemBase itemBase in _viewMenuItems.Items)
        {
            if (itemBase is KryptonContextMenuItem item)
            {
                item.Checked = item.Tag is View itemView && itemView == view;
            }
        }
    }

    private void ApplyNavigationButtonGlyphs()
    {
        var palette = KryptonManager.CurrentGlobalPalette;
        var strings = DialogStrings;
        ConfigureGlyphButton(
            _backButton,
            palette.GetButtonSpecImage(PaletteButtonSpecStyle.ArrowLeft, PaletteState.Normal),
            palette.GetButtonSpecImageTransparentColor(PaletteButtonSpecStyle.ArrowLeft),
            strings.Back);
        ConfigureGlyphButton(
            _forwardButton,
            palette.GetButtonSpecImage(PaletteButtonSpecStyle.ArrowRight, PaletteState.Normal),
            palette.GetButtonSpecImageTransparentColor(PaletteButtonSpecStyle.ArrowRight),
            strings.Forward);
        ConfigureGlyphButton(
            _upButton,
            palette.GetButtonSpecImage(PaletteButtonSpecStyle.ArrowUp, PaletteState.Normal),
            palette.GetButtonSpecImageTransparentColor(PaletteButtonSpecStyle.ArrowUp),
            strings.Up);
    }

    private static void ConfigureGlyphButton(KryptonButton button, Image? image, Color imageTransparentColor, string toolTip)
    {
        button.Values.Text = string.Empty;
        button.Values.Image = image;
        button.Values.ImageTransparentColor = imageTransparentColor;
        button.ToolTipValues.EnableToolTips = true;
        button.ToolTipValues.Heading = toolTip;
        button.AccessibleName = toolTip;
    }

    private void ApplyDialogOptions()
    {
        var strings = DialogStrings;
        Text = string.IsNullOrWhiteSpace(_options.Title) ? GetDefaultCaption() : _options.Title;
        Icon = _options.Icon;
        _addressEditBox.Text = _currentPath;
        _fileNameTextBox.Text = ResolveInitialFileName();
        ApplyFileNameCueHint();
        _addressEditBox.CueHint.CueHintText = strings.AddressPathCueText;
        _searchTextBox.CueHint.CueHintText = strings.SearchCueText;
        _fileNameLabel.Values.Text = _options.Kind == KryptonDialogKind.SelectFolder
            ? strings.FolderLabel
            : strings.FileNameLabel;
        _filterLabel.Values.Text = strings.FilterLabel;
        _acceptButton.Values.Text = GetAcceptCaption();
        _cancelButton.Values.Text = KryptonManager.Strings.GeneralStrings.Cancel;
        _columnName.Text = strings.ColumnName;
        _columnType.Text = strings.ColumnType;
        _columnModified.Text = strings.ColumnModified;
        _columnSize.Text = strings.ColumnSize;
        ApplyScaledColumnWidths();

        var showFilter = _options.Kind != KryptonDialogKind.SelectFolder;
        _filterLabel.Visible = showFilter;
        _filterComboBox.Visible = showFilter;

        foreach (var filter in _filters)
        {
            _filterComboBox.Items.Add(filter);
        }

        if (_filterComboBox.Items.Count > 0)
        {
            var selectedIndex = Math.Max(0, Math.Min(_options.FilterIndex - 1, _filterComboBox.Items.Count - 1));
            _filterComboBox.SelectedIndex = selectedIndex;
        }

        ShowStatus(strings.Loading);
    }

    private void OnBackButtonClick(object? sender, EventArgs e) => NavigateHistory(-1);

    private void OnForwardButtonClick(object? sender, EventArgs e) => NavigateHistory(1);

    private void OnUpButtonClick(object? sender, EventArgs e) => NavigateUp();

    private void OnRefreshButtonClick(object? sender, EventArgs e) => RefreshListing();

    private void OnSearchTextChanged(object? sender, EventArgs e)
    {
        ApplyEntryFilter();
        UpdateSearchSuggestions();
    }

    private void OnDateModifiedFilterChanged(object? sender, EventArgs e) => ApplyEntryFilter();

    private void OnSearchKeyDown(object? sender, KeyEventArgs e)
    {
        if (HandleSuggestionKeys(_searchSuggestionPopup, e))
        {
            return;
        }

        if (e.KeyCode == Keys.Enter)
        {
            RememberSearchTerm(_searchTextBox.Text);
            CloseSearchSuggestions();
            e.Handled = true;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Escape)
        {
            _searchTextBox.Clear();
            CloseSearchSuggestions();
            e.Handled = true;
            e.SuppressKeyPress = true;
        }
    }

    private void OnFilterSelectedIndexChanged(object? sender, EventArgs e) => RefreshListing();

    private void OnAcceptButtonClick(object? sender, EventArgs e) => TryAcceptSelection();

    private void OnCancelButtonClick(object? sender, EventArgs e)
    {
        _providerResult.DialogResult = DialogResult.Cancel;
        Close();
    }

    public KryptonDialogResult ShowProviderDialog()
    {
        var owner = _context.Owner;
        var dialogResult = owner != null ? ShowDialog(owner) : ShowDialog();
        if (_providerResult.DialogResult == DialogResult.None)
        {
            _providerResult.DialogResult = dialogResult;
        }

        return _providerResult;
    }

    public async Task<KryptonDialogResult> ShowProviderDialogAsync()
    {
        var owner = _context.Owner;
        // Await required: provider result is finalized after the dialog closes.
        var dialogResult = await KryptonFormAsync.ShowDialogAsync(this, owner).ConfigureAwait(false);
        if (_providerResult.DialogResult == DialogResult.None)
        {
            _providerResult.DialogResult = dialogResult;
        }

        return _providerResult;
    }

    private string GetDefaultCaption() => _options.Kind switch
    {
        KryptonDialogKind.SaveFile => DialogStrings.Save,
        KryptonDialogKind.SelectFolder => DialogStrings.SelectFolder,
        _ => DialogStrings.Open
    };

    private string GetAcceptCaption() => _options.Kind switch
    {
        KryptonDialogKind.SaveFile => DialogStrings.Save,
        KryptonDialogKind.SelectFolder => DialogStrings.SelectFolder,
        _ => DialogStrings.Open
    };

    private string ResolveInitialFileName()
    {
        if (_options.Kind == KryptonDialogKind.SelectFolder)
        {
            return _currentPath;
        }

        if (!string.IsNullOrWhiteSpace(_options.FileName))
        {
            return System.IO.Path.GetFileName(_options.FileName);
        }

        return string.Empty;
    }

    private void ApplyFileNameCueHint()
    {
        var strings = DialogStrings;
        _fileNameTextBox.CueHint.CueHintText = _options.Kind == KryptonDialogKind.SelectFolder
            ? strings.FolderPathCueText
            : strings.FileNameCueText;
    }

    private string ResolveStartingPath()
    {
        var candidates = new[]
        {
            _options.CurrentPath,
            _options.InitialDirectory,
            ExtractDirectoryName(_options.FileName),
            Environment.GetFolderPath(_options.RootFolder),
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
        };

        foreach (var candidate in candidates)
        {
            if (!string.IsNullOrWhiteSpace(candidate) && Directory.Exists(candidate))
            {
                return candidate;
            }
        }

        return Environment.CurrentDirectory;
    }

    private static string ExtractDirectoryName(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return string.Empty;
        }

        try
        {
            return System.IO.Path.GetDirectoryName(path) ?? string.Empty;
        }
        catch
        {
            return string.Empty;
        }
    }

    private static List<FilterEntry> ParseFilters(string filter)
    {
        var filters = new List<FilterEntry>();
        if (string.IsNullOrWhiteSpace(filter))
        {
            filters.Add(new FilterEntry(DialogStrings.AllFilesFilter, @"*.*"));
            return filters;
        }

        var parts = filter.Split('|');
        for (var i = 0; i + 1 < parts.Length; i += 2)
        {
            if (!string.IsNullOrWhiteSpace(parts[i]) && !string.IsNullOrWhiteSpace(parts[i + 1]))
            {
                filters.Add(new FilterEntry(parts[i], parts[i + 1]));
            }
        }

        if (filters.Count == 0)
        {
            filters.Add(new FilterEntry(DialogStrings.AllFilesFilter, @"*.*"));
        }

        return filters;
    }

    private void BuildNavigationTree()
    {
        // Suspend redraw before detaching the ImageList; detaching changes the node height and
        // would otherwise repaint the tree once without icons before the nodes are rebuilt.
        _navigationTree.BeginUpdate();
        try
        {
            // Icons are added to the list while it is detached, so the tree is not invalidated per icon.
            _navigationTree.ImageList = null;
            _navigationTree.Nodes.Clear();

            var strings = DialogStrings;
            var placesIcon = GetShellIconIndex(null, ShellIconKind.PlacesRoot);
            var placesNode = new TreeNode(strings.CommonPlaces, placesIcon, placesIcon);
            AddPlaceNode(placesNode, strings.Desktop, Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            AddPlaceNode(placesNode, strings.Documents, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            AddPlaceNode(placesNode, strings.Pictures, Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
            AddPlaceNode(placesNode, strings.Music, Environment.GetFolderPath(Environment.SpecialFolder.MyMusic));
            AddPlaceNode(placesNode, strings.Downloads, System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), @"Downloads"));

            // RootFolder is a starting/fallback path only (ResolveStartingPath); do not surface it as a place node.

            if (_options.CustomPlaces.Count > 0)
            {
                var customPlacesIcon = GetShellIconIndex(null, ShellIconKind.Folder);
                var customPlacesNode = new TreeNode(strings.CustomPlaces, customPlacesIcon, customPlacesIcon);
                foreach (var customPlace in _options.CustomPlaces)
                {
                    if (Directory.Exists(customPlace))
                    {
                        AddPlaceNode(customPlacesNode, customPlace, customPlace);
                    }
                }

                if (customPlacesNode.Nodes.Count > 0)
                {
                    customPlacesNode.Expand();
                    _navigationTree.Nodes.Add(customPlacesNode);
                }
            }

            placesNode.Expand();
            _navigationTree.Nodes.Add(placesNode);

            var drivesIcon = GetShellIconIndex(null, ShellIconKind.DrivesRoot);
            var drivesNode = new TreeNode(strings.Drives, drivesIcon, drivesIcon);
            foreach (var drive in DriveInfo.GetDrives().Where(static drive => drive.IsReady))
            {
                AddPlaceNode(drivesNode, drive.Name, drive.RootDirectory.FullName, isDrive: true);
            }

            drivesNode.Expand();
            _navigationTree.Nodes.Add(drivesNode);
        }
        finally
        {
            _navigationTree.ImageList = _shellSmallImageList;
            _navigationTree.EndUpdate();
        }
    }

    private void AddPlaceNode(TreeNode parent, string text, string path, bool isDrive = false)
    {
        if (string.IsNullOrWhiteSpace(path) || !Directory.Exists(path))
        {
            return;
        }

        var iconIndex = GetShellIconIndex(path, isDrive ? ShellIconKind.Drive : ShellIconKind.Place);
        var node = new TreeNode(text, iconIndex, iconIndex)
        {
            Tag = new NavigationTarget(text, path)
        };

        AddExpansionPlaceholder(node, path);
        parent.Nodes.Add(node);
    }

    /// <summary>
    /// Gives the node an expand glyph without walking the folder tree; the real children are loaded on first expand.
    /// </summary>
    private static void AddExpansionPlaceholder(TreeNode node, string path)
    {
        if (HasSubDirectories(path))
        {
            node.Nodes.Add(new TreeNode(string.Empty) { Name = PLACEHOLDER_NODE_NAME });
        }
    }

    private static bool HasSubDirectories(string path)
    {
        try
        {
            return Directory.EnumerateDirectories(path).Any();
        }
        catch (Exception)
        {
            // Unreadable or disconnected locations are treated as having nothing to expand.
            return false;
        }
    }

    private static bool IsExpansionPlaceholder(TreeNode node) =>
        node.Tag == null && string.Equals(node.Name, PLACEHOLDER_NODE_NAME, StringComparison.Ordinal);

    /// <summary>
    /// Replaces the placeholder child with the sub folders of the node target; does nothing once already loaded.
    /// </summary>
    private void PopulateChildNodes(TreeNode node)
    {
        if (node.Tag is not NavigationTarget navigationTarget
            || node.Nodes.Count != 1
            || !IsExpansionPlaceholder(node.Nodes[0]))
        {
            return;
        }

        _navigationTree.BeginUpdate();
        try
        {
            // Adding shell icons to an attached ImageList invalidates the tree for every image.
            _navigationTree.ImageList = null;
            node.Nodes.Clear();
            foreach (var directory in EnumerateSubDirectories(navigationTarget.Path))
            {
                var iconIndex = GetShellIconIndex(directory.FullName, ShellIconKind.Folder);
                var child = new TreeNode(directory.Name, iconIndex, iconIndex)
                {
                    Tag = new NavigationTarget(directory.Name, directory.FullName)
                };

                AddExpansionPlaceholder(child, directory.FullName);
                node.Nodes.Add(child);
            }
        }
        finally
        {
            _navigationTree.ImageList = _shellSmallImageList;
            _navigationTree.EndUpdate();
        }
    }

    private static DirectoryInfo[] EnumerateSubDirectories(string path)
    {
        try
        {
            return new DirectoryInfo(path).GetDirectories()
                .OrderBy(static dir => dir.Name, StringComparer.CurrentCultureIgnoreCase)
                .ToArray();
        }
        catch (Exception)
        {
            return [];
        }
    }

    private int GetShellIconIndex(string? path, ShellIconKind kind)
    {
        if (_shellSmallImageList == null || _shellLargeImageList == null)
        {
            return -1;
        }

        var cacheKey = kind switch
        {
            ShellIconKind.Folder => @"DIR",
            ShellIconKind.File => @"FILE:" + System.IO.Path.GetExtension(path ?? string.Empty),
            ShellIconKind.Drive => @"DRIVE:" + (path ?? string.Empty),
            ShellIconKind.Place => @"PLACE:" + (path ?? string.Empty),
            ShellIconKind.PlacesRoot => @"ROOT:PLACES",
            ShellIconKind.DrivesRoot => @"ROOT:DRIVES",
            _ => @"DEFAULT"
        };

        if (_shellIconCache.TryGetValue(cacheKey, out var cachedIndex))
        {
            return cachedIndex;
        }

        using var smallIcon = ResolveShellIcon(path, kind, largeIcon: false);
        using var largeIcon = ResolveShellIcon(path, kind, largeIcon: true) ?? ResolveShellIcon(path, kind, largeIcon: false);
        if (smallIcon == null && largeIcon == null)
        {
            return -1;
        }

        var index = AddShellIcon(smallIcon ?? largeIcon!, largeIcon ?? smallIcon!);
        if (index >= 0)
        {
            _shellIconCache[cacheKey] = index;
            return index;
        }

        return -1;
    }

    private static Icon? ResolveShellIcon(string? path, ShellIconKind kind, bool largeIcon)
    {
        try
        {
            switch (kind)
            {
                case ShellIconKind.Drive:
                case ShellIconKind.Place:
                    if (string.IsNullOrWhiteSpace(path))
                    {
                        return FileSystemIconHelper.GetFolderIcon(largeIcon);
                    }

                    return FileSystemIconHelper.GetFileSystemIcon(path!, largeIcon);
                case ShellIconKind.Folder:
                    return FileSystemIconHelper.GetFolderIcon(largeIcon);
                case ShellIconKind.File:
                    return FileSystemIconHelper.GetFileIcon(System.IO.Path.GetExtension(path ?? string.Empty), largeIcon);
                case ShellIconKind.PlacesRoot:
                    return FileSystemIconHelper.GetFileSystemIcon(
                               Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                               largeIcon)
                           ?? FileSystemIconHelper.GetFolderIcon(largeIcon);
                case ShellIconKind.DrivesRoot:
                    foreach (var drive in DriveInfo.GetDrives().Where(static drive => drive.IsReady))
                    {
                        var driveIcon = FileSystemIconHelper.GetFileSystemIcon(drive.RootDirectory.FullName, largeIcon);
                        if (driveIcon != null)
                        {
                            return driveIcon;
                        }
                    }

                    return FileSystemIconHelper.GetFolderIcon(largeIcon);
                default:
                    return FileSystemIconHelper.GetFolderIcon(largeIcon);
            }
        }
        catch
        {
            return null;
        }
    }

    private int AddShellIcon(Icon smallIcon, Icon largeIcon)
    {
        if (_shellSmallImageList == null || _shellLargeImageList == null)
        {
            return -1;
        }

        var index = _shellSmallImageList.Images.Count;
        AddIconToImageList(_shellSmallImageList, smallIcon);
        AddIconToImageList(_shellLargeImageList, largeIcon);
        return index;
    }

    private static void AddIconToImageList(ImageList imageList, Icon icon)
    {
        using var sourceBitmap = icon.ToBitmap();
        var bitmapToAdd = new Bitmap(
            imageList.ImageSize.Width,
            imageList.ImageSize.Height,
            PixelFormat.Format32bppArgb);

        try
        {
            using (var graphics = Graphics.FromImage(bitmapToAdd))
            {
                graphics.Clear(Color.Transparent);
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(sourceBitmap, 0, 0, imageList.ImageSize.Width, imageList.ImageSize.Height);
            }

            imageList.Images.Add(bitmapToAdd);
            _ = imageList.Handle;
        }
        finally
        {
            bitmapToAdd.Dispose();
        }
    }

    private void OnDialogShown(object? sender, EventArgs e)
    {
        if (_initialLoadQueued)
        {
            return;
        }

        _initialLoadQueued = true;
        if (_splitContainer.Width > ScaleX(500))
        {
            _splitContainer.SplitterDistance = Math.Max(ScaleX(200), Math.Min(ScaleX(280), _splitContainer.Width / 3));
        }

        BuildNavigationTree();
        NavigateToPath(_currentPath, updatePathText: true, selectTreeNode: true);
    }

    private void NavigateHistory(int direction)
    {
        var nextIndex = _historyIndex + direction;
        if (nextIndex < 0 || nextIndex >= _navigationHistory.Count)
        {
            return;
        }

        _navigatingHistory = true;
        try
        {
            _historyIndex = nextIndex;
            NavigateToPath(_navigationHistory[_historyIndex], updatePathText: true, selectTreeNode: true, recordHistory: false);
        }
        finally
        {
            _navigatingHistory = false;
            UpdateNavigationButtons();
        }
    }

    private void NavigateUp()
    {
        try
        {
            var parent = Directory.GetParent(_currentPath);
            if (parent != null)
            {
                NavigateToPath(parent.FullName, updatePathText: true, selectTreeNode: true);
            }
        }
        catch (Exception ex)
        {
            ShowStatus(ex.Message);
        }
    }

    private void RefreshListing() => NavigateToPath(_currentPath, updatePathText: false, selectTreeNode: false, recordHistory: false, forceReload: true);

    private void NavigateToPath(string path, bool updatePathText, bool selectTreeNode, bool recordHistory = true, bool forceReload = false)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return;
        }

        try
        {
            var normalizedPath = System.IO.Path.GetFullPath(path);
            if (!Directory.Exists(normalizedPath))
            {
                ShowStatus(string.Format(CultureInfo.CurrentCulture, DialogStrings.PathDoesNotExist, normalizedPath));
                return;
            }

            // Skip only after the first successful listing of this folder (re-entrant tree/address sync).
            // Constructor pre-sets _currentPath, so the first Shown navigate must not early-return
            // or breadcrumbs stay on the default "Root" caption and the file list never loads.
            if (!forceReload
                && _loadGeneration > 0
                && string.Equals(_currentPath, normalizedPath, StringComparison.OrdinalIgnoreCase))
            {
                if (updatePathText)
                {
                    _addressEditBox.Text = normalizedPath;
                }

                if (selectTreeNode)
                {
                    SelectNavigationNode(normalizedPath);
                }

                return;
            }

            _currentPath = normalizedPath;
            if (updatePathText)
            {
                _addressEditBox.Text = normalizedPath;
            }

            UpdateBreadcrumbs(normalizedPath);
            if (recordHistory)
            {
                RecordNavigationHistory(normalizedPath);
            }

            if (_options.Kind == KryptonDialogKind.SelectFolder)
            {
                _fileNameTextBox.Text = normalizedPath;
            }

            LoadEntriesAsync(normalizedPath);
            if (selectTreeNode)
            {
                SelectNavigationNode(normalizedPath);
            }
        }
        catch (Exception ex)
        {
            ShowStatus(ex.Message);
        }
    }

    private void LoadEntriesAsync(string path)
    {
        _loadGeneration++;
        var generation = _loadGeneration;
        var patterns = GetSelectedFilterPatterns();
        _loadedEntries = [];
        ShowStatus(string.Format(CultureInfo.CurrentCulture, DialogStrings.LoadingPath, path));

        Task.Run(() => LoadEntriesCore(path, patterns))
            .ContinueWith(task =>
            {
                if (IsDisposed || Disposing || generation != _loadGeneration)
                {
                    return;
                }

                _fileList.BeginUpdate();
                try
                {
                    var result = task.Status == TaskStatus.RanToCompletion
                        ? task.Result
                        : new DirectoryLoadResult { ErrorMessage = task.Exception?.GetBaseException().Message ?? DialogStrings.UnableToLoadDirectory };

                    if (!string.IsNullOrWhiteSpace(result.ErrorMessage))
                    {
                        _fileList.Items.Clear();
                        ShowStatus(result.ErrorMessage ?? DialogStrings.UnableToLoadDirectory);
                        return;
                    }

                    _loadedEntries = result.Entries;
                    ApplyEntryFilter(suspendUpdates: false);
                    UpdateSearchSuggestions();
                }
                finally
                {
                    _fileList.EndUpdate();
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
    }

    private DirectoryLoadResult LoadEntriesCore(string path, string[] patterns)
    {
        var result = new DirectoryLoadResult();

        try
        {
            foreach (var directory in new DirectoryInfo(path).GetDirectories().OrderBy(static dir => dir.Name))
            {
                result.Entries.Add(new FileEntry(directory.FullName, isDirectory: true, 0, directory.LastWriteTime));
            }

            if (_options.Kind != KryptonDialogKind.SelectFolder)
            {
                foreach (var file in new DirectoryInfo(path).GetFiles()
                             .Where(file => MatchesSelectedFilter(file, patterns))
                             .OrderBy(static file => file.Name))
                {
                    result.Entries.Add(new FileEntry(file.FullName, isDirectory: false, file.Length, file.LastWriteTime));
                }
            }
        }
        catch (Exception ex)
        {
            result.ErrorMessage = ex.Message;
        }

        return result;
    }

    private void AddEntry(FileEntry entry)
    {
        var iconIndex = GetShellIconIndex(
            entry.Path,
            entry.IsDirectory ? ShellIconKind.Folder : ShellIconKind.File);
        var item = new ListViewItem(entry.Name, iconIndex)
        {
            Tag = entry
        };
        item.SubItems.Add(entry.IsDirectory ? DialogStrings.FolderType : GetTypeDescription(entry.Path));
        item.SubItems.Add(entry.LastWriteTime.ToString(@"g", CultureInfo.CurrentCulture));
        item.SubItems.Add(entry.IsDirectory ? string.Empty : FormatFileSize(entry.Length));
        _fileList.Items.Add(item);
    }

    private void ApplyEntryFilter(bool suspendUpdates = true)
    {
        if (_fileList.IsDisposed)
        {
            return;
        }

        var searchText = (_searchTextBox.Text ?? string.Empty).Trim();
        var dateFilter = GetSelectedDateModifiedFilter();
        IEnumerable<FileEntry> visibleEntries = _loadedEntries;
        if (!string.IsNullOrWhiteSpace(searchText))
        {
            visibleEntries = visibleEntries.Where(entry =>
                entry.Name.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        if (dateFilter != DateModifiedFilter.AnyTime)
        {
            visibleEntries = visibleEntries.Where(entry => MatchesDateModifiedFilter(entry.LastWriteTime, dateFilter));
        }

        if (suspendUpdates)
        {
            _fileList.BeginUpdate();
        }

        try
        {
            _fileList.Items.Clear();
            foreach (var entry in visibleEntries)
            {
                AddEntry(entry);
            }
        }
        finally
        {
            if (suspendUpdates)
            {
                _fileList.EndUpdate();
            }
        }

        var hasActiveFilter = !string.IsNullOrWhiteSpace(searchText) || dateFilter != DateModifiedFilter.AnyTime;
        ShowStatus(string.Format(
            CultureInfo.CurrentCulture,
            hasActiveFilter ? DialogStrings.MatchingItemsInPath : DialogStrings.ItemsInPath,
            _fileList.Items.Count,
            _currentPath));
    }

    private DateModifiedFilter GetSelectedDateModifiedFilter() =>
        _dateModifiedComboBox?.SelectedItem is DateModifiedFilterEntry entry
            ? entry.Filter
            : DateModifiedFilter.AnyTime;

    private static bool MatchesDateModifiedFilter(DateTime lastWriteTime, DateModifiedFilter filter)
    {
        var modifiedDate = lastWriteTime.Date;
        var today = DateTime.Today;

        switch (filter)
        {
            case DateModifiedFilter.Today:
                return modifiedDate == today;
            case DateModifiedFilter.Yesterday:
                return modifiedDate == today.AddDays(-1);
            case DateModifiedFilter.ThisWeek:
            {
                var weekStart = GetStartOfWeek(today);
                return modifiedDate >= weekStart && modifiedDate <= today;
            }
            case DateModifiedFilter.LastWeek:
            {
                var thisWeekStart = GetStartOfWeek(today);
                var lastWeekStart = thisWeekStart.AddDays(-7);
                return modifiedDate >= lastWeekStart && modifiedDate < thisWeekStart;
            }
            case DateModifiedFilter.ThisMonth:
                return modifiedDate.Year == today.Year
                       && modifiedDate.Month == today.Month
                       && modifiedDate <= today;
            case DateModifiedFilter.LastMonth:
            {
                var lastMonth = today.AddMonths(-1);
                return modifiedDate.Year == lastMonth.Year && modifiedDate.Month == lastMonth.Month;
            }
            case DateModifiedFilter.ThisYear:
                return modifiedDate.Year == today.Year && modifiedDate <= today;
            case DateModifiedFilter.LastYear:
                return modifiedDate.Year == today.Year - 1;
            default:
                return true;
        }
    }

    private static DateTime GetStartOfWeek(DateTime date)
    {
        var firstDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
        var offset = ((int)date.DayOfWeek - (int)firstDayOfWeek + 7) % 7;
        return date.Date.AddDays(-offset);
    }

    private void RecordNavigationHistory(string path)
    {
        if (_navigatingHistory)
        {
            return;
        }

        if (_historyIndex >= 0 && _historyIndex < _navigationHistory.Count
            && string.Equals(_navigationHistory[_historyIndex], path, StringComparison.OrdinalIgnoreCase))
        {
            UpdateNavigationButtons();
            return;
        }

        if (_historyIndex < _navigationHistory.Count - 1)
        {
            _navigationHistory.RemoveRange(_historyIndex + 1, _navigationHistory.Count - _historyIndex - 1);
        }

        _navigationHistory.Add(path);
        _historyIndex = _navigationHistory.Count - 1;
        UpdateNavigationButtons();
    }

    private void UpdateNavigationButtons()
    {
        _backButton.Enabled = _historyIndex > 0;
        _forwardButton.Enabled = _historyIndex >= 0 && _historyIndex < _navigationHistory.Count - 1;
    }

    private void UpdateBreadcrumbs(string path)
    {
        var rootPath = System.IO.Path.GetPathRoot(path);
        if (string.IsNullOrWhiteSpace(rootPath))
        {
            return;
        }

        _updatingBreadcrumbs = true;
        try
        {
            var rootItem = _addressBar.RootItem;
            _addressBar.SelectedItem = rootItem;
            rootItem.Items.Clear();
            // ShortText is the visible step; LongText is also drawn by the crumb button, so keep it empty.
            // Full path for navigation lives in Tag only.
            rootItem.ShortText = rootPath.TrimEnd(System.IO.Path.DirectorySeparatorChar, System.IO.Path.AltDirectorySeparatorChar);
            if (string.IsNullOrEmpty(rootItem.ShortText))
            {
                rootItem.ShortText = rootPath;
            }

            rootItem.LongText = string.Empty;
            rootItem.Tag = rootPath;

            var currentItem = rootItem;
            var currentPath = rootPath;
            var remainder = path.Substring(rootPath.Length)
                .Split(new[] { System.IO.Path.DirectorySeparatorChar, System.IO.Path.AltDirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var segment in remainder)
            {
                currentPath = System.IO.Path.Combine(currentPath, segment);
                var child = new KryptonBreadCrumbItem(segment)
                {
                    LongText = string.Empty,
                    Tag = currentPath
                };
                currentItem.Items.Add(child);
                currentItem = child;
            }

            _addressBar.SelectedItem = currentItem;
            _addressBar.Visible = true;
            _addressEditBox.Visible = false;
            _addressBar.Invalidate(true);
            _addressBar.Update();
        }
        finally
        {
            _updatingBreadcrumbs = false;
        }
    }

    private void OnAddressBarSelectedItemChanged(object? sender, EventArgs e)
    {
        if (_updatingBreadcrumbs)
        {
            return;
        }

        _suppressAddressEdit = true;
        if (_addressBar.SelectedItem?.Tag is string selectedPath
            && !string.IsNullOrWhiteSpace(selectedPath)
            && !string.Equals(selectedPath, _currentPath, StringComparison.OrdinalIgnoreCase))
        {
            NavigateToPath(selectedPath, updatePathText: true, selectTreeNode: true);
        }
    }

    private void OnAddressBarMouseUp(object? sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Right)
        {
            ShowBreadcrumbContextMenu(_addressBar.PointToScreen(e.Location));
            return;
        }

        if (e.Button != MouseButtons.Left)
        {
            return;
        }

        if (_suppressAddressEdit)
        {
            _suppressAddressEdit = false;
            return;
        }

        BeginAddressEdit();
    }

    private void ShowBreadcrumbContextMenu(Point screenLocation)
    {
        if (_breadcrumbContextMenu == null || _deleteHistoryMenuItem == null)
        {
            return;
        }

        _deleteHistoryMenuItem.Enabled = _navigationHistory.Count > 1;
        _breadcrumbContextMenu.Show(_addressBar, screenLocation);
    }

    private void OnCopyAddress(object? sender, EventArgs e) => CopyAddressToClipboard(quoteAddress: true);

    private void OnCopyAddressAsText(object? sender, EventArgs e) => CopyAddressToClipboard(quoteAddress: false);

    private void CopyAddressToClipboard(bool quoteAddress)
    {
        try
        {
            var clipboardText = quoteAddress ? $@"""{_currentPath}""" : _currentPath;
            Clipboard.SetText(clipboardText, TextDataFormat.UnicodeText);
        }
        catch (ExternalException ex)
        {
            ShowStatus(ex.Message);
        }
    }

    private void OnEditAddress(object? sender, EventArgs e) => BeginAddressEdit();

    private void OnDeleteAddressHistory(object? sender, EventArgs e)
    {
        _navigationHistory.Clear();
        _navigationHistory.Add(_currentPath);
        _historyIndex = 0;
        UpdateNavigationButtons();
    }

    private void OnFormKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Apps || (e.KeyCode == Keys.F10 && e.Shift))
        {
            var contextLocation = _addressBar.PointToScreen(new Point(0, _addressBar.Height));
            ShowBreadcrumbContextMenu(contextLocation);
            e.Handled = true;
            e.SuppressKeyPress = true;
            return;
        }

        if (e.KeyCode == Keys.L && e.Control && !e.Alt && !e.Shift)
        {
            BeginAddressEdit();
            e.Handled = true;
            e.SuppressKeyPress = true;
            return;
        }

        if (e.KeyCode == Keys.F4 && !e.Alt && !e.Control && !e.Shift)
        {
            BeginAddressEdit();
            e.Handled = true;
            e.SuppressKeyPress = true;
        }
    }

    private void OnAddressEditBoxKeyDown(object? sender, KeyEventArgs e)
    {
        if (HandleSuggestionKeys(_addressSuggestionPopup, e))
        {
            return;
        }

        if (e.KeyCode == Keys.Enter)
        {
            CommitAddressEdit();
            e.Handled = true;
            e.SuppressKeyPress = true;
            return;
        }

        if (e.KeyCode == Keys.Escape)
        {
            CancelAddressEdit();
            e.Handled = true;
            e.SuppressKeyPress = true;
        }
    }

    private void OnAddressEditBoxLostFocus(object? sender, EventArgs e)
    {
        if (_committingAddressEdit || !_addressEditBox.Visible)
        {
            return;
        }

        // Defer so context-menu / focus transitions can complete first.
        BeginInvoke(new Action(() =>
        {
            if (!_addressEditBox.Focused
                && _addressSuggestionPopup?.ContainsFocus != true
                && _addressEditBox.Visible
                && !_committingAddressEdit)
            {
                CancelAddressEdit();
            }
        }));
    }

    private void OnAddressEditBoxTextChanged(object? sender, EventArgs e)
    {
        _addressSuggestionTimer.Stop();
        _addressSuggestionGeneration++;

        if (!_addressEditBox.Visible || !_addressEditBox.Focused || string.IsNullOrWhiteSpace(_addressEditBox.Text))
        {
            CloseAddressSuggestions();
            return;
        }

        _addressSuggestionTimer.Start();
    }

    private void OnAddressSuggestionTimerTick(object? sender, EventArgs e)
    {
        _addressSuggestionTimer.Stop();
        var typedPath = (_addressEditBox.Text ?? string.Empty).Trim().Trim('"');
        var generation = _addressSuggestionGeneration;

        Task.Run(() => BuildAddressSuggestions(typedPath))
            .ContinueWith(task =>
            {
                if (IsDisposed
                    || Disposing
                    || generation != _addressSuggestionGeneration
                    || !_addressEditBox.Visible
                    || !_addressEditBox.Focused)
                {
                    return;
                }

                var suggestions = task.Status == TaskStatus.RanToCompletion
                    ? task.Result
                    : Array.Empty<string>();
                GetAddressSuggestionPopup().ShowSuggestions(_addressEditBox, suggestions);
            }, TaskScheduler.FromCurrentSynchronizationContext());
    }

    private string[] BuildAddressSuggestions(string typedPath)
    {
        try
        {
            var expandedPath = Environment.ExpandEnvironmentVariables(typedPath);
            if (!System.IO.Path.IsPathRooted(expandedPath))
            {
                expandedPath = System.IO.Path.Combine(_currentPath, expandedPath);
            }

            var endsWithSeparator = expandedPath.EndsWith(@"\", StringComparison.Ordinal)
                                    || expandedPath.EndsWith(@"/", StringComparison.Ordinal);
            var parentPath = endsWithSeparator
                ? expandedPath
                : System.IO.Path.GetDirectoryName(expandedPath);
            var namePrefix = endsWithSeparator ? string.Empty : System.IO.Path.GetFileName(expandedPath);

            if (string.IsNullOrWhiteSpace(parentPath) || !Directory.Exists(parentPath))
            {
                return Array.Empty<string>();
            }

            return new DirectoryInfo(parentPath).GetDirectories()
                .Where(directory => directory.Name.StartsWith(namePrefix, StringComparison.CurrentCultureIgnoreCase))
                .OrderBy(directory => directory.Name, StringComparer.CurrentCultureIgnoreCase)
                .Take(MAXIMUM_AUTOCOMPLETE_SUGGESTIONS)
                .Select(directory => directory.FullName + System.IO.Path.DirectorySeparatorChar)
                .ToArray();
        }
        catch (Exception)
        {
            return Array.Empty<string>();
        }
    }

    private static bool HandleSuggestionKeys(VisualCustomFileDialogSuggestionPopup? popup, KeyEventArgs e)
    {
        if (popup?.IsPopupVisible != true)
        {
            return false;
        }

        switch (e.KeyCode)
        {
            case Keys.Down:
                popup.SelectNext();
                break;
            case Keys.Up:
                popup.SelectPrevious();
                break;
            case Keys.Enter:
                popup.AcceptSelected();
                break;
            case Keys.Escape:
                popup.ClosePopup();
                break;
            default:
                return false;
        }

        e.Handled = true;
        e.SuppressKeyPress = true;
        return true;
    }

    private void ApplyAddressSuggestion(string suggestion)
    {
        CloseAddressSuggestions();
        _addressEditBox.Text = suggestion;
        _addressEditBox.SelectionStart = suggestion.Length;
        _addressEditBox.SelectionLength = 0;
        _addressEditBox.Focus();
    }

    private void UpdateSearchSuggestions()
    {
        if (!_searchTextBox.Focused)
        {
            CloseSearchSuggestions();
            return;
        }

        var searchText = (_searchTextBox.Text ?? string.Empty).Trim();
        if (searchText.Length == 0)
        {
            CloseSearchSuggestions();
            return;
        }

        List<string> history;
        lock (_searchHistorySync)
        {
            history = _searchHistory.ToList();
        }

        var dateFilter = GetSelectedDateModifiedFilter();
        var suggestions = history
            .Concat(_loadedEntries
                .Where(entry => dateFilter == DateModifiedFilter.AnyTime
                                || MatchesDateModifiedFilter(entry.LastWriteTime, dateFilter))
                .Select(entry => entry.Name))
            .Where(value => value.IndexOf(searchText, StringComparison.CurrentCultureIgnoreCase) >= 0)
            .Distinct(StringComparer.CurrentCultureIgnoreCase)
            .Take(MAXIMUM_AUTOCOMPLETE_SUGGESTIONS)
            .ToArray();
        GetSearchSuggestionPopup().ShowSuggestions(_searchTextBox, suggestions);
    }

    private void ApplySearchSuggestion(string suggestion)
    {
        CloseSearchSuggestions();
        _searchTextBox.Text = suggestion;
        _searchTextBox.SelectionStart = suggestion.Length;
        _searchTextBox.SelectionLength = 0;
        _searchTextBox.Focus();
        RememberSearchTerm(suggestion);
    }

    private static void RememberSearchTerm(string? searchTerm)
    {
        var trimmedTerm = (searchTerm ?? string.Empty).Trim();
        if (trimmedTerm.Length == 0)
        {
            return;
        }

        lock (_searchHistorySync)
        {
            _searchHistory.RemoveAll(item => string.Equals(item, trimmedTerm, StringComparison.CurrentCultureIgnoreCase));
            _searchHistory.Insert(0, trimmedTerm);
            if (_searchHistory.Count > MAXIMUM_SEARCH_HISTORY_ITEMS)
            {
                _searchHistory.RemoveRange(MAXIMUM_SEARCH_HISTORY_ITEMS, _searchHistory.Count - MAXIMUM_SEARCH_HISTORY_ITEMS);
            }
        }
    }

    private VisualCustomFileDialogSuggestionPopup GetAddressSuggestionPopup()
    {
        if (_addressSuggestionPopup == null || _addressSuggestionPopup.IsDisposed)
        {
            _addressSuggestionPopup = new VisualCustomFileDialogSuggestionPopup(ApplyAddressSuggestion);
        }

        return _addressSuggestionPopup;
    }

    private VisualCustomFileDialogSuggestionPopup GetSearchSuggestionPopup()
    {
        if (_searchSuggestionPopup == null || _searchSuggestionPopup.IsDisposed)
        {
            _searchSuggestionPopup = new VisualCustomFileDialogSuggestionPopup(ApplySearchSuggestion);
        }

        return _searchSuggestionPopup;
    }

    private void CloseAddressSuggestions()
    {
        if (_addressSuggestionPopup?.IsDisposed == false)
        {
            _addressSuggestionPopup.ClosePopup();
        }

        _addressSuggestionPopup = null;
    }

    private void CloseSearchSuggestions()
    {
        if (_searchSuggestionPopup?.IsDisposed == false)
        {
            _searchSuggestionPopup.ClosePopup();
        }

        _searchSuggestionPopup = null;
    }

    private void OnDialogFormClosed(object? sender, FormClosedEventArgs e)
    {
        _addressSuggestionTimer.Stop();
        _addressSuggestionTimer.Dispose();
        _addressSuggestionPopup?.Dispose();
        _searchSuggestionPopup?.Dispose();
        _breadcrumbContextMenu?.Dispose();
        DisposeShellIcons();
    }

    private void BeginAddressEdit()
    {
        if (_addressEditBox.Visible)
        {
            _addressEditBox.Focus();
            _addressEditBox.SelectAll();
            return;
        }

        _addressEditBox.Text = _currentPath;
        _addressBar.Visible = false;
        _addressEditBox.Visible = true;
        _addressEditBox.BringToFront();
        _addressEditBox.Focus();
        _addressEditBox.SelectAll();
    }

    private void CommitAddressEdit()
    {
        if (!_addressEditBox.Visible)
        {
            return;
        }

        _committingAddressEdit = true;
        try
        {
            var typedPath = (_addressEditBox.Text ?? string.Empty).Trim().Trim('"');
            EndAddressEdit();
            if (!string.IsNullOrWhiteSpace(typedPath))
            {
                NavigateToPath(typedPath, updatePathText: true, selectTreeNode: true);
            }
        }
        finally
        {
            _committingAddressEdit = false;
        }
    }

    private void CancelAddressEdit()
    {
        if (!_addressEditBox.Visible)
        {
            return;
        }

        _addressEditBox.Text = _currentPath;
        EndAddressEdit();
    }

    private void EndAddressEdit()
    {
        _addressSuggestionTimer.Stop();
        CloseAddressSuggestions();
        _addressEditBox.Visible = false;
        _addressBar.Visible = true;
        _addressBar.Focus();
    }

    private string[] GetSelectedFilterPatterns()
    {
        if (_options.Kind == KryptonDialogKind.SelectFolder)
        {
            return Array.Empty<string>();
        }

        var filter = _filterComboBox.SelectedItem as FilterEntry ?? _filters.FirstOrDefault();
        return filter?.Patterns ?? Array.Empty<string>();
    }

    private static bool MatchesSelectedFilter(FileInfo fileInfo, string[] patterns)
    {
        if (patterns.Length == 0)
        {
            return true;
        }

        foreach (var pattern in patterns)
        {
            if (pattern == @"*.*" || pattern == @"*")
            {
                return true;
            }

            var regexPattern = @"^" + Regex.Escape(pattern)
                .Replace(@"\*", @".*")
                .Replace(@"\?", @".") + @"$";
            if (Regex.IsMatch(fileInfo.Name, regexPattern, RegexOptions.IgnoreCase))
            {
                return true;
            }
        }

        return false;
    }

    private static string GetTypeDescription(string path)
    {
        var extension = System.IO.Path.GetExtension(path);
        return string.IsNullOrWhiteSpace(extension)
            ? DialogStrings.GenericFileType
            : string.Format(CultureInfo.CurrentCulture, DialogStrings.FileType, extension.ToUpperInvariant());
    }

    private static string FormatFileSize(long length)
    {
        var size = (double)length;
        string[] units = { @"B", @"KB", @"MB", @"GB", @"TB" };
        var unitIndex = 0;
        while (size >= 1024 && unitIndex < units.Length - 1)
        {
            size /= 1024;
            unitIndex++;
        }

        return string.Format(CultureInfo.CurrentCulture, @"{0:0.##} {1}", size, units[unitIndex]);
    }

    private void SelectNavigationNode(string path)
    {
        // Locating a deep path can populate several lazily loaded levels, so keep the whole
        // search and selection inside one update block instead of repainting per level.
        _navigationTree.BeginUpdate();
        try
        {
            var match = FindNodeForPath(path);

            if (match == null || ReferenceEquals(_navigationTree.SelectedNode, match))
            {
                return;
            }

            _updatingNavigationSelection = true;
            try
            {
                _navigationTree.SelectedNode = match;
                match.EnsureVisible();
            }
            finally
            {
                _updatingNavigationSelection = false;
            }
        }
        finally
        {
            _navigationTree.EndUpdate();
        }
    }

    /// <summary>
    /// Locates the node for a path, loading the lazily populated levels between the closest known ancestor and the target.
    /// </summary>
    private TreeNode? FindNodeForPath(string path)
    {
        foreach (TreeNode rootNode in _navigationTree.Nodes)
        {
            var match = FindNodeByPath(rootNode, path);
            if (match != null)
            {
                return match;
            }
        }

        var current = FindClosestAncestorNode(path);
        while (current?.Tag is NavigationTarget currentTarget && !PathsEqual(currentTarget.Path, path))
        {
            PopulateChildNodes(current);

            TreeNode? next = null;
            foreach (TreeNode child in current.Nodes)
            {
                if (child.Tag is NavigationTarget childTarget && IsSameOrDescendantPath(path, childTarget.Path))
                {
                    next = child;
                    break;
                }
            }

            if (next == null)
            {
                return null;
            }

            current = next;
        }

        return current;
    }

    private TreeNode? FindClosestAncestorNode(string path)
    {
        TreeNode? closest = null;
        var closestLength = -1;
        foreach (TreeNode rootNode in _navigationTree.Nodes)
        {
            FindClosestAncestorNode(rootNode, path, ref closest, ref closestLength);
        }

        return closest;
    }

    private static void FindClosestAncestorNode(TreeNode node, string path, ref TreeNode? closest, ref int closestLength)
    {
        if (node.Tag is NavigationTarget navigationTarget
            && IsSameOrDescendantPath(path, navigationTarget.Path)
            && navigationTarget.Path.Length > closestLength)
        {
            closest = node;
            closestLength = navigationTarget.Path.Length;
        }

        foreach (TreeNode child in node.Nodes)
        {
            FindClosestAncestorNode(child, path, ref closest, ref closestLength);
        }
    }

    private static TreeNode? FindNodeByPath(TreeNode node, string path)
    {
        if (node.Tag is NavigationTarget navigationTarget
            && PathsEqual(navigationTarget.Path, path))
        {
            return node;
        }

        foreach (TreeNode child in node.Nodes)
        {
            var match = FindNodeByPath(child, path);
            if (match != null)
            {
                return match;
            }
        }

        return null;
    }

    private static bool PathsEqual(string left, string right) =>
        string.Equals(left.TrimEnd('\\'), right.TrimEnd('\\'), StringComparison.OrdinalIgnoreCase);

    /// <summary>Determines whether <paramref name="candidate"/> is <paramref name="path"/> itself or one of its parents.</summary>
    private static bool IsSameOrDescendantPath(string path, string candidate)
    {
        var trimmedPath = path.TrimEnd('\\');
        var trimmedCandidate = candidate.TrimEnd('\\');

        return trimmedCandidate.Length > 0
               && (trimmedPath.Equals(trimmedCandidate, StringComparison.OrdinalIgnoreCase)
                   || trimmedPath.StartsWith(trimmedCandidate + @"\", StringComparison.OrdinalIgnoreCase));
    }

    private void OnNavigationBeforeExpand(object? sender, TreeViewCancelEventArgs e)
    {
        if (e.Node != null)
        {
            PopulateChildNodes(e.Node);
        }
    }

    private void OnNavigationAfterSelect(object? sender, TreeViewEventArgs e)
    {
        if (_updatingNavigationSelection)
        {
            return;
        }

        if (e.Node?.Tag is NavigationTarget navigationTarget)
        {
            NavigateToPath(navigationTarget.Path, updatePathText: true, selectTreeNode: false);
        }
    }

    private void OnFileListSelectedIndexChanged(object? sender, EventArgs e)
    {
        if (_fileList.SelectedItems.Count == 0)
        {
            if (_options.Kind == KryptonDialogKind.SelectFolder)
            {
                _fileNameTextBox.Text = _currentPath;
            }

            return;
        }

        var selectedEntry = _fileList.SelectedItems[0].Tag as FileEntry;
        if (selectedEntry == null)
        {
            return;
        }

        if (_options.Kind == KryptonDialogKind.SelectFolder)
        {
            _fileNameTextBox.Text = selectedEntry.IsDirectory ? selectedEntry.Path : _currentPath;
            return;
        }

        _fileNameTextBox.Text = selectedEntry.IsDirectory ? selectedEntry.Name : System.IO.Path.GetFileName(selectedEntry.Path);
    }

    private void OnFileListItemActivate(object? sender, EventArgs e)
    {
        if (_fileList.SelectedItems.Count == 0)
        {
            return;
        }

        var selectedEntry = _fileList.SelectedItems[0].Tag as FileEntry;
        if (selectedEntry == null)
        {
            return;
        }

        if (selectedEntry.IsDirectory)
        {
            NavigateToPath(selectedEntry.Path, updatePathText: true, selectTreeNode: true);
            return;
        }

        if (_options.Kind != KryptonDialogKind.SelectFolder)
        {
            TryAcceptSelection();
        }
    }

    private void OnFileNameTextBoxKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            TryAcceptSelection();
            e.Handled = true;
        }
    }

    private void TryAcceptSelection()
    {
        switch (_options.Kind)
        {
            case KryptonDialogKind.SelectFolder:
                TryAcceptFolderSelection();
                break;
            case KryptonDialogKind.SaveFile:
                TryAcceptSaveSelection();
                break;
            default:
                TryAcceptOpenSelection();
                break;
        }
    }

    private void TryAcceptFolderSelection()
    {
        var selectedPath = _fileList.SelectedItems.Count > 0
                           && _fileList.SelectedItems[0].Tag is FileEntry entry
                           && entry.IsDirectory
            ? entry.Path
            : _fileNameTextBox.Text;

        if (string.IsNullOrWhiteSpace(selectedPath))
        {
            selectedPath = _currentPath;
        }

        if (!Directory.Exists(selectedPath))
        {
            ShowValidationMessage(string.Format(CultureInfo.CurrentCulture, DialogStrings.InvalidFolder, selectedPath));
            return;
        }

        _providerResult = new KryptonDialogResult
        {
            DialogResult = DialogResult.OK,
            SelectedPath = selectedPath,
            FileName = selectedPath,
            FileNames = new[] { selectedPath }
        };
        DialogResult = DialogResult.OK;
        Close();
    }

    private void TryAcceptOpenSelection()
    {
        var selectedEntry = _fileList.SelectedItems.Count > 0 ? _fileList.SelectedItems[0].Tag as FileEntry : null;
        if (selectedEntry != null && selectedEntry.IsDirectory)
        {
            NavigateToPath(selectedEntry.Path, updatePathText: true, selectTreeNode: true);
            return;
        }

        var rawFileName = string.IsNullOrWhiteSpace(_fileNameTextBox.Text)
            ? selectedEntry?.Name ?? string.Empty
            : _fileNameTextBox.Text.Trim();
        if (!TryBuildTargetPath(rawFileName, out var targetPath))
        {
            return;
        }

        if (_options.CheckFileExists && !File.Exists(targetPath))
        {
            ShowValidationMessage(string.Format(CultureInfo.CurrentCulture, DialogStrings.PathDoesNotExist, targetPath));
            return;
        }

        if (!RaiseFileOk())
        {
            return;
        }

        _providerResult = new KryptonDialogResult
        {
            DialogResult = DialogResult.OK,
            SelectedPath = System.IO.Path.GetDirectoryName(targetPath) ?? string.Empty,
            FileName = targetPath,
            FileNames = new[] { targetPath },
            ReadOnlyChecked = _options.ReadOnlyChecked
        };
        DialogResult = DialogResult.OK;
        Close();
    }

    private void TryAcceptSaveSelection()
    {
        var rawFileName = _fileNameTextBox.Text.Trim();
        if (!TryBuildTargetPath(rawFileName, out var targetPath))
        {
            return;
        }

        targetPath = ApplyDefaultExtension(targetPath);
        var targetDirectory = System.IO.Path.GetDirectoryName(targetPath) ?? string.Empty;
        if (_options.CheckPathExists && !Directory.Exists(targetDirectory))
        {
            ShowValidationMessage(string.Format(CultureInfo.CurrentCulture, DialogStrings.PathDoesNotExist, targetDirectory));
            return;
        }

        if (File.Exists(targetPath) && _options.OverwritePrompt)
        {
            var overwrite = KryptonMessageBox.Show(
                owner: (IWin32Window)this,
                text: string.Format(CultureInfo.CurrentCulture, DialogStrings.ConfirmSaveAsText, targetPath),
                caption: DialogStrings.ConfirmSaveAsCaption,
                buttons: KryptonMessageBoxButtons.YesNo,
                icon: KryptonMessageBoxIcon.Warning);
            if (overwrite != DialogResult.Yes)
            {
                return;
            }
        }

        if (_options.CreatePrompt && !File.Exists(targetPath))
        {
            var create = KryptonMessageBox.Show(
                owner: (IWin32Window)this,
                text: string.Format(CultureInfo.CurrentCulture, DialogStrings.ConfirmCreateText, targetPath),
                caption: DialogStrings.ConfirmCreateCaption,
                buttons: KryptonMessageBoxButtons.YesNo,
                icon: KryptonMessageBoxIcon.Question);
            if (create != DialogResult.Yes)
            {
                return;
            }
        }

        if (!RaiseFileOk())
        {
            return;
        }

        _providerResult = new KryptonDialogResult
        {
            DialogResult = DialogResult.OK,
            SelectedPath = targetDirectory,
            FileName = targetPath,
            FileNames = new[] { targetPath }
        };
        DialogResult = DialogResult.OK;
        Close();
    }

    private bool TryBuildTargetPath(string rawFileName, out string targetPath)
    {
        targetPath = string.Empty;
        if (string.IsNullOrWhiteSpace(rawFileName))
        {
            ShowValidationMessage(DialogStrings.EnterFileName);
            return false;
        }

        if (_options.ValidateNames)
        {
            foreach (var invalidChar in System.IO.Path.GetInvalidFileNameChars())
            {
                if (rawFileName.IndexOf(invalidChar) >= 0)
                {
                    ShowValidationMessage(DialogStrings.InvalidFileNameCharacters);
                    return false;
                }
            }
        }

        targetPath = System.IO.Path.IsPathRooted(rawFileName)
            ? rawFileName
            : System.IO.Path.Combine(_currentPath, rawFileName);
        return true;
    }

    private string ApplyDefaultExtension(string targetPath)
    {
        if (!_options.AddExtension || !string.IsNullOrWhiteSpace(System.IO.Path.GetExtension(targetPath)))
        {
            return targetPath;
        }

        var extension = ResolvePreferredExtension();
        if (string.IsNullOrWhiteSpace(extension))
        {
            extension = _options.DefaultExt;
        }

        if (string.IsNullOrWhiteSpace(extension))
        {
            return targetPath;
        }

        extension = extension.Trim();
        if (!extension.StartsWith(".", StringComparison.Ordinal))
        {
            extension = "." + extension.TrimStart('*');
        }

        return targetPath + extension;
    }

    private string ResolvePreferredExtension()
    {
        var filter = _filterComboBox.SelectedItem as FilterEntry;
        if (filter == null)
        {
            return string.Empty;
        }

        foreach (var pattern in filter.Patterns)
        {
            if (pattern.StartsWith("*.", StringComparison.Ordinal))
            {
                return pattern.Substring(1);
            }
        }

        return string.Empty;
    }

    private bool RaiseFileOk()
    {
        if (_context.Wrapper is FileDialogWrapper fileDialogWrapper)
        {
            return fileDialogWrapper.RaiseFileOk();
        }

        return true;
    }

    private void ShowValidationMessage(string message)
    {
        ShowStatus(message);
        KryptonMessageBox.Show(
            owner: (IWin32Window)this,
            text: message,
            caption: DialogStrings.ValidationCaption,
            buttons: KryptonMessageBoxButtons.OK,
            icon: KryptonMessageBoxIcon.Warning);
    }

    private void ShowStatus(string message)
    {
        _statusLabel.Values.Text = message;
    }
}

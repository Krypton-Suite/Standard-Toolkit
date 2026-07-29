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

    private readonly KryptonDialogProviderContext _context;
    private readonly KryptonDialogOptions _options;
    private readonly List<FilterEntry> _filters;
    private readonly List<string> _navigationHistory;
    private readonly Dictionary<string, int> _shellIconCache;
    private KryptonDialogResult _providerResult;
    private string _currentPath;
    private List<FileEntry> _loadedEntries;
    private ImageList? _shellSmallImageList;
    private ImageList? _shellLargeImageList;
    private KryptonContextMenuItems? _viewMenuItems;
    private int _loadGeneration;
    private bool _initialLoadQueued;
    private int _historyIndex;
    private bool _navigatingHistory;
    private bool _updatingBreadcrumbs;

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
        InitializeShellIcons();
        InitializeViewModes();
        ApplyNavigationButtonGlyphs();
        ApplyDialogOptions();
        Shown += OnDialogShown;
    }

    private void InitializeShellIcons()
    {
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

        _navigationTree.ImageList = _shellSmallImageList;
        _fileList.SmallImageList = _shellSmallImageList;
        _fileList.LargeImageList = _shellLargeImageList;
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

    private static ViewModeEntry[] GetViewModes() =>
    [
        new ViewModeEntry(@"Details", View.Details),
        new ViewModeEntry(@"Large icons", View.LargeIcon),
        new ViewModeEntry(@"Small icons", View.SmallIcon),
        new ViewModeEntry(@"List", View.List),
        new ViewModeEntry(@"Tiles", View.Tile)
    ];

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
            _fileList.TileSize = new Size(216, 48);
        }

        var modes = GetViewModes();
        var selectedMode = modes.FirstOrDefault(mode => mode.View == view) ?? modes[0];
        _viewButton.Values.Text = selectedMode.DisplayName;
        _viewButton.ToolTipValues.Heading = $@"View: {selectedMode.DisplayName}";
        _viewButton.AccessibleName = $@"View: {selectedMode.DisplayName}";

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
        ConfigureGlyphButton(
            _backButton,
            palette.GetButtonSpecImage(PaletteButtonSpecStyle.ArrowLeft, PaletteState.Normal),
            palette.GetButtonSpecImageTransparentColor(PaletteButtonSpecStyle.ArrowLeft),
            @"Back");
        ConfigureGlyphButton(
            _forwardButton,
            palette.GetButtonSpecImage(PaletteButtonSpecStyle.ArrowRight, PaletteState.Normal),
            palette.GetButtonSpecImageTransparentColor(PaletteButtonSpecStyle.ArrowRight),
            @"Forward");
        ConfigureGlyphButton(
            _upButton,
            palette.GetButtonSpecImage(PaletteButtonSpecStyle.ArrowUp, PaletteState.Normal),
            palette.GetButtonSpecImageTransparentColor(PaletteButtonSpecStyle.ArrowUp),
            @"Up");
    }

    private static void ConfigureGlyphButton(KryptonButton button, Image? image, Color imageTransparentColor, string toolTip)
    {
        button.Values.Text = string.Empty;
        button.Values.Image = image;
        button.Values.ImageTransparentColor = imageTransparentColor;
        button.AutoSize = true;
        button.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        button.MinimumSize = new Size(28, 28);
        button.ToolTipValues.EnableToolTips = true;
        button.ToolTipValues.Heading = toolTip;
        button.AccessibleName = toolTip;
    }

    private void ApplyDialogOptions()
    {
        Text = string.IsNullOrWhiteSpace(_options.Title) ? GetDefaultCaption() : _options.Title;
        Icon = _options.Icon;
        _pathTextBox.Text = _currentPath;
        _fileNameTextBox.Text = ResolveInitialFileName();
        _fileNameLabel.Values.Text = _options.Kind == KryptonDialogKind.SelectFolder ? @"Folder:" : @"File name:";
        _acceptButton.Values.Text = GetAcceptCaption();

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

        ShowStatus(@"Loading...");
    }

    private void OnBackButtonClick(object? sender, EventArgs e) => NavigateHistory(-1);

    private void OnForwardButtonClick(object? sender, EventArgs e) => NavigateHistory(1);

    private void OnUpButtonClick(object? sender, EventArgs e) => NavigateUp();

    private void OnRefreshButtonClick(object? sender, EventArgs e) => RefreshListing();

    private void OnSearchTextChanged(object? sender, EventArgs e) => ApplyEntryFilter();

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

    private string GetDefaultCaption() => _options.Kind switch
    {
        KryptonDialogKind.SaveFile => @"Save",
        KryptonDialogKind.SelectFolder => @"Select Folder",
        _ => @"Open"
    };

    private string GetAcceptCaption() => _options.Kind switch
    {
        KryptonDialogKind.SaveFile => @"Save",
        KryptonDialogKind.SelectFolder => @"Select Folder",
        _ => @"Open"
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
            filters.Add(new FilterEntry(@"All files (*.*)", @"*.*"));
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
            filters.Add(new FilterEntry(@"All files (*.*)", @"*.*"));
        }

        return filters;
    }

    private void BuildNavigationTree()
    {
        _navigationTree.BeginUpdate();
        _navigationTree.Nodes.Clear();

        var placesIcon = GetShellIconIndex(null, ShellIconKind.PlacesRoot);
        var placesNode = new TreeNode(@"Common Places", placesIcon, placesIcon);
        AddPlaceNode(placesNode, @"Desktop", Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
        AddPlaceNode(placesNode, @"Documents", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
        AddPlaceNode(placesNode, @"Pictures", Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
        AddPlaceNode(placesNode, @"Music", Environment.GetFolderPath(Environment.SpecialFolder.MyMusic));
        AddPlaceNode(placesNode, @"Downloads", System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), @"Downloads"));
        AddPlaceNode(placesNode, @"Root Folder", Environment.GetFolderPath(_options.RootFolder));

        if (_options.CustomPlaces.Count > 0)
        {
            var customPlacesIcon = GetShellIconIndex(null, ShellIconKind.Folder);
            var customPlacesNode = new TreeNode(@"Custom Places", customPlacesIcon, customPlacesIcon);
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
        var drivesNode = new TreeNode(@"Drives", drivesIcon, drivesIcon);
        foreach (var drive in DriveInfo.GetDrives().Where(static drive => drive.IsReady))
        {
            AddPlaceNode(drivesNode, drive.Name, drive.RootDirectory.FullName, isDrive: true);
        }

        drivesNode.Expand();
        _navigationTree.Nodes.Add(drivesNode);
        _navigationTree.EndUpdate();
    }

    private void AddPlaceNode(TreeNode parent, string text, string path, bool isDrive = false)
    {
        if (string.IsNullOrWhiteSpace(path) || !Directory.Exists(path))
        {
            return;
        }

        var iconIndex = GetShellIconIndex(path, isDrive ? ShellIconKind.Drive : ShellIconKind.Place);
        parent.Nodes.Add(new TreeNode(text, iconIndex, iconIndex)
        {
            Tag = new NavigationTarget(text, path)
        });
    }

    private int GetShellIconIndex(string? path, ShellIconKind kind)
    {
        if (_shellSmallImageList == null || _shellLargeImageList == null)
        {
            return 0;
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
            return 0;
        }

        var index = AddShellIcon(smallIcon ?? largeIcon!, largeIcon ?? smallIcon!);
        if (index >= 0)
        {
            _shellIconCache[cacheKey] = index;
            return index;
        }

        return 0;
    }

    private static Icon? ResolveShellIcon(string? path, ShellIconKind kind, bool largeIcon)
    {
        try
        {
            switch (kind)
            {
                case ShellIconKind.Drive:
                case ShellIconKind.Place:
                    return string.IsNullOrWhiteSpace(path)
                        ? FileSystemIconHelper.GetFolderIcon(largeIcon)
                        : FileSystemIconHelper.GetFileSystemIcon(path, largeIcon);
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
                NavigateToPath(parent.FullName, updatePathText: true, selectTreeNode: false);
            }
        }
        catch (Exception ex)
        {
            ShowStatus(ex.Message);
        }
    }

    private void RefreshListing() => NavigateToPath(_currentPath, updatePathText: false, selectTreeNode: false);

    private void NavigateToPath(string path, bool updatePathText, bool selectTreeNode, bool recordHistory = true)
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
                ShowStatus($@"'{normalizedPath}' does not exist.");
                return;
            }

            _currentPath = normalizedPath;
            if (updatePathText)
            {
                _pathTextBox.Text = normalizedPath;
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
        _fileList.Enabled = false;
        ShowStatus($@"Loading {path}...");

        Task.Run(() => LoadEntriesCore(path, patterns))
            .ContinueWith(task =>
            {
                if (IsDisposed || Disposing || generation != _loadGeneration)
                {
                    return;
                }

                _fileList.BeginUpdate();
                _fileList.Items.Clear();

                try
                {
                    var result = task.Status == TaskStatus.RanToCompletion
                        ? task.Result
                        : new DirectoryLoadResult { ErrorMessage = task.Exception?.GetBaseException().Message ?? @"Unable to load directory." };

                    if (!string.IsNullOrWhiteSpace(result.ErrorMessage))
                    {
                        ShowStatus(result.ErrorMessage ?? @"Unable to load directory.");
                        return;
                    }

                    _loadedEntries = result.Entries;
                    ApplyEntryFilter();
                }
                finally
                {
                    _fileList.EndUpdate();
                    _fileList.Enabled = true;
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
        item.SubItems.Add(entry.IsDirectory ? @"Folder" : GetTypeDescription(entry.Path));
        item.SubItems.Add(entry.LastWriteTime.ToString(@"g", CultureInfo.CurrentCulture));
        item.SubItems.Add(entry.IsDirectory ? string.Empty : FormatFileSize(entry.Length));
        _fileList.Items.Add(item);
    }

    private void ApplyEntryFilter()
    {
        if (_fileList.IsDisposed)
        {
            return;
        }

        var searchText = (_searchTextBox.Text ?? string.Empty).Trim();
        IEnumerable<FileEntry> visibleEntries = _loadedEntries;
        if (!string.IsNullOrWhiteSpace(searchText))
        {
            visibleEntries = visibleEntries.Where(entry =>
                entry.Name.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        _fileList.BeginUpdate();
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
            _fileList.EndUpdate();
        }

        ShowStatus(string.IsNullOrWhiteSpace(searchText)
            ? $@"{_fileList.Items.Count} item(s) in {_currentPath}"
            : $@"{_fileList.Items.Count} matching item(s) in {_currentPath}");
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
            rootItem.ShortText = rootPath;
            rootItem.LongText = rootPath;
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
                    LongText = currentPath,
                    Tag = currentPath
                };
                currentItem.Items.Add(child);
                currentItem = child;
            }

            _addressBar.SelectedItem = currentItem;
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

        if (_addressBar.SelectedItem?.Tag is string selectedPath
            && !string.IsNullOrWhiteSpace(selectedPath)
            && !string.Equals(selectedPath, _currentPath, StringComparison.OrdinalIgnoreCase))
        {
            NavigateToPath(selectedPath, updatePathText: true, selectTreeNode: true);
        }
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
        return string.IsNullOrWhiteSpace(extension) ? @"File" : extension.ToUpperInvariant() + @" File";
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
        foreach (TreeNode rootNode in _navigationTree.Nodes)
        {
            var match = FindNodeByPath(rootNode, path);
            if (match != null)
            {
                _navigationTree.SelectedNode = match;
                return;
            }
        }
    }

    private static TreeNode? FindNodeByPath(TreeNode node, string path)
    {
        if (node.Tag is NavigationTarget navigationTarget
            && string.Equals(navigationTarget.Path.TrimEnd('\\'), path.TrimEnd('\\'), StringComparison.OrdinalIgnoreCase))
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

    private void OnNavigationAfterSelect(object? sender, TreeViewEventArgs e)
    {
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
            NavigateToPath(selectedEntry.Path, updatePathText: true, selectTreeNode: false);
            return;
        }

        if (_options.Kind != KryptonDialogKind.SelectFolder)
        {
            TryAcceptSelection();
        }
    }

    private void OnPathTextBoxKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            NavigateToPath(_pathTextBox.Text, updatePathText: true, selectTreeNode: true);
            e.Handled = true;
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
            ShowValidationMessage($@"'{selectedPath}' is not a valid folder.");
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
            NavigateToPath(selectedEntry.Path, updatePathText: true, selectTreeNode: false);
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
            ShowValidationMessage($@"'{targetPath}' does not exist.");
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
            ShowValidationMessage($@"'{targetDirectory}' does not exist.");
            return;
        }

        if (File.Exists(targetPath) && _options.OverwritePrompt)
        {
            var overwrite = KryptonMessageBox.Show(
                owner: (IWin32Window)this,
                text: $@"'{targetPath}' already exists. Do you want to replace it?",
                caption: @"Confirm Save As",
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
                text: $@"Create '{targetPath}'?",
                caption: @"Confirm Create",
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
            ShowValidationMessage(@"Enter a file name.");
            return false;
        }

        if (_options.ValidateNames)
        {
            foreach (var invalidChar in System.IO.Path.GetInvalidFileNameChars())
            {
                if (rawFileName.IndexOf(invalidChar) >= 0)
                {
                    ShowValidationMessage(@"The file name contains invalid characters.");
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
            caption: Text,
            buttons: KryptonMessageBoxButtons.OK,
            icon: KryptonMessageBoxIcon.Warning);
    }

    private void ShowStatus(string message)
    {
        _statusLabel.Values.Text = message;
    }
}

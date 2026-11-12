#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

[ToolboxItem(true)]
[DesignerCategory("Code")]
[DefaultEvent(nameof(FilesDropped))]
public class KryptonDropZoneAlternate : KryptonPanel
{
    private const string DefaultExtensions = ".txt, .png, .jpg, .jpeg, .gif, .bmp, .xls, .xlsx, .csv, .pdf, .doc, .docx";
    private const int GlyphQueued = 0, GlyphUploading = 1, GlyphCompleted = 2, GlyphFailed = 3,
                      GlyphCancelled = 4, GlyphModified = 5, GlyphMissing = 6;

    #region Fields

    private readonly FileDropCollection _collection = new FileDropCollection();
    private readonly List<IDropDataSource> _dataSources = new List<IDropDataSource> { new VirtualFileSource(), new ClipboardImageSource() };
    private readonly FileStalenessMonitor _monitor;
    private readonly ThumbnailProvider _thumbs;
    private readonly ImageList _glyphImages = new ImageList { ColorDepth = ColorDepth.Depth32Bit };
    private readonly HashSet<FileEntry> _dirty = new HashSet<FileEntry>();
    private readonly System.Windows.Forms.Timer _flushTimer = new System.Windows.Forms.Timer { Interval = 100 };
    private readonly SemaphoreSlim _opLock = new SemaphoreSlim(1, 1);
    private readonly ToolTip _toolTip = new ToolTip();

    private KryptonListView _fileListView;
    private KryptonWrapLabel _dropZoneLabel;
    private KryptonButton _clearButton, _browseButton;
    private KryptonLabel _statusLabel;
    private KryptonSeparator _separator;
    private KryptonPanel _dropzonePanel, _controlsPanel;
    private ProgressBar _progressBar;
    private ContextMenuStrip _contextMenu;
    private ToolStripMenuItem _menuOpen, _menuRemove, _menuOpenFolder, _menuCopy, _menuUndo, _menuClear;
    private ToolStripMenuItem[] _viewItems;

    private DropZoneTheme _theme;
    private FileEntryBindingList _bindingList;
    private CancellationTokenSource _opCts;
    private List<string> _allowed = ExtensionParser.Parse(DefaultExtensions);
    private FileViewMode _viewMode = FileViewMode.Details;
    private FileViewMode _effectiveView = FileViewMode.Details;
    private string _dropZoneText, _statusPrefix;
    private int _busy, _virtualThreshold = 2000, _fileListHeight = 140;
    private bool _virtual, _dragHover;
    private bool _showClearButton = true, _showStatusLabel = true, _showFileListView = true;

    #endregion

    #region Properties

    [Category("Behavior"), DefaultValue(true)]
    public bool ShowFileListView { get => _showFileListView; set { _showFileListView = value; _fileListView.Visible = value; UpdateControlsPanelHeight(); } }

    [Category("Behavior"), DefaultValue(true)]
    public bool ShowClearButton { get => _showClearButton; set { _showClearButton = value; _clearButton.Visible = value; UpdateControlsPanelHeight(); } }

    [Category("Behavior"), DefaultValue(true)]
    public bool ShowBrowseButton { get => _browseButton.Visible; set => _browseButton.Visible = value; }

    [Category("Behavior"), DefaultValue(true)]
    public bool ShowStatusLabel { get => _showStatusLabel; set { _showStatusLabel = value; _statusLabel.Visible = value; UpdateControlsPanelHeight(); } }

    [Category("Appearance"), DefaultValue(140)]
    [Description("Height in pixels of the file list.")]
    public int FileListHeight { get => _fileListHeight; set { _fileListHeight = Math.Max(40, value); UpdateControlsPanelHeight(); } }

    [Category("Appearance"), DefaultValue(FileViewMode.Details)]
    public FileViewMode ViewMode { get => _viewMode; set { if (_viewMode != value) { _viewMode = value; RefreshView(_statusPrefix); } } }

    [Category("Behavior"), DefaultValue(2000)]
    [Description("At or above this many files the list switches to virtual mode (no Tiles view, no drag-reorder).")]
    public int VirtualModeThreshold { get => _virtualThreshold; set { _virtualThreshold = Math.Max(100, value); RefreshView(_statusPrefix); } }

    [Category("Behavior"), DefaultValue(DefaultExtensions)]
    [Description("Allowed extensions, comma-separated. Empty allows everything.")]
    [Editor(typeof(ExtensionListEditor), typeof(UITypeEditor))]
    public string AllowedExtensionsText
    {
        get => string.Join(", ", _allowed);
        set { _allowed.Clear(); _allowed.AddRange(ExtensionParser.Parse(value)); }
    }

    /// <summary>Runtime access to the list. Edits take effect on the next drop. Use AllowedExtensionsText in the designer.</summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public List<string> AllowedExtensions { get => _allowed; set => _allowed = value ?? new List<string>(); }

    [Category("Behavior"), DefaultValue(0)] public int MaxFileCount { get; set; }
    [Category("Behavior"), DefaultValue(0L)] public long MaxFileSize { get; set; }
    [Category("Behavior"), DefaultValue(0L)] public long MaxTotalSize { get; set; }
    [Category("Behavior"), DefaultValue(true)] public bool AllowDirectories { get; set; } = true;
    [Category("Behavior"), DefaultValue(false)] public bool SearchSubdirectories { get; set; }
    [Category("Behavior"), DefaultValue(true)] public bool ValidateFileSignatures { get; set; } = true;
    [Category("Behavior"), DefaultValue(false)]
    [Description("Reject files whose content is identical to one already listed (hashes only same-size files).")]
    public bool DetectDuplicateContent { get; set; }
    [Category("Behavior"), DefaultValue(true)]
    public bool EnableUndo { get => _collection.EnableUndo; set => _collection.EnableUndo = value; }

    [Category("Appearance")]
    public string DropZoneText
    {
        get => _dropZoneText ?? DropZoneStrings.Get("DropHere");
        set { _dropZoneText = value; _dropZoneLabel.Text = DropZoneText; }
    }
    private bool ShouldSerializeDropZoneText() => _dropZoneText != null;
    private void ResetDropZoneText() => DropZoneText = null;

    [Browsable(false)] public FileDropCollection Collection => _collection;
    [Browsable(false)] public IList<IFileValidator> CustomValidators => _collection.Validators;
    [Browsable(false)] public IReadOnlyList<FileEntry> Entries => _collection;
    [Browsable(false)] public IReadOnlyList<FileEntry> RejectedFiles => _collection.Rejected;
    [Browsable(false)] public IReadOnlyList<string> DroppedFiles => _collection.Select(f => f.Path).ToList().AsReadOnly();
    [Browsable(false)] public int FileCount => _collection.Count;
    [Browsable(false)] public bool CanUndo => _collection.CanUndo;
    [Browsable(false)] public bool IsScanning => _busy > 0;
    [Browsable(false)] public string StatusText => _statusLabel.Text;

    #endregion

    #region Events

    public event EventHandler<FilesAddedEventArgs> FilesDropped;
    /// <summary>Raised on the UI thread, after built-in validation. Set Cancel to reject.</summary>
    public event EventHandler<FileValidationEventArgs> FileValidating;
    public event EventHandler FilesCleared;
    public event EventHandler<FileEntry> EntryStatusChanged;

    #endregion

    #region Identity

    public KryptonDropZoneAlternate()
    {
        AllowDrop = true;
        Padding = new Padding(3); // leaves room for the border, which docked children would otherwise cover
        _theme = DropZoneTheme.FromGlobalPalette();
        _thumbs = new ThumbnailProvider(new Size(Px(64), Px(64)), new Size(Px(16), Px(16)), 400);
        _thumbs.Ready += MarkDirty;

        InitializeControls();
        WireDropTargets();
        RebuildGlyphs();

        _collection.CollectionChanged += (s, e) => RefreshView();
        _collection.FilesAdded += OnFilesAdded;
        _collection.Cleared += (s, e) => FilesCleared?.Invoke(this, EventArgs.Empty);
        _collection.EntryChanged += (s, e) => { MarkDirty(e); EntryStatusChanged?.Invoke(this, e); };
        _monitor = new FileStalenessMonitor(_collection);

        _flushTimer.Tick += (s, e) => FlushDirty();
        KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;
        Paint += (s, e) => DrawBorder(e.Graphics);
        RefreshView();
    }

    protected override void OnHandleCreated(EventArgs e) { base.OnHandleCreated(e); if (!DesignMode)
        {
            _flushTimer.Start();
        }
    }
    protected override void OnHandleDestroyed(EventArgs e) { _flushTimer.Stop(); base.OnHandleDestroyed(e); }
    protected override AccessibleObject CreateAccessibilityInstance() => new DropZoneAccessibleObject(this);

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            KryptonManager.GlobalPaletteChanged -= OnGlobalPaletteChanged;
            _flushTimer.Dispose();
            CancelScan();
            _monitor.Dispose();
            if (_bindingList != null)
            {
                _bindingList.Dispose();
            }

            _fileListView.SmallImageList = null;
            _fileListView.LargeImageList = null;
            _fileListView.StateImageList = null;
            _thumbs.Dispose();
            _glyphImages.Dispose();
            _collection.Dispose(); // deletes temp files from virtual drops
            _contextMenu.Dispose();
            _toolTip.Dispose();
        }
        base.Dispose(disposing);
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        if (keyData == Keys.Escape && IsScanning) { CancelScan(); return true; }
        return base.ProcessCmdKey(ref msg, keyData);
    }

    private int Px(int logical) => (int)Math.Round(logical * DeviceDpi / 96.0);

    #endregion

    #region Initialization

    private void InitializeControls()
    {
        _fileListView = new KryptonListView
        {
            Dock = DockStyle.Fill,
            View = View.Details,
            HeaderStyle = ColumnHeaderStyle.Clickable,
            FullRowSelect = true,
            MultiSelect = true,
            AllowDrop = true,
            ShowItemToolTips = true,
            SmallImageList = _thumbs.Small,
            LargeImageList = _thumbs.Large,
            StateImageList = _glyphImages,
            AccessibleName = DropZoneStrings.Get("Acc_List"),
            AccessibleDescription = DropZoneStrings.Get("Acc_ListDesc")
        };
        _fileListView.Columns.Add(new ColumnHeader { Text = DropZoneStrings.Get("Col_Name"), Width = Px(200) });
        _fileListView.Columns.Add(new ColumnHeader { Text = DropZoneStrings.Get("Col_Size"), Width = Px(80), TextAlign = HorizontalAlignment.Right });
        _fileListView.Columns.Add(new ColumnHeader { Text = DropZoneStrings.Get("Col_Modified"), Width = Px(130) });
        _fileListView.Columns.Add(new ColumnHeader { Text = DropZoneStrings.Get("Col_Status"), Width = Px(140) });

        _fileListView.DragEnter += FileListView_DragEnterOrOver;
        _fileListView.DragOver += FileListView_DragEnterOrOver;
        _fileListView.DragLeave += (s, e) => SetDragHover(false);
        _fileListView.DragDrop += FileListView_DragDrop;
        _fileListView.ItemDrag += (s, e) => { if (!_virtual) { _fileListView.DoDragDrop(e.Item, DragDropEffects.Move); } };
        _fileListView.ColumnClick += FileListView_ColumnClick;
        _fileListView.KeyDown += FileListView_KeyDown;
        _fileListView.MouseDoubleClick += (s, e) => OpenSelectedFile();
        _fileListView.SizeChanged += (s, e) => ResizeColumns();
        _fileListView.RetrieveVirtualItem += (s, e) =>
            e.Item = e.ItemIndex < _collection.Count ? BuildItem(_collection[e.ItemIndex]) : new ListViewItem();

        BuildContextMenu();
        _fileListView.ContextMenuStrip = _contextMenu;

        _dropZoneLabel = new KryptonWrapLabel { Text = DropZoneText, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter };
        _browseButton = new KryptonButton
        {
            Text = DropZoneStrings.Get("Browse"),
            Dock = DockStyle.Bottom,
            Height = 28,
            AccessibleName = DropZoneStrings.Get("Browse"),
            AccessibleDescription = DropZoneStrings.Get("Acc_Description")
        };
        _browseButton.Click += (s, e) => BrowseForFiles();
        _progressBar = new ProgressBar { Dock = DockStyle.Bottom, Height = 4, Style = ProgressBarStyle.Marquee, MarqueeAnimationSpeed = 30, Visible = false };

        _dropzonePanel = new KryptonPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(5),
            AccessibleName = DropZoneStrings.Get("Acc_Name"),
            AccessibleDescription = DropZoneStrings.Get("Acc_Description")
        };
        _dropzonePanel.Controls.Add(_dropZoneLabel);   // fill first, then edge-docked controls
        _dropzonePanel.Controls.Add(_browseButton);
        _dropzonePanel.Controls.Add(_progressBar);

        _statusLabel = new KryptonLabel { Dock = DockStyle.Top, AutoSize = false, Height = 22, AccessibleRole = AccessibleRole.StatusBar };
        _clearButton = new KryptonButton { Text = DropZoneStrings.Get("ClearAll"), Dock = DockStyle.Bottom, Height = 28, Enabled = false };
        _clearButton.Click += (s, e) => ClearFiles();

        _controlsPanel = new KryptonPanel { Dock = DockStyle.Bottom, Padding = new Padding(5) };
        _controlsPanel.Controls.Add(_fileListView);
        _controlsPanel.Controls.Add(_clearButton);
        _controlsPanel.Controls.Add(_statusLabel);

        _separator = new KryptonSeparator { Dock = DockStyle.Top, Height = 1 };
        Controls.Add(_dropzonePanel);
        Controls.Add(_separator);
        Controls.Add(_controlsPanel);
        UpdateControlsPanelHeight();
    }

    private void BuildContextMenu()
    {
        _contextMenu = new ContextMenuStrip();
        _menuOpen = new ToolStripMenuItem(DropZoneStrings.Get("Menu_Open"), null, (s, e) => OpenSelectedFile());
        _menuRemove = new ToolStripMenuItem(DropZoneStrings.Get("Menu_Remove"), null, (s, e) => RemoveSelectedFiles()) { ShortcutKeyDisplayString = "Del" };
        _menuOpenFolder = new ToolStripMenuItem(DropZoneStrings.Get("Menu_OpenFolder"), null, (s, e) => OpenContainingFolder());
        _menuCopy = new ToolStripMenuItem(DropZoneStrings.Get("Menu_CopyPaths"), null, (s, e) => CopySelectedPaths()) { ShortcutKeyDisplayString = "Ctrl+C" };

        var sort = new ToolStripMenuItem(DropZoneStrings.Get("Menu_SortBy"));
        sort.DropDownItems.Add(DropZoneStrings.Get("Col_Name"), null, (s, e) => SortBy(FileSortMode.Name));
        sort.DropDownItems.Add(DropZoneStrings.Get("Col_Size"), null, (s, e) => SortBy(FileSortMode.Size));
        sort.DropDownItems.Add(DropZoneStrings.Get("Menu_Date"), null, (s, e) => SortBy(FileSortMode.Date));
        sort.DropDownItems.Add(DropZoneStrings.Get("Menu_Extension"), null, (s, e) => SortBy(FileSortMode.Extension));

        var view = new ToolStripMenuItem(DropZoneStrings.Get("Menu_View"));
        _viewItems = new[]
        {
                new ToolStripMenuItem(DropZoneStrings.Get("View_Details"), null, (s, e) => ViewMode = FileViewMode.Details) { ShortcutKeyDisplayString = "Ctrl+1" },
                new ToolStripMenuItem(DropZoneStrings.Get("View_Tiles"), null, (s, e) => ViewMode = FileViewMode.Tiles) { ShortcutKeyDisplayString = "Ctrl+2" },
                new ToolStripMenuItem(DropZoneStrings.Get("View_Thumbnails"), null, (s, e) => ViewMode = FileViewMode.Thumbnails) { ShortcutKeyDisplayString = "Ctrl+3" },
            };
        view.DropDownItems.AddRange(_viewItems);

        var verify = new ToolStripMenuItem(DropZoneStrings.Get("Menu_Verify"), null, (s, e) => { var _ = VerifyFilesAsync(); });
        _menuUndo = new ToolStripMenuItem(DropZoneStrings.Get("Menu_Undo"), null, (s, e) => Undo()) { ShortcutKeyDisplayString = "Ctrl+Z" };
        _menuClear = new ToolStripMenuItem(DropZoneStrings.Get("ClearAll"), null, (s, e) => ClearFiles());

        _contextMenu.Items.AddRange(new ToolStripItem[]
        {
                _menuOpen, _menuRemove, new ToolStripSeparator(), _menuOpenFolder, _menuCopy, new ToolStripSeparator(),
                sort, view, verify, new ToolStripSeparator(), _menuUndo, _menuClear
        });
        _contextMenu.Opening += (s, e) =>
        {
            int n = _fileListView.SelectedIndices.Count;
            _menuOpen.Enabled = _menuOpenFolder.Enabled = n == 1;
            _menuRemove.Enabled = _menuCopy.Enabled = n > 0;
            _menuUndo.Enabled = CanUndo;
            _menuClear.Enabled = _collection.Count > 0;
            for (int i = 0; i < _viewItems.Length; i++)
            {
                _viewItems[i].Checked = (int)_viewMode == i;
            }
        };
    }

    /// <summary>Child controls need their own handlers, otherwise drops onto them are rejected.</summary>
    private void WireDropTargets()
    {
        foreach (Control c in new Control[] { this, _dropzonePanel, _dropZoneLabel, _browseButton, _controlsPanel, _statusLabel, _clearButton })
        {
            c.AllowDrop = true;
            c.DragEnter += OnDragEnterOrOver;
            c.DragOver += OnDragEnterOrOver;
            c.DragLeave += (s, e) => SetDragHover(false);
            c.DragDrop += OnDragDrop;
        }
    }

    private void UpdateControlsPanelHeight()
    {
        if (_controlsPanel == null)
        {
            return;
        }

        int h = _controlsPanel.Padding.Vertical;
        if (_statusLabel.Visible)
        {
            h += _statusLabel.Height;
        }

        if (_fileListView.Visible)
        {
            h += _fileListHeight;
        }

        if (_clearButton.Visible)
        {
            h += _clearButton.Height;
        }

        _controlsPanel.Visible = _statusLabel.Visible || _fileListView.Visible || _clearButton.Visible;
        _controlsPanel.Height = h;
    }

    #endregion

    #region Theming

    private void OnGlobalPaletteChanged(object sender, EventArgs e)
    {
        if (IsDisposed)
        {
            return;
        }

        if (InvokeRequired)
        {
            BeginInvoke(new Action(ApplyTheme));
        }
        else
        {
            ApplyTheme();
        }
    }

    private void ApplyTheme()
    {
        _theme = DropZoneTheme.FromGlobalPalette();
        RebuildGlyphs();
        RefreshView(_statusPrefix);
        Invalidate();
    }

    private void RebuildGlyphs()
    {
        int size = Px(16);
        _glyphImages.Images.Clear();
        _glyphImages.ImageSize = new Size(size, size);
        foreach (Bitmap b in _theme.CreateGlyphs(size)) { _glyphImages.Images.Add(b); b.Dispose(); }
    }

    private void DrawBorder(Graphics g)
    {
        using (var pen = new Pen(_dragHover ? _theme.Accent : _theme.Border, _dragHover ? 2f : 1f))
        {
            pen.DashStyle = _dragHover ? DashStyle.Solid : DashStyle.Dot;
            g.DrawRectangle(pen, 1, 1, Width - 3, Height - 3);
        }
    }

    private void SetDragHover(bool hover)
    {
        if (_dragHover == hover)
        {
            return;
        }

        _dragHover = hover;
        Invalidate();
    }

    #endregion

    #region Drag, drop, keyboard

    private static bool IsAcceptable(IDataObject data) =>
        data.GetDataPresent(DataFormats.FileDrop) || data.GetDataPresent("FileGroupDescriptorW") || data.GetDataPresent(DataFormats.Bitmap);

    private void OnDragEnterOrOver(object sender, DragEventArgs e)
    {
        bool ok = IsAcceptable(e.Data);
        e.Effect = ok ? DragDropEffects.Copy : DragDropEffects.None;
        SetDragHover(ok);
    }

    private async void OnDragDrop(object sender, DragEventArgs e)
    {
        SetDragHover(false);
        try
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                await AddPathsAsync((string[])e.Data.GetData(DataFormats.FileDrop));
            }
            else
            {
                IDropDataSource source = _dataSources.FirstOrDefault(s => s.CanHandle(e.Data));
                if (source == null)
                {
                    return;
                }
                // Must run on the UI thread (COM IDataObject). Big Outlook attachments will block briefly.
                await AddEntriesAsync(source.Extract(e.Data).ToList());
            }
        }
        catch (Exception ex) { _statusLabel.Text = DropZoneStrings.Format("Err_Add", ex.Message); }
    }

    private void FileListView_DragEnterOrOver(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(typeof(ListViewItem)))
        {
            e.Effect = DragDropEffects.Move;
        }
        else
        {
            OnDragEnterOrOver(sender, e);
        }
    }

    private void FileListView_DragDrop(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(typeof(ListViewItem)))
        {
            HandleListReorder(e);
        }
        else
        {
            OnDragDrop(sender, e);
        }
    }

    private void HandleListReorder(DragEventArgs e)
    {
        List<int> selected = _fileListView.SelectedIndices.Cast<int>().ToList();
        if (selected.Count == 0)
        {
            return;
        }

        Point p = _fileListView.PointToClient(new Point(e.X, e.Y));
        ListViewItem target = _fileListView.GetItemAt(p.X, p.Y);
        if (target == null)
        {
            return;
        }

        int start = _collection.Move(selected, target.Index);
        if (start < 0)
        {
            return;
        }

        for (int i = 0; i < selected.Count; i++)
        {
            _fileListView.Items[start + i].Selected = true;
        }

        _fileListView.Items[start].Focused = true;
    }

    private void FileListView_KeyDown(object sender, KeyEventArgs e)
    {
        e.Handled = true;
        if (e.KeyCode == Keys.Delete)
        {
            RemoveSelectedFiles();
        }
        else if (e.Control && e.KeyCode == Keys.Z)
        {
            Undo();
        }
        else if (e.Control && e.KeyCode == Keys.C)
        {
            CopySelectedPaths();
        }
        else if (e.Control && e.KeyCode == Keys.V)
        {
            PasteFromClipboard();
        }
        else if (e.Control && e.KeyCode == Keys.O)
        {
            BrowseForFiles();
        }
        else if (e.Control && e.KeyCode == Keys.D1)
        {
            ViewMode = FileViewMode.Details;
        }
        else if (e.Control && e.KeyCode == Keys.D2)
        {
            ViewMode = FileViewMode.Tiles;
        }
        else if (e.Control && e.KeyCode == Keys.D3)
        {
            ViewMode = FileViewMode.Thumbnails;
        }
        else if (e.KeyCode == Keys.Enter)
        {
            OpenSelectedFile();
        }
        else
        {
            e.Handled = false;
        }
    }

    private void FileListView_ColumnClick(object sender, ColumnClickEventArgs e)
    {
        if (e.Column == 0)
        {
            SortBy(FileSortMode.Name);
        }
        else if (e.Column == 1)
        {
            SortBy(FileSortMode.Size);
        }
        else if (e.Column == 2)
        {
            SortBy(FileSortMode.Date);
        }
    }

    private void BrowseForFiles()
    {
        using (var dialog = new OpenFileDialog { Multiselect = true, Title = DropZoneStrings.Get("Dlg_Select") })
        {
            if (_allowed.Count > 0)
            {
                string pattern = string.Join(";", _allowed.Select(x => "*" + x));
                dialog.Filter = DropZoneStrings.Get("Filter_Allowed") + " (" + pattern + ")|" + pattern + "|" +
                                DropZoneStrings.Get("Filter_All") + " (*.*)|*.*";
            }
            if (dialog.ShowDialog(this) == DialogResult.OK) { var _ = AddPathsAsync(dialog.FileNames); }
        }
    }

    private void PasteFromClipboard()
    {
        try
        {
            if (Clipboard.ContainsFileDropList())
            {
                var _ = AddPathsAsync(Clipboard.GetFileDropList().Cast<string>().ToArray());
                return;
            }
            IDataObject data = Clipboard.GetDataObject();
            IDropDataSource source = data == null ? null : _dataSources.FirstOrDefault(s => s.CanHandle(data));
            if (source != null) { var _ = AddEntriesAsync(source.Extract(data).ToList()); }
        }
        catch (ExternalException) { /* clipboard locked by another process */ }
    }

    #endregion

    #region Adding files (background scan → background validation → UI commit)

    public Task AddPathsAsync(IEnumerable<string> paths)
    {
        string[] list = paths?.ToArray() ?? new string[0];
        return RunOperationAsync(async token =>
        {
            bool dirs = AllowDirectories, recurse = SearchSubdirectories;
            var progress = new Progress<int>(n => _statusLabel.Text = DropZoneStrings.Format("Scanning", n));
            List<FileEntry> scanned = await Task.Run(() => ScanPaths(list, dirs, recurse, progress, token), token);
            await AddEntriesCoreAsync(scanned, token);
        });
    }

    public Task AddEntriesAsync(IEnumerable<FileEntry> entries)
    {
        List<FileEntry> list = entries.ToList();
        return RunOperationAsync(token => AddEntriesCoreAsync(list, token));
    }

    /// <summary>Synchronous; fine for a handful of files. Hash/signature checks hit the disk on the UI thread.</summary>
    public void AddFiles(IEnumerable<string> filePaths)
    {
        if (filePaths == null)
        {
            return;
        }

        AddBatch batch = _collection.PrepareAdd(
            filePaths.Where(File.Exists).Select(p => FileEntry.FromDisk(p)), MaxCountOrNull(), BuildValidators());
        ApplyHostValidation(batch);
        _collection.Commit(batch);
    }

    private async Task AddEntriesCoreAsync(List<FileEntry> entries, CancellationToken token)
    {
        AddBatch batch = await _collection.PrepareAddAsync(entries, MaxCountOrNull(), BuildValidators(), token);
        ApplyHostValidation(batch);
        _collection.Commit(batch);
    }

    private int? MaxCountOrNull() => MaxFileCount > 0 ? MaxFileCount : (int?)null;

    /// <summary>Built per operation from current settings, so changes take effect on the next drop.</summary>
    private List<IFileValidator> BuildValidators()
    {
        var list = new List<IFileValidator>();
        if (_allowed.Count > 0)
        {
            list.Add(new ExtensionValidator(_allowed.ToArray()));
        }

        if (MaxFileSize > 0)
        {
            list.Add(new FileSizeValidator(MaxFileSize));
        }

        if (MaxTotalSize > 0)
        {
            list.Add(new TotalSizeValidator(MaxTotalSize));
        }

        if (ValidateFileSignatures)
        {
            list.Add(new FileSignatureValidator());
        }

        if (DetectDuplicateContent)
        {
            list.Add(new ContentHashValidator());
        }

        return list;
    }

    private void ApplyHostValidation(AddBatch batch)
    {
        EventHandler<FileValidationEventArgs> handler = FileValidating;
        if (handler == null)
        {
            return;
        }

        foreach (FileEntry entry in batch.Accepted.ToList())
        {
            var args = new FileValidationEventArgs(entry.Path);
            handler(this, args);
            if (args.Cancel)
            {
                batch.Reject(entry, string.IsNullOrEmpty(args.ValidationMessage) ? DropZoneStrings.Get("Reject_Custom") : args.ValidationMessage);
            }
        }
    }

    private async Task RunOperationAsync(Func<CancellationToken, Task> work)
    {
        await _opLock.WaitAsync();               // drops made while busy queue up in order
        var cts = new CancellationTokenSource();
        _opCts = cts;
        BeginBusy();
        try { await work(cts.Token); }
        catch (OperationCanceledException) { _statusPrefix = DropZoneStrings.Get("Cancelled"); UpdateStatus(true); }
        catch (Exception ex) { _statusLabel.Text = DropZoneStrings.Format("Err_Add", ex.Message); }
        finally
        {
            _opCts = null;
            cts.Dispose();
            if (!IsDisposed)
            {
                EndBusy();
            }

            _opLock.Release();
        }
    }

    public void CancelScan()
    {
        CancellationTokenSource cts = _opCts;
        if (cts != null) { try { cts.Cancel(); } catch (ObjectDisposedException) { } }
    }

    private void BeginBusy() { _busy++; _progressBar.Visible = true; }
    private void EndBusy() { if (--_busy <= 0) { _busy = 0; _progressBar.Visible = false; } }

    private static List<FileEntry> ScanPaths(string[] paths, bool allowDirs, bool recurse, IProgress<int> progress, CancellationToken token)
    {
        var results = new List<FileEntry>();
        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (string path in paths)
        {
            token.ThrowIfCancellationRequested();
            if (Directory.Exists(path))
            {
                if (!allowDirs)
                {
                    continue;
                }

                foreach (string file in EnumerateFilesSafe(path, recurse, token))
                {
                    if (!seen.Add(file))
                    {
                        continue;
                    }

                    results.Add(FileEntry.FromDisk(file));
                    if (results.Count % 200 == 0)
                    {
                        progress.Report(results.Count);
                    }
                }
            }
            else if (File.Exists(path) && seen.Add(path))
            {
                results.Add(FileEntry.FromDisk(path));
            }
        }
        progress.Report(results.Count);
        return results;
    }

    private static IEnumerable<string> EnumerateFilesSafe(string root, bool recurse, CancellationToken token)
    {
        var pending = new Stack<string>();
        pending.Push(root);
        while (pending.Count > 0)
        {
            token.ThrowIfCancellationRequested();
            string dir = pending.Pop();

            string[] files = null;
            try { files = Directory.GetFiles(dir); } catch (UnauthorizedAccessException) { } catch (IOException) { }
            if (files != null)
            {
                foreach (string f in files)
                {
                    yield return f;
                }
            }

            if (!recurse)
            {
                continue;
            }

            string[] subs = null;
            try { subs = Directory.GetDirectories(dir); } catch (UnauthorizedAccessException) { } catch (IOException) { }
            if (subs == null)
            {
                continue;
            }

            foreach (string sub in subs)
            {
                try { if ((File.GetAttributes(sub) & FileAttributes.ReparsePoint) != 0) { continue; } } catch (Exception) { continue; }
                pending.Push(sub);
            }
        }
    }

    private void OnFilesAdded(object sender, FilesAddedEventArgs e)
    {
        var parts = new List<string>();
        if (e.Accepted.Count > 0)
        {
            parts.Add(DropZoneStrings.Format("Count_Added", e.Accepted.Count));
        }

        if (e.Rejected.Count > 0)
        {
            parts.Add(DropZoneStrings.Format("Count_Skipped", e.Rejected.Count));
        }

        if (e.DuplicateCount > 0)
        {
            parts.Add(DropZoneStrings.Format("Count_Duplicates", e.DuplicateCount));
        }

        _statusPrefix = string.Join(", ", parts);

        var lines = e.Rejected.Take(10).Select(r => r.Name + ": " + r.RejectionReason).ToList();
        if (e.Rejected.Count > 10)
        {
            lines.Add("…");
        }

        _toolTip.SetToolTip(_statusLabel, string.Join(Environment.NewLine, lines));

        UpdateStatus(true);
        FilesDropped?.Invoke(this, e);
    }

    #endregion

    #region View

    private void RefreshView(string prefix = null)
    {
        _statusPrefix = prefix;
        bool wantVirtual = _collection.Count >= _virtualThreshold;

        if (wantVirtual != _virtual)
        {
            if (_virtual)
            {
                _fileListView.VirtualListSize = 0;
            }
            else
            {
                _fileListView.Items.Clear();
            }

            _fileListView.VirtualMode = wantVirtual;
            _virtual = wantVirtual;
        }
        _effectiveView = (_viewMode == FileViewMode.Tiles && _virtual) ? FileViewMode.Thumbnails : _viewMode;
        ApplyEffectiveView();

        _fileListView.BeginUpdate();
        try
        {
            if (_virtual)
            {
                _fileListView.VirtualListSize = 0; // reset so cached virtual items are re-requested
                _fileListView.VirtualListSize = _collection.Count;
            }
            else
            {
                _fileListView.Items.Clear();
                var items = new ListViewItem[_collection.Count];
                for (int i = 0; i < items.Length; i++)
                {
                    items[i] = BuildItem(_collection[i]);
                }

                _fileListView.Items.AddRange(items);
            }
        }
        finally { _fileListView.EndUpdate(); }

        _clearButton.Enabled = _collection.Count > 0;
        UpdateSortIndicator();
        UpdateStatus(false);
    }

    private void ApplyEffectiveView()
    {
        switch (_effectiveView)
        {
            case FileViewMode.Tiles:
                _fileListView.View = View.Tile;
                _fileListView.TileSize = new Size(Px(250), Px(72));
                break;
            case FileViewMode.Thumbnails:
                _fileListView.View = View.LargeIcon;
                break;
            default:
                _fileListView.View = View.Details;
                _fileListView.HeaderStyle = ColumnHeaderStyle.Clickable;
                break;
        }
    }

    private ListViewItem BuildItem(FileEntry e)
    {
        var item = new ListViewItem(e.Name);
        item.SubItems.Add(DropZoneStrings.FormatSize(e.Size));
        item.SubItems.Add(e.Modified == DateTime.MinValue ? string.Empty : e.Modified.ToString("g"));
        item.SubItems.Add(string.Empty);
        ApplyEntryVisuals(item, e);
        return item;
    }

    private void ApplyEntryVisuals(ListViewItem item, FileEntry e)
    {
        string key = _thumbs.GetKey(e, _effectiveView != FileViewMode.Details);
        if (_virtual)
        {
            item.ImageIndex = _thumbs.IndexOf(key); // rebuilt on every retrieve, so an index is safe
        }
        else
        {
            item.ImageKey = key;                              // survives LRU eviction shifting indices
        }

        string status = StatusFor(e);
        item.StateImageIndex = GlyphIndex(e);
        item.SubItems[3].Text = status;
        item.ToolTipText = status.Length > 0 ? e.Path + Environment.NewLine + status : e.Path;
        item.ForeColor = e.Stale == StaleState.Missing ? _theme.DisabledText : Color.Empty;
    }

    private static int GlyphIndex(FileEntry e)
    {
        if (e.Stale == StaleState.Missing)
        {
            return GlyphMissing;
        }

        switch (e.Status)
        {
            case UploadStatus.Queued: return GlyphQueued;
            case UploadStatus.Uploading: return GlyphUploading;
            case UploadStatus.Completed: return GlyphCompleted;
            case UploadStatus.Failed: return GlyphFailed;
            case UploadStatus.Cancelled: return GlyphCancelled;
        }
        return e.Stale == StaleState.Modified ? GlyphModified : -1;
    }

    private static string StatusFor(FileEntry e)
    {
        string s = null;
        switch (e.Status)
        {
            case UploadStatus.Queued: s = DropZoneStrings.Get("Status_Queued"); break;
            case UploadStatus.Uploading: s = DropZoneStrings.Format("Status_Uploading", e.Progress); break;
            case UploadStatus.Completed: s = DropZoneStrings.Get("Status_Completed"); break;
            case UploadStatus.Failed: s = DropZoneStrings.Format("Status_Failed", e.StatusMessage ?? string.Empty); break;
            case UploadStatus.Cancelled: s = DropZoneStrings.Get("Status_Cancelled"); break;
        }
        string stale = e.Stale == StaleState.Missing ? DropZoneStrings.Get("Stale_Missing")
                     : e.Stale == StaleState.Modified ? DropZoneStrings.Get("Stale_Modified") : null;
        if (s == null)
        {
            return stale ?? string.Empty;
        }

        return stale == null ? s : s + " • " + stale;
    }

    private void MarkDirty(FileEntry e) { _dirty.Add(e); }

    /// <summary>Coalesces per-file updates (upload progress, thumbnails, staleness) into one repaint every 100 ms.</summary>
    private void FlushDirty()
    {
        if (_dirty.Count == 0)
        {
            return;
        }

        List<FileEntry> batch = _dirty.ToList();
        _dirty.Clear();

        if (_virtual && batch.Count > 500)
        {
            _fileListView.Invalidate();
        }
        else
        {
            foreach (FileEntry e in batch)
            {
                int i = _collection.IndexOf(e);
                if (i < 0)
                {
                    continue;
                }

                if (_virtual)
                {
                    _fileListView.RedrawItems(i, i, true);
                }
                else if (i < _fileListView.Items.Count)
                {
                    ApplyEntryVisuals(_fileListView.Items[i], e);
                }
            }
        }
        UpdateStatus(false);
    }

    private void UpdateSortIndicator()
    {
        string[] keys = { "Col_Name", "Col_Size", "Col_Modified" };
        for (int i = 0; i < keys.Length; i++)
        {
            _fileListView.Columns[i].Text = DropZoneStrings.Get(keys[i]);
        }

        if (!_collection.IsSorted)
        {
            return;
        }

        int col = _collection.SortMode == FileSortMode.Name ? 0 : _collection.SortMode == FileSortMode.Size ? 1
                : _collection.SortMode == FileSortMode.Date ? 2 : -1;
        if (col >= 0)
        {
            _fileListView.Columns[col].Text += _collection.SortAscending ? " ▲" : " ▼";
        }
    }

    private void ResizeColumns()
    {
        if (_fileListView.Columns.Count < 4 || _effectiveView != FileViewMode.Details)
        {
            return;
        }

        int fixedWidth = _fileListView.Columns[1].Width + _fileListView.Columns[2].Width + _fileListView.Columns[3].Width;
        _fileListView.Columns[0].Width = Math.Max(Px(100), _fileListView.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - fixedWidth);
    }

    private void UpdateStatus(bool announce)
    {
        int count = _collection.Count, active = 0, failed = 0, stale = 0;
        long total = 0;
        foreach (FileEntry e in _collection)
        {
            total += e.Size;
            if (e.Status == UploadStatus.Queued || e.Status == UploadStatus.Uploading)
            {
                active++;
            }
            else if (e.Status == UploadStatus.Failed)
            {
                failed++;
            }

            if (e.Stale != StaleState.None)
            {
                stale++;
            }
        }

        string text = count == 0 ? DropZoneStrings.Get("NoFiles")
            : count == 1 ? DropZoneStrings.Format("Summary_One", DropZoneStrings.FormatSize(total))
            : DropZoneStrings.Format("Summary_Many", count, DropZoneStrings.FormatSize(total));
        if (active > 0)
        {
            text += " • " + DropZoneStrings.Format("Count_Active", active);
        }

        if (failed > 0)
        {
            text += " • " + DropZoneStrings.Format("Count_Failed", failed);
        }

        if (stale > 0)
        {
            text += " • " + DropZoneStrings.Format("Count_Stale", stale);
        }

        if (!string.IsNullOrEmpty(_statusPrefix))
        {
            text = _statusPrefix + " — " + text;
        }

        if (_statusLabel.Text == text)
        {
            return;
        }

        _statusLabel.Text = text;
        if (announce)
        {
            AccessibilityNotifyClients(AccessibleEvents.ValueChange, -1);
            AccessibilityNotifyClients((AccessibleEvents)0x8019 /* EVENT_OBJECT_LIVEREGIONCHANGED */, -1);
        }
    }

    #endregion

    #region Commands

    private List<FileEntry> SelectedEntries() =>
        _fileListView.SelectedIndices.Cast<int>().Where(i => i < _collection.Count).Select(i => _collection[i]).ToList();

    private FileEntry SingleSelection() { List<FileEntry> s = SelectedEntries(); return s.Count == 1 ? s[0] : null; }

    private void OpenSelectedFile()
    {
        FileEntry entry = SingleSelection();
        if (entry == null || !File.Exists(entry.Path))
        {
            return;
        }

        try { Process.Start(new ProcessStartInfo(entry.Path) { UseShellExecute = true }); }
        catch (Exception ex) { MessageBox.Show(this, DropZoneStrings.Format("Err_Open", ex.Message), string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning); }
    }

    private void OpenContainingFolder()
    {
        FileEntry entry = SingleSelection();
        if (entry == null || !File.Exists(entry.Path))
        {
            return;
        }

        try { Process.Start("explorer.exe", "/select,\"" + entry.Path + "\""); }
        catch (Exception ex) { MessageBox.Show(this, DropZoneStrings.Format("Err_Open", ex.Message), string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning); }
    }

    private void CopySelectedPaths()
    {
        List<FileEntry> sel = SelectedEntries();
        if (sel.Count == 0)
        {
            return;
        }

        try { Clipboard.SetText(string.Join(Environment.NewLine, sel.Select(e => e.Path))); }
        catch (ExternalException) { }
    }

    public void ClearFiles()
    {
        _collection.Clear();
        _statusPrefix = DropZoneStrings.Get("Cleared");
        UpdateStatus(true);
    }

    public void RemoveSelectedFiles() => _collection.RemoveAt(_fileListView.SelectedIndices.Cast<int>().ToList());

    public void Undo()
    {
        if (!CanUndo)
        {
            return;
        }

        _collection.Undo();
        _statusPrefix = DropZoneStrings.Get("Undone");
        UpdateStatus(true);
    }

    public void SortBy(FileSortMode mode) => _collection.Sort(mode);
    public Task VerifyFilesAsync() => _monitor.VerifyAllAsync();

    public FileEntryBindingList GetBindingList() => _bindingList ?? (_bindingList = new FileEntryBindingList(_collection));

    public void SaveToFile(string path) => File.WriteAllLines(path, _collection.Select(f => f.Path));

    public void LoadFromFile(string path)
    {
        if (!File.Exists(path))
        {
            return;
        }

        _collection.Clear();
        var _ = AddPathsAsync(File.ReadAllLines(path).Where(File.Exists));
    }

    public IEnumerable<FileEntry> GetFilesByExtension(string extension)
    {
        string ext = ExtensionParser.Parse(extension).FirstOrDefault();
        return _collection.Where(f => string.Equals(f.Extension, ext, StringComparison.OrdinalIgnoreCase));
    }

    #endregion

    #region Upload status

    public void SetStatus(FileEntry entry, UploadStatus status, int progress = 0, string message = null) =>
        _collection.SetStatus(entry, status, progress, message);

    public void ResetStatuses() => _collection.ResetStatuses();

    /// <summary>
    /// Runs <paramref name="upload"/> for every entry that isn't queued/done, updating per-file status and progress.
    /// Call from the UI thread. Failures are captured per file and don't stop the others.
    /// </summary>
    public async Task UploadAllAsync(Func<FileEntry, IProgress<int>, CancellationToken, Task> upload,
        int maxParallel = 2, CancellationToken token = default(CancellationToken))
    {
        List<FileEntry> targets = _collection
            .Where(e => e.Status == UploadStatus.None || e.Status == UploadStatus.Failed || e.Status == UploadStatus.Cancelled)
            .ToList();
        foreach (FileEntry e in targets)
        {
            SetStatus(e, UploadStatus.Queued);
        }

        var gate = new SemaphoreSlim(Math.Max(1, maxParallel));
        await Task.WhenAll(targets.Select(async entry =>
        {
            try { await gate.WaitAsync(token); }
            catch (OperationCanceledException) { SetStatus(entry, UploadStatus.Cancelled); return; }
            try
            {
                SetStatus(entry, UploadStatus.Uploading, 0);
                var progress = new Progress<int>(p => SetStatus(entry, UploadStatus.Uploading, p));
                await upload(entry, progress, token);
                SetStatus(entry, UploadStatus.Completed, 100);
            }
            catch (OperationCanceledException) { SetStatus(entry, UploadStatus.Cancelled); }
            catch (Exception ex) { SetStatus(entry, UploadStatus.Failed, 0, ex.Message); }
            finally { gate.Release(); }
        }));
    }

    #endregion
}
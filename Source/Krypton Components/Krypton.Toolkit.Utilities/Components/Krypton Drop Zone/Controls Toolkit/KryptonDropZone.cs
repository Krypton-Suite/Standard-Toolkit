#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using ContentAlignment = System.Drawing.ContentAlignment;
using Timer = System.Windows.Forms.Timer;

namespace Krypton.Toolkit.Utilities;

[ToolboxItem(true)]
[DesignerCategory("Code")]
[DefaultEvent(nameof(FilesDropped))]
public class KryptonDropZone : KryptonPanel
{
    #region Nested Types

    public enum FileSortMode
    {
        Name, 
        Size,
        Date, 
        Extension
    }

    /// <summary>Visual feedback scenarios for drop-zone animations.</summary>
    public enum DropZoneAnimationScenario
    {
        Idle,
        DragHover,
        DropSuccess,
        DropRejected,
        DropPartial,
        QuotaWarning,
        QuotaExceeded
    }

    public class FilesDroppedEventArgs : EventArgs
    {
        public IReadOnlyList<string> ValidFiles { get; }
        public IReadOnlyList<string> InvalidFiles { get; }
        public IReadOnlyList<string> AllFiles { get; }

        internal FilesDroppedEventArgs(List<string> validFiles, List<string> invalidFiles, List<string> allFiles)
        {
            ValidFiles = validFiles.AsReadOnly();
            InvalidFiles = invalidFiles.AsReadOnly();
            AllFiles = allFiles.AsReadOnly();
        }
    }

    public class FileValidationEventArgs : CancelEventArgs
    {
        public string FilePath { get; }
        public string ValidationMessage { get; set; } = string.Empty;

        internal FileValidationEventArgs(string filePath) => FilePath = filePath;
    }

    #endregion

    #region Constants

    private const int MaxUndoLevels = 10;

    #endregion

    #region Instance Fields

    private KryptonListView _fileListView;
    private KryptonWrapLabel _dropZoneLabel;
    private KryptonButton _clearButton;
    private KryptonButton _browseButton;
    private KryptonLabel _statusLabel;
    private KryptonSeparator _controlSeparator;
    private KryptonPanel _dropzonePanel;
    private KryptonPanel _controlsPanel;
    private ProgressBar _scanProgressBar;
    private KryptonProgressBar _quotaProgressBar;
    private KryptonLabel _quotaLabel;
    private ContextMenuStrip _contextMenu;
    private ToolStripMenuItem _menuOpen, _menuRemove, _menuOpenFolder, _menuCopyPaths, _menuUndo, _menuClear;
    private ToolStripMenuItem _menuSortBy, _menuSortName, _menuSortSize, _menuSortDate, _menuSortExtension;

    private readonly KryptonDropZoneStrings _strings;
    private readonly KryptonDropZoneAnimationValues _animation;
    private readonly KryptonDropZoneAppearanceValues _appearance;
    private readonly KryptonDropZoneBehaviorValues _behavior;
    private readonly KryptonDropZoneDataValues _data;

    private KryptonLabel _headerLabel;
    private KryptonLabel _previewHeaderLabel;
    private KryptonButton _cancelButton;
    private KryptonButton _submitButton;
    private KryptonPanel _actionPanel;
    private PictureBox _uploadIcon;
    private readonly ImageList _previewIconCache = new()
    {
        ImageSize = new Size(32, 32),
        ColorDepth = ColorDepth.Depth32Bit
    };

    private Color _dropZoneOverlayColor = Color.Transparent;
    private bool _isDragHoverInvalid;
    private int _stripeOffset;
    private Timer? _stripeTimer;
    private Image? _renderedUploadIconImage;
    private bool _outerBorderPaintAttached;

    private readonly List<string> _droppedFilesList = [];
    private readonly Dictionary<string, string> _rejectionReasons = new();
    private readonly List<List<string>> _undoStack = [];
    private readonly ImageList _iconCache = new()
    {
        ImageSize = new Size(16, 16),
        ColorDepth = ColorDepth.Depth32Bit
    };

    private FileSortMode _currentSortMode = FileSortMode.Name;
    private bool _sortAscending = true;
    private Timer? _animationTimer;
    private Color _animationStartColor;
    private Color _animationTargetColor;
    private DateTime _animationStartTime;
    private int _animationDurationMs;
    private Action? _animationCompleteCallback;
    private bool _isDragHoverActive;
    private DropZoneAnimationScenario _currentAnimationScenario = DropZoneAnimationScenario.Idle;

    private ListViewItem? _reorderDragItem;
    private Point _reorderDragStartPoint;
    private bool _isReorderDragInProgress;

    #endregion

    #region Properties

    [Category("Visuals")]
    [Description("Localizable strings used by the drop zone control.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonDropZoneStrings Strings => _strings;

    [Category("Visuals")]
    [Description("Animation settings for drop-zone visual feedback.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonDropZoneAnimationValues Animation => _animation;

    [Category("Visuals")]
    [Description("Layout and card-style appearance settings for the drop zone control.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonDropZoneAppearanceValues Appearance => _appearance;

    [Category("Behavior")]
    [Description("Behavior settings for the drop zone control.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonDropZoneBehaviorValues Behavior => _behavior;

    public bool ShouldSerializeBehavior() => !Behavior.IsDefault;

    private void ResetBehavior() => Behavior.Reset();

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool ShowFileListView
    {
        get => _behavior.ShowFileListView;
        set => _behavior.ShowFileListView = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool ShowClearButton
    {
        get => _behavior.ShowClearButton;
        set => _behavior.ShowClearButton = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool ShowBrowseButton
    {
        get => _behavior.ShowBrowseButton;
        set => _behavior.ShowBrowseButton = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool ShowStatusLabel
    {
        get => _behavior.ShowStatusLabel;
        set => _behavior.ShowStatusLabel = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public List<string> AllowedExtensions => _behavior.AllowedExtensions;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int MaxFileCount
    {
        get => _behavior.MaxFileCount;
        set => _behavior.MaxFileCount = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public long MaxFileSize
    {
        get => _behavior.MaxFileSize;
        set => _behavior.MaxFileSize = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public long UploadSizeQuota
    {
        get => _behavior.UploadSizeQuota;
        set => _behavior.UploadSizeQuota = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool ShowUploadQuotaProgressBar
    {
        get => _behavior.ShowUploadQuotaProgressBar;
        set => _behavior.ShowUploadQuotaProgressBar = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool AllowDirectories
    {
        get => _behavior.AllowDirectories;
        set => _behavior.AllowDirectories = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool SearchSubdirectories
    {
        get => _behavior.SearchSubdirectories;
        set => _behavior.SearchSubdirectories = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool EnableUndo
    {
        get => _behavior.EnableUndo;
        set => _behavior.EnableUndo = value;
    }

    [Category("Data")]
    [Description("Read-only runtime state of the dropped file list, selection, and animation.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonDropZoneDataValues Data => _data;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Shortcut to Strings.DropZoneText. Prefer Strings.DropZoneText in the designer.")]
    [DefaultValue(KryptonDropZoneStrings.DEFAULT_DROP_ZONE_TEXT)]
    public string DropZoneText
    {
        get => _strings.DropZoneText;
        set => _strings.DropZoneText = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IReadOnlyList<string> DroppedFiles => _data.DroppedFiles;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string? SelectedFile => _data.SelectedFile;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IReadOnlyList<string> SelectedFiles => _data.SelectedFiles;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int FileCount => _data.FileCount;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool CanUndo => _data.CanUndo;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public long TotalDroppedSize => _data.TotalDroppedSize;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public long RemainingUploadSize => _data.RemainingUploadSize;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DropZoneAnimationScenario CurrentAnimationScenario => _data.CurrentAnimationScenario;

    #endregion

    #region Events

    public event EventHandler<FilesDroppedEventArgs> FilesDropped;
    public event EventHandler<FileValidationEventArgs> FileValidating;
    public event EventHandler FilesCleared;
    public event EventHandler FilesSubmit;
    public event PropertyChangedEventHandler PropertyChanged;

    #endregion

    #region Identity

    public KryptonDropZone()
    {
        _strings = new KryptonDropZoneStrings(this);
        _animation = new KryptonDropZoneAnimationValues(this);
        _appearance = new KryptonDropZoneAppearanceValues(this);
        _behavior = new KryptonDropZoneBehaviorValues(this);
        _data = new KryptonDropZoneDataValues(this);
        AllowDrop = true;
        InitializeControls();
        InitializeEvents();
        ApplyThemeStyling();
        ApplyBehaviorToControls();
        ApplyLayout();
        ApplyStrings();
        SetDropZonePanelColor(_animation.IdleColor);
        UpdateStatusLabel();
        UpdateQuotaDisplay();
        KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            KryptonManager.GlobalPaletteChanged -= OnGlobalPaletteChanged;
            StopDropZoneAnimation();
            StopStripeAnimation();
            _animationTimer?.Dispose();
            _iconCache?.Dispose();
            _previewIconCache?.Dispose();
            DisposeRenderedUploadIconImage();
            _contextMenu?.Dispose();
        }
        base.Dispose(disposing);
    }

    #endregion

    #region Initialization

    private void InitializeControls()
    {
        _fileListView = new KryptonListView
        {
            Dock = DockStyle.Fill,
            View = View.Details,
            HeaderStyle = ColumnHeaderStyle.None,
            FullRowSelect = true,
            MultiSelect = true,
            SmallImageList = _iconCache,
            AllowDrop = true,
            ShowItemToolTips = true
        };
        _fileListView.Columns.Add(new ColumnHeader { Width = -2 });
        _fileListView.DragEnter += FileListView_DragEnter;
        _fileListView.DragDrop += FileListView_DragDrop;
        _fileListView.MouseDown += FileListView_MouseDown;
        _fileListView.MouseMove += FileListView_MouseMove;
        _fileListView.MouseUp += FileListView_MouseUp;
        _fileListView.KeyDown += FileListView_KeyDown;
        _fileListView.MouseDoubleClick += FileListView_MouseDoubleClick;

        BuildContextMenu();
        _fileListView.ContextMenuStrip = _contextMenu;

        _dropZoneLabel = new KryptonWrapLabel
        {
            Dock = DockStyle.Fill,
            AutoSize = false,
            TextAlign = ContentAlignment.MiddleCenter
        };

        _uploadIcon = new PictureBox
        {
            Size = new Size(48, 48),
            SizeMode = PictureBoxSizeMode.Zoom,
            BackColor = Color.Transparent,
            TabStop = false
        };

        _headerLabel = new KryptonLabel
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = 28,
            LabelStyle = LabelStyle.TitleControl,
            Visible = false
        };

        _previewHeaderLabel = new KryptonLabel
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = 22,
            LabelStyle = LabelStyle.BoldControl,
            Visible = false
        };

        _browseButton = new KryptonButton
        {
            Dock = DockStyle.Bottom,
            Height = 28
        };
        _browseButton.Click += BrowseButton_Click;

        _scanProgressBar = new ProgressBar
        {
            Dock = DockStyle.Bottom,
            Height = 4,
            Style = ProgressBarStyle.Marquee,
            MarqueeAnimationSpeed = 30,
            Visible = false
        };

        _dropzonePanel = new KryptonPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(5),
            AllowDrop = true
        };
        _dropzonePanel.Controls.Add(_scanProgressBar);
        _dropzonePanel.Controls.Add(_browseButton);
        _dropzonePanel.Controls.Add(_uploadIcon);
        _dropzonePanel.Controls.Add(_dropZoneLabel);
        _dropzonePanel.Resize += (_, _) => CenterDropZoneContent();
        _dropzonePanel.Layout += (_, _) => CenterDropZoneContent();
        _dropzonePanel.Paint += DropzonePanel_Paint;
        _dropzonePanel.Click += OnDropZoneAreaClick;
        _dropzonePanel.DragEnter += KryptonDropZone_DragEnter;
        _dropzonePanel.DragOver += KryptonDropZone_DragOver;
        _dropzonePanel.DragLeave += KryptonDropZone_DragLeave;
        _dropzonePanel.DragDrop += KryptonDropZone_DragDrop;
        _dropZoneLabel.Click += OnDropZoneAreaClick;
        _uploadIcon.Click += OnDropZoneAreaClick;

        _statusLabel = new KryptonLabel
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = 20,
            LabelStyle = LabelStyle.AlternateControl
        };

        _quotaLabel = new KryptonLabel
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = 20,
            LabelStyle = LabelStyle.AlternateControl,
            Visible = false
        };

        _quotaProgressBar = new KryptonProgressBar
        {
            Dock = DockStyle.Top,
            Height = 18,
            Minimum = 0,
            Maximum = 100,
            Value = 0,
            Visible = false
        };

        _clearButton = new KryptonButton
        {
            Dock = DockStyle.Bottom,
            Height = 30
        };
        _clearButton.Click += (s, e) => ClearFiles();

        _cancelButton = new KryptonButton
        {
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            ButtonStyle = ButtonStyle.LowProfile
        };
        _cancelButton.Click += (_, _) => ClearFiles();

        _submitButton = new KryptonButton
        {
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            ButtonStyle = ButtonStyle.LowProfile
        };
        _submitButton.Click += (_, _) => FilesSubmit?.Invoke(this, EventArgs.Empty);

        var actionButtonFlow = new FlowLayoutPanel
        {
            Dock = DockStyle.Right,
            AutoSize = true,
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = false,
            Padding = new Padding(0),
            Margin = new Padding(0)
        };
        actionButtonFlow.Controls.Add(_submitButton);
        actionButtonFlow.Controls.Add(_cancelButton);

        _actionPanel = new KryptonPanel
        {
            Dock = DockStyle.Bottom,
            Height = 36,
            Padding = new Padding(0, 4, 0, 0),
            Visible = false
        };
        _actionPanel.Controls.Add(actionButtonFlow);

        _controlsPanel = new KryptonPanel
        {
            Dock = DockStyle.Bottom,
            AutoSize = true,
            Padding = new Padding(5)
        };
        _controlsPanel.Controls.Add(_previewHeaderLabel);
        _controlsPanel.Controls.Add(_statusLabel);
        _controlsPanel.Controls.Add(_quotaLabel);
        _controlsPanel.Controls.Add(_quotaProgressBar);
        _controlsPanel.Controls.Add(_actionPanel);
        _controlsPanel.Controls.Add(_clearButton);
        _controlsPanel.Controls.Add(_fileListView);

        _controlSeparator = new KryptonSeparator { Dock = DockStyle.Top, Height = 1 };

        Controls.Add(_headerLabel);
        Controls.Add(_dropzonePanel);
        Controls.Add(_controlSeparator);
        Controls.Add(_controlsPanel);
    }

    private void BuildContextMenu()
    {
        _contextMenu = new ContextMenuStrip();

        _menuOpen = new ToolStripMenuItem(null, null, (s, e) => OpenSelectedFile());
        _menuRemove = new ToolStripMenuItem(null, null, (s, e) => RemoveSelectedFiles());
        _menuOpenFolder = new ToolStripMenuItem(null, null, (s, e) => OpenContainingFolder());
        _menuCopyPaths = new ToolStripMenuItem(null, null, (s, e) => CopySelectedPaths());

        _menuSortBy = new ToolStripMenuItem();
        _menuSortName = new ToolStripMenuItem(null, null, (s, e) => SortFiles(FileSortMode.Name));
        _menuSortSize = new ToolStripMenuItem(null, null, (s, e) => SortFiles(FileSortMode.Size));
        _menuSortDate = new ToolStripMenuItem(null, null, (s, e) => SortFiles(FileSortMode.Date));
        _menuSortExtension = new ToolStripMenuItem(null, null, (s, e) => SortFiles(FileSortMode.Extension));
        _menuSortBy.DropDownItems.AddRange([_menuSortName, _menuSortSize, _menuSortDate, _menuSortExtension]);

        _menuUndo = new ToolStripMenuItem(null, null, (s, e) => Undo());
        _menuClear = new ToolStripMenuItem(null, null, (s, e) => ClearFiles());

        _contextMenu.Items.AddRange([
            _menuOpen, _menuRemove, new ToolStripSeparator(),
            _menuOpenFolder, _menuCopyPaths, new ToolStripSeparator(),
            _menuSortBy, new ToolStripSeparator(),
            _menuUndo, _menuClear
        ]);

        _contextMenu.Opening += (s, e) =>
        {
            int selected = _fileListView.SelectedItems.Count;
            _menuOpen.Enabled = selected == 1;
            _menuRemove.Enabled = selected > 0;
            _menuOpenFolder.Enabled = selected == 1;
            _menuCopyPaths.Enabled = selected > 0;
            _menuUndo.Enabled = EnableUndo && CanUndo;
            _menuClear.Enabled = _droppedFilesList.Count > 0;
        };
    }

    private void InitializeEvents()
    {
        DragEnter += KryptonDropZone_DragEnter;
        DragOver += KryptonDropZone_DragOver;
        DragLeave += KryptonDropZone_DragLeave;
        DragDrop += KryptonDropZone_DragDrop;
    }

    private void ApplyThemeStyling()
    {
        if (IsCardLayout)
        {
            if (_outerBorderPaintAttached)
            {
                Paint -= OnOuterBorderPaint;
                _outerBorderPaintAttached = false;
            }

            ApplyCardThemeColors();
        }
        else
        {
            StateCommon.Color1 = Color.WhiteSmoke;
            StateCommon.Color2 = Color.WhiteSmoke;
            if (!_outerBorderPaintAttached)
            {
                Paint += OnOuterBorderPaint;
                _outerBorderPaintAttached = true;
            }
        }
    }

    internal void OnAppearanceValuesChanged()
    {
        ApplyLayout();
        ApplyThemeStyling();
        UpdateListView();
        if (!_isDragHoverActive)
        {
            RefreshIdleDropZoneAppearance(instant: !_animation.Enabled);
        }
    }

    internal void OnStringsChanged()
    {
        ApplyStrings();
        UpdateListView();
        OnPropertyChanged(nameof(DropZoneText));
    }

    internal void OnAnimationValuesChanged()
    {
        if (!_isDragHoverActive)
        {
            RefreshIdleDropZoneAppearance(instant: !_animation.Enabled);
        }
    }

    internal void OnBehaviorValuesChanged(string? propertyName)
    {
        ApplyBehaviorToControls();
        UpdateQuotaDisplay();
        if (propertyName is { Length: > 0 })
        {
            OnPropertyChanged(propertyName);
        }
    }

    private void ApplyBehaviorToControls()
    {
        _fileListView.Visible = _behavior.ShowFileListView;
        _browseButton.Visible = _behavior.ShowBrowseButton;
        _statusLabel.Visible = _behavior.ShowStatusLabel;
        ApplyLayout();
    }

    private void ApplyStrings()
    {
        _fileListView.AccessibleName = _strings.FileListAccessibleName;
        _fileListView.AccessibleDescription = _strings.FileListAccessibleDescription;
        if (_fileListView.Columns.Count > 0)
        {
            _fileListView.Columns[0].Text = _strings.FileNameColumn;
        }

        _dropZoneLabel.Text = _strings.DropZoneText;
        _browseButton.Text = _strings.BrowseButton;
        _headerLabel.Text = _strings.HeaderText;
        _previewHeaderLabel.Text = _strings.PreviewHeader;
        _cancelButton.Text = _strings.CancelButton;
        _submitButton.Text = _strings.SubmitButton;
        _browseButton.AccessibleName = _strings.BrowseAccessibleName;
        _browseButton.AccessibleDescription = _strings.BrowseAccessibleDescription;
        _dropzonePanel.AccessibleName = _strings.DropZoneAccessibleName;
        _dropzonePanel.AccessibleDescription = _strings.DropZoneAccessibleDescription;
        _clearButton.Text = _strings.ClearAllButton;

        _menuOpen.Text = _strings.MenuOpen;
        _menuRemove.Text = _strings.MenuRemove;
        _menuRemove.ShortcutKeyDisplayString = _strings.ShortcutDelete;
        _menuOpenFolder.Text = _strings.MenuOpenFolder;
        _menuCopyPaths.Text = _strings.MenuCopyPaths;
        _menuCopyPaths.ShortcutKeyDisplayString = _strings.ShortcutCopy;
        _menuSortBy.Text = _strings.MenuSortBy;
        _menuSortName.Text = _strings.MenuSortName;
        _menuSortSize.Text = _strings.MenuSortSize;
        _menuSortDate.Text = _strings.MenuSortDate;
        _menuSortExtension.Text = _strings.MenuSortExtension;
        _menuUndo.Text = _strings.MenuUndo;
        _menuUndo.ShortcutKeyDisplayString = _strings.ShortcutUndo;
        _menuClear.Text = _strings.MenuClear;

        _quotaLabel.AccessibleName = _strings.UploadQuotaAccessibleName;
        _quotaLabel.AccessibleDescription = _strings.UploadQuotaAccessibleDescription;
        _quotaProgressBar.AccessibleName = _strings.UploadQuotaAccessibleName;
        _quotaProgressBar.AccessibleDescription = _strings.UploadQuotaAccessibleDescription;
        ApplyLayout();
    }

    private void ApplyLayout()
    {
        bool card = IsCardLayout;

        _headerLabel.Visible = card && !string.IsNullOrWhiteSpace(_strings.HeaderText);
        _headerLabel.Padding = card ? new Padding(4, 0, 4, 4) : Padding.Empty;
        _previewHeaderLabel.Visible = card && _appearance.ShowPreviewHeader && ShowFileListView;
        _actionPanel.Visible = card && _appearance.ShowActionButtons;
        _uploadIcon.Visible = card && _appearance.ShowUploadIcon;
        _controlSeparator.Visible = !card;

        int iconSize = _appearance.UploadIconSize;
        _uploadIcon.Size = new Size(iconSize, iconSize);

        _dropzonePanel.Padding = card ? new Padding(16) : new Padding(5);
        _dropzonePanel.MinimumSize = card ? new Size(0, _appearance.MinDropZoneHeight) : Size.Empty;
        _dropzonePanel.Cursor = card ? Cursors.Hand : Cursors.Default;
        _dropzonePanel.StateCommon.Color1 = card ? Color.Transparent : _dropzonePanel.StateCommon.Color1;
        _dropzonePanel.StateCommon.Color2 = card ? Color.Transparent : _dropzonePanel.StateCommon.Color2;

        _dropZoneLabel.Dock = card ? DockStyle.None : DockStyle.Fill;
        _dropZoneLabel.AutoSize = false;
        _dropZoneLabel.TextAlign = ContentAlignment.MiddleCenter;
        _dropZoneLabel.LabelStyle = card ? LabelStyle.NormalControl : _dropZoneLabel.LabelStyle;
        _dropZoneLabel.Cursor = card ? Cursors.Hand : Cursors.Default;

        _browseButton.Visible = ShowBrowseButton && !card;
        _clearButton.Visible = ShowClearButton && !card && _droppedFilesList.Count > 0;

        _fileListView.View = card ? View.Tile : View.Details;
        _fileListView.HeaderStyle = card ? ColumnHeaderStyle.None : ColumnHeaderStyle.None;
        _fileListView.SmallImageList = _appearance.ShowFileListIcons
            ? (card ? _previewIconCache : _iconCache)
            : null;
        if (card)
        {
            _fileListView.TileSize = new Size(72, 72);
            _fileListView.Height = _appearance.PreviewListHeight;
            _fileListView.Dock = DockStyle.Top;
        }
        else
        {
            _fileListView.Height = 120;
            _fileListView.Dock = DockStyle.Fill;
        }

        if (card)
        {
            UpdateUploadIcon();
            CenterDropZoneContent();
        }

        Invalidate();
        _dropzonePanel.Invalidate();
    }

    private bool IsCardLayout => _appearance.Layout == DropZoneLayout.Card;

    #endregion

    #region Event Handlers

    private void KryptonDropZone_DragEnter(object? sender, DragEventArgs e)
    {
        e.Effect = e.Data!.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;
        if (e.Effect == DragDropEffects.Copy)
        {
            UpdateDragHoverInvalidState(e);
            BeginDragHoverAnimation();
        }
    }

    private void KryptonDropZone_DragOver(object? sender, DragEventArgs e)
    {
        e.Effect = e.Data!.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;
        if (e.Effect == DragDropEffects.Copy)
        {
            UpdateDragHoverInvalidState(e);
        }
    }

    private void KryptonDropZone_DragLeave(object? sender, EventArgs e)
    {
        _isDragHoverInvalid = false;
        EndDragHoverAnimation();
    }

    private async void KryptonDropZone_DragDrop(object? sender, DragEventArgs e)
    {
        EndDragHoverAnimation();
        await HandleDropAsync(e);
    }

    private void FileListView_DragEnter(object? sender, DragEventArgs e)
    {
        if (e.Data!.GetDataPresent(typeof(ListViewItem)))
        {
            e.Effect = DragDropEffects.Move;
        }
        else if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            e.Effect = DragDropEffects.Copy;
            BeginDragHoverAnimation();
        }
        else
        {
            e.Effect = DragDropEffects.None;
        }
    }

    private async void FileListView_DragDrop(object? sender, DragEventArgs e)
    {
        if (e.Data!.GetDataPresent(typeof(ListViewItem)))
        {
            HandleListReorder(e);
        }
        else if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            EndDragHoverAnimation();
            await HandleDropAsync(e);
        }
    }

    private void FileListView_MouseDown(object? sender, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Left)
        {
            return;
        }

        _reorderDragItem = _fileListView.GetItemAt(e.X, e.Y);
        if (_reorderDragItem == null)
        {
            return;
        }

        if (!_fileListView.SelectedItems.Contains(_reorderDragItem))
        {
            _fileListView.SelectedItems.Clear();
            _reorderDragItem.Selected = true;
        }

        _reorderDragStartPoint = new Point(e.X, e.Y);
        _isReorderDragInProgress = false;
    }

    private void FileListView_MouseMove(object? sender, MouseEventArgs e)
    {
        if (_reorderDragItem == null)
        {
            return;
        }

        if ((Control.MouseButtons & MouseButtons.Left) == 0 || _isReorderDragInProgress)
        {
            return;
        }

        int dx = e.X - _reorderDragStartPoint.X;
        int dy = e.Y - _reorderDragStartPoint.Y;
        if (Math.Abs(dx) < SystemInformation.DragSize.Width && Math.Abs(dy) < SystemInformation.DragSize.Height)
        {
            return;
        }

        _isReorderDragInProgress = true;
        if (!_fileListView.SelectedItems.Contains(_reorderDragItem))
        {
            _fileListView.SelectedItems.Clear();
            _reorderDragItem.Selected = true;
        }

        _fileListView.DoDragDrop(_reorderDragItem, DragDropEffects.Move);

        _reorderDragItem = null;
        _isReorderDragInProgress = false;
    }

    private void FileListView_MouseUp(object? sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            _reorderDragItem = null;
            _isReorderDragInProgress = false;
        }
    }

    private void FileListView_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Delete && _fileListView.SelectedItems.Count > 0)
        {
            RemoveSelectedFiles(); e.Handled = true;
        }
        else if (e is { Control: true, KeyCode: Keys.Z })
        {
            Undo(); e.Handled = true;
        }
        else if (e is { Control: true, KeyCode: Keys.C })
        {
            CopySelectedPaths(); 
            e.Handled = true;
        }
        else if (e is { KeyCode: Keys.Enter })
        {
            OpenSelectedFile(); e.Handled = true;
        }
    }

    private void FileListView_MouseDoubleClick(object? sender, MouseEventArgs e) => OpenSelectedFile();

    private void BrowseButton_Click(object? sender, EventArgs e)
    {
        using var dialog = new OpenFileDialog { Multiselect = true, Title = _strings.OpenFileDialogTitle };
        if (AllowedExtensions.Count > 0)
        {
            string filter = string.Join(";", AllowedExtensions.Select(ext => "*" + ext));
            dialog.Filter = string.Format(_strings.OpenFileDialogFilterFormat, filter);
        }
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            AddFiles(dialog.FileNames);
        }
    }

    private void OnDropZoneAreaClick(object? sender, EventArgs e)
    {
        if (IsCardLayout)
        {
            BrowseButton_Click(sender, e);
        }
    }

    #endregion

    #region Core Logic

    private void OnOuterBorderPaint(object? sender, PaintEventArgs e) => DrawDottedBorder(e.Graphics);

    private void DrawDottedBorder(Graphics g)
    {
        using var pen = new Pen(Color.Gray) { DashStyle = DashStyle.Dot };
        var rect = ClientRectangle;
        rect.Inflate(-1, -1);
        g.DrawRectangle(pen, rect);
    }

    private async Task HandleDropAsync(DragEventArgs e)
    {
        if (e.Data!.GetData(DataFormats.FileDrop) is not string[] droppedPaths)
        {
            return;
        }

        _scanProgressBar.Visible = true;
        List<string> allFiles;
        try
        {
            allFiles = await Task.Run(() =>
            {
                var results = new List<string>();
                var searchOption = SearchSubdirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
                foreach (string path in droppedPaths)
                {
                    if (Directory.Exists(path) && AllowDirectories)
                    {
                        try { results.AddRange(Directory.GetFiles(path, "*.*", searchOption)); }
                        catch (UnauthorizedAccessException) { /* skip inaccessible folders */ }
                    }
                    else if (File.Exists(path))
                    {
                        results.Add(path);
                    }
                }
                return results.Distinct().ToList();
            });
        }
        finally
        {
            _scanProgressBar.Visible = false;
        }

        ProcessIncomingFiles(allFiles);
    }

    private void ProcessIncomingFiles(List<string> allFiles)
    {
        var validFiles = new List<string>();
        var invalidFiles = new List<string>();
        _rejectionReasons.Clear();

        foreach (string file in allFiles)
        {
            var validationArgs = new FileValidationEventArgs(file);
            FileValidating?.Invoke(this, validationArgs);

            if (validationArgs.Cancel)
            {
                invalidFiles.Add(file);
                _rejectionReasons[file] = string.IsNullOrEmpty(validationArgs.ValidationMessage) ? _strings.CustomValidationRejected : validationArgs.ValidationMessage;
                continue;
            }

            if (!IsValidFile(file, out string reason))
            {
                invalidFiles.Add(file);
                _rejectionReasons[file] = reason;
                continue;
            }

            validFiles.Add(file);
        }

        if (MaxFileCount > 0 && _droppedFilesList.Count + validFiles.Count > MaxFileCount)
        {
            int allowedCount = Math.Max(0, MaxFileCount - _droppedFilesList.Count);
            var overflow = validFiles.Skip(allowedCount).ToList();
            foreach (var f in overflow) _rejectionReasons[f] = string.Format(_strings.ExceedsMaxFileCountFormat, MaxFileCount);
            invalidFiles.AddRange(overflow);
            validFiles = validFiles.Take(allowedCount).ToList();
        }

        if (_behavior.UploadSizeQuota > 0 && validFiles.Count > 0)
        {
            long used = GetTotalDroppedSize();
            var withinQuota = new List<string>(validFiles.Count);
            foreach (string file in validFiles)
            {
                long fileSize = GetFileSizeSafe(file);
                if (used + fileSize > _behavior.UploadSizeQuota)
                {
                    invalidFiles.Add(file);
                    _rejectionReasons[file] = string.Format(_strings.ExceedsUploadQuotaFormat, FormatFileSize(Math.Max(0, _behavior.UploadSizeQuota - used)));
                    continue;
                }

                withinQuota.Add(file);
                used += fileSize;
            }

            validFiles = withinQuota;
        }

        var newFiles = validFiles.Where(f => !_droppedFilesList.Contains(f)).ToList();

        if (newFiles.Count > 0)
        {
            PushUndoState();
        }

        _droppedFilesList.AddRange(newFiles);

        UpdateListView();
        FilesDropped?.Invoke(this, new FilesDroppedEventArgs(newFiles, invalidFiles, allFiles));
        PlayDropResultAnimation(newFiles.Count, invalidFiles.Count);
    }

    private bool IsValidFile(string filePath, out string reason)
    {
        reason = string.Empty;

        if (AllowedExtensions.Count > 0)
        {
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (!AllowedExtensions.Any(a => string.Equals(a, ext, StringComparison.OrdinalIgnoreCase)))
            {
                reason = string.Format(_strings.ExtensionNotAllowedFormat, ext);
                return false;
            }
        }

        if (MaxFileSize > 0)
        {
            try
            {
                var info = new FileInfo(filePath);
                if (info.Length > MaxFileSize)
                {
                    reason = string.Format(_strings.FileExceedsMaxSizeFormat, FormatFileSize(MaxFileSize));
                    return false;
                }
            }
            catch (Exception ex)
            {
                reason = string.Format(_strings.UnableToInspectFileFormat, ex.Message);
                return false;
            }
        }

        return true;
    }

    private void UpdateListView()
    {
        _fileListView.BeginUpdate();
        try
        {
            _fileListView.Items.Clear();
            foreach (string file in _droppedFilesList)
            {
                ImageList iconCache = IsCardLayout ? _previewIconCache : _iconCache;
                string? imageKey = ResolveListItemImageKey(file, iconCache);

                bool valid = IsValidFile(file, out string reason);
                var item = new ListViewItem(Path.GetFileName(file), imageKey)
                {
                    Tag = file,
                    ToolTipText = valid ? file : string.Format(_strings.SkippedFileToolTipFormat, file, _strings.SkippedFileToolTipNewLine, reason)
                };
                if (!valid)
                {
                    item.ForeColor = Color.Gray;
                }

                _fileListView.Items.Add(item);
            }

            bool showClassicClear = ShowClearButton && !IsCardLayout && _droppedFilesList.Count > 0;
            _clearButton.Visible = showClassicClear;
            _submitButton.Enabled = _droppedFilesList.Count > 0;
        }
        finally
        {
            _fileListView.EndUpdate();
        }
        UpdateStatusLabel();
        UpdateQuotaDisplay();
    }

    private void UpdateStatusLabel()
    {
        if (_droppedFilesList.Count == 0)
        {
            _statusLabel.Text = _strings.StatusEmpty;
            return;
        }

        int invalidCount = _droppedFilesList.Count(f => !IsValidFile(f, out _));
        int validCount = _droppedFilesList.Count - invalidCount;
        long totalSize = _droppedFilesList.Sum(GetFileSizeSafe);

        _statusLabel.Text = invalidCount > 0
            ? string.Format(_strings.StatusWithInvalidFormat, _droppedFilesList.Count, validCount, invalidCount, FormatFileSize(totalSize))
            : string.Format(_strings.StatusFormat, _droppedFilesList.Count, FormatFileSize(totalSize));
    }

    private void UpdateQuotaDisplay()
    {
        bool show = _behavior.ShowUploadQuotaProgressBar && _behavior.UploadSizeQuota > 0;
        _quotaLabel.Visible = show;
        _quotaProgressBar.Visible = show;
        if (show)
        {
            long used = GetTotalDroppedSize();
            long remaining = Math.Max(0, _behavior.UploadSizeQuota - used);
            _quotaLabel.Text = string.Format(_strings.UploadQuotaFormat, FormatFileSize(used), FormatFileSize(_behavior.UploadSizeQuota), FormatFileSize(remaining));
            _quotaProgressBar.Value = (int)Math.Min(100, used * 100L / _behavior.UploadSizeQuota);
        }

        if (!_isDragHoverActive)
        {
            RefreshIdleDropZoneAppearance(instant: !_animation.Enabled);
        }
    }

    #region Drop Zone Animation

    private void BeginDragHoverAnimation()
    {
        _isDragHoverActive = true;
        DropZoneAnimationScenario scenario = GetDragHoverScenario();
        SetAnimationScenario(scenario);
        AnimateDropZoneColor(GetScenarioColor(scenario), _animation.Duration, null);
        StartStripeAnimation();
    }

    private void EndDragHoverAnimation()
    {
        if (!_isDragHoverActive)
        {
            return;
        }

        _isDragHoverActive = false;
        StopStripeAnimation();
        RefreshIdleDropZoneAppearance(instant: !_animation.Enabled);
    }

    private void PlayDropResultAnimation(int acceptedCount, int rejectedCount)
    {
        if (acceptedCount == 0 && rejectedCount == 0)
        {
            return;
        }

        DropZoneAnimationScenario scenario;
        if (acceptedCount > 0 && rejectedCount == 0)
        {
            scenario = DropZoneAnimationScenario.DropSuccess;
        }
        else if (acceptedCount == 0)
        {
            scenario = DropZoneAnimationScenario.DropRejected;
        }
        else
        {
            scenario = DropZoneAnimationScenario.DropPartial;
        }

        FlashDropZone(scenario);
    }

    private void FlashDropZone(DropZoneAnimationScenario scenario)
    {
        SetAnimationScenario(scenario);
        int halfDuration = Math.Max(0, _animation.FlashDuration / 2);
        AnimateDropZoneColor(GetScenarioColor(scenario), halfDuration, () =>
        {
            if (!_isDragHoverActive)
            {
                RefreshIdleDropZoneAppearance(instant: !_animation.Enabled);
            }
        });
    }

    private void RefreshIdleDropZoneAppearance(bool instant)
    {
        DropZoneAnimationScenario scenario = GetIdleScenario();
        SetAnimationScenario(scenario);
        Color target = GetScenarioColor(scenario);
        if (instant)
        {
            SetDropZonePanelColor(target);
        }
        else
        {
            AnimateDropZoneColor(target, _animation.Duration, null);
        }
    }

    private DropZoneAnimationScenario GetIdleScenario()
    {
        if (_behavior.UploadSizeQuota > 0)
        {
            if (RemainingUploadSize <= 0)
            {
                return DropZoneAnimationScenario.QuotaExceeded;
            }

            if (GetQuotaUsagePercent() >= _animation.QuotaWarningThresholdPercent)
            {
                return DropZoneAnimationScenario.QuotaWarning;
            }
        }

        return DropZoneAnimationScenario.Idle;
    }

    private DropZoneAnimationScenario GetDragHoverScenario()
    {
        if (_isDragHoverInvalid)
        {
            return DropZoneAnimationScenario.DropRejected;
        }

        if (_behavior.UploadSizeQuota > 0)
        {
            if (RemainingUploadSize <= 0)
            {
                return DropZoneAnimationScenario.QuotaExceeded;
            }

            if (GetQuotaUsagePercent() >= _animation.QuotaWarningThresholdPercent)
            {
                return DropZoneAnimationScenario.QuotaWarning;
            }
        }

        return DropZoneAnimationScenario.DragHover;
    }

    private Color GetScenarioColor(DropZoneAnimationScenario scenario) => scenario switch
    {
        DropZoneAnimationScenario.DragHover => _animation.DragHoverColor,
        DropZoneAnimationScenario.DropSuccess => _animation.DropSuccessColor,
        DropZoneAnimationScenario.DropRejected => _animation.DropRejectedColor,
        DropZoneAnimationScenario.DropPartial => _animation.DropPartialColor,
        DropZoneAnimationScenario.QuotaWarning => _animation.QuotaWarningColor,
        DropZoneAnimationScenario.QuotaExceeded => _animation.QuotaExceededColor,
        _ => _animation.IdleColor
    };

    private void SetAnimationScenario(DropZoneAnimationScenario scenario)
    {
        if (_currentAnimationScenario == scenario)
        {
            return;
        }

        _currentAnimationScenario = scenario;
        OnPropertyChanged(nameof(CurrentAnimationScenario));
    }

    private int GetQuotaUsagePercent()
    {
        if (_behavior.UploadSizeQuota <= 0)
        {
            return 0;
        }

        return (int)Math.Min(100, TotalDroppedSize * 100L / _behavior.UploadSizeQuota);
    }

    private void AnimateDropZoneColor(Color targetColor, int durationMs, Action? onComplete)
    {
        if (!_animation.Enabled || durationMs <= 0)
        {
            SetDropZonePanelColor(targetColor);
            onComplete?.Invoke();
            return;
        }

        StopDropZoneAnimation();
        _animationStartColor = GetDropZonePanelColor();
        _animationTargetColor = targetColor;
        _animationStartTime = DateTime.Now;
        _animationDurationMs = durationMs;
        _animationCompleteCallback = onComplete;
        _animationTimer = new Timer { Interval = 16 };
        _animationTimer.Tick += OnDropZoneAnimationTick;
        _animationTimer.Start();
    }

    private void OnDropZoneAnimationTick(object? sender, EventArgs e)
    {
        double elapsed = (DateTime.Now - _animationStartTime).TotalMilliseconds;
        double progress = Math.Min(1.0, elapsed / _animationDurationMs);
        double eased = 1 - Math.Pow(1 - progress, 3);
        SetDropZonePanelColor(InterpolateColor(_animationStartColor, _animationTargetColor, eased));

        if (progress < 1.0)
        {
            return;
        }

        Action? callback = _animationCompleteCallback;
        StopDropZoneAnimation();
        callback?.Invoke();
    }

    private void StopDropZoneAnimation()
    {
        if (_animationTimer == null)
        {
            return;
        }

        _animationTimer.Tick -= OnDropZoneAnimationTick;
        _animationTimer.Stop();
        _animationTimer.Dispose();
        _animationTimer = null;
        _animationCompleteCallback = null;
    }

    private Color GetDropZonePanelColor() =>
        IsCardLayout ? _dropZoneOverlayColor : _dropzonePanel.StateCommon.Color1;

    private void SetDropZonePanelColor(Color color)
    {
        if (IsCardLayout)
        {
            _dropZoneOverlayColor = color;
            if (_appearance.UsePaletteColors && _currentAnimationScenario == DropZoneAnimationScenario.Idle)
            {
                _dropZoneOverlayColor = Color.Transparent;
            }

            _dropzonePanel.Invalidate();
            return;
        }

        _dropzonePanel.StateCommon.Color1 = color;
        _dropzonePanel.StateCommon.Color2 = color;
        _dropzonePanel.Invalidate();
    }

    private static Color InterpolateColor(Color from, Color to, double amount)
    {
        int r = (int)(from.R + ((to.R - from.R) * amount));
        int g = (int)(from.G + ((to.G - from.G) * amount));
        int b = (int)(from.B + ((to.B - from.B) * amount));
        return Color.FromArgb(from.A, r, g, b);
    }

    #endregion

    private void HandleListReorder(DragEventArgs e)
    {
        Point clientPoint = _fileListView.PointToClient(new Point(e.X, e.Y));
        if (_fileListView.GetItemAt(clientPoint.X, clientPoint.Y) is not { } targetItem)
        {
            return;
        }

        if (_fileListView.SelectedItems.Count == 0)
        {
            return;
        }

        var draggedItem = _fileListView.SelectedItems[0];
        if (draggedItem == targetItem)
        {
            return;
        }

        PushUndoState();

        int draggedIndex = draggedItem.Index;
        int targetIndex = targetItem.Index;
        string moved = _droppedFilesList[draggedIndex];
        _droppedFilesList.RemoveAt(draggedIndex);
        _droppedFilesList.Insert(targetIndex, moved);

        UpdateListView();
        _fileListView.Items[targetIndex].Selected = true;
        _fileListView.Items[targetIndex].Focused = true;
    }

    private void SortFiles(FileSortMode mode)
    {
        if (_currentSortMode == mode)
        {
            _sortAscending = !_sortAscending;
        }
        else { _currentSortMode = mode; _sortAscending = true; }

        Comparison<string> comparison = mode switch
        {
            FileSortMode.Size => (a, b) => GetFileSizeSafe(a).CompareTo(GetFileSizeSafe(b)),
            FileSortMode.Date => (a, b) => GetFileDateSafe(a).CompareTo(GetFileDateSafe(b)),
            FileSortMode.Extension => (a, b) => string.Compare(Path.GetExtension(a), Path.GetExtension(b), StringComparison.OrdinalIgnoreCase),
            _ => (a, b) => string.Compare(Path.GetFileName(a), Path.GetFileName(b), StringComparison.OrdinalIgnoreCase)
        };

        _droppedFilesList.Sort(comparison);
        if (!_sortAscending)
        {
            _droppedFilesList.Reverse();
        }

        UpdateListView();
    }

    #endregion

    #region Undo

    private void PushUndoState()
    {
        if (!EnableUndo)
        {
            return;
        }

        if (_undoStack.Count >= MaxUndoLevels)
        {
            _undoStack.RemoveAt(0);
        }

        _undoStack.Add(new List<string>(_droppedFilesList));
    }

    public void Undo()
    {
        if (!EnableUndo || _undoStack.Count == 0)
        {
            return;
        }

        var previous = _undoStack[_undoStack.Count - 1];
        _undoStack.RemoveAt(_undoStack.Count - 1);
        _droppedFilesList.Clear();
        _droppedFilesList.AddRange(previous);
        UpdateListView();
    }

    #endregion

    #region Context Menu Actions

    private void OpenSelectedFile()
    {
        if (_fileListView.SelectedItems.Count != 1)
        {
            return;
        }

        string? path = _fileListView.SelectedItems[0].Tag as string;
        if (string.IsNullOrEmpty(path) || !File.Exists(path))
        {
            return;
        }

        try { Process.Start(new ProcessStartInfo(path) { UseShellExecute = true }); }
        catch (Exception ex) { MessageBox.Show(string.Format(_strings.UnableToOpenFileFormat, ex.Message), _strings.ErrorDialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning); }
    }

    private void OpenContainingFolder()
    {
        if (_fileListView.SelectedItems.Count != 1)
        {
            return;
        }

        string? path = _fileListView.SelectedItems[0].Tag as string;
        if (string.IsNullOrEmpty(path) || !File.Exists(path))
        {
            return;
        }

        try { Process.Start("explorer.exe", $"/select,\"{path}\""); }
        catch (Exception ex) { MessageBox.Show(string.Format(_strings.UnableToOpenFolderFormat, ex.Message), _strings.ErrorDialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning); }
    }

    private void CopySelectedPaths()
    {
        if (_fileListView.SelectedItems.Count == 0)
        {
            return;
        }

        var paths = _fileListView.SelectedItems.Cast<ListViewItem>()
            .Select(i => i.Tag as string)
            .Where(p => !string.IsNullOrEmpty(p));
        try { Clipboard.SetText(string.Join(Environment.NewLine, paths)); }
        catch (ExternalException) { /* clipboard locked by another process */ }
    }

    #endregion

    #region Public Methods

    public void ClearFiles()
    {
        if (_droppedFilesList.Count == 0)
        {
            return;
        }

        PushUndoState();
        _droppedFilesList.Clear();
        UpdateListView();
        FilesCleared?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveSelectedFiles()
    {
        if (_fileListView.SelectedItems.Count == 0)
        {
            return;
        }

        PushUndoState();
        var indices = _fileListView.SelectedIndices.Cast<int>().OrderByDescending(i => i).ToList();
        foreach (int index in indices) _droppedFilesList.RemoveAt(index);
        UpdateListView();
    }

    public void AddFiles(IEnumerable<string> filePaths)
    {
        if (filePaths == null)
        {
            return;
        }

        ProcessIncomingFiles(filePaths.Where(File.Exists).Distinct().ToList());
    }

    /// <summary>
    /// Refreshes Card/Classic chrome and list-view rendering (tile/details + icons) without processing drops.
    /// </summary>
    public void RefreshLayout()
    {
        ApplyLayout();
        UpdateListView();
        CenterDropZoneContent();

        _fileListView.Invalidate();
        _controlsPanel.Invalidate();
    }

    public void SaveToFile(string path) => File.WriteAllLines(path, _droppedFilesList);

    public void LoadFromFile(string path)
    {
        if (!File.Exists(path))
        {
            return;
        }

        PushUndoState();
        _droppedFilesList.Clear();
        _droppedFilesList.AddRange(File.ReadAllLines(path).Where(File.Exists));
        UpdateListView();
    }

    public IEnumerable<string> GetFilesByExtension(string extension) =>
        _droppedFilesList.Where(f => string.Equals(Path.GetExtension(f), extension, StringComparison.OrdinalIgnoreCase));

    #endregion

    #region Helpers

    internal IReadOnlyList<string> GetDroppedFilesSnapshot() => _droppedFilesList.AsReadOnly();

    internal int GetDroppedFileCount() => _droppedFilesList.Count;

    internal long GetTotalDroppedSize() => _droppedFilesList.Sum(GetFileSizeSafe);

    internal long GetRemainingUploadSize() => _behavior.UploadSizeQuota > 0
        ? Math.Max(0, _behavior.UploadSizeQuota - GetTotalDroppedSize())
        : long.MaxValue;

    internal string? GetSelectedFilePath()
    {
        if (_fileListView.SelectedItems.Count == 0)
        {
            return null;
        }

        return _fileListView.SelectedItems[0].Tag as string;
    }

    internal IReadOnlyList<string> GetSelectedFilePaths() => GetSelectedFiles();

    internal bool GetCanUndo() => _undoStack.Count > 0;

    internal DropZoneAnimationScenario GetCurrentAnimationScenario() => _currentAnimationScenario;

    private string? ResolveListItemImageKey(string path, ImageList iconCache)
    {
        if (!_appearance.ShowFileListIcons)
        {
            return null;
        }

        bool largeIcon = IsCardLayout;
        string cacheKey = Directory.Exists(path)
            ? $"__folder__:{path}"
            : Path.GetExtension(path).ToLowerInvariant();

        if (string.IsNullOrEmpty(cacheKey))
        {
            cacheKey = path;
        }

        if (!iconCache.Images.ContainsKey(cacheKey))
        {
            Icon? icon = FileSystemIconHelper.GetFileSystemIcon(path, largeIcon);

            if (icon != null)
            {
                try
                {
                    iconCache.Images.Add(cacheKey, icon.ToBitmap());
                }
                finally
                {
                    icon.Dispose();
                }
            }
        }

        return iconCache.Images.ContainsKey(cacheKey) ? cacheKey : null;
    }

    private IReadOnlyList<string> GetSelectedFiles()
    {
        if (_fileListView.SelectedItems.Count == 0)
        {
            return Array.Empty<string>();
        }

        var paths = new List<string>(_fileListView.SelectedItems.Count);
        foreach (ListViewItem item in _fileListView.SelectedItems)
        {
            if (item.Tag is string path && !string.IsNullOrEmpty(path))
            {
                paths.Add(path);
            }
        }

        return paths.AsReadOnly();
    }

    private static long GetFileSizeSafe(string path)
    {
        try { return new FileInfo(path).Length; } catch { return 0; }
    }

    private static DateTime GetFileDateSafe(string path)
    {
        try { return File.GetLastWriteTime(path); } catch { return DateTime.MinValue; }
    }

    private string FormatFileSize(long bytes)
    {
        string[] units = _strings.GetFileSizeUnits();
        double size = bytes;
        int unitIndex = 0;
        while (size >= 1024 && unitIndex < units.Length - 1) { size /= 1024; unitIndex++; }
        return $"{size:0.#} {units[unitIndex]}";
    }

    private void OnGlobalPaletteChanged(object? sender, EventArgs e)
    {
        if (!IsCardLayout)
        {
            return;
        }

        ApplyCardThemeColors();
        UpdateUploadIcon();
        _dropzonePanel.Invalidate();
    }

    private void ApplyCardThemeColors()
    {
        Color panelColor = _appearance.UsePaletteColors ? GetThemedPanelBackColor() : _animation.IdleColor;
        StateCommon.Color1 = panelColor;
        StateCommon.Color2 = panelColor;
        _controlsPanel.StateCommon.Color1 = panelColor;
        _controlsPanel.StateCommon.Color2 = panelColor;
    }

    private PaletteBase GetPalette() => KryptonManager.CurrentGlobalPalette;

    private Color GetThemedPanelBackColor() =>
        GetPalette().GetBackColor1(PaletteBackStyle.PanelClient, PaletteState.Normal);

    private Color GetThemedBorderColor() =>
        GetPalette().GetBorderColor1(PaletteBorderStyle.InputControlStandalone, PaletteState.Normal);

    private Color GetThemedContentColor() =>
        GetPalette().GetContentShortTextColor1(PaletteContentStyle.LabelNormalControl, PaletteState.Normal);

    private Color GetThemedStripeColor() =>
        GetPalette().GetBorderColor1(PaletteBorderStyle.GridDataCellList, PaletteState.Normal);

    private Color GetThemedErrorStripeColor() =>
        GetPalette().GetBorderColor1(PaletteBorderStyle.ButtonStandalone, PaletteState.Disabled);

    private void DropzonePanel_Paint(object? sender, PaintEventArgs e)
    {
        if (!IsCardLayout)
        {
            return;
        }

        Graphics g = e.Graphics;
        Rectangle rect = _dropzonePanel.ClientRectangle;
        rect.Inflate(-1, -1);

        Color baseColor = _appearance.UsePaletteColors ? GetThemedPanelBackColor() : _animation.IdleColor;
        using (var brush = new SolidBrush(baseColor))
        {
            g.FillRectangle(brush, rect);
        }

        if (_isDragHoverActive && _appearance.ShowStripedDragFeedback)
        {
            Color stripeColor = _isDragHoverInvalid
                ? (_appearance.UsePaletteColors ? GetThemedErrorStripeColor() : _animation.DropRejectedColor)
                : (_appearance.UsePaletteColors ? GetThemedStripeColor() : Color.LightGray);
            DrawDiagonalStripes(g, rect, stripeColor, Color.White, _stripeOffset, _appearance.StripeWidth);
        }

        if (_dropZoneOverlayColor.A > 0)
        {
            Color overlay = _isDragHoverActive && _appearance.ShowStripedDragFeedback
                ? Color.FromArgb(48, _dropZoneOverlayColor)
                : _dropZoneOverlayColor;
            using var overlayBrush = new SolidBrush(overlay);
            g.FillRectangle(overlayBrush, rect);
        }

        Color borderColor = _appearance.UsePaletteColors ? GetThemedBorderColor() : Color.Gray;
        using var pen = new Pen(borderColor, 1) { DashStyle = DashStyle.Dash };
        g.DrawRectangle(pen, rect);
    }

    private static void DrawDiagonalStripes(Graphics g, Rectangle bounds, Color stripeColor, Color gapColor, int offset, int stripeWidth)
    {
        using var clip = new Region(bounds);
        g.SetClip(clip, CombineMode.Intersect);

        using var gapBrush = new SolidBrush(gapColor);
        g.FillRectangle(gapBrush, bounds);

        int step = Math.Max(4, stripeWidth);
        using var stripePen = new Pen(stripeColor, step);
        for (int x = bounds.Left - bounds.Height + offset; x < bounds.Right + bounds.Height; x += step * 2)
        {
            g.DrawLine(stripePen, x, bounds.Bottom, x + bounds.Height, bounds.Top);
        }

        g.ResetClip();
    }

    private void StartStripeAnimation()
    {
        if (!IsCardLayout || !_appearance.ShowStripedDragFeedback)
        {
            return;
        }

        StopStripeAnimation();
        _stripeTimer = new Timer { Interval = _appearance.StripeAnimationInterval };
        _stripeTimer.Tick += OnStripeAnimationTick;
        _stripeTimer.Start();
    }

    private void OnStripeAnimationTick(object? sender, EventArgs e)
    {
        _stripeOffset = (_stripeOffset + 3) % (_appearance.StripeWidth * 4);
        _dropzonePanel.Invalidate();
    }

    private void StopStripeAnimation()
    {
        if (_stripeTimer == null)
        {
            return;
        }

        _stripeTimer.Tick -= OnStripeAnimationTick;
        _stripeTimer.Stop();
        _stripeTimer.Dispose();
        _stripeTimer = null;
        _stripeOffset = 0;
    }

    private void UpdateDragHoverInvalidState(DragEventArgs e)
    {
        bool wasInvalid = _isDragHoverInvalid;
        if (!e.Data!.GetDataPresent(DataFormats.FileDrop))
        {
            _isDragHoverInvalid = true;
        }
        else if (e.Data.GetData(DataFormats.FileDrop) is string[] paths)
        {
            _isDragHoverInvalid = !WouldAcceptAnyDraggedContent(paths);
        }
        else
        {
            _isDragHoverInvalid = true;
        }

        if (wasInvalid == _isDragHoverInvalid)
        {
            return;
        }

        if (_isDragHoverActive)
        {
            DropZoneAnimationScenario scenario = GetDragHoverScenario();
            SetAnimationScenario(scenario);
            AnimateDropZoneColor(GetScenarioColor(scenario), _animation.Duration, null);
            _dropzonePanel.Invalidate();
        }
    }

    private bool WouldAcceptAnyDraggedContent(string[] droppedPaths)
    {
        List<string> files = ExpandDraggedPaths(droppedPaths);
        if (files.Count == 0)
        {
            return AllowDirectories && droppedPaths.Any(Directory.Exists);
        }

        foreach (string file in files)
        {
            if (!IsValidFile(file, out _))
            {
                continue;
            }

            if (MaxFileCount > 0 && _droppedFilesList.Count >= MaxFileCount)
            {
                continue;
            }

            if (_behavior.UploadSizeQuota > 0)
            {
                long fileSize = GetFileSizeSafe(file);
                if (GetTotalDroppedSize() + fileSize > _behavior.UploadSizeQuota)
                {
                    continue;
                }
            }

            return true;
        }

        return false;
    }

    private List<string> ExpandDraggedPaths(string[] droppedPaths)
    {
        var results = new List<string>();
        var searchOption = SearchSubdirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
        foreach (string path in droppedPaths)
        {
            if (Directory.Exists(path) && AllowDirectories)
            {
                try { results.AddRange(Directory.GetFiles(path, "*.*", searchOption)); }
                catch (UnauthorizedAccessException) { /* skip inaccessible folders */ }
            }
            else if (File.Exists(path))
            {
                results.Add(path);
            }
        }

        return results.Distinct().ToList();
    }

    private void CenterDropZoneContent()
    {
        if (!IsCardLayout || _dropzonePanel.ClientSize.Width <= 0)
        {
            return;
        }

        int labelWidth = Math.Max(120, _dropzonePanel.ClientSize.Width - 48);
        Size labelSize = _dropZoneLabel.GetPreferredSize(new Size(labelWidth, 200));
        int labelHeight = Math.Max(20, labelSize.Height);

        int iconHeight = _uploadIcon.Visible ? _uploadIcon.Height : 0;
        int gap = _uploadIcon.Visible ? 12 : 0;
        int totalHeight = iconHeight + gap + labelHeight;
        int y = Math.Max(0, (_dropzonePanel.ClientSize.Height - totalHeight) / 2);

        if (_uploadIcon.Visible)
        {
            _uploadIcon.Location = new Point((_dropzonePanel.ClientSize.Width - _uploadIcon.Width) / 2, y);
            y += iconHeight + gap;
        }

        _dropZoneLabel.Location = new Point((_dropzonePanel.ClientSize.Width - labelWidth) / 2, y);
        _dropZoneLabel.Size = new Size(labelWidth, labelHeight);

        _uploadIcon.BringToFront();
        _dropZoneLabel.BringToFront();
    }

    private void UpdateUploadIcon()
    {
        if (!IsCardLayout || !_appearance.ShowUploadIcon)
        {
            return;
        }

        Color iconColor = _appearance.UsePaletteColors ? GetThemedContentColor() : Color.Gray;
        DisposeRenderedUploadIconImage();

        Image sourceIcon = _appearance.UploadIcon ?? DropZoneResources.UploadDocument;
        bool isDefaultIcon = _appearance.UploadIcon == null;
        _renderedUploadIconImage = isDefaultIcon || !_appearance.UsePaletteColors
            ? CreateScaledIconImage(sourceIcon, _appearance.UploadIconSize)
            : CreateTintedIconImage(sourceIcon, iconColor, _appearance.UploadIconSize);
        _uploadIcon.Image = _renderedUploadIconImage;
        CenterDropZoneContent();
    }

    private void DisposeRenderedUploadIconImage()
    {
        if (_renderedUploadIconImage == null)
        {
            return;
        }

        _renderedUploadIconImage.Dispose();
        _renderedUploadIconImage = null;
    }

    private static Bitmap CreateScaledIconImage(Image source, int size)
    {
        var bitmap = new Bitmap(size, size, PixelFormat.Format32bppArgb);
        using Graphics g = Graphics.FromImage(bitmap);
        g.Clear(Color.Transparent);
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        g.PixelOffsetMode = PixelOffsetMode.HighQuality;

        float scale = Math.Min((float)size / source.Width, (float)size / source.Height);
        int width = Math.Max(1, (int)(source.Width * scale));
        int height = Math.Max(1, (int)(source.Height * scale));
        int x = (size - width) / 2;
        int y = (size - height) / 2;
        g.DrawImage(source, new Rectangle(x, y, width, height));

        return bitmap;
    }

    private static Bitmap CreateTintedIconImage(Image source, Color tint, int size)
    {
        var bitmap = new Bitmap(size, size, PixelFormat.Format32bppArgb);
        using Graphics g = Graphics.FromImage(bitmap);
        g.Clear(Color.Transparent);
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        g.PixelOffsetMode = PixelOffsetMode.HighQuality;

        var dest = new Rectangle(0, 0, size, size);
        using var attributes = new ImageAttributes();
        attributes.SetColorMatrix(CreateIconTintMatrix(tint), ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
        g.DrawImage(source, dest, 0, 0, source.Width, source.Height, GraphicsUnit.Pixel, attributes);

        return bitmap;
    }

    private static ColorMatrix CreateIconTintMatrix(Color tint)
    {
        float r = tint.R / 255f;
        float g = tint.G / 255f;
        float b = tint.B / 255f;

        return new ColorMatrix(new float[][]
        {
            [0.299f * r, 0.299f * g, 0.299f * b, 0, 0],
            [0.587f * r, 0.587f * g, 0.587f * b, 0, 0],
            [0.114f * r, 0.114f * g, 0.114f * b, 0, 0],
            [0, 0, 0, 1, 0],
            [0, 0, 0, 0, 1]
        });
    }

    protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    #endregion
}

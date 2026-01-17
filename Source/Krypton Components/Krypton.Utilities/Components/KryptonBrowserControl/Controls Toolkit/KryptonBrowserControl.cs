#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

/// <summary>
/// Provides a composite control for browsing the file system with a tree view and list view.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonPanel), "ToolboxBitmaps.KryptonPanel.bmp")]
[DefaultEvent(nameof(PathChanged))]
[DefaultProperty(nameof(CurrentPath))]
[DesignerCategory(@"code")]
[Description(@"A composite control for browsing the file system with tree and list views.")]
[Docking(DockingBehavior.Ask)]
public class KryptonBrowserControl : KryptonPanel
{
    #region Instance Fields

    private KryptonSplitContainer? _splitContainer;
    private KryptonFileSystemTreeView? _treeView;
    private KryptonFileSystemListView? _listView;
    private KryptonSplitterPanel? _treeViewPanel;
    private KryptonSplitterPanel? _listViewPanel;

    private string _currentPath = string.Empty;
    private View _viewMode = View.Details;
    private bool _showFiles = true;
    private bool _showHiddenFiles = false;
    private bool _showSystemFiles = false;
    private string _fileFilter = "*.*";
    private SelectionMode _selectionMode = SelectionMode.One;
    private bool _showTreeView = true;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when the current path changes.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when the current path changes.")]
    public event EventHandler? PathChanged;

    /// <summary>
    /// Occurs when the file selection changes.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when the file selection changes.")]
    public event EventHandler? SelectionChanged;

    /// <summary>
    /// Occurs when a file system error occurs.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when a file system error occurs.")]
    public event EventHandler<FileSystemErrorEventArgs>? FileSystemError;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the KryptonBrowserControl class.
    /// </summary>
    public KryptonBrowserControl()
    {
        InitializeComponent();
        SetupControls();
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets or sets the current directory path.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The current directory path being displayed.")]
    [DefaultValue("")]
    public string CurrentPath
    {
        get => _currentPath;
        set
        {
            if (_currentPath != value)
            {
                NavigateTo(value);
            }
        }
    }

    /// <summary>
    /// Gets or sets the view mode for the list view.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The view mode for the list view.")]
    [DefaultValue(View.Details)]
    public View ViewMode
    {
        get => _viewMode;
        set
        {
            if (_viewMode != value)
            {
                _viewMode = value;
                if (_listView != null)
                {
                    _listView.View = value;
                }
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether files are displayed.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether files are displayed.")]
    [DefaultValue(true)]
    public bool ShowFiles
    {
        get => _showFiles;
        set
        {
            if (_showFiles != value)
            {
                _showFiles = value;
                if (_treeView != null)
                {
                    _treeView.FileSystemTreeViewValues.ShowFiles = value;
                }

                if (_listView != null)
                {
                    _listView.FileSystemListViewValues.ShowFiles = value;
                }

                RefreshListView();
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether hidden files are displayed.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether hidden files are displayed.")]
    [DefaultValue(false)]
    public bool ShowHiddenFiles
    {
        get => _showHiddenFiles;
        set
        {
            if (_showHiddenFiles != value)
            {
                _showHiddenFiles = value;
                if (_treeView != null)
                {
                    _treeView.FileSystemTreeViewValues.ShowHiddenFiles = value;
                }

                if (_listView != null)
                {
                    _listView.FileSystemListViewValues.ShowHiddenFiles = value;
                }

                RefreshListView();
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether system files are displayed.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether system files are displayed.")]
    [DefaultValue(false)]
    public bool ShowSystemFiles
    {
        get => _showSystemFiles;
        set
        {
            if (_showSystemFiles != value)
            {
                _showSystemFiles = value;
                if (_treeView != null)
                {
                    _treeView.FileSystemTreeViewValues.ShowSystemFiles = value;
                }

                if (_listView != null)
                {
                    _listView.FileSystemListViewValues.ShowSystemFiles = value;
                }

                RefreshListView();
            }
        }
    }

    /// <summary>
    /// Gets or sets the file filter pattern (e.g., "*.txt" or "*.txt;*.doc").
    /// </summary>
    [Category(@"Behavior")]
    [Description("The file filter pattern (e.g., \"*.txt\" or \"*.txt;*.doc\").")]
    [DefaultValue("*.*")]
    public string FileFilter
    {
        get => _fileFilter;
        set
        {
            if (_fileFilter != value)
            {
                _fileFilter = value ?? "*.*";
                if (_treeView != null)
                {
                    _treeView.FileSystemTreeViewValues.FileFilter = _fileFilter;
                }

                if (_listView != null)
                {
                    _listView.FileSystemListViewValues.FileFilter = _fileFilter;
                }

                RefreshListView();
            }
        }
    }

    /// <summary>
    /// Gets or sets the selection mode for the list view.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The selection mode for the list view.")]
    [DefaultValue(SelectionMode.One)]
    public SelectionMode SelectionMode
    {
        get => _selectionMode;
        set
        {
            if (_selectionMode != value)
            {
                _selectionMode = value;
                if (_listView != null)
                {
                    _listView.MultiSelect = value != SelectionMode.One;
                }
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the tree view is visible.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Indicates whether the tree view is visible.")]
    [DefaultValue(true)]
    public bool ShowTreeView
    {
        get => _showTreeView;
        set
        {
            if (_showTreeView != value)
            {
                _showTreeView = value;
                if (_splitContainer != null)
                {
                    _splitContainer.Panel1Collapsed = !value;
                }
            }
        }
    }

    /// <summary>
    /// Gets the tree view control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonFileSystemTreeView? TreeView => _treeView;

    /// <summary>
    /// Gets the list view control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonFileSystemListView? ListView => _listView;

    /// <summary>
    /// Gets the tree view panel.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonSplitterPanel? TreeViewPanel => _treeViewPanel;

    /// <summary>
    /// Gets the list view panel.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonSplitterPanel? ListViewPanel => _listViewPanel;

    /// <summary>
    /// Gets the currently selected file path.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string? SelectedPath => _listView?.FileSystemListViewValues.SelectedPath;

    /// <summary>
    /// Gets an array of selected file paths.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string[] SelectedPaths => _listView?.FileSystemListViewValues.SelectedPaths ?? Array.Empty<string>();

    #endregion

    #region Visual State Properties

    /// <summary>
    /// Gets access to the common tree view panel appearance that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common TreeView panel appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack? TreeViewPanelStateCommon => _treeViewPanel?.StateCommon;

    private bool ShouldSerializeTreeViewPanelStateCommon() => TreeViewPanelStateCommon is { IsDefault: false };

    /// <summary>
    /// Gets access to the disabled tree view panel appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled TreeView panel appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack? TreeViewPanelStateDisabled => _treeViewPanel?.StateDisabled;

    private bool ShouldSerializeTreeViewPanelStateDisabled() => TreeViewPanelStateDisabled is { IsDefault: false };

    /// <summary>
    /// Gets access to the normal tree view panel appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal TreeView panel appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack? TreeViewPanelStateNormal => _treeViewPanel?.StateNormal;

    private bool ShouldSerializeTreeViewPanelStateNormal() => TreeViewPanelStateNormal is { IsDefault: false };

    /// <summary>
    /// Gets access to the common list view panel appearance that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common ListView panel appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack? ListViewPanelStateCommon => _listViewPanel?.StateCommon;

    private bool ShouldSerializeListViewPanelStateCommon() => ListViewPanelStateCommon is { IsDefault: false };

    /// <summary>
    /// Gets access to the disabled list view panel appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled ListView panel appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack? ListViewPanelStateDisabled => _listViewPanel?.StateDisabled;

    private bool ShouldSerializeListViewPanelStateDisabled() => ListViewPanelStateDisabled is { IsDefault: false };

    /// <summary>
    /// Gets access to the normal list view panel appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal ListView panel appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack? ListViewPanelStateNormal => _listViewPanel?.StateNormal;

    private bool ShouldSerializeListViewPanelStateNormal() => ListViewPanelStateNormal is { IsDefault: false };

    #endregion

    #region Public Methods

    /// <summary>
    /// Navigates to the specified path.
    /// </summary>
    /// <param name="path">The path to navigate to.</param>
    public void NavigateTo(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            return;
        }

        if (!Directory.Exists(path))
        {
            OnFileSystemError(new FileSystemErrorEventArgs(path,
                new DirectoryNotFoundException($"Directory not found: {path}")));
            return;
        }

        _currentPath = path;

        // Update tree view
        if (_treeView != null)
        {
            _treeView.FileSystemTreeViewValues.RootPath = path;
            _treeView.NavigateToPath(path);
        }

        // Update list view
        if (_listView != null)
        {
            _listView.FileSystemListViewValues.CurrentPath = path;
        }
        else
        {
            RefreshListView();
        }

        OnPathChanged(EventArgs.Empty);
    }

    /// <summary>
    /// Refreshes the current view.
    /// </summary>
    public new void Refresh()
    {
        if (_treeView != null)
        {
            _treeView.Reload();
        }

        RefreshListView();
        base.Refresh();
    }

    #endregion

    #region Protected Virtual

    /// <summary>
    /// Raises the PathChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnPathChanged(EventArgs e)
    {
        PathChanged?.Invoke(this, e);
    }

    /// <summary>
    /// Raises the SelectionChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnSelectionChanged(EventArgs e)
    {
        SelectionChanged?.Invoke(this, e);
    }

    /// <summary>
    /// Raises the FileSystemError event.
    /// </summary>
    /// <param name="e">A FileSystemErrorEventArgs containing the event data.</param>
    protected virtual void OnFileSystemError(FileSystemErrorEventArgs e)
    {
        FileSystemError?.Invoke(this, e);
    }

    /// <summary>
    /// Raises the EnabledChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnEnabledChanged(EventArgs e)
    {
        base.OnEnabledChanged(e);
        if (_splitContainer != null) _splitContainer.Enabled = Enabled;
        if (_treeView != null) _treeView.Enabled = Enabled;
        if (_listView != null) _listView.Enabled = Enabled;
        if (_treeViewPanel != null) _treeViewPanel.Enabled = Enabled;
        if (_listViewPanel != null) _listViewPanel.Enabled = Enabled;
    }

    #endregion

    #region Implementation

    private void InitializeComponent()
    {
        SuspendLayout();

        // Create split container
        _splitContainer = new KryptonSplitContainer
        {
            Dock = DockStyle.Fill,
            Orientation = Orientation.Vertical,
            SplitterDistance = 200,
            FixedPanel = FixedPanel.Panel1,
            Panel1MinSize = 100,
            Panel2MinSize = 100
        };

        // Get panel references
        _treeViewPanel = _splitContainer.Panel1;
        _listViewPanel = _splitContainer.Panel2;

        // Create tree view
        _treeView = new KryptonFileSystemTreeView
        {
            Dock = DockStyle.Fill
        };
        _treeView.AfterSelect += TreeView_AfterSelect;
        _treeView.FileSystemError += TreeView_FileSystemError;

        // Create list view
        _listView = new KryptonFileSystemListView
        {
            Dock = DockStyle.Fill,
            View = _viewMode
        };
        _listView.SelectedIndexChanged += ListView_SelectedIndexChanged;
        _listView.DoubleClick += ListView_DoubleClick;
        _listView.PathChanged += ListView_PathChanged;
        _listView.FileSystemError += ListView_FileSystemError;

        // Add controls to panels
        _treeViewPanel.Controls.Add(_treeView);
        _listViewPanel.Controls.Add(_listView);

        // Add split container to this control
        Controls.Add(_splitContainer);

        ResumeLayout(false);
    }

    private void SetupControls()
    {
        // Set initial path if empty
        if (string.IsNullOrEmpty(_currentPath))
        {
            _currentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        // Configure tree view
        if (_treeView != null)
        {
            _treeView.FileSystemTreeViewValues.RootPath = _currentPath;
            _treeView.FileSystemTreeViewValues.ShowFiles = _showFiles;
            _treeView.FileSystemTreeViewValues.ShowHiddenFiles = _showHiddenFiles;
            _treeView.FileSystemTreeViewValues.ShowSystemFiles = _showSystemFiles;
            _treeView.FileSystemTreeViewValues.FileFilter = _fileFilter;
        }

        // Configure list view
        if (_listView != null)
        {
            _listView.View = _viewMode;
            _listView.MultiSelect = _selectionMode != SelectionMode.One;
            _listView.FileSystemListViewValues.ShowFiles = _showFiles;
            _listView.FileSystemListViewValues.ShowHiddenFiles = _showHiddenFiles;
            _listView.FileSystemListViewValues.ShowSystemFiles = _showSystemFiles;
            _listView.FileSystemListViewValues.FileFilter = _fileFilter;
        }

        // Configure split container
        if (_splitContainer != null)
        {
            _splitContainer.Panel1Collapsed = !_showTreeView;
        }

        // Load initial directory
        RefreshListView();
    }

    private void RefreshListView()
    {
        // If using KryptonFileSystemListView, it handles its own refresh
        if (_listView is KryptonFileSystemListView)
        {
            return;
        }

        // Fallback for regular ListView (shouldn't happen now, but kept for safety)
        if (_listView == null || string.IsNullOrEmpty(_currentPath) || !Directory.Exists(_currentPath))
        {
            return;
        }

        _listView.Items.Clear();

        try
        {
            // Add parent directory item
            DirectoryInfo? parent = Directory.GetParent(_currentPath);
            if (parent != null)
            {
                ListViewItem parentItem = new ListViewItem("..")
                {
                    Tag = parent.FullName,
                    ImageIndex = 0
                };
                _listView.Items.Add(parentItem);
            }

            // Add directories
            string[] directories = Directory.GetDirectories(_currentPath);
            foreach (string dir in directories)
            {
                try
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(dir);
                    bool isHidden = (dirInfo.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden;
                    bool isSystem = (dirInfo.Attributes & FileAttributes.System) == FileAttributes.System;

                    if ((isHidden && !_showHiddenFiles) || (isSystem && !_showSystemFiles))
                    {
                        continue;
                    }

                    ListViewItem item = new ListViewItem(dirInfo.Name)
                    {
                        Tag = dirInfo.FullName,
                        ImageIndex = 0
                    };
                    item.SubItems.Add("Folder");
                    item.SubItems.Add("");
                    _listView.Items.Add(item);
                }
                catch (UnauthorizedAccessException ex)
                {
                    OnFileSystemError(new FileSystemErrorEventArgs(dir, ex));
                }
            }

            // Add files
            if (_showFiles)
            {
                string[] files = Directory.GetFiles(_currentPath, _fileFilter);
                foreach (string file in files)
                {
                    try
                    {
                        FileInfo fileInfo = new FileInfo(file);
                        bool isHidden = (fileInfo.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden;
                        bool isSystem = (fileInfo.Attributes & FileAttributes.System) == FileAttributes.System;

                        if ((isHidden && !_showHiddenFiles) || (isSystem && !_showSystemFiles))
                        {
                            continue;
                        }

                        ListViewItem item = new ListViewItem(fileInfo.Name)
                        {
                            Tag = fileInfo.FullName,
                            ImageIndex = 1
                        };
                        item.SubItems.Add("File");
                        item.SubItems.Add(fileInfo.Length.ToString("N0"));
                        _listView.Items.Add(item);
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        OnFileSystemError(new FileSystemErrorEventArgs(file, ex));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            OnFileSystemError(new FileSystemErrorEventArgs(_currentPath, ex));
        }
    }

    private void TreeView_AfterSelect(object? sender, TreeViewEventArgs e)
    {
        if (e.Node?.Tag is string path && Directory.Exists(path))
        {
            NavigateTo(path);
        }
    }

    private void TreeView_FileSystemError(object? sender, FileSystemErrorEventArgs e)
    {
        OnFileSystemError(e);
    }

    private void ListView_SelectedIndexChanged(object? sender, EventArgs e)
    {
        OnSelectionChanged(EventArgs.Empty);
    }

    private void ListView_DoubleClick(object? sender, EventArgs e)
    {
        if (_listView?.SelectedItems.Count > 0)
        {
            string? path = _listView.SelectedItems[0].Tag as string;
            if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
            {
                NavigateTo(path!);
            }
        }
    }

    private void ListView_PathChanged(object? sender, EventArgs e)
    {
        if (_listView is KryptonFileSystemListView fsListView)
        {
            string newPath = fsListView.FileSystemListViewValues.CurrentPath;
            if (_currentPath != newPath)
            {
                _currentPath = newPath;
                // Sync with tree view
                if (_treeView != null)
                {
                    _treeView.FileSystemTreeViewValues.RootPath = newPath;
                    _treeView.NavigateToPath(newPath);
                }

                OnPathChanged(EventArgs.Empty);
            }
        }
    }

    private void ListView_FileSystemError(object? sender, FileSystemErrorEventArgs e)
    {
        OnFileSystemError(e);
    }

    #endregion
}
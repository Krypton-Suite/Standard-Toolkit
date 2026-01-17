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
/// Provides a comprehensive file explorer control with navigation toolbar, address bar, and status bar.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonPanel), "ToolboxBitmaps.KryptonPanel.bmp")]
[DefaultEvent(nameof(PathChanged))]
[DefaultProperty(nameof(ExplorerValues))]
[DesignerCategory(@"code")]
[Description(@"A comprehensive file explorer control with navigation toolbar, address bar, and status bar.")]
[Docking(DockingBehavior.Ask)]
public class KryptonExplorerBrowser : KryptonPanel
{
    #region Instance Fields

    internal KryptonPanel? _toolbarPanel;
    private KryptonPanel? _contentPanel;
    internal KryptonPanel? _statusPanel;
    internal KryptonSplitContainer? _splitContainer;
    private KryptonFileSystemTreeView? _treeView;
    private KryptonFileSystemListView? _listView;
    private KryptonSplitterPanel? _treeViewPanel;
    private KryptonSplitterPanel? _listViewPanel;

    private KryptonButton? kbtnBack;
    private KryptonButton? kbtnForward;
    private KryptonButton? kbtnUp;
    private KryptonButton? kbtnRefresh;
    private KryptonButton? kbtnViewDetails;
    private KryptonButton? kbtnViewList;
    private KryptonButton? kbtnViewLargeIcons;
    private KryptonButton? kbtnViewSmallIcons;
    private KryptonTextBox? ktxtAddressBar;
    private KryptonSearchBox? _searchBox;
    private KryptonLabel? klblStatus;

    private readonly ExplorerBrowserValues _explorerValues;
    private readonly NavigationValues _navigationValues;
    private readonly DisplayValues _displayValues;

    private readonly Stack<string> _backHistory;
    private readonly Stack<string> _forwardHistory;
    internal string _currentPath = string.Empty;

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
    /// Initialize a new instance of the KryptonExplorerBrowser class.
    /// </summary>
    public KryptonExplorerBrowser()
    {
        _backHistory = new Stack<string>();
        _forwardHistory = new Stack<string>();

        _explorerValues = new ExplorerBrowserValues(this);
        _navigationValues = new NavigationValues(this);
        _displayValues = new DisplayValues(this);

        InitializeComponent();
        SetupControls();
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets access to the explorer browser values.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Groups file system explorer browser properties.")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public ExplorerBrowserValues ExplorerValues => _explorerValues;

    /// <summary>
    /// Gets access to the navigation values.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Groups navigation-related properties.")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public NavigationValues NavigationValues => _navigationValues;

    /// <summary>
    /// Gets access to the display values.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Groups display-related properties.")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public DisplayValues DisplayValues => _displayValues;

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
            OnFileSystemError(new FileSystemErrorEventArgs(path, new DirectoryNotFoundException($"Directory not found: {path}")));
            return;
        }

        // Add current path to back history
        if (!string.IsNullOrEmpty(_currentPath) && _currentPath != path)
        {
            _backHistory.Push(_currentPath);
            _forwardHistory.Clear();
            UpdateNavigationButtons();
        }

        _currentPath = path;
        _navigationValues.CurrentPath = path;

        // Update tree view
        if (_treeView != null)
        {
            _treeView.FileSystemTreeViewValues.RootPath = path;
            _treeView.NavigateToPath(path);
        }

        // Update list view
        _listView?.FileSystemListViewValues.CurrentPath = path;

        // Update address bar
        ktxtAddressBar?.Text = path;

        // Update status
        UpdateStatus();

        OnPathChanged(EventArgs.Empty);
    }

    /// <summary>
    /// Navigates back in history.
    /// </summary>
    public void NavigateBack()
    {
        if (_backHistory.Count > 0)
        {
            string path = _backHistory.Pop();
            _forwardHistory.Push(_currentPath);
            NavigateTo(path);
        }
    }

    /// <summary>
    /// Navigates forward in history.
    /// </summary>
    public void NavigateForward()
    {
        if (_forwardHistory.Count > 0)
        {
            string path = _forwardHistory.Pop();
            _backHistory.Push(_currentPath);
            NavigateTo(path);
        }
    }

    /// <summary>
    /// Navigates up one directory level.
    /// </summary>
    public void NavigateUp()
    {
        if (string.IsNullOrEmpty(_currentPath))
        {
            return;
        }

        DirectoryInfo? parent = Directory.GetParent(_currentPath);
        if (parent != null)
        {
            NavigateTo(parent.FullName);
        }
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
        if (_listView != null)
        {
            _listView.Refresh();
        }
        UpdateStatus();
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
        if (_toolbarPanel != null) _toolbarPanel.Enabled = Enabled;
        if (_statusPanel != null) _statusPanel.Enabled = Enabled;
    }

    #endregion

    #region Implementation

    private void InitializeComponent()
    {
        SuspendLayout();

        // Create toolbar panel
        _toolbarPanel = new KryptonPanel
        {
            Dock = DockStyle.Top,
            Height = 60,
            Padding = new Padding(4)
        };

        // Create navigation buttons
        kbtnBack = new KryptonButton
        {
            Text = @"←",
            Size = new Size(32, 32),
            Location = new Point(4, 4)
        };
        kbtnBack.Click += kbtnBack_Click;

        kbtnForward = new KryptonButton
        {
            Text = @"→",
            Size = new Size(32, 32),
            Location = new Point(40, 4)
        };

        kbtnForward.Click += kbtnForward_Click;

        kbtnUp = new KryptonButton
        {
            Text = @"↑",
            Size = new Size(32, 32),
            Location = new Point(76, 4)
        };

        kbtnUp.Click += kbtnUp_Click;

        kbtnRefresh = new KryptonButton
        {
            Text = @"↻",
            Size = new Size(32, 32),
            Location = new Point(112, 4)
        };

        kbtnRefresh.Click += kbtnRefresh_Click;

        // Create view buttons
        kbtnViewDetails = new KryptonButton
        {
            Text = "Details",
            Size = new Size(60, 32),
            Location = new Point(152, 4)
        };

        kbtnViewDetails.Click += kbtnViewDetails_Click;

        kbtnViewList = new KryptonButton
        {
            Text = "List",
            Size = new Size(60, 32),
            Location = new Point(216, 4)
        };

        kbtnViewList.Click += kbtnViewList_Click;

        kbtnViewLargeIcons = new KryptonButton
        {
            Text = "Large",
            Size = new Size(60, 32),
            Location = new Point(280, 4)
        };

        kbtnViewLargeIcons.Click += kbtnViewLargeIcons_Click;

        kbtnViewSmallIcons = new KryptonButton
        {
            Text = "Small",
            Size = new Size(60, 32),
            Location = new Point(344, 4)
        };

        kbtnViewSmallIcons.Click += kbtnViewSmallIcons_Click;

        // Create address bar
        ktxtAddressBar = new KryptonTextBox
        {
            Location = new Point(4, 40),
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
            Width = _toolbarPanel.Width - 8
        };

        ktxtAddressBar.KeyDown += ktxtAddressBar_KeyDown;

        // Create search box
        _searchBox = new KryptonSearchBox
        {
            Location = new Point(412, 4),
            Size = new Size(200, 32),
            Anchor = AnchorStyles.Top | AnchorStyles.Right
        };

        // Add controls to toolbar
        _toolbarPanel.Controls.Add(kbtnBack);
        _toolbarPanel.Controls.Add(kbtnForward);
        _toolbarPanel.Controls.Add(kbtnUp);
        _toolbarPanel.Controls.Add(kbtnRefresh);
        _toolbarPanel.Controls.Add(kbtnViewDetails);
        _toolbarPanel.Controls.Add(kbtnViewList);
        _toolbarPanel.Controls.Add(kbtnViewLargeIcons);
        _toolbarPanel.Controls.Add(kbtnViewSmallIcons);
        _toolbarPanel.Controls.Add(ktxtAddressBar);
        _toolbarPanel.Controls.Add(_searchBox);

        // Create content panel
        _contentPanel = new KryptonPanel
        {
            Dock = DockStyle.Fill
        };

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
            View = View.Details
        };
        _listView.SelectedIndexChanged += ListView_SelectedIndexChanged;
        _listView.DoubleClick += ListView_DoubleClick;
        _listView.PathChanged += ListView_PathChanged;
        _listView.FileSystemError += ListView_FileSystemError;

        // Add controls to panels
        _treeViewPanel.Controls.Add(_treeView);
        _listViewPanel.Controls.Add(_listView);

        // Add split container to content panel
        _contentPanel.Controls.Add(_splitContainer);

        // Create status panel
        _statusPanel = new KryptonPanel
        {
            Dock = DockStyle.Bottom,
            Height = 24,
            Padding = new Padding(4, 2, 4, 2)
        };

        klblStatus = new KryptonLabel
        {
            Dock = DockStyle.Fill,
            Text = "Ready"
        };
        _statusPanel.Controls.Add(klblStatus);

        // Add panels to main control
        Controls.Add(_contentPanel);
        Controls.Add(_toolbarPanel);
        Controls.Add(_statusPanel);

        ResumeLayout(false);
    }

    private void SetupControls()
    {
        // Set initial path
        if (string.IsNullOrEmpty(_currentPath))
        {
            _currentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            _navigationValues.CurrentPath = _currentPath;
        }

        // Configure tree view
        if (_treeView != null)
        {
            _treeView.FileSystemTreeViewValues.RootPath = _currentPath;
            _treeView.FileSystemTreeViewValues.ShowFiles = _explorerValues.ShowFiles;
            _treeView.FileSystemTreeViewValues.ShowHiddenFiles = _explorerValues.ShowHiddenFiles;
            _treeView.FileSystemTreeViewValues.ShowSystemFiles = _explorerValues.ShowSystemFiles;
            _treeView.FileSystemTreeViewValues.FileFilter = _explorerValues.FileFilter;
        }

        // Configure list view
        if (_listView != null)
        {
            _listView.View = _displayValues.ViewMode;
            _listView.MultiSelect = _displayValues.SelectionMode != SelectionMode.One;
            _listView.FileSystemListViewValues.ShowFiles = _explorerValues.ShowFiles;
            _listView.FileSystemListViewValues.ShowHiddenFiles = _explorerValues.ShowHiddenFiles;
            _listView.FileSystemListViewValues.ShowSystemFiles = _explorerValues.ShowSystemFiles;
            _listView.FileSystemListViewValues.FileFilter = _explorerValues.FileFilter;
            _listView.FileSystemListViewValues.CurrentPath = _currentPath;
        }

        // Configure UI visibility
        if (_toolbarPanel != null)
        {
            _toolbarPanel.Visible = _displayValues.ShowToolbar;
        }
        if (_statusPanel != null)
        {
            _statusPanel.Visible = _displayValues.ShowStatusBar;
        }
        if (_splitContainer != null)
        {
            _splitContainer.Panel1Collapsed = !_displayValues.ShowTreeView;
        }

        // Update address bar
        if (ktxtAddressBar != null)
        {
            ktxtAddressBar.Text = _currentPath;
        }

        // Update navigation buttons
        UpdateNavigationButtons();
        UpdateStatus();
    }

    private void UpdateNavigationButtons()
    {
        if (kbtnBack != null)
        {
            kbtnBack.Enabled = _backHistory.Count > 0;
        }
        if (kbtnForward != null)
        {
            kbtnForward.Enabled = _forwardHistory.Count > 0;
        }
        if (kbtnUp != null)
        {
            kbtnUp.Enabled = !string.IsNullOrEmpty(_currentPath) && Directory.GetParent(_currentPath) != null;
        }
    }

    private void UpdateStatus()
    {
        if (klblStatus == null || string.IsNullOrEmpty(_currentPath))
        {
            return;
        }

        try
        {
            if (Directory.Exists(_currentPath))
            {
                string[] files = Directory.GetFiles(_currentPath);
                string[] dirs = Directory.GetDirectories(_currentPath);
                klblStatus.Text = $"{dirs.Length} folder(s), {files.Length} file(s)";
            }
            else
            {
                klblStatus.Text = "Ready";
            }
        }
        catch
        {
            klblStatus.Text = "Ready";
        }
    }

    private void kbtnBack_Click(object? sender, EventArgs e)
    {
        NavigateBack();
    }

    private void kbtnForward_Click(object? sender, EventArgs e)
    {
        NavigateForward();
    }

    private void kbtnUp_Click(object? sender, EventArgs e)
    {
        NavigateUp();
    }

    private void kbtnRefresh_Click(object? sender, EventArgs e)
    {
        Refresh();
    }

    private void kbtnViewDetails_Click(object? sender, EventArgs e)
    {
        _displayValues.ViewMode = View.Details;
    }

    private void kbtnViewList_Click(object? sender, EventArgs e)
    {
        _displayValues.ViewMode = View.List;
    }

    private void kbtnViewLargeIcons_Click(object? sender, EventArgs e)
    {
        _displayValues.ViewMode = View.LargeIcon;
    }

    private void kbtnViewSmallIcons_Click(object? sender, EventArgs e)
    {
        _displayValues.ViewMode = View.SmallIcon;
    }

    private void ktxtAddressBar_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter && ktxtAddressBar != null)
        {
            NavigateTo(ktxtAddressBar.Text);
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
                NavigateTo(newPath);
            }
        }
    }

    private void ListView_FileSystemError(object? sender, FileSystemErrorEventArgs e)
    {
        OnFileSystemError(e);
    }

    #endregion
}
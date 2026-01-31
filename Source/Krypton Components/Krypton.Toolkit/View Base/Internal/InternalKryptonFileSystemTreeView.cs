#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

using System.ComponentModel;
using System.IO;

/// <summary>
/// Provides a Krypton-styled TreeView control for browsing the file system.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonTreeView), "ToolboxBitmaps.KryptonTreeView.bmp")]
[DefaultEvent(nameof(AfterSelect))]
[DefaultProperty(nameof(FileSystemTreeViewValues))]
[DesignerCategory(@"code")]
[Description(@"Displays a hierarchical file system tree with Krypton styling.")]
[Docking(DockingBehavior.Ask)]
public class InternalKryptonFileSystemTreeView : KryptonTreeView
{
    #region Instance Fields

    private const string DUMMY_NODE_KEY = "__DUMMY__";
    private readonly FileSystemTreeViewValues _fileSystemValues;
    private ImageList? _imageList;
    private readonly Dictionary<string, int> _iconCache = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
    private const int DEFAULT_ICON_SIZE = 16;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when a directory is being expanded.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when a directory is being expanded.")]
    public event EventHandler<DirectoryExpandingEventArgs>? DirectoryExpanding;

    /// <summary>
    /// Occurs when a directory has been expanded.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when a directory has been expanded.")]
    public event EventHandler<DirectoryExpandedEventArgs>? DirectoryExpanded;

    /// <summary>
    /// Occurs when an error occurs while loading the file system.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when an error occurs while loading the file system.")]
    public event EventHandler<FileSystemErrorEventArgs>? FileSystemError;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="InternalKryptonFileSystemTreeView"/> class.
    /// </summary>
    public InternalKryptonFileSystemTreeView()
    {
        // Create the expandable properties object
        _fileSystemValues = new FileSystemTreeViewValues(this);

        // Set default properties
        ShowPlusMinus = true;
        ShowRootLines = true;
        ShowLines = true;

        // Initialize ImageList for icons
        InitializeImageList();

        // Wire up events
        BeforeExpand += OnBeforeExpand;
        AfterSelect += OnAfterSelect;
        AfterExpand += OnAfterExpand;
        BeforeCollapse += OnBeforeCollapse;

        // Hide dummy nodes - minimal interference with Krypton rendering
        TreeView.DrawNode += OnDrawNode;
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets the file system TreeView values.
    /// </summary>
    /// <value>
    /// The file system TreeView values.
    /// </value>
    public FileSystemTreeViewValues FileSystemTreeViewValues => _fileSystemValues;

    /// <summary>
    /// Gets or sets the root mode for the tree view.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Determines the root display mode: Desktop (Explorer-style with special folders), Computer (drives only), Drives (all drives), or CustomPath (use RootPath).")]
    [DefaultValue(FileSystemRootMode.Drives)]
    public FileSystemRootMode RootMode
    {
        get => _fileSystemValues.RootMode;
        set => _fileSystemValues.RootMode = value;
    }

    /// <summary>
    /// Gets or sets the root directory path to display in the tree view (used when RootMode is CustomPath).
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The root directory path to display in the tree view (used when RootMode is CustomPath).")]
    [DefaultValue("")]
    public string RootPath
    {
        get => _fileSystemValues.RootPath;
        set => _fileSystemValues.RootPath = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether files should be displayed in the tree view.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether files should be displayed in the tree view.")]
    [DefaultValue(true)]
    public bool ShowFiles
    {
        get => _fileSystemValues.ShowFiles;
        set => _fileSystemValues.ShowFiles = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether hidden files should be displayed.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether hidden files should be displayed.")]
    [DefaultValue(false)]
    public bool ShowHiddenFiles
    {
        get => _fileSystemValues.ShowHiddenFiles;
        set => _fileSystemValues.ShowHiddenFiles = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether system files should be displayed.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether system files should be displayed.")]
    [DefaultValue(false)]
    public bool ShowSystemFiles
    {
        get => _fileSystemValues.ShowSystemFiles;
        set => _fileSystemValues.ShowSystemFiles = value;
    }

    /// <summary>
    /// Gets or sets the file filter to apply when showing files (e.g., "*.txt" or "*.txt;*.doc").
    /// </summary>
    [Category(@"Behavior")]
    [Description("The file filter to apply when showing files (e.g., \"*.txt\" or \"*.txt;*.doc\").")]
    [DefaultValue("*.*")]
    public string FileFilter
    {
        get => _fileSystemValues.FileFilter;
        set => _fileSystemValues.FileFilter = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether special folders (Desktop, Computer, Network, Recycle Bin, etc.) should be displayed when RootMode is Desktop.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether special folders (Desktop, Computer, Network, Recycle Bin, etc.) should be displayed when RootMode is Desktop.")]
    [DefaultValue(true)]
    public bool ShowSpecialFolders
    {
        get => _fileSystemValues.ShowSpecialFolders;
        set => _fileSystemValues.ShowSpecialFolders = value;
    }

    /// <summary>
    /// Gets the full path of the currently selected file or folder.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string? SelectedPath => SelectedNode?.Tag as string;

    #endregion

    #region Public Methods

    /// <summary>
    /// Reloads the tree view from the root path.
    /// </summary>
    public void Reload()
    {
        Nodes.Clear();

        switch (_fileSystemValues.RootMode)
        {
            case FileSystemRootMode.Desktop:
                LoadDesktopRoot();
                break;

            case FileSystemRootMode.Computer:
                LoadComputerRoot();
                break;

            case FileSystemRootMode.Drives:
                LoadDriveRoots();
                break;

            case FileSystemRootMode.CustomPath:
                LoadCustomPath();
                break;
        }
    }

    private void LoadCustomPath()
    {
        // If RootPath is not set or invalid, fall back to drives
        if (string.IsNullOrEmpty(_fileSystemValues.RootPath) || !Directory.Exists(_fileSystemValues.RootPath))
        {
            LoadDriveRoots();
            return;
        }

        try
        {
            TreeNode rootNode = CreateDirectoryNode(_fileSystemValues.RootPath);
            Nodes.Add(rootNode);
            rootNode.Expand();
        }
        catch (Exception ex)
        {
            OnFileSystemError(new FileSystemErrorEventArgs(_fileSystemValues.RootPath, ex));
        }
    }

    /// <summary>
    /// Navigates to the specified path in the tree view.
    /// </summary>
    /// <param name="path">The path to navigate to.</param>
    /// <returns>True if the path was found and selected; otherwise, false.</returns>
    public bool NavigateToPath(string path)
    {
        if (string.IsNullOrEmpty(path) || (!Directory.Exists(path) && !File.Exists(path)))
        {
            return false;
        }

        string[] pathParts = path.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        TreeNode? currentNode = null;

        foreach (string part in pathParts)
        {
            if (string.IsNullOrEmpty(part))
            {
                continue;
            }

            TreeNodeCollection nodesToSearch = currentNode?.Nodes ?? Nodes;
            TreeNode? nextNode = null;

            foreach (TreeNode node in nodesToSearch)
            {
                if (node.Text.Equals(part, StringComparison.OrdinalIgnoreCase))
                {
                    nextNode = node;
                    break;
                }
            }

            if (nextNode == null)
            {
                break;
            }

            currentNode = nextNode;
        }

        if (currentNode != null)
        {
            SelectedNode = currentNode;
            currentNode.EnsureVisible();
            return true;
        }

        return false;
    }

    #endregion

    #region Protected Virtual

    /// <summary>
    /// Ensure initial population of the tree when the control is created.
    /// </summary>
    /// <param name="e">Event args.</param>
    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);
        if (!DesignMode)
        {
            Reload();
        }
    }

    /// <summary>
    /// Raises the DirectoryExpanding event.
    /// </summary>
    /// <param name="e">A DirectoryExpandingEventArgs containing the event data.</param>
    protected virtual void OnDirectoryExpanding(DirectoryExpandingEventArgs e) => DirectoryExpanding?.Invoke(this, e);

    /// <summary>
    /// Raises the DirectoryExpanded event.
    /// </summary>
    /// <param name="e">A DirectoryExpandedEventArgs containing the event data.</param>
    protected virtual void OnDirectoryExpanded(DirectoryExpandedEventArgs e) => DirectoryExpanded?.Invoke(this, e);

    /// <summary>
    /// Raises the FileSystemError event.
    /// </summary>
    /// <param name="e">A FileSystemErrorEventArgs containing the event data.</param>
    protected virtual void OnFileSystemError(FileSystemErrorEventArgs e) => FileSystemError?.Invoke(this, e);

    #endregion

    #region Implementation

    private void OnDrawNode(object? sender, DrawTreeNodeEventArgs e)
    {
        // Hide dummy nodes only - let KryptonTreeView handle everything else
        if (e.Node?.Name == DUMMY_NODE_KEY)
        {
            e.DrawDefault = false;
        }
    }

    private TreeNode CreateDirectoryNode(string path)
    {
        string displayName = Path.GetFileName(path);
        if (string.IsNullOrEmpty(displayName))
        {
            displayName = path;
        }

        var node = new TreeNode(displayName)
        {
            Tag = path
        };

        // Set icon for directory
        int iconIndex = GetIconIndex(path, isDirectory: true);
        node.ImageIndex = iconIndex;
        node.SelectedImageIndex = iconIndex;

        // Add dummy node to enable expansion
        TreeNode dummyNode = new TreeNode(DUMMY_NODE_KEY)
        {
            Tag = null,
            Name = DUMMY_NODE_KEY,
            Text = string.Empty
        };
        node.Nodes.Add(dummyNode);

        return node;
    }

    private TreeNode CreateFileNode(string path)
    {
        string displayName = Path.GetFileName(path);
        var node = new TreeNode(displayName)
        {
            Tag = path
        };

        // Set icon for file
        int iconIndex = GetIconIndex(path, isDirectory: false);
        node.ImageIndex = iconIndex;
        node.SelectedImageIndex = iconIndex;

        return node;
    }

    private void OnBeforeExpand(object? sender, TreeViewCancelEventArgs e)
    {
        if (e.Node?.Tag is string path)
        {
            // Check if this node has a dummy child node
            if (e.Node.Nodes.Count == 1)
            {
                TreeNode firstChild = e.Node.Nodes[0];
                if (firstChild.Name == DUMMY_NODE_KEY)
                {
                    e.Node.Nodes.Clear();

                    var expandingArgs = new DirectoryExpandingEventArgs(path);
                    OnDirectoryExpanding(expandingArgs);

                    if (!expandingArgs.Cancel)
                    {
                        // Handle special folder CLSIDs
                        if (path.StartsWith("::", StringComparison.Ordinal))
                        {
                            LoadSpecialFolderNodes(e.Node, path);
                        }
                        else if (Directory.Exists(path))
                        {
                            LoadDirectoryNodes(e.Node, path);
                        }
                    }
                }
            }
        }
    }

    private void LoadSpecialFolderNodes(TreeNode parentNode, string clsid)
    {
        // Handle Computer CLSID - load drives
        if (clsid == "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}")
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                try
                {
                    string path = drive.RootDirectory.FullName;
                    TreeNode driveNode = CreateDirectoryNode(path);

                    string driveLabel = path;
                    if (drive.IsReady && !string.IsNullOrEmpty(drive.VolumeLabel))
                    {
                        driveLabel = $"{drive.VolumeLabel} ({path})";
                    }
                    else
                    {
                        driveLabel = $"{path} ({drive.DriveType})";
                    }

                    driveNode.Text = driveLabel;
                    parentNode.Nodes.Add(driveNode);
                }
                catch (Exception ex)
                {
                    OnFileSystemError(new FileSystemErrorEventArgs(drive.Name, ex));
                }
            }
        }
        // Other special folders (Control Panel, Network, Recycle Bin) don't have file system children
        // They could be expanded to show their virtual contents, but that's beyond basic file system browsing
    }

    private void OnAfterSelect(object? sender, TreeViewEventArgs e)
    {
        if (e.Node?.Tag is string path)
        {
            OnDirectoryExpanded(new DirectoryExpandedEventArgs(path));
        }
    }

    private void LoadDirectoryNodes(TreeNode parentNode, string directoryPath)
    {
        try
        {
            // Get directories
            string[] directories = Directory.GetDirectories(directoryPath);
            foreach (string dir in directories)
            {
                try
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(dir);
                    bool isHidden = (dirInfo.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden;
                    bool isSystem = (dirInfo.Attributes & FileAttributes.System) == FileAttributes.System;

                    if ((isHidden && !_fileSystemValues.ShowHiddenFiles) || (isSystem && !_fileSystemValues.ShowSystemFiles))
                    {
                        continue;
                    }

                    TreeNode dirNode = CreateDirectoryNode(dir);
                    parentNode.Nodes.Add(dirNode);
                }
                catch (UnauthorizedAccessException ex)
                {
                    OnFileSystemError(new FileSystemErrorEventArgs(dir, ex));
                }
            }

            // Get files
            if (_fileSystemValues.ShowFiles)
            {
                string[] files = Directory.GetFiles(directoryPath, _fileSystemValues.FileFilter);
                foreach (string file in files)
                {
                    try
                    {
                        FileInfo fileInfo = new FileInfo(file);
                        bool isHidden = (fileInfo.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden;
                        bool isSystem = (fileInfo.Attributes & FileAttributes.System) == FileAttributes.System;

                        if ((isHidden && !_fileSystemValues.ShowHiddenFiles) || (isSystem && !_fileSystemValues.ShowSystemFiles))
                        {
                            continue;
                        }

                        TreeNode fileNode = CreateFileNode(file);
                        parentNode.Nodes.Add(fileNode);
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
            OnFileSystemError(new FileSystemErrorEventArgs(directoryPath, ex));
        }
    }

    private void LoadDesktopRoot()
    {
        try
        {
            // Create Desktop root node
            TreeNode desktopNode = new TreeNode("Desktop")
            {
                Tag = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            };
            Nodes.Add(desktopNode);

            // Add special folders if enabled
            if (_fileSystemValues.ShowSpecialFolders)
            {
                // Computer (contains all drives)
                TreeNode computerNode = new TreeNode("Computer")
                {
                    Tag = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}" // CLSID for Computer
                };
                TreeNode dummyComputer = new TreeNode(DUMMY_NODE_KEY) { Tag = null, Name = DUMMY_NODE_KEY, Text = string.Empty };
                computerNode.Nodes.Add(dummyComputer);
                desktopNode.Nodes.Add(computerNode);

                // Control Panel
                TreeNode controlPanelNode = new TreeNode("Control Panel")
                {
                    Tag = "::{21EC2020-3AEA-1069-A2DD-08002B30309D}" // CLSID for Control Panel
                };
                desktopNode.Nodes.Add(controlPanelNode);

                // My Documents
                try
                {
                    string myDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    if (Directory.Exists(myDocuments))
                    {
                        TreeNode myDocsNode = CreateDirectoryNode(myDocuments);
                        myDocsNode.Text = "My Documents";
                        desktopNode.Nodes.Add(myDocsNode);
                    }
                }
                catch
                {
                    // Ignore errors
                }

                // Shared Documents
                try
                {
                    string sharedDocs = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
                    if (Directory.Exists(sharedDocs))
                    {
                        TreeNode sharedDocsNode = CreateDirectoryNode(sharedDocs);
                        sharedDocsNode.Text = "Shared Documents";
                        desktopNode.Nodes.Add(sharedDocsNode);
                    }
                }
                catch
                {
                    // Ignore errors
                }

                // Network
                TreeNode networkNode = new TreeNode("Network")
                {
                    Tag = "::{208D2C60-3AEA-1069-A2D7-08002B30309D}" // CLSID for Network
                };
                desktopNode.Nodes.Add(networkNode);

                // Documents (Public Documents)
                try
                {
                    string publicDocs = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
                    if (Directory.Exists(publicDocs))
                    {
                        TreeNode docsNode = CreateDirectoryNode(publicDocs);
                        docsNode.Text = "Documents";
                        desktopNode.Nodes.Add(docsNode);
                    }
                }
                catch
                {
                    // Ignore errors
                }

                // Recycle Bin
                TreeNode recycleBinNode = new TreeNode("Recycle Bin")
                {
                    Tag = "::{645FF040-5081-101B-9F08-00AA002F954E}" // CLSID for Recycle Bin
                };
                desktopNode.Nodes.Add(recycleBinNode);
            }

            // Add all drives directly under Desktop
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                try
                {
                    string path = drive.RootDirectory.FullName;
                    TreeNode driveNode = CreateDirectoryNode(path);

                    // Format drive label
                    string driveLabel = path;
                    if (drive.IsReady && !string.IsNullOrEmpty(drive.VolumeLabel))
                    {
                        driveLabel = $"{drive.VolumeLabel} ({path})";
                    }
                    else
                    {
                        driveLabel = $"{path} ({drive.DriveType})";
                    }

                    driveNode.Text = driveLabel;
                    desktopNode.Nodes.Add(driveNode);
                }
                catch (Exception ex)
                {
                    OnFileSystemError(new FileSystemErrorEventArgs(drive.Name, ex));
                }
            }

            desktopNode.Expand();
        }
        catch (Exception ex)
        {
            OnFileSystemError(new FileSystemErrorEventArgs("Desktop", ex));
        }
    }

    private void LoadComputerRoot()
    {
        try
        {
            // Create Computer root node
            TreeNode computerNode = new TreeNode("Computer")
            {
                Tag = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}" // CLSID for Computer
            };
            Nodes.Add(computerNode);

            // Add all drives under Computer
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                try
                {
                    string path = drive.RootDirectory.FullName;
                    TreeNode driveNode = CreateDirectoryNode(path);

                    // Format drive label
                    string driveLabel = path;
                    if (drive.IsReady && !string.IsNullOrEmpty(drive.VolumeLabel))
                    {
                        driveLabel = $"{drive.VolumeLabel} ({path})";
                    }
                    else
                    {
                        driveLabel = $"{path} ({drive.DriveType})";
                    }

                    driveNode.Text = driveLabel;
                    computerNode.Nodes.Add(driveNode);
                }
                catch (Exception ex)
                {
                    OnFileSystemError(new FileSystemErrorEventArgs(drive.Name, ex));
                }
            }

            computerNode.Expand();
        }
        catch (Exception ex)
        {
            OnFileSystemError(new FileSystemErrorEventArgs("Computer", ex));
        }
    }

    private void LoadDriveRoots()
    {
        try
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                try
                {
                    string path = drive.RootDirectory.FullName;
                    TreeNode driveNode = CreateDirectoryNode(path);

                    // Format drive label
                    string driveLabel = path;
                    if (drive.IsReady && !string.IsNullOrEmpty(drive.VolumeLabel))
                    {
                        driveLabel = $"{drive.VolumeLabel} ({path})";
                    }
                    else
                    {
                        // Show drive even if not ready (e.g., CD-ROM without disc)
                        string driveTypeName = drive.DriveType.ToString();
                        driveLabel = $"{path} ({driveTypeName})";
                    }

                    driveNode.Text = driveLabel;

                    // Set drive-specific icon
                    int driveIconIndex = GetDriveIconIndex(drive);
                    driveNode.ImageIndex = driveIconIndex;
                    driveNode.SelectedImageIndex = driveIconIndex;

                    Nodes.Add(driveNode);
                }
                catch (Exception ex)
                {
                    OnFileSystemError(new FileSystemErrorEventArgs(drive.Name, ex));
                }
            }
        }
        catch (Exception ex)
        {
            OnFileSystemError(new FileSystemErrorEventArgs(string.Empty, ex));
        }
    }

    private void InitializeImageList()
    {
        _imageList = new ImageList
        {
            ImageSize = new Size(DEFAULT_ICON_SIZE, DEFAULT_ICON_SIZE),
            ColorDepth = ColorDepth.Depth32Bit
        };

        ImageList = _imageList;

        // Add default folder icon
        AddDefaultIcon();
    }

    private void AddDefaultIcon()
    {
        if (_imageList == null)
        {
            return;
        }

        try
        {
            Icon? folderIcon = FileSystemIconHelper.GetFolderIcon(largeIcon: false);
            if (folderIcon != null)
            {
                using (folderIcon)
                {
                    Bitmap sourceBitmap = folderIcon.ToBitmap();
                    try
                    {
                        // Create a bitmap with the exact size needed for ImageList
                        Bitmap bitmapToAdd = new Bitmap(_imageList.ImageSize.Width, _imageList.ImageSize.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                        using (Graphics g = Graphics.FromImage(bitmapToAdd))
                        {
                            g.Clear(Color.Transparent);
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            g.DrawImage(sourceBitmap, 0, 0, _imageList.ImageSize.Width, _imageList.ImageSize.Height);
                        }

                        // Validate and add - ImageList will make its own copy
                        if (bitmapToAdd.Width > 0 && bitmapToAdd.Height > 0)
                        {
                            _imageList.Images.Add(bitmapToAdd);
                            // Force ImageList to create handle and copy the bitmap immediately
                            _ = _imageList.Handle;
                        }
                        bitmapToAdd.Dispose();
                    }
                    finally
                    {
                        sourceBitmap.Dispose();
                    }
                }
            }
        }
        catch
        {
            // If icon extraction fails, create a simple colored bitmap
            Bitmap defaultBitmap = new Bitmap(_imageList.ImageSize.Width, _imageList.ImageSize.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            try
            {
                using (Graphics g = Graphics.FromImage(defaultBitmap))
                {
                    g.Clear(Color.Transparent);
                    g.FillRectangle(Brushes.Blue, 2, 2, _imageList.ImageSize.Width - 4, _imageList.ImageSize.Height - 4);
                }
                _imageList.Images.Add(defaultBitmap);
                _ = _imageList.Handle;
            }
            finally
            {
                defaultBitmap.Dispose();
            }
        }
    }

    private int GetIconIndex(string path, bool isDirectory)
    {
        if (_imageList == null)
        {
            return 0;
        }

        // Create cache key
        string cacheKey = isDirectory ? "DIR" : Path.GetExtension(path).ToLowerInvariant();

        // Check cache
        if (_iconCache.TryGetValue(cacheKey, out int cachedIndex))
        {
            return cachedIndex;
        }

        // Get icon
        Icon? icon = null;
        try
        {
            if (isDirectory)
            {
                icon = FileSystemIconHelper.GetFolderIcon(largeIcon: false);
            }
            else
            {
                string extension = Path.GetExtension(path);
                icon = FileSystemIconHelper.GetFileIcon(extension, largeIcon: false);
            }

            if (icon != null)
            {
                using (icon)
                {
                    Bitmap sourceBitmap = icon.ToBitmap();
                    try
                    {
                        // Create a bitmap with the exact size needed for ImageList
                        Bitmap bitmapToAdd = new Bitmap(_imageList.ImageSize.Width, _imageList.ImageSize.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                        using (Graphics g = Graphics.FromImage(bitmapToAdd))
                        {
                            g.Clear(Color.Transparent);
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            g.DrawImage(sourceBitmap, 0, 0, _imageList.ImageSize.Width, _imageList.ImageSize.Height);
                        }

                        // Validate and add - ImageList will make its own copy
                        if (bitmapToAdd.Width > 0 && bitmapToAdd.Height > 0)
                        {
                            int index = _imageList.Images.Count;
                            _imageList.Images.Add(bitmapToAdd);
                            // Force ImageList to create handle and copy the bitmap immediately
                            _ = _imageList.Handle;

                            // Cache the index
                            _iconCache[cacheKey] = index;
                            bitmapToAdd.Dispose();
                            return index;
                        }
                        bitmapToAdd.Dispose();
                    }
                    finally
                    {
                        sourceBitmap.Dispose();
                    }
                }
            }
        }
        catch
        {
            // Fall back to default icon (index 0)
        }

        // Return default icon index
        return 0;
    }

    private int GetDriveIconIndex(DriveInfo drive)
    {
        if (_imageList == null)
        {
            return 0;
        }

        // Create cache key for drive type
        string cacheKey = $"DRIVE_{drive.DriveType}";

        // Check cache
        if (_iconCache.TryGetValue(cacheKey, out int cachedIndex))
        {
            return cachedIndex;
        }

        // Get drive icon based on type
        Icon? icon = null;
        try
        {
            StockIconHelper.StockIconId stockIconId = drive.DriveType switch
            {
                DriveType.Fixed => StockIconHelper.StockIconId.DriveFixed,
                DriveType.Network => StockIconHelper.StockIconId.DriveNetwork,
                DriveType.CDRom => StockIconHelper.StockIconId.DriveCD,
                DriveType.Removable => StockIconHelper.StockIconId.DriveRemove,
                DriveType.Ram => StockIconHelper.StockIconId.DriveRAM,
                _ => StockIconHelper.StockIconId.DriveFixed
            };

            icon = StockIconHelper.GetStockIcon(stockIconId);

            if (icon != null)
            {
                using (icon)
                {
                    Bitmap sourceBitmap = icon.ToBitmap();
                    try
                    {
                        // Create a bitmap with the exact size needed for ImageList
                        Bitmap bitmapToAdd = new Bitmap(_imageList.ImageSize.Width, _imageList.ImageSize.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                        using (Graphics g = Graphics.FromImage(bitmapToAdd))
                        {
                            g.Clear(Color.Transparent);
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            g.DrawImage(sourceBitmap, 0, 0, _imageList.ImageSize.Width, _imageList.ImageSize.Height);
                        }

                        // Validate and add - ImageList will make its own copy
                        if (bitmapToAdd.Width > 0 && bitmapToAdd.Height > 0)
                        {
                            int index = _imageList.Images.Count;
                            _imageList.Images.Add(bitmapToAdd);
                            // Force ImageList to create handle and copy the bitmap immediately
                            _ = _imageList.Handle;

                            // Cache the index
                            _iconCache[cacheKey] = index;
                            bitmapToAdd.Dispose();
                            return index;
                        }
                        bitmapToAdd.Dispose();
                    }
                    finally
                    {
                        sourceBitmap.Dispose();
                    }
                }
            }
        }
        catch
        {
            // Fall back to default icon (index 0)
        }

        // Return default icon index
        return 0;
    }

    private void OnAfterExpand(object? sender, TreeViewEventArgs e)
    {
        // Update folder icon to open state when expanded
        if (e.Node?.Tag is string path && Directory.Exists(path))
        {
            int openIconIndex = GetFolderOpenIconIndex();
            if (openIconIndex >= 0)
            {
                e.Node.SelectedImageIndex = openIconIndex;
            }
        }
    }

    private void OnBeforeCollapse(object? sender, TreeViewCancelEventArgs e)
    {
        // Update folder icon to closed state when collapsed
        if (e.Node?.Tag is string path && Directory.Exists(path))
        {
            // Reset to closed folder icon (same as ImageIndex)
            e.Node.SelectedImageIndex = e.Node.ImageIndex;
        }
    }

    private int GetFolderOpenIconIndex()
    {
        if (_imageList == null)
        {
            return -1;
        }

        const string cacheKey = "DIR_OPEN";

        // Check cache
        if (_iconCache.TryGetValue(cacheKey, out int cachedIndex))
        {
            return cachedIndex;
        }

        // Get folder open icon
        Icon? icon = null;
        try
        {
            icon = StockIconHelper.GetStockIcon(StockIconHelper.StockIconId.FolderOpen);

            if (icon != null)
            {
                using (icon)
                {
                    Bitmap sourceBitmap = icon.ToBitmap();
                    try
                    {
                        // Create a bitmap with the exact size needed for ImageList
                        Bitmap bitmapToAdd = new Bitmap(_imageList.ImageSize.Width, _imageList.ImageSize.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                        using (Graphics g = Graphics.FromImage(bitmapToAdd))
                        {
                            g.Clear(Color.Transparent);
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            g.DrawImage(sourceBitmap, 0, 0, _imageList.ImageSize.Width, _imageList.ImageSize.Height);
                        }

                        // Validate and add - ImageList will make its own copy
                        if (bitmapToAdd.Width > 0 && bitmapToAdd.Height > 0)
                        {
                            int index = _imageList.Images.Count;
                            _imageList.Images.Add(bitmapToAdd);
                            // Force ImageList to create handle and copy the bitmap immediately
                            _ = _imageList.Handle;

                            // Cache the index
                            _iconCache[cacheKey] = index;
                            bitmapToAdd.Dispose();
                            return index;
                        }
                        bitmapToAdd.Dispose();
                    }
                    finally
                    {
                        sourceBitmap.Dispose();
                    }
                }
            }
        }
        catch
        {
            // Fall back to default folder icon
            return 0;
        }

        // Return default icon index
        return 0;
    }

    #endregion
}
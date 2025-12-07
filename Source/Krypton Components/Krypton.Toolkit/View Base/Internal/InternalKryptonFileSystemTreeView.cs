#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;
/// <summary>
/// Provides a Krypton-styled TreeView control for browsing the file system with proper icons.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonTreeView), "ToolboxBitmaps.KryptonTreeView.bmp")]
[DefaultEvent(nameof(AfterSelect))]
[DefaultProperty(nameof(FileSystemTreeViewValues.RootPath))]
[DesignerCategory(@"code")]
[Description(@"Displays a hierarchical file system tree with folder and file icons.")]
[Docking(DockingBehavior.Ask)]
internal class InternalKryptonFileSystemTreeView : KryptonTreeView
{
    #region Instance Fields

    private readonly ImageList _imageList;
    private readonly Dictionary<string, int> _iconCache;
    private const string DUMMY_NODE_KEY = "__DUMMY__";
    private readonly FileSystemTreeViewValues _fileSystemValues;

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
    /// Initializes a new instance of the <see cref="InternalKryptonFileSystemTreeView"/> class.
    /// </summary>
    public InternalKryptonFileSystemTreeView()
    {
        _iconCache = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        _imageList = new ImageList
        {
            ColorDepth = ColorDepth.Depth32Bit,
            ImageSize = new Size(16, 16)
        };

        // Create the expandable properties object
        _fileSystemValues = new FileSystemTreeViewValues(this);

        // Add a default folder icon
        AddDefaultIcon();

        ImageList = _imageList;

        // Set default properties
        ShowPlusMinus = true;
        ShowRootLines = true;
        ShowLines = true;

        // Wire up events
        BeforeExpand += OnBeforeExpand;
        AfterSelect += OnAfterSelect;

        // Access the internal TreeView to hook into DrawNode event
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
    /// Gets the full path of the currently selected file or folder.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string? SelectedPath => SelectedNode?.Tag as string;

    internal Dictionary<string, int> IconCache => _iconCache;

    #endregion

    #region Public Methods

    /// <summary>
    /// Reloads the tree view from the root path.
    /// </summary>
    public void Reload()
    {
        Nodes.Clear();
        _iconCache.Clear();
        _imageList.Images.Clear();
        AddDefaultIcon();

        if (string.IsNullOrEmpty(FileSystemTreeViewValues.RootPath) || !Directory.Exists(FileSystemTreeViewValues.RootPath))
        {
            return;
        }

        try
        {
            TreeNode rootNode = CreateDirectoryNode(FileSystemTreeViewValues.RootPath);
            Nodes.Add(rootNode);
            rootNode.Expand();
        }
        catch (Exception ex)
        {
            OnFileSystemError(new FileSystemErrorEventArgs(FileSystemTreeViewValues.RootPath, ex));
        }
    }

    /// <summary>
    /// Navigates to the specified path in the tree view.
    /// </summary>
    /// <param name="path">The path to navigate to.</param>
    /// <returns>True if the path was found and selected; otherwise, false.</returns>
    public bool NavigateToPath(string path)
    {
        if (string.IsNullOrEmpty(path) || !Directory.Exists(path) && !File.Exists(path))
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

    internal void AddDefaultIcon()
    {
        try
        {
            Icon? folderIcon = StockIconHelper.GetStockIcon(StockIconHelper.StockIconId.Folder);
            if (folderIcon != null)
            {
                using (folderIcon)
                {
                    Bitmap sourceBitmap = folderIcon.ToBitmap();
                    try
                    {
                        // Create a bitmap with the exact size needed for ImageList
                        Bitmap bitmapToAdd = new Bitmap(_imageList.ImageSize.Width, _imageList.ImageSize.Height, PixelFormat.Format32bppArgb);
                        using (Graphics g = Graphics.FromImage(bitmapToAdd))
                        {
                            g.Clear(Color.Transparent);
                            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
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
            // If stock icon fails, create a simple colored bitmap
            Bitmap defaultBitmap = new Bitmap(_imageList.ImageSize.Width, _imageList.ImageSize.Height, PixelFormat.Format32bppArgb);
            try
            {
                using (Graphics g = Graphics.FromImage(defaultBitmap))
                {
                    g.Clear(Color.Transparent);
                    g.FillRectangle(new SolidBrush(Color.LightGray), 0, 0, defaultBitmap.Width, defaultBitmap.Height);
                }

                if (defaultBitmap.Width > 0 && defaultBitmap.Height > 0)
                {
                    _imageList.Images.Add(defaultBitmap);
                    // Force ImageList to create handle and copy the bitmap immediately
                    _ = _imageList.Handle;
                }
            }
            finally
            {
                defaultBitmap.Dispose();
            }
        }
    }

    private int GetIconIndex(string path, bool isDirectory)
    {
        // Create cache key
        string cacheKey = isDirectory ? "__DIRECTORY__" : Path.GetExtension(path).ToLowerInvariant();
        if (string.IsNullOrEmpty(cacheKey))
        {
            cacheKey = "__FILE__";
        }

        // Check cache first
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
                // Try to get specific folder icon first
                if (Directory.Exists(path))
                {
                    icon = FileSystemIconHelper.GetFileSystemIcon(path, FileSystemTreeViewValues.UseLargeIcons);
                }

                // Fallback to generic folder icon
                if (icon == null)
                {
                    icon = FileSystemIconHelper.GetFolderIcon(FileSystemTreeViewValues.UseLargeIcons);
                }

                // Final fallback to stock icon
                if (icon == null)
                {
                    icon = StockIconHelper.GetStockIcon(StockIconHelper.StockIconId.Folder);
                }
            }
            else
            {
                // For files, get icon based on extension or actual file
                if (File.Exists(path))
                {
                    icon = FileSystemIconHelper.GetFileSystemIcon(path, FileSystemTreeViewValues.UseLargeIcons);
                }
                else
                {
                    string extension = Path.GetExtension(path);
                    if (!string.IsNullOrEmpty(extension))
                    {
                        icon = FileSystemIconHelper.GetFileIcon(extension, FileSystemTreeViewValues.UseLargeIcons);
                    }
                }

                // Fallback to document icon
                if (icon == null)
                {
                    icon = StockIconHelper.GetStockIcon(StockIconHelper.StockIconId.DocumentNotAssociated);
                }
            }
        }
        catch
        {
            // Icon retrieval failed
        }

        // Convert icon to bitmap and add to ImageList
        if (icon != null)
        {
            try
            {
                using (icon)
                {
                    Bitmap sourceBitmap = icon.ToBitmap();
                    try
                    {
                        // Create a bitmap with the exact size needed for ImageList
                        Bitmap bitmapToAdd = new Bitmap(_imageList.ImageSize.Width, _imageList.ImageSize.Height, PixelFormat.Format32bppArgb);
                        using (Graphics g = Graphics.FromImage(bitmapToAdd))
                        {
                            g.Clear(Color.Transparent);
                            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            g.DrawImage(sourceBitmap, 0, 0, _imageList.ImageSize.Width, _imageList.ImageSize.Height);
                        }

                        // Validate and add to ImageList - ImageList will make its own copy
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
            catch
            {
                // Icon disposal handled by using statement
            }
        }

        // Fallback to default icon (index 0)
        return 0;
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
            Tag = path,
            ImageIndex = GetIconIndex(path, true),
            SelectedImageIndex = GetIconIndex(path, true)
        };

        // Add dummy node to enable expansion (hidden from view)
        TreeNode dummyNode = new TreeNode(DUMMY_NODE_KEY)
        {
            Tag = null,
            Name = DUMMY_NODE_KEY,  // Use Name to identify it
            Text = string.Empty  // Hide the dummy node text
        };
        node.Nodes.Add(dummyNode);

        return node;
    }

    private TreeNode CreateFileNode(string path)
    {
        string displayName = Path.GetFileName(path);
        var node = new TreeNode(displayName)
        {
            Tag = path,
            ImageIndex = GetIconIndex(path, false),
            SelectedImageIndex = GetIconIndex(path, false)
        };

        return node;
    }

    private void OnBeforeExpand(object? sender, TreeViewCancelEventArgs e)
    {
        if (e.Node?.Tag is string path && Directory.Exists(path))
        {
            // Check if this is a dummy node (check by Name, Tag and empty text)
            if (e.Node.Nodes.Count == 1)
            {
                TreeNode firstChild = e.Node.Nodes[0];
                // Check if it's the dummy node by checking Name, Tag is null and text is empty
                if (firstChild.Name == DUMMY_NODE_KEY || (firstChild.Tag == null && string.IsNullOrEmpty(firstChild.Text)))
                {
                    e.Node.Nodes.Clear();

                    var expandingArgs = new DirectoryExpandingEventArgs(path);
                    OnDirectoryExpanding(expandingArgs);

                    if (!expandingArgs.Cancel)
                    {
                        LoadDirectoryNodes(e.Node, path);
                    }
                }
            }
        }
    }

    private void OnAfterSelect(object? sender, TreeViewEventArgs e)
    {
        if (e.Node?.Tag is string path)
        {
            OnDirectoryExpanded(new DirectoryExpandedEventArgs(path));
        }
    }

    private void OnDrawNode(object? sender, DrawTreeNodeEventArgs e)
    {
        // Skip drawing dummy nodes
        if (e.Node?.Name == DUMMY_NODE_KEY ||
            (e.Node?.Tag == null && string.IsNullOrEmpty(e.Node?.Text) && e.Node?.Name == DUMMY_NODE_KEY))
        {
            e.DrawDefault = false;
            return;
        }

        // Let the base class handle normal drawing
        e.DrawDefault = true;
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

                    if ((isHidden && !FileSystemTreeViewValues.ShowHiddenFiles) || (isSystem && !FileSystemTreeViewValues.ShowSystemFiles))
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
            if (FileSystemTreeViewValues.ShowFiles)
            {
                string[] files = Directory.GetFiles(directoryPath, FileSystemTreeViewValues.FileFilter);
                foreach (string file in files)
                {
                    try
                    {
                        FileInfo fileInfo = new FileInfo(file);
                        bool isHidden = (fileInfo.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden;
                        bool isSystem = (fileInfo.Attributes & FileAttributes.System) == FileAttributes.System;

                        if ((isHidden && !FileSystemTreeViewValues.ShowHiddenFiles) || (isSystem && !FileSystemTreeViewValues.ShowSystemFiles))
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

    #endregion
}

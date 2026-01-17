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
/// Provides a Krypton-styled ListView control for browsing the file system with proper icons.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonListView))]
[DefaultEvent(nameof(SelectedIndexChanged))]
[DefaultProperty(nameof(FileSystemListViewValues.CurrentPath))]
[DesignerCategory(@"code")]
[Description(@"Displays a file system list with folder and file icons.")]
[Docking(DockingBehavior.Ask)]
public class KryptonFileSystemListView : KryptonListView
{
    #region Instance Fields

    private readonly ImageList _imageList;
    private readonly Dictionary<string, int> _iconCache;
    private readonly FileSystemListViewValues _fileSystemValues;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when the current path changes.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when the current path changes.")]
    public event EventHandler? PathChanged;

    /// <summary>
    /// Occurs when an error occurs while loading the file system.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when an error occurs while loading the file system.")]
    public event EventHandler<FileSystemErrorEventArgs>? FileSystemError;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonFileSystemListView"/> class.
    /// </summary>
    public KryptonFileSystemListView()
    {
        _iconCache = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        _imageList = new ImageList
        {
            ColorDepth = ColorDepth.Depth32Bit,
            ImageSize = new Size(16, 16)
        };

        // Create the expandable properties object
        _fileSystemValues = new FileSystemListViewValues(this);

        // Add a default folder icon
        AddDefaultIcon();

        SmallImageList = _imageList;
        LargeImageList = _imageList;

        // Set default properties
        View = View.Details;
        FullRowSelect = true;
        GridLines = true;
        AllowColumnReorder = true;

        // Setup default columns
        SetupColumns();

        // Wire up events
        DoubleClick += OnDoubleClick;
        SelectedIndexChanged += OnSelectedIndexChanged;

        // Monitor View property changes
        View = View.Details; // Set after wiring events
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets the file system ListView values.
    /// </summary>
    /// <value>
    /// The file system ListView values.
    /// </value>
    public FileSystemListViewValues FileSystemListViewValues => _fileSystemValues;

    /*/// <summary>
    /// Gets or sets the current directory path.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The current directory path being displayed.")]
    [DefaultValue("")]
    public string CurrentPath
    {
        get => _fileSystemValues.CurrentPath;
        set
        {
            if (_fileSystemValues.CurrentPath != value)
            {
                NavigateTo(value);
            }
        }
    }

    /// <summary>
    /// Gets the full path of the currently selected file or folder.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string? SelectedPath
    {
        get
        {
            if (SelectedItems.Count > 0)
            {
                return SelectedItems[0].Tag as string;
            }
            return null;
        }
    }

    /// <summary>
    /// Gets an array of selected file paths.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string[] SelectedPaths
    {
        get
        {
            if (SelectedItems.Count > 0)
            {
                var paths = new string[SelectedItems.Count];
                for (int i = 0; i < SelectedItems.Count; i++)
                {
                    paths[i] = SelectedItems[i].Tag as string ?? string.Empty;
                }
                return paths;
            }
            return Array.Empty<string>();
        }
    }*/

    internal Dictionary<string, int> IconCache => _iconCache;

    #endregion

    #region Public Methods

    /// <summary>
    /// Reloads the list view from the current path.
    /// </summary>
    public void Reload()
    {
        Items.Clear();
        _iconCache.Clear();
        _imageList.Images.Clear();
        AddDefaultIcon();

        if (string.IsNullOrEmpty(_fileSystemValues.CurrentPath) || !Directory.Exists(_fileSystemValues.CurrentPath))
        {
            return;
        }

        try
        {
            LoadDirectory(_fileSystemValues.CurrentPath);

            // Auto-resize columns after loading if in Details view
            if (View == View.Details && Columns.Count > 0)
            {
                // Auto-resize the name column to content, others to header
                if (Items.Count > 0)
                {
                    AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.ColumnContent);
                }
                else
                {
                    AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
                }
            }
        }
        catch (Exception ex)
        {
            OnFileSystemError(new FileSystemErrorEventArgs(_fileSystemValues.CurrentPath, ex));
        }
    }

    /// <summary>
    /// Navigates to the specified path in the list view.
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
            OnFileSystemError(new FileSystemErrorEventArgs(path, new DirectoryNotFoundException($"{KryptonManager.Strings.FileSystemListViewStrings.DirectoryNotFoundMessage}: {path}")));
            return;
        }

        _fileSystemValues.CurrentPath = path;
        Reload();
        OnPathChanged(EventArgs.Empty);
    }

    /// <summary>
    /// Adds a default icon to the image list.
    /// </summary>
    public void AddDefaultIcon()
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
                        try
                        {
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
                        }
                        finally
                        {
                            bitmapToAdd.Dispose();
                        }
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
    /// Raises the FileSystemError event.
    /// </summary>
    /// <param name="e">A FileSystemErrorEventArgs containing the event data.</param>
    protected virtual void OnFileSystemError(FileSystemErrorEventArgs e)
    {
        FileSystemError?.Invoke(this, e);
    }

    #endregion

    #region Implementation

    private void SetupColumns()
    {
        // Only setup columns if in Details view
        if (View != View.Details)
        {
            return;
        }

        Columns.Clear();

        // Add columns with appropriate widths
        ColumnHeader nameColumn = new ColumnHeader
        {
            Text = KryptonManager.Strings.FileSystemListViewStrings.ColumnNameName,
            Width = 250,
            TextAlign = HorizontalAlignment.Left
        };
        Columns.Add(nameColumn);

        ColumnHeader typeColumn = new ColumnHeader
        {
            Text = KryptonManager.Strings.FileSystemListViewStrings.ColumnTypeName,
            Width = 100,
            TextAlign = HorizontalAlignment.Left
        };
        Columns.Add(typeColumn);

        ColumnHeader sizeColumn = new ColumnHeader
        {
            Text = KryptonManager.Strings.FileSystemListViewStrings.ColumnSizeName,
            Width = 100,
            TextAlign = HorizontalAlignment.Right
        };
        Columns.Add(sizeColumn);

        ColumnHeader modifiedColumn = new ColumnHeader
        {
            Text = KryptonManager.Strings.FileSystemListViewStrings.ColumnDateModifiedName,
            Width = 150,
            TextAlign = HorizontalAlignment.Left
        };
        Columns.Add(modifiedColumn);
    }

    private void LoadDirectory(string directoryPath)
    {
        try
        {
            // Add parent directory item
            DirectoryInfo? parent = Directory.GetParent(directoryPath);
            if (parent != null)
            {
                ListViewItem parentItem = CreateDirectoryItem("..", parent.FullName);
                Items.Add(parentItem);
            }

            // Add directories
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

                    ListViewItem dirItem = CreateDirectoryItem(dirInfo.Name, dirInfo.FullName);
                    Items.Add(dirItem);
                }
                catch (UnauthorizedAccessException ex)
                {
                    OnFileSystemError(new FileSystemErrorEventArgs(dir, ex));
                }
            }

            // Add files
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

                        ListViewItem fileItem = CreateFileItem(fileInfo);
                        Items.Add(fileItem);
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

    private ListViewItem CreateDirectoryItem(string name, string fullPath)
    {
        ListViewItem item = new ListViewItem(name)
        {
            Tag = fullPath,
            ImageIndex = GetIconIndex(fullPath, true)
        };

        // Type column
        item.SubItems.Add("File folder");

        // Size column (empty for directories)
        item.SubItems.Add("");

        // Date Modified column
        try
        {
            DateTime lastWrite = Directory.GetLastWriteTime(fullPath);
            item.SubItems.Add(lastWrite.ToString("g"));
        }
        catch
        {
            item.SubItems.Add("");
        }

        return item;
    }

    private ListViewItem CreateFileItem(FileInfo fileInfo)
    {
        ListViewItem item = new ListViewItem(fileInfo.Name)
        {
            Tag = fileInfo.FullName,
            ImageIndex = GetIconIndex(fileInfo.FullName, false)
        };

        // Type column - get file extension description
        string fileType = GetFileTypeDescription(fileInfo.Extension);
        item.SubItems.Add(fileType);

        // Size column - format file size
        item.SubItems.Add(FormatFileSize(fileInfo.Length));

        // Date Modified column
        item.SubItems.Add(fileInfo.LastWriteTime.ToString("g"));

        return item;
    }

    private static string GetFileTypeDescription(string extension)
    {
        if (string.IsNullOrEmpty(extension))
        {
            return "File";
        }

        // Remove the dot and convert to uppercase for display
        string ext = extension.TrimStart('.').ToUpperInvariant();

        // Common file type descriptions
        return ext switch
        {
            "TXT" => "Text Document",
            "DOC" or "DOCX" => "Microsoft Word Document",
            "XLS" or "XLSX" => "Microsoft Excel Worksheet",
            "PPT" or "PPTX" => "Microsoft PowerPoint Presentation",
            "PDF" => "PDF Document",
            "ZIP" or "RAR" or "7Z" => "Compressed Folder",
            "JPG" or "JPEG" or "PNG" or "GIF" or "BMP" => "Image File",
            "MP3" or "WAV" or "WMA" => "Audio File",
            "MP4" or "AVI" or "WMV" => "Video File",
            "EXE" => "Application",
            "DLL" => "Application Extension",
            "CS" => "C# Source File",
            "VB" => "Visual Basic Source File",
            "JS" => "JavaScript File",
            "HTML" or "HTM" => "HTML Document",
            "CSS" => "Cascading Style Sheet",
            "XML" => "XML Document",
            "JSON" => "JSON Document",
            _ => $"{ext} File"
        };
    }

    private static string FormatFileSize(long bytes)
    {
        string[] sizes = { "B", "KB", "MB", "GB", "TB" };
        double len = bytes;
        int order = 0;

        while (len >= 1024 && order < sizes.Length - 1)
        {
            order++;
            len = len / 1024;
        }

        // Format with appropriate decimal places
        string format = order == 0 ? "N0" : "N2";
        return $"{len.ToString(format)} {sizes[order]}";
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
                    icon = FileSystemIconHelper.GetFileSystemIcon(path, _fileSystemValues.UseLargeIcons);
                }

                // Fallback to generic folder icon
                if (icon == null)
                {
                    icon = FileSystemIconHelper.GetFolderIcon(_fileSystemValues.UseLargeIcons);
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
                    icon = FileSystemIconHelper.GetFileSystemIcon(path, _fileSystemValues.UseLargeIcons);
                }
                else
                {
                    string extension = Path.GetExtension(path);
                    if (!string.IsNullOrEmpty(extension))
                    {
                        icon = FileSystemIconHelper.GetFileIcon(extension, _fileSystemValues.UseLargeIcons);
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
                        Bitmap? bitmapToAdd = CreateBitmapForImageList(sourceBitmap);

                        if (bitmapToAdd != null)
                        {
                            try
                            {
                                int index = _imageList.Images.Count;
                                _imageList.Images.Add(bitmapToAdd);
                                // Force ImageList to create handle and copy the bitmap immediately
                                _ = _imageList.Handle;

                                // Cache the index
                                _iconCache[cacheKey] = index;

                                return index;
                            }
                            finally
                            {
                                bitmapToAdd.Dispose();
                            }
                        }
                    }
                    finally
                    {
                        sourceBitmap.Dispose();
                    }
                }
            }
            catch
            {
                // Icon retrieval or bitmap creation failed
            }
        }

        // Fallback to default icon (index 0)
        return 0;
    }

    private Bitmap? CreateBitmapForImageList(Bitmap sourceBitmap)
    {
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

            // Validate
            if (bitmapToAdd.Width > 0 && bitmapToAdd.Height > 0)
            {
                return bitmapToAdd;
            }
            else
            {
                bitmapToAdd.Dispose();
                return null;
            }
        }
        catch
        {
            return null;
        }
    }

    private void OnDoubleClick(object? sender, EventArgs e)
    {
        if (SelectedItems.Count > 0)
        {
            string? path = SelectedItems[0].Tag as string;
            if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
            {
                NavigateTo(path!);
            }
        }
    }

    private void OnSelectedIndexChanged(object? sender, EventArgs e)
    {
        // Event is already exposed by base class
    }

    /// <summary>
    /// Gets or sets how items are displayed in the control.
    /// </summary>
    [DefaultValue(View.Details)] 
    public new View View
    {
        get => base.View;
        set
        {
            if (base.View != value)
            {
                base.View = value;

                // Setup columns when switching to Details view
                if (value == View.Details && Columns.Count == 0)
                {
                    SetupColumns();
                }
            }
        }
    }

    /// <summary>
    /// Auto-resizes all columns to fit their content.
    /// </summary>
    public void AutoResizeAllColumns()
    {
        if (View == View.Details && Columns.Count > 0)
        {
            AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }
    }

    /// <summary>
    /// Auto-resizes all columns to fit the header text.
    /// </summary>
    public void AutoResizeColumnHeaders()
    {
        if (View == View.Details && Columns.Count > 0)
        {
            AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }
    }

    #endregion
}
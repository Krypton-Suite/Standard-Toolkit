namespace Krypton.Toolkit;

/// <summary>
/// Groups file system list view specific properties for display in the PropertyGrid.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class FileSystemListViewValues : Storage
{
    #region Instance Fields

    private string _currentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    private bool _showFiles = true;
    private bool _showHiddenFiles = false;
    private bool _showSystemFiles = false;
    private string _fileFilter = "*.*";
    private bool _useLargeIcons = false;

    private readonly InternalKryptonFileSystemListView _owner;

    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="FileSystemListViewValues"/> class.
    /// </summary>
    /// <param name="owner">The owner.</param>
    internal FileSystemListViewValues(InternalKryptonFileSystemListView owner)
    {
        _owner = owner;
    }

    /// <summary>
    /// Gets or sets the current directory path to display in the list view.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The current directory path to display in the list view.")]
    [DefaultValue("")]
    public string CurrentPath
    {
        get => _currentPath;
        set
        {
            if (_currentPath != value)
            {
                _currentPath = value ?? string.Empty;
                _owner.Reload();
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether files should be displayed in the list view.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether files should be displayed in the list view.")]
    [DefaultValue(true)]
    public bool ShowFiles
    {
        get => _showFiles;
        set
        {
            if (_showFiles != value)
            {
                _showFiles = value;
                _owner.Reload();
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether hidden files should be displayed.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether hidden files should be displayed.")]
    [DefaultValue(false)]
    public bool ShowHiddenFiles
    {
        get => _showHiddenFiles;
        set
        {
            if (_showHiddenFiles != value)
            {
                _showHiddenFiles = value;
                _owner.Reload();
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether system files should be displayed.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether system files should be displayed.")]
    [DefaultValue(false)]
    public bool ShowSystemFiles
    {
        get => _showSystemFiles;
        set
        {
            if (_showSystemFiles != value)
            {
                _showSystemFiles = value;
                _owner.Reload();
            }
        }
    }

    /// <summary>
    /// Gets or sets the file filter to apply when showing files (e.g., "*.txt" or "*.txt;*.doc").
    /// </summary>
    [Category(@"Behavior")]
    [Description("The file filter to apply when showing files (e.g., \"*.txt\" or \"*.txt;*.doc\").")]
    [DefaultValue("*.*")]
    public string FileFilter
    {
        get => _fileFilter;
        set
        {
            if (_fileFilter != value)
            {
                _fileFilter = value ?? "*.*";
                if (_showFiles)
                {
                    _owner.Reload();
                }
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to use large icons (32x32) instead of small icons (16x16).
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Indicates whether to use large icons (32x32) instead of small icons (16x16).")]
    [DefaultValue(false)]
    public bool UseLargeIcons
    {
        get => _useLargeIcons;
        set
        {
            if (_useLargeIcons != value)
            {
                _useLargeIcons = value;

                // Update ImageList size
                Size newSize = value ? new Size(32, 32) : new Size(16, 16);
                if (_owner.SmallImageList?.ImageSize != newSize)
                {
                    if (_owner.SmallImageList != null)
                    {
                        _owner.SmallImageList.ImageSize = newSize;
                    }
                    if (_owner.LargeImageList != null)
                    {
                        _owner.LargeImageList.ImageSize = newSize;
                    }

                    // Clear cache and reload
                    _owner.IconCache.Clear();
                    _owner.SmallImageList?.Images.Clear();
                    _owner.LargeImageList?.Images.Clear();
                    _owner.AddDefaultIcon();

                    if (!string.IsNullOrEmpty(_currentPath))
                    {
                        _owner.Reload();
                    }
                }
            }
        }
    }

    public override bool IsDefault => throw new NotImplementedException();

    /// <summary>
    /// Returns a string representation of this object.
    /// </summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString() => "File System List View Values";
}
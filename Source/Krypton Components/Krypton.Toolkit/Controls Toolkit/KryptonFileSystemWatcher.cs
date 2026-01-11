#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using FileSystemWatcher = System.IO.FileSystemWatcher;

namespace Krypton.Toolkit;

/// <summary>
/// Listens to the file system change notifications and raises events when a directory, or file in a directory, changes with Krypton integration.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(FileSystemWatcher))]
[DefaultEvent(nameof(Created))]
[DefaultProperty(nameof(Path))]
[DesignerCategory(@"code")]
[Description(@"Listens to the file system change notifications and raises events when a directory, or file in a directory, changes.")]
public class KryptonFileSystemWatcher : Component
{
    #region Instance Fields

    private readonly FileSystemWatcher _fileSystemWatcher;
    private PaletteBase? _palette;
    private PaletteMode _paletteMode;
    private bool _disposed;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when a file or directory in the specified Path is created.
    /// </summary>
    [Category(@"File System")]
    [Description(@"Occurs when a file or directory in the specified Path is created.")]
    public event FileSystemEventHandler? Created;

    /// <summary>
    /// Occurs when a file or directory in the specified Path is changed.
    /// </summary>
    [Category(@"File System")]
    [Description(@"Occurs when a file or directory in the specified Path is changed.")]
    public event FileSystemEventHandler? Changed;

    /// <summary>
    /// Occurs when a file or directory in the specified Path is deleted.
    /// </summary>
    [Category(@"File System")]
    [Description(@"Occurs when a file or directory in the specified Path is deleted.")]
    public event FileSystemEventHandler? Deleted;

    /// <summary>
    /// Occurs when a file or directory in the specified Path is renamed.
    /// </summary>
    [Category(@"File System")]
    [Description(@"Occurs when a file or directory in the specified Path is renamed.")]
    public event RenamedEventHandler? Renamed;

    /// <summary>
    /// Occurs when the internal buffer overflows.
    /// </summary>
    [Category(@"File System")]
    [Description(@"Occurs when the internal buffer overflows.")]
    public event ErrorEventHandler? Error;

    /// <summary>
    /// Raises the Created event.
    /// </summary>
    protected virtual void OnCreated(FileSystemEventArgs e) => Created?.Invoke(this, e);

    /// <summary>
    /// Raises the Changed event.
    /// </summary>
    protected virtual void OnChanged(FileSystemEventArgs e) => Changed?.Invoke(this, e);

    /// <summary>
    /// Raises the Deleted event.
    /// </summary>
    protected virtual void OnDeleted(FileSystemEventArgs e) => Deleted?.Invoke(this, e);

    /// <summary>
    /// Raises the Renamed event.
    /// </summary>
    protected virtual void OnRenamed(RenamedEventArgs e) => Renamed?.Invoke(this, e);

    /// <summary>
    /// Raises the Error event.
    /// </summary>
    protected virtual void OnError(ErrorEventArgs e) => Error?.Invoke(this, e);

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the KryptonFileSystemWatcher class.
    /// </summary>
    public KryptonFileSystemWatcher()
    {
        _fileSystemWatcher = new FileSystemWatcher();
        _fileSystemWatcher.Created += OnFileSystemWatcherCreated;
        _fileSystemWatcher.Changed += OnFileSystemWatcherChanged;
        _fileSystemWatcher.Deleted += OnFileSystemWatcherDeleted;
        _fileSystemWatcher.Renamed += OnFileSystemWatcherRenamed;
        _fileSystemWatcher.Error += OnFileSystemWatcherError;

        _paletteMode = PaletteMode.Global;
        _palette = KryptonManager.CurrentGlobalPalette;

        // Hook into global palette changes
        KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;
    }

    /// <summary>
    /// Initialize a new instance of the KryptonFileSystemWatcher class with a container.
    /// </summary>
    /// <param name="container">The IContainer that will contain this file system watcher.</param>
    public KryptonFileSystemWatcher(IContainer container)
        : this()
    {
        container.Add(this);
    }

    /// <summary>
    /// Initialize a new instance of the KryptonFileSystemWatcher class with the specified path.
    /// </summary>
    /// <param name="path">The directory to monitor, in standard or Universal Naming Convention (UNC) notation.</param>
    public KryptonFileSystemWatcher(string path)
        : this()
    {
        Path = path;
    }

    /// <summary>
    /// Initialize a new instance of the KryptonFileSystemWatcher class with the specified path and filter.
    /// </summary>
    /// <param name="path">The directory to monitor, in standard or Universal Naming Convention (UNC) notation.</param>
    /// <param name="filter">The type of files to watch. For example, "*.txt" watches for changes to all text files.</param>
    public KryptonFileSystemWatcher(string path, string filter)
        : this()
    {
        Path = path;
        Filter = filter;
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the path of the directory to watch.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The path of the directory to watch.")]
    [DefaultValue("")]
    [Editor(typeof(FolderNameEditor), typeof(UITypeEditor))]
    public string Path
    {
        get => _fileSystemWatcher.Path ?? string.Empty;
        set
        {
            _fileSystemWatcher.Path = value;
        }
    }

    /// <summary>
    /// Gets or sets the filter string used to determine what files are monitored in a directory.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The filter string used to determine what files are monitored in a directory.")]
    [DefaultValue("*.*")]
    public string Filter
    {
        get => _fileSystemWatcher.Filter ?? "*.*";
        set
        {
            _fileSystemWatcher.Filter = value;
        }
    }

    /// <summary>
    /// Gets or sets the type of changes to watch for.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The type of changes to watch for.")]
    [DefaultValue(NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.LastWrite)]
    public NotifyFilters NotifyFilter
    {
        get => _fileSystemWatcher.NotifyFilter;
        set
        {
            _fileSystemWatcher.NotifyFilter = value;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the component is enabled.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether the component is enabled.")]
    [DefaultValue(false)]
    public bool EnableRaisingEvents
    {
        get => _fileSystemWatcher.EnableRaisingEvents;
        set
        {
            _fileSystemWatcher.EnableRaisingEvents = value;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether subdirectories within the specified path should be monitored.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether subdirectories within the specified path should be monitored.")]
    [DefaultValue(false)]
    public bool IncludeSubdirectories
    {
        get => _fileSystemWatcher.IncludeSubdirectories;
        set
        {
            _fileSystemWatcher.IncludeSubdirectories = value;
        }
    }

    /// <summary>
    /// Gets or sets the size (in bytes) of the internal buffer.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The size (in bytes) of the internal buffer.")]
    [DefaultValue(8192)]
    public int InternalBufferSize
    {
        get => _fileSystemWatcher.InternalBufferSize;
        set
        {
            _fileSystemWatcher.InternalBufferSize = value;
        }
    }

    /// <summary>
    /// Gets or sets the object used to marshal the event handler calls that are issued as a result of a directory change.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The object used to marshal the event handler calls that are issued as a result of a directory change.")]
    [DefaultValue(null)]
    [Browsable(false)]
    public ISynchronizeInvoke? SynchronizingObject
    {
        get => _fileSystemWatcher.SynchronizingObject;
        set
        {
            _fileSystemWatcher.SynchronizingObject = value;
        }
    }

    /// <summary>
    /// Gets or sets the palette mode.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Sets the palette mode.")]
    [DefaultValue(PaletteMode.Global)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public PaletteMode PaletteMode
    {
        get => _paletteMode;
        set
        {
            if (_paletteMode != value)
            {
                switch (value)
                {
                    case PaletteMode.Custom:
                        // Do nothing, you must have a palette to set
                        break;
                    default:
                        _paletteMode = value;
                        _palette = KryptonManager.GetPaletteForMode(_paletteMode);
                        break;
                }
            }
        }
    }

    /// <summary>
    /// Gets and sets the custom palette.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Sets the custom palette to be used.")]
    [DefaultValue(null)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public PaletteBase? Palette
    {
        get => _paletteMode == PaletteMode.Custom ? _palette : null;
        set
        {
            if (_palette != value)
            {
                _palette = value;

                if (value == null)
                {
                    _paletteMode = PaletteMode.Global;
                    _palette = KryptonManager.CurrentGlobalPalette;
                }
                else
                {
                    _paletteMode = PaletteMode.Custom;
                }
            }
        }
    }

    /// <summary>
    /// Gets access to the underlying FileSystemWatcher component.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public FileSystemWatcher FileSystemWatcher => _fileSystemWatcher;

    /// <summary>
    /// Begins the watching of the specified directory.
    /// </summary>
    public void BeginInit()
    {
        _fileSystemWatcher.BeginInit();
    }

    /// <summary>
    /// Ends the initialization of a FileSystemWatcher used on a form or used by another component.
    /// </summary>
    public void EndInit()
    {
        _fileSystemWatcher.EndInit();
    }

    /// <summary>
    /// A synchronous method that returns a structure that contains specific information on the change that occurred, given the type of change you want to monitor.
    /// </summary>
    /// <param name="changeType">The type of change to watch for.</param>
    /// <returns>A WaitForChangedResult that contains specific information on the change that occurred.</returns>
    public WaitForChangedResult WaitForChanged(WatcherChangeTypes changeType) => _fileSystemWatcher.WaitForChanged(changeType);

    /// <summary>
    /// A synchronous method that returns a structure that contains specific information on the change that occurred, given the type of change you want to monitor and the time (in milliseconds) to wait before timing out.
    /// </summary>
    /// <param name="changeType">The type of change to watch for.</param>
    /// <param name="timeout">The time (in milliseconds) to wait before timing out.</param>
    /// <returns>A WaitForChangedResult that contains specific information on the change that occurred.</returns>
    public WaitForChangedResult WaitForChanged(WatcherChangeTypes changeType, int timeout) => _fileSystemWatcher.WaitForChanged(changeType, timeout);

    #endregion

    #region Implementation

    private void OnFileSystemWatcherCreated(object? sender, FileSystemEventArgs e)
    {
        OnCreated(e);
    }

    private void OnFileSystemWatcherChanged(object? sender, FileSystemEventArgs e)
    {
        OnChanged(e);
    }

    private void OnFileSystemWatcherDeleted(object? sender, FileSystemEventArgs e)
    {
        OnDeleted(e);
    }

    private void OnFileSystemWatcherRenamed(object? sender, RenamedEventArgs e)
    {
        OnRenamed(e);
    }

    private void OnFileSystemWatcherError(object? sender, ErrorEventArgs e)
    {
        OnError(e);
    }

    private void OnGlobalPaletteChanged(object? sender, EventArgs e)
    {
        // Only update if we're using the global palette
        if (_paletteMode == PaletteMode.Global)
        {
            _palette = KryptonManager.CurrentGlobalPalette;
        }
    }

    #endregion

    #region Disposal

    private new void Dispose(bool isDisposing)
    {
        if (!_disposed)
        {
            if (isDisposing)
            {
                _fileSystemWatcher?.Dispose();
            }

            _disposed = true;
        }
    }

    ~KryptonFileSystemWatcher() => Dispose(false);

    /// <summary>
    /// Dispose and garbage collection.
    /// </summary>
    public new void Dispose()
    {
        Dispose(true);

        GC.SuppressFinalize(this);
    }

    #endregion
}

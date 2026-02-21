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
/// Groups file system tree view specific properties for display in the PropertyGrid.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class FileSystemTreeViewValues : Storage
{
    #region Instance Fields

    private FileSystemRootMode _rootMode = FileSystemRootMode.Drives;
    private string _rootPath = string.Empty;
    private bool _showFiles = true;
    private bool _showHiddenFiles = false;
    private bool _showSystemFiles = false;
    private string _fileFilter = "*.*";
    private bool _showSpecialFolders = true;

    private readonly KryptonFileSystemTreeView _owner;

    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="FileSystemTreeViewValues"/> class.
    /// </summary>
    /// <param name="owner">The owner.</param>
    internal FileSystemTreeViewValues(KryptonFileSystemTreeView owner)
    {
        _owner = owner;
    }

    /// <summary>
    /// Gets or sets the root mode for the tree view.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Determines the root display mode: Desktop (Explorer-style with special folders), Computer (drives only), Drives (all drives), or CustomPath (use RootPath).")]
    [DefaultValue(FileSystemRootMode.Drives)]
    public FileSystemRootMode RootMode
    {
        get => _rootMode;
        set
        {
            if (_rootMode != value)
            {
                _rootMode = value;
                _owner.Reload();
            }
        }
    }

    /// <summary>
    /// Gets or sets the root directory path to display in the tree view (used when RootMode is CustomPath).
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The root directory path to display in the tree view (used when RootMode is CustomPath).")]
    [DefaultValue("")]
    public string RootPath
    {
        get => _rootPath;
        set
        {
            if (_rootPath != value)
            {
                _rootPath = value ?? string.Empty;
                _owner.Reload();
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether files should be displayed in the tree view.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether files should be displayed in the tree view.")]
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
    /// Gets or sets a value indicating whether special folders (Desktop, Computer, Network, Recycle Bin, etc.) should be displayed when RootMode is Desktop.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether special folders (Desktop, Computer, Network, Recycle Bin, etc.) should be displayed when RootMode is Desktop.")]
    [DefaultValue(true)]
    public bool ShowSpecialFolders
    {
        get => _showSpecialFolders;
        set
        {
            if (_showSpecialFolders != value)
            {
                _showSpecialFolders = value;
                if (_rootMode == FileSystemRootMode.Desktop)
                {
                    _owner.Reload();
                }
            }
        }
    }

    public override bool IsDefault => throw new NotImplementedException();

    /// <summary>
    /// Returns a string representation of this object.
    /// </summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString() => IsDefault ? string.Empty : "Modified";
}

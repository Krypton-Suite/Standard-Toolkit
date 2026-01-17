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
/// Groups explorer browser specific properties for display in the PropertyGrid.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class ExplorerBrowserValues : Storage
{
    #region Instance Fields

    private bool _showFiles = true;
    private bool _showHiddenFiles = false;
    private bool _showSystemFiles = false;
    private string _fileFilter = "*.*";

    private readonly KryptonExplorerBrowser _owner;

    #endregion

    #region Identity

    /// <summary>
    /// Initializes a new instance of the <see cref="ExplorerBrowserValues"/> class.
    /// </summary>
    /// <param name="owner">The owner.</param>
    internal ExplorerBrowserValues(KryptonExplorerBrowser owner)
    {
        _owner = owner;
    }

    #endregion

    #region Public

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
                UpdateControls();
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
                UpdateControls();
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
                UpdateControls();
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
                UpdateControls();
            }
        }
    }

    #endregion

    #region IsDefault

    /// <inheritdoc />
    [Browsable(false)]
    public override bool IsDefault => ShowFiles.Equals(true) &&
                                      ShowHiddenFiles.Equals(false) &&
                                      ShowSystemFiles.Equals(false) &&
                                      FileFilter.Equals("*.*");

    #endregion

    /// <summary>
    /// Returns a string representation of this object.
    /// </summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString() => IsDefault ? string.Empty : "Modified";

    private void UpdateControls()
    {
        if (_owner.TreeView != null)
        {
            _owner.TreeView.FileSystemTreeViewValues.ShowFiles = _showFiles;
            _owner.TreeView.FileSystemTreeViewValues.ShowHiddenFiles = _showHiddenFiles;
            _owner.TreeView.FileSystemTreeViewValues.ShowSystemFiles = _showSystemFiles;
            _owner.TreeView.FileSystemTreeViewValues.FileFilter = _fileFilter;
        }
        if (_owner.ListView != null)
        {
            _owner.ListView.FileSystemListViewValues.ShowFiles = _showFiles;
            _owner.ListView.FileSystemListViewValues.ShowHiddenFiles = _showHiddenFiles;
            _owner.ListView.FileSystemListViewValues.ShowSystemFiles = _showSystemFiles;
            _owner.ListView.FileSystemListViewValues.FileFilter = _fileFilter;
        }
        _owner.Refresh();
    }
}
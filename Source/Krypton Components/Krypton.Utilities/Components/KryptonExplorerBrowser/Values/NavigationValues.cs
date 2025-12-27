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
/// Groups navigation-related properties for display in the PropertyGrid.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class NavigationValues : Storage
{
    #region Instance Fields

    private string _currentPath = string.Empty;
    private int _maxHistorySize = 50;

    private readonly KryptonExplorerBrowser _owner;

    #endregion

    #region Identity

    /// <summary>
    /// Initializes a new instance of the <see cref="NavigationValues"/> class.
    /// </summary>
    /// <param name="owner">The owner.</param>
    internal NavigationValues(KryptonExplorerBrowser owner)
    {
        _owner = owner;
    }

    #endregion

    #region Public

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
                _currentPath = value ?? string.Empty;
                _owner.NavigateTo(_currentPath);
            }
        }
    }

    /// <summary>
    /// Gets or sets the maximum number of items in navigation history.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The maximum number of items in navigation history.")]
    [DefaultValue(50)]
    public int MaxHistorySize
    {
        get => _maxHistorySize;
        set
        {
            if (_maxHistorySize != value && value > 0)
            {
                _maxHistorySize = value;
            }
        }
    }

    #endregion

    /// <inheritdoc />
    [Browsable(false)]
    public override bool IsDefault => CurrentPath.Equals(string.Empty) &&
                                      MaxHistorySize == 50;

    /// <summary>
    /// Returns a string representation of this object.
    /// </summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString() => IsDefault ? string.Empty : "Modified";
}
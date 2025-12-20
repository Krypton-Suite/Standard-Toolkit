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
/// Groups display-related properties for display in the PropertyGrid.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class DisplayValues : Storage
{
    #region Instance Fields

    private View _viewMode = View.Details;
    private SelectionMode _selectionMode = SelectionMode.One;
    private bool _showToolbar = true;
    private bool _showStatusBar = true;
    private bool _showTreeView = true;

    private readonly KryptonExplorerBrowser _owner;

    #endregion

    #region Identity

    /// <summary>
    /// Initializes a new instance of the <see cref="DisplayValues"/> class.
    /// </summary>
    /// <param name="owner">The owner.</param>
    internal DisplayValues(KryptonExplorerBrowser owner)
    {
        _owner = owner;
    }

    #endregion

    #region Public

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
                if (_owner.ListView != null)
                {
                    _owner.ListView.View = value;
                }
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
                if (_owner.ListView != null)
                {
                    _owner.ListView.MultiSelect = value != SelectionMode.One;
                }
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the toolbar is visible.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Indicates whether the toolbar is visible.")]
    [DefaultValue(true)]
    public bool ShowToolbar
    {
        get => _showToolbar;
        set
        {
            if (_showToolbar != value)
            {
                _showToolbar = value;
                if (_owner._toolbarPanel != null)
                {
                    _owner._toolbarPanel.Visible = value;
                }
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the status bar is visible.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Indicates whether the status bar is visible.")]
    [DefaultValue(true)]
    public bool ShowStatusBar
    {
        get => _showStatusBar;
        set
        {
            if (_showStatusBar != value)
            {
                _showStatusBar = value;
                if (_owner._statusPanel != null)
                {
                    _owner._statusPanel.Visible = value;
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
                if (_owner._splitContainer != null)
                {
                    _owner._splitContainer.Panel1Collapsed = !value;
                }
            }
        }
    }

    #endregion

    #region IsDefault

    [Browsable(false)]
    public override bool IsDefault => ViewMode.Equals(View.Details) &&
                                      SelectionMode.Equals(SelectionMode.One) &&
                                      ShowToolbar.Equals(true) &&
                                      ShowStatusBar.Equals(true) &&
                                      ShowTreeView.Equals(true);

    #endregion

    /// <summary>
    /// Returns a string representation of this object.
    /// </summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString() => IsDefault ? string.Empty : "Modified";
}
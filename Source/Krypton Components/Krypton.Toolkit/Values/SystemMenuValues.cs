#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Storage for system menu value information.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class SystemMenuValues : Storage, INotifyPropertyChanged
{
    #region Static Fields
    private const bool DEFAULT_ENABLED = true;
    //private const bool DEFAULT_SHOW_ON_LEFT_CLICK = false;
    //private const bool DEFAULT_USE__SYSTEM_MENU = true;
    private const bool DEFAULT_SHOW_ON_RIGHT_CLICK = true;
    private const bool DEFAULT_SHOW_ON_ALT_SPACE = true;
    private const bool DEFAULT_SHOW_ON_ICON_CLICK = true;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;
    #endregion

    #region Instance Fields
    private bool _enabled;
    //private bool _showOnLeftClick;
    //private bool _useSystemMenu;
    private bool _showOnRightClick;
    private bool _showOnAltSpace;
    private bool _showOnIconClick;
    private SystemMenuItemCollection? _customMenuItems;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the SystemMenuValues class.
    /// </summary>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public SystemMenuValues(NeedPaintHandler needPaint)
    {
        // Store the provided paint notification delegate
        NeedPaint = needPaint;

        // Set initial values
        _enabled = DEFAULT_ENABLED;
        //_showOnLeftClick = DEFAULT_SHOW_ON_LEFT_CLICK;
        //_useSystemMenu = DEFAULT_USE__SYSTEM_MENU;
        _showOnRightClick = DEFAULT_SHOW_ON_RIGHT_CLICK;
        _showOnAltSpace = DEFAULT_SHOW_ON_ALT_SPACE;
        _showOnIconClick = DEFAULT_SHOW_ON_ICON_CLICK;

        // Initialize custom menu items collection
        _customMenuItems = new SystemMenuItemCollection();
        _customMenuItems.CollectionChanged += OnCustomMenuItemsChanged;
    }

    /// <summary>
    /// Initialize a new instance of the SystemMenuValues class for designer serialization.
    /// </summary>
    public SystemMenuValues() : this(null!)
    {
        // This constructor is required for designer serialization
        // The NeedPaint delegate will be set later by the designer
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => (Enabled == DEFAULT_ENABLED) &&
                                     //(ShowOnLeftClick == DEFAULT_SHOW_ON_LEFT_CLICK) &&
                                     //(UseSystemMenu == DEFAULT_USE__SYSTEM_MENU) &&
                                     (ShowOnRightClick == DEFAULT_SHOW_ON_RIGHT_CLICK) &&
                                     (ShowOnAltSpace == DEFAULT_SHOW_ON_ALT_SPACE) &&
                                     (ShowOnIconClick == DEFAULT_SHOW_ON_ICON_CLICK) &&
                                     (_customMenuItems?.Count == 0);
    #endregion

    #region Enabled
    /// <summary>
    /// Gets and sets whether the system menu is enabled.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Enables or disables the system menu.")]
    [DefaultValue(DEFAULT_ENABLED)]
    public bool Enabled
    {
        get => _enabled;

        set
        {
            if (_enabled != value)
            {
                _enabled = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Enabled)));
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeEnabled() => Enabled != DEFAULT_ENABLED;

    /// <summary>
    /// Resets the Enabled property to its default value.
    /// </summary>
    public void ResetEnabled() => Enabled = DEFAULT_ENABLED;
    #endregion

    #region ShowOnLeftClick
    /*/// <summary>
    /// Gets and sets whether left-click on title bar shows the system menu.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Determines if left-click on title bar shows the system menu.")]
    [DefaultValue(DEFAULT_SHOW_ON_LEFT_CLICK)]
    public bool ShowOnLeftClick
    {
        get => _showOnLeftClick;

        set
        {
            if (_showOnLeftClick != value)
            {
                _showOnLeftClick = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShowOnLeftClick)));
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeShowOnLeftClick() => ShowOnLeftClick != DEFAULT_SHOW_ON_LEFT_CLICK;

    /// <summary>
    /// Resets the ShowOnLeftClick property to its default value.
    /// </summary>
    public void ResetShowOnLeftClick() => ShowOnLeftClick = DEFAULT_SHOW_ON_LEFT_CLICK;*/
    #endregion

    #region UseSystemMenu

    /*/// <summary>
    /// Gets and sets whether to use the system menu instead of the default system menu.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Determines if the system menu is used instead of the default system menu.")]
    [DefaultValue(DEFAULT_USE__SYSTEM_MENU)]
    public bool UseSystemMenu
    {
        get => _useSystemMenu;
        set
        {
            if (_useSystemMenu != value)
            {
                _useSystemMenu = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UseSystemMenu)));
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeUseSystemMenu() => UseSystemMenu != DEFAULT_USE__SYSTEM_MENU;

    /// <summary>
    /// Resets the UseSystemMenu property to its default value.
    /// </summary>
    public void ResetUseSystemMenu() => UseSystemMenu = DEFAULT_USE__SYSTEM_MENU;*/

    #endregion

    #region ShowOnRightClick
    /// <summary>
    /// Gets and sets whether right-click on title bar shows the system menu.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Determines if right-click on title bar shows the system menu.")]
    [DefaultValue(DEFAULT_SHOW_ON_RIGHT_CLICK)]
    public bool ShowOnRightClick
    {
        get => _showOnRightClick;

        set
        {
            if (_showOnRightClick != value)
            {
                _showOnRightClick = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShowOnRightClick)));
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeShowOnRightClick() => ShowOnRightClick != DEFAULT_SHOW_ON_RIGHT_CLICK;

    /// <summary>
    /// Resets the ShowOnRightClick property to its default value.
    /// </summary>
    public void ResetShowOnRightClick() => ShowOnRightClick = DEFAULT_SHOW_ON_RIGHT_CLICK;
    #endregion

    #region ShowOnAltSpace
    /// <summary>
    /// Gets and sets whether Alt+Space shows the system menu.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Determines if Alt+Space shows the system menu.")]
    [DefaultValue(DEFAULT_SHOW_ON_ALT_SPACE)]
    public bool ShowOnAltSpace
    {
        get => _showOnAltSpace;

        set
        {
            if (_showOnAltSpace != value)
            {
                _showOnAltSpace = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShowOnAltSpace)));
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeShowOnAltSpace() => ShowOnAltSpace != DEFAULT_SHOW_ON_ALT_SPACE;

    /// <summary>
    /// Resets the ShowOnAltSpace property to its default value.
    /// </summary>
    public void ResetShowOnAltSpace() => ShowOnAltSpace = DEFAULT_SHOW_ON_ALT_SPACE;

    #endregion

    #region ShowOnIconClick
    /// <summary>
    /// Gets and sets whether left-click on title bar icon shows the system menu.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Determines if left-click on title bar icon shows the system menu.")]
    [DefaultValue(DEFAULT_SHOW_ON_ICON_CLICK)]
    public bool ShowOnIconClick
    {
        get => _showOnIconClick;

        set
        {
            if (_showOnIconClick != value)
            {
                _showOnIconClick = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShowOnIconClick)));
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeShowOnIconClick() => ShowOnIconClick != DEFAULT_SHOW_ON_ICON_CLICK;

    /// <summary>
    /// Resets the ShowOnIconClick property to its default value.
    /// </summary>
    public void ResetShowOnIconClick() => ShowOnIconClick = DEFAULT_SHOW_ON_ICON_CLICK;

    #endregion

    #region CustomMenuItems
    /// <summary>
    /// Gets the collection of custom menu items for the system menu.
    /// </summary>
    [Category(@"Menu Items")]
    [Description(@"Custom menu items to display in the system menu.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Editor(typeof(SystemMenuItemsEditor), typeof(UITypeEditor))]
    public SystemMenuItemCollection? CustomMenuItems
    {
        get => _customMenuItems;

        set
        {
            if (_customMenuItems != value)
            {
                _customMenuItems?.CollectionChanged -= OnCustomMenuItemsChanged;

                _customMenuItems = value;

                _customMenuItems?.CollectionChanged += OnCustomMenuItemsChanged;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CustomMenuItems)));
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeCustomMenuItems()
    {
        var shouldSerialize = _customMenuItems is { Count: > 0 };
        return shouldSerialize;
    }

    /// <summary>
    /// Resets the CustomMenuItems collection to its default value.
    /// </summary>
    public void ResetCustomMenuItems() => _customMenuItems?.Clear();

    #endregion

    #region Private Methods
    /// <summary>
    /// Handles changes to the custom menu items collection.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Event arguments.</param>
    private void OnCustomMenuItemsChanged(object? sender, EventArgs e)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CustomMenuItems)));
        PerformNeedPaint(true);
    }
    #endregion

    #region Reset and Serialization

    /// <summary>
    /// Resets all properties to their default values.
    /// </summary>
    public void Reset()
    {
        ResetEnabled();
        //ResetShowOnLeftClick();
        //ResetUseSystemMenu();
        ResetShowOnRightClick();
        ResetShowOnAltSpace();
        ResetShowOnIconClick();
        ResetCustomMenuItems();
    }

    /// <summary>
    /// Gets a value indicating if any properties should be serialized.
    /// </summary>
    /// <returns>True if any properties should be serialized; otherwise false.</returns>
    public bool ShouldSerialize()
    {
        var shouldSerialize = !IsDefault;
        return shouldSerialize;
    }

    #endregion
}
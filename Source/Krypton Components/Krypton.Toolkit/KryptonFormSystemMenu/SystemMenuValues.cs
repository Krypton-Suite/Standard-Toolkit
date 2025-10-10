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
[ToolboxItem(false)]
[DesignerCategory(@"code")]
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
    private bool _showOnRightClick;
    private bool _showOnAltSpace;
    private bool _showOnIconClick;
    private KryptonContextMenu _contextMenu;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the SystemMenuValues class.
    /// </summary>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    /// <param name="contextMenu">A valid KryptonContextMenu instance.</param>
    public SystemMenuValues(NeedPaintHandler needPaint, KryptonContextMenu contextMenu)
    {
        // Store the provided paint notification delegate
        NeedPaint = needPaint;
        _contextMenu = contextMenu;

        // Set initial values
        _enabled = DEFAULT_ENABLED;
        _showOnRightClick = DEFAULT_SHOW_ON_RIGHT_CLICK;
        _showOnAltSpace = DEFAULT_SHOW_ON_ALT_SPACE;
        _showOnIconClick = DEFAULT_SHOW_ON_ICON_CLICK;

    }
    #endregion

    #region MenuItems
    /// <summary>
    /// Acces to the System menu item collection.<br/>
    /// Note: Be careful with the default items, as those are present as well.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonContextMenuCollection MenuItems => _contextMenu.Items;
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => !ShouldSerializeEnabled()
        && !ShouldSerializeShowOnRightClick()
        && !ShouldSerializeShowOnAltSpace()
        && !ShouldSerializeShowOnIconClick();
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
                OnPropertyChanged(nameof(ShowOnRightClick));
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
                OnPropertyChanged(nameof(ShowOnAltSpace));
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
                OnPropertyChanged(nameof(ShowOnIconClick));
            }
        }
    }

    private bool ShouldSerializeShowOnIconClick() => ShowOnIconClick != DEFAULT_SHOW_ON_ICON_CLICK;

    /// <summary>
    /// Resets the ShowOnIconClick property to its default value.
    /// </summary>
    public void ResetShowOnIconClick() => ShowOnIconClick = DEFAULT_SHOW_ON_ICON_CLICK;

    #endregion

    #region Private
    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
    }

    /// <summary>
    /// Gets a value indicating if any properties should be serialized.
    /// </summary>
    /// <returns>True if any properties should be serialized; otherwise false.</returns>
    public bool ShouldSerialize()
    {
        return ShouldSerializeEnabled() ||
               ShouldSerializeShowOnRightClick() ||
               ShouldSerializeShowOnAltSpace() ||
               ShouldSerializeShowOnIconClick();
    }

    #endregion
}
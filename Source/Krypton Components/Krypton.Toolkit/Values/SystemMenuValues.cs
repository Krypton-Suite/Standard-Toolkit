#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Storage for system menu value information.
/// </summary>
[ToolboxItem(false)]
[DesignerCategory(@"code")]
[TypeConverter(typeof(ExpandableObjectConverter))]
public class SystemMenuValues : INotifyPropertyChanged
{
    #region Static Fields
    private const bool DEFAULT_ENABLED = true;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;
    #endregion

    #region Instance Fields
    private bool _enabled;
    private KryptonContextMenu _contextMenu;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the SystemMenuValues class.
    /// </summary>
    /// <param name="contextMenu">A valid KryptonContextMenu instance.</param>
    public SystemMenuValues(KryptonContextMenu contextMenu)
    {
        // The System menu context menu
        _contextMenu = contextMenu;

        // Set initial values
        _enabled = DEFAULT_ENABLED;
    }
    #endregion

    #region MenuItems
    /// <summary>
    /// Acces to the System menu items collection.<br/>
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
    public bool IsDefault => !ShouldSerializeEnabled();
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
                OnPropertyChanged(nameof(Enabled));
            }
        }
    }

    private bool ShouldSerializeEnabled() => Enabled != DEFAULT_ENABLED;

    /// <summary>
    /// Resets the Enabled property to its default value.
    /// </summary>
    public void ResetEnabled() => Enabled = DEFAULT_ENABLED;
    #endregion

    #region Private
    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion

    #region Public override
    /// <summary>
    /// Not implemented / used internally.
    /// </summary>
    /// <returns>string.Empty</returns>
    public override string ToString()
    {
        return IsDefault ? string.Empty : "Modified";
    }
    #endregion

    #region Reset and Serialization

    /// <summary>
    /// Resets all properties to their default values.
    /// </summary>
    public void Reset()
    {
        ResetEnabled();
    }

    /// <summary>
    /// Gets a value indicating if any properties should be serialized.
    /// </summary>
    /// <returns>True if any properties should be serialized; otherwise false.</returns>
    public bool ShouldSerialize()
    {
        return ShouldSerializeEnabled();
    }
    #endregion
}
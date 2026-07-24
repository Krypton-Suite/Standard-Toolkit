#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Expandable configuration for <see cref="KryptonExpandingMenuItem"/>.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class ExpandingMenuItemValues : Storage
{
    #region Instance Fields

    private bool _isStandardItem;
    private bool _alwaysHidden;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="ExpandingMenuItemValues"/> class.
    /// </summary>
    /// <param name="owner">Owning expanding menu item.</param>
    public ExpandingMenuItemValues(KryptonExpandingMenuItem owner)
    {
        _ = owner ?? throw new ArgumentNullException(nameof(owner));
    }

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? @"Modified" : string.Empty;

    #endregion

    #region IsDefault

    /// <inheritdoc />
    [Browsable(false)]
    public override bool IsDefault => !_isStandardItem && !_alwaysHidden;

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets whether this item is a standard menu item. Standard menu
    /// items are always displayed, regardless of the expand status.
    /// </summary>
    /// <value>True if the menu item is a standard item, otherwise false</value>
    [Category(@"Behavior")]
    [DefaultValue(false)]
    [Description(@"True if the menu item is a standard item, otherwise false")]
    public bool IsStandardItem
    {
        get => _isStandardItem;
        set => _isStandardItem = value;
    }

    /// <summary>
    /// Gets or sets whether this menu item is always hidden for this user.
    /// </summary>
    /// <value>True if this user should not see this menu item, otherwise false</value>
    /// <remarks>
    /// This is necessary because Visible is used to display menu items.
    /// We can't track it because during OnDropDown it's always false.
    /// </remarks>
    [Category(@"Behavior")]
    [DefaultValue(false)]
    [Description(@"True if this user should not see this menu item, otherwise false")]
    public bool AlwaysHidden
    {
        get => _alwaysHidden;
        set => _alwaysHidden = value;
    }

    /// <summary>
    /// Resets all values to their defaults.
    /// </summary>
    public void Reset()
    {
        _isStandardItem = false;
        _alwaysHidden = false;
    }

    #endregion
}

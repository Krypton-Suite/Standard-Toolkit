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
/// Expandable configuration for <see cref="KryptonEnhancedToolStripMenuItem"/>.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class EnhancedMenuItemValues : Storage
{
    #region Instance Fields

    private readonly KryptonEnhancedToolStripMenuItem _owner;
    private Enum? _associatedEnumValue;
    private CheckMarkDisplayStyle _displayStyle = CheckMarkDisplayStyle.RadioButton;
    private string? _radioButtonGroupName;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="EnhancedMenuItemValues"/> class.
    /// </summary>
    /// <param name="owner">Owning enhanced menu item.</param>
    public EnhancedMenuItemValues(KryptonEnhancedToolStripMenuItem owner) =>
        _owner = owner ?? throw new ArgumentNullException(nameof(owner));

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? @"Modified" : string.Empty;

    #endregion

    #region IsDefault

    /// <inheritdoc />
    [Browsable(false)]
    public override bool IsDefault =>
        _associatedEnumValue is null &&
        _displayStyle == CheckMarkDisplayStyle.RadioButton &&
        _radioButtonGroupName is null;

    #endregion

    #region Public

    /// <summary>
    /// Menu items with radio button display can be used to bind enum values.
    /// This property can be used to store the associated <see cref="Enum"/> value.
    /// </summary>
    [Category(@"Data")]
    [DefaultValue(null)]
    public Enum? AssociatedEnumValue
    {
        get => _associatedEnumValue;
        set => _associatedEnumValue = value;
    }

    /// <summary>
    /// Switches between CheckBox or RadioButton style.
    /// </summary>
    [Category(@"Appearance")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [NotifyParentProperty(true)]
    [DefaultValue(CheckMarkDisplayStyle.RadioButton)]
    public CheckMarkDisplayStyle DisplayStyle
    {
        get => _displayStyle;
        set { _displayStyle = value; _owner.Invalidate(); }
    }

    /// <summary>
    /// In order to provide behavior similar to a RadioButton group, we need a way to mark groups.
    /// This property is used for this purpose. All menu items with an identical <see cref="RadioButtonGroupName"/>
    /// belong to the same group.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(null)]
    public string? RadioButtonGroupName
    {
        get => _radioButtonGroupName;
        set => _radioButtonGroupName = value;
    }

    /// <summary>
    /// Resets all values to their defaults.
    /// </summary>
    public void Reset()
    {
        _associatedEnumValue = null;
        _displayStyle = CheckMarkDisplayStyle.RadioButton;
        _radioButtonGroupName = null;
        _owner.Invalidate();
    }

    #endregion
}

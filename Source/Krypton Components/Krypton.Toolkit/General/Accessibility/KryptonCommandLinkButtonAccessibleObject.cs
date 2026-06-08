#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Provides accessibility information for KryptonCommandLinkButton.
/// </summary>
internal class KryptonCommandLinkButtonAccessibleObject : Control.ControlAccessibleObject
{
    #region Instance Fields
    private readonly KryptonCommandLinkButton _owner;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonCommandLinkButtonAccessibleObject class.
    /// </summary>
    /// <param name="owner">The KryptonCommandLinkButton control that owns this accessible object.</param>
    public KryptonCommandLinkButtonAccessibleObject(KryptonCommandLinkButton owner)
        : base(owner)
    {
        _owner = owner;
    }
    #endregion

    #region Public Overrides
    /// <summary>
    /// Gets the accessible name of the control.
    /// </summary>
    public override string? Name
    {
        get
        {
            if (!string.IsNullOrWhiteSpace(_owner.AccessibleName))
            {
                return _owner.AccessibleName;
            }

            return FirstNonEmpty(_owner.CommandLinkTextValues?.Heading, _owner.Text, _owner.Name);
        }

        set => _owner.AccessibleName = value;
    }

    /// <summary>
    /// Gets the accessible description of the control.
    /// </summary>
    public override string? Description
    {
        get
        {
            if (!string.IsNullOrWhiteSpace(_owner.AccessibleDescription))
            {
                return _owner.AccessibleDescription;
            }

            return FirstNonEmpty(_owner.CommandLinkTextValues?.Description, base.Description);
        }
    }

    /// <summary>
    /// Gets the accessible role of the control.
    /// </summary>
    public override AccessibleRole Role => AccessibleRole.PushButton;

    /// <summary>
    /// Gets the default action for the control.
    /// </summary>
    public override string DefaultAction => @"Press";

    /// <summary>
    /// Performs the default action associated with this accessible object.
    /// </summary>
    public override void DoDefaultAction()
    {
        if (_owner.Enabled)
        {
            _owner.PerformClick();
        }
    }
    #endregion

    #region Implementation
    private static string? FirstNonEmpty(params string?[] values)
    {
        foreach (string? value in values)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                return value;
            }
        }

        return null;
    }
    #endregion
}

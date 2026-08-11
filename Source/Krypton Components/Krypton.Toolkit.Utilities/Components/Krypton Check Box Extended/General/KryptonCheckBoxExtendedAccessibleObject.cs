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
/// Provides accessibility information for <see cref="KryptonCheckBoxExtended"/> controls.
/// </summary>
internal sealed class KryptonCheckBoxExtendedAccessibleObject : Control.ControlAccessibleObject
{
    #region Instance Fields

    private readonly KryptonCheckBoxExtended _owner;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonCheckBoxExtendedAccessibleObject"/> class.
    /// </summary>
    /// <param name="owner">The owning control.</param>
    public KryptonCheckBoxExtendedAccessibleObject(KryptonCheckBoxExtended owner)
        : base(owner)
    {
        _owner = owner;
    }

    #endregion

    #region Public Overrides

    /// <inheritdoc />
    public override string? Name
    {
        get
        {
            if (!string.IsNullOrEmpty(_owner.AccessibleName))
            {
                return _owner.AccessibleName;
            }

            string text = _owner.Text;
            return string.IsNullOrEmpty(text) ? base.Name ?? _owner.Name : text.Replace(@"&", string.Empty);
        }
    }

    /// <inheritdoc />
    public override string? Description
    {
        get
        {
            if (!string.IsNullOrEmpty(_owner.AccessibleDescription))
            {
                return _owner.AccessibleDescription;
            }

            return string.IsNullOrEmpty(_owner.Values.Subtext) ? base.Description : _owner.Values.Subtext;
        }
    }

    /// <inheritdoc />
    public override AccessibleRole Role => _owner.AccessibleRole != AccessibleRole.Default
        ? _owner.AccessibleRole
        : AccessibleRole.CheckButton;

    /// <inheritdoc />
    public override AccessibleStates State
    {
        get
        {
            AccessibleStates state = AccessibleStates.Focusable;

            if (_owner.Focused)
            {
                state |= AccessibleStates.Focused;
            }

            if (_owner.Checked)
            {
                state |= AccessibleStates.Checked;
            }

            if (!_owner.Visible)
            {
                state |= AccessibleStates.Invisible;
            }

            if (!_owner.Enabled)
            {
                state |= AccessibleStates.Unavailable;
            }

            return state;
        }
    }

    /// <inheritdoc />
    public override string DefaultAction => @"Check";

    /// <inheritdoc />
    public override void DoDefaultAction()
    {
        if (_owner.Enabled && _owner.Visible)
        {
            _owner.PerformAccessibilityClick();
        }
    }

    #endregion
}

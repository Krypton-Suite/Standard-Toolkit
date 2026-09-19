#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Provides accessibility information for <see cref="KryptonRating"/>.
/// </summary>
internal class KryptonRatingAccessibleObject : Control.ControlAccessibleObject
{
    #region Instance Fields

    private readonly KryptonRating _owner;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonRatingAccessibleObject"/> class.
    /// </summary>
    /// <param name="owner">Owning rating control.</param>
    public KryptonRatingAccessibleObject(KryptonRating owner)
        : base(owner) =>
        _owner = owner;

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

            return base.Name ?? @"Rating";
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

            return $@"Rating from 0 to {_owner.Maximum}";
        }
    }

    /// <inheritdoc />
    public override AccessibleRole Role
    {
        get
        {
            if (_owner.AccessibleRole != AccessibleRole.Default)
            {
                return _owner.AccessibleRole;
            }

            return AccessibleRole.Slider;
        }
    }

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

            if (!_owner.Enabled)
            {
                state |= AccessibleStates.Unavailable;
            }

            if (_owner.ReadOnly)
            {
                state |= AccessibleStates.ReadOnly;
            }

            return state;
        }
    }

    /// <inheritdoc />
    public override string? Value
    {
        get => _owner.Value.ToString(CultureInfo.CurrentCulture);
        set
        {
            if (decimal.TryParse(value, NumberStyles.Number, CultureInfo.CurrentCulture, out decimal result))
            {
                _owner.Value = result;
            }
        }
    }

    /// <inheritdoc />
    public override void DoDefaultAction()
    {
        if (_owner.CanFocus)
        {
            _owner.Focus();
        }
    }

    #endregion
}

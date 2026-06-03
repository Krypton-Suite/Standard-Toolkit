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
/// Provides shared accessibility behavior for Krypton controls with a single default action.
/// </summary>
/// <typeparam name="T">The owner control type.</typeparam>
internal abstract class KryptonActionControlAccessibleObject<T> : Control.ControlAccessibleObject
    where T : Control
{
    #region Instance Fields
    private readonly AccessibleRole _defaultRole;
    private readonly string _defaultAction;
    #endregion

    #region Identity
    protected KryptonActionControlAccessibleObject(T owner, AccessibleRole defaultRole, string defaultAction)
        : base(owner)
    {
        OwnerControl = owner;
        _defaultRole = defaultRole;
        _defaultAction = defaultAction;
    }
    #endregion

    #region Protected
    protected T OwnerControl { get; }

    protected virtual bool IsChecked => false;

    protected virtual string? DefaultName => null;

    protected virtual string? DefaultDescription => null;

    protected abstract void PerformAction();
    #endregion

    #region Public Overrides
    public override string? Name
    {
        get
        {
            if (!string.IsNullOrEmpty(OwnerControl.AccessibleName))
            {
                return OwnerControl.AccessibleName;
            }

            if (!string.IsNullOrEmpty(DefaultName))
            {
                return DefaultName;
            }

            return base.Name ?? OwnerControl.Name;
        }
    }

    public override string? Description
    {
        get
        {
            if (!string.IsNullOrEmpty(OwnerControl.AccessibleDescription))
            {
                return OwnerControl.AccessibleDescription;
            }

            return !string.IsNullOrEmpty(DefaultDescription)
                ? DefaultDescription
                : base.Description;
        }
    }

    public override AccessibleRole Role => OwnerControl.AccessibleRole != AccessibleRole.Default
        ? OwnerControl.AccessibleRole
        : _defaultRole;

    public override AccessibleStates State
    {
        get
        {
            AccessibleStates state = AccessibleStates.Focusable;

            if (OwnerControl.Focused)
            {
                state |= AccessibleStates.Focused;
            }

            if (IsChecked)
            {
                state |= AccessibleStates.Checked;
            }

            if (!OwnerControl.Visible)
            {
                state |= AccessibleStates.Invisible;
            }

            if (!OwnerControl.Enabled)
            {
                state |= AccessibleStates.Unavailable;
            }

            return state;
        }
    }

    public override string DefaultAction => _defaultAction;

    public override void DoDefaultAction()
    {
        if (OwnerControl.Enabled && OwnerControl.Visible)
        {
            PerformAction();
        }
    }
    #endregion
}

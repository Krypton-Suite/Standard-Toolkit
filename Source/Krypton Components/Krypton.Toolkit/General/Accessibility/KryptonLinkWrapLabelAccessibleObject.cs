#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Provides accessibility information for KryptonLinkWrapLabel control.
/// Provides proper accessibility support without causing infinite recursion by avoiding access to the owner's AccessibilityObject property.
/// </summary>
internal class KryptonLinkWrapLabelAccessibleObject : Control.ControlAccessibleObject
{
    #region Instance Fields
    private readonly KryptonLinkWrapLabel _owner;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonLinkWrapLabelAccessibleObject class.
    /// </summary>
    /// <param name="owner">The KryptonLinkWrapLabel control that owns this accessible object.</param>
    public KryptonLinkWrapLabelAccessibleObject(KryptonLinkWrapLabel owner)
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
            // Use the control's AccessibleName if set, otherwise fall back to control's Name or base implementation
            if (!string.IsNullOrEmpty(_owner.AccessibleName))
            {
                return _owner.AccessibleName;
            }

            return base.Name ?? _owner.Name;
        }
    }

    /// <summary>
    /// Gets the accessible description of the control.
    /// </summary>
    public override string? Description
    {
        get
        {
            // Use the control's AccessibleDescription if set, otherwise fall back to base implementation
            if (!string.IsNullOrEmpty(_owner.AccessibleDescription))
            {
                return _owner.AccessibleDescription;
            }

            return base.Description;
        }
    }

    /// <summary>
    /// Gets the accessible role of the control.
    /// </summary>
    public override AccessibleRole Role
    {
        get
        {
            // Use the control's AccessibleRole if set, otherwise return Link role for LinkLabel controls
            if (_owner.AccessibleRole != AccessibleRole.Default)
            {
                return _owner.AccessibleRole;
            }

            return AccessibleRole.Link;
        }
    }

    /// <summary>
    /// Gets the state of this accessible object.
    /// </summary>
    public override AccessibleStates State
    {
        get
        {
            // Build state from control properties
            AccessibleStates state = 0;

            if (_owner.Focused)
            {
                state |= AccessibleStates.Focused;
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

    /// <summary>
    /// Gets the value of the accessible object.
    /// </summary>
    public override string? Value
    {
        get
        {
            // For LinkLabel, the value is the text content
            return _owner.Text;
        }
    }

    /// <summary>
    /// Performs the default action associated with this accessible object.
    /// </summary>
    public override void DoDefaultAction()
    {
        // For LinkLabel, the default action is to focus the control
        // The actual link click will be triggered by user interaction (Enter key or mouse click)
        if (!_owner.Enabled || !_owner.Visible)
        {
            return;
        }

        // Focus the control so user can interact with it
        if (_owner.CanFocus)
        {
            _owner.Focus();
        }

        // Use base implementation which handles standard accessibility behavior
        base.DoDefaultAction();
    }

    /// <summary>
    /// Retrieves the child accessible object corresponding to the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the child accessible object.</param>
    /// <returns>An AccessibleObject that represents the child accessible object corresponding to the specified index.</returns>
    public override AccessibleObject? GetChild(int index)
    {
        // LinkLabel children are individual links, but since we're providing a single accessible object,
        // we don't expose children separately. Return null to indicate no children.
        return null;
    }

    /// <summary>
    /// Retrieves the number of children belonging to an accessible object.
    /// </summary>
    /// <returns>The number of children belonging to an accessible object.</returns>
    public override int GetChildCount()
    {
        // LinkLabel children are individual links, but we're treating the control as a single accessible object
        return 0;
    }

    /// <summary>
    /// Navigates to another accessible object.
    /// </summary>
    /// <param name="direction">One of the NavigateDirection values.</param>
    /// <returns>An AccessibleObject representing one of the NavigateDirection values.</returns>
    public override AccessibleObject? Navigate(AccessibleNavigation direction)
    {
        // Use base implementation for navigation
        return base.Navigate(direction);
    }

    /// <summary>
    /// Selects this accessible object.
    /// </summary>
    /// <param name="flags">One of the AccessibleSelection values.</param>
    public override void Select(AccessibleSelection flags)
    {
        // Use base implementation for selection
        base.Select(flags);
    }
    #endregion
}

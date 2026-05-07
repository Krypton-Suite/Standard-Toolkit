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
/// Provides accessibility information for KryptonComboBox control.
/// Delegates to the internal ComboBox's accessibility object to ensure proper UIA provider support.
/// </summary>
internal class KryptonComboBoxAccessibleObject : Control.ControlAccessibleObject
{
    #region Instance Fields
    private readonly KryptonComboBox _owner;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonComboBoxAccessibleObject class.
    /// </summary>
    /// <param name="owner">The KryptonComboBox control that owns this accessible object.</param>
    public KryptonComboBoxAccessibleObject(KryptonComboBox owner)
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
            // Try to get name from internal ComboBox first
            var internalAccessible = _owner.ComboBox?.AccessibilityObject;
            if (internalAccessible?.Name != null)
            {
                return internalAccessible.Name;
            }

            // Fall back to base implementation
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
            // Try to get description from internal ComboBox first
            var internalAccessible = _owner.ComboBox?.AccessibilityObject;
            if (internalAccessible?.Description != null)
            {
                return internalAccessible.Description;
            }

            // Fall back to base implementation
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
            // Delegate to internal ComboBox's role
            var internalAccessible = _owner.ComboBox?.AccessibilityObject;
            if (internalAccessible != null)
            {
                var role = internalAccessible.Role;
                // Ensure we have a valid role (legacy TFMs might return Default)
                if (role != AccessibleRole.Default && role != AccessibleRole.None)
                {
                    return role;
                }
            }

            // Fall back to ComboBox role
            return AccessibleRole.ComboBox;
        }
    }

    /// <summary>
    /// Gets the state of this accessible object.
    /// </summary>
    public override AccessibleStates State
    {
        get
        {
            // Delegate to internal ComboBox's state
            var internalAccessible = _owner.ComboBox?.AccessibilityObject;
            if (internalAccessible != null)
            {
                return internalAccessible.State;
            }

            // Fall back to base implementation
            return base.State;
        }
    }

    /// <summary>
    /// Gets the value of the accessible object.
    /// </summary>
    public override string? Value
    {
        get
        {
            // Delegate to internal ComboBox's value
            var internalAccessible = _owner.ComboBox?.AccessibilityObject;
            if (internalAccessible?.Value != null)
            {
                return internalAccessible.Value;
            }

            // Fall back to control's text
            return _owner.Text;
        }
    }

    /// <summary>
    /// Performs the default action associated with this accessible object.
    /// </summary>
    public override void DoDefaultAction()
    {
        // Delegate to internal ComboBox's default action
        var internalAccessible = _owner.ComboBox?.AccessibilityObject;
        if (internalAccessible != null)
        {
            internalAccessible.DoDefaultAction();
        }
        else
        {
            base.DoDefaultAction();
        }
    }

    /// <summary>
    /// Retrieves the child accessible object corresponding to the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the child accessible object.</param>
    /// <returns>An AccessibleObject that represents the child accessible object corresponding to the specified index.</returns>
    public override AccessibleObject? GetChild(int index)
    {
        // Delegate to internal ComboBox's children
        var internalAccessible = _owner.ComboBox?.AccessibilityObject;
        if (internalAccessible != null)
        {
            return internalAccessible.GetChild(index);
        }

        return base.GetChild(index);
    }

    /// <summary>
    /// Retrieves the number of children belonging to an accessible object.
    /// </summary>
    /// <returns>The number of children belonging to an accessible object.</returns>
    public override int GetChildCount()
    {
        // Delegate to internal ComboBox's child count
        var internalAccessible = _owner.ComboBox?.AccessibilityObject;
        if (internalAccessible != null)
        {
            return internalAccessible.GetChildCount();
        }

        return base.GetChildCount();
    }

    /// <summary>
    /// Navigates to another accessible object.
    /// </summary>
    /// <param name="direction">One of the NavigateDirection values.</param>
    /// <returns>An AccessibleObject representing one of the NavigateDirection values.</returns>
    public override AccessibleObject? Navigate(AccessibleNavigation direction)
    {
        // Delegate to internal ComboBox's navigation
        var internalAccessible = _owner.ComboBox?.AccessibilityObject;
        if (internalAccessible != null)
        {
            return internalAccessible.Navigate(direction);
        }

        return base.Navigate(direction);
    }

    /// <summary>
    /// Selects this accessible object.
    /// </summary>
    /// <param name="flags">One of the AccessibleSelection values.</param>
    public override void Select(AccessibleSelection flags)
    {
        // Delegate to internal ComboBox's selection
        var internalAccessible = _owner.ComboBox?.AccessibilityObject;
        if (internalAccessible != null)
        {
            internalAccessible.Select(flags);
        }
        else
        {
            base.Select(flags);
        }
    }
    #endregion
}

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
/// Provides accessibility information for KryptonCheckedListBox control.
/// Delegates to the internal CheckedListBox's accessibility object to ensure proper UIA provider support.
/// </summary>
internal class KryptonCheckedListBoxAccessibleObject : Control.ControlAccessibleObject
{
    #region Instance Fields
    private readonly KryptonCheckedListBox _owner;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonCheckedListBoxAccessibleObject class.
    /// </summary>
    /// <param name="owner">The KryptonCheckedListBox control that owns this accessible object.</param>
    public KryptonCheckedListBoxAccessibleObject(KryptonCheckedListBox owner)
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
            // Try to get name from internal ListBox (CheckedListBox inherits from ListBox) first
            var internalAccessible = _owner.ListBox?.AccessibilityObject;
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
            // Try to get description from internal ListBox first
            var internalAccessible = _owner.ListBox?.AccessibilityObject;
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
            // Delegate to internal ListBox's role
            var internalAccessible = _owner.ListBox?.AccessibilityObject;
            if (internalAccessible != null)
            {
                var role = internalAccessible.Role;
                // Ensure we have a valid role (legacy TFMs might return Default)
                if (role != AccessibleRole.Default && role != AccessibleRole.None)
                {
                    return role;
                }
            }

            // Fall back to List role for CheckedListBox controls
            return AccessibleRole.List;
        }
    }

    /// <summary>
    /// Gets the state of this accessible object.
    /// </summary>
    public override AccessibleStates State
    {
        get
        {
            // Delegate to internal ListBox's state
            var internalAccessible = _owner.ListBox?.AccessibilityObject;
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
            // Delegate to internal ListBox's value
            var internalAccessible = _owner.ListBox?.AccessibilityObject;
            if (internalAccessible?.Value != null)
            {
                return internalAccessible.Value;
            }

            // Fall back to base implementation
            return base.Value;
        }
    }

    /// <summary>
    /// Performs the default action associated with this accessible object.
    /// </summary>
    public override void DoDefaultAction()
    {
        // Delegate to internal ListBox's default action
        var internalAccessible = _owner.ListBox?.AccessibilityObject;
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
        // Delegate to internal ListBox's children
        var internalAccessible = _owner.ListBox?.AccessibilityObject;
        if (internalAccessible != null && HasNativeItemChildren(internalAccessible))
        {
            return internalAccessible.GetChild(index);
        }

        return index >= 0 && index < _owner.Items.Count
            ? new KryptonCheckedListBoxItemAccessibleObject(this, _owner, index)
            : null;
    }

    /// <summary>
    /// Retrieves the number of children belonging to an accessible object.
    /// </summary>
    /// <returns>The number of children belonging to an accessible object.</returns>
    public override int GetChildCount()
    {
        // Delegate to internal ListBox's child count
        var internalAccessible = _owner.ListBox?.AccessibilityObject;
        if (internalAccessible != null && HasNativeItemChildren(internalAccessible))
        {
            return internalAccessible.GetChildCount();
        }

        return _owner.Items.Count;
    }

    /// <summary>
    /// Retrieves the currently selected child.
    /// </summary>
    /// <returns>The selected child accessible object.</returns>
    public override AccessibleObject? GetSelected()
    {
        var internalAccessible = _owner.ListBox?.AccessibilityObject;
        if (internalAccessible != null && HasNativeItemChildren(internalAccessible))
        {
            return internalAccessible.GetSelected();
        }

        return _owner.SelectedIndex >= 0 ? GetChild(_owner.SelectedIndex) : null;
    }

    /// <summary>
    /// Navigates to another accessible object.
    /// </summary>
    /// <param name="direction">One of the NavigateDirection values.</param>
    /// <returns>An AccessibleObject representing one of the NavigateDirection values.</returns>
    public override AccessibleObject? Navigate(AccessibleNavigation direction)
    {
        // Delegate to internal ListBox's navigation
        var internalAccessible = _owner.ListBox?.AccessibilityObject;
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
        // Delegate to internal ListBox's selection
        var internalAccessible = _owner.ListBox?.AccessibilityObject;
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

    #region Implementation
    private static bool HasNativeItemChildren(AccessibleObject accessibleObject)
    {
        for (int i = 0; i < accessibleObject.GetChildCount(); i++)
        {
            AccessibleObject? child = accessibleObject.GetChild(i);
            if (child?.Role == AccessibleRole.CheckButton || child?.Role == AccessibleRole.ListItem)
            {
                return true;
            }
        }

        return false;
    }

    private sealed class KryptonCheckedListBoxItemAccessibleObject : AccessibleObject
    {
        private readonly AccessibleObject _parent;
        private readonly KryptonCheckedListBox _owner;
        private readonly int _index;

        public KryptonCheckedListBoxItemAccessibleObject(AccessibleObject parent, KryptonCheckedListBox owner, int index)
        {
            _parent = parent;
            _owner = owner;
            _index = index;
        }

        public override AccessibleObject? Parent => _parent;

        public override string? Name => _owner.ListBox.GetItemText(_owner.Items[_index]);

        public override AccessibleRole Role => AccessibleRole.CheckButton;

        public override AccessibleStates State
        {
            get
            {
                AccessibleStates state = AccessibleStates.Focusable | AccessibleStates.Selectable;

                if (_owner.GetSelected(_index))
                {
                    state |= AccessibleStates.Selected;
                }

                if (_owner.ListBox.Focused && _owner.SelectedIndex == _index)
                {
                    state |= AccessibleStates.Focused;
                }

                state |= _owner.GetItemCheckState(_index) switch
                {
                    CheckState.Checked => AccessibleStates.Checked,
                    CheckState.Indeterminate => AccessibleStates.Indeterminate,
                    _ => 0
                };

                if (!_owner.Enabled)
                {
                    state |= AccessibleStates.Unavailable;
                }

                Rectangle bounds = _owner.GetItemRectangle(_index);
                if (bounds.IsEmpty)
                {
                    state |= AccessibleStates.Invisible;
                }

                return state;
            }
        }

        public override Rectangle Bounds => _owner.ListBox.RectangleToScreen(_owner.GetItemRectangle(_index));

        public override string DefaultAction => @"Toggle";

        public override void DoDefaultAction()
        {
            if (!_owner.Enabled || _owner.SelectionMode == CheckedSelectionMode.None)
            {
                return;
            }

            _owner.SelectedIndex = _index;

            if (_owner.SelectionMode == CheckedSelectionMode.Radio)
            {
                for (int i = 0; i < _owner.Items.Count; i++)
                {
                    if (i != _index && _owner.GetItemCheckState(i) != CheckState.Unchecked)
                    {
                        _owner.SetItemCheckState(i, CheckState.Unchecked);
                    }
                }

                if (_owner.GetItemCheckState(_index) == CheckState.Unchecked)
                {
                    _owner.SetItemCheckState(_index, CheckState.Checked);
                }

                return;
            }

            CheckState current = _owner.GetItemCheckState(_index);
            _owner.SetItemCheckState(_index, current == CheckState.Unchecked ? CheckState.Checked : CheckState.Unchecked);
        }

        public override void Select(AccessibleSelection flags)
        {
            if (_owner.SelectionMode == CheckedSelectionMode.None)
            {
                return;
            }

            if ((flags & AccessibleSelection.TakeSelection) == AccessibleSelection.TakeSelection)
            {
                _owner.SelectedIndex = _index;
            }
            else if ((flags & AccessibleSelection.AddSelection) == AccessibleSelection.AddSelection)
            {
                _owner.SetSelected(_index, true);
            }
            else if ((flags & AccessibleSelection.RemoveSelection) == AccessibleSelection.RemoveSelection)
            {
                _owner.SetSelected(_index, false);
            }
        }

        public override int GetChildCount() => 0;
    }
    #endregion
}

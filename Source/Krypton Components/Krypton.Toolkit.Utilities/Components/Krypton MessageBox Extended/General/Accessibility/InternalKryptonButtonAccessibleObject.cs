#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), tobitege, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Provides accessibility information for InternalKryptonButton controls.
/// </summary>
internal class InternalKryptonButtonAccessibleObject : UtilitiesActionControlAccessibleObject<InternalKryptonButton>
{
    /// <summary>
    /// Initialize a new instance of the InternalKryptonButtonAccessibleObject class.
    /// </summary>
    /// <param name="owner">The InternalKryptonButton control that owns this accessible object.</param>
    public InternalKryptonButtonAccessibleObject(InternalKryptonButton owner)
        : base(owner, AccessibleRole.PushButton, @"Press")
    {
    }

    /// <summary>
    /// Gets the default name of the control.
    /// </summary>
    protected override string? DefaultName => OwnerControl.Values.Text;

    /// <inheritdoc />
    protected override void PerformAction() => OwnerControl.PerformClick();
}

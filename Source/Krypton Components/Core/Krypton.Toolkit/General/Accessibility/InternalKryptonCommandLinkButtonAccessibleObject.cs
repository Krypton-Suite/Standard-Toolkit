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
/// Provides accessibility information for InternalKryptonCommandLinkButton controls.
/// </summary>
internal class InternalKryptonCommandLinkButtonAccessibleObject : KryptonActionControlAccessibleObject<InternalKryptonCommandLinkButton>
{
    /// <summary>
    /// Initialize a new instance of the InternalKryptonCommandLinkButtonAccessibleObject class.
    /// </summary>
    /// <param name="owner">The InternalKryptonCommandLinkButton control that owns this accessible object.</param>
    public InternalKryptonCommandLinkButtonAccessibleObject(InternalKryptonCommandLinkButton owner)
        : base(owner, AccessibleRole.PushButton, @"Press")
    {
    }

    /// <summary>
    /// Gets the default name of the control.
    /// </summary>
    protected override string? DefaultName => OwnerControl.CommandLinkTextValues.Heading;

    /// <summary>
    /// Gets the default description of the control.
    /// </summary>
    protected override string? DefaultDescription => OwnerControl.CommandLinkTextValues.Description;

    /// <inheritdoc />
    protected override void PerformAction() => OwnerControl.PerformClick();
}

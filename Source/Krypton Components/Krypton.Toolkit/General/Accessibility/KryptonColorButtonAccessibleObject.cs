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
/// Provides accessibility information for KryptonColorButton controls.
/// </summary>
internal class KryptonColorButtonAccessibleObject : KryptonActionControlAccessibleObject<KryptonColorButton>
{
    /// <summary>
    /// Initialize a new instance of the KryptonColorButtonAccessibleObject class.
    /// </summary>
    /// <param name="owner">The KryptonColorButton control that owns this accessible object.</param>
    public KryptonColorButtonAccessibleObject(KryptonColorButton owner)
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

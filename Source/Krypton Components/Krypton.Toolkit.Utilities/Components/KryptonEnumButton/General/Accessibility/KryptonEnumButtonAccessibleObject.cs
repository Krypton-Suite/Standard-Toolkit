#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Provides accessibility information for <see cref="KryptonEnumButton"/>. Exposes the current value's
/// display text as the accessible name and reports the enum being cycled as the description, so
/// assistive technologies announce the selected value and that the button cycles through a set.
/// </summary>
internal class KryptonEnumButtonAccessibleObject : UtilitiesActionControlAccessibleObject<KryptonEnumButton>
{
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonEnumButtonAccessibleObject"/> class.
    /// </summary>
    /// <param name="owner">The <see cref="KryptonEnumButton"/> that owns this accessible object.</param>
    public KryptonEnumButtonAccessibleObject(KryptonEnumButton owner)
        : base(owner, AccessibleRole.PushButton, @"Cycle")
    {
    }

    /// <inheritdoc />
    protected override string? DefaultName => OwnerControl.SelectedDisplayText;

    /// <inheritdoc />
    protected override string? DefaultDescription =>
        OwnerControl.EnumType is { } enumType
            ? $@"Cycles through {enumType.Name} values"
            : null;

    /// <inheritdoc />
    public override string? Value => OwnerControl.SelectedDisplayText;

    /// <inheritdoc />
    protected override void PerformAction() => OwnerControl.PerformClick();
}

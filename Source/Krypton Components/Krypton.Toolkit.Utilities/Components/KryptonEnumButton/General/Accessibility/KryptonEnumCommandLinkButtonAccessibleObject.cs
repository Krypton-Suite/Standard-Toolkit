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
/// Provides accessibility information for <see cref="KryptonEnumCommandLinkButton"/>. Exposes the
/// current value's heading as the accessible name and its description as the accessible description,
/// so assistive technologies announce the selected value and that the button cycles through a set.
/// </summary>
internal class KryptonEnumCommandLinkButtonAccessibleObject : UtilitiesActionControlAccessibleObject<KryptonEnumCommandLinkButton>
{
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonEnumCommandLinkButtonAccessibleObject"/> class.
    /// </summary>
    /// <param name="owner">The <see cref="KryptonEnumCommandLinkButton"/> that owns this accessible object.</param>
    public KryptonEnumCommandLinkButtonAccessibleObject(KryptonEnumCommandLinkButton owner)
        : base(owner, AccessibleRole.PushButton, @"Cycle")
    {
    }

    /// <inheritdoc />
    protected override string? DefaultName => OwnerControl.SelectedHeadingText;

    /// <inheritdoc />
    protected override string? DefaultDescription =>
        !string.IsNullOrEmpty(OwnerControl.SelectedDescriptionText)
            ? OwnerControl.SelectedDescriptionText
            : OwnerControl.EnumType is { } enumType
                ? $@"Cycles through {enumType.Name} values"
                : null;

    /// <inheritdoc />
    public override string? Value => OwnerControl.SelectedHeadingText;

    /// <inheritdoc />
    protected override void PerformAction() => OwnerControl.PerformClick();
}

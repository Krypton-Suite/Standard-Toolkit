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
/// Provides accessibility information for KryptonRadioButton controls.
/// </summary>
internal class KryptonRadioButtonAccessibleObject : KryptonActionControlAccessibleObject<KryptonRadioButton>
{
    /// <summary>
    /// Initialize a new instance of the KryptonRadioButtonAccessibleObject class.
    /// </summary>
    /// <param name="owner">The KryptonRadioButton control that owns this accessible object.</param>
    public KryptonRadioButtonAccessibleObject(KryptonRadioButton owner)
        : base(owner, AccessibleRole.RadioButton, @"Select")
    {
    }

    /// <summary>
    /// Gets the default name of the control.
    /// </summary>
    protected override string? DefaultName => OwnerControl.Values.Text;

    /// <summary>
    /// Gets a value indicating whether the control is checked.
    /// </summary>
    protected override bool IsChecked => OwnerControl.Checked;

    /// <inheritdoc />
    protected override void PerformAction() => OwnerControl.PerformAccessibilityClick();
}

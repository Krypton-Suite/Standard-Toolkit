#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Provides accessibility information for KryptonCommandLinkButton.
/// </summary>
internal class KryptonCommandLinkButtonAccessibleObject : UtilitiesActionControlAccessibleObject<KryptonCommandLinkButton>
{
    /// <summary>
    /// Initialize a new instance of the KryptonCommandLinkButtonAccessibleObject class.
    /// </summary>
    /// <param name="owner">The KryptonCommandLinkButton control that owns this accessible object.</param>
    public KryptonCommandLinkButtonAccessibleObject(KryptonCommandLinkButton owner)
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

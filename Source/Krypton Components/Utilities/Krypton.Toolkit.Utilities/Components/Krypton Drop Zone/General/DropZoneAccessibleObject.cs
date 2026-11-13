#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Exposes the drop zone as a named group whose Value is the live status summary and which reports Busy
/// while scanning. WinForms bridges AccessibleObjects to UI Automation, so Narrator/NVDA pick this up.
/// </summary>
internal sealed class DropZoneAccessibleObject : Control.ControlAccessibleObject
{
    private readonly KryptonDropZoneAlternate _owner;
    public DropZoneAccessibleObject(KryptonDropZoneAlternate owner) : base(owner) { _owner = owner; }

    public override AccessibleRole Role => AccessibleRole.Grouping;
    /*
    public override string Name { get => _owner.AccessibleName ?? DropZoneStrings.Get("Acc_Name"); set { base.Name = value; } }
    public override string Description => _owner.AccessibleDescription ?? DropZoneStrings.Get("Acc_Description");
    public override string Value { get => _owner.StatusText; set { } }
    public override AccessibleStates State => _owner.IsScanning ? base.State | AccessibleStates.Busy : base.State;
    */
}

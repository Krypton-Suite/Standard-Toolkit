#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Demo for Issue #3850: tooltip placement respects the full cursor image and hotspot.
/// </summary>
public partial class Bug3850TooltipHotSpotDemo : KryptonForm
{
    public Bug3850TooltipHotSpotDemo()
    {
        InitializeComponent();
        kryptonToolTip1.ContainerControl = this;

        kbtnMouse.ToolTipValues.ToolTipPosition.PlacementMode = PlacementMode.Mouse;
        kbtnRelativePoint.ToolTipValues.ToolTipPosition.PlacementMode = PlacementMode.RelativePoint;
        kbtnCenter.ToolTipValues.ToolTipPosition.PlacementMode = PlacementMode.Center;

        kryptonToolTip1.SetToolTip(kbtnLegacy,
            "Legacy cursor placement",
            "Uses ShowCalculatingSize — tooltip should appear below-right of the cursor hotspot, not on top of it.");

        cmbCursor.SelectedIndex = 0;
        cmbCursor.SelectedIndexChanged += (_, _) => ApplySelectedCursor();
        chkLegacyPlacement.CheckedChanged += (_, _) =>
            kryptonToolTip1.UseLegacyCursorAnchoredPlacement = chkLegacyPlacement.Checked;

        ApplySelectedCursor();
    }

    private void ApplySelectedCursor()
    {
        pnlCursorRegion.Cursor = cmbCursor.SelectedIndex switch
        {
            1 => Cursors.IBeam,
            2 => Cursors.WaitCursor,
            3 => Cursors.SizeAll,
            _ => Cursors.Default
        };
    }
}

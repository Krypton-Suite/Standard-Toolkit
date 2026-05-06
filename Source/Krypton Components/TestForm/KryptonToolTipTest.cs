#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp) & Simon Coghlan (aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Manual demo for <see cref="Krypton.Toolkit.KryptonToolTip"/> — themed tooltips on standard WinForms and Krypton controls (Issue #3380).
/// </summary>
public partial class KryptonToolTipTest : KryptonForm
{
    public KryptonToolTipTest()
    {
        InitializeComponent();
        kryptonToolTip1.ContainerControl = this;
        kryptonToolTip1.ToolTipValues.ToolTipStyle = LabelStyle.SuperTip;
        kryptonToolTip1.SetToolTip(btnStandardWinFormsButton,
            "Standard WinForms button",
            "This is a themed Krypton tooltip on a plain System.Windows.Forms.Button via KryptonToolTip.");
        kryptonToolTip1.SetToolTip(kbtnSample,
            "Krypton theme",
            "KryptonButton can already show built-in tips; here the same wrapper shows a tooltip for comparison.");
        kryptonToolTip1.SetToolTip(pnlHoverRegion,
            "Panel region",
            "Hover anywhere on this panel surface to verify hit-testing hooks on composite controls.");
        klblInstructions.Values.Text =
            "Hover the controls below. Tooltips use the global palette and SuperTip styling. "
            + "Placement follows ToolTipValues.ToolTipPosition (default: below the control). "
            + "Set kryptonToolTip1.UseLegacyCursorAnchoredPlacement = true in code to use cursor-offset placement instead.";
    }
}

#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Manual demo for GitHub issue #3383: KryptonButton corner rounding on hover when <see cref="KryptonButton.OverrideFocus"/> and
/// <see cref="KryptonButton.StateTracking"/> specify different border rounding while the button has focus (focus + tracking palette merge).
/// </summary>
public partial class Bug3383KryptonButtonStateTrackingRoundingDemo : KryptonForm
{
    /// <summary>Dark green outline for the demo (issue #3383 used black; green reads clearly without a harsh black halo).</summary>
    private static readonly Color DemoOutline = Color.FromArgb(28, 92, 28);

    public Bug3383KryptonButtonStateTrackingRoundingDemo()
    {
        InitializeComponent();
    }
}

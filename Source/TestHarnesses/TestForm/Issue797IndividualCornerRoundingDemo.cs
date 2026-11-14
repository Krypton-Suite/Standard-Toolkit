#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Manual demo for GitHub issue #797: per-corner border rounding on palette borders.
/// </summary>
public partial class Issue797IndividualCornerRoundingDemo : KryptonForm
{
    public Issue797IndividualCornerRoundingDemo()
    {
        InitializeComponent();
        ConfigureCornerRounding();
    }

    private void ConfigureCornerRounding()
    {
        kbIndividualCorners.StateCommon.Border.RoundingTopLeft = 5F;
        kbIndividualCorners.StateCommon.Border.RoundingTopRight = 5F;
        kbIndividualCorners.StateCommon.Border.RoundingBottomLeft = 5F;
        kbIndividualCorners.StateCommon.Border.RoundingBottomRight = 0F;
    }
}

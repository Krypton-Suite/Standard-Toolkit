#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

[ToolboxBitmap(typeof(ToolStrip)), Description("A standard tool strip equipped with the Krypton theme."), ToolboxItem(true)]
public class KryptonBasicToolStrip : ToolStrip
{
    #region Constructor
    public KryptonBasicToolStrip()
    {
        // Use Krypton
        RenderMode = ToolStripRenderMode.ManagerRenderMode;
    }
    #endregion
}
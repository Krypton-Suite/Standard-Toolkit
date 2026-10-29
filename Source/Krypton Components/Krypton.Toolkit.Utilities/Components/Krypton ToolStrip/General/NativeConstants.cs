#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

internal sealed class NativeConstants
{
    #region Static Fields

    internal const uint WM_MOUSEACTIVATE = 0x21;
    internal const uint MA_ACTIVATE = 1;
    internal const uint MA_ACTIVATEANDEAT = 2;
    internal const uint MA_NOACTIVATE = 3;
    internal const uint MA_NOACTIVATEANDEAT = 4;
    
    #endregion

    #region Identity

    public NativeConstants()
    {

    }
    
    #endregion
}

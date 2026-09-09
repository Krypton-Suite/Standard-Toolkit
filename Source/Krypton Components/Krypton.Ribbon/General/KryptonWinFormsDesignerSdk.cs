#region BSD License
/*
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Assembly names used by <see cref="DesignerAttribute"/> for in-process and out-of-process designers.
/// </summary>
internal static class KryptonWinFormsDesignerSdk
{
#if NETFRAMEWORK
    internal const string AssemblyName = "Krypton.Ribbon";
#else
    internal const string AssemblyName = "Krypton.Ribbon.Design";
#endif
}

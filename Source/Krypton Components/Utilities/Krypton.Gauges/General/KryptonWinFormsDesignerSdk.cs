#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, tobitege, Lesandro, KamaniAR, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Gauges;

/// <summary>
/// Assembly names used by <see cref="System.ComponentModel.DesignerAttribute"/> for in-process and out-of-process designers.
/// </summary>
internal static class KryptonWinFormsDesignerSdk
{
#if NETFRAMEWORK
    internal const string AssemblyName = "Krypton.Gauges";
#else
    internal const string AssemblyName = "Krypton.Gauges.Design";
#endif
}

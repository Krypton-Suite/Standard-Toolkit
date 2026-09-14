#region BSD License
/*
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Assembly names used by <see cref="DesignerAttribute"/> for in-process and out-of-process designers.
/// </summary>
internal static class KryptonWinFormsDesignerSdk
{
#if NETFRAMEWORK
    internal const string AssemblyName = "Krypton.Toolkit.Utilities";
    internal const string ToolkitAssemblyName = "Krypton.Toolkit";
#else
    internal const string AssemblyName = "Krypton.Toolkit.Utilities.Design";
    internal const string ToolkitAssemblyName = "Krypton.Toolkit.Design";
#endif

    /// <summary>
    /// <see cref="EditorAttribute"/> type name for the image picker (see Toolkit helper).
    /// </summary>
#if NETFRAMEWORK
    internal const string ImageEditor = "Krypton.Toolkit.KryptonDesignerImageEditor, Krypton.Toolkit";
#else
    internal const string ImageEditor = "Krypton.Toolkit.Design.Client.KryptonDesignerImageEditor, Krypton.Toolkit.Design.Client";
#endif
}

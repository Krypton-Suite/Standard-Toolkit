#region BSD License
/*
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Assembly names used by <see cref="DesignerAttribute"/> for the in-process (.NET Framework)
/// and out-of-process (WinForms Designer Extensibility SDK) designers.
/// </summary>
internal static class KryptonWinFormsDesignerSdk
{
#if NETFRAMEWORK
    /// <summary>
    /// Runtime assembly that contains designers for .NET Framework (in-process Visual Studio designer).
    /// </summary>
    internal const string AssemblyName = "Krypton.Toolkit";
#else
    /// <summary>
    /// DesignToolsServer assembly that contains SDK designers for .NET (out-of-process designer).
    /// </summary>
    internal const string AssemblyName = "Krypton.Toolkit.Design";
#endif

    /// <summary>
    /// <see cref="EditorAttribute"/> type name for the image picker.
    /// Framework uses the in-process editor; modern TFMs use the Client assembly-qualified name.
    /// Must match <c>KryptonDesignerEditorNames.ImageEditor</c>.
    /// </summary>
#if NETFRAMEWORK
    internal const string ImageEditor = "Krypton.Toolkit.KryptonDesignerImageEditor, Krypton.Toolkit";
#else
    internal const string ImageEditor = "Krypton.Toolkit.Design.Client.KryptonDesignerImageEditor, Krypton.Toolkit.Design.Client";
#endif

    /// <summary>
    /// <see cref="EditorAttribute"/> type name for the folder picker.
    /// </summary>
#if NETFRAMEWORK
    internal const string FolderNameEditor = "Krypton.Toolkit.KryptonDesignerFolderNameEditor, Krypton.Toolkit";
#else
    internal const string FolderNameEditor = "Krypton.Toolkit.Design.Client.KryptonDesignerFolderNameEditor, Krypton.Toolkit.Design.Client";
#endif

    /// <summary>
    /// <see cref="EditorAttribute"/> type name for initial-directory folder properties.
    /// </summary>
#if NETFRAMEWORK
    internal const string InitialDirectoryEditor = "Krypton.Toolkit.KryptonInitialDirectoryEditor, Krypton.Toolkit";
#else
    internal const string InitialDirectoryEditor = "Krypton.Toolkit.Design.Client.KryptonDesignerFolderNameEditor, Krypton.Toolkit.Design.Client";
#endif
}

#region BSD License
/*
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit.Design.Protocol;

/// <summary>
/// Assembly-qualified editor type names used on modern TFMs.
/// Keep these in sync with <c>KryptonWinFormsDesignerSdk.ImageEditor</c> / folder constants
/// in the runtime assemblies. SDK 1.6's net472 Client surface has no type-routing API;
/// Visual Studio resolves these strings from <c>Design/WinForms/</c> Client DLLs.
/// </summary>
public static class KryptonDesignerEditorNames
{
    /// <summary>
    /// Client-hosted image picker.
    /// </summary>
    public const string ImageEditor = "Krypton.Toolkit.Design.Client.KryptonDesignerImageEditor, Krypton.Toolkit.Design.Client";

    /// <summary>
    /// Client-hosted folder picker.
    /// </summary>
    public const string FolderNameEditor = "Krypton.Toolkit.Design.Client.KryptonDesignerFolderNameEditor, Krypton.Toolkit.Design.Client";

    /// <summary>
    /// Folder picker used for initial-directory properties (same Client editor).
    /// </summary>
    public const string InitialDirectoryEditor = FolderNameEditor;

    /// <summary>
    /// Folder picker used for selected-path properties (same Client editor).
    /// </summary>
    public const string SelectedPathEditor = FolderNameEditor;
}

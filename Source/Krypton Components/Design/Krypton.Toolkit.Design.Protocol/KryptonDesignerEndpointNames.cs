#region BSD License
/*
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit.Design.Protocol;

/// <summary>
/// Names of JSON-RPC endpoints used between the Visual Studio client and DesignToolsServer.
/// </summary>
public static class KryptonDesignerEndpointNames
{
    /// <summary>
    /// Edits an <see cref="System.Drawing.Image"/> value in Visual Studio and returns PNG bytes.
    /// </summary>
    public const string EditImage = "Krypton.Designer.EditImage";

    /// <summary>
    /// Edits a folder path in Visual Studio.
    /// </summary>
    public const string EditFolderPath = "Krypton.Designer.EditFolderPath";
}

#region BSD License
/*
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit.Design.Protocol;

/// <summary>
/// Request to edit an image on the Visual Studio client.
/// </summary>
public sealed class KryptonDesignerEditImageRequest
{
    /// <summary>
    /// Gets or sets PNG bytes for the current image, or <see langword="null"/> when empty.
    /// </summary>
    public byte[]? ImagePng { get; set; }
}

/// <summary>
/// Response from the Visual Studio client after editing an image.
/// </summary>
public sealed class KryptonDesignerEditImageResponse
{
    /// <summary>
    /// Gets or sets a value indicating whether the user confirmed a new image.
    /// </summary>
    public bool Accepted { get; set; }

    /// <summary>
    /// Gets or sets PNG bytes for the selected image, or <see langword="null"/> to clear.
    /// </summary>
    public byte[]? ImagePng { get; set; }
}

/// <summary>
/// Request to edit a folder path on the Visual Studio client.
/// </summary>
public sealed class KryptonDesignerEditFolderRequest
{
    /// <summary>
    /// Gets or sets the current folder path.
    /// </summary>
    public string? Path { get; set; }
}

/// <summary>
/// Response from the Visual Studio client after editing a folder path.
/// </summary>
public sealed class KryptonDesignerEditFolderResponse
{
    /// <summary>
    /// Gets or sets a value indicating whether the user confirmed a path.
    /// </summary>
    public bool Accepted { get; set; }

    /// <summary>
    /// Gets or sets the selected folder path.
    /// </summary>
    public string? Path { get; set; }
}

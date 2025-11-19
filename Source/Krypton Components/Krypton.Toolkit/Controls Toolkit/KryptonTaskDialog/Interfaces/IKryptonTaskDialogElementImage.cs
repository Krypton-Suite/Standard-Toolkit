#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

public interface IKryptonTaskDialogElementContentImage
{
    /// <summary>
    /// Image to display
    /// </summary>
    public Image? Image { get; set; }

    /// <summary>
    /// Size of the image to display
    /// </summary>
    public Size Size { get; set; }

    /// <summary>
    /// If the image is to be displayed.
    /// </summary>
    public bool Visible { get; set; }
}

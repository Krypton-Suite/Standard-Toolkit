#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Event arguments for the drop-down menu of a gallery.
/// </summary>
public class GalleryDropMenuEventArgs : CancelEventArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the GalleryDropMenuEventArgs class.
    /// </summary>
    /// <param name="contextMenu">Context menu.</param>
    public GalleryDropMenuEventArgs(KryptonContextMenu contextMenu) => KryptonContextMenu = contextMenu;

    #endregion

    #region Public
    /// <summary>
    /// KryptonContextMenu for display.
    /// </summary>
    public KryptonContextMenu? KryptonContextMenu { get; }

    #endregion
}
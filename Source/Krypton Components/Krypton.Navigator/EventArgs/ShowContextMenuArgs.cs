#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Navigator;

/// <summary>
/// Details for a close button action event.
/// </summary>
public class ShowContextMenuArgs : KryptonPageCancelEventArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the ShowContextMenuArgs class.
    /// </summary>
    /// <param name="page">Page effected by event.</param>
    /// <param name="index">Index of page in the owning collection.</param>
    public ShowContextMenuArgs(KryptonPage? page, int index)
        : base(page, index)
    {
        if (page != null)
        {
            ContextMenuStrip = page.ContextMenuStrip;
            KryptonContextMenu = page.KryptonContextMenu;
        }
    }
    #endregion

    #region ContextMenuStrip
    /// <summary>
    /// Gets and sets the context menu strip.
    /// </summary>
    public ContextMenuStrip? ContextMenuStrip { get; set; }

    #endregion

    #region KryptonContextMenu
    /// <summary>
    /// Gets and sets the context menu strip.
    /// </summary>
    public KryptonContextMenu? KryptonContextMenu { get; set; }

    #endregion
}
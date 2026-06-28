#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Docking;

/// <summary>
/// Cancellable event arguments for drop-down context menu display with associated page context.
/// </summary>
public class CancelDropDownEventArgs : CancelEventArgs
{
    #region Identity
    /// <summary>
    /// Associates the context menu and page with the event; cancellation defaults to false.
    /// </summary>
    /// <param name="contextMenu">Context menu about to be shown; may be null.</param>
    /// <param name="page">Page associated with the drop-down; may be null.</param>
    public CancelDropDownEventArgs(KryptonContextMenu? contextMenu, KryptonPage? page)
        : base(false)
    {
        KryptonContextMenu = contextMenu;
        Page = page;
    }
    #endregion

    #region Public
    /// <summary>
    /// Context menu about to be shown; may be null.
    /// </summary>
    public KryptonContextMenu? KryptonContextMenu { get; }

    /// <summary>
    /// Page associated with the drop-down; may be null.
    /// </summary>
    public KryptonPage? Page { get; }

    #endregion
}

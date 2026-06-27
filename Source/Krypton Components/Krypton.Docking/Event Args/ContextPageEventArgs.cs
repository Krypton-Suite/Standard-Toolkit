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
/// Cancellable event arguments for page context menu display and customization before the menu is shown.
/// </summary>
public class ContextPageEventArgs : CancelEventArgs
{
    #region Identity
    /// <summary>
    /// Associates the page, context menu, and initial cancellation state with the event.
    /// </summary>
    /// <param name="page">Page whose context menu is shown; may be null.</param>
    /// <param name="contextMenu">Context menu available for customization before display.</param>
    /// <param name="cancel">Initial value indicating whether menu display should be suppressed.</param>
    public ContextPageEventArgs(KryptonPage? page, 
        KryptonContextMenu contextMenu,
        bool cancel)
        : base(cancel)
    {
        Page = page;
        KryptonContextMenu = contextMenu;
    }
    #endregion

    #region Public
    /// <summary>
    /// Page whose context menu is shown; may be null.
    /// </summary>
    public KryptonPage? Page { get; }

    /// <summary>
    /// Context menu available for customization before display; may be null.
    /// </summary>
    public KryptonContextMenu? KryptonContextMenu { get; }

    #endregion
}

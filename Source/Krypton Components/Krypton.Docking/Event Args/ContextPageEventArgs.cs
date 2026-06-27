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
/// Event payload for page context menu display where handlers may cancel showing or customize the menu.
/// </summary>
public class ContextPageEventArgs : CancelEventArgs
{
    #region Identity
    /// <summary>
    /// Captures the target page, context menu, and initial cancel flag for context menu display.
    /// </summary>
    /// <param name="page">Page whose context menu is being shown; may be null.</param>
    /// <param name="contextMenu">Context menu to display; handlers may customize items before display.</param>
    /// <param name="cancel">Initial cancel flag; when true the menu is not shown.</param>
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
    /// Page whose context menu is being shown; may be null.
    /// </summary>
    public KryptonPage? Page { get; }

    /// <summary>
    /// Context menu to display; handlers may customize items before display. May be null.
    /// </summary>
    public KryptonContextMenu? KryptonContextMenu { get; }

    #endregion
}

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

namespace Krypton.Docking;

/// <summary>
/// Event arguments for events that need a page and context menu.
/// </summary>
public class ContextPageEventArgs : CancelEventArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the ContextPageEventArgs class.
    /// </summary>
    /// <param name="page">Page associated with the context menu.</param>
    /// <param name="contextMenu">Context menu that can be customized.</param>
    /// <param name="cancel">Initial value for the cancel property.</param>
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
    /// Gets access to page associated with the context menu.
    /// </summary>
    public KryptonPage? Page { get; }

    /// <summary>
    /// Gets access to context menu that can be customized.
    /// </summary>
    public KryptonContextMenu? KryptonContextMenu { get; }

    #endregion
}
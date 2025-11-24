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
/// Event arguments for cancellable events that need to provide a unique name and context menu.
/// </summary>
public class CancelDropDownEventArgs : CancelEventArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the CancelDropDownEventArgs class.
    /// </summary>
    /// <param name="contextMenu">Reference to associated context menu.</param>
    /// <param name="page">Reference to the associated page.</param>
    public CancelDropDownEventArgs(KryptonContextMenu? contextMenu, KryptonPage? page)
        : base(false)
    {
        KryptonContextMenu = contextMenu;
        Page = page;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets a reference to the context menu.
    /// </summary>
    public KryptonContextMenu? KryptonContextMenu { get; }

    /// <summary>
    /// Gets a reference to the page.
    /// </summary>
    public KryptonPage? Page { get; }

    #endregion
}
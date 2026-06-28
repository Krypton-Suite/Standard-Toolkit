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
/// Event arguments raised when an auto-hidden group panel is added to or removed from an edge.
/// </summary>
public class AutoHiddenGroupPanelEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Records the panel control and edge auto-hidden docking element for the add or remove operation.
    /// </summary>
    /// <param name="autoHiddenPanel">Panel control sliding out from the auto-hidden edge.</param>
    /// <param name="element">Docking element for the auto-hidden edge hosting the panel.</param>
    public AutoHiddenGroupPanelEventArgs(KryptonAutoHiddenPanel autoHiddenPanel,
        KryptonDockingEdgeAutoHidden element)
    {
        AutoHiddenPanelControl = autoHiddenPanel;
        EdgeAutoHiddenElement = element;
    }
    #endregion

    #region Public
    /// <summary>
    /// Panel control sliding out from the auto-hidden edge.
    /// </summary>
    public KryptonAutoHiddenPanel AutoHiddenPanelControl { get; }

    /// <summary>
    /// Docking element for the auto-hidden edge hosting the panel.
    /// </summary>
    public KryptonDockingEdgeAutoHidden EdgeAutoHiddenElement { get; }

    #endregion
}

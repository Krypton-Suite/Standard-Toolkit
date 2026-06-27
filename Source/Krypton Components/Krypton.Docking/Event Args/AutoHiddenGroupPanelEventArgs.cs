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
/// Event payload when an auto-hidden group panel is added to or removed from a docking edge.
/// </summary>
public class AutoHiddenGroupPanelEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Captures the auto-hidden panel control and its owning edge docking element.
    /// </summary>
    /// <param name="autoHiddenPanel">Auto-hidden panel control that was added or removed.</param>
    /// <param name="element">Docking element that owns the auto-hidden panel.</param>
    public AutoHiddenGroupPanelEventArgs(KryptonAutoHiddenPanel autoHiddenPanel,
        KryptonDockingEdgeAutoHidden element)
    {
        AutoHiddenPanelControl = autoHiddenPanel;
        EdgeAutoHiddenElement = element;
    }
    #endregion

    #region Public
    /// <summary>
    /// Auto-hidden panel control that was added or removed; assigned at construction.
    /// </summary>
    public KryptonAutoHiddenPanel AutoHiddenPanelControl { get; }

    /// <summary>
    /// Docking element that owns the auto-hidden panel; assigned at construction.
    /// </summary>
    public KryptonDockingEdgeAutoHidden EdgeAutoHiddenElement { get; }

    #endregion
}

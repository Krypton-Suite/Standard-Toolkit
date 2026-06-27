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
/// Event arguments raised when an auto-hidden group is added or removed.
/// </summary>
public class AutoHiddenGroupEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Records the auto-hidden group control and its docking element for the add or remove operation.
    /// </summary>
    /// <param name="control">Auto-hidden group control being added or removed.</param>
    /// <param name="element">Docking element representing the auto-hidden group in the layout tree.</param>
    public AutoHiddenGroupEventArgs(KryptonAutoHiddenGroup control,
        KryptonDockingAutoHiddenGroup element)
    {
        AutoHiddenGroupControl = control;
        AutoHiddenGroupElement = element;
    }
    #endregion

    #region Public
    /// <summary>
    /// Auto-hidden group UI control being added or removed.
    /// </summary>
    public KryptonAutoHiddenGroup AutoHiddenGroupControl { get; }

    /// <summary>
    /// Docking element representing the auto-hidden group in the layout tree.
    /// </summary>
    public KryptonDockingAutoHiddenGroup AutoHiddenGroupElement { get; }

    #endregion
}

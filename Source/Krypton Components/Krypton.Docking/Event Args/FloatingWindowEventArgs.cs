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
/// Event arguments raised when a floating window is added to or removed from the docking layout.
/// </summary>
public class FloatingWindowEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Records the floating window control and docking element for the add or remove operation.
    /// </summary>
    /// <param name="floatingWindow">Floating window control being added or removed.</param>
    /// <param name="element">Docking element coordinating the floating window in the layout tree.</param>
    public FloatingWindowEventArgs(KryptonFloatingWindow floatingWindow,
        KryptonDockingFloatingWindow element)
    {
        FloatingWindow = floatingWindow;
        FloatingWindowElement = element;
    }
    #endregion

    #region Public
    /// <summary>
    /// Floating window UI control being added or removed.
    /// </summary>
    public KryptonFloatingWindow FloatingWindow { get; }

    /// <summary>
    /// Docking element coordinating the floating window in the layout tree.
    /// </summary>
    public KryptonDockingFloatingWindow FloatingWindowElement { get; }

    #endregion
}

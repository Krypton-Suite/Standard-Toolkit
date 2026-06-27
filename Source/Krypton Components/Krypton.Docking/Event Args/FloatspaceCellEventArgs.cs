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
/// Event arguments raised when a workspace cell is added to or removed from a floatspace.
/// </summary>
public class FloatspaceCellEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Captures the floatspace control, docking element, and workspace cell involved in the add or remove operation.
    /// </summary>
    /// <param name="floatspace">Floatspace control hosting the cell; may be null during removal.</param>
    /// <param name="element">Docking element coordinating the floatspace layout.</param>
    /// <param name="cell">Workspace cell being added to or removed from the floatspace.</param>
    public FloatspaceCellEventArgs(KryptonFloatspace? floatspace,
        KryptonDockingFloatspace element,
        KryptonWorkspaceCell cell)
    {
        FloatspaceControl = floatspace;
        FloatspaceElement = element;
        CellControl = cell;
    }
    #endregion

    #region Public
    /// <summary>
    /// Floatspace UI control hosting the cell; may be null during removal.
    /// </summary>
    public KryptonFloatspace? FloatspaceControl { get; }

    /// <summary>
    /// Docking element coordinating the floatspace layout in the docking tree.
    /// </summary>
    public KryptonDockingFloatspace FloatspaceElement { get; }

    /// <summary>
    /// Workspace cell added to or removed from the floatspace.
    /// </summary>
    public KryptonWorkspaceCell CellControl { get; }

    #endregion
}

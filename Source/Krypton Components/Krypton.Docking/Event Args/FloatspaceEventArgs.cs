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
/// Event payload when a floatspace control is added to or removed from the docking tree.
/// </summary>
public class FloatspaceEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Captures the floatspace control and its owning docking element.
    /// </summary>
    /// <param name="floatspace">Floatspace control that was added or removed; may be null.</param>
    /// <param name="element">Docking element that owns the floatspace control.</param>
    public FloatspaceEventArgs(KryptonFloatspace? floatspace,
        KryptonDockingFloatspace element)
    {
        FloatspaceControl = floatspace;
        FloatspaceElement = element;
    }
    #endregion

    #region Public
    /// <summary>
    /// Floatspace control that was added or removed; may be null.
    /// </summary>
    public KryptonFloatspace? FloatspaceControl { get; }

    /// <summary>
    /// Docking element that owns the floatspace control; assigned at construction.
    /// </summary>
    public KryptonDockingFloatspace FloatspaceElement { get; }

    #endregion
}

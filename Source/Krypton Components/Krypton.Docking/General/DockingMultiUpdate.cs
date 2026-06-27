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
/// RAII scope that signals StartUpdate and EndUpdate to the docking hierarchy for batched layout changes.
/// </summary>
public class DockingMultiUpdate : IDisposable
{
    #region Instance Fields
    private readonly IDockingElement _dockingElement;
    #endregion

    #region Identity
    /// <summary>
    /// Begins a multi-part update by propagating StartUpdate from the given root element.
    /// </summary>
    /// <param name="dockingElement">Reference to root element of docking hierarchy.</param>
    /// <exception cref="ArgumentNullException"><paramref name="dockingElement"/> is null.</exception>
    public DockingMultiUpdate(IDockingElement dockingElement)
    {

        // Inform docking elements that a multi-part update is starting
        _dockingElement = dockingElement ?? throw new ArgumentNullException(nameof(dockingElement));
        _dockingElement.PropogateAction(DockingPropogateAction.StartUpdate, null as string[]);
    }

    /// <summary>
    /// Ends the multi-part update by propagating EndUpdate from the root element.
    /// </summary>
    public void Dispose()
    {
        // Inform docking elements that a multi-part update has ended
        _dockingElement.PropogateAction(DockingPropogateAction.EndUpdate, null as string[]);
        GC.SuppressFinalize(this);
    }
    #endregion
}
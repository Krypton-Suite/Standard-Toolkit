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
/// Scope object that signals the start and end of a batched docking hierarchy update.
/// </summary>
public class DockingMultiUpdate : IDisposable
{
    #region Instance Fields
    private readonly IDockingElement _dockingElement;
    #endregion

    #region Identity
    /// <summary>
    /// Signals <see cref="DockingPropogateAction.StartUpdate"/> to the supplied hierarchy root.
    /// </summary>
    /// <param name="dockingElement">Root element whose descendants receive the start-update broadcast.</param>
    /// <exception cref="ArgumentNullException"><paramref name="dockingElement"/> is <see langword="null"/>.</exception>
    public DockingMultiUpdate(IDockingElement dockingElement)
    {

        // Inform docking elements that a multi-part update is starting
        _dockingElement = dockingElement ?? throw new ArgumentNullException(nameof(dockingElement));
        _dockingElement.PropogateAction(DockingPropogateAction.StartUpdate, null as string[]);
    }

    /// <summary>
    /// Signals <see cref="DockingPropogateAction.EndUpdate"/> to the hierarchy root supplied at construction.
    /// </summary>
    public void Dispose()
    {
        // Inform docking elements that a multi-part update has ended
        _dockingElement.PropogateAction(DockingPropogateAction.EndUpdate, null as string[]);
        GC.SuppressFinalize(this);
    }
    #endregion
}

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
/// Helper class used inside a 'using' statement to notify start and end of a multi-part update.
/// </summary>
public class DockingMultiUpdate : IDisposable
{
    #region Instance Fields
    private readonly IDockingElement _dockingElement;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the DockingMultiUpdate class.
    /// </summary>
    /// <param name="dockingElement">Reference to root element of docking hierarchy.</param>
    public DockingMultiUpdate(IDockingElement dockingElement)
    {

        // Inform docking elements that a multi-part update is starting
        _dockingElement = dockingElement ?? throw new ArgumentNullException(nameof(dockingElement));
        _dockingElement.PropogateAction(DockingPropogateAction.StartUpdate, null as string[]);
    }

    /// <summary>
    /// Release managed and unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        // Inform docking elements that a multi-part update has ended
        _dockingElement.PropogateAction(DockingPropogateAction.EndUpdate, null as string[]);
        GC.SuppressFinalize(this);
    }
    #endregion
}
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
/// Event payload raised while global docking layout is being loaded from XML.
/// </summary>
public class DockGlobalLoadingEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Captures the docking manager and XML reader active during global layout load.
    /// </summary>
    /// <param name="manager">Docking manager owning the load operation; may be null.</param>
    /// <param name="xmlReading">XML reader supplying persisted global docking data.</param>
    public DockGlobalLoadingEventArgs(KryptonDockingManager? manager,
        XmlReader xmlReading)
    {
        DockingManager = manager;
        XmlReader = xmlReading;
    }
    #endregion

    #region Public
    /// <summary>
    /// Docking manager owning the load operation; may be null.
    /// </summary>
    public KryptonDockingManager? DockingManager { get; }

    /// <summary>
    /// XML reader supplying persisted global docking data; handlers read custom elements from this stream.
    /// </summary>
    public XmlReader XmlReader { get; }

    #endregion
}

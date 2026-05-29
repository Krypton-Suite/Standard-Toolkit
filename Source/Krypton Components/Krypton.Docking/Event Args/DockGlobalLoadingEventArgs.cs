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
/// Event data for loading global docking configuration.
/// </summary>
public class DockGlobalLoadingEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the DockGlobalLoadingEventArgs class.
    /// </summary>
    /// <param name="manager">Reference to owning docking manager instance.</param>
    /// <param name="xmlReading">Xml reader for persisting custom data.</param>
    public DockGlobalLoadingEventArgs(KryptonDockingManager? manager,
        XmlReader xmlReading)
    {
        DockingManager = manager;
        XmlReader = xmlReading;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets the docking manager reference.
    /// </summary>
    public KryptonDockingManager? DockingManager { get; }

    /// <summary>
    /// Gets the xml reader.
    /// </summary>
    public XmlReader XmlReader { get; }

    #endregion
}
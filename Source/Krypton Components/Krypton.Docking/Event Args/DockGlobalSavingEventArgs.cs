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
/// Event arguments raised while global docking configuration is serialized to XML.
/// </summary>
public class DockGlobalSavingEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Associates the docking manager with the XML writer used to persist global configuration.
    /// </summary>
    /// <param name="manager">Docking manager performing the save operation; may be null.</param>
    /// <param name="xmlWriter">XML writer receiving global docking configuration data.</param>
    public DockGlobalSavingEventArgs(KryptonDockingManager? manager,
        XmlWriter xmlWriter)
    {
        DockingManager = manager;
        XmlWriter = xmlWriter;
    }
    #endregion

    #region Public
    /// <summary>
    /// Docking manager performing the save operation; may be null.
    /// </summary>
    public KryptonDockingManager? DockingManager { get; }

    /// <summary>
    /// XML writer receiving global docking configuration for custom persistence extensions.
    /// </summary>
    public XmlWriter XmlWriter { get; }

    #endregion
}

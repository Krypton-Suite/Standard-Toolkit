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
/// Event arguments raised while a single docking page configuration is deserialized from XML.
/// </summary>
public class DockPageLoadingEventArgs : DockGlobalLoadingEventArgs
{
    #region Identity
    /// <summary>
    /// Extends global loading event arguments with the page being restored from configuration XML.
    /// </summary>
    /// <param name="manager">Docking manager performing the load operation; may be null.</param>
    /// <param name="xmlReading">XML reader positioned at page configuration data.</param>
    /// <param name="page">Page being restored from XML; may be null while configuration is still being deserialized.</param>
    public DockPageLoadingEventArgs(KryptonDockingManager? manager,
        XmlReader xmlReading,
        KryptonPage? page)
        : base(manager, xmlReading) =>
        Page = page;

    #endregion

    #region Public
    /// <summary>
    /// Page being restored from XML; may be null while configuration is still being deserialized.
    /// </summary>
    public KryptonPage? Page { get; }

    #endregion
}

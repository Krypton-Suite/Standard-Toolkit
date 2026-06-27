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
/// Event payload raised while a single page docking layout is being loaded from XML.
/// </summary>
public class DockPageLoadingEventArgs : DockGlobalLoadingEventArgs
{
    #region Identity
    /// <summary>
    /// Captures the docking manager, XML reader, and page whose layout is being restored.
    /// </summary>
    /// <param name="manager">Docking manager owning the load operation; may be null.</param>
    /// <param name="xmlReading">XML reader supplying persisted page docking data.</param>
    /// <param name="page">Page whose layout is being restored; may be null.</param>
    public DockPageLoadingEventArgs(KryptonDockingManager? manager,
        XmlReader xmlReading,
        KryptonPage? page)
        : base(manager, xmlReading) =>
        Page = page;

    #endregion

    #region Public
    /// <summary>
    /// Page whose layout is being restored; may be null.
    /// </summary>
    public KryptonPage? Page { get; }

    #endregion
}

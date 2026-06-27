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
/// Event arguments raised while a single docking page configuration is serialized to XML.
/// </summary>
public class DockPageSavingEventArgs : DockGlobalSavingEventArgs
{
    #region Identity
    /// <summary>
    /// Extends global saving event arguments with the page whose configuration is being persisted.
    /// </summary>
    /// <param name="manager">Docking manager performing the save operation; may be null.</param>
    /// <param name="xmlWriter">XML writer receiving page configuration data.</param>
    /// <param name="page">Page whose docking configuration is being written to XML.</param>
    public DockPageSavingEventArgs(KryptonDockingManager? manager,
        XmlWriter xmlWriter,
        KryptonPage page)
        : base(manager, xmlWriter) =>
        Page = page;

    #endregion

    #region Public
    /// <summary>
    /// Page whose docking configuration is being written to XML.
    /// </summary>
    public KryptonPage Page { get; }

    #endregion
}

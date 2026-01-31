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
/// Event data for saving docking page configuration.
/// </summary>
public class DockPageSavingEventArgs : DockGlobalSavingEventArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the DockPageSavingEventArgs class.
    /// </summary>
    /// <param name="manager">Reference to owning docking manager instance.</param>
    /// <param name="xmlWriter">Xml writer for persisting custom data.</param>
    /// <param name="page">Reference to page being saved.</param>
    public DockPageSavingEventArgs(KryptonDockingManager? manager,
        XmlWriter xmlWriter,
        KryptonPage page)
        : base(manager, xmlWriter) =>
        Page = page;

    #endregion

    #region Public
    /// <summary>
    /// Gets the saving page reference.
    /// </summary>
    public KryptonPage Page { get; }

    #endregion
}
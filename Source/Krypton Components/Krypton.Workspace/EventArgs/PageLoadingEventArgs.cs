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

namespace Krypton.Workspace;

/// <summary>
/// Event data for persisting extra data for a workspace cell page.
/// </summary>
public class PageLoadingEventArgs : XmlLoadingEventArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the PageLoadingEventArgs class.
    /// </summary>
    /// <param name="workspace">Reference to owning workspace control.</param>
    /// <param name="page">Reference to owning workspace cell page.</param>
    /// <param name="xmlReader">Xml reader for persisting custom data.</param>
    public PageLoadingEventArgs(KryptonWorkspace workspace,
        KryptonPage? page,
        XmlReader xmlReader)
        : base(workspace, xmlReader) =>
        Page = page;

    #endregion

    #region Public
    /// <summary>
    /// Gets the workspace cell page reference.
    /// </summary>
    public KryptonPage? Page { get; set; }

    #endregion
}
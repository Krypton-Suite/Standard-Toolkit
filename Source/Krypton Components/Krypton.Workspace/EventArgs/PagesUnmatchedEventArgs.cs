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
/// Event data for listing pages unmatched during the load process.
/// </summary>
public class PagesUnmatchedEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the PagesUnmatchedEventArgs class.
    /// </summary>
    /// <param name="workspace">Reference to owning workspace control.</param>
    /// <param name="unmatched">List of pages unmatched during the load process.</param>
    public PagesUnmatchedEventArgs(KryptonWorkspace workspace,
        List<KryptonPage> unmatched)
    {
        Workspace = workspace;
        Unmatched = unmatched;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets the workspace reference.
    /// </summary>
    public KryptonWorkspace Workspace { get; }

    /// <summary>
    /// Gets the xml reader.
    /// </summary>
    public List<KryptonPage> Unmatched { get; }

    #endregion
}
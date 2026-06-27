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
/// Event arguments identifying a page by unique name where the pending docking operation can be cancelled.
/// </summary>
public class CancelUniqueNameEventArgs : UniqueNameEventArgs
{
    #region Identity
    /// <summary>
    /// Stores the page unique name and initial cancellation flag for the event.
    /// </summary>
    /// <param name="uniqueName">Stable unique name identifying the docking page.</param>
    /// <param name="cancel">Initial value indicating whether the pending operation should be suppressed.</param>
    public CancelUniqueNameEventArgs([DisallowNull] string uniqueName, bool cancel)
        : base(uniqueName) =>
        Cancel = cancel;

    #endregion

    #region Public
    /// <summary>
    /// When true, suppresses the pending docking operation identified by <see cref="UniqueNameEventArgs.UniqueName"/>.
    /// </summary>
    public bool Cancel { get; set; }

    #endregion
}

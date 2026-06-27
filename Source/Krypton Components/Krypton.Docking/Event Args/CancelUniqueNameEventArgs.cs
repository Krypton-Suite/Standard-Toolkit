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
/// Event payload for page layout change requests that handlers can veto.
/// </summary>
public class CancelUniqueNameEventArgs : UniqueNameEventArgs
{
    #region Identity
    /// <summary>
    /// Captures the page unique name and initial cancel flag for the layout request.
    /// </summary>
    /// <param name="uniqueName">Unique name of the page.</param>
    /// <param name="cancel">Initial cancel flag; when true the request is already vetoed before handlers run.</param>
    public CancelUniqueNameEventArgs([DisallowNull] string uniqueName, bool cancel)
        : base(uniqueName) =>
        Cancel = cancel;

    #endregion

    #region Public
    /// <summary>
    /// When true after handlers run, the requested layout change is not applied.
    /// </summary>
    public bool Cancel { get; set; }

    #endregion
}

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
/// Event arguments exposing a collection of pages for bulk docking operations.
/// </summary>
public class PagesEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Wraps the page collection supplied by the event source.
    /// </summary>
    /// <param name="pages">Collection of pages affected by or referenced in the event.</param>
    public PagesEventArgs(KryptonPageCollection pages) => Pages = pages;

    #endregion

    #region Public
    /// <summary>
    /// Read-only collection of pages affected by or referenced in the event.
    /// </summary>
    public KryptonPageCollection Pages { get; }

    #endregion
}

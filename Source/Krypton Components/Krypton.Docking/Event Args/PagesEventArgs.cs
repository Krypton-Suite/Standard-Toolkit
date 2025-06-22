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
/// Event arguments for events that need to provide a colletion of pages.
/// </summary>
public class PagesEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the PagesEventArgs class.
    /// </summary>
    /// <param name="pages">Collection of pages.</param>
    public PagesEventArgs(KryptonPageCollection pages) => Pages = pages;

    #endregion

    #region Public
    /// <summary>
    /// Gets access to a collection of pages.
    /// </summary>
    public KryptonPageCollection Pages { get; }

    #endregion
}
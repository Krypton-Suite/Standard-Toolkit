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
/// Event arguments raised when an auto-hidden page transitions between show states.
/// </summary>
public class AutoHiddenShowingStateEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Records the affected page and its new auto-hidden showing state.
    /// </summary>
    /// <param name="page">Page whose showing state changed; may be null.</param>
    /// <param name="state">Updated auto-hidden show state after the transition.</param>
    public AutoHiddenShowingStateEventArgs(KryptonPage? page, DockingAutoHiddenShowState state)
    {
        Page = page;
        NewState = state;
    }
    #endregion

    #region Public
    /// <summary>
    /// Page whose auto-hidden showing state changed; may be null.
    /// </summary>
    public KryptonPage? Page { get; }

    /// <summary>
    /// Updated auto-hidden show state after the transition.
    /// </summary>
    public DockingAutoHiddenShowState NewState { get; }

    #endregion
}

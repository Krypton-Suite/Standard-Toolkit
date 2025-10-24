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
/// Event arguments for the change in auto hidden page showing state.
/// </summary>
public class AutoHiddenShowingStateEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the AutoHiddenShowingStateEventArgs class.
    /// </summary>
    /// <param name="page">Page for which state has changed.</param>
    /// <param name="state">New state of the auto hidden page.</param>
    public AutoHiddenShowingStateEventArgs(KryptonPage? page, DockingAutoHiddenShowState state)
    {
        Page = page;
        NewState = state;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets the page that has had the state change.
    /// </summary>
    public KryptonPage? Page { get; }

    /// <summary>
    /// Gets the new state of the auto hidden page.
    /// </summary>
    public DockingAutoHiddenShowState NewState { get; }

    #endregion
}
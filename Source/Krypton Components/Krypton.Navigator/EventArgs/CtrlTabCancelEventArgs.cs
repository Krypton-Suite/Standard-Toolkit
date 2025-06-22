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

namespace Krypton.Navigator;

/// <summary>
/// Details for control tabbing events.
/// </summary>
public class CtrlTabCancelEventArgs : CancelEventArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the CtrlTabCancelEventArgs class.
    /// </summary>
    /// <param name="forward">Tabbing in forward or backwards direction.</param>
    public CtrlTabCancelEventArgs(bool forward) => Forward = forward;

    #endregion

    #region Forward
    /// <summary>
    /// Gets a value indicating if control tabbing forward.
    /// </summary>
    public bool Forward { get; }

    #endregion
}
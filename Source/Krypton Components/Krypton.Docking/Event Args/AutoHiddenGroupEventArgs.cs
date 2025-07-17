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
/// Event arguments for a AutoHiddenGroupAdding/AutoHiddenGroupRemoved events.
/// </summary>
public class AutoHiddenGroupEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the AutoHiddenGroupEventArgs class.
    /// </summary>
    /// <param name="control">Reference to auto hidden group control instance.</param>
    /// <param name="element">Reference to docking auto hidden group element that is managing the control.</param>
    public AutoHiddenGroupEventArgs(KryptonAutoHiddenGroup control,
        KryptonDockingAutoHiddenGroup element)
    {
        AutoHiddenGroupControl = control;
        AutoHiddenGroupElement = element;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets a reference to the KryptonAutoHiddenGroup control.
    /// </summary>
    public KryptonAutoHiddenGroup AutoHiddenGroupControl { get; }

    /// <summary>
    /// Gets a reference to the KryptonDockingAutoHiddenGroup that is managing the group.
    /// </summary>
    public KryptonDockingAutoHiddenGroup AutoHiddenGroupElement { get; }

    #endregion
}
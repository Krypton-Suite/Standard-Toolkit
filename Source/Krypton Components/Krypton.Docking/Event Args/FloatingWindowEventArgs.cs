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
/// Event arguments for a FloatingWindowAdding/FloatingWindowRemoved event.
/// </summary>
public class FloatingWindowEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the FloatingWindowEventArgs class.
    /// </summary>
    /// <param name="floatingWindow">Reference to floating window instance.</param>
    /// <param name="element">Reference to docking floating winodw element that is managing the floating window.</param>
    public FloatingWindowEventArgs(KryptonFloatingWindow floatingWindow,
        KryptonDockingFloatingWindow element)
    {
        FloatingWindow = floatingWindow;
        FloatingWindowElement = element;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets a reference to the KryptonFloatingWindow control.
    /// </summary>
    public KryptonFloatingWindow FloatingWindow { get; }

    /// <summary>
    /// Gets a reference to the KryptonDockingFloatingWindow that is managing the dockspace.
    /// </summary>
    public KryptonDockingFloatingWindow FloatingWindowElement { get; }

    #endregion
}
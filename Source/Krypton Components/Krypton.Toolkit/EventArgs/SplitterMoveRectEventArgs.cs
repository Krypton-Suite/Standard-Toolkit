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

namespace Krypton.Toolkit;

/// <summary>
/// Provides a movement rectangle that will be used to limit separator movement.
/// </summary>
public class SplitterMoveRectMenuArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the SplitterMoveRectMenuArgs class.
    /// </summary>
    /// <param name="moveRect">Initial movement rectangle that limits separator movements.</param>
    public SplitterMoveRectMenuArgs(Rectangle moveRect) => MoveRect = moveRect;

    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the movement box for a separator.
    /// </summary>
    public Rectangle MoveRect { get; set; }

    #endregion
}
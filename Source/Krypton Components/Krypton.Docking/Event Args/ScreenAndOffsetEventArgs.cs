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
/// Event arguments for events that need a screen point and element offset.
/// </summary>
public class ScreenAndOffsetEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the ScreenAndOffsetEventArgs class.
    /// </summary>
    /// <param name="screenPoint">Screen point.</param>
    /// <param name="elementOffset">Element offset.</param>
    public ScreenAndOffsetEventArgs(Point screenPoint, Point elementOffset)
    {
        ScreenPoint = screenPoint;
        ElementOffset = elementOffset;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets the screen point.
    /// </summary>
    public Point ScreenPoint { get; }

    /// <summary>
    /// Gets the element offset.
    /// </summary>
    public Point ElementOffset { get; }

    #endregion
}
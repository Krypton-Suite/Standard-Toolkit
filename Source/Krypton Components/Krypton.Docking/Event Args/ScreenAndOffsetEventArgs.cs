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
/// Event payload carrying screen coordinates when a floating window caption drag begins.
/// </summary>
public class ScreenAndOffsetEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Captures the screen pointer position and offset from the window origin at drag start.
    /// </summary>
    /// <param name="screenPoint">Screen coordinates of the pointer when dragging started.</param>
    /// <param name="elementOffset">Pointer offset from the floating window top-left corner at drag start.</param>
    public ScreenAndOffsetEventArgs(Point screenPoint, Point elementOffset)
    {
        ScreenPoint = screenPoint;
        ElementOffset = elementOffset;
    }
    #endregion

    #region Public
    /// <summary>
    /// Screen coordinates of the pointer when dragging started; assigned at construction.
    /// </summary>
    public Point ScreenPoint { get; }

    /// <summary>
    /// Pointer offset from the floating window top-left corner at drag start; assigned at construction.
    /// </summary>
    public Point ElementOffset { get; }

    #endregion
}

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
/// Event arguments raised when a floating window caption drag begins, supplying screen mouse position and window-relative offset.
/// </summary>
public class ScreenAndOffsetEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Captures the screen mouse position and window-relative offset at caption drag start.
    /// </summary>
    /// <param name="screenPoint">Mouse position in screen coordinates when the drag begins.</param>
    /// <param name="elementOffset">Mouse offset relative to the window origin at drag start.</param>
    public ScreenAndOffsetEventArgs(Point screenPoint, Point elementOffset)
    {
        ScreenPoint = screenPoint;
        ElementOffset = elementOffset;
    }
    #endregion

    #region Public
    /// <summary>
    /// Mouse position in screen coordinates when the caption drag begins.
    /// </summary>
    public Point ScreenPoint { get; }

    /// <summary>
    /// Mouse offset relative to the window origin at caption drag start.
    /// </summary>
    public Point ElementOffset { get; }

    #endregion
}

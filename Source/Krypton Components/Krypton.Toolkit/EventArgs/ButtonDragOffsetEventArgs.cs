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
/// Details for an event that provides a button drag offset value.
/// </summary>
public class ButtonDragOffsetEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the ButtonDragOffsetEventArgs class.
    /// </summary>
    /// <param name="offset">Mouse offset for button dragging.</param>
    public ButtonDragOffsetEventArgs(Point offset) => PointOffset = offset;

    #endregion

    #region Point
    /// <summary>
    /// Gets access to the left mouse dragging offer.
    /// </summary>
    public Point PointOffset { get; }

    #endregion
}
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
/// Details for an cancellable event that provides a position, offset and control value.
/// </summary>
public class DragStartEventCancelArgs : PointEventCancelArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the DragStartEventCancelArgs class.
    /// </summary>
    /// <param name="point">Point associated with event.</param>
    /// <param name="offset">Offset associated with event.</param>
    /// <param name="c">Control that is generating the drag start.</param>
    public DragStartEventCancelArgs(Point point, Point offset, Control c)
        : base(point)
    {
        Offset = offset;
        Control = c;
    }
    #endregion

    #region Point
    /// <summary>
    /// Gets and sets the offset.
    /// </summary>
    public Point Offset { get; set; }

    #endregion

    #region Point
    /// <summary>
    /// Gets the control starting the drag.
    /// </summary>
    public Control Control { get; }

    #endregion
}
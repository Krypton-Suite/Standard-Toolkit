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
/// Details for an event that provides a Point value.
/// </summary>
public class PointEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the PointEventArgs class.
    /// </summary>
    /// <param name="point">Point associated with event.</param>
    public PointEventArgs(Point point) => Point = point;

    #endregion

    #region Point
    /// <summary>
    /// Gets and sets the point.
    /// </summary>
    public Point Point { get; set; }

    #endregion
}
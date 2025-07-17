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
/// Details for context menu related events.
/// </summary>
public class ViewControlHitTestArgs : CancelEventArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewControlHitTestArgs class.
    /// </summary>
    /// <param name="pt">Point associated with hit test message.</param>
    public ViewControlHitTestArgs(Point pt)
        : base(true)
    {
        Point = pt;
        Result = IntPtr.Zero;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets access to the point.
    /// </summary>
    public Point Point { get; }

    /// <summary>
    /// Gets and sets the result.
    /// </summary>
    public IntPtr Result { get; set; }

    #endregion
}
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
/// Details for need layout events.
/// </summary>
public class NeedLayoutEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the NeedLayoutEventArgs class.
    /// </summary>
    /// <param name="needLayout">Does the layout need regenerating.</param>
    public NeedLayoutEventArgs(bool needLayout)
        : this(needLayout, Rectangle.Empty)
    {
    }

    /// <summary>
    /// Initialize a new instance of the NeedLayoutEventArgs class.
    /// </summary>
    /// <param name="needLayout">Does the layout need regenerating.</param>
    /// <param name="invalidRect">Specifies an invalidation rectangle.</param>
    public NeedLayoutEventArgs(bool needLayout,
        Rectangle invalidRect)
    {
        NeedLayout = needLayout;
        InvalidRect = invalidRect;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets a value indicating if the layout needs regenerating.
    /// </summary>
    public bool NeedLayout { get; }

    /// <summary>
    /// Gets the rectangle to be invalidated.
    /// </summary>
    public Rectangle InvalidRect { get; }

    #endregion
}
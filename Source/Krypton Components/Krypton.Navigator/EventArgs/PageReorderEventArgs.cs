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

namespace Krypton.Navigator;

/// <summary>
/// Details of an event that is fired just before a page is reordered.
/// </summary>
public class PageReorderEventArgs : CancelEventArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the PageReorderEventArgs class.
    /// </summary>
    /// <param name="pageMoving">Reference to page being moved.</param>
    /// <param name="pageTarget">Reference to target paged.</param>
    /// <param name="movingBefore">True if moving page is to be positioned before the target; otherwise after the target.</param>
    public PageReorderEventArgs(KryptonPage pageMoving, 
        KryptonPage pageTarget, 
        bool movingBefore)
    {
        PageMoving = pageMoving;
        PageTarget = pageTarget;
        MovingBefore = movingBefore;
    }
    #endregion

    #region PageMoving
    /// <summary>
    /// Gets the page that is being moved.
    /// </summary>
    public KryptonPage PageMoving { get; }

    #endregion

    #region PageTarget
    /// <summary>
    /// Gets the page that is the target for the move.
    /// </summary>
    public KryptonPage PageTarget { get; }

    #endregion

    #region MovingBefore
    /// <summary>
    /// Gets a value indicating if the page being moved is to be placed before the target page.
    /// </summary>
    public bool MovingBefore { get; }

    #endregion
}
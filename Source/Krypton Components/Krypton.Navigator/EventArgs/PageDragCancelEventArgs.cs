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
/// Details for an cancellable event that provides pages associated with a page dragging event.
/// </summary>
public class PageDragCancelEventArgs : CancelEventArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the PageDragCancelEventArgs class.
    /// </summary>
    /// <param name="elementOffset">Offset from the top left of the element.</param>
    /// <param name="screenPoint">Screen point of the mouse.</param>
    /// <param name="c">Control that started the drag operation.</param>
    /// <param name="pages">Array of event associated pages.</param>
    public PageDragCancelEventArgs(Point screenPoint,
        Point elementOffset,
        Control c,
        KryptonPage[] pages)
    {
        ScreenPoint = screenPoint;
        ElementOffset = elementOffset;
        Control = c;
        Pages = new KryptonPageCollection();

        if (pages != null)
        {
            Pages.AddRange(pages);
        }
    }

    /// <summary>
    /// Initialize a new instance of the PageDragCancelEventArgs class.
    /// </summary>
    /// <param name="screenPoint">Screen point of the mouse.</param>
    /// <param name="elementOffset">Offset from the top left of the element.</param>
    /// <param name="c">Control that started the drag operation.</param>
    /// <param name="pages">Collection of event associated pages.</param>
    public PageDragCancelEventArgs(Point screenPoint,
        Point elementOffset,
        Control c,
        KryptonPageCollection pages)
    {
        ScreenPoint = screenPoint;
        ElementOffset = elementOffset;
        Control = c;
        Pages = pages;
    }
    #endregion

    #region ScreenPoint
    /// <summary>
    /// Gets access to the associated screen point.
    /// </summary>
    public Point ScreenPoint { get; }

    #endregion

    #region ElementOffset
    /// <summary>
    /// Gets access to the associated element offset.
    /// </summary>
    public Point ElementOffset { get; }

    #endregion

    #region ElementOffset
    /// <summary>
    /// Gets access to the control that started the drag operation.
    /// </summary>
    public Control Control { get; }

    #endregion

    #region Pages
    /// <summary>
    /// Gets access to the collection of pages.
    /// </summary>
    public KryptonPageCollection Pages { get; }

    #endregion
}
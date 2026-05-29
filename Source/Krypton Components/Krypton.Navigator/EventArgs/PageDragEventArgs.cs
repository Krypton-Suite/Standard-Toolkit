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
/// Details for an event that provides pages associated with a page dragging event.
/// </summary>
public class PageDragEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonPageDragEventArgs class.
    /// </summary>
    /// <param name="screenPoint">Screen point of the mouse.</param>
    /// <param name="pages">Array of event associated pages.</param>
    public PageDragEventArgs(Point screenPoint,
        KryptonPage[] pages)
    {
        ScreenPoint = screenPoint;
        Pages = new KryptonPageCollection();

        if (pages != null)
        {
            Pages.AddRange(pages);
        }
    }

    /// <summary>
    /// Initialize a new instance of the KryptonPageDragEventArgs class.
    /// </summary>
    /// <param name="screenPoint">Screen point of the mouse.</param>
    /// <param name="pages">Collection of event associated pages.</param>
    public PageDragEventArgs(Point screenPoint,
        KryptonPageCollection pages)
    {
        ScreenPoint = screenPoint;
        Pages = pages;
    }
    #endregion

    #region ScreenPoint
    /// <summary>
    /// Gets access to the associated screen point.
    /// </summary>
    public Point ScreenPoint { get; }

    #endregion

    #region Pages
    /// <summary>
    /// Gets access to the collection of pages.
    /// </summary>
    public KryptonPageCollection Pages { get; }

    #endregion
}
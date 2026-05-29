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
public class PageDragEndEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the PageDragEndEventArgs class.
    /// </summary>
    /// <param name="dropped">True if a drop was performed; otherwise false.</param>
    /// <param name="pages">Array of event associated pages.</param>
    public PageDragEndEventArgs(bool dropped,
        KryptonPage[]? pages)
    {
        Dropped = dropped;
        Pages = new KryptonPageCollection();

        if (pages != null)
        {
            Pages.AddRange(pages);
        }
    }
    #endregion

    #region Dropped
    /// <summary>
    /// Gets a value indicating if the drop was performed.
    /// </summary>
    public bool Dropped { get; }

    #endregion

    #region Pages
    /// <summary>
    /// Gets access to the collection of pages.
    /// </summary>
    public KryptonPageCollection Pages { get; }

    #endregion
}
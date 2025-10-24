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
/// Details for an event that indicates a page is being dropped.
/// </summary>
public class PageDropEventArgs : CancelEventArgs
{
    #region Instance Fields
    private KryptonPage? _page;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PageDropEventArgs class.
    /// </summary>
    /// <param name="page">Page that is being dropped.</param>
    public PageDropEventArgs(KryptonPage? page) => _page = page;

    #endregion

    #region Page
    /// <summary>
    /// Gets and sets the page to be dropped.
    /// </summary>
    public KryptonPage? Page
    {
        get => _page;
        set => _page = value;
    }
    #endregion
}
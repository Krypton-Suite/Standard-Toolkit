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
/// Details for a popup page event.
/// </summary>
public class PopupPageEventArgs : KryptonPageCancelEventArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the PopupPageEventArgs class.
    /// </summary>
    /// <param name="page">Page effected by event.</param>
    /// <param name="index">Index of page in the owning collection.</param>
    /// <param name="screenRect">Screen rectangle for showing the popup.</param>
    public PopupPageEventArgs(KryptonPage? page, 
        int index, 
        Rectangle screenRect)
        : base(page, index) =>
        ScreenRect = screenRect;

    #endregion

    #region ScreenRect
    /// <summary>
    /// Gets and sets the screen rectangle for showing the popup page.
    /// </summary>
    public Rectangle ScreenRect { get; set; }

    #endregion
}
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
/// Details for a close button action event.
/// </summary>
public class CloseActionEventArgs : KryptonPageEventArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the CloseActionEventArgs class.
    /// </summary>
    /// <param name="page">Page effected by event.</param>
    /// <param name="index">Index of page in the owning collection.</param>
    /// <param name="action">Close action to take.</param>
    public CloseActionEventArgs(KryptonPage page, 
        int index, 
        CloseButtonAction action)
        : base(page, index) =>
        Action = action;

    #endregion

    #region Action
    /// <summary>
    /// Gets and sets the close action to take.
    /// </summary>
    public CloseButtonAction Action { get; set; }

    #endregion
}
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
/// Details for a direction button (next/previous) action event.
/// </summary>
public class DirectionActionEventArgs : KryptonPageEventArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the DirectionActionEventArgs class.
    /// </summary>
    /// <param name="page">Page effected by event.</param>
    /// <param name="index">Index of page in the owning collection.</param>
    /// <param name="action">Previous/Next action to take.</param>
    public DirectionActionEventArgs(KryptonPage page, 
        int index,
        DirectionButtonAction action)
        : base(page, index) =>
        Action = action;

    #endregion

    #region Action
    /// <summary>
    /// Gets and sets the next/previous action to take.
    /// </summary>
    public DirectionButtonAction Action { get; set; }

    #endregion
}
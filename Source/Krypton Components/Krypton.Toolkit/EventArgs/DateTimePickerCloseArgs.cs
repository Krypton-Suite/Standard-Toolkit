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
/// Details about the context menu that has been closed up on a KryptonDateTimePicker.
/// </summary>
public class DateTimePickerCloseArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the DateTimePickerCloseArgs class.
    /// </summary>
    /// <param name="kcm">KryptonContextMenu that can be examined.</param>
    public DateTimePickerCloseArgs(KryptonContextMenu kcm) => KryptonContextMenu = kcm;

    #endregion

    #region Public
    /// <summary>
    /// Gets access to the KryptonContextMenu instance.
    /// </summary>
    public KryptonContextMenu? KryptonContextMenu { get; }

    #endregion
}
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
/// Details about the context menu about to be shown when clicking the drop-down button on a KryptonDateTimePicker.
/// </summary>
public class DateTimePickerDropArgs : CancelEventArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the DateTimePickerDropArgs class.
    /// </summary>
    /// <param name="kcm">KryptonContextMenu that can be customized.</param>
    /// <param name="positionH">Relative horizontal position of the KryptonContextMenu.</param>
    /// <param name="positionV">Relative vertical position of the KryptonContextMenu.</param>
    public DateTimePickerDropArgs(KryptonContextMenu kcm,
        KryptonContextMenuPositionH positionH,
        KryptonContextMenuPositionV positionV)
    {
        KryptonContextMenu = kcm;
        PositionH = positionH;
        PositionV = positionV;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets access to the KryptonContextMenu instance.
    /// </summary>
    public KryptonContextMenu? KryptonContextMenu { get; }

    /// <summary>
    /// Gets and sets the relative horizontal position of the KryptonContextMenu.
    /// </summary>
    public KryptonContextMenuPositionH PositionH { get; set; }

    /// <summary>
    /// Gets and sets the relative vertical position of the KryptonContextMenu.
    /// </summary>
    public KryptonContextMenuPositionV PositionV { get; set; }

    #endregion
}
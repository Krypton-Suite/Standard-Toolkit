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
/// Event argument data for a data grid view buttons spec.
/// </summary>
public class DataGridViewButtonSpecClickEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the DataGridViewButtonSpecClickEventArgs class.
    /// </summary>
    /// <param name="column">Reference to data grid view column.</param>
    /// <param name="cell">Reference to data grid view cell.</param>
    /// <param name="buttonSpec">Reference to button spec.</param>
    public DataGridViewButtonSpecClickEventArgs(DataGridViewColumn column,
        DataGridViewCell cell,
        ButtonSpecAny buttonSpec)
    {
        Column = column;
        Cell = cell;
        ButtonSpec = buttonSpec;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets a reference to the column associated with the button spec.
    /// </summary>
    public DataGridViewColumn Column { get; }

    /// <summary>
    /// Gets a reference to the cell that generated the click event.
    /// </summary>
    public DataGridViewCell Cell { get; }

    /// <summary>
    /// Gets a reference to the button spec that is performing the click.
    /// </summary>
    public ButtonSpecAny ButtonSpec { get; }

    #endregion
}
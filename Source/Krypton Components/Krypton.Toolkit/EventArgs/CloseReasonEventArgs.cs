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
/// Details for close reason event handlers.
/// </summary>
public class CloseReasonEventArgs : CancelEventArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the CloseReasonEventArgs class.
    /// </summary>
    /// <param name="closeReason">Reason for the close action occuring.</param>
    public CloseReasonEventArgs(ToolStripDropDownCloseReason closeReason) => CloseReason = closeReason;

    #endregion

    #region Public
    /// <summary>
    /// Gets access to the reason for the context menu closing.
    /// </summary>
    public ToolStripDropDownCloseReason CloseReason { get; }

    #endregion
}
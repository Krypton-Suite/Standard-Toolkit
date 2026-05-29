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

namespace Krypton.Docking;

/// <summary>
/// Event arguments for events that need to provide a unique name but can be cancelled.
/// </summary>
public class CancelUniqueNameEventArgs : UniqueNameEventArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the CancelUniqueNameEventArgs class.
    /// </summary>
    /// <param name="uniqueName">Unique name of page.</param>
    /// <param name="cancel">Initial value for the cancel property.</param>
    public CancelUniqueNameEventArgs([DisallowNull] string uniqueName, bool cancel)
        : base(uniqueName) =>
        Cancel = cancel;

    #endregion

    #region Public
    /// <summary>
    /// Gets and sets a value indicating if the event action should be cancelled.
    /// </summary>
    public bool Cancel { get; set; }

    #endregion
}
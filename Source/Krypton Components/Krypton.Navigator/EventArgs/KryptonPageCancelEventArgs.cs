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
/// Details for page related events that can be cancelled.
/// </summary>
public class KryptonPageCancelEventArgs : KryptonPageEventArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonCancelPageEventArgs class.
    /// </summary>
    /// <param name="page">Page effected by event.</param>
    /// <param name="index">Index of page in the owning collection.</param>
    public KryptonPageCancelEventArgs(KryptonPage? page, int index)
        : base(page, index)
    {
    }
    #endregion

    #region Cancel
    /// <summary>
    /// Gets the page associated with the event.
    /// </summary>
    public bool Cancel { get; set; }

    #endregion
}
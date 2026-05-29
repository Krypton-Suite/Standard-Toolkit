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
/// Details for button specification related events.
/// </summary>
public class ButtonSpecEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the ButtonSpecEventArgs class.
    /// </summary>
    /// <param name="spec">Button spec effected by event.</param>
    /// <param name="index">Index of page in the owning collection.</param>
    public ButtonSpecEventArgs([DisallowNull] ButtonSpec spec, int index)
    {
        Debug.Assert(spec != null);
        Debug.Assert(index >= 0);

        // Remember parameter details
        ButtonSpec = spec ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull(nameof(spec)));
        Index = index;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets the navigator button spec associated with the event.
    /// </summary>
    public ButtonSpec ButtonSpec { get; }

    /// <summary>
    /// Gets the index of ButtonSpec associated with the event.
    /// </summary>
    public int Index { get; }

    #endregion
}
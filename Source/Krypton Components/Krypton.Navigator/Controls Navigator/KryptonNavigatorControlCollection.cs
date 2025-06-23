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

// ReSharper disable RedundantNullableFlowAttribute
namespace Krypton.Navigator;

/// <summary>
/// Represents a collection of child controls for the navigator.
/// </summary>
public class KryptonNavigatorControlCollection : KryptonControlCollection
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonNavigatorControlCollection class.
    /// </summary>
    /// <param name="owner">Control containing this collection.</param>
    public KryptonNavigatorControlCollection([DisallowNull] KryptonNavigator owner)
        : base(owner)
    {
        Debug.Assert(owner != null);
    }
    #endregion

    #region Public Overrides
    /// <summary>
    /// Adds the specified control to the control collection.
    /// </summary>
    /// <param name="value">The KryptonPage to add to the control collection.</param>
    public override void Add(Control? value)
    {
        Debug.Assert(value is not null);

        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        // We only allow KryptonPage controls to be added
        if (value is not KryptonPage)
        {
            throw new ArgumentException(@"Only KryptonPage controls can be added.", nameof(value));
        }

        // Let base class perform actual add
        base.Add(value);
    }
    #endregion
}
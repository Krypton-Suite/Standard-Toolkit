#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Specialise the generic collection with type specific rules for group item accessor.
/// </summary>
public class KryptonRibbonGroupCollection : TypedCollection<KryptonRibbonGroup>
{
    #region Public
    /// <summary>
    /// Gets the item with the provided unique name.
    /// </summary>
    /// <param name="name">Name of the ribbon group instance.</param>
    /// <returns>Item at specified index.</returns>
    public override KryptonRibbonGroup? this[string name]
    {
        get
        {
            // Search for a group with the same text as that requested.
            foreach (KryptonRibbonGroup group in this.Where(group => (group.TextLine1 == name) ||
                                                                     (group.TextLine2 == name) ||
                                                                     ((group.TextLine1 + " " + group.TextLine2) == name)))
            {
                return group;
            }

            // Let base class perform standard processing
            return base[name];
        }
    }
    #endregion
}
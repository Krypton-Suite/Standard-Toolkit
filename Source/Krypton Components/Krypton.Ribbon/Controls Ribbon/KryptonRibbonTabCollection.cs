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
/// Specialise the generic collection with type specific rules for tab item accessor.
/// </summary>
public class KryptonRibbonTabCollection : TypedCollection<KryptonRibbonTab>
{
    #region Public
    /// <summary>
    /// Gets the item with the provided unique name.
    /// </summary>
    /// <param name="name">Name of the ribbon tab instance.</param>
    /// <returns>Item at specified index.</returns>
    public override KryptonRibbonTab? this[string name] 
    { 
        get 
        { 
            foreach (KryptonRibbonTab tab in this.Where(tab => tab.Text == name)) 
            { 
                return tab; 
            } 
            return base[name]; 
        } 
    }
    #endregion
}
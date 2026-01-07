#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Specialise the generic collection with type specific rules for item accessor.
/// </summary>
public class KryptonBackstagePageCollection : TypedCollection<KryptonBackstagePage>
{
    /// <summary>
    /// Gets the item with the provided name.
    /// </summary>
    /// <param name="name">Name of the backstage page instance.</param>
    /// <returns>Item at specified index.</returns>
    public override KryptonBackstagePage? this[string name]
    {
        get
        {
            // First priority is the design time Name
            foreach (KryptonBackstagePage page in this.Where(page => page.Name == name))
            {
                return page;
            }

            // Second priority is the Text of the page
            foreach (KryptonBackstagePage page in this.Where(page => page.Text == name))
            {
                return page;
            }

            // Let base class perform standard processing
            return base[name];
        }
    }
}
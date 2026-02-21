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
public class KryptonBackstageCommandCollection : TypedCollection<KryptonBackstageCommand>
{
    /// <summary>
    /// Gets the item with the provided name.
    /// </summary>
    /// <param name="name">Name of the backstage command instance.</param>
    /// <returns>Item at specified index.</returns>
    public override KryptonBackstageCommand? this[string name]
    {
        get
        {
            // First priority is the Text of the command
            foreach (KryptonBackstageCommand command in this.Where(command => command.Text == name))
            {
                return command;
            }

            // Let base class perform standard processing
            return base[name];
        }
    }
}

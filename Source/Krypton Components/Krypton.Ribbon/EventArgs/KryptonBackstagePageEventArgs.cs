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
/// Specialise the generic collection event args with specific type.
/// </summary>
public class KryptonBackstagePageEventArgs : TypedCollectionEventArgs<KryptonBackstagePage>
{
    /// <summary>
    /// Initialize a new instance of the KryptonBackstagePageEventArgs class.
    /// </summary>
    /// <param name="item">Page affected by event.</param>
    /// <param name="index">Index of page in the owning collection.</param>
    public KryptonBackstagePageEventArgs(KryptonBackstagePage? item, int index)
        : base(item, index)
    {
    }
}

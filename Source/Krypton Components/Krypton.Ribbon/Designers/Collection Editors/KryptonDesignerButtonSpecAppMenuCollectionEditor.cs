#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Krypton-themed designer editor for collections of <see cref="ButtonSpecAppMenu"/>.
/// </summary>
internal sealed class KryptonDesignerButtonSpecAppMenuCollectionEditor : KryptonDesignerButtonSpecAnyCollectionEditor
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonDesignerButtonSpecAppMenuCollectionEditor"/> class.
    /// </summary>
    public KryptonDesignerButtonSpecAppMenuCollectionEditor()
        : base(typeof(ButtonSpecAppMenu))
    {
    }
    #endregion
}

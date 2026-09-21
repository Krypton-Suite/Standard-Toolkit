#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Collection of <see cref="KryptonContextMenuGalleryRange"/> instances.
/// </summary>
public class KryptonContextMenuGalleryRangeCollection : TypedRestrictCollection<KryptonContextMenuGalleryRange>
{
    #region Static Fields

    private static readonly Type[] _types = [typeof(KryptonContextMenuGalleryRange)];

    #endregion

    #region Restrict

    /// <inheritdoc />
    public override Type[] RestrictTypes => _types;

    #endregion
}

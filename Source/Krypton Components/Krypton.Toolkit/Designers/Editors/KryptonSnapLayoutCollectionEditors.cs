#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Collection editor for <see cref="KryptonSnapLayoutCardCollection"/>.
/// </summary>
public sealed class KryptonSnapLayoutCardCollectionEditor : KryptonDesignerStandardCollectionEditor
{
    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonSnapLayoutCardCollectionEditor"/> class.
    /// </summary>
    public KryptonSnapLayoutCardCollectionEditor()
        : base(typeof(KryptonSnapLayoutCard))
    {
    }

    /// <inheritdoc />
    protected override Type[] CreateNewItemTypes() => new[] { DesignerCollectionItemType };

    /// <inheritdoc />
    protected override Type CreateCollectionItemType() => DesignerCollectionItemType;
}

/// <summary>
/// Collection editor for <see cref="KryptonSnapLayoutZoneCollection"/>.
/// </summary>
public sealed class KryptonSnapLayoutZoneCollectionEditor : KryptonDesignerStandardCollectionEditor
{
    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonSnapLayoutZoneCollectionEditor"/> class.
    /// </summary>
    public KryptonSnapLayoutZoneCollectionEditor()
        : base(typeof(KryptonSnapLayoutZone))
    {
    }

    /// <inheritdoc />
    protected override Type[] CreateNewItemTypes() => new[] { DesignerCollectionItemType };

    /// <inheritdoc />
    protected override Type CreateCollectionItemType() => DesignerCollectionItemType;
}

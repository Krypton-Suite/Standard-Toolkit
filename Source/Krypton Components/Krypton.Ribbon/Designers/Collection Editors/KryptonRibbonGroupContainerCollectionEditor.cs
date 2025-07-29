#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2017 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Ribbon;

internal class KryptonRibbonGroupContainerCollectionEditor : CollectionEditor
{
    #region Static Fields
    private static readonly Type[] _supportedTypes = new[]
    {
        typeof(KryptonRibbonGroupLines),
        typeof(KryptonRibbonGroupTriple),
        typeof(KryptonRibbonGroupSeparator)
    };
    #endregion

    /// <summary>
    /// Initialize a new instance of the KryptonRibbonGroupTopCollectionEditor class.
    /// </summary>
    public KryptonRibbonGroupContainerCollectionEditor()
        : base(typeof(KryptonRibbonGroupContainerCollection))
    {
#if NET5_0_OR_GREATER && !NET8_0_OR_GREATER
        // .NET 5–7 designer relies on NewItemTypes; property has a setter in those versions.
        NewItemTypes = _supportedTypes;
#else
        // .NET Framework and .NET 8+ fall back to CreateNewItemTypes().
#endif
    }

    /// <summary>
    /// Gets the data types that this collection editor can contain.
    /// </summary>
    /// <returns>An array of data types that this collection can contain.</returns>
    // Older designers still invoke this method; simply return the shared list.
    protected override Type[] CreateNewItemTypes() => _supportedTypes;
}
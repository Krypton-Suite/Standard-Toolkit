#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Ribbon;

internal class KryptonRibbonGroupContainerCollectionEditor : CollectionEditor
{
    /// <summary>
    /// Initialize a new instance of the KryptonRibbonGroupTopCollectionEditor class.
    /// </summary>
    public KryptonRibbonGroupContainerCollectionEditor()
        : base(typeof(KryptonRibbonGroupContainerCollection))
    {
    }

    /// <summary>
    /// Gets the data types that this collection editor can contain. 
    /// </summary>
    /// <returns>An array of data types that this collection can contain.</returns>
    protected override Type[] CreateNewItemTypes() =>
        // Bug https://github.com/Krypton-Suite/Standard-Toolkit/issues/66
        // For some reason in .Net5 onwards, the following function is not called
        [
            typeof(KryptonRibbonGroupLines),
            typeof(KryptonRibbonGroupTriple),
            typeof(KryptonRibbonGroupSeparator)
        ];
}
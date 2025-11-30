#region BSD License
/*
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), Giduac, tobitege, Lesandro & KamaniAR et al. 2025 - 2025. All rights reserved.
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Collection editor for the backstage pages collection.
/// </summary>
internal class KryptonRibbonBackstagePageCollectionEditor : CollectionEditor
{
    /// <summary>
    /// Initialize a new instance of the KryptonRibbonBackstagePageCollectionEditor class.
    /// </summary>
    public KryptonRibbonBackstagePageCollectionEditor()
        : base(typeof(KryptonRibbonBackstagePageCollection))
    {
    }

    /// <summary>
    /// Gets the data types that this collection editor can contain. 
    /// </summary>
    /// <returns>An array of data types that this collection can contain.</returns>
    protected override Type[] CreateNewItemTypes() => [typeof(KryptonRibbonBackstagePage)];

    /// <summary>
    /// Gets the help keyword to display for the Help button in the dialog.
    /// </summary>
    protected override string HelpTopic => @"KryptonRibbonBackstagePageCollection Editor";
}

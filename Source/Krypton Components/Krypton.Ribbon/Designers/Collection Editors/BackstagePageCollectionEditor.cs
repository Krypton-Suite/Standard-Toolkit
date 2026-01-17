#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Ribbon;

internal class BackstagePageCollectionEditor : CollectionEditor
{
    /// <summary>
    /// Initialize a new instance of the BackstagePageCollectionEditor class.
    /// </summary>
    public BackstagePageCollectionEditor()
        : base(typeof(KryptonBackstagePageCollection))
    {
    }

    /// <summary>
    /// Gets the data types that this collection editor can contain.
    /// </summary>
    /// <returns>An array of data types that this collection can contain.</returns>
    protected override Type[] CreateNewItemTypes() => new[] { typeof(KryptonBackstagePage) };

    /// <summary>
    /// Sets the specified array as the items of the collection.
    /// </summary>
    /// <param name="editValue">The collection to edit.</param>
    /// <param name="value">An array of objects to set as the collection items.</param>
    /// <returns>The newly created collection object.</returns>
    protected override object? SetItems(object? editValue, object[]? value)
    {
        // Cast the context into the expected control type
        var backstage = Context!.Instance as KryptonBackstageView;

        // Suspend changes until collection has been updated
        backstage?.SuspendLayout();

        // Let base class update the collection
        var ret = base.SetItems(editValue, value);

        backstage?.ResumeLayout(true);

        return ret;
    }
}

#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Navigator;

internal class NavigatorPageCollectionEditor : CollectionEditor
{
    /// <summary>
    /// Initialize a new instance of the NavigatorPageCollectionEditor class.
    /// </summary>
    public NavigatorPageCollectionEditor()
        : base(typeof(KryptonPageCollection))
    {
    }

    /// <summary>
    /// Gets the data types that this collection editor can contain. 
    /// </summary>
    /// <returns>An array of data types that this collection can contain.</returns>
    protected override Type[] CreateNewItemTypes() => new[] { typeof(KryptonPage) };

    /// <summary>
    /// Sets the specified array as the items of the collection.
    /// </summary>
    /// <param name="editValue">The collection to edit.</param>
    /// <param name="value">An array of objects to set as the collection items.</param>
    /// <returns>The newly created collection object.</returns>
    protected override object? SetItems(object? editValue, object[]? value)
    {
        // Cast the context into the expected control type
        var navigator = Context!.Instance as KryptonNavigator;

        // Suspend changes until collection has been updated
        navigator?.SuspendLayout();

        // Let base class update the collection
        var ret = base.SetItems(editValue, value);

        navigator?.ResumeLayout(true);

        return ret;
    }
}
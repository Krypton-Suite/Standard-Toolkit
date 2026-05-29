#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

namespace Krypton.Ribbon;

internal class KryptonRibbonQATButtonCollectionEditor : CollectionEditor
{
    /// <summary>
    /// Initialize a new instance of the KryptonRibbonQATButtonCollectionEditor class.
    /// </summary>
    public KryptonRibbonQATButtonCollectionEditor()
        : base(typeof(KryptonRibbonQATButtonCollection))
    {
    }

    /// <summary>
    /// Gets the data types that this collection editor can contain. 
    /// </summary>
    /// <returns>An array of data types that this collection can contain.</returns>
    protected override Type[] CreateNewItemTypes() => [typeof(KryptonRibbonQATButton)];

    /// <summary>
    /// Sets the specified array as the items of the collection.
    /// </summary>
    /// <param name="editValue">The collection to edit.</param>
    /// <param name="value">An array of objects to set as the collection items.</param>
    /// <returns>The newly created collection object.</returns>
    protected override object? SetItems(object? editValue, object[]? value)
    {
        // Cast the context into the expected control type
        var ribbon = Context?.Instance as KryptonRibbon ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("ribbon"));

        // Suspend changes until collection has been updated
        ribbon.SuspendLayout();

        // Let base class update the collection
        var ret = base.SetItems(editValue, value);

        ribbon.ResumeLayout(true);

        return ret;
    }
}
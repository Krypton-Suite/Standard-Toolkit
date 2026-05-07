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

namespace Krypton.Toolkit;

/// <summary>
/// Designer for a collection of context menu items.
/// </summary>
public partial class KryptonContextMenuCollectionEditor : CollectionEditor
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonContextMenuCollectionEditor class.
    /// </summary>
    public KryptonContextMenuCollectionEditor()
        : base(typeof(KryptonContextMenuCollection))
    {
    }
    #endregion

    #region Protected Overrides
    /// <summary>
    /// Creates a new form to display and edit the current collection.
    /// </summary>
    /// <returns>A CollectionForm to provide as the user interface for editing the collection.</returns>
    protected override CollectionForm CreateCollectionForm() => new KryptonContextMenuCollectionForm(this);

    /// <summary>
    /// Gets the data types that this collection editor can contain. 
    /// </summary>
    /// <returns>An array of data types that this collection can contain.</returns>
    protected override Type[] CreateNewItemTypes() =>
    [
        typeof(KryptonContextMenuItems),
        typeof(KryptonContextMenuItem),
        typeof(KryptonContextMenuSeparator),
        typeof(KryptonContextMenuHeading),
        typeof(KryptonContextMenuLinkLabel),
        typeof(KryptonContextMenuCheckBox),
        typeof(KryptonContextMenuCheckButton),
        typeof(KryptonContextMenuRadioButton),
        typeof(KryptonContextMenuColorColumns),
        typeof(KryptonContextMenuMonthCalendar),
        typeof(KryptonContextMenuImageSelect)
    ];
    #endregion
}
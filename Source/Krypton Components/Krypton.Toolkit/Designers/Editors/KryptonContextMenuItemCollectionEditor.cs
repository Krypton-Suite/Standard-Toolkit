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
/// CollectionEditor used for a KryptonContextMenuItemCollection instance.
/// </summary>
public class KryptonContextMenuItemCollectionEditor : CollectionEditor
{
    /// <summary>
    /// Initialize a new instance of the KryptonContextMenuItemCollectionEditor class.
    /// </summary>
    public KryptonContextMenuItemCollectionEditor()
        : base(typeof(KryptonContextMenuItemCollection))
    {
    }

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
}
#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, tobitege, Lesandro & KamaniAR et al. 2025 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Action list for KryptonListBox using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonListBoxExtensibilityActionList : KryptonExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonListBox _listBox;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonListBoxExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonListBoxExtensibilityActionList(KryptonListBoxExtensibilityDesigner owner)
        : base(owner)
    {
        // Remember the listbox instance
        _listBox = (owner.Component as KryptonListBox)!;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the listbox text.
    /// </summary>
    public string Text
    {
        get => _listBox.Text;
        set => SetPropertyValue(_listBox, nameof(Text), _listBox.Text, value, v => _listBox.Text = v);
    }

    /// <summary>
    /// Gets and sets the palette mode.
    /// </summary>
    public PaletteMode PaletteMode
    {
        get => _listBox.PaletteMode;
        set => SetPropertyValue(_listBox, nameof(PaletteMode), _listBox.PaletteMode, value, v => _listBox.PaletteMode = v);
    }

    /// <summary>
    /// Gets and sets the selection mode.
    /// </summary>
    public SelectionMode SelectionMode
    {
        get => _listBox.SelectionMode;
        set => SetPropertyValue(_listBox, nameof(SelectionMode), _listBox.SelectionMode, value, v => _listBox.SelectionMode = v);
    }

    /// <summary>
    /// Gets and sets whether the listbox is sorted.
    /// </summary>
    public bool Sorted
    {
        get => _listBox.Sorted;
        set => SetPropertyValue(_listBox, nameof(Sorted), _listBox.Sorted, value, v => _listBox.Sorted = v);
    }

    /// <summary>
    /// Gets and sets whether scroll is always visible.
    /// </summary>
    public bool ScrollAlwaysVisible
    {
        get => _listBox.ScrollAlwaysVisible;
        set => SetPropertyValue(_listBox, nameof(ScrollAlwaysVisible), _listBox.ScrollAlwaysVisible, value, v => _listBox.ScrollAlwaysVisible = v);
    }
    #endregion

    #region Public Override
    /// <summary>
    /// Returns the collection of DesignerActionItem objects contained in the list.
    /// </summary>
    /// <returns>A DesignerActionItem array that contains the items in this list.</returns>
    public override DesignerActionItemCollection GetSortedActionItems()
    {
        // Create a new collection for holding the single item we want to create
        var actions = new DesignerActionItemCollection();

        // This can be null when deleting a control instance at design time
        if (_listBox != null)
        {
            // Add the list of listbox specific actions
            actions.Add(new DesignerActionHeaderItem(nameof(Appearance)));
            actions.Add(new DesignerActionPropertyItem(nameof(Text), @"Text", nameof(Appearance), @"ListBox text"));
            actions.Add(new DesignerActionHeaderItem(@"Behavior"));
            actions.Add(new DesignerActionPropertyItem(nameof(SelectionMode), nameof(SelectionMode), @"Behavior", @"Selection mode"));
            actions.Add(new DesignerActionPropertyItem(nameof(Sorted), nameof(Sorted), @"Behavior", @"Sort items alphabetically"));
            actions.Add(new DesignerActionPropertyItem(nameof(ScrollAlwaysVisible), nameof(ScrollAlwaysVisible), @"Behavior", @"Always show scroll bar"));
            actions.Add(new DesignerActionHeaderItem(@"Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), @"Palette", @"Visuals", @"Palette applied to drawing"));
        }

        return actions;
    }
    #endregion
}

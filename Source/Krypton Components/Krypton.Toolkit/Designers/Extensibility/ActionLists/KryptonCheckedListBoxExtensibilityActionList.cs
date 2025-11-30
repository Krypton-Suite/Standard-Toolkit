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
/// Action list for the KryptonCheckedListBox control using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonCheckedListBoxExtensibilityActionList : KryptonExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonCheckedListBox? _checkedListBox;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonCheckedListBoxExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonCheckedListBoxExtensibilityActionList(ControlDesigner owner)
        : base(owner)
    {
        _checkedListBox = (KryptonCheckedListBox?)owner.Component;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets and sets the palette to be applied.
    /// </summary>
    public PaletteMode PaletteMode
    {
        get => _checkedListBox?.PaletteMode ?? PaletteMode.Global;
        set => SetPropertyValue(_checkedListBox!, nameof(PaletteMode), PaletteMode, value, v => _checkedListBox!.PaletteMode = v);
    }

    /// <summary>
    /// Gets and sets whether checkboxes are shown.
    /// </summary>
    public bool CheckOnClick
    {
        get => _checkedListBox?.CheckOnClick ?? false;
        set => SetPropertyValue(_checkedListBox!, nameof(CheckOnClick), CheckOnClick, value, v => _checkedListBox!.CheckOnClick = v);
    }

    /// <summary>
    /// Gets and sets whether the list is sorted.
    /// </summary>
    public bool Sorted
    {
        get => _checkedListBox?.Sorted ?? false;
        set => SetPropertyValue(_checkedListBox!, nameof(Sorted), Sorted, value, v => _checkedListBox!.Sorted = v);
    }

    /// <summary>
    /// Gets and sets whether selection is enabled.
    /// </summary>
    public CheckedSelectionMode SelectionMode
    {
        get => _checkedListBox?.SelectionMode ?? CheckedSelectionMode.One;
        set => SetPropertyValue(_checkedListBox!, nameof(SelectionMode), SelectionMode, value, v => _checkedListBox!.SelectionMode = v);
    }
    #endregion

    #region Public Override
    /// <summary>
    /// Returns the collection of DesignerActionItem objects contained in the list.
    /// </summary>
    /// <returns>A DesignerActionItem array that contains the items in this list.</returns>
    public override DesignerActionItemCollection GetSortedActionItems()
    {
        var actions = new DesignerActionItemCollection();

        // This can be null when deleting a control instance at design time
        if (_checkedListBox != null)
        {
            // Add the list of CheckedListBox specific actions
            actions.Add(new DesignerActionHeaderItem(@"Appearance"));
            actions.Add(new DesignerActionPropertyItem(nameof(CheckOnClick), @"Check On Click", @"Appearance", @"Check on click"));
            actions.Add(new DesignerActionPropertyItem(nameof(Sorted), @"Sorted", @"Appearance", @"Sort items"));
            actions.Add(new DesignerActionPropertyItem(nameof(SelectionMode), @"Selection Mode", @"Appearance", @"Selection mode"));
            actions.Add(new DesignerActionHeaderItem(@"Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), @"Palette", @"Visuals", @"Palette applied to drawing"));
        }

        return actions;
    }
    #endregion
}

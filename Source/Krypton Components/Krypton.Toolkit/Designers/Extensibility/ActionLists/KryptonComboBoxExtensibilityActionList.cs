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
/// Action list for KryptonComboBox using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonComboBoxExtensibilityActionList : KryptonExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonComboBox _comboBox;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonComboBoxExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonComboBoxExtensibilityActionList(KryptonComboBoxExtensibilityDesigner owner)
        : base(owner)
    {
        // Remember the combobox instance
        _comboBox = (owner.Component as KryptonComboBox)!;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the combobox style.
    /// </summary>
    public ComboBoxStyle DropDownStyle
    {
        get => _comboBox.DropDownStyle;
        set => SetPropertyValue(_comboBox, nameof(DropDownStyle), _comboBox.DropDownStyle, value, v => _comboBox.DropDownStyle = v);
    }

    /// <summary>
    /// Gets and sets the combobox text.
    /// </summary>
    public string Text
    {
        get => _comboBox.Text;
        set => SetPropertyValue(_comboBox, nameof(Text), _comboBox.Text, value, v => _comboBox.Text = v);
    }

    /// <summary>
    /// Gets and sets the palette mode.
    /// </summary>
    public PaletteMode PaletteMode
    {
        get => _comboBox.PaletteMode;
        set => SetPropertyValue(_comboBox, nameof(PaletteMode), _comboBox.PaletteMode, value, v => _comboBox.PaletteMode = v);
    }

    /// <summary>
    /// Gets and sets whether the combobox is sorted.
    /// </summary>
    public bool Sorted
    {
        get => _comboBox.Sorted;
        set => SetPropertyValue(_comboBox, nameof(Sorted), _comboBox.Sorted, value, v => _comboBox.Sorted = v);
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
        if (_comboBox != null)
        {
            // Add the list of combobox specific actions
            actions.Add(new DesignerActionHeaderItem(nameof(Appearance)));
            actions.Add(new DesignerActionPropertyItem(nameof(DropDownStyle), @"Drop Down Style", nameof(Appearance), @"ComboBox drop down style"));
            actions.Add(new DesignerActionHeaderItem(@"Behavior"));
            actions.Add(new DesignerActionPropertyItem(nameof(Text), nameof(Text), @"Behavior", @"ComboBox text"));
            actions.Add(new DesignerActionPropertyItem(nameof(Sorted), nameof(Sorted), @"Behavior", @"Sort items alphabetically"));
            actions.Add(new DesignerActionHeaderItem(@"Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), @"Palette", @"Visuals", @"Palette applied to drawing"));
        }

        return actions;
    }
    #endregion
}

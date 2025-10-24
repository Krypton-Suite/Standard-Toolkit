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

internal class KryptonComboBoxActionList : DesignerActionList
{
    #region Instance Fields
    private readonly KryptonComboBox _comboBox;
    private readonly IComponentChangeService? _service;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonComboBoxActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonComboBoxActionList(KryptonComboBoxDesigner owner)
        : base(owner.Component)
    {
        // Remember the combo box instance
        _comboBox = (owner.Component as KryptonComboBox)!;

        // Cache service used to notify when a property has changed
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }
    #endregion

    #region Public
    /// <summary>Gets or sets the Krypton Context Menu.</summary>
    /// <value>The Krypton Context Menu.</value>
    public KryptonContextMenu? KryptonContextMenu
    {
        get => _comboBox.KryptonContextMenu;

        set
        {
            if (_comboBox.KryptonContextMenu != value)
            {
                _service?.OnComponentChanged(_comboBox, null, _comboBox.KryptonContextMenu, value);

                _comboBox.KryptonContextMenu = value;
            }
        }
    }

    /// <summary>Gets or sets the drop-down style.</summary>
    /// <value>The drop-down style.</value>
    public ComboBoxStyle DropDownStyle
    {
        get => _comboBox.DropDownStyle;

        set
        {
            if (_comboBox.DropDownStyle != value)
            {
                _service?.OnComponentChanged(_comboBox, null, _comboBox.DropDownStyle, value);

                _comboBox.DropDownStyle = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the palette mode.
    /// </summary>
    public PaletteMode PaletteMode
    {
        get => _comboBox.PaletteMode;

        set
        {
            if (_comboBox.PaletteMode != value)
            {
                _service?.OnComponentChanged(_comboBox, null, _comboBox.PaletteMode, value);
                _comboBox.PaletteMode = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the input control style.
    /// </summary>
    public InputControlStyle InputControlStyle
    {
        get => _comboBox.InputControlStyle;

        set
        {
            if (_comboBox.InputControlStyle != value)
            {
                _service?.OnComponentChanged(_comboBox, null, _comboBox.InputControlStyle, value);
                _comboBox.InputControlStyle = value;
            }
        }
    }

    /// <summary>Gets or sets the font.</summary>
    /// <value>The font.</value>
    public Font Font
    {
        get => _comboBox.StateCommon.ComboBox.Content.Font!;

        set
        {
            if (!Equals(_comboBox.StateCommon.ComboBox.Content.Font, value))
            {
                _service?.OnComponentChanged(_comboBox, null, _comboBox.StateCommon.ComboBox.Content.Font, value);

                _comboBox.StateCommon.ComboBox.Content.Font = value;
            }
        }
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
            // Add the list of label specific actions
            actions.Add(new DesignerActionHeaderItem(nameof(Appearance)));
            actions.Add(new DesignerActionPropertyItem(nameof(KryptonContextMenu), @"Krypton Context Menu", nameof(Appearance), @"The Krypton Context Menu for the control."));
            actions.Add(new DesignerActionPropertyItem(nameof(DropDownStyle), @"Drop Down Style", nameof(Appearance), @"The combobox drop-down style."));
            actions.Add(new DesignerActionPropertyItem(nameof(InputControlStyle), @"Style", nameof(Appearance), @"ComboBox display style."));
            actions.Add(new DesignerActionPropertyItem(nameof(Font), nameof(Font), nameof(Appearance), @"The font for the combobox."));
            actions.Add(new DesignerActionHeaderItem(@"Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), @"Palette", @"Visuals", @"Palette applied to drawing"));
        }

        return actions;
    }
    #endregion
}
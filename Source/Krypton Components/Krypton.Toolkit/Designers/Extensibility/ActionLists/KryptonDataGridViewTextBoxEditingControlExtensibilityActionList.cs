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
/// Action list for the KryptonDataGridViewTextBoxEditingControl control using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonDataGridViewTextBoxEditingControlExtensibilityActionList : KryptonExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonDataGridViewTextBoxEditingControl? _editingControl;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonDataGridViewTextBoxEditingControlExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonDataGridViewTextBoxEditingControlExtensibilityActionList(ControlDesigner owner)
        : base(owner)
    {
        _editingControl = (KryptonDataGridViewTextBoxEditingControl?)owner.Component;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets and sets the palette to be applied.
    /// </summary>
    public PaletteMode PaletteMode
    {
        get => _editingControl?.PaletteMode ?? PaletteMode.Global;
        set => SetPropertyValue(_editingControl!, nameof(PaletteMode), PaletteMode, value, v => _editingControl!.PaletteMode = v);
    }

    /// <summary>
    /// Gets and sets the input control style.
    /// </summary>
    public InputControlStyle InputControlStyle
    {
        get => _editingControl?.InputControlStyle ?? InputControlStyle.Standalone;
        set => SetPropertyValue(_editingControl!, nameof(InputControlStyle), InputControlStyle, value, v => _editingControl!.InputControlStyle = v);
    }

    /// <summary>
    /// Gets and sets the text associated with the control.
    /// </summary>
    public string Text
    {
        get => _editingControl?.Text ?? string.Empty;
        set => SetPropertyValue(_editingControl!, nameof(Text), Text, value, v => _editingControl!.Text = v);
    }

    /// <summary>
    /// Gets and sets the maximum length of text.
    /// </summary>
    public int MaxLength
    {
        get => _editingControl?.MaxLength ?? 0;
        set => SetPropertyValue(_editingControl!, nameof(MaxLength), MaxLength, value, v => _editingControl!.MaxLength = v);
    }

    /// <summary>
    /// Gets and sets whether the control is multiline.
    /// </summary>
    public bool Multiline
    {
        get => _editingControl?.Multiline ?? false;
        set => SetPropertyValue(_editingControl!, nameof(Multiline), Multiline, value, v => _editingControl!.Multiline = v);
    }

    /// <summary>
    /// Gets and sets whether the control uses system password character.
    /// </summary>
    public bool UseSystemPasswordChar
    {
        get => _editingControl?.UseSystemPasswordChar ?? false;
        set => SetPropertyValue(_editingControl!, nameof(UseSystemPasswordChar), UseSystemPasswordChar, value, v => _editingControl!.UseSystemPasswordChar = v);
    }

    /// <summary>
    /// Gets and sets whether the control is enabled.
    /// </summary>
    public bool Enabled
    {
        get => _editingControl?.Enabled ?? true;
        set => SetPropertyValue(_editingControl!, nameof(Enabled), Enabled, value, v => _editingControl!.Enabled = v);
    }

    /// <summary>
    /// Gets and sets whether the control is visible.
    /// </summary>
    public bool Visible
    {
        get => _editingControl?.Visible ?? true;
        set => SetPropertyValue(_editingControl!, nameof(Visible), Visible, value, v => _editingControl!.Visible = v);
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
        if (_editingControl != null)
        {
            // Add the list of DataGridViewTextBoxEditingControl specific actions
            actions.Add(new DesignerActionHeaderItem(@"Appearance"));
            actions.Add(new DesignerActionPropertyItem(nameof(InputControlStyle), @"Style", @"Appearance", @"Input control style"));
            actions.Add(new DesignerActionPropertyItem(nameof(Text), @"Text", @"Appearance", @"Text content"));
            actions.Add(new DesignerActionPropertyItem(nameof(MaxLength), @"Max Length", @"Appearance", @"Maximum text length"));
            actions.Add(new DesignerActionPropertyItem(nameof(Multiline), @"Multiline", @"Appearance", @"Multiline text"));
            actions.Add(new DesignerActionPropertyItem(nameof(UseSystemPasswordChar), @"Use System Password Char", @"Appearance", @"Use system password character"));
            actions.Add(new DesignerActionHeaderItem(@"Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), @"Palette", @"Visuals", @"Palette applied to drawing"));
            actions.Add(new DesignerActionHeaderItem(@"Behavior"));
            actions.Add(new DesignerActionPropertyItem(nameof(Enabled), @"Enabled", @"Behavior", @"Control enabled"));
            actions.Add(new DesignerActionPropertyItem(nameof(Visible), @"Visible", @"Behavior", @"Control visible"));
        }

        return actions;
    }
    #endregion
}

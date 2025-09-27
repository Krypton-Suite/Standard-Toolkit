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
/// Action list for the KryptonDateTimePicker control using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonDateTimePickerExtensibilityActionList : KryptonExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonDateTimePicker? _dateTimePicker;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonDateTimePickerExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonDateTimePickerExtensibilityActionList(ControlDesigner owner)
        : base(owner)
    {
        _dateTimePicker = (KryptonDateTimePicker?)owner.Component;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets and sets the input control style.
    /// </summary>
    public InputControlStyle InputControlStyle
    {
        get => _dateTimePicker?.InputControlStyle ?? InputControlStyle.Standalone;
        set => SetPropertyValue(_dateTimePicker!, nameof(InputControlStyle), InputControlStyle, value, v => _dateTimePicker!.InputControlStyle = v);
    }

    /// <summary>
    /// Gets and sets the format of date and time displayed.
    /// </summary>
    public DateTimePickerFormat Format
    {
        get => _dateTimePicker?.Format ?? DateTimePickerFormat.Long;
        set => SetPropertyValue(_dateTimePicker!, nameof(Format), Format, value, v => _dateTimePicker!.Format = v);
    }

    /// <summary>
    /// Gets and sets the custom format string used to display the date and time.
    /// </summary>
    public string CustomFormat
    {
        get => _dateTimePicker?.CustomFormat ?? string.Empty;
        set => SetPropertyValue(_dateTimePicker!, nameof(CustomFormat), CustomFormat, value, v => _dateTimePicker!.CustomFormat = v);
    }

    /// <summary>
    /// Gets and sets the palette to be applied.
    /// </summary>
    public PaletteMode PaletteMode
    {
        get => _dateTimePicker?.PaletteMode ?? PaletteMode.Global;
        set => SetPropertyValue(_dateTimePicker!, nameof(PaletteMode), PaletteMode, value, v => _dateTimePicker!.PaletteMode = v);
    }

    /// <summary>
    /// Gets and sets if the control can be focused.
    /// </summary>
    public bool AllowButtonSpecToolTips
    {
        get => _dateTimePicker?.AllowButtonSpecToolTips ?? false;
        set => SetPropertyValue(_dateTimePicker!, nameof(AllowButtonSpecToolTips), AllowButtonSpecToolTips, value, v => _dateTimePicker!.AllowButtonSpecToolTips = v);
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
        if (_dateTimePicker != null)
        {
            // Add the list of DateTimePicker specific actions
            actions.Add(new DesignerActionHeaderItem(@"Appearance"));
            actions.Add(new DesignerActionPropertyItem(nameof(InputControlStyle), @"Style", @"Appearance", @"Input control style"));
            actions.Add(new DesignerActionPropertyItem(nameof(Format), @"Format", @"Appearance", @"Date and time format"));
            actions.Add(new DesignerActionPropertyItem(nameof(CustomFormat), @"Custom Format", @"Appearance", @"Custom format string"));
            actions.Add(new DesignerActionHeaderItem(@"Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), @"Palette", @"Visuals", @"Palette applied to drawing"));
            actions.Add(new DesignerActionHeaderItem(@"Behavior"));
            actions.Add(new DesignerActionPropertyItem(nameof(AllowButtonSpecToolTips), @"Allow Button Spec ToolTips", @"Behavior", @"Allow button spec tooltips"));
        }

        return actions;
    }
    #endregion
}

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
/// Action list for the KryptonDomainUpDown control using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonDomainUpDownExtensibilityActionList : KryptonExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonDomainUpDown? _domainUpDown;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonDomainUpDownExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonDomainUpDownExtensibilityActionList(ControlDesigner owner)
        : base(owner)
    {
        _domainUpDown = (KryptonDomainUpDown?)owner.Component;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets and sets the input control style.
    /// </summary>
    public InputControlStyle InputControlStyle
    {
        get => _domainUpDown?.InputControlStyle ?? InputControlStyle.Standalone;
        set => SetPropertyValue(_domainUpDown!, nameof(InputControlStyle), InputControlStyle, value, v => _domainUpDown!.InputControlStyle = v);
    }

    /// <summary>
    /// Gets and sets the text associated with the control.
    /// </summary>
    public string Text
    {
        get => _domainUpDown?.Text ?? string.Empty;
        set => SetPropertyValue(_domainUpDown!, nameof(Text), Text, value, v => _domainUpDown!.Text = v);
    }

    /// <summary>
    /// Gets and sets the palette to be applied.
    /// </summary>
    public PaletteMode PaletteMode
    {
        get => _domainUpDown?.PaletteMode ?? PaletteMode.Global;
        set => SetPropertyValue(_domainUpDown!, nameof(PaletteMode), PaletteMode, value, v => _domainUpDown!.PaletteMode = v);
    }

    /// <summary>
    /// Gets and sets if the control can be focused.
    /// </summary>
    public bool AllowButtonSpecToolTips
    {
        get => _domainUpDown?.AllowButtonSpecToolTips ?? false;
        set => SetPropertyValue(_domainUpDown!, nameof(AllowButtonSpecToolTips), AllowButtonSpecToolTips, value, v => _domainUpDown!.AllowButtonSpecToolTips = v);
    }

    /// <summary>
    /// Gets and sets a value indicating whether the collection of strings is sorted.
    /// </summary>
    public bool Sorted
    {
        get => _domainUpDown?.Sorted ?? false;
        set => SetPropertyValue(_domainUpDown!, nameof(Sorted), Sorted, value, v => _domainUpDown!.Sorted = v);
    }

    /// <summary>
    /// Gets and sets how the text should be aligned for edit controls.
    /// </summary>
    public HorizontalAlignment TextAlign
    {
        get => _domainUpDown?.TextAlign ?? HorizontalAlignment.Left;
        set => SetPropertyValue(_domainUpDown!, nameof(TextAlign), TextAlign, value, v => _domainUpDown!.TextAlign = v);
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
        if (_domainUpDown != null)
        {
            // Add the list of DomainUpDown specific actions
            actions.Add(new DesignerActionHeaderItem(@"Appearance"));
            actions.Add(new DesignerActionPropertyItem(nameof(InputControlStyle), @"Style", @"Appearance", @"Input control style"));
            actions.Add(new DesignerActionPropertyItem(nameof(Text), @"Text", @"Appearance", @"Text content"));
            actions.Add(new DesignerActionPropertyItem(nameof(TextAlign), @"Text Align", @"Appearance", @"Text alignment"));
            actions.Add(new DesignerActionHeaderItem(@"Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), @"Palette", @"Visuals", @"Palette applied to drawing"));
            actions.Add(new DesignerActionHeaderItem(@"Behavior"));
            actions.Add(new DesignerActionPropertyItem(nameof(Sorted), @"Sorted", @"Behavior", @"Sort collection of strings"));
            actions.Add(new DesignerActionPropertyItem(nameof(AllowButtonSpecToolTips), @"Allow Button Spec ToolTips", @"Behavior", @"Allow button spec tooltips"));
        }

        return actions;
    }
    #endregion
}

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
/// Action list for the KryptonWrapLabel control using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonWrapLabelExtensibilityActionList : KryptonExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonWrapLabel? _wrapLabel;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonWrapLabelExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonWrapLabelExtensibilityActionList(ControlDesigner owner)
        : base(owner)
    {
        _wrapLabel = (KryptonWrapLabel?)owner.Component;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets and sets the label style.
    /// </summary>
    public LabelStyle LabelStyle
    {
        get => _wrapLabel?.LabelStyle ?? LabelStyle.NormalControl;
        set => SetPropertyValue(_wrapLabel!, nameof(LabelStyle), LabelStyle, value, v => _wrapLabel!.LabelStyle = v);
    }

    /// <summary>
    /// Gets and sets the text associated with the control.
    /// </summary>
    public string Text
    {
        get => _wrapLabel?.Text ?? string.Empty;
        set => SetPropertyValue(_wrapLabel!, nameof(Text), Text, value, v => _wrapLabel!.Text = v);
    }

    /// <summary>
    /// Gets and sets the image to display.
    /// </summary>
    public Image? Image
    {
        get => _wrapLabel?.Image;
        set => SetPropertyValue(_wrapLabel!, nameof(Image), Image, value, v => _wrapLabel!.Image = v);
    }

    /// <summary>
    /// Gets and sets the palette to be applied.
    /// </summary>
    public PaletteMode PaletteMode
    {
        get => _wrapLabel?.PaletteMode ?? PaletteMode.Global;
        set => SetPropertyValue(_wrapLabel!, nameof(PaletteMode), PaletteMode, value, v => _wrapLabel!.PaletteMode = v);
    }

    /// <summary>
    /// Gets and sets the target control.
    /// </summary>
    public Control? Target
    {
        get => _wrapLabel?.Target;
        set => SetPropertyValue(_wrapLabel!, nameof(Target), Target, value, v => _wrapLabel!.Target = v);
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
        if (_wrapLabel != null)
        {
            // Add the list of WrapLabel specific actions
            actions.Add(new DesignerActionHeaderItem(@"Appearance"));
            actions.Add(new DesignerActionPropertyItem(nameof(LabelStyle), @"Style", @"Appearance", @"Label style"));
            actions.Add(new DesignerActionPropertyItem(nameof(Text), @"Text", @"Appearance", @"Text content"));
            actions.Add(new DesignerActionPropertyItem(nameof(Image), @"Image", @"Appearance", @"Image content"));
            actions.Add(new DesignerActionPropertyItem(nameof(Target), @"Target", @"Appearance", @"Target control"));
            actions.Add(new DesignerActionHeaderItem(@"Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), @"Palette", @"Visuals", @"Palette applied to drawing"));
        }

        return actions;
    }
    #endregion
}

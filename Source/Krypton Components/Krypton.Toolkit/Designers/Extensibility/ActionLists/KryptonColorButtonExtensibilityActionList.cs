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
/// Action list for the KryptonColorButton control using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonColorButtonExtensibilityActionList : KryptonExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonColorButton? _colorButton;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonColorButtonExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonColorButtonExtensibilityActionList(ControlDesigner owner)
        : base(owner)
    {
        _colorButton = (KryptonColorButton?)owner.Component;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets and sets the button style.
    /// </summary>
    public ButtonStyle ButtonStyle
    {
        get => _colorButton?.ButtonStyle ?? ButtonStyle.Standalone;
        set => SetPropertyValue(_colorButton!, nameof(ButtonStyle), ButtonStyle, value, v => _colorButton!.ButtonStyle = v);
    }

    /// <summary>
    /// Gets and sets the text associated with the control.
    /// </summary>
    public string Text
    {
        get => _colorButton?.Text ?? string.Empty;
        set => SetPropertyValue(_colorButton!, nameof(Text), Text, value, v => _colorButton!.Text = v);
    }

    /// <summary>
    /// Gets and sets the selected color.
    /// </summary>
    public Color SelectedColor
    {
        get => _colorButton?.SelectedColor ?? Color.Empty;
        set => SetPropertyValue(_colorButton!, nameof(SelectedColor), SelectedColor, value, v => _colorButton!.SelectedColor = v);
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
        if (_colorButton != null)
        {
            // Add the list of ColorButton specific actions
            actions.Add(new DesignerActionHeaderItem(@"Appearance"));
            actions.Add(new DesignerActionPropertyItem(nameof(ButtonStyle), @"Style", @"Appearance", @"Button style"));
            actions.Add(new DesignerActionPropertyItem(nameof(Text), @"Text", @"Appearance", @"Text content"));
            actions.Add(new DesignerActionPropertyItem(nameof(SelectedColor), @"Selected Color", @"Appearance", @"Selected color"));
        }

        return actions;
    }
    #endregion
}

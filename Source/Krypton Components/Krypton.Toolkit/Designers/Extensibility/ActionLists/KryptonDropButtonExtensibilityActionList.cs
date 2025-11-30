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
/// Action list for the KryptonDropButton control using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonDropButtonExtensibilityActionList : KryptonExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonDropButton? _dropButton;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonDropButtonExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonDropButtonExtensibilityActionList(ControlDesigner owner)
        : base(owner)
    {
        _dropButton = (KryptonDropButton?)owner.Component;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets and sets the button style.
    /// </summary>
    public ButtonStyle ButtonStyle
    {
        get => _dropButton?.ButtonStyle ?? ButtonStyle.Standalone;
        set => SetPropertyValue(_dropButton!, nameof(ButtonStyle), ButtonStyle, value, v => _dropButton!.ButtonStyle = v);
    }

    /// <summary>
    /// Gets and sets the text associated with the control.
    /// </summary>
    public string Text
    {
        get => _dropButton?.Text ?? string.Empty;
        set => SetPropertyValue(_dropButton!, nameof(Text), Text, value, v => _dropButton!.Text = v);
    }

    /// <summary>
    /// Gets and sets the drop direction.
    /// </summary>
    public VisualOrientation DropDownPosition
    {
        get => _dropButton?.DropDownPosition ?? VisualOrientation.Bottom;
        set => SetPropertyValue(_dropButton!, nameof(DropDownPosition), DropDownPosition, value, v => _dropButton!.DropDownPosition = v);
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
        if (_dropButton != null)
        {
            // Add the list of DropButton specific actions
            actions.Add(new DesignerActionHeaderItem(@"Appearance"));
            actions.Add(new DesignerActionPropertyItem(nameof(ButtonStyle), @"Style", @"Appearance", @"Button style"));
            actions.Add(new DesignerActionPropertyItem(nameof(Text), @"Text", @"Appearance", @"Text content"));
            actions.Add(new DesignerActionPropertyItem(nameof(DropDownPosition), @"Drop Down Position", @"Appearance", @"Drop down position"));
        }

        return actions;
    }
    #endregion
}

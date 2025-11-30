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
/// Action list for KryptonButton using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonButtonExtensibilityActionList : KryptonExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonButton _button;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonButtonExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonButtonExtensibilityActionList(KryptonButtonExtensibilityDesigner owner)
        : base(owner)
    {
        // Remember the button instance
        _button = (owner.Component as KryptonButton)!;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the button style.
    /// </summary>
    public ButtonStyle ButtonStyle
    {
        get => _button.ButtonStyle;
        set => SetPropertyValue(_button, nameof(ButtonStyle), _button.ButtonStyle, value, v => _button.ButtonStyle = v);
    }

    /// <summary>Gets or sets the dialog result.</summary>
    /// <value>The dialog result.</value>
    public DialogResult DialogResult
    {
        get => _button.DialogResult;
        set => SetPropertyValue(_button, nameof(DialogResult), _button.DialogResult, value, v => _button.DialogResult = v);
    }

    /// <summary>Gets or sets the krypton context menu.</summary>
    /// <value>The krypton context menu.</value>
    public KryptonContextMenu? KryptonContextMenu
    {
        get => _button.KryptonContextMenu;
        set => SetPropertyValue(_button, nameof(KryptonContextMenu), _button.KryptonContextMenu, value, v => _button.KryptonContextMenu = v);
    }

    /// <summary>
    /// Gets and sets the visual orientation.
    /// </summary>
    public VisualOrientation Orientation
    {
        get => _button.Orientation;
        set => SetPropertyValue(_button, nameof(Orientation), _button.Orientation, value, v => _button.Orientation = v);
    }

    /// <summary>
    /// Gets and sets the button text.
    /// </summary>
    public string Text
    {
        get => _button.Values.Text;
        set => SetPropertyValue(_button, nameof(Text), _button.Values.Text, value, v => _button.Values.Text = v);
    }

    /// <summary>
    /// Gets and sets the extra button text.
    /// </summary>
    public string ExtraText
    {
        get => _button.Values.ExtraText;
        set => SetPropertyValue(_button, nameof(ExtraText), _button.Values.ExtraText, value, v => _button.Values.ExtraText = v);
    }

    /// <summary>
    /// Gets and sets the button image.
    /// </summary>
    public Image? Image
    {
        get => _button.Values.Image;
        set => SetPropertyValue(_button, nameof(Image), _button.Values.Image, value, v => _button.Values.Image = v);
    }

    /// <summary>
    /// Gets and sets the palette mode.
    /// </summary>
    public PaletteMode PaletteMode
    {
        get => _button.PaletteMode;
        set => SetPropertyValue(_button, nameof(PaletteMode), _button.PaletteMode, value, v => _button.PaletteMode = v);
    }

    /// <summary>Gets or sets the font.</summary>
    /// <value>The font.</value>
    public Font? StateCommonShortTextFont
    {
        get => _button.StateCommon.Content.ShortText.Font;
        set => SetPropertyValue(_button, nameof(StateCommonShortTextFont), _button.StateCommon.Content.ShortText.Font, value, v => _button.StateCommon.Content.ShortText.Font = v);
    }

    /// <summary>Gets or sets the font.</summary>
    /// <value>The font.</value>
    public Font? StateCommonLongTextFont
    {
        get => _button.StateCommon.Content.LongText.Font;
        set => SetPropertyValue(_button, nameof(StateCommonLongTextFont), _button.StateCommon.Content.LongText.Font, value, v => _button.StateCommon.Content.LongText.Font = v);
    }

    /// <summary>Gets or sets a value indicating whether [use as uac elevated button].</summary>
    /// <value><c>true</c> if [use as uac elevated button]; otherwise, <c>false</c>.</value>
    [DefaultValue(false)]
    public bool UseAsUACElevatedButton
    {
        get => _button.Values.UseAsUACElevationButton;
        set => SetPropertyValue(_button, nameof(UseAsUACElevatedButton), _button.Values.UseAsUACElevationButton, value, v => _button.Values.UseAsUACElevationButton = v);
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
        if (_button != null)
        {
            // Add the list of button specific actions
            actions.Add(new DesignerActionHeaderItem(nameof(Appearance)));
            actions.Add(new DesignerActionPropertyItem(nameof(ButtonStyle), @"Style", nameof(Appearance), @"Button style"));
            actions.Add(new DesignerActionPropertyItem(nameof(KryptonContextMenu), @"Krypton Context Menu", nameof(Appearance), @"The Krypton Context Menu for the control."));
            actions.Add(new DesignerActionPropertyItem(nameof(Orientation), nameof(Orientation), nameof(Appearance), @"Button orientation"));
            actions.Add(new DesignerActionPropertyItem(nameof(StateCommonShortTextFont), @"State Common Short Text Font", nameof(Appearance), @"The State Common Short Text Font."));
            actions.Add(new DesignerActionPropertyItem(nameof(StateCommonLongTextFont), @"State Common State Common Long Text Font", nameof(Appearance), @"The State Common State Common Long Text Font."));
            actions.Add(new DesignerActionHeaderItem(@"Values"));
            actions.Add(new DesignerActionPropertyItem(nameof(Text), nameof(Text), @"Values", @"Button text"));
            actions.Add(new DesignerActionPropertyItem(nameof(ExtraText), nameof(ExtraText), @"Values", @"Button extra text"));
            actions.Add(new DesignerActionPropertyItem(nameof(Image), nameof(Image), @"Values", @"Button image"));
            actions.Add(new DesignerActionPropertyItem(nameof(DialogResult), nameof(DialogResult), @"Values", @"The DialogResult for this button"));
            actions.Add(new DesignerActionHeaderItem(@"Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), @"Palette", @"Visuals", @"Palette applied to drawing"));
            actions.Add(new DesignerActionHeaderItem(@"UAC Elevation"));
            actions.Add(new DesignerActionPropertyItem(nameof(UseAsUACElevatedButton), @"Use as an UAC Elevated Button", @"UAC Elevation", @"Use this button to elevate a process."));
        }

        return actions;
    }
    #endregion
}

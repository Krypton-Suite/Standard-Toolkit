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

internal class KryptonButtonActionList : DesignerActionList
{
    #region Instance Fields
    private readonly KryptonButton _button;
    private readonly IComponentChangeService? _service;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonButtonActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonButtonActionList(KryptonButtonDesigner owner)
        : base(owner.Component)
    {
        // Remember the button instance
        _button = (owner.Component as KryptonButton)!;

        // Cache service used to notify when a property has changed
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the button style.
    /// </summary>
    public ButtonStyle ButtonStyle
    {
        get =>_button.ButtonStyle;

        set
        {
            if (_button.ButtonStyle != value)
            {
                _service?.OnComponentChanged(_button, null, _button.ButtonStyle, value);
                _button.ButtonStyle = value;
            }
        }
    }

    /// <summary>Gets or sets the dialog result.</summary>
    /// <value>The dialog result.</value>
    public DialogResult DialogResult
    {
        get =>_button.DialogResult;

        set
        {
            if (_button.DialogResult != value)
            {
                _service?.OnComponentChanged(_button, null, _button.DialogResult, value);
                _button.DialogResult = value;
            }
        }
    }

    /// <summary>Gets or sets the krypton context menu.</summary>
    /// <value>The krypton context menu.</value>
    public KryptonContextMenu? KryptonContextMenu
    {
        get =>_button.KryptonContextMenu;

        set
        {
            if (_button.KryptonContextMenu != value)
            {
                _service?.OnComponentChanged(_button, null, _button.KryptonContextMenu, value);

                _button.KryptonContextMenu = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the visual orientation.
    /// </summary>
    public VisualOrientation Orientation
    {
        get =>_button.Orientation;

        set
        {
            if (_button.Orientation != value)
            {
                _service?.OnComponentChanged(_button, null, _button.Orientation, value);
                _button.Orientation = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the button text.
    /// </summary>
    public string Text
    {
        get =>_button.Values.Text;

        set
        {
            if (_button.Values.Text != value)
            {
                _service?.OnComponentChanged(_button, null, _button.Values.Text, value);
                _button.Values.Text = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the extra button text.
    /// </summary>
    public string ExtraText
    {
        get =>_button.Values.ExtraText;

        set
        {
            if (_button.Values.ExtraText != value)
            {
                _service?.OnComponentChanged(_button, null, _button.Values.ExtraText, value);
                _button.Values.ExtraText = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the button image.
    /// </summary>
    public Image? Image
    {
        get =>_button.Values.Image;

        set
        {
            if (_button.Values.Image != value)
            {
                _service?.OnComponentChanged(_button, null, _button.Values.Image, value);
                _button.Values.Image = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the palette mode.
    /// </summary>
    public PaletteMode PaletteMode
    {
        get =>_button.PaletteMode;

        set
        {
            if (_button.PaletteMode != value)
            {
                _service?.OnComponentChanged(_button, null, _button.PaletteMode, value);
                _button.PaletteMode = value;
            }
        }
    }

    /// <summary>Gets or sets the font.</summary>
    /// <value>The font.</value>
    public Font? StateCommonShortTextFont
    {
        get =>_button.StateCommon.Content.ShortText.Font;

        set
        {
            if (!Equals(_button.StateCommon.Content.ShortText.Font, value))
            {
                _service?.OnComponentChanged(_button, null, _button.StateCommon.Content.ShortText.Font, value);

                _button.StateCommon.Content.ShortText.Font = value;
            }
        }
    }

    /// <summary>Gets or sets the font.</summary>
    /// <value>The font.</value>
    public Font? StateCommonLongTextFont
    {
        get =>_button.StateCommon.Content.LongText.Font;

        set
        {
            if (!Equals(_button.StateCommon.Content.LongText.Font, value))
            {
                _service?.OnComponentChanged(_button, null, _button.StateCommon.Content.LongText.Font, value);

                _button.StateCommon.Content.LongText.Font = value;
            }
        }
    }

    /// <summary>Gets or sets a value indicating whether [use as uac elevated button].</summary>
    /// <value><c>true</c> if [use as uac elevated button]; otherwise, <c>false</c>.</value>
    [DefaultValue(false)]
    public bool UseAsUACElevatedButton
    {
        get =>_button.Values.UseAsUACElevationButton;

        set
        {
            if (!_button.Values.UseAsUACElevationButton.Equals(value))
            {
                _service?.OnComponentChanged(_button, null, _button.Values.UseAsUACElevationButton, value);

                _button.Values.UseAsUACElevationButton = value;
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
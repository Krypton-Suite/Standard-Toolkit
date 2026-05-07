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

internal class KryptonRadioButtonActionList : DesignerActionList
{
    #region Instance Fields
    private readonly KryptonRadioButton _radioButton;
    private readonly IComponentChangeService? _service;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonRadioButtonActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonRadioButtonActionList(KryptonRadioButtonDesigner owner)
        : base(owner.Component)
    {
        // Remember the radio button instance
        _radioButton = (owner.Component as KryptonRadioButton)!;

        // Cache service used to notify when a property has changed
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets a value indicating if the radio button is checked.
    /// </summary>
    public bool Checked
    {
        get => _radioButton.Checked;

        set
        {
            if (_radioButton.Checked != value)
            {
                _service?.OnComponentChanged(_radioButton, null, _radioButton.Checked, value);
                _radioButton.Checked = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets a value indicating if the radio button should be three state.
    /// </summary>
    public bool AutoCheck
    {
        get => _radioButton.AutoCheck;

        set
        {
            if (_radioButton.AutoCheck != value)
            {
                _service?.OnComponentChanged(_radioButton, null, _radioButton.AutoCheck, value);
                _radioButton.AutoCheck = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the label style.
    /// </summary>
    public LabelStyle LabelStyle
    {
        get => _radioButton.LabelStyle;

        set
        {
            if (_radioButton.LabelStyle != value)
            {
                _service?.OnComponentChanged(_radioButton, null, _radioButton.LabelStyle, value);
                _radioButton.LabelStyle = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the visual orientation.
    /// </summary>
    public VisualOrientation Orientation
    {
        get => _radioButton.Orientation;

        set
        {
            if (_radioButton.Orientation != value)
            {
                _service?.OnComponentChanged(_radioButton, null, _radioButton.Orientation, value);
                _radioButton.Orientation = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the radio button text.
    /// </summary>
    public string Text
    {
        get => _radioButton.Values.Text;

        set
        {
            if (_radioButton.Values.Text != value)
            {
                _service?.OnComponentChanged(_radioButton, null, _radioButton.Values.Text, value);
                _radioButton.Values.Text = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the extra radio button text.
    /// </summary>
    public string ExtraText
    {
        get => _radioButton.Values.ExtraText;

        set
        {
            if (_radioButton.Values.ExtraText != value)
            {
                _service?.OnComponentChanged(_radioButton, null, _radioButton.Values.ExtraText, value);
                _radioButton.Values.ExtraText = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the radio button image.
    /// </summary>
    public Image? Image
    {
        get => _radioButton.Values.Image;

        set
        {
            if (_radioButton.Values.Image != value)
            {
                _service?.OnComponentChanged(_radioButton, null, _radioButton.Values.Image, value);
                _radioButton.Values.Image = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the palette mode.
    /// </summary>
    public PaletteMode PaletteMode
    {
        get => _radioButton.PaletteMode;

        set
        {
            if (_radioButton.PaletteMode != value)
            {
                _service?.OnComponentChanged(_radioButton, null, _radioButton.PaletteMode, value);
                _radioButton.PaletteMode = value;
            }
        }
    }

    /// <summary>Gets or sets the font.</summary>
    /// <value>The font.</value>
    public Font StateCommonShortTextFont
    {
        get => _radioButton.StateCommon.ShortText.Font!;

        set
        {
            if (_radioButton.StateCommon.ShortText.Font != value)
            {
                _service?.OnComponentChanged(_radioButton, null, _radioButton.StateCommon.ShortText.Font, value);

                _radioButton.StateCommon.ShortText.Font = value;
            }
        }
    }

    /// <summary>Gets or sets the font.</summary>
    /// <value>The font.</value>
    public Font StateCommonLongTextFont
    {
        get => _radioButton.StateCommon.LongText.Font!;

        set
        {
            if (_radioButton.StateCommon.LongText.Font != value)
            {
                _service?.OnComponentChanged(_radioButton, null, _radioButton.StateCommon.LongText.Font, value);

                _radioButton.StateCommon.LongText.Font = value;
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
        if (_radioButton != null)
        {
            // Add the list of radio button specific actions
            actions.Add(new DesignerActionHeaderItem(nameof(Operation)));
            actions.Add(new DesignerActionPropertyItem(nameof(Checked), nameof(Checked), nameof(Operation), @"Checked state"));
            actions.Add(new DesignerActionPropertyItem(nameof(AutoCheck), nameof(AutoCheck), nameof(Operation), @"AutoCheck of other instances."));
            actions.Add(new DesignerActionHeaderItem(nameof(Appearance)));
            actions.Add(new DesignerActionPropertyItem(nameof(LabelStyle), @"Style", nameof(Appearance), @"Label style"));
            actions.Add(new DesignerActionPropertyItem(nameof(Orientation), nameof(Orientation), nameof(Appearance), @"Visual orientation"));
            actions.Add(new DesignerActionPropertyItem(nameof(StateCommonShortTextFont), @"State Common Short Text Font", nameof(Appearance), @"The State Common Short Text Font."));
            actions.Add(new DesignerActionPropertyItem(nameof(StateCommonLongTextFont), @"State Common State Common Long Text Font", nameof(Appearance), @"The State Common State Common Long Text Font."));
            actions.Add(new DesignerActionHeaderItem(@"Values"));
            actions.Add(new DesignerActionPropertyItem(nameof(Text), nameof(Text), @"Values", @"Radio button text"));
            actions.Add(new DesignerActionPropertyItem(nameof(ExtraText), nameof(ExtraText), @"Values", @"Radio button extra text"));
            actions.Add(new DesignerActionPropertyItem(nameof(Image), nameof(Image), @"Values", @"Radio button image"));
            actions.Add(new DesignerActionHeaderItem(@"Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), @"Palette", @"Visuals", @"Palette applied to drawing"));
        }

        return actions;
    }
    #endregion
}
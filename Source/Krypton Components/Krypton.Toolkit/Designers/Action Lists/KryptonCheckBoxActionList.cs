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

internal class KryptonCheckBoxActionList : DesignerActionList
{
    #region Instance Fields
    private readonly KryptonCheckBox _checkBox;
    private readonly IComponentChangeService? _service;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonCheckBoxActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonCheckBoxActionList(KryptonCheckBoxDesigner owner)
        : base(owner.Component)
    {
        // Remember the checkbox instance
        _checkBox = (owner.Component as KryptonCheckBox)!;

        // Cache service used to notify when a property has changed
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets a value indicating if the check box is checked.
    /// </summary>
    public bool Checked
    {
        get => _checkBox.Checked;

        set
        {
            if (_checkBox.Checked != value)
            {
                _service?.OnComponentChanged(_checkBox, null, _checkBox.Checked, value);
                _checkBox.Checked = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the checked state.
    /// </summary>
    public CheckState CheckState
    {
        get => _checkBox.CheckState;

        set
        {
            if (_checkBox.CheckState != value)
            {
                _service?.OnComponentChanged(_checkBox, null, _checkBox.CheckState, value);
                _checkBox.CheckState = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets a value indicating if the check box should be three state.
    /// </summary>
    public bool ThreeState
    {
        get => _checkBox.ThreeState;

        set
        {
            if (_checkBox.ThreeState != value)
            {
                _service?.OnComponentChanged(_checkBox, null, _checkBox.ThreeState, value);
                _checkBox.ThreeState = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets a value indicating if the check box should be three state.
    /// </summary>
    public bool AutoCheck
    {
        get => _checkBox.AutoCheck;

        set
        {
            if (_checkBox.AutoCheck != value)
            {
                _service?.OnComponentChanged(_checkBox, null, _checkBox.AutoCheck, value);
                _checkBox.AutoCheck = value;
            }
        }
    }

    /// <summary>Gets or sets the Krypton Context Menu.</summary>
    /// <value>The Krypton Context Menu.</value>
    public KryptonContextMenu? KryptonContextMenu
    {
        get => _checkBox.KryptonContextMenu;

        set
        {
            if (_checkBox.KryptonContextMenu != value)
            {
                _service?.OnComponentChanged(_checkBox, null, _checkBox.KryptonContextMenu, value);

                _checkBox.KryptonContextMenu = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the label style.
    /// </summary>
    public LabelStyle LabelStyle
    {
        get => _checkBox.LabelStyle;

        set
        {
            if (_checkBox.LabelStyle != value)
            {
                _service?.OnComponentChanged(_checkBox, null, _checkBox.LabelStyle, value);
                _checkBox.LabelStyle = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the visual orientation.
    /// </summary>
    public VisualOrientation Orientation
    {
        get => _checkBox.Orientation;

        set
        {
            if (_checkBox.Orientation != value)
            {
                _service?.OnComponentChanged(_checkBox, null, _checkBox.Orientation, value);
                _checkBox.Orientation = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the checkbox text.
    /// </summary>
    public string Text
    {
        get => _checkBox.Values.Text;

        set
        {
            if (_checkBox.Values.Text != value)
            {
                _service?.OnComponentChanged(_checkBox, null, _checkBox.Values.Text, value);
                _checkBox.Values.Text = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the extra checkbox text.
    /// </summary>
    public string ExtraText
    {
        get => _checkBox.Values.ExtraText;

        set
        {
            if (_checkBox.Values.ExtraText != value)
            {
                _service?.OnComponentChanged(_checkBox, null, _checkBox.Values.ExtraText, value);
                _checkBox.Values.ExtraText = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the checkbox image.
    /// </summary>
    public Image? Image
    {
        get => _checkBox.Values.Image;

        set
        {
            if (_checkBox.Values.Image != value)
            {
                _service?.OnComponentChanged(_checkBox, null, _checkBox.Values.Image, value);
                _checkBox.Values.Image = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the palette mode.
    /// </summary>
    public PaletteMode PaletteMode
    {
        get => _checkBox.PaletteMode;

        set
        {
            if (_checkBox.PaletteMode != value)
            {
                _service?.OnComponentChanged(_checkBox, null, _checkBox.PaletteMode, value);
                _checkBox.PaletteMode = value;
            }
        }
    }

    /// <summary>Gets or sets the font.</summary>
    /// <value>The font.</value>
    public Font? StateCommonShortTextFont
    {
        get => _checkBox.StateCommon.ShortText.Font;

        set
        {
            if (_checkBox.StateCommon.ShortText.Font != value)
            {
                _service?.OnComponentChanged(_checkBox, null, _checkBox.StateCommon.ShortText.Font, value);

                _checkBox.StateCommon.ShortText.Font = value;
            }
        }
    }

    /// <summary>Gets or sets the font.</summary>
    /// <value>The font.</value>
    public Font? StateCommonLongTextFont
    {
        get => _checkBox.StateCommon.LongText.Font;

        set
        {
            if (_checkBox.StateCommon.LongText.Font != value)
            {
                _service?.OnComponentChanged(_checkBox, null, _checkBox.StateCommon.LongText.Font, value);

                _checkBox.StateCommon.LongText.Font = value;
            }
        }
    }

    /// <summary>Gets or sets the long text trim.</summary>
    /// <value>The long text trim.</value>
    public PaletteTextTrim LongTextTrim
    {
        get => _checkBox.StateCommon.LongText.Trim;

        set
        {
            if (_checkBox.StateCommon.LongText.Trim != value)
            {
                _service?.OnComponentChanged(_checkBox, null, _checkBox.StateCommon.LongText.Trim, value);

                _checkBox.StateCommon.LongText.Trim = value;
            }
        }
    }

    /// <summary>Gets or sets the short text trim.</summary>
    /// <value>The short text trim.</value>
    public PaletteTextTrim ShortTextTrim
    {
        get => _checkBox.StateCommon.ShortText.Trim;

        set
        {
            if (_checkBox.StateCommon.ShortText.Trim != value)
            {
                _service?.OnComponentChanged(_checkBox, null, _checkBox.StateCommon.ShortText.Trim, value);

                _checkBox.StateCommon.ShortText.Trim = value;
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
        if (_checkBox != null)
        {
            // Add the list of checkbox specific actions
            actions.Add(new DesignerActionHeaderItem(nameof(Operation)));
            actions.Add(new DesignerActionPropertyItem(nameof(Checked), nameof(Checked), nameof(Operation), @"Checked state"));
            actions.Add(new DesignerActionPropertyItem(nameof(AutoCheck), nameof(AutoCheck), nameof(Operation), @"AutoCheck of other instances."));
            actions.Add(new DesignerActionPropertyItem(nameof(ThreeState), nameof(ThreeState), nameof(Operation), @"ThreeState setting"));
            actions.Add(new DesignerActionHeaderItem(nameof(Appearance)));
            actions.Add(new DesignerActionPropertyItem(nameof(LabelStyle), @"Style", nameof(Appearance), @"Label style"));
            actions.Add(new DesignerActionPropertyItem(nameof(Orientation), nameof(Orientation), nameof(Appearance), @"Visual orientation"));
            actions.Add(new DesignerActionPropertyItem(nameof(StateCommonShortTextFont), @"State Common Short Text Font", nameof(Appearance), @"The State Common Short Text Font."));
            actions.Add(new DesignerActionPropertyItem(nameof(ShortTextTrim), @"Short Text Trim", nameof(Appearance), @"The trim mode of the short text."));
            actions.Add(new DesignerActionPropertyItem(nameof(StateCommonLongTextFont), @"State Common State Common Long Text Font", nameof(Appearance), @"The State Common State Common Long Text Font."));
            actions.Add(new DesignerActionPropertyItem(nameof(LongTextTrim), @"Long Text Trim", nameof(Appearance), @"The trim mode of the long text."));
            actions.Add(new DesignerActionPropertyItem(nameof(KryptonContextMenu), @"Krypton Context Menu", nameof(Appearance), @"The Krypton Context Menu for the control."));
            actions.Add(new DesignerActionHeaderItem(@"Values"));
            actions.Add(new DesignerActionPropertyItem(nameof(Text), nameof(Text), @"Values", @"Checkbox text"));
            actions.Add(new DesignerActionPropertyItem(nameof(ExtraText), nameof(ExtraText), @"Values", @"Checkbox extra text"));
            actions.Add(new DesignerActionPropertyItem(nameof(Image), nameof(Image), @"Values", @"Checkbox image"));
            actions.Add(new DesignerActionHeaderItem(@"Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), @"Palette", @"Visuals", @"Palette applied to drawing"));
        }

        return actions;
    }
    #endregion
}
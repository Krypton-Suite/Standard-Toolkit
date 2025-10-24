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

internal class KryptonLabelActionList : DesignerActionList
{
    #region Instance Fields
    private readonly KryptonLabel _label;
    private readonly IComponentChangeService? _service;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonLabelActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonLabelActionList(KryptonLabelDesigner owner)
        : base(owner.Component)
    {
        // Remember the label instance
        _label = (owner.Component as KryptonLabel)!;

        // Cache service used to notify when a property has changed
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the label style.
    /// </summary>
    public LabelStyle LabelStyle
    {
        get => _label.LabelStyle;

        set
        {
            if (_label.LabelStyle != value)
            {
                _service?.OnComponentChanged(_label, null, _label.LabelStyle, value);
                _label.LabelStyle = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the visual orientation.
    /// </summary>
    public VisualOrientation Orientation
    {
        get => _label.Orientation;

        set
        {
            if (_label.Orientation != value)
            {
                _service?.OnComponentChanged(_label, null, _label.Orientation, value);
                _label.Orientation = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the label text.
    /// </summary>
    public string Text
    {
        get => _label.Values.Text;

        set
        {
            if (_label.Values.Text != value)
            {
                _service?.OnComponentChanged(_label, null, _label.Values.Text, value);
                _label.Values.Text = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the extra label text.
    /// </summary>
    public string ExtraText
    {
        get => _label.Values.ExtraText;

        set
        {
            if (_label.Values.ExtraText != value)
            {
                _service?.OnComponentChanged(_label, null, _label.Values.ExtraText, value);
                _label.Values.ExtraText = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the label image.
    /// </summary>
    public Image? Image
    {
        get => _label.Values.Image;

        set
        {
            if (_label.Values.Image != value)
            {
                _service?.OnComponentChanged(_label, null, _label.Values.Image, value);
                _label.Values.Image = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the palette mode.
    /// </summary>
    public PaletteMode PaletteMode
    {
        get => _label.PaletteMode;

        set
        {
            if (_label.PaletteMode != value)
            {
                _service?.OnComponentChanged(_label, null, _label.PaletteMode, value);
                _label.PaletteMode = value;
            }
        }
    }

    /// <summary>Gets or sets the font.</summary>
    /// <value>The font.</value>
    public Font StateCommonShortTextFont
    {
        get => _label.StateCommon.ShortText.Font!;

        set
        {
            if (_label.StateCommon.ShortText.Font != value)
            {
                _service?.OnComponentChanged(_label, null, _label.StateCommon.ShortText.Font, value);

                _label.StateCommon.ShortText.Font = value;
            }
        }
    }

    /// <summary>Gets or sets the font.</summary>
    /// <value>The font.</value>
    public Font StateCommonLongTextFont
    {
        get => _label.StateCommon.LongText.Font!;

        set
        {
            if (_label.StateCommon.LongText.Font != value)
            {
                _service?.OnComponentChanged(_label, null, _label.StateCommon.LongText.Font, value);

                _label.StateCommon.LongText.Font = value;
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
        if (_label != null)
        {
            // Add the list of label specific actions
            actions.Add(new DesignerActionHeaderItem(nameof(Appearance)));
            actions.Add(new DesignerActionPropertyItem(nameof(LabelStyle), @"Style", nameof(Appearance), @"Label style"));
            actions.Add(new DesignerActionPropertyItem(nameof(Orientation), nameof(Orientation), nameof(Appearance), @"Visual orientation"));
            actions.Add(new DesignerActionPropertyItem(nameof(StateCommonShortTextFont), @"State Common Short Text Font", nameof(Appearance), @"The State Common Short Text Font."));
            actions.Add(new DesignerActionPropertyItem(nameof(StateCommonLongTextFont), @"State Common State Common Long Text Font", nameof(Appearance), @"The State Common State Common Long Text Font."));
            actions.Add(new DesignerActionHeaderItem(@"Values"));
            actions.Add(new DesignerActionPropertyItem(nameof(Text), nameof(Text), @"Values", @"Label text"));
            actions.Add(new DesignerActionPropertyItem(nameof(ExtraText), nameof(ExtraText), @"Values", @"Label extra text"));
            actions.Add(new DesignerActionPropertyItem(nameof(Image), nameof(Image), @"Values", @"Label image"));
            actions.Add(new DesignerActionHeaderItem(@"Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), @"Palette", @"Visuals", @"Palette applied to drawing"));
        }

        return actions;
    }
    #endregion
}
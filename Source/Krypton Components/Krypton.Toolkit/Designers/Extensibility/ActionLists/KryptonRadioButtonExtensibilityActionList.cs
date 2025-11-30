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
/// Action list for KryptonRadioButton using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonRadioButtonExtensibilityActionList : KryptonExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonRadioButton _radioButton;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonRadioButtonExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonRadioButtonExtensibilityActionList(KryptonRadioButtonExtensibilityDesigner owner)
        : base(owner)
    {
        // Remember the radiobutton instance
        _radioButton = (owner.Component as KryptonRadioButton)!;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the radiobutton style.
    /// </summary>
    public LabelStyle LabelStyle
    {
        get => _radioButton.LabelStyle;
        set => SetPropertyValue(_radioButton, nameof(LabelStyle), _radioButton.LabelStyle, value, v => _radioButton.LabelStyle = v);
    }

    /// <summary>
    /// Gets and sets the visual orientation.
    /// </summary>
    public VisualOrientation Orientation
    {
        get => _radioButton.Orientation;
        set => SetPropertyValue(_radioButton, nameof(Orientation), _radioButton.Orientation, value, v => _radioButton.Orientation = v);
    }

    /// <summary>
    /// Gets and sets the radiobutton text.
    /// </summary>
    public string Text
    {
        get => _radioButton.Text;
        set => SetPropertyValue(_radioButton, nameof(Text), _radioButton.Text, value, v => _radioButton.Text = v);
    }

    /// <summary>
    /// Gets and sets the extra radiobutton text.
    /// </summary>
    public string ExtraText
    {
        get => _radioButton.Values.ExtraText;
        set => SetPropertyValue(_radioButton, nameof(ExtraText), _radioButton.Values.ExtraText, value, v => _radioButton.Values.ExtraText = v);
    }

    /// <summary>
    /// Gets and sets the radiobutton image.
    /// </summary>
    public Image? Image
    {
        get => _radioButton.Values.Image;
        set => SetPropertyValue(_radioButton, nameof(Image), _radioButton.Values.Image, value, v => _radioButton.Values.Image = v);
    }

    /// <summary>
    /// Gets and sets the palette mode.
    /// </summary>
    public PaletteMode PaletteMode
    {
        get => _radioButton.PaletteMode;
        set => SetPropertyValue(_radioButton, nameof(PaletteMode), _radioButton.PaletteMode, value, v => _radioButton.PaletteMode = v);
    }

    /// <summary>
    /// Gets and sets the checked state.
    /// </summary>
    public bool Checked
    {
        get => _radioButton.Checked;
        set => SetPropertyValue(_radioButton, nameof(Checked), _radioButton.Checked, value, v => _radioButton.Checked = v);
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
            // Add the list of radiobutton specific actions
            actions.Add(new DesignerActionHeaderItem(nameof(Appearance)));
            actions.Add(new DesignerActionPropertyItem(nameof(LabelStyle), @"Style", nameof(Appearance), @"RadioButton style"));
            actions.Add(new DesignerActionPropertyItem(nameof(Orientation), nameof(Orientation), nameof(Appearance), @"RadioButton orientation"));
            actions.Add(new DesignerActionHeaderItem(@"Behavior"));
            actions.Add(new DesignerActionPropertyItem(nameof(Checked), nameof(Checked), @"Behavior", @"RadioButton checked state"));
            actions.Add(new DesignerActionHeaderItem(@"Values"));
            actions.Add(new DesignerActionPropertyItem(nameof(Text), nameof(Text), @"Values", @"RadioButton text"));
            actions.Add(new DesignerActionPropertyItem(nameof(ExtraText), nameof(ExtraText), @"Values", @"RadioButton extra text"));
            actions.Add(new DesignerActionPropertyItem(nameof(Image), nameof(Image), @"Values", @"RadioButton image"));
            actions.Add(new DesignerActionHeaderItem(@"Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), @"Palette", @"Visuals", @"Palette applied to drawing"));
        }

        return actions;
    }
    #endregion
}

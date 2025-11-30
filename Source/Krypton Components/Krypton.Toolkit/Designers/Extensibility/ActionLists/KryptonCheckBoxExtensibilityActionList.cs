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
/// Action list for KryptonCheckBox using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonCheckBoxExtensibilityActionList : KryptonExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonCheckBox _checkBox;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonCheckBoxExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonCheckBoxExtensibilityActionList(KryptonCheckBoxExtensibilityDesigner owner)
        : base(owner)
    {
        // Remember the checkbox instance
        _checkBox = (owner.Component as KryptonCheckBox)!;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the checkbox style.
    /// </summary>
    public LabelStyle LabelStyle
    {
        get => _checkBox.LabelStyle;
        set => SetPropertyValue(_checkBox, nameof(LabelStyle), _checkBox.LabelStyle, value, v => _checkBox.LabelStyle = v);
    }

    /// <summary>
    /// Gets and sets the visual orientation.
    /// </summary>
    public VisualOrientation Orientation
    {
        get => _checkBox.Orientation;
        set => SetPropertyValue(_checkBox, nameof(Orientation), _checkBox.Orientation, value, v => _checkBox.Orientation = v);
    }

    /// <summary>
    /// Gets and sets the checkbox text.
    /// </summary>
    public string Text
    {
        get => _checkBox.Text;
        set => SetPropertyValue(_checkBox, nameof(Text), _checkBox.Text, value, v => _checkBox.Text = v);
    }

    /// <summary>
    /// Gets and sets the extra checkbox text.
    /// </summary>
    public string ExtraText
    {
        get => _checkBox.Values.ExtraText;
        set => SetPropertyValue(_checkBox, nameof(ExtraText), _checkBox.Values.ExtraText, value, v => _checkBox.Values.ExtraText = v);
    }

    /// <summary>
    /// Gets and sets the checkbox image.
    /// </summary>
    public Image? Image
    {
        get => _checkBox.Values.Image;
        set => SetPropertyValue(_checkBox, nameof(Image), _checkBox.Values.Image, value, v => _checkBox.Values.Image = v);
    }

    /// <summary>
    /// Gets and sets the palette mode.
    /// </summary>
    public PaletteMode PaletteMode
    {
        get => _checkBox.PaletteMode;
        set => SetPropertyValue(_checkBox, nameof(PaletteMode), _checkBox.PaletteMode, value, v => _checkBox.PaletteMode = v);
    }

    /// <summary>
    /// Gets and sets the checked state.
    /// </summary>
    public bool Checked
    {
        get => _checkBox.Checked;
        set => SetPropertyValue(_checkBox, nameof(Checked), _checkBox.Checked, value, v => _checkBox.Checked = v);
    }

    /// <summary>
    /// Gets and sets the three state mode.
    /// </summary>
    public bool ThreeState
    {
        get => _checkBox.ThreeState;
        set => SetPropertyValue(_checkBox, nameof(ThreeState), _checkBox.ThreeState, value, v => _checkBox.ThreeState = v);
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
            actions.Add(new DesignerActionHeaderItem(nameof(Appearance)));
            actions.Add(new DesignerActionPropertyItem(nameof(LabelStyle), @"Style", nameof(Appearance), @"CheckBox style"));
            actions.Add(new DesignerActionPropertyItem(nameof(Orientation), nameof(Orientation), nameof(Appearance), @"CheckBox orientation"));
            actions.Add(new DesignerActionHeaderItem(@"Behavior"));
            actions.Add(new DesignerActionPropertyItem(nameof(Checked), nameof(Checked), @"Behavior", @"CheckBox checked state"));
            actions.Add(new DesignerActionPropertyItem(nameof(ThreeState), nameof(ThreeState), @"Behavior", @"Enable three state mode"));
            actions.Add(new DesignerActionHeaderItem(@"Values"));
            actions.Add(new DesignerActionPropertyItem(nameof(Text), nameof(Text), @"Values", @"CheckBox text"));
            actions.Add(new DesignerActionPropertyItem(nameof(ExtraText), nameof(ExtraText), @"Values", @"CheckBox extra text"));
            actions.Add(new DesignerActionPropertyItem(nameof(Image), nameof(Image), @"Values", @"CheckBox image"));
            actions.Add(new DesignerActionHeaderItem(@"Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), @"Palette", @"Visuals", @"Palette applied to drawing"));
        }

        return actions;
    }
    #endregion
}

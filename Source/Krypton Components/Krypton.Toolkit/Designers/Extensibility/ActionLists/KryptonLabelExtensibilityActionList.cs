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
/// Action list for KryptonLabel using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonLabelExtensibilityActionList : KryptonExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonLabel _label;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonLabelExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonLabelExtensibilityActionList(KryptonLabelExtensibilityDesigner owner)
        : base(owner)
    {
        // Remember the label instance
        _label = (owner.Component as KryptonLabel)!;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the label style.
    /// </summary>
    public LabelStyle LabelStyle
    {
        get => _label.LabelStyle;
        set => SetPropertyValue(_label, nameof(LabelStyle), _label.LabelStyle, value, v => _label.LabelStyle = v);
    }

    /// <summary>
    /// Gets and sets the visual orientation.
    /// </summary>
    public VisualOrientation Orientation
    {
        get => _label.Orientation;
        set => SetPropertyValue(_label, nameof(Orientation), _label.Orientation, value, v => _label.Orientation = v);
    }

    /// <summary>
    /// Gets and sets the label text.
    /// </summary>
    public string Text
    {
        get => _label.Values.Text;
        set => SetPropertyValue(_label, nameof(Text), _label.Values.Text, value, v => _label.Values.Text = v);
    }

    /// <summary>
    /// Gets and sets the extra label text.
    /// </summary>
    public string ExtraText
    {
        get => _label.Values.ExtraText;
        set => SetPropertyValue(_label, nameof(ExtraText), _label.Values.ExtraText, value, v => _label.Values.ExtraText = v);
    }

    /// <summary>
    /// Gets and sets the label image.
    /// </summary>
    public Image? Image
    {
        get => _label.Values.Image;
        set => SetPropertyValue(_label, nameof(Image), _label.Values.Image, value, v => _label.Values.Image = v);
    }

    /// <summary>
    /// Gets and sets the palette mode.
    /// </summary>
    public PaletteMode PaletteMode
    {
        get => _label.PaletteMode;
        set => SetPropertyValue(_label, nameof(PaletteMode), _label.PaletteMode, value, v => _label.PaletteMode = v);
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
            actions.Add(new DesignerActionPropertyItem(nameof(Orientation), nameof(Orientation), nameof(Appearance), @"Label orientation"));
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

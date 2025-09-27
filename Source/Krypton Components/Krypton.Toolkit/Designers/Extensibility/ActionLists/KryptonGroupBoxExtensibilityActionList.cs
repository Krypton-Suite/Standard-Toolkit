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
/// Action list for KryptonGroupBox using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonGroupBoxExtensibilityActionList : KryptonExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonGroupBox _groupBox;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonGroupBoxExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonGroupBoxExtensibilityActionList(KryptonGroupBoxExtensibilityDesigner owner)
        : base(owner)
    {
        // Remember the groupbox instance
        _groupBox = (owner.Component as KryptonGroupBox)!;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the groupbox text.
    /// </summary>
    public string Text
    {
        get => _groupBox.Text;
        set => SetPropertyValue(_groupBox, nameof(Text), _groupBox.Text, value, v => _groupBox.Text = v);
    }

    /// <summary>
    /// Gets and sets the caption edge.
    /// </summary>
    public VisualOrientation CaptionEdge
    {
        get => _groupBox.CaptionEdge;
        set => SetPropertyValue(_groupBox, nameof(CaptionEdge), _groupBox.CaptionEdge, value, v => _groupBox.CaptionEdge = v);
    }

    /// <summary>
    /// Gets and sets the caption orientation.
    /// </summary>
    public ButtonOrientation CaptionOrientation
    {
        get => _groupBox.CaptionOrientation;
        set => SetPropertyValue(_groupBox, nameof(CaptionOrientation), _groupBox.CaptionOrientation, value, v => _groupBox.CaptionOrientation = v);
    }

    /// <summary>
    /// Gets and sets the caption style.
    /// </summary>
    public LabelStyle CaptionStyle
    {
        get => _groupBox.CaptionStyle;
        set => SetPropertyValue(_groupBox, nameof(CaptionStyle), _groupBox.CaptionStyle, value, v => _groupBox.CaptionStyle = v);
    }

    /// <summary>
    /// Gets and sets the group back style.
    /// </summary>
    public PaletteBackStyle GroupBackStyle
    {
        get => _groupBox.GroupBackStyle;
        set => SetPropertyValue(_groupBox, nameof(GroupBackStyle), _groupBox.GroupBackStyle, value, v => _groupBox.GroupBackStyle = v);
    }

    /// <summary>
    /// Gets and sets the palette mode.
    /// </summary>
    public PaletteMode PaletteMode
    {
        get => _groupBox.PaletteMode;
        set => SetPropertyValue(_groupBox, nameof(PaletteMode), _groupBox.PaletteMode, value, v => _groupBox.PaletteMode = v);
    }

    /// <summary>Gets or sets the font.</summary>
    /// <value>The font.</value>
    public Font? StateCommonShortTextFont
    {
        get => _groupBox.StateCommon.Content.ShortText.Font;
        set => SetPropertyValue(_groupBox, nameof(StateCommonShortTextFont), _groupBox.StateCommon.Content.ShortText.Font, value, v => _groupBox.StateCommon.Content.ShortText.Font = v);
    }

    /// <summary>Gets or sets the font.</summary>
    /// <value>The font.</value>
    public Font? StateCommonLongTextFont
    {
        get => _groupBox.StateCommon.Content.LongText.Font;
        set => SetPropertyValue(_groupBox, nameof(StateCommonLongTextFont), _groupBox.StateCommon.Content.LongText.Font, value, v => _groupBox.StateCommon.Content.LongText.Font = v);
    }

    /// <summary>
    /// Gets and sets the auto size mode.
    /// </summary>
    public AutoSizeMode AutoSizeMode
    {
        get => _groupBox.AutoSizeMode;
        set => SetPropertyValue(_groupBox, nameof(AutoSizeMode), _groupBox.AutoSizeMode, value, v => _groupBox.AutoSizeMode = v);
    }

    /// <summary>
    /// Gets and sets whether the groupbox auto sizes.
    /// </summary>
    public bool AutoSize
    {
        get => _groupBox.AutoSize;
        set => SetPropertyValue(_groupBox, nameof(AutoSize), _groupBox.AutoSize, value, v => _groupBox.AutoSize = v);
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
        if (_groupBox != null)
        {
            // Add the list of groupbox specific actions
            actions.Add(new DesignerActionHeaderItem(nameof(Appearance)));
            actions.Add(new DesignerActionPropertyItem(nameof(CaptionEdge), nameof(CaptionEdge), nameof(Appearance), @"Caption edge position"));
            actions.Add(new DesignerActionPropertyItem(nameof(CaptionOrientation), nameof(CaptionOrientation), nameof(Appearance), @"Caption orientation"));
            actions.Add(new DesignerActionPropertyItem(nameof(CaptionStyle), nameof(CaptionStyle), nameof(Appearance), @"Caption style"));
            actions.Add(new DesignerActionPropertyItem(nameof(GroupBackStyle), nameof(GroupBackStyle), nameof(Appearance), @"Group back style"));
            actions.Add(new DesignerActionPropertyItem(nameof(StateCommonShortTextFont), @"State Common Short Text Font", nameof(Appearance), @"The State Common Short Text Font."));
            actions.Add(new DesignerActionPropertyItem(nameof(StateCommonLongTextFont), @"State Common Long Text Font", nameof(Appearance), @"The State Common Long Text Font."));
            actions.Add(new DesignerActionHeaderItem(@"Layout"));
            actions.Add(new DesignerActionPropertyItem(nameof(AutoSize), nameof(AutoSize), @"Layout", @"Automatically size the groupbox"));
            actions.Add(new DesignerActionPropertyItem(nameof(AutoSizeMode), nameof(AutoSizeMode), @"Layout", @"Auto size mode"));
            actions.Add(new DesignerActionHeaderItem(@"Values"));
            actions.Add(new DesignerActionPropertyItem(nameof(Text), nameof(Text), @"Values", @"GroupBox text"));
            actions.Add(new DesignerActionHeaderItem(@"Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), @"Palette", @"Visuals", @"Palette applied to drawing"));
        }

        return actions;
    }
    #endregion
}

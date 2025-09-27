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
/// Action list for the KryptonLinkWrapLabel control using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonLinkWrapLabelExtensibilityActionList : KryptonExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonLinkWrapLabel? _linkWrapLabel;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonLinkWrapLabelExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonLinkWrapLabelExtensibilityActionList(ControlDesigner owner)
        : base(owner)
    {
        _linkWrapLabel = (KryptonLinkWrapLabel?)owner.Component;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets and sets the palette to be applied.
    /// </summary>
    public PaletteMode PaletteMode
    {
        get => _linkWrapLabel?.PaletteMode ?? PaletteMode.Global;
        set => SetPropertyValue(_linkWrapLabel!, nameof(PaletteMode), PaletteMode, value, v => _linkWrapLabel!.PaletteMode = v);
    }

    /// <summary>
    /// Gets and sets the label style.
    /// </summary>
    public LabelStyle LabelStyle
    {
        get => _linkWrapLabel?.LabelStyle ?? LabelStyle.NormalControl;
        set => SetPropertyValue(_linkWrapLabel!, nameof(LabelStyle), LabelStyle, value, v => _linkWrapLabel!.LabelStyle = v);
    }

    /// <summary>
    /// Gets and sets the text associated with the control.
    /// </summary>
    public string Text
    {
        get => _linkWrapLabel?.Text ?? string.Empty;
        set => SetPropertyValue(_linkWrapLabel!, nameof(Text), Text, value, v => _linkWrapLabel!.Text = v);
    }

    /// <summary>
    /// Gets and sets the image to display.
    /// </summary>
    public Image? Image
    {
        get => _linkWrapLabel?.Image;
        set => SetPropertyValue(_linkWrapLabel!, nameof(Image), Image, value, v => _linkWrapLabel!.Image = v);
    }

    /// <summary>
    /// Gets and sets the link behavior.
    /// </summary>
    public LinkBehavior LinkBehavior
    {
        get => _linkWrapLabel?.LinkBehavior ?? LinkBehavior.SystemDefault;
        set => SetPropertyValue(_linkWrapLabel!, nameof(LinkBehavior), LinkBehavior, value, v => _linkWrapLabel!.LinkBehavior = v);
    }

    /// <summary>
    /// Gets and sets whether the link is visited.
    /// </summary>
    public bool LinkVisited
    {
        get => _linkWrapLabel?.LinkVisited ?? false;
        set => SetPropertyValue(_linkWrapLabel!, nameof(LinkVisited), LinkVisited, value, v => _linkWrapLabel!.LinkVisited = v);
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
        if (_linkWrapLabel != null)
        {
            // Add the list of LinkWrapLabel specific actions
            actions.Add(new DesignerActionHeaderItem(@"Appearance"));
            actions.Add(new DesignerActionPropertyItem(nameof(LabelStyle), @"Style", @"Appearance", @"Label style"));
            actions.Add(new DesignerActionPropertyItem(nameof(Text), @"Text", @"Appearance", @"Text content"));
            actions.Add(new DesignerActionPropertyItem(nameof(Image), @"Image", @"Appearance", @"Image content"));
            actions.Add(new DesignerActionHeaderItem(@"Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), @"Palette", @"Visuals", @"Palette applied to drawing"));
            actions.Add(new DesignerActionHeaderItem(@"Behavior"));
            actions.Add(new DesignerActionPropertyItem(nameof(LinkBehavior), @"Link Behavior", @"Behavior", @"Link behavior"));
            actions.Add(new DesignerActionPropertyItem(nameof(LinkVisited), @"Link Visited", @"Behavior", @"Link visited state"));
        }

        return actions;
    }
    #endregion
}

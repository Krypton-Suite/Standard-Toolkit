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
/// Action list for the KryptonHeaderGroup control using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonHeaderGroupExtensibilityActionList : KryptonExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonHeaderGroup? _headerGroup;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonHeaderGroupExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonHeaderGroupExtensibilityActionList(ControlDesigner owner)
        : base(owner)
    {
        _headerGroup = (KryptonHeaderGroup?)owner.Component;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets and sets the header group text.
    /// </summary>
    public string Text
    {
        get => _headerGroup?.Text ?? string.Empty;
        set => SetPropertyValue(_headerGroup!, nameof(Text), Text, value, v => _headerGroup!.Text = v);
    }

    /// <summary>
    /// Gets and sets the header group caption.
    /// </summary>
    public string Caption
    {
        get => _headerGroup?.ValuesPrimary.Heading ?? string.Empty;
        set => SetPropertyValue(_headerGroup!, nameof(Caption), Caption, value, v => _headerGroup!.ValuesPrimary.Heading = v);
    }

    /// <summary>
    /// Gets and sets the header group caption description.
    /// </summary>
    public string CaptionDescription
    {
        get => _headerGroup?.ValuesPrimary.Description ?? string.Empty;
        set => SetPropertyValue(_headerGroup!, nameof(CaptionDescription), CaptionDescription, value, v => _headerGroup!.ValuesPrimary.Description = v);
    }

    /// <summary>
    /// Gets and sets the header group caption image.
    /// </summary>
    public Image? CaptionImage
    {
        get => _headerGroup?.ValuesPrimary.Image;
        set => SetPropertyValue(_headerGroup!, nameof(CaptionImage), CaptionImage, value, v => _headerGroup!.ValuesPrimary.Image = v);
    }

    /// <summary>
    /// Gets and sets the header group primary position.
    /// </summary>
    public VisualOrientation HeaderPositionPrimary
    {
        get => _headerGroup?.HeaderPositionPrimary ?? VisualOrientation.Top;
        set => SetPropertyValue(_headerGroup!, nameof(HeaderPositionPrimary), HeaderPositionPrimary, value, v => _headerGroup!.HeaderPositionPrimary = v);
    }

    /// <summary>
    /// Gets and sets the header group secondary position.
    /// </summary>
    public VisualOrientation HeaderPositionSecondary
    {
        get => _headerGroup?.HeaderPositionSecondary ?? VisualOrientation.Bottom;
        set => SetPropertyValue(_headerGroup!, nameof(HeaderPositionSecondary), HeaderPositionSecondary, value, v => _headerGroup!.HeaderPositionSecondary = v);
    }

    /// <summary>
    /// Gets and sets whether the header group primary is visible.
    /// </summary>
    public bool HeaderVisiblePrimary
    {
        get => _headerGroup?.HeaderVisiblePrimary ?? true;
        set => SetPropertyValue(_headerGroup!, nameof(HeaderVisiblePrimary), HeaderVisiblePrimary, value, v => _headerGroup!.HeaderVisiblePrimary = v);
    }

    /// <summary>
    /// Gets and sets whether the header group secondary is visible.
    /// </summary>
    public bool HeaderVisibleSecondary
    {
        get => _headerGroup?.HeaderVisibleSecondary ?? true;
        set => SetPropertyValue(_headerGroup!, nameof(HeaderVisibleSecondary), HeaderVisibleSecondary, value, v => _headerGroup!.HeaderVisibleSecondary = v);
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
        if (_headerGroup != null)
        {
            // Add the list of HeaderGroup specific actions
            actions.Add(new DesignerActionHeaderItem(@"Appearance"));
            actions.Add(new DesignerActionPropertyItem(nameof(Text), @"Text", @"Appearance", @"Header group text"));
            actions.Add(new DesignerActionPropertyItem(nameof(Caption), @"Caption", @"Appearance", @"Group caption"));
            actions.Add(new DesignerActionPropertyItem(nameof(CaptionDescription), @"Caption Description", @"Appearance", @"Group caption description"));
            actions.Add(new DesignerActionPropertyItem(nameof(CaptionImage), @"Caption Image", @"Appearance", @"Group caption image"));
            actions.Add(new DesignerActionPropertyItem(nameof(HeaderPositionPrimary), @"Header Position Primary", @"Appearance", @"Primary header position"));
            actions.Add(new DesignerActionPropertyItem(nameof(HeaderPositionSecondary), @"Header Position Secondary", @"Appearance", @"Secondary header position"));
            actions.Add(new DesignerActionHeaderItem(@"Behavior"));
            actions.Add(new DesignerActionPropertyItem(nameof(HeaderVisiblePrimary), @"Header Visible Primary", @"Behavior", @"Primary header visible"));
            actions.Add(new DesignerActionPropertyItem(nameof(HeaderVisibleSecondary), @"Header Visible Secondary", @"Behavior", @"Secondary header visible"));
        }

        return actions;
    }
    #endregion
}

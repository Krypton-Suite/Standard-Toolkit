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
/// Action list for the KryptonBreadCrumbItem control using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonBreadCrumbItemExtensibilityActionList : KryptonExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonBreadCrumbItem? _breadCrumbItem;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonBreadCrumbItemExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonBreadCrumbItemExtensibilityActionList(ComponentDesigner owner)
        : base(owner)
    {
        _breadCrumbItem = (KryptonBreadCrumbItem?)owner.Component;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets and sets the breadcrumb item short text.
    /// </summary>
    public string ShortText
    {
        get => _breadCrumbItem?.ShortText ?? string.Empty;
        set => SetPropertyValue(_breadCrumbItem!, nameof(ShortText), ShortText, value, v => _breadCrumbItem!.ShortText = v);
    }

    /// <summary>
    /// Gets and sets the breadcrumb item long text.
    /// </summary>
    public string LongText
    {
        get => _breadCrumbItem?.LongText ?? string.Empty;
        set => SetPropertyValue(_breadCrumbItem!, nameof(LongText), LongText, value, v => _breadCrumbItem!.LongText = v);
    }

    /// <summary>
    /// Gets and sets the breadcrumb item image.
    /// </summary>
    public Image? Image
    {
        get => _breadCrumbItem?.Image;
        set => SetPropertyValue(_breadCrumbItem!, nameof(Image), Image, value, v => _breadCrumbItem!.Image = v);
    }

    /// <summary>
    /// Gets and sets the breadcrumb item tag.
    /// </summary>
    public object? Tag
    {
        get => _breadCrumbItem?.Tag;
        set => SetPropertyValue(_breadCrumbItem!, nameof(Tag), Tag, value, v => _breadCrumbItem!.Tag = v);
    }

    /// <summary>
    /// Gets and sets the breadcrumb item image transparent color.
    /// </summary>
    public Color ImageTransparentColor
    {
        get => _breadCrumbItem?.ImageTransparentColor ?? Color.Empty;
        set => SetPropertyValue(_breadCrumbItem!, nameof(ImageTransparentColor), ImageTransparentColor, value, v => _breadCrumbItem!.ImageTransparentColor = v);
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
        if (_breadCrumbItem != null)
        {
            // Add the list of BreadCrumbItem specific actions
            actions.Add(new DesignerActionHeaderItem(@"Appearance"));
            actions.Add(new DesignerActionPropertyItem(nameof(ShortText), @"Short Text", @"Appearance", @"Item short text"));
            actions.Add(new DesignerActionPropertyItem(nameof(LongText), @"Long Text", @"Appearance", @"Item long text"));
            actions.Add(new DesignerActionPropertyItem(nameof(Image), @"Image", @"Appearance", @"Item image"));
            actions.Add(new DesignerActionPropertyItem(nameof(ImageTransparentColor), @"Image Transparent Color", @"Appearance", @"Image transparent color"));
            actions.Add(new DesignerActionPropertyItem(nameof(Tag), @"Tag", @"Appearance", @"Item tag"));
        }

        return actions;
    }
    #endregion
}

#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, tobitege, Lesandro & KamaniAR et al. 2025 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Action list for KryptonGallery using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonGalleryExtensibilityActionList : KryptonRibbonExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonGallery _gallery;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonGalleryExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list.</param>
    public KryptonGalleryExtensibilityActionList(KryptonGalleryExtensibilityDesigner owner)
        : base(owner)
    {
        _gallery = (owner.Component as KryptonGallery)!;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets or sets the selected index.
    /// </summary>
    [Category("Behavior")]
    [Description("Selected index.")]
    [DefaultValue(-1)]
    public int SelectedIndex
    {
        get => _gallery.SelectedIndex;
        set => SetPropertyValue(nameof(SelectedIndex), value);
    }

    /// <summary>
    /// Gets or sets the preferred item size.
    /// </summary>
    [Category("Appearance")]
    [Description("Preferred item size.")]
    [DefaultValue(typeof(Size), "16, 16")]
    public Size PreferredItemSize
    {
        get => _gallery.PreferredItemSize;
        set => SetPropertyValue(nameof(PreferredItemSize), value);
    }

    /// <summary>
    /// Gets or sets the image list.
    /// </summary>
    [Category("Data")]
    [Description("Image list.")]
    [DefaultValue(null)]
    public ImageList? ImageList
    {
        get => _gallery.ImageList;
        set => SetPropertyValue(nameof(ImageList), value ?? (object)string.Empty);
    }
    #endregion

    #region Public Overrides
    /// <summary>
    /// Returns the collection of DesignerActionItem objects contained in the list.
    /// </summary>
    /// <returns>A DesignerActionItem array that contains the items in this list.</returns>
    public override DesignerActionItemCollection GetSortedActionItems()
    {
        var items = new DesignerActionItemCollection();

        // Add the action items
        items.Add(new DesignerActionHeaderItem("Appearance"));
        items.Add(new DesignerActionPropertyItem(nameof(PreferredItemSize), "Preferred Item Size", "Appearance", "Preferred item size."));
        items.Add(new DesignerActionHeaderItem("Behavior"));
        items.Add(new DesignerActionPropertyItem(nameof(SelectedIndex), "Selected Index", "Behavior", "Selected index."));
        items.Add(new DesignerActionHeaderItem("Data"));
        items.Add(new DesignerActionPropertyItem(nameof(ImageList), "Image List", "Data", "Image list."));

        return items;
    }
    #endregion
}

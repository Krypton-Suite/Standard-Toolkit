#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, tobitege, Lesandro & KamaniAR et al. 2025 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Navigator;

/// <summary>
/// Action list for KryptonPage using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonPageExtensibilityActionList : KryptonNavigatorExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonPage _page;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonPageExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list.</param>
    public KryptonPageExtensibilityActionList(KryptonPageExtensibilityDesigner owner)
        : base(owner)
    {
        _page = (owner.Component as KryptonPage)!;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets or sets the page text.
    /// </summary>
    [Category("Appearance")]
    [Description("Page text.")]
    [DefaultValue("")]
    public string Text
    {
        get => _page.Text;
        set => SetPropertyValue(nameof(Text), value);
    }

    /// <summary>
    /// Gets or sets the page title.
    /// </summary>
    [Category("Appearance")]
    [Description("Page title.")]
    [DefaultValue("")]
    public string TextTitle
    {
        get => _page.TextTitle;
        set => SetPropertyValue(nameof(TextTitle), value);
    }

    /// <summary>
    /// Gets or sets the page description.
    /// </summary>
    [Category("Appearance")]
    [Description("Page description.")]
    [DefaultValue("")]
    public string TextDescription
    {
        get => _page.TextDescription;
        set => SetPropertyValue(nameof(TextDescription), value);
    }

    /// <summary>
    /// Gets or sets the page image.
    /// </summary>
    [Category("Appearance")]
    [Description("Page image.")]
    [DefaultValue(null)]
    public Image? ImageSmall
    {
        get => _page.ImageSmall;
        set => SetPropertyValue(nameof(ImageSmall), value ?? (object)string.Empty);
    }

    /// <summary>
    /// Gets or sets the page medium image.
    /// </summary>
    [Category("Appearance")]
    [Description("Page medium image.")]
    [DefaultValue(null)]
    public Image? ImageMedium
    {
        get => _page.ImageMedium;
        set => SetPropertyValue(nameof(ImageMedium), value ?? (object)string.Empty);
    }

    /// <summary>
    /// Gets or sets the page large image.
    /// </summary>
    [Category("Appearance")]
    [Description("Page large image.")]
    [DefaultValue(null)]
    public Image? ImageLarge
    {
        get => _page.ImageLarge;
        set => SetPropertyValue(nameof(ImageLarge), value ?? (object)string.Empty);
    }

    /// <summary>
    /// Gets or sets the page tooltip title.
    /// </summary>
    [Category("Appearance")]
    [Description("Page tooltip title.")]
    [DefaultValue("")]
    public string ToolTipTitle
    {
        get => _page.ToolTipTitle;
        set => SetPropertyValue(nameof(ToolTipTitle), value);
    }

    /// <summary>
    /// Gets or sets the page tooltip image.
    /// </summary>
    [Category("Appearance")]
    [Description("Page tooltip image.")]
    [DefaultValue(null)]
    public Image? ToolTipImage
    {
        get => _page.ToolTipImage;
        set => SetPropertyValue(nameof(ToolTipImage), value ?? (object)string.Empty);
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
        items.Add(new DesignerActionPropertyItem(nameof(Text), "Text", "Appearance", "Page text."));
        items.Add(new DesignerActionPropertyItem(nameof(TextTitle), "Text Title", "Appearance", "Page title."));
        items.Add(new DesignerActionPropertyItem(nameof(TextDescription), "Text Description", "Appearance", "Page description."));
        items.Add(new DesignerActionPropertyItem(nameof(ImageSmall), "Image Small", "Appearance", "Page small image."));
        items.Add(new DesignerActionPropertyItem(nameof(ImageMedium), "Image Medium", "Appearance", "Page medium image."));
        items.Add(new DesignerActionPropertyItem(nameof(ImageLarge), "Image Large", "Appearance", "Page large image."));
        items.Add(new DesignerActionPropertyItem(nameof(ToolTipTitle), "ToolTip Title", "Appearance", "Page tooltip title."));
        items.Add(new DesignerActionPropertyItem(nameof(ToolTipImage), "ToolTip Image", "Appearance", "Page tooltip image."));

        return items;
    }
    #endregion
}

#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Draws an design time only for adding a new container to a group.
/// </summary>
internal class ViewDrawRibbonDesignGroupContainer : ViewDrawRibbonDesignBase
{
    #region Static Fields
    private static readonly ImageList _imageList;
    #endregion

    #region Instance Fields
    private readonly KryptonRibbonGroup _ribbonGroup;
    private ContextMenuStrip _cms;
    private readonly Padding _padding; // = new(1, 0, 0, 0);
    #endregion

    #region Identity
    static ViewDrawRibbonDesignGroupContainer()
    {
        // Use image list to convert background Magenta to transparent
        _imageList = new ImageList
        {
            TransparentColor = GlobalStaticValues.TRANSPARENCY_KEY_COLOR
        };
        _imageList.Images.AddRange([
            GenericImageResources.KryptonRibbonGroupTriple,
            GenericImageResources.KryptonRibbonGroupLines,
            GenericImageResources.KryptonRibbonGroupSeparator,
            GenericImageResources.KryptonGallery
        ]);
    }

    /// <summary>
    /// Initialize a new instance of the ViewDrawRibbonDesignGroup class.
    /// </summary>
    /// <param name="ribbon">Reference to owning ribbon control.</param>
    /// <param name="ribbonGroup">Associated ribbon group.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public ViewDrawRibbonDesignGroupContainer(KryptonRibbon ribbon,
        [DisallowNull] KryptonRibbonGroup ribbonGroup,
        NeedPaintHandler needPaint)
        : base(ribbon, needPaint)
    {
        Debug.Assert(ribbonGroup != null);
        _ribbonGroup = ribbonGroup!;
        _padding = new Padding((int)(1 * FactorDpiX), 0, 0, 0);
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $@"ViewDrawRibbonDesignGroupContainer:{Id}";

    #endregion

    #region Protected
    /// <summary>
    /// Gets the short text used as the main ribbon title.
    /// </summary>
    /// <returns>Title string.</returns>
    public override string GetShortText() => "New";

    /// <summary>
    /// Gets the padding to use when calculating the preferred size.
    /// </summary>
    protected override Padding PreferredPadding => _padding;

    /// <summary>
    /// Gets the padding to use when laying out the view.
    /// </summary>
    protected override Padding LayoutPadding => Padding.Empty;

    /// <summary>
    /// Gets the padding to shrink the client area by when laying out.
    /// </summary>
    protected override Padding OuterPadding => _padding;

    /// <summary>
    /// Raises the Click event.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected override void OnClick(object? sender, EventArgs e)
    {
        // Create the context strip the first time around
        if (_cms == null)
        {
            _cms = new ContextMenuStrip
            {
                ImageList = _imageList
            };

            // Create child items
            var menuTriple = new ToolStripMenuItem("Add Triple", null, OnAddTriple);
            var menuLines = new ToolStripMenuItem("Add Lines", null, OnAddLines);
            var menuSeparator = new ToolStripMenuItem("Add Separator", null, OnAddSeparator);
            var menuGallery = new ToolStripMenuItem("Add Gallery", null, OnAddGallery);

            // Assign correct images
            menuTriple.ImageIndex = 0;
            menuLines.ImageIndex = 1;
            menuSeparator.ImageIndex = 2;
            menuGallery.ImageIndex = 3;

            // Finally, add all items to the strip
            _cms.Items.AddRange(new ToolStripItem[] { menuTriple, menuLines, menuSeparator, menuGallery });
        }

        if (CommonHelper.ValidContextMenuStrip(_cms))
        {
            // Find the screen area of this view item
            Rectangle screenRect = Ribbon.ViewRectangleToScreen(this);

            // Make sure the popup is shown in a compatible way with any popups
            VisualPopupManager.Singleton.ShowContextMenuStrip(_cms, new Point(screenRect.X, screenRect.Bottom));
        }
    }
    #endregion

    #region Implementation
    private void OnAddTriple(object? sender, EventArgs e) => _ribbonGroup.OnDesignTimeAddTriple();

    private void OnAddLines(object? sender, EventArgs e) => _ribbonGroup.OnDesignTimeAddLines();

    private void OnAddSeparator(object? sender, EventArgs e) => _ribbonGroup.OnDesignTimeAddSeparator();

    private void OnAddGallery(object? sender, EventArgs e) => _ribbonGroup.OnDesignTimeAddGallery();
    #endregion
}
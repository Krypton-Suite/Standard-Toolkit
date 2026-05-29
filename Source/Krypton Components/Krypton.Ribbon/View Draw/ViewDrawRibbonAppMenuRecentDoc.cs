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
/// Draw the image/text of a recent document in the application menu.
/// </summary>
internal class ViewDrawRibbonAppMenuRecentDec : ViewDrawCanvas
{
    #region Instance Fields
    private readonly int _maxWidth;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawRibbonAppMenuRecentDec class.
    /// </summary>
    /// <param name="ribbon">Reference to owning ribbon instance.</param>
    /// <param name="provider">Provider of context menu information.</param>
    /// <param name="recentDoc">Source recent document instance.</param>
    /// <param name="maxWidth">Maximum width allowed for the item.</param>
    /// <param name="needPaintDelegate">Delegate for requesting paint updates.</param>
    /// <param name="index">Recent document index.</param>
    public ViewDrawRibbonAppMenuRecentDec(KryptonRibbon ribbon,
        IContextMenuProvider provider,
        KryptonRibbonRecentDoc recentDoc,
        int maxWidth,
        NeedPaintHandler needPaintDelegate,
        int index)
        : base(provider.ProviderStateNormal.ItemHighlight.Back,
            provider.ProviderStateNormal.ItemHighlight.Border,
            provider.ProviderStateNormal.ItemHighlight,
            PaletteMetricPadding.ContextMenuItemHighlight,
            VisualOrientation.Top)
    {
        _maxWidth = maxWidth;
        Provider = provider;
        RecentDoc = recentDoc;
        ShortcutText = index < 10 ? $@"&{index}" : @"A";

        // Use docker to organize horizontal items
        var docker = new ViewLayoutDocker
        {

            // End of line gap
            { new ViewLayoutSeparator(5), ViewDockStyle.Right }
        };

        // Add the text/extraText/Image entry
        var entryContent = new FixedContentValue(recentDoc.Text, recentDoc.ExtraText, recentDoc.Image,
            recentDoc.ImageTransparentColor);
        var entryPalette = new RibbonRecentDocsEntryToContent(ribbon.StateCommon.RibbonGeneral,
            ribbon.StateCommon.RibbonAppMenuDocsEntry);
        var entryDraw = new ViewDrawContent(entryPalette, entryContent, VisualOrientation.Top);
        docker.Add(entryDraw, ViewDockStyle.Fill);

        // Shortcut to Content gap
        docker.Add(new ViewLayoutSeparator(5), ViewDockStyle.Left);

        // Add the shortcut column
        var shortcutContent = new FixedContentValue(ShortcutText, null, null, Color.Empty);
        var shortcutPalette = new RibbonRecentDocsShortcutToContent(ribbon.StateCommon.RibbonGeneral,
            ribbon.StateCommon.RibbonAppMenuDocsEntry);
        var shortcutDraw = new ViewDrawRibbonRecentShortcut(shortcutPalette, shortcutContent);
        docker.Add(shortcutDraw, ViewDockStyle.Left);

        // Start of line gap
        docker.Add(new ViewLayoutSeparator(3), ViewDockStyle.Left);

        // Attach a controller so menu item can be tracked and pressed
        var controller = new RecentDocController(Provider.ProviderViewManager, this, needPaintDelegate);
        MouseController = controller;
        KeyController = controller;
        SourceController = controller;

        Add(docker);
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $@"ViewDrawRibbonAppMenuRecentDec:{Id}";

    #endregion

    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        // Validate incoming reference
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        // Enforce the maximum width value
        Size preferredSize = base.GetPreferredSize(context);
        preferredSize.Width = Math.Min(_maxWidth, preferredSize.Width);
        return preferredSize;
    }

    #region Public
    /// <summary>
    /// Gets access to the originating recent doc definition.
    /// </summary>
    public KryptonRibbonRecentDoc RecentDoc { get; }

    /// <summary>
    /// Gets access to the items shortcut text.
    /// </summary>
    public string ShortcutText { get; }

    /// <summary>
    /// Gets a value indicating if the menu is capable of being closed.
    /// </summary>
    public bool CanCloseMenu => Provider.ProviderCanCloseMenu;

    /// <summary>
    /// Raises the Closing event on the provider.
    /// </summary>
    /// <param name="cea">A CancelEventArgs containing the event data.</param>
    public void Closing(CancelEventArgs cea) => Provider.OnClosing(cea);

    /// <summary>
    /// Raises the Close event on the provider.
    /// </summary>
    /// <param name="e">A CancelEventArgs containing the event data.</param>
    public void Close(CloseReasonEventArgs e) => Provider.OnClose(e);

    /// <summary>
    /// Gets direct access to the context menu provider.
    /// </summary>
    public IContextMenuProvider Provider { get; }

    #endregion
}
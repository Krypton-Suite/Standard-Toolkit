﻿namespace Krypton.Ribbon
{
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
            ShortcutText = index < 10 ? @"&" + index.ToString() : @"A";

            // Use docker to organize horizontal items
            ViewLayoutDocker docker = new()
            {

                // End of line gap
                { new ViewLayoutSeparator(5), ViewDockStyle.Right }
            };

            // Add the text/extraText/Image entry
            FixedContentValue entryContent = new(recentDoc.Text, recentDoc.ExtraText, recentDoc.Image, recentDoc.ImageTransparentColor);
            RibbonRecentDocsEntryToContent entryPalette = new(ribbon.StateCommon.RibbonGeneral, ribbon.StateCommon.RibbonAppMenuDocsEntry);
            ViewDrawContent entryDraw = new(entryPalette, entryContent, VisualOrientation.Top);
            docker.Add(entryDraw, ViewDockStyle.Fill);

            // Shortcut to Content gap
            docker.Add(new ViewLayoutSeparator(5), ViewDockStyle.Left);

            // Add the shortcut column
            FixedContentValue shortcutContent = new(ShortcutText, null, null, Color.Empty);
            RibbonRecentDocsShortcutToContent shortcutPalette = new(ribbon.StateCommon.RibbonGeneral, ribbon.StateCommon.RibbonAppMenuDocsEntry);
            ViewDrawRibbonRecentShortcut shortcutDraw = new(shortcutPalette, shortcutContent);
            docker.Add(shortcutDraw, ViewDockStyle.Left);

            // Start of line gap
            docker.Add(new ViewLayoutSeparator(3), ViewDockStyle.Left);

            // Attach a controller so menu item can be tracked and pressed
            RecentDocController controller = new(Provider.ProviderViewManager, this, needPaintDelegate);
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
            @"ViewDrawRibbonAppMenuRecentDec:" + Id;

        #endregion

        /// <summary>
        /// Discover the preferred size of the element.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override Size GetPreferredSize(ViewLayoutContext context)
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
        public void Closing(CancelEventArgs cea)
        {
            Provider.OnClosing(cea);
        }

        /// <summary>
        /// Raises the Close event on the provider.
        /// </summary>
        /// <param name="e">A CancelEventArgs containing the event data.</param>
        public void Close(CloseReasonEventArgs e)
        {
            Provider.OnClose(e);
        }

        /// <summary>
        /// Gets direct access to the context menu provider.
        /// </summary>
        public IContextMenuProvider Provider { get; }

        #endregion
    }
}

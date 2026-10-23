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
/// Ribbon layout that creates and positions context title drawing elements.
/// </summary>
internal class ViewLayoutRibbonContextTitles : ViewLayoutDocker
{
    #region Classes

    private class ViewDrawRibbonContextTitleList : List<ViewDrawRibbonContextTitle>;

    #endregion

    #region Instance Fields
    private readonly KryptonRibbon _ribbon;
    private readonly ViewDrawRibbonContextTitleList _contextTitlesCache;
    private readonly ViewDrawRibbonCaptionArea _captionArea;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewLayoutRibbonContextTitles class.
    /// </summary>
    /// <param name="ribbon">Reference to source ribbon control.</param>
    /// <param name="captionArea">Reference to view element that tracks the top level form.</param>
    public ViewLayoutRibbonContextTitles([DisallowNull] KryptonRibbon? ribbon,
        [DisallowNull] ViewDrawRibbonCaptionArea? captionArea)
    {
        Debug.Assert(captionArea is not null);
        Debug.Assert(ribbon is not null);
            
        _ribbon = ribbon ?? throw new ArgumentNullException(nameof(ribbon));
        _captionArea = captionArea ?? throw new ArgumentNullException(nameof(captionArea));

        // Create cache of draw elements
        _contextTitlesCache = [];
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $@"ViewLayoutRibbonContextTitles:{Id}";

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            Clear();

            foreach (ViewDrawRibbonContextTitle title in _contextTitlesCache)
            {
                title.Dispose();
            }

            _contextTitlesCache.Clear();
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize(ViewLayoutContext context) =>
        // We have no preferred size, we take all we are given
        Size.Empty;

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        // Sync children to match the current context tabs
        SyncChildrenToContexts();

        // We take on all the available display area
        ClientRectangle = context!.DisplayRectangle;

        // Find any filler child
        ViewBase? filler = this.FirstOrDefault(child => GetDock(child) == ViewDockStyle.Fill);

        var xLeftMost = ClientRectangle.Right;
        var xRightMost = ClientRectangle.Left;

        // Find the correct position for each child context set
        // We are only interested in laying out context titles
        foreach (ViewDrawRibbonContextTitle childContextTitle in this.Where(static child => child.Visible).OfType<ViewDrawRibbonContextTitle>())
        {
            // Get the context set it is representing
            ContextTabSet tabContext = childContextTitle.ContextTabSet!;

            // Get the screen position of the left and right hand positions
            Point leftTab = tabContext.GetLeftScreenPosition();
            Point rightTab = tabContext.GetRightScreenPosition();

            // If our position is above the ribbon control we must be in the chrome
            if (_captionArea is { UsingCustomChrome: true})
            {
                var leftPadding = _captionArea.RealWindowBorders.Left;
                leftTab.X += leftPadding;
                rightTab.X += leftPadding;
            }

            // Convert the screen to our own coordinates
            leftTab = context.TopControl!.PointToClient(leftTab);
            rightTab = context.TopControl.PointToClient(rightTab);

            // Calculate the position of the child and layout
            context.DisplayRectangle = new Rectangle(leftTab.X, ClientLocation.Y, rightTab.X - leftTab.X, ClientHeight);
            childContextTitle.Layout(context);

            // Track the left and right most positions
            xLeftMost = Math.Min(xLeftMost, leftTab.X);
            xRightMost = Math.Max(xRightMost, rightTab.X);
        }

        // Do we need to position a filler element?
        if (filler != null)
        {
            // How much space available on the left side
            var leftSpace = xLeftMost - ClientRectangle.Left;
            var rightSpace = ClientRectangle.Right - xRightMost;

            // Use the side with the most space
            context.DisplayRectangle = leftSpace >= rightSpace
                ? new Rectangle(ClientLocation.X, ClientLocation.Y, leftSpace, ClientHeight)
                : new Rectangle(xRightMost, ClientLocation.Y, rightSpace, ClientHeight);

            filler.Layout(context);
        }

        // Put the original value back again
        context.DisplayRectangle = ClientRectangle;
    }
    #endregion

    #region Paint
    /// <summary>
    /// Perform rendering.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public override void Render(RenderContext context)
    {
        Rectangle clipRect = ClientRectangle;
        clipRect.Height++;

        // Limit drawing to our client area
        using var clip = new Clipping(context.Graphics, clipRect);
        base.Render(context);
    }
    #endregion

    #region Implementation
    private void SyncChildrenToContexts()
    {
        // Find any filler child
        ViewBase? filler = null;

        foreach (ViewBase child in this)
        {
            if (GetDock(child) == ViewDockStyle.Fill)
            {
                filler = child;
                break;
            }
        }

        // Remove all child elements
        Clear();

        // Make sure we have enough cached elements
        if (_contextTitlesCache.Count < ViewLayoutRibbonTabs.ContextTabSets.Count)
        {
            for (var i = _contextTitlesCache.Count; i < ViewLayoutRibbonTabs.ContextTabSets.Count; i++)
            {
                // Create a new view element and an associated button controller
                var viewContextTitle = new ViewDrawRibbonContextTitle(_ribbon, _ribbon.StateContextCheckedNormal.RibbonTab)
                {
                    MouseController = new ContextTitleController(_ribbon)
                };
                _contextTitlesCache.Add(viewContextTitle);
            }
        }

        // Add a view element per context and update with correct reference
        for (var i = 0; i < ViewLayoutRibbonTabs.ContextTabSets.Count; i++)
        {
            ViewDrawRibbonContextTitle viewContext = _contextTitlesCache[i];
            var viewController = viewContext.MouseController as ContextTitleController;
            viewContext.ContextTabSet = ViewLayoutRibbonTabs.ContextTabSets[i];
            viewController!.ContextTabSet = viewContext.ContextTabSet;
            Add(viewContext);
        }

        // Put back any filler
        if (filler != null)
        {
            Add(filler, ViewDockStyle.Fill);
        }
    }

    private Color CheckForContextColor(PaletteState state)
    {
        // We need an associated ribbon tab
        // Does the ribbon tab have a context setting?
        if (!string.IsNullOrEmpty(_ribbon.SelectedTab?.ContextName))
        {
            // Find the context definition for this context
            if (_ribbon.SelectedTab != null)
            {
                KryptonRibbonContext? ribbonContext = _ribbon.RibbonContexts[_ribbon.SelectedTab.ContextName];

                // Should always work, but you never know!
                if (ribbonContext != null)
                {
                    // Return the context specific color
                    return ribbonContext.ContextColor;
                }
            }
        }

        return Color.Empty;
    }
    #endregion
}
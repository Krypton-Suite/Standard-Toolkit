#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Docking;

/// <summary>
/// Bar-tab navigator that hosts auto-hidden pages along a docking edge.
/// </summary>
[ToolboxItem(false)]
[DesignerCategory("code")]
[DesignTimeVisible(false)]
public class KryptonAutoHiddenGroup : KryptonNavigator
{
    #region Events
    /// <summary>
    /// Raised before a page is replaced with a store placeholder so handlers can ensure its unique name remains valid for this auto-hidden location.
    /// </summary>
    public event EventHandler<UniqueNameEventArgs>? StoringPage;
    #endregion

    #region Identity
    /// <summary>
    /// Configures bar-tab-only navigator appearance, tab orientation, and dock alignment for the specified edge.
    /// </summary>
    /// <param name="edge">Docking edge that determines bar orientation and control dock style.</param>
    public KryptonAutoHiddenGroup(DockingEdge edge)
    {
        // Define appropriate appearance/behavior for an auto hidden group
        AutoSize = true;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;
        AllowTabFocus = false;
        AllowTabSelect = false;
        Bar.TabBorderStyle = TabBorderStyle.DockEqual;
        Bar.TabStyle = TabStyle.DockAutoHidden;
        Bar.BarFirstItemInset = 3;
        Bar.BarLastItemInset = 12;
        Bar.BarMinimumHeight = 0;
        Button.ButtonDisplayLogic = ButtonDisplayLogic.None;
        Button.CloseButtonDisplay = ButtonDisplay.Hide;
        NavigatorMode = NavigatorMode.BarTabOnly;

        // Edge dependent values
        switch (edge)
        {
            case DockingEdge.Left:
                Bar.BarOrientation = VisualOrientation.Right;
                Bar.ItemOrientation = ButtonOrientation.FixedRight;
                Dock = DockStyle.Top;
                break;
            case DockingEdge.Right:
                Bar.BarOrientation = VisualOrientation.Left;
                Bar.ItemOrientation = ButtonOrientation.FixedRight;
                Dock = DockStyle.Top;
                break;
            case DockingEdge.Top:
                Bar.BarOrientation = VisualOrientation.Bottom;
                Bar.ItemOrientation = ButtonOrientation.FixedTop;
                Dock = DockStyle.Left;
                break;
            case DockingEdge.Bottom:
                Bar.BarOrientation = VisualOrientation.Top;
                Bar.ItemOrientation = ButtonOrientation.FixedTop;
                Dock = DockStyle.Left;
                break;
        }
    }
    #endregion

    #region Public
    /// <summary>
    /// Replaces every non-placeholder page with a <see cref="KryptonStorePage"/> that retains the same unique name.
    /// </summary>
    public void StoreAllPages()
    {
        var uniqueNames = new List<string>();

        // Create a list of pages that have not yet store placeholders
        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (KryptonPage page in Pages)
        {
            if (page is not KryptonStorePage)
            {
                uniqueNames.Add(page.UniqueName);
            }
        }

        StorePages(uniqueNames.ToArray());
    }

    /// <summary>
    /// Replaces matching non-placeholder pages with store placeholders that keep the same unique names.
    /// </summary>
    /// <param name="uniqueNames">Unique names of pages to store; when null, no pages are changed.</param>
    public void StorePages(string[]? uniqueNames)
    {
        if (uniqueNames == null)
        {
            return;
        }

        foreach (var uniqueName in uniqueNames)
        {
            // If a matching page exists and it is not a store placeholder already
            KryptonPage? page = Pages[uniqueName];
            if ((page is not null and not KryptonStorePage))
            {
                // Notify that we are storing a page, so handlers can ensure it will be unique to the auto hidden location
                OnStoringPage(new UniqueNameEventArgs(page.UniqueName));

                // Replace the existing page with a placeholder that has the same unique name
                var placeholder = new KryptonStorePage(uniqueName, "AutoHiddenGroup");
                Pages.Insert(Pages.IndexOf(page), placeholder);
                Pages.Remove(page);
            }
        }
    }

    /// <summary>
    /// Swaps store placeholders for the supplied pages when unique names match.
    /// </summary>
    /// <param name="pages">Pages to restore into the group.</param>
    public void RestorePages(KryptonPage[] pages)
    {
        foreach (KryptonPage page in pages)
        {
            // If a matching page exists and it is not a store placeholder already
            if (!string.IsNullOrWhiteSpace(page.UniqueName))
            {
                KryptonPage? storePage = Pages[page.UniqueName];
                if (storePage is KryptonStorePage)
                {
                    // Replace the existing placeholder with the actual page
                    Pages.Insert(Pages.IndexOf(storePage), page);
                    Pages.Remove(storePage);
                }
            }
        }
    }
    #endregion

    #region Protected
    /// <summary>
    /// Raises the StoringPage event.
    /// </summary>
    /// <param name="e">An StorePageEventArgs containing the event data.</param>
    protected virtual void OnStoringPage(UniqueNameEventArgs e) => StoringPage?.Invoke(this, e);

    /// <summary>
    /// Raises the TabCountChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event args.</param>
    protected override void OnTabCountChanged(EventArgs e)
    {
        // When all the pages have been removed we kill ourself
        if (Pages.Count == 0)
        {
            Dispose();
        }
    }
    #endregion
}

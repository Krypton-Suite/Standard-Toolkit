#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Navigator;

/// <summary>
/// Implements the NavigatorMode.BarCheckButtonOnly mode.
/// </summary>
internal class ViewBuilderBarCheckButtonOnly : ViewBuilderBarItemBase
{
    #region Public
    /// <summary>
    /// Construct the view appropriate for this builder.
    /// </summary>
    /// <param name="navigator">Reference to navigator instance.</param>
    /// <param name="manager">Reference to current manager.</param>
    /// <param name="redirector">Palette redirector.</param>
    public override void Construct(KryptonNavigator navigator,
        ViewManager manager,
        PaletteRedirect redirector) =>
        // Let base class perform common operations
        base.Construct(navigator, manager, redirector);

    /// <summary>
    /// Gets a value indicating if the mode is a tab strip style mode.
    /// </summary>
    public override bool IsTabStripMode => true;

    /// <summary>
    /// User has used the keyboard to select the currently selected page.
    /// </summary>
    public override void KeyPressedPageView()
    {
        // If there is a currently selected page
        if (Navigator.SelectedPage != null)
        {
            // Grab the view for the page
            INavCheckItem? checkItem = _pageLookup![Navigator.SelectedPage];

            // If the item also has the focus
            if (checkItem.HasFocus)
            {
                // Then perform the click action for the button
                checkItem.PerformClick();
            }
        }
    }

    /// <summary>
    /// Destruct the previously created view.
    /// </summary>
    public override void Destruct() =>
        // Let base class perform common operations
        base.Destruct();
    #endregion

    #region Protected
    /// <summary>
    /// Create the view hierarchy for this view mode.
    /// </summary>
    protected override void CreateCheckItemView()
    {
        // Create the view element that lays out the check buttons
        _layoutBar = new ViewLayoutBar(Navigator.StateCommon!.Bar,
            PaletteMetricInt.CheckButtonGap,
            Navigator.Bar.ItemSizing,
            Navigator.Bar.ItemAlignment,
            Navigator.Bar.BarMultiline,
            Navigator.Bar.ItemMinimumSize,
            Navigator.Bar.ItemMaximumSize,
            Navigator.Bar.BarMinimumHeight,
            false);

        // Create the scroll spacer that restricts display
        _layoutBarViewport = new ViewLayoutViewport(Navigator.StateCommon.Bar,
            PaletteMetricPadding.BarPaddingOnly,
            PaletteMetricInt.CheckButtonGap,
            Navigator.Bar.BarOrientation,
            Navigator.Bar.ItemAlignment,
            Navigator.Bar.BarAnimation)
        {
            _layoutBar
        };

        // Create the button bar area docker
        _layoutBarDocker = new ViewLayoutDocker
        {
            { _layoutBarViewport, ViewDockStyle.Fill }
        };

        // Add a separators for insetting items
        _layoutBarSeparatorFirst = new ViewLayoutSeparator(0);
        _layoutBarSeparatorLast = new ViewLayoutSeparator(0);
        _layoutBarDocker.Add(_layoutBarSeparatorFirst, ViewDockStyle.Left);
        _layoutBarDocker.Add(_layoutBarSeparatorLast, ViewDockStyle.Right);

        // Create the docker used to layout contents of main panel and fill with group
        _layoutPanelDocker = new ViewLayoutDocker
        {
            { _layoutBarDocker, ViewDockStyle.Fill },
            { new ViewLayoutPageHide(Navigator), ViewDockStyle.Top }
        };

        // Create the top level panel and put a layout docker inside it
        _drawPanel = new ViewDrawPanel(Navigator.StateNormal.Back)
        {
            _layoutPanelDocker
        };
        _newRoot = _drawPanel;

        // Must call the base class to perform common actions
        base.CreateCheckItemView();
    }
    #endregion
}
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
/// Implements the NavigatorMode.BarRibbonTabGroup mode.
/// </summary>
internal class ViewBuilderBarRibbonTabGroup : ViewBuilderBarRibbonTabBase
{
    #region Instance Fields
    private ViewLayoutInsetOverlap _layoutOverlap;
    #endregion

    #region Public
    /// <summary>
    /// Gets a value indicating if the mode is a tab strip style mode.
    /// </summary>
    public override bool IsTabStripMode => false;

    #endregion

    #region Protected
    /// <summary>
    /// Create the view hierarchy for this view mode.
    /// </summary>
    protected override void CreateCheckItemView()
    {
        // Create a canvas for containing the selected page and put old root inside it
        _drawGroup = new ViewDrawCanvas(Navigator.StateNormal.HeaderGroup.Back,
            Navigator.StateNormal.HeaderGroup.Border,
            VisualOrientation.Top)
        {
            _oldRoot
        };

        // Create the view element that lays out the check buttons
        var layoutBar = new ViewLayoutBarForTabs(Navigator.StateCommon!.Bar,
            PaletteMetricInt.RibbonTabGap, Navigator.Bar.ItemSizing, Navigator.Bar.ItemAlignment,
            Navigator.Bar.BarMultiline, Navigator.Bar.ItemMinimumSize, Navigator.Bar.ItemMaximumSize,
            Navigator.Bar.BarMinimumHeight, Navigator.Bar.TabBorderStyle, true);
        _layoutBar = layoutBar;

        // Create the scroll spacer that restricts display
        _layoutBarViewport = new ViewLayoutViewport(Navigator.StateCommon.Bar,
            PaletteMetricPadding.BarPaddingTabs,
            PaletteMetricInt.RibbonTabGap,
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

        // Create the layout that insets the contents to allow for rounding of the group border
        _layoutOverlap = new ViewLayoutInsetOverlap(_drawGroup)
        {
            _layoutBarDocker
        };

        // Create the docker used to layout contents of main panel and fill with group
        _layoutPanelDocker = new ViewLayoutDockerOverlap(_drawGroup, _layoutOverlap, layoutBar)
        {
            { _layoutOverlap, ViewDockStyle.Top },
            { _drawGroup, ViewDockStyle.Fill }
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

    /// <summary>
    /// Update the bar orientation.
    /// </summary>
    protected override void UpdateOrientation()
    {
        // Let base class update other view elements
        base.UpdateOrientation();

        switch (Navigator.Bar.BarOrientation)
        {
            case VisualOrientation.Top:
                _layoutPanelDocker.SetDock(_layoutOverlap, ViewDockStyle.Top);
                _layoutBarViewport.CounterAlignment = RelativePositionAlign.Far;
                break;
            case VisualOrientation.Bottom:
                _layoutPanelDocker.SetDock(_layoutOverlap, ViewDockStyle.Bottom);
                _layoutBarViewport.CounterAlignment = RelativePositionAlign.Near;
                break;
            case VisualOrientation.Left:
                _layoutPanelDocker.SetDock(_layoutOverlap, ViewDockStyle.Left);
                _layoutBarViewport.CounterAlignment = RelativePositionAlign.Far;
                break;
            case VisualOrientation.Right:
                _layoutPanelDocker.SetDock(_layoutOverlap, ViewDockStyle.Right);
                _layoutBarViewport.CounterAlignment = RelativePositionAlign.Near;
                break;
        }

        // Keep the overlap layout in sync with the bar orientation
        _layoutOverlap.Orientation = Navigator.Bar.BarOrientation;
    }
    #endregion
}
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
/// Base class for implementation of 'Bar' modes.
/// </summary>
internal abstract class ViewBuilderBarItemBase : ViewBuilderItemBase
{
    #region Instance Fields
    protected ViewLayoutDocker _layoutPanelDocker;
    protected ViewLayoutSeparator _layoutBarSeparatorFirst;
    protected ViewLayoutSeparator _layoutBarSeparatorLast;
    #endregion

    #region Public
    /// <summary>
    /// Gets the appropriate popup page position for the current mode.
    /// </summary>
    /// <returns>Calculated PopupPagePosition</returns>
    public override PopupPagePosition GetPopupPagePosition() => Navigator.Bar.BarOrientation switch
    {
        VisualOrientation.Bottom => PopupPagePosition.AboveNear,
        VisualOrientation.Left => PopupPagePosition.FarTop,
        VisualOrientation.Right => PopupPagePosition.NearTop,
        _ => PopupPagePosition.BelowNear
    };
    #endregion

    #region Protected
    /// <summary>
    /// Process the change in a property that might effect the view builder.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">Property changed details.</param>
    protected override void OnViewBuilderPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case @"BarOrientation":
                UpdateOrientation();
                UpdateItemOrientation();
                _buttonManager?.RecreateButtons();
                Navigator.PerformNeedPaint(true);
                break;
            case @"BarFirstItemInset":
                UpdateFirstItemInset();
                Navigator.PerformNeedPaint(true);
                break;
            case @"BarLastItemInset":
                UpdateLastItemInset();
                Navigator.PerformNeedPaint(true);
                break;
            default:
                // We do not recognise the property, let base process it
                base.OnViewBuilderPropertyChanged(sender, e);
                break;
        }
    }

    /// <summary>
    /// Update the bar orientation.
    /// </summary>
    protected override void UpdateOrientation()
    {
        switch (Navigator.Bar.BarOrientation)
        {
            case VisualOrientation.Top:
                _layoutPanelDocker.SetDock(_layoutBarDocker, ViewDockStyle.Top);
                _layoutBarDocker.Orientation = VisualOrientation.Top;
                _layoutBarViewport.CounterAlignment = RelativePositionAlign.Near;
                break;
            case VisualOrientation.Bottom:
                _layoutPanelDocker.SetDock(_layoutBarDocker, ViewDockStyle.Bottom);
                _layoutBarDocker.Orientation = VisualOrientation.Top;
                _layoutBarViewport.CounterAlignment = RelativePositionAlign.Far;
                break;
            case VisualOrientation.Left:
                _layoutPanelDocker.SetDock(_layoutBarDocker, ViewDockStyle.Left);
                _layoutBarDocker.Orientation = VisualOrientation.Right;
                _layoutBarViewport.CounterAlignment = RelativePositionAlign.Near;
                break;
            case VisualOrientation.Right:
                _layoutPanelDocker.SetDock(_layoutBarDocker, ViewDockStyle.Right);
                _layoutBarDocker.Orientation = VisualOrientation.Right;
                _layoutBarViewport.CounterAlignment = RelativePositionAlign.Far;
                break;
        }

        _layoutBar.Orientation = Navigator.Bar.BarOrientation;
        _layoutBarViewport.Orientation = Navigator.Bar.BarOrientation;

        UpdateFirstItemInset();
        UpdateLastItemInset();
    }

    /// <summary>
    /// Update the separator used to inset the first item.
    /// </summary>
    protected void UpdateFirstItemInset()
    {
        switch (Navigator.Bar.BarOrientation)
        {
            case VisualOrientation.Top:
            case VisualOrientation.Bottom:
                _layoutBarSeparatorFirst.SeparatorSize = new Size(Navigator.Bar.BarFirstItemInset, 0);
                _layoutBarDocker.SetDock(_layoutBarSeparatorFirst, ViewDockStyle.Left);
                break;
            case VisualOrientation.Left:
            case VisualOrientation.Right:
                _layoutBarSeparatorFirst.SeparatorSize = new Size(0, Navigator.Bar.BarFirstItemInset);
                _layoutBarDocker.SetDock(_layoutBarSeparatorFirst, ViewDockStyle.Left);
                break;
        }
    }

    /// <summary>
    /// Update the separator used to inset the last item.
    /// </summary>
    protected void UpdateLastItemInset()
    {
        switch (Navigator.Bar.BarOrientation)
        {
            case VisualOrientation.Top:
            case VisualOrientation.Bottom:
                _layoutBarSeparatorLast.SeparatorSize = new Size(Navigator.Bar.BarLastItemInset, 0);
                _layoutBarDocker.SetDock(_layoutBarSeparatorLast, ViewDockStyle.Right);
                break;
            case VisualOrientation.Left:
            case VisualOrientation.Right:
                _layoutBarSeparatorLast.SeparatorSize = new Size(0, Navigator.Bar.BarLastItemInset);
                _layoutBarDocker.SetDock(_layoutBarSeparatorLast, ViewDockStyle.Right);
                break;
        }
    }
    #endregion
}
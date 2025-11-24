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
/// Base class for implementation of 'HeaderBar - CheckButton' modes.
/// </summary>
internal abstract class ViewBuilderHeaderBarCheckButtonBase : ViewBuilderItemBase
{
    #region Instance Fields
    protected ViewDrawDocker _drawPanelDocker;
    protected ViewDrawDocker _viewHeadingBar;
    #endregion

    #region Protected
    /// <summary>
    /// Construct the view appropriate for this builder.
    /// </summary>
    /// <param name="navigator">Reference to navigator instance.</param>
    /// <param name="manager">Reference to current manager.</param>
    /// <param name="redirector">Palette redirector.</param>
    public override void Construct(KryptonNavigator navigator,
        ViewManager manager,
        PaletteRedirect redirector)
    {
        // Let base class perform common operations
        base.Construct(navigator, manager, redirector);

        // Need to monitor changes in the enabled state
        Navigator.EnabledChanged += OnEnabledChanged;
    }

    /// <summary>
    /// Destruct the previously created view.
    /// </summary>
    public override void Destruct()
    {
        // Unhook from events
        Navigator.EnabledChanged -= OnEnabledChanged;

        // Let base class do standard work
        base.Destruct();
    }

    /// <summary>
    /// Create a manager for handling the button specifications.
    /// </summary>
    protected override void CreateButtonSpecManager() =>
        // Create button specification collection manager
        _buttonManager = new ButtonSpecNavManagerLayoutHeaderBar(Navigator, Redirector, Navigator.Button.ButtonSpecs, Navigator.FixedSpecs,
            new[] { _layoutBarDocker },
            new IPaletteMetric[] { Navigator.StateCommon!.Bar },
            new[] { PaletteMetricInt.BarButtonEdgeInside },
            new[] { PaletteMetricInt.BarButtonEdgeOutside },
            new[] { PaletteMetricPadding.BarButtonPadding },
            Navigator.CreateToolStripRenderer,
            NeedPaintDelegate,
            GetRemappingPaletteContent(),
            GetRemappingPaletteState())
        {

            // Hook up the tooltip manager so that tooltips can be generated
            ToolTipManager = Navigator.ToolTipManager
        };

    /// <summary>
    /// Allow operations to occur after main construct actions.
    /// </summary>
    protected override void PostCreate()
    {
        SetHeaderStyle(_viewHeadingBar, Navigator.StateCommon!.HeaderGroup.HeaderBar, Navigator.Header.HeaderStyleBar);
        _viewHeadingBar.Visible = Navigator.Header.HeaderVisibleBar;
        base.PostCreate();
    }

    /// <summary>
    /// Ensure the correct state palettes are being used.
    /// </summary>
    public override void UpdateStatePalettes()
    {
        PaletteNavigator paletteState;

        // If whole navigator is disabled then all views are disabled
        var enabled = Navigator.Enabled;

        // If there is no selected page
        if (Navigator.SelectedPage == null)
        {
            paletteState = Navigator.Enabled ? Navigator.StateNormal : Navigator.StateDisabled;
        }
        else
        {
            if (Navigator.SelectedPage.Enabled)
            {
                paletteState = Navigator.SelectedPage.StateNormal;
            }
            else
            {
                paletteState = Navigator.SelectedPage.StateDisabled;

                // If page is disabled then all of view should look disabled
                enabled = false;
            }
        }

        // Update with correct state specific palettes
        if (paletteState?.HeaderGroup != null)
        {
            _viewHeadingBar.SetPalettes(paletteState.HeaderGroup.HeaderBar.Back,
                paletteState.HeaderGroup.HeaderBar.Border);
        }

        // Update with correct enabled state
        _viewHeadingBar.Enabled = enabled;

        // Let base class perform common actions
        base.UpdateStatePalettes();
    }

    /// <summary>
    /// Process the change in a property that might effect the view builder.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">Property changed details.</param>
    protected override void OnViewBuilderPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case @"HeaderStyleBar":
                SetHeaderStyle(_viewHeadingBar, Navigator.StateCommon!.HeaderGroup.HeaderBar, Navigator.Header.HeaderStyleBar);
                UpdateStatePalettes();
                Navigator.PerformNeedPaint(true);
                break;
            case @"HeaderVisibleBar":
                _viewHeadingBar.Visible = Navigator.Header.HeaderVisibleBar;
                Navigator.PerformNeedPaint(true);
                break;
            case @"HeaderPositionBar":
                UpdateOrientation();
                UpdateItemOrientation();
                _buttonManager!.RecreateButtons();
                Navigator.PerformNeedPaint(true);
                break;
            default:
                // We do not recognise the property, let base process it
                base.OnViewBuilderPropertyChanged(sender, e);
                break;
        }
    }

    /// <summary>
    /// Gets the visual orientation of the check button.
    /// </summary>
    /// <returns>Visual orientation.</returns>
    protected override VisualOrientation ConvertButtonBorderBackOrientation() => ResolveButtonContentOrientation(Navigator.Header.HeaderPositionBar);

    /// <summary>
    /// Gets the visual orientation of the check buttons content.
    /// </summary>
    /// <returns>Visual orientation.</returns>
    protected override VisualOrientation ConvertButtonContentOrientation() => ResolveButtonContentOrientation(Navigator.Header.HeaderPositionBar);

    #endregion

    #region Implementation
    private void SetHeaderStyle(ViewDrawDocker drawDocker,
        PaletteTripleMetricRedirect palette,
        HeaderStyle style)
    {
        palette.SetStyles(style);

        if (_buttonManager != null)
        {
            switch (style)
            {
                case HeaderStyle.Primary:
                    _buttonManager.SetDockerMetrics(drawDocker, palette,
                        PaletteMetricInt.HeaderButtonEdgeInsetPrimary,
                        PaletteMetricPadding.HeaderButtonPaddingPrimary);
                    break;
                case HeaderStyle.Secondary:
                    _buttonManager.SetDockerMetrics(drawDocker, palette,
                        PaletteMetricInt.HeaderButtonEdgeInsetSecondary,
                        PaletteMetricPadding.HeaderButtonPaddingSecondary);
                    break;
                case HeaderStyle.DockActive:
                    _buttonManager.SetDockerMetrics(drawDocker, palette,
                        PaletteMetricInt.HeaderButtonEdgeInsetDockActive,
                        PaletteMetricPadding.HeaderButtonPaddingDockActive);
                    break;
                case HeaderStyle.DockInactive:
                    _buttonManager.SetDockerMetrics(drawDocker, palette,
                        PaletteMetricInt.HeaderButtonEdgeInsetDockInactive,
                        PaletteMetricPadding.HeaderButtonPaddingDockInactive);
                    break;
                case HeaderStyle.Form:
                    _buttonManager.SetDockerMetrics(drawDocker, palette,
                        PaletteMetricInt.HeaderButtonEdgeInsetForm,
                        PaletteMetricPadding.HeaderButtonPaddingForm);
                    break;
                case HeaderStyle.Calendar:
                    _buttonManager.SetDockerMetrics(drawDocker, palette,
                        PaletteMetricInt.HeaderButtonEdgeInsetCalendar,
                        PaletteMetricPadding.HeaderButtonPaddingCalendar);
                    break;
                case HeaderStyle.Custom1:
                    _buttonManager.SetDockerMetrics(drawDocker, palette,
                        PaletteMetricInt.HeaderButtonEdgeInsetCustom1,
                        PaletteMetricPadding.HeaderButtonPaddingCustom1);
                    break;
                case HeaderStyle.Custom2:
                    _buttonManager.SetDockerMetrics(drawDocker, palette,
                        PaletteMetricInt.HeaderButtonEdgeInsetCustom2,
                        PaletteMetricPadding.HeaderButtonPaddingCustom2);
                    break;
                case HeaderStyle.Custom3:
                    _buttonManager.SetDockerMetrics(drawDocker, palette,
                        PaletteMetricInt.HeaderButtonEdgeInsetCustom3,
                        PaletteMetricPadding.HeaderButtonPaddingCustom3);
                    break;
            }
        }
    }

    private IPaletteContent GetRemappingPaletteContent() => Navigator.Enabled
        ? Navigator.StateNormal.HeaderGroup.HeaderBar.Content
        : Navigator.StateDisabled.HeaderGroup.HeaderBar.Content;

    private PaletteState GetRemappingPaletteState() =>
        Navigator.Enabled ? PaletteState.Normal : PaletteState.Disabled;

    private void OnEnabledChanged(object? sender, EventArgs e)
    {
        if (_buttonManager != null)
        {
            // Cast button manager to correct type
            var headerBarBM = _buttonManager as ButtonSpecNavManagerLayoutHeaderBar;

            // Update with newly calculated values
            headerBarBM?.UpdateRemapping(GetRemappingPaletteContent(),
                GetRemappingPaletteState());
        }
    }
    #endregion
}
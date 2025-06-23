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

namespace Krypton.Toolkit;

/// <summary>
/// Create and manage the view for a ButtonSpec definition.
/// </summary>
public class ButtonSpecView : GlobalId,
    IContentValues
{
    #region Instance Fields
    private readonly PaletteRedirect _redirector;
    private readonly PaletteTripleRedirect _palette;
    private readonly EventHandler? _finishDelegate;
    private ButtonController? _controller;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ButtonSpecView class.
    /// </summary>
    /// <param name="redirector">Palette redirector.</param>
    /// <param name="paletteMetric">Source for metric values.</param>
    /// <param name="metricPadding">Padding metric for border padding.</param>
    /// <param name="manager">Reference to owning manager.</param>
    /// <param name="buttonSpec">Access</param>
    public ButtonSpecView([DisallowNull] PaletteRedirect redirector,
        IPaletteMetric paletteMetric,
        PaletteMetricPadding metricPadding,
        [DisallowNull] ButtonSpecManagerBase manager,
        [DisallowNull] ButtonSpec buttonSpec)
    {
        Debug.Assert(redirector != null);
        Debug.Assert(manager != null);
        Debug.Assert(buttonSpec != null);

        // Remember references
        _redirector = redirector!;
        Manager = manager!;
        ButtonSpec = buttonSpec!;
        _finishDelegate = OnFinishDelegate;

        // Create delegate for paint notifications
        NeedPaintHandler needPaint = OnNeedPaint;

        // Intercept calls from the button for color remapping and instead use
        // the button spec defined map and the container foreground color
        RemapPalette = Manager.CreateButtonSpecRemap(redirector!, ButtonSpec);

        // Use a redirector to get button values directly from palette
        _palette = new PaletteTripleRedirect(RemapPalette,
            PaletteBackStyle.ButtonButtonSpec,
            PaletteBorderStyle.ButtonButtonSpec,
            PaletteContentStyle.ButtonButtonSpec,
            needPaint);

        // Create the view for displaying a button
        ViewButton = new ViewDrawButton(_palette, _palette, _palette, _palette,
            paletteMetric, this, VisualOrientation.Top, false);

        // Associate the view with the source component (for design time support)
        if (ButtonSpec.AllowComponent)
        {
            ViewButton.Component = buttonSpec;
        }

        // Use a view center to place button in centre of given space
        ViewCenter = new ViewLayoutCenter(paletteMetric, metricPadding, VisualOrientation.Top)
        {
            ViewButton
        };

        // Create a controller for managing button behavior
        ButtonSpecViewControllers controllers = CreateController(ViewButton, needPaint, OnClick);
        ViewButton.MouseController = controllers.MouseController;
        ViewButton.SourceController = controllers.SourceController;
        ViewButton.KeyController = controllers.KeyController;

        // We need notifying whenever a button specification property changes
        ButtonSpec.ButtonSpecPropertyChanged += OnPropertyChanged;

        // Associate the button spec with the view that is drawing it
        ButtonSpec.SetView(ViewButton);

        // Finally update view with current button spec settings
        UpdateButtonStyle();
        UpdateVisible();
        UpdateEnabled();
        UpdateChecked();
        UpdateShowDrop();
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets access to the owning manager.
    /// </summary>
    public ButtonSpecManagerBase Manager { get; }

    /// <summary>
    /// Gets access to the monitored button spec
    /// </summary>
    public ButtonSpec ButtonSpec { get; }

    /// <summary>
    /// Gets access to the view centering that contains the button.
    /// </summary>
    public ViewLayoutCenter ViewCenter { get; }

    /// <summary>
    /// Gets access to the view centering that contains the button.
    /// </summary>
    public ViewDrawButton ViewButton { get; }

    /// <summary>
    /// Gets access to the remapping palette.
    /// </summary>
    public PaletteRedirect RemapPalette { get; }

    /// <summary>
    /// Requests a repaint and optional layout be performed.
    /// </summary>
    /// <param name="needLayout">Does the palette change require a layout.</param>
    public void PerformNeedPaint(bool needLayout) => Manager.PerformNeedPaint(this, needLayout);

    /// <summary>
    /// Update the button style to reflect new button style setting.
    /// </summary>
    public void UpdateButtonStyle() => _palette.SetStyles(ButtonSpec.GetStyle(_redirector));

    /// <summary>
    /// Update view button to reflect new button visible setting.
    /// </summary>
    public bool UpdateVisible()
    {
        // Decide if the view should be visible or not
        var prevVisible = ViewCenter.Visible;
        ViewCenter.Visible = ButtonSpec.GetVisible(_redirector);

        // Return if a change has occurred
        return prevVisible != ViewCenter.Visible;
    }

    /// <summary>
    /// Update view button to reflect new button enabled setting.
    /// </summary>
    /// <returns>True is a change in state has occurred.</returns>
    public bool UpdateEnabled()
    {
        var changed = false;

        // Remember the initial state
        ViewBase? newDependent;
        bool newEnabled;

        switch (ButtonSpec.GetEnabled(_redirector))
        {
            case ButtonEnabled.True:
                newDependent = null;
                newEnabled = true;
                break;
            case ButtonEnabled.False:
                newDependent = null;
                newEnabled = false;
                break;
            case ButtonEnabled.Container:
                newDependent = ViewCenter.Parent;
                newEnabled = true;
                break;
            default:
                // Should never happen!
                Debug.Assert(false);
                newDependent = null;
                newEnabled = false;
                DebugTools.NotImplemented(ButtonSpec.GetEnabled(_redirector).ToString());
                break;
        }

        // Only make change if the values have changed
        if (newEnabled != ViewButton.Enabled)
        {
            ViewButton.Enabled = newEnabled;
            changed = true;
        }

        if (newDependent != ViewButton.DependantEnabledState)
        {
            ViewButton.DependantEnabledState = newDependent;
            changed = true;
        }

        return changed;
    }

    /// <summary>
    /// Update view button to reflect new button checked setting.
    /// </summary>
    /// <returns>True is a change in state has occurred.</returns>
    public bool UpdateChecked()
    {
        // Remember the initial state
        bool newChecked;

        switch (ButtonSpec.GetChecked(_redirector))
        {
            case ButtonCheckState.NotCheckButton:
            case ButtonCheckState.Unchecked:
                newChecked = false;
                break;
            case ButtonCheckState.Checked:
                newChecked = true;
                break;
            default:
                // Should never happen!
                Debug.Assert(false);
                newChecked = false;
                DebugTools.NotImplemented(ButtonSpec.GetChecked(_redirector).ToString());
                break;
        }

        // Only make change if the value has changed
        if (newChecked != ViewButton.Checked)
        {
            ViewButton.Checked = newChecked;
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Update view button to reflect new button DropDown drawing/detection setting.
    /// </summary>
    public void UpdateShowDrop()
    {
        if (ButtonSpec is ButtonSpecAny buttonSpecAny)
        {
            ViewButton.DropDown = buttonSpecAny.ShowDrop;
            ViewButton.Splitter = buttonSpecAny.ShowDrop;
        }
    }

    /// <summary>
    /// Destruct the previously created views.
    /// </summary>
    public void Destruct()
    {
        // Unhook from events
        ButtonSpec.ButtonSpecPropertyChanged -= OnPropertyChanged;

        // Remove ButtonSpec/view association
        ButtonSpec.SetView(null!);

        // Remove all view element resources
        ViewCenter.Dispose();
    }
    #endregion

    #region Protected
    /// <summary>
    /// Create a button controller for the view.
    /// </summary>
    /// <param name="viewButton">View to be controlled.</param>
    /// <param name="needPaint">Paint delegate.</param>
    /// <param name="clickHandler">Reference to click handler.</param>
    /// <returns>Controller instance.</returns>
    public virtual ButtonSpecViewControllers CreateController(ViewDrawButton viewButton,
        NeedPaintHandler needPaint,
        MouseEventHandler? clickHandler)
    {
        // Create a standard button controller
        _controller = new ButtonController(viewButton, needPaint)
        {
            BecomesFixed = true
        };
        _controller.Click += clickHandler;

        // If associated with a tooltip manager then pass mouse messages onto tooltip manager
        IMouseController? mouseController = _controller;
        if (Manager.ToolTipManager != null)
        {
            mouseController = new ToolTipController(Manager.ToolTipManager, viewButton, _controller);
        }

        // Return a collection of controllers
        return new ButtonSpecViewControllers(mouseController, _controller, _controller);
    }

    /// <summary>
    /// Processes the finish of the button being pressed.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected virtual void OnFinishDelegate(object? sender, EventArgs? e) =>
        // Ask the button to remove the fixed pressed appearance
        _controller?.RemoveFixed();

    #endregion

    #region IContentValues

    /// <summary>
    /// Gets the content image.
    /// </summary>
    /// <param name="state">The state for which the image is needed.</param>
    /// <returns>Image value.</returns>
    public Image? GetImage(PaletteState state)
    {
        // Get value from button spec passing inheritance redirector
        Image? baseImage = ButtonSpec.GetImage(_redirector, state);

        float dpiFactor = _controller?.Target.FactorDpiX ?? 1f;
        return (baseImage != null)
            ? CommonHelper.ScaleImageForSizedDisplay(baseImage, baseImage.Width * dpiFactor,
                baseImage.Height * dpiFactor, true)
            : null;
    }

    /// <summary>
    /// Gets the content image transparent color.
    /// </summary>
    /// <param name="state">The state for which the image color is needed.</param>
    /// <returns>Color value.</returns>
    public Color GetImageTransparentColor(PaletteState state) =>
        // Get value from button spec passing inheritance redirector
        ButtonSpec.GetImageTransparentColor(_redirector);

    /// <summary>
    /// Gets the content short text.
    /// </summary>
    /// <returns>String value.</returns>
    public string GetShortText() =>
        // Get value from button spec passing inheritance redirector
        ButtonSpec.GetShortText(_redirector);

    /// <summary>
    /// Gets the content long text.
    /// </summary>
    /// <returns>String value.</returns>
    public string GetLongText() =>
        // Get value from button spec passing inheritance redirector
        ButtonSpec.GetLongText(_redirector);

    #endregion

    #region Implementation

    private void OnClick(object? sender, MouseEventArgs e)
    {
        var performFinishDelegate = true;
        // Never show a context menu in design mode
        if (!CommonHelper.DesignMode(Manager.Control))
        {
            // ButtonSpec's used to drop menu's if they had a context menu;
            // BUT; Disable default action, if this is a drop button and it is clicked
            bool performDefaultClick = !(ButtonSpec is ButtonSpecAny { ShowDrop: true }
                                         && ViewButton.SplitRectangle.Contains(e.Location));

            if (performDefaultClick)
            {
                // Fire the event handlers hooked into the button spec click event
                ButtonSpec.PerformClick(e);
            }

            // Does the button spec define a krypton context menu?
            if (ButtonSpec.KryptonContextMenu != null)
            {
                performFinishDelegate = false;
                // Convert from control coordinates to screen coordinates
                Rectangle rect = ViewButton.ClientRectangle;

                // If the button spec is on the chrome titlebar then find position manually
                Point pt = Manager.Control is Form form
                    ? new Point(form.Left + rect.Left, form.Top + rect.Bottom + 3)
                    : Manager.Control!.PointToScreen(new Point(rect.Left, rect.Bottom + 3));

                // Show the context menu just below the view itself
                ButtonSpec.KryptonContextMenu.Closed += OnKryptonContextMenuClosed;
                if (!ButtonSpec.KryptonContextMenu.Show(ButtonSpec, pt))
                {
                    // Menu not being shown, so clean up
                    ButtonSpec.KryptonContextMenu.Closed -= OnKryptonContextMenuClosed;

                    // Not showing a context menu, so remove the fixed view immediately
                    _finishDelegate?.Invoke(this, EventArgs.Empty);
                }
            }
            else if (ButtonSpec.ContextMenuStrip != null)
            {
                performFinishDelegate = false;
                // Set the correct renderer for the menu strip
                ButtonSpec.ContextMenuStrip.Renderer = Manager.RenderToolStrip();

                // Convert from control coordinates to screen coordinates
                Rectangle rect = ViewButton.ClientRectangle;
                Point pt = Manager.Control!.PointToScreen(new Point(rect.Left, rect.Bottom + 3));

                // Show the context menu just below the view itself
                VisualPopupManager.Singleton.ShowContextMenuStrip(ButtonSpec.ContextMenuStrip, pt,
                    _finishDelegate);
            }
        }

        if (performFinishDelegate)
        {
            // Not showing a context menu, so remove the fixed view immediately
            _finishDelegate?.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnKryptonContextMenuClosed(object? sender, ToolStripDropDownClosedEventArgs e)
    {
        // Unhook from context menu event, so that it can garbage collected in the future
        var kcm = sender as KryptonContextMenu ?? throw new ArgumentNullException(nameof(sender));
        kcm.Closed -= OnKryptonContextMenuClosed;

        // Remove the fixed button appearance
        OnFinishDelegate(sender, e);
    }

    private void OnNeedPaint(object? sender, NeedLayoutEventArgs e) => PerformNeedPaint(e.NeedLayout);

    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(Image):
            case @"Text":
            case @"ExtraText":
            case nameof(ColorMap):
                PerformNeedPaint(true);
                break;
            case @"Style":
                UpdateButtonStyle();
                PerformNeedPaint(true);
                break;
            case @"Visible":
                UpdateVisible();
                PerformNeedPaint(true);
                break;
            case @"Enabled":
                UpdateEnabled();
                PerformNeedPaint(true);
                break;
            case @"Checked":
                UpdateChecked();
                PerformNeedPaint(true);
                break;
            case @"ShowDrop":
                UpdateShowDrop();
                PerformNeedPaint(true);
                break;
        }
    }
    #endregion
}
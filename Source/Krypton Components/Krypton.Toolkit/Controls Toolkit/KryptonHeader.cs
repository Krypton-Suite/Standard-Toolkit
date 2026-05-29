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
/// Provides a description for a section of your form.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonHeader), "ToolboxBitmaps.KryptonHeader.bmp")]
[DefaultEvent(nameof(Paint))]
[DefaultProperty(nameof(Text))]
[Designer(typeof(KryptonHeaderDesigner))]
[DesignerCategory(@"code")]
[Description(@"Display a descriptive caption.")]
public class KryptonHeader : VisualSimpleBase
{
    #region Type Definitions
    /// <summary>
    /// Collection for managing ButtonSpecAny instances.
    /// </summary>
    public class HeaderButtonSpecCollection : ButtonSpecCollection<ButtonSpecAny>
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the HeaderButtonSpecCollection class.
        /// </summary>
        /// <param name="owner">Reference to owning object.</param>
        public HeaderButtonSpecCollection(KryptonHeader owner)
            : base(owner)
        {
        }
        #endregion
    }
    #endregion

    #region Instance Fields

    private HeaderStyle _style;
    private VisualOrientation _orientation;
    private readonly ViewDrawDocker _drawDocker;
    private readonly ViewDrawContent _drawContent;
    private readonly ButtonSpecManagerDraw? _buttonManager;
    private VisualPopupToolTip? _visualPopupToolTip;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonHeader class.
    /// </summary>
    public KryptonHeader()
    {
        // The header cannot take the focus
        SetStyle(ControlStyles.Selectable, false);

        // Set default values
        _style = HeaderStyle.Primary;
        _orientation = VisualOrientation.Top;
        AllowButtonSpecToolTips = false;
        AllowButtonSpecToolTipPriority = false;

        // Create storage objects
        Values = new HeaderValues(NeedPaintDelegate, GetDpiFactor);
        Values.TextChanged += OnHeaderTextChanged;
        ButtonSpecs = new HeaderButtonSpecCollection(this);

        // Create the palette storage
        StateCommon = new PaletteHeaderRedirect(Redirector, PaletteBackStyle.HeaderPrimary, PaletteBorderStyle.HeaderPrimary, PaletteContentStyle.HeaderPrimary, NeedPaintDelegate);
        StateDisabled = new PaletteTripleMetric(StateCommon, NeedPaintDelegate);
        StateNormal = new PaletteTripleMetric(StateCommon, NeedPaintDelegate);

        // Our view contains background and border with content inside
        _drawDocker = new ViewDrawDocker(StateNormal.Back, StateNormal.Border, null);
        _drawContent = new ViewDrawContent(StateNormal.Content, Values, Orientation);
        _drawDocker.Add(_drawContent, ViewDockStyle.Fill);

        // Create the view manager instance
        ViewManager = new ViewManager(this, _drawDocker);

        // Create button specification collection manager
        _buttonManager = new ButtonSpecManagerDraw(this, Redirector, ButtonSpecs, null,
            [_drawDocker],
            [StateCommon],
            [PaletteMetricInt.HeaderButtonEdgeInsetPrimary],
            [PaletteMetricPadding.HeaderButtonPaddingPrimary],
            CreateToolStripRenderer,
            NeedPaintDelegate);

        // Create the manager for handling tooltips
        ToolTipManager = new ToolTipManager(ToolTipValues);
        ToolTipManager.ShowToolTip += OnShowToolTip;
        ToolTipManager.CancelToolTip += OnCancelToolTip;
        _buttonManager.ToolTipManager = ToolTipManager;

        // We want to be auto sized by default, but not the property default!
        AutoSize = true;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;
    }

    private float GetDpiFactor() => DeviceDpi / 96F;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Remove any showing tooltip
            OnCancelToolTip(this, EventArgs.Empty);

            // Remember to pull down the manager instance
            _buttonManager?.Destruct();
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the automatic resize of the control to fit contents.
    /// </summary>
    [Browsable(true)]
    [Localizable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DefaultValue(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [RefreshProperties(RefreshProperties.All)]
    public override bool AutoSize
    {
        get => base.AutoSize;
        set => base.AutoSize = value;
    }

    /// <summary>
    /// Gets and sets the auto size mode.
    /// </summary>
    [DefaultValue(AutoSizeMode.GrowAndShrink)]
    public new AutoSizeMode AutoSizeMode
    {
        get => base.AutoSizeMode;
        set => base.AutoSizeMode = value;
    }

    /// <summary>
    /// Gets and sets the internal padding space.
    /// </summary>
    [Browsable(false)]
    [Localizable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Padding Padding
    {
        get => base.Padding;
        set => base.Padding = value;
    }

    /// <summary>
    /// Gets or sets the text associated with this control. 
    /// </summary>
    [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
    [AllowNull]
    public override string Text
    {
        get => Values.Heading;

        set => Values.Heading = value;
    }

    private bool ShouldSerializeText() =>
        // Never serialize, let the header values serialize instead
        false;

    /// <summary>
    /// Resets the Text property to its default value.
    /// </summary>
    public override void ResetText() =>
        // Map onto the heading property from the values
        Values.ResetHeading();

    /// <summary>
    /// Gets and sets the visual orientation of the control.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Visual orientation of the control.")]
    [DefaultValue(VisualOrientation.Top)]
    public virtual VisualOrientation Orientation
    {
        get => _orientation;

        set
        {
            if (_orientation != value)
            {
                _orientation = value;

                // Update the associated visual elements that are effected
                _drawDocker.Orientation = value;
                _drawContent.Orientation = value;
                _buttonManager?.RecreateButtons();

                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether mnemonics will fire button spec buttons.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Defines if mnemonic characters generate click events for button specs.")]
    [DefaultValue(true)]
    public bool UseMnemonic
    {
        get => _buttonManager!.UseMnemonic;

        set
        {
            if (_buttonManager!.UseMnemonic != value)
            {
                _buttonManager.UseMnemonic = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets the collection of button specifications.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Collection of button specifications.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public HeaderButtonSpecCollection ButtonSpecs { get; }

    /// <summary>
    /// Gets and sets a value indicating if tooltips should be Displayed for button specs.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Should tooltips be Displayed for button specs.")]
    [DefaultValue(false)]
    public bool AllowButtonSpecToolTips { get; set; }

    /// <summary>
    /// Gets and sets a value indicating if button spec tooltips should remove the parent tooltip.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Should button spec tooltips should remove the parent tooltip")]
    [DefaultValue(false)]
    public bool AllowButtonSpecToolTipPriority { get; set; }

    /// <summary>
    /// Gets and sets the header style.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Header style.")]
    [DefaultValue(HeaderStyle.Primary)]
    public HeaderStyle HeaderStyle
    {
        get => _style;

        set
        {
            if (_style != value)
            {
                _style = value;
                StateCommon.SetStyles(_style);

                // Update the drawing to reflect style change
                switch (_style)
                {
                    case HeaderStyle.Primary:
                        _buttonManager?.SetDockerMetrics(_drawDocker, StateCommon,
                            PaletteMetricInt.HeaderButtonEdgeInsetPrimary,
                            PaletteMetricPadding.HeaderButtonPaddingPrimary);
                        break;
                    case HeaderStyle.Secondary:
                        _buttonManager?.SetDockerMetrics(_drawDocker, StateCommon, PaletteMetricInt.HeaderButtonEdgeInsetSecondary, PaletteMetricPadding.HeaderButtonPaddingSecondary);
                        break;
                    case HeaderStyle.DockActive:
                        _buttonManager?.SetDockerMetrics(_drawDocker, StateCommon, PaletteMetricInt.HeaderButtonEdgeInsetDockActive, PaletteMetricPadding.HeaderButtonPaddingDockActive);
                        break;
                    case HeaderStyle.DockInactive:
                        _buttonManager?.SetDockerMetrics(_drawDocker, StateCommon, PaletteMetricInt.HeaderButtonEdgeInsetDockInactive, PaletteMetricPadding.HeaderButtonPaddingDockInactive);
                        break;
                    case HeaderStyle.Form:
                        _buttonManager?.SetDockerMetrics(_drawDocker, StateCommon, PaletteMetricInt.HeaderButtonEdgeInsetForm, PaletteMetricPadding.HeaderButtonPaddingForm);
                        break;
                    case HeaderStyle.Calendar:
                        _buttonManager?.SetDockerMetrics(_drawDocker, StateCommon, PaletteMetricInt.HeaderButtonEdgeInsetCalendar, PaletteMetricPadding.HeaderButtonPaddingCalendar);
                        break;
                    case HeaderStyle.Custom1:
                        _buttonManager?.SetDockerMetrics(_drawDocker, StateCommon, PaletteMetricInt.HeaderButtonEdgeInsetCustom1, PaletteMetricPadding.HeaderButtonPaddingCustom1);
                        break;
                    case HeaderStyle.Custom2:
                        _buttonManager?.SetDockerMetrics(_drawDocker, StateCommon, PaletteMetricInt.HeaderButtonEdgeInsetCustom2, PaletteMetricPadding.HeaderButtonPaddingCustom2);
                        break;
                    case HeaderStyle.Custom3:
                        _buttonManager?.SetDockerMetrics(_drawDocker, StateCommon, PaletteMetricInt.HeaderButtonEdgeInsetCustom3, PaletteMetricPadding.HeaderButtonPaddingCustom3);
                        break;
                    default:
                        // Should never happen!
                        Debug.Assert(false);
                        DebugTools.NotImplemented(_style.ToString());
                        break;
                }

                PerformNeedPaint(true);
            }
        }
    }

    private void ResetHeaderStyle() => HeaderStyle = HeaderStyle.Primary;

    private bool ShouldSerializeHeaderStyle() => HeaderStyle != HeaderStyle.Primary;

    /// <summary>
    /// Gets access to the header content.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Header values")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public HeaderValues Values { get; }

    private bool ShouldSerializeValues() => !Values.IsDefault;

    /// <summary>
    /// Gets access to the common header appearance that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common header appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteHeaderRedirect StateCommon { get; }

    private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

    /// <summary>
    /// Gets access to the disabled header appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled header appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTripleMetric StateDisabled { get; }

    private bool ShouldSerializeStateDisabled() => !StateDisabled.IsDefault;

    /// <summary>
    /// Gets access to the normal header appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal header appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTripleMetric StateNormal { get; }

    private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;

    /// <summary>
    /// Fix the control to a particular palette state.
    /// </summary>
    /// <param name="state">Palette state to fix.</param>
    public virtual void SetFixedState(PaletteState state) =>
        // Request fixed state from the view
        _drawDocker.FixedState = state;

    /// <summary>
    /// Gets access to the ToolTipManager used for displaying tool tips.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ToolTipManager ToolTipManager { get; }

    /// <summary>
    /// Internal design time method.
    /// </summary>
    /// <param name="pt">Mouse location.</param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public bool DesignerGetHitTest(Point pt)
    {
        // Ignore call as view builder is already destructed
        if (IsDisposed)
        {
            return false;
        }

        // Check if any of the button specs want the point
        return (_buttonManager != null) && _buttonManager.DesignerGetHitTest(pt);
    }

    /// <summary>
    /// Internal design time method.
    /// </summary>
    /// <param name="pt">Mouse location.</param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public Component? DesignerComponentFromPoint(Point pt) =>
        // Ignore call as view builder is already destructed
        IsDisposed ? null : ViewManager?.ComponentFromPoint(pt);

    // Ask the current view for a decision
    /// <summary>
    /// Internal design time method.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public void DesignerMouseLeave() =>
        // Simulate the mouse leaving the control so that the tracking
        // element that thinks it has the focus is informed it does not
        OnMouseLeave(EventArgs.Empty);

    #endregion

    #region Protected Overrides
    /// <summary>
    /// Processes a mnemonic character.
    /// </summary>
    /// <param name="charCode">The mnemonic character entered.</param>
    /// <returns>true if the mnemonic was processed; otherwise, false.</returns>
    protected override bool ProcessMnemonic(char charCode)
    {
        // If the button manager wants to process mnemonic characters and
        // this control is currently visible and enabled then...
        if (UseMnemonic && CanProcessMnemonic())
        {
            // Pass request onto the button spec manager
            if (_buttonManager!.ProcessMnemonic(charCode))
            {
                return true;
            }
        }

        // No match found, let base class do standard processing
        return base.ProcessMnemonic(charCode);
    }

    /// <summary>
    /// Raises the EnabledChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnEnabledChanged(EventArgs e)
    {
        // Push correct palettes into the view
        if (Enabled)
        {
            _drawDocker.SetPalettes(StateNormal.Back, StateNormal.Border);
            _drawContent?.SetPalette(StateNormal.Content);
        }
        else
        {
            _drawDocker.SetPalettes(StateDisabled.Back, StateDisabled.Border);
            _drawContent?.SetPalette(StateDisabled.Content);
        }

        _drawDocker.Enabled = Enabled;
        _drawContent!.Enabled = Enabled;

        // Update state to reflect change in enabled state
        _buttonManager?.RefreshButtons();

        // Change in enabled state requires a layout and repaint
        PerformNeedPaint(true);

        // Let base class fire standard event
        base.OnEnabledChanged(e);
    }

    /// <summary>
    /// Gets the default size of the control.
    /// </summary>
    protected override Size DefaultSize => new Size(240, 30);

    /// <summary>
    /// Processes a notification from palette storage of a button spec change.
    /// </summary>
    /// <param name="sender">Source of notification.</param>
    /// <param name="e">An EventArgs containing event data.</param>
    protected override void OnButtonSpecChanged(object? sender, EventArgs e)
    {
        // Recreate all the button specs with new values
        _buttonManager?.RecreateButtons();

        // Let base class perform standard processing
        base.OnButtonSpecChanged(sender, e);
    }
    #endregion

    #region Implementation
    private void OnHeaderTextChanged(object? sender, EventArgs e) => OnTextChanged(EventArgs.Empty);

    private void OnShowToolTip(object? sender, ToolTipEventArgs e)
    {
        if (!IsDisposed)
        {
            // Do not show tooltips when the form we are in does not have focus
            Form? topForm = FindForm();
            if (topForm is { ContainsFocus: false })
            {
                return;
            }

            // Never show tooltips are design time
            if (!DesignMode)
            {
                IContentValues? sourceContent = null;
                var toolTipStyle = LabelStyle.ToolTip;

                var shadow = true;

                // Find the button spec associated with the tooltip request
                ButtonSpec? buttonSpec = _buttonManager?.ButtonSpecFromView(e.Target);

                // If the tooltip is for a button spec
                if (buttonSpec != null)
                {
                    // Are we allowed to show page related tooltips
                    if (AllowButtonSpecToolTips)
                    {
                        // Create a helper object to provide tooltip values
                        var buttonSpecMapping = new ButtonSpecToContent(Redirector, buttonSpec);

                        // Is there actually anything to show for the tooltip
                        if (buttonSpecMapping.HasContent)
                        {
                            sourceContent = buttonSpecMapping;
                            toolTipStyle = buttonSpec.ToolTipStyle;
                            shadow = buttonSpec.ToolTipShadow;
                        }
                    }
                }

                if (sourceContent != null)
                {
                    // Remove any currently showing tooltip
                    _visualPopupToolTip?.Dispose();

                    if (AllowButtonSpecToolTipPriority)
                    {
                        visualBasePopupToolTip?.Dispose();
                    }

                    // Create the actual tooltip popup object
                    _visualPopupToolTip = new VisualPopupToolTip(Redirector,
                        sourceContent,
                        Renderer,
                        PaletteBackStyle.ControlToolTip,
                        PaletteBorderStyle.ControlToolTip,
                        CommonHelper.ContentStyleFromLabelStyle(toolTipStyle),
                        shadow);

                    _visualPopupToolTip.Disposed += OnVisualPopupToolTipDisposed;
                    _visualPopupToolTip?.ShowRelativeTo(e.Target, e.ControlMousePosition);
                }
            }
        }
    }

    private void OnCancelToolTip(object? sender, EventArgs e) =>
        // Remove any currently showing tooltip
        _visualPopupToolTip?.Dispose();

    private void OnVisualPopupToolTipDisposed(object? sender, EventArgs e)
    {
        // Unhook events from the specific instance that generated event
        var popupToolTip = sender as VisualPopupToolTip ?? throw new ArgumentNullException(nameof(sender));
        popupToolTip.Disposed -= OnVisualPopupToolTipDisposed;

        // Not showing a popup page any more
        _visualPopupToolTip = null;
    }
    #endregion
}
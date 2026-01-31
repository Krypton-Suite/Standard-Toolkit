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
/// Combines the benefits of the KryptonHeader and the KryptonGroup into one.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonHeaderGroup), "ToolboxBitmaps.KryptonHeaderGroup.bmp")]
[DefaultEvent(nameof(Paint))]
[DefaultProperty(nameof(ValuesPrimary))]
[Designer(typeof(KryptonHeaderGroupDesigner))]
[DesignerCategory(@"code")]
[Description(@"Group a collection of controls with a descriptive caption.")]
[Docking(DockingBehavior.Ask)]
public class KryptonHeaderGroup : VisualControlContainment
{
    #region Type Definitions
    /// <summary>
    /// Collection for managing HeaderGroupButtonSpec instances.
    /// </summary>
    public class HeaderGroupButtonSpecCollection : ButtonSpecCollection<ButtonSpecHeaderGroup>
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the HeaderGroupButtonSpecCollection class.
        /// </summary>
        /// <param name="owner">Reference to owning object.</param>
        public HeaderGroupButtonSpecCollection(KryptonHeaderGroup owner)
            : base(owner)
        {
        }
        #endregion
    }
    #endregion

    #region Instance Fields

    private HeaderStyle _style1;
    private HeaderStyle _style2;
    private VisualOrientation _position1;
    private VisualOrientation _position2;
    private HeaderGroupCollapsedTarget _collapsedTarget;
    private readonly ViewDrawDocker _drawDocker;
    private readonly ViewDrawDocker _drawHeading1;
    private readonly ViewDrawContent _drawContent1;
    private readonly ViewDrawDocker _drawHeading2;
    private readonly ViewDrawContent _drawContent2;
    private readonly ViewLayoutFill _layoutFill;
    private readonly ButtonSpecManagerDraw _buttonManager;
    private VisualPopupToolTip? _visualPopupToolTip;
    private ScreenObscurer? _obscurer;
    private readonly EventHandler? _removeObscurer;
    private bool _forcedLayout;
    private bool _visiblePrimary;
    private bool _visibleSecondary;
    private bool _collapsed;
    private readonly bool _ignoreLayout;
    private bool _layingOut;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when the collapsed property changes.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs when the value of the Collapsed property is changed.")]
    public event EventHandler? CollapsedChanged;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonHeaderGroup class.
    /// </summary>
    public KryptonHeaderGroup()
    {
        // Set default values
        _style1 = HeaderStyle.Primary;
        _style2 = HeaderStyle.Secondary;
        _position1 = VisualOrientation.Top;
        _position2 = VisualOrientation.Bottom;
        _collapsedTarget = HeaderGroupCollapsedTarget.CollapsedToPrimary;
        _collapsed = false;
        AutoCollapseArrow = true;
        AllowButtonSpecToolTips = false;
        AllowButtonSpecToolTipPriority = false;
        _visiblePrimary = true;
        _visibleSecondary = true;

        // Create storage objects
        ValuesPrimary = new HeaderGroupValuesPrimary(NeedPaintDelegate, GetDpiFactor);
        ValuesPrimary.TextChanged += OnHeaderGroupTextChanged;
        ValuesSecondary = new HeaderGroupValuesSecondary(NeedPaintDelegate, GetDpiFactor);
        ButtonSpecs = new HeaderGroupButtonSpecCollection(this);

        // We need to monitor button spec click events
        ButtonSpecs.Inserted += OnButtonSpecInserted;
        ButtonSpecs.Removed += OnButtonSpecRemoved;

        // Create the palette storage
        StateCommon = new PaletteHeaderGroupRedirect(Redirector, NeedPaintDelegate);
        StateDisabled = new PaletteHeaderGroup(StateCommon, StateCommon.HeaderPrimary, StateCommon.HeaderSecondary, NeedPaintDelegate);
        StateNormal = new PaletteHeaderGroup(StateCommon, StateCommon.HeaderPrimary, StateCommon.HeaderSecondary, NeedPaintDelegate);

        // Create the internal panel used for containing content
        Panel = new KryptonGroupPanel(this, StateCommon, StateDisabled, StateNormal, OnGroupPanelPaint!)
        {
            // Make sure the panel back style always mimics our back style
            PanelBackStyle = PaletteBackStyle.PanelClient
        };

        // Create view for header 1
        _drawHeading1 = new ViewDrawDocker(StateNormal.HeaderPrimary.Back,
            StateNormal.HeaderPrimary.Border,
            StateNormal.HeaderPrimary,
            PaletteMetricBool.None,
            PaletteMetricPadding.HeaderGroupPaddingPrimary,
            VisualOrientation.Top);

        _drawContent1 = new ViewDrawContent(StateNormal.HeaderPrimary.Content, ValuesPrimary, VisualOrientation.Top);
        _drawHeading1.Add(_drawContent1, ViewDockStyle.Fill);

        // Create view for header 2
        _drawHeading2 = new ViewDrawDocker(StateNormal.HeaderSecondary.Back,
            StateNormal.HeaderSecondary.Border,
            StateNormal.HeaderSecondary,
            PaletteMetricBool.None,
            PaletteMetricPadding.HeaderGroupPaddingSecondary,
            VisualOrientation.Top);

        _drawContent2 = new ViewDrawContent(StateNormal.HeaderSecondary.Content, ValuesSecondary, VisualOrientation.Top);
        _drawHeading2.Add(_drawContent2, ViewDockStyle.Fill);

        // Create view for the control border and background
        _drawDocker = new ViewDrawDocker(StateNormal.Back, StateNormal.Border, StateNormal,
            PaletteMetricBool.HeaderGroupOverlay)
        {
            // Layout child view on top of the border space
            IgnoreBorderSpace = true,

            // Prevent adjacent headers from having two borders
            RemoveChildBorders = true
        };

        // Create the element that fills the remainder space and remembers fill rectangle
        _layoutFill = new ViewLayoutFill(Panel);

        // Add headers into the docker with initial dock edges defined
        _drawDocker.Add(_drawHeading2, ViewDockStyle.Bottom);
        _drawDocker.Add(_drawHeading1, ViewDockStyle.Top);
        _drawDocker.Add(_layoutFill, ViewDockStyle.Fill);

        // Create the view manager instance
        ViewManager = new ViewManager(this, _drawDocker);

        // Create button specification collection manager
        _buttonManager = new ButtonSpecManagerDraw(this, Redirector, ButtonSpecs, null,
            [_drawHeading1, _drawHeading2],
            [StateCommon.HeaderPrimary, StateCommon.HeaderSecondary],
            [PaletteMetricInt.HeaderButtonEdgeInsetPrimary, PaletteMetricInt.HeaderButtonEdgeInsetSecondary],
            [PaletteMetricPadding.HeaderButtonPaddingPrimary, PaletteMetricPadding.HeaderButtonPaddingSecondary],
            CreateToolStripRenderer,
            NeedPaintDelegate);

        // Create the manager for handling tooltips
        ToolTipManager = new ToolTipManager(ToolTipValues);
        ToolTipManager.ShowToolTip += OnShowToolTip;
        ToolTipManager.CancelToolTip += OnCancelToolTip;
        _buttonManager.ToolTipManager = ToolTipManager;

        // We want to default to shrinking and growing (base class defaults to GrowOnly)
        AutoSizeMode = AutoSizeMode.GrowAndShrink;

        // Create the delegate used when we need to ensure obscurer is removed
        _removeObscurer = OnRemoveObscurer;

        // Need to prevent the AddInternal from causing a layout, otherwise the
        // layout will probably try to measure text which causes the handle for the
        // control to be created which means the handle is created at the wrong time
        // and so child controls are not added properly in the future! (for the TabControl
        // at the very least).
        _ignoreLayout = true;

        // Add panel to the controls collection
        ((KryptonReadOnlyControls)Controls).AddInternal(Panel);

        _ignoreLayout = false;
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
            // Remove any cached obscurer
            if (_obscurer != null!)
            {
                try
                {
                    _obscurer.Uncover();
                    _obscurer.Dispose();
                    _obscurer = null!;
                }
                catch
                {
                    // ignored
                }
            }

            // Remove ant showing tooltip
            OnCancelToolTip(this, EventArgs.Empty);

            // Remember to pull down the manager instance
            _buttonManager.Destruct();
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the name of the control.
    /// </summary>
    [Browsable(false)]
    [AllowNull]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new string Name
    {
        get => base.Name;

        set
        {
            base.Name = value;
            Panel.Name = $"{value}.Panel";
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the control is automatically resized to display its entire contents.
    /// </summary>
    [Browsable(true)]
    [DefaultValue(false)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public override bool AutoSize
    {
        get => base.AutoSize;
        set => base.AutoSize = value;
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
    /// Gets and sets the auto size mode.
    /// </summary>
    [Category(@"Layout")]
    [Description(@"Specifies if the control grows and shrinks to fit the contents exactly.")]
    [DefaultValue(AutoSizeMode.GrowAndShrink)]
    public AutoSizeMode AutoSizeMode
    {
        // ReSharper disable RedundantBaseQualifier
        get => base.GetAutoSizeMode();
        // ReSharper restore RedundantBaseQualifier

        set
        {
            // ReSharper disable RedundantBaseQualifier
            if (value != base.GetAutoSizeMode())
            {
                base.SetAutoSizeMode(value);
                // ReSharper restore RedundantBaseQualifier

                // Only perform an immediate layout if
                // currently performing auto size operations
                if (AutoSize)
                {
                    PerformNeedPaint(true);
                }
            }
        }
    }

    /// <summary>
    /// Gets or sets the text associated with this control. 
    /// </summary>
    [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
    [AllowNull]
    public override string Text
    {
        get => ValuesPrimary.Heading;

        set => ValuesPrimary.Heading = value;
    }

    // Never serialize, let the header values serialize instead
    private bool ShouldSerializeText() => false;

    /// <summary>
    /// Resets the Text property to its default value.
    /// </summary>
    // Map onto the heading property from the values
    public override void ResetText() => ValuesPrimary.ResetHeading();

    /// <summary>
    /// Gets or sets a value indicating whether mnemonics will fire button spec buttons.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Defines if mnemonic characters generate click events for button specs.")]
    [DefaultValue(true)]
    public bool UseMnemonic
    {
        get => _buttonManager.UseMnemonic;

        set
        {
            if (_buttonManager.UseMnemonic != value)
            {
                _buttonManager.UseMnemonic = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets access to the internal panel that contains group content.
    /// </summary>
    [Localizable(false)]
    [Category(@"Appearance")]
    [Description(@"The internal panel that contains group content.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonGroupPanel Panel { get; }

    /// <summary>
    /// Gets or sets a value indicating if collapsed mode is auto toggled by arrow button specs.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Clicking an arrow button spec should toggle collapse state.")]
    [DefaultValue(true)]
    public bool AutoCollapseArrow { get; set; }

    /// <summary>
    /// Gets or sets a value indicating if the appearance is collapsed.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Specifies if the appearance is collapsed.")]
    [DefaultValue(false)]
    public bool Collapsed
    {
        get => _collapsed;

        set
        {
            if (_collapsed != value)
            {
                SuspendLayout();

                try
                {
                    _collapsed = value;
                    _layoutFill.Visible = !value;
                    Panel.Visible = !value;
                    ReapplyVisible();
                    OnCollapsedChanged(EventArgs.Empty);
                    PerformNeedPaint(false);
                }
                finally
                {
                    // Make sure we match the suspend layout
                    ResumeLayout(true);
                }
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating how to collapsed the appearance when entering collapse mode.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Specifies how to collapsed the appearance when entering collapse mode.")]
    public HeaderGroupCollapsedTarget CollapseTarget
    {
        get => _collapsedTarget;

        set
        {
            if (_collapsedTarget != value)
            {
                _collapsedTarget = value;
                ReapplyVisible();
                PerformNeedPaint(true);
            }
        }
    }

    private void ResetCollapseTarget() => CollapseTarget = HeaderGroupCollapsedTarget.CollapsedToPrimary;

    private bool ShouldSerializeCollapseTarget() => CollapseTarget != HeaderGroupCollapsedTarget.CollapsedToPrimary;


    /// <summary>
    /// Gets the collection of button specifications.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Collection of button specifications.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public HeaderGroupButtonSpecCollection ButtonSpecs { get; }

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
    /// Gets and sets the border style.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Border style.")]
    public PaletteBorderStyle GroupBorderStyle
    {
        get => StateCommon.BorderStyle;

        set
        {
            if (StateCommon.BorderStyle != value)
            {
                StateCommon.BorderStyle = value;
                PerformNeedPaint(true);
            }
        }
    }

    private void ResetGroupBorderStyle() => GroupBorderStyle = PaletteBorderStyle.ControlClient;

    private bool ShouldSerializeGroupBorderStyle() => GroupBorderStyle != PaletteBorderStyle.ControlClient;

    /// <summary>
    /// Gets and sets the background style.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Background style.")]
    public PaletteBackStyle GroupBackStyle
    {
        get => StateCommon.BackStyle;

        set
        {
            if (StateCommon.BackStyle != value)
            {
                StateCommon.BackStyle = value;
                Panel.PanelBackStyle = value;
                PerformNeedPaint(true);
            }
        }
    }

    private void ResetGroupBackStyle() => GroupBackStyle = PaletteBackStyle.PanelClient;
    private bool ShouldSerializeGroupBackStyle() => GroupBackStyle != PaletteBackStyle.PanelClient;

    /// <summary>
    /// Gets and sets the primary header style.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Primary header style.")]
    public HeaderStyle HeaderStylePrimary
    {
        get => _style1;

        set
        {
            if (_style1 != value)
            {
                _style1 = value;
                SetHeaderStyle(_drawHeading1, StateCommon.HeaderPrimary, _style1);
                PerformNeedPaint(true);
            }
        }
    }

    private void ResetHeaderStylePrimary() => HeaderStylePrimary = HeaderStyle.Primary;

    private bool ShouldSerializeHeaderStylePrimary() => HeaderStylePrimary != HeaderStyle.Primary;

    /// <summary>
    /// Gets and sets the secondary header style.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Secondary header style.")]
    public HeaderStyle HeaderStyleSecondary
    {
        get => _style2;

        set
        {
            if (_style2 != value)
            {
                _style2 = value;
                SetHeaderStyle(_drawHeading2, StateCommon.HeaderSecondary, _style2);
                PerformNeedPaint(true);
            }
        }
    }

    private void ResetHeaderStyleSecondary() => HeaderStyleSecondary = HeaderStyle.Secondary;

    private bool ShouldSerializeHeaderStyleSecondary() => HeaderStyleSecondary != HeaderStyle.Secondary;

    /// <summary>
    /// Gets and sets the position of the primary header.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Edge position of the primary header.")]
    [DefaultValue(VisualOrientation.Top)]
    public VisualOrientation HeaderPositionPrimary
    {
        get => _position1;

        set
        {
            if (_position1 != value)
            {
                _position1 = value;
                SetHeaderPosition(_drawHeading1, _drawContent1, _position1);
                _buttonManager.RecreateButtons();
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets and sets the position of the secondary header.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Edge position of the secondary header.")]
    [DefaultValue(VisualOrientation.Bottom)]
    public VisualOrientation HeaderPositionSecondary
    {
        get => _position2;

        set
        {
            if (_position2 != value)
            {
                _position2 = value;
                SetHeaderPosition(_drawHeading2, _drawContent2, _position2);
                _buttonManager.RecreateButtons();
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets and sets the primary header visibility.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Primary header visibility.")]
    [DefaultValue(true)]
    public bool HeaderVisiblePrimary
    {
        get => _visiblePrimary;

        set
        {
            if (_visiblePrimary != value)
            {
                _visiblePrimary = value;
                ReapplyVisible();
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets and sets the secondary header visibility.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Secondary header visibility.")]
    [DefaultValue(true)]
    public bool HeaderVisibleSecondary
    {
        get => _visibleSecondary;

        set
        {
            if (_visibleSecondary != value)
            {
                _visibleSecondary = value;
                ReapplyVisible();
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets access to the common header group appearance that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common header group appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteHeaderGroupRedirect StateCommon { get; }

    private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

    /// <summary>
    /// Gets access to the disabled header group appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled header group appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteHeaderGroup StateDisabled { get; }

    private bool ShouldSerializeStateDisabled() => !StateDisabled.IsDefault;

    /// <summary>
    /// Gets access to the normal header group appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal header group appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteHeaderGroup StateNormal { get; }

    private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;

    /// <summary>
    /// Gets access to the primary header content.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Primary header values")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public HeaderGroupValuesPrimary ValuesPrimary { get; }

    private bool ShouldSerializeValuesPrimary() => !ValuesPrimary.IsDefault;

    /// <summary>
    /// Gets access to the secondary header content.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Secondary header values")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public HeaderGroupValuesSecondary ValuesSecondary { get; }

    private bool ShouldSerializeValuesSecondary() => !ValuesSecondary.IsDefault;

    /// <summary>
    /// Get the preferred size of the control based on a proposed size.
    /// </summary>
    /// <param name="proposedSize">Starting size proposed by the caller.</param>
    /// <returns>Calculated preferred size.</returns>
    public override Size GetPreferredSize(Size proposedSize)
    {
        // Do we have a manager to ask for a preferred size?
        if (ViewManager != null)
        {
            // Ask the view to perform a layout
            Size retSize = ViewManager.GetPreferredSize(Renderer, proposedSize);

            // Apply the maximum sizing
            if (MaximumSize.Width > 0)
            {
                retSize.Width = Math.Min(MaximumSize.Width, retSize.Width);
            }

            if (MaximumSize.Height > 0)
            {
                retSize.Height = Math.Min(MaximumSize.Height, retSize.Height);
            }

            // Apply the minimum sizing
            if (MinimumSize.Width > 0)
            {
                retSize.Width = Math.Max(MinimumSize.Width, retSize.Width);
            }

            if (MinimumSize.Height > 0)
            {
                retSize.Height = Math.Max(MinimumSize.Height, retSize.Height);
            }

            return retSize;
        }
        else
        {
            // Fall back on default control processing
            return base.GetPreferredSize(proposedSize);
        }
    }

    /// <summary>
    /// Gets the rectangle that represents the display area of the control.
    /// </summary>
    public override Rectangle DisplayRectangle
    {
        get
        {
            // Ensure that the layout is calculated in order to know the remaining display space
            ForceViewLayout();

            // The inside panel is the client rectangle size
            return new Rectangle(Panel.Location, Panel.Size);
        }
    }

    /// <summary>
    /// Fix the control to a particular palette state.
    /// </summary>
    /// <param name="state">Palette state to fix.</param>
    public virtual void SetFixedState(PaletteState state)
    {
        // Request fixed state from the view
        _drawDocker.FixedState = state;
        Panel.SetFixedState(state);
    }

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
        return _buttonManager.DesignerGetHitTest(pt);
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

    #region Protected
    /// <summary>
    /// Force the layout logic to size and position the panels.
    /// </summary>
    protected void ForceControlLayout()
    {
        // Usually the layout will not occur if currently initializing, but
        // we need to force the layout processing because otherwise the size
        // of the panel controls will not have been calculated when controls
        // are added to the panels. That would then cause problems with
        // anchor controls as they would then resize incorrectly.
        if (!IsInitialized)
        {
            _forcedLayout = true;
            OnLayout(new LayoutEventArgs(null, null));
            _forcedLayout = false;
        }
    }
    #endregion

    #region Protected Virtual
    /// <summary>
    /// Raises the CollapsedChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnCollapsedChanged(EventArgs e) => CollapsedChanged?.Invoke(this, e);
    #endregion

    #region Protected Overrides
    /// <summary>
    /// Creates a new instance of the control collection for the KryptonHeaderGroup.
    /// </summary>
    /// <returns>A new instance of Control.ControlCollection assigned to the control.</returns>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected override ControlCollection CreateControlsInstance() => new KryptonReadOnlyControls(this);

    /// <summary>
    /// Raises the HandleCreated event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected override void OnHandleCreated(EventArgs e)
    {
        // Let base class do standard stuff
        base.OnHandleCreated(e);

        // We need a layout to occur before any painting
        InvokeLayout();
    }

    /// <summary>
    /// Raises the Initialized event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnInitialized(EventArgs e)
    {
        // Let base class raise events
        base.OnInitialized(e);

        // Force a layout now that initialization is complete
        OnLayout(new LayoutEventArgs(null, null));
    }

    /// <summary>
    /// Raises the Layout event.
    /// </summary>
    /// <param name="levent">A LayoutEventArgs containing the event data.</param>
    protected override void OnLayout(LayoutEventArgs levent)
    {
        // Remember if we are inside a layout cycle
        _layingOut = true;

        // Must ignore any layout caused by the AddInternal in the constructor,
        // otherwise layout processing causes the controls handle to be constructed
        // (because it tries to measure text for the headers) and so child controls
        // are not added correctly when inside a TabControl. Bonkers but true.
        if (!_ignoreLayout)
        {
            // Let base class calculate fill rectangle
            base.OnLayout(levent);

            // Only use layout logic if control is fully initialized or if being forced
            // to allow a relayout or if in design mode.
            if (IsInitialized || _forcedLayout || DesignMode )
            {
                Rectangle fillRect = _layoutFill.FillRect;

                Panel.SetBounds(fillRect.X,
                    fillRect.Y,
                    fillRect.Width,
                    fillRect.Height);
            }
        }

        _layingOut = false;
    }

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
            if (_buttonManager.ProcessMnemonic(charCode))
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
            _drawHeading1.SetPalettes(StateNormal.HeaderPrimary.Back,
                StateNormal.HeaderPrimary.Border,
                StateNormal.HeaderPrimary);

            _drawContent1.SetPalette(StateNormal.HeaderPrimary.Content);

            _drawHeading2.SetPalettes(StateNormal.HeaderSecondary.Back,
                StateNormal.HeaderSecondary.Border,
                StateNormal.HeaderSecondary);

            _drawContent2.SetPalette(StateNormal.HeaderSecondary.Content);

            _drawDocker.SetPalettes(StateNormal.Back,
                StateNormal.Border,
                StateNormal);
        }
        else
        {
            _drawHeading1.SetPalettes(StateDisabled.HeaderPrimary.Back,
                StateDisabled.HeaderPrimary.Border,
                StateDisabled.HeaderPrimary);

            _drawContent1.SetPalette(StateDisabled.HeaderPrimary.Content);

            _drawHeading2.SetPalettes(StateDisabled.HeaderSecondary.Back,
                StateDisabled.HeaderSecondary.Border,
                StateDisabled.HeaderSecondary);

            _drawContent2.SetPalette(StateDisabled.HeaderSecondary.Content);

            _drawDocker.SetPalettes(StateDisabled.Back,
                StateNormal.Border,
                StateNormal);
        }

        _drawHeading1.Enabled = Enabled;
        _drawContent1.Enabled = Enabled;
        _drawHeading2.Enabled = Enabled;
        _drawContent2.Enabled = Enabled;
        _drawDocker.Enabled = Enabled;

        // Update state to reflect change in enabled state
        _buttonManager.RefreshButtons();

        // Change in enabled state requires a layout and repaint
        PerformNeedPaint(true);

        // Let base class fire standard event
        base.OnEnabledChanged(e);
    }

    /// <summary>
    /// Raises the Resize event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnResize(EventArgs e)
    {
        // Let base class raise events
        base.OnResize(e);

        // We must have a layout calculation
        ForceControlLayout();
    }

    /// <summary>
    /// Processes a notification from palette storage of a paint and optional layout required.
    /// </summary>
    /// <param name="sender">Source of notification.</param>
    /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
    protected override void OnNeedPaint(object? sender, NeedLayoutEventArgs e)
    {
        if (IsInitialized || !e.NeedLayout)
        {
            // As the contained group panel is using our palette storage
            // we also need to pass on any paint request to it as well
            Panel.PerformNeedPaint(e.NeedLayout);
        }
        else
        {
            ForceControlLayout();
        }

        base.OnNeedPaint(sender, e);
    }

    /// <summary>
    /// Process Windows-based messages.
    /// </summary>
    /// <param name="m">A Windows-based message.</param>
    protected override void WndProc(ref Message m)
    {
        switch (m.Msg)
        {
            case PI.WM_.WINDOWPOSCHANGING:
            {
                // First time around we need to create the obscurer
                _obscurer ??= new ScreenObscurer();

                // Obscure the display area of the control
                if (!IsDisposed && IsHandleCreated && !DesignMode)
                {
                    _obscurer.Cover(this);
                }

                // Just in case the WM_WINDOWPOSCHANGED does not occur we can 
                // ensure the obscurer is removed using this async delegate call
                BeginInvoke(_removeObscurer!);
                break;
            }
            case PI.WM_.WINDOWPOSCHANGED:
                // Uncover from the covered area
                _obscurer?.Uncover();
                break;
        }

        base.WndProc(ref m);
    }

    /// <summary>
    /// Processes a notification from palette storage of a button spec change.
    /// </summary>
    /// <param name="sender">Source of notification.</param>
    /// <param name="e">An EventArgs containing event data.</param>
    protected override void OnButtonSpecChanged(object? sender, EventArgs e)
    {
        // Recreate all the button specs with new values
        _buttonManager.RecreateButtons();

        // Let base class perform standard processing
        base.OnButtonSpecChanged(sender, e);
    }
    #endregion

    #region Implementation
    private void OnRemoveObscurer(object? sender, EventArgs e) => _obscurer?.Uncover();

    private void OnHeaderGroupTextChanged(object? sender, EventArgs e) => OnTextChanged(EventArgs.Empty);

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
                ButtonSpec? buttonSpec = _buttonManager.ButtonSpecFromView(e.Target);

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
                    _visualPopupToolTip.ShowRelativeTo(e.Target, e.ControlMousePosition);
                }
            }
        }
    }

    // Remove any currently showing tooltip
    private void OnCancelToolTip(object? sender, EventArgs e) => _visualPopupToolTip?.Dispose();

    private void OnVisualPopupToolTipDisposed(object? sender, EventArgs e)
    {
        // Unhook events from the specific instance that generated event
        var popupToolTip = sender as VisualPopupToolTip ?? throw new ArgumentNullException(nameof(sender));
        popupToolTip.Disposed -= OnVisualPopupToolTipDisposed;

        // Not showing a popup page anymore
        _visualPopupToolTip = null;
    }

    // Monitor the button spec being clicked
    private void OnButtonSpecInserted(object? sender, ButtonSpecEventArgs e) => e.ButtonSpec.Click += OnButtonSpecClicked;

    // Unhook from monitoring the button spec
    private void OnButtonSpecRemoved(object? sender, ButtonSpecEventArgs e) => e.ButtonSpec.Click -= OnButtonSpecClicked;

    private void OnButtonSpecClicked(object? sender, EventArgs e)
    {
        // Do we need to automatically switch collapsed modes?
        if (AutoCollapseArrow && sender is ButtonSpecHeaderGroup buttonSpec)
        {
            // Action depends on the arrow
            switch (buttonSpec.Type)
            {
                case PaletteButtonSpecStyle.ArrowLeft:
                    buttonSpec.Type = PaletteButtonSpecStyle.ArrowRight;
                    Collapsed = !Collapsed;
                    break;
                case PaletteButtonSpecStyle.ArrowRight:
                    buttonSpec.Type = PaletteButtonSpecStyle.ArrowLeft;
                    Collapsed = !Collapsed;
                    break;
                case PaletteButtonSpecStyle.ArrowUp:
                    buttonSpec.Type = PaletteButtonSpecStyle.ArrowDown;
                    Collapsed = !Collapsed;
                    break;
                case PaletteButtonSpecStyle.ArrowDown:
                    buttonSpec.Type = PaletteButtonSpecStyle.ArrowUp;
                    Collapsed = !Collapsed;
                    break;
            }
        }
    }

    private void OnGroupPanelPaint(object sender, NeedLayoutEventArgs e)
    {
        // If the child panel is layout out but not because we are, then it must be
        // laying out because a child has changed visibility/size/etc. If we are an
        // AutoSize control then we need to ensure we layout as well to change size.
        if (e.NeedLayout && !_layingOut && AutoSize)
        {
            PerformNeedPaint(true);
        }
    }

    private void SetHeaderPosition(ViewDrawCanvas canvas,
        ViewDrawContent content,
        VisualOrientation position)
    {
        switch (position)
        {
            case VisualOrientation.Top:
                _drawDocker.SetDock(canvas, ViewDockStyle.Top);
                canvas.Orientation = VisualOrientation.Top;
                content.Orientation = VisualOrientation.Top;
                break;
            case VisualOrientation.Bottom:
                _drawDocker.SetDock(canvas, ViewDockStyle.Bottom);
                canvas.Orientation = VisualOrientation.Top;
                content.Orientation = VisualOrientation.Top;
                break;
            case VisualOrientation.Left:
                _drawDocker.SetDock(canvas, ViewDockStyle.Left);
                canvas.Orientation = VisualOrientation.Left;
                content.Orientation = VisualOrientation.Left;
                break;
            case VisualOrientation.Right:
                _drawDocker.SetDock(canvas, ViewDockStyle.Right);
                canvas.Orientation = VisualOrientation.Right;
                content.Orientation = VisualOrientation.Right;
                break;
        }
    }

    private void SetHeaderStyle(ViewDrawDocker drawDocker,
        PaletteTripleMetricRedirect palette,
        HeaderStyle style)
    {
        palette.SetStyles(style);

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
            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(style.ToString());
                break;
        }
    }

    private void ReapplyVisible()
    {
        // Default to the appropriate header property
        var primaryVisible = _visiblePrimary;
        var secondaryVisible = _visibleSecondary;

        // If currently in the collapsed state
        if (Collapsed)
        {
            // Override visibility with target state instead
            switch (CollapseTarget)
            {
                case HeaderGroupCollapsedTarget.CollapsedToPrimary:
                    primaryVisible = true;
                    secondaryVisible = false;
                    break;
                case HeaderGroupCollapsedTarget.CollapsedToSecondary:
                    primaryVisible = false;
                    secondaryVisible = true;
                    break;
                case HeaderGroupCollapsedTarget.CollapsedToBoth:
                    primaryVisible = true;
                    secondaryVisible = true;
                    break;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    DebugTools.NotImplemented(CollapseTarget.ToString());
                    break;
            }
        }

        _drawHeading1.Visible = primaryVisible;
        _drawHeading2.Visible = secondaryVisible;
    }
    #endregion

    #region Implementation Static
    private static int PaddingEdgeNeeded(int padding, int client)
    {
        // If the padding value is less than that allocated to children
        if (padding < client)
        {
            // Then no additional padding is needed, because the children
            // overlap all of the padding edge anyway
            return 0;
        }
        else
        {
            // Then we need only the extra space between the client and the
            // padding edge, as the rest is overlapped by the children
            return padding - client;
        }
    }
    #endregion
}
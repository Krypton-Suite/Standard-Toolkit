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
/// Group related controls together with Krypton Toolkit styling.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonGroup), "ToolboxBitmaps.KryptonGroup.bmp")]
[DefaultEvent(nameof(Paint))]
[DefaultProperty(nameof(GroupBackStyle))]
[Designer(typeof(KryptonGroupDesigner))]
[DesignerCategory(@"code")]
[Description(@"Enables you to group collections of controls.")]
[Docking(DockingBehavior.Ask)]
public class KryptonGroup : VisualControlContainment
{
    #region Instance Fields

    private readonly ViewDrawDocker _drawDocker;
    private readonly ViewLayoutFill _layoutFill;
    private bool _forcedLayout;
    private bool _layingOut;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonGroup class.
    /// </summary>
    public KryptonGroup()
    {
        SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer, true);

        // Create the palette storage
        StateCommon = new PaletteDoubleRedirect(Redirector, PaletteBackStyle.PanelClient, PaletteBorderStyle.ControlClient, NeedPaintDelegate);
        StateDisabled = new PaletteDouble(StateCommon, NeedPaintDelegate);
        StateNormal = new PaletteDouble(StateCommon, NeedPaintDelegate);

        // Create the internal panel used for containing content
        Panel = new KryptonGroupPanel(this, StateCommon, StateDisabled, StateNormal, OnGroupPanelPaint)
        {

            // Make sure the panel back style always mimics our back style
            PanelBackStyle = PaletteBackStyle.PanelClient
        };

        // Create the element that fills the remainder space and remembers fill rectangle
        _layoutFill = new ViewLayoutFill(Panel);

        // Create view for the control border and background
        _drawDocker = new ViewDrawDocker(StateNormal.Back, StateNormal.Border)
        {
            { _layoutFill, ViewDockStyle.Fill }
        };

        // Create the view manager instance
        ViewManager = new ViewManager(this, _drawDocker);

        // We want to default to shrinking and growing (base class defaults to GrowOnly)
        AutoSizeMode = AutoSizeMode.GrowAndShrink;

        // Add panel to the controls collection
        ((KryptonReadOnlyControls)Controls).AddInternal(Panel);
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the name of the control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [AllowNull]
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
    /// Gets access to the internal panel that contains group content.
    /// </summary>
    [Localizable(false)]
    [Category(@"Appearance")]
    [Description(@"The internal panel that contains group content.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonGroupPanel Panel { get; }

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

    private bool ShouldSerializeGroupBorderStyle() => GroupBorderStyle != PaletteBorderStyle.ControlClient;

    private void ResetGroupBorderStyle() => GroupBorderStyle = PaletteBorderStyle.ControlClient;

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

    private bool ShouldSerializeGroupBackStyle() => GroupBackStyle != PaletteBackStyle.PanelClient;
    private void ResetGroupBackStyle() => GroupBackStyle = PaletteBackStyle.PanelClient;

    /// <summary>
    /// Gets access to the common group appearance entries that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common group appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteDoubleRedirect StateCommon { get; }

    private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

    /// <summary>
    /// Gets access to the disabled group appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled group appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteDouble? StateDisabled { get; }

    private bool ShouldSerializeStateDisabled() => !StateDisabled!.IsDefault;

    /// <summary>
    /// Gets access to the normal group appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal group appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteDouble? StateNormal { get; }

    private bool ShouldSerializeStateNormal() => !StateNormal!.IsDefault;

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
            // Ask the view to Perform a layout
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
        Panel?.SetFixedState(state);
    }
    #endregion

    #region Protected
    /// <summary>
    /// Force the layout logic to size and position the panels.
    /// </summary>
    protected void ForceControlLayout()
    {
        // Usually the layout will not occur if currently initializing but
        // we need to force the layout processing because overwise the size
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

    #region Protected Overrides
    /// <summary>
    /// Creates a new instance of the control collection for the KryptonGroup.
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
    /// Raises the EnabledChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnEnabledChanged(EventArgs e)
    {
        // Push correct palettes into the view
        if (Enabled)
        {
            _drawDocker.SetPalettes(StateNormal!.Back, StateNormal.Border);
        }
        else
        {
            _drawDocker.SetPalettes(StateDisabled!.Back, StateDisabled.Border);
        }

        _drawDocker.Enabled = Enabled;

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
    /// Raises the Layout event.
    /// </summary>
    /// <param name="levent">An EventArgs that contains the event data.</param>
    protected override void OnLayout(LayoutEventArgs levent)
    {
        // Remember if we are inside a layout cycle
        _layingOut = true;

        // Let base class calulcate fill rectangle
        base.OnLayout(levent);

        // Only use layout logic if control is fully initialized or if being forced
        // to allow a relayout or if in design mode.
        if (IsInitialized || _forcedLayout || (DesignMode && (Panel != null)))
        {
            Rectangle fillRect = _layoutFill.FillRect;
            Panel?.SetBounds(fillRect.X, fillRect.Y, fillRect.Width, fillRect.Height);
        }

        _layingOut = false;
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
            Panel?.PerformNeedPaint(e.NeedLayout);
        }
        else
        {
            ForceControlLayout();
        }

        base.OnNeedPaint(sender, e);
    }
    #endregion

    #region Implementation
    private void OnGroupPanelPaint(object? sender, NeedLayoutEventArgs e)
    {
        // If the child panel is layout out but not because we are, then it must be
        // laying out because a child has changed visibility/size/etc. If we are an
        // AutoSize control then we need to ensure we layout as well to change size.
        if (e.NeedLayout && !_layingOut && AutoSize)
        {
            PerformNeedPaint(true);
        }
    }
    #endregion
}
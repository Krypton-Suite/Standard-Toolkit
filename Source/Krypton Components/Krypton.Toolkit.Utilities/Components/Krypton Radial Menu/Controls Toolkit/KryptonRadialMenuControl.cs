#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Form-hosted Syncfusion-style radial menu control that shares items and painting with <see cref="KryptonRadialMenu"/>.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonRadialMenuControl), "ToolboxBitmaps.KryptonRadialMenuControl.bmp")]
[DefaultEvent(nameof(ItemClick))]
[DefaultProperty(nameof(Items))]
[DesignerCategory(@"code")]
[Designer(typeof(KryptonRadialMenuControlDesigner))]
[Description(@"Displays a radial menu as a hosted control on a form or container.")]
public class KryptonRadialMenuControl : Control, IRadialMenuAppearance, IRadialMenuInteractionHost
{
    #region Instance Fields

    private readonly RadialMenuInteractionCore _core;
    private readonly RadialMenuToolTipHost _toolTipHost;
    private readonly PaletteRedirect _paletteRedirect;
    private readonly PaletteBorderInheritRedirect _stateCommonRedirect;
    private PaletteMode _paletteMode;
    private KryptonCustomPaletteBase? _localCustomPalette;
    private float _dpiScale = 1f;
    private bool _useHub;
    private bool _expanded = true;
    private bool _hubTracking;
    private bool _allowMove;
    private bool _movePending;
    private bool _moving;
    private bool _isFloating;
    private Point _moveScreenStart;
    private Point _moveLocationStart;
    private Control? _dockParent;
    private int _dockChildIndex;
    private DockStyle _dockStyle;
    private Size _dockSize;
    private Point _dockLocation;
    private VisualRadialMenuFloatForm? _floatForm;
    private MethodInfo? _paintTransparentBackground;

    private static readonly Color TransparencyKeyColor = Color.Magenta;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when any item is activated.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when a radial menu item is activated.")]
    public event EventHandler<KryptonRadialMenuItemClickEventArgs>? ItemClick;

    /// <summary>
    /// Occurs when the centre button is clicked at the root level.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the centre button is clicked at the root level.")]
    public event EventHandler? CenterButtonClick;

    /// <summary>
    /// Occurs when <see cref="Expanded"/> changes while hub mode is enabled.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the hub expands or collapses the radial menu.")]
    public event EventHandler? ExpandedChanged;

    /// <summary>
    /// Occurs when the control floats outside its parent or docks back.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the control starts floating outside its parent or docks back.")]
    public event EventHandler? FloatingChanged;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonRadialMenuControl"/> class.
    /// </summary>
    public KryptonRadialMenuControl()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint
            | ControlStyles.UserPaint
            | ControlStyles.OptimizedDoubleBuffer
            | ControlStyles.ResizeRedraw
            | ControlStyles.SupportsTransparentBackColor, true);
        SetStyle(ControlStyles.Opaque, false);
        BackColor = Color.Transparent;
        TabStop = true;
        AccessibleRole = AccessibleRole.MenuPopup;
        AccessibleName = @"Radial menu";

        _paletteMode = PaletteMode.Global;
        Items = [];
        Values = new KryptonRadialMenuValues(OnNeedPaint);

        _paletteRedirect = new PaletteRedirect(ResolvePalette());
        _stateCommonRedirect = new PaletteBorderInheritRedirect(_paletteRedirect, PaletteBorderStyle.ControlClient);
        StateCommon = new PaletteBorder(_stateCommonRedirect, OnNeedPaint);
        StateDisabled = new PaletteBorder(StateCommon, OnNeedPaint);
        StateNormal = new PaletteBorder(StateCommon, OnNeedPaint);
        StateTracking = new PaletteBorder(StateCommon, OnNeedPaint);
        StatePressed = new PaletteBorder(StateCommon, OnNeedPaint);

        _toolTipHost = new RadialMenuToolTipHost(this, ResolvePalette);
        _core = new RadialMenuInteractionCore(this);

        var diameter = RadialMenuMetrics.DiameterFromRadius(Values.MenuRadius);
        Size = new Size(diameter, diameter);
        MinimumSize = RadialMenuMetrics.MinControlSize;
    }

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _toolTipHost.Dispose();
            DestroyFloatForm(dockBack: false);
        }

        base.Dispose(disposing);
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets the collection of radial menu items.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Collection of radial menu items.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonRadialMenuItemCollection Items { get; }

    /// <summary>
    /// Gets access to appearance values (radius, glyph, colours, display style).
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Appearance values for the radial menu.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonRadialMenuValues Values { get; }

    private bool ShouldSerializeValues() => !Values.IsDefault;

    /// <summary>
    /// Gets or sets the palette mode.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Palette applied to drawing.")]
    [DefaultValue(PaletteMode.Global)]
    public PaletteMode PaletteMode
    {
        get => _paletteMode;
        set
        {
            if (_paletteMode != value)
            {
                _paletteMode = value;
                SyncPaletteRedirect();
                Invalidate();
            }
        }
    }

    /// <summary>
    /// Gets or sets a custom palette implementation.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Custom palette applied to drawing.")]
    [DefaultValue(null)]
    public KryptonCustomPaletteBase? LocalCustomPalette
    {
        get => _localCustomPalette;
        set
        {
            if (!ReferenceEquals(_localCustomPalette, value))
            {
                _localCustomPalette = value;
                SyncPaletteRedirect();
                Invalidate();
            }
        }
    }

    /// <summary>Gets access to the common outer-ring border appearance.</summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common outer-ring border appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBorder StateCommon { get; }

    private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

    /// <summary>Gets access to the disabled outer-ring border appearance.</summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled outer-ring border appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBorder StateDisabled { get; }

    private bool ShouldSerializeStateDisabled() => !StateDisabled.IsDefault;

    /// <summary>Gets access to the normal outer-ring border appearance.</summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal outer-ring border appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBorder StateNormal { get; }

    private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;

    /// <summary>Gets access to the tracking (hot) outer-ring border appearance.</summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining hot tracking outer-ring border appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBorder StateTracking { get; }

    private bool ShouldSerializeStateTracking() => !StateTracking.IsDefault;

    /// <summary>Gets access to the pressed outer-ring border appearance.</summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining pressed outer-ring border appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBorder StatePressed { get; }

    private bool ShouldSerializeStatePressed() => !StatePressed.IsDefault;

    /// <summary>Gets or sets the outer menu radius. Proxy for <see cref="KryptonRadialMenuValues.MenuRadius"/>.</summary>
    [Category(@"Visuals")]
    [Description(@"Outer radius of the radial menu in pixels.")]
    [DefaultValue(RadialMenuMetrics.DefaultMenuRadius)]
    public int MenuRadius
    {
        get => Values.MenuRadius;
        set
        {
            Values.MenuRadius = value;
            UpdatePreferredSizeFromRadius();
        }
    }

    /// <summary>Gets or sets the inner radius. Proxy for <see cref="KryptonRadialMenuValues.InnerRadius"/>.</summary>
    [Category(@"Visuals")]
    [Description(@"Centre button radius in pixels.")]
    [DefaultValue(RadialMenuMetrics.DefaultInnerRadius)]
    public int InnerRadius
    {
        get => Values.InnerRadius;
        set => Values.InnerRadius = value;
    }

    /// <summary>Gets or sets the centre / hub image. Proxy for <see cref="KryptonRadialMenuValues.Glyph"/>.</summary>
    [Category(@"Visuals")]
    [Description(@"Image on the centre button and collapsed hub. When set, HubText is not drawn.")]
    [DefaultValue(null)]
    public Image? Glyph
    {
        get => Values.Glyph;
        set => Values.Glyph = value;
    }

    /// <summary>Gets or sets collapsed-hub caption text. Proxy for <see cref="KryptonRadialMenuValues.HubText"/>.</summary>
    [Category(@"Visuals")]
    [Description(@"Text on the collapsed hub when Glyph is null. Default is +.")]
    [DefaultValue("+")]
    [Localizable(true)]
    public string HubText
    {
        get => Values.HubText;
        set => Values.HubText = value;
    }

    /// <summary>Gets or sets the display style. Proxy for <see cref="KryptonRadialMenuValues.DisplayStyle"/>.</summary>
    [Category(@"Visuals")]
    [Description(@"How text and images are arranged on sectors.")]
    [DefaultValue(KryptonRadialMenuDisplayStyle.ImageAboveText)]
    public KryptonRadialMenuDisplayStyle DisplayStyle
    {
        get => Values.DisplayStyle;
        set => Values.DisplayStyle = value;
    }

    /// <summary>Gets or sets the sector image size. Proxy for <see cref="KryptonRadialMenuValues.ItemImageSize"/>.</summary>
    [Category(@"Visuals")]
    [Description(@"Sector image size in pixels.")]
    [DefaultValue(RadialMenuMetrics.DefaultItemImageSize)]
    public int ItemImageSize
    {
        get => Values.ItemImageSize;
        set => Values.ItemImageSize = value;
    }

    /// <summary>Gets or sets outer ring thickness. Proxy for <see cref="KryptonRadialMenuValues.OuterRingThickness"/>.</summary>
    [Category(@"Visuals")]
    [Description(@"Thickness of the outer ring stroke in 96-DPI logical pixels. Zero hides the stroke.")]
    [DefaultValue(RadialMenuMetrics.DefaultOuterRingThickness)]
    public float OuterRingThickness
    {
        get => Values.OuterRingThickness;
        set => Values.OuterRingThickness = value;
    }

    /// <summary>Gets or sets the uniform scale factor. Proxy for <see cref="KryptonRadialMenuValues.Scale"/>.</summary>
    [Category(@"Visuals")]
    [Description(@"Uniform scale factor (0.5–3). Multiplied with device DPI for layout and painting.")]
    [DefaultValue(RadialMenuMetrics.DefaultScale)]
    public new float Scale
    {
        get => Values.Scale;
        set
        {
            Values.Scale = value;
            UpdatePreferredSizeFromRadius();
        }
    }

    /// <summary>Gets or sets the start angle. Proxy for <see cref="KryptonRadialMenuValues.StartAngle"/>.</summary>
    [Category(@"Visuals")]
    [Description(@"First sector angle in degrees.")]
    [DefaultValue(RadialMenuMetrics.DefaultStartAngle)]
    public float StartAngle
    {
        get => Values.StartAngle;
        set => Values.StartAngle = value;
    }

    /// <summary>
    /// Gets or sets whether the user can drag the hub (or expanded centre) to reposition the control.
    /// </summary>
    /// <remarks>
    /// A short click without dragging still expands / activates. Docked controls are undocked on the first drag so they can move.
    /// Dragging the centre outside the parent promotes the control into a borderless floating window; dropping back over the
    /// original parent docks it again.
    /// </remarks>
    [Category(@"Behavior")]
    [Description(@"Allows dragging the hub or centre to move the control, including floating outside the host window.")]
    [DefaultValue(false)]
    public bool AllowMove
    {
        get => _allowMove;
        set => _allowMove = value;
    }

    /// <summary>
    /// Gets whether the control is currently hosted in a floating top-level window outside its original parent.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsFloating => _isFloating;

    /// <summary>
    /// Gets or sets whether the control shows a collapsed hub that expands into the radial menu when pressed.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"When true, shows a centre hub button; press to expand the radial menu, press centre again (or AutoClose) to collapse.")]
    [DefaultValue(false)]
    public bool UseHub
    {
        get => _useHub;
        set
        {
            if (_useHub == value)
            {
                return;
            }

            _useHub = value;
            if (!_useHub)
            {
                SetExpanded(true, raiseEvent: false);
            }
            else
            {
                SetExpanded(false, raiseEvent: false);
                _core.ResetToRoot();
            }

            Invalidate();
        }
    }

    /// <summary>
    /// Gets or sets whether the radial menu is expanded. When <see cref="UseHub"/> is false, always treated as expanded.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Whether the radial sectors are visible. Only meaningful when UseHub is true.")]
    [DefaultValue(true)]
    public bool Expanded
    {
        get => !_useHub || _expanded;
        set => SetExpanded(value, raiseEvent: true);
    }

    /// <summary>
    /// Expands the radial menu from the hub.
    /// </summary>
    public void Expand() => Expanded = true;

    /// <summary>
    /// Collapses the radial menu back to the hub (no-op when <see cref="UseHub"/> is false).
    /// </summary>
    public void Collapse()
    {
        if (_useHub)
        {
            Expanded = false;
        }
    }

    /// <summary>
    /// Promotes the control into a floating top-level window at its current screen position.
    /// </summary>
    /// <returns><c>true</c> if the control is floating after the call; otherwise <c>false</c>.</returns>
    public bool Float()
    {
        if (_isFloating || Parent == null || DesignMode)
        {
            return _isFloating;
        }

        CaptureDockSnapshot();
        BeginFloat(PointToScreen(new Point(Width / 2, Height / 2)), rebaseMove: false);
        return _isFloating;
    }

    /// <summary>
    /// Returns a floating control to its original parent, dock style, and size.
    /// </summary>
    /// <returns><c>true</c> if the control was docked back; otherwise <c>false</c>.</returns>
    public bool DockBack()
    {
        if (!_isFloating || _dockParent == null || _dockParent.IsDisposed)
        {
            return false;
        }

        return EndFloat(dockBack: true);
    }

    /// <summary>
    /// Resets navigation to the root item collection.
    /// </summary>
    public void ResetNavigation()
    {
        UpdateDpiScale();
        _core.ResetToRoot();
        Invalidate();
    }

    #endregion

    #region IRadialMenuAppearance / IRadialMenuInteractionHost

    Color IRadialMenuAppearance.ResolveOuterRingColor(PaletteState state) => ResolveOuterRingColor(state);

    KryptonRadialMenuValues IRadialMenuInteractionHost.Values => Values;

    KryptonRadialMenuItemCollection IRadialMenuInteractionHost.RootItems => Items;

    bool IRadialMenuInteractionHost.Enabled => Enabled;

    IRadialMenuAppearance IRadialMenuInteractionHost.Appearance => this;

    PaletteBase IRadialMenuInteractionHost.ResolvePalette() => ResolvePalette();

    Size IRadialMenuInteractionHost.ClientSize => ClientSize;

    bool IRadialMenuInteractionHost.IsRightToLeft => RightToLeft == RightToLeft.Yes;

    float IRadialMenuInteractionHost.LayoutScale => CurrentMetrics().LayoutScale;

    RadialMenuMetrics IRadialMenuInteractionHost.Metrics => CurrentMetrics();

    int IRadialMenuInteractionHost.EffectiveMenuRadius => CurrentMetrics().MenuRadius;

    int IRadialMenuInteractionHost.EffectiveInnerRadius => CurrentMetrics().InnerRadius;

    RadialMenuToolTipHost? IRadialMenuInteractionHost.ToolTipHost => _toolTipHost;

    // Hub mode: AutoClose / root centre collapse the expanded ring back to the hub.
    bool IRadialMenuInteractionHost.SupportsAutoClose => _useHub && _expanded;

    void IRadialMenuInteractionHost.InvalidateSurface() => Invalidate();

    void IRadialMenuInteractionHost.RaiseItemClick(KryptonRadialMenuItemBase item) =>
        ItemClick?.Invoke(this, new KryptonRadialMenuItemClickEventArgs(item));

    void IRadialMenuInteractionHost.RaiseCenterButtonClick() =>
        CenterButtonClick?.Invoke(this, EventArgs.Empty);

    void IRadialMenuInteractionHost.RequestClose(ToolStripDropDownCloseReason reason)
    {
        if (_useHub)
        {
            Collapse();
        }
    }

    void IRadialMenuInteractionHost.OnNavigated()
    {
        // No open animation on the hosted control.
    }

    void IRadialMenuInteractionHost.SetAccessibleName(string name) => AccessibleName = name;

    #endregion

    #region Protected

    /// <inheritdoc />
    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);
        UpdateDpiScale();
        _core.ResetToRoot();
    }

    /// <inheritdoc />
    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        UpdateDpiScale();
        _core.RefreshLayout();
        Invalidate();
    }

    /// <inheritdoc />
    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
        // Copy the parent surface into the double-buffer so corners outside the radial
        // artwork are see-through. When floating, Magenta matches the float form colour key.
        if (!PaintParentBackground(pevent))
        {
            var back = _isFloating ? TransparencyKeyColor : ResolveSurfaceBackColor();
            using var brush = new SolidBrush(back);
            pevent.Graphics.FillRectangle(brush, ClientRectangle);
        }
    }

    /// <inheritdoc />
    protected override void OnPaint(PaintEventArgs e)
    {
        if (!IsHandleCreated)
        {
            return;
        }

        UpdateDpiScale();
        if (_useHub && !_expanded)
        {
            var colors = RadialMenuColorSet.FromPalette(ResolvePalette(), Values);
            RadialMenuPainter.PaintHub(
                e.Graphics,
                ClientRectangle,
                Values,
                colors,
                CurrentMetrics(),
                _hubTracking);
        }
        else
        {
            _core.Paint(e.Graphics, ClientRectangle);
        }

        base.OnPaint(e);
    }

    /// <inheritdoc />
    protected override void OnMouseMove(MouseEventArgs e)
    {
        if (!Enabled)
        {
            base.OnMouseMove(e);
            return;
        }

        if (_movePending || _moving)
        {
            var screen = PointToScreen(e.Location);
            var dx = screen.X - _moveScreenStart.X;
            var dy = screen.Y - _moveScreenStart.Y;
            if (!_moving && ((Math.Abs(dx) >= CurrentMetrics().MoveDragThreshold) || (Math.Abs(dy) >= CurrentMetrics().MoveDragThreshold)))
            {
                _moving = true;
                _movePending = false;
                PrepareForMove();
                Cursor = Cursors.SizeAll;
            }

            if (_moving)
            {
                ApplyMoveDelta(dx, dy);
                if (!_isFloating && ShouldFloatOut())
                {
                    BeginFloat(screen, rebaseMove: true);
                }

                return;
            }
        }

        if (_useHub && !_expanded)
        {
            var overHub = IsOverHub(e.Location);
            if (_hubTracking != overHub)
            {
                _hubTracking = overHub;
                Invalidate();
            }

            Cursor = overHub
                ? (_allowMove ? Cursors.SizeAll : Cursors.Hand)
                : Cursors.Default;
            base.OnMouseMove(e);
            return;
        }

        var hit = _core.UpdateTrackingFromMove(e.Location, suppressToolTips: false);
        if (hit is { Kind: RadialHitKind.OuterRing, SectorIndex: >= 0 }
            && hit.SectorIndex < _core.VisibleItems.Count
            && _core.VisibleItems[hit.SectorIndex].HasChildren
            && _core.VisibleItems[hit.SectorIndex].Enabled)
        {
            Cursor = Cursors.Hand;
        }
        else if (_allowMove && hit.Kind == RadialHitKind.Center)
        {
            Cursor = Cursors.SizeAll;
        }
        else
        {
            Cursor = Cursors.Default;
        }

        base.OnMouseMove(e);
    }

    /// <inheritdoc />
    protected override void OnMouseDown(MouseEventArgs e)
    {
        if (Enabled && e.Button == MouseButtons.Left)
        {
            if (!Focused)
            {
                Focus();
            }

            var canStartMove = _allowMove && (
                (_useHub && !_expanded && IsOverHub(e.Location))
                || (!(_useHub && !_expanded) && _core.HitTest(e.Location).Kind == RadialHitKind.Center));

            if (canStartMove)
            {
                // Defer expand / centre click until mouse-up; start a move if the pointer travels far enough.
                _movePending = true;
                _moving = false;
                CaptureDockSnapshot();
                _moveScreenStart = PointToScreen(e.Location);
                _moveLocationStart = _isFloating && _floatForm != null
                    ? _floatForm.Location
                    : Location;
                Cursor = Cursors.SizeAll;
                Capture = true;
            }
            else if (!(_useHub && !_expanded))
            {
                _core.BeginPress(e.Location);
            }
        }

        base.OnMouseDown(e);
    }

    /// <inheritdoc />
    protected override void OnMouseUp(MouseEventArgs e)
    {
        if (Enabled && e.Button == MouseButtons.Left)
        {
            if (_movePending || _moving)
            {
                var moved = _moving;
                _movePending = false;
                _moving = false;
                Cursor = Cursors.Default;
                if (Capture)
                {
                    Capture = false;
                }

                if (moved)
                {
                    // Drop over the original parent docks the floating surface back.
                    if (_isFloating && ShouldDockBackAtCursor())
                    {
                        DockBack();
                    }

                    // Reposition only — do not expand / activate.
                    base.OnMouseUp(e);
                    return;
                }

                // Pressed without dragging: honour hub expand or centre action.
                if (_useHub && !_expanded)
                {
                    if (IsOverHub(e.Location))
                    {
                        Expand();
                    }
                }
                else
                {
                    _core.HandleCenterClick();
                    Invalidate();
                }

                base.OnMouseUp(e);
                return;
            }

            if (_useHub && !_expanded)
            {
                if (IsOverHub(e.Location))
                {
                    Expand();
                }
            }
            else
            {
                _core.CompleteClick(e.Location);
            }
        }

        base.OnMouseUp(e);
    }

    /// <inheritdoc />
    protected override void OnMouseLeave(EventArgs e)
    {
        if (_hubTracking)
        {
            _hubTracking = false;
            Invalidate();
        }

        if (!_movePending && !_moving)
        {
            _core.ClearTracking();
        }

        base.OnMouseLeave(e);
    }

    /// <inheritdoc />
    protected override void OnMouseWheel(MouseEventArgs e)
    {
        if (Enabled && !(_useHub && !_expanded))
        {
            _core.HandleMouseWheel(e.Delta);
        }

        base.OnMouseWheel(e);
    }

    /// <inheritdoc />
    protected override void OnKeyPress(KeyPressEventArgs e)
    {
        if (Enabled && !(_useHub && !_expanded) && _core.HandleKeyPress(e.KeyChar))
        {
            e.Handled = true;
            return;
        }

        base.OnKeyPress(e);
    }

    /// <inheritdoc />
    protected override bool IsInputKey(Keys keyData)
    {
        switch (keyData)
        {
            case Keys.Left:
            case Keys.Right:
            case Keys.Up:
            case Keys.Down:
            case Keys.Home:
            case Keys.End:
            case Keys.Enter:
            case Keys.Space:
            case Keys.Escape:
            case Keys.Back:
                return true;
            default:
                return base.IsInputKey(keyData);
        }
    }

    /// <inheritdoc />
    protected override void OnKeyDown(KeyEventArgs e)
    {
        if (!Enabled)
        {
            base.OnKeyDown(e);
            return;
        }

        if (_useHub && !_expanded)
        {
            if (e.KeyData is Keys.Enter or Keys.Space)
            {
                Expand();
                e.Handled = true;
                return;
            }
        }
        else if (_useHub && _expanded && e.KeyData == Keys.Escape && !_core.CanGoBack)
        {
            Collapse();
            e.Handled = true;
            return;
        }
        else if (_core.TryProcessKeyboard(e.KeyData, allowRootEscapeDismiss: false))
        {
            e.Handled = true;
            return;
        }

        base.OnKeyDown(e);
    }

    /// <inheritdoc />
    protected override void OnEnabledChanged(EventArgs e)
    {
        base.OnEnabledChanged(e);
        Invalidate();
    }

    /// <inheritdoc />
    protected override void OnRightToLeftChanged(EventArgs e)
    {
        base.OnRightToLeftChanged(e);
        _core.RefreshLayout();
        Invalidate();
    }

    #endregion

    #region Implementation

    private void OnNeedPaint(object? sender, NeedLayoutEventArgs e)
    {
        if (IsHandleCreated)
        {
            _core.RefreshLayout();
            Invalidate();
        }
    }

    private void UpdatePreferredSizeFromRadius()
    {
        UpdateDpiScale();
        var metrics = RadialMenuMetrics.From(
            Values,
            IsHandleCreated ? _dpiScale : Values.Scale,
            new Size(int.MaxValue / 4, int.MaxValue / 4));
        var diameter = RadialMenuMetrics.DiameterFromRadius(metrics.PreferredMenuRadius);
        Size = new Size(Math.Max(MinimumSize.Width, diameter), Math.Max(MinimumSize.Height, diameter));

        if (IsHandleCreated)
        {
            _core.RefreshLayout();
            Invalidate();
        }
    }

    private RadialMenuMetrics CurrentMetrics()
    {
        UpdateDpiScale();
        var available = ClientSize.Width > 0 && ClientSize.Height > 0
            ? ClientSize
            : new Size(
                RadialMenuMetrics.DiameterFromRadius(Values.MenuRadius),
                RadialMenuMetrics.DiameterFromRadius(Values.MenuRadius));
        return RadialMenuMetrics.From(Values, _dpiScale, available);
    }

    private Size GetPreferredFloatSize()
    {
        UpdateDpiScale();
        var metrics = RadialMenuMetrics.From(Values, _dpiScale, new Size(int.MaxValue / 4, int.MaxValue / 4));
        var diameter = RadialMenuMetrics.DiameterFromRadius(metrics.PreferredMenuRadius);
        return new Size(
            Math.Max(MinimumSize.Width, diameter),
            Math.Max(MinimumSize.Height, diameter));
    }

    private void PrepareForMove()
    {
        if (_isFloating || Dock == DockStyle.None)
        {
            return;
        }

        // Convert from docked layout to absolute location so the control can be dragged.
        var bounds = Bounds;
        Dock = DockStyle.None;
        Bounds = bounds;
        _moveLocationStart = Location;
    }

    private void CaptureDockSnapshot()
    {
        if (_isFloating || Parent == null)
        {
            return;
        }

        _dockParent = Parent;
        _dockChildIndex = Parent.Controls.GetChildIndex(this);
        _dockStyle = Dock;
        _dockSize = Size;
        _dockLocation = Location;
    }

    private void ApplyMoveDelta(int dx, int dy)
    {
        var location = new Point(_moveLocationStart.X + dx, _moveLocationStart.Y + dy);
        if (_isFloating && _floatForm != null)
        {
            _floatForm.Location = location;
        }
        else
        {
            Location = location;
        }
    }

    private bool ShouldFloatOut()
    {
        if (DesignMode || Parent == null || !Parent.IsHandleCreated)
        {
            return false;
        }

        var centre = PointToScreen(new Point(Width / 2, Height / 2));
        var parentScreen = Parent.RectangleToScreen(Parent.ClientRectangle);
        return !parentScreen.Contains(centre);
    }

    private bool ShouldDockBackAtCursor()
    {
        if (_dockParent == null || !_dockParent.IsHandleCreated || _dockParent.IsDisposed)
        {
            return false;
        }

        var parentScreen = _dockParent.RectangleToScreen(_dockParent.ClientRectangle);
        return parentScreen.Contains(Control.MousePosition);
    }

    private void BeginFloat(Point screenAnchor, bool rebaseMove)
    {
        if (_isFloating || Parent == null || DesignMode)
        {
            return;
        }

        if (_dockParent == null)
        {
            CaptureDockSnapshot();
        }

        if (_dockParent == null)
        {
            return;
        }

        var floatSize = GetPreferredFloatSize();
        var centre = PointToScreen(new Point(Width / 2, Height / 2));
        var floatLocation = new Point(centre.X - (floatSize.Width / 2), centre.Y - (floatSize.Height / 2));

        var owner = FindForm();
        _floatForm = new VisualRadialMenuFloatForm
        {
            Size = floatSize,
            Location = floatLocation
        };
        _floatForm.FormClosing += OnFloatFormClosing;

        var wasCapture = Capture;
        Parent.Controls.Remove(this);
        Dock = DockStyle.Fill;
        Size = floatSize;
        _floatForm.Controls.Add(this);
        _isFloating = true;

        if (owner != null)
        {
            _floatForm.Show(owner);
        }
        else
        {
            _floatForm.Show();
        }

        if (rebaseMove)
        {
            _moveScreenStart = screenAnchor;
            _moveLocationStart = _floatForm.Location;
        }

        if (wasCapture)
        {
            Capture = true;
        }

        FloatingChanged?.Invoke(this, EventArgs.Empty);
    }

    private bool EndFloat(bool dockBack)
    {
        if (!_isFloating || _floatForm == null)
        {
            return false;
        }

        var floatForm = _floatForm;
        floatForm.FormClosing -= OnFloatFormClosing;

        floatForm.Controls.Remove(this);
        Dock = DockStyle.None;

        if (dockBack && _dockParent != null && !_dockParent.IsDisposed)
        {
            Size = _dockSize;
            Dock = _dockStyle;
            if (_dockStyle == DockStyle.None)
            {
                Location = _dockLocation;
            }

            _dockParent.Controls.Add(this);
            var index = Math.Min(_dockChildIndex, _dockParent.Controls.Count - 1);
            if (index >= 0)
            {
                _dockParent.Controls.SetChildIndex(this, index);
            }
        }

        _floatForm = null;
        _isFloating = false;

        floatForm.Hide();
        floatForm.Dispose();

        FloatingChanged?.Invoke(this, EventArgs.Empty);
        return dockBack;
    }

    private void DestroyFloatForm(bool dockBack)
    {
        if (_isFloating)
        {
            EndFloat(dockBack);
        }
    }

    private void OnFloatFormClosing(object? sender, FormClosingEventArgs e)
    {
        if (e.CloseReason == CloseReason.UserClosing)
        {
            e.Cancel = true;
            DockBack();
        }
    }

    private void SetExpanded(bool value, bool raiseEvent)
    {
        if (!_useHub)
        {
            value = true;
        }

        if (_expanded == value)
        {
            return;
        }

        _expanded = value;
        if (!_expanded)
        {
            _core.ResetToRoot();
            _hubTracking = false;
            Cursor = Cursors.Default;
        }
        else
        {
            UpdateDpiScale();
            _core.RefreshLayout();
        }

        Invalidate();
        if (raiseEvent)
        {
            ExpandedChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private bool IsOverHub(Point clientPoint)
    {
        var cx = ClientSize.Width / 2.0;
        var cy = ClientSize.Height / 2.0;
        var dx = clientPoint.X - cx;
        var dy = clientPoint.Y - cy;
        var radius = CurrentMetrics().InnerRadius;
        return (dx * dx) + (dy * dy) <= (radius * radius);
    }

    private bool PaintParentBackground(PaintEventArgs e)
    {
        if (Parent == null)
        {
            return false;
        }

        try
        {
            if (_paintTransparentBackground == null)
            {
                // Same WinForms internal helper used by VisualControlBase / KryptonWrapLabel.
                _paintTransparentBackground = typeof(Control).GetMethod(
                    @"PaintTransparentBackground",
                    BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod,
                    null,
                    CallingConventions.HasThis,
                    [typeof(PaintEventArgs), typeof(Rectangle), typeof(Region)],
                    null);
            }

            if (_paintTransparentBackground == null)
            {
                return false;
            }

            _paintTransparentBackground.Invoke(this, [e, ClientRectangle, null!]);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private Color ResolveSurfaceBackColor()
    {
        if (BackColor.A == 255 && BackColor != Color.Transparent && BackColor != TransparencyKeyColor)
        {
            return BackColor;
        }

        var palette = ResolvePalette();
        var state = Enabled ? PaletteState.Normal : PaletteState.Disabled;
        var color = palette.GetBackColor1(PaletteBackStyle.PanelClient, state);
        if (!color.IsEmpty)
        {
            return color;
        }

        if (Parent != null && Parent.BackColor.A == 255 && Parent.BackColor != TransparencyKeyColor)
        {
            return Parent.BackColor;
        }

        return SystemColors.Control;
    }

    private void UpdateDpiScale()
    {
        _dpiScale = 1f;
        if (IsHandleCreated)
        {
            try
            {
                _dpiScale = DeviceDpi / 96f;
            }
            catch
            {
                _dpiScale = 1f;
            }
        }

        if (_dpiScale < RadialMenuMetrics.MinLayoutScale)
        {
            _dpiScale = 1f;
        }
    }

    private void SyncPaletteRedirect() => _paletteRedirect.Target = ResolvePalette();

    internal PaletteBase ResolvePalette()
    {
        if (_localCustomPalette != null)
        {
            return _localCustomPalette;
        }

        return _paletteMode switch
        {
            PaletteMode.Global => KryptonManager.CurrentGlobalPalette,
            _ => KryptonManager.GetPaletteForMode(_paletteMode)
        };
    }

    private Color ResolveOuterRingColor(PaletteState state)
    {
        SyncPaletteRedirect();
        var border = state switch
        {
            PaletteState.Disabled => StateDisabled,
            PaletteState.Tracking => StateTracking,
            PaletteState.Pressed => StatePressed,
            _ => StateNormal
        };

        var color = border.GetBorderColor1(state);
        return color.IsEmpty ? SystemColors.ControlDark : color;
    }

    #endregion
}

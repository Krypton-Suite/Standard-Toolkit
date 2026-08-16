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
/// Popup host that renders and interacts with a <see cref="KryptonRadialMenu"/>.
/// </summary>
internal class VisualRadialMenuPopup : VisualPopup, IRadialMenuInteractionHost
{
    #region Instance Fields

    private readonly KryptonRadialMenu _owner;
    private readonly RadialMenuInteractionCore _core;
    private readonly RadialMenuToolTipHost _toolTipHost;
    private float _dpiScale = 1f;
    private bool _movePending;
    private bool _moving;
    private bool _closing;
    private bool _hovering;
    private Point _moveScreenStart;
    private Point _moveLocationStart;
    private double _animationProgress = 1.0;
    private System.Windows.Forms.Timer? _animationTimer;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="VisualRadialMenuPopup"/> class.
    /// </summary>
    /// <param name="owner">Owning radial menu component.</param>
    /// <param name="renderer">Renderer used by the base popup infrastructure.</param>
    public VisualRadialMenuPopup(KryptonRadialMenu owner, IRenderer? renderer)
        : base(new ViewManager(), renderer, owner.Values.ShowShadow)
    {
        _owner = owner ?? ThrowHelper.ThrowArgumentNullException(owner);
        ViewManager!.Control = this;
        ViewManager.AlignControl = this;
        ViewManager.Root = new ViewLayoutNull();

        // Circular Region clips the HWND; avoid Magenta colour-key (AA edges become a pink fringe).
        SetStyle(ControlStyles.Opaque, false);
        SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        BackColor = Color.Transparent;
        AccessibleRole = AccessibleRole.MenuPopup;
        AccessibleName = @"Radial menu";
        _toolTipHost = new RadialMenuToolTipHost(this, owner.ResolvePalette);
        _core = new RadialMenuInteractionCore(this);
    }

    #endregion

    #region IRadialMenuInteractionHost

    KryptonRadialMenuValues IRadialMenuInteractionHost.Values => _owner.Values;

    KryptonRadialMenuItemCollection IRadialMenuInteractionHost.RootItems => _owner.Values.Items;

    bool IRadialMenuInteractionHost.Enabled => _owner.Enabled;

    IRadialMenuAppearance IRadialMenuInteractionHost.Appearance => _owner;

    PaletteBase IRadialMenuInteractionHost.ResolvePalette() => _owner.ResolvePalette();

    Size IRadialMenuInteractionHost.ClientSize => ClientSize;

    bool IRadialMenuInteractionHost.IsRightToLeft => RightToLeft == RightToLeft.Yes;

    float IRadialMenuInteractionHost.LayoutScale => CurrentMetrics().LayoutScale;

    RadialMenuMetrics IRadialMenuInteractionHost.Metrics => CurrentMetrics();

    int IRadialMenuInteractionHost.EffectiveMenuRadius => CurrentMetrics().MenuRadius;

    int IRadialMenuInteractionHost.EffectiveInnerRadius => CurrentMetrics().InnerRadius;

    private RadialMenuMetrics CurrentMetrics()
    {
        UpdateDpiScale();
        return RadialMenuMetrics.From(_owner.Values, _dpiScale, ClientSize.Width > 0 ? ClientSize : PreferredWorkingAreaSize(Location));
    }

    private static Size PreferredWorkingAreaSize(Point screenPoint)
    {
        var area = Screen.FromPoint(screenPoint).WorkingArea;
        return area.Size;
    }

    RadialMenuToolTipHost? IRadialMenuInteractionHost.ToolTipHost => _toolTipHost;

    bool IRadialMenuInteractionHost.SupportsAutoClose => true;

    void IRadialMenuInteractionHost.InvalidateSurface() => Invalidate();

    void IRadialMenuInteractionHost.RaiseItemClick(KryptonRadialMenuItemBase item) => _owner.RaiseItemClick(item);

    void IRadialMenuInteractionHost.RaiseCenterButtonClick() => _owner.OnCenterButtonClick();

    void IRadialMenuInteractionHost.RequestClose(ToolStripDropDownCloseReason reason) => _owner.Close(reason);

    void IRadialMenuInteractionHost.OnNavigated()
    {
        if (!_closing
            && _owner.Values.AnimationStyle != KryptonRadialMenuAnimationStyle.None
            && _owner.Values.AnimationDuration > 0
            && IsHandleCreated)
        {
            BeginAnimation();
        }
    }

    void IRadialMenuInteractionHost.SetAccessibleName(string name) => AccessibleName = name;

    #endregion

    #region Public

    /// <summary>
    /// Shows the popup centred on the provided screen point.
    /// </summary>
    /// <param name="screenCenter">Screen centre point.</param>
    /// <param name="animated">Whether to run a short open animation.</param>
    public void ShowCentered(Point screenCenter, bool animated)
    {
        _closing = false;
        UpdateDpiScale();
        _core.ResetToRoot();

        var metrics = RadialMenuMetrics.From(_owner.Values, _dpiScale, PreferredWorkingAreaSize(screenCenter));
        var diameter = RadialMenuMetrics.DiameterFromRadius(metrics.MenuRadius);
        var size = new Size(diameter, diameter);
        var location = new Point(screenCenter.X - (size.Width / 2), screenCenter.Y - (size.Height / 2));
        ApplyCircularRegion(size);

        var style = _owner.Values.AnimationStyle;
        var shouldAnimate = animated
            && style != KryptonRadialMenuAnimationStyle.None
            && _owner.Values.AnimationDuration > 0;

        if (shouldAnimate)
        {
            _animationProgress = 0.0;
            Show(new Rectangle(location, size));
            ApplyCircularRegion(size);
            BeginAnimation();
        }
        else
        {
            _animationProgress = 1.0;
            Show(new Rectangle(location, size));
            ApplyCircularRegion(size);
        }

        SyncShadowAppearance();
    }

    /// <inheritdoc />
    public override void Show(Rectangle screenRect)
    {
        // Use padded shadow so outer halo rings are not clipped to the menu rectangle.
        SetBounds(screenRect.X, screenRect.Y, screenRect.Width, screenRect.Height);
        DefineCircularShadowPaths(screenRect.Size);
        var metrics = CurrentMetrics();
        ShowShadow(screenRect, metrics.ShadowPadding, metrics.ShadowOffset);
        PI.ShowWindow(Handle, PI.ShowWindowCommands.SW_SHOWNOACTIVATE);
        VisualPopupManager.Singleton.StartTracking(this);
    }

    #endregion

    #region Protected

    /// <inheritdoc />
    protected override AccessibleObject CreateAccessibilityInstance() =>
        new VisualRadialMenuPopupAccessibleObject(this);

    /// <inheritdoc />
    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
        // Circular Region already clips the HWND; fill the disc so the double-buffer is not black.
        FillPopupSurface(pevent.Graphics);
    }

    /// <inheritdoc />
    protected override void OnPaint(PaintEventArgs? e)
    {
        if (IsDisposed || e == null)
        {
            return;
        }

        var g = e.Graphics;
        FillPopupSurface(g);
        var state = g.Save();
        try
        {
            ApplyAnimationTransform(g);
            g.CompositingMode = CompositingMode.SourceOver;
            _core.Paint(g, ClientRectangle);
        }
        finally
        {
            g.Restore(state);
        }
    }

    /// <inheritdoc />
    protected override void OnMouseMove(MouseEventArgs e)
    {
        if (IsDisposed || _closing)
        {
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
                Cursor = Cursors.SizeAll;
            }

            if (_moving)
            {
                Location = new Point(_moveLocationStart.X + dx, _moveLocationStart.Y + dy);
                _toolTipHost.Cancel();
                return;
            }
        }

        var hit = _core.UpdateTrackingFromMove(e.Location, suppressToolTips: false);
        _hovering = hit.Kind != RadialHitKind.None;
        if (hit is { Kind: RadialHitKind.OuterRing, SectorIndex: >= 0 }
            && hit.SectorIndex < _core.VisibleItems.Count
            && _core.VisibleItems[hit.SectorIndex].HasChildren
            && _core.VisibleItems[hit.SectorIndex].Enabled)
        {
            Cursor = Cursors.Hand;
        }
        else
        {
            Cursor = (_owner.AllowMove && hit.Kind == RadialHitKind.Center) ? Cursors.SizeAll : Cursors.Default;
        }

        SyncShadowAppearance();
        base.OnMouseMove(e);
    }

    /// <inheritdoc />
    protected override void OnMouseDown(MouseEventArgs e)
    {
        if (IsDisposed || _closing || e.Button != MouseButtons.Left)
        {
            base.OnMouseDown(e);
            return;
        }

        var hit = _core.HitTest(e.Location);
        if (_owner.AllowMove && hit.Kind == RadialHitKind.Center)
        {
            _movePending = true;
            _moving = false;
            _moveScreenStart = PointToScreen(e.Location);
            _moveLocationStart = Location;
            Cursor = Cursors.SizeAll;
            Capture = true;
        }
        else
        {
            _core.BeginPress(e.Location);
            SyncShadowAppearance();
        }

        base.OnMouseDown(e);
    }

    /// <inheritdoc />
    protected override void OnMouseUp(MouseEventArgs e)
    {
        if (IsDisposed || _closing)
        {
            base.OnMouseUp(e);
            return;
        }

        if (e.Button == MouseButtons.Left)
        {
            if (_core.DraggingSlider)
            {
                _core.EndSliderDrag();
                base.OnMouseUp(e);
                return;
            }

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
                    base.OnMouseUp(e);
                    return;
                }

                _core.HandleCenterClick();
                Invalidate();
                base.OnMouseUp(e);
                return;
            }

            _core.CompleteClick(e.Location);
        }

        SyncShadowAppearance();
        base.OnMouseUp(e);
    }

    /// <inheritdoc />
    protected override void OnMouseLeave(EventArgs e)
    {
        _hovering = false;
        _core.ClearTracking();
        SyncShadowAppearance();
        base.OnMouseLeave(e);
    }

    /// <inheritdoc />
    protected override void OnMouseWheel(MouseEventArgs e)
    {
        if (!IsDisposed && !_closing)
        {
            _core.HandleMouseWheel(e.Delta);
        }

        base.OnMouseWheel(e);
    }

    /// <inheritdoc />
    protected override void OnKeyPress(KeyPressEventArgs e)
    {
        if (!IsDisposed && !_closing && _core.HandleKeyPress(e.KeyChar))
        {
            e.Handled = true;
            return;
        }

        base.OnKeyPress(e);
    }

    /// <inheritdoc />
    protected override bool ProcessDialogKey(Keys keyData)
    {
        if (IsDisposed || _closing)
        {
            return base.ProcessDialogKey(keyData);
        }

        if (_core.TryProcessKeyboard(keyData, allowRootEscapeDismiss: true))
        {
            return true;
        }

        return base.ProcessDialogKey(keyData);
    }

    /// <inheritdoc />
    protected override void OnKeyDown(KeyEventArgs e)
    {
        if (!IsDisposed && !_closing && _core.TryProcessKeyboard(e.KeyData, allowRootEscapeDismiss: true))
        {
            e.Handled = true;
            return;
        }

        base.OnKeyDown(e);
    }

    /// <inheritdoc />
    public override bool DoesCurrentMouseDownEndAllTracking(Message m, Point pt)
    {
        if (_closing)
        {
            return false;
        }

        if (!ClientRectangle.Contains(pt))
        {
            return true;
        }

        return DistanceFromCenter(pt) > CurrentMetrics().MenuRadius;
    }

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            StopAnimationTimer();
            _toolTipHost.Dispose();
        }

        base.Dispose(disposing);
    }

    #endregion

    #region Internal

    /// <summary>
    /// Rebuilds sector layout for the current navigation level (e.g. after a live import refresh).
    /// </summary>
    internal void RefreshCurrentLevel()
    {
        if (IsDisposed)
        {
            return;
        }

        UpdateDpiScale();
        _core.ResetToRoot();
        Invalidate();
    }

    /// <summary>
    /// Rebuilds sectors for the current navigation level without resetting the stack.
    /// </summary>
    internal void RefreshLayout()
    {
        if (IsDisposed)
        {
            return;
        }

        UpdateDpiScale();
        _core.RefreshLayout();
        Invalidate();
    }

    /// <summary>
    /// Plays the close animation (reverse of open), then invokes <paramref name="completed"/>.
    /// </summary>
    /// <param name="completed">Callback invoked after the animation finishes (typically ends popup tracking).</param>
    internal void BeginCloseAnimation(Action? completed)
    {
        if (IsDisposed)
        {
            completed?.Invoke();
            return;
        }

        if (_closing)
        {
            return;
        }

        _closing = true;
        _toolTipHost.Cancel();
        StopAnimationTimer();

        var style = _owner.Values.AnimationStyle;
        var duration = _owner.Values.AnimationDuration;
        if (style == KryptonRadialMenuAnimationStyle.None || duration <= 0 || _animationProgress <= 0.0)
        {
            completed?.Invoke();
            return;
        }

        var startProgress = Math.Max(0.0, Math.Min(1.0, _animationProgress));
        var started = Environment.TickCount;
        _animationTimer = new System.Windows.Forms.Timer { Interval = RadialMenuMetrics.AnimationFrameIntervalMs };
        _animationTimer.Tick += (_, _) =>
        {
            if (IsDisposed)
            {
                StopAnimationTimer();
                completed?.Invoke();
                return;
            }

            var elapsed = Environment.TickCount - started;
            var linear = Math.Min(1.0, elapsed / (double)duration);
            _animationProgress = startProgress * (1.0 - EaseOutCubic(linear));
            Invalidate();
            if (linear >= 1.0)
            {
                _animationProgress = 0.0;
                StopAnimationTimer();
                completed?.Invoke();
            }
        };
        _animationTimer.Start();
    }

    /// <summary>
    /// Gets the visible sector items for accessibility.
    /// </summary>
    internal IReadOnlyList<KryptonRadialMenuItemBase> AccessibleSectorItems => _core.VisibleItems;

    /// <summary>
    /// Gets the current tracking sector index for accessibility.
    /// </summary>
    internal int AccessibleTrackingIndex => _core.TrackingIndex;

    #endregion

    #region Implementation

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

    private void BeginAnimation()
    {
        StopAnimationTimer();

        var style = _owner.Values.AnimationStyle;
        var duration = _owner.Values.AnimationDuration;
        if (style == KryptonRadialMenuAnimationStyle.None || duration <= 0)
        {
            _animationProgress = 1.0;
            Invalidate();
            return;
        }

        _animationProgress = 0.0;
        var started = Environment.TickCount;
        _animationTimer = new System.Windows.Forms.Timer { Interval = RadialMenuMetrics.AnimationFrameIntervalMs };
        _animationTimer.Tick += (_, _) =>
        {
            var elapsed = Environment.TickCount - started;
            var linear = Math.Min(1.0, elapsed / (double)duration);
            _animationProgress = EaseOutCubic(linear);
            Invalidate();
            if (linear >= 1.0)
            {
                _animationProgress = 1.0;
                StopAnimationTimer();
                Invalidate();
            }
        };
        _animationTimer.Start();
    }

    private void StopAnimationTimer()
    {
        if (_animationTimer == null)
        {
            return;
        }

        _animationTimer.Stop();
        _animationTimer.Dispose();
        _animationTimer = null;
    }

    private void ApplyAnimationTransform(Graphics g)
    {
        var progress = (float)_animationProgress;
        if (progress >= 0.999f)
        {
            return;
        }

        var style = _owner.Values.AnimationStyle;
        if (style == KryptonRadialMenuAnimationStyle.None)
        {
            return;
        }

        var cx = ClientSize.Width / 2f;
        var cy = ClientSize.Height / 2f;
        g.TranslateTransform(cx, cy);

        switch (style)
        {
            case KryptonRadialMenuAnimationStyle.FadeScale:
                SafeScaleTransform(g, 0.75f + (0.25f * progress));
                break;
            case KryptonRadialMenuAnimationStyle.Spiral:
                g.RotateTransform(360f * (1f - progress));
                SafeScaleTransform(g, 0.55f + (0.45f * progress));
                break;
            case KryptonRadialMenuAnimationStyle.Pop:
                SafeScaleTransform(g, EaseOutBack(progress));
                break;
            case KryptonRadialMenuAnimationStyle.Sweep:
            default:
                SafeScaleTransform(g, 0.92f + (0.08f * progress));
                break;
        }

        g.TranslateTransform(-cx, -cy);

        if (style == KryptonRadialMenuAnimationStyle.Sweep)
        {
            using var clip = new GraphicsPath();
            var diameter = Math.Max(ClientSize.Width, ClientSize.Height) * 1.2f;
            var rect = Rectangle.Round(new RectangleF(cx - (diameter / 2f), cy - (diameter / 2f), diameter, diameter));
            var sweep = Math.Max(1f, 360f * progress);
            clip.AddPie(rect, -90f, sweep);
            g.SetClip(clip);
        }
    }

    private static double EaseOutCubic(double t) => 1.0 - Math.Pow(1.0 - t, 3);

    private static float EaseOutBack(float t)
    {
        const float c1 = 1.70158f;
        const float c3 = c1 + 1f;
        var p = t - 1f;
        return 1f + (c3 * p * p * p) + (c1 * p * p);
    }

    private static void SafeScaleTransform(Graphics g, float scale)
    {
        if (float.IsNaN(scale) || float.IsInfinity(scale))
        {
            return;
        }

        scale = Math.Max(0.01f, Math.Min(scale, 8f));
        g.ScaleTransform(scale, scale);
    }

    private void FillPopupSurface(Graphics g)
    {
        if (ClientSize.Width <= 0 || ClientSize.Height <= 0)
        {
            return;
        }

        var colors = RadialMenuColorSet.FromPalette(_owner.ResolvePalette(), _owner.Values);
        using var brush = new SolidBrush(colors.SectorNormal);
        // Aliased fill avoids fringe pixels against the clipped Region edge.
        using (new GraphicsHint(g, PaletteGraphicsHint.None))
        {
            g.FillEllipse(brush, 0, 0, ClientSize.Width - 1, ClientSize.Height - 1);
        }
    }

    private void ApplyCircularRegion(Size size)
    {
        using var path = new GraphicsPath();
        path.AddEllipse(0, 0, size.Width - 1, size.Height - 1);
        Region?.Dispose();
        Region = new Region(path);
        DefineCircularShadowPaths(size);
    }

    internal void SyncShadowAppearance()
    {
        if (IsDisposed || !_owner.Values.ShowShadow)
        {
            return;
        }

        PaletteState state;
        if (!_owner.Enabled)
        {
            state = PaletteState.Disabled;
        }
        else if (_core.PressedIndex >= 0 || _moving)
        {
            state = PaletteState.Pressed;
        }
        else if (_hovering || _core.TrackingIndex >= 0 || _core.TrackingEditorIndex >= 0)
        {
            state = PaletteState.Tracking;
        }
        else
        {
            state = PaletteState.Normal;
        }

        UpdateShadowAppearance(_owner.ResolveShadowColor(state), _owner.Values.ShadowOpacity);
    }

    /// <summary>
    /// Rebuilds shadow halo geometry from current <see cref="KryptonRadialMenuValues.ShadowBlur"/> / Offset.
    /// </summary>
    internal void RefreshShadowGeometry()
    {
        if (IsDisposed || !IsHandleCreated || !Visible || !_owner.Values.ShowShadow)
        {
            return;
        }

        DefineCircularShadowPaths(ClientSize);
        var metrics = CurrentMetrics();
        ShowShadow(new Rectangle(Location, Size), metrics.ShadowPadding, metrics.ShadowOffset);
    }

    private void DefineCircularShadowPaths(Size menuSize)
    {
        if (!_owner.Values.ShowShadow)
        {
            return;
        }

        var metrics = CurrentMetrics();
        var blur = metrics.ShadowBlur;
        var pad = metrics.ShadowPadding;
        // Paths are in the padded shadow client: menu disc is at (pad, pad).
        GraphicsPath CreateOuterHalo(int outerExtra)
        {
            outerExtra = Math.Max(1, outerExtra);
            var path = new GraphicsPath { FillMode = FillMode.Alternate };
            var ox = pad - outerExtra;
            var oy = pad - outerExtra;
            path.AddEllipse(ox, oy, menuSize.Width - 1 + (outerExtra * 2), menuSize.Height - 1 + (outerExtra * 2));
            // Punch a hole slightly smaller than the menu so Magenta fringe sits under the opaque popup.
            var inset = RadialMenuMetrics.ShadowHoleInset;
            path.AddEllipse(
                pad + inset,
                pad + inset,
                Math.Max(1, menuSize.Width - 1 - (inset * 2)),
                Math.Max(1, menuSize.Height - 1 - (inset * 2)));
            return path;
        }

        var mid = Math.Max(1, (blur * 2) / 3);
        var inner = Math.Max(1, blur / 3);
        // Soft falloff: largest ring first (strongest brush in VisualPopupShadow.DrawPaths).
        DefineShadowPaths(
            CreateOuterHalo(Math.Max(1, blur)),
            CreateOuterHalo(mid),
            CreateOuterHalo(inner));
    }

    private double DistanceFromCenter(Point clientPoint)
    {
        var cx = ClientSize.Width / 2.0;
        var cy = ClientSize.Height / 2.0;
        var dx = clientPoint.X - cx;
        var dy = clientPoint.Y - cy;
        return Math.Sqrt((dx * dx) + (dy * dy));
    }

    #endregion

    #region Accessibility

    private sealed class VisualRadialMenuPopupAccessibleObject(VisualRadialMenuPopup owner)
        : ControlAccessibleObject(owner)
    {
        public override AccessibleRole Role => AccessibleRole.MenuPopup;

        public override string? Name
        {
            get => owner.AccessibleName;
            set => owner.AccessibleName = value;
        }

        public override AccessibleObject? GetChild(int index)
        {
            var items = owner.AccessibleSectorItems;
            if (index < 0 || index >= items.Count)
            {
                return null;
            }

            return new RadialSectorAccessibleObject(owner, items[index], index);
        }

        public override int GetChildCount() => owner.AccessibleSectorItems.Count;

        public override AccessibleObject? GetFocused()
        {
            var index = owner.AccessibleTrackingIndex;
            return index >= 0 ? GetChild(index) : null;
        }

        public override AccessibleObject? GetSelected() => GetFocused();
    }

    private sealed class RadialSectorAccessibleObject(
        VisualRadialMenuPopup popup,
        KryptonRadialMenuItemBase item,
        int index)
        : AccessibleObject
    {
        public override AccessibleRole Role => AccessibleRole.MenuItem;

        public override string? Name => RadialMenuInteractionCore.GetItemAccessibleName(item);

        public override AccessibleStates State
        {
            get
            {
                var state = AccessibleStates.Focusable;
                if (!item.Enabled)
                {
                    state |= AccessibleStates.Unavailable;
                }

                if (popup.AccessibleTrackingIndex == index)
                {
                    state |= AccessibleStates.Focused | AccessibleStates.Selected;
                }

                if (item is KryptonRadialMenuItem { Checked: true })
                {
                    state |= AccessibleStates.Checked;
                }

                return state;
            }
        }

        public override AccessibleObject? Parent => popup.AccessibilityObject;

        public override int GetChildCount() => 0;
    }

    #endregion
}

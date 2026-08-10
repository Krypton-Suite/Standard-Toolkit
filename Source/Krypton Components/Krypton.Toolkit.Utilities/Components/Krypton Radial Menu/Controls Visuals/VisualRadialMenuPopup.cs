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
internal class VisualRadialMenuPopup : VisualPopup
{
    #region Instance Fields

    private readonly KryptonRadialMenu _owner;
    private readonly Stack<KryptonRadialMenuItemCollection> _navigation = new Stack<KryptonRadialMenuItemCollection>();
    private KryptonRadialMenuItemCollection _currentItems;
    private KryptonRadialMenuItemBase? _activeEditor;
    private RadialSectorInfo[] _sectors = Array.Empty<RadialSectorInfo>();
    private List<KryptonRadialMenuItemBase> _visibleItems = [];
    private List<KryptonRadialMenuItemBase> _allVisibleItems = [];
    private int _trackingIndex = -1;
    private int _pressedIndex = -1;
    private bool _trackingOuterRing;
    private bool _pressedOuterRing;
    private int _trackingEditorIndex = -1;
    private int _pageOffset;
    private float _dpiScale = 1f;
    private bool _draggingSlider;
    private bool _movePending;
    private bool _moving;
    private bool _closing;
    private bool _hovering;
    private Point _moveScreenStart;
    private Point _moveLocationStart;
    private double _animationProgress = 1.0;
    private System.Windows.Forms.Timer? _animationTimer;
    private readonly RadialMenuToolTipHost _toolTipHost;

    private const int MoveDragThreshold = 8;
    private const int EditorPageSize = 8;

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
        _owner = owner ?? throw new ArgumentNullException(nameof(owner));
        _currentItems = owner.Items;
        ViewManager!.Control = this;
        ViewManager.AlignControl = this;
        ViewManager.Root = new ViewLayoutNull();

        // Outside the circular Region must not paint an opaque system colour.
        SetStyle(ControlStyles.Opaque, false);
        SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        BackColor = Color.Transparent;
        AccessibleRole = AccessibleRole.MenuPopup;
        AccessibleName = @"Radial menu";
        _toolTipHost = new RadialMenuToolTipHost(this, owner.ResolvePalette);
    }

    #endregion

    #region Public

    /// <summary>
    /// Shows the popup centred on the provided screen point.
    /// </summary>
    /// <param name="screenCenter">Screen centre point.</param>
    /// <param name="animated">Whether to run a short open animation.</param>
    public void ShowCentered(Point screenCenter, bool animated)
    {
        _navigation.Clear();
        _currentItems = _owner.Items;
        _activeEditor = null;
        _trackingIndex = -1;
        _pressedIndex = -1;
        _trackingOuterRing = false;
        _pressedOuterRing = false;
        _trackingEditorIndex = -1;
        _pageOffset = 0;
        _closing = false;
        RebuildLayout();

        var diameter = (_owner.Values.MenuRadius * 2) + 8;
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

    #endregion

    #region Protected

    /// <inheritdoc />
    protected override AccessibleObject CreateAccessibilityInstance() =>
        new VisualRadialMenuPopupAccessibleObject(this);

    /// <inheritdoc />
    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
        // Suppress default opaque background fill outside the radial artwork.
    }

    /// <inheritdoc />
    protected override void OnPaint(PaintEventArgs? e)
    {
        if (IsDisposed || e == null)
        {
            return;
        }

        var g = e.Graphics;
        var state = g.Save();
        try
        {
            ApplyAnimationTransform(g);
            g.CompositingMode = CompositingMode.SourceOver;

            var colors = RadialMenuColorSet.FromPalette(_owner.ResolvePalette(), _owner.Values);
            RadialMenuPainter.Paint(
                g,
                ClientRectangle,
                _owner.Values,
                colors,
                _visibleItems,
                _sectors,
                _trackingIndex,
                _trackingOuterRing,
                _pressedIndex,
                _pressedOuterRing,
                _navigation.Count > 0 || _activeEditor != null,
                _activeEditor != null,
                _activeEditor,
                _trackingEditorIndex,
                _owner);
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

        if (_draggingSlider && _activeEditor is KryptonRadialMenuSliderItem slider)
        {
            var center = new PointF(ClientSize.Width / 2f, ClientSize.Height / 2f);
            slider.SetNormalizedValue(RadialLayoutEngine.AngleToNormalized(e.Location, center, _owner.Values.StartAngle));
            Invalidate();
            return;
        }

        if (_movePending || _moving)
        {
            var screen = PointToScreen(e.Location);
            var dx = screen.X - _moveScreenStart.X;
            var dy = screen.Y - _moveScreenStart.Y;
            if (!_moving && ((Math.Abs(dx) >= MoveDragThreshold) || (Math.Abs(dy) >= MoveDragThreshold)))
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

        var hit = HitTest(e.Location);
        _hovering = hit.Kind != RadialHitKind.None;
        if (hit is { Kind: RadialHitKind.OuterRing, SectorIndex: >= 0 }
            && hit.SectorIndex < _visibleItems.Count
            && _visibleItems[hit.SectorIndex].HasChildren
            && _visibleItems[hit.SectorIndex].Enabled)
        {
            Cursor = Cursors.Hand;
        }
        else
        {
            Cursor = (_owner.AllowMove && hit.Kind == RadialHitKind.Center) ? Cursors.SizeAll : Cursors.Default;
        }

        UpdateToolTipHover(hit);
        var changed = false;
        if (_activeEditor != null)
        {
            if (_trackingEditorIndex != hit.EditorIndex)
            {
                _trackingEditorIndex = hit.Kind == RadialHitKind.Editor ? hit.EditorIndex : -1;
                changed = true;
            }
        }
        else
        {
            var nextIndex = hit.Kind is RadialHitKind.Sector or RadialHitKind.OuterRing ? hit.SectorIndex : -1;
            var nextOuter = hit.Kind == RadialHitKind.OuterRing;
            if (_trackingIndex != nextIndex || _trackingOuterRing != nextOuter)
            {
                _trackingIndex = nextIndex;
                _trackingOuterRing = nextOuter;
                changed = true;
                UpdateAccessibleName();
            }
        }

        if (changed)
        {
            Invalidate();
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

        var hit = HitTest(e.Location);
        if (_owner.AllowMove && hit.Kind == RadialHitKind.Center)
        {
            // Defer centre click until mouse-up; start a move if the pointer travels far enough.
            _movePending = true;
            _moving = false;
            _moveScreenStart = PointToScreen(e.Location);
            _moveLocationStart = Location;
            Cursor = Cursors.SizeAll;
            Capture = true;
        }
        else if (_activeEditor is KryptonRadialMenuSliderItem item && hit.Kind != RadialHitKind.Center)
        {
            _draggingSlider = true;
            var center = new PointF(ClientSize.Width / 2f, ClientSize.Height / 2f);
            item.SetNormalizedValue(
                RadialLayoutEngine.AngleToNormalized(e.Location, center, _owner.Values.StartAngle));
            Invalidate();
        }
        else if (hit.Kind == RadialHitKind.Sector || hit.Kind == RadialHitKind.OuterRing)
        {
            _pressedIndex = hit.SectorIndex;
            _pressedOuterRing = hit.Kind == RadialHitKind.OuterRing;
            Invalidate();
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
            if (_draggingSlider)
            {
                _draggingSlider = false;
                Invalidate();
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
                    // Reposition only — do not treat as a centre click.
                    base.OnMouseUp(e);
                    return;
                }

                // Pressed the centre without dragging: always honour the centre action.
                HandleCenterClick();
                Invalidate();
                base.OnMouseUp(e);
                return;
            }

            var hit = HitTest(e.Location);
            _pressedIndex = -1;
            _pressedOuterRing = false;

            switch (hit.Kind)
            {
                case RadialHitKind.Center:
                    HandleCenterClick();
                    break;
                case RadialHitKind.OuterRing:
                    HandleOuterRingClick(hit.SectorIndex);
                    break;
                case RadialHitKind.Sector:
                    HandleSectorBodyClick(hit.SectorIndex);
                    break;
                case RadialHitKind.Editor:
                    HandleEditorClick(hit.EditorIndex);
                    break;
            }

            Invalidate();
        }

        SyncShadowAppearance();
        base.OnMouseUp(e);
    }

    /// <inheritdoc />
    protected override void OnMouseLeave(EventArgs e)
    {
        _hovering = false;
        if (_trackingIndex != -1 || _trackingOuterRing || _trackingEditorIndex != -1)
        {
            _trackingIndex = -1;
            _trackingOuterRing = false;
            _trackingEditorIndex = -1;
            Invalidate();
        }

        SyncShadowAppearance();
        base.OnMouseLeave(e);
    }

    /// <inheritdoc />
    protected override void OnMouseWheel(MouseEventArgs e)
    {
        if (IsDisposed || _closing)
        {
            base.OnMouseWheel(e);
            return;
        }

        if (_activeEditor is KryptonRadialMenuFontListItem fonts)
        {
            fonts.ScrollOffset += e.Delta > 0 ? -1 : 1;
            Invalidate();
        }
        else if (_activeEditor is KryptonRadialMenuSliderItem slider)
        {
            slider.Value += e.Delta > 0 ? slider.SmallChange : -slider.SmallChange;
            Invalidate();
        }
        else if (_activeEditor is KryptonRadialMenuCalendarItem calendar)
        {
            calendar.ShiftMonth(e.Delta > 0 ? -1 : 1);
            Invalidate();
        }
        else if (_activeEditor == null)
        {
            var maxVisible = _owner.Values.MaxVisibleItems;
            if (maxVisible > 0 && _allVisibleItems.Count > maxVisible)
            {
                var maxOffset = _allVisibleItems.Count - maxVisible;
                var next = _pageOffset + (e.Delta > 0 ? -1 : 1);
                next = Math.Max(0, Math.Min(maxOffset, next));
                if (next != _pageOffset)
                {
                    _pageOffset = next;
                    RebuildLayout();
                    Invalidate();
                }
            }
        }

        base.OnMouseWheel(e);
    }

    /// <inheritdoc />
    protected override void OnKeyPress(KeyPressEventArgs e)
    {
        if (!IsDisposed && !_closing && _activeEditor is KryptonRadialMenuTextItem textItem)
        {
            if (!char.IsControl(e.KeyChar))
            {
                textItem.DraftText += e.KeyChar;
                Invalidate();
                e.Handled = true;
                return;
            }
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

        if (TryProcessKeyboard(keyData))
        {
            return true;
        }

        return base.ProcessDialogKey(keyData);
    }

    /// <inheritdoc />
    protected override void OnKeyDown(KeyEventArgs e)
    {
        if (!IsDisposed && !_closing && TryProcessKeyboard(e.KeyData))
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

        // Dismiss when outside the circular menu (corners of the bounding box or outside client).
        if (!ClientRectangle.Contains(pt))
        {
            return true;
        }

        return DistanceFromCenter(pt) > _owner.Values.MenuRadius;
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

        _navigation.Clear();
        _currentItems = _owner.Items;
        _activeEditor = null;
        _pageOffset = 0;
        RebuildLayout();
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

        RebuildLayout();
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
        _animationTimer = new System.Windows.Forms.Timer { Interval = 16 };
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
    internal IReadOnlyList<KryptonRadialMenuItemBase> AccessibleSectorItems => _visibleItems;

    /// <summary>
    /// Gets the current tracking sector index for accessibility.
    /// </summary>
    internal int AccessibleTrackingIndex => _trackingIndex;

    #endregion

    #region Implementation

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
        _animationTimer = new System.Windows.Forms.Timer { Interval = 16 };
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
                // EaseOutBack(0) is 0; GDI+ rejects ScaleTransform(0, 0).
                SafeScaleTransform(g, EaseOutBack(progress));
                break;
            case KryptonRadialMenuAnimationStyle.Sweep:
            default:
                // Sweep uses a clip rather than a transform; scale slightly for polish.
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

    /// <summary>
    /// Applies a scale transform, clamping to a positive finite range GDI+ accepts.
    /// </summary>
    private static void SafeScaleTransform(Graphics g, float scale)
    {
        if (float.IsNaN(scale) || float.IsInfinity(scale))
        {
            return;
        }

        // ScaleTransform throws ArgumentException for zero / non-positive factors.
        scale = Math.Max(0.01f, Math.Min(scale, 8f));
        g.ScaleTransform(scale, scale);
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
        else if (_pressedIndex >= 0 || _moving)
        {
            state = PaletteState.Pressed;
        }
        else if (_hovering || _trackingIndex >= 0 || _trackingEditorIndex >= 0)
        {
            state = PaletteState.Tracking;
        }
        else
        {
            state = PaletteState.Normal;
        }

        UpdateShadowAppearance(_owner.ResolveShadowColor(state), _owner.Values.ShadowOpacity);
    }

    private void DefineCircularShadowPaths(Size size)
    {
        if (!_owner.Values.ShowShadow)
        {
            return;
        }

        GraphicsPath CreateEllipse(int inflate)
        {
            var path = new GraphicsPath();
            path.AddEllipse(-inflate, -inflate, size.Width - 1 + (inflate * 2), size.Height - 1 + (inflate * 2));
            return path;
        }

        // Three concentric ellipses match VisualPopupShadow's three-layer draw.
        DefineShadowPaths(CreateEllipse(0), CreateEllipse(1), CreateEllipse(2));
    }

    private bool TryProcessKeyboard(Keys keyData)
    {
        switch (keyData)
        {
            case Keys.Escape:
                if (_activeEditor is KryptonRadialMenuTextItem textEsc)
                {
                    textEsc.CancelEdit();
                    _activeEditor = null;
                    _trackingEditorIndex = -1;
                    Invalidate();
                    return true;
                }

                if (_activeEditor != null || _navigation.Count > 0)
                {
                    HandleCenterClick();
                    return true;
                }

                // Root: let base dispose / dismiss.
                return false;

            case Keys.Back:
                if (_activeEditor is KryptonRadialMenuTextItem textBack)
                {
                    if (textBack.DraftText.Length > 0)
                    {
                        textBack.DraftText = textBack.DraftText.Substring(0, textBack.DraftText.Length - 1);
                        Invalidate();
                    }

                    return true;
                }

                HandleCenterClick();
                return true;

            case Keys.Enter:
            case Keys.Space:
                ActivateKeyboardSelection();
                return true;

            case Keys.Left:
            case Keys.Up:
                MoveTracking(-1);
                return true;

            case Keys.Right:
            case Keys.Down:
                MoveTracking(1);
                return true;

            case Keys.Home:
                if (_activeEditor == null && _visibleItems.Count > 0)
                {
                    _trackingIndex = 0;
                    UpdateAccessibleName();
                    Invalidate();
                }

                return true;

            case Keys.End:
                if (_activeEditor == null && _visibleItems.Count > 0)
                {
                    _trackingIndex = _visibleItems.Count - 1;
                    UpdateAccessibleName();
                    Invalidate();
                }

                return true;

            default:
                return false;
        }
    }

    private void MoveTracking(int delta)
    {
        if (_activeEditor != null)
        {
            MoveEditorTracking(delta);
            return;
        }

        if (_visibleItems.Count == 0)
        {
            return;
        }

        if (_trackingIndex < 0)
        {
            _trackingIndex = delta > 0 ? 0 : _visibleItems.Count - 1;
        }
        else
        {
            _trackingIndex = (_trackingIndex + delta + _visibleItems.Count) % _visibleItems.Count;
        }

        UpdateToolTipHover(new RadialHitResult(RadialHitKind.Sector, _trackingIndex, -1));
        UpdateAccessibleName();
        Invalidate();
    }

    private void MoveEditorTracking(int delta)
    {
        if (_activeEditor is KryptonRadialMenuCalendarItem calendar)
        {
            MoveCalendarTracking(calendar, delta);
            return;
        }

        if (_activeEditor is KryptonRadialMenuSliderItem slider)
        {
            slider.Value += delta > 0 ? slider.SmallChange : -slider.SmallChange;
            Invalidate();
            return;
        }

        var count = GetEditorSectorCount(_activeEditor);
        if (count <= 0)
        {
            return;
        }

        if (_trackingEditorIndex < 0)
        {
            _trackingEditorIndex = delta > 0 ? 0 : count - 1;
        }
        else
        {
            _trackingEditorIndex = (_trackingEditorIndex + delta + count) % count;
        }

        Invalidate();
    }

    private void MoveCalendarTracking(KryptonRadialMenuCalendarItem calendar, int delta)
    {
        var days = calendar.GetMonthDays();
        if (days.Length == 0)
        {
            return;
        }

        var offset = Math.Min(calendar.ScrollOffset, Math.Max(0, days.Length - 1));
        var count = Math.Min(EditorPageSize, days.Length - offset);
        if (count <= 0)
        {
            return;
        }

        if (_trackingEditorIndex < 0)
        {
            _trackingEditorIndex = delta > 0 ? 0 : count - 1;
            Invalidate();
            return;
        }

        var next = _trackingEditorIndex + delta;
        if (next >= count)
        {
            if (offset + count < days.Length)
            {
                calendar.ScrollOffset = offset + 1;
                _trackingEditorIndex = count - 1;
            }
            else
            {
                _trackingEditorIndex = 0;
            }
        }
        else if (next < 0)
        {
            if (offset > 0)
            {
                calendar.ScrollOffset = offset - 1;
                _trackingEditorIndex = 0;
            }
            else
            {
                _trackingEditorIndex = count - 1;
            }
        }
        else
        {
            _trackingEditorIndex = next;
        }

        Invalidate();
    }

    private void ActivateKeyboardSelection()
    {
        if (_activeEditor != null)
        {
            if (_trackingEditorIndex >= 0)
            {
                HandleEditorClick(_trackingEditorIndex);
            }
            else if (_activeEditor is KryptonRadialMenuSliderItem)
            {
                // Keep slider open until Esc / centre.
            }
            else if (_activeEditor is KryptonRadialMenuTextItem textItem)
            {
                textItem.CommitEdit();
                _activeEditor = null;
                _trackingEditorIndex = -1;
                Invalidate();
            }
            else
            {
                HandleCenterClick();
            }

            return;
        }

        if (_trackingIndex >= 0)
        {
            // Keyboard Enter still opens children/editors (ring is the pointer affordance).
            OpenChildOrEditor(_trackingIndex);
            Invalidate();
            return;
        }

        HandleCenterClick();
    }

    private void RebuildLayout()
    {
        List<KryptonRadialMenuItemBase> list = new List<KryptonRadialMenuItemBase>();
        foreach (var item in _currentItems.GetVisibleItems())
        {
            list.Add(item);
        }
        _allVisibleItems = list;
        if (RightToLeft == RightToLeft.Yes)
        {
            _allVisibleItems.Reverse();
        }

        var maxVisible = _owner.Values.MaxVisibleItems;
        if (maxVisible > 0 && _allVisibleItems.Count > maxVisible)
        {
            var maxOffset = Math.Max(0, _allVisibleItems.Count - maxVisible);
            if (_pageOffset > maxOffset)
            {
                _pageOffset = maxOffset;
            }

            if (_pageOffset < 0)
            {
                _pageOffset = 0;
            }

            List<KryptonRadialMenuItemBase> list1 = new List<KryptonRadialMenuItemBase>();
            foreach (var @base in _allVisibleItems.Skip(_pageOffset).Take(maxVisible))
            {
                list1.Add(@base);
            }
            _visibleItems = list1;
        }
        else
        {
            _pageOffset = 0;
            _visibleItems = _allVisibleItems;
        }

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

        if (_dpiScale < 0.25f)
        {
            _dpiScale = 1f;
        }

        _sectors = RadialLayoutEngine.BuildSectors(
            _visibleItems.Count,
            _owner.Values.MenuRadius,
            _owner.Values.InnerRadius,
            _owner.Values.StartAngle);
        _trackingIndex = -1;
        _pressedIndex = -1;
        _trackingOuterRing = false;
        _pressedOuterRing = false;
        _trackingEditorIndex = -1;
        _toolTipHost.Cancel();
        UpdateAccessibleName();

        // Re-run the open animation when navigating rings (submenu / back / editor exit).
        if (!_closing
            && _owner.Values.AnimationStyle != KryptonRadialMenuAnimationStyle.None
            && _owner.Values.AnimationDuration > 0
            && IsHandleCreated)
        {
            BeginAnimation();
        }
    }

    private void UpdateAccessibleName()
    {
        if (_trackingIndex >= 0 && _trackingIndex < _visibleItems.Count)
        {
            AccessibleName = GetItemAccessibleName(_visibleItems[_trackingIndex]);
        }
        else
        {
            AccessibleName = @"Radial menu";
        }
    }

    private static string GetItemAccessibleName(KryptonRadialMenuItemBase item)
    {
        var name = item switch
        {
            KryptonRadialMenuItem command => string.IsNullOrEmpty(command.ResolveText) ? command.ToString() : command.ResolveText,
            KryptonRadialMenuSliderItem slider => string.IsNullOrEmpty(slider.Text) ? slider.ToString() : slider.Text,
            KryptonRadialMenuColorPaletteItem colors => string.IsNullOrEmpty(colors.Text) ? colors.ToString() : colors.Text,
            KryptonRadialMenuFontListItem fonts => string.IsNullOrEmpty(fonts.Text) ? fonts.ToString() : fonts.Text,
            KryptonRadialMenuTextItem textItem => string.IsNullOrEmpty(textItem.Label) ? textItem.ToString() : textItem.Label,
            KryptonRadialMenuCalendarItem calendar => string.IsNullOrEmpty(calendar.Text) ? calendar.ToString() : calendar.Text,
            _ => item.ToString()
        };

        return string.IsNullOrEmpty(name) ? @"Radial menu item" : name!;
    }

    private void UpdateToolTipHover(RadialHitResult hit)
    {
        if (_moving || _movePending || _draggingSlider || _activeEditor != null || _closing)
        {
            _toolTipHost.Cancel();
            return;
        }

        if ((hit.Kind is RadialHitKind.Sector or RadialHitKind.OuterRing)
            && hit.SectorIndex >= 0
            && hit.SectorIndex < _visibleItems.Count)
        {
            _toolTipHost.UpdateHover(_visibleItems[hit.SectorIndex]);
            return;
        }

        _toolTipHost.Cancel();
    }

    private RadialHitResult HitTest(Point clientPoint)
    {
        var center = new PointF(ClientSize.Width / 2f, ClientSize.Height / 2f);
        var startAngle = _owner.Values.StartAngle;
        var hitPadding = _owner.Values.HitPadding * _dpiScale;
        var editorCount = GetEditorSectorCount(_activeEditor);
        if (_activeEditor is KryptonRadialMenuSliderItem)
        {
            // Slider uses drag anywhere in the ring; treat non-center as editor.
            var dx = clientPoint.X - center.X;
            var dy = clientPoint.Y - center.Y;
            var distance = Math.Sqrt((dx * dx) + (dy * dy));
            var inner = Math.Max(0f, _owner.Values.InnerRadius - hitPadding);
            var outer = _owner.Values.MenuRadius + hitPadding;
            if (distance <= inner)
            {
                return new RadialHitResult(RadialHitKind.Center, -1, -1);
            }

            if (distance <= outer)
            {
                return new RadialHitResult(RadialHitKind.Editor, -1, 0);
            }

            return RadialHitResult.None;
        }

        return RadialLayoutEngine.HitTest(
            clientPoint,
            center,
            _owner.Values.MenuRadius,
            _owner.Values.InnerRadius,
            _sectors,
            _activeEditor != null,
            editorCount,
            startAngle,
            hitPadding,
            _owner.Values.OuterRingThickness * _dpiScale);
    }

    private static int GetEditorSectorCount(KryptonRadialMenuItemBase? editor)
    {
        switch (editor)
        {
            case KryptonRadialMenuColorPaletteItem colors:
                return colors.Colors.Length;
            case KryptonRadialMenuFontListItem fonts:
                return Math.Min(EditorPageSize, fonts.FontFamilies.Length);
            case KryptonRadialMenuTextItem:
                return 2;
            case KryptonRadialMenuCalendarItem calendar:
            {
                var days = calendar.GetMonthDays();
                var offset = Math.Min(calendar.ScrollOffset, Math.Max(0, days.Length - 1));
                return Math.Min(EditorPageSize, Math.Max(0, days.Length - offset));
            }
            case KryptonRadialMenuSliderItem:
                return 1;
            default:
                return 0;
        }
    }

    private double DistanceFromCenter(Point clientPoint)
    {
        var cx = ClientSize.Width / 2.0;
        var cy = ClientSize.Height / 2.0;
        var dx = clientPoint.X - cx;
        var dy = clientPoint.Y - cy;
        return Math.Sqrt((dx * dx) + (dy * dy));
    }

    private void HandleCenterClick()
    {
        _toolTipHost.Cancel();
        if (_activeEditor != null)
        {
            if (_activeEditor is KryptonRadialMenuTextItem textItem)
            {
                textItem.CancelEdit();
            }

            _activeEditor = null;
            _draggingSlider = false;
            _trackingEditorIndex = -1;
            Invalidate();
            return;
        }

        if (_navigation.Count > 0)
        {
            _currentItems = _navigation.Pop();
            _pageOffset = 0;
            RebuildLayout();
            Invalidate();
            return;
        }

        _owner.OnCenterButtonClick();
        _owner.Close(ToolStripDropDownCloseReason.CloseCalled);
    }

    private void HandleOuterRingClick(int sectorIndex)
    {
        // Outer-ring band is the only pointer path that opens a child ring / editor.
        OpenChildOrEditor(sectorIndex);
    }

    private void HandleSectorBodyClick(int sectorIndex)
    {
        _toolTipHost.Cancel();
        if (sectorIndex < 0 || sectorIndex >= _visibleItems.Count)
        {
            return;
        }

        var item = _visibleItems[sectorIndex];
        if (!item.Enabled)
        {
            return;
        }

        // Parents / editors: body click raises ItemClick but does not drill in.
        if (item.HasChildren)
        {
            _owner.RaiseItemClick(item);
            return;
        }

        _owner.RaiseItemClick(item);
        if (item is KryptonRadialMenuItem commandItem)
        {
            commandItem.PerformClick();
            if (commandItem.AutoClose)
            {
                _owner.Close(ToolStripDropDownCloseReason.ItemClicked);
            }
        }
    }

    private void OpenChildOrEditor(int sectorIndex)
    {
        _toolTipHost.Cancel();
        if (sectorIndex < 0 || sectorIndex >= _visibleItems.Count)
        {
            return;
        }

        var item = _visibleItems[sectorIndex];
        if (!item.Enabled)
        {
            return;
        }

        _owner.RaiseItemClick(item);

        switch (item)
        {
            case KryptonRadialMenuItem { HasChildren: true } commandItem:
                _navigation.Push(_currentItems);
                _currentItems = commandItem.Items;
                _pageOffset = 0;
                RebuildLayout();
                break;
            case KryptonRadialMenuItem commandItem:
                commandItem.PerformClick();
                if (commandItem.AutoClose)
                {
                    _owner.Close(ToolStripDropDownCloseReason.ItemClicked);
                }
                break;
            case KryptonRadialMenuSliderItem:
            case KryptonRadialMenuColorPaletteItem:
            case KryptonRadialMenuFontListItem:
                _activeEditor = item;
                _trackingEditorIndex = -1;
                break;
            case KryptonRadialMenuTextItem textItem:
                textItem.BeginEdit();
                _activeEditor = textItem;
                _trackingEditorIndex = -1;
                break;
            case KryptonRadialMenuCalendarItem calendarItem:
                _activeEditor = calendarItem;
                _trackingEditorIndex = -1;
                break;
        }
    }

    private void HandleEditorClick(int editorIndex)
    {
        switch (_activeEditor)
        {
            case KryptonRadialMenuColorPaletteItem colors:
                if (editorIndex >= 0 && editorIndex < colors.Colors.Length)
                {
                    colors.SelectedColor = colors.Colors[editorIndex];
                    _activeEditor = null;
                    _owner.Close(ToolStripDropDownCloseReason.ItemClicked);
                }
                break;
            case KryptonRadialMenuFontListItem fonts:
                var families = fonts.FontFamilies;
                if (families.Length == 0)
                {
                    break;
                }

                var visible = Math.Min(EditorPageSize, families.Length);
                if (editorIndex >= 0 && editorIndex < visible)
                {
                    var familyIndex = (fonts.ScrollOffset + editorIndex) % families.Length;
                    fonts.SelectFamily(families[familyIndex]);
                    _activeEditor = null;
                    _owner.Close(ToolStripDropDownCloseReason.ItemClicked);
                }
                break;
            case KryptonRadialMenuTextItem textItem:
                if (editorIndex == 0)
                {
                    textItem.CancelEdit();
                    _activeEditor = null;
                    _trackingEditorIndex = -1;
                }
                else if (editorIndex == 1)
                {
                    textItem.CommitEdit();
                    _activeEditor = null;
                    _trackingEditorIndex = -1;
                }
                break;
            case KryptonRadialMenuCalendarItem calendarItem:
            {
                var days = calendarItem.GetMonthDays();
                var offset = Math.Min(calendarItem.ScrollOffset, Math.Max(0, days.Length - 1));
                var count = Math.Min(EditorPageSize, days.Length - offset);
                if (editorIndex >= 0 && editorIndex < count)
                {
                    calendarItem.SelectedDate = days[offset + editorIndex];
                    _activeEditor = null;
                    _trackingEditorIndex = -1;
                    _owner.Close(ToolStripDropDownCloseReason.ItemClicked);
                }
                break;
            }
        }
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

        public override string? Name => GetItemAccessibleName(item);

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

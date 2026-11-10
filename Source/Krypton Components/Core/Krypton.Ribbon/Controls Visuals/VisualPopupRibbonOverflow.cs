#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Popup of the compact commands that did not fit on the row, still grouped.
/// </summary>
internal class VisualPopupRibbonOverflow : VisualPopup
{
    #region Instance Fields
    private const int ANIMATION_DURATION_MS = 180;
    private const int ANIMATION_INTERVAL_MS = 15;

    private readonly KryptonRibbon _ribbon;
    private readonly ViewLayoutRibbonOverflowPopup _root;
    private readonly List<ViewDrawRibbonGroup> _groupViews;
    private System.Windows.Forms.Timer? _animationTimer;
    private Point _fullOrigin;
    private Size _fullSize;
    private int _animationFromHeight;
    private int _animationToHeight;
    private int _animationStarted;
    private bool _openUpward;
    private bool _prepared;
    private bool _closeRequested;
    private bool _disposingNow;
    private bool _seenMouseDown;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the <see cref="VisualPopupRibbonOverflow"/> class.
    /// </summary>
    /// <param name="ribbon">Owning ribbon control.</param>
    /// <param name="ribbonTab">Tab whose overflow is shown.</param>
    /// <param name="renderer">Drawing renderer.</param>
    public VisualPopupRibbonOverflow(KryptonRibbon ribbon,
        KryptonRibbonTab ribbonTab,
        IRenderer renderer)
        : base(renderer, true)
    {
        _ribbon = ribbon;
        RibbonRtlLayout.ApplyTo(this, _ribbon);
        _groupViews = new List<ViewDrawRibbonGroup>();

        foreach (KryptonRibbonGroup group in CompactRibbonOverflow.GroupsWithOverflow(ribbonTab, ribbon))
        {
            // The group view constructor claims GroupView. Put the ribbon's view back.
            ViewBase previous = group.GroupView;
            var view = new ViewDrawRibbonGroup(ribbon, group, NeedPaintDelegate, true)
            {
                // The launcher stays on the row when part of the group is still there.
                HideDialogLauncher = CompactRibbonOverflow.HasRemaining(group, ribbon)
            };
            group.GroupView = previous;

            _groupViews.Add(view);
        }

        _root = new ViewLayoutRibbonOverflowPopup(ribbon, _groupViews);
        ViewManager = new ViewManager(this, _root);
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        // The tracking manager disposes the popup immediately. Ease the height shut first.
        if (disposing && !_disposingNow && !_closeRequested && CanAnimateClose())
        {
            _closeRequested = true;
            BeginAnimation(Height, 1);
            return;
        }

        if (disposing && _closeRequested && !_disposingNow && (_animationTimer != null) && _animationTimer.Enabled)
        {
            return;
        }

        _disposingNow = true;
        if (_animationTimer != null)
        {
            _animationTimer.Stop();
            _animationTimer.Tick -= OnAnimationTick;
            _animationTimer.Dispose();
            _animationTimer = null;
        }

        if (disposing)
        {
            ViewManager?.MouseLeave(EventArgs.Empty);
            for (var i = Controls.Count - 1; i >= 0; i--)
            {
                Controls.RemoveAt(0);
            }

            foreach (ViewDrawRibbonGroup view in _groupViews)
            {
                view.Dispose();
            }

            _groupViews.Clear();
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Public
    /// <summary>
    /// Show the popup under the ellipsis.
    /// </summary>
    /// <param name="parentScreenRect">Screen rectangle of the ellipsis.</param>
    public void ShowCalculatingSize(Rectangle parentScreenRect)
    {
        Size popupSize;
        using (var context = new ViewLayoutContext(this, Renderer))
        {
            popupSize = _root.GetPreferredSize(context);
        }

        Rectangle area = Screen.FromControl(_ribbon).WorkingArea;
        popupSize.Width = Math.Min(Math.Max(popupSize.Width, 16), Math.Max(16, area.Width - 8));
        popupSize.Height = Math.Min(Math.Max(popupSize.Height, 16), Math.Max(16, area.Height - 8));

        int x = RibbonRtlLayout.IsRtl(_ribbon)
            ? parentScreenRect.Right - popupSize.Width
            : parentScreenRect.Left;
        int y = parentScreenRect.Bottom + 1;
        if (x + popupSize.Width > area.Right)
        {
            x = Math.Max(area.Left, area.Right - popupSize.Width);
        }

        if (x < area.Left)
        {
            x = area.Left;
        }

        if (y + popupSize.Height > area.Bottom)
        {
            y = Math.Max(area.Top, parentScreenRect.Top - popupSize.Height);
        }

        _fullOrigin = new Point(x, y);
        _fullSize = popupSize;
        _openUpward = y < parentScreenRect.Top;
        _prepared = true;

        if (!_ribbon.AnimateCompactMode)
        {
            _root.RevealOffset = 0;
            Show(_fullOrigin, _fullSize);
            return;
        }

        // Grow from the edge nearest the ellipsis. Closing eases back to that edge.
        _root.RevealOffset = _openUpward ? popupSize.Height - 1 : 0;
        int startY = _openUpward ? y + popupSize.Height - 1 : y;
        Show(new Point(x, startY), new Size(popupSize.Width, 1));
        BeginAnimation(1, popupSize.Height);
    }
    #endregion

    #region Protected
    /// <summary>
    /// Raises the MouseDown event.
    /// </summary>
    /// <param name="e">A MouseEventArgs that contains the event data.</param>
    protected override void OnMouseDown(MouseEventArgs e)
    {
        _seenMouseDown = true;
        base.OnMouseDown(e);
    }

    /// <summary>
    /// Raises the MouseUp event.
    /// </summary>
    /// <param name="e">A MouseEventArgs that contains the event data.</param>
    protected override void OnMouseUp(MouseEventArgs e)
    {
        base.OnMouseUp(e);

        // Ignore the mouse up that follows the ellipsis press, or the popup would close immediately.
        // A command click is a down then up on the popup. That click has already run.
        if (!_seenMouseDown ||
            (e.Button != MouseButtons.Left) ||
            IsDisposed ||
            !ReferenceEquals(VisualPopupManager.Singleton.CurrentPopup, this))
        {
            return;
        }

        VisualPopupManager.Singleton.EndPopupTracking(this);
    }

    private bool CanAnimateClose() =>
        _prepared && _ribbon.AnimateCompactMode && IsHandleCreated && !IsDisposed && (Height > 1);

    private void BeginAnimation(int fromHeight, int toHeight)
    {
        _animationFromHeight = fromHeight;
        _animationToHeight = toHeight;
        _animationStarted = Environment.TickCount;

        if (_animationTimer == null)
        {
            _animationTimer = new System.Windows.Forms.Timer
            {
                Interval = ANIMATION_INTERVAL_MS
            };
            _animationTimer.Tick += OnAnimationTick;
        }

        _animationTimer.Start();
    }

    private void OnAnimationTick(object? sender, EventArgs e)
    {
        if (_disposingNow || IsDisposed)
        {
            return;
        }

        int elapsed = unchecked(Environment.TickCount - _animationStarted);
        bool finished = elapsed >= ANIMATION_DURATION_MS;
        float t = finished ? 1f : elapsed / (float)ANIMATION_DURATION_MS;
        float eased = 1f - ((1f - t) * (1f - t) * (1f - t));
        int height = _animationFromHeight + (int)Math.Round((_animationToHeight - _animationFromHeight) * eased);
        ApplyFrame(height);

        if (!finished)
        {
            return;
        }

        if (_animationTimer != null)
        {
            _animationTimer.Stop();
        }

        if (_closeRequested && IsHandleCreated)
        {
            BeginInvoke((Action)delegate { Dispose(); });
        }
    }

    private void ApplyFrame(int height)
    {
        height = Math.Max(1, Math.Min(height, Math.Max(1, _fullSize.Height)));
        int y = _openUpward ? _fullOrigin.Y + _fullSize.Height - height : _fullOrigin.Y;
        _root.RevealOffset = _openUpward ? _fullSize.Height - height : 0;
        var rect = new Rectangle(_fullOrigin.X, y, _fullSize.Width, height);
        SetBounds(rect.X, rect.Y, rect.Width, rect.Height);
        ShowShadow(rect, 0);
    }

    /// <summary>
    /// Raises the Layout event.
    /// </summary>
    /// <param name="levent">An EventArgs that contains the event data.</param>
    protected override void OnLayout(LayoutEventArgs levent)
    {
        base.OnLayout(levent);
        using GraphicsPath roundPath = CommonHelper.RoundedRectanglePath(ClientRectangle, 2);
        Region = new Region(roundPath);
    }
    #endregion
}

/// <summary>
/// Stacks one compact row for each overflowed group.
/// </summary>
internal class ViewLayoutRibbonOverflowPopup : ViewComposite
{
    #region Instance Fields
    private readonly KryptonRibbon _ribbon;
    private readonly List<int> _separatorY;
    #endregion

    #region Public
    /// <summary>
    /// Pixels of content kept above the window when the popup opens upward.
    /// </summary>
    public int RevealOffset { get; set; }
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the <see cref="ViewLayoutRibbonOverflowPopup"/> class.
    /// </summary>
    /// <param name="ribbon">Owning ribbon control.</param>
    /// <param name="groups">Group views that show only overflowed commands.</param>
    public ViewLayoutRibbonOverflowPopup(KryptonRibbon ribbon, IList<ViewDrawRibbonGroup> groups)
    {
        _ribbon = ribbon;
        _separatorY = new List<int>();
        foreach (ViewDrawRibbonGroup group in groups)
        {
            Add(group);
        }
    }
    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize(ViewLayoutContext context)
    {
        float dpi = _ribbon.DeviceDpi / 96f;
        int pad = Math.Max(4, (int)(6 * dpi));
        int gap = Math.Max(3, (int)(5 * dpi));
        var preferred = new Size(pad * 2, pad * 2);
        var any = false;
        foreach (ViewBase child in this)
        {
            if (!child.Visible)
            {
                continue;
            }

            Size childSize = child.GetPreferredSize(context);
            preferred.Width = Math.Max(preferred.Width, childSize.Width + (pad * 2));
            if (any)
            {
                preferred.Height += gap;
            }

            preferred.Height += childSize.Height;
            any = true;
        }

        return preferred;
    }

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        ClientRectangle = context.DisplayRectangle;
        _separatorY.Clear();
        float dpi = _ribbon.DeviceDpi / 96f;
        int pad = Math.Max(4, (int)(6 * dpi));
        int gap = Math.Max(3, (int)(5 * dpi));
        int y = ClientRectangle.Y + pad - RevealOffset;
        int width = Math.Max(0, ClientRectangle.Width - (pad * 2));
        var any = false;
        foreach (ViewBase child in this)
        {
            if (!child.Visible)
            {
                continue;
            }

            Size childSize = child.GetPreferredSize(context);
            if (any)
            {
                _separatorY.Add(y + (gap / 2));
                y += gap;
            }

            context.DisplayRectangle = new Rectangle(ClientRectangle.X + pad, y, width, childSize.Height);
            child.Layout(context);
            y += childSize.Height;
            any = true;
        }

        context.DisplayRectangle = ClientRectangle;
    }
    #endregion

    #region Paint
    /// <summary>
    /// Perform rendering before child elements are rendered.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public override void RenderBefore(RenderContext context)
    {
        IPaletteRibbonBack area = _ribbon.StateCheckedNormal.RibbonGroupArea;
        Color back = area.GetRibbonBackColor1(PaletteState.Normal);
        if (back.IsEmpty || (back.A == 0))
        {
            back = Color.White;
        }

        using var brush = new SolidBrush(back);
        context.Graphics.FillRectangle(brush, ClientRectangle);
    }

    /// <summary>
    /// Perform rendering after child elements are rendered.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public override void RenderAfter(RenderContext context)
    {
        if (_separatorY.Count == 0)
        {
            return;
        }

        Color color = _ribbon.StateCommon.RibbonGeneral.GetRibbonGroupSeparatorDark(PaletteState.Normal);
        if (color.IsEmpty || (color.A == 0))
        {
            color = Color.FromArgb(160, 128, 128, 128);
        }

        float dpi = _ribbon.DeviceDpi / 96f;
        int inset = Math.Max(6, (int)(10 * dpi));
        using var pen = new Pen(color);
        foreach (int y in _separatorY)
        {
            context.Graphics.DrawLine(pen, ClientRectangle.X + inset, y, ClientRectangle.Right - inset, y);
        }
    }
    #endregion
}

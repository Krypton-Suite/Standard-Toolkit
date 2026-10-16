#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

public partial class KryptonForm
{
    #region Instance Fields

    private const int CaptionTextBoxHorizontalGap = 4;
    private const int CaptionTextBoxVerticalGap = 2;

    private readonly List<CaptionEntry> _captionEntries = new List<CaptionEntry>();
    private Control? _captionShortcut;
    private FormCaptionTextBoxEdge _captionTextBoxEdge = FormCaptionTextBoxEdge.Right;
    private bool _captionTextBoxApplyingBounds;
    private bool _captionTextBoxSyncing;
    private bool _captionOrderDirty;
    private Form? _captionTextBoxMdiHook;

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the control assigned through this property.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The caption is non-client chrome, so each control is parented to its own borderless owned window
    /// over a reserved slot. Assign <c>null</c> to remove only this control. Other caption controls stay.
    /// Each control keeps the edge given to <see cref="AddCaptionControl"/> or <see cref="SetCaptionControlEdge"/>.
    /// </para>
    /// <para>
    /// Width stays on the control. The height is clamped to the caption.
    /// <see cref="FormBorderStyle.None"/> and a hidden caption leave every control as a normal child.
    /// A <see cref="Form"/> cannot be hosted.
    /// </para>
    /// </remarks>
    [Category(@"Visuals")]
    [Description(@"Control assigned to the caption. Other caption controls are unchanged.")]
    [DefaultValue(null)]
    [RefreshProperties(RefreshProperties.All)]
    public Control? CaptionControl
    {
        get => FindCaptionEntry(_captionShortcut) == null ? null : _captionShortcut;

        set
        {
            if (ReferenceEquals(CaptionControl, value) || !IsCaptionControlAllowed(value))
            {
                return;
            }

            if (_captionShortcut != null && !ReferenceEquals(_captionShortcut, value))
            {
                RemoveCaptionControl(_captionShortcut);
            }

            _captionShortcut = value;
            if (value != null)
            {
                AddCaptionControl(value, _captionTextBoxEdge);
            }
        }
    }

    /// <summary>
    /// Gets or sets the <see cref="KryptonTextBox"/> assigned through <see cref="CaptionControl"/>.
    /// </summary>
    /// <remarks>
    /// Returns the shortcut when it is a <see cref="KryptonTextBox"/>, otherwise the first hosted text box.
    /// Assigning a text box adds it on <see cref="CaptionTextBoxEdge"/> and does not move the other caption controls.
    /// Assigning <c>null</c> removes that text box only. <c>KryptonSearchBox</c> can be assigned because it derives from
    /// <see cref="KryptonTextBox"/>.
    /// </remarks>
    [Category(@"Visuals")]
    [Description(@"Text box in the caption. KryptonSearchBox can be assigned. Other caption controls stay.")]
    [DefaultValue(null)]
    [RefreshProperties(RefreshProperties.All)]
    public KryptonTextBox? CaptionTextBox
    {
        get
        {
            if (_captionShortcut is KryptonTextBox shortcut && FindCaptionEntry(shortcut) != null)
            {
                return shortcut;
            }

            foreach (CaptionEntry entry in _captionEntries)
            {
                if (entry.Control is KryptonTextBox textBox)
                {
                    return textBox;
                }
            }

            return null;
        }

        set
        {
            if (value == null)
            {
                KryptonTextBox? current = CaptionTextBox;
                if (current != null)
                {
                    RemoveCaptionControl(current);
                }

                return;
            }

            _captionShortcut = value;
            AddCaptionControl(value, _captionTextBoxEdge);
        }
    }

    /// <summary>
    /// <see langword="true"/> when the designer should write <see cref="CaptionTextBox"/>.
    /// </summary>
    /// <returns><see langword="true"/> when the shortcut control is a <see cref="KryptonTextBox"/>.</returns>
    public bool ShouldSerializeCaptionTextBox() => _captionShortcut is KryptonTextBox && FindCaptionEntry(_captionShortcut) != null;

    /// <summary>
    /// Clears <see cref="CaptionTextBox"/>.
    /// </summary>
    public void ResetCaptionTextBox() => CaptionTextBox = null;

    /// <summary>
    /// <see langword="true"/> when the designer should write <see cref="CaptionControl"/>.
    /// </summary>
    /// <returns><see langword="true"/> when the shortcut control is not a <see cref="KryptonTextBox"/>.</returns>
    public bool ShouldSerializeCaptionControl() =>
        _captionShortcut != null && !(_captionShortcut is KryptonTextBox) && FindCaptionEntry(_captionShortcut) != null;

    /// <summary>
    /// Clears <see cref="CaptionControl"/>.
    /// </summary>
    public void ResetCaptionControl() => CaptionControl = null;

    /// <summary>
    /// Gets or sets the edge of the control assigned through <see cref="CaptionControl"/> or <see cref="CaptionTextBox"/>.
    /// </summary>
    /// <remarks>
    /// This edge belongs to that one control. Controls added with <see cref="AddCaptionControl"/> keep their own edges.
    /// <see cref="FormCaptionTextBoxEdge.Right"/> places it before the window buttons.
    /// <see cref="FormCaptionTextBoxEdge.Left"/> places it after left-docked title-bar button specs and before the icon and title.
    /// <see cref="FormCaptionTextBoxEdge.Center"/> places it in the centre of the remaining caption, and the title keeps the near side.
    /// Several controls on one edge stack, with the earlier control nearer the window edge.
    /// </remarks>
    [Category(@"Visuals")]
    [Description(@"Edge of the shortcut caption control. Each caption control has its own edge.")]
    [DefaultValue(FormCaptionTextBoxEdge.Right)]
    public FormCaptionTextBoxEdge CaptionTextBoxEdge
    {
        get
        {
            CaptionEntry? entry = FindCaptionEntry(_captionShortcut);
            return entry == null ? _captionTextBoxEdge : entry.Edge;
        }

        set
        {
            if (_captionTextBoxEdge == value && (FindCaptionEntry(_captionShortcut) == null || FindCaptionEntry(_captionShortcut)!.Edge == value))
            {
                return;
            }

            _captionTextBoxEdge = value;
            if (_captionShortcut != null && FindCaptionEntry(_captionShortcut) != null)
            {
                SetCaptionControlEdge(_captionShortcut, value);
            }
        }
    }

    /// <summary>
    /// Adds <paramref name="control"/> to the caption on <paramref name="edge"/>.
    /// </summary>
    /// <param name="control">Control to host. A form is ignored.</param>
    /// <param name="edge">Edge reserved for this control. Other caption controls are not moved.</param>
    public void AddCaptionControl(Control control, FormCaptionTextBoxEdge edge)
    {
        if (!IsCaptionControlAllowed(control))
        {
            return;
        }

        CaptionEntry? entry = FindCaptionEntry(control);
        if (entry == null)
        {
            entry = new CaptionEntry(control);
            AttachCaptionEntry(entry);
            _captionEntries.Add(entry);
        }

        entry.Edge = edge;
        _captionOrderDirty = true;
        if (ReferenceEquals(_captionShortcut, control))
        {
            _captionTextBoxEdge = edge;
        }

        PerformNeedPaint(true);
        SyncCaptionTextBoxHost();
    }

    /// <summary>
    /// Removes <paramref name="control"/> from the caption and restores its previous parent.
    /// </summary>
    /// <param name="control">Control previously added to the caption.</param>
    public void RemoveCaptionControl(Control? control)
    {
        CaptionEntry? entry = FindCaptionEntry(control);
        if (entry == null)
        {
            return;
        }

        DetachCaptionEntry(entry);
        _captionEntries.Remove(entry);
        if (ReferenceEquals(_captionShortcut, control))
        {
            _captionShortcut = null;
        }

        _captionOrderDirty = true;
        PerformNeedPaint(true);
        SyncCaptionTextBoxHost();
    }

    /// <summary>
    /// Moves a caption control to <paramref name="edge"/> without moving the others.
    /// </summary>
    /// <param name="control">Control already in the caption, or a control to add.</param>
    /// <param name="edge">Edge reserved for this control.</param>
    public void SetCaptionControlEdge(Control control, FormCaptionTextBoxEdge edge)
    {
        if (FindCaptionEntry(control) == null)
        {
            AddCaptionControl(control, edge);
            return;
        }

        CaptionEntry entry = FindCaptionEntry(control)!;
        if (entry.Edge == edge)
        {
            return;
        }

        entry.Edge = edge;
        _captionOrderDirty = true;
        if (ReferenceEquals(_captionShortcut, control))
        {
            _captionTextBoxEdge = edge;
        }

        PerformNeedPaint(true);
        SyncCaptionTextBoxHost();
    }

    /// <summary>
    /// Gets the edge of a control hosted in the caption.
    /// </summary>
    /// <param name="control">Control to look up.</param>
    /// <param name="edge">Receives the control's edge when this method returns <see langword="true"/>.</param>
    /// <returns><see langword="true"/> when <paramref name="control"/> is in the caption.</returns>
    public bool TryGetCaptionControlEdge(Control? control, out FormCaptionTextBoxEdge edge)
    {
        CaptionEntry? entry = FindCaptionEntry(control);
        if (entry == null)
        {
            edge = FormCaptionTextBoxEdge.Right;
            return false;
        }

        edge = entry.Edge;
        return true;
    }

    #endregion

    #region Caption Text Box

    /// <summary>
    /// Reserves caption space for each hosted control before the heading is laid out.
    /// </summary>
    /// <returns><see langword="true"/> when a slot size or edge changed and a layout is required.</returns>
    private bool PrepareCaptionTextBoxSlot()
    {
        if (!CanHostCaptionChrome())
        {
            return RemoveAllCaptionSlots();
        }

        var changed = false;
        if (_captionOrderDirty)
        {
            changed = RebuildCaptionSlots();
            _captionOrderDirty = false;
        }

        foreach (CaptionEntry entry in _captionEntries)
        {
            if (entry.Edge == FormCaptionTextBoxEdge.Center)
            {
                continue;
            }

            var size = new Size(CaptionSlotWidth(entry), 1);
            if (entry.Slot.SeparatorSize != size)
            {
                entry.Slot.SeparatorSize = size;
                changed = true;
            }

            if (!entry.Slot.Visible)
            {
                entry.Slot.Visible = true;
                changed = true;
            }
        }

        return changed;
    }

    /// <summary>
    /// Centres every centre-aligned caption control and keeps the title on the near side of that group.
    /// </summary>
    /// <param name="context">Context from the heading layout that just completed.</param>
    private void PlaceCaptionTextBoxCenter(ViewLayoutContext context)
    {
        foreach (CaptionEntry entry in _captionEntries)
        {
            if (entry.Edge != FormCaptionTextBoxEdge.Center)
            {
                entry.CenterRect = Rectangle.Empty;
            }
        }

        if (!CanHostCaptionChrome())
        {
            return;
        }

        var centers = new List<CaptionEntry>();
        var total = 0;
        foreach (CaptionEntry entry in _captionEntries)
        {
            if (entry.Edge != FormCaptionTextBoxEdge.Center || entry.Control.IsDisposed)
            {
                continue;
            }

            centers.Add(entry);
            total += CaptionSlotWidth(entry);
        }

        if (centers.Count == 0)
        {
            return;
        }

        var fill = _drawHeading.FillRectangle;
        if (fill.Width < 16 || fill.Height < 8)
        {
            foreach (CaptionEntry entry in centers)
            {
                entry.CenterRect = Rectangle.Empty;
            }

            return;
        }

        var groupWidth = Math.Min(total, fill.Width);
        var groupLeft = fill.X + Math.Max(0, (fill.Width - groupWidth) / 2);
        var x = groupLeft;
        foreach (CaptionEntry entry in centers)
        {
            var width = Math.Min(CaptionSlotWidth(entry), Math.Max(0, groupLeft + groupWidth - x));
            entry.CenterRect = new Rectangle(x, fill.Y, width, fill.Height);
            x += width;
        }

        // The fill already spanned the whole remainder. Lay it out again on the near side
        // so the icon, title, and integrated ribbon tabs stop at the centre group.
        var nearIsRight = CommonHelper.IsRightToLeftLayout(this);
        var titleRect = nearIsRight
            ? new Rectangle(groupLeft + groupWidth, fill.Y, Math.Max(0, fill.Right - (groupLeft + groupWidth)), fill.Height)
            : new Rectangle(fill.X, fill.Y, Math.Max(0, groupLeft - fill.X), fill.Height);
        var previous = context.DisplayRectangle;
        foreach (ViewBase child in _drawHeading)
        {
            if (child.Visible && _drawHeading.GetDock(child) == ViewDockStyle.Fill)
            {
                context.DisplayRectangle = titleRect;
                child.Layout(context);
            }
        }

        context.DisplayRectangle = previous;
    }

    /// <summary>
    /// Moves each owned window onto its laid-out caption slot.
    /// </summary>
    private void SyncCaptionTextBoxHost()
    {
        if (_captionTextBoxSyncing || IsDisposed || Disposing)
        {
            return;
        }

        _captionTextBoxSyncing = true;
        try
        {
            SyncCaptionTextBoxMdiHook();

            if (!CanHostCaptionChrome())
            {
                RestoreAllCaptionControls();
                HideAllCaptionHosts();
                return;
            }

            if (!IsHandleCreated || !Visible || WindowState == FormWindowState.Minimized || !IsActiveCaptionTextBoxMdiChild())
            {
                HideAllCaptionHosts();
                return;
            }

            var windowRect = new PI.RECT();
            if (!PI.GetWindowRect(Handle, ref windowRect))
            {
                HideAllCaptionHosts();
                return;
            }

            foreach (CaptionEntry entry in _captionEntries.ToArray())
            {
                SyncCaptionEntry(entry, windowRect);
            }
        }
        finally
        {
            _captionTextBoxSyncing = false;
        }
    }

    /// <summary>
    /// <see langword="true"/> when <paramref name="windowPoint"/> is over a caption control slot
    /// and not on the top resize band.
    /// </summary>
    /// <param name="windowPoint">Point in window coordinates.</param>
    private bool HitTestCaptionTextBox(Point windowPoint)
    {
        var hit = false;
        foreach (CaptionEntry entry in _captionEntries)
        {
            if (CaptionSlotBounds(entry).Contains(windowPoint))
            {
                hit = true;
                break;
            }
        }

        if (!hit)
        {
            return false;
        }

        Padding borders = RealWindowBorders;
        var topBand = borders.Top > borders.Left ? borders.Left : borders.Top;
        return topBand <= 0 || windowPoint.Y > topBand;
    }

    /// <summary>
    /// Keeps the caption painted active while a caption control has focus and this form, or its host, is active.
    /// </summary>
    private void KeepCaptionActiveForTextBox()
    {
        if (CaptionTextBoxKeepsChromeActive())
        {
            WindowActive = true;
        }
    }

    private void DisposeCaptionTextBoxHost()
    {
        UnhookCaptionTextBoxMdi();
        foreach (CaptionEntry entry in _captionEntries.ToArray())
        {
            DetachCaptionEntry(entry);
        }

        _captionEntries.Clear();
        _captionShortcut = null;
    }

    private bool CanHostCaptionChrome() =>
        !CommonHelper.DesignMode()
        && !DesignMode
        && !IsDisposed
        && !Disposing
        && FormBorderStyle != FormBorderStyle.None
        && !ShouldHideCaption()
        && base.UseThemeFormChromeBorderWidth
        && UseThemeFormChromeBorderWidth;

    private bool IsActiveCaptionTextBoxMdiChild() =>
        MdiParent == null || MdiParent.ActiveMdiChild == this;

    private bool CaptionTextBoxKeepsChromeActive()
    {
        Form? active = ActiveForm;
        foreach (CaptionEntry entry in _captionEntries)
        {
            if (entry.Control.IsDisposed || !entry.Control.ContainsFocus)
            {
                continue;
            }

            if (active == this || active == entry.Host)
            {
                return true;
            }
        }

        return false;
    }

    private bool IsCaptionControlAllowed(Control? control)
    {
        if (control == null)
        {
            return true;
        }

        if (control.IsDisposed || control == this || control is Form)
        {
            return false;
        }

        for (Control? ancestor = Parent; ancestor != null; ancestor = ancestor.Parent)
        {
            if (ancestor == control)
            {
                return false;
            }
        }

        return true;
    }

    private CaptionEntry? FindCaptionEntry(Control? control)
    {
        if (control == null)
        {
            return null;
        }

        foreach (CaptionEntry entry in _captionEntries)
        {
            if (ReferenceEquals(entry.Control, control))
            {
                return entry;
            }
        }

        return null;
    }

    private int CaptionSlotWidth(CaptionEntry entry) =>
        Math.Max(1, entry.NaturalWidth + (CaptionTextBoxHorizontalGap * 2));

    private Rectangle CaptionSlotBounds(CaptionEntry entry)
    {
        if (entry.Edge == FormCaptionTextBoxEdge.Center)
        {
            return entry.CenterRect;
        }

        return entry.Slot.Parent == _drawHeading && entry.Slot.Visible
            ? entry.Slot.ClientRectangle
            : Rectangle.Empty;
    }

    private bool RebuildCaptionSlots()
    {
        var changed = RemoveAllCaptionSlots();
        foreach (CaptionEntry entry in _captionEntries)
        {
            if (entry.Edge == FormCaptionTextBoxEdge.Center || entry.Control.IsDisposed)
            {
                continue;
            }

            // Inserting at 0 in add order puts the earlier control nearer the window edge.
            entry.Slot.Visible = true;
            entry.Slot.SeparatorSize = new Size(CaptionSlotWidth(entry), 1);
            _drawHeading.Insert(0, entry.Slot);
            _drawHeading.SetDock(entry.Slot, entry.Edge == FormCaptionTextBoxEdge.Left
                ? ViewDockStyle.Left
                : ViewDockStyle.Right);
            changed = true;
        }

        return changed;
    }

    private bool RemoveAllCaptionSlots()
    {
        var removed = false;
        foreach (CaptionEntry entry in _captionEntries)
        {
            if (entry.Slot.Parent is ViewComposite parent)
            {
                parent.Remove(entry.Slot);
                removed = true;
            }

            if (entry.Slot.Visible)
            {
                entry.Slot.Visible = false;
                removed = true;
            }

            entry.CenterRect = Rectangle.Empty;
        }

        return removed;
    }

    private void SyncCaptionEntry(CaptionEntry entry, PI.RECT windowRect)
    {
        if (entry.Control.IsDisposed)
        {
            return;
        }

        EnsureCaptionEntryHosted(entry);
        var slot = CaptionSlotBounds(entry);
        slot.Intersect(RealWindowRectangle);
        if (slot.Width < 16 || slot.Height < 8)
        {
            HideCaptionHost(entry);
            return;
        }

        var availableHeight = Math.Max(0, slot.Height - (CaptionTextBoxVerticalGap * 2));
        var height = Math.Min(entry.NaturalHeight, availableHeight);
        if (height < 16)
        {
            height = availableHeight;
        }

        var availableWidth = Math.Max(0, slot.Width - (CaptionTextBoxHorizontalGap * 2));
        var width = Math.Min(entry.NaturalWidth, availableWidth);
        if (width < 16)
        {
            HideCaptionHost(entry);
            return;
        }

        var editorRect = new Rectangle(
            slot.X + Math.Max(0, (slot.Width - width) / 2),
            slot.Y + Math.Max(0, (slot.Height - height) / 2),
            width,
            height);
        var place = new Rectangle(
            windowRect.left + editorRect.X,
            windowRect.top + editorRect.Y,
            editorRect.Width,
            editorRect.Height);

        CaptionTextBoxHost host = EnsureCaptionHost(entry);
        var owner = MdiParent ?? this;
        if (!ReferenceEquals(host.Owner, owner))
        {
            host.Owner = owner;
        }

        if (host.TopMost != TopMost)
        {
            host.TopMost = TopMost;
        }

        if (entry.Control.Parent == host
            && (entry.Control.Width != place.Width || entry.Control.Height != place.Height))
        {
            _captionTextBoxApplyingBounds = true;
            entry.ApplyingBounds = true;
            try
            {
                entry.Control.SetBounds(0, 0, place.Width, place.Height);
            }
            finally
            {
                entry.ApplyingBounds = false;
                _captionTextBoxApplyingBounds = false;
            }
        }

        if (place != entry.LastPlace || !host.Visible)
        {
            host.Place(place);
            entry.LastPlace = place;
        }

        host.ShowWithoutActivate();
    }

    private void AttachCaptionEntry(CaptionEntry entry)
    {
        Control control = entry.Control;
        entry.NaturalWidth = control.Width > 0 ? control.Width : 160;
        entry.NaturalHeight = control.Height > 0 ? control.Height : 23;
        RememberCaptionPlacement(entry);
        control.SizeChanged += OnCaptionTextBoxSizeChanged;
        control.Enter += OnCaptionTextBoxFocusChanged;
        control.Leave += OnCaptionTextBoxFocusChanged;
        if (control is KryptonTextBox textBox)
        {
            textBox.TextBox.GotFocus += OnCaptionTextBoxFocusChanged;
            textBox.TextBox.LostFocus += OnCaptionTextBoxFocusChanged;
        }

        control.Disposed += OnCaptionTextBoxDisposed;
    }

    private void DetachCaptionEntry(CaptionEntry entry)
    {
        UnhookCaptionControl(entry.Control);
        if (entry.Host != null && entry.Control.Parent == entry.Host)
        {
            entry.Host.Controls.Remove(entry.Control);
        }

        RestoreCaptionPlacement(entry);
        HideCaptionHost(entry);
        if (entry.Slot.Parent is ViewComposite parent)
        {
            parent.Remove(entry.Slot);
        }

        if (entry.Host != null)
        {
            entry.Host.Dispose();
            entry.Host = null;
        }
    }

    private void UnhookCaptionControl(Control control)
    {
        control.SizeChanged -= OnCaptionTextBoxSizeChanged;
        control.Enter -= OnCaptionTextBoxFocusChanged;
        control.Leave -= OnCaptionTextBoxFocusChanged;
        if (control is KryptonTextBox textBox)
        {
            textBox.TextBox.GotFocus -= OnCaptionTextBoxFocusChanged;
            textBox.TextBox.LostFocus -= OnCaptionTextBoxFocusChanged;
        }

        control.Disposed -= OnCaptionTextBoxDisposed;
    }

    private void RememberCaptionPlacement(CaptionEntry entry)
    {
        Control control = entry.Control;
        if (control.Parent != null && control.Parent != entry.Host)
        {
            entry.RestoreParent = control.Parent;
            entry.RestoreBounds = control.Bounds;
            entry.RestoreDock = control.Dock;
            entry.RestoreAnchor = control.Anchor;
            entry.HasRestore = true;
        }
    }

    private void EnsureCaptionEntryHosted(CaptionEntry entry)
    {
        CaptionTextBoxHost host = EnsureCaptionHost(entry);
        if (entry.Control.Parent == host)
        {
            return;
        }

        RememberCaptionPlacement(entry);
        entry.Control.Parent?.Controls.Remove(entry.Control);
        entry.Control.Dock = DockStyle.None;
        entry.Control.Anchor = AnchorStyles.Top | AnchorStyles.Left;
        host.Controls.Add(entry.Control);
        host.Editor = entry.Control;
    }

    private void RestoreAllCaptionControls()
    {
        foreach (CaptionEntry entry in _captionEntries)
        {
            if (entry.Host != null && entry.Control.Parent == entry.Host && !entry.Control.IsDisposed)
            {
                entry.Host.Controls.Remove(entry.Control);
                RestoreCaptionPlacement(entry);
            }
        }
    }

    private void RestoreCaptionPlacement(CaptionEntry entry)
    {
        if (entry.Control.IsDisposed || !entry.HasRestore)
        {
            entry.HasRestore = false;
            return;
        }

        entry.Control.Dock = entry.RestoreDock;
        entry.Control.Anchor = entry.RestoreAnchor;
        if (entry.RestoreParent is { IsDisposed: false })
        {
            entry.RestoreParent.Controls.Add(entry.Control);
            if (entry.RestoreDock == DockStyle.None)
            {
                entry.Control.Bounds = entry.RestoreBounds;
            }
        }

        entry.HasRestore = false;
        entry.RestoreParent = null;
    }

    private CaptionTextBoxHost EnsureCaptionHost(CaptionEntry entry)
    {
        if (entry.Host == null || entry.Host.IsDisposed)
        {
            entry.Host = new CaptionTextBoxHost();
            entry.LastPlace = Rectangle.Empty;
        }

        entry.Host.Editor = entry.Control;
        entry.Host.TabOutOwner = this;
        return entry.Host;
    }

    private void HideAllCaptionHosts()
    {
        foreach (CaptionEntry entry in _captionEntries)
        {
            HideCaptionHost(entry);
        }
    }

    private static void HideCaptionHost(CaptionEntry entry)
    {
        if (entry.Host is { IsDisposed: false, Visible: true })
        {
            entry.Host.HideWithoutActivate();
        }

        entry.LastPlace = Rectangle.Empty;
    }

    private void SyncCaptionTextBoxMdiHook()
    {
        Form? parent = MdiParent;
        if (ReferenceEquals(_captionTextBoxMdiHook, parent))
        {
            return;
        }

        UnhookCaptionTextBoxMdi();
        _captionTextBoxMdiHook = parent;
        if (_captionTextBoxMdiHook != null)
        {
            _captionTextBoxMdiHook.MdiChildActivate += OnCaptionTextBoxMdiChildActivate;
        }
    }

    private void UnhookCaptionTextBoxMdi()
    {
        if (_captionTextBoxMdiHook != null)
        {
            _captionTextBoxMdiHook.MdiChildActivate -= OnCaptionTextBoxMdiChildActivate;
            _captionTextBoxMdiHook = null;
        }
    }

    private void OnCaptionTextBoxSizeChanged(object? sender, EventArgs e)
    {
        if (_captionTextBoxApplyingBounds || sender is not Control control)
        {
            return;
        }

        CaptionEntry? entry = FindCaptionEntry(control);
        if (entry == null || entry.ApplyingBounds || control.Parent != entry.Host)
        {
            return;
        }

        var changed = false;
        if (control.Width > 0 && control.Width != entry.NaturalWidth)
        {
            entry.NaturalWidth = control.Width;
            changed = true;
        }

        if (control.Height > 0 && control.Height != entry.NaturalHeight)
        {
            entry.NaturalHeight = control.Height;
            changed = true;
        }

        if (changed)
        {
            PerformNeedPaint(true);
        }
    }

    private void OnCaptionTextBoxFocusChanged(object? sender, EventArgs e) => KeepCaptionActiveForTextBox();

    private void OnCaptionTextBoxDisposed(object? sender, EventArgs e)
    {
        if (sender is Control control)
        {
            RemoveCaptionControl(control);
        }
    }

    private void OnCaptionTextBoxMdiChildActivate(object? sender, EventArgs e) => SyncCaptionTextBoxHost();

    /// <summary>
    /// Keeps the caption painted active when an owned caption control takes focus.
    /// </summary>
    /// <param name="m">A Windows-based message.</param>
    /// <returns><see langword="true"/> when the message was processed.</returns>
    protected override bool OnWM_NCACTIVATE(ref Message m)
    {
        var handled = base.OnWM_NCACTIVATE(ref m);
        KeepCaptionActiveForTextBox();
        return handled;
    }

    /// <summary>
    /// Raises the Deactivate event and keeps the caption active while a caption control has focus.
    /// </summary>
    /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
    protected override void OnDeactivate(EventArgs e)
    {
        base.OnDeactivate(e);
        KeepCaptionActiveForTextBox();
    }

    /// <summary>
    /// Raises the VisibleChanged event and shows or hides the caption controls with the form.
    /// </summary>
    /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
    protected override void OnVisibleChanged(EventArgs e)
    {
        base.OnVisibleChanged(e);
        SyncCaptionTextBoxHost();
    }

    /// <summary>
    /// Raises the DpiChanged event and repositions the caption controls.
    /// </summary>
    /// <param name="e">An <see cref="DpiChangedEventArgs"/> that contains the event data.</param>
    protected override void OnDpiChanged(DpiChangedEventArgs e)
    {
        base.OnDpiChanged(e);
        if (!_captionTextBoxApplyingBounds)
        {
            foreach (CaptionEntry entry in _captionEntries)
            {
                if (entry.ApplyingBounds || entry.Control.IsDisposed)
                {
                    continue;
                }

                if (entry.Control.Width > 0)
                {
                    entry.NaturalWidth = entry.Control.Width;
                }

                if (entry.Control.Height > 0)
                {
                    entry.NaturalHeight = entry.Control.Height;
                }
            }
        }

        SyncCaptionTextBoxHost();
    }

    /// <summary>
    /// One control hosted in the caption, with its own edge and owned window.
    /// </summary>
    private sealed class CaptionEntry
    {
        public CaptionEntry(Control control) => Control = control;

        public Control Control { get; }

        public FormCaptionTextBoxEdge Edge { get; set; } = FormCaptionTextBoxEdge.Right;

        public ViewLayoutSeparator Slot { get; } = new ViewLayoutSeparator(1, 1);

        public CaptionTextBoxHost? Host { get; set; }

        public Rectangle CenterRect { get; set; }

        public Rectangle LastPlace { get; set; }

        public int NaturalWidth { get; set; } = 160;

        public int NaturalHeight { get; set; } = 23;

        public bool ApplyingBounds { get; set; }

        public bool HasRestore { get; set; }

        public Control? RestoreParent { get; set; }

        public Rectangle RestoreBounds { get; set; }

        public DockStyle RestoreDock { get; set; }

        public AnchorStyles RestoreAnchor { get; set; }
    }

    /// <summary>
    /// Borderless owned window that hosts one caption control over the non-client caption.
    /// Clicks do not activate this window, so the owner caption stays active.
    /// </summary>
    private sealed class CaptionTextBoxHost : Form
    {
        public CaptionTextBoxHost()
        {
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            ControlBox = false;
            MinimizeBox = false;
            MaximizeBox = false;
            ShowIcon = false;
            AutoScaleMode = AutoScaleMode.None;
            Padding = Padding.Empty;
            Location = new Point(-32000, -32000);
            Size = new Size(1, 1);
            TabStop = false;
        }

        /// <summary>Control parented to this host.</summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        internal Control? Editor { get; set; }

        /// <summary>Form that receives Tab when focus leaves the editor.</summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        internal KryptonForm? TabOutOwner { get; set; }

        /// <summary>
        /// The host does not take activation when it is shown.
        /// </summary>
        protected override bool ShowWithoutActivation => true;

        /// <summary>
        /// Tool window without taskbar or activation so it can sit on the caption.
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= unchecked((int)(PI.WS_EX_.NOACTIVATE | PI.WS_EX_.TOOLWINDOW));
                cp.ExStyle &= ~unchecked((int)PI.WS_EX_.APPWINDOW);
                return cp;
            }
        }

        /// <summary>
        /// Moves the host in screen pixels without activating it.
        /// </summary>
        /// <param name="screenRect">Screen rectangle in physical pixels.</param>
        public void Place(Rectangle screenRect)
        {
            if (!IsHandleCreated)
            {
                CreateHandle();
            }

            PI.SetWindowPos(Handle, IntPtr.Zero, screenRect.X, screenRect.Y, screenRect.Width, screenRect.Height,
                PI.SWP_.NOACTIVATE | PI.SWP_.NOZORDER | PI.SWP_.NOOWNERZORDER);
            UpdateBounds(screenRect.X, screenRect.Y, screenRect.Width, screenRect.Height);
        }

        /// <summary>Shows the host without activating it.</summary>
        public void ShowWithoutActivate()
        {
            if (!IsHandleCreated)
            {
                CreateHandle();
            }

            if (!Visible)
            {
                PI.ShowWindow(Handle, PI.ShowWindowCommands.SW_SHOWNOACTIVATE);
            }
        }

        /// <summary>Hides the host without activating another window.</summary>
        public void HideWithoutActivate()
        {
            if (IsHandleCreated && Visible)
            {
                PI.ShowWindow(Handle, PI.ShowWindowCommands.SW_HIDE);
            }
        }

        /// <summary>
        /// Tab moves focus back to the owner form.
        /// </summary>
        /// <param name="keyData">Key data for the dialog key.</param>
        /// <returns><see langword="true"/> when the key was handled.</returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if ((keyData & Keys.KeyCode) == Keys.Tab && TabOutOwner is { IsDisposed: false })
            {
                var forward = (keyData & Keys.Shift) == 0;
                TabOutOwner.Activate();
                TabOutOwner.SelectNextControl(null, forward, true, true, true);
                return true;
            }

            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// A click focuses the editor and does not activate this window.
        /// </summary>
        /// <param name="m">The Windows message to process.</param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == PI.WM_.MOUSEACTIVATE)
            {
                m.Result = (IntPtr)PI.MA_NOACTIVATE;
                if (IsHandleCreated)
                {
                    BeginInvoke(new Action(FocusEditorIfNeeded));
                }

                return;
            }

            base.WndProc(ref m);
        }

        /// <summary>
        /// User closing is ignored. The owner disposes this host.
        /// </summary>
        /// <param name="e">A <see cref="FormClosingEventArgs"/> that contains the event data.</param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                return;
            }

            base.OnFormClosing(e);
        }

        private void FocusEditorIfNeeded()
        {
            if (Editor is not { IsDisposed: false, IsHandleCreated: true } || Editor.ContainsFocus)
            {
                return;
            }

            if (Editor is KryptonTextBox textBox && textBox.TextBox.IsHandleCreated)
            {
                textBox.TextBox.Focus();
                return;
            }

            Editor.Focus();
        }
    }

    #endregion
}

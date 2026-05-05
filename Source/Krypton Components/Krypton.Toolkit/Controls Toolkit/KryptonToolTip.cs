#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

using Timer = System.Windows.Forms.Timer;

/// <summary>
/// Provides themed Krypton tooltips for arbitrary <see cref="Control"/> instances (designer-extended title/body/image and palette settings reuse <see cref="VisualPopupToolTip"/>).
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(ToolTip))]
[DefaultProperty(nameof(ToolTipValues))]
[DesignerCategory(@"code")]
[ProvideProperty(@"KryptonToolTipTitle", typeof(Control))]
[ProvideProperty(@"KryptonToolTipDescription", typeof(Control))]
[ProvideProperty(@"KryptonToolTipImage", typeof(Control))]
[Description(@"Provides themed Krypton tooltips for Windows Forms controls.")]
public class KryptonToolTip : Component, IExtenderProvider
{
    #region Identity

    private readonly Dictionary<Control, ToolTipAssociation> _associations;

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonToolTip"/> class.
    /// </summary>
    public KryptonToolTip()
    {
        _associations = new Dictionary<Control, ToolTipAssociation>();
        PaletteModeInternal = PaletteMode.Global;
        PaletteInternal = KryptonManager.CurrentGlobalPalette;
        ToolTipValues = new ToolTipValues(OnTooltipValuesNeedPaint, GetDpiFactorFromContext);
        ToolTipValues.EnableToolTips = true;
        InitializeRendering();
        KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonToolTip"/> class with the specified container.
    /// </summary>
    /// <param name="container">The <see cref="IContainer"/> to add this component to.</param>
    public KryptonToolTip(IContainer container)
        : this()
    {
        container?.Add(this);
    }

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            KryptonManager.GlobalPaletteChanged -= OnGlobalPaletteChanged;
            CleanupTransientState(false);
            foreach (Control c in _associations.Keys.ToArray())
            {
                UnhookControlEvents(c);
            }

            _associations.Clear();

            Redirector?.Dispose();
            Redirector = null;
            Renderer = null;
            PaletteInternal = null;
        }

        base.Dispose(disposing);
    }

    #endregion

    #region Instance Fields

    private PaletteBase? PaletteInternal;

    private PaletteMode PaletteModeInternal;

    private PaletteRedirect? Redirector;

    private IRenderer? Renderer;

    private ContainerControl? _containerControl;

    private Control? _hoverControl;

    private Timer? _showTimer;

    private Timer? _closeTimer;

    private VisualPopupToolTip? _popup;

    private readonly HashSet<Control> _hookedControls = new HashSet<Control>();

    private readonly Dictionary<Control, PlacementRectangleAssociation> _placementRectangles = new Dictionary<Control, PlacementRectangleAssociation>();

    private Point _showAnchorScreenPoint;

    #endregion

    #region Public Palette

    /// <inheritdoc cref="PaletteMode" />
    [Category(@"Visuals")]
    [Description(@"Palette applied when rendering tooltip pop-ups.")]
    [DefaultValue(PaletteMode.Global)]
    public PaletteMode PaletteMode
    {
        get => PaletteModeInternal;

        set
        {
            if (PaletteModeInternal != value)
            {
                switch (value)
                {
                    case PaletteMode.Custom:
                        break;
                    default:
                        PaletteModeInternal = value;
                        PaletteInternal = KryptonManager.GetPaletteForMode(PaletteModeInternal);
                        RefreshRenderingReferences();
                        break;
                }
            }
        }
    }

    /// <inheritdoc cref="Palette" />
    [Category(@"Visuals")]
    [Description(@"Custom palette for tooltip rendering when PaletteMode is Custom.")]
    [DefaultValue(null)]
    public PaletteBase? Palette
    {
        get => PaletteModeInternal == PaletteMode.Custom ? PaletteInternal : null;

        set
        {
            if (PaletteInternal == value)
            {
                return;
            }

            PaletteInternal = value;
            if (value == null)
            {
                PaletteModeInternal = PaletteMode.Global;
                PaletteInternal = KryptonManager.CurrentGlobalPalette;
            }
            else
            {
                PaletteModeInternal = PaletteMode.Custom;
            }

            RefreshRenderingReferences();
        }
    }

    #endregion

    #region ToolTip Values (styles, delays)

    /// <summary>
    /// Gets shared tooltip appearance (label style, shadow, delays, placement, etc.) applied when showing pop-ups.
    /// By default, <see cref="ToolTipValues.ToolTipPosition"/> is resolved via <see cref="VisualPopupToolTip.ShowRelativeTo(Control, Point, PopupPositionValues)"/>
    /// with the hovered control as the fallback bounds; see <see cref="UseLegacyCursorAnchoredPlacement"/> for cursor-only behaviour.
    /// </summary>
    [Category(@"ToolTip")]
    [Description(@"Shared tooltip styling, timing, and placement for all extended controls.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ToolTipValues ToolTipValues { get; }

    private bool ShouldSerializeToolTipValues() => !ToolTipValues.IsDefault;

    /// <inheritdoc />
    public void ResetToolTipValues() => ToolTipValues.Reset();

    #endregion

    #region Placement

    /// <summary>
    /// When <see langword="true"/>, tooltips appear using <see cref="VisualPopupToolTip.ShowCalculatingSize"/> near the cursor instead of honoring <see cref="ToolTipValues.ToolTipPosition"/> relative to the hovered control.
    /// </summary>
    [Category(@"ToolTip")]
    [Description(@"When true, use legacy cursor-offset placement. When false (default), use ToolTipPosition with the hovered control as placement fallback.")]
    [DefaultValue(false)]
    public bool UseLegacyCursorAnchoredPlacement { get; set; }

    private bool ShouldSerializeUseLegacyCursorAnchoredPlacement() => UseLegacyCursorAnchoredPlacement;

    private void ResetUseLegacyCursorAnchoredPlacement() => UseLegacyCursorAnchoredPlacement = false;

    /// <summary>
    /// Enables keyboard/focus tooltip activation in addition to mouse hover.
    /// </summary>
    [Category(@"ToolTip")]
    [Description(@"When true, focused controls can display tooltips using the same timing/placement rules as mouse hover.")]
    [DefaultValue(true)]
    public bool EnableKeyboardToolTips { get; set; } = true;

    private bool ShouldSerializeEnableKeyboardToolTips() => !EnableKeyboardToolTips;

    private void ResetEnableKeyboardToolTips() => EnableKeyboardToolTips = true;

    #endregion

    #region Container

    /// <summary>
    /// Gets or sets the container control used for DPI context when sizing tooltip content (optional).
    /// </summary>
    [Browsable(false)]
    [DefaultValue(null)]
    public ContainerControl? ContainerControl
    {
        get => _containerControl;

        set => _containerControl = value;
    }

    #endregion

    #region Programmatic API

    /// <summary>
    /// Sets title, description and optional heading image for a control’s Krypton tooltip.
    /// </summary>
    /// <param name="control">The target control.</param>
    /// <param name="title">Heading shown as short text (SuperTip).</param>
    /// <param name="description">Body shown as description text.</param>
    /// <param name="image">Optional tooltip image.</param>
    /// <param name="imageTransparentColor">Transparent colour for <paramref name="image"/>.</param>
    public void SetToolTip(
        Control? control,
        string title,
        string description,
        Image? image = null,
        Color imageTransparentColor = default)
    {
        if (control == null || (!HasRenderableTextOrImage(title, description, image)))
        {
            if (control != null)
            {
                RemoveAssociations(control);
            }

            return;
        }

        _associations[control] =
            new ToolTipAssociation(title ?? string.Empty, description ?? string.Empty, image,
                imageTransparentColor == default ? GlobalStaticVariables.EMPTY_COLOR : imageTransparentColor);
        SyncHooksFor(control);
    }

    /// <summary>
    /// Removes Krypton tooltip data and hooks for <paramref name="control"/>.
    /// </summary>
    /// <param name="control">The control whose tooltip state should be cleared.</param>
    public void ClearToolTip(Control? control)
    {
        if (control != null)
        {
            RemoveAssociations(control);
        }
    }

    #endregion

    #region IExtenderProvider

    /// <inheritdoc />
    public bool CanExtend(object? extendee) =>
        extendee is Control;

    /// <summary>
    /// Gets heading text for control tooltips (<see cref="HeaderValues"/> short text mapping).
    /// </summary>
    [ExtenderProvidedProperty]
    [Category(@"ToolTip")]
    [Description(@"Optional heading displayed in themed tooltips for this control.")]
    [Localizable(true)]
    [DefaultValue("")]
    public string GetKryptonToolTipTitle(Control control) =>
        _associations.TryGetValue(control, out ToolTipAssociation a)
            ? a.Title
            : string.Empty;

    /// <summary>
    /// Sets heading text used for themed tooltips for <paramref name="control"/>.
    /// </summary>
    public void SetKryptonToolTipTitle(Control control, string value)
    {
        UpsertHeading(control, value ?? string.Empty);
    }

    /// <inheritdoc cref="SetKryptonToolTipTitle" />
    [ExtenderProvidedProperty]
    [Category(@"ToolTip")]
    [Description(@"Body text displayed in themed tooltips for this control.")]
    [Localizable(true)]
    [DefaultValue("")]
    public string GetKryptonToolTipDescription(Control control) =>
        _associations.TryGetValue(control, out ToolTipAssociation a)
            ? a.Description
            : string.Empty;

    /// <summary>
    /// Sets body text used for themed tooltips for <paramref name="control"/>.
    /// </summary>
    public void SetKryptonToolTipDescription(Control control, string value)
    {
        UpsertDescription(control, value ?? string.Empty);
    }

    /// <summary>
    /// Gets heading image displayed for this control tooltip.
    /// </summary>
    [ExtenderProvidedProperty]
    [Category(@"ToolTip")]
    [Description(@"Optional tooltip image.")]
    [DefaultValue(null)]
    public Image? GetKryptonToolTipImage(Control control) =>
        _associations.TryGetValue(control, out ToolTipAssociation a)
            ? a.Image
            : null;

    /// <inheritdoc cref="GetKryptonToolTipImage"/>
    public void SetKryptonToolTipImage(Control control, Image? value)
    {
        UpsertImage(control, value);
    }

    /// <summary>
    /// Gets the authored placement rectangle override for the control.
    /// </summary>
    /// <param name="control">Control whose override should be queried.</param>
    /// <returns>The authored rectangle, or <see cref="Rectangle.Empty"/> if no override exists.</returns>
    public Rectangle GetPlacementRectangle(Control control) =>
        _placementRectangles.TryGetValue(control, out PlacementRectangleAssociation association)
            ? association.Rectangle
            : Rectangle.Empty;

    /// <summary>
    /// Sets a control-specific placement rectangle override used when showing tooltips.
    /// </summary>
    /// <param name="control">Control owning the override.</param>
    /// <param name="placementRectangle">Placement rectangle in either client or screen coordinates.</param>
    /// <param name="isScreenCoordinates">True when <paramref name="placementRectangle"/> is already in screen coordinates; false when it is in control client coordinates.</param>
    public void SetPlacementRectangle(Control? control, Rectangle placementRectangle, bool isScreenCoordinates = false)
    {
        if (control == null)
        {
            return;
        }

        if (placementRectangle.IsEmpty)
        {
            _placementRectangles.Remove(control);
            return;
        }

        _placementRectangles[control] = new PlacementRectangleAssociation(placementRectangle, isScreenCoordinates);
    }

    /// <summary>
    /// Clears a control-specific placement rectangle override.
    /// </summary>
    /// <param name="control">Control whose override should be removed.</param>
    public void ClearPlacementRectangle(Control? control)
    {
        if (control != null)
        {
            _placementRectangles.Remove(control);
        }
    }

    #endregion

    #region Private Types

    private readonly struct PlacementRectangleAssociation
    {
        public PlacementRectangleAssociation(Rectangle rectangle, bool isScreenCoordinates)
        {
            Rectangle = rectangle;
            IsScreenCoordinates = isScreenCoordinates;
        }

        public Rectangle Rectangle { get; }

        public bool IsScreenCoordinates { get; }
    }

    private readonly struct ToolTipAssociation
    {
        internal ToolTipAssociation(
            string title,
            string description,
            Image? image,
            Color imageTransparentColor)
        {
            Title = title ?? string.Empty;
            Description = description ?? string.Empty;
            Image = image;
            ImageTransparentColor = imageTransparentColor;
        }

        public string Title { get; }

        public string Description { get; }

        public Image? Image { get; }

        public Color ImageTransparentColor { get; }
    }

    private sealed class PerControlToolTipContent : IContentValues
    {
        private readonly ToolTipAssociation _association;

        public PerControlToolTipContent(ToolTipAssociation association)
        {
            _association = association;
        }

        /// <inheritdoc />
        public Image? GetImage(PaletteState state) => _association.Image;

        /// <inheritdoc />
        public Color GetImageTransparentColor(PaletteState state) => _association.ImageTransparentColor;

        /// <inheritdoc />
        public string GetShortText() => _association.Title ?? string.Empty;

        /// <inheritdoc />
        public string GetLongText() => _association.Description ?? string.Empty;

        /// <inheritdoc />
        public Image? GetOverlayImage(PaletteState state) => null;

        /// <inheritdoc />
        public Color GetOverlayImageTransparentColor(PaletteState state) => GlobalStaticVariables.EMPTY_COLOR;

        /// <inheritdoc />
        public OverlayImagePosition GetOverlayImagePosition(PaletteState state) => OverlayImagePosition.TopRight;

        /// <inheritdoc />
        public OverlayImageScaleMode GetOverlayImageScaleMode(PaletteState state) => OverlayImageScaleMode.None;

        /// <inheritdoc />
        public float GetOverlayImageScaleFactor(PaletteState state) => 0.5f;

        /// <inheritdoc />
        public Size GetOverlayImageFixedSize(PaletteState state) => new Size(16, 16);
    }

    #endregion

    #region Association Sync

    private void UpsertHeading(Control control, string heading)
    {
        _associations.TryGetValue(control, out ToolTipAssociation prior);
        if (!HasRenderableTextOrImage(heading, prior.Description, prior.Image))
        {
            RemoveAssociations(control);
            return;
        }

        ToolTipAssociation current = EnsureAssociation(control);
        _associations[control] = new ToolTipAssociation(
            heading,
            current.Description,
            current.Image,
            current.ImageTransparentColor);
        SyncHooksFor(control);
    }

    private void UpsertDescription(Control control, string description)
    {
        _associations.TryGetValue(control, out ToolTipAssociation prior);
        if (!HasRenderableTextOrImage(prior.Title, description, prior.Image))
        {
            RemoveAssociations(control);
            return;
        }

        ToolTipAssociation current = EnsureAssociation(control);
        _associations[control] = new ToolTipAssociation(
            current.Title,
            description,
            current.Image,
            current.ImageTransparentColor);
        SyncHooksFor(control);
    }

    private void UpsertImage(Control control, Image? image)
    {
        ToolTipAssociation current = EnsureAssociation(control);
        if (!HasRenderableTextOrImage(current.Title, current.Description, image))
        {
            RemoveAssociations(control);
            return;
        }

        _associations[control] =
            new ToolTipAssociation(current.Title, current.Description, image, current.ImageTransparentColor);
        SyncHooksFor(control);
    }

    private ToolTipAssociation EnsureAssociation(Control control)
    {
        if (_associations.TryGetValue(control, out ToolTipAssociation existing))
        {
            return existing;
        }

        var created = new ToolTipAssociation(string.Empty, string.Empty, null, GlobalStaticVariables.EMPTY_COLOR);
        _associations[control] = created;
        return created;
    }

    private static bool HasRenderableTextOrImage(string? title, string? description, Image? image) =>
        !(string.IsNullOrEmpty(title) && string.IsNullOrEmpty(description) && image == null);

    private void RemoveAssociations(Control control)
    {
        _associations.Remove(control);
        _placementRectangles.Remove(control);
        UnhookControlEvents(control);
    }

    private void SyncHooksFor(Control control)
    {
        if (!_associations.TryGetValue(control, out ToolTipAssociation a) ||
            !HasRenderableTextOrImage(a.Title, a.Description, a.Image))
        {
            UnhookControlEvents(control);
            return;
        }

        if (_hookedControls.Contains(control))
        {
            return;
        }

        control.MouseEnter += OnTargetMouseEnter;
        control.MouseLeave += OnTargetMouseLeave;
        control.MouseDown += OnTargetMouseDown;
        control.GotFocus += OnTargetGotFocus;
        control.LostFocus += OnTargetLostFocus;
        control.Disposed += OnTargetDisposed;
        _hookedControls.Add(control);
    }

    private void UnhookControlEvents(Control control)
    {
        if (!_hookedControls.Contains(control))
        {
            return;
        }

        UnhookCore(control);
        if (_hoverControl == control)
        {
            _hoverControl = null;
            CleanupTransientState(false);
        }
    }

    private void OnTargetDisposed(object? sender, EventArgs e)
    {
        if (sender is Control c)
        {
            _associations.Remove(c);
            UnhookCore(c);
            if (_hoverControl == c)
            {
                _hoverControl = null;
                CleanupTransientState(false);
            }
        }
    }

    private void UnhookCore(Control control)
    {
        control.MouseEnter -= OnTargetMouseEnter;
        control.MouseLeave -= OnTargetMouseLeave;
        control.MouseDown -= OnTargetMouseDown;
        control.GotFocus -= OnTargetGotFocus;
        control.LostFocus -= OnTargetLostFocus;
        control.Disposed -= OnTargetDisposed;
        _hookedControls.Remove(control);
    }

    #endregion

    #region Hover / Popup

    private void OnTargetMouseEnter(object? sender, EventArgs e)
    {
        if (sender is not Control c || !ToolTipValues.EnableToolTips)
        {
            return;
        }

        ScheduleShow(c, Cursor.Position);
    }

    private void OnTargetMouseLeave(object? sender, EventArgs e)
    {
        CleanupTransientState(false);
        _hoverControl = null;
    }

    private void OnTargetMouseDown(object? sender, MouseEventArgs e)
    {
        CleanupTransientState(false);
    }

    private void OnTargetGotFocus(object? sender, EventArgs e)
    {
        if (!EnableKeyboardToolTips || sender is not Control c || !ToolTipValues.EnableToolTips)
        {
            return;
        }

        Rectangle client = c.ClientRectangle;
        Point centerBottom = new Point(client.Left + (client.Width / 2), client.Bottom);
        Point screenAnchor = c.PointToScreen(centerBottom);
        ScheduleShow(c, screenAnchor);
    }

    private void OnTargetLostFocus(object? sender, EventArgs e)
    {
        if (!EnableKeyboardToolTips)
        {
            return;
        }

        if (sender is Control c && ReferenceEquals(_hoverControl, c))
        {
            CleanupTransientState(false);
            _hoverControl = null;
        }
    }

    private void ScheduleShow(Control control, Point anchorScreenPoint)
    {
        CleanupTransientState(false);

        _hoverControl = control;
        _showAnchorScreenPoint = anchorScreenPoint;
        _showTimer?.Stop();
        _showTimer?.Dispose();
        _showTimer = new Timer
        {
            Interval = Math.Max(1, ToolTipValues.ShowIntervalDelay)
        };
        _showTimer.Tick += OnShowTimerTick;
        _showTimer.Start();
    }

    private void OnShowTimerTick(object? sender, EventArgs e)
    {
        _showTimer?.Stop();
        Control? hcNullable = _hoverControl;
        if (hcNullable is not { IsDisposed: false } hc ||
            IsDesignMode(hc) || PaletteInternal == null || Redirector == null ||
            Renderer == null)
        {
            return;
        }

        Form? owningForm = hc.FindForm();
        if (owningForm is null || owningForm.ContainsFocus == false)
        {
            return;
        }

        if (!_associations.TryGetValue(hc, out ToolTipAssociation association) ||
            !HasRenderableTextOrImage(association.Title, association.Description, association.Image))
        {
            return;
        }

        _popup?.Dispose();
        var content = new PerControlToolTipContent(association);

        PaletteContentStyle style =
            CommonHelper.ContentStyleFromLabelStyle(ToolTipValues.ToolTipStyle);

        _popup =
            new VisualPopupToolTip(
                Redirector,
                content,
                Renderer,
                PaletteBackStyle.ControlToolTip,
                PaletteBorderStyle.ControlToolTip,
                style,
                ToolTipValues.ToolTipShadow);

        _popup.Disposed += OnPopupDisposed;
        Point anchor = _showAnchorScreenPoint;
        if (UseLegacyCursorAnchoredPlacement)
        {
            _popup.ShowCalculatingSize(anchor);
        }
        else
        {
            _popup.ShowRelativeTo(hc, anchor, CreateEffectivePositionValues(hc));
        }

        int closeDelay = ToolTipValues.CloseIntervalDelay;
        if (closeDelay > 0)
        {
            _closeTimer?.Dispose();
            _closeTimer =
                new Timer
                {
                    Interval = Math.Max(1, closeDelay)
                };
            _closeTimer.Tick += OnCloseTimerTick;
            _closeTimer.Start();
        }
    }

    private void OnCloseTimerTick(object? sender, EventArgs e)
    {
        HidePopupOnly();
    }

    private void HidePopupOnly()
    {
        _closeTimer?.Stop();
        _closeTimer?.Dispose();
        _closeTimer = null;

        _popup?.Dispose();
    }

    private void OnPopupDisposed(object? sender, EventArgs e)
    {
        if (sender is VisualPopupToolTip t)
        {
            t.Disposed -= OnPopupDisposed;
        }

        if (ReferenceEquals(_popup, sender))
        {
            _popup = null;
        }
    }

    private void CleanupTransientState(bool unhookTrackedControls)
    {
        _showTimer?.Stop();
        _showTimer?.Dispose();
        _showTimer = null;

        _closeTimer?.Stop();
        _closeTimer?.Dispose();
        _closeTimer = null;

        _popup?.Dispose();
        _popup = null;

        if (!unhookTrackedControls)
        {
            return;
        }

        foreach (Control c in _hookedControls.ToArray())
        {
            RemoveAssociations(c);
        }
    }

    #endregion

    #region Rendering

    private static bool IsDesignMode(Control? control)
    {
        Control? walker = control;
        while (walker != null && !walker.IsDisposed)
        {
            if (walker.Site?.DesignMode == true)
            {
                return true;
            }

            walker = walker.Parent;
        }

        return false;
    }

    private float GetDpiFactorFromContext()
    {
        Control? dpiSource = _hoverControl ?? ContainerControl;

        float dpiFactor = dpiSource is Control c && c.IsDisposed == false ? c.DeviceDpi / 96f : 96f / 96f;

        return dpiFactor;
    }

    private void OnTooltipValuesNeedPaint(object? sender, NeedLayoutEventArgs e)
    {
    }

    private void InitializeRendering()
    {
        if (PaletteInternal != null)
        {
            Redirector = new PaletteRedirect(PaletteInternal);
            Renderer = PaletteInternal.GetRenderer();
        }
    }

    private void RefreshRenderingReferences()
    {
        HidePopupOnly();
        Redirector?.Dispose();
        Redirector = PaletteInternal != null ? new PaletteRedirect(PaletteInternal) : null;
        Renderer = PaletteInternal?.GetRenderer();
    }

    private PopupPositionValues CreateEffectivePositionValues(Control control)
    {
        PopupPositionValues authored = ToolTipValues.ToolTipPosition;
        if (!_placementRectangles.TryGetValue(control, out PlacementRectangleAssociation association))
        {
            return authored;
        }

        var effective = new PopupPositionValues
        {
            PlacementMode = authored.PlacementMode,
            PlacementTarget = authored.PlacementTarget
        };

        Rectangle screenRect = association.IsScreenCoordinates
            ? association.Rectangle
            : control.RectangleToScreen(association.Rectangle);
        effective.PlacementRectangle = screenRect;

        return effective;
    }

    private void OnGlobalPaletteChanged(object? sender, EventArgs e)
    {
        if (PaletteModeInternal == PaletteMode.Global)
        {
            PaletteInternal = KryptonManager.CurrentGlobalPalette;
            RefreshRenderingReferences();
        }
    }

    #endregion
}

#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

using Timer = System.Windows.Forms.Timer;

/// <summary>
/// Provides themed Krypton tooltips for arbitrary <see cref="Control"/> instances.
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
            CleanupTransientState();
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

    private Control? _hoverControl;

    private Timer? _showTimer;

    private Timer? _closeTimer;

    private VisualPopupToolTip? _popup;

    private readonly HashSet<Control> _hookedControls = new HashSet<Control>();

    private readonly Dictionary<Control, PlacementRectangleAssociation> _placementRectangles =
        new Dictionary<Control, PlacementRectangleAssociation>();

    private Point _showAnchorScreenPoint;

    #endregion

    #region Public

    /// <summary>
    /// Gets shared tooltip appearance (label style, shadow, delays, placement) applied when showing pop-ups.
    /// </summary>
    [Category(@"ToolTip")]
    [Description(@"Shared tooltip styling, timing, and placement for all extended controls.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ToolTipValues ToolTipValues { get; }

    private bool ShouldSerializeToolTipValues() => !ToolTipValues.IsDefault;

    /// <summary>
    /// Resets <see cref="ToolTipValues"/> to defaults.
    /// </summary>
    public void ResetToolTipValues() => ToolTipValues.Reset();

    /// <summary>
    /// Sets title, description and optional heading image for a control’s Krypton tooltip.
    /// </summary>
    public void SetToolTip(
        Control? control,
        string title,
        string description,
        Image? image = null,
        Color imageTransparentColor = default)
    {
        if (control == null || !HasRenderableTextOrImage(title, description, image))
        {
            if (control != null)
            {
                RemoveAssociations(control);
            }

            return;
        }

        _associations[control] = new ToolTipAssociation(
            title ?? string.Empty,
            description ?? string.Empty,
            image,
            imageTransparentColor == default ? GlobalStaticValues.EMPTY_COLOR : imageTransparentColor);
        SyncHooksFor(control);
    }

    /// <summary>
    /// Shows the current association for <paramref name="control"/> after the usual delay.
    /// </summary>
    /// <param name="control">Control that already has a <see cref="SetToolTip(Control, string, string, Image, Color)"/> association.</param>
    /// <param name="immediate">When <see langword="true"/>, skip the show delay (used when moving between items).</param>
    public void ShowFor(Control? control, bool immediate = false)
    {
        if (control == null ||
            !_associations.TryGetValue(control, out ToolTipAssociation? association) ||
            !HasRenderableTextOrImage(association.Title, association.Description, association.Image) ||
            !ToolTipValues.EnableToolTips)
        {
            HideFor(control);
            return;
        }

        ScheduleShow(control, Cursor.Position);
        if (immediate && _showTimer != null)
        {
            _showTimer.Interval = 1;
        }
    }

    /// <summary>
    /// Hides the tooltip if it is showing for <paramref name="control"/>.
    /// </summary>
    public void HideFor(Control? control)
    {
        if (control == null)
        {
            return;
        }

        if (_hoverControl == null || ReferenceEquals(_hoverControl, control))
        {
            CleanupTransientState();
            _hoverControl = null;
        }
    }

    /// <summary>
    /// Removes Krypton tooltip data and hooks for <paramref name="control"/>.
    /// </summary>
    public void ClearToolTip(Control? control)
    {
        if (control != null)
        {
            RemoveAssociations(control);
        }
    }

    /// <inheritdoc />
    public bool CanExtend(object? extendee) => extendee is Control;

    /// <summary>
    /// Gets heading text for control tooltips.
    /// </summary>
    [ExtenderProvidedProperty]
    [Category(@"ToolTip")]
    [Description(@"Optional heading displayed in themed tooltips for this control.")]
    [Localizable(true)]
    [DefaultValue("")]
    public string GetKryptonToolTipTitle(Control control) =>
        _associations.TryGetValue(control, out ToolTipAssociation? a) ? a.Title : string.Empty;

    /// <summary>
    /// Sets heading text used for themed tooltips for <paramref name="control"/>.
    /// </summary>
    public void SetKryptonToolTipTitle(Control control, string value) =>
        SetToolTip(control, value ?? string.Empty, GetKryptonToolTipDescription(control), GetKryptonToolTipImage(control));

    /// <summary>
    /// Gets body text for control tooltips.
    /// </summary>
    [ExtenderProvidedProperty]
    [Category(@"ToolTip")]
    [Description(@"Body text displayed in themed tooltips for this control.")]
    [Localizable(true)]
    [DefaultValue("")]
    public string GetKryptonToolTipDescription(Control control) =>
        _associations.TryGetValue(control, out ToolTipAssociation? a) ? a.Description : string.Empty;

    /// <summary>
    /// Sets body text used for themed tooltips for <paramref name="control"/>.
    /// </summary>
    public void SetKryptonToolTipDescription(Control control, string value) =>
        SetToolTip(control, GetKryptonToolTipTitle(control), value ?? string.Empty, GetKryptonToolTipImage(control));

    /// <summary>
    /// Gets heading image displayed for this control tooltip.
    /// </summary>
    [ExtenderProvidedProperty]
    [Category(@"ToolTip")]
    [Description(@"Optional tooltip image.")]
    [DefaultValue(null)]
    public Image? GetKryptonToolTipImage(Control control) =>
        _associations.TryGetValue(control, out ToolTipAssociation? a) ? a.Image : null;

    /// <summary>
    /// Sets heading image used for themed tooltips for <paramref name="control"/>.
    /// </summary>
    public void SetKryptonToolTipImage(Control control, Image? value) =>
        SetToolTip(control, GetKryptonToolTipTitle(control), GetKryptonToolTipDescription(control), value);

    /// <summary>
    /// Sets a control-specific placement rectangle override used when showing tooltips.
    /// </summary>
    /// <param name="control">Control owning the override.</param>
    /// <param name="placementRectangle">Placement rectangle in either client or screen coordinates.</param>
    /// <param name="isScreenCoordinates">True when <paramref name="placementRectangle"/> is already in screen coordinates.</param>
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

    private sealed class ToolTipAssociation
    {
        internal ToolTipAssociation(string title, string description, Image? image, Color imageTransparentColor)
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

        public PerControlToolTipContent(ToolTipAssociation association) => _association = association;

        public Image? GetImage(PaletteState state) => _association.Image;

        public Color GetImageTransparentColor(PaletteState state) => _association.ImageTransparentColor;

        public string GetShortText() => _association.Title ?? string.Empty;

        public string GetLongText() => _association.Description ?? string.Empty;
    }

    #endregion

    #region Association Sync

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
        if (!_associations.TryGetValue(control, out ToolTipAssociation? a) ||
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
        control.Disposed += OnTargetDisposed;
        _hookedControls.Add(control);
    }

    private void UnhookControlEvents(Control control)
    {
        if (!_hookedControls.Contains(control))
        {
            return;
        }

        control.MouseEnter -= OnTargetMouseEnter;
        control.MouseLeave -= OnTargetMouseLeave;
        control.MouseDown -= OnTargetMouseDown;
        control.Disposed -= OnTargetDisposed;
        _hookedControls.Remove(control);
        if (_hoverControl == control)
        {
            _hoverControl = null;
            CleanupTransientState();
        }
    }

    private void OnTargetDisposed(object? sender, EventArgs e)
    {
        if (sender is Control c)
        {
            RemoveAssociations(c);
        }
    }

    #endregion

    #region Hover / Popup

    private void OnTargetMouseEnter(object? sender, EventArgs e)
    {
        if (sender is Control c && ToolTipValues.EnableToolTips)
        {
            ScheduleShow(c, Cursor.Position);
        }
    }

    private void OnTargetMouseLeave(object? sender, EventArgs e)
    {
        CleanupTransientState();
        _hoverControl = null;
    }

    private void OnTargetMouseDown(object? sender, MouseEventArgs e) => CleanupTransientState();

    private void ScheduleShow(Control control, Point anchorScreenPoint)
    {
        CleanupTransientState();

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
            IsDesignMode(hc) || PaletteInternal == null || Redirector == null || Renderer == null)
        {
            return;
        }

        Form? owningForm = hc.FindForm();
        if (owningForm is null || owningForm.ContainsFocus == false)
        {
            return;
        }

        if (!_associations.TryGetValue(hc, out ToolTipAssociation? association) ||
            !HasRenderableTextOrImage(association.Title, association.Description, association.Image))
        {
            return;
        }

        _popup?.Dispose();

        PaletteContentStyle style = CommonHelper.ContentStyleFromLabelStyle(ToolTipValues.ToolTipStyle);
        var heading = new PerControlToolTipContent(association);
        _popup = new VisualPopupToolTip(
            Redirector,
            heading,
            Renderer,
            PaletteBackStyle.ControlToolTip,
            PaletteBorderStyle.ControlToolTip,
            style,
            ToolTipValues.ToolTipShadow);

        _popup.Disposed += OnPopupDisposed;
        _popup.ShowRelativeTo(hc, _showAnchorScreenPoint, CreateEffectivePositionValues(hc), GetFallbackPlacementRect(hc));

        int closeDelay = ToolTipValues.CloseIntervalDelay;
        if (closeDelay > 0)
        {
            _closeTimer?.Dispose();
            _closeTimer = new Timer
            {
                Interval = Math.Max(1, closeDelay)
            };
            _closeTimer.Tick += OnCloseTimerTick;
            _closeTimer.Start();
        }
    }

    private void OnCloseTimerTick(object? sender, EventArgs e)
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

    private void CleanupTransientState()
    {
        _showTimer?.Stop();
        _showTimer?.Dispose();
        _showTimer = null;

        _closeTimer?.Stop();
        _closeTimer?.Dispose();
        _closeTimer = null;

        _popup?.Dispose();
        _popup = null;
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
        Control? dpiSource = _hoverControl;
        return dpiSource is { IsDisposed: false } c ? c.DeviceDpi / 96f : 1f;
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
        _popup?.Dispose();
        Redirector?.Dispose();
        Redirector = PaletteInternal != null ? new PaletteRedirect(PaletteInternal) : null;
        Renderer = PaletteInternal?.GetRenderer();
    }

    private PopupPositionValues CreateEffectivePositionValues(Control control)
    {
        PopupPositionValues authored = ToolTipValues.ToolTipPosition;
        if (!_placementRectangles.TryGetValue(control, out PlacementRectangleAssociation association)
            || !association.IsScreenCoordinates)
        {
            return authored;
        }

        return new PopupPositionValues
        {
            PlacementMode = authored.PlacementMode,
            PlacementTarget = authored.PlacementTarget,
            PlacementRectangle = association.Rectangle
        };
    }

    private Rectangle GetFallbackPlacementRect(Control control)
    {
        if (_placementRectangles.TryGetValue(control, out PlacementRectangleAssociation association)
            && !association.IsScreenCoordinates
            && !association.Rectangle.IsEmpty)
        {
            return association.Rectangle;
        }

        return control.ClientRectangle;
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

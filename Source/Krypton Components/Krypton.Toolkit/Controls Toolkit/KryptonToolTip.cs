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
/// Provides themed Krypton tooltips for arbitrary <see cref="Control"/> instances (designer-extended title/body/image, optional hosted controls, and palette settings reuse <see cref="VisualPopupToolTip"/>).
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(ToolTip), "ToolboxBitmaps.KryptonToolTip.bmp")]
[DefaultProperty(nameof(ToolTipValues))]
[DesignerCategory(@"code")]
[ProvideProperty(@"KryptonToolTipTitle", typeof(Control))]
[ProvideProperty(@"KryptonToolTipDescription", typeof(Control))]
[ProvideProperty(@"KryptonToolTipImage", typeof(Control))]
[ProvideProperty(@"KryptonToolTipContent", typeof(Control))]
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

    private Timer? _lingerTimer;

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

    /// <summary>
    /// Grace period (milliseconds) after the pointer leaves the target or popup before an interactive tooltip is dismissed.
    /// Allows the pointer to travel from the hovered control onto the popup.
    /// </summary>
    [Category(@"ToolTip")]
    [Description(@"Milliseconds to wait after mouse leave before dismissing an interactive (hosted-control) tooltip.")]
    [DefaultValue(300)]
    public int InteractiveLingerDelay { get; set; } = 300;

    private bool ShouldSerializeInteractiveLingerDelay() => InteractiveLingerDelay != 300;

    private void ResetInteractiveLingerDelay() => InteractiveLingerDelay = 300;

    /// <summary>
    /// Occurs when a hyperlink created by <see cref="SetLinkToolTip"/> is clicked.
    /// Set <see cref="ToolTipLinkClickedEventArgs.Cancel"/> to skip the default shell open.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when a tooltip hyperlink is clicked.")]
    public event EventHandler<ToolTipLinkClickedEventArgs>? LinkClicked;

    /// <summary>
    /// When true, interactive hosted tooltips receive keyboard input (Escape dismisses). Default is false.
    /// </summary>
    [Category(@"ToolTip")]
    [Description(@"When true, hosted-control tooltips receive keyboard input.")]
    [DefaultValue(false)]
    public bool EnableInteractiveKeyboard { get; set; }

    private bool ShouldSerializeEnableInteractiveKeyboard() => EnableInteractiveKeyboard;

    private void ResetEnableInteractiveKeyboard() => EnableInteractiveKeyboard = false;

    /// <summary>
    /// When true, <see cref="ToolTipValues.CloseIntervalDelay"/> applies to interactive tooltips. Default is false.
    /// </summary>
    [Category(@"ToolTip")]
    [Description(@"When true, CloseIntervalDelay also closes interactive hosted-control tooltips.")]
    [DefaultValue(false)]
    public bool UseCloseTimerForInteractive { get; set; }

    private bool ShouldSerializeUseCloseTimerForInteractive() => UseCloseTimerForInteractive;

    private void ResetUseCloseTimerForInteractive() => UseCloseTimerForInteractive = false;

    /// <summary>
    /// When true, mouse-down on the hover target dismisses an interactive tooltip. Default is false.
    /// </summary>
    [Category(@"ToolTip")]
    [Description(@"When true, clicking the hover target dismisses an interactive tooltip.")]
    [DefaultValue(false)]
    public bool DismissInteractiveOnTargetMouseDown { get; set; }

    private bool ShouldSerializeDismissInteractiveOnTargetMouseDown() => DismissInteractiveOnTargetMouseDown;

    private void ResetDismissInteractiveOnTargetMouseDown() => DismissInteractiveOnTargetMouseDown = false;

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
                imageTransparentColor == default ? SharedStaticVariables.EMPTY_COLOR : imageTransparentColor);
        SyncHooksFor(control);
    }

    /// <summary>
    /// Shows <paramref name="content"/> inside a themed tooltip when <paramref name="control"/> is hovered.
    /// </summary>
    /// <param name="control">The target control.</param>
    /// <param name="content">Control hosted in the popup. Cannot be a <see cref="Form"/> or <paramref name="control"/> itself.</param>
    /// <param name="ownsContent">When <see langword="true"/>, <paramref name="content"/> is disposed with this component (not when the popup hides).</param>
    public void SetToolTip(Control? control, Control? content, bool ownsContent = true) =>
        SetToolTip(control, string.Empty, content, ownsContent);

    /// <summary>
    /// Shows an optional heading plus <paramref name="content"/> inside a themed tooltip when <paramref name="control"/> is hovered.
    /// </summary>
    /// <param name="control">The target control.</param>
    /// <param name="title">Optional heading drawn above the hosted control.</param>
    /// <param name="content">Control hosted in the popup. Cannot be a <see cref="Form"/> or <paramref name="control"/> itself.</param>
    /// <param name="ownsContent">When <see langword="true"/>, <paramref name="content"/> is disposed with this component (not when the popup hides).</param>
    public void SetToolTip(Control? control, string title, Control? content, bool ownsContent = true)
    {
        if (control is null)
        {
            return;
        }

        if (content is null)
        {
            RemoveAssociations(control);
            return;
        }

        if (content is Form || ReferenceEquals(content, control))
        {
            ThrowHelper.ThrowArgumentException(@"Hosted tooltip content cannot be a Form or the hover target.", nameof(content));
        }

        foreach (KeyValuePair<Control, ToolTipAssociation> pair in _associations)
        {
            if (!ReferenceEquals(pair.Key, control) && ReferenceEquals(pair.Value.HostedContent, content))
            {
                ThrowHelper.ThrowArgumentException(@"This control is already hosted by another KryptonToolTip association.", nameof(content));
            }
        }

        DisposeOwnedHostedContent(control);

        _associations[control] = new ToolTipAssociation(
            title ?? string.Empty,
            string.Empty,
            null,
            SharedStaticVariables.EMPTY_COLOR)
        {
            HostedContent = content,
            OwnsContent = ownsContent
        };
        SyncHooksFor(control);
    }

    /// <summary>
    /// Shows the current association for <paramref name="control"/> after the usual delay.
    /// </summary>
    /// <param name="control">Control that already has a <see cref="SetToolTip(Control, string, string, Image, Color)"/> association.</param>
    /// <param name="immediate">When <see langword="true"/>, skip the show delay (used when moving between items).</param>
    /// <remarks>
    /// Call this when tooltip text changes while the pointer is already over the control,
    /// for example when hovering successive <see cref="ListViewItem"/> rows.
    /// </remarks>
    public void ShowFor(Control? control, bool immediate = false)
    {
        if (control == null ||
            !_associations.TryGetValue(control, out ToolTipAssociation? association) ||
            !HasAssociationContent(association) ||
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
    /// <param name="control">Control whose tooltip should be dismissed; ignored when <see langword="null"/>.</param>
    public void HideFor(Control? control)
    {
        if (control == null)
        {
            return;
        }

        if (_hoverControl == null || ReferenceEquals(_hoverControl, control))
        {
            CleanupTransientState(false);
            _hoverControl = null;
        }
    }

    /// <summary>
    /// Convenience for a clickable hyperlink inside a themed tooltip.
    /// </summary>
    /// <param name="control">The target control.</param>
    /// <param name="title">Optional heading.</param>
    /// <param name="linkText">Text shown on the <see cref="KryptonLinkLabel"/>.</param>
    /// <param name="url">Address opened with the shell when the link is clicked.</param>
    public void SetLinkToolTip(Control? control, string title, string linkText, string url)
    {
        if (control is null || string.IsNullOrEmpty(linkText) || string.IsNullOrEmpty(url))
        {
            if (control is not null)
            {
                RemoveAssociations(control);
            }

            return;
        }

        var link = new KryptonLinkLabel
        {
            AutoSize = true,
            Name = @"kryptonToolTipLink"
        };
        link.Values.Text = linkText;
        string capturedUrl = url;
        Control capturedTarget = control;
        link.LinkClicked += (_, _) =>
        {
            var args = new ToolTipLinkClickedEventArgs(capturedTarget, capturedUrl);
            LinkClicked?.Invoke(this, args);
            if (!args.Cancel)
            {
                TryOpenUrl(capturedUrl);
            }
        };

        SetToolTip(control, title ?? string.Empty, link, ownsContent: true);
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
        _associations.TryGetValue(control, out ToolTipAssociation? a)
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
        _associations.TryGetValue(control, out ToolTipAssociation? a)
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
        _associations.TryGetValue(control, out ToolTipAssociation? a)
            ? a.Image
            : null;

    /// <inheritdoc cref="GetKryptonToolTipImage"/>
    public void SetKryptonToolTipImage(Control control, Image? value)
    {
        UpsertImage(control, value);
    }

    /// <summary>
    /// Gets the hosted interactive content control for this target, if any.
    /// Assigning a form control here reparents it into the popup while shown.
    /// </summary>
    [ExtenderProvidedProperty]
    [Category(@"ToolTip")]
    [Description(@"Optional control hosted inside the tooltip. Prefer creating content in code; a form child will leave the form while the tip is shown.")]
    [DefaultValue(null)]
    public Control? GetKryptonToolTipContent(Control control) =>
        _associations.TryGetValue(control, out ToolTipAssociation? a)
            ? a.HostedContent
            : null;

    /// <summary>
    /// Sets hosted interactive tooltip content. Ownership is not taken (the designer/form owns the control).
    /// </summary>
    public void SetKryptonToolTipContent(Control control, Control? value)
    {
        if (value is null)
        {
            if (_associations.TryGetValue(control, out ToolTipAssociation? existing) && existing.IsInteractive)
            {
                existing.HostedContent = null;
                existing.OwnsContent = false;
                if (!HasAssociationContent(existing))
                {
                    RemoveAssociations(control);
                }
            }

            return;
        }

        string title = _associations.TryGetValue(control, out ToolTipAssociation? prior) ? prior.Title : string.Empty;
        SetToolTip(control, title, value, ownsContent: false);
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

    private sealed class ToolTipAssociation
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

        public string Title { get; set; }

        public string Description { get; set; }

        public Image? Image { get; set; }

        public Color ImageTransparentColor { get; set; }

        public Control? HostedContent { get; set; }

        public bool OwnsContent { get; set; }

        public bool IsInteractive => HostedContent is not null;
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
        public Color GetOverlayImageTransparentColor(PaletteState state) => SharedStaticVariables.EMPTY_COLOR;

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
        _associations.TryGetValue(control, out ToolTipAssociation? prior);
        if (prior?.IsInteractive != true
            && !HasRenderableTextOrImage(heading, prior?.Description, prior?.Image))
        {
            RemoveAssociations(control);
            return;
        }

        ToolTipAssociation current = EnsureAssociation(control);
        current.Title = heading;
        SyncHooksFor(control);
    }

    private void UpsertDescription(Control control, string description)
    {
        _associations.TryGetValue(control, out ToolTipAssociation? prior);
        if (prior?.IsInteractive != true
            && !HasRenderableTextOrImage(prior?.Title, description, prior?.Image))
        {
            RemoveAssociations(control);
            return;
        }

        ToolTipAssociation current = EnsureAssociation(control);
        current.Description = description;
        SyncHooksFor(control);
    }

    private void UpsertImage(Control control, Image? image)
    {
        ToolTipAssociation current = EnsureAssociation(control);
        if (!current.IsInteractive && !HasRenderableTextOrImage(current.Title, current.Description, image))
        {
            RemoveAssociations(control);
            return;
        }

        current.Image = image;
        SyncHooksFor(control);
    }

    private ToolTipAssociation EnsureAssociation(Control control)
    {
        if (_associations.TryGetValue(control, out ToolTipAssociation? existing))
        {
            return existing;
        }

        var created = new ToolTipAssociation(string.Empty, string.Empty, null, SharedStaticVariables.EMPTY_COLOR);
        _associations[control] = created;
        return created;
    }

    private static bool HasRenderableTextOrImage(string? title, string? description, Image? image) =>
        !(string.IsNullOrEmpty(title) && string.IsNullOrEmpty(description) && image == null);

    private static bool HasAssociationContent(ToolTipAssociation association) =>
        association.IsInteractive
        || HasRenderableTextOrImage(association.Title, association.Description, association.Image);

    private void DisposeOwnedHostedContent(Control control)
    {
        if (!_associations.TryGetValue(control, out ToolTipAssociation? existing)
            || existing.HostedContent is null
            || !existing.OwnsContent
            || existing.HostedContent.IsDisposed)
        {
            return;
        }

        existing.HostedContent.Dispose();
        existing.HostedContent = null;
    }

    private static void TryOpenUrl(string url)
    {
        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }
        catch
        {
            // Shell execute can fail if the association is missing; ignore so the tooltip stays usable.
        }
    }

    private void RemoveAssociations(Control control)
    {
        DisposeOwnedHostedContent(control);
        _associations.Remove(control);
        _placementRectangles.Remove(control);
        UnhookControlEvents(control);
    }

    private void SyncHooksFor(Control control)
    {
        if (!_associations.TryGetValue(control, out ToolTipAssociation? a) ||
            !HasAssociationContent(a))
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
            RemoveAssociations(c);
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

        StopLingerTimer();

        // Pointer returned to the target while an interactive popup is already showing.
        if (_popup is { IsDisposed: false, IsInteractive: true } && ReferenceEquals(_hoverControl, c))
        {
            return;
        }

        ScheduleShow(c, Cursor.Position);
    }

    private void OnTargetMouseLeave(object? sender, EventArgs e)
    {
        if (IsCurrentInteractivePopupVisible())
        {
            ScheduleLingerDismiss();
            return;
        }

        CleanupTransientState(false);
        _hoverControl = null;
    }

    private void OnTargetMouseDown(object? sender, MouseEventArgs e)
    {
        if (IsCurrentInteractivePopupVisible() && !DismissInteractiveOnTargetMouseDown)
        {
            return;
        }

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
            if (IsCurrentInteractivePopupVisible() && (_popup?.ContainsFocus == true || EnableInteractiveKeyboard))
            {
                return;
            }

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

        if (!_associations.TryGetValue(hc, out ToolTipAssociation? association) ||
            !HasAssociationContent(association))
        {
            return;
        }

        _popup?.Dispose();

        PaletteContentStyle style =
            CommonHelper.ContentStyleFromLabelStyle(ToolTipValues.ToolTipStyle);

        PerControlToolTipContent heading = new PerControlToolTipContent(association);
        bool hasHeading = HasRenderableTextOrImage(association.Title, association.Description, association.Image);

        if (association.HostedContent is { IsDisposed: false } hosted)
        {
            _popup = new VisualPopupToolTip(
                Redirector,
                hosted,
                Renderer,
                PaletteBackStyle.ControlToolTip,
                PaletteBorderStyle.ControlToolTip,
                style,
                ToolTipValues.ToolTipShadow,
                hasHeading ? heading : null,
                keyboardInert: !EnableInteractiveKeyboard);
        }
        else
        {
            _popup = new VisualPopupToolTip(
                Redirector,
                heading,
                Renderer,
                PaletteBackStyle.ControlToolTip,
                PaletteBorderStyle.ControlToolTip,
                style,
                ToolTipValues.ToolTipShadow);
        }

        _popup.Disposed += OnPopupDisposed;
        Point anchor = _showAnchorScreenPoint;
        if (UseLegacyCursorAnchoredPlacement)
        {
            _popup.ShowCalculatingSize(anchor);
        }
        else
        {
            _popup.ShowRelativeTo(hc, anchor, CreateEffectivePositionValues(hc), GetFallbackPlacementRect(hc));
        }

        // Interactive tips stay until leave-both or click-away; hover tips still honor CloseIntervalDelay.
        int closeDelay = ToolTipValues.CloseIntervalDelay;
        if (closeDelay > 0 && (_popup.IsInteractive == false || UseCloseTimerForInteractive))
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

    private bool IsCurrentInteractivePopupVisible() =>
        _popup is { IsDisposed: false, IsInteractive: true };

    private static bool IsPointerOver(Control? control)
    {
        if (control is null || control.IsDisposed || !control.IsHandleCreated || !control.Visible)
        {
            return false;
        }

        return control.RectangleToScreen(control.ClientRectangle).Contains(Control.MousePosition);
    }

    private void ScheduleLingerDismiss()
    {
        if (_lingerTimer is not null)
        {
            return;
        }

        int delay = Math.Max(1, InteractiveLingerDelay);
        _lingerTimer = new Timer { Interval = delay };
        _lingerTimer.Tick += OnLingerTimerTick;
        _lingerTimer.Start();
    }

    private void StopLingerTimer()
    {
        if (_lingerTimer is null)
        {
            return;
        }

        _lingerTimer.Tick -= OnLingerTimerTick;
        _lingerTimer.Stop();
        _lingerTimer.Dispose();
        _lingerTimer = null;
    }

    private void OnLingerTimerTick(object? sender, EventArgs e)
    {
        if (IsPointerOver(_hoverControl) || IsPointerOver(_popup))
        {
            return;
        }

        StopLingerTimer();
        CleanupTransientState(false);
        _hoverControl = null;
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
        StopLingerTimer();

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
        if (!_placementRectangles.TryGetValue(control, out PlacementRectangleAssociation association)
            || !association.IsScreenCoordinates)
        {
            return authored;
        }

        var effective = new PopupPositionValues
        {
            PlacementMode = authored.PlacementMode,
            PlacementTarget = authored.PlacementTarget,
            PlacementRectangle = association.Rectangle
        };

        return effective;
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

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
/// OneNote-style radial popup menu with nested levels, optional <see cref="KryptonCommand"/> binding,
/// Syncfusion-style slider/colour/font editor items, and import from <see cref="KryptonContextMenu"/>.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonRadialMenu), "ToolboxBitmaps.KryptonRadialMenu.bmp")]
[DefaultEvent(nameof(Opening))]
[DefaultProperty(nameof(Items))]
[DesignerCategory(@"code")]
[Designer(typeof(KryptonRadialMenuDesigner))]
[Description(@"Displays a radial shortcut menu in a popup window.")]
public class KryptonRadialMenu : Component
{
    #region Instance Fields

    private bool _disposed;
    private VisualRadialMenuPopup? _popup;
    private PaletteMode _paletteMode;
    private KryptonCustomPaletteBase? _localCustomPalette;
    private ToolStripDropDownCloseReason _closeReason;
    private KryptonContextMenu? _boundContextMenu;
    private bool _importSyncing;
    private TypedHandler<KryptonContextMenuItemBase>? _boundInsertedHandler;
    private TypedHandler<KryptonContextMenuItemBase>? _boundRemovedHandler;
    private EventHandler? _boundClearedHandler;
    private EventHandler? _boundReorderedHandler;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when the radial menu is opening.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the radial menu is opening but not yet displayed.")]
    public event CancelEventHandler? Opening;

    /// <summary>
    /// Occurs when the radial menu is fully opened.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the radial menu is fully opened for display.")]
    public event EventHandler? Opened;

    /// <summary>
    /// Occurs when the radial menu is about to close.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the radial menu is about to close.")]
    public event CancelEventHandler? Closing;

    /// <summary>
    /// Occurs when the radial menu has closed.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the radial menu has been closed.")]
    public event ToolStripDropDownClosedEventHandler? Closed;

    /// <summary>
    /// Occurs when any item is activated.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when a radial menu item is activated.")]
    public event EventHandler<KryptonRadialMenuItemClickEventArgs>? ItemClick;

    /// <summary>
    /// Occurs when the centre button is clicked at the root level (before close).
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the centre button is clicked at the root level.")]
    public event EventHandler? CenterButtonClick;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonRadialMenu"/> class.
    /// </summary>
    public KryptonRadialMenu()
    {
        _paletteMode = PaletteMode.Global;
        Items = [];
        Values = new KryptonRadialMenuValues(OnNeedPaint);
        Enabled = true;
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            DetachContextMenuSync();
            Close();
            _disposed = true;
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
    /// Gets or sets whether the menu can be shown.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether the radial menu can be shown.")]
    [DefaultValue(true)]
    public bool Enabled { get; set; }

    /// <summary>
    /// Gets or sets whether the user can drag the centre button to reposition the open menu.
    /// </summary>
    /// <remarks>
    /// When enabled, drag the centre button to move the popup. A short click without dragging
    /// still performs the usual centre action (back / close).
    /// </remarks>
    [Category(@"Behavior")]
    [Description(@"Allows dragging the centre button to move the open radial menu.")]
    [DefaultValue(false)]
    public bool AllowMove { get; set; }

    /// <summary>
    /// Gets or sets the open / navigation animation style. Proxy for <see cref="KryptonRadialMenuValues.AnimationStyle"/>.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Animation used when the menu opens or navigates to another ring.")]
    [DefaultValue(KryptonRadialMenuAnimationStyle.Sweep)]
    public KryptonRadialMenuAnimationStyle AnimationStyle
    {
        get => Values.AnimationStyle;
        set => Values.AnimationStyle = value;
    }

    /// <summary>
    /// Gets or sets the animation duration in milliseconds. Proxy for <see cref="KryptonRadialMenuValues.AnimationDuration"/>.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Duration of the open / navigation animation in milliseconds.")]
    [DefaultValue(220)]
    public int AnimationDuration
    {
        get => Values.AnimationDuration;
        set => Values.AnimationDuration = value;
    }

    /// <summary>
    /// Gets or sets the palette mode.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Palette applied to drawing.")]
    [DefaultValue(PaletteMode.Global)]
    public PaletteMode PaletteMode
    {
        get => _paletteMode;
        set => _paletteMode = value;
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
        set => _localCustomPalette = value;
    }

    /// <summary>
    /// Gets whether the radial menu is currently visible.
    /// </summary>
    [Browsable(false)]
    public bool Visible => _popup is { IsDisposed: false, Visible: true };

    /// <summary>
    /// Gets a reference to the caller that caused the menu to be shown.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public object? Caller { get; private set; }

    /// <summary>
    /// Gets the outer menu radius. Proxy for <see cref="KryptonRadialMenuValues.MenuRadius"/>.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Outer radius of the radial menu in pixels.")]
    [DefaultValue(140)]
    public int MenuRadius
    {
        get => Values.MenuRadius;
        set => Values.MenuRadius = value;
    }

    /// <summary>
    /// Gets the inner centre radius. Proxy for <see cref="KryptonRadialMenuValues.InnerRadius"/>.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Inner radius of the centre button in pixels.")]
    [DefaultValue(42)]
    public int InnerRadius
    {
        get => Values.InnerRadius;
        set => Values.InnerRadius = value;
    }

    /// <summary>
    /// Gets or sets the centre button image. Proxy for <see cref="KryptonRadialMenuValues.Glyph"/>.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Image displayed on the centre button.")]
    [DefaultValue(null)]
    public Image? Glyph
    {
        get => Values.Glyph;
        set => Values.Glyph = value;
    }

    /// <summary>
    /// Gets or sets the accent colour. Proxy for <see cref="KryptonRadialMenuValues.MenuColor"/>.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Accent colour for the centre button and highlights.")]
    public Color MenuColor
    {
        get => Values.MenuColor;
        set => Values.MenuColor = value;
    }

    private bool ShouldSerializeMenuColor() => !Values.MenuColor.IsEmpty;
    private void ResetMenuColor() => Values.MenuColor = Color.Empty;

    /// <summary>
    /// Gets or sets the submenu hover accent. Proxy for <see cref="KryptonRadialMenuValues.SubMenuHoverColor"/>.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Hover accent colour for submenu sectors.")]
    public Color SubMenuHoverColor
    {
        get => Values.SubMenuHoverColor;
        set => Values.SubMenuHoverColor = value;
    }

    private bool ShouldSerializeSubMenuHoverColor() => !Values.SubMenuHoverColor.IsEmpty;
    private void ResetSubMenuHoverColor() => Values.SubMenuHoverColor = Color.Empty;

    /// <summary>
    /// Gets or sets sector content arrangement. Proxy for <see cref="KryptonRadialMenuValues.DisplayStyle"/>.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"How text and images are arranged in sectors.")]
    [DefaultValue(KryptonRadialMenuDisplayStyle.ImageAboveText)]
    public KryptonRadialMenuDisplayStyle DisplayStyle
    {
        get => Values.DisplayStyle;
        set => Values.DisplayStyle = value;
    }

    /// <summary>
    /// Gets or sets sector image size. Proxy for <see cref="KryptonRadialMenuValues.ItemImageSize"/>.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Size in pixels of images drawn in item sectors.")]
    [DefaultValue(24)]
    public int ItemImageSize
    {
        get => Values.ItemImageSize;
        set => Values.ItemImageSize = value;
    }

    /// <summary>
    /// Gets or sets whether a circular popup shadow is shown. Proxy for <see cref="KryptonRadialMenuValues.ShowShadow"/>.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Shows a circular shadow behind the radial popup.")]
    [DefaultValue(true)]
    public bool ShowShadow
    {
        get => Values.ShowShadow;
        set => Values.ShowShadow = value;
    }

    /// <summary>
    /// Gets or sets whether checked sectors draw a checkmark. Proxy for <see cref="KryptonRadialMenuValues.ShowCheckedGlyph"/>.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Draws a checkmark glyph on checked sectors.")]
    [DefaultValue(true)]
    public bool ShowCheckedGlyph
    {
        get => Values.ShowCheckedGlyph;
        set => Values.ShowCheckedGlyph = value;
    }

    /// <summary>
    /// Show the radial menu at the current mouse location.
    /// </summary>
    /// <param name="caller">Reference to the object causing the menu to be shown.</param>
    /// <returns>True if the menu became displayed.</returns>
    public bool Show(object? caller) => Show(caller, Control.MousePosition);

    /// <summary>
    /// Show the radial menu centred on the provided screen point.
    /// </summary>
    /// <param name="caller">Reference to the object causing the menu to be shown.</param>
    /// <param name="screenPt">Screen location for the menu centre.</param>
    /// <returns>True if the menu became displayed.</returns>
    public bool Show(object? caller, Point screenPt) => ShowPopup(caller, screenPt, animated: true);

    /// <summary>
    /// Show the radial menu centred relative to a control client point.
    /// </summary>
    /// <param name="control">Control providing coordinate space.</param>
    /// <param name="clientPt">Client point inside <paramref name="control"/>.</param>
    /// <returns>True if the menu became displayed.</returns>
    public bool Show(Control control, Point clientPt)
    {
        if (control == null)
        {
            throw new ArgumentNullException(nameof(control));
        }

        return Show(control, control.PointToScreen(clientPt));
    }

    /// <summary>
    /// Show the radial menu with optional open animation.
    /// </summary>
    /// <param name="caller">Reference to the object causing the menu to be shown.</param>
    /// <param name="screenPt">Screen location for the menu centre.</param>
    /// <param name="animated">Whether to animate the open transition.</param>
    /// <returns>True if the menu became displayed.</returns>
    public bool ShowPopup(object? caller, Point screenPt, bool animated)
    {
        if (_disposed || !Enabled)
        {
            return false;
        }

        if (_popup != null)
        {
            return false;
        }

        Caller = caller;

        var cea = new CancelEventArgs();
        OnOpening(cea);
        if (cea.Cancel)
        {
            return false;
        }

        _closeReason = ToolStripDropDownCloseReason.AppFocusChange;

        var palette = ResolvePalette();
        var renderer = palette.GetRenderer();
        _popup = new VisualRadialMenuPopup(this, renderer);
        _popup.Disposed += OnPopupDisposed;
        _popup.ShowCentered(screenPt, animated);

        OnOpened(EventArgs.Empty);
        return true;
    }

    /// <summary>
    /// Close any showing radial menu.
    /// </summary>
    public void Close() => Close(ToolStripDropDownCloseReason.CloseCalled);

    /// <summary>
    /// Close any showing radial menu.
    /// </summary>
    /// <param name="reason">Reason why the menu is being closed.</param>
    public void Close(ToolStripDropDownCloseReason reason)
    {
        if (_popup == null)
        {
            return;
        }

        var cea = new CancelEventArgs();
        OnClosing(cea);
        if (cea.Cancel)
        {
            return;
        }

        _closeReason = reason;
        VisualPopupManager.Singleton.EndPopupTracking(_popup);
    }

    /// <summary>
    /// Clears existing items and imports supported items from a <see cref="KryptonContextMenu"/>.
    /// </summary>
    /// <param name="menu">Source context menu.</param>
    public void ImportFrom(KryptonContextMenu menu) => ImportFrom(menu, liveSync: false);

    /// <summary>
    /// Clears existing items and imports supported items from a <see cref="KryptonContextMenu"/>.
    /// </summary>
    /// <param name="menu">Source context menu.</param>
    /// <param name="liveSync">
    /// When <c>true</c>, re-imports whenever the source <see cref="KryptonContextMenu.Items"/> collection changes.
    /// Does not dual-host the same item instance in both UIs.
    /// </param>
    public void ImportFrom(KryptonContextMenu menu, bool liveSync)
    {
        if (menu == null)
        {
            throw new ArgumentNullException(nameof(menu));
        }

        DetachContextMenuSync();
        PopulateFromContextMenu(menu);

        if (liveSync)
        {
            AttachContextMenuSync(menu);
        }
    }

    /// <summary>
    /// Re-imports from the context menu previously bound with live sync, or no-ops when unbound.
    /// </summary>
    public void RefreshFromContextMenu()
    {
        if (_boundContextMenu == null)
        {
            return;
        }

        PopulateFromContextMenu(_boundContextMenu);
        _popup?.RefreshCurrentLevel();
    }

    /// <summary>
    /// Creates a new radial menu populated from a <see cref="KryptonContextMenu"/>.
    /// </summary>
    /// <param name="menu">Source context menu.</param>
    /// <returns>A new radial menu instance.</returns>
    public static KryptonRadialMenu FromContextMenu(KryptonContextMenu menu) => FromContextMenu(menu, liveSync: false);

    /// <summary>
    /// Creates a new radial menu populated from a <see cref="KryptonContextMenu"/>.
    /// </summary>
    /// <param name="menu">Source context menu.</param>
    /// <param name="liveSync">When <c>true</c>, keeps the radial items in sync with collection changes.</param>
    /// <returns>A new radial menu instance.</returns>
    public static KryptonRadialMenu FromContextMenu(KryptonContextMenu menu, bool liveSync)
    {
        var radial = new KryptonRadialMenu();
        radial.ImportFrom(menu, liveSync);
        return radial;
    }

    #endregion

    #region Internal

    /// <summary>
    /// Raises <see cref="ItemClick"/> for the popup host.
    /// </summary>
    /// <param name="item">Activated item.</param>
    internal void RaiseItemClick(KryptonRadialMenuItemBase item) =>
        ItemClick?.Invoke(this, new KryptonRadialMenuItemClickEventArgs(item));

    /// <summary>
    /// Raises <see cref="CenterButtonClick"/> for the popup host.
    /// </summary>
    internal void OnCenterButtonClick() => CenterButtonClick?.Invoke(this, EventArgs.Empty);

    /// <summary>
    /// Resolves the palette used for radial menu painting.
    /// </summary>
    /// <returns>Active palette.</returns>
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

    #endregion

    #region Protected

    /// <summary>
    /// Raises the <see cref="Opening"/> event.
    /// </summary>
    /// <param name="e">Event arguments.</param>
    protected virtual void OnOpening(CancelEventArgs e) => Opening?.Invoke(this, e);

    /// <summary>
    /// Raises the <see cref="Opened"/> event.
    /// </summary>
    /// <param name="e">Event arguments.</param>
    protected virtual void OnOpened(EventArgs e) => Opened?.Invoke(this, e);

    /// <summary>
    /// Raises the <see cref="Closing"/> event.
    /// </summary>
    /// <param name="e">Event arguments.</param>
    protected virtual void OnClosing(CancelEventArgs e) => Closing?.Invoke(this, e);

    /// <summary>
    /// Raises the <see cref="Closed"/> event.
    /// </summary>
    /// <param name="e">Event arguments.</param>
    protected virtual void OnClosed(ToolStripDropDownClosedEventArgs e) => Closed?.Invoke(this, e);

    #endregion

    #region Implementation

    private void PopulateFromContextMenu(KryptonContextMenu menu)
    {
        _importSyncing = true;
        try
        {
            if (Visible)
            {
                Close(ToolStripDropDownCloseReason.CloseCalled);
            }

            Items.Clear();
            foreach (var item in KryptonRadialMenuContextMenuBridge.ConvertItems(menu.Items))
            {
                Items.Add(item);
            }
        }
        finally
        {
            _importSyncing = false;
        }
    }

    private void AttachContextMenuSync(KryptonContextMenu menu)
    {
        _boundContextMenu = menu;
        _boundInsertedHandler = OnBoundContextMenuCollectionChanged;
        _boundRemovedHandler = OnBoundContextMenuCollectionChanged;
        _boundClearedHandler = OnBoundContextMenuCollectionChanged;
        _boundReorderedHandler = OnBoundContextMenuCollectionChanged;
        menu.Items.Inserted += _boundInsertedHandler;
        menu.Items.Removed += _boundRemovedHandler;
        menu.Items.Cleared += _boundClearedHandler;
        menu.Items.Reordered += _boundReorderedHandler;
    }

    private void DetachContextMenuSync()
    {
        if (_boundContextMenu == null)
        {
            return;
        }

        if (_boundInsertedHandler != null)
        {
            _boundContextMenu.Items.Inserted -= _boundInsertedHandler;
        }

        if (_boundRemovedHandler != null)
        {
            _boundContextMenu.Items.Removed -= _boundRemovedHandler;
        }

        if (_boundClearedHandler != null)
        {
            _boundContextMenu.Items.Cleared -= _boundClearedHandler;
        }

        if (_boundReorderedHandler != null)
        {
            _boundContextMenu.Items.Reordered -= _boundReorderedHandler;
        }

        _boundContextMenu = null;
        _boundInsertedHandler = null;
        _boundRemovedHandler = null;
        _boundClearedHandler = null;
        _boundReorderedHandler = null;
    }

    private void OnBoundContextMenuCollectionChanged(object? sender, EventArgs e)
    {
        if (_importSyncing || _boundContextMenu == null)
        {
            return;
        }

        PopulateFromContextMenu(_boundContextMenu);
    }

    private void OnBoundContextMenuCollectionChanged(object sender, TypedCollectionEventArgs<KryptonContextMenuItemBase> e) =>
        OnBoundContextMenuCollectionChanged(sender, EventArgs.Empty);

    private void OnPopupDisposed(object? sender, EventArgs e)
    {
        if (_popup != null)
        {
            _popup.Disposed -= OnPopupDisposed;
            _popup = null;
        }

        OnClosed(new ToolStripDropDownClosedEventArgs(_closeReason));
    }

    private void OnNeedPaint(object? sender, NeedLayoutEventArgs e)
    {
        if (_popup is { IsDisposed: false })
        {
            _popup.Invalidate();
        }
    }

    #endregion
}

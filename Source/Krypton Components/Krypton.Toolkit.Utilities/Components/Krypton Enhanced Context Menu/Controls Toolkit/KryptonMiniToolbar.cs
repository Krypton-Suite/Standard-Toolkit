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
/// Office-style Mini Toolbar: a compact formatting strip that can appear above a context menu
/// or fade in near a text selection.
/// </summary>
[ToolboxItem(true)]
[DefaultEvent(nameof(ItemClick))]
[DefaultProperty(nameof(Items))]
[DesignerCategory(@"code")]
[Designer(typeof(KryptonMiniToolbarDesigner))]
[Description(@"Compact formatting toolbar used with KryptonEnhancedContextMenu or on text selection.")]
public class KryptonMiniToolbar : Component
{
    #region Static Fields

    private const byte DefaultIdleOpacity = 40;
    private const int DefaultApproachDistance = 80;

    #endregion

    #region Instance Fields

    private bool _disposed;
    private PaletteMode _paletteMode;
    private KryptonCustomPaletteBase? _localCustomPalette;
    private readonly PaletteRedirect _redirector;
    private VisualMiniToolbarPopup? _popup;
    private Control? _attachedControl;
    private TextBoxBase? _attachedEditor;
    private System.Windows.Forms.Timer? _approachTimer;
    private Rectangle _selectionScreenRect;
    private bool _selectionVisible;
    private byte _idleOpacity;
    private int _approachDistance;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when the Mini Toolbar is about to open.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the Mini Toolbar is about to open.")]
    public event CancelEventHandler? Opening;

    /// <summary>
    /// Occurs when the Mini Toolbar is displayed.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the Mini Toolbar is displayed.")]
    public event EventHandler? Opened;

    /// <summary>
    /// Occurs when the Mini Toolbar is about to close.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the Mini Toolbar is about to close.")]
    public event CancelEventHandler? Closing;

    /// <summary>
    /// Occurs when the Mini Toolbar has closed.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the Mini Toolbar has closed.")]
    public event EventHandler? Closed;

    /// <summary>
    /// Occurs when any Mini Toolbar item is activated.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when a Mini Toolbar item is activated.")]
    public event EventHandler<KryptonMiniToolbarItemClickEventArgs>? ItemClick;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonMiniToolbar"/> class.
    /// </summary>
    public KryptonMiniToolbar()
    {
        Items = [];
        _paletteMode = PaletteMode.Global;
        _redirector = new PaletteRedirect(null);
        StateCommon = new PaletteContextMenuRedirect(_redirector, OnNeedPaint);
        ShowShadow = true;
        _idleOpacity = DefaultIdleOpacity;
        _approachDistance = DefaultApproachDistance;
        Enabled = true;
        ResolvePalette();
        KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;
    }

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            KryptonManager.GlobalPaletteChanged -= OnGlobalPaletteChanged;
            Detach();
            Hide();
            _disposed = true;
        }

        base.Dispose(disposing);
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets the Mini Toolbar items.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Items shown on the Mini Toolbar. Edit in the designer or add buttons, combos, and galleries at runtime.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Editor(typeof(KryptonMiniToolbarItemCollectionEditor), typeof(UITypeEditor))]
    [MergableProperty(false)]
    public KryptonMiniToolbarItemCollection Items { get; }

    private bool ShouldSerializeItems() => Items.Count > 0;

    /// <summary>
    /// Gets access to the common appearance values used for Mini Toolbar chrome.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining Mini Toolbar chrome appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteContextMenuRedirect StateCommon { get; }

    private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

    /// <summary>
    /// Gets or sets the palette to be applied.
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
                NotifyPaletteSettingsChanged();
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
                NotifyPaletteSettingsChanged();
            }
        }
    }

    /// <summary>
    /// Gets or sets whether the Mini Toolbar is enabled.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether the Mini Toolbar is enabled.")]
    [DefaultValue(true)]
    public bool Enabled { get; set; }

    /// <summary>
    /// Gets or sets whether the popup draws a shadow.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Indicates whether the Mini Toolbar popup draws a shadow.")]
    [DefaultValue(true)]
    public bool ShowShadow { get; set; }

    /// <summary>
    /// Gets or sets the idle opacity of the selection Mini Toolbar (0-255).
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Idle opacity of the selection Mini Toolbar (0-255).")]
    [DefaultValue(DefaultIdleOpacity)]
    public byte IdleOpacity
    {
        get => _idleOpacity;
        set
        {
            if (_idleOpacity != value)
            {
                _idleOpacity = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets the mouse distance in pixels at which the selection Mini Toolbar becomes fully opaque.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Mouse distance in pixels at which the selection Mini Toolbar becomes fully opaque.")]
    [DefaultValue(DefaultApproachDistance)]
    public int ApproachDistance
    {
        get => _approachDistance;
        set => _approachDistance = Math.Max(1, value);
    }

    /// <summary>
    /// Gets whether a Mini Toolbar popup is currently displayed.
    /// </summary>
    [Browsable(false)]
    public bool IsShowing => _popup != null && !_popup.IsDisposed && _popup.Visible;

    /// <summary>
    /// Shows the Mini Toolbar as a tracked popup above or at the screen rectangle.
    /// </summary>
    /// <param name="screenAnchor">Screen rectangle to position against.</param>
    /// <returns>True if displayed.</returns>
    public bool Show(Rectangle screenAnchor) => Show(screenAnchor, false);

    /// <summary>
    /// Hides any showing Mini Toolbar popup.
    /// </summary>
    public void Hide()
    {
        if (_popup == null)
        {
            return;
        }

        var cea = new CancelEventArgs();
        Closing?.Invoke(this, cea);
        if (cea.Cancel)
        {
            return;
        }

        _popup.Disposed -= OnPopupDisposed;
        if (!_popup.IsDisposed)
        {
            if (VisualPopupManager.Singleton.CurrentPopup == _popup)
            {
                VisualPopupManager.Singleton.EndPopupTracking(_popup);
            }
            else
            {
                _popup.Dispose();
            }
        }

        _popup = null;
        _selectionVisible = false;
        Closed?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Attaches the selection Mini Toolbar to a text editor. Supports <see cref="TextBoxBase"/>,
    /// <see cref="KryptonTextBox"/>, and <see cref="KryptonRichTextBox"/>.
    /// </summary>
    /// <param name="host">Host control.</param>
    public void Attach(Control host)
    {
        ThrowHelper.ThrowIfNull(host);
        Detach();
        _attachedControl = host;
        _attachedEditor = ResolveEditor(host);
        if (_attachedEditor != null)
        {
            _attachedEditor.MouseUp += OnEditorMouseUp;
            _attachedEditor.KeyUp += OnEditorKeyUp;
            _attachedEditor.LostFocus += OnEditorLostFocus;
        }
        else
        {
            host.MouseUp += OnHostMouseUp;
        }
    }

    /// <summary>
    /// Detaches the selection Mini Toolbar from its host control.
    /// </summary>
    public void Detach()
    {
        StopApproachTimer();
        if (_attachedEditor != null)
        {
            _attachedEditor.MouseUp -= OnEditorMouseUp;
            _attachedEditor.KeyUp -= OnEditorKeyUp;
            _attachedEditor.LostFocus -= OnEditorLostFocus;
        }

        if (_attachedControl != null)
        {
            _attachedControl.MouseUp -= OnHostMouseUp;
        }

        _attachedControl = null;
        _attachedEditor = null;
        if (_selectionVisible)
        {
            Hide();
        }
    }

    #endregion

    #region Internal

    /// <summary>
    /// Raised when <see cref="PaletteMode"/>, <see cref="LocalCustomPalette"/>, or the global palette changes.
    /// </summary>
    internal event EventHandler? PaletteSettingsChanged;

    /// <summary>
    /// Resolves the palette used for Mini Toolbar chrome.
    /// </summary>
    /// <returns>Palette instance.</returns>
    internal PaletteBase ResolvePalette()
    {
        PaletteBase palette = _localCustomPalette
                              ?? (_paletteMode == PaletteMode.Global
                                  ? KryptonManager.CurrentGlobalPalette
                                  : KryptonManager.GetPaletteForMode(_paletteMode));
        _redirector.Target = palette;
        return palette;
    }

    /// <summary>
    /// Gets the palette background colour used for Mini Toolbar chrome.
    /// </summary>
    /// <returns>Chrome fill colour.</returns>
    internal Color GetChromeBackColor()
    {
        PaletteBase palette = ResolvePalette();
        Color color = palette.GetBackColor1(PaletteBackStyle.ContextMenuOuter, PaletteState.Normal);
        return color.IsEmpty ? SystemColors.Control : color;
    }

    /// <summary>
    /// Raises <see cref="ItemClick"/> for the specified item.
    /// </summary>
    /// <param name="item">Activated item.</param>
    internal void OnItemActivated(KryptonMiniToolbarItemBase item) =>
        ItemClick?.Invoke(this, new KryptonMiniToolbarItemClickEventArgs(item));

    #endregion

    #region Implementation

    private bool Show(Rectangle screenAnchor, bool selectionMode)
    {
        if (!Enabled)
        {
            return false;
        }

        var cea = new CancelEventArgs();
        Opening?.Invoke(this, cea);
        if (cea.Cancel)
        {
            return false;
        }

        Hide();
        PaletteBase palette = ResolvePalette();
        _popup = new VisualMiniToolbarPopup(this, palette.GetRenderer(), selectionMode);
        _popup.Disposed += OnPopupDisposed;
        _popup.LayoutStrip();
        Size preferred = _popup.CalculatePreferredSize();
        var location = new Point(screenAnchor.Left, screenAnchor.Top - preferred.Height - 2);
        Rectangle working = Screen.GetWorkingArea(screenAnchor);
        if (location.Y < working.Top)
        {
            location.Y = screenAnchor.Bottom + 2;
        }

        if (location.X + preferred.Width > working.Right)
        {
            location.X = Math.Max(working.Left, working.Right - preferred.Width);
        }

        var bounds = new Rectangle(location, preferred);
        if (selectionMode)
        {
            _popup.ApplyOpacity(IdleOpacity);
            _popup.ShowSelection(bounds);
            StartApproachTimer();
            _selectionVisible = true;
        }
        else
        {
            _popup.Show(bounds);
        }

        Opened?.Invoke(this, EventArgs.Empty);
        return true;
    }

    private void OnNeedPaint(object? sender, NeedLayoutEventArgs e) => _popup?.PerformNeedPaint(e.NeedLayout);

    private void OnGlobalPaletteChanged(object? sender, EventArgs e)
    {
        if (_paletteMode == PaletteMode.Global)
        {
            NotifyPaletteSettingsChanged();
        }
    }

    private void NotifyPaletteSettingsChanged()
    {
        ResolvePalette();
        PaletteSettingsChanged?.Invoke(this, EventArgs.Empty);
        OnNeedPaint(this, new NeedLayoutEventArgs(true));
    }

    private void OnPopupDisposed(object? sender, EventArgs e)
    {
        if (_popup != null)
        {
            _popup.Disposed -= OnPopupDisposed;
            _popup = null;
        }

        StopApproachTimer();
        _selectionVisible = false;
        Closed?.Invoke(this, EventArgs.Empty);
    }

    private static TextBoxBase? ResolveEditor(Control host) =>
        host switch
        {
            TextBoxBase textBoxBase => textBoxBase,
            KryptonTextBox kryptonTextBox => kryptonTextBox.TextBox,
            KryptonRichTextBox kryptonRichTextBox => kryptonRichTextBox.RichTextBox,
            _ => null
        };

    private void OnEditorMouseUp(object? sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            TryShowFromSelection();
        }
    }

    private void OnHostMouseUp(object? sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left && _attachedControl != null)
        {
            _selectionScreenRect = _attachedControl.RectangleToScreen(_attachedControl.ClientRectangle);
            Show(_selectionScreenRect, true);
        }
    }

    private void OnEditorKeyUp(object? sender, KeyEventArgs e)
    {
        if (e.Shift || e.KeyCode is Keys.Left or Keys.Right or Keys.Up or Keys.Down or Keys.Home or Keys.End)
        {
            TryShowFromSelection();
        }
    }

    private void OnEditorLostFocus(object? sender, EventArgs e)
    {
        if (_selectionVisible && _popup != null && !_popup.RectangleToScreen(_popup.ClientRectangle).Contains(Control.MousePosition))
        {
            Hide();
        }
    }

    private void TryShowFromSelection()
    {
        if (_attachedEditor == null || _attachedEditor.SelectionLength <= 0)
        {
            if (_selectionVisible)
            {
                Hide();
            }

            return;
        }

        Point start = _attachedEditor.GetPositionFromCharIndex(_attachedEditor.SelectionStart);
        Point end = _attachedEditor.GetPositionFromCharIndex(_attachedEditor.SelectionStart + _attachedEditor.SelectionLength);
        var local = Rectangle.FromLTRB(
            Math.Min(start.X, end.X),
            Math.Min(start.Y, end.Y),
            Math.Max(start.X, end.X) + 8,
            Math.Max(start.Y, end.Y) + _attachedEditor.Font.Height);
        _selectionScreenRect = _attachedEditor.RectangleToScreen(local);
        Show(_selectionScreenRect, true);
    }

    private void StartApproachTimer()
    {
        StopApproachTimer();
        _approachTimer = new System.Windows.Forms.Timer { Interval = 30 };
        _approachTimer.Tick += OnApproachTick;
        _approachTimer.Start();
    }

    private void StopApproachTimer()
    {
        if (_approachTimer != null)
        {
            _approachTimer.Stop();
            _approachTimer.Tick -= OnApproachTick;
            _approachTimer.Dispose();
            _approachTimer = null;
        }
    }

    private void OnApproachTick(object? sender, EventArgs e)
    {
        if (_popup == null || _popup.IsDisposed)
        {
            StopApproachTimer();
            return;
        }

        if (Control.MouseButtons != MouseButtons.None
            && !_popup.RectangleToScreen(_popup.ClientRectangle).Contains(Control.MousePosition)
            && !_selectionScreenRect.Contains(Control.MousePosition))
        {
            Hide();
            return;
        }

        Rectangle popupScreen = _popup.RectangleToScreen(_popup.ClientRectangle);
        Point mouse = Control.MousePosition;
        if (popupScreen.Contains(mouse))
        {
            _popup.ApplyOpacity(255);
            return;
        }

        var dx = mouse.X < popupScreen.Left ? popupScreen.Left - mouse.X : mouse.X > popupScreen.Right ? mouse.X - popupScreen.Right : 0;
        var dy = mouse.Y < popupScreen.Top ? popupScreen.Top - mouse.Y : mouse.Y > popupScreen.Bottom ? mouse.Y - popupScreen.Bottom : 0;
        var distance = Math.Sqrt((dx * dx) + (dy * dy));
        var range = Math.Max(1, ApproachDistance);
        var t = 1.0 - Math.Min(1.0, distance / range);
        var opacity = (byte)(IdleOpacity + ((255 - IdleOpacity) * t));
        _popup.ApplyOpacity(opacity);
    }

    #endregion
}

#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Specifies a component that creates an icon in the notification area.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(NotifyIcon))]
[DefaultEvent(nameof(MouseClick))]
[DefaultProperty(nameof(Text))]
[DesignerCategory(@"code")]
[Description(@"Specifies a component that creates an icon in the notification area.")]
public class KryptonNotifyIcon : Component
{
    #region Instance Fields
    
    private readonly NotifyIcon _notifyIcon;
    private Icon? _icon;
    private string _text;
    private bool _visible;
    private PaletteBase? _palette;
    private PaletteMode _paletteMode;
    private bool _disposed;
   
    #endregion

    #region Events
    
    /// <summary>
    /// Occurs when the user clicks the icon in the notification area.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the user clicks the icon in the notification area.")]
    public event EventHandler? Click;

    /// <summary>
    /// Occurs when the user double-clicks the icon in the notification area.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the user double-clicks the icon in the notification area.")]
    public event EventHandler? DoubleClick;

    /// <summary>
    /// Occurs when the user clicks a mouse button while the pointer is over the icon in the notification area.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the user clicks a mouse button while the pointer is over the icon in the notification area.")]
    public event MouseEventHandler? MouseClick;

    /// <summary>
    /// Occurs when the user double-clicks the icon in the notification area with the mouse.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the user double-clicks the icon in the notification area with the mouse.")]
    public event MouseEventHandler? MouseDoubleClick;

    /// <summary>
    /// Occurs when the user moves the mouse over the icon in the notification area.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the user moves the mouse over the icon in the notification area.")]
    public event MouseEventHandler? MouseMove;

    /// <summary>
    /// Occurs when the user presses a mouse button while the pointer is over the icon in the notification area.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the user presses a mouse button while the pointer is over the icon in the notification area.")]
    public event MouseEventHandler? MouseDown;

    /// <summary>
    /// Occurs when the user releases a mouse button while the pointer is over the icon in the notification area.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the user releases a mouse button while the pointer is over the icon in the notification area.")]
    public event MouseEventHandler? MouseUp;

    /// <summary>
    /// Occurs when the BalloonTip is clicked.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the BalloonTip is clicked.")]
    public event EventHandler? BalloonTipClicked;

    /// <summary>
    /// Occurs when the BalloonTip is closed by the user.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the BalloonTip is closed by the user.")]
    public event EventHandler? BalloonTipClosed;

    /// <summary>
    /// Occurs when the BalloonTip is displayed on the screen.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the BalloonTip is displayed on the screen.")]
    public event EventHandler? BalloonTipShown;
    
    #endregion

    #region Identity
    
    /// <summary>
    /// Initialize a new instance of the KryptonNotifyIcon class.
    /// </summary>
    public KryptonNotifyIcon()
    {
        _text = string.Empty;
        _visible = false;
        _paletteMode = PaletteMode.Global;
        _palette = KryptonManager.CurrentGlobalPalette;

        // Create the underlying NotifyIcon
        _notifyIcon = new NotifyIcon();

        // Hook into events
        _notifyIcon.Click += OnClick;
        _notifyIcon.DoubleClick += OnDoubleClick;
        _notifyIcon.MouseClick += OnMouseClick;
        _notifyIcon.MouseDoubleClick += OnMouseDoubleClick;
        _notifyIcon.MouseMove += OnMouseMove;
        _notifyIcon.MouseDown += OnMouseDown;
        _notifyIcon.MouseUp += OnMouseUp;
        _notifyIcon.BalloonTipClicked += OnBalloonTipClicked;
        _notifyIcon.BalloonTipClosed += OnBalloonTipClosed;
        _notifyIcon.BalloonTipShown += OnBalloonTipShown;

        // Hook into global palette changes
        KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;
    }

    /// <summary>
    /// Initialize a new instance of the KryptonNotifyIcon class with a container.
    /// </summary>
    /// <param name="container">The IContainer that contains this component.</param>
    public KryptonNotifyIcon(IContainer container)
        : this()
    {
        container?.Add(this);
    }
    
    #endregion

    #region Public
    
    /// <summary>
    /// Gets and sets the palette mode.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Sets the palette mode.")]
    [DefaultValue(PaletteMode.Global)]
    public PaletteMode PaletteMode
    {
        get => _paletteMode;

        set
        {
            if (_paletteMode != value)
            {
                // Action depends on new value
                switch (value)
                {
                    case PaletteMode.Custom:
                        // Do nothing, you must have a palette to set
                        break;
                    default:
                        // Use the one of the built in palettes
                        _paletteMode = value;
                        _palette = KryptonManager.GetPaletteForMode(_paletteMode);
                        UpdateIcon();
                        break;
                }
            }
        }
    }

    private bool ShouldSerializePaletteMode() => PaletteMode != PaletteMode.Global;

    private void ResetPaletteMode() => PaletteMode = PaletteMode.Global;

    /// <summary>
    /// Gets and sets the custom palette.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Sets the custom palette to be used.")]
    [DefaultValue(null)]
    public PaletteBase? Palette
    {
        get => _paletteMode == PaletteMode.Custom ? _palette : null;

        set
        {
            // Only interested in changes of value
            if (_palette != value)
            {
                // Remember new palette
                _palette = value;

                // If no custom palette provided, then must be using a built in palette
                if (value == null)
                {
                    _paletteMode = PaletteMode.Global;
                    _palette = KryptonManager.CurrentGlobalPalette;
                }
                else
                {
                    // No longer using a built in palette
                    _paletteMode = PaletteMode.Custom;
                }

                UpdateIcon();
            }
        }
    }

    private bool ShouldSerializePalette() => PaletteMode == PaletteMode.Custom && _palette != null;

    private void ResetPalette()
    {
        PaletteMode = PaletteMode.Global;
        _palette = null;
    }

    /// <summary>
    /// Gets or sets the current icon.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The icon to display in the notification area.")]
    [DefaultValue(null)]
    public Icon? Icon
    {
        get => _icon;

        set
        {
            if (_icon != value)
            {
                _icon?.Dispose();
                _icon = value;
                _notifyIcon.Icon = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets the ToolTip text displayed when the mouse pointer rests on a notification area icon.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The ToolTip text displayed when the mouse pointer rests on a notification area icon.")]
    [DefaultValue("")]
    [Localizable(true)]
    [AllowNull]
    public string Text
    {
        get => _text;

        set
        {
            if (_text != value)
            {
                _text = value ?? string.Empty;
                _notifyIcon.Text = _text;
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the icon is visible in the notification area.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether the icon is visible in the notification area.")]
    [DefaultValue(false)]
    public bool Visible
    {
        get => _visible;

        set
        {
            if (_visible != value)
            {
                _visible = value;
                _notifyIcon.Visible = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets the text to display on the BalloonTip associated with the NotifyIcon.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The text to display on the BalloonTip.")]
    [DefaultValue("")]
    [Localizable(true)]
    [AllowNull]
    public string BalloonTipText
    {
        get => _notifyIcon.BalloonTipText ?? string.Empty;
        set
        {
            if (value != null)
            {
                _notifyIcon.BalloonTipText = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets the title of the BalloonTip displayed on the NotifyIcon.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The title of the BalloonTip.")]
    [DefaultValue("")]
    [Localizable(true)]
    [AllowNull]
    public string BalloonTipTitle
    {
        get => _notifyIcon.BalloonTipTitle ?? string.Empty;
        set
        {
            if (value != null)
            {
                _notifyIcon.BalloonTipTitle = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets the icon to display on the BalloonTip associated with the NotifyIcon.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The icon to display on the BalloonTip.")]
    [DefaultValue(ToolTipIcon.None)]
    public ToolTipIcon BalloonTipIcon
    {
        get => _notifyIcon.BalloonTipIcon;
        set => _notifyIcon.BalloonTipIcon = value;
    }

    /// <summary>
    /// Gets the underlying NotifyIcon instance.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public NotifyIcon NotifyIcon => _notifyIcon;

    /// <summary>
    /// Displays a balloon tip in the taskbar for the specified time period.
    /// </summary>
    /// <param name="timeout">The time period, in milliseconds, the balloon tip should display.</param>
    public void ShowBalloonTip(int timeout) => _notifyIcon.ShowBalloonTip(timeout);

    /// <summary>
    /// Displays a balloon tip with the specified title, text, and icon in the taskbar for the specified time period.
    /// </summary>
    /// <param name="timeout">The time period, in milliseconds, the balloon tip should display.</param>
    /// <param name="tipTitle">The title to display on the balloon tip.</param>
    /// <param name="tipText">The text to display on the balloon tip.</param>
    /// <param name="tipIcon">One of the ToolTipIcon values.</param>
    public void ShowBalloonTip(int timeout, string tipTitle, string tipText, ToolTipIcon tipIcon) => _notifyIcon.ShowBalloonTip(timeout, tipTitle, tipText, tipIcon);

    #endregion

    #region Implementation
    
    private void UpdateIcon()
    {
        // For now, use the icon that was set
        // In the future, this could be customized based on the palette
        if (_icon != null)
        {
            _notifyIcon.Icon = _icon;
        }
    }

    private void OnClick(object? sender, EventArgs e) => Click?.Invoke(this, e);

    private void OnDoubleClick(object? sender, EventArgs e) => DoubleClick?.Invoke(this, e);

    private void OnMouseClick(object? sender, MouseEventArgs e) => MouseClick?.Invoke(this, e);

    private void OnMouseDoubleClick(object? sender, MouseEventArgs e) => MouseDoubleClick?.Invoke(this, e);

    private void OnMouseMove(object? sender, MouseEventArgs e) => MouseMove?.Invoke(this, e);

    private void OnMouseDown(object? sender, MouseEventArgs e) => MouseDown?.Invoke(this, e);

    private void OnMouseUp(object? sender, MouseEventArgs e) => MouseUp?.Invoke(this, e);

    private void OnBalloonTipClicked(object? sender, EventArgs e) => BalloonTipClicked?.Invoke(this, e);

    private void OnBalloonTipClosed(object? sender, EventArgs e) => BalloonTipClosed?.Invoke(this, e);

    private void OnBalloonTipShown(object? sender, EventArgs e) => BalloonTipShown?.Invoke(this, e);

    private void OnGlobalPaletteChanged(object? sender, EventArgs e)
    {
        // Only update if we're using the global palette
        if (_paletteMode == PaletteMode.Global)
        {
            _palette = KryptonManager.CurrentGlobalPalette;
            UpdateIcon();
        }
    }

    #endregion

    #region Disposal

    private new void Dispose(bool isDisposing)
    {
        if (!_disposed)
        {
            _notifyIcon?.Dispose();
        }

        _disposed = true;
    }

    ~KryptonNotifyIcon() => Dispose(false);

    public new void Dispose()
    {
        Dispose(true);

        GC.SuppressFinalize(this);
    }

    #endregion
}


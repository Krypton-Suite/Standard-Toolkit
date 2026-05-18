#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

#if WEBVIEW2_AVAILABLE

#region Using Directives
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;

// Use a using alias to avoid namespace resolution issues
using WebView2Base = Microsoft.Web.WebView2.WinForms.WebView2;

#endregion

/// <summary>
/// Provide a WebView2 control with Krypton styling applied.
/// 
/// <para>
/// KryptonWebView2 is a modern web browser control that integrates Microsoft's WebView2 engine
/// with the Krypton Toolkit's theming system. It provides a consistent look and feel with other
/// Krypton controls while offering modern web rendering capabilities.
/// </para>
/// 
/// <para>
/// Key Features:
/// - Modern web engine based on Microsoft Edge Chromium
/// - Full integration with Krypton theming system
/// - KryptonContextMenu support for custom right-click menus
/// - Designer support for Visual Studio
/// - Async navigation and JavaScript execution capabilities
/// - Customizable zoom levels and background colors
/// </para>
/// 
/// <para>
/// Usage Example:
/// <code>
/// var webView = new KryptonWebView2();
/// webView.KryptonContextMenu = myKryptonContextMenu;
/// 
/// // Initialize and navigate
/// await webView.EnsureCoreWebView2Async();
/// webView.CoreWebView2?.Navigate("https://example.com");
/// </code>
/// </para>
/// 
/// <para>
/// Requirements:
/// - WebView2 Runtime must be installed on the target system
/// - Compatible with .NET Framework 4.7.2+ and .NET 8+
/// - Windows platform only
/// </para>
/// 
/// <para>
/// See also: <see cref="KryptonWebBrowser"/> for legacy WebBrowser control with Krypton theming.
/// </para>
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonWebView2), "KryptonWebView2.ToolboxBitmaps.WebView2.bmp")]
[Designer(typeof(KryptonWebView2Designer))]
[DesignerCategory(@"code")]
[Description(@"Enables the user to browse web pages using the modern WebView2 engine with Krypton theming support. Note: The WebView2 SDK must be installed or available for this control to function.")]
public class KryptonWebView2 : WebView2Base
{
    #region Instance Fields

    private PaletteBase? _palette;
    private readonly PaletteMode _paletteMode = PaletteMode.Global;
    private KryptonContextMenu? _kryptonContextMenu;
    private IRenderer _renderer;
    private readonly PaletteRedirect _redirector;
    private readonly PaletteTripleRedirect _stateCommon;
    private readonly PaletteTriple _stateNormal;
    private readonly PaletteTriple _stateDisabled;
    private readonly PaletteTriple _stateActive;
    private bool _alwaysActive;
    private bool? _fixedActive;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when the value of the BackColor property changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler? BackColorChanged;

    /// <summary>
    /// Occurs when the value of the BackgroundImage property changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler? BackgroundImageChanged;

    /// <summary>
    /// Occurs when the value of the BackgroundImageLayout property changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler? BackgroundImageLayoutChanged;

    /// <summary>
    /// Occurs when the value of the ForeColor property changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler? ForeColorChanged;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the KryptonWebView2 class.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The constructor initializes the control with Krypton-specific styling and behavior:
    /// </para>
    /// <list type="bullet">
    /// <item><description>Enables double buffering for smooth rendering</description></item>
    /// <item><description>Sets up resize redraw for proper repainting</description></item>
    /// <item><description>Initializes the Krypton palette system</description></item>
    /// <item><description>Registers for global palette change notifications</description></item>
    /// </list>
    /// <para>
    /// Note: The WebView2 engine itself is not initialized until EnsureCoreWebView2Async()
    /// is called. This allows for better error handling and async initialization.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// var webView = new KryptonWebView2();
    /// webView.Size = new Size(800, 600);
    /// 
    /// // Initialize the WebView2 engine asynchronously
    /// await webView.EnsureCoreWebView2Async();
    /// </code>
    /// </example>
    public KryptonWebView2()
    {
        // We use double buffering to reduce drawing flicker
        // Use reflection to call SetStyle since it may not be directly accessible in all WebView2 versions
        var setStyleMethod = typeof(Control).GetMethod("SetStyle", BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { typeof(ControlStyles), typeof(bool) }, null);
        if (setStyleMethod != null)
        {
            setStyleMethod.Invoke(this, new object[] { ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true });
            setStyleMethod.Invoke(this, new object[] { ControlStyles.ResizeRedraw, true });
        }

        // Yes, we want to be drawn double buffered by default
        // Use reflection to set DoubleBuffered property since it may not be directly accessible in all WebView2 versions
        var doubleBufferedProperty = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        if (doubleBufferedProperty != null && doubleBufferedProperty.CanWrite)
        {
            doubleBufferedProperty.SetValue(this, true);
        }

        _redirector = new PaletteRedirect(null);
        _stateCommon = new PaletteTripleRedirect(_redirector, PaletteBackStyle.PanelClient, PaletteBorderStyle.ControlClient, PaletteContentStyle.InputControlStandalone, OnPalettePaint);
        _stateNormal = new PaletteTriple(_stateCommon, OnPalettePaint);
        _stateDisabled = new PaletteTriple(_stateCommon, OnPalettePaint);
        _stateActive = new PaletteTriple(_stateCommon, OnPalettePaint);

        SetPalette(KryptonManager.CurrentGlobalPalette);

        // Hook into global palette changes
        KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    /// <remarks>
    /// <para>
    /// This method performs the following cleanup operations:
    /// </para>
    /// <list type="bullet">
    /// <item><description>Unhooks from global palette change notifications</description></item>
    /// <item><description>Disposes of the associated KryptonContextMenu if present</description></item>
    /// <item><description>Calls the base class Dispose method to clean up WebView2 resources</description></item>
    /// </list>
    /// <para>
    /// The WebView2 engine will automatically clean up its native resources when the control is disposed.
    /// </para>
    /// </remarks>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Unhook from global palette changes
            KryptonManager.GlobalPaletteChanged -= OnGlobalPaletteChanged;

            // Dispose of the krypton context menu
            _kryptonContextMenu?.Dispose();
        }

        base.Dispose(disposing);
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets and sets the KryptonContextMenu to show when right-clicked.
    /// </summary>
    /// <value>
    /// A <see cref="KryptonContextMenu"/> instance that will be displayed when the user right-clicks
    /// on the web content, or null if no custom context menu should be shown.
    /// </value>
    /// <remarks>
    /// <para>
    /// When set to a non-null value, this property overrides the default WebView2 context menu
    /// with a Krypton-styled context menu. This allows for consistent theming across your
    /// application while providing custom functionality.
    /// </para>
    /// <para>
    /// The context menu will be shown at the mouse position when right-clicking within the
    /// WebView2 control's client area. If the menu is activated via keyboard (Menu key),
    /// it will be centered on the control.
    /// </para>
    /// <para>
    /// Setting this property to null will restore the default WebView2 context menu behavior.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// var contextMenu = new KryptonContextMenu();
    /// contextMenu.Items.Add(new KryptonContextMenuItem("Copy"));
    /// contextMenu.Items.Add(new KryptonContextMenuItem("Paste"));
    /// 
    /// webView.KryptonContextMenu = contextMenu;
    /// </code>
    /// </example>
    [Category(@"Behavior")]
    [Description(@"The shortcut menu to show when the user right-clicks the page.")]
    [DefaultValue(null)]
    public KryptonContextMenu? KryptonContextMenu
    {
        get => _kryptonContextMenu;

        set
        {
            if (_kryptonContextMenu != value)
            {
                if (_kryptonContextMenu != null)
                {
                    _kryptonContextMenu.Disposed -= OnKryptonContextMenuDisposed;
                }

                _kryptonContextMenu = value;

                if (_kryptonContextMenu != null)
                {
                    _kryptonContextMenu.Disposed += OnKryptonContextMenuDisposed;
                }
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the control allows external drag and drop operations.
    /// </summary>
    /// <value>
    /// <c>true</c> if external drag and drop is allowed; otherwise, <c>false</c>.
    /// </value>
    /// <remarks>
    /// This property forwards to the base WebView2 AllowExternalDrop property.
    /// </remarks>
    [Category(@"Behavior")]
    [Description(@"Indicates whether the control allows external drag and drop operations.")]
    [DefaultValue(false)]
    public new bool AllowExternalDrop
    {
        get
        {
            // Use reflection to access the base class property since it may not be directly accessible in all WebView2 versions
            // Use GetType().BaseType instead of typeof(WebView2) to avoid compile-time type dependency
            var baseType = this.GetType().BaseType;
            if (baseType != null)
            {
                var property = baseType.GetProperty("AllowExternalDrop", BindingFlags.Public | BindingFlags.Instance);
                if (property != null && property.CanRead)
                {
                    var value = property.GetValue(this);
                    return value is bool boolValue && boolValue;
                }
            }
            return false;
        }
        set
        {
            // Use reflection to set the base class property since it may not be directly accessible in all WebView2 versions
            // Use GetType().BaseType instead of typeof(WebView2) to avoid compile-time type dependency
            var baseType = this.GetType().BaseType;
            if (baseType != null)
            {
                var property = baseType.GetProperty("AllowExternalDrop", BindingFlags.Public | BindingFlags.Instance);
                if (property != null && property.CanWrite)
                {
                    property.SetValue(this, value);
                }
            }
        }
    }

    /// <summary>
    /// Gets or sets the creation properties for the WebView2 control.
    /// </summary>
    /// <value>
    /// The creation properties, or null to use default properties.
    /// </value>
    /// <remarks>
    /// This property forwards to the base WebView2 CreationProperties property.
    /// </remarks>
    [Category(@"Behavior")]
    [Description(@"Gets or sets the creation properties for the WebView2 control.")]
    [DefaultValue(null)]
    public new object? CreationProperties
    {
        get
        {
            // Use reflection to access the base class property since it may not be directly accessible in all WebView2 versions
            var baseType = this.GetType().BaseType;
            if (baseType != null)
            {
                var property = baseType.GetProperty("CreationProperties", BindingFlags.Public | BindingFlags.Instance);
                if (property != null && property.CanRead)
                {
                    return property.GetValue(this);
                }
            }
            return null;
        }
        set
        {
            // Use reflection to set the base class property since it may not be directly accessible in all WebView2 versions
            var baseType = this.GetType().BaseType;
            if (baseType != null)
            {
                var property = baseType.GetProperty("CreationProperties", BindingFlags.Public | BindingFlags.Instance);
                if (property != null && property.CanWrite)
                {
                    property.SetValue(this, value);
                }
            }
        }
    }

    /// <summary>
    /// Gets or sets the background color for the control.
    /// </summary>
    [Browsable(false)]
    [Bindable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Color BackColor
    {
        get => GetResolvedBackColor();
        set
        {
            StateCommon.Back.Color1 = value;
            ApplyAppearanceColors();
        }
    }

    /// <summary>
    /// Gets or sets the font of the text displayed by the control.
    /// </summary>
    [Browsable(false)]
    [Bindable(false)]
    [AmbientValue(null)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [AllowNull]
    public override Font Font
    {
        get => base.Font;
        set => base.Font = value;
    }

    /// <summary>
    /// Gets or sets the foreground color for the control.
    /// </summary>
    [Browsable(false)]
    [Bindable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Color ForeColor
    {
        get => GetResolvedForeColor();
        set
        {
            StateCommon.Content.ShortText.Color1 = value;
            ApplyAppearanceColors();
        }
    }

    /// <summary>
    /// Gets or sets the default background color for the WebView2 control.
    /// </summary>
    /// <value>
    /// The default background color shown before web content is rendered.
    /// </value>
    /// <remarks>
    /// This value is synchronized from the current palette state. Use <see cref="StateCommon"/>,
    /// <see cref="StateNormal"/>, <see cref="StateActive"/>, or <see cref="StateDisabled"/> to override palette colors.
    /// </remarks>
    [Browsable(false)]
    [Bindable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Color DefaultBackgroundColor
    {
        get => GetResolvedBackColor();
        set
        {
            StateCommon.Back.Color1 = value;
            ApplyAppearanceColors();
        }
    }

    /// <summary>
    /// Gets or sets the background image displayed in the control.
    /// </summary>
    [Browsable(false)]
    [Bindable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Image? BackgroundImage
    {
        get => base.BackgroundImage;
        set => base.BackgroundImage = value;
    }

    /// <summary>
    /// Gets or sets the background image layout as defined in the ImageLayout enumeration.
    /// </summary>
    [Browsable(false)]
    [Bindable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override ImageLayout BackgroundImageLayout
    {
        get => base.BackgroundImageLayout;
        set => base.BackgroundImageLayout = value;
    }

    /// <summary>
    /// Gets and sets the WebView2 background palette style.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"WebView2 background palette style.")]
    [DefaultValue(PaletteBackStyle.PanelClient)]
    public PaletteBackStyle WebViewBackStyle
    {
        get => StateCommon.BackStyle;

        set
        {
            if (StateCommon.BackStyle != value)
            {
                StateCommon.BackStyle = value;
                ApplyAppearanceColors();
            }
        }
    }

    private bool ShouldSerializeWebViewBackStyle() => WebViewBackStyle != PaletteBackStyle.PanelClient;

    private void ResetWebViewBackStyle() => WebViewBackStyle = PaletteBackStyle.PanelClient;

    /// <summary>
    /// Gets and sets the WebView2 content palette style used for foreground color.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"WebView2 content palette style used for foreground color.")]
    [DefaultValue(PaletteContentStyle.InputControlStandalone)]
    public PaletteContentStyle WebViewContentStyle
    {
        get => StateCommon.ContentStyle;

        set
        {
            if (StateCommon.ContentStyle != value)
            {
                StateCommon.ContentStyle = value;
                ApplyAppearanceColors();
            }
        }
    }

    private bool ShouldSerializeWebViewContentStyle() => WebViewContentStyle != PaletteContentStyle.InputControlStandalone;

    private void ResetWebViewContentStyle() => WebViewContentStyle = PaletteContentStyle.InputControlStandalone;

    /// <summary>
    /// Gets access to the common WebView2 appearance that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common WebView2 appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTripleRedirect StateCommon => _stateCommon;

    private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

    /// <summary>
    /// Gets access to the normal WebView2 appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal WebView2 appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StateNormal => _stateNormal;

    private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;

    /// <summary>
    /// Gets access to the disabled WebView2 appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled WebView2 appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StateDisabled => _stateDisabled;

    private bool ShouldSerializeStateDisabled() => !StateDisabled.IsDefault;

    /// <summary>
    /// Gets access to the active WebView2 appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining active WebView2 appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StateActive => _stateActive;

    private bool ShouldSerializeStateActive() => !StateActive.IsDefault;

    /// <summary>
    /// Gets and sets the value indicating if the control is always in the active state.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Determines if the control is always in the active state.")]
    [DefaultValue(false)]
    public bool AlwaysActive
    {
        get => _alwaysActive;

        set
        {
            if (_alwaysActive != value)
            {
                _alwaysActive = value;
                ApplyAppearanceColors();
            }
        }
    }

    private bool ShouldSerializeAlwaysActive() => _alwaysActive;

    /// <summary>
    /// Sets the fixed state of the control.
    /// </summary>
    /// <param name="active">Should the control be fixed as active.</param>
    public void SetFixedState(bool active) => _fixedActive = active;

    /// <summary>
    /// Gets a value indicating if the WebView2 control is active.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool IsActive => _fixedActive ?? DesignMode || AlwaysActive || ContainsFocus;

    /// <summary>
    /// Ensures that the WebView2 runtime is installed and initializes the CoreWebView2 environment.
    /// </summary>
    /// <param name="environment">A pre-created environment used to create the WebView2 controls.</param>
    /// <returns>A Task that represents the background initialization operation.</returns>
    public new Task EnsureCoreWebView2Async(CoreWebView2Environment? environment = null)
    {
        Task task = base.EnsureCoreWebView2Async(environment);
        return ApplyAppearanceAfterInitializationAsync(task);
    }

    /// <summary>
    /// Ensures that the WebView2 runtime is installed and initializes the CoreWebView2 environment.
    /// </summary>
    /// <param name="environment">A pre-created environment used to create the WebView2 controls.</param>
    /// <param name="controllerOptions">Options flags for the creation of the controller.</param>
    /// <returns>A Task that represents the background initialization operation.</returns>
    public new Task EnsureCoreWebView2Async(CoreWebView2Environment environment, CoreWebView2ControllerOptions controllerOptions)
    {
        Task task = base.EnsureCoreWebView2Async(environment, controllerOptions);
        return ApplyAppearanceAfterInitializationAsync(task);
    }

    /// <summary>
    /// Gets or sets the zoom factor for the WebView2 control.
    /// </summary>
    /// <value>
    /// The zoom factor (1.0 = 100%).
    /// </value>
    /// <remarks>
    /// This property forwards to the base WebView2 ZoomFactor property.
    /// </remarks>
    [Category(@"Behavior")]
    [Description(@"Gets or sets the zoom factor for the WebView2 control.")]
    [DefaultValue(1.0)]
    public new double ZoomFactor
    {
        get
        {
            var baseType = this.GetType().BaseType;
            if (baseType != null)
            {
                var property = baseType.GetProperty("ZoomFactor", BindingFlags.Public | BindingFlags.Instance);
                if (property != null && property.CanRead)
                {
                    var value = property.GetValue(this);
                    if (value is double zoomFactor)
                    {
                        return zoomFactor;
                    }
                }
            }
            return 1.0;
        }
        set
        {
            var baseType = this.GetType().BaseType;
            if (baseType != null)
            {
                var property = baseType.GetProperty("ZoomFactor", BindingFlags.Public | BindingFlags.Instance);
                if (property != null && property.CanWrite)
                {
                    property.SetValue(this, value);
                }
            }
        }
    }

    /// <summary>
    /// Gets or sets which edge of the parent container a control is docked to.
    /// </summary>
    /// <value>
    /// One of the <see cref="DockStyle"/> values. The default is <see cref="DockStyle.None"/>.
    /// </value>
    /// <remarks>
    /// This property forwards to the base Control Dock property.
    /// </remarks>
    [Category(@"Layout")]
    [Description(@"Gets or sets which edge of the parent container a control is docked to.")]
    [DefaultValue(DockStyle.None)]
    public new DockStyle Dock
    {
        get
        {
            // Use reflection to access the base Control Dock property
            var controlType = typeof(Control);
            var property = controlType.GetProperty("Dock", BindingFlags.Public | BindingFlags.Instance);
            if (property != null && property.CanRead)
            {
                var value = property.GetValue(this);
                if (value is DockStyle dockStyle)
                {
                    return dockStyle;
                }
            }
            return DockStyle.None;
        }
        set
        {
            // Use reflection to set the base Control Dock property
            var controlType = typeof(Control);
            var property = controlType.GetProperty("Dock", BindingFlags.Public | BindingFlags.Instance);
            if (property != null && property.CanWrite)
            {
                property.SetValue(this, value);
            }
        }
    }

    #endregion

    #region Protected Overrides

    /// <summary>
    /// Raises the EnabledChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnEnabledChanged(EventArgs e)
    {
        ApplyAppearanceColors();
        base.OnEnabledChanged(e);
    }

    /// <summary>
    /// Raises the GotFocus event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnGotFocus(EventArgs e)
    {
        ApplyAppearanceColors();
        base.OnGotFocus(e);
    }

    /// <summary>
    /// Raises the LostFocus event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnLostFocus(EventArgs e)
    {
        ApplyAppearanceColors();
        base.OnLostFocus(e);
    }

    /// <summary>
    /// Process Windows-based messages.
    /// </summary>
    /// <param name="m">A Windows-based message.</param>
    /// <remarks>
    /// <para>
    /// This method overrides the base WndProc to intercept context menu messages and provide
    /// custom KryptonContextMenu handling. It processes the following message types:
    /// </para>
    /// <list type="bullet">
    /// <item><description>WM_CONTEXTMENU - Context menu activation (right-click or Menu key)</description></item>
    /// <item><description>WM_PARENTNOTIFY with WM_RBUTTONDOWN - Right mouse button down</description></item>
    /// </list>
    /// <para>
    /// When a KryptonContextMenu is assigned, this method will:
    /// </para>
    /// <list type="number">
    /// <item><description>Extract the mouse position from the message parameters</description></item>
    /// <item><description>Handle keyboard activation by centering the menu on the control</description></item>
    /// <item><description>Adjust the position to match standard context menu behavior</description></item>
    /// <item><description>Show the KryptonContextMenu if the mouse is within the client area</description></item>
    /// <item><description>Consume the message to prevent default WebView2 context menu</description></item>
    /// </list>
    /// <para>
    /// If no KryptonContextMenu is assigned, the message is passed to the base class for
    /// default WebView2 context menu handling.
    /// </para>
    /// </remarks>
    protected override void WndProc(ref Message m)
    {
        if ((m.Msg == WebView2MessageHelper.WM_.CONTEXTMENU) || 
            (m.Msg == WebView2MessageHelper.WM_.PARENTNOTIFY && WebView2MessageHelper.LOWORD(m.WParam) == WebView2MessageHelper.WM_.RBUTTONDOWN))
        {
            // Only interested in overriding the behavior when we have a krypton context menu
            if (KryptonContextMenu != null)
            {
                // Extract the screen mouse position (if might not actually be provided)
                var mousePt = new Point(WebView2MessageHelper.LOWORD(m.LParam), WebView2MessageHelper.HIWORD(m.LParam));

                // If keyboard activated, the menu position is centered
                if (((int)m.LParam) == -1)
                {
                    // Use reflection to access Size property since it may not be directly accessible
                    var sizeProperty = typeof(Control).GetProperty("Size", BindingFlags.Public | BindingFlags.Instance);
                    if (sizeProperty != null && sizeProperty.GetValue(this) is Size size)
                    {
                        mousePt = new Point(size.Width / 2, size.Height / 2);
                    }
                    else
                    {
                        // Fallback: use ClientSize if available
                        var clientSizeProperty = typeof(Control).GetProperty("ClientSize", BindingFlags.Public | BindingFlags.Instance);
                        if (clientSizeProperty != null && clientSizeProperty.GetValue(this) is Size clientSize)
                        {
                            mousePt = new Point(clientSize.Width / 2, clientSize.Height / 2);
                        }
                        else
                        {
                            // Last resort: default to a reasonable center point
                            mousePt = new Point(100, 100);
                        }
                    }
                }
                else
                {
                    if (m.Msg == WebView2MessageHelper.WM_.CONTEXTMENU)
                    {
                        // Use reflection to call PointToClient since it may not be directly accessible
                        var pointToClientMethod = typeof(Control).GetMethod("PointToClient", BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(Point) }, null);
                        if (pointToClientMethod != null)
                        {
                            mousePt = (Point)pointToClientMethod.Invoke(this, new object[] { mousePt })!;
                        }
                    }

                    // Mouse point up and left 1 pixel so that the mouse overlaps the top left corner
                    // of the showing context menu just like it happens for a ContextMenuStrip.
                    mousePt.X -= 1;
                    mousePt.Y -= 1;
                }

                // If the mouse position is within our client area
                // Use reflection to access ClientRectangle since it may not be directly accessible
                var clientRectangleProperty = typeof(Control).GetProperty("ClientRectangle", BindingFlags.Public | BindingFlags.Instance);
                var clientRect = clientRectangleProperty?.GetValue(this) as Rectangle?;
                if (clientRect.HasValue && clientRect.Value.Contains(mousePt))
                {
                    // Show the context menu
                    // Use reflection to call PointToScreen since it may not be directly accessible
                    var pointToScreenMethod = typeof(Control).GetMethod("PointToScreen", BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(Point) }, null);
                    if (pointToScreenMethod != null)
                    {
                        var screenPoint = (Point)pointToScreenMethod.Invoke(this, new object[] { mousePt })!;
                        KryptonContextMenu.Show(this, screenPoint);
                    }
                    else
                    {
                        // Fallback: show at mouse position if PointToScreen is not available
                        KryptonContextMenu.Show(this, mousePt);
                    }

                    // We eat the message!
                    return;
                }
            }
        }

        base.WndProc(ref m);
    }

    private void OnKryptonContextMenuDisposed(object? sender, EventArgs e) =>
        // When the current krypton context menu is disposed, we should remove
        // it to prevent it being used again, as that would just throw an exception
        // because it has been disposed.
        KryptonContextMenu = null;

    #endregion

    #region Palette Controls

    /// <summary>Sets the palette being used.</summary>
    /// <param name="palette">The chosen palette.</param>
    private void SetPalette(PaletteBase palette)
    {
        if (palette != _palette)
        {
            // Unhook from current palette events
            if (_palette != null)
            {
                _palette.BasePaletteChanged -= OnBaseChanged;
                _palette.BaseRendererChanged -= OnBaseChanged;
            }

            // Remember the new palette
            _palette = palette;
            _redirector.Target = palette;

            // Get the renderer associated with the palette
            _renderer = _palette.GetRenderer();

            // Hook to new palette events
            if (_palette != null)
            {
                _palette.BasePaletteChanged += OnBaseChanged;
                _palette.BaseRendererChanged += OnBaseChanged;
            }

            ApplyAppearanceColors();
        }
    }

    /// <summary>Called when there is a change in base renderer or base palette.</summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    private void OnBaseChanged(object? sender, EventArgs e)
    {
        // Change in base renderer or base palette require we fetch the latest renderer
        _renderer = _palette!.GetRenderer();
        ApplyAppearanceColors();
    }

    private void OnPalettePaint(object? sender, NeedLayoutEventArgs e) => ApplyAppearanceColors();

    private IPaletteTriple GetTripleState() => Enabled ? (IsActive ? _stateActive : _stateNormal) : _stateDisabled;

    private PaletteState GetPaletteState()
    {
        if (!Enabled)
        {
            return PaletteState.Disabled;
        }

        return IsActive ? PaletteState.Tracking : PaletteState.Normal;
    }

    private Color GetResolvedBackColor()
    {
        IPaletteTriple triple = GetTripleState();
        return triple.PaletteBack.GetBackColor1(GetPaletteState());
    }

    private Color GetResolvedForeColor()
    {
        IPaletteTriple triple = GetTripleState();
        return triple.PaletteContent!.GetContentShortTextColor1(GetPaletteState());
    }

    private void ApplyAppearanceColors()
    {
        if (_palette == null)
        {
            return;
        }

        Color backColor = GetResolvedBackColor();
        Color foreColor = GetResolvedForeColor();

        SetControlBackColor(backColor);
        SetControlForeColor(foreColor);
        SetBaseDefaultBackgroundColor(backColor);
    }

    private void SetControlBackColor(Color color)
    {
        var property = typeof(Control).GetProperty(nameof(BackColor), BindingFlags.Public | BindingFlags.Instance);
        property?.SetValue(this, color);
    }

    private void SetControlForeColor(Color color)
    {
        var property = typeof(Control).GetProperty(nameof(ForeColor), BindingFlags.Public | BindingFlags.Instance);
        property?.SetValue(this, color);
    }

    private void SetBaseDefaultBackgroundColor(Color color)
    {
        var baseType = GetType().BaseType;
        if (baseType != null)
        {
            var property = baseType.GetProperty("DefaultBackgroundColor", BindingFlags.Public | BindingFlags.Instance);
            property?.SetValue(this, color);
        }
    }

    private async Task ApplyAppearanceAfterInitializationAsync(Task initializationTask)
    {
        await initializationTask.ConfigureAwait(true);
        ApplyAppearanceColors();
    }

    /// <summary>
    /// Occurs when the global palette has been changed.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected virtual void OnGlobalPaletteChanged(object? sender, EventArgs e)
    {
        // We only care if we are using the global palette
        if (_paletteMode == PaletteMode.Global)
        {
            // Update self with the new global palette
            SetPalette(KryptonManager.CurrentGlobalPalette);
        }
    }

    /// <summary>
    /// Create a tool strip renderer appropriate for the current renderer/palette pair.
    /// </summary>
    /// <returns>
    /// A <see cref="ToolStripRenderer"/> instance that provides Krypton-themed rendering
    /// for tool strips, or null if no renderer is available.
    /// </returns>
    /// <remarks>
    /// <para>
    /// This method creates a tool strip renderer that matches the current Krypton theme
    /// and palette. The renderer is used internally by the control to ensure consistent
    /// theming across all UI elements.
    /// </para>
    /// <para>
    /// The method uses the resolved palette (either the control's specific palette or
    /// the global palette) to create the appropriate renderer for the current theme.
    /// </para>
    /// <para>
    /// This method is primarily used internally by the Krypton framework but can be
    /// called by advanced users who need custom tool strip rendering.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// var renderer = webView.CreateToolStripRenderer();
    /// if (renderer != null)
    /// {
    ///     myToolStrip.Renderer = renderer;
    /// }
    /// </code>
    /// </example>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public ToolStripRenderer? CreateToolStripRenderer()
    {
        var palette = GetResolvedPalette() ?? KryptonManager.CurrentGlobalPalette;
        return _renderer?.RenderToolStrip(palette);
    }

    /// <summary>
    /// Gets the resolved palette to actually use when drawing.
    /// </summary>
    /// <returns>
    /// A <see cref="PaletteBase"/> instance that represents the currently active palette
    /// for this control, or null if no palette is set.
    /// </returns>
    /// <remarks>
    /// <para>
    /// This method returns the palette that is currently being used for rendering this control.
    /// The resolved palette is determined by the control's palette mode and the global palette
    /// settings.
    /// </para>
    /// <para>
    /// This method is primarily used internally by the Krypton framework for rendering
    /// operations and theme consistency.
    /// </para>
    /// </remarks>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public PaletteBase? GetResolvedPalette() => _palette;

    #endregion
}

#else

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

/// <summary>
/// Stub when WebView2 SDK/package is not available. Run dotnet restore or add WebView2 SDK for full implementation.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonWebView2), "KryptonWebView2.ToolboxBitmaps.WebView2.bmp")]
[Description(@"WebView2 with Krypton theming. WebView2 SDK or package required; run dotnet restore or add WebView2 SDK.")]
public class KryptonWebView2 : Control
{
    public KryptonWebView2()
    {
        BackColor = SystemColors.Control;
    }
}

#endif

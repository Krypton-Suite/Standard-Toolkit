#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

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
[ToolboxBitmap(typeof(WebView2), "ToolboxBitmaps.WebView2.bmp")]
[Designer(typeof(KryptonWebView2Designer))]
[DesignerCategory(@"code")]
[Description(@"Enables the user to browse web pages using the modern WebView2 engine with Krypton theming support.")]
public class KryptonWebView2 : WebView2
{
    #region Instance Fields
    private PaletteBase _palette;
    private readonly PaletteMode _paletteMode = PaletteMode.Global;
    private KryptonContextMenu? _kryptonContextMenu;
    private IRenderer _renderer;
    #endregion Instance Fields

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
    /// Note: The WebView2 engine itself is not initialized until <see cref="EnsureCoreWebView2Async()"/>
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
        SetStyle(ControlStyles.OptimizedDoubleBuffer |
                 ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.UserPaint, true);

        // We need to repaint entire control whenever resized
        SetStyle(ControlStyles.ResizeRedraw, true);

        // Yes, we want to be drawn double buffered by default
        DoubleBuffered = true;

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
    #endregion Identity

    #region Public
    /// <summary>
    /// Gets and sets the KryptonContextMenu to show when right clicked.
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
        if ((m.Msg == PI.WM_.CONTEXTMENU) || 
            (m.Msg == PI.WM_.PARENTNOTIFY && PI.LOWORD(m.WParam) == PI.WM_.RBUTTONDOWN))
        {
            // Only interested in overriding the behavior when we have a krypton context menu
            if (KryptonContextMenu != null)
            {
                // Extract the screen mouse position (if might not actually be provided)
                var mousePt = new Point(PI.LOWORD(m.LParam), PI.HIWORD(m.LParam));

                // If keyboard activated, the menu position is centered
                if (((int)(long)m.LParam) == -1)
                {
                    mousePt = new Point(Width / 2, Height / 2);
                }
                else
                {
                    if (m.Msg == PI.WM_.CONTEXTMENU)
                    {
                        mousePt = PointToClient(mousePt);
                    }

                    // Mouse point up and left 1 pixel so that the mouse overlaps the top left corner
                    // of the showing context menu just like it happens for a ContextMenuStrip.
                    mousePt.X -= 1;
                    mousePt.Y -= 1;
                }

                // If the mouse position is within our client area
                if (ClientRectangle.Contains(mousePt))
                {
                    // Show the context menu
                    KryptonContextMenu.Show(this, PointToScreen(mousePt));

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

    #endregion Public

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

            // Get the renderer associated with the palette
            _renderer = _palette.GetRenderer();

            // Hook to new palette events
            if (_palette != null)
            {
                _palette.BasePaletteChanged += OnBaseChanged;
                _palette.BaseRendererChanged += OnBaseChanged;
            }
        }
    }

    /// <summary>Called when there is a change in base renderer or base palette.</summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    private void OnBaseChanged(object? sender, EventArgs e) =>
        // Change in base renderer or base palette require we fetch the latest renderer
        _renderer = _palette.GetRenderer();

    /// <summary>
    /// Occurs when the global palette has been changed.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected virtual void OnGlobalPaletteChanged(object sender, EventArgs e)
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
    /// for this control.
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
    public PaletteBase GetResolvedPalette() => _palette;

    #endregion Palette Controls
}

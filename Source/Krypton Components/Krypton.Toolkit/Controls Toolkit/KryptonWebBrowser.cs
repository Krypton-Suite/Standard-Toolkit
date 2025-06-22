#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Provide a WebBrowser control with Krypton styling applied.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(WebBrowser), "ToolboxBitmaps.WebBrowser.bmp")]
[Designer(typeof(KryptonWebBrowserDesigner))]
[DesignerCategory(@"code")]
[Description(@"Enables the user to browse web page, inside your form. Mainly to be used as a Rich Text Editor")]
public class KryptonWebBrowser : WebBrowser
{
    #region Instance Fields
    private PaletteBase _palette;
    private readonly PaletteMode _paletteMode = PaletteMode.Global;
    private KryptonContextMenu? _kryptonContextMenu;
    private IRenderer _renderer;
    #endregion Instance Fields

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonWebBrowser class.
    /// </summary>
    public KryptonWebBrowser()
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

    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
        }

        base.Dispose(disposing);
    }
    #endregion


    #region MenuStrip Overrides

    /// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ContextMenuStrip" /> associated with this control.</summary>
    /// <returns>The <see cref="T:System.Windows.Forms.ContextMenuStrip" /> for this control, or <see langword="null" /> if there is no <see cref="T:System.Windows.Forms.ContextMenuStrip" />. The default is <see langword="null" />.</returns>
    [Category(@"Behavior")]
    [Description(@"Consider using KryptonContextMenu within the behaviors section.\nThe Winforms shortcut menu to show when the user right-clicks the page.\nNote: The ContextMenu will be rendered.")]
    [DefaultValue(null)]
    public override ContextMenuStrip? ContextMenuStrip
    {
        //[DebuggerStepThrough]
        get => base.ContextMenuStrip;

        set
        {
            // Unhook from any current menu strip
            if (base.ContextMenuStrip != null)
            {
                base.ContextMenuStrip.Opening -= OnContextMenuStripOpening;
            }

            // Let parent handle actual storage
            base.ContextMenuStrip = value;

            // Hook into the strip being shown (so we can set the correct renderer)
            if (base.ContextMenuStrip != null)
            {
                base.ContextMenuStrip.Opening += OnContextMenuStripOpening;
            }
        }
    }

    /// <summary>
    /// Gets and sets the KryptonContextMenu to show when right clicked.
    /// </summary>
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

    private void OnContextMenuStripOpening(object? sender, CancelEventArgs e)
    {
        // Get the actual strip instance
        ContextMenuStrip? cms = base.ContextMenuStrip;

        // Make sure it has the correct renderer
        cms!.Renderer = CreateToolStripRenderer();
    }


    /// <summary>
    /// Process Windows-based messages.
    /// </summary>
    /// <param name="m">A Windows-based message.</param>
    protected override void WndProc(ref Message m)
    {
        if ( (m.Msg == PI.WM_.CONTEXTMENU)  // For some reason this is not being fired here, therefore the Menu KeyDown is also being lost.
             || (m.Msg == PI.WM_.PARENTNOTIFY && PI.LOWORD(m.WParam) == PI.WM_.RBUTTONDOWN)    // Hack to intercept the mouse button due to the above
           )
        {
            // Only interested in overriding the behavior when we have a krypton context menu...
            if (KryptonContextMenu != null)
            {
                // Extract the screen mouse position (if might not actually be provided)
                var mousePt = new Point(PI.LOWORD(m.LParam), PI.HIWORD(m.LParam));

                // If keyboard activated, the menu position is centered
                if (((int) (long) m.LParam) == -1)
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

    #endregion MenuStrip Overrides

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
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public ToolStripRenderer CreateToolStripRenderer() => _renderer.RenderToolStrip(GetResolvedPalette());

    /// <summary>
    /// Gets the resolved palette to actually use when drawing.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public PaletteBase GetResolvedPalette() => _palette;

    #endregion Palette Controls
}
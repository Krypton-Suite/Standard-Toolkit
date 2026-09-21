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
/// Popup host for a standalone or selection <see cref="KryptonMiniToolbar"/>.
/// </summary>
internal sealed class VisualMiniToolbarPopup : VisualPopup
{
    #region Instance Fields

    private readonly KryptonMiniToolbar _owner;
    private readonly MiniToolbarStrip _strip;
    private readonly bool _selectionMode;
    private byte _opacity = 255;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="VisualMiniToolbarPopup"/> class.
    /// </summary>
    /// <param name="owner">Owning Mini Toolbar.</param>
    /// <param name="renderer">Renderer.</param>
    /// <param name="selectionMode">True for a non-activating selection Mini Toolbar.</param>
    public VisualMiniToolbarPopup(KryptonMiniToolbar owner, IRenderer? renderer, bool selectionMode)
        : base(new ViewManager(), renderer, owner.ShowShadow)
    {
        _owner = owner;
        _selectionMode = selectionMode;
        var docker = new ViewDrawDocker(owner.StateCommon.ControlOuter.Back, owner.StateCommon.ControlOuter.Border, null);
        docker.Add(new ViewLayoutNull(), ViewDockStyle.Fill);
        ViewManager!.Control = this;
        ViewManager.AlignControl = this;
        ViewManager.Root = docker;

        _strip = new MiniToolbarStrip(owner);
        _strip.Rebuild();
        Controls.Add(_strip);
        ApplyChromeColors();
        _owner.PaletteSettingsChanged += OnOwnerPaletteSettingsChanged;
    }

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _owner.PaletteSettingsChanged -= OnOwnerPaletteSettingsChanged;
        }

        base.Dispose(disposing);
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets the hosted strip.
    /// </summary>
    public MiniToolbarStrip Strip => _strip;

    /// <summary>
    /// Shows the popup at the specified screen rectangle without taking activation.
    /// </summary>
    /// <param name="screenRect">Screen bounds.</param>
    public void ShowSelection(Rectangle screenRect)
    {
        SetBounds(screenRect.X, screenRect.Y, screenRect.Width, screenRect.Height);
        PI.ShowWindow(Handle, PI.ShowWindowCommands.SW_SHOWNOACTIVATE);
        ApplyOpacity(_opacity);
    }

    /// <summary>
    /// Applies layered-window opacity.
    /// </summary>
    /// <param name="opacity">0-255 alpha.</param>
    public void ApplyOpacity(byte opacity)
    {
        _opacity = opacity;
        if (!IsHandleCreated || !_selectionMode)
        {
            return;
        }

        PI.SetLayeredWindowAttributes(Handle, 0, opacity, PI.LWA_.ALPHA);
    }

    /// <summary>
    /// Calculates the preferred popup size including chrome padding.
    /// </summary>
    /// <returns>Preferred size.</returns>
    public Size CalculatePreferredSize()
    {
        Size stripSize = _strip.GetPreferredSize(new Size(int.MaxValue, 0));
        return new Size(stripSize.Width + 8, stripSize.Height + 8);
    }

    /// <summary>
    /// Positions the strip inside the chrome padding.
    /// </summary>
    public void LayoutStrip()
    {
        Size preferred = CalculatePreferredSize();
        if (ClientSize != preferred)
        {
            ClientSize = preferred;
        }

        _strip.Location = new Point(4, 4);
    }

    /// <inheritdoc />
    public override bool DoesStackedClientMouseDownBecomeCurrent(Message m, Point pt) =>
        base.DoesStackedClientMouseDownBecomeCurrent(m, pt) || IsOwnedComboDropDown(Control.MousePosition);

    /// <inheritdoc />
    public override bool AllowMouseMove(Message m, Point pt) =>
        base.AllowMouseMove(m, pt) || IsOwnedComboDropDown(pt);

    #endregion

    #region Protected

    /// <inheritdoc />
    protected override CreateParams CreateParams
    {
        get
        {
            CreateParams cp = base.CreateParams;
            if (_selectionMode)
            {
                cp.ExStyle |= unchecked((int)(PI.WS_EX_.LAYERED | PI.WS_EX_.NOACTIVATE));
            }

            return cp;
        }
    }

    /// <inheritdoc />
    protected override void WndProc(ref Message m)
    {
        if (_selectionMode && m.Msg == PI.WM_.MOUSEACTIVATE)
        {
            m.Result = (IntPtr)PI.MA_NOACTIVATE;
            return;
        }

        base.WndProc(ref m);
    }

    /// <inheritdoc />
    protected override void OnKeyDown(KeyEventArgs e)
    {
        if (!IsDisposed && e.KeyData == Keys.Escape)
        {
            VisualPopupManager.Singleton.EndCurrentTracking();
        }

        base.OnKeyDown(e);
    }

    /// <inheritdoc />
    protected override void OnLayout(LayoutEventArgs lEvent)
    {
        if (ViewManager?.Root == null || Renderer == null)
        {
            return;
        }

        base.OnLayout(lEvent);
        if (IsDisposed || Renderer == null)
        {
            return;
        }

        LayoutStrip();
        using var context = new RenderContext(this, null, ClientRectangle, Renderer);
        using var gh = new GraphicsHint(context.Graphics,
            _owner.StateCommon.ControlOuter.Border.GetBorderGraphicsHint(PaletteState.Normal));
        Rectangle borderRect = ClientRectangle;
        GraphicsPath borderPath1 = Renderer.RenderStandardBorder.GetOutsideBorderPath(context, borderRect, _owner.StateCommon.ControlOuter.Border, VisualOrientation.Top, PaletteState.Normal);
        borderRect.Inflate(-1, -1);
        GraphicsPath borderPath2 = Renderer.RenderStandardBorder.GetOutsideBorderPath(context, borderRect, _owner.StateCommon.ControlOuter.Border, VisualOrientation.Top, PaletteState.Normal);
        borderRect.Inflate(-1, -1);
        GraphicsPath borderPath3 = Renderer.RenderStandardBorder.GetOutsideBorderPath(context, borderRect, _owner.StateCommon.ControlOuter.Border, VisualOrientation.Top, PaletteState.Normal);
        Region = new Region(borderPath1);
        DefineShadowPaths(borderPath1, borderPath2, borderPath3);
    }

    #endregion

    #region Implementation

    private void OnOwnerPaletteSettingsChanged(object? sender, EventArgs e)
    {
        ApplyChromeColors();
        if (IsHandleCreated)
        {
            BeginInvoke(new Action(RebuildAfterPaletteChange));
        }
    }

    private void RebuildAfterPaletteChange()
    {
        if (IsDisposed)
        {
            return;
        }

        _strip.Rebuild();
        ApplyChromeColors();
        LayoutStrip();
        Invalidate(true);
    }

    private void ApplyChromeColors()
    {
        Color chrome = _owner.GetChromeBackColor();
        BackColor = chrome;
        _strip.ApplyPalette();
    }

    private bool IsOwnedComboDropDown(Point screenPt)
    {
        var screenPIPt = new PI.POINT
        {
            X = screenPt.X,
            Y = screenPt.Y
        };
        var hWnd = PI.WindowFromPoint(screenPIPt);
        if (hWnd == IntPtr.Zero || PI.GetClassNameString(hWnd) != "ComboLBox")
        {
            return false;
        }

        var owner = PI.GetParent(hWnd);
        if (owner == IntPtr.Zero)
        {
            owner = PI.GetWindow(hWnd, PI.GetWindowType.GW_OWNER);
        }

        foreach (Control control in _strip.Controls)
        {
            if (control is KryptonComboBox combo
                && (combo.Handle == owner || combo.ComboBox.Handle == owner))
            {
                return true;
            }
        }

        return false;
    }

    #endregion
}

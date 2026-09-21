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
/// Context-menu popup used by <see cref="KryptonEnhancedContextMenu"/>. The Mini Toolbar is a separate window.
/// </summary>
internal sealed class VisualEnhancedContextMenu : VisualPopup
{
    #region Instance Fields

    private readonly KryptonEnhancedContextMenu _owner;
    private readonly PaletteRedirect _redirector;
    private readonly PaletteRedirectContextMenu _redirectorImages;
    private readonly ContextMenuProvider _provider;
    private readonly ViewLayoutStack _viewColumns;
    private readonly List<ViewLayoutContextMenuOverflowColumn> _overflowColumns = [];
    private PaletteBase? _palette;
    private readonly ViewDrawDocker _drawDocker;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="VisualEnhancedContextMenu"/> class.
    /// </summary>
    public VisualEnhancedContextMenu(KryptonEnhancedContextMenu owner,
        KryptonContextMenu menu,
        bool keyboardActivated)
        : base(menu.StateCommon.HasShadow)
    {
        _owner = owner;

        ViewManager = new ViewContextMenuManager(this, new ViewLayoutNull());
        // StateCommon is wired to the menu redirector; reuse it so GetMetricPadding has a palette target.
        _redirector = menu.Redirector;
        _redirectorImages = menu.RedirectorImages;
        SetPalette(menu.LocalCustomPalette ?? KryptonManager.GetPaletteForMode(menu.PaletteMode));

        _viewColumns = new ViewLayoutStack(true);
        _provider = new ContextMenuProvider(menu, (ViewContextMenuManager)ViewManager, _viewColumns,
            _palette, menu.PaletteMode, _redirector, _redirectorImages,
            NeedPaintDelegate, menu.Enabled);
        _provider.Closing += OnProviderClosing;
        _provider.Close += OnProviderClose;
        _provider.Dispose += OnProviderDispose;

        menu.Items.GenerateView(_provider, this, _viewColumns, true, true, NeedPaintDelegate);
        WrapColumnsForOverflow();

        var menuBackground = new ViewDrawCanvas(_provider.ProviderStateCommon.ControlInner.Back,
            _provider.ProviderStateCommon.ControlInner.Border, VisualOrientation.Top)
        {
            _viewColumns
        };

        var layoutDocker = new ViewLayoutDocker();
        Padding outerPadding = _provider.ProviderRedirector.GetMetricPadding(null, PaletteState.Normal, PaletteMetricPadding.ContextMenuItemOuter);
        layoutDocker.Add(new ViewLayoutSeparator(outerPadding.Top), ViewDockStyle.Top);
        layoutDocker.Add(new ViewLayoutSeparator(outerPadding.Bottom), ViewDockStyle.Bottom);
        layoutDocker.Add(new ViewLayoutSeparator(outerPadding.Left), ViewDockStyle.Left);
        layoutDocker.Add(new ViewLayoutSeparator(outerPadding.Right), ViewDockStyle.Right);
        layoutDocker.Add(menuBackground, ViewDockStyle.Fill);

        _drawDocker = new ViewDrawDocker(_provider.ProviderStateCommon.ControlOuter.Back, _provider.ProviderStateCommon.ControlOuter.Border, null)
        {
            { layoutDocker, ViewDockStyle.Fill }
        };
        _drawDocker.KeyController = new ContextMenuController(ViewManager as ViewContextMenuManager);
        ViewManager.Root = _drawDocker;

        if (keyboardActivated)
        {
            ((ViewContextMenuManager)ViewManager).KeyDown();
        }
    }

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_palette != null)
            {
                _palette.PalettePaintInternal -= OnPaletteNeedPaint;
                _palette.BasePaletteChanged -= OnBaseChanged;
                _palette.BaseRendererChanged -= OnBaseChanged;
            }
        }

        base.Dispose(disposing);
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets the close reason from the provider.
    /// </summary>
    public ToolStripDropDownCloseReason? CloseReason => _provider.ProviderCloseReason;

    /// <summary>
    /// Discovers the preferred size of the menu chrome.
    /// </summary>
    /// <returns>Preferred size.</returns>
    public Size GetPreferredSize()
    {
        SuspendLayout();
        try
        {
            using var context = new ViewLayoutContext(this, Renderer);
            return ViewManager!.Root.GetPreferredSize(context);
        }
        finally
        {
            ResumeLayout();
        }
    }

    /// <summary>
    /// Show at the provided screen bounds.
    /// </summary>
    /// <param name="screenBounds">Screen rectangle.</param>
    public void ShowAt(Rectangle screenBounds)
    {
        ShowHorz = KryptonContextMenuPositionH.After;
        ShowVert = KryptonContextMenuPositionV.Top;
        base.Show(screenBounds);
    }

    /// <summary>
    /// Gets and sets the horizontal setting used to position sub menus.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonContextMenuPositionH ShowHorz
    {
        get => _provider.ProviderShowHorz;
        set => _provider.ProviderShowHorz = value;
    }

    /// <summary>
    /// Gets and sets the vertical setting used to position sub menus.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonContextMenuPositionV ShowVert
    {
        get => _provider.ProviderShowVert;
        set => _provider.ProviderShowVert = value;
    }

    #endregion

    #region Protected

    /// <inheritdoc />
    protected override void OnKeyDown(KeyEventArgs e)
    {
        if (!IsDisposed && e.KeyData == Keys.Escape)
        {
            _provider.ProviderCloseReason = ToolStripDropDownCloseReason.Keyboard;
        }

        base.OnKeyDown(e);
    }

    /// <inheritdoc />
    protected override void OnLayout(LayoutEventArgs levent)
    {
        base.OnLayout(levent);
        if (IsDisposed || Renderer == null)
        {
            return;
        }

        using var context = new RenderContext(this, null, ClientRectangle, Renderer);
        using var gh = new GraphicsHint(context.Graphics,
            _provider.ProviderStateCommon.ControlOuter.Border.GetBorderGraphicsHint(PaletteState.Normal));
        Rectangle borderRect = ClientRectangle;
        GraphicsPath borderPath1 = Renderer.RenderStandardBorder.GetOutsideBorderPath(context, borderRect, _provider.ProviderStateCommon.ControlOuter.Border, VisualOrientation.Top, PaletteState.Normal);
        borderRect.Inflate(-1, -1);
        GraphicsPath borderPath2 = Renderer.RenderStandardBorder.GetOutsideBorderPath(context, borderRect, _provider.ProviderStateCommon.ControlOuter.Border, VisualOrientation.Top, PaletteState.Normal);
        borderRect.Inflate(-1, -1);
        GraphicsPath borderPath3 = Renderer.RenderStandardBorder.GetOutsideBorderPath(context, borderRect, _provider.ProviderStateCommon.ControlOuter.Border, VisualOrientation.Top, PaletteState.Normal);
        Region = new Region(borderPath1);
        DefineShadowPaths(borderPath1, borderPath2, borderPath3);
    }

    #endregion

    #region Implementation

    private void SetPalette(PaletteBase? palette)
    {
        palette ??= KryptonManager.CurrentGlobalPalette;
        if (palette != _palette)
        {
            if (_palette is not null)
            {
                _palette.PalettePaintInternal -= OnPaletteNeedPaint;
                _palette.BasePaletteChanged -= OnBaseChanged;
                _palette.BaseRendererChanged -= OnBaseChanged;
            }

            _palette = palette;
            _redirector.Target = _palette;
            Renderer = _palette.GetRenderer();
            _palette.PalettePaintInternal += OnPaletteNeedPaint;
            _palette.BasePaletteChanged += OnBaseChanged;
            _palette.BaseRendererChanged += OnBaseChanged;
        }
        else if (_redirector.Target == null && _palette != null)
        {
            _redirector.Target = _palette;
            Renderer = _palette.GetRenderer();
        }
    }

    private void OnBaseChanged(object? sender, EventArgs e) => Renderer = _palette!.GetRenderer();

    private void OnPaletteNeedPaint(object? sender, NeedLayoutEventArgs e) => OnNeedPaint(sender, e);

    private void OnProviderClosing(object? sender, CancelEventArgs e) => _owner.RaiseClosing(e);

    private void OnProviderClose(object? sender, CloseReasonEventArgs e) => _owner.Close(e.CloseReason);

    private void OnProviderDispose(object? sender, EventArgs e)
    {
        _provider.Dispose -= OnProviderDispose;
        Dispose();
    }

    private void WrapColumnsForOverflow()
    {
        _overflowColumns.Clear();
        if (ViewManager is not ViewContextMenuManager contextMenuManager)
        {
            return;
        }

        for (var i = 0; i < _viewColumns.Count; i++)
        {
            WrapOverflowTargets(_viewColumns[i], contextMenuManager);
        }

        contextMenuManager.OverflowColumns = _overflowColumns;
    }

    private void WrapOverflowTargets(ViewBase? columnRoot, ViewContextMenuManager contextMenuManager)
    {
        if (columnRoot == null)
        {
            return;
        }

        if (columnRoot is ViewLayoutStack { Horizontal: false } column)
        {
            WrapItemStack(column, contextMenuManager);
        }
    }

    private void WrapItemStack(ViewLayoutStack itemStack, ViewContextMenuManager contextMenuManager)
    {
        if (itemStack.Count == 0)
        {
            return;
        }

        var overflowColumn = new ViewLayoutContextMenuOverflowColumn(_provider, contextMenuManager, NeedPaintDelegate);
        overflowColumn.Adopt(itemStack);
        itemStack.Add(overflowColumn);
        _overflowColumns.Add(overflowColumn);
    }

    #endregion
}

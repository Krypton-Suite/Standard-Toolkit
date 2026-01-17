#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2026 - 2026. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Utilities;

/// <summary>
/// Provides a fully custom PrintPreviewControl with complete Krypton theming support.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonPrintPreviewControl), "ToolboxBitmaps.KryptonPrintPreviewControl.bmp")]
[DefaultEvent(nameof(StartPageChanged))]
[DefaultProperty(nameof(Document))]
[DesignerCategory(@"code")]
[Description(@"Displays a PrintDocument in a preview format with full Krypton theming support.")]
public class KryptonPrintPreviewControl : VisualPanel
{
    #region Instance Fields

    private PrintDocument? _document;
    private readonly PrintPreviewPageCache _pageCache;
    private double _zoom = 0.3;
    private int _columns = 1;
    private int _rows = 1;
    private int _startPage = 0;
    private bool _useAntiAlias = true;
    private bool _autoZoom = false;
    private Point _scrollPosition = Point.Empty;
    private Size _totalSize = Size.Empty;
    private bool _isGenerating;
    private bool _isPanning;
    private Point _panStart;
    private Point _scrollStart;
    private readonly PaletteDoubleRedirect _stateCommon;
    private readonly PaletteDouble? _stateDisabled;
    private readonly PaletteDouble? _stateNormal;
    private readonly PaletteDouble? _stateActive;
    private PaletteBackStyle _pageBackStyle = PaletteBackStyle.PanelAlternate;
    private readonly PaletteDoubleRedirect? _pageStateCommon;
    private readonly PaletteDouble? _pageStateNormal;
    private readonly PaletteDouble? _pageStateDisabled;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when the starting page changes.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs when the starting page changes.")]
    public event EventHandler? StartPageChanged;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the KryptonPrintPreviewControl class.
    /// </summary>
    public KryptonPrintPreviewControl()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.UserPaint |
                 ControlStyles.OptimizedDoubleBuffer |
                 ControlStyles.ResizeRedraw, true);

        // Create the palette storage for control background
        _stateCommon = new PaletteDoubleRedirect(Redirector!, PaletteBackStyle.PanelClient, PaletteBorderStyle.ControlClient, NeedPaintDelegate);
        _stateDisabled = new PaletteDouble(_stateCommon, NeedPaintDelegate);
        _stateNormal = new PaletteDouble(_stateCommon, NeedPaintDelegate);
        _stateActive = new PaletteDouble(_stateCommon, NeedPaintDelegate);

        // Create separate palette for page backgrounds
        var pagePaletteRedirect = new PaletteRedirect(Redirector!);
        _pageStateCommon = new PaletteDoubleRedirect(pagePaletteRedirect, _pageBackStyle, PaletteBorderStyle.ControlClient, NeedPaintDelegate);
        _pageStateDisabled = new PaletteDouble(_pageStateCommon, NeedPaintDelegate);
        _pageStateNormal = new PaletteDouble(_pageStateCommon, NeedPaintDelegate);

        // Initialize page cache
        _pageCache = new PrintPreviewPageCache();

        // Enable auto-scrolling
        AutoScroll = true;

        // Enable mouse wheel
        MouseWheel += OnMouseWheel;
        MouseDown += OnMouseDown;
        MouseMove += OnMouseMove;
        MouseUp += OnMouseUp;
        MouseLeave += OnMouseLeave;
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _pageCache?.Clear();
        }

        base.Dispose(disposing);
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets access to the common print preview control appearance that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common print preview control appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack StateCommon => _stateCommon.Back;

    private bool ShouldSerializeStateCommon() => !_stateCommon.Back.IsDefault;

    /// <summary>
    /// Gets access to the disabled print preview control appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled print preview control appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack StateDisabled => _stateDisabled!.Back;

    private bool ShouldSerializeStateDisabled() => !_stateDisabled!.Back.IsDefault;

    /// <summary>
    /// Gets access to the normal print preview control appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal print preview control appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack StateNormal => _stateNormal!.Back;

    private bool ShouldSerializeStateNormal() => !_stateNormal!.Back.IsDefault;

    /// <summary>
    /// Gets or sets the PrintDocument to preview.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The PrintDocument to preview.")]
    [DefaultValue(null)]
    public PrintDocument? Document
    {
        get => _document;
        set
        {
            if (_document != value)
            {
                _document = value;
                InvalidatePreview();
            }
        }
    }

    /// <summary>
    /// Gets or sets the number of pages displayed horizontally across the page.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The number of pages displayed horizontally across the page.")]
    [DefaultValue(1)]
    public int Columns
    {
        get => _columns;
        set
        {
            if (value < 1) value = 1;
            if (_columns != value)
            {
                _columns = value;
                InvalidatePreview();
            }
        }
    }

    /// <summary>
    /// Gets or sets the number of pages displayed vertically down the page.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The number of pages displayed vertically down the page.")]
    [DefaultValue(1)]
    public int Rows
    {
        get => _rows;
        set
        {
            if (value < 1) value = 1;
            if (_rows != value)
            {
                _rows = value;
                InvalidatePreview();
            }
        }
    }

    /// <summary>
    /// Gets or sets the zoom level of the pages.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The zoom level of the pages.")]
    [DefaultValue(0.3)]
    public double Zoom
    {
        get => _zoom;
        set
        {
            if (value < 0.1) value = 0.1;
            if (value > 5.0) value = 5.0;
            if (Math.Abs(_zoom - value) > 0.001)
            {
                _zoom = value;
                InvalidatePreview();
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the control automatically resizes to fit its contents.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether the control automatically resizes to fit its contents.")]
    [DefaultValue(false)]
    public bool AutoZoom
    {
        get => _autoZoom;
        set
        {
            if (_autoZoom != value)
            {
                _autoZoom = value;
                if (value)
                {
                    CalculateAutoZoom();
                }
                InvalidatePreview();
            }
        }
    }

    /// <summary>
    /// Gets or sets the starting page number.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The starting page number.")]
    [DefaultValue(0)]
    public int StartPage
    {
        get => _startPage;
        set
        {
            var maxPage = Math.Max(0, _pageCache.PageCount - 1);
            if (value < 0) value = 0;
            if (value > maxPage) value = maxPage;
            if (_startPage != value)
            {
                _startPage = value;
                OnStartPageChanged(EventArgs.Empty);
                InvalidatePreview();
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether anti-aliasing is used when rendering the page.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether anti-aliasing is used when rendering the page.")]
    [DefaultValue(true)]
    public bool UseAntiAlias
    {
        get => _useAntiAlias;
        set
        {
            if (_useAntiAlias != value)
            {
                _useAntiAlias = value;
                Invalidate();
            }
        }
    }

    /// <summary>
    /// Gets the total number of pages in the document.
    /// </summary>
    [Browsable(false)]
    public int PageCount => _pageCache.PageCount;

    /// <summary>
    /// Gets and sets the panel style for page backgrounds.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Panel style for page backgrounds.")]
    [DefaultValue(PaletteBackStyle.PanelAlternate)]
    public PaletteBackStyle PageBackStyle
    {
        get => _pageBackStyle;
        set
        {
            if (_pageBackStyle != value)
            {
                _pageBackStyle = value;
                if (_pageStateCommon != null)
                {
                    _pageStateCommon.BackStyle = value;
                    Invalidate();
                }
            }
        }
    }

    private bool ShouldSerializePageBackStyle() => PageBackStyle != PaletteBackStyle.PanelAlternate;

    private void ResetPageBackStyle() => PageBackStyle = PaletteBackStyle.PanelAlternate;

    #endregion

    #region Protected

    /// <summary>
    /// Raises the EnabledChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnEnabledChanged(EventArgs e)
    {
        // Change in enabled state requires a repaint
        Invalidate();
        base.OnEnabledChanged(e);
    }

    /// <summary>
    /// Raises the Resize event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        if (_autoZoom)
        {
            CalculateAutoZoom();
            InvalidatePreview();
        }
        else
        {
            InvalidatePreview();
        }
    }

    /// <summary>
    /// Raises the Paint event.
    /// </summary>
    /// <param name="e">A PaintEventArgs that contains the event data.</param>
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        if (DesignMode)
        {
            // Draw design-time placeholder
            using var brush = new SolidBrush(Color.LightGray);
            e.Graphics.FillRectangle(brush, ClientRectangle);
            using var font = new Font("Arial", 12);
            using var textBrush = new SolidBrush(Color.DarkGray);
            var text = "KryptonPrintPreviewControl";
            var size = e.Graphics.MeasureString(text, font);
            var x = (ClientRectangle.Width - size.Width) / 2;
            var y = (ClientRectangle.Height - size.Height) / 2;
            e.Graphics.DrawString(text, font, textBrush, x, y);
            return;
        }

        // Get background color from palette
        var backColor = Enabled
            ? _stateNormal!.Back.GetBackColor1(PaletteState.Normal)
            : _stateDisabled!.Back.GetBackColor1(PaletteState.Disabled);

        // Fill background
        using (var brush = new SolidBrush(backColor))
        {
            e.Graphics.FillRectangle(brush, ClientRectangle);
        }

        // Generate pages if needed
        if (_document != null && _pageCache.PageCount == 0 && !_isGenerating)
        {
            GeneratePreview();
            if (_autoZoom)
            {
                CalculateAutoZoom();
            }
        }

        // Render pages
        if (_pageCache.PageCount > 0)
        {
            RenderPages(e.Graphics, e.ClipRectangle);
        }
    }

    /// <summary>
    /// Raises the StartPageChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnStartPageChanged(EventArgs e) => StartPageChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the PaletteChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected override void OnPaletteChanged(EventArgs e)
    {
        _stateCommon.SetRedirector(Redirector!);
        if (_pageStateCommon != null)
        {
            var pagePaletteRedirect = new PaletteRedirect(Redirector!);
            _pageStateCommon.SetRedirector(pagePaletteRedirect);
        }
        Invalidate();
        base.OnPaletteChanged(e);
    }

    /// <summary>
    /// Handles mouse wheel events for zooming.
    /// </summary>
    private void OnMouseWheel(object? sender, MouseEventArgs e)
    {
        if (ModifierKeys == Keys.Control)
        {
            // Zoom in/out with Ctrl+Wheel
            var zoomDelta = e.Delta > 0 ? 0.1 : -0.1;
            Zoom = Math.Max(0.1, Math.Min(5.0, Zoom + zoomDelta));
        }
        else
        {
            // Default scrolling behavior
            base.OnMouseWheel(e);
        }
    }

    /// <summary>
    /// Handles mouse down events for panning.
    /// </summary>
    private void OnMouseDown(object? sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Middle || (e.Button == MouseButtons.Left && ModifierKeys == Keys.Shift))
        {
            _isPanning = true;
            _panStart = e.Location;
            _scrollStart = AutoScrollPosition;
            Cursor = Cursors.Hand;
            Capture = true;
        }
    }

    /// <summary>
    /// Handles mouse move events for panning.
    /// </summary>
    private void OnMouseMove(object? sender, MouseEventArgs e)
    {
        if (_isPanning)
        {
            var deltaX = e.X - _panStart.X;
            var deltaY = e.Y - _panStart.Y;
            AutoScrollPosition = new Point(
                -(_scrollStart.X - deltaX),
                -(_scrollStart.Y - deltaY));
        }
    }

    /// <summary>
    /// Handles mouse up events to end panning.
    /// </summary>
    private void OnMouseUp(object? sender, MouseEventArgs e)
    {
        if (_isPanning && (e.Button == MouseButtons.Middle || e.Button == MouseButtons.Left))
        {
            _isPanning = false;
            Cursor = Cursors.Default;
            Capture = false;
        }
    }

    /// <summary>
    /// Handles mouse leave events to reset panning.
    /// </summary>
    private void OnMouseLeave(object? sender, EventArgs e)
    {
        if (_isPanning)
        {
            _isPanning = false;
            Cursor = Cursors.Default;
            Capture = false;
        }
    }

    #endregion

    #region Implementation

    private void InvalidatePreview()
    {
        CalculateTotalSize();
        Invalidate();
    }

    private void GeneratePreview()
    {
        if (_document == null || _isGenerating)
        {
            return;
        }

        _isGenerating = true;
        try
        {
            _pageCache.GeneratePages(_document);
            CalculateTotalSize();
            Invalidate();
        }
        finally
        {
            _isGenerating = false;
        }
    }

    private void CalculateTotalSize()
    {
        if (_pageCache.PageCount == 0)
        {
            _totalSize = Size.Empty;
            AutoScrollMinSize = Size.Empty;
            return;
        }

        var firstPage = _pageCache.GetPage(0);
        if (firstPage == null)
        {
            _totalSize = Size.Empty;
            AutoScrollMinSize = Size.Empty;
            return;
        }

        var pageSize = firstPage.Image.Size;
        var scaledWidth = (int)(pageSize.Width * _zoom);
        var scaledHeight = (int)(pageSize.Height * _zoom);

        // Calculate spacing between pages
        const int pageSpacing = 20;
        var totalWidth = (_columns * scaledWidth) + ((_columns - 1) * pageSpacing);
        var totalHeight = (_rows * scaledHeight) + ((_rows - 1) * pageSpacing);

        _totalSize = new Size(totalWidth, totalHeight);
        AutoScrollMinSize = _totalSize;
    }

    private void CalculateAutoZoom()
    {
        if (_pageCache.PageCount == 0 || ClientSize.Width == 0 || ClientSize.Height == 0)
        {
            return;
        }

        var firstPage = _pageCache.GetPage(0);
        if (firstPage == null)
        {
            return;
        }

        var pageSize = firstPage.Image.Size;
        var availableWidth = ClientSize.Width - (AutoScroll ? SystemInformation.VerticalScrollBarWidth : 0) - 40;
        var availableHeight = ClientSize.Height - (AutoScroll ? SystemInformation.HorizontalScrollBarHeight : 0) - 40;

        var zoomX = availableWidth / (double)(_columns * pageSize.Width);
        var zoomY = availableHeight / (double)(_rows * pageSize.Height);

        _zoom = Math.Min(zoomX, zoomY);
        if (_zoom < 0.1) _zoom = 0.1;
        if (_zoom > 5.0) _zoom = 5.0;
    }

    private void RenderPages(Graphics g, Rectangle clipRect)
    {
        if (_pageCache.PageCount == 0)
        {
            return;
        }

        // Set up anti-aliasing
        if (_useAntiAlias)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        }

        // Get page size from first page
        var firstPage = _pageCache.GetPage(0);
        if (firstPage == null)
        {
            return;
        }

        var pageSize = firstPage.Image.Size;
        var scaledWidth = (int)(pageSize.Width * _zoom);
        var scaledHeight = (int)(pageSize.Height * _zoom);
        const int pageSpacing = 20;

        // Get border color from palette
        var borderColor = Enabled
            ? _stateNormal!.Border.GetBorderColor1(PaletteState.Normal)
            : _stateDisabled!.Border.GetBorderColor1(PaletteState.Disabled);

        // Calculate starting position
        var startX = AutoScrollPosition.X;
        var startY = AutoScrollPosition.Y;

        // Render pages in grid
        var pageIndex = _startPage;
        for (var row = 0; row < _rows && pageIndex < _pageCache.PageCount; row++)
        {
            for (var col = 0; col < _columns && pageIndex < _pageCache.PageCount; col++)
            {
                var page = _pageCache.GetPage(pageIndex);
                if (page == null)
                {
                    pageIndex++;
                    continue;
                }

                var x = startX + (col * (scaledWidth + pageSpacing));
                var y = startY + (row * (scaledHeight + pageSpacing));
                var pageRect = new Rectangle(x, y, scaledWidth, scaledHeight);

                // Only render if visible
                if (clipRect.IntersectsWith(pageRect))
                {
                    // Draw page shadow
                    var shadowRect = new Rectangle(pageRect.X + 3, pageRect.Y + 3, pageRect.Width, pageRect.Height);
                    using (var shadowBrush = new SolidBrush(Color.FromArgb(50, Color.Black)))
                    {
                        g.FillRectangle(shadowBrush, shadowRect);
                    }

                    // Draw page background using palette color (PanelAlternate style)
                    var pageBackColor = Enabled
                        ? _pageStateNormal!.Back.GetBackColor1(PaletteState.Normal)
                        : _pageStateDisabled!.Back.GetBackColor1(PaletteState.Disabled);
                    using (var pageBrush = new SolidBrush(pageBackColor))
                    {
                        g.FillRectangle(pageBrush, pageRect);
                    }

                    // Draw page border
                    using (var borderPen = new Pen(borderColor, 1))
                    {
                        g.DrawRectangle(borderPen, pageRect);
                    }

                    // Draw page content
                    g.DrawImage(page.Image, pageRect, new Rectangle(0, 0, page.Image.Width, page.Image.Height), GraphicsUnit.Pixel);
                }

                pageIndex++;
            }
        }
    }

    #endregion
}

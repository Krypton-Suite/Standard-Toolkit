#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Drawing;

/// <summary>
/// Displays a static SVG picture. Markup is parsed once and rasterized again when the
/// size mode, client size, or DPI changes. The control does not play SMIL animation.
/// </summary>
/// <remarks>
/// This type does not derive from <see cref="PictureBox"/>. A picture box serializes its
/// raster <see cref="PictureBox.Image"/>, which would replace the SVG source in the designer.
/// </remarks>
[Category(@"code")]
[ToolboxBitmap(typeof(PictureBox))]
[DefaultProperty(nameof(Svg))]
[Description(@"Displays a scalable vector picture from SVG markup.")]
[ToolboxItem(true)]
public class KryptonSVGPictureBox : Control
{
    private const int MaximumRasterEdge = 8192;

    private readonly ToolTipManager _toolTipManager;
    private readonly ViewLayoutNull _toolTipTarget = new ViewLayoutNull();

    private VisualPopupToolTip? _visualPopupToolTip;
    private string _svg = string.Empty;
    private PictureBoxSizeMode _sizeMode = PictureBoxSizeMode.Normal;
    private IKryptonSvgDocument? _document;
    private Bitmap? _raster;
    private Size _rasterLogicalSize;
    private int _rasterDpi;
    private PictureBoxSizeMode _rasterSizeMode;
    private int _documentGeneration;
    private int _rasterGeneration = -1;

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonSVGPictureBox"/> class.
    /// </summary>
    public KryptonSVGPictureBox()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint
                 | ControlStyles.UserPaint
                 | ControlStyles.OptimizedDoubleBuffer
                 | ControlStyles.ResizeRedraw
                 | ControlStyles.SupportsTransparentBackColor, true);
        BackColor = Color.Transparent;

        ToolTipValues = new ToolTipValues(null, GetDpiFactor);
        _toolTipManager = new ToolTipManager(ToolTipValues);
        _toolTipManager.ShowToolTip += OnShowToolTip;
        _toolTipManager.CancelToolTip += OnCancelToolTip;
    }

    /// <summary>
    /// Gets or sets the SVG markup drawn by the control.
    /// </summary>
    /// <remarks>
    /// An empty string clears the picture. Markup that cannot be parsed is kept, the picture
    /// is cleared, and <see cref="SvgError"/> receives the parser message. The setter does not throw.
    /// </remarks>
    [Category(@"Appearance")]
    [DefaultValue("")]
    [Description(@"SVG markup drawn by the control.")]
    public string Svg
    {
        get => _svg;
        set
        {
            var next = value ?? string.Empty;
            if (string.Equals(_svg, next, StringComparison.Ordinal))
            {
                return;
            }

            _svg = next;
            ReloadDocument();
            ApplyAutoSize();
            Invalidate();
        }
    }

    /// <summary>
    /// Gets or sets how the picture is arranged inside the client area.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(PictureBoxSizeMode.Normal)]
    [Description(@"How the SVG is arranged inside the client area.")]
    public PictureBoxSizeMode SizeMode
    {
        get => _sizeMode;
        set
        {
            if (_sizeMode == value)
            {
                return;
            }

            _sizeMode = value;
            ApplyAutoSize();
            Invalidate();
        }
    }

    /// <summary>
    /// Gets the message from the last failed parse, or <see langword="null"/> when the current markup is empty or valid.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string? SvgError { get; private set; }

    /// <summary>
    /// Gets the rasterizer that draws <see cref="Svg"/> on this operating system.
    /// </summary>
    /// <remarks>
    /// Windows 10 version 1703 and later use Direct2D. Earlier Windows versions use Svg.Skia.
    /// The value is chosen once for the process and is not serialized.
    /// </remarks>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonSvgRasterizer SvgRasterizer => KryptonSvgRasterizerChoice.Active;

    /// <summary>
    /// Gets access to the ToolTip values for this control.
    /// </summary>
    [Category(@"ToolTip")]
    [Description(@"Control ToolTip Text")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ToolTipValues ToolTipValues { get; set; }

    private bool ShouldSerializeToolTipValues() => !ToolTipValues.IsDefault;

    /// <summary>
    /// Resets the <see cref="ToolTipValues"/> property to its default value.
    /// </summary>
    public void ResetToolTipValues() => ToolTipValues.Reset();

    /// <summary>
    /// Gets access to the <see cref="ToolTipManager"/> used for displaying tool tips.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ToolTipManager ToolTipManager => _toolTipManager;

    /// <summary>
    /// Reads a UTF-8 SVG file and assigns the text to <see cref="Svg"/>.
    /// </summary>
    /// <param name="path">Full path of the SVG file.</param>
    /// <exception cref="ArgumentNullException"><paramref name="path"/> is <see langword="null"/>.</exception>
    public void LoadFromFile(string path)
    {
        if (path == null)
        {
            throw new ArgumentNullException(nameof(path));
        }

        Svg = File.ReadAllText(path);
    }

    /// <summary>
    /// Reads UTF-8 SVG markup from a stream and assigns it to <see cref="Svg"/>.
    /// </summary>
    /// <param name="stream">Stream positioned at the start of the markup. The stream is left open.</param>
    /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <see langword="null"/>.</exception>
    public void Load(Stream stream)
    {
        if (stream == null)
        {
            throw new ArgumentNullException(nameof(stream));
        }

        using (var reader = new StreamReader(stream, Encoding.UTF8, true, 1024, true))
        {
            Svg = reader.ReadToEnd();
        }
    }

    /// <inheritdoc />
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Color BackColor
    {
        get => base.BackColor;
        set => base.BackColor = value;
    }

    /// <inheritdoc />
    protected override Size DefaultSize => new Size(120, 60);

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            OnCancelToolTip(this, EventArgs.Empty);
            DisposeRaster();
            DisposeDocument();
        }

        base.Dispose(disposing);
    }

    /// <inheritdoc />
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        EnsureRaster();
        if (_raster == null || _rasterLogicalSize.Width <= 0 || _rasterLogicalSize.Height <= 0)
        {
            return;
        }

        var origin = Point.Empty;
        if (_sizeMode == PictureBoxSizeMode.CenterImage || _sizeMode == PictureBoxSizeMode.Zoom)
        {
            origin = new Point(
                (ClientSize.Width - _rasterLogicalSize.Width) / 2,
                (ClientSize.Height - _rasterLogicalSize.Height) / 2);
        }

        var previous = e.Graphics.InterpolationMode;
        e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        e.Graphics.DrawImage(_raster, new Rectangle(origin, _rasterLogicalSize));
        e.Graphics.InterpolationMode = previous;
    }

    /// <inheritdoc />
    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        if (_sizeMode == PictureBoxSizeMode.StretchImage || _sizeMode == PictureBoxSizeMode.Zoom)
        {
            Invalidate();
        }
    }

    /// <inheritdoc />
    protected override void OnDpiChangedAfterParent(EventArgs e)
    {
        base.OnDpiChangedAfterParent(e);
        ApplyAutoSize();
        Invalidate();
    }

    /// <inheritdoc />
    protected override void OnMouseEnter(EventArgs e)
    {
        _toolTipManager.MouseEnter(_toolTipTarget, this);
        base.OnMouseEnter(e);
    }

    /// <inheritdoc />
    protected override void OnMouseMove(MouseEventArgs e)
    {
        _toolTipManager.MouseMove(_toolTipTarget, this, e.Location);
        base.OnMouseMove(e);
    }

    /// <inheritdoc />
    protected override void OnMouseDown(MouseEventArgs e)
    {
        _toolTipManager.MouseDown(_toolTipTarget, this, e.Location, e.Button);
        base.OnMouseDown(e);
    }

    /// <inheritdoc />
    protected override void OnMouseUp(MouseEventArgs e)
    {
        _toolTipManager.MouseUp(_toolTipTarget, this, e.Location, e.Button);
        base.OnMouseUp(e);
    }

    /// <inheritdoc />
    protected override void OnMouseLeave(EventArgs e)
    {
        _toolTipManager.MouseLeave(null, this, null);
        base.OnMouseLeave(e);
    }

    private void ReloadDocument()
    {
        DisposeDocument();
        DisposeRaster();
        SvgError = null;
        _documentGeneration++;

        if (_svg.Trim().Length == 0)
        {
            return;
        }

        try
        {
            _document = KryptonSvgDocuments.TryOpen(_svg, out var error);
            SvgError = error;
        }
        catch (Exception ex)
        {
            DisposeDocument();
            SvgError = ex.Message;
        }
    }

    private void ApplyAutoSize()
    {
        if (_sizeMode != PictureBoxSizeMode.AutoSize || _document == null)
        {
            return;
        }

        var size = GetLogicalIntrinsicSize();
        if (!size.IsEmpty && ClientSize != size)
        {
            ClientSize = size;
        }
    }

    private void EnsureRaster()
    {
        if (_document == null)
        {
            DisposeRaster();
            return;
        }

        var logical = GetLogicalDrawSize();
        if (logical.Width <= 0 || logical.Height <= 0)
        {
            DisposeRaster();
            return;
        }

        var scale = Math.Max(DeviceDpi, 96) / 96f;
        var pixelWidth = ClampEdge((int)Math.Ceiling(logical.Width * scale));
        var pixelHeight = ClampEdge((int)Math.Ceiling(logical.Height * scale));
        if (_raster != null
            && _raster.Width == pixelWidth
            && _raster.Height == pixelHeight
            && _rasterLogicalSize == logical
            && _rasterDpi == DeviceDpi
            && _rasterSizeMode == _sizeMode
            && _rasterGeneration == _documentGeneration)
        {
            return;
        }

        DisposeRaster();
        try
        {
            _raster = _document.Rasterize(pixelWidth, pixelHeight);
            _rasterLogicalSize = logical;
            _rasterDpi = DeviceDpi;
            _rasterSizeMode = _sizeMode;
            _rasterGeneration = _documentGeneration;
            SvgError = null;
        }
        catch (Exception ex)
        {
            DisposeRaster();
            SvgError = ex.Message;
        }
    }

    private Size GetLogicalDrawSize()
    {
        var intrinsic = GetLogicalIntrinsicSize();
        if (intrinsic.Width <= 0 || intrinsic.Height <= 0)
        {
            return Size.Empty;
        }

        switch (_sizeMode)
        {
            case PictureBoxSizeMode.StretchImage:
                return ClientSize.Width <= 0 || ClientSize.Height <= 0
                    ? Size.Empty
                    : ClientSize;
            case PictureBoxSizeMode.Zoom:
                if (ClientSize.Width <= 0 || ClientSize.Height <= 0)
                {
                    return Size.Empty;
                }

                var fit = Math.Min(ClientSize.Width / (float)intrinsic.Width, ClientSize.Height / (float)intrinsic.Height);
                return new Size(
                    Math.Max(1, (int)Math.Floor(intrinsic.Width * fit)),
                    Math.Max(1, (int)Math.Floor(intrinsic.Height * fit)));
            default:
                return intrinsic;
        }
    }

    private Size GetLogicalIntrinsicSize()
    {
        if (_document == null)
        {
            return Size.Empty;
        }

        var size = _document.IntrinsicDipSize;
        if (size.Width <= 0f || size.Height <= 0f || float.IsNaN(size.Width) || float.IsNaN(size.Height))
        {
            return Size.Empty;
        }

        return new Size(
            Math.Max(1, (int)Math.Ceiling(size.Width)),
            Math.Max(1, (int)Math.Ceiling(size.Height)));
    }

    private static int ClampEdge(int pixels) => Math.Max(1, Math.Min(pixels, MaximumRasterEdge));

    private void DisposeRaster()
    {
        _raster?.Dispose();
        _raster = null;
        _rasterGeneration = -1;
    }

    private void DisposeDocument()
    {
        _document?.Dispose();
        _document = null;
    }

    private float GetDpiFactor() => DeviceDpi / 96F;

    private void OnShowToolTip(object? sender, ToolTipEventArgs e)
    {
        if (!IsDisposed
            && ToolTipValues.EnableToolTips
            && !DesignMode)
        {
            if (FindForm() is { ContainsFocus: false })
            {
                return;
            }

            _visualPopupToolTip?.Dispose();

            var palette = KryptonManager.CurrentGlobalPalette;
            if (palette != null)
            {
                var redirector = new PaletteRedirect(palette);
                var renderer = palette.GetRenderer();

                _visualPopupToolTip = new VisualPopupToolTip(redirector,
                    ToolTipValues,
                    renderer,
                    PaletteBackStyle.ControlToolTip,
                    PaletteBorderStyle.ControlToolTip,
                    CommonHelper.ContentStyleFromLabelStyle(ToolTipValues.ToolTipStyle),
                    ToolTipValues.ToolTipShadow);

                _visualPopupToolTip.Disposed += OnVisualPopupToolTipDisposed;
                _visualPopupToolTip.ShowRelativeTo(e.Target, e.ControlMousePosition);
            }
        }
    }

    private void OnCancelToolTip(object? sender, EventArgs e) => _visualPopupToolTip?.Dispose();

    private void OnVisualPopupToolTipDisposed(object? sender, EventArgs e)
    {
        if (sender is VisualPopupToolTip popup)
        {
            popup.Disposed -= OnVisualPopupToolTipDisposed;
        }

        _visualPopupToolTip = null;
    }
}

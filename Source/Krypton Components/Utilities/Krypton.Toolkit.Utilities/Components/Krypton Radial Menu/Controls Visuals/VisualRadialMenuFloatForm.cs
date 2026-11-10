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
/// Borderless top-level host used when a <see cref="KryptonRadialMenuControl"/> is dragged outside its parent.
/// </summary>
/// <remarks>
/// Uses <c>UpdateLayeredWindow</c> per-pixel alpha so the disc can anti-alias against true transparency.
/// Colour-key / Region hosts cannot blend smoothly and child fills defeat form <see cref="Form.TransparencyKey"/>.
/// The radial control is kept as a hidden child for capture/input; this form forwards mouse/keys and publishes frames.
/// </remarks>
internal sealed class VisualRadialMenuFloatForm : Form
{
    private KryptonRadialMenuControl? _surface;
    private bool _publishPending;

    /// <summary>
    /// Initialize a new instance of the <see cref="VisualRadialMenuFloatForm"/> class.
    /// </summary>
    public VisualRadialMenuFloatForm()
    {
        FormBorderStyle = FormBorderStyle.None;
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.Manual;
        MinimizeBox = false;
        MaximizeBox = false;
        ControlBox = false;
        AutoScaleMode = AutoScaleMode.None;
        KeyPreview = true;
        Text = @"Radial Menu";
        // No TransparencyKey / Region — the layered bitmap carries alpha.
        BackColor = Color.Black;
    }

    /// <inheritdoc />
    protected override bool ShowWithoutActivation => false;

    /// <summary>
    /// Binds the radial surface that paints into the layered frame and receives forwarded input.
    /// </summary>
    /// <param name="surface">Hosted radial control.</param>
    public void BindSurface(KryptonRadialMenuControl surface) => _surface = surface;

    /// <summary>
    /// Rebuilds and publishes the per-pixel alpha frame.
    /// </summary>
    public void PublishFrame()
    {
        _publishPending = false;
        if (!IsHandleCreated || IsDisposed || _surface == null)
        {
            return;
        }

        var width = Width;
        var height = Height;
        if (width <= 0 || height <= 0)
        {
            return;
        }

        using var bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
        using (var g = Graphics.FromImage(bitmap))
        {
            g.Clear(Color.Transparent);
            g.CompositingMode = CompositingMode.SourceOver;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            _surface.PaintFloatingSurface(g, new Rectangle(0, 0, width, height));
        }

        PublishBitmap(bitmap, Location);
    }

    /// <summary>
    /// Queues a frame publish on the next idle tick (coalesces rapid invalidates).
    /// </summary>
    public void RequestPublishFrame()
    {
        if (_publishPending || !IsHandleCreated || IsDisposed)
        {
            return;
        }

        _publishPending = true;
        BeginInvoke(new Action(() =>
        {
            if (_publishPending)
            {
                PublishFrame();
            }
        }));
    }

    /// <inheritdoc />
    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        if (IsHandleCreated && Visible)
        {
            RequestPublishFrame();
        }
    }

    /// <inheritdoc />
    protected override void OnMouseDown(MouseEventArgs e)
    {
        _surface?.ForwardFloatMouseDown(e);
        base.OnMouseDown(e);
    }

    /// <inheritdoc />
    protected override void OnMouseMove(MouseEventArgs e)
    {
        _surface?.ForwardFloatMouseMove(e);
        base.OnMouseMove(e);
    }

    /// <inheritdoc />
    protected override void OnMouseUp(MouseEventArgs e)
    {
        _surface?.ForwardFloatMouseUp(e);
        base.OnMouseUp(e);
    }

    /// <inheritdoc />
    protected override void OnMouseLeave(EventArgs e)
    {
        _surface?.ForwardFloatMouseLeave(e);
        base.OnMouseLeave(e);
    }

    /// <inheritdoc />
    protected override void OnMouseWheel(MouseEventArgs e)
    {
        _surface?.ForwardFloatMouseWheel(e);
        base.OnMouseWheel(e);
    }

    /// <inheritdoc />
    protected override void OnKeyDown(KeyEventArgs e)
    {
        _surface?.ForwardFloatKeyDown(e);
        base.OnKeyDown(e);
    }

    /// <inheritdoc />
    protected override void OnKeyPress(KeyPressEventArgs e)
    {
        _surface?.ForwardFloatKeyPress(e);
        base.OnKeyPress(e);
    }

    /// <inheritdoc />
    protected override CreateParams CreateParams
    {
        get
        {
            var cp = base.CreateParams;
            cp.ExStyle |= unchecked((int)(PI.WS_EX_.TOOLWINDOW | PI.WS_EX_.LAYERED));
            return cp;
        }
    }

    private void PublishBitmap(Bitmap bitmap, Point screenLocation)
    {
        var screenDc = PI.GetDC(IntPtr.Zero);
        var memDc = PI.CreateCompatibleDC(screenDc);
        var hBitmap = IntPtr.Zero;
        var oldBitmap = IntPtr.Zero;
        try
        {
            // Colour.FromArgb(0) premultiplies for UpdateLayeredWindow.
            hBitmap = bitmap.GetHbitmap(Color.FromArgb(0));
            oldBitmap = PI.SelectObject(memDc, hBitmap);

            var size = new PI.SIZE(bitmap.Width, bitmap.Height);
            var pointSource = new PI.POINT(0, 0);
            var topPos = new PI.POINT(screenLocation.X, screenLocation.Y);
            var blend = new PI.BLENDFUNCTION
            {
                BlendOp = PI.AC_SRC_OVER,
                BlendFlags = 0,
                SourceConstantAlpha = 255,
                AlphaFormat = PI.AC_SRC_ALPHA
            };

            PI.UpdateLayeredWindow(
                Handle,
                screenDc,
                ref topPos,
                ref size,
                memDc,
                ref pointSource,
                0,
                ref blend,
                PI.ULW_ALPHA);
        }
        finally
        {
            if (hBitmap != IntPtr.Zero)
            {
                PI.SelectObject(memDc, oldBitmap);
                PI.DeleteObject(hBitmap);
            }

            PI.DeleteDC(memDc);
            PI.ReleaseDC(IntPtr.Zero, screenDc);
        }
    }
}

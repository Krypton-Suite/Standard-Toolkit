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

[DebuggerDisplay("({_visualOrientation} {_optimisedVisible})")]
internal class VisualShadowBase : NativeWindow, IDisposable
{
    #region Instance Fields
    private readonly ShadowValues _shadowValues;
    private readonly VisualOrientation _visualOrientation;
    private bool _optimisedVisible;
    private Bitmap _shadowClip;
    #endregion

    #region Identity

    /// <summary>
    /// Create a shadow thingy
    /// </summary>
    /// <param name="shadowValues">What value will be used</param>
    /// <param name="visualOrientation">What orientation for the shadow placement</param>
    public VisualShadowBase(ShadowValues shadowValues, VisualOrientation visualOrientation)
    {
        _shadowValues = shadowValues;
        _visualOrientation = visualOrientation;
        // Update form properties so we do not have a border and do not show
        // in the task bar. We draw the background in Magenta and set that as
        // the transparency key so it is a see through window.
        var cp = new CreateParams
        {
            // Define the screen position/size
            X = -2,
            Y = -2,
            Height = 2,
            Width = 2,

            Parent = IntPtr.Zero,//_ownerHandle,
            Style = unchecked((int)(PI.WS_.DISABLED | PI.WS_.POPUP)),
            ExStyle = unchecked((int)(PI.WS_EX_.LAYERED | PI.WS_EX_.NOACTIVATE | PI.WS_EX_.TRANSPARENT | PI.WS_EX_.NOPARENTNOTIFY))
        };

        _shadowClip = new Bitmap(1, 1);
        // Make the default transparent color transparent for myBitmap.
        _shadowClip.MakeTransparent();

        // Create the actual window
        CreateHandle(cp);
    }

    /// <summary>
    /// Make sure the resources are disposed of gracefully.
    /// </summary>
    public void Dispose()
    {
        DestroyHandle();
        _shadowClip.Dispose();
    }
    #endregion

    #region Public
    /// <summary>
    /// Show the window without activating it (i.e. do not take focus)
    /// </summary>
    public bool Visible
    {
        get => _optimisedVisible;
        set
        {
            if (_optimisedVisible != value)
            {
                _optimisedVisible = value;
                if (!value)
                {
                    PI.ShowWindow(Handle, PI.ShowWindowCommands.SW_HIDE);
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public Rectangle TargetRect { get; private set; }

    #endregion


    #region Implementation
    /// <summary>
    /// Calculate the new position, but DO NOT Move
    /// </summary>
    /// <param name="clientLocation">screen location of parent</param>
    /// <param name="windowBounds"></param>
    /// <returns>true, if the position has changed</returns>
    /// <remarks>
    /// Move operations have to be done as a single operation to reduce flickering
    /// </remarks>
    public bool CalcPositionShadowForm(Point clientLocation, Rectangle windowBounds)
    {
        Rectangle rect = CalcRectangle(windowBounds);
        rect.Offset(clientLocation);
        rect.Offset(_shadowValues.Offset);
        if (TargetRect != rect)
        {
            TargetRect = rect;
            return true;
        }

        return false;
    }


    /// <summary>
    /// Also invalidates to perform a redraw
    /// </summary>
    /// <param name="sourceBitmap">This will be a single bitmap that would represent all the shadows</param>
    /// <param name="windowBounds"></param>
    public void ReCalcShadow(Bitmap sourceBitmap, Rectangle windowBounds)
    {
        Rectangle clipRect = CalcRectangle(windowBounds);
        if (clipRect is { Width: > 0, Height: > 0 })
        {
            _shadowClip = sourceBitmap.Clone(clipRect, sourceBitmap.PixelFormat);
        }
        else
        {
            _shadowClip = new Bitmap(1, 1);
            _shadowClip.MakeTransparent();
        }

        UpdateShadowLayer();
    }

    internal void UpdateShadowLayer()
    {
        // The Following is also in
        // $:\Krypton-NET-4.7\Source\Krypton Components\Krypton.Navigator\Dragging\DropDockingIndicatorsRounded.cs
        // Does this bitmap contain an alpha channel?
        if (_shadowClip.PixelFormat != PixelFormat.Format32bppArgb)
        {
            throw new ApplicationException("The bitmap must be 32bpp with alpha-channel.");
        }

        // Must have a visible rectangle to render
        if (TargetRect.Width <= 0 || TargetRect.Height <= 0)
        {
            return;
        }

        // Get device contexts
        var screenDc = PI.GetDC(IntPtr.Zero);
        var memDc = PI.CreateCompatibleDC(screenDc);
        var hBitmap = IntPtr.Zero;
        var hOldBitmap = IntPtr.Zero;

        try
        {
            // Get handle to the new bitmap and select it into the current 
            // device context.
            hBitmap = _shadowClip.GetHbitmap(Color.FromArgb(0));
            hOldBitmap = PI.SelectObject(memDc, hBitmap);

            // Set parameters for layered window update.
            var newSize = new PI.SIZE(_shadowClip.Width, _shadowClip.Height);
            var sourceLocation = new PI.POINT(0, 0);
            var newLocation = new PI.POINT(TargetRect.Left, TargetRect.Top);
            var blend = new PI.BLENDFUNCTION
            {
                BlendOp = PI.AC_SRC_OVER,
                BlendFlags = 0,
                SourceConstantAlpha = (byte)(255 * _shadowValues.Opacity / 100.0),
                AlphaFormat = PI.AC_SRC_ALPHA
            };

            // Update the window.
            PI.UpdateLayeredWindow(
                Handle, // Handle to the layered window
                screenDc, // Handle to the screen DC
                ref newLocation, // New screen position of the layered window
                ref newSize, // New size of the layered window
                memDc, // Handle to the layered window surface DC
                ref sourceLocation, // Location of the layer in the DC
                0, // Color key of the layered window
                ref blend, // Transparency of the layered window
                PI.ULW_ALPHA // Use blend as the blend function
            );
        }
        finally
        {
            // Release device context.
            PI.ReleaseDC(IntPtr.Zero, screenDc);
            if (hBitmap != IntPtr.Zero)
            {
                PI.SelectObject(memDc, hOldBitmap);
                PI.DeleteObject(hBitmap);
            }

            PI.DeleteDC(memDc);
        }
    }

    /// <summary>
    /// Q: Why go to this trouble and not just have a "Huge bitmap"
    /// A: Memory for a 4K screen can eat a lot for a 32bit per pixel shader !
    /// </summary>
    /// <param name="windowBounds"></param>
    /// <returns></returns>
    private Rectangle CalcRectangle(Rectangle windowBounds)
    {
        int extraWidth = _shadowValues.ExtraWidth;
        var w = windowBounds.Width + extraWidth * 2;
        var h = windowBounds.Height + extraWidth * 2;

        var top = 0;
        var left = 0;
        var bottom = 0;
        var right = 0;

        switch (_visualOrientation)
        {
            case VisualOrientation.Top:
                top = 0;
                left = 0;
                bottom = Math.Abs(_shadowValues.Offset.Y) + extraWidth;
                right = w;
                break;
            case VisualOrientation.Left:
                top = Math.Abs(_shadowValues.Offset.Y) + extraWidth;
                left = 0;
                bottom = h;
                right = Math.Abs(_shadowValues.Offset.X) + extraWidth;
                break;
            case VisualOrientation.Bottom:
                top = windowBounds.Height - (Math.Abs(_shadowValues.Offset.Y) + extraWidth);
                left = Math.Abs(_shadowValues.Offset.X) + extraWidth;
                bottom = h;
                right = windowBounds.Width - (Math.Abs(_shadowValues.Offset.X) + extraWidth);
                break;
            case VisualOrientation.Right:
                top = Math.Abs(_shadowValues.Offset.Y) + extraWidth;
                left = windowBounds.Width - (Math.Abs(_shadowValues.Offset.X) + extraWidth);
                bottom = h;
                right = w;
                break;
        }

        return Rectangle.FromLTRB(left, top, right, bottom);
    }
    #endregion
}
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

/// <summary>Creates the blur effect for windows.</summary>
/// <seealso cref="NativeWindow" />
/// <seealso cref="IDisposable" />
public class VisualBlur : NativeWindow, IDisposable
{
    #region Instance Fields
    private readonly BlurValues _blurValues;
    private Bitmap _blurredForm;
    private bool _optimisedVisible;

    #endregion

    #region Identity

    /// <summary>
    /// Create a shadow thingy
    /// </summary>
    /// <param name="values"></param>
    public VisualBlur(BlurValues values)
    {
        _blurValues = values;
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

        _blurredForm = new Bitmap(1, 1);

        // Create the actual window
        CreateHandle(cp);
        _optimisedVisible = true;
    }

    /// <summary>
    /// Make sure the resources are disposed of gracefully.
    /// </summary>
    public void Dispose()
    {
        DestroyHandle();
        GC.SuppressFinalize(this);
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
    public void SetTargetRect(Point clientLocation, Rectangle windowBounds)
    {
        Rectangle rect = GetTargetRectangle(clientLocation, windowBounds);
        TargetRect = rect;
    }

    internal static Rectangle GetTargetRectangle(Point clientLocation, Rectangle windowBounds)
    {
        var rect = windowBounds with { X = 0, Y = 0 };
        rect.Offset(clientLocation);
        return rect;
    }

    internal void UpdateShadowLayer()
    {
        // The Following is also in
        // $:\Krypton-NET-4.7\Source\Krypton Components\Krypton.Navigator\Dragging\DropDockingIndicatorsRounded.cs
        // Does this bitmap contain an alpha channel?
        if (_blurredForm.PixelFormat != PixelFormat.Format32bppArgb)
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
            hBitmap = _blurredForm.GetHbitmap(Color.FromArgb(0));
            hOldBitmap = PI.SelectObject(memDc, hBitmap);

            // Set parameters for layered window update.
            var newSize = new PI.SIZE(_blurredForm.Width, _blurredForm.Height);
            var sourceLocation = new PI.POINT(0, 0);
            var newLocation = new PI.POINT(TargetRect.Left, TargetRect.Top);
            var blend = new PI.BLENDFUNCTION
            {
                BlendOp = PI.AC_SRC_OVER,
                BlendFlags = 0,
                SourceConstantAlpha = (byte)(255 * _blurValues.Opacity / 100.0),
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

    #endregion

    /// <summary>Updates the blur.</summary>
    /// <param name="bmp">The BMP.</param>
    public void UpdateBlur(Bitmap bmp)
    {
        _blurredForm = ApplyConvolutionFilter(bmp, GaussianBlur5X5, 0);
        UpdateShadowLayer();
    }


    // some filters fom https://icodebroker.tistory.com/9229
    #region filters
    /// <summary>
    /// MEAN 3X3
    /// </summary>
    public static double[,] Mean3X3
    {
        get
        {
            return new double[,]
            {
                { 1, 1, 1 },
                { 1, 1, 1 },
                { 1, 1, 1 }
            };
        }
    }
    /// <summary>
    /// MEAN 5X5
    /// </summary>
    public static double[,] Mean5X5
    {
        get
        {
            return new double[,]
            {
                { 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1 }
            };
        }
    }

    /// <summary>
    /// MEAN 9X9
    /// </summary>
    public static double[,] Mean9X9
    {
        get
        {
            return new double[,]
            {
                { 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1, 1, 1, 1 }
            };
        }
    }
    ///<summary>
    /// Gaussian blur kernel with the size 5x5
    ///</summary>
    public static double[,] KernelGaussianBlur5x5 = {
        {1,  4,  7,  4, 1},
        {4, 16, 26, 16, 4},
        {7, 26, 41, 26, 7},
        {4, 16, 26, 16, 4},
        {1,  4,  7,  4, 1}
    };

    /// <summary>
    /// GAUSSIAN BLUR 5X5
    /// </summary>
    public static double[,] GaussianBlur5X5
    {
        get
        {
            return new double[,]
            {
                { 2, 04, 05, 04, 2 },
                { 4, 09, 12, 09, 4 },
                { 5, 12, 15, 12, 5 },
                { 4, 09, 12, 09, 4 },
                { 2, 04, 05, 04, 2 }
            };
        }
    }

    ///<summary>
    /// Gaussian blur kernel with the size 3x3
    ///</summary>
    public static double[,] KernelGaussianBlur3x3 =
    {
        { 16, 26, 16 },
        { 26, 41, 26 },
        { 16, 26, 16 }
    };

    /// <summary>
    /// GAUSSIAN BLUR 3X3
    /// </summary>
    public static double[,] GaussianBlur3X3
    {
        get
        {
            return new double[,]
            {
                { 1, 2, 1 },
                { 2, 4, 2 },
                { 1, 2, 1 }
            };
        }
    }
    #endregion filters

    // Taken from // filters fom https://icodebroker.tistory.com/9229
    // Then a little optimisation
    private static Bitmap ApplyConvolutionFilter(Bitmap sourceBitmap, double[,] filterArray, int bias = 0)
    {
        BitmapData sourceBitmapData = sourceBitmap.LockBits
        (
            new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height),
            ImageLockMode.ReadOnly,
            PixelFormat.Format32bppArgb
        );
        var factor = 1.0 / filterArray.Cast<double>().Sum();
        var sourceByteArray = new byte[sourceBitmapData.Stride * sourceBitmapData.Height];
        var targetByteArray = new byte[sourceBitmapData.Stride * sourceBitmapData.Height];

        Marshal.Copy(sourceBitmapData.Scan0, sourceByteArray, 0, sourceByteArray.Length);

        sourceBitmap.UnlockBits(sourceBitmapData);

        var filterWidth = filterArray.GetLength(1);
        var filterOffset = (filterWidth - 1) / 2;

        //Parallel.For(filterOffset, sourceBitmap.Height - filterOffset, offsetY =>
        for (var offsetY = filterOffset; offsetY < sourceBitmap.Height - filterOffset; offsetY++)
        {
            Parallel.For(filterOffset, sourceBitmap.Width - filterOffset, offsetX =>
                    //for (var offsetX = filterOffset; offsetX < sourceBitmap.Width - filterOffset; offsetX++)
                {
                    double blue = 0;
                    double green = 0;
                    double red = 0;

                    var byteOffset = offsetY * sourceBitmapData.Stride + offsetX * 4;

                    for (var filterY = -filterOffset; filterY <= filterOffset; filterY++)
                    {
                        var yOffset = filterY + filterOffset;
                        for (var filterX = -filterOffset; filterX <= filterOffset; filterX++)
                        {
                            var temporaryOffset = byteOffset + (filterX * 4) + (filterY * sourceBitmapData.Stride);
                            var xOffset = filterX + filterOffset;

                            var filter = filterArray[yOffset, xOffset];

                            blue += sourceByteArray[temporaryOffset] * filter;

                            green += sourceByteArray[temporaryOffset + 1] * filter;

                            red += sourceByteArray[temporaryOffset + 2] * filter;
                        }
                    }

                    blue = factor * blue + bias;
                    green = factor * green + bias;
                    red = factor * red + bias;

                    blue = (blue > 255 ? 255 : (blue < 0 ? 0 : blue));
                    green = (green > 255 ? 255 : (green < 0 ? 0 : green));
                    red = (red > 255 ? 255 : (red < 0 ? 0 : red));

                    targetByteArray[byteOffset] = (byte)(blue);
                    targetByteArray[byteOffset + 1] = (byte)(green);
                    targetByteArray[byteOffset + 2] = (byte)(red);
                    targetByteArray[byteOffset + 3] = 255;
                }
            );
        }
        //);

        var targetBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);

        BitmapData targetBitmapData = targetBitmap.LockBits
        (
            new Rectangle(0, 0, targetBitmap.Width, targetBitmap.Height),
            ImageLockMode.WriteOnly,
            PixelFormat.Format32bppArgb
        );

        Marshal.Copy(targetByteArray, 0, targetBitmapData.Scan0, targetByteArray.Length);

        targetBitmap.UnlockBits(targetBitmapData);

        return targetBitmap;
    }

    #region Old Blurring Techniques
    // These have been kept for reference in case we need to revisit this again.
    //public bool GaussianBlur(Bitmap b)
    //{
    //    var m = new ConvMatrix();
    //    m.SetAll(1);
    //    m.Pixel = _blurValues.Radius;
    //    m.TopMid = m.MidLeft = m.MidRight = m.BottomMid = 2;
    //    m.Factor = _blurValues.Radius + 12;

    //    BitmapFilter.Conv3x3(b, m);
    //    BitmapFilter.Conv3x3(b, m);
    //    return BitmapFilter.Conv3x3(b, m);
    //}

    //public class ConvMatrix
    //{
    //    public int TopLeft, TopMid, TopRight;
    //    public int MidLeft, Pixel = 1, MidRight;
    //    public int BottomLeft, BottomMid, BottomRight;
    //    public int Factor = 1;
    //    public int Offset = 0;
    //    public void SetAll(int nVal)
    //    {
    //        TopLeft = TopMid = TopRight = MidLeft = Pixel = MidRight = BottomLeft = BottomMid = BottomRight = nVal;
    //    }
    //}

    //public class BitmapFilter
    //{
    //    internal static bool Conv3x3(Bitmap b, ConvMatrix m)
    //    {
    //        // Avoid divide by zero errors
    //        if (0 == m.Factor) return false;

    //        Bitmap bSrc = (Bitmap)b.Clone();

    //        // GDI+ still lies to us - the return format is BGR, NOT RGB.
    //        BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.WriteOnly,
    //            PixelFormat.Format24bppRgb);
    //        BitmapData bmSrc = bSrc.LockBits(new Rectangle(0, 0, bSrc.Width, bSrc.Height), ImageLockMode.ReadOnly,
    //            PixelFormat.Format24bppRgb);

    //        int stride = bmData.Stride;
    //        int stride2 = stride * 2;
    //        System.IntPtr Scan0 = bmData.Scan0;
    //        System.IntPtr SrcScan0 = bmSrc.Scan0;

    //        unsafe
    //        {
    //            byte* p = (byte*)(void*)Scan0;
    //            byte* pSrc = (byte*)(void*)SrcScan0;

    //            int nOffset = stride + 6 - b.Width * 3;
    //            int nWidth = b.Width - 2;
    //            int nHeight = b.Height - 2;

    //            int nPixel;

    //            for (int y = 0; y < nHeight; ++y)
    //            {
    //                for (int x = 0; x < nWidth; ++x)
    //                {
    //                    nPixel = ((((pSrc[2] * m.TopLeft) + (pSrc[5] * m.TopMid) + (pSrc[8] * m.TopRight) +
    //                                (pSrc[2 + stride] * m.MidLeft) + (pSrc[5 + stride] * m.Pixel) +
    //                                (pSrc[8 + stride] * m.MidRight) +
    //                                (pSrc[2 + stride2] * m.BottomLeft) + (pSrc[5 + stride2] * m.BottomMid) +
    //                                (pSrc[8 + stride2] * m.BottomRight)) / m.Factor) + m.Offset);

    //                    if (nPixel < 0) nPixel = 0;
    //                    if (nPixel > 255) nPixel = 255;

    //                    p[5 + stride] = (byte)nPixel;

    //                    nPixel = ((((pSrc[1] * m.TopLeft) + (pSrc[4] * m.TopMid) + (pSrc[7] * m.TopRight) +
    //                                (pSrc[1 + stride] * m.MidLeft) + (pSrc[4 + stride] * m.Pixel) +
    //                                (pSrc[7 + stride] * m.MidRight) +
    //                                (pSrc[1 + stride2] * m.BottomLeft) + (pSrc[4 + stride2] * m.BottomMid) +
    //                                (pSrc[7 + stride2] * m.BottomRight)) / m.Factor) + m.Offset);

    //                    if (nPixel < 0) nPixel = 0;
    //                    if (nPixel > 255) nPixel = 255;

    //                    p[4 + stride] = (byte)nPixel;

    //                    nPixel = ((((pSrc[0] * m.TopLeft) + (pSrc[3] * m.TopMid) + (pSrc[6] * m.TopRight) +
    //                                (pSrc[0 + stride] * m.MidLeft) + (pSrc[3 + stride] * m.Pixel) +
    //                                (pSrc[6 + stride] * m.MidRight) +
    //                                (pSrc[0 + stride2] * m.BottomLeft) + (pSrc[3 + stride2] * m.BottomMid) +
    //                                (pSrc[6 + stride2] * m.BottomRight)) / m.Factor) + m.Offset);

    //                    if (nPixel < 0) nPixel = 0;
    //                    if (nPixel > 255) nPixel = 255;

    //                    p[3 + stride] = (byte)nPixel;

    //                    p += 3;
    //                    pSrc += 3;
    //                }

    //                p += nOffset;
    //                pSrc += nOffset;
    //            }
    //        }

    //        b.UnlockBits(bmData);
    //        bSrc.UnlockBits(bmSrc);

    //        return true;
    //    }
    //}
    ///// <summary>Gaussian blur.</summary>
    //private void GaussianBlur()
    //{
    //    PI.BlurParams blurParams = new()
    //    {
    //        Radius = _blurValues.Radius,
    //        ExpandEdges = false
    //    };
    //    // Create the GDI+ BlurEffect, using the Guid
    //    var result = PI.GdipCreateEffect(PI.BlurEffectGuid, out IntPtr blurEffect);
    //    if (result == 0)
    //    {
    //        IntPtr hBlurParams = IntPtr.Zero;
    //        try
    //        {
    //            // Allocate space in unmanaged memory
    //            hBlurParams = Marshal.AllocHGlobal(Marshal.SizeOf(blurParams));
    //            // Copy the structure to the unmanaged memory
    //            Marshal.StructureToPtr(blurParams, hBlurParams, false);
    //            // Set the blurParams to the effect
    //            PI.GdipSetEffectParameters(blurEffect, hBlurParams, (uint)Marshal.SizeOf(blurParams));
    //            PI.RECT rect = new()
    //            {
    //                top = 0,
    //                left = 0,
    //                right = _blurredForm.Width,
    //                bottom = _blurredForm.Height
    //            };
    //            // Somewhere it said we can use destinationBitmap.GetHbitmap(), this doesn't work!!
    //            // Get the private nativeImage property from the Bitmap
    //            IntPtr nativeImage = PI.GetNativeImage(_blurredForm);
    //            PI.GdipBitmapApplyEffect(nativeImage, blurEffect, ref rect, false, IntPtr.Zero, 0);
    //        }
    //        finally
    //        {
    //            try
    //            {
    //                if (blurEffect != IntPtr.Zero)
    //                {
    //                    PI.GdipDeleteEffect(blurEffect);
    //                }

    //                if (hBlurParams != IntPtr.Zero)
    //                {
    //                    Marshal.FreeHGlobal(hBlurParams);
    //                }
    //            }
    //            catch
    //            {
    //                // empty - Should log something !
    //            }
    //        }
    //    }
    //    else
    //    {
    //        // Log some sort of message ?
    //    }
    //}

    #endregion Old Blurring Techniques
}
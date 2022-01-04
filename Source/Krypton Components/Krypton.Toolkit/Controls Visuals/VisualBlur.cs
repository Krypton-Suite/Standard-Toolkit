#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2022. All rights reserved. 
 *  
 */
#endregion


namespace Krypton.Toolkit
{
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
            CreateParams cp = new()
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
        }

        /// <summary>
        /// Make sure the resources are disposed of gracefully.
        /// </summary>
        public void Dispose()
        {
            DestroyHandle();
            _blurredForm.Dispose();
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
            Rectangle rect = new(0, 0, windowBounds.Width, windowBounds.Height);
            rect.Offset(clientLocation);
            TargetRect = rect;
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
            IntPtr screenDc = PI.GetDC(IntPtr.Zero);
            IntPtr memDc = PI.CreateCompatibleDC(screenDc);
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr hOldBitmap = IntPtr.Zero;

            try
            {
                // Get handle to the new bitmap and select it into the current 
                // device context.
                hBitmap = _blurredForm.GetHbitmap(Color.FromArgb(0));
                hOldBitmap = PI.SelectObject(memDc, hBitmap);

                // Set parameters for layered window update.
                PI.SIZE newSize = new(_blurredForm.Width, _blurredForm.Height);
                PI.POINT sourceLocation = new(0, 0);
                PI.POINT newLocation = new(TargetRect.Left, TargetRect.Top);
                PI.BLENDFUNCTION blend = new()
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
            _blurredForm = bmp;
            GaussianBlur();
            UpdateShadowLayer();
        }

        /// <summary>Gaussian blur.</summary>
        private void GaussianBlur()
        {
            PI.BlurParams blurParams = new()
            {
                Radius = _blurValues.Radius,
                ExpandEdges = false
            };
            var result = PI.GdipCreateEffect(PI.BlurEffectGuid, out IntPtr blurEffect);
            if (result == 0)
            {
                IntPtr handle = IntPtr.Zero;
                try
                {
                    handle = Marshal.AllocHGlobal(Marshal.SizeOf(blurParams));
                    Marshal.StructureToPtr(blurParams, handle, true);
                    PI.GdipSetEffectParameters(blurEffect, handle, (uint)Marshal.SizeOf(blurParams));
                    PI.RECT rect = new()
                    {
                        top = 0,
                        left = 0,
                        right = _blurredForm.Width,
                        bottom = _blurredForm.Height
                    };
                    PI.GdipBitmapApplyEffect(PI.GetNativeImage(_blurredForm), blurEffect, ref rect, false, IntPtr.Zero,
                        0);
                    // GdipBitmapCreateApplyEffect
                    PI.GdipDeleteEffect(blurEffect);
                }
                finally
                {
                    if (handle != IntPtr.Zero)
                    {
                        Marshal.FreeHGlobal(handle);
                    }
                }
            }
            else
            {
                // Log some sort of message ?
            }
        }

    }
}

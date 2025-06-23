#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2020 - 2025. All rights reserved. 
 *  
 */
#endregion

#if NET8_0_OR_GREATER
using MethodInvoker = System.Windows.Forms.MethodInvoker;
#endif 

namespace Krypton.Toolkit;

/// <summary>
/// Manages the drawing of Shadows
/// </summary>
internal class ShadowManager
{
    #region Instance Fields
    private readonly VisualForm _parentForm;
    private readonly ShadowValues _shadowValues;
    private bool _allowDrawing;
    private VisualShadowBase[]? _shadowForms;
    #endregion

    #region Identity
    public ShadowManager(VisualForm kryptonForm, ShadowValues shadowValues)
    {
        _parentForm = kryptonForm;
        _shadowValues = shadowValues;

#if NET10_0_OR_GREATER
            _parentForm.FormClosing += KryptonFormOnClosing;
#else
        _parentForm.Closing += KryptonFormOnClosing;
#endif

        _parentForm.Load += FormLoaded;

        shadowValues.EnableShadowsChanged += ShadowValues_EnableShadowsChanged;
        shadowValues.MarginsChanged += ShadowValues_MarginsChanged;
        shadowValues.BlurDistanceChanged += ShadowValues_BlurDistanceChanged;
        shadowValues.ColourChanged += ShadowValues_ColourChanged;
        shadowValues.OpacityChanged += ShadowValues_OpacityChanged;
    }

    internal void WndProc(ref Message m)
    {
        switch (m.Msg)
        {
            case PI.WM_.WINDOWPOSCHANGED:
            {
                PI.WINDOWPOS structure = (PI.WINDOWPOS)Marshal.PtrToStructure(m.LParam, typeof(PI.WINDOWPOS))!;
                var move = !structure.flags.HasFlag(PI.SWP_.NOSIZE | PI.SWP_.NOMOVE);
                PositionShadowForms(move);
                    
                if (!move)
                {
                    ReCalcBrushes();
                }
            }
                break;
        }
    }

    #endregion

    private void InitialiseShadowForms()
    {
        if (_shadowForms != null)
        {
            foreach (VisualShadowBase shadowForm in _shadowForms)
            {
                shadowForm.Visible = false;
                shadowForm.Dispose();
            }
        }
        _shadowForms = new VisualShadowBase[4];

        for (var i = 0; i < 4; i++)
        {
            _shadowForms[i] = new VisualShadowBase(_shadowValues, (VisualOrientation)i);
        }
    }

    private bool AllowDrawing =>
        _allowDrawing
        && _shadowValues.EnableShadows
        && _parentForm.Visible;

    private void KryptonFormOnClosing(object? sender, /*Cancel*/EventArgs e)
    {
        _allowDrawing = false;
        FlashWindowExListener.FlashEvent -= OnFlashWindowExListenerOnFlashEvent;

        if (_shadowForms != null)
        {
            foreach (VisualShadowBase shadowForm in _shadowForms)
            {
                shadowForm.Visible = false;
                shadowForm.Dispose();
            }
        }
    }

    private void FormLoaded(object? sender, EventArgs e)
    {
        _allowDrawing = (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
                        && (Process.GetCurrentProcess().ProcessName != @"devenv");
        if (_shadowForms == null)
        {
            InitialiseShadowForms();
            SetShadowFormsSizes();
            FlashWindowExListener.Register(_parentForm as Form);
            FlashWindowExListener.FlashEvent += OnFlashWindowExListenerOnFlashEvent;
        }
        else
        {
            PositionShadowForms(false);
        }
    }

    // Try and keep the shadows where  they are supposed to be when the form is flashing.
    private void OnFlashWindowExListenerOnFlashEvent(Form form, bool flashing)
    {
        if (!flashing)
        {
            _parentForm.BeginInvoke((MethodInvoker)(() => PositionShadowForms(false)));
        }
    }

    private void ShadowValues_ColourChanged(object? sender, ColorEventArgs e) => ReCalcBrushes();

    private void ShadowValues_BlurDistanceChanged(object? sender, EventArgs e) => ReCalcBrushes();

    private void ShadowValues_OpacityChanged(object? sender, EventArgs e) => ReCalcBrushes();

    private void ShadowValues_MarginsChanged(object? sender, EventArgs e) => SetShadowFormsSizes();

    private void ShadowValues_EnableShadowsChanged(object? sender, EventArgs e)
    {
        if (!_allowDrawing
            || _shadowForms == null)
        {
            // Call before shown is complete
            return;
        }

        foreach (VisualShadowBase shadowForm in _shadowForms)
        {
            shadowForm.Visible = AllowDrawing;
        }
    }


    private void SetShadowFormsSizes()
    {
        PositionShadowForms(false);
        ReCalcBrushes();
    }

    private void ReCalcBrushes()
    {
        if (!AllowDrawing
            || _shadowForms == null)
        {
            return;
        }

        // calculate the "whole" shadow
        Rectangle clientRectangle = CommonHelper.RealClientRectangle(_parentForm.Handle);
        using Bitmap allShadow = DrawShadowBitmap(clientRectangle);
        foreach (VisualShadowBase shadowForm in _shadowForms)
        {
            shadowForm.ReCalcShadow(allShadow, clientRectangle);
        }
    }

    /// <summary>
    /// Probably a more efficient way, but this gives the easiest testable visible results
    /// </summary>
    /// <param name="clientRectangle"></param>
    /// <returns></returns>
    private Bitmap DrawShadowBitmap(Rectangle clientRectangle)
    {
        int extraWidth = _shadowValues.ExtraWidth;
        var w = clientRectangle.Width + extraWidth * 2;
        var h = clientRectangle.Height + extraWidth * 2;

        var blur = (float)(_shadowValues.BlurDistance / 100.0 * extraWidth);
        var solidW = clientRectangle.Width + blur * 2;
        var solidH = clientRectangle.Height + blur * 2;
        var blurOffset = _shadowValues.ExtraWidth - blur;
        var bitmap = new Bitmap(w, h);
        bitmap.MakeTransparent();
        using Graphics g = Graphics.FromImage(bitmap);
        // fill background
        //g.FillRectangle(Brushes.Magenta, 0,0,w,h);
        // +1 to fill the gap
        g.FillRectangle(new SolidBrush(_shadowValues.Colour),
            blurOffset, blurOffset, solidW + 1, solidH + 1);

        // four dir gradient
        if (blurOffset > 0)
        {
            using (var brush = new LinearGradientBrush(new PointF(0, 0), new PointF(blurOffset, 0),
                       Color.Transparent, _shadowValues.Colour))
            {
                // Left
                g.FillRectangle(brush, 0, blurOffset, blurOffset, solidH);

                // Top
                brush.RotateTransform(90);
                g.FillRectangle(brush, blurOffset, 0, solidW, blurOffset);

                // Right
                // make sure pattern is correct
                brush.ResetTransform();
                brush.TranslateTransform(w % blurOffset, h % blurOffset);

                brush.RotateTransform(180);
                g.FillRectangle(brush, w - blurOffset, blurOffset, blurOffset, solidH);
                // Bottom
                brush.RotateTransform(90);
                g.FillRectangle(brush, blurOffset, h - blurOffset, solidW, blurOffset);
            }


            // four corner
            using (var gp = new GraphicsPath())
            using (var matrix = new Matrix())
            {
                gp.AddEllipse(0, 0, blurOffset * 2, blurOffset * 2);
                using (var pgb = new PathGradientBrush(gp)
                       {
                           CenterColor = _shadowValues.Colour,
                           SurroundColors = [Color.Transparent],
                           CenterPoint = new PointF(blurOffset, blurOffset)
                       })
                {
                    // left-Top
                    g.FillPie(pgb, 0, 0, blurOffset * 2, blurOffset * 2, 180, 90);

                    // right-Top
                    matrix.Translate(w - blurOffset * 2, 0);

                    pgb.Transform = matrix;
                    //pgb.Transform.Translate(w-blur*2, 0);
                    g.FillPie(pgb, w - blurOffset * 2, 0, blurOffset * 2, blurOffset * 2, 270, 90);

                    // right-Bottom
                    matrix.Translate(0, h - blurOffset * 2);
                    pgb.Transform = matrix;
                    g.FillPie(pgb, w - blurOffset * 2, h - blurOffset * 2, blurOffset * 2, blurOffset * 2, 0, 90);

                    // left-Bottom
                    matrix.Reset();
                    matrix.Translate(0, h - blurOffset * 2);
                    pgb.Transform = matrix;
                    g.FillPie(pgb, 0, h - blurOffset * 2, blurOffset * 2, blurOffset * 2, 90, 90);
                }
            }
        }

        //
        return bitmap;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Move operations have to be done as a single operation to reduce flickering
    /// </remarks>
    private void PositionShadowForms(bool move)
    {
        if (!_allowDrawing
            || _shadowForms == null)
        {
            // Probably called before shown is complete
            return;
        }

        void Mi()
        {
            var shadowFormVisible = AllowDrawing;
            foreach (VisualShadowBase shadowForm in _shadowForms)
            {
                shadowForm.Visible = shadowFormVisible;
            }
            if (!shadowFormVisible)
            {
                return;
            }

            Point desktopLocation = _parentForm.DesktopLocation;

            var hWinPosInfo = PI.BeginDeferWindowPos(_shadowForms.Length);

            foreach (VisualShadowBase shadowForm in _shadowForms)
            {
                shadowForm.CalcPositionShadowForm(desktopLocation,
                    CommonHelper.RealClientRectangle(_parentForm.Handle));
                Rectangle targetRect = shadowForm.TargetRect;
                hWinPosInfo = PI.DeferWindowPos(hWinPosInfo, shadowForm.Handle, /*PI.HWND_TOPMOST, //*/_parentForm.Handle,
                    targetRect.X, targetRect.Y, targetRect.Width, targetRect.Height,
                    (move ? PI.SWP_.NOSIZE : 0) |
                    PI.SWP_.NOACTIVATE
                    | PI.SWP_.NOREDRAW
                    | PI.SWP_.SHOWWINDOW
                    | PI.SWP_.NOCOPYBITS
                    | PI.SWP_.NOOWNERZORDER
                );
            }

            PI.EndDeferWindowPos(hWinPosInfo);
        }

        if (_parentForm.InvokeRequired)
        {
            _parentForm.Invoke((MethodInvoker)Mi);
        }
        else
        {
            Mi();
        }
    }
}

/// <summary>
/// https://stackoverflow.com/questions/25681443/how-to-detect-if-window-is-flashing
/// </summary>
internal static class FlashWindowExListener
{
    private static readonly Dictionary<IntPtr, Form> _forms = new Dictionary<IntPtr, Form>();
    private static readonly IntPtr _hHook;
    // Keep the HookProc delegate alive manually, such as using a class member as shown below,
    // otherwise the garbage collector will clean up the hook delegate eventually,
    // resulting in the code throwing a System.NullReferenceException.
    // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
    private static readonly PI.HookProc _hookProc;

    /// <summary>
    /// Make sure there is something to call first
    /// </summary>
    public static event FlashWindowExEventHandler FlashEvent = delegate { };

    /// <summary>
    /// the callback that is expected to be used
    /// </summary>
    /// <param name="f"></param>
    /// <param name="isFlashing"></param>
    public delegate void FlashWindowExEventHandler(Form f, bool isFlashing);

    static FlashWindowExListener()
    {
        var processId = PI.GetCurrentThreadId();
        // create an instance of the delegate that
        // won't be garbage collected to avoid:
        //   Managed Debugging Assistant 'CallbackOnCollectedDelegate' :** 
        //   'A callback was made on a garbage collected delegate of type 
        //   'WpfApp1!WpfApp1.MainWindow+NativeMethods+CBTProc::Invoke'. 
        //   This may cause application crashes, corruption and data loss. 
        //   When passing delegates to unmanaged code, they must be 
        //   kept alive by the managed application until it is guaranteed 
        //   that they will never be called.'
        _hookProc = ShellProc;

        // we are interested in listening to WH_SHELL events, mainly the HSHELL_REDRAW event.
        _hHook = PI.SetWindowsHookEx(PI.WH_.SHELL, _hookProc, IntPtr.Zero, processId);

        Application.ApplicationExit += delegate { PI.UnhookWindowsHookEx(_hHook); };
    }

    internal static void Register(Form f)
    {
        if (f.IsDisposed)
        {
            throw new ArgumentException("Cannot use disposed form.");
        }

        void OnHandleKnown()
        {
            // hold the handle here to unregister it without depending on the form
            var handle = f.Handle;
            _forms[handle] = f;

#if NET10_0_OR_GREATER
                f.FormClosing += delegate { Unregister(handle); };
#else
            f.Closing += delegate { Unregister(handle); };
#endif
        }

        if (f.Handle == IntPtr.Zero)
        {
            f.HandleCreated += delegate { OnHandleKnown(); };
        }
        else
        {
            OnHandleKnown();
        }
    }

    internal static void Unregister(IntPtr handle)
    {
        // We can't use 'null', since the type of the object is 'IntPtr'. So we need to use 'IntPtr.Zero'.
        if (handle != IntPtr.Zero)
        {
            _forms.Remove(handle);
        }
    }

    internal static void Unregister(Form f)
    {
        // This will crash if f has been disposed
        // We can't use 'null', since the type of the object is 'IntPtr'. So we need to use 'IntPtr.Zero'.
        if (f.Handle != IntPtr.Zero)
        {
            _forms.Remove(f.Handle);
        }
    }

    private static IntPtr ShellProc(int code, IntPtr wParam, IntPtr lParam)
    {
        if (code == PI.HSHELL_REDRAW)
        {
            try
            {
                if (_forms.TryGetValue(wParam, out var f))
                {
                    FlashEvent(f, (int)lParam == 1);
                }
            }
            catch
            {
                //
            }
        }

        return PI.CallNextHookEx(_hHook, code, wParam, lParam);
    }
}
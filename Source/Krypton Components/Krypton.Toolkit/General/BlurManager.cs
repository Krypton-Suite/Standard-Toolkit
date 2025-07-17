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
/// Manages the drawing of Shadows
/// </summary>
internal class BlurManager
{
    #region Instance Fields
    private readonly VisualForm _parentForm;
    private readonly BlurValues _blurValues;
    private VisualBlur? _visualBlur;
    private readonly System.Windows.Forms.Timer _detectIsActiveTimer;
    private Bitmap? _currentFormDisplay;
    private double? _parentBeforeOpacity;
    #endregion

    #region Identity

    public BlurManager(VisualForm kryptonForm, BlurValues blurValues)
    {
        _parentForm = kryptonForm;
        _blurValues = blurValues;

#if NET10_0_OR_GREATER
            _parentForm.FormClosing += KryptonFormOnClosing;
#else
        _parentForm.Closing += KryptonFormOnClosing;
#endif

        _detectIsActiveTimer = new System.Windows.Forms.Timer { Enabled = false, Interval = 200 };
        _detectIsActiveTimer.Tick += DetectIsTopMost;

        _blurValues.BlurWhenFocusLostChanged += BlurValues_EnableBlurChanged;
        _blurValues.OpacityChanged += BlurValuesOnOpacityChanged;
    }

    internal void SetBlurState(bool parentIsActive)
    {
        if (_parentForm.IsDisposed
            || _parentForm.Disposing)
        {
            return;
        }

        if (_blurValues.BlurWhenFocusLost
            && !parentIsActive
           )
        {
            DoTheBlur();
        }
        else
        {
            RemoveBlur();
        }
    }

    #endregion Identity

    private void KryptonFormOnClosing(object? sender, /*Cancel*/EventArgs e) => RemoveBlur();

    private void RemoveBlur()
    {
        if (_parentForm is { IsDisposed: false, Disposing: false }
            && _parentBeforeOpacity.HasValue
           )
        {
            _parentForm.Opacity = _parentBeforeOpacity.Value;
            _parentBeforeOpacity = null;
        }

        _detectIsActiveTimer.Enabled = false;
        if (_visualBlur != null)
        {
            _visualBlur.Visible = false;
            _visualBlur.Dispose();
            _visualBlur = null;
        }
    }

    public bool IsOverlapped()
    {
        if (_blurValues.BlurWhenFocusLost
            || !PI.IsWindowVisible(_parentForm.Handle)
           )
        {
            return false;
        }

        var hWnd = _parentForm.Handle;
        // The set is used to make calling GetWindow in a loop stable by checking if we have already
        //  visited the window returned by GetWindow. This avoids the possibility of an infinite loop.
        var visited = new HashSet<IntPtr> { hWnd };
        try
        {
            var activeForm = Form.ActiveForm;
            if (activeForm != null)
            {
                visited.Add(activeForm.Handle);
            }

            visited.Add(_visualBlur!.Handle);


            var thisRect = new PI.RECT();
            PI.GetWindowRect(hWnd, ref thisRect);

            while ((hWnd = PI.GetWindow(hWnd, PI.GetWindowType.GW_HWNDPREV)) != IntPtr.Zero
                   && !visited.Contains(hWnd))
            {
                visited.Add(hWnd);
                var testRect = new PI.RECT();
                if (PI.IsWindowVisible(hWnd)
                    && PI.GetWindowRect(hWnd, ref testRect)
                    && PI.IntersectRect(out _, ref thisRect, ref testRect)
                   )
                {
                    return true;
                }
            }

            return false;
        }
        finally
        {
            // Attempt to clear sooner to allow handles to be released.
            visited.Clear();
            visited = null;
        }
    }

    private void BlurValuesOnOpacityChanged(object? sender, EventArgs e)
    {
        if (_visualBlur != null)
        {
            _visualBlur.Visible = false;
            DoTheBlur();
        }
    }

    private void BlurValues_EnableBlurChanged(object? sender, EventArgs e)
    {
        if (!_blurValues.BlurWhenFocusLost)
        {
            RemoveBlur();
        }
    }

    private void DetectIsTopMost(object? sender, EventArgs e)
    {
        if ((_visualBlur != null)
            && IsOverlapped()
           )
        {
            RemoveBlur();
        }
    }

    private void DoTheBlur()
    {
        // If the blur is not enabled, or the parent form is disposed or disposing, then do not continue
        if (!_blurValues.BlurWhenFocusLost
            || _parentForm.IsDisposed
            || _parentForm.Disposing
            || DesignModeHelper.IsInDesignMode
           )
        {
            // Has blur been turned off ?
            return;
        }

        _visualBlur = new VisualBlur(_blurValues);
        Rectangle clientRectangle = CommonHelper.RealClientRectangle(_parentForm.Handle);
        _visualBlur.SetTargetRect(_parentForm.DesktopLocation, clientRectangle);

        Rectangle targetRect = _visualBlur.TargetRect;
        _visualBlur.UpdateBlur(_currentFormDisplay!);
        // As UpdateBlur can take a few moments, then it is possible for the app to be closed before getting to the next line
        if ((_visualBlur == null)
            || _parentForm.IsDisposed
            || _parentForm.Disposing
           )
        {
            return;
        }

        PI.SetWindowPos(_visualBlur.Handle, PI.HWND_TOP,//.HWND_TOPMOST,
            //PI.SetWindowPos(_visualBlur.Handle, _parentForm.Handle,
            targetRect.X, targetRect.Y, targetRect.Width, targetRect.Height,
            PI.SWP_.NOACTIVATE
            | PI.SWP_.NOREDRAW
            | PI.SWP_.SHOWWINDOW
            | PI.SWP_.NOCOPYBITS
            //| PI.SWP_.NOOWNERZORDER
        );
        // Set parent form opacity afterwards to prevent flicker
        _parentBeforeOpacity ??= _parentForm.Opacity;

        _parentForm.Opacity = _blurValues.Opacity / 100.0;
        _detectIsActiveTimer.Enabled = true;

    }

    internal void TakeSnapshot()
    {
        _currentFormDisplay?.Dispose();
        if (!_blurValues.BlurWhenFocusLost
            || _parentForm.IsDisposed
            || _parentForm.Disposing
           )
        {
            // Has blur been turned off ?
            _currentFormDisplay = null;
            return;
        }
        Rectangle clientRectangle = CommonHelper.RealClientRectangle(_parentForm.Handle);
        _currentFormDisplay = TakeSnapshot(VisualBlur.GetTargetRectangle(_parentForm.DesktopLocation, clientRectangle));
    }

    private static Bitmap TakeSnapshot(Rectangle targetRectangle)
    {
        var bmp = new Bitmap(targetRectangle.Width, targetRectangle.Height);
        Graphics g = Graphics.FromImage(bmp);
        g.CopyFromScreen(targetRectangle.Left, targetRectangle.Top, 0, 0, bmp.Size);
        return bmp;
    }

}
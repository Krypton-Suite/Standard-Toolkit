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
    /// <summary>
    /// Manages the drawing of Shadows
    /// </summary>
    internal class BlurManager
    {
        #region Instance Fields
        private readonly VisualForm _parentForm;
        private readonly BlurValues _blurValues;
        private VisualBlur _visualBlur;
        private readonly System.Windows.Forms.Timer _detectIsActiveTimer;
        #endregion

        #region Identity

        public BlurManager(VisualForm kryptonForm, BlurValues blurValues)
        {
            _parentForm = kryptonForm;
            _blurValues = blurValues;

            _parentForm.Closing += KryptonFormOnClosing;
            _detectIsActiveTimer = new System.Windows.Forms.Timer { Enabled = false, Interval = 200 };
            _detectIsActiveTimer.Tick += DetectIsTopMost;

            _blurValues.EnableBlurChanged += BlurValues_EnableBlurChanged;
            _blurValues.RadiusChanged += BlurValuesOnRadiusChanged;
            _blurValues.OpacityChanged += BlurValuesOnOpacityChanged;
        }

        internal void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case PI.WM_.ACTIVATE:
                    if ((PI.LOWORD(m.WParam) == 0) // WA_INACTIVE
                        && (PI.HIWORD(m.WParam) == 0)
                        )
                    {
                        if (_blurValues.BlurWhenFocusLost
                            || (Form.ActiveForm != null)
                            )
                        {
                            DoTheBlur();
                        }
                    }
                    else
                    {
                        RemoveBlur();
                    }
                    break;
                default:
                    break;
            }
        }

        #endregion Identity

        private void KryptonFormOnClosing(object sender, /*Cancel*/EventArgs e) => RemoveBlur();

        private void RemoveBlur()
        {
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

            IntPtr hWnd = _parentForm.Handle;
            // The set is used to make calling GetWindow in a loop stable by checking if we have already
            //  visited the window returned by GetWindow. This avoids the possibility of an infinite loop.
            var visited = new HashSet<IntPtr> { hWnd };
            Form activeForm = Form.ActiveForm;
            if (activeForm != null)
            {
                visited.Add(activeForm.Handle);
            }

            visited.Add(_visualBlur.Handle);


            PI.RECT thisRect = new();
            PI.GetWindowRect(hWnd, ref thisRect);

            while ((hWnd = PI.GetWindow(hWnd, PI.GetWindowType.GW_HWNDPREV)) != IntPtr.Zero
                   && !visited.Contains(hWnd))
            {
                visited.Add(hWnd);
                PI.RECT testRect = new();
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

        private void BlurValuesOnOpacityChanged(object sender, EventArgs e)
        {
            if (_visualBlur != null)
            {
                _visualBlur.Visible = false;
                DoTheBlur();
            }
        }

        private void BlurValuesOnRadiusChanged(object sender, EventArgs e)
        {
            if (_visualBlur != null)
            {
                _visualBlur.Visible = false;
                DoTheBlur();
            }
        }

        private void BlurValues_EnableBlurChanged(object sender, EventArgs e)
        {
            if (!_blurValues.EnableBlur)
            {
                RemoveBlur();
            }
        }

        private void DetectIsTopMost(object sender, EventArgs e)
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
            if (!_blurValues.EnableBlur)
            {
                // Has blur been turned off ?
                return;
            }
            _visualBlur = new VisualBlur(_blurValues);
            Rectangle clientRectangle = CommonHelper.RealClientRectangle(_parentForm.Handle);
            _visualBlur.SetTargetRect(_parentForm.DesktopLocation, clientRectangle);

            Rectangle targetRect = _visualBlur.TargetRect;
            Bitmap bmp = TakeSnapshot(targetRect);
            _visualBlur.UpdateBlur(bmp);
            PI.SetWindowPos(_visualBlur.Handle, PI.HWND_TOPMOST,
            //PI.SetWindowPos(_visualBlur.Handle, _parentForm.Handle,
                targetRect.X, targetRect.Y, targetRect.Width, targetRect.Height,
                PI.SWP_.NOACTIVATE
                | PI.SWP_.NOREDRAW
                | PI.SWP_.SHOWWINDOW
                | PI.SWP_.NOCOPYBITS
            //| PI.SWP_.NOOWNERZORDER
            );
            _detectIsActiveTimer.Enabled = true;

        }

        private static Bitmap TakeSnapshot(Rectangle targetRectangle)
        {
            Bitmap bmp = new(targetRectangle.Width, targetRectangle.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(targetRectangle.Left, targetRectangle.Top, 0, 0, bmp.Size);
            return bmp;
        }

    }
}

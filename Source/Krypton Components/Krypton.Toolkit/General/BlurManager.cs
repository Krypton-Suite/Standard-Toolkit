using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Krypton.Toolkit.General
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
        private Timer _detectIsActiveTimer;
        #endregion

        #region Identity

        public BlurManager(VisualForm kryptonForm, BlurValues blurValues)
        {
            _parentForm = kryptonForm;
            _blurValues = blurValues;

            _parentForm.Closing += KryptonFormOnClosing;
            _detectIsActiveTimer = new Timer { Enabled = false, Interval = 200 };
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
            }
        }

        #endregion Identity

        private void KryptonFormOnClosing(object sender, /*Cancel*/EventArgs e)
        {
            RemoveBlur();
        }

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
            HashSet<IntPtr> visited = new HashSet<IntPtr> { hWnd };
            Form activeForm = Form.ActiveForm;
            if (activeForm != null)
            {
                visited.Add(activeForm.Handle);
            }

            visited.Add(_visualBlur.Handle);


            PI.RECT thisRect = new PI.RECT();
            PI.GetWindowRect(hWnd, ref thisRect);

            while ((hWnd = PI.GetWindow(hWnd, PI.GetWindowType.GW_HWNDPREV)) != IntPtr.Zero
                   && !visited.Contains(hWnd))
            {
                visited.Add(hWnd);
                PI.RECT testRect = new PI.RECT();
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
            Bitmap bmp = new Bitmap(targetRectangle.Width, targetRectangle.Height);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmp);
            g.CopyFromScreen(targetRectangle.Left, targetRectangle.Top, 0, 0, bmp.Size);
            return bmp;
        }

    }
}

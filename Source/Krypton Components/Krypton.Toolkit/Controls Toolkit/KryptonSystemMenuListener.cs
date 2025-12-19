#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal class KryptonSystemMenuListener : NativeWindow
{
    #region Events
    public event Action<Point>? KeyAltSpaceDown;
    public event Action<Point>? NCRightMouseButtonDown;
    public event Action<Point>? NCLeftMouseButtonDown;
    #endregion

    #region Fields
    private readonly KryptonForm _form;
    private ViewDrawContent _drawContent;
    #endregion

    #region Identity
    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <param name="kryptonForm">The instance the KryptonSystemMenu is operating on.</param>
    /// <param name="drawContent">ViewDrawContent component for title bar image detection.</param>
    public KryptonSystemMenuListener(KryptonForm kryptonForm, ViewDrawContent drawContent)
    {
        _form = kryptonForm;
        _drawContent = drawContent;
    }
    #endregion

    #region Public
    /// <summary>
    /// Stop listening for mouse and keyboard events that trigger the system menu.
    /// </summary>
    public void DisableListener()
    {
        if (Handle != IntPtr.Zero)
        {
            ReleaseHandle();
        }
    }

    /// <summary>
    /// Enable listening for mouse and keyboard events that trigger the system menu.
    /// </summary>
    public void EnableListener()
    {
        if (Handle == IntPtr.Zero)
        {
            AssignHandle(_form.Handle);
        }
    }

    /// <inheritdoc/>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override void ReleaseHandle()
    {
        // Retain default behaviour, but hide this method from the editor

        base.ReleaseHandle();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public new void AssignHandle(IntPtr handle)
    {
        // Retain default behaviour, but hide this method from the editor

        base.AssignHandle(handle);
    }
    #endregion

    #region Protected (override)
    protected override void WndProc(ref Message m)
    {
        if (m.Msg == PI.WM_.NCRBUTTONDOWN)
        {
            Point screenPoint = GetScreenPointFromLParam(m.LParam);
            if (_form.IsInTitleBarArea(screenPoint))
            {
                OnNCRightMouseButtonDown(screenPoint);
                // Eat the message
                return;
            }
        }
        else if (m.Msg == PI.WM_.NCLBUTTONDOWN)
        {
            Point screenPoint = GetScreenPointFromLParam(m.LParam);
            if (_form.IsInTitleBarArea(screenPoint) && !_form.IsOnControlButtons(screenPoint))
            {
                // Discover if the form icon is being Displayed
                using var context = GetLayoutContext();

                if (_drawContent.IsImageDisplayed(context))
                {
                    // Convert to window coordinates
                    Point windowPoint = ScreenToWindow(screenPoint);

                    // Check if the mouse is over the Application icon image area
                    if (_drawContent.ImageRectangle(context).Contains(windowPoint))
                    {
                        OnNCLeftMouseButtonDown(screenPoint);
                        // Eat the message
                        return;
                    }
                }
            }
        }
        else if ((m.Msg & PI.WM_.SYSKEYDOWN) == PI.WM_.SYSKEYDOWN || (m.WParam.ToInt64() & PI.WM_.KEYDOWN) == PI.WM_.KEYDOWN)
        {
            if ((Control.ModifierKeys & Keys.Alt) == Keys.Alt)
            {
                if (m.WParam.ToInt64() == (int)Keys.Space)
                {
                    // Discover if the form icon is being Displayed
                    using var context = GetLayoutContext();

                    if (_drawContent.IsImageDisplayed(context))
                    {
                        // PointToScreen maps to the form's client area
                        var screenPoint = _form.PointToScreen(_drawContent.ImageRectangle(context).Location);
                        // Subtract the titlebar's height to move the position up to the image.
                        screenPoint.Y -= _drawContent.ClientHeight;

                        OnKeyAltSpaceDown(screenPoint);
                        // Eat the message
                        return;
                    }
                }
            }
        }

        base.WndProc(ref m);
    }
    #endregion

    #region Private
    private ViewLayoutContext GetLayoutContext()
    {
        return new ViewLayoutContext(_form, _form.Renderer);
    }

    private Point ScreenToWindow(Point screenPoint)
    {
        // First of all convert to client coordinates
        Point clientPoint = _form.PointToClient(screenPoint);

        // Now adjust to take into account the top and left borders
        clientPoint.Offset(_form.RealWindowBorders.Left, _form.RealWindowBorders.Top);

        return clientPoint;
    }

    private Point GetScreenPointFromLParam(IntPtr LParam)
    {
        return new Point(PI.GET_X_LPARAM(LParam), PI.GET_Y_LPARAM(LParam));
    }

    private void OnNCLeftMouseButtonDown(Point screenPoint)
    {
        NCLeftMouseButtonDown?.Invoke(screenPoint);
    }

    private void OnNCRightMouseButtonDown(Point screenPoint)
    {
        NCRightMouseButtonDown?.Invoke(screenPoint);
    }

    private void OnKeyAltSpaceDown(Point screenPoint)
    {
        KeyAltSpaceDown?.Invoke(screenPoint);
    }
    #endregion
}

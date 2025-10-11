#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal class KryptonSystemMenuListener : NativeWindow
{
    /*
     * What needs to be intercepted
     *      Right Mouse click in the title bar (the menu shows on the position of the mouse cursor)
     *      Left Mouse click on the ControlBox (the menu shows on the position of the ControlBox)
     *      ALT + Space (the menu shows on the position of the ControlBox)
     */

    #region Events
    public event Action<Point>? KeyAltSpaceDown;
    public event Action<Point>? NCRightMouseButtonDown;
    public event Action<Point>? NCLeftMouseButtonDown;
    #endregion

    #region Fields
    private readonly KryptonForm _form;
    private ViewDrawDocker _drawHeading;
    private ViewDrawContent _drawContent;
    #endregion

    #region Identity
    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <param name="kryptonForm">The instance the KryptonSystemMenu is operating on.</param>
    /// <param name="drawHeading">Heading component for title bar detection.</param>
    public KryptonSystemMenuListener(KryptonForm kryptonForm, ViewDrawDocker drawHeading, ViewDrawContent drawContent)
    {
        _form = kryptonForm;
        _drawHeading = drawHeading;
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
        if (_form.SystemMenuValues.ShowOnRightClick && m.Msg == PI.WM_.NCRBUTTONDOWN)
        {
            // Intercept Non Client Area Right Mouse Down
            // But it needs to know if the click happens on the title bar, otherwise
            // the message must be forwarded.
            
            //if (some where in the title bar)
            {
                // Get the screen coordinates from the message

                Point screenPoint = GetScreenPointFromLParam(m.LParam);
                if (_form.IsInTitleBarArea(screenPoint))
                {
                    OnNCRightMouseButtonDown(screenPoint);
                    // Eat the message
                    return;
                }


            }
        }
        else if (_form.SystemMenuValues.ShowOnIconClick && m.Msg == PI.WM_.NCLBUTTONDOWN)
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
        else if (_form.SystemMenuValues.ShowOnAltSpace
            && (m.Msg & PI.WM_.SYSKEYDOWN) == PI.WM_.SYSKEYDOWN || (m.WParam.ToInt32() & PI.WM_.KEYDOWN) == PI.WM_.KEYDOWN)
        {
            if ((Control.ModifierKeys & Keys.Alt) == Keys.Alt)
            {
                if (m.WParam.ToInt32() == (nint)Keys.Space)
                {
                    // Discover if the form icon is being Displayed
                    using var context = GetLayoutContext();

                    // Check if the mouse is over the Application icon image area
                    if (_drawContent.IsImageDisplayed(context))
                    {
                        OnKeyAltSpaceDown(_drawContent.ImageRectangle(context).Location);

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
        Debug.Print($"OnNCLeftMouseButtonDown");
        NCLeftMouseButtonDown?.Invoke(screenPoint);
    }

    private void OnNCRightMouseButtonDown(Point screenPoint)
    {
        Debug.Print($"OnNCRightMouseButtonDown");
        NCRightMouseButtonDown?.Invoke(screenPoint);
    }

    private void OnKeyAltSpaceDown(Point screenPoint)
    {
        Debug.Print($"OnKeyAltSpaceDown");
        KeyAltSpaceDown?.Invoke(screenPoint);
    }
    #endregion
}

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
/// Manage the filtering of message for popup controls.
/// </summary>
public class VisualPopupManager : IMessageFilter
{
    #region Type Declarations
    private class PopupStack : Stack<VisualPopup>;
    #endregion

    #region Static Fields
    [ThreadStatic]
    private static VisualPopupManager? _singleton;
    #endregion

    #region Instance Fields
    private readonly PopupStack _stack;
    private IntPtr _activeWindow;
    private bool _filtering;
    private int _suspended;
    private EventHandler? _cmsFinishDelegate;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the VisualPopupManager class.
    /// </summary>
    private VisualPopupManager() => _stack = new PopupStack();

    #endregion

    #region Singleton
    /// <summary>
    /// Gets access to the single instance of the VisualPopupManager class.
    /// </summary>
    public static VisualPopupManager Singleton
    {
        [DebuggerStepThrough]
        get => _singleton ??= new VisualPopupManager();
    }
    #endregion

    #region IsShowingCMS
    /// <summary>
    /// Gets a value indicating if currently showing a context menu strip.
    /// </summary>
    public bool IsShowingCMS { get; private set; }

    #endregion

    #region IsTracking
    /// <summary>
    /// Gets a value indicating if currently tracking a popup.
    /// </summary>
    public bool IsTracking => CurrentPopup != null;

    #endregion

    #region CurrentPopup
    /// <summary>
    /// Gets the current visual popup being tracked.
    /// </summary>
    public VisualPopup? CurrentPopup
    {
        [DebuggerStepThrough]
        get;
        private set;
    }

    #endregion

    #region StackedPopups
    /// <summary>
    /// Gets the stacked set of popups as an array.
    /// </summary>
    public VisualPopup[] StackedPopups => _stack.ToArray();

    #endregion

    #region TrackingByType
    /// <summary>
    /// Gets the popup that matches the provided type.
    /// </summary>
    /// <param name="t">Type to find.</param>
    /// <returns>Matching instance; otherwise null.</returns>
    public Control? TrackingByType(Type t)
    {
        if (IsTracking)
        {
            // Is the current popup matching the type?
            if (CurrentPopup?.GetType() == t)
            {
                return CurrentPopup;
            }

            // Check the stack items in reverse order for a match
            var popups = _stack.ToArray();
            for (var i = popups.Length - 1; i >= 0; i--)
            {
                if (!popups[i].IsDisposed)
                {
                    if (popups[i].GetType() == t)
                    {
                        return popups[i];
                    }
                }
            }
        }

        return null;
    }
    #endregion

    #region StartTracking
    /// <summary>
    /// Start tracking the provided popup.
    /// </summary>
    /// <param name="popup">Popup instance to track.</param>
    public void StartTracking([DisallowNull] VisualPopup popup)
    {
        Debug.Assert(popup is not null);
        Debug.Assert(!popup!.IsDisposed);
        Debug.Assert(popup.IsHandleCreated);
        Debug.Assert(_suspended == 0);

        // The popup control must be valid
        if (popup is { IsDisposed: false, IsHandleCreated: true })
        {
            // Cannot start tracking when a popup menu is alive
            if (_suspended == 0)
            {
                // If we already have a popup...
                if (CurrentPopup != null)
                {
                    // Then stack it
                    _stack.Push(CurrentPopup);
                }
                else
                {
                    // Cache the currently active window
                    _activeWindow = PI.GetActiveWindow();

                    // For the first popup, we hook into message processing
                    FilterMessages(true);
                }

                // Store reference
                CurrentPopup = popup;
            }
        }
    }
    #endregion

    #region EndAllTracking
    /// <summary>
    /// Finish tracking all popups.
    /// </summary>
    public void EndAllTracking()
    {
        // Are we tracking a popup?
        if (CurrentPopup != null)
        {
            // Kill the popup window
            if (!CurrentPopup.IsDisposed)
            {
                CurrentPopup.Dispose();
                CurrentPopup = null;
            }

            // Is there anything stacked?
            while (_stack.Count > 0)
            {
                // Pop back the popup
                CurrentPopup = _stack.Pop();

                // Kill the popup
                CurrentPopup.Dispose();
                CurrentPopup = null;
            }

            // No longer need to filter
            FilterMessages(false);
        }
    }
    #endregion

    #region EndPopupTracking
    /// <summary>
    /// Finish tracking from the current back to and including the provided popup.
    /// </summary>
    /// <param name="popup">Popup that needs to be killed.</param>
    public void EndPopupTracking(VisualPopup popup)
    {
        // Are we tracking a popup?
        if (CurrentPopup != null)
        {
            bool found;

            do
            {
                // Is this the target?
                found = CurrentPopup == popup;

                // If possible then kill the current popup
                if (!CurrentPopup.IsDisposed)
                {
                    CurrentPopup.Dispose();
                }

                CurrentPopup = null;

                // If anything on stack, then it becomes the current one
                if (_stack.Count > 0)
                {
                    CurrentPopup = _stack.Pop();
                }
            }
            while (!found && (CurrentPopup != null));

            // If we removed all the popups
            if (CurrentPopup == null)
            {
                // No longer need to filter
                FilterMessages(false);
            }
        }
    }
    #endregion

    #region EndCurrentTracking
    /// <summary>
    /// Finish tracking the current popup.
    /// </summary>
    public void EndCurrentTracking()
    {
        // Are we tracking a popup?
        if (CurrentPopup != null)
        {
            // Kill the popup window
            if (!CurrentPopup.IsDisposed)
            {
                CurrentPopup.Dispose();
            }

            // Is there anything stacked?
            if (_stack.Count > 0)
            {
                // Pop back and now track
                CurrentPopup = _stack.Pop();
            }
            else
            {
                // No longer tracking any popup
                CurrentPopup = null;

                // Last popup removed, so unhook from message processing
                FilterMessages(false);
            }
        }
    }
    #endregion

    #region ShowContextMenuStrip
    /// <summary>
    /// Show the provided context strip in a way compatible with any popups.
    /// </summary>
    /// <param name="cms">Reference to ContextMenuStrip.</param>
    /// <param name="screenPt">Screen position for showing the context menu strip.</param>
    public void ShowContextMenuStrip(ContextMenuStrip cms,
        Point screenPt) =>
        ShowContextMenuStrip(cms, screenPt, null);

    /// <summary>
    /// Show the provided context strip in a way compatible with any popups.
    /// </summary>
    /// <param name="cms">Reference to ContextMenuStrip.</param>
    /// <param name="screenPt">Screen position for showing the context menu strip.</param>
    /// <param name="cmsFinishDelegate">Delegate to call when strip dismissed.</param>
    public void ShowContextMenuStrip([DisallowNull] ContextMenuStrip cms,
        Point screenPt,
        EventHandler? cmsFinishDelegate)
    {
        Debug.Assert(cms != null);

        if (cms != null)
        {
            // Need to know when the context strip is removed
            cms.Closed += OnCMSClosed;

            // Remember delegate to fire when context menu is dismissed
            _cmsFinishDelegate = cmsFinishDelegate;

            // Suspend processing of messages until context strip removed
            _suspended++;

            // Need to filter to prevent non-client mouse move from occurring
            FilterMessages(true);

            // We are showing a context menu strip
            IsShowingCMS = true;

            // Remember the strip reference for use in message processing

            cms.Show(screenPt);
        }
    }
    #endregion

    #region PreFilterMessage
    /// <summary>
    /// Filters out a message before it is dispatched.
    /// </summary>
    /// <param name="m">The message to be dispatched. You cannot modify this message.</param>
    /// <returns>true to filter the message and stop it from being dispatched; false to allow the message to continue to the next filter or control.</returns>
    public bool PreFilterMessage(ref Message m)
    {
        // If we have suspended operation....
        if (_suspended > 0)
        {
            // Intercept the non-client mouse move to prevent the custom
            // chrome of the form from providing hot tracking feedback
            if (m.Msg == PI.WM_.NCMOUSEMOVE)
            {
                return true;
            }

            // A mouse move can occur because a context menu is showing with a popup also 
            // already showing. We suppress the mouse move to prevent tracking of the popup
            return m.Msg == PI.WM_.MOUSEMOVE && ProcessMouseMoveWithCMS(ref m);
        }

        if (CurrentPopup != null)
        {
            // If the popup has been become disposed
            if (CurrentPopup.IsDisposed)
            {
                EndCurrentTracking();
                return false;
            }
            else
            {
                // Get the active window
                var activeWindow = PI.GetActiveWindow();

                // Is there a change in active window?
                if (activeWindow != _activeWindow)
                {
                    // If the current window has become active, ask popup if that is allowed
                    if ((activeWindow == CurrentPopup.Handle) && CurrentPopup.AllowBecomeActiveWhenCurrent)
                    {
                        _activeWindow = CurrentPopup.Handle;
                    }
                    else
                    {
                        var focus = CurrentPopup.ContainsFocus;

                        if (!focus)
                        {
                            var popups = _stack.ToArray();

                            // For from last to first for any popup that has the focus
                            for (var i = popups.Length - 1; i >= 0; i--)
                            {
                                if (!popups[i].IsDisposed)
                                {
                                    if (popups[i].ContainsFocus)
                                    {
                                        focus = true;
                                        break;
                                    }
                                }
                            }
                        }

                        // If the change in active window (focus) is not to the current
                        // or a stacked popup then we need to pull down the entire stack
                        // as focus has been shifted away from the use of any popup.
                        if (!focus)
                        {
                            EndAllTracking();
                            return false;
                        }
                    }
                }
            }

            // We only intercept and handle keyboard and mouse messages
            if (!IsKeyOrMouseMessage(ref m))
            {
                return false;
            }

            switch (m.Msg)
            {
                case PI.WM_.KEYDOWN:
                case PI.WM_.SYSKEYDOWN:
                    // If the popup is telling us to redirect keyboard to itself
                    if (!CurrentPopup.KeyboardInert)
                    {
                        // If the focus is not inside the actual current tracking popup
                        // then we need to manually translate the message to ensure that
                        // KeyPress events occur for the current popup.
                        if (!CurrentPopup.ContainsFocus)
                        {
                            var msg = new PI.MSG
                            {
                                hwnd = m.HWnd,
                                message = m.Msg,
                                lParam = m.LParam,
                                wParam = m.WParam
                            };
                            PI.TranslateMessage(ref msg);
                        }
                        return ProcessKeyboard(ref m);
                    }
                    break;

                case PI.WM_.CHAR:
                case PI.WM_.KEYUP:
                case PI.WM_.DEADCHAR:
                case PI.WM_.SYSCHAR:
                case PI.WM_.SYSKEYUP:
                case PI.WM_.SYSDEADCHAR:
                    // If the popup is telling us to redirect keyboard to itself
                    if (!CurrentPopup.KeyboardInert)
                    {
                        return ProcessKeyboard(ref m);
                    }
                    break;

                case PI.WM_.MOUSEMOVE:
                case PI.WM_.NCMOUSEMOVE:
                    return ProcessMouseMove(ref m);
                case PI.WM_.LBUTTONDOWN:
                case PI.WM_.RBUTTONDOWN:
                case PI.WM_.MBUTTONDOWN:
                    return ProcessClientMouseDown(ref m);
                case PI.WM_.NCLBUTTONDOWN:
                case PI.WM_.NCRBUTTONDOWN:
                case PI.WM_.NCMBUTTONDOWN:
                    return ProcessNonClientMouseDown(ref m);
            }
        }

        return false;
    }
    #endregion

    #region Implementation
    private bool ProcessKeyboard(ref Message m)
    {
        // If focus is not inside the current popup...
        if (!CurrentPopup!.ContainsFocus)
        {
            // ...then redirect the message to the popup so it can process all
            // keyboard input. We just send the message on by altering the handle
            // to the current popup and then suppress processing of current message.
            PI.SendMessage(CurrentPopup.Handle, m.Msg, m.WParam, m.LParam);
            return true;
        }
        else
        {
            // Focus is inside the current popup, so let message be sent there
            return false;
        }
    }

    private bool ProcessClientMouseDown(ref Message m)
    {
        var processed = false;

        // Convert the client position to screen point
        Point screenPt = CommonHelper.ClientMouseMessageToScreenPt(m);

        // Is this message for the current popup?
        if (m.HWnd == CurrentPopup!.Handle)
        {
            // Message is intended for the current popup which means we ask the popup if it
            // would like to kill the entire stack because it knows the mouse down should
            // cancel the showing of popups.
            if (CurrentPopup.DoesCurrentMouseDownEndAllTracking(m, ScreenPtToClientPt(screenPt)))
            {
                EndAllTracking();
            }
        }
        else
        {
            // If the current popup is not the intended recipient but the current popup knows
            // that the mouse down is safe because it is within the client area of itself, then
            // just let the message carry on as normal.
            if (CurrentPopup.DoesCurrentMouseDownContinueTracking(m, ScreenPtToClientPt(screenPt)))
            {
                return processed;
            }
            else
            {
                // Mouse is not inside the client area of the current popup, so we are going to end all tracking
                // unless we can find a popup that wants to become the current popup because the mouse happens to
                // be other it, and it wants it.
                var popups = _stack.ToArray();

                // Search from end towards the front, the last entry is the most recent 'Push'
                foreach (VisualPopup popup in popups)
                {
                    if (!popup.IsDisposed)
                    {
                        // If the mouse down is inside the popup instance
                        if (popup.RectangleToScreen(popup.ClientRectangle).Contains(screenPt))
                        {
                            // Does this stacked popup want to become the current one?
                            if (popup.DoesStackedClientMouseDownBecomeCurrent(m, ScreenPtToClientPt(screenPt, popup.Handle)))
                            {
                                // Kill the popups until back at the requested popup
                                while ((CurrentPopup != null) && (CurrentPopup != popup))
                                {
                                    CurrentPopup.Dispose();
                                    if (_stack.Count > 0)
                                    {
                                        CurrentPopup = _stack.Pop();
                                    }
                                }
                            }

                            return processed;
                        }
                    }
                }

                // Do any of the current popups want the mouse down to be eaten?
                if (CurrentPopup != null)
                {
                    processed = CurrentPopup.DoesMouseDownGetEaten(m, screenPt);
                    if (!processed)
                    {
                        // Search from end towards the front, the last entry is the most recent 'Push'
                        foreach (VisualPopup popup in popups)
                        {
                            if (!popup.IsDisposed)
                            {
                                processed = popup.DoesMouseDownGetEaten(m, screenPt);
                                if (processed)
                                {
                                    break;
                                }
                            }
                        }
                    }
                }

                // Mouse down not intercepted by any popup, so end tracking
                EndAllTracking();
            }
        }

        return processed;
    }

    private bool ProcessNonClientMouseDown(ref Message m)
    {
        // Extract the x and y mouse position from message
        var screenPt = new Point(PI.LOWORD((int)m.LParam), PI.HIWORD((int)m.LParam));

        // Ask the popup if this message causes the entire stack to be killed
        if (CurrentPopup!.DoesCurrentMouseDownEndAllTracking(m, ScreenPtToClientPt(screenPt)))
        {
            EndAllTracking();
        }

        // Do any of the current popups want the mouse down to be eaten?
        var processed = false;
        if (CurrentPopup != null)
        {
            processed = CurrentPopup.DoesMouseDownGetEaten(m, screenPt);
            if (!processed)
            {
                // Search from end towards the front, the last entry is the most recent 'Push'
                var popups = _stack.ToArray();
                foreach (VisualPopup popup in popups)
                {
                    if (!popup.IsDisposed)
                    {
                        processed = popup.DoesMouseDownGetEaten(m, screenPt);
                        if (processed)
                        {
                            break;
                        }
                    }
                }
            }
        }

        return processed;
    }

    private bool ProcessMouseMove(ref Message m)
    {
        // Is this message for a different window?
        if (m.HWnd != CurrentPopup!.Handle)
        {
            // Convert the client position to screen point
            Point screenPt = CommonHelper.ClientMouseMessageToScreenPt(m);

            // Ask the current popup if it allows the mouse move
            if (CurrentPopup.AllowMouseMove(m, screenPt))
            {
                return false;
            }

            // Ask each stacked entry if they allow the mouse move
            var popups = _stack.ToArray();

            // Search from end towards the front, the last entry is the most recent 'Push'
            for (var i = popups.Length - 1; i >= 0; i--)
            {
                if (!popups[i].IsDisposed)
                {
                    if (popups[i].AllowMouseMove(m, screenPt))
                    {
                        return false;
                    }
                }
            }

            // No popup allows the mouse move, so suppress it
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool ProcessMouseMoveWithCMS(ref Message m)
    {
        if (CurrentPopup == null)
        {
            return false;
        }

        // Convert the client position to screen point
        Point screenPt = CommonHelper.ClientMouseMessageToScreenPt(m);

        // Convert from a class to a structure
        var screenPIPt = new PI.POINT
        {
            X = screenPt.X,
            Y = screenPt.Y
        };

        // Get the window handle of the window under this screen point
        var hWnd = PI.WindowFromPoint(screenPIPt);

        // Is the window handle that of the currently tracking popup
        if (CurrentPopup.Handle == hWnd)
        {
            return true;
        }

        // Search all the stacked popups for any that match the window handle
        var popups = _stack.ToArray();
        return popups.Where(popup => !popup.IsDisposed).Any(popup => popup.Handle == hWnd);

        // Mouse move is not over a popup, so allow it
    }

    private Point ScreenPtToClientPt(Point pt) => ScreenPtToClientPt(pt, CurrentPopup!.Handle);

    private Point ScreenPtToClientPt(Point pt, IntPtr handle)
    {
        var clientPt = new PI.POINTC
        {
            x = pt.X,
            y = pt.Y
        };

        // Negative positions are in the range 32767 -> 65535, 
        // so convert to actual int values for the negative positions
        if (clientPt.x >= 32767)
        {
            clientPt.x -= 65536;
        }

        if (clientPt.y >= 32767)
        {
            clientPt.y -= 65536;
        }

        // Convert a 0,0 point from client to screen to find offsetting
        var zeroPIPt = new PI.POINTC
        {
            x = 0,
            y = 0
        };
        PI.MapWindowPoints(IntPtr.Zero, handle, zeroPIPt, 1);

        // Adjust the client coordinate by the offset to get to screen
        clientPt.x += zeroPIPt.x;
        clientPt.y += zeroPIPt.y;

        // Return as a managed point type
        return new Point(clientPt.x, clientPt.y);
    }

    private bool IsKeyOrMouseMessage(ref Message m)
    {
        if (m.Msg is >= PI.WM_.MOUSEMOVE and <= PI.WM_.MOUSEWHEEL)
        {
            return true;
        }

        return m.Msg is >= PI.WM_.NCMOUSEMOVE and <= PI.WM_.NCMBUTTONDBLCLK or >= PI.WM_.KEYDOWN and <= PI.WM_.KEYLAST;
    }

    private void FilterMessages(bool filter)
    {
        if (filter != _filtering)
        {
            if (filter)
            {
                Application.AddMessageFilter(this);
                _filtering = true;
            }
            else
            {
                Application.RemoveMessageFilter(this);
                _filtering = false;
            }
        }
    }

    private void OnCMSClosed(object? sender, ToolStripDropDownClosedEventArgs e)
    {
        // Unhook event from object
        var cms = sender as ContextMenuStrip;
        cms!.Closed -= OnCMSClosed;

        // Revoke the suspended state
        _suspended--;

        // If we are filtering messages but no longer need to filter
        if (_filtering && (CurrentPopup == null))
        {
            Application.RemoveMessageFilter(this);
            _filtering = false;
        }

        // No longer showing a context menu strip
        IsShowingCMS = false;

        // Do we fire a delegate to notify end of the strip?
        if (_cmsFinishDelegate != null)
        {
            _cmsFinishDelegate(this, e);
            _cmsFinishDelegate = null;
        }
    }
    #endregion
}
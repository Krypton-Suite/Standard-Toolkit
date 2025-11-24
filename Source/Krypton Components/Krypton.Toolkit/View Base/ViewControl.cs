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
/// Control that is contained inside an element to act as clipping of real controls.
/// </summary>
[ToolboxItem(false)]
public class ViewControl : Control
{
    #region Static Field
    private static MethodInfo? _miPTB;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when the background needs painting.
    /// </summary>
    public event PaintEventHandler? PaintBackground;

    /// <summary>
    /// Occurs when the WM_NCHITTEST occurs.
    /// </summary>
    public event EventHandler<ViewControlHitTestArgs>? WndProcHitTest;
    #endregion

    #region Instance Fields
    private VisualControl? _rootControl;
    private VisualPopup? _rootPopup;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewControl class.
    /// </summary>
    /// <param name="rootControl">Top level visual control.</param>
    public ViewControl([DisallowNull] VisualControl rootControl)
    {
        Debug.Assert(rootControl != null);

        // We use double buffering to reduce drawing flicker
        SetStyle(ControlStyles.OptimizedDoubleBuffer |
                 ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.UserPaint, true);

        // We need to repaint entire control whenever resized
        SetStyle(ControlStyles.ResizeRedraw, true);

        // We are not selectable
        SetStyle(ControlStyles.Selectable, false);

        // Default
        TransparentBackground = false;
        InDesignMode = false;

        // Remember incoming references
        _rootControl = rootControl;

        // Create delegate so child elements can request a repaint
        NeedPaintDelegate = OnNeedPaint;
    }
    #endregion

    #region ViewLayoutControl
    /// <summary>
    /// Gets and sets access to the view layout control.
    /// </summary>
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public ViewLayoutControl ViewLayoutControl { get; set; }

    #endregion

    #region UpdateParent
    /// <summary>
    /// Gets and sets the root control for point translation and message dispatch. 
    /// </summary>
    /// <param name="parent">Parent control.</param>
    public void UpdateParent(Control? parent)
    {
        // Keep looking till we run out of parents
        while (parent != null)
        {
            // We can hook into a visual control derived class
            if (parent is VisualControl control)
            {
                _rootControl = control;
                _rootPopup = null;
                break;
            }

            // We can hook into a visual popup derived class
            if (parent is VisualPopup popup)
            {
                _rootControl = null;
                _rootPopup = popup;
                break;
            }

            // Move up another level
            parent = parent.Parent!;
        }
    }
    #endregion

    #region TransparentBackground
    /// <summary>
    /// Gets and sets if the background is transparent.
    /// </summary>
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden)]
    public bool TransparentBackground { get; set; }

    #endregion

    #region InDesignMode
    /// <summary>
    /// Gets and sets a value indicating if the control is in design mode.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool InDesignMode { get; set; }

    #endregion

    #region NeedPaintDelegate
    /// <summary>
    /// Gets access to the need paint delegate.
    /// </summary>
    public NeedPaintHandler NeedPaintDelegate { get; }

    #endregion

    #region Protected
    /// <summary>
    /// Raises the Paint event.
    /// </summary>
    /// <param name="e">A PaintEventArgs that contains the event data.</param>
    protected override void OnPaint(PaintEventArgs e)
    {
        // Cannot process a message for a disposed control
        if (!IsDisposed && !Disposing && !RootInstance!.IsDisposed)
        {

            // Do we need to paint the background as the foreground of the parent
            if (TransparentBackground)
            {
                PaintTransparentBackground(e);
            }

            // Give handles a change to draw the background
            PaintBackground?.Invoke(this, e);

            // Create a render context for drawing the view
            using var context = new RenderContext(GetViewManager(), this, RootInstance, e.Graphics,
                e.ClipRectangle, Renderer!);
            // Ask the view to paint itself
            ViewLayoutControl.ChildView?.Render(context);
        }
    }

    /// <summary>
    /// Raises the DoubleClick event.
    /// </summary>
    /// <param name="e">A KeyEventArgs that contains the event data.</param>
    protected override void OnDoubleClick(EventArgs e)
    {
        // Cannot process a message for a disposed control
        if (!IsDisposed && !Disposing && !RootInstance!.IsDisposed)
        {
            // Do we have a manager for processing mouse messages?
            // Use the root controls view manager to process the event
            GetViewManager()?.DoubleClick(PointToClient(MousePosition));
        }

        // Let base class fire events
        base.OnDoubleClick(e);
    }

    /// <summary>
    /// Raises the MouseMove event.
    /// </summary>
    /// <param name="e">A MouseEventArgs that contains the event data.</param>
    protected override void OnMouseMove(MouseEventArgs e)
    {
        // Cannot process a message for a disposed control
        if (!IsDisposed && !Disposing && !RootInstance!.IsDisposed)
        {
            // Do we have a manager for processing mouse messages?
            ViewManager? viewManager = GetViewManager();
            if (viewManager != null)
            {
                // Convert from control to parent control coordinates
                Point rootPoint = RootInstance.PointToClient(PointToScreen(new Point(e.X, e.Y)));

                // Use the root controls view manager to process the event
                viewManager.MouseMove(new MouseEventArgs(e.Button,
                        e.Clicks,
                        rootPoint.X,
                        rootPoint.Y,
                        e.Delta),
                    new Point(e.X, e.Y));
            }
        }

        // Let base class fire events
        base.OnMouseMove(e);
    }

    /// <summary>
    /// Raises the MouseDown event.
    /// </summary>
    /// <param name="e">A MouseEventArgs that contains the event data.</param>
    protected override void OnMouseDown(MouseEventArgs e)
    {
        // Cannot process a message for a disposed control
        if (!IsDisposed && !Disposing && !RootInstance!.IsDisposed)
        {
            // Do we have a manager for processing mouse messages?
            ViewManager? viewManager = GetViewManager();
            if (viewManager != null)
            {
                // Convert from control to parent control coordinates
                Point rootPoint = RootInstance.PointToClient(PointToScreen(new Point(e.X, e.Y)));

                // Use the root controls view manager to process the event
                viewManager.MouseDown(new MouseEventArgs(e.Button,
                        e.Clicks,
                        rootPoint.X,
                        rootPoint.Y,
                        e.Delta),
                    new Point(e.X, e.Y));
            }

            // If the root control does not have focus, then give it the focus now
            if (!RootInstance.ContainsFocus && RootInstance.CanSelect)
            {
                // Do not change focus at design time because 
                if (!InDesignMode)
                {
                    RootInstance.Focus();
                }
            }
        }

        // Let base class fire events
        base.OnMouseDown(e);
    }

    /// <summary>
    /// Raises the MouseUp event.
    /// </summary>
    /// <param name="e">A MouseEventArgs that contains the event data.</param>
    protected override void OnMouseUp(MouseEventArgs e)
    {
        // Cannot process a message for a disposed control
        if (!IsDisposed && !Disposing && !RootInstance!.IsDisposed)
        {
            // Do we have a manager for processing mouse messages?
            ViewManager? viewManager = GetViewManager();
            if (viewManager != null)
            {
                // Convert from control to parent control coordinates
                Point rootPoint = RootInstance.PointToClient(PointToScreen(new Point(e.X, e.Y)));

                // Use the root controls view manager to process the event
                viewManager.MouseUp(new MouseEventArgs(e.Button,
                        e.Clicks,
                        rootPoint.X,
                        rootPoint.Y,
                        e.Delta),
                    new Point(e.X, e.Y));
            }
        }

        // Let base class fire events
        base.OnMouseUp(e);
    }

    /// <summary>
    /// Raises the MouseLeave event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnMouseLeave(EventArgs e)
    {
        // Cannot process a message for a disposed control
        if (!IsDisposed && !Disposing && !RootInstance!.IsDisposed)
        {
            // Do we have a manager for processing mouse messages?
            // Use the root controls view manager to process the event
            GetViewManager()?.MouseLeave(e);
        }

        // Let base class fire events
        base.OnMouseLeave(e);
    }

    /// <summary>
    /// Raises the KeyDown event.
    /// </summary>
    /// <param name="e">A KeyEventArgs that contains the event data.</param>
    protected override void OnKeyDown(KeyEventArgs e)
    {
        // Cannot process a message for a disposed control
        if (!IsDisposed && !Disposing && !RootInstance!.IsDisposed)
        {
            // Do we have a manager for processing mouse messages?
            GetViewManager()?.KeyDown(e);
        }

        // Let base class fire events
        base.OnKeyDown(e);
    }

    /// <summary>
    /// Raises the KeyPress event.
    /// </summary>
    /// <param name="e">A KeyPressEventArgs that contains the event data.</param>
    protected override void OnKeyPress(KeyPressEventArgs e)
    {
        // Cannot process a message for a disposed control
        if (!IsDisposed && !Disposing && !RootInstance!.IsDisposed)
        {
            // Do we have a manager for processing mouse messages?
            GetViewManager()?.KeyPress(e);
        }

        // Let base class fire events
        base.OnKeyPress(e);
    }

    /// <summary>
    /// Raises the KeyUp event.
    /// </summary>
    /// <param name="e">A KeyEventArgs that contains the event data.</param>
    protected override void OnKeyUp(KeyEventArgs e)
    {
        // Cannot process a message for a disposed control
        if (!IsDisposed && !Disposing && !RootInstance!.IsDisposed)
        {
            // Do we have a manager for processing mouse messages?
            GetViewManager()?.KeyUp(e);
        }

        // Let base class fire events
        base.OnKeyUp(e);
    }

    /// <summary>
    /// Processes a notification from palette storage of a paint and optional layout required.
    /// </summary>
    /// <param name="sender">Source of notification.</param>
    /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
    /// <exception cref="ArgumentNullException"></exception>
    protected virtual void OnNeedPaint(object? sender, [DisallowNull] NeedLayoutEventArgs e)
    {
        Debug.Assert(e != null);

        // Validate incoming reference
        if (e == null)
        {
            throw new ArgumentNullException(nameof(e));
        }

        if (IsHandleCreated)
        {
            // Always request the repaint immediately
            if (e.InvalidRect.IsEmpty)
            {
                Invalidate(true);
            }
            else
            {
                Invalidate(e.InvalidRect, true);
            }
        }
    }

    /// <summary>
    /// Process Windows-based messages.
    /// </summary>
    /// <param name="m">A Windows-based message.</param>
    protected override void WndProc(ref Message m)
    {
        // Only interested in intercepting the hit testing
        if (m.Msg == PI.WM_.NCHITTEST)
        {
            // Extract the screen point for the hit test
            var screenPoint = new Point((int)m.LParam.ToInt64());

            // Generate event so message can be processed
            var args = new ViewControlHitTestArgs(PointToClient(screenPoint));
            OnWndProcHitTest(args);

            if (!args.Cancel)
            {
                m.Result = args.Result;
                return;
            }
        }

        base.WndProc(ref m);
    }

    /// <summary>
    /// Raises the WndProcHitTest event.
    /// </summary>
    /// <param name="e">A ViewControlHitTestArgs containing the event data.</param>
    protected virtual void OnWndProcHitTest(ViewControlHitTestArgs e) => WndProcHitTest?.Invoke(this, e);

    #endregion

    #region Implementation
    private Control? RootInstance
    {
        get
        {
            if (_rootControl != null)
            {
                return _rootControl;
            }
            else if (_rootPopup != null)
            {
                return _rootPopup;
            }
            else
            {
                Debug.Assert(false);
                return null;
            }
        }
    }

    private ViewManager? GetViewManager()
    {
        if (_rootControl != null)
        {
            return _rootControl.GetViewManager();
        }
        else if (_rootPopup != null)
        {
            return _rootPopup.GetViewManager();
        }
        else
        {
            Debug.Assert(false);
            return null;
        }
    }

    private IRenderer? Renderer
    {
        get
        {
            if (_rootControl != null)
            {
                return _rootControl.Renderer;
            }
            else if (_rootPopup != null)
            {
                return _rootPopup.Renderer;
            }
            else
            {
                Debug.Assert(false);
                return null;
            }
        }
    }

    private void PaintTransparentBackground(PaintEventArgs e)
    {
        // Get the parent control for transparent drawing purposes
        Control? parent = Parent;

        // Do we have a parent control and we need to paint background?
        if (parent != null)
        {
            // Only grab the required reference once
            if (_miPTB == null)
            {
                // Use reflection so we can call the Windows Forms internal method for painting parent background
                _miPTB = typeof(Control).GetMethod(nameof(PaintTransparentBackground),
                    BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod,
                    null, CallingConventions.HasThis,
                    [typeof(PaintEventArgs), typeof(Rectangle), typeof(Region)],
                    null);
            }

            try
            {
                _ = _miPTB?.Invoke(this, [e, ClientRectangle, null!]);
            }
            catch
            {
                _miPTB = null;
            }
        }
    }
    #endregion
}
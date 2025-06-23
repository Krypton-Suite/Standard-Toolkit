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
/// Manages a view presentation for a control display surface.
/// </summary>
public class ViewManager : GlobalId,
    IDisposable
{
    #region Instance Fields
    private ViewBase? _root;
    private ViewBase? _activeView;
    private long _outputStart;
    #endregion

    #region Events
    /// <summary>
    /// Occurs just before the layout cycle.
    /// </summary>
    public event EventHandler? LayoutBefore;

    /// <summary>
    /// Occurs just after the layout cycle.
    /// </summary>
    public event EventHandler? LayoutAfter;

    /// <summary>
    /// Occurs when the mouse down event is processed.
    /// </summary>
    public event MouseEventHandler? MouseDownProcessed;

    /// <summary>
    /// Occurs when the mouse up event is processed.
    /// </summary>
    public event MouseEventHandler? MouseUpProcessed;

    /// <summary>
    /// Occurs when the mouse up event is processed.
    /// </summary>
    public event PointHandler? DoubleClickProcessed;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewManager class.
    /// </summary>
    public ViewManager()
    {
    }

    /// <summary>
    /// Initialize a new instance of the ViewManager class.
    /// </summary>
    /// <param name="control">Owning control.</param>
    /// <param name="root">Root of the view hierarchy.</param>
    public ViewManager(Control control, ViewBase root)
    {
        _root = root;
        _root.OwningControl = control;
        Control = control;
        AlignControl = control;
    }

    /// <summary>
    /// Clean up any resources.
    /// </summary>
    public virtual void Dispose()
    {
        // Dispose of the associated element hierarchy
        _root?.Dispose();
        GC.SuppressFinalize(this);
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Attach the view manager to provided control and root element.
    /// </summary>
    /// <param name="control">Owning control.</param>
    /// <param name="root">Root of the view hierarchy.</param>
    public void Attach(Control control, ViewBase root)
    {
        _root = root;
        _root.OwningControl = control;
        Control = control;
        AlignControl = control;
    }

    /// <summary>
    /// Gets and sets the view root.
    /// </summary>
    [DisallowNull]
    public ViewBase Root
    {
        [DebuggerStepThrough]
        get => _root!;

        set
        {
            Debug.Assert(value != null);
            _root = value;
            _root!.OwningControl = Control;
        }
    }

    /// <summary>
    /// Control owning the view manager.
    /// </summary>
    public Control Control
    {
        [DebuggerStepThrough]
        get;
        set;
    }

    /// <summary>
    /// Control used to align view elements.
    /// </summary>
    public Control AlignControl
    {
        [DebuggerStepThrough]
        get;
        set;
    }

    /// <summary>
    /// Should child controls be laid out during layout calls.
    /// </summary>
    public bool DoNotLayoutControls
    {
        [DebuggerStepThrough]
        get;
        set;
    }

    /// <summary>
    /// Should debug information be output during layout and paint cycles.
    /// </summary>
    public bool OutputDebug
    {
        [DebuggerStepThrough]
        get;
        set;
    }

    #endregion

    #region GetPreferredSize
    /// <summary>
    /// Discover the preferred size of the view.
    /// </summary>
    /// <param name="renderer">Renderer provider.</param>
    /// <param name="proposedSize">The custom-sized area for a control.</param>
    public virtual Size GetPreferredSize(IRenderer renderer,
        Size proposedSize)
    {
        if (renderer == null || Root == null)
        {
            return proposedSize;
        }

        var retSize = Size.Empty;

        // Short circuit for a disposed control
        if (!Control.IsDisposed)
        {
            // Create a layout context for calculating size and positioning
            using var context = new ViewLayoutContext(this,
                Control, AlignControl, renderer, proposedSize);
            retSize = Root.GetPreferredSize(context);
        }

        if (OutputDebug)
        {
            Console.WriteLine(@"Id:{0} GetPreferredSize Type:{1} Ret:{2} Proposed:{3}",
                Id,
                Control.GetType(),
                retSize,
                proposedSize);
        }

        return retSize;
    }
    #endregion

    #region EvalTransparentPaint

    /// <summary>
    /// Perform a layout of the view.
    /// </summary>
    /// <param name="renderer">Renderer provider.</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns>True if it contains transparent painting.</returns>
    public bool EvalTransparentPaint([DisallowNull] IRenderer renderer)
    {
        Debug.Assert(renderer != null);
        Debug.Assert(Root != null);

        // Validate incoming reference
        if (renderer == null)
        {
            throw new ArgumentNullException(nameof(renderer));
        }

        // Create a layout context for calculating size and positioning
        using var context = new ViewContext(this, Control, AlignControl, renderer);
        // Ask the view to perform operation
        return Root!.EvalTransparentPaint(context);
    }
    #endregion

    #region ActiveView
    /// <summary>
    /// Gets and sets the active view element.
    /// </summary>
    public ViewBase? ActiveView
    {
        get => _activeView;

        set
        {
            // Is there a change in the view?
            if (value != _activeView)
            {
                // Inform old element that mouse is leaving
                _activeView?.MouseLeave(value);

                _activeView = value;

                // Inform new element that mouse is entering
                _activeView?.MouseEnter();
            }
        }
    }
    #endregion

    #region ComponentFromPoint
    /// <summary>
    /// Is the provided point associated with a component.
    /// </summary>
    /// <param name="pt">Mouse point.</param>
    /// <returns>Component reference; otherwise false.</returns>
    public virtual Component? ComponentFromPoint(Point pt)
    {
        // Find the view element associated with the point
        ViewBase? target = Root.ViewFromPoint(pt);

        // Climb parent chain looking for the first element that has a component
        while (target != null)
        {
            if (target.Component != null)
            {
                return target.Component;
            }

            target = target.Parent;
        }

        return null;
    }
    #endregion

    #region MouseCaptured
    /// <summary>
    /// Gets and sets a value indicating if the mouse is capturing input.
    /// </summary>
    public bool MouseCaptured { get; set; }

    #endregion

    #region Layout
    /// <summary>
    /// Perform a layout of the view.
    /// </summary>
    /// <param name="renderer">Renderer provider.</param>
    public virtual void Layout([DisallowNull] IRenderer renderer)
    {
        Debug.Assert(renderer != null);
        Debug.Assert(Root != null);

        // Do nothing if the control is disposed
        if (!Control.IsDisposed)
        {
            // Create a layout context for calculating size and positioning
            using var context = new ViewLayoutContext(this, Control, AlignControl, renderer!);
            Layout(context);
        }
    }

    /// <summary>
    /// Perform a layout of the view.
    /// </summary>
    /// <param name="context">View context for layout operation.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public virtual void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);
        Debug.Assert(context?.Renderer != null);
        Debug.Assert(Root != null);

        // Do nothing if the control is disposed
        if (!context!.Control!.IsDisposed)
        {
            if (OutputDebug)
            {
                PI.QueryPerformanceCounter(ref _outputStart);
            }

            // Validate incoming references
            if (context.Renderer == null)
            {
                throw new ArgumentNullException(nameof(context.Renderer));
            }

            // If someone is interested, tell them the layout cycle to beginning
            LayoutBefore?.Invoke(this, EventArgs.Empty);

            // Ask the view to perform a layout
            Root?.Layout(context);

            // If someone is interested, tell them the layout cycle has finished
            LayoutAfter?.Invoke(this, EventArgs.Empty);

            if (OutputDebug)
            {
                long outputEnd = 0;
                PI.QueryPerformanceCounter(ref outputEnd);
                var outputDiff = outputEnd - _outputStart;

                Console.WriteLine(@"Id:{0} Layout Type:{1} Elapsed:{2} Rect:{3}",
                    Id,
                    context.Control.GetType(),
                    outputDiff,
                    context.DisplayRectangle);

            }

            // Maintain internal counters for measuring perf
            LayoutCounter++;
        }
    }
    #endregion

    #region Paint

    /// <summary>
    /// Perform a paint of the view.
    /// </summary>
    /// <param name="renderer">Renderer provider.</param>
    /// <param name="e">A PaintEventArgs that contains the event data.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public virtual void Paint([DisallowNull] IRenderer renderer, PaintEventArgs? e)
    {
        Debug.Assert(renderer != null);
        Debug.Assert(e != null);

        // Validate incoming references
        if (renderer == null)
        {
            throw new ArgumentNullException(nameof(renderer));
        }

        if (e == null)
        {
            throw new ArgumentNullException(nameof(e));
        }

        // Do nothing if the control is disposed or inside a layout call
        if (!Control.IsDisposed)
        {
            // Create a render context for drawing the view
            using var context = new RenderContext(this,
                Control, AlignControl, e.Graphics, e.ClipRectangle, renderer);
            Paint(context);
        }
    }

    /// <summary>
    /// Perform a paint of the view.
    /// </summary>
    /// <param name="context">Renderer context.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public virtual void Paint([DisallowNull] RenderContext context)
    {
        Debug.Assert(context != null);
        Debug.Assert(Root != null);

        // Validate incoming reference
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        // Do nothing if the control is disposed or inside a layout call
        if (!Control.IsDisposed)
        {
            if (OutputDebug)
            {
                PI.QueryPerformanceCounter(ref _outputStart);
            }

            // Ask the view to paint itself
            Root?.Render(context);

            if (OutputDebug)
            {
                long outputEnd = 0;
                PI.QueryPerformanceCounter(ref outputEnd);
                var outputDiff = outputEnd - _outputStart;

                Console.WriteLine(@"Id:{0} Paint Type:{1} Elapsed: {2}",
                    Id,
                    Control.GetType(),
                    outputDiff);
            }
        }

        // Maintain internal counters for measuring perf
        PaintCounter++;
    }
    #endregion

    #region Mouse

    /// <summary>
    /// Perform mouse movement handling.
    /// </summary>
    /// <param name="e">A MouseEventArgs that contains the event data.</param>
    /// <param name="rawPt">The actual point provided from the windows message.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public virtual void MouseMove([DisallowNull] MouseEventArgs e, Point rawPt)
    {
        Debug.Assert(e != null);

        // Validate incoming reference
        if (e == null)
        {
            throw new ArgumentNullException(nameof(e));
        }

        var pt = new Point(e.X, e.Y);

        // Set the correct active view from the point
        UpdateViewFromPoint(Control, pt);

        // Tell current view of mouse movement
        ActiveView?.MouseMove(rawPt);
    }

    /// <summary>
    /// Perform mouse down processing.
    /// </summary>
    /// <param name="e">A MouseEventArgs that contains the event data.</param>
    /// <param name="rawPt">The actual point provided from the windows message.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public virtual void MouseDown([DisallowNull] MouseEventArgs e, Point rawPt)
    {
        Debug.Assert(e != null);

        // Validate incoming reference
        if (e == null)
        {
            throw new ArgumentNullException(nameof(e));
        }

        var pt = new Point(e.X, e.Y);

        // Set the correct active view from the point
        UpdateViewFromPoint(Control, pt);

        // Tell current view of mouse down
        if (ActiveView != null)
        {
            MouseCaptured = ActiveView.MouseDown(rawPt, e.Button);
        }

        // Generate event to indicate the view manager has processed a mouse down
        PerformMouseDownProcessed(e);
    }

    /// <summary>
    /// Perform mouse up processing.
    /// </summary>
    /// <param name="e">A MouseEventArgs that contains the event data.</param>
    /// <param name="rawPt">The actual point provided from the windows message.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public virtual void MouseUp([DisallowNull] MouseEventArgs e, Point rawPt)
    {
        Debug.Assert(e != null);

        // Validate incoming reference
        if (e == null)
        {
            throw new ArgumentNullException(nameof(e));
        }

        var pt = new Point(e.X, e.Y);

        // Set the correct active view from the point
        UpdateViewFromPoint(Control, pt);

        // Tell current view of mouse up
        ActiveView?.MouseUp(rawPt, e.Button);

        // Release any capture of the mouse
        MouseCaptured = false;

        // Generate event to indicate the view manager has processed a mouse up
        PerformMouseUpProcessed(e);
    }


    /// <summary>
    /// Perform mouse leave processing.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public virtual void MouseLeave([DisallowNull] EventArgs e)
    {
        Debug.Assert(e != null);

        // Validate incoming reference
        if (e == null)
        {
            throw new ArgumentNullException(nameof(e));
        }

        // If there is an active element
        if (ActiveView != null)
        {
            // Remove active view
            ActiveView = null;

            // No capture is in place anymore
            MouseCaptured = false;
        }
    }

    /// <summary>
    /// Perform double click processing.
    /// </summary>
    /// <param name="pt">Control coordinates point.</param>
    public virtual void DoubleClick(Point pt)
    {
        // If there is an active element
        ActiveView?.DoubleClick(pt);

        // Generate event to indicate the view manager has processed a mouse up
        DoubleClickProcessed?.Invoke(this, pt);
    }

    /// <summary>
    /// Raises the MouseDownProcessed event.
    /// </summary>
    /// <param name="e">A MouseEventArgs containing the event data.</param>
    public void PerformMouseDownProcessed(MouseEventArgs e) => MouseDownProcessed?.Invoke(this, e);

    /// <summary>
    /// Raises the MouseUpProcessed event.
    /// </summary>
    /// <param name="e">A MouseEventArgs containing the event data.</param>
    public void PerformMouseUpProcessed(MouseEventArgs e) => MouseUpProcessed?.Invoke(this, e);

    #endregion

    #region Key
    /// <summary>
    /// Perform key down handling.
    /// </summary>
    /// <param name="e">A KeyEventArgs that contains the event data.</param>
    public virtual void KeyDown(KeyEventArgs e)
    {
        // Tell current view of key event
        if (ActiveView != null)
        {
            ActiveView.KeyDown(e);
        }
        else
        {
            _root?.KeyDown(e);
        }
    }

    /// <summary>
    /// Perform key press handling.
    /// </summary>
    /// <param name="e">A KeyPressEventArgs that contains the event data.</param>
    public virtual void KeyPress(KeyPressEventArgs e)
    {
        // Tell current view of key event
        if (ActiveView != null)
        {
            ActiveView.KeyPress(e);
        }
        else
        {
            _root?.KeyPress(e);
        }
    }

    /// <summary>
    /// Perform key up handling.
    /// </summary>
    /// <param name="e">A KeyEventArgs that contains the event data.</param>
    public virtual void KeyUp(KeyEventArgs e)
    {
        // Tell current view of key event
        if (ActiveView != null)
        {
            MouseCaptured = ActiveView.KeyUp(e);
        }
        else if (_root != null)
        {
            MouseCaptured = _root.KeyUp(e);
        }
    }
    #endregion

    #region Source
    /// <summary>
    /// Perform got focus handling.
    /// </summary>
    public virtual void GotFocus()
    {
        // Tell current view of source event
        if (ActiveView != null)
        {
            ActiveView.GotFocus(Control);
        }
        else
        {
            _root?.GotFocus(Control);
        }
    }

    /// <summary>
    /// Perform lost focus handling.
    /// </summary>
    public virtual void LostFocus()
    {
        // Tell current view of source event
        if (ActiveView != null)
        {
            ActiveView.LostFocus(Control);
        }
        else
        {
            _root?.LostFocus(Control);
        }
    }
    #endregion

    #region ResetCounters
    /// <summary>
    /// Reset the internal counters.
    /// </summary>
    public void ResetCounters()
    {
        LayoutCounter = 0;
        PaintCounter = 0;
    }

    /// <summary>
    /// Gets the number of layout cycles performed since last reset.
    /// </summary>
    public int LayoutCounter { get; private set; }

    /// <summary>
    /// Gets the number of paint cycles performed since last reset.
    /// </summary>
    public int PaintCounter { get; private set; }

    #endregion

    #region Protected
    /// <summary>
    /// Update the active view based on the mouse position.
    /// </summary>
    /// <param name="control">Source control.</param>
    /// <param name="pt">Point within the source control.</param>
    protected virtual void UpdateViewFromPoint(Control control, Point pt)
    {
        // Can only change view if not captured
        if (!MouseCaptured)
        {
            // Update the active view with that found under the mouse position
            ActiveView = Root.ViewFromPoint(pt);
        }
    }
    #endregion
}
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
/// Base class from which all view types derive.
/// </summary>
public abstract class ViewBase : GlobalId,
    IDisposable,
    IList<ViewBase>
//ICollection<ViewBase>,
//IEnumerable<ViewBase>
{
    #region Instance Fields

    private bool _enabled;
    private bool _enableDependant;
    private bool _visible;
    private bool _fixed;
    private ViewBase? _enableDependantView;
    private RectangleF _clientRectF;
    private PaletteState _fixedState;
    private PaletteState _elementState;

    private Control? _owningControl;
    private float _factorDpiY;
    private float _factorDpiX;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewBase class.
    /// </summary>
    protected ViewBase()
    {
        // Default remaining internal state
        _enabled = true;
        _visible = true;
        _fixed = false;
        _enableDependant = false;
        _clientRectF = RectangleF.Empty;

        // Default the initial state
        _elementState = PaletteState.Normal;
    }

    /// <summary>
    /// Release resources.
    /// </summary>
    ~ViewBase()
    {
        // Only dispose of resources once
        if (!IsDisposed)
        {
            // Only dispose of managed resources
            Dispose(false);
        }
    }

    /// <summary>
    /// Release managed and unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        // Only dispose of resources once
        if (!IsDisposed)
        {
            // Dispose of managed and unmanaged resources
            Dispose(true);
        }
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Release unmanaged and optionally managed resources.
    /// </summary>
    /// <param name="disposing">Called from Dispose method.</param>
    protected virtual void Dispose(bool disposing)
    {
        // If called from explicit call to Dispose
        if (disposing)
        {
            // Remove reference to parent view
            Parent = null;
        }

        // Mark as disposed
        IsDisposed = true;
    }

    /// <summary>
    /// Gets a value indicating if the view has been disposed.
    /// </summary>
    public bool IsDisposed { get; private set; }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $@"ViewBase:{Id}";

    #endregion

    #region ViewControl
    /// <summary>
    /// Gets and sets a reference to the control instance that contains this view element.
    /// </summary>
    public virtual Control? OwningControl
    {
        get
        {
            Control? owningCrl = _owningControl;
            ViewBase? parent = Parent;
            while (owningCrl is null
                   && parent is not null)
            {
                owningCrl = parent.OwningControl;
                parent = parent.Parent;
            }
            return owningCrl;
        }

        set => _owningControl = value;
    }
    #endregion

    #region Enabled
    /// <summary>
    /// Gets and sets the enabled state of the element.
    /// </summary>
    public virtual bool Enabled
    {
        [DebuggerStepThrough]
        get => _enabled;
        set => _enabled = value;
    }
    #endregion

    #region Visible
    /// <summary>
    /// Gets and sets the enabled state of the element.
    /// </summary>
    public virtual bool Visible
    {
        [DebuggerStepThrough]
        get => _visible;
        set => _visible = value;
    }
    #endregion

    #region Size & Location
    /// <summary>
    /// Gets and sets the rectangle bounding the client area.
    /// </summary>
    public virtual Rectangle ClientRectangle
    {
        [DebuggerStepThrough]
        get => Rectangle.Round(_clientRectF);
        set => _clientRectF = value;
    }

    /// <summary>
    /// 
    /// </summary>
    public virtual RectangleF ClientRectangleF
    {
        [DebuggerStepThrough]
        get => _clientRectF;
        set => _clientRectF = value;
    }

    /// <summary>
    /// Gets and sets the location of the view inside the parent view.
    /// </summary>
    public virtual Point ClientLocation
    {
        [DebuggerStepThrough]
        get => ClientRectangle.Location;
        set => _clientRectF.Location = value;
    }

    /// <summary>
    /// Gets and sets the size of the view.
    /// </summary>
    public virtual Size ClientSize
    {
        [DebuggerStepThrough]
        get => ClientRectangle.Size;
        set => _clientRectF.Size = value;
    }

    /// <summary>
    /// Gets and sets the width of the view.
    /// </summary>
    public virtual int ClientWidth
    {
        [DebuggerStepThrough]
        get => ClientRectangle.Width;
        set => _clientRectF.Width = value;
    }

    /// <summary>
    /// Gets and sets the height of the view.
    /// </summary>
    public virtual int ClientHeight
    {
        [DebuggerStepThrough]
        get => ClientRectangle.Height;
        set => _clientRectF.Height = value;
    }

    /// <summary>
    /// Gets the DpiX of the view.
    /// </summary>
    public float FactorDpiX
    {
        get
        {
            if (_factorDpiX <= 0.1)
            {
                InitialiseFactors();
            }

            return _factorDpiX;
        }
    }

    private void InitialiseFactors()
    {
        // This does mean that the app will not change it's dpi awareness until restarted !
        // Do not use the control dpi, as these values are being used to target the screen
        var screenDc = PI.GetDC(IntPtr.Zero);
        if (screenDc != IntPtr.Zero)
        {
            _factorDpiX = PI.GetDeviceCaps(screenDc, PI.DeviceCap.LOGPIXELSX) / 96f;
            _factorDpiY = PI.GetDeviceCaps(screenDc, PI.DeviceCap.LOGPIXELSY) / 96f;
            PI.ReleaseDC(IntPtr.Zero, screenDc);
        }
        else
        {
            // Do it the slow "init everything long way"
            using Graphics graphics = Graphics.FromHwnd(IntPtr.Zero);
            _factorDpiX = graphics.DpiX / 96f;
            _factorDpiY = graphics.DpiY / 96f;
        }
    }

    /// <summary>
    /// Gets the DpiY of the view.
    /// </summary>
    public float FactorDpiY
    {
        get
        {
            if (_factorDpiY <= 0.1)
            {
                InitialiseFactors();
            }

            return _factorDpiY;
        }
    }

    #endregion

    #region Component
    /// <summary>
    /// Gets the component associated with the element.
    /// </summary>
    public virtual Component? Component { get; set; }

    #endregion

    #region Eval
    /// <summary>
    /// Evaluate the need for drawing transparent areas.
    /// </summary>
    /// <param name="context">Evaluation context.</param>
    /// <returns>True if transparent areas exist; otherwise false.</returns>
    public abstract bool EvalTransparentPaint(ViewContext context);
    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public abstract Size GetPreferredSize(ViewLayoutContext context);

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public abstract void Layout(ViewLayoutContext context);
    #endregion

    #region Paint
    /// <summary>
    /// Perform a render of the elements.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public abstract void Render(RenderContext context);

    /// <summary>
    /// Perform rendering before child elements are rendered.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public virtual void RenderBefore(RenderContext context) { }

    /// <summary>
    /// Perform rendering after child elements are rendered.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public virtual void RenderAfter(RenderContext context) { }
    #endregion

    #region Collection
    /// <summary>
    /// Gets the parent view.
    /// </summary>
    public ViewBase? Parent
    {
        [DebuggerStepThrough]
        get;
        set;
    }

    /// <summary>
    /// Append a view to the collection.
    /// </summary>
    /// <param name="item">ViewBase reference.</param>
    public abstract void Add(ViewBase item);

    /// <summary>
    /// Remove all views from the collection.
    /// </summary>
    public abstract void Clear();

    /// <summary>
    /// Determines whether the collection contains the view.
    /// </summary>
    /// <param name="item">ViewBase reference.</param>
    /// <returns>True if view found; otherwise false.</returns>
    public abstract bool Contains(ViewBase item);

    /// <summary>
    /// Determines whether any part of the view hierarchy is the specified view.
    /// </summary>
    /// <param name="item">ViewBase reference.</param>
    /// <returns>True if view found; otherwise false.</returns>
    public abstract bool ContainsRecurse(ViewBase? item);

    /// <summary>
    /// Copies views to specified array starting at particular index.
    /// </summary>
    /// <param name="array">Target array.</param>
    /// <param name="arrayIndex">Starting array index.</param>
    public abstract void CopyTo(ViewBase[] array, int arrayIndex);

    /// <summary>
    /// Removes first occurrence of specified view.
    /// </summary>
    /// <param name="item">ViewBase reference.</param>
    /// <returns>True if removed; otherwise false.</returns>
    public abstract bool Remove(ViewBase item);

    /// <summary>
    /// Gets the number of views in collection.
    /// </summary>
    public abstract int Count { get; }

    /// <summary>
    /// Gets a value indicating whether the collection is read-only.
    /// </summary>
    public bool IsReadOnly
    {
        [DebuggerStepThrough]
        get => false;
    }

    /// <summary>
    /// Determines the index of the specified view in the collection.
    /// </summary>
    /// <param name="item">ViewBase reference.</param>
    /// <returns>-1 if not found; otherwise index position.</returns>
    public abstract int IndexOf(ViewBase item);

    /// <summary>
    /// Inserts a view to the collection at the specified index.
    /// </summary>
    /// <param name="index">Insert index.</param>
    /// <param name="item">ViewBase reference.</param>
    public abstract void Insert(int index, ViewBase item);

    /// <summary>
    /// Removes the view at the specified index.
    /// </summary>
    /// <param name="index">Remove index.</param>
    public abstract void RemoveAt(int index);

    /// <summary>
    /// Gets or sets the view at the specified index.
    /// </summary>
    /// <param name="index">ViewBase index.</param>
    /// <returns>ViewBase at specified index.</returns>
    public abstract ViewBase this[int index] { get; set; }

    /// <summary>
    /// Shallow enumerate forward over children of the element.
    /// </summary>
    /// <returns>Enumerator instance.</returns>
    public abstract IEnumerator<ViewBase> GetEnumerator();

    /// <summary>
    /// Deep enumerate forward over children of the element.
    /// </summary>
    /// <returns>Enumerator instance.</returns>
    public abstract IEnumerable<ViewBase> Recurse();

    /// <summary>
    /// Shallow enumerate backwards over children of the element.
    /// </summary>
    /// <returns>Enumerator instance.</returns>
    public abstract IEnumerable<ViewBase> Reverse();

    /// <summary>
    /// Deep enumerate backwards over children of the element.
    /// </summary>
    /// <returns>Enumerator instance.</returns>
    public abstract IEnumerable<ViewBase> ReverseRecurse();

    /// <summary>
    /// Enumerate using non-generic interface.
    /// </summary>
    /// <returns>Enumerator instance.</returns>
    [DebuggerStepThrough]
    IEnumerator IEnumerable.GetEnumerator() =>
        // Boilerplate code to satisfy IEnumerable<T> base class.
        GetEnumerator();

    #endregion

    #region Controllers
    /// <summary>
    /// Gets and sets the associated mouse controller.
    /// </summary>
    public virtual IMouseController? MouseController
    {
        [DebuggerStepThrough]
        get;
        set;
    }

    /// <summary>
    /// Gets and sets the associated key controller.
    /// </summary>
    public virtual IKeyController? KeyController
    {
        [DebuggerStepThrough]
        get;
        set;
    }

    /// <summary>
    /// Gets and sets the associated source controller.
    /// </summary>
    public virtual ISourceController? SourceController
    {
        [DebuggerStepThrough]
        get;
        set;
    }

    #endregion

    #region Mouse Events
    /// <summary>
    /// Mouse has entered the view.
    /// </summary>
    public virtual IMouseController? FindMouseController() =>
        // Use mouse controller as first preference
        MouseController ?? Parent?.FindMouseController();

    /// <summary>
    /// Mouse has entered the view.
    /// </summary>
    public virtual void MouseEnter()
    {
        // Use mouse controller as first preference
        if (MouseController != null)
        {
            MouseController.MouseEnter(OwningControl!);
        }
        else
        {
            // Bubble event up to the parent
            Parent?.MouseEnter();
        }
    }

    /// <summary>
    /// Mouse has moved inside the view.
    /// </summary>
    /// <param name="pt">Mouse position relative to control.</param>
    public virtual void MouseMove(Point pt)
    {
        // Use mouse controller as first preference
        if (MouseController != null)
        {
            MouseController.MouseMove(OwningControl!, pt);
        }
        else
        {
            // Bubble event up to the parent
            Parent?.MouseMove(pt);
        }
    }

    /// <summary>
    /// Mouse button has been pressed in the view.
    /// </summary>
    /// <param name="pt">Mouse position relative to control.</param>
    /// <param name="button">Mouse button pressed down.</param>
    /// <returns>True if capturing input; otherwise false.</returns>
    public virtual bool MouseDown(Point pt, MouseButtons button)
    {
        // Use mouse controller as first preference
        if (MouseController != null)
        {
            return MouseController.MouseDown(OwningControl!, pt, button);
        }
        else
        {
            // Bubble event up to the parent
            return Parent?.MouseDown(pt, button) ?? false;
        }
    }

    /// <summary>
    /// Mouse button has been released in the view.
    /// </summary>
    /// <param name="pt">Mouse position relative to control.</param>
    /// <param name="button">Mouse button released.</param>
    public virtual void MouseUp(Point pt, MouseButtons button)
    {
        // Use mouse controller as first preference
        if (MouseController != null)
        {
            MouseController.MouseUp(OwningControl!, pt, button);
        }
        else
        {
            // Bubble event up to the parent
            Parent?.MouseUp(pt, button);
        }
    }

    /// <summary>
    /// Mouse has left the view.
    /// </summary>
    /// <param name="next">Reference to view that is next to have the mouse.</param>
    public virtual void MouseLeave(ViewBase? next)
    {
        // Use mouse controller as first preference
        if (MouseController != null)
        {
            MouseController.MouseLeave(OwningControl!, next);
        }
        else
        {
            // Bubble event up to the parent
            Parent?.MouseLeave(next);
        }
    }

    /// <summary>
    /// Left mouse button has been double clicked.
    /// </summary>
    /// <param name="pt">Mouse position relative to control.</param>
    public virtual void DoubleClick(Point pt)
    {
        // Use mouse controller as first preference
        if (MouseController != null)
        {
            MouseController.DoubleClick(pt);
        }
        else
        {
            // Bubble event up to the parent
            Parent?.DoubleClick(pt);
        }
    }
    #endregion

    #region Key Events
    /// <summary>
    /// Key has been pressed down.
    /// </summary>
    /// <param name="e">A KeyEventArgs that contains the event data.</param>
    public virtual void KeyDown(KeyEventArgs e)
    {
        // Use key controller as first preference
        if (KeyController != null)
        {
            KeyController.KeyDown(OwningControl!, e);
        }
        else
        {
            // Bubble event up to the parent
            Parent?.KeyDown(e);
        }
    }

    /// <summary>
    /// Key has been pressed.
    /// </summary>
    /// <param name="e">A KeyPressEventArgs that contains the event data.</param>
    public virtual void KeyPress(KeyPressEventArgs e)
    {
        // Use mouse controller as first preference
        if (KeyController != null)
        {
            KeyController.KeyPress(OwningControl!, e);
        }
        else
        {
            // Bubble event up to the parent
            Parent?.KeyPress(e);
        }
    }

    /// <summary>
    /// Key has been released.
    /// </summary>
    /// <param name="e">A KeyEventArgs that contains the event data.</param>
    /// <returns>True if capturing input; otherwise false.</returns>
    public virtual bool KeyUp(KeyEventArgs e)
    {
        // Use mouse controller as first preference
        if (KeyController != null)
        {
            return KeyController.KeyUp(OwningControl!, e);
        }
        else
        {
            // Bubble event up to the parent
            return Parent?.KeyUp(e) ?? false;
        }
    }
    #endregion

    #region Source Events
    /// <summary>
    /// Source control has got the focus.
    /// </summary>
    /// <param name="c">Reference to the source control instance.</param>
    public virtual void GotFocus(Control c)
    {
        // Use source controller as first preference
        if (SourceController != null)
        {
            SourceController.GotFocus(c);
        }
        else
        {
            // Bubble event up to the parent
            Parent?.GotFocus(c);
        }
    }

    /// <summary>
    /// Source control has lost the focus.
    /// </summary>
    /// <param name="c">Reference to the source control instance.</param>
    public virtual void LostFocus([DisallowNull] Control c)
    {
        // Use source controller as first preference
        if (SourceController != null)
        {
            SourceController.LostFocus(c);
        }
        else
        {
            // Bubble event up to the parent
            Parent?.LostFocus(c);
        }
    }
    #endregion

    #region ElementState
    /// <summary>
    /// Gets and sets the visual state of the element.
    /// </summary>
    public virtual PaletteState ElementState
    {
        [DebuggerStepThrough]
        get => _elementState;
        set => _elementState = value;
    }

    /// <summary>
    /// Gets the visual state taking into account the owning controls state.
    /// </summary>
    public virtual PaletteState State
    {
        get
        {
            // If fixed then always return the fixed state
            if (IsFixed)
            {
                return _fixedState;
            }
            else
            {
                // If enabled state is dependant on another
                if (IsEnableDependant)
                {
                    // If dependant view is disabled, then so are we
                    if (_enableDependantView is { Enabled: false })
                    {
                        return PaletteState.Disabled;
                    }
                }
                else
                {
                    // If the view disabled, that overrides any element state
                    if (!Enabled)
                    {
                        return PaletteState.Disabled;
                    }
                }

                // No reason to disable the view, so return requested element state
                return ElementState;
            }
        }
    }
    #endregion

    #region FixedState
    /// <summary>
    /// Set a fixed state to override usual behavior and appearance
    /// </summary>
    public virtual PaletteState FixedState
    {
        [DebuggerStepThrough]
        get => _fixedState;

        set
        {
            // Cache the required fixed state
            _fixed = true;
            _fixedState = value;
        }
    }

    /// <summary>
    /// Clear down the use of the fixed state
    /// </summary>
    public virtual void ClearFixedState() =>
        // Clear down the fixed state
        _fixed = false;

    /// <summary>
    /// Gets a value indicating if view is using a fixed state.
    /// </summary>
    public virtual bool IsFixed
    {
        [DebuggerStepThrough]
        get => _fixed;
    }
    #endregion

    #region EnableDependant
    /// <summary>
    /// Get and set the view the enabled state of this view element is dependant on.
    /// </summary>
    public virtual ViewBase? DependantEnabledState
    {
        [DebuggerStepThrough]
        get => _enableDependantView;

        set
        {
            if (value != null)
            {
                _enableDependant = true;
                _enableDependantView = value;
            }
            else
            {
                _enableDependant = false;
                _enableDependantView = null;
            }
        }
    }

    /// <summary>
    /// Gets a value indicating if view enabled state is dependent on another view.
    /// </summary>
    public virtual bool IsEnableDependant
    {
        [DebuggerStepThrough]
        get => _enableDependant;
    }
    #endregion

    #region ViewFromPoint
    /// <summary>
    /// Find the view that contains the specified point.
    /// </summary>
    /// <param name="pt">Point in view coordinates.</param>
    /// <returns>ViewBase if a match is found; otherwise false.</returns>
    public abstract ViewBase? ViewFromPoint(Point pt);
    #endregion
}
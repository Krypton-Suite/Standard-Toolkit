#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

namespace Krypton.Ribbon;

internal class ViewRibbonMinimizedManager : ViewManager
{
    #region Instance Fields
    private readonly KryptonRibbon _ribbon;
    private readonly ViewDrawRibbonGroupsBorderSynch? _viewGroups;
    private ViewDrawRibbonGroup? _activeGroup;
    private readonly NeedPaintHandler? _needPaintDelegate;
    private readonly bool _minimizedMode;
    private bool _active;
    private bool _layingOut;
    private ViewBase? _focusView;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewRibbonManager class.
    /// </summary>
    /// <param name="control">Owning control.</param>
    /// <param name="viewGroups">Group view elements.</param>
    /// <param name="root">Root of the view hierarchy.</param>
    /// <param name="minimizedMode">Is this manager for handling the minimized mode popup.</param>
    /// <param name="needPaintDelegate">Delegate for requesting paint changes.</param>
    public ViewRibbonMinimizedManager([DisallowNull] KryptonRibbon control,
        [DisallowNull] ViewDrawRibbonGroupsBorderSynch viewGroups,
        [DisallowNull] ViewBase root,
        bool minimizedMode,
        [DisallowNull] NeedPaintHandler needPaintDelegate)
        : base(control, root)
    {
        Debug.Assert(viewGroups is not null);
        Debug.Assert(root is not null);
        Debug.Assert(needPaintDelegate is not null);

        _ribbon = control ?? throw new ArgumentNullException(nameof(_ribbon));
        _viewGroups = viewGroups ?? throw new ArgumentNullException(nameof(_viewGroups));
        _needPaintDelegate = needPaintDelegate ?? throw new ArgumentNullException(nameof(needPaintDelegate));
        _active = true;
        _minimizedMode = minimizedMode;
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    public override void Dispose()
    {
        // Remove focus from current view
        FocusView = null;

        base.Dispose();
    }
    #endregion

    #region Active
    /// <summary>
    /// Application we are inside has become active.
    /// </summary>
    public void Active() => _active = true;
    #endregion

    #region Inactive
    /// <summary>
    /// Application we are inside has become inactive.
    /// </summary>
    public void Inactive()
    {
        if (_active)
        {
            // Simulate the mouse leaving the application
            MouseLeave(EventArgs.Empty);

            // No longer active
            _active = false;
        }
    }
    #endregion

    #region GetPreferredSize
    /// <summary>
    /// Discover the preferred size of the view.
    /// </summary>
    /// <param name="renderer">Renderer provider.</param>
    /// <param name="proposedSize">The custom-sized area for a control.</param>
    public override Size GetPreferredSize(IRenderer renderer,
        Size proposedSize)
    {
        // Update the calculate values used during layout calls
        _ribbon.CalculatedValues.Recalculate();

        // Let base class perform standard preferred sizing actions
        return base.GetPreferredSize(renderer, proposedSize);
    }
    #endregion

    #region Layout
    /// <summary>
    /// Perform a layout of the view.
    /// </summary>
    /// <param name="context">View context for layout operation.</param>
    public override void Layout(ViewLayoutContext context)
    {
        // Prevent reentrancy
        if (!_layingOut)
        {
            _layingOut = true;
                
            Form? ownerForm = _ribbon.FindForm();

            // We do not need to layout if inside a control that is minimized
            if (ownerForm is { WindowState: FormWindowState.Minimized })
            {
                return;
            }

            // Update the calculate values used during layout calls
            _ribbon.CalculatedValues.Recalculate();

            // Let base class perform standard layout actions
            base.Layout(context);

            _layingOut = false;
        }
    }
    #endregion

    #region Mouse
    /// <summary>
    /// Perform mouse movement handling.
    /// </summary>
    /// <param name="e">A MouseEventArgs that contains the event data.</param>
    /// <param name="rawPt">The actual point provided from the windows message.</param>
    public override void MouseMove([DisallowNull] MouseEventArgs e, Point rawPt)
    {
        Debug.Assert(e != null);

        // Validate incoming reference
        if (e == null)
        {
            throw new ArgumentNullException(nameof(e));
        }

        // Only interested if the application window we are inside is active
        if (_active)
        {
            // Only hot track groups if in the correct mode
            if (_minimizedMode == _ribbon.RealMinimizedMode)
            {
                // Get the view group instance that matches this point
                ViewDrawRibbonGroup? viewGroup = _viewGroups!.ViewGroupFromPoint(new Point(e.X, e.Y));

                // Is there a change in active group?
                if (viewGroup != _activeGroup)
                {
                    if (_activeGroup != null)
                    {
                        _activeGroup.Tracking = false;
                        _activeGroup.PerformNeedPaint(false, _activeGroup.ClientRectangle);
                    }

                    _activeGroup = viewGroup;

                    if (_activeGroup != null)
                    {
                        _activeGroup.Tracking = true;
                        _activeGroup.PerformNeedPaint(false, _activeGroup.ClientRectangle);
                    }
                }
            }
        }

        // Remember to call base class for standard mouse processing
        base.MouseMove(e, rawPt);
    }

    /// <summary>
    /// Perform mouse leave processing.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    public override void MouseLeave([DisallowNull] EventArgs e)
    {
        Debug.Assert(e != null);

        // Validate incoming reference
        if (e == null)
        {
            throw new ArgumentNullException(nameof(e));
        }

        // Only interested if the application window we are inside is active
        if (_active)
        {
            // If there is an active element
            if (_activeGroup != null)
            {
                _activeGroup.PerformNeedPaint(false, _activeGroup.ClientRectangle);
                _activeGroup.Tracking = false;
                _activeGroup = null;
            }
        }

        // Remember to call base class for standard mouse processing
        base.MouseLeave(e);
    }
    #endregion

    #region Key
    /// <summary>
    /// Perform key down handling.
    /// </summary>
    /// <param name="e">A KeyEventArgs that contains the event data.</param>
    public override void KeyDown(KeyEventArgs e)
    {
        // Tell current view of key event
        if (FocusView != null)
        {
            FocusView.KeyDown(e);
        }
        else
        {
            // Pass onto the ribbon so it can transfer focus to next
            _ribbon.MinimizedKeyDown(e.KeyData);
        }
    }

    /// <summary>
    /// Perform key press handling.
    /// </summary>
    /// <param name="e">A KeyPressEventArgs that contains the event data.</param>
    public override void KeyPress(KeyPressEventArgs e) =>
        // Tell current view of key event
        FocusView?.KeyPress(e);

    /// <summary>
    /// Perform key up handling.
    /// </summary>
    /// <param name="e">A KeyEventArgs that contains the event data.</param>
    public override void KeyUp(KeyEventArgs e)
    {
        // Tell current view of key event
        if (FocusView != null)
        {
            MouseCaptured = FocusView.KeyUp(e);
        }
    }
    #endregion

    #region Protected
    /// <summary>
    /// Update the active view based on the mouse position.
    /// </summary>
    /// <param name="control">Source control.</param>
    /// <param name="pt">Point within the source control.</param>
    protected override void UpdateViewFromPoint(Control control, Point pt)
    {
        // If our application is inactive
        if (!_active)
        {
            // And the mouse is not captured
            if (!MouseCaptured)
            {
                // Then get the view under the mouse
                ViewBase? mouseView = Root.ViewFromPoint(pt);

                // We only allow application button views to be interacted with
                ActiveView = mouseView is ViewDrawRibbonAppButton ? mouseView : null;
            }
        }
        else
        {
            base.UpdateViewFromPoint(control, pt);
        }
    }
    #endregion

    #region Implementation
    /// <summary>
    /// Gets and sets the view that has the focus.
    /// </summary>
    public ViewBase? FocusView
    {
        get => _focusView;

        set
        {
            // Only interested in changes of focus
            if (_focusView != value)
            {
                // Remove focus from existing view
                _focusView?.LostFocus(Root.OwningControl!);

                _focusView = value;

                // Add focus to the new view
                _focusView?.GotFocus(Root.OwningControl!);
            }
        }
    }

    private void PerformNeedPaint(bool needLayout) => PerformNeedPaint(needLayout, Rectangle.Empty);

    private void PerformNeedPaint(bool needLayout, Rectangle invalidRect) => _needPaintDelegate?.Invoke(this, new NeedLayoutEventArgs(needLayout, invalidRect));
    #endregion
}
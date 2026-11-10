#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Process mouse and keyboard events for a context-menu gallery item.
/// </summary>
internal class MenuGalleryItemController : GlobalId,
    IMouseController,
    ISourceController,
    IKeyController,
    IContextMenuTarget
{
    #region Instance Fields

    private readonly ViewDrawMenuGalleryItem _target;
    private readonly ViewLayoutMenuGallery _layout;
    private readonly ViewContextMenuManager _viewManager;
    private NeedPaintHandler? _needPaint;
    private bool _mouseOver;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when the mouse is used to left click the target.
    /// </summary>
    public event MouseEventHandler? Click;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="MenuGalleryItemController"/> class.
    /// </summary>
    public MenuGalleryItemController(ViewContextMenuManager viewManager,
        ViewDrawMenuGalleryItem target,
        ViewLayoutMenuGallery layout,
        NeedPaintHandler? needPaint)
    {
        Debug.Assert(viewManager is not null);
        Debug.Assert(target is not null);
        Debug.Assert(layout is not null);
        Debug.Assert(needPaint is not null);

        MousePoint = CommonHelper.NullPoint;
        _viewManager = viewManager!;
        _target = target!;
        _layout = layout!;
        NeedPaint = needPaint!;
    }

    #endregion

    #region MousePoint

    /// <summary>
    /// Gets the current tracking mouse point.
    /// </summary>
    public Point MousePoint { get; private set; }

    #endregion

    #region IContextMenuTarget

    /// <inheritdoc />
    public virtual bool HasSubMenu => false;

    /// <inheritdoc />
    public virtual void ShowTarget()
    {
        _target.Track();
        UpdateTargetState(new Point(int.MaxValue, int.MaxValue));
    }

    /// <inheritdoc />
    public virtual void ClearTarget()
    {
        _target.Untrack();
        UpdateTargetState(new Point(int.MaxValue, int.MaxValue));
    }

    /// <inheritdoc />
    public void ShowSubMenu()
    {
    }

    /// <inheritdoc />
    public void ClearSubMenu()
    {
    }

    /// <inheritdoc />
    public bool MatchMnemonic(char charCode) => false;

    /// <inheritdoc />
    public void MnemonicActivate()
    {
    }

    /// <inheritdoc />
    public ViewBase GetActiveView() => _target;

    /// <inheritdoc />
    public Rectangle ClientRectangle => _target.ClientRectangle;

    /// <inheritdoc />
    public bool DoesStackedClientMouseDownBecomeCurrent(Point pt) => true;

    #endregion

    #region IMouseController

    /// <inheritdoc />
    public virtual void MouseEnter(Control c)
    {
        if (_layout.ItemEnabled)
        {
            _mouseOver = true;
            UpdateTargetState(c);
        }
    }

    /// <inheritdoc />
    public virtual void MouseMove(Control c, Point pt)
    {
        if (_layout.ItemEnabled)
        {
            MousePoint = pt;
            UpdateTargetState(pt);
        }
    }

    /// <inheritdoc />
    public virtual bool MouseDown(Control c, Point pt, MouseButtons button)
    {
        if (_layout.ItemEnabled && button == MouseButtons.Left)
        {
            Captured = true;
            UpdateTargetState(pt);
        }

        return Captured;
    }

    /// <inheritdoc />
    public virtual void MouseUp(Control c, Point pt, MouseButtons button)
    {
        if (_layout.ItemEnabled && Captured)
        {
            Captured = false;
            if (button == MouseButtons.Left)
            {
                if (_target.ElementState == PaletteState.Pressed)
                {
                    _target.ElementState = PaletteState.Tracking;
                    if (_target.Enabled)
                    {
                        OnClick(new MouseEventArgs(MouseButtons.Left, 1, pt.X, pt.Y, 0));
                    }
                }

                OnNeedPaint(true);
            }
            else
            {
                UpdateTargetState(pt);
            }
        }
    }

    /// <inheritdoc />
    public virtual void MouseLeave(Control c, ViewBase? next)
    {
        if (!_target.ContainsRecurse(next))
        {
            _mouseOver = false;
            MousePoint = CommonHelper.NullPoint;
            Captured = false;
            UpdateTargetState(c);
        }
    }

    /// <inheritdoc />
    public virtual void DoubleClick(Point pt)
    {
    }

    /// <inheritdoc />
    public virtual bool IgnoreVisualFormLeftButtonDown => false;

    #endregion

    #region ISourceController

    /// <inheritdoc />
    public void GotFocus(Control c)
    {
    }

    /// <inheritdoc />
    public void LostFocus([DisallowNull] Control c)
    {
    }

    #endregion

    #region IKeyController

    /// <inheritdoc />
    public virtual void KeyDown([DisallowNull] Control c, [DisallowNull] KeyEventArgs e)
    {
        if (c == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(c));
        }

        if (e == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(e));
        }

        switch (e.KeyCode)
        {
            case Keys.Enter:
            case Keys.Space:
                if (_layout.ItemEnabled)
                {
                    var pt = new Point(int.MaxValue, int.MaxValue);
                    OnClick(new MouseEventArgs(MouseButtons.Left, 1, pt.X, pt.Y, 0));
                    UpdateTargetState(pt);
                }
                break;
            case Keys.Tab:
                _viewManager.KeyTab(e.Shift);
                break;
            case Keys.Home:
                _viewManager.KeyHome();
                break;
            case Keys.End:
                _viewManager.KeyEnd();
                break;
            case Keys.Up:
                _viewManager.KeyUp();
                break;
            case Keys.Down:
                _viewManager.KeyDown();
                break;
            case Keys.Left:
                _viewManager.KeyLeft(true);
                break;
            case Keys.Right:
                _viewManager.KeyRight();
                break;
        }
    }

    /// <inheritdoc />
    public virtual void KeyPress([DisallowNull] Control c, [DisallowNull] KeyPressEventArgs e)
    {
        if (c == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(c));
        }

        if (e == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(e));
        }

        _viewManager.KeyMnemonic(e.KeyChar);
    }

    /// <inheritdoc />
    public virtual bool KeyUp([DisallowNull] Control c, [DisallowNull] KeyEventArgs e)
    {
        if (c == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(c));
        }

        ThrowHelper.ThrowIfNull(e);
        return false;
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets and sets the need paint delegate for notifying paint requests.
    /// </summary>
    public NeedPaintHandler? NeedPaint
    {
        get => _needPaint;
        set => _needPaint = value;
    }

    /// <summary>
    /// Gets access to the associated target of the controller.
    /// </summary>
    public ViewBase Target => _target;

    #endregion

    #region Protected

    /// <summary>
    /// Gets a value indicating if mouse input is being captured.
    /// </summary>
    protected bool Captured { get; set; }

    /// <summary>
    /// Set the correct visual state of the target.
    /// </summary>
    /// <param name="c">Owning control.</param>
    protected void UpdateTargetState(Control c)
    {
        if (c is { IsDisposed: false })
        {
            Form? f = c.FindForm();
            if (f is { Visible: true })
            {
                UpdateTargetState(c.PointToClient(Control.MousePosition));
                return;
            }
        }

        UpdateTargetState(new Point(int.MaxValue, int.MaxValue));
    }

    /// <summary>
    /// Set the correct visual state of the target.
    /// </summary>
    /// <param name="pt">Mouse point.</param>
    protected virtual void UpdateTargetState(Point pt)
    {
        PaletteState newState;
        if (!_target.Enabled)
        {
            newState = PaletteState.Disabled;
        }
        else if (Captured)
        {
            newState = _target.ClientRectangle.Contains(pt) ? PaletteState.Pressed : PaletteState.Tracking;
        }
        else
        {
            newState = _mouseOver ? PaletteState.Tracking : PaletteState.Normal;
        }

        if (_target.ElementState != newState)
        {
            if (newState == PaletteState.Tracking)
            {
                _target.Track();
            }
            else
            {
                _target.Untrack();
            }

            _target.ElementState = newState;
            OnNeedPaint(false);
        }
    }

    /// <summary>
    /// Raises the Click event.
    /// </summary>
    /// <param name="e">Mouse event data.</param>
    protected virtual void OnClick(MouseEventArgs e) => Click?.Invoke(_target, e);

    /// <summary>
    /// Raises the NeedPaint event.
    /// </summary>
    /// <param name="needLayout">Does the palette change require a layout.</param>
    protected virtual void OnNeedPaint(bool needLayout) =>
        _needPaint?.Invoke(this, new NeedLayoutEventArgs(needLayout, _target.ClientRectangle));

    #endregion
}

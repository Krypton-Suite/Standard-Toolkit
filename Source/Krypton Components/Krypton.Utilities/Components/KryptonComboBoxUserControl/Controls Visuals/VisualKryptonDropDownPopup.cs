#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

/// <summary>
/// VisualPopup subclass that hosts an arbitrary <see cref="Control"/> (typically a developer-supplied
/// <see cref="UserControl"/>) as the drop-down portion of a <see cref="KryptonComboBoxUserControl"/>.
/// </summary>
internal sealed class VisualKryptonDropDownPopup : VisualPopup
{
    #region Constants

    /// <summary>Side length of the bottom-right resize grip, in pixels.</summary>
    private const int GripSize = 12;

    #endregion

    #region Instance Fields

    private readonly KryptonComboBoxUserControl _owner;
    private readonly Control _dropContent;
    private readonly IKryptonDropDownUserControl? _contractContent;
    private readonly bool _resizable;
    private readonly Size _minDropSize;
    private readonly Size _maxDropSize;
    private bool _closedNotified;

    #endregion

    #region Events

    /// <summary>Raised when the drop-down content commits a value.</summary>
    public event EventHandler<KryptonDropDownCommitEventArgs>? ValueCommitted;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="VisualKryptonDropDownPopup"/> class.
    /// </summary>
    /// <param name="owner">The owning combo box.</param>
    /// <param name="dropContent">The user-supplied control to host.</param>
    /// <param name="renderer">The renderer to use for the popup chrome.</param>
    /// <param name="resizable">Whether the popup should expose a bottom-right resize grip.</param>
    /// <param name="minDropSize">Minimum allowed popup size when resizing. Use <see cref="Size.Empty"/> to skip.</param>
    /// <param name="maxDropSize">Maximum allowed popup size when resizing. Use <see cref="Size.Empty"/> to skip.</param>
    public VisualKryptonDropDownPopup(KryptonComboBoxUserControl owner,
                                      Control dropContent,
                                      IRenderer? renderer,
                                      bool resizable,
                                      Size minDropSize,
                                      Size maxDropSize)
        : base(new ViewManager(), renderer, true)
    {
        _owner = owner;
        _dropContent = dropContent;
        _contractContent = dropContent as IKryptonDropDownUserControl;
        _resizable = resizable;
        _minDropSize = minDropSize;
        _maxDropSize = maxDropSize;

        _dropContent.Dock = DockStyle.Fill;

        // Build a docker view so the popup paints a Krypton border around the hosted control
        var layoutFill = new ViewLayoutFill(_dropContent);
        var layoutDocker = new ViewLayoutDocker
        {
            { layoutFill, ViewDockStyle.Fill }
        };

        ViewManager!.Control = this;
        ViewManager!.AlignControl = this;
        ViewManager!.Root = layoutDocker;

        Controls.Add(_dropContent);

        if (_contractContent != null)
        {
            _contractContent.CommitValue += OnContentCommitValue;
            _contractContent.RequestClose += OnContentRequestClose;
        }
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Detach (do not dispose) the user-supplied control so the host can reuse it
            if (Controls.Contains(_dropContent))
            {
                Controls.Remove(_dropContent);
                _dropContent.Dock = DockStyle.None;
            }

            if (_contractContent != null)
            {
                _contractContent.CommitValue -= OnContentCommitValue;
                _contractContent.RequestClose -= OnContentRequestClose;
            }

            // Always notify the contract content that the drop-down has closed, even when the
            // popup was dismissed by the popup manager (e.g. click outside the popup).
            if (!_closedNotified)
            {
                _closedNotified = true;
                _contractContent?.OnDropDownClosed(_owner);
            }
        }

        base.Dispose(disposing);
    }

    #endregion

    #region Public

    /// <summary>
    /// Show the popup anchored to the supplied screen rectangle (typically the parent control's
    /// screen rectangle). The popup positions itself below the rectangle when there is room,
    /// otherwise above. Horizontal alignment honours <paramref name="alignment"/>.
    /// </summary>
    /// <param name="anchorScreenRect">Screen rectangle of the parent control.</param>
    /// <param name="popupSize">Desired popup size.</param>
    /// <param name="alignment">Left or right alignment relative to <paramref name="anchorScreenRect"/>.</param>
    public void ShowAnchored(Rectangle anchorScreenRect, Size popupSize, LeftRightAlignment alignment)
    {
        // Notify the contract content (if any) that we are about to open
        _contractContent?.OnDropDownOpening(_owner);

        // Clamp size to the requested min/max
        if (!_minDropSize.IsEmpty)
        {
            popupSize = new Size(Math.Max(popupSize.Width, _minDropSize.Width),
                                 Math.Max(popupSize.Height, _minDropSize.Height));
        }
        if (!_maxDropSize.IsEmpty)
        {
            popupSize = new Size(Math.Min(popupSize.Width, _maxDropSize.Width),
                                 Math.Min(popupSize.Height, _maxDropSize.Height));
        }

        Screen screen = Screen.FromRectangle(anchorScreenRect);
        Rectangle work = screen.WorkingArea;

        int x = alignment == LeftRightAlignment.Right
            ? anchorScreenRect.Right - popupSize.Width
            : anchorScreenRect.Left;

        // Keep horizontally on screen
        if (x + popupSize.Width > work.Right)
        {
            x = work.Right - popupSize.Width;
        }
        if (x < work.Left)
        {
            x = work.Left;
        }

        // Prefer below the anchor; if not enough room, flip above
        int y = anchorScreenRect.Bottom;
        if (y + popupSize.Height > work.Bottom)
        {
            int aboveY = anchorScreenRect.Top - popupSize.Height;
            if (aboveY >= work.Top)
            {
                y = aboveY;
            }
            else
            {
                // Not enough room either way; clamp downward and trim height
                y = work.Bottom - popupSize.Height;
                if (y < work.Top)
                {
                    y = work.Top;
                    popupSize = new Size(popupSize.Width, work.Bottom - work.Top);
                }
            }
        }

        Show(new Rectangle(x, y, popupSize.Width, popupSize.Height));

        _contractContent?.OnDropDownOpened(_owner);
    }

    #endregion

    #region Protected Overrides

    /// <summary>
    /// Allow drop-down content to receive focus (e.g. a tree, grid or list inside the popup).
    /// </summary>
    public override bool AllowBecomeActiveWhenCurrent => true;

    /// <summary>
    /// Allow content controls inside the popup to handle their own keyboard input.
    /// </summary>
    public override bool KeyboardInert => true;

    /// <summary>
    /// Mouse-down inside the resize grip must keep the popup alive.
    /// </summary>
    public override bool DoesCurrentMouseDownEndAllTracking(Message m, Point pt)
    {
        if (_resizable && IsInResizeGrip(pt))
        {
            return false;
        }

        return base.DoesCurrentMouseDownEndAllTracking(m, pt);
    }

    /// <summary>
    /// Process Windows-based messages.
    /// </summary>
    /// <param name="m">A Windows-based message.</param>
    protected override void WndProc(ref Message m)
    {
        if (_resizable && m.Msg == PI.WM_.NCHITTEST)
        {
            // Convert lParam (screen point) to client and check the grip area
            int lParam = (int)m.LParam;
            var screenPt = new Point((short)PI.LOWORD(lParam), (short)PI.HIWORD(lParam));
            Point clientPt = PointToClient(screenPt);

            if (IsInResizeGrip(clientPt))
            {
                m.Result = (IntPtr)PI.HT.BOTTOMRIGHT;
                return;
            }
        }

        base.WndProc(ref m);
    }

    /// <summary>
    /// Paint the resize grip in the bottom-right corner when resizing is enabled.
    /// </summary>
    /// <param name="e">A PaintEventArgs that contains the event data.</param>
    protected override void OnPaint(PaintEventArgs? e)
    {
        base.OnPaint(e);

        if (_resizable && e != null)
        {
            ControlPaint.DrawSizeGrip(e.Graphics, BackColor,
                Width - GripSize - 1, Height - GripSize - 1, GripSize, GripSize);
        }
    }

    #endregion

    #region Implementation

    private bool IsInResizeGrip(Point clientPt) =>
        clientPt.X >= Width - GripSize && clientPt.Y >= Height - GripSize;

    private void OnContentCommitValue(object? sender, KryptonDropDownCommitEventArgs e)
    {
        ValueCommitted?.Invoke(this, e);

        if (!e.KeepOpen)
        {
            CloseInternal();
        }
    }

    private void OnContentRequestClose(object? sender, EventArgs e) => CloseInternal();

    private void CloseInternal()
    {
        if (IsDisposed)
        {
            return;
        }

        bool cancel = false;
        _contractContent?.OnDropDownClosing(_owner, ref cancel);
        if (cancel)
        {
            return;
        }

        if (IsHandleCreated)
        {
            VisualPopupManager.Singleton.EndPopupTracking(this);
        }
    }

    #endregion
}

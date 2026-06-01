#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Borderless top-level form that hosts an arbitrary <see cref="Control"/> as the drop-down portion of
/// a <see cref="KryptonComboBoxUserControl"/>. Uses standard WinForms HWND hosting instead of
/// <see cref="VisualPopup"/> so nested interactive controls (tree views, grids, etc.) paint reliably.
/// </summary>
internal sealed class KryptonDropDownHostForm : Form
{
    #region Constants

    private const int GripSize = 12;

    #endregion

    #region Instance Fields

    private readonly KryptonComboBoxUserControl _owner;
    private readonly Panel _panel;
    private readonly Control _dropContent;
    private readonly IKryptonDropDownUserControl? _contractContent;
    private readonly bool _resizable;
    private readonly Size _minDropSize;
    private readonly Size _maxDropSize;
    private readonly KryptonDropDownHostMessageFilter _messageFilter;
    private VisualPopupShadow? _shadow;
    private bool _dropDownVisible;
    private bool _closedNotified;

    #endregion

    #region Events

    public event EventHandler<KryptonDropDownCommitEventArgs>? ValueCommitted;

    #endregion

    #region Identity

    public KryptonDropDownHostForm(KryptonComboBoxUserControl owner,
                                   Control dropContent,
                                   bool resizable,
                                   Size minDropSize,
                                   Size maxDropSize)
    {
        _owner = owner;
        _dropContent = dropContent;
        _contractContent = dropContent as IKryptonDropDownUserControl;
        _resizable = resizable;
        _minDropSize = minDropSize;
        _maxDropSize = maxDropSize;
        _messageFilter = new KryptonDropDownHostMessageFilter(this);

        FormBorderStyle = FormBorderStyle.None;
        ShowInTaskbar = false;
        TopMost = true;
        StartPosition = FormStartPosition.Manual;
        AutoScaleMode = AutoScaleMode.None;
        ControlBox = false;
        MaximizeBox = false;
        MinimizeBox = false;

        EnableDoubleBuffering(this);

        // A plain Panel avoids VisualPanel/KryptonPanel UserPaint fighting nested HWND children.
        _panel = new Panel
        {
            Dock = DockStyle.Fill,
            BorderStyle = BorderStyle.FixedSingle
        };
        EnableDoubleBuffering(_panel);
        _panel.Paint += OnPanelPaint;

        _dropContent.Dock = DockStyle.Fill;
        EnableDoubleBuffering(_dropContent);

        SuspendLayout();
        _panel.SuspendLayout();
        _panel.Controls.Add(_dropContent);
        Controls.Add(_panel);
        _panel.ResumeLayout(false);
        ResumeLayout(false);

        ApplyPanelChrome();

        if (_contractContent != null)
        {
            _contractContent.CommitValue += OnContentCommitValue;
            _contractContent.RequestClose += OnContentRequestClose;
        }

        Deactivate += OnHostDeactivate;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            Deactivate -= OnHostDeactivate;
            Application.RemoveMessageFilter(_messageFilter);

            if (_shadow != null)
            {
                _shadow.Dispose();
                _shadow = null;
            }

            _panel.Paint -= OnPanelPaint;

            HideDropDownSurface();

            if (_panel.Controls.Contains(_dropContent))
            {
                _panel.Controls.Remove(_dropContent);
                _dropContent.Dock = DockStyle.None;
            }

            if (_contractContent != null)
            {
                _contractContent.CommitValue -= OnContentCommitValue;
                _contractContent.RequestClose -= OnContentRequestClose;
            }

            if (!_closedNotified)
            {
                _closedNotified = true;
                _contractContent?.OnDropDownClosed(_owner);
            }
        }

        base.Dispose(disposing);
    }

    protected override CreateParams CreateParams
    {
        get
        {
            CreateParams cp = base.CreateParams;
            cp.ExStyle |= unchecked((int)(PI.WS_EX_.TOPMOST | PI.WS_EX_.TOOLWINDOW | PI.WS_EX_.COMPOSITED));
            return cp;
        }
    }

    protected override bool ShowWithoutActivation => true;

    #endregion

    #region Public

    public bool IsDropDownVisible => _dropDownVisible && !IsDisposed;

    public void ShowAnchored(Rectangle anchorScreenRect, Size popupSize, LeftRightAlignment alignment)
    {
        _contractContent?.OnDropDownOpening(_owner);

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

        if (x + popupSize.Width > work.Right)
        {
            x = work.Right - popupSize.Width;
        }

        if (x < work.Left)
        {
            x = work.Left;
        }

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
                y = work.Bottom - popupSize.Height;
                if (y < work.Top)
                {
                    y = work.Top;
                    popupSize = new Size(popupSize.Width, work.Bottom - work.Top);
                }
            }
        }

        var screenRect = new Rectangle(x, y, popupSize.Width, popupSize.Height);
        Bounds = screenRect;
        MinimumSize = _minDropSize.IsEmpty ? Size.Empty : _minDropSize;
        MaximumSize = _maxDropSize.IsEmpty ? Size.Empty : _maxDropSize;

        ApplyPanelChrome();

        _shadow = new VisualPopupShadow();
        _shadow.Show(screenRect);

        Application.AddMessageFilter(_messageFilter);
        Show();

        _dropDownVisible = true;
        _contractContent?.OnDropDownOpened(_owner);
    }

    public void CloseDropDown() => CloseInternal();

    internal bool IsInResizeGrip(Point clientPt) =>
        _resizable && clientPt.X >= Width - GripSize && clientPt.Y >= Height - GripSize;

    internal bool IsOwnerOrHost(IntPtr hwnd)
    {
        if (hwnd == IntPtr.Zero)
        {
            return false;
        }

        for (Control? c = Control.FromHandle(hwnd); c != null; c = c.Parent)
        {
            if (c == this || c == _owner)
            {
                return true;
            }
        }

        return false;
    }

    #endregion

    #region Protected Overrides

    protected override void WndProc(ref Message m)
    {
        if (_resizable && m.Msg == (int)PI.WM_.NCHITTEST)
        {
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

    #endregion

    #region Implementation

    private void OnPanelPaint(object? sender, PaintEventArgs e)
    {
        if (_resizable)
        {
            ControlPaint.DrawSizeGrip(e.Graphics, _panel.BackColor,
                _panel.Width - GripSize, _panel.Height - GripSize, GripSize, GripSize);
        }
    }

    private void ApplyPanelChrome()
    {
        Color backColor = SystemColors.Window;

        try
        {
            PaletteBase palette = _owner.GetResolvedPalette();
            backColor = palette.GetBackColor1(PaletteBackStyle.PanelClient, PaletteState.Normal);
        }
        catch
        {
            // Use the system window colour when the palette cannot be resolved.
        }

        BackColor = backColor;
        _panel.BackColor = backColor;
        _dropContent.BackColor = backColor;
    }

    private static void EnableDoubleBuffering(Control control)
    {
        typeof(Control).InvokeMember(@"DoubleBuffered",
            BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
            null, control, new object[] { true });

        foreach (Control child in control.Controls)
        {
            EnableDoubleBuffering(child);
        }
    }

    private void OnHostDeactivate(object? sender, EventArgs e)
    {
        if (IsDisposed)
        {
            return;
        }

        BeginInvoke(new Action(() =>
        {
            if (IsDisposed)
            {
                return;
            }

            if (ContainsFocus || _owner.ContainsFocus)
            {
                return;
            }

            CloseInternal();
        }));
    }

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
        if (IsDisposed || !_dropDownVisible)
        {
            return;
        }

        bool cancel = false;
        _contractContent?.OnDropDownClosing(_owner, ref cancel);
        if (cancel)
        {
            return;
        }

        HideDropDownSurface();
    }

    private void HideDropDownSurface()
    {
        if (!_dropDownVisible)
        {
            return;
        }

        _dropDownVisible = false;
        Application.RemoveMessageFilter(_messageFilter);

        if (_shadow != null)
        {
            _shadow.Dispose();
            _shadow = null;
        }

        if (IsHandleCreated)
        {
            Hide();
        }

        _contractContent?.OnDropDownClosed(_owner);
    }

    #endregion

    #region Message Filter

    private sealed class KryptonDropDownHostMessageFilter : IMessageFilter
    {
        private readonly KryptonDropDownHostForm _host;

        public KryptonDropDownHostMessageFilter(KryptonDropDownHostForm host) => _host = host;

        public bool PreFilterMessage(ref Message m)
        {
            if (_host.IsDisposed)
            {
                return false;
            }

            switch (m.Msg)
            {
                case (int)PI.WM_.LBUTTONDOWN:
                case (int)PI.WM_.RBUTTONDOWN:
                case (int)PI.WM_.MBUTTONDOWN:
                    return ProcessMouseDown(ref m);
                case (int)PI.WM_.NCLBUTTONDOWN:
                case (int)PI.WM_.NCRBUTTONDOWN:
                case (int)PI.WM_.NCMBUTTONDOWN:
                    return ProcessNonClientMouseDown(ref m);
            }

            return false;
        }

        private bool ProcessMouseDown(ref Message m)
        {
            Point screenPt = CommonHelper.ClientMouseMessageToScreenPt(m);

            if (_host.IsInResizeGrip(_host.PointToClient(screenPt)))
            {
                return false;
            }

            if (_host.IsOwnerOrHost(m.HWnd))
            {
                return false;
            }

            _host.CloseInternal();
            return false;
        }

        private bool ProcessNonClientMouseDown(ref Message m)
        {
            int lParam = (int)m.LParam;
            var screenPt = new Point((short)PI.LOWORD(lParam), (short)PI.HIWORD(lParam));

            if (_host.Bounds.Contains(screenPt))
            {
                return false;
            }

            if (_host._owner.RectangleToScreen(_host._owner.ClientRectangle).Contains(screenPt))
            {
                return false;
            }

            _host.CloseInternal();
            return false;
        }
    }

    #endregion
}

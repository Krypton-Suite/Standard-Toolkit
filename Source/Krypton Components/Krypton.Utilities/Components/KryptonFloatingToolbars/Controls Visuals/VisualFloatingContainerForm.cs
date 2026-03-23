#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

public partial class VisualFloatingContainerForm : KryptonForm
{
    #region Instance Fields

    private bool _showWindowFrame;

    private KryptonFloatableMenuStrip? _floatableMenuStrip;

    private KryptonFloatableToolStrip? _floatableToolStrip;

    private readonly int _dFrameWidth = 8;
    
    private readonly int _captionWidth = 18;

    private readonly int _maxWidth = 0;

    private int _minWidth = 0;

    #endregion

    #region Public

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool ShowWindowFrame { get => _showWindowFrame; set { _showWindowFrame = value; Invalidate(); } }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonFloatableMenuStrip? KryptonFloatableMenuStrip
    {
        get => _floatableMenuStrip;

        set
        {
            if (_floatableMenuStrip != null && _floatableMenuStrip != value)
            {
                _floatableMenuStrip = value;
            }
        }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonFloatableToolStrip? KryptonFloatableToolStrip
    {
        get => _floatableToolStrip;

        set
        {
            if (_floatableToolStrip != null && _floatableToolStrip != value)
            {
                _floatableToolStrip = value;
            }
        }
    }

    #endregion

    #region Runtime Routines
    [DllImport("user32.dll")]
    static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

    [DllImport("user32.dll")]
    static extern int GetMenuItemCount(IntPtr hMenu);

    [DllImport("user32.dll")]
    static extern bool RemoveMenu(IntPtr hMenu, uint uPosition, uint uFlags);
    #endregion

    #region Constants
    //private const int SC_SIZE = 0xF000;
    //private const int SC_MOVE = 0xF010;
    private const int SC_MINIMIZE = 0xF020;
    private const int SC_MAXIMIZE = 0xF030;
    private const int SC_RESTORE = 0xF120;
    private const int MF_BYCOMMAND = 0x0000;
    //private const int MF_BYPOSITION = 0x400;

    //private const int SC_NEXTWINDOW = 0xF040;
    //private const int SC_PREVWINDOW = 0xF050;
    //private const int SC_CLOSE = 0xF060;
    //private const int SC_VSCROLL = 0xF070;
    //private const int SC_HSCROLL = 0xF080;
    //private const int SC_MOUSEMENU = 0xF090;
    //private const int SC_KEYMENU = 0xF100;
    //private const int SC_ARRANGE = 0xF110;

    //private const int SC_TASKLIST = 0xF130;
    //private const int SC_SCREENSAVE = 0xF140;
    //private const int SC_HOTKEY = 0xF150;

    private const int WM_NCLBUTTONDBLCLK = 0xA3;
    #endregion

    #region Event Handlers
    public event EventHandler? NCLBUTTONDBLCLK;
    #endregion

    #region Identity

    public VisualFloatingContainerForm()
    {
        InitializeComponent();
    }

    #endregion

    #region Methods

    private void CalculateMinimumWidth()
    {
        if (_floatableMenuStrip != null)
        {
            foreach (ToolStripItem item in _floatableMenuStrip.Items)
            {
                if (item.Width > _minWidth)
                {
                    _minWidth = item.Width;
                }
            }

            _minWidth += _dFrameWidth + 3;

            _minWidth = _minWidth switch
            {
                < 46 => 48 + _dFrameWidth,
                _ => _minWidth
            };
        }
        else if (_floatableToolStrip != null)
        {
            foreach (ToolStripItem items in _floatableToolStrip.Items)
            {
                if (items.Width > _minWidth)
                {
                    _minWidth = items.Width;
                }
            }

            _minWidth += _dFrameWidth + 3;

            _minWidth = _minWidth switch
            {
                < 46 => 48 + _dFrameWidth,
                _ => _minWidth
            };
        }
    }

    private void FloatingContainerForm_FormClosing(object sender, FormClosingEventArgs e) => NCLBUTTONDBLCLK?.Invoke(this, EventArgs.Empty);

    #endregion

    #region Overrides

    /// <inheritdoc />
    protected override void OnResize(EventArgs e)
    {
        if (_floatableMenuStrip != null)
        {
            Height = _floatableMenuStrip.Size.Height + _dFrameWidth + _captionWidth;

            if (Width > _maxWidth)
            {
                Width = _maxWidth;
            }
            else if (Width < _minWidth + 23)
            {
                Width = _minWidth;
            }
        }
        else if (_floatableToolStrip != null)
        {
            Height = _floatableToolStrip.Size.Height + _dFrameWidth + _captionWidth;

            if (Width > _maxWidth)
            {
                Width = _maxWidth;
            }
            else if (Width < _minWidth + 23)
            {
                Width = _minWidth;
            }
        }

        base.OnResize(e);
    }

    /// <inheritdoc />
    protected override void OnPaint(PaintEventArgs e)
    {
        FormBorderStyle = _showWindowFrame ? FormBorderStyle.Fixed3D : FormBorderStyle.None;

        if (_floatableMenuStrip != null)
        {
            KryptonFloatableToolStrip = null;
        }
        else if (_floatableToolStrip != null)
        {
            KryptonFloatableMenuStrip = null;
        }

        base.OnPaint(e);
    }

    protected override void WndProc(ref Message m)
    {
        if (m.Msg == WM_NCLBUTTONDBLCLK)
        {
            if (NCLBUTTONDBLCLK != null)
            {
                NCLBUTTONDBLCLK(this, new());
            }
        }
        else
        {
            base.WndProc(ref m);
        }
    }

    #endregion
}
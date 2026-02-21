using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Krypton.Utilities;

public partial class VisualMenuStripContainerForm : KryptonForm
{
    #region Instance Fields

    private bool _showWindowFrame;

    private KryptonFloatableMenuStrip? _floatableMenuStrip;

    private readonly int _dFrameWidth = 8;
    
    private readonly int _captionWidth = 18;

    private int _maxWidth = 0;
    
    private int _minWidth = 0;

    #endregion

    #region Public

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool ShowWindowFrame
    {
        get => _showWindowFrame;

        set
        {
            _showWindowFrame = value;

            Invalidate();
        }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonFloatableMenuStrip? KryptonFloatableMenuStrip
    {
        get => _floatableMenuStrip;

        set
        {
            _floatableMenuStrip = value;

            if (_floatableMenuStrip != null)
            {
                Text = _floatableMenuStrip.FloatingWindowText;

                ControlBox = _floatableMenuStrip.ShowFloatingWindowControlBox;

                SuspendLayout();

                ((Control)_floatableMenuStrip).Dock = DockStyle.None;

                _floatableMenuStrip.LayoutStyle = ToolStripLayoutStyle.Flow;

                Controls.Add(_floatableMenuStrip);

                ResumeLayout();

                _maxWidth = _floatableMenuStrip.PreferredSize.Width + _dFrameWidth;

                CalculateMinimumWidth();

                Size = new Size(_maxWidth, _floatableMenuStrip.PreferredSize.Height + _dFrameWidth + _captionWidth);
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

    #region Constructor
    public VisualMenuStripContainerForm()
    {
        InitializeComponent();

        _dFrameWidth = Width - ClientSize.Width;

        _captionWidth = Height - ClientSize.Height - _dFrameWidth;

        IntPtr pm = GetSystemMenu(Handle, false);

        RemoveMenu(pm, SC_RESTORE, MF_BYCOMMAND);

        RemoveMenu(pm, SC_MINIMIZE, MF_BYCOMMAND);

        RemoveMenu(pm, SC_MAXIMIZE, MF_BYCOMMAND);
    }

    private void MenuStripContainerWindow_FormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = false;
    }

    private void MenuStripContainerWindow_FormClosed(object sender, FormClosedEventArgs e)
    {
        //e.CloseReason = CloseReason.None;
    }
    #endregion

    #region Methods
    private void CalculateMinimumWidth()
    {
        if (_floatableMenuStrip?.Items != null)
        {
            foreach (ToolStripItem items in _floatableMenuStrip?.Items!)
            {
                if (items.Width > _minWidth)
                {
                    _minWidth = items.Width;
                }
            }
        }

        _minWidth += _dFrameWidth + 3;

        if (_minWidth < 46)
        {
            _minWidth = 48 + _dFrameWidth;
        }
    }
    #endregion

    #region Overrides
    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);

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
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        // Apply custom painting if theme has custom painter
        if (_floatableMenuStrip?.WindowTheme?.CustomPainter != null)
        {
            var painter = _floatableMenuStrip.WindowTheme.CustomPainter;
            painter.PaintBackground(e, ClientRectangle);

            // Paint title bar if needed
            if (_floatableMenuStrip.WindowTheme.TitleBarColor != Color.Empty)
            {
                Rectangle titleRect = new Rectangle(0, 0, Width, _captionWidth);
                painter.PaintTitleBar(e, titleRect, Text);
            }

            // Paint border
            painter.PaintBorder(e, ClientRectangle);
        }
        else
        {
            // Default painting
            if (_showWindowFrame)
            {
                FormBorderStyle = FormBorderStyle.Fixed3D;
            }
            else
            {
                FormBorderStyle = FormBorderStyle.None;
            }
        }

        base.OnPaint(e);
    }

    protected override void OnPaintBackground(PaintEventArgs e)
    {
        // Let custom painter handle background if available
        if (_floatableMenuStrip?.WindowTheme?.CustomPainter == null)
        {
            base.OnPaintBackground(e);
        }
    }

    protected override void WndProc(ref Message m)
    {
        if (m.Msg == WM_NCLBUTTONDBLCLK)
        {
            if (NCLBUTTONDBLCLK != null)
            {
                NCLBUTTONDBLCLK(this, new EventArgs());
            }
        }
        else
        {
            base.WndProc(ref m);
        }
    }
    #endregion
}
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

[ToolboxBitmap(typeof(VScrollBar))]
[ToolboxItem(false)]
public class VScrollSkin : Panel
{
    #region "   Members   "
    //private static List<WeakReference> __ENCList = new List<WeakReference>();

    [AccessedThroughProperty(nameof(_win))]
    private Control __win;

    [AccessedThroughProperty(nameof(VScrollBar1))]
    private KryptonScrollBar _vScrollBar1;

    [AccessedThroughProperty(nameof(HScrollBar1))]
    private KryptonScrollBar _hScrollBar1;

    private readonly IContainer components;

    public WIN32ScrollBars.ScrollInfo si;

    private VScrollBar VSB;
    private HScrollBar HSC;

    private static PaletteBase? _palette;
    private readonly PaletteRedirect _paletteRedirect;

    #endregion

    #region "   Properties   "

    private Control _win
    {
        get => __win;
        set => __win = value;
    }

    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden)]
    internal virtual KryptonScrollBar VScrollBar1
    {
        get => _vScrollBar1;
        set => _vScrollBar1 = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal virtual KryptonScrollBar HScrollBar1
    {
        get => _hScrollBar1;
        set => _hScrollBar1 = value;
    }
    #endregion

    #region "   CTor   "
    public VScrollSkin()
    {
        // add Palette Handler
        if (_palette != null)
        {
            _palette.PalettePaint += OnPalettePaint;
        }

        KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;

        _palette = KryptonManager.CurrentGlobalPalette;
        _paletteRedirect = new PaletteRedirect(_palette);

        ControlAdded += scrollSkin_ControlAdded;
        //_win = null;

        // This call is required by the Windows Form Designer.
        InitializeComponent();

    }

    public VScrollSkin(Control win)
    {

        // add Palette Handler
        if (_palette != null)
        {
            _palette.PalettePaint += OnPalettePaint;
        }

        KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;

        _palette = KryptonManager.CurrentGlobalPalette;
        _paletteRedirect = new PaletteRedirect(_palette);

        ControlAdded += scrollSkin_ControlAdded;

        _win = win;
        Controls.Add(win);

        // This call is required by the Windows Form Designer.
        InitializeComponent();

        // Fix the fake scrolling control to overlap the real scrollable control
        VScrollBar1.Size = new Size(0x12, _win.Height);
        HScrollBar1.Size = new Size(_win.Width, 0x12);

        Size = new Size(_win.Width, _win.Height);
        Location = new Point(_win.Left, _win.Top);
        Dock = _win.Dock;
        _win.Top = 0;
        _win.Left = 0;
        _win.SendToBack();
        Name = $"skin{_win.Name}";
    }

    #endregion

    #region "   Init   "
    private void InitializeComponent()
    {
        VScrollBar1 = new KryptonScrollBar();
        HScrollBar1 = new KryptonScrollBar();

        SuspendLayout();

        //VScrollBar1.Anchor = AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Top;
        VScrollBar1.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right); VScrollBar1.Dock = DockStyle.Right;
        VScrollBar1.LargeChange = 10;
        VScrollBar1.Location = new Point(0x91, 0); // (145,0)
        VScrollBar1.Maximum = 100;
        VScrollBar1.Minimum = 0;
        VScrollBar1.MinimumSize = new Size(0x13, 15); //(19,15)
        VScrollBar1.Name = nameof(VScrollBar1);
        VScrollBar1.Size = new Size(0x13, 0x7f);//(19,127)
        VScrollBar1.SmallChange = 1;
        VScrollBar1.TabIndex = 0;
        VScrollBar1.Scroll += VScrollBar1_miScroll;
        VScrollBar1.Visible = false;
        VScrollBar1.Orientation = ScrollBarOrientation.Vertical;

        HScrollBar1.Dock = DockStyle.Bottom;
        HScrollBar1.LargeChange = 10;
        HScrollBar1.Location = new Point(0, 0x6c);//(0,108)
        HScrollBar1.Maximum = 20;
        HScrollBar1.Minimum = 0;
        HScrollBar1.MinimumSize = new Size(15, 0x13);//(15,19)
        HScrollBar1.Name = nameof(HScrollBar1);
        HScrollBar1.Size = new Size(0x96, 15);//(150,15)
        HScrollBar1.SmallChange = 1;
        HScrollBar1.TabIndex = 1;
        HScrollBar1.Scroll += HScrollBar1_miScroll;
        HScrollBar1.Visible = false;
        HScrollBar1.Orientation = ScrollBarOrientation.Horizontal;

        BackColor = Color.Transparent;

        Controls.Add(VScrollBar1);
        //Controls.Add(HScrollBar1);

        Size = new Size(0xa4, 0x7f); //(164,127)
        ResumeLayout(false);

        if (_win != null)
        {
            __win.Resize += win_Resize;
        }

    }
    #endregion

    #region "   Control Added   "
    /// <summary>
    /// Linking the Scrollable control with Me
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <remarks></remarks>
    private void scrollSkin_ControlAdded(object? sender, ControlEventArgs e)
    {
        if (e is not null
            && e.Control is not null
            && Controls.Count != 1
            && _win == null)
        {
            _win = e.Control;
            if (_win!.GetType() == typeof(DataGridView) || (_win.GetType() == typeof(KryptonDataGridView)))
            {
                var dgv = (DataGridView)_win;
                dgv.Scroll += dgv_Scroll;

                foreach (Control control in dgv.Controls)
                {
                    switch (control)
                    {
                        case HScrollBar hscroll:
                            hscroll.VisibleChanged += HorizontalScrollBar_VisibleChanged;
                            break;
                        case VScrollBar vscroll:
                            vscroll.VisibleChanged += VerticalScrollBar_VisibleChanged;
                            break;
                    }
                }
            }
        }
    }
    #endregion

    #region "   Vertical Scroll   "

    /// <summary>
    /// Comming from the customControl
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <remarks></remarks>
    private void VScrollBar1_miScroll(object? sender, ScrollEventArgs e)
    {
        if (_win.GetType() == typeof(ListView))
        {
            var listView1 = (ListView)_win;

            var min = IntPtr.Zero;
            var max = IntPtr.Zero;
            PI.GetScrollRange(listView1.Handle, PI.SB_.VERT, ref min, ref max);

            var nMax = max.ToInt32();
            nMax += 3;

            var nHeight = listView1.DisplayRectangle.Height;
            var itemRectHeight = listView1.GetItemRect(0).Height;

            var nTimes = (nHeight - 17) / itemRectHeight;
            var nScrollPositions = nMax - nTimes + 1;

            double nThePos = VScrollBar1.Maximum / nScrollPositions;

            double RealPos;
            if (nThePos <= 0.0)
            {
                RealPos = VScrollBar1.Value;
            }
            else
            {
                RealPos = VScrollBar1.Value / nThePos;
            }

            var nPos = PI.GetScrollPos(listView1.Handle, PI.SB_.VERT);

            var nShouldBeAt = RealPos * itemRectHeight;
            double nIsAt = nPos * itemRectHeight;

            var pixelsToScroll = Convert.ToInt32(nShouldBeAt - nIsAt);

            PI.SendMessage(listView1.Handle, PI.LVM_SCROLL, IntPtr.Zero, (IntPtr)pixelsToScroll);

            Invalidate();

        }
        else
        {
            if (_win.GetType() == typeof(DataGridView) || (_win.GetType() == typeof(KryptonDataGridView)))
            {
                var dgv = (DataGridView)_win;
                if (GetDGVScrollbar(ref dgv, out VSB))
                {
                    foreach (Control control in dgv.Controls)
                    {
                        if (control is not VScrollBar { Visible: true })
                        {
                            continue;
                        }

                        switch (e.Type)
                        {
                            case ScrollEventType.ThumbTrack:
                                if (e.NewValue >= e.OldValue)
                                {
                                    PI.SendMessage(_win.Handle, PI.WM_.VSCROLL, (IntPtr)PI.SB_.LINEDOWN,
                                        VSB.Handle);
                                }
                                else
                                {
                                    PI.SendMessage(_win.Handle, PI.WM_.VSCROLL, (IntPtr)PI.SB_.LINEUP,
                                        VSB.Handle);
                                }

                                PI.SendMessage(_win.Handle, PI.WM_.VSCROLL, (IntPtr)PI.SB_.THUMBTRACK,
                                    VSB.Handle);
                                break;

                            default:
                                PI.SendMessage(_win.Handle, PI.WM_.VSCROLL, (IntPtr)e.Type, VSB.Handle);
                                break;
                        }
                    }


                }
                else
                {
                    VScrollBar1.Visible = false;
                }
            }
            else
            {
                if (_win.GetType() == typeof(TreeView) || (_win.GetType() == typeof(KryptonTreeView)))
                {
                    switch (e.Type)
                    {
                        case ScrollEventType.ThumbTrack:
                            if (e.NewValue >= e.OldValue)
                            {
                                PI.SendMessage(_win.Handle, PI.WM_.VSCROLL, (IntPtr)PI.SB_.LINEDOWN, IntPtr.Zero);
                            }
                            else
                            {
                                PI.SendMessage(_win.Handle, PI.WM_.VSCROLL, (IntPtr)PI.SB_.LINEUP, IntPtr.Zero);
                            }

                            PI.SendMessage(_win.Handle, PI.WM_.VSCROLL, (IntPtr)PI.SB_.THUMBTRACK, IntPtr.Zero);
                            break;

                        default:
                            PI.SendMessage(_win.Handle, PI.WM_.VSCROLL, (IntPtr)e.Type, IntPtr.Zero);
                            break;
                    }
                }
                else
                {
                    PI.PostMessage(_win.Handle, PI.WM_.VSCROLL, (IntPtr)(PI.SB_.THUMBPOSITION + (0x10000 * VScrollBar1.Value)), IntPtr.Zero);
                }
            }
        }

    }

    #endregion

    #region "   Horizontal Scroll   "
    private void HScrollBar1_miScroll(object? sender, ScrollEventArgs e)
    {
        if (_win.GetType() == typeof(ListView))
        {
            var listView1 = (ListView)_win;

            var nIsAt = PI.GetScrollPos(listView1.Handle, PI.SB_.HORZ);
            var nShouldBeAt = (int)e.NewValue;

            var pixelsToScroll = Convert.ToInt32(nShouldBeAt - nIsAt);

            PI.SendMessage(listView1.Handle, PI.LVM_SCROLL, (IntPtr)pixelsToScroll, IntPtr.Zero);

            Invalidate();
        }
        else
        {
            if (_win.GetType() == typeof(DataGridView) || (_win.GetType() == typeof(KryptonDataGridView)))
            {
                var dgv = (DataGridView)_win;
                if (GetDGHScrollbar(ref dgv, out HSC))
                {
                    foreach (Control control in dgv.Controls)
                    {
                        if (control is HScrollBar { Visible: true })
                        {
                            if (e.Type == ScrollEventType.ThumbTrack)
                            {
                                if (e.NewValue >= e.OldValue)
                                {
                                    PI.SendMessage(_win.Handle, PI.WM_.HSCROLL, (IntPtr)PI.SB_.LINEDOWN, HSC.Handle);
                                }
                                else
                                {
                                    PI.SendMessage(_win.Handle, PI.WM_.HSCROLL, (IntPtr)PI.SB_.LINEUP, HSC.Handle);
                                }

                                PI.SendMessage(_win.Handle, PI.WM_.HSCROLL, (IntPtr)PI.SB_.THUMBTRACK, HSC.Handle);
                            }
                            else
                            {
                                PI.SendMessage(_win.Handle, PI.WM_.HSCROLL, (IntPtr)e.Type, HSC.Handle);
                            }
                        }
                    }
                }
                else
                {
                    HScrollBar1.Visible = false;
                }
            }
            else
            {
                if (_win.GetType() == typeof(TreeView) || (_win.GetType() == typeof(KryptonTreeView)))
                {
                    if (e.Type == ScrollEventType.ThumbTrack)
                    {
                        if (e.NewValue >= e.OldValue)
                        {
                            PI.SendMessage(_win.Handle, PI.WM_.HSCROLL, (IntPtr)PI.SB_.LINEDOWN, IntPtr.Zero);
                        }
                        else
                        {
                            PI.SendMessage(_win.Handle, PI.WM_.HSCROLL, (IntPtr)PI.SB_.LINEUP, IntPtr.Zero);
                        }

                        PI.SendMessage(_win.Handle, PI.WM_.HSCROLL, (IntPtr)PI.SB_.THUMBTRACK, IntPtr.Zero);
                    }
                    else
                    {
                        PI.SendMessage(_win.Handle, PI.WM_.HSCROLL, (IntPtr)e.Type, IntPtr.Zero);
                    }
                }
                else
                {
                    PI.PostMessage(_win.Handle, PI.WM_.HSCROLL, (IntPtr)(PI.SB_.THUMBPOSITION + (0x10000 * HScrollBar1.Value)), IntPtr.Zero);
                }
            }
        }

    }

    #endregion

    #region "   DGV Scrollbar VisibleChanged    "

    private void VerticalScrollBar_VisibleChanged(object? sender, EventArgs e)
    {
        var vscroll = sender as VScrollBar ?? throw new ArgumentNullException(nameof(sender));
        _vScrollBar1.Visible = vscroll.Visible;
    }

    private void HorizontalScrollBar_VisibleChanged(object? sender, EventArgs e)
    {
        var hscroll = sender as HScrollBar ?? throw new ArgumentNullException(nameof(sender));
        _hScrollBar1.Visible = hscroll.Visible;
    }

    #endregion

    #region "   DGV Scroll   "

    private void dgv_Scroll(object? sender, ScrollEventArgs e)
    {
        var dgv = (DataGridView)_win;
        if (GetDGVScrollbar(ref dgv, out VSB))
        {
            if (VSB.Visible)
            {
                VScrollBar1.Visible = true;
                SetDGVScrollBarValue(ref dgv, ref VSB);
            }
            else
            {
                VScrollBar1.Visible = false;
            }
        }

        if (GetDGHScrollbar(ref dgv, out HSC))
        {
            if (HSC.Visible)
            {
                HScrollBar1.Visible = true;
                SetDGVScrollBarValue(ref dgv, ref HSC);
            }
            else
            {
                HScrollBar1.Visible = false;
            }
        }
        dgv.Invalidate();
        dgv.Refresh();
        dgv.PerformLayout();
    }

    #endregion

    #region "   Win Resize   "

    /// <summary>
    /// Almost done move and resize the Scrollable control over Me 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <remarks></remarks>
    private void win_Resize(object? sender, EventArgs e)
    {
        VScrollBar1.Size = new Size(0x12, _win.Height); //for the gap 
        VScrollBar1.Left = _win.Right - 0x12;

        //test code
        HScrollBar1.Size = new Size(_win.Width, 0x12); //for the gap
        HScrollBar1.Top = _win.Height - 0x12;

        Size = new Size(_win.Width, _win.Height);

        _win.Top = 0;
        _win.Left = 0;
    }

    #endregion

    #region "   WndProc   "
    /// <summary>
    /// Overrided to controll del scrolling of the customControl VScrollBar1
    /// </summary>
    /// <param name="m"></param>
    /// <remarks></remarks>
    protected override void WndProc(ref Message m)
    {
        if (!DesignMode && !(!Parent!.CanFocus | (_win == null)))
        {

            //int listStyle = WIN32ScrollBars.GetWindowLong(_win.Handle, WIN32ScrollBars.GWL_STYLE);
            //listStyle |= WIN32ScrollBars.WS_VSCROLL | WIN32ScrollBars.WS_HSCROLL;
            //listStyle |= WIN32ScrollBars.WS_VSCROLL;
            //listStyle |= WIN32ScrollBars.WS_HSCROLL;
            //listStyle = WIN32ScrollBars.SetWindowLong(_win.Handle, WIN32ScrollBars.GWL_STYLE, listStyle);

            var wndStyle = PI.GetWindowLong(_win!.Handle, PI.GWL_.STYLE);
            var hsVisible = (wndStyle & PI.WS_.HSCROLL) != 0;
            var vsVisible = (wndStyle & PI.WS_.VSCROLL) != 0;

            //Vertical
            if (vsVisible)
            {
                si.fMask = (int)PI.SIF_.ALL;
                si.cbSize = Marshal.SizeOf(si);
                PI.GetScrollInfo(_win.Handle, PI.SB_.VERT, ref si);

                if ((si.nMax + 0) <= si.nPage)
                {
                    VScrollBar1.Visible = false;
                }
                else
                {
                    VScrollBar1.Visible = true;
                    if (si.nPage != 0)
                    {
                        VScrollBar1.LargeChange = si.nPage;
                        VScrollBar1.Maximum = si.nMax;
                        VScrollBar1.Minimum = si.nMin;
                        VScrollBar1.SmallChange = 1;
                        VScrollBar1.Update();//.SyncThumbPositionWithLogicalValue();
                    }
                }
            }

            //horizontal
            if (hsVisible)
            {
                si.fMask = (int)PI.SIF_.ALL;
                si.cbSize = Marshal.SizeOf(si);
                PI.GetScrollInfo(_win.Handle, PI.SB_.HORZ, ref si);

                if ((si.nMax + 0) <= si.nPage)
                {
                    HScrollBar1.Visible = false;
                }
                else
                {
                    HScrollBar1.Visible = true;
                    if (si.nPage != 0)
                    {
                        HScrollBar1.LargeChange = si.nPage;
                        HScrollBar1.Maximum = si.nMax;
                        HScrollBar1.Minimum = si.nMin;
                        HScrollBar1.SmallChange = 1;
                        HScrollBar1.Update();//.SyncThumbPositionWithLogicalValue();
                    }
                }
            }

            if (_win.GetType() == typeof(ListView))
            {
                var listView1 = (ListView)_win;

                var si = new WIN32ScrollBars.ScrollInfo();
                si.cbSize = Marshal.SizeOf(si);
                si.fMask = (int)PI.SIF_.ALL;
                if (PI.GetScrollInfo(listView1.Handle, PI.SB_.VERT, ref si))
                {
                    VScrollBar1.LargeChange = si.nPage;
                    VScrollBar1.Maximum = si.nMax;
                    VScrollBar1.Minimum = si.nMin;
                    VScrollBar1.SmallChange = 1;
                    VScrollBar1.Update();//.SyncThumbPositionWithLogicalValue();
                }


                si = new WIN32ScrollBars.ScrollInfo();
                si.cbSize = Marshal.SizeOf(si);
                si.fMask = (int)PI.SIF_.ALL;
                if (PI.GetScrollInfo(listView1.Handle, PI.SB_.HORZ, ref si))
                {
                    HScrollBar1.LargeChange = si.nPage;
                    HScrollBar1.Maximum = si.nMax;
                    HScrollBar1.Minimum = si.nMin;
                    HScrollBar1.SmallChange = 1;
                    HScrollBar1.Update();//.SyncThumbPositionWithLogicalValue();
                }
            }
        }

        base.WndProc(ref m);
    }
    #endregion

    #region "   Support Subs   "

    public static bool GetDGVScrollbar(ref DataGridView dgv, out VScrollBar VSB)
    {
        var isPresent = false;
        VSB = new VScrollBar();

        foreach (Control ctr in dgv.Controls)
        {
            if (ctr is VScrollBar bar)
            {
                VSB = bar;
                isPresent = true;
            }
        }
        return isPresent;
    }

    public void SetDGVScrollBarValue(ref DataGridView dgv, ref VScrollBar VSB)
    {
        var listStyle = PI.GetWindowLong(dgv.Handle, PI.GWL_.STYLE);
        //listStyle |= WIN32ScrollBars.WS_VSCROLL | WIN32ScrollBars.WS_HSCROLL;
        listStyle |= PI.WS_.VSCROLL;
        _ = PI.SetWindowLong(dgv.Handle, PI.GWL_.STYLE, listStyle);

        VScrollBar1.Value = VSB.Value;
        VScrollBar1.Visible = true;
        VScrollBar1.LargeChange = VSB.LargeChange;
        VScrollBar1.Maximum = VSB.Maximum;
        VScrollBar1.Minimum = VSB.Minimum;
        VScrollBar1.SmallChange = VSB.SmallChange;
        VScrollBar1.Update();//.SyncThumbPositionWithLogicalValue();
        //Invalidate();
    }

    public static bool GetDGHScrollbar(ref DataGridView dgv, out HScrollBar HSB)
    {
        var isPresent = false;
        HSB = new HScrollBar();

        foreach (Control ctr in dgv.Controls)
        {
            if (ctr is HScrollBar bar)
            {
                HSB = bar;
                isPresent = true;
            }
        }
        return isPresent;
    }

    public void SetDGVScrollBarValue(ref DataGridView dgv, ref HScrollBar HSB)
    {
        var listStyle = PI.GetWindowLong(dgv.Handle, PI.GWL_.STYLE);
        //listStyle |= WIN32ScrollBars.WS_VSCROLL | WIN32ScrollBars.WS_HSCROLL;
        listStyle |= PI.WS_.HSCROLL;
        _ = PI.SetWindowLong(dgv.Handle, PI.GWL_.STYLE, listStyle);

        HScrollBar1.Value = HSB.Value;
        HScrollBar1.Visible = true;
        HScrollBar1.LargeChange = HSB.LargeChange;
        HScrollBar1.Maximum = HSB.Maximum;
        HScrollBar1.Minimum = HSB.Minimum;
        HScrollBar1.SmallChange = HSB.SmallChange;
        HScrollBar1.Update();//.SyncThumbPositionWithLogicalValue();
        //Invalidate();
    }
    protected static ScrollBars GetVisibleScrollbars(Control ctl)
    {
        var wndStyle = PI.GetWindowLong(ctl.Handle, PI.GWL_.STYLE);
        var hsVisible = (wndStyle & PI.WS_.HSCROLL) != 0;
        var vsVisible = (wndStyle & PI.WS_.VSCROLL) != 0;

        return hsVisible
            ? vsVisible
                ? ScrollBars.Both
                : ScrollBars.Horizontal
            : vsVisible
                ? ScrollBars.Vertical
                : ScrollBars.None;
    }


    #endregion

    #region "   (Designer) Dispose   "
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        if (disposing)
        {
            if (_palette != null)
            {
                _palette.PalettePaint -= OnPalettePaint;
                _palette = null;
            }

            KryptonManager.GlobalPaletteChanged -= OnGlobalPaletteChanged;
        }
        base.Dispose(disposing);
    }

    #endregion

    #region ... Krypton ...


    //Krypton Palette Events
    private void OnGlobalPaletteChanged(object? sender, EventArgs e)
    {
        if (_palette != null)
        {
            _palette.PalettePaint -= OnPalettePaint;
        }

        _palette = KryptonManager.CurrentGlobalPalette;
        _paletteRedirect.Target = _palette;

        if (_palette != null)
        {
            _palette.PalettePaint += OnPalettePaint;
        }

        Invalidate();
    }

    //Krypton Palette Events
    private void OnPalettePaint(object? sender, PaletteLayoutEventArgs e) => Invalidate();

    #endregion
}
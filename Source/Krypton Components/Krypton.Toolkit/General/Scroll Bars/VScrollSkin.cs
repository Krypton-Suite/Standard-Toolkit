#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    [ToolboxBitmap(typeof(VScrollBar))]
    [ToolboxItem(false)]
    public class VScrollSkin : Panel
    {
        #region "   Members   "
        //private static List<WeakReference> __ENCList = new List<WeakReference>();

        [AccessedThroughProperty("_win")]
        private Control __win;

        [AccessedThroughProperty("VScrollBar1")]
        private KryptonScrollBar _VScrollBar1;

        [AccessedThroughProperty("HScrollBar1")]
        private KryptonScrollBar _HScrollBar1;

        private IContainer components;

        public WIN32ScrollBars.ScrollInfo si;
        //public WIN32ScrollBars.ScrollInfo si2;

        private VScrollBar VSB;
        private HScrollBar HSC;

        private static IPalette _palette;
        private PaletteRedirect _paletteRedirect;

        #endregion

        #region "   Properties   "

        private Control _win
        {
            get
            {
                return __win;
            }
            set
            {
                __win = value;
            }
        }

        internal virtual KryptonScrollBar VScrollBar1
        {
            get
            {
                return _VScrollBar1;
            }
            set
            {
                _VScrollBar1 = value;
            }
        }

        internal virtual KryptonScrollBar HScrollBar1
        {
            get
            {
                return _HScrollBar1;
            }
            set
            {
                _HScrollBar1 = value;
            }
        }
        #endregion

        #region "   CTor   "
        public VScrollSkin()
        {
            // add Palette Handler
            if (_palette != null)
                _palette.PalettePaint += new EventHandler<PaletteLayoutEventArgs>(OnPalettePaint);

            KryptonManager.GlobalPaletteChanged += new EventHandler(OnGlobalPaletteChanged);

            _palette = KryptonManager.CurrentGlobalPalette;
            _paletteRedirect = new PaletteRedirect(_palette);

            base.ControlAdded += new ControlEventHandler(scrollSkin_ControlAdded);
            //_win = null;

            // This call is required by the Windows Form Designer.
            InitializeComponent();

        }

        public VScrollSkin(Control win)
        {

            // add Palette Handler
            if (_palette != null)
                _palette.PalettePaint += new EventHandler<PaletteLayoutEventArgs>(OnPalettePaint);

            KryptonManager.GlobalPaletteChanged += new EventHandler(OnGlobalPaletteChanged);

            _palette = KryptonManager.CurrentGlobalPalette;
            _paletteRedirect = new PaletteRedirect(_palette);

            base.ControlAdded += new ControlEventHandler(scrollSkin_ControlAdded);

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
            Name = "skin" + _win.Name;
        }

        #endregion

        #region "   Init   "
        private void InitializeComponent()
        {
            VScrollBar1 = new KryptonScrollBar();
            HScrollBar1 = new KryptonScrollBar();

            SuspendLayout();

            //VScrollBar1.Anchor = AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Top;
            VScrollBar1.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Right))); VScrollBar1.Dock = DockStyle.Right;
            VScrollBar1.LargeChange = 10;
            VScrollBar1.Location = new Point(0x91, 0); // (145,0)
            VScrollBar1.Maximum = 100;
            VScrollBar1.Minimum = 0;
            VScrollBar1.MinimumSize = new Size(0x13, 15); //(19,15)
            VScrollBar1.Name = "VScrollBar1";
            VScrollBar1.Size = new Size(0x13, 0x7f);//(19,127)
            VScrollBar1.SmallChange = 1;
            VScrollBar1.TabIndex = 0;
            VScrollBar1.Scroll += VScrollBar1_miScroll;
            VScrollBar1.Visible = false;
            VScrollBar1.Orientation = ScrollBarOrientation.VERTICAL;

            HScrollBar1.Dock = DockStyle.Bottom;
            HScrollBar1.LargeChange = 10;
            HScrollBar1.Location = new Point(0, 0x6c);//(0,108)
            HScrollBar1.Maximum = 20;
            HScrollBar1.Minimum = 0;
            HScrollBar1.MinimumSize = new Size(15, 0x13);//(15,19)
            HScrollBar1.Name = "HScrollBar1";
            HScrollBar1.Size = new Size(0x96, 15);//(150,15)
            HScrollBar1.SmallChange = 1;
            HScrollBar1.TabIndex = 1;
            HScrollBar1.Scroll += HScrollBar1_miScroll;
            HScrollBar1.Visible = false;
            HScrollBar1.Orientation = ScrollBarOrientation.HORIZONTAL;

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
        private void scrollSkin_ControlAdded(object sender, ControlEventArgs e)
        {
            if ((Controls.Count != 1) && (_win == null))
            {
                _win = e.Control;
                if (_win.GetType() == typeof(DataGridView) || (_win.GetType() == typeof(KryptonDataGridView)))
                {
                    DataGridView dgv = (DataGridView)_win;
                    dgv.Scroll += dgv_Scroll;

                    foreach (Control control in dgv.Controls)
                    {
                        if (control is HScrollBar)
                        {
                            HScrollBar hscroll = (HScrollBar)control;
                            hscroll.VisibleChanged += new EventHandler(HorizontalScrollBar_VisibleChanged);
                        }
                        if (control is VScrollBar)
                        {
                            VScrollBar vscroll = (VScrollBar)control;
                            vscroll.VisibleChanged += new EventHandler(VerticalScrollBar_VisibleChanged);
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
        private void VScrollBar1_miScroll(object sender, ScrollEventArgs e)
        {
            if (_win.GetType() == typeof(ListView))
            {
                ListView listView1 = (ListView)_win;

                IntPtr min = IntPtr.Zero;
                IntPtr max = IntPtr.Zero;
                WIN32ScrollBars.GetScrollRange(listView1.Handle, WIN32ScrollBars.SB_VERT, ref min, ref max);

                int nMax = max.ToInt32();
                nMax += 3;

                int nHeight = listView1.DisplayRectangle.Height;
                int ItemRectHeight = listView1.GetItemRect(0).Height;

                int nTimes = (nHeight - 17) / ItemRectHeight;
                int nScrollPositions = (nMax - nTimes) + 1;

                double nThePos = VScrollBar1.Maximum / nScrollPositions;

                double RealPos = 0.0;
                if (nThePos <= 0.0)
                    RealPos = VScrollBar1.Value;
                else
                    RealPos = VScrollBar1.Value / nThePos;

                int nPos = WIN32ScrollBars.GetScrollPos(listView1.Handle, WIN32ScrollBars.SB_VERT);

                double nShouldBeAt = RealPos * ItemRectHeight;
                double nIsAt = nPos * ItemRectHeight;

                int pixelsToScroll = Convert.ToInt32((nShouldBeAt - nIsAt));

                WIN32ScrollBars.SendMessage(listView1.Handle, WIN32ScrollBars.LVM_SCROLL, IntPtr.Zero, (IntPtr)pixelsToScroll);

                Invalidate();

            }
            else
            {
                if (_win.GetType() == typeof(DataGridView) || (_win.GetType() == typeof(KryptonDataGridView)))
                {
                    DataGridView dgv = (DataGridView)_win;
                    if (GetDGVScrollbar(ref dgv, out VSB))
                    {
                        foreach (Control control in dgv.Controls)
                        {
                            if (control is VScrollBar)
                            {
                                VScrollBar vscroll = (VScrollBar)control;
                                if (vscroll.Visible)
                                {
                                    switch (e.Type)
                                    {
                                        case ScrollEventType.ThumbTrack:
                                            if (e.NewValue >= e.OldValue)
                                            {
                                                WIN32ScrollBars.SendMessage(_win.Handle, WIN32ScrollBars.WM_VSCROLL, (IntPtr)WIN32ScrollBars.SB_LINEDOWN, VSB.Handle);
                                            }
                                            else
                                            {
                                                WIN32ScrollBars.SendMessage(_win.Handle, WIN32ScrollBars.WM_VSCROLL, (IntPtr)WIN32ScrollBars.SB_LINEUP, VSB.Handle);
                                            }

                                            WIN32ScrollBars.SendMessage(_win.Handle, WIN32ScrollBars.WM_VSCROLL, (IntPtr)WIN32ScrollBars.SB_THUMBTRACK, VSB.Handle);
                                            break;

                                        default:
                                            WIN32ScrollBars.SendMessage(_win.Handle, WIN32ScrollBars.WM_VSCROLL, (IntPtr)e.Type, VSB.Handle);
                                            break;
                                    }

                                }
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
                    if (_win.GetType() == typeof(TreeView) || (_win.GetType() == typeof(Krypton.Toolkit.KryptonTreeView)))
                    {
                        switch (e.Type)
                        {
                            case ScrollEventType.ThumbTrack:
                                if (e.NewValue >= e.OldValue)
                                {
                                    WIN32ScrollBars.SendMessage(_win.Handle, WIN32ScrollBars.WM_VSCROLL, (IntPtr)WIN32ScrollBars.SB_LINEDOWN, IntPtr.Zero);
                                }
                                else
                                {
                                    WIN32ScrollBars.SendMessage(_win.Handle, WIN32ScrollBars.WM_VSCROLL, (IntPtr)WIN32ScrollBars.SB_LINEUP, IntPtr.Zero);
                                }

                                WIN32ScrollBars.SendMessage(_win.Handle, WIN32ScrollBars.WM_VSCROLL, (IntPtr)WIN32ScrollBars.SB_THUMBTRACK, IntPtr.Zero);
                                break;

                            default:
                                WIN32ScrollBars.SendMessage(_win.Handle, WIN32ScrollBars.WM_VSCROLL, (IntPtr)e.Type, IntPtr.Zero);
                                break;
                        }
                    }
                    else
                    {
                        WIN32ScrollBars.PostMessageA(_win.Handle, WIN32ScrollBars.WM_VSCROLL, WIN32ScrollBars.SB_THUMBPOSITION + (0x10000 * VScrollBar1.Value), 0);
                    }
                }
            }

        }

        #endregion

        #region "   Horizontal Scroll   "
        private void HScrollBar1_miScroll(object sender, ScrollEventArgs e)
        {
            if (_win.GetType() == typeof(ListView))
            {
                ListView listView1 = (ListView)_win;

                int nIsAt = WIN32ScrollBars.GetScrollPos(listView1.Handle, WIN32ScrollBars.SB_HORZ);
                int nShouldBeAt = (int)e.NewValue;

                int pixelsToScroll = Convert.ToInt32((nShouldBeAt - nIsAt));

                WIN32ScrollBars.SendMessage(listView1.Handle, (int)WIN32ScrollBars.LVM_SCROLL, pixelsToScroll, 0);

                Invalidate();
            }
            else
            {
                if (_win.GetType() == typeof(DataGridView) || (_win.GetType() == typeof(KryptonDataGridView)))
                {
                    DataGridView dgv = (DataGridView)_win;
                    if (GetDGHScrollbar(ref dgv, out HSC))
                    {
                        foreach (Control control in dgv.Controls)
                        {
                            if (control is HScrollBar)
                            {
                                HScrollBar hscroll = (HScrollBar)control;
                                if (hscroll.Visible)
                                {
                                    switch (e.Type)
                                    {
                                        case ScrollEventType.ThumbTrack:
                                            if (e.NewValue >= e.OldValue)
                                            {
                                                WIN32ScrollBars.SendMessage(_win.Handle, WIN32ScrollBars.WM_HSCROLL, (IntPtr)WIN32ScrollBars.SB_LINEDOWN, HSC.Handle);
                                            }
                                            else
                                            {
                                                WIN32ScrollBars.SendMessage(_win.Handle, WIN32ScrollBars.WM_HSCROLL, (IntPtr)WIN32ScrollBars.SB_LINEUP, HSC.Handle);
                                            }

                                            WIN32ScrollBars.SendMessage(_win.Handle, WIN32ScrollBars.WM_HSCROLL, (IntPtr)WIN32ScrollBars.SB_THUMBTRACK, HSC.Handle);
                                            break;

                                        default:
                                            WIN32ScrollBars.SendMessage(_win.Handle, WIN32ScrollBars.WM_HSCROLL, (IntPtr)e.Type, HSC.Handle);
                                            break;
                                    }

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
                    if (_win.GetType() == typeof(TreeView) || (_win.GetType() == typeof(Krypton.Toolkit.KryptonTreeView)))
                    {
                        switch (e.Type)
                        {
                            case ScrollEventType.ThumbTrack:
                                if (e.NewValue >= e.OldValue)
                                {
                                    WIN32ScrollBars.SendMessage(_win.Handle, WIN32ScrollBars.WM_HSCROLL, (IntPtr)WIN32ScrollBars.SB_LINEDOWN, IntPtr.Zero);
                                }
                                else
                                {
                                    WIN32ScrollBars.SendMessage(_win.Handle, WIN32ScrollBars.WM_HSCROLL, (IntPtr)WIN32ScrollBars.SB_LINEUP, IntPtr.Zero);
                                }

                                WIN32ScrollBars.SendMessage(_win.Handle, WIN32ScrollBars.WM_HSCROLL, (IntPtr)WIN32ScrollBars.SB_THUMBTRACK, IntPtr.Zero);
                                break;

                            default:
                                WIN32ScrollBars.SendMessage(_win.Handle, WIN32ScrollBars.WM_HSCROLL, (IntPtr)e.Type, IntPtr.Zero);
                                break;
                        }
                    }
                    else
                    {
                        WIN32ScrollBars.PostMessageA(_win.Handle, WIN32ScrollBars.WM_HSCROLL, WIN32ScrollBars.SB_THUMBPOSITION + (0x10000 * HScrollBar1.Value), 0);
                    }
                }
            }

        }

        #endregion

        #region "   DGV Scrollbar VisibleChanged    "

        private void VerticalScrollBar_VisibleChanged(object sender, EventArgs e)
        {
            VScrollBar vscroll = (VScrollBar)sender;
            if (vscroll.Visible)
            {
                _VScrollBar1.Visible = true;
            }
            else
            {
                _VScrollBar1.Visible = false;
            }
        }

        private void HorizontalScrollBar_VisibleChanged(object sender, EventArgs e)
        {
            HScrollBar hscroll = (HScrollBar)sender;
            if (hscroll.Visible)
            {
                _HScrollBar1.Visible = true;
            }
            else
            {
                _HScrollBar1.Visible = false;
            }
        }

        #endregion

        #region "   DGV Scroll   "

        private void dgv_Scroll(object sender, ScrollEventArgs e)
        {
            DataGridView dgv = (DataGridView)_win;
            if (GetDGVScrollbar(ref dgv, out VSB))
            {
                if (VSB.Visible == true)
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
                if (HSC.Visible == true)
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
        private void win_Resize(object sender, EventArgs e)
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
            if (!DesignMode && !(!Parent.CanFocus | (_win == null)))
            {

                //int listStyle = WIN32ScrollBars.GetWindowLong(_win.Handle, WIN32ScrollBars.GWL_STYLE);
                //listStyle |= WIN32ScrollBars.WS_VSCROLL | WIN32ScrollBars.WS_HSCROLL;
                //listStyle |= WIN32ScrollBars.WS_VSCROLL;
                //listStyle |= WIN32ScrollBars.WS_HSCROLL;
                //listStyle = WIN32ScrollBars.SetWindowLong(_win.Handle, WIN32ScrollBars.GWL_STYLE, listStyle);

                int wndStyle = WIN32ScrollBars.GetWindowLong(_win.Handle, WIN32ScrollBars.GWL_STYLE);
                bool hsVisible = (wndStyle & WIN32ScrollBars.WS_HSCROLL) != 0;
                bool vsVisible = (wndStyle & WIN32ScrollBars.WS_VSCROLL) != 0;

                //Vertical
                if (vsVisible)
                {
                    si.fMask = (int)WIN32ScrollBars.ScrollInfoMask.SIF_ALL;
                    si.cbSize = Marshal.SizeOf(si);
                    WIN32ScrollBars.GetScrollInfo(_win.Handle, (int)WIN32ScrollBars.ScrollBarDirection.SB_VERT, ref si);

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
                    si.fMask = (int)WIN32ScrollBars.ScrollInfoMask.SIF_ALL;
                    si.cbSize = Marshal.SizeOf(si);
                    WIN32ScrollBars.GetScrollInfo(_win.Handle, (int)WIN32ScrollBars.ScrollBarDirection.SB_HORZ, ref si);

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
                    ListView listView1 = (ListView)_win;

                    WIN32ScrollBars.ScrollInfo si = new WIN32ScrollBars.ScrollInfo();
                    si.cbSize = Marshal.SizeOf(si);
                    si.fMask = (int)WIN32ScrollBars.ScrollInfoMask.SIF_ALL;
                    if (WIN32ScrollBars.GetScrollInfo(listView1.Handle, (int)WIN32ScrollBars.ScrollBarDirection.SB_VERT, ref si))
                    {
                        VScrollBar1.LargeChange = si.nPage;
                        VScrollBar1.Maximum = si.nMax;
                        VScrollBar1.Minimum = si.nMin;
                        VScrollBar1.SmallChange = 1;
                        VScrollBar1.Update();//.SyncThumbPositionWithLogicalValue();
                    }


                    si = new WIN32ScrollBars.ScrollInfo();
                    si.cbSize = Marshal.SizeOf(si);
                    si.fMask = (int)WIN32ScrollBars.ScrollInfoMask.SIF_ALL;
                    if (WIN32ScrollBars.GetScrollInfo(listView1.Handle, (int)WIN32ScrollBars.ScrollBarDirection.SB_HORZ, ref si))
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
            bool isPresent = false;
            VSB = new VScrollBar();

            foreach (Control ctr in dgv.Controls)
            {
                if ((ctr) is VScrollBar)
                {
                    VSB = (VScrollBar)ctr;
                    isPresent = true;
                }
            }
            return isPresent;
        }

        public void SetDGVScrollBarValue(ref DataGridView dgv, ref VScrollBar VSB)
        {
            int listStyle = WIN32ScrollBars.GetWindowLong(dgv.Handle, WIN32ScrollBars.GWL_STYLE);
            //listStyle |= WIN32ScrollBars.WS_VSCROLL | WIN32ScrollBars.WS_HSCROLL;
            listStyle |= WIN32ScrollBars.WS_VSCROLL;
            listStyle = WIN32ScrollBars.SetWindowLong(dgv.Handle, WIN32ScrollBars.GWL_STYLE, listStyle);

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
            bool isPresent = false;
            HSB = new HScrollBar();

            foreach (Control ctr in dgv.Controls)
            {
                if ((ctr) is HScrollBar)
                {
                    HSB = (HScrollBar)ctr;
                    isPresent = true;
                }
            }
            return isPresent;
        }

        public void SetDGVScrollBarValue(ref DataGridView dgv, ref HScrollBar HSB)
        {
            int listStyle = WIN32ScrollBars.GetWindowLong(dgv.Handle, WIN32ScrollBars.GWL_STYLE);
            //listStyle |= WIN32ScrollBars.WS_VSCROLL | WIN32ScrollBars.WS_HSCROLL;
            listStyle |= WIN32ScrollBars.WS_HSCROLL;
            listStyle = WIN32ScrollBars.SetWindowLong(dgv.Handle, WIN32ScrollBars.GWL_STYLE, listStyle);

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
            int wndStyle = WIN32ScrollBars.GetWindowLong(ctl.Handle, WIN32ScrollBars.GWL_STYLE);
            bool hsVisible = (wndStyle & WIN32ScrollBars.WS_HSCROLL) != 0;
            bool vsVisible = (wndStyle & WIN32ScrollBars.WS_VSCROLL) != 0;

            if (hsVisible)
                return vsVisible ? ScrollBars.Both : ScrollBars.Horizontal;
            else
                return vsVisible ? ScrollBars.Vertical : ScrollBars.None;
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
                    _palette.PalettePaint -= new EventHandler<PaletteLayoutEventArgs>(OnPalettePaint);
                    _palette = null;
                }

                KryptonManager.GlobalPaletteChanged -= new EventHandler(OnGlobalPaletteChanged);
            }
            base.Dispose(disposing);
        }

        #endregion

        #region ... Krypton ...


        //Kripton Palette Events
        private void OnGlobalPaletteChanged(object sender, EventArgs e)
        {
            if (_palette != null)
                _palette.PalettePaint -= new EventHandler<PaletteLayoutEventArgs>(OnPalettePaint);

            _palette = KryptonManager.CurrentGlobalPalette;
            _paletteRedirect.Target = _palette;

            if (_palette != null)
            {
                _palette.PalettePaint += new EventHandler<PaletteLayoutEventArgs>(OnPalettePaint);
            }

            Invalidate();
        }

        //Kripton Palette Events
        private void OnPalettePaint(object sender, PaletteLayoutEventArgs e)
        {
            Invalidate();
        }

        #endregion
    }

}
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
    [ToolboxBitmap(typeof(HScrollBar))]
    [ToolboxItem(false)]
    public class HScrollSkin : Panel
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

        //public WIN32ScrollBars.ScrollInfo si;
        public WIN32ScrollBars.ScrollInfo si2;

        private HScrollBar HSB;

        private static IPalette _palette;
        private PaletteRedirect _paletteRedirect;
        #endregion

        #region "   Properties   "

        private Control _win
        {
            get
            {
                return this.__win;
            }
            set
            {
                this.__win = value;
            }
        }

        internal virtual KryptonScrollBar VScrollBar1
        {
            get
            {
                return this._VScrollBar1;
            }
            set
            {
                this._VScrollBar1 = value;
            }
        }

        internal virtual KryptonScrollBar HScrollBar1
        {
            get
            {
                return this._HScrollBar1;
            }
            set
            {
                this._HScrollBar1 = value;
            }
        }
        #endregion

        #region "   CTor   "
        public HScrollSkin()
        {
            // add Palette Handler
            if (_palette != null)
                _palette.PalettePaint += new EventHandler<PaletteLayoutEventArgs>(OnPalettePaint);

            KryptonManager.GlobalPaletteChanged += new EventHandler(OnGlobalPaletteChanged);

            _palette = KryptonManager.CurrentGlobalPalette;
            _paletteRedirect = new PaletteRedirect(_palette);

            base.ControlAdded += new ControlEventHandler(this.scrollSkin_ControlAdded);
            //this._win = null;

            // This call is required by the Windows Form Designer.
            this.InitializeComponent();


        }

        public HScrollSkin(Control win)
        {

            // add Palette Handler
            if (_palette != null)
                _palette.PalettePaint += new EventHandler<PaletteLayoutEventArgs>(OnPalettePaint);

            KryptonManager.GlobalPaletteChanged += new EventHandler(OnGlobalPaletteChanged);

            _palette = KryptonManager.CurrentGlobalPalette;
            _paletteRedirect = new PaletteRedirect(_palette);

            base.ControlAdded += new ControlEventHandler(this.scrollSkin_ControlAdded);

            this._win = win;
            this.Controls.Add(win);

            // This call is required by the Windows Form Designer.
            this.InitializeComponent();

            // Fix the fake scrolling control to overlap the real scrollable control
            this.VScrollBar1.Size = new Size(0x12, this._win.Height);
            this.HScrollBar1.Size = new Size(this._win.Width, 0x12);

            this.Size = new Size(this._win.Width, this._win.Height);
            this.Location = new Point(this._win.Left, this._win.Top);
            this.Dock = this._win.Dock;
            this._win.Top = 0;
            this._win.Left = 0;
            this._win.SendToBack();
            this.Name = "skin" + this._win.Name;
        }

        #endregion

        #region "   Init   "
        private void InitializeComponent()
        {
            this.VScrollBar1 = new KryptonScrollBar();
            this.HScrollBar1 = new KryptonScrollBar();

            this.SuspendLayout();

            //this.VScrollBar1.Anchor = AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Top;
            this.VScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Right))); this.VScrollBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.VScrollBar1.LargeChange = 10;
            this.VScrollBar1.Location = new Point(0x91, 0); // (145,0)
            this.VScrollBar1.Maximum = 20;
            this.VScrollBar1.Minimum = 0;
            this.VScrollBar1.MinimumSize = new Size(0x13, 15); //(19,15)
            this.VScrollBar1.Name = "VScrollBar1";
            this.VScrollBar1.Size = new Size(0x13, 0x7f);//(19,127)
            this.VScrollBar1.SmallChange = 1;
            this.VScrollBar1.TabIndex = 0;
            this.VScrollBar1.Scroll += VScrollBar1_miScroll;
            this.VScrollBar1.Visible = false;
            this.VScrollBar1.Orientation = ScrollBarOrientation.VERTICAL;


            //this.HScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.HScrollBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.HScrollBar1.LargeChange = 10;
            this.HScrollBar1.Location = new Point(0, 0x6c);//(0,108)
            this.HScrollBar1.Maximum = 20;
            this.HScrollBar1.Minimum = 0;
            this.HScrollBar1.MinimumSize = new Size(15, 0x13);//(15,19)
            this.HScrollBar1.Name = "HScrollBar1";
            this.HScrollBar1.Size = new Size(0x96, 15);//(150,15)
            this.HScrollBar1.SmallChange = 1;
            this.HScrollBar1.TabIndex = 1;
            this.HScrollBar1.Scroll += HScrollBar1_miScroll;
            this.HScrollBar1.Visible = false;
            this.HScrollBar1.Orientation = ScrollBarOrientation.HORIZONTAL;

            this.BackColor = Color.Transparent;

            //this.Controls.Add(this.VScrollBar1);
            this.Controls.Add(this.HScrollBar1);

            this.Size = new Size(0xa4, 0x7f); //(164,127)
            this.ResumeLayout(false);

            if (_win != null)
                this.__win.Resize += win_Resize;

        }
        #endregion

        #region "   Control Added   "
        /// <summary>
        /// Linking the Scrollable control with Me
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        public void scrollSkin_ControlAdded(object sender, ControlEventArgs e)
        {
            if ((this.Controls.Count != 1) && (_win == null))
            {
                this._win = e.Control;
                if (_win.GetType() == typeof(System.Windows.Forms.DataGridView) || (_win.GetType() == typeof(KryptonDataGridView)))
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
            WIN32ScrollBars.PostMessageA(this._win.Handle, WIN32ScrollBars.WM_HSCROLL, WIN32ScrollBars.SB_THUMBPOSITION + (0x10000 * this.VScrollBar1.Value), 0);
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
                if (_win.GetType() == typeof(System.Windows.Forms.DataGridView) || (_win.GetType() == typeof(KryptonDataGridView)))
                {
                    DataGridView dgv = (DataGridView)_win;
                    if (GetDGVScrollbar(ref dgv, out HSB))
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
                                                WIN32ScrollBars.SendMessage(_win.Handle, WIN32ScrollBars.WM_HSCROLL, (IntPtr)WIN32ScrollBars.SB_LINEDOWN, HSB.Handle);
                                            }
                                            else
                                            {
                                                WIN32ScrollBars.SendMessage(_win.Handle, WIN32ScrollBars.WM_HSCROLL, (IntPtr)WIN32ScrollBars.SB_LINEUP, HSB.Handle);
                                            }

                                            WIN32ScrollBars.SendMessage(_win.Handle, WIN32ScrollBars.WM_HSCROLL, (IntPtr)WIN32ScrollBars.SB_THUMBTRACK, HSB.Handle);
                                            break;

                                        default:
                                            WIN32ScrollBars.SendMessage(_win.Handle, WIN32ScrollBars.WM_HSCROLL, (IntPtr)e.Type, HSB.Handle);
                                            break;
                                    }

                                }
                            }
                        }


                    }
                    else
                    {
                        this.HScrollBar1.Visible = false;
                    }
                }
                else
                {
                    if (_win.GetType() == typeof(System.Windows.Forms.TreeView) || (_win.GetType() == typeof(Krypton.Toolkit.KryptonTreeView)))
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
                        WIN32ScrollBars.PostMessageA(this._win.Handle, WIN32ScrollBars.WM_HSCROLL, WIN32ScrollBars.SB_THUMBPOSITION + (0x10000 * this.HScrollBar1.Value), 0);
                    }
                }
            }

        }

        #endregion

        #region "   DGV Scrollbars VisibleChanged   "

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

        #endregion

        #region "   DGV Scroll   "

        private void dgv_Scroll(object sender, ScrollEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (GetDGVScrollbar(ref dgv, out HSB))
            {
                if (HSB.Visible == true)
                {
                    this.HScrollBar1.Visible = true;
                    SetDGVScrollBarValue(ref dgv, ref HSB);
                }
                else
                {
                    this.HScrollBar1.Visible = false;
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
            this.VScrollBar1.Size = new Size(0x12, this._win.Height); //for the gap 
            this.VScrollBar1.Left = this._win.Right - 0x12;

            //test code
            this.HScrollBar1.Size = new Size(this._win.Width, 0x12); //for the gap
            this.HScrollBar1.Top = this._win.Height - 0x12;

            this.Size = new Size(this._win.Width, this._win.Height);

            this._win.Top = 0;
            this._win.Left = 0;
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
            if (!this.DesignMode && !(!this.Parent.CanFocus | (_win == null)))
            {

                int wndStyle = WIN32ScrollBars.GetWindowLong(this._win.Handle, WIN32ScrollBars.GWL_STYLE);
                bool hsVisible = (wndStyle & WIN32ScrollBars.WS_HSCROLL) != 0;
                bool vsVisible = (wndStyle & WIN32ScrollBars.WS_VSCROLL) != 0;

                //horizontal
                if (hsVisible)
                {
                    this.si2.fMask = (int)WIN32ScrollBars.ScrollInfoMask.SIF_ALL;
                    this.si2.cbSize = Marshal.SizeOf(this.si2);
                    WIN32ScrollBars.GetScrollInfo(this._win.Handle, (int)WIN32ScrollBars.ScrollBarDirection.SB_HORZ, ref this.si2);

                    if ((this.si2.nMax + 0) <= this.si2.nPage)
                    {
                        this.HScrollBar1.Visible = false;
                    }
                    else
                    {
                        this.HScrollBar1.Visible = true;
                        if (this.si2.nPage != 0)
                        {
                            HScrollBar1.LargeChange = si2.nPage;
                            HScrollBar1.Maximum = si2.nMax;
                            HScrollBar1.Minimum = si2.nMin;
                            HScrollBar1.SmallChange = 1;
                            this.HScrollBar1.Update();//.SyncThumbPositionWithLogicalValue();
                        }
                    }
                }
                if (_win.GetType() == typeof(System.Windows.Forms.ListView))
                {
                    ListView listView1 = (ListView)_win;

                    WIN32ScrollBars.ScrollInfo si = new WIN32ScrollBars.ScrollInfo();
                    si.cbSize = Marshal.SizeOf(si);
                    si.fMask = (int)WIN32ScrollBars.ScrollInfoMask.SIF_ALL;
                    if (WIN32ScrollBars.GetScrollInfo(listView1.Handle, (int)WIN32ScrollBars.ScrollBarDirection.SB_HORZ, ref si))
                    {
                        HScrollBar1.LargeChange = si.nPage;
                        HScrollBar1.Maximum = si.nMax;
                        HScrollBar1.Minimum = si.nMin;
                        HScrollBar1.SmallChange = 1;
                        this.HScrollBar1.Update();//.SyncThumbPositionWithLogicalValue();
                    }
                }
                if (_win.GetType() == typeof(System.Windows.Forms.DataGridView) || (_win.GetType() == typeof(KryptonDataGridView)))
                {
                    WIN32ScrollBars.SendMessage(_win.Handle, WIN32ScrollBars.WM_HSCROLL, (IntPtr)WIN32ScrollBars.SB_TOP, IntPtr.Zero);
                    _win.Invalidate();
                }

            }
            base.WndProc(ref m);
        }

        #endregion

        #region "   Support Subs   "

        public static bool GetDGVScrollbar(ref DataGridView dgv, out HScrollBar HSB)
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

        public void SetDGVScrollBarValue(ref DataGridView dgv, ref HScrollBar HSC)
        {
            int listStyle = WIN32ScrollBars.GetWindowLong(dgv.Handle, WIN32ScrollBars.GWL_STYLE);
            //listStyle |= WIN32ScrollBars.WS_VSCROLL | WIN32ScrollBars.WS_HSCROLL;
            listStyle |= WIN32ScrollBars.WS_HSCROLL;
            listStyle = WIN32ScrollBars.SetWindowLong(dgv.Handle, WIN32ScrollBars.GWL_STYLE, listStyle);

            HScrollBar1.Value = HSC.Value;
            HScrollBar1.Visible = true;
            HScrollBar1.LargeChange = HSC.LargeChange;
            HScrollBar1.Maximum = HSC.Maximum;
            HScrollBar1.Minimum = HSC.Minimum;
            HScrollBar1.SmallChange = HSC.SmallChange;
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
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
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
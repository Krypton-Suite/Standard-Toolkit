#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2021 - 2025. All rights reserved.
 */
#endregion

using ListView = System.Windows.Forms.ListView;
// ReSharper disable UnusedMember.Global
#pragma warning disable 67

namespace Krypton.Toolkit;

/// <summary>
/// Provide a ListView with Krypton styling applied.
/// </summary>
/// <seealso cref="ListView" />
[ToolboxItem(true)]
[ToolboxBitmap(typeof(ListView))]
[Designer(typeof(KryptonListViewDesigner))]
[DesignerCategory(@"code")]
[Description(@"A Kryptonised listview.")]
public class KryptonListView : VisualControlBase,
    IContainedInputControl
{
    #region Classes
    private class InternalListView : ListView
    {
        #region Instance Fields

        private readonly ViewManager? _viewManager;
        private readonly KryptonListView _kryptonListView;
        private readonly IntPtr _screenDC;
        private bool _mouseOver;

        #endregion

        #region Events

        /// <summary>
        /// Occurs when the mouse enters the InternalListView.
        /// </summary>
        public event EventHandler? TrackMouseEnter;

        /// <summary>
        /// Occurs when the mouse leaves the InternalListView.
        /// </summary>
        public event EventHandler? TrackMouseLeave;

        #endregion

        #region Identity

        /// <summary>
        /// Initialize a new instance of the InternalListView class.
        /// </summary>
        /// <param name="kryptonListView">Reference to owning control.</param>
        public InternalListView(KryptonListView kryptonListView)
        {
            SetStyle(ControlStyles.ResizeRedraw
                     | ControlStyles.AllPaintingInWmPaint
                     | ControlStyles.OptimizedDoubleBuffer, true);

            _kryptonListView = kryptonListView;
            MouseIndex = -1;

            // Create manager and view for drawing the background
            ViewDrawPanel = new ViewDrawPanel();
            _viewManager = new ViewManager(this, ViewDrawPanel);

            // ReSharper disable RedundantBaseQualifier
            // Owner-draw items so StateTracking / StateChecked* paint instead of Win32 hot-track.
            base.Size = Size.Empty;
            base.BorderStyle = BorderStyle.None;
            base.OwnerDraw = true;
            // ReSharper restore RedundantBaseQualifier

            // We need to create and cache a device context compatible with the display
            _screenDC = PI.CreateCompatibleDC(IntPtr.Zero);
        }

        /// <summary>
        /// Releases all resources used by the Control.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (_screenDC != IntPtr.Zero)
            {
                PI.DeleteDC(_screenDC);
            }
        }

        #endregion

        #region Public

        /// <summary>
        /// Recreate the window handle.
        /// </summary>
        public void Recreate() => RecreateHandle();

        /// <summary>
        /// Gets access to the contained view draw panel instance.
        /// </summary>
        public ViewDrawPanel ViewDrawPanel { get; }

        /// <summary>
        /// Gets the item index the mouse is over, or <c>-1</c> when it is not over an item.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int MouseIndex { get; private set; }

        /// <summary>
        /// Gets and sets if the mouse is currently over the combo box.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool MouseOver
        {
            get => _mouseOver;
            set
            {
                // Only interested in changes
                if (_mouseOver != value)
                {
                    _mouseOver = value;

                    // Generate appropriate change event
                    if (_mouseOver)
                    {
                        OnTrackMouseEnter(EventArgs.Empty);
                    }
                    else
                    {
                        OnTrackMouseLeave(EventArgs.Empty);
                        if (MouseIndex != -1)
                        {
                            int oldIndex = MouseIndex;
                            MouseIndex = -1;
                            _kryptonListView.InvalidateTrackedItems(oldIndex, -1);
                        }
                    }
                }
            }
        }

        #endregion

        #region Protected

        /// <summary>Raises the <see cref="E:System.Windows.Forms.Control.SystemColorsChanged" /> event.</summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnSystemColorsChanged(EventArgs e)
        {
            // DO nothing, It's Krypton Colours that are in use !
        }

        /// <summary>
        /// Raises the Layout event.
        /// </summary>
        /// <param name="levent">A LayoutEventArgs containing the event data.</param>
        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);

            // Ask the panel to layout given our available size
            using var context =
                new ViewLayoutContext(_viewManager, this, _kryptonListView, _kryptonListView.Renderer);
            ViewDrawPanel.Layout(context);
        }

        /// <summary>
        /// Process Windows-based messages.
        /// </summary>
        /// <param name="m">A Windows-based message.</param>
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case PI.WM_.ERASEBKGND:
                    // Do not draw the background here, always do it in the paint
                    // instead to prevent flicker because of a two stage drawing process
                    break;
                case PI.WM_.PRINTCLIENT:
                case PI.WM_.PAINT:
                    WmPaint(ref m);
                    break;
                case PI.WM_.VSCROLL:
                case PI.WM_.HSCROLL:
                case PI.WM_.MOUSEWHEEL:
                    Invalidate();
                    base.WndProc(ref m);
                    break;
                case PI.WM_.MOUSELEAVE:
                    if (MouseOver)
                    {
                        MouseOver = false;
                        _kryptonListView.PerformNeedPaint(true);
                        Invalidate();
                    }

                    base.WndProc(ref m);
                    break;
                case PI.WM_.MOUSEMOVE:
                    if (!MouseOver)
                    {
                        MouseOver = true;
                        _kryptonListView.PerformNeedPaint(true);
                        Invalidate();
                    }

                    UpdateMouseIndexFromLParam(m.LParam);
                    base.WndProc(ref m);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        /// <summary>
        /// Raises the HandleCreated event.
        /// </summary>
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            // Native infotips would paint Win32 hover text over KryptonToolTip.
            ShowItemToolTips = false;
        }

        #endregion

        #region Private

        private void UpdateMouseIndexFromLParam(IntPtr lParam)
        {
            var mousePoint = new Point((int)lParam.ToInt64());
            var mouseIndex = -1;
            try
            {
                ListViewHitTestInfo hit = HitTest(mousePoint);
                if (hit.Item != null)
                {
                    mouseIndex = hit.Item.Index;
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                mouseIndex = -1;
            }

            if (MouseIndex == mouseIndex)
            {
                return;
            }

            int oldIndex = MouseIndex;
            MouseIndex = mouseIndex;
            _kryptonListView.InvalidateTrackedItems(oldIndex, mouseIndex);
        }

        /// <summary>
        /// Raises the TrackMouseEnter event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        private void OnTrackMouseEnter(EventArgs e) => TrackMouseEnter?.Invoke(this, e);

        /// <summary>
        /// Raises the TrackMouseLeave event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        private void OnTrackMouseLeave(EventArgs e) => TrackMouseLeave?.Invoke(this, e);

        private void WmPaint(ref Message m)
        {
            var ps = new PI.PAINTSTRUCT();

            // Do we need to BeginPaint or just take the given HDC?
            IntPtr hdc = m.WParam == IntPtr.Zero ? PI.BeginPaint(Handle, ref ps) : m.WParam;

            // Create bitmap that all drawing occurs onto, then we can blit it later to remove flicker
            Rectangle realRect = CommonHelper.RealClientRectangle(Handle);

            // No point drawing when one of the dimensions is zero
            if (realRect is { Width: > 0, Height: > 0 })
            {
                IntPtr hBitmap = PI.CreateCompatibleBitmap(hdc, realRect.Width, realRect.Height);

                // If we managed to get a compatible bitmap
                if (hBitmap != IntPtr.Zero)
                {
                    // Must use the screen device context for the bitmap when drawing into the
                    // bitmap otherwise the Opacity and RightToLeftLayout will not work correctly.
                    // Select the new bitmap into the screen DC
                    var oldBitmap = PI.SelectObject(_screenDC, hBitmap);

                    try
                    {
                        // Easier to draw using a graphics instance than a DC!
                        using (Graphics g = Graphics.FromHdc(_screenDC))
                        {
                            // Ask the view element to layout in given space, needs this before a render call
                            using (var context = new ViewLayoutContext(this, _kryptonListView.Renderer))
                            {
                                context.DisplayRectangle = realRect;
                                ViewDrawPanel.Layout(context);
                            }

                            using (var context = new RenderContext(this, _kryptonListView, g, realRect,
                                       _kryptonListView.Renderer))
                            {
                                ViewDrawPanel.Render(context);
                            }

                            // We can only control the background color by using the built in property and not
                            // by overriding the drawing directly, therefore we can only provide a single color.
                            Color color1 = ViewDrawPanel.GetPalette().GetBackColor1(ViewDrawPanel.State);
                            if (color1 != BackColor)
                            {
                                BackColor = color1;
                            }

                            // Replace given DC with the screen DC for base window proc drawing
                            IntPtr beforeDC = m.WParam;
                            m.WParam = _screenDC;
                            DefWndProc(ref m);
                            m.WParam = beforeDC;
                        }

                        // Now blit from the bitmap from the screen to the real dc
                        PI.BitBlt(hdc, 0, 0, realRect.Width, realRect.Height, _screenDC, 0, 0, PI.SRCCOPY);
                    }
                    finally
                    {
                        // Restore the original bitmap
                        PI.SelectObject(_screenDC, oldBitmap);

                        // Delete the temporary bitmap
                        PI.DeleteObject(hBitmap);
                    }
                }
            }

            // Do we need to match the original BeginPaint?
            if (m.WParam == IntPtr.Zero)
            {
                PI.EndPaint(Handle, ref ps);
            }
        }

        #endregion
    }

    #endregion

    #region Instance Fields

    private readonly PaletteTripleOverride _overrideNormal;
    private readonly PaletteTripleOverride _overrideDisabled;
    private readonly PaletteTripleOverride _overrideTracking;
    private readonly PaletteTripleOverride _overrideCheckedNormal;
    private readonly PaletteTripleOverride _overrideCheckedTracking;
    private readonly ViewDrawDocker _drawDockerOuter;
    private readonly ViewLayoutFill _layoutFill;
    private readonly InternalListView _listView;
    private bool? _fixedActive;
    private readonly IntPtr _screenDC;
    private bool _mouseOver;
    private bool _alwaysActive;
    private bool _forcedLayout;
    private readonly KryptonToolTip _itemToolTip;
    private bool _showItemToolTips;
    private int _itemToolTipIndex = -1;
    #endregion

    #region Events

    /// <summary>Occurs when the label for an item is edited by the user.</summary>
    [Category("Behavior")]
    [Description("ListView AfterLabelEdit")]
    public event LabelEditEventHandler? AfterLabelEdit;

    /// <summary>Occurs when the user starts editing the label of an item.</summary>
    [Category("Behavior")]
    [Description("tView BeforeLabelEdit")]
    public event LabelEditEventHandler? BeforeLabelEdit;

    /// <summary>Occurs when the user clicks a column header within the list view control.</summary>
    [Category("Action")]
    [Description("ListView ColumnClick")]
    public event ColumnClickEventHandler? ColumnClick;

    /// <summary>
    /// </summary>
    [Description(@"")]
    [Category(@"Property Changed")]
    public event EventHandler? ItemActivate;

    /// <summary>Occurs when the check state of an item changes.</summary>
    [Category("Behavior")]
    [Description("CheckedListBox ItemCheck")]
    public event ItemCheckEventHandler? ItemCheck;

    /// <summary>Occurs when the checked state of an item changes.</summary>
    [Category("Behavior")]
    [Description("ListView ItemChecked")]
    public event ItemCheckedEventHandler? ItemChecked;

    /// <summary>Occurs when the selection state of an item changes.</summary>
    [Category("Behavior")]
    [Description("ListView ItemSelectionChanged")]
    public event ListViewItemSelectionChangedEventHandler? ItemSelectionChanged;

    /// <summary>Occurs when the <see cref="T:System.Windows.Forms.ListView" /> is in virtual mode and the contents of its cache need to be refreshed.</summary>
    [Category("Action")]
    [Description("ListView CacheVirtualItems")]
    public event CacheVirtualItemsEventHandler? CacheVirtualItems;

    /// <summary>Occurs when the <see cref="T:System.Windows.Forms.ListView" /> is in virtual mode and a <see cref="T:System.Windows.Forms.ListViewItem" /> must be provided.</summary>
    [Category("Action")]
    [Description("ListView RetrieveVirtualItem")]
    public event RetrieveVirtualItemEventHandler? RetrieveVirtualItem;

    /// <summary>Occurs when the <see cref="T:System.Windows.Forms.ListView" /> is in virtual mode and a search is taking place.</summary>
    [Category("Action")]
    [Description("ListView SearchForVirtualItem")]
    public event SearchForVirtualItemEventHandler? SearchForVirtualItem;

    /// <summary>
    /// </summary>
    [Description(@"Behavior")]
    [Category(@"Property Changed")]
    public event EventHandler? SelectedIndexChanged;

    /// <summary>Occurs when a <see cref="T:System.Windows.Forms.ListView" /> is in virtual mode and the selection state of a range of items has changed.</summary>
    [Category("Behavior")]
    [Description("ListViewVirtualItemsSelectionRangeChanged")]
    public event ListViewVirtualItemsSelectionRangeChangedEventHandler? VirtualItemsSelectionRangeChanged;

    #endregion

    public KryptonListView()
    {
        // Contains another control and needs marking as such for validation to work
        SetStyle(ControlStyles.ContainerControl, true);

        // Cannot select this control, only the child tree view and does not generate a
        SetStyle(ControlStyles.Selectable | ControlStyles.StandardClick, false);
        // Default fields
        base.Padding = new Padding(1);

        // Create the palette storage
        var backInherit = new PaletteBackInheritRedirect(Redirector, PaletteBackStyle.InputControlStandalone);
        var borderInherit = new PaletteBorderInheritRedirect(Redirector, PaletteBorderStyle.InputControlStandalone);
        var commonBack = new PaletteBackColor1(backInherit, NeedPaintDelegate);
        var commonBorder = new PaletteBorder(borderInherit, NeedPaintDelegate);
        StateCommon = new PaletteTreeStateRedirect(Redirector, commonBack, backInherit, commonBorder, borderInherit,
            NeedPaintDelegate);
        var disabledBack = new PaletteBackColor1(StateCommon.PaletteBack, NeedPaintDelegate);
        var disabledBorder = new PaletteBorder(StateCommon.PaletteBorder!, NeedPaintDelegate);
        StateDisabled = new PaletteTreeState(StateCommon, disabledBack, disabledBorder, NeedPaintDelegate);

        var normalBack = new PaletteBackColor1(StateCommon.PaletteBack, NeedPaintDelegate);
        var normalBorder = new PaletteBorder(StateCommon.PaletteBorder!, NeedPaintDelegate);
        StateNormal = new PaletteTreeState(StateCommon, normalBack, normalBorder, NeedPaintDelegate);

        var activeBack = new PaletteBackColor1(StateCommon.PaletteBack, NeedPaintDelegate);
        var activeBorder = new PaletteBorder(StateCommon.PaletteBorder!, NeedPaintDelegate);
        StateActive = new PaletteDouble(StateCommon, activeBack, activeBorder, NeedPaintDelegate);

        StateTracking = new PaletteTreeNodeTriple(StateCommon.Node, NeedPaintDelegate);
        StateCheckedNormal = new PaletteTreeNodeTriple(StateCommon.Node, NeedPaintDelegate);
        StateCheckedTracking = new PaletteTreeNodeTriple(StateCommon.Node, NeedPaintDelegate);

        // Create the override handling classes
        OverrideFocus = new PaletteTreeNodeTripleRedirect(Redirector, PaletteBackStyle.ButtonListItem,
            PaletteBorderStyle.ButtonListItem, PaletteContentStyle.ButtonListItem, NeedPaintDelegate);
        _overrideNormal = new PaletteTripleOverride(OverrideFocus.Node, StateNormal.Node, PaletteState.FocusOverride);
        _overrideDisabled = new PaletteTripleOverride(StateNormal.Node, StateDisabled.Node, PaletteState.Disabled);
        _overrideTracking = new PaletteTripleOverride(OverrideFocus.Node, StateTracking.Node, PaletteState.FocusOverride);
        _overrideCheckedNormal = new PaletteTripleOverride(OverrideFocus.Node, StateCheckedNormal.Node, PaletteState.FocusOverride);
        _overrideCheckedTracking = new PaletteTripleOverride(OverrideFocus.Node, StateCheckedTracking.Node, PaletteState.FocusOverride);

        _itemToolTip = new KryptonToolTip();
        _itemToolTip.ToolTipValues.EnableToolTips = true;
        _itemToolTip.ToolTipValues.ToolTipStyle = LabelStyle.SuperTip;
        _itemToolTip.ToolTipValues.ToolTipPosition.PlacementMode = PlacementMode.Bottom;

        // Create the internal list box used for containing content
        _listView = new InternalListView(this);
        _listView.AfterLabelEdit += OnAfterLabelEdit;
        _listView.BeforeLabelEdit += OnBeforeLabelEdit;
        _listView.Click += OnListViewClick; // SKC: make sure that the default click is also routed.
        _listView.ColumnClick += OnColumnClick;
        _listView.DoubleClick += OnListViewDoubleClick;
        _listView.GotFocus += OnListViewGotFocus;
        _listView.ItemActivate += OnItemActivate;
        _listView.ItemCheck += OnItemCheck;
        _listView.ItemChecked += OnItemChecked;
        _listView.ItemSelectionChanged += OnItemSelectionChanged;
        _listView.KeyDown += OnListViewKeyDown;
        _listView.KeyPress += OnListViewKeyPress;
        _listView.KeyUp += OnListViewKeyUp;
        _listView.LostFocus += OnListViewLostFocus;
        _listView.CacheVirtualItems += OnCacheVirtualItems;
        _listView.RetrieveVirtualItem += OnRetrieveVirtualItem;
        _listView.SearchForVirtualItem += OnSearchForVirtualItem;
        _listView.SelectedIndexChanged += OnSelectedIndexChanged;
        _listView.VirtualItemsSelectionRangeChanged += OnVirtualItemsSelectionRangeChanged;
        _listView.DrawItem += OnListViewDrawItem;
        _listView.DrawSubItem += OnListViewDrawSubItem;
        _listView.DrawColumnHeader += OnListViewDrawColumnHeader;

        _layoutFill = new ViewLayoutFill(_listView)
        {
            DisplayPadding = new Padding(1)
        };

        // Create inner view for placing inside the drawing docker
        var drawDockerInner = new ViewLayoutDocker
        {
            { _layoutFill, ViewDockStyle.Fill }
        };

        // Create view for the control border and background
        _drawDockerOuter = new ViewDrawDocker(StateNormal.Back, StateNormal.Border)
        {
            { drawDockerInner, ViewDockStyle.Fill }
        };

        // Create the view manager instance
        ViewManager = new ViewManager(this, _drawDockerOuter);

        // We need to create and cache a device context compatible with the display
        _screenDC = PI.CreateCompatibleDC(IntPtr.Zero);

        // Add tree view to the controls collection
        ((KryptonReadOnlyControls)Controls).AddInternal(_listView);
    }

    private void OnAfterLabelEdit(object? sender, LabelEditEventArgs e) => AfterLabelEdit?.Invoke(this, e);

    private void OnBeforeLabelEdit(object? sender, LabelEditEventArgs e) => BeforeLabelEdit?.Invoke(this, e);

    private void OnColumnClick(object? sender, ColumnClickEventArgs e) => ColumnClick?.Invoke(this, e);

    private void OnListViewDoubleClick(object? sender, EventArgs e) => OnDoubleClick(e);

    private void OnItemActivate(object? sender, EventArgs e) => ItemActivate?.Invoke(this, e);

    private void OnItemCheck(object? sender, ItemCheckEventArgs e) => ItemCheck?.Invoke(this, e);

    private void OnItemChecked(object? sender, ItemCheckedEventArgs e) => ItemChecked?.Invoke(this, e);

    private void OnItemSelectionChanged(object? sender, ListViewItemSelectionChangedEventArgs e) => ItemSelectionChanged?.Invoke(this, e);

    private void OnListViewKeyDown(object? sender, KeyEventArgs e) => OnKeyDown(e);

    private void OnListViewKeyPress(object? sender, KeyPressEventArgs e) => OnKeyPress(e);

    private void OnListViewKeyUp(object? sender, KeyEventArgs e) => OnKeyUp(e);

    private void OnCacheVirtualItems(object? sender, CacheVirtualItemsEventArgs e) => CacheVirtualItems?.Invoke(this, e);

    private void OnRetrieveVirtualItem(object? sender, RetrieveVirtualItemEventArgs e)
    {
        RetrieveVirtualItem?.Invoke(this, e);
        if (e.Item != null)
        {
            SetItemState(e.Item);
        }
    }

    private void OnSearchForVirtualItem(object? sender, SearchForVirtualItemEventArgs e) => SearchForVirtualItem?.Invoke(this, e);

    private void OnSelectedIndexChanged(object? sender, EventArgs e) => SelectedIndexChanged?.Invoke(this, e);

    private void OnVirtualItemsSelectionRangeChanged(object? sender,
        ListViewVirtualItemsSelectionRangeChangedEventArgs e)
        => VirtualItemsSelectionRangeChanged?.Invoke(this, e);

    private void OnListViewClick(object? sender, EventArgs e) => OnClick(e);

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _itemToolTip.Dispose();
        }

        base.Dispose(disposing);
        if (_screenDC != IntPtr.Zero)
        {
            PI.DeleteDC(_screenDC);
        }
    }

    #region Public

    /// <summary>
    /// Gets access to the contained TreeView instance.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Browsable(false)]
    public ListView ListView => _listView;

    /// <summary>
    /// Gets access to the contained input control.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Browsable(false)]
    public Control ContainedControl => _listView;

    /// <summary>Gets or sets the type of action the user must take to activate an item.</summary>
    /// <returns>One of the <see cref="T:System.Windows.Forms.ItemActivation" /> values. The default is <see cref="F:System.Windows.Forms.ItemActivation.Standard" />.</returns>
    /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is not one of the <see cref="T:System.Windows.Forms.ItemActivation" /> members.</exception>
    [Category("Behavior")]
    [DefaultValue(ItemActivation.Standard)]
    [Description("ListViewActivationDescr")]
    public ItemActivation Activation
    {
        get => _listView.Activation;
        set => _listView.Activation = value;
    }

    /// <summary>Gets or sets the alignment of items in the control.</summary>
    /// <returns>One of the <see cref="T:System.Windows.Forms.ListViewAlignment" /> values. The default is <see cref="F:System.Windows.Forms.ListViewAlignment.Top" />.</returns>
    /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is not one of the <see cref="T:System.Windows.Forms.ListViewAlignment" /> values.</exception>
    [Category("Behavior")]
    [DefaultValue(ListViewAlignment.Top)]
    [Localizable(true)]
    [Description("ListViewAlignmentDescr")]
    public ListViewAlignment Alignment
    {
        get => _listView.Alignment;
        set => _listView.Alignment = value;
    }

    /// <summary>Gets or sets a value indicating whether the user can drag column headers to reorder columns in the control.</summary>
    /// <returns>
    /// <see langword="true" /> if drag-and-drop column reordering is allowed; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("ListViewAllowColumnReorderDescr")]
    public bool AllowColumnReorder
    {
        get => _listView.AllowColumnReorder;
        set => _listView.AllowColumnReorder = value;
    }

    /// <summary>Gets or sets whether icons are automatically kept arranged.</summary>
    /// <returns>
    /// <see langword="true" /> if icons are automatically kept arranged and snapped to the grid; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
    [Category("Behavior")]
    [DefaultValue(true)]
    [Description("ListViewAutoArrangeDescr")]
    public bool AutoArrange
    {
        get => _listView.AutoArrange;
        set => _listView.AutoArrange = value;
    }

    /// <summary>Gets or sets a value indicating whether the background image of the <see cref="T:System.Windows.Forms.ListView" /> should be tiled.</summary>
    /// <returns>
    /// <see langword="true" /> if the background image of the <see cref="T:System.Windows.Forms.ListView" /> should be tiled; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
    [Category("Appearance")]
    [DefaultValue(false)]
    [Description("ListViewBackgroundImageTiledDescr")]
    public bool BackgroundImageTiled
    {
        get => _listView.BackgroundImageTiled;
        set => _listView.BackgroundImageTiled = value;
    }


    /// <summary>Gets or sets a value indicating whether a check box appears next to each item in the control.</summary>
    /// <returns>
    /// <see langword="true" /> if a check box appears next to each item in the <see cref="T:System.Windows.Forms.ListView" /> control; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
    [Category("Appearance")]
    [DefaultValue(false)]
    [Description("ListViewCheckBoxesDescr")]
    public bool CheckBoxes
    {
        get => _listView.CheckBoxes;
        set => _listView.CheckBoxes = value;
    }


    /// <summary>
    /// Gets or sets the text for the control.
    /// </summary>
    [Browsable(false)]
    [Bindable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [AllowNull]
    public override string Text
    {
        get => base.Text;
        set => base.Text = value;
    }

    /// <summary>
    /// Gets or sets the background color for the control.
    /// </summary>
    [Browsable(false)]
    [Bindable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Color BackColor
    {
        get => base.BackColor;
        set => base.BackColor = value;
    }

    /// <summary>
    /// Gets or sets the font of the text Displayed by the control.
    /// </summary>
    [Browsable(false)]
    [Bindable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [AmbientValue(null)]
    [AllowNull]
    public override Font Font
    {
        get => base.Font;
        set => base.Font = value!;
    }

    /// <summary>
    /// Gets or sets the foreground color for the control.
    /// </summary>
    [Browsable(false)]
    [Bindable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Color ForeColor
    {
        get => base.ForeColor;
        set => base.ForeColor = value;
    }

    /// <summary>
    /// Gets and sets the internal padding space.
    /// </summary>
    [DefaultValue(typeof(Padding), "1,1,1,1")]
    public new Padding Padding
    {
        get => base.Padding;

        set
        {
            base.Padding = value;
            _layoutFill.DisplayPadding = value;
            PerformNeedPaint(true);
        }
    }

    /// <summary>Gets the indexes of the currently checked items in the control.</summary>
    /// <returns>A <see cref="T:System.Windows.Forms.ListView.CheckedIndexCollection" /> that contains the indexes of the currently checked items. If no items are currently checked, an empty <see cref="T:System.Windows.Forms.ListView.CheckedIndexCollection" /> is returned.</returns>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ListView.CheckedIndexCollection CheckedIndices => _listView.CheckedIndices;

    /// <summary>Gets the currently checked items in the control.</summary>
    /// <returns>A <see cref="T:System.Windows.Forms.ListView.CheckedListViewItemCollection" /> that contains the currently checked items. If no items are currently checked, an empty <see cref="T:System.Windows.Forms.ListView.CheckedListViewItemCollection" /> is returned.</returns>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ListView.CheckedListViewItemCollection CheckedItems => _listView.CheckedItems;

    /// <summary>Gets the collection of all column headers that appear in the control.</summary>
    /// <returns>A <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" /> that represents the column headers that appear when the <see cref="P:System.Windows.Forms.ListView.View" /> property is set to <see cref="F:System.Windows.Forms.View.Details" />.</returns>
    [Category("CatBehavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Editor("System.Windows.Forms.Design.ColumnHeaderCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
    [Description("collection of all column headers that appear in the control")]
    [Localizable(true)]
    [MergableProperty(false)]
    public ListView.ColumnHeaderCollection Columns => _listView.Columns;


    /// <summary>Gets or sets the item in the control that currently has focus.</summary>
    /// <returns>A <see cref="T:System.Windows.Forms.ListViewItem" /> that represents the item that has focus, or <see langword="null" /> if no item has the focus in the <see cref="T:System.Windows.Forms.ListView" />.</returns>
    [Category("Appearance")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("ListViewFocusedItemDescr")]
    public ListViewItem? FocusedItem
    {
        get => _listView.FocusedItem;
        set => _listView.FocusedItem = value;
    }

    /// <summary>Gets or sets a value indicating whether clicking an item selects all its subitems.</summary>
    /// <returns>
    /// <see langword="true" /> if clicking an item selects the item and all its subitems; <see langword="false" /> if clicking an item selects only the item itself. The default is <see langword="false" />.</returns>
    [Category("Appearance")]
    [DefaultValue(false)]
    [Description("if clicking an item selects the item and all its subitems")]
    public bool FullRowSelect
    {
        get => _listView.FullRowSelect;
        set => _listView.FullRowSelect = value;
    }

    /// <summary>Gets or sets a value indicating whether grid lines appear between the rows and columns containing the items and subitems in the control.</summary>
    /// <returns>
    /// <see langword="true" /> if grid lines are drawn around items and subitems; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
    [Category("Appearance")]
    [DefaultValue(false)]
    [Description("if grid lines are drawn around items and subitems")]
    public bool GridLines
    {
        get => _listView.GridLines;
        set => _listView.GridLines = value;
    }

    /// <summary>Gets the collection of <see cref="T:System.Windows.Forms.ListViewGroup" /> objects assigned to the control.</summary>
    /// <returns>A <see cref="T:System.Windows.Forms.ListViewGroupCollection" /> that contains all the groups in the <see cref="T:System.Windows.Forms.ListView" /> control.</returns>
    [Category("Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    [Editor("System.Windows.Forms.Design.ListViewGroupCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
        typeof(UITypeEditor))]
    [Description("ListViewGroupsDescr")]
    [MergableProperty(false)]
    public ListViewGroupCollection Groups => _listView.Groups;

    /// <summary>Gets or sets the column header style.</summary>
    /// <returns>One of the <see cref="T:System.Windows.Forms.ColumnHeaderStyle" /> values. The default is <see cref="F:System.Windows.Forms.ColumnHeaderStyle.Clickable" />.</returns>
    /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is not one of the <see cref="T:System.Windows.Forms.ColumnHeaderStyle" /> values.</exception>
    [Category("Behavior")]
    [DefaultValue(ColumnHeaderStyle.Clickable)]
    [Description("ListViewHeaderStyleDescr")]
    public ColumnHeaderStyle HeaderStyle
    {
        get => _listView.HeaderStyle;
        set => _listView.HeaderStyle = value;
    }

    /// <summary>Gets or sets a value indicating whether the selected item in the control remains highlighted when the control loses focus.</summary>
    /// <returns>
    /// <see langword="true" /> if the selected item does not appear highlighted when the control loses focus; <see langword="false" /> if the selected item still appears highlighted when the control loses focus. The default is <see langword="true" />.</returns>
    [Category("Behavior")]
    [DefaultValue(true)]
    [Description("if the selected item does not appear highlighted when the control loses focus")]
    public bool HideSelection
    {
        get => _listView.HideSelection;
        set => _listView.HideSelection = value;
    }

    /// <summary>
    /// Gets or sets whether native one-click item activation is used.
    /// Hover appearance always uses <see cref="StateTracking"/> / <see cref="StateCheckedTracking"/>.
    /// </summary>
    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("Native one-click activation only. Hover paint always uses StateTracking.")]
    public bool HotTracking
    {
        get => _listView.HotTracking;
        set => _listView.HotTracking = value;
    }

    /// <summary>Gets or sets a value indicating whether an item is automatically selected when the mouse pointer remains over the item for a few seconds.</summary>
    /// <returns>
    /// <see langword="true" /> if an item is automatically selected when the mouse pointer hovers over it; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("if an item is automatically selected when the mouse pointer hovers over ")]
    public bool HoverSelection
    {
        get => _listView.HoverSelection;
        set => _listView.HoverSelection = value;
    }


    /// <summary>Gets a collection containing all items in the control.</summary>
    /// <returns>A <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> that contains all the items in the <see cref="T:System.Windows.Forms.ListView" /> control.</returns>
    [Category("Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    [Editor(
        "System.Windows.Forms.Design.ListViewItemCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
        typeof(UITypeEditor))]
    [Description("Gets a collection containing all items in the control")]
    [MergableProperty(false)]
    public ListView.ListViewItemCollection Items => _listView.Items;


    /// <summary>Gets or sets a value indicating whether the user can edit the labels of items in the control.</summary>
    /// <returns>
    /// <see langword="true" /> if the user can edit the labels of items at run time; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("ListViewLabelEditDescr")]
    public bool LabelEdit
    {
        get => _listView.LabelEdit;
        set => _listView.LabelEdit = value;
    }

    /// <summary>Gets or sets a value indicating whether item labels wrap when items are displayed in the control as icons.</summary>
    /// <returns>
    /// <see langword="true" /> if item labels wrap when items are displayed as icons; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
    [Category("Behavior")]
    [DefaultValue(true)]
    [Localizable(true)]
    [Description("if item labels wrap when items are displayed as icons")]
    public bool LabelWrap
    {
        get => _listView.LabelWrap;
        set => _listView.LabelWrap = value;
    }

    /// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ImageList" /> to use when displaying items as large icons in the control.</summary>
    /// <returns>An <see cref="T:System.Windows.Forms.ImageList" /> that contains the icons to use when the <see cref="P:System.Windows.Forms.ListView.View" /> property is set to <see cref="F:System.Windows.Forms.View.LargeIcon" />. The default is <see langword="null" />.</returns>
    [Category("Behavior")]
    [DefaultValue(null)]
    [Description("contains the icons to use")]
    public ImageList? LargeImageList
    {
        get => _listView.LargeImageList;
        set => _listView.LargeImageList = value;
    }

    /// <summary>Gets or sets the sorting comparer for the control.</summary>
    /// <returns>An <see cref="T:System.Collections.IComparer" /> that represents the sorting comparer for the control.</returns>
    [Category("Behavior")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("sorting comparer for the control")]
    public IComparer? ListViewItemSorter
    {
        get => _listView.ListViewItemSorter;
        set => _listView.ListViewItemSorter = value;
    }

    /// <summary>Gets or sets a value indicating whether multiple items can be selected.</summary>
    /// <returns>
    /// <see langword="true" /> if multiple items in the control can be selected at one time; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
    [Category("Behavior")]
    [DefaultValue(true)]
    [Description("if multiple items in the control can be selected at one time")]
    public bool MultiSelect
    {
        get => _listView.MultiSelect;
        set => _listView.MultiSelect = value;
    }

    /// <summary>Gets or sets a value indicating whether the control is laid out from right to left.</summary>
    /// <returns>
    /// <see langword="true" /> to indicate the <see cref="T:System.Windows.Forms.ListView" /> control is laid out from right to left; otherwise, <see langword="false" />.</returns>
    [Category("Appearance")]
    [Localizable(true)]
    [DefaultValue(false)]
    [Description("control is laid out from right to left")]
    public bool RightToLeftLayout
    {
        get => _listView.RightToLeftLayout;
        set => _listView.RightToLeftLayout = value;
    }

    /// <summary>Gets or sets a value indicating whether a scroll bar is added to the control when there is not enough room to display all items.</summary>
    /// <returns>
    /// <see langword="true" /> if scroll bars are added to the control when necessary to allow the user to see all the items; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
    [Category("Behavior")]
    [DefaultValue(true)]
    [Description("if scroll bars are added to the control when necessary to allow the user to see all the items")]
    public bool Scrollable
    {
        get => _listView.Scrollable;
        set => _listView.Scrollable = value;
    }

    /// <summary>Gets the indexes of the selected items in the control.</summary>
    /// <returns>A <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" /> that contains the indexes of the selected items. If no items are currently selected, an empty <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" /> is returned.</returns>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ListView.SelectedIndexCollection SelectedIndices => _listView.SelectedIndices;

    /// <summary>Gets the items that are selected in the control.</summary>
    /// <returns>A <see cref="T:System.Windows.Forms.ListView.SelectedListViewItemCollection" /> that contains the items that are selected in the control. If no items are currently selected, an empty <see cref="T:System.Windows.Forms.ListView.SelectedListViewItemCollection" /> is returned.</returns>
    [Category("Appearance")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets the items that are selected in the control")]
    public ListView.SelectedListViewItemCollection SelectedItems => _listView.SelectedItems;

    /// <summary>Gets or sets a value indicating whether items are displayed in groups.</summary>
    /// <returns>
    /// <see langword="true" /> to display items in groups; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
    [Category("Behavior")]
    [DefaultValue(true)]
    [Description("to display items in groups")]
    public bool ShowGroups
    {
        get => _listView.ShowGroups;
        set => _listView.ShowGroups = value;
    }

    /// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ImageList" /> to use when displaying items as small icons in the control.</summary>
    /// <returns>An <see cref="T:System.Windows.Forms.ImageList" /> that contains the icons to use when the <see cref="P:System.Windows.Forms.ListView.View" /> property is set to <see cref="F:System.Windows.Forms.View.SmallIcon" />. The default is <see langword="null" />.</returns>
    [Category("Behavior")]
    [DefaultValue(null)]
    [Description("use when displaying items as small icons in the control")]
    public ImageList? SmallImageList
    {
        get => _listView.SmallImageList;
        set => _listView.SmallImageList = value;
    }

    /// <summary>
    /// Gets or sets whether item tooltips are shown with <see cref="KryptonToolTip"/> instead of Win32 infotips.
    /// </summary>
    /// <remarks>
    /// Uses <see cref="ListViewItem.ToolTipText"/> when set, otherwise the item text.
    /// Native <see cref="ListView.ShowItemToolTips"/> is not used.
    /// </remarks>
    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("Show item tooltips with KryptonToolTip instead of Win32 infotips.")]
    public bool ShowItemToolTips
    {
        get => _showItemToolTips;
        set
        {
            if (_showItemToolTips == value)
            {
                return;
            }

            _showItemToolTips = value;
            _listView.ShowItemToolTips = false;
            if (!_showItemToolTips)
            {
                HideItemToolTip();
            }
            else
            {
                UpdateItemToolTip(immediate: false);
            }
        }
    }

    /// <summary>
    /// Gets the <see cref="KryptonToolTip"/> used when <see cref="ShowItemToolTips"/> is <see langword="true"/>.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonToolTip ItemToolTip => _itemToolTip;

    /// <summary>Gets or sets the sort order for items in the control.</summary>
    /// <returns>One of the <see cref="T:System.Windows.Forms.SortOrder" /> values. The default is <see cref="F:System.Windows.Forms.SortOrder.None" />.</returns>
    /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is not one of the <see cref="T:System.Windows.Forms.SortOrder" /> values.</exception>
    [Category("Behavior")]
    [DefaultValue(SortOrder.None)]
    [Description("ListViewSortingDescr")]
    public SortOrder Sorting
    {
        get => _listView.Sorting;
        set => _listView.Sorting = value;
    }

    /// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ImageList" /> associated with application-defined states in the control.</summary>
    /// <returns>An <see cref="T:System.Windows.Forms.ImageList" /> that contains a set of state images that can be used to indicate an application-defined state of an item. The default is <see langword="null" />.</returns>
    [Category("CatBehavior")]
    [DefaultValue(null)]
    [Description("ListViewStateImageListDescr")]
    public ImageList? StateImageList
    {
        get => _listView.StateImageList;
        set => _listView.StateImageList = value;
    }

    /// <summary>Gets or sets the size of the tiles shown in tile view.</summary>
    /// <returns>A <see cref="T:System.Drawing.Size" /> that contains the new tile size.</returns>
    [Category("Appearance")]
    [Browsable(true)]
    [Description("contains the new tile size")]
    public Size TileSize
    {
        get => _listView.TileSize;
        set => _listView.TileSize = value;
    }
    private bool ShouldSerializeTileSize() => !TileSize.Equals(Size.Empty);

    /// <summary>Gets or sets the first visible item in the control.</summary>
    /// <returns>A <see cref="T:System.Windows.Forms.ListViewItem" /> that represents the first visible item in the control.</returns>
    /// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Windows.Forms.ListView.View" /> property is set to <see cref="F:System.Windows.Forms.View.LargeIcon" />,  <see cref="F:System.Windows.Forms.View.SmallIcon" />, or <see cref="F:System.Windows.Forms.View.Tile" />.</exception>
    [Category("Appearance")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("ListViewTopItemDescr")]
    public ListViewItem? TopItem
    {
        get => _listView.TopItem;
        set => _listView.TopItem = value;
    }

    /// <summary>Gets or sets how items are displayed in the control.</summary>
    /// <returns>One of the <see cref="T:System.Windows.Forms.View" /> values. The default is <see cref="F:System.Windows.Forms.View.LargeIcon" />.</returns>
    /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is not one of the <see cref="T:System.Windows.Forms.View" /> values.</exception>
    [Category("Appearance")]
    [DefaultValue(View.LargeIcon)]
    [Description("ListViewViewDescr")]
    public View View
    {
        get => _listView.View;
        set => _listView.View = value;
    }

    /// <summary>Gets or sets the number of <see cref="T:System.Windows.Forms.ListViewItem" /> objects contained in the list when in virtual mode.</summary>
    /// <returns>The number of <see cref="T:System.Windows.Forms.ListViewItem" /> objects contained in the <see cref="T:System.Windows.Forms.ListView" /> when in virtual mode.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <see cref="P:System.Windows.Forms.ListView.VirtualListSize" /> is set to a value less than 0.</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <see cref="P:System.Windows.Forms.ListView.VirtualMode" /> is set to <see langword="true" />, <see cref="P:System.Windows.Forms.ListView.VirtualListSize" /> is greater than 0, and <see cref="E:System.Windows.Forms.ListView.RetrieveVirtualItem" /> is not handled.</exception>
    [Category("Behavior")]
    [DefaultValue(0)]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Description("ListViewVirtualListSizeDescr")]
    public int VirtualListSize
    {
        get => _listView.VirtualListSize;
        set => _listView.VirtualListSize = value;
    }

    /// <summary>Gets or sets a value indicating whether you have provided your own data-management operations for the <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
    /// <returns>
    /// <see langword="true" /> if <see cref="T:System.Windows.Forms.ListView" /> uses data-management operations that you provide; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///         <see cref="P:System.Windows.Forms.ListView.VirtualMode" /> is set to <see langword="true" /> and one of the following conditions exist:
    ///
    /// <see cref="P:System.Windows.Forms.ListView.VirtualListSize" /> is greater than 0 and <see cref="E:System.Windows.Forms.ListView.RetrieveVirtualItem" /> is not handled.
    ///  -or-
    ///
    /// <see cref="P:System.Windows.Forms.ListView.Items" />, <see cref="P:System.Windows.Forms.ListView.CheckedItems" />, or <see cref="P:System.Windows.Forms.ListView.SelectedItems" /> contains items.
    ///  -or-
    ///
    /// Edits are made to <see cref="P:System.Windows.Forms.ListView.Items" />.</exception>
    [Category("Behavior")]
    [DefaultValue(false)]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Description("ListViewVirtualModeDescr")]
    public bool VirtualMode
    {
        get => _listView.VirtualMode;
        set => _listView.VirtualMode = value;
    }

    /// <summary>Arranges items in the control when they are displayed as icons with a specified alignment setting.</summary>
    /// <param name="value">One of the <see cref="T:System.Windows.Forms.ListViewAlignment" /> values.</param>
    /// <exception cref="T:System.ArgumentException">The value specified in the <paramref name="value" /> parameter is not a member of the <see cref="T:System.Windows.Forms.ListViewAlignment" /> enumeration.</exception>
    public void ArrangeIcons(ListViewAlignment value) => _listView.ArrangeIcons(value);
    /// <summary>Arranges items in the control when they are displayed as icons based on the value of the <see cref="P:System.Windows.Forms.ListView.Alignment" /> property.</summary>
    public void ArrangeIcons() => _listView.ArrangeIcons(ListViewAlignment.Default);

    /// <summary>Resizes the width of the columns as indicated by the resize style.</summary>
    /// <param name="headerAutoResize">One of the <see cref="T:System.Windows.Forms.ColumnHeaderAutoResizeStyle" /> values.</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// <see cref="M:System.Windows.Forms.ListView.AutoResizeColumn(System.Int32,System.Windows.Forms.ColumnHeaderAutoResizeStyle)" /> is called with a value other than <see cref="F:System.Windows.Forms.ColumnHeaderAutoResizeStyle.None" /> when <see cref="P:System.Windows.Forms.ListView.View" /> is not set to <see cref="F:System.Windows.Forms.View.Details" />.</exception>
    public void AutoResizeColumns(ColumnHeaderAutoResizeStyle headerAutoResize) =>
        _listView.AutoResizeColumns(headerAutoResize);

    /// <summary>Resizes the width of the given column as indicated by the resize style.</summary>
    /// <param name="columnIndex">The zero-based index of the column to resize.</param>
    /// <param name="headerAutoResize">One of the <see cref="T:System.Windows.Forms.ColumnHeaderAutoResizeStyle" /> values.</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///         <paramref name="columnIndex" /> is greater than 0 when <see cref="P:System.Windows.Forms.ListView.Columns" /> is <see langword="null" />
    /// -or-
    /// <paramref name="columnIndex" /> is less than 0 or greater than the number of columns set.</exception>
    /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
    /// <paramref name="headerAutoResize" /> is not a member of the <see cref="T:System.Windows.Forms.ColumnHeaderAutoResizeStyle" /> enumeration.</exception>
    public void AutoResizeColumn(int columnIndex, ColumnHeaderAutoResizeStyle headerAutoResize) => _listView.AutoResizeColumn(columnIndex, headerAutoResize);

    /// <summary>Removes all items and columns from the control.</summary>
    public void Clear() => _listView.Clear();

    /// <summary>Finds the first <see cref="T:System.Windows.Forms.ListViewItem" /> that begins with the specified text value.</summary>
    /// <param name="text">The text to search for.</param>
    /// <returns>The first <see cref="T:System.Windows.Forms.ListViewItem" /> that begins with the specified text value.</returns>
    public ListViewItem? FindItemWithText(string text) => _listView.FindItemWithText(text);

    /// <summary>Finds the first <see cref="T:System.Windows.Forms.ListViewItem" /> or <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />, if indicated, that begins with the specified text value. The search starts at the specified index.</summary>
    /// <param name="text">The text to search for.</param>
    /// <param name="includeSubItemsInSearch">
    /// <see langword="true" /> to include subitems in the search; otherwise, <see langword="false" />.</param>
    /// <param name="startIndex">The index of the item at which to start the search.</param>
    /// <returns>The first <see cref="T:System.Windows.Forms.ListViewItem" /> that begins with the specified text value.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> is less 0 or more than the number items in the <see cref="T:System.Windows.Forms.ListView" />.</exception>
    public ListViewItem? FindItemWithText(string text, bool includeSubItemsInSearch, int startIndex) =>
        _listView.FindItemWithText(text, includeSubItemsInSearch, startIndex);

    /// <summary>Finds the first <see cref="T:System.Windows.Forms.ListViewItem" /> or <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />, if indicated, that begins with the specified text value. The search starts at the specified index.</summary>
    /// <param name="text">The text to search for.</param>
    /// <param name="includeSubItemsInSearch">
    /// <see langword="true" /> to include subitems in the search; otherwise, <see langword="false" />.</param>
    /// <param name="startIndex">The index of the item at which to start the search.</param>
    /// <param name="isPrefixSearch">
    /// <see langword="true" /> to allow partial matches; otherwise, <see langword="false" />.</param>
    /// <returns>The first <see cref="T:System.Windows.Forms.ListViewItem" /> that begins with the specified text value.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> is less than 0 or more than the number of items in the <see cref="T:System.Windows.Forms.ListView" />.</exception>
    public ListViewItem? FindItemWithText(
        string text,
        bool includeSubItemsInSearch,
        int startIndex,
        bool isPrefixSearch) =>
        _listView.FindItemWithText(text, includeSubItemsInSearch, startIndex, isPrefixSearch);

    /// <summary>Finds the next item from the given point, searching in the specified direction</summary>
    /// <param name="dir">One of the <see cref="T:System.Windows.Forms.SearchDirectionHint" /> values.</param>
    /// <param name="point">The point at which to begin searching.</param>
    /// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> that is closest to the given point, searching in the specified direction.</returns>
    /// <exception cref="T:System.InvalidOperationException">
    /// <see cref="P:System.Windows.Forms.ListView.View" /> is set to a value other than <see cref="F:System.Windows.Forms.View.SmallIcon" /> or <see cref="F:System.Windows.Forms.View.LargeIcon" />.</exception>
    public ListViewItem? FindNearestItem(SearchDirectionHint dir, Point point) =>
        _listView.FindNearestItem(dir, point);

    /// <summary>Finds the next item from the given x- and y-coordinates, searching in the specified direction.</summary>
    /// <param name="searchDirection">One of the <see cref="T:System.Windows.Forms.SearchDirectionHint" /> values.</param>
    /// <param name="x">The x-coordinate for the point at which to begin searching.</param>
    /// <param name="y">The y-coordinate for the point at which to begin searching.</param>
    /// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> that is closest to the given coordinates, searching in the specified direction.</returns>
    /// <exception cref="T:System.InvalidOperationException">
    /// <see cref="P:System.Windows.Forms.ListView.View" /> is set to a value other than <see cref="F:System.Windows.Forms.View.SmallIcon" /> or <see cref="F:System.Windows.Forms.View.LargeIcon" />.</exception>
    public ListViewItem? FindNearestItem(SearchDirectionHint searchDirection, int x, int y) =>
        _listView.FindNearestItem(searchDirection, x, y);

    /// <summary>Retrieves the item at the specified location.</summary>
    /// <param name="x">The x-coordinate of the location to search for an item (expressed in client coordinates).</param>
    /// <param name="y">The y-coordinate of the location to search for an item (expressed in client coordinates).</param>
    /// <returns>A <see cref="T:System.Windows.Forms.ListViewItem" /> that represents the item at the specified position. If there is no item at the specified location, the method returns <see langword="null" />.</returns>
    public ListViewItem? GetItemAt(int x, int y) => _listView.GetItemAt(x, y);

    /// <summary>Retrieves the bounding rectangle for a specific item within the list view control.</summary>
    /// <param name="index">The zero-based index of the item within the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> whose bounding rectangle you want to return.</param>
    /// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounding rectangle of the specified <see cref="T:System.Windows.Forms.ListViewItem" />.</returns>
    public Rectangle GetItemRect(int index) => this.GetItemRect(index, ItemBoundsPortion.Entire);

    /// <summary>Retrieves the specified portion of the bounding rectangle for a specific item within the list view control.</summary>
    /// <param name="index">The zero-based index of the item within the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> whose bounding rectangle you want to return.</param>
    /// <param name="portion">One of the <see cref="T:System.Windows.Forms.ItemBoundsPortion" /> values that represents a portion of the <see cref="T:System.Windows.Forms.ListViewItem" /> for which to retrieve the bounding rectangle.</param>
    /// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounding rectangle for the specified portion of the specified <see cref="T:System.Windows.Forms.ListViewItem" />.</returns>
    public Rectangle GetItemRect(int index, ItemBoundsPortion portion) => _listView.GetItemRect(index, portion);

    /// <summary>Provides item information, given a point.</summary>
    /// <param name="point">The <see cref="T:System.Drawing.Point" /> at which to retrieve the item information. The coordinates are relative to the upper-left corner of the control.</param>
    /// <returns>The item information, given a point.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">The point contains coordinates that are less than 0.</exception>
    public ListViewHitTestInfo HitTest(Point point) => this.HitTest(point.X, point.Y);

    /// <summary>Provides item information, given x- and y-coordinates.</summary>
    /// <param name="x">The x-coordinate at which to retrieve the item information. The coordinate is relative to the upper-left corner of the control.</param>
    /// <param name="y">The y-coordinate at which to retrieve the item information. The coordinate is relative to the upper-left corner of the control.</param>
    /// <returns>The item information, given x- and y- coordinates.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">The x- or y-coordinate is less than 0.</exception>
    public ListViewHitTestInfo HitTest(int x, int y) => _listView.HitTest(x, y);

    /// <summary>Forces a range of <see cref="T:System.Windows.Forms.ListViewItem" /> objects to be redrawn.</summary>
    /// <param name="startIndex">The index for the first item in the range to be redrawn.</param>
    /// <param name="endIndex">The index for the last item of the range to be redrawn.</param>
    /// <param name="invalidateOnly">
    /// <see langword="true" /> to invalidate the range of items; <see langword="false" /> to invalidate and repaint the items.</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///         <paramref name="startIndex" /> or <paramref name="endIndex" /> is less than 0, greater than or equal to the number of items in the <see cref="T:System.Windows.Forms.ListView" /> or, if in virtual mode, greater than the value of <see cref="P:System.Windows.Forms.ListView.VirtualListSize" />.
    /// -or-
    /// The given <paramref name="startIndex" /> is greater than the <paramref name="endIndex." /></exception>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public void RedrawItems(int startIndex, int endIndex, bool invalidateOnly) =>
        _listView.RedrawItems(startIndex, endIndex, invalidateOnly);

    /// <summary>Sorts the items of the list view.</summary>
    public void Sort() => _listView.Sort();

    /// <summary>Returns a string representation of the <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
    /// <returns>A string that states the control type, the count of items in the <see cref="T:System.Windows.Forms.ListView" /> control, and the type of the first item in the <see cref="T:System.Windows.Forms.ListView" />, if the count is not 0.</returns>
    public override string ToString() => _listView.ToString();

    /// <summary>
    /// Maintains performance while items are added to the ListBox one at a time by preventing the control from drawing until the EndUpdate method is called.
    /// </summary>
    public void BeginUpdate() => _listView.BeginUpdate();

    /// <summary>
    /// Resumes painting the ListBox control after painting is suspended by the BeginUpdate method.
    /// </summary>
    public void EndUpdate() => _listView.EndUpdate();

    /// <summary>Ensures that the specified item is visible within the control, scrolling the contents of the control if necessary.</summary>
    /// <param name="index">The zero-based index of the item to scroll into view.</param>
    public void EnsureVisible(int index) => _listView.EnsureVisible(index);

    /// <summary>
    /// Gets access to the common appearance entries that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTreeStateRedirect StateCommon { get; }

    private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

    /// <summary>
    /// Gets access to the disabled appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTreeState StateDisabled { get; }

    private bool ShouldSerializeStateDisabled() => !StateDisabled.IsDefault;

    /// <summary>
    /// Gets access to the normal appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTreeState StateNormal { get; }

    private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;

    /// <summary>
    /// Gets access to the active appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining active appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteDouble StateActive { get; }

    private bool ShouldSerializeStateActive() => !StateActive.IsDefault;

    /// <summary>
    /// Gets access to the hot tracking item appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining hot tracking item appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTreeNodeTriple StateTracking { get; }

    private bool ShouldSerializeStateTracking() => !StateTracking.IsDefault;

    /// <summary>
    /// Gets access to the normal checked item appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal checked item appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTreeNodeTriple StateCheckedNormal { get; }

    private bool ShouldSerializeStateCheckedNormal() => !StateCheckedNormal.IsDefault;

    /// <summary>
    /// Gets access to the hot tracking checked item appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining hot tracking checked item appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTreeNodeTriple StateCheckedTracking { get; }

    private bool ShouldSerializeStateCheckedTracking() => !StateCheckedTracking.IsDefault;

    /// <summary>
    /// Gets and sets Determines if the control is always active or only when the mouse is over the control or has focus.
    /// </summary>
    [Category(@"Visuals")]
    [Description(
        @"Determines if the control is always active or only when the mouse is over the control or has focus.")]
    [DefaultValue(false)]
    public bool AlwaysActive
    {
        get => _alwaysActive;

        set
        {
            if (_alwaysActive != value)
            {
                _alwaysActive = value;
                Invalidate();
            }
        }
    }

    /// <summary>
    /// Sets the fixed state of the control.
    /// </summary>
    /// <param name="active">Should the control be fixed as active.</param>
    public void SetFixedState(bool active) => _fixedActive = active;

    /// <summary>
    /// Gets a value indicating if the input control is active.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool IsActive => _fixedActive ?? DesignMode || AlwaysActive || ContainsFocus || _mouseOver;

    /// <summary>
    /// Sets input focus to the control.
    /// </summary>
    /// <returns>true if the input focus request was successful; otherwise, false.</returns>
    public new bool Focus() => _listView.Focus();

    /// <summary>
    /// Activates the control.
    /// </summary>
    public new void Select() => _listView.Select();

    #endregion public

    #region DrawItem and SubItem

    private void UpdateStateAndPalettes()
    {
        if (!IsDisposed)
        {
            // Find the new state of the main view element
            PaletteState state = Enabled
                ? (IsActive ? PaletteState.Tracking : PaletteState.Normal)
                : PaletteState.Disabled;
            // Get the correct palette settings to use
            var doubleState = GetDoubleState();
            _listView.ViewDrawPanel.SetPalettes(doubleState.PaletteBack);
            _drawDockerOuter.SetPalettes(doubleState.PaletteBack, doubleState.PaletteBorder!);
            _drawDockerOuter.Enabled = Enabled;
            _listView.ViewDrawPanel.ElementState = state;
            _drawDockerOuter.ElementState = state;

            _listView.BackColor = doubleState.PaletteBack.GetBackColor1(state);
            if (_listView.VirtualMode)
            {
                // Enumerating Items in virtual mode retrieves every row. Visible items are styled in OnRetrieveVirtualItem.
                _listView.Invalidate();
            }
            else
            {
                foreach (ListViewItem li in Items)
                {
                    SetItemState(li);
                }
            }
        }
    }

    private IPaletteDouble GetDoubleState() => Enabled ? (IsActive ? StateActive : StateNormal) : StateDisabled;

    /// <summary>
    /// Redraws the previous and current hot-tracking items after <see cref="InternalListView.MouseIndex"/> changes.
    /// </summary>
    internal void InvalidateTrackedItems(int oldIndex, int newIndex)
    {
        if (!_listView.IsHandleCreated)
        {
            return;
        }

        int count = VirtualMode ? VirtualListSize : Items.Count;
        try
        {
            if (oldIndex >= 0 && oldIndex < count)
            {
                _listView.RedrawItems(oldIndex, oldIndex, true);
            }

            if (newIndex >= 0 && newIndex < count && newIndex != oldIndex)
            {
                _listView.RedrawItems(newIndex, newIndex, true);
            }
        }
        catch (ArgumentException)
        {
            _listView.Invalidate();
        }
        catch (InvalidOperationException)
        {
            _listView.Invalidate();
        }

        UpdateItemToolTip(immediate: oldIndex >= 0 && newIndex >= 0);
    }

    private void HideItemToolTip()
    {
        _itemToolTipIndex = -1;
        _itemToolTip.HideFor(_listView);
        _itemToolTip.ClearPlacementRectangle(_listView);
        _itemToolTip.SetToolTip(_listView, string.Empty, string.Empty);
    }

    private void UpdateItemToolTip(bool immediate)
    {
        if (!_showItemToolTips || !Enabled)
        {
            HideItemToolTip();
            return;
        }

        ListViewItem? item = GetTrackedItem();
        if (item == null)
        {
            HideItemToolTip();
            return;
        }

        if (_itemToolTipIndex == item.Index && immediate)
        {
            return;
        }

        _itemToolTipIndex = item.Index;
        string title = item.Text ?? string.Empty;
        string description = item.ToolTipText ?? string.Empty;
        if (string.IsNullOrEmpty(description))
        {
            description = title;
            title = string.Empty;
        }

        if (string.IsNullOrEmpty(title) && string.IsNullOrEmpty(description))
        {
            HideItemToolTip();
            return;
        }

        try
        {
            Rectangle bounds = _listView.GetItemRect(item.Index, ItemBoundsPortion.Label);
            if (bounds.Width <= 0 || bounds.Height <= 0)
            {
                bounds = item.Bounds;
            }

            if (bounds.Width > 0 && bounds.Height > 0)
            {
                _itemToolTip.SetPlacementRectangle(_listView, bounds);
            }
            else
            {
                _itemToolTip.ClearPlacementRectangle(_listView);
            }
        }
        catch (ArgumentException)
        {
            _itemToolTip.ClearPlacementRectangle(_listView);
        }

        _itemToolTip.SetToolTip(_listView, title, description);
        _itemToolTip.ShowFor(_listView, immediate);
    }

    private ListViewItem? GetTrackedItem()
    {
        int index = _listView.MouseIndex;
        if (index < 0)
        {
            return null;
        }

        try
        {
            int count = VirtualMode ? VirtualListSize : Items.Count;
            if (index >= count)
            {
                return null;
            }

            return Items[index];
        }
        catch (ArgumentException)
        {
            return null;
        }
        catch (InvalidOperationException)
        {
            return null;
        }
    }

    private bool IsItemHot(ListViewItem item) =>
        Enabled && _listView.MouseIndex >= 0 && item.Index == _listView.MouseIndex;

    private bool IsItemDrawnSelected(ListViewItem item) =>
        item.Selected && (_listView.Focused || !HideSelection);

    private void ResolveItemPalette(bool isHot, bool isSelected, bool hasFocus,
        out IPaletteTriple nodeState, out PaletteState state)
    {
        _overrideNormal.Apply = hasFocus;
        _overrideTracking.Apply = hasFocus;
        _overrideCheckedTracking.Apply = hasFocus;
        _overrideCheckedNormal.Apply = hasFocus;

        if (!Enabled)
        {
            nodeState = _overrideDisabled;
            state = PaletteState.Disabled;
            return;
        }

        if (isSelected)
        {
            if (isHot)
            {
                nodeState = _overrideCheckedTracking;
                state = PaletteState.CheckedTracking;
            }
            else
            {
                nodeState = _overrideCheckedNormal;
                state = PaletteState.CheckedNormal;
            }
        }
        else if (isHot)
        {
            nodeState = _overrideTracking;
            state = PaletteState.Tracking;
        }
        else
        {
            nodeState = _overrideNormal;
            state = PaletteState.Normal;
        }
    }

    private void SetItemState(ListViewItem li)
    {
        ResolveItemPalette(IsItemHot(li), IsItemDrawnSelected(li), li.Focused,
            out IPaletteTriple nodeState, out PaletteState state);

        li.BackColor = nodeState.PaletteBack.GetBackColor1(state);
        li.ForeColor = nodeState.PaletteContent!.GetContentShortTextColor1(state);
        li.Font = nodeState.PaletteContent.GetContentShortTextFont(state) ?? Font;
    }

    private void OnListViewDrawItem(object? sender, DrawListViewItemEventArgs e)
    {
        e.DrawDefault = false;
        if (e.Item == null || View == View.Details)
        {
            return;
        }

        var isHot = IsItemHot(e.Item) || (e.State & ListViewItemStates.Hot) == ListViewItemStates.Hot;
        var isSelected = IsItemDrawnSelected(e.Item) ||
                         (e.State & ListViewItemStates.Selected) == ListViewItemStates.Selected;
        var hasFocus = (e.State & ListViewItemStates.Focused) == ListViewItemStates.Focused;
        ResolveItemPalette(isHot, isSelected, hasFocus, out IPaletteTriple nodeState, out PaletteState state);

        PaintItemChrome(e.Graphics, e.Bounds, nodeState, state);
        PaintItemForeground(e.Graphics, e.Item, e.Bounds, nodeState, state, -1);
    }

    private void OnListViewDrawSubItem(object? sender, DrawListViewSubItemEventArgs e)
    {
        e.DrawDefault = false;
        if (e.Item == null || View != View.Details)
        {
            return;
        }

        ResolveItemPalette(IsItemHot(e.Item), IsItemDrawnSelected(e.Item), e.Item.Focused,
            out IPaletteTriple nodeState, out PaletteState state);

        if (e.ColumnIndex == 0)
        {
            Rectangle chrome = FullRowSelect
                ? e.Item.Bounds
                : GetDetailsLabelChromeBounds(e.Item, e.Bounds);
            PaintItemChrome(e.Graphics, chrome, nodeState, state);
            PaintItemForeground(e.Graphics, e.Item, e.Bounds, nodeState, state, 0);
        }
        else
        {
            HorizontalAlignment align = e.Header?.TextAlign ?? HorizontalAlignment.Left;
            PaintCellText(e.Graphics, e.SubItem?.Text ?? string.Empty, e.Bounds, nodeState, state, align, false);
        }

        PaintGridLines(e.Graphics, e.Bounds);
    }

    private void OnListViewDrawColumnHeader(object? sender, DrawListViewColumnHeaderEventArgs e)
    {
        e.DrawDefault = true;
    }

    private static Rectangle GetDetailsLabelChromeBounds(ListViewItem item, Rectangle fallback)
    {
        try
        {
            Rectangle label = item.GetBounds(ItemBoundsPortion.Label);
            Rectangle icon = item.GetBounds(ItemBoundsPortion.Icon);
            if (label.Width <= 0 && icon.Width <= 0)
            {
                return fallback;
            }

            if (label.Width <= 0)
            {
                return icon;
            }

            return icon.Width <= 0 ? label : Rectangle.Union(label, icon);
        }
        catch (ArgumentException)
        {
            return fallback;
        }
    }

    private void PaintItemChrome(Graphics graphics, Rectangle bounds, IPaletteTriple nodeState, PaletteState state)
    {
        if (bounds.Width <= 0 || bounds.Height <= 0)
        {
            return;
        }

        try
        {
            using var context = new RenderContext(_listView, this, graphics, bounds, Renderer);
            if (nodeState.PaletteBack.GetBackDraw(state) == InheritBool.True)
            {
                GraphicsPath path = Renderer.RenderStandardBorder.GetBackPath(context, bounds,
                    nodeState.PaletteBorder!, VisualOrientation.Top, state);
                try
                {
                    IDisposable? backMemento = Renderer.RenderStandardBack.DrawBack(context, bounds, path,
                        nodeState.PaletteBack, VisualOrientation.Top, state, null);
                    backMemento?.Dispose();
                }
                finally
                {
                    path.Dispose();
                }
            }

            if (nodeState.PaletteBorder!.GetBorderDraw(state) == InheritBool.True)
            {
                Renderer.RenderStandardBorder.DrawBorder(context, bounds, nodeState.PaletteBorder,
                    VisualOrientation.Top, state);
            }
        }
        catch
        {
            Color back = nodeState.PaletteBack.GetBackColor1(state);
            if (!back.IsEmpty)
            {
                using var brush = new SolidBrush(back);
                graphics.FillRectangle(brush, bounds);
            }
        }
    }

    private void PaintItemForeground(Graphics graphics, ListViewItem item, Rectangle bounds,
        IPaletteTriple nodeState, PaletteState state, int columnIndex)
    {
        if (columnIndex <= 0)
        {
            DrawItemCheckOrStateImage(graphics, item, bounds);
            DrawItemImage(graphics, item);
        }

        Rectangle textBounds;
        HorizontalAlignment align;
        string text;
        var multiline = View is View.LargeIcon or View.Tile;
        try
        {
            if (columnIndex > 0)
            {
                textBounds = bounds;
                align = Columns.Count > columnIndex ? Columns[columnIndex].TextAlign : HorizontalAlignment.Left;
                text = columnIndex < item.SubItems.Count ? item.SubItems[columnIndex].Text : string.Empty;
            }
            else
            {
                textBounds = item.GetBounds(ItemBoundsPortion.Label);
                if (textBounds.Width <= 0 || textBounds.Height <= 0)
                {
                    textBounds = bounds;
                }

                align = View == View.Details && Columns.Count > 0
                    ? Columns[0].TextAlign
                    : (multiline ? HorizontalAlignment.Center : HorizontalAlignment.Left);
                text = item.Text;
            }
        }
        catch (ArgumentException)
        {
            textBounds = bounds;
            align = HorizontalAlignment.Left;
            text = item.Text;
        }

        PaintCellText(graphics, text, textBounds, nodeState, state, align, multiline);
    }

    private void PaintCellText(Graphics graphics, string text, Rectangle bounds, IPaletteTriple nodeState,
        PaletteState state, HorizontalAlignment alignment, bool multiline)
    {
        if (string.IsNullOrEmpty(text) || bounds.Width <= 0 || bounds.Height <= 0)
        {
            return;
        }

        Color fore = nodeState.PaletteContent!.GetContentShortTextColor1(state);
        Font font = nodeState.PaletteContent.GetContentShortTextFont(state) ?? Font;
        TextRenderer.DrawText(graphics, text, font, bounds, fore, GetTextFormatFlags(alignment, multiline));
    }

    private static TextFormatFlags GetTextFormatFlags(HorizontalAlignment alignment, bool multiline)
    {
        var flags = TextFormatFlags.EndEllipsis
                    | TextFormatFlags.NoPrefix
                    | TextFormatFlags.PreserveGraphicsClipping
                    | TextFormatFlags.PreserveGraphicsTranslateTransform
                    | TextFormatFlags.GlyphOverhangPadding;
        flags |= alignment switch
        {
            HorizontalAlignment.Center => TextFormatFlags.HorizontalCenter,
            HorizontalAlignment.Right => TextFormatFlags.Right,
            _ => TextFormatFlags.Left
        };
        return multiline
            ? flags | TextFormatFlags.WordBreak | TextFormatFlags.Top
            : flags | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine;
    }

    private void DrawItemCheckOrStateImage(Graphics graphics, ListViewItem item, Rectangle bounds)
    {
        if (StateImageList != null)
        {
            int index = item.StateImageIndex;
            if (index >= 0 && index < StateImageList.Images.Count)
            {
                var location = new Point(bounds.X + 2,
                    bounds.Y + Math.Max(0, (bounds.Height - StateImageList.ImageSize.Height) / 2));
                StateImageList.Draw(graphics, location, index);
            }

            return;
        }

        if (!CheckBoxes)
        {
            return;
        }

        CheckBoxState boxState = item.Checked
            ? (Enabled ? CheckBoxState.CheckedNormal : CheckBoxState.CheckedDisabled)
            : (Enabled ? CheckBoxState.UncheckedNormal : CheckBoxState.UncheckedDisabled);
        Size glyph = CheckBoxRenderer.GetGlyphSize(graphics, boxState);
        var origin = new Point(bounds.X + 2, bounds.Y + Math.Max(0, (bounds.Height - glyph.Height) / 2));
        CheckBoxRenderer.DrawCheckBox(graphics, origin, boxState);
    }

    private void DrawItemImage(Graphics graphics, ListViewItem item)
    {
        ImageList? list = View is View.LargeIcon or View.Tile ? LargeImageList : SmallImageList;
        if (list == null)
        {
            return;
        }

        int index = item.ImageIndex;
        if (index < 0 && !string.IsNullOrEmpty(item.ImageKey))
        {
            index = list.Images.IndexOfKey(item.ImageKey);
        }

        if (index < 0 || index >= list.Images.Count)
        {
            return;
        }

        try
        {
            Rectangle iconBounds = item.GetBounds(ItemBoundsPortion.Icon);
            if (iconBounds.Width > 0 && iconBounds.Height > 0)
            {
                list.Draw(graphics, iconBounds.Location, index);
            }
        }
        catch (ArgumentException)
        {
            // Virtual-mode items can report empty icon bounds before they are realised.
        }
    }

    private void PaintGridLines(Graphics graphics, Rectangle bounds)
    {
        if (!GridLines || View != View.Details || bounds.Width <= 0 || bounds.Height <= 0)
        {
            return;
        }

        Color line = StateNormal.Border.GetBorderColor1(Enabled ? PaletteState.Normal : PaletteState.Disabled);
        using var pen = new Pen(line);
        graphics.DrawLine(pen, bounds.Left, bounds.Bottom - 1, bounds.Right - 1, bounds.Bottom - 1);
        graphics.DrawLine(pen, bounds.Right - 1, bounds.Top, bounds.Right - 1, bounds.Bottom - 1);
    }

    #endregion

    #region Others Overrides

    /// <summary>
    /// Process Windows-based messages.
    /// </summary>
    /// <param name="m">A Windows-based message.</param>
    protected override void WndProc(ref Message m)
    {
        switch (m.Msg)
        {
            case PI.WM_.ERASEBKGND:
                // Do not draw the background here, always do it in the paint
                // instead to prevent flicker because of a two stage drawing process
                break;
            //case PI.WM_.PRINTCLIENT:
            //case PI.WM_.PAINT:
            //    WmPaint(ref m);
            //    break;
            case PI.WM_.VSCROLL:
            case PI.WM_.HSCROLL:
            case PI.WM_.MOUSEWHEEL:
                Invalidate();
                base.WndProc(ref m);
                break;
            //case PI.WM_.MOUSEMOVE:// TODO: On Mouse Enter ??
            //    if (!_mouseOver)
            //    {
            //        _mouseOver = true;
            //        Invalidate();
            //    }
            //    base.WndProc(ref m);
            //    break;
            // We need to snoop the need to show a context menu
            case PI.WM_.CONTEXTMENU:
                // Only interested in overriding the behaviour when we have a krypton context menu...
                if (KryptonContextMenu != null)
                {
                    // Extract the screen mouse position (if might not actually be provided)
                    var mousePt = new Point(PI.LOWORD(m.LParam), PI.HIWORD(m.LParam));

                    // If keyboard activated, the menu position is centered
                    if (((int)(long)m.LParam) == -1)
                    {
                        mousePt = new Point(Width / 2, Height / 2);
                    }
                    else
                    {
                        mousePt = PointToClient(mousePt);

                        // Mouse point up and left 1 pixel so that the mouse overlaps the top left corner
                        // of the showing context menu just like it happens for a ContextMenuStrip.
                        mousePt.X -= 1;
                        mousePt.Y -= 1;
                    }

                    // If the mouse position is within our client area
                    if (ClientRectangle.Contains(mousePt))
                    {
                        // Show the context menu
                        KryptonContextMenu.Show(this, PointToScreen(mousePt));
                    }
                }

                break;
            default:
                base.WndProc(ref m);
                break;
        }
    }

    private void OnListViewGotFocus(object? sender, EventArgs e)
    {
        UpdateStateAndPalettes();
        _listView.Invalidate();
        PerformNeedPaint(true);
        OnGotFocus(e);
    }

    private void OnListViewLostFocus(object? sender, EventArgs e)
    {
        UpdateStateAndPalettes();
        _listView.Invalidate();
        PerformNeedPaint(true);
        OnLostFocus(e);
    }

    /// <inheritdoc />
    protected override void OnEnabledChanged(EventArgs e)
    {
        UpdateStateAndPalettes();
        PerformNeedPaint(true);
        base.OnEnabledChanged(e);
    }

    /// <summary>
    /// Creates a new instance of the control collection for the KryptonTreeView.
    /// </summary>
    /// <returns>A new instance of Control.ControlCollection assigned to the control.</returns>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected override ControlCollection CreateControlsInstance() => new KryptonReadOnlyControls(this);

    /// <summary>
    /// Creates the accessibility object for the KryptonListView control.
    /// </summary>
    /// <returns>A new KryptonListViewAccessibleObject instance for the control.</returns>
    protected override AccessibleObject CreateAccessibilityInstance() => new KryptonListViewAccessibleObject(this);

    /// <summary>
    /// Raises the PaletteChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnPaletteChanged(EventArgs e)
    {
        _listView.Recreate();
        UpdateStateAndPalettes();
        _listView.Invalidate();
        base.OnPaletteChanged(e);
    }

    /// <summary>
    /// Raises the TabStop event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnTabStopChanged(EventArgs e)
    {
        _listView.TabStop = TabStop;
        base.OnTabStopChanged(e);
    }

    /// <summary>
    /// Raises the CausesValidationChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnCausesValidationChanged(EventArgs e)
    {
        _listView.CausesValidation = CausesValidation;
        base.OnCausesValidationChanged(e);
    }

    /// <summary>
    /// Raises the HandleCreated event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected override void OnHandleCreated(EventArgs e)
    {
        // Let base class do standard stuff
        base.OnHandleCreated(e);

        // Force the font to be set into the text box child control
        PerformNeedPaint(false);

        // We need a layout to occur before any painting
        InvokeLayout();
    }


    /// <summary>
    /// Raises the MouseDown event.
    /// </summary>
    /// <param name="e">A MouseEventArgs that contains the event data.</param>
    protected override void OnMouseDown(MouseEventArgs e)
    {
        _mouseOver = false;

        PerformNeedPaint(true);

        _listView.Invalidate();

        base.OnMouseDown(e);
    }

    /// <summary>
    /// Gets the default size of the control.
    /// </summary>
    protected override Size DefaultSize => new Size(120, 96);

    /// <inheritdoc/>>
    protected override void CreateHandle()
    {
        base.CreateHandle();

        PI.SetWindowTheme(Handle, @"DarkMode_Explorer", null);
    }

    /// <summary>
    /// Force the layout logic to size and position the controls.
    /// </summary>
    protected void ForceControlLayout()
    {
        if (!IsHandleCreated)
        {
            _forcedLayout = true;
            OnLayout(new LayoutEventArgs(null, null));
            _forcedLayout = false;
        }
    }

    /// <inheritdoc />
    /// <summary>
    /// Processes a notification from palette storage of a paint and optional layout required.
    /// </summary>
    /// <param name="sender">Source of notification.</param>
    /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
    protected override void OnNeedPaint(object? sender, NeedLayoutEventArgs e)
    {
        if (IsHandleCreated && !e.NeedLayout)
        {
            _listView.Invalidate();
        }
        else
        {
            ForceControlLayout();
        }

        // Update palette to reflect latest state
        UpdateStateAndPalettes();
        base.OnNeedPaint(sender, e);
    }

    /// <summary>
    /// Raises the Layout event.
    /// </summary>
    /// <param name="levent">An EventArgs that contains the event data.</param>
    protected override void OnLayout(LayoutEventArgs levent)
    {
        base.OnLayout(levent);

        // Only use layout logic if control is fully initialized or if being forced
        // to allow a relayout or if in design mode.
        if (IsHandleCreated || _forcedLayout || (DesignMode))
        {
            Rectangle fillRect = _layoutFill.FillRect;
            _listView.SetBounds(fillRect.X, fillRect.Y, fillRect.Width, fillRect.Height);
        }
    }

    /// <summary>
    /// Raises the MouseEnter event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnMouseEnter(EventArgs e)
    {
        _mouseOver = true;
        PerformNeedPaint(true);
        _listView.Invalidate();
        base.OnMouseEnter(e);
    }

    /// <summary>
    /// Raises the MouseLeave event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnMouseLeave(EventArgs e)
    {
        _mouseOver = false;
        PerformNeedPaint(true);
        _listView.Invalidate();
        base.OnMouseLeave(e);
    }

    /// <inheritdoc />
    protected override void OnNotifyMessage(Message m)
    {
        if (m.Msg != 0x14)
        {
            base.OnNotifyMessage(m);
        }
    }

    #endregion

    /// <summary>
    /// Gets and sets the background style.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Style used to draw the background.")]
    public PaletteBackStyle BackStyle
    {
        get => StateCommon.BackStyle;

        set
        {
            if (StateCommon.BackStyle != value)
            {
                StateCommon.BackStyle = value;
                PerformNeedPaint(true);
            }
        }
    }

    private void ResetBackStyle() => BackStyle = PaletteBackStyle.InputControlStandalone;

    private bool ShouldSerializeBackStyle() => BackStyle != PaletteBackStyle.InputControlStandalone;

    /// <summary>
    /// Gets and sets the border style.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Style used to draw the border.")]
    public PaletteBorderStyle BorderStyle
    {
        get => StateCommon.BorderStyle;

        set
        {
            if (StateCommon.BorderStyle != value)
            {
                StateCommon.BorderStyle = value;
                PerformNeedPaint(true);
            }
        }
    }

    private void ResetBorderStyle() => BorderStyle = PaletteBorderStyle.InputControlStandalone;

    private bool ShouldSerializeBorderStyle() => BorderStyle != PaletteBorderStyle.InputControlStandalone;

    /// <summary>
    /// Gets access to the item appearance when it has focus.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining item appearance when it has focus.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTreeNodeTripleRedirect OverrideFocus { get; }

    private bool ShouldSerializeOverrideFocus() => !OverrideFocus.IsDefault;

}
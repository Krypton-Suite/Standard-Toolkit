#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2017 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Provide a TreeView with Krypton styling applied.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonTreeView), "ToolboxBitmaps.KryptonTreeView.bmp")]
[DefaultEvent(nameof(AfterSelect))]
[DefaultProperty(nameof(Nodes))]
[Designer(typeof(KryptonTreeViewDesigner))]
[DesignerCategory(@"code")]
[Description(@"Displays a hierarchical collection of labeled items, each represented by a TreeNode")]
[Docking(DockingBehavior.Ask)]
public class KryptonTreeView : VisualControlBase,
    IContainedInputControl
{
    #region Classes
    private class InternalTreeView : TreeView
    {
        #region Static Fields
        private static MethodInfo? _miRI;
        #endregion

        #region Instance Fields
        private readonly ViewManager? _viewManager;
        private readonly KryptonTreeView _kryptonTreeView;
        private readonly IntPtr _screenDC;
        private bool _mouseOver;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the mouse enters the InternalTreeView.
        /// </summary>
        public event EventHandler? TrackMouseEnter;

        /// <summary>
        /// Occurs when the mouse leaves the InternalTreeView.
        /// </summary>
        public event EventHandler? TrackMouseLeave;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the InternalTreeView class.
        /// </summary>
        /// <param name="kryptonTreeView">Reference to owning control.</param>
        public InternalTreeView(KryptonTreeView kryptonTreeView)
        {
            SetStyle(ControlStyles.ResizeRedraw, true);

            _kryptonTreeView = kryptonTreeView;

            // Create manager and view for drawing the background
            ViewDrawPanel = new ViewDrawPanel();
            _viewManager = new ViewManager(this, ViewDrawPanel);

            // ReSharper disable RedundantBaseQualifier
            // Set required properties to act as an owner draw list box
            base.Size = Size.Empty;
            base.BorderStyle = BorderStyle.None;
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
                    }
                }
            }
        }

        public void ResetIndent()
        {
            // Only grab the required reference once
            if (_miRI is null)
            {
                // Use reflection so we can call the TreeView private method
                _miRI = typeof(TreeView).GetMethod(nameof(ResetIndent),
                    BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod,
                    null, CallingConventions.HasThis,
                    Array.Empty<Type>(), null);
            }

            _miRI!.Invoke(this, Array.Empty<object>());
        }
        #endregion

        #region Protected
        /// <summary>
        /// Raises the Layout event.
        /// </summary>
        /// <param name="levent">A LayoutEventArgs containing the event data.</param>
        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);

            // Ask the panel to layout given our available size
            using var context = new ViewLayoutContext(_viewManager, this, _kryptonTreeView, _kryptonTreeView.Renderer);
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
                        _kryptonTreeView.PerformNeedPaint(true);
                        Invalidate();
                    }
                    base.WndProc(ref m);
                    break;
                case PI.WM_.MOUSEMOVE:
                    if (!MouseOver)
                    {
                        MouseOver = true;
                        _kryptonTreeView.PerformNeedPaint(true);
                        Invalidate();
                    }
                    base.WndProc(ref m);
                    break;
                case PI.WM_.CREATE:
                    _kryptonTreeView._isRecreating = true;
                    base.WndProc(ref m);
                    _kryptonTreeView._isRecreating = false;
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
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
        #endregion

        #region Private
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
                            using (var context = new ViewLayoutContext(this, _kryptonTreeView.Renderer))
                            {
                                context.DisplayRectangle = realRect;
                                ViewDrawPanel.Layout(context);
                            }

                            using (var context = new RenderContext(this, _kryptonTreeView, g, realRect,
                                       _kryptonTreeView.Renderer))
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
    private readonly PaletteTripleOverride _overrideTracking;
    private readonly PaletteTripleOverride _overrideMultiSelect;
    private readonly PaletteTripleOverride _overrideCheckedNormal;
    private readonly PaletteTripleOverride _overrideCheckedTracking;
    private readonly PaletteTripleOverride _overrideCheckedMultiSelect;
    private readonly PaletteNodeOverride _overrideNormalNode;
    private readonly PaletteRedirectTreeView? _redirectImages;
    private readonly ViewDrawDocker _drawDockerOuter;
    private readonly ViewLayoutFill _layoutFill;
    private readonly ViewDrawButton _drawButton;
    private readonly ViewDrawCheckBox _drawCheckBox;
    private readonly ViewLayoutStack _layoutCheckBoxStack;
    private readonly ViewLayoutDocker _layoutDocker;
    private readonly ViewLayoutStack _layoutImageStack;
    private readonly ViewLayoutCenter _layoutImageCenterState;
    private readonly ViewLayoutSeparator _layoutImage;
    private readonly ViewLayoutSeparator _layoutImageState;
    private readonly InternalTreeView _treeView;
    private readonly FixedContentValue? _contentValues;
    private bool? _fixedActive;
    private ButtonStyle _style;
    private readonly IntPtr _screenDC;
    private bool _itemHeightDefault;
    private bool _mouseOver;
    private bool _alwaysActive;
    private bool _forcedLayout;
    private bool _trackingMouseEnter;
    private bool _isRecreating; // https://github.com/Krypton-Suite/Standard-Toolkit/issues/777
    private bool _multiSelect;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when a checkbox has been checked or unchecked.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when a checkbox has been checked or unchecked.")]
    public event TreeViewEventHandler? AfterCheck;

    /// <summary>
    /// Occurs when a node has been collapsed.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when a node has been collapsed.")]
    public event TreeViewEventHandler? AfterCollapse;

    /// <summary>
    /// Occurs when a node has been expanded.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when a node has been expanded.")]
    public event TreeViewEventHandler? AfterExpand;

    /// <summary>
    /// Occurs when the text of node has been edited by the user.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when the text of node has been edited by the user.")]
    public event NodeLabelEditEventHandler? AfterLabelEdit;

    /// <summary>
    /// Occurs when the selected has been changed.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when the selection has been changed.")]
    public event TreeViewEventHandler? AfterSelect;

    /// <summary>
    /// Occurs when a checkbox is about to be checked or unchecked.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when a checkbox is about to be checked or unchecked.")]
    public event TreeViewCancelEventHandler? BeforeCheck;

    /// <summary>
    /// Occurs when a node is about to be collapsed.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when a node is about to be collapsed.")]
    public event TreeViewCancelEventHandler? BeforeCollapse;

    /// <summary>
    /// Occurs when a node is about to be expanded.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when a node is about to be expanded.")]
    public event TreeViewCancelEventHandler? BeforeExpand;

    /// <summary>
    /// Occurs when the text of node is about to be edited by the user.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when the text of node is about to be edited by the user.")]
    public event NodeLabelEditEventHandler? BeforeLabelEdit;

    /// <summary>
    /// Occurs when the selection is about to be changed.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when the selection is about to be changed.")]
    public event TreeViewCancelEventHandler? BeforeSelect;

    /// <summary>
    /// Occurs when the user begins dragging an item.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the user begins dragging an item.")]
    public event ItemDragEventHandler? ItemDrag;

    /// <summary>
    /// Occurs when a node is clicked with the mouse.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when a node is clicked with the mouse.")]
    public event TreeNodeMouseClickEventHandler? NodeMouseClick;

    /// <summary>
    /// Occurs when a node is double-clicked with the mouse.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when a node is double clicked with the mouse.")]
    public event TreeNodeMouseClickEventHandler? NodeMouseDoubleClick;

    /// <summary>
    /// Occurs when the mouse hovers over a node.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the mouse hovers over a node.")]
    public event TreeNodeMouseHoverEventHandler? NodeMouseHover;

    /// <summary>
    /// Occurs when the value of the RightToLeftLayout property changes.
    /// </summary>
    [Category(@"PropertyChanged")]
    [Description(@"Occurs when the value of the RightToLeftLayout property changes.")]
    public event EventHandler? RightToLeftLayoutChanged;

    /// <summary>
    /// Occurs when the value of the BackColor property changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler? BackColorChanged;

    /// <summary>
    /// Occurs when the value of the BackgroundImage property changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler? BackgroundImageChanged;

    /// <summary>
    /// Occurs when the value of the BackgroundImageLayout property changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler? BackgroundImageLayoutChanged;

    /// <summary>
    /// Occurs when the value of the ForeColor property changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler? ForeColorChanged;

    /// <summary>
    /// Occurs when the value of the MouseClick property changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler? PaddingChanged;

    /// <summary>
    /// Occurs when the value of the MouseClick property changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event PaintEventHandler? Paint;

    /// <summary>
    /// Occurs when the value of the TextChanged property changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler? TextChanged;

    /// <summary>
    /// Occurs when the mouse enters the control.
    /// </summary>
    [Description(@"Raises the TrackMouseEnter event in the wrapped control.")]
    [Category(@"Mouse")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public event EventHandler? TrackMouseEnter;

    /// <summary>
    /// Occurs when the mouse leaves the control.
    /// </summary>
    [Description(@"Raises the TrackMouseLeave event in the wrapped control.")]
    [Category(@"Mouse")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public event EventHandler? TrackMouseLeave;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonTreeView class.
    /// </summary>
    public KryptonTreeView()
    {
        // Contains another control and needs marking as such for validation to work
        SetStyle(ControlStyles.ContainerControl, true);

        // Cannot select this control, only the child tree view and does not generate a click event
        SetStyle(ControlStyles.Selectable | ControlStyles.StandardClick, false);

        // Default fields
        _alwaysActive = true;
        _style = ButtonStyle.ListItem;
        _itemHeightDefault = true;
        PlusMinusImages = new TreeViewImages();
        CheckBoxImages = new CheckBoxImages();
        base.Padding = new Padding(1);

        // Create the palette storage
        _redirectImages = new PaletteRedirectTreeView(Redirector, PlusMinusImages, CheckBoxImages);
        var backInherit = new PaletteBackInheritRedirect(Redirector, PaletteBackStyle.InputControlStandalone);
        var borderInherit = new PaletteBorderInheritRedirect(Redirector, PaletteBorderStyle.InputControlStandalone);
        var commonBack = new PaletteBackColor1(backInherit, NeedPaintDelegate);
        var commonBorder = new PaletteBorder(borderInherit, NeedPaintDelegate);
        StateCommon = new PaletteTreeStateRedirect(Redirector, commonBack, backInherit, commonBorder, borderInherit, NeedPaintDelegate);

        var disabledBack = new PaletteBackColor1(StateCommon.PaletteBack, NeedPaintDelegate);
        var disabledBorder = new PaletteBorder(StateCommon.PaletteBorder!, NeedPaintDelegate);
        StateDisabled = new PaletteTreeState(StateCommon, disabledBack, disabledBorder, NeedPaintDelegate);

        var normalBack = new PaletteBackColor1(StateCommon.PaletteBack, NeedPaintDelegate);
        var normalBorder = new PaletteBorder(StateCommon.PaletteBorder!, NeedPaintDelegate);
        StateNormal = new PaletteTreeState(StateCommon, normalBack, normalBorder, NeedPaintDelegate);

        var activeBack = new PaletteBackColor1(StateCommon.PaletteBack, NeedPaintDelegate);
        var activeBorder = new PaletteBorder(StateCommon.PaletteBorder!, NeedPaintDelegate);
        StateActive = new PaletteDouble(StateCommon, activeBack, activeBorder, NeedPaintDelegate);

        OverrideFocus = new PaletteTreeNodeTripleRedirect(Redirector, PaletteBackStyle.ButtonListItem, PaletteBorderStyle.ButtonListItem, PaletteContentStyle.ButtonListItem, NeedPaintDelegate);
        StateTracking = new PaletteTreeNodeTriple(StateCommon.Node, NeedPaintDelegate);
        StateMultiSelect = new PaletteTreeNodeTriple(StateCommon.Node, NeedPaintDelegate);
        StateCheckedNormal = new PaletteTreeNodeTriple(StateCommon.Node, NeedPaintDelegate);
        StateCheckedTracking = new PaletteTreeNodeTriple(StateCommon.Node, NeedPaintDelegate);
        StateCheckedMultiSelect = new PaletteTreeNodeTriple(StateCommon.Node, NeedPaintDelegate);

        // Create the override handling classes
        _overrideNormal = new PaletteTripleOverride(OverrideFocus.Node, StateNormal.Node, PaletteState.FocusOverride);
        _overrideTracking = new PaletteTripleOverride(OverrideFocus.Node, StateTracking.Node, PaletteState.FocusOverride);
        _overrideMultiSelect = new PaletteTripleOverride(OverrideFocus.Node, StateMultiSelect.Node, PaletteState.FocusOverride);
        _overrideCheckedNormal = new PaletteTripleOverride(OverrideFocus.Node, StateCheckedNormal.Node, PaletteState.FocusOverride);
        _overrideCheckedTracking = new PaletteTripleOverride(OverrideFocus.Node, StateCheckedTracking.Node, PaletteState.FocusOverride);
        _overrideCheckedMultiSelect = new PaletteTripleOverride(OverrideFocus.Node, StateCheckedMultiSelect.Node, PaletteState.FocusOverride);
        _overrideNormalNode = new PaletteNodeOverride(_overrideNormal);

        // Create the checkbox image drawer and place inside element so it is always centered
        _drawCheckBox = new ViewDrawCheckBox(_redirectImages);
        var layoutCheckBox = new ViewLayoutCenter
        {
            _drawCheckBox
        };
        var layoutCheckBoxAfter = new ViewLayoutSeparator(3, 0);
        _layoutCheckBoxStack = new ViewLayoutStack(true)
        {
            layoutCheckBox,
            layoutCheckBoxAfter
        };

        // Stack used to layout the location of the node image
        _layoutImage = new ViewLayoutSeparator(0, 0);
        var layoutImageAfter = new ViewLayoutSeparator(3, 0);
        var layoutImageCenter = new ViewLayoutCenter(_layoutImage);
        _layoutImageStack = new ViewLayoutStack(true)
        {
            layoutImageCenter,
            layoutImageAfter
        };
        _layoutImageState = new ViewLayoutSeparator(16, 16);
        _layoutImageCenterState = new ViewLayoutCenter(_layoutImageState);

        // Create the draw element for owner drawing individual items
        _contentValues = new FixedContentValue();
        _drawButton = new ViewDrawButton(StateDisabled.Node, _overrideNormalNode,
            _overrideTracking, _overrideMultiSelect,
            _overrideCheckedNormal, _overrideCheckedTracking,
            _overrideCheckedMultiSelect,
            new PaletteMetricRedirect(Redirector),
            _contentValues, VisualOrientation.Top, false);

        // Place check box on the left and the label in the remainder
        _layoutDocker = new ViewLayoutDocker
        {
            { _layoutImageStack, ViewDockStyle.Left },
            { _layoutImageCenterState, ViewDockStyle.Left },
            { _layoutCheckBoxStack, ViewDockStyle.Left },
            { _drawButton, ViewDockStyle.Fill }
        };

        // Create the internal tree view used for containing content
        _treeView = new InternalTreeView(this);
        _treeView.DoubleClick += OnDoubleClick;
        _treeView.TrackMouseEnter += OnTreeViewMouseChange;
        _treeView.TrackMouseLeave += OnTreeViewMouseChange;
        _treeView.GotFocus += OnTreeViewGotFocus;
        _treeView.LostFocus += OnTreeViewLostFocus;
        _treeView.MouseDoubleClick += OnMouseDoubleClick;
        _treeView.KeyDown += OnTreeViewKeyDown;
        _treeView.KeyUp += OnTreeViewKeyUp;
        _treeView.KeyPress += OnTreeViewKeyPress;
        _treeView.PreviewKeyDown += OnTreeViewPreviewKeyDown;
        _treeView.Validating += OnTreeViewValidating;
        _treeView.Validated += OnTreeViewValidated;
        _treeView.AfterCheck += OnTreeViewAfterCheck;
        _treeView.AfterCollapse += OnTreeViewAfterCollapse;
        _treeView.AfterExpand += OnTreeViewAfterExpand;
        _treeView.AfterLabelEdit += OnTreeViewAfterLabelEdit;
        _treeView.AfterSelect += OnTreeViewAfterSelect;
        _treeView.BeforeCheck += OnTreeViewBeforeCheck;
        _treeView.BeforeCollapse += OnTreeViewBeforeCollapse;
        _treeView.BeforeExpand += OnTreeViewBeforeExpand;
        _treeView.BeforeLabelEdit += OnTreeViewBeforeLabelEdit;
        _treeView.BeforeSelect += OnTreeViewBeforeSelect;
        _treeView.ItemDrag += OnTreeViewItemDrag;
        _treeView.NodeMouseClick += OnTreeViewNodeMouseClick;
        _treeView.NodeMouseDoubleClick += OnTreeViewNodeMouseDoubleClick;
        _treeView.NodeMouseHover += OnTreeViewNodeMouseHover;
        _treeView.DrawNode += OnTreeViewDrawNode;
        _treeView.DrawMode = TreeViewDrawMode.OwnerDrawAll;
        _treeView.Click += OnTreeClick;  // SKC: make sure that the default click is also routed.

        // Create the element that fills the remainder space and remembers fill rectangle
        _layoutFill = new ViewLayoutFill(_treeView)
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
        ((KryptonReadOnlyControls)Controls).AddInternal(_treeView);
    }

    private void OnTreeClick(object? sender, EventArgs e) => OnClick(e);

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
    /// Gets access to the contained TreeView instance.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Browsable(false)]
    public TreeView TreeView => _treeView;

    /// <summary>
    /// Gets access to the contained input control.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Browsable(false)]
    public Control ContainedControl => TreeView;

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

    /// <summary>
    /// Gets or sets the height of each tree node in the tree view control.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The height of every node in the control.")]
    public int ItemHeight
    {
        get => _treeView.ItemHeight;

        set
        {
            if (_treeView.ItemHeight != value)
            {
                _itemHeightDefault = false;
                _treeView.ItemHeight = value;
            }
        }
    }
    private bool ShouldSerializeItemHeight() => !_itemHeightDefault;
    private void ResetItemHeight()
    {
        _itemHeightDefault = true;
        UpdateItemHeight();
    }

    /// <summary>
    /// Gets or sets a value indicating whether check boxes are Displayed next to the tree nodes in the tree view control.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Indicates whether check boxes are Displayed next to nodes")]
    [DefaultValue(false)]
    public bool CheckBoxes
    {
        get => _treeView.CheckBoxes;
        set => _treeView.CheckBoxes = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether check boxes are Displayed next to the tree nodes in the tree view control.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Indicates whether 'MultiSelect' is implemented on Selection")]
    [DefaultValue(false)]
    public bool MultiSelect
    {
        get => _multiSelect || CheckBoxes;
        set
        {
            _multiSelect = value;
            // Force redraw of current options
            var checkedNodes = CheckedNodes;
            CheckedNodes = checkedNodes;
        }
    }

    private bool ShouldSerializeMultiSelect() => _multiSelect;
    private void ResetMultiSelect() => _multiSelect = false;

    /// <summary>
    /// Gets or sets a value indicating whether the selection highlight spans the width of the tree view control.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether the selection highlight spans the width of the control.")]
    [DefaultValue(false)]
    public bool FullRowSelect
    {
        get => _treeView.FullRowSelect;
        set => _treeView.FullRowSelect = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the selected tree node remains highlighted even when the tree view has lost the focus.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Removes highlight from the control when it no longer has focus.")]
    [DefaultValue(true)]
    public bool HideSelection
    {
        get => _treeView.HideSelection;
        set => _treeView.HideSelection = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether a tree node label takes on the appearance of a hyperlink as the mouse pointer passes over it.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates if the node gives feedback as the mouse moves over them.")]
    [DefaultValue(false)]
    public bool HotTracking
    {
        get => _treeView.HotTracking;
        set => _treeView.HotTracking = value;
    }

    /// <summary>
    /// Gets or sets the image-list index value of the default image that is Displayed by the tree nodes.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The default image index for nodes.")]
    [Localizable(true)]
    [TypeConverter(typeof(NoneExcludedImageIndexConverter))]
    [Editor(@"System.Windows.Forms.Design.ImageIndexEditor", typeof(UITypeEditor))]
    [RefreshProperties(RefreshProperties.Repaint)]
    [RelatedImageList(nameof(ImageList))]
    [DefaultValue(-1)]
    public int ImageIndex
    {
        get => _treeView.ImageIndex;
        set => _treeView.ImageIndex = value;
    }

    /// <summary>
    /// Gets or sets the key of the default image for each node in the TreeView control when it is in an unselected state.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The default image key for the nodes.")]
    [Localizable(true)]
    [TypeConverter(typeof(ImageKeyConverter))]
    [Editor(@"System.Windows.Forms.Design.ImageIndexEditor", typeof(UITypeEditor))]
    [RefreshProperties(RefreshProperties.Repaint)]
    [RelatedImageList(nameof(ImageList))]
    [DefaultValue("")]
    public string ImageKey
    {
        get => _treeView.ImageKey;
        set => _treeView.ImageKey = value;
    }

    /// <summary>
    /// Gets or sets the ImageList that contains the Image objects that are used by the tree nodes.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The ImageList control from which nodes images are taken.")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [DefaultValue(null)]
    public ImageList? ImageList
    {
        get => _treeView.ImageList;
        set => _treeView.ImageList = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the label text of the tree nodes can be edited.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether the user can edit the label of nodes.")]
    [DefaultValue(false)]
    public bool LabelEdit
    {
        get => _treeView.LabelEdit;
        set => _treeView.LabelEdit = value;
    }

    /// <summary>
    /// Gets or sets the delimiter string that the tree node path uses.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The delimitor used for separating nodes with the FullPath property.")]
    [DefaultValue(@"\")]
    public string PathSeparator
    {
        get => _treeView.PathSeparator;
        set => _treeView.PathSeparator = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the tree view control displays scroll bars when they are needed.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether the control displays scroll bars when they are needed.")]
    [DefaultValue(true)]
    public bool Scrollable
    {
        get => _treeView.Scrollable;
        set => _treeView.Scrollable = value;
    }

    /// <summary>
    /// Gets or sets the image list index value of the image that is Displayed when a tree node is selected.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The default image index for selected nodes.")]
    [Localizable(true)]
    [TypeConverter(typeof(NoneExcludedImageIndexConverter))]
    [Editor(@"System.Windows.Forms.Design.ImageIndexEditor", typeof(UITypeEditor))]
    [RelatedImageList(nameof(ImageList))]
    [DefaultValue(-1)]
    public int SelectedImageIndex
    {
        get => _treeView.SelectedImageIndex;
        set => _treeView.SelectedImageIndex = value;
    }

    /// <summary>
    /// Gets or sets the key of the default image shown when a TreeNode is in a selected state.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The default image for selected nodes.")]
    [Localizable(true)]
    [TypeConverter(typeof(ImageKeyConverter))]
    [Editor(@"System.Windows.Forms.Design.ImageIndexEditor", typeof(UITypeEditor))]
    [RelatedImageList(nameof(ImageList))]
    [RefreshProperties(RefreshProperties.Repaint)]
    [DefaultValue("")]
    public string SelectedImageKey
    {
        get => _treeView.SelectedImageKey;
        set => _treeView.SelectedImageKey = value;
    }

    /// <summary>
    /// Gets or sets the tree node that is currently selected in the tree view control.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Node that is currently selected.")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public TreeNode? SelectedNode
    {
        get => _treeView.SelectedNode;
        set => _treeView.SelectedNode = value;
    }

    /// <summary>
    /// Gets or sets the tree node that is currently selected in the tree view control.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Node(s) that have check set; Will used in MultiSelect as well.")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public List<TreeNode> CheckedNodes
    {
        get => TreeView.Nodes.Cast<TreeNode>().Where(node => node.Checked).ToList();
        set
        {
            foreach (TreeNode node in TreeView.Nodes)
            {
                node.Checked = false;
            }

            foreach (TreeNode node in value)
            {
                node.Checked = true;
                if (!MultiSelect)
                {
                    // Only do the first one !
                    break;
                }
            }
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether lines are drawn between tree nodes in the tree view control.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Indicates whether lines are drawn between sibling and parent/child nodes.")]
    [DefaultValue(true)]
    public bool ShowLines
    {
        get => _treeView.ShowLines;
        set => _treeView.ShowLines = value;
    }

    /// <summary>
    /// Gets or sets a value indicating ToolTips are shown when the mouse pointer hovers over a TreeNode.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether ToolTips are Displayed for the nodes.")]
    [DefaultValue(false)]
    public bool ShowNodeToolTips
    {
        get => _treeView.ShowNodeToolTips;
        set => _treeView.ShowNodeToolTips = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether plus-sign (+) and minus-sign (-) buttons are Displayed next to tree nodes that contain child tree nodes.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether plus/minus nodes are drawn next to parent nodes.")]
    [DefaultValue(true)]
    public bool ShowPlusMinus
    {
        get => _treeView.ShowPlusMinus;
        set => _treeView.ShowPlusMinus = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether lines are drawn between the tree nodes that are at the root of the tree view.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether lines are shown between root nodes.")]
    [DefaultValue(true)]
    public bool ShowRootLines
    {
        get => _treeView.ShowRootLines;
        set => _treeView.ShowRootLines = value;
    }

    /// <summary>
    /// Gets or sets the image list that is used to indicate the state of the TreeView and its nodes.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The ImageList used by the control for custom states.")]
    [DefaultValue(null)]
    public ImageList? StateImageList
    {
        get => _treeView.StateImageList;
        set => _treeView.StateImageList = value;
    }

    /// <summary>
    /// Gets or sets the first fully-visible tree node in the tree view control.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"First fully-visible node.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public TreeNode? TopNode
    {
        get => _treeView.TopNode;
        set => _treeView.TopNode = value;
    }

    /// <summary>
    /// Gets or sets the implementation of IComparer to perform a custom sort of the TreeView nodes.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"IComparer used to perform custom sorting.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public IComparer? TreeViewNodeSorter
    {
        get => _treeView.TreeViewNodeSorter;
        set => _treeView.TreeViewNodeSorter = value;
    }

    /// <summary>
    /// Gets the number of tree nodes that can be fully visible in the tree view control.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Returns number of visible nodes in the control.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public int VisibleCount => _treeView.VisibleCount;

    /// <summary>
    /// Indicates whether the control layout is right-to-left when the RightToLeft property is True.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Indicates whether the control layout is right-to-left when the RightToLeft property is True.")]
    [DefaultValue(false)]
    [RefreshProperties(RefreshProperties.Repaint)]
    public bool RightToLeftLayout
    {
        get => _treeView.RightToLeftLayout;
        set => _treeView.RightToLeftLayout = value;
    }

    /// <summary>
    /// Gets the collection of tree nodes that are assigned to the tree view control.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The root nodes in the KryptonTreeView control.")]
    [Editor(@"System.Windows.Forms.Design.TreeNodeCollectionEditor", typeof(UITypeEditor))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [MergableProperty(false)]
    [Localizable(true)]
    public TreeNodeCollection Nodes => _treeView.Nodes;

    /// <summary>
    /// Gets and sets the item style.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Item style.")]
    public ButtonStyle ItemStyle
    {
        get => _style;

        set
        {
            if (_style != value)
            {
                _style = value;
                StateCommon.Node.SetStyles(_style);
                OverrideFocus.Node.SetStyles(_style);
                _treeView.Recreate();
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeItemStyle() => ItemStyle != ButtonStyle.ListItem;

    private void ResetItemStyle() => ItemStyle = ButtonStyle.ListItem;

    /// <summary>
    /// Gets or sets a value indicating whether the items in the KryptonTreeView are sorted alphabetically.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Controls whether the list is sorted.")]
    [DefaultValue(false)]
    public bool Sorted
    {
        get => _treeView.Sorted;
        set => _treeView.Sorted = value;
    }

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
                _treeView.Recreate();
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeBackStyle() => BackStyle != PaletteBackStyle.InputControlStandalone;

    private void ResetBackStyle() => BackStyle = PaletteBackStyle.InputControlStandalone;

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
                _treeView.Recreate();
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeBorderStyle() => BorderStyle != PaletteBorderStyle.InputControlStandalone;

    private void ResetBorderStyle() => BorderStyle = PaletteBorderStyle.InputControlStandalone;

    /// <summary>
    /// Gets access to the plus/minus image value overrides.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Plus/minus image value overrides.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public TreeViewImages PlusMinusImages { get; }

    private bool ShouldSerializePlusMinusImages() => !PlusMinusImages.IsDefault;

    /// <summary>
    /// Gets access to the checkbox image value overrides.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"CheckBox image value overrides.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public CheckBoxImages CheckBoxImages { get; }

    private bool ShouldSerializeCheckBoxImages() => !CheckBoxImages.IsDefault;

    /// <summary>
    /// Gets access to the item appearance when it has focus.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining item appearance when it has focus.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTreeNodeTripleRedirect OverrideFocus { get; }

    private bool ShouldSerializeOverrideFocus() => !OverrideFocus.IsDefault;

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
    /// Gets access to the pressed item appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining (Multi) Select item appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTreeNodeTriple StateMultiSelect { get; }

    private bool ShouldSerializeStateMultiSelect() => !StateMultiSelect.IsDefault;

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
    /// Gets access to the pressed checked item appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining (Multi) Select checked item appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTreeNodeTriple StateCheckedMultiSelect { get; }

    private bool ShouldSerializeStateCheckedMultiSelect() => !StateCheckedMultiSelect.IsDefault;

    /// <summary>
    /// Gets and sets Determines if the control is always active or only when the mouse is over the control or has focus.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Determines if the control is always active or only when the mouse is over the control or has focus.")]
    [DefaultValue(true)]
    public bool AlwaysActive
    {
        get => _alwaysActive;

        set
        {
            if (_alwaysActive != value)
            {
                _alwaysActive = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Collapses all the tree nodes.
    /// </summary>
    public void CollapseAll() => _treeView.CollapseAll();

    /// <summary>
    /// Expands all the tree nodes.
    /// </summary>
    public void ExpandAll() => _treeView.ExpandAll();

    /// <summary>
    /// Sorts the items in KryptonTreeView control.
    /// </summary>
    public void Sort() => _treeView.Sort();

    /// <summary>
    /// Maintains performance while items are added to the TreeView one at a time by preventing the control from drawing until the EndUpdate method is called.
    /// </summary>
    public void BeginUpdate() => _treeView.BeginUpdate();

    /// <summary>
    /// Resumes painting the TreeView control after painting is suspended by the BeginUpdate method.
    /// </summary>
    public void EndUpdate() => _treeView.EndUpdate();

    /// <summary>
    /// Retrieves the tree node that is at the specified point.
    /// </summary>
    /// <param name="pt">The Point to evaluate and retrieve the node from. </param>
    /// <returns>The TreeNode at the specified point, in tree view (client) coordinates, or null if there is no node at that location.</returns>
    public TreeNode? GetNodeAt(Point pt) => _treeView.GetNodeAt(pt);

    /// <summary>
    /// Retrieves the tree node at the point with the specified coordinates.
    /// </summary>
    /// <param name="x">The X position to evaluate and retrieve the node from.</param>
    /// <param name="y">The Y position to evaluate and retrieve the node from.</param>
    /// <returns>The TreeNode at the specified location, in tree view (client) coordinates, or null if there is no node at that location.</returns>
    public TreeNode? GetNodeAt(int x, int y) => _treeView.GetNodeAt(x, y);

    /// <summary>
    /// Retrieves the number of tree nodes, optionally including those in all subtrees, assigned to the tree view control.
    /// </summary>
    /// <param name="includeSubTrees">true to count the TreeNode items that the subtrees contain; otherwise, false.</param>
    /// <returns>The number of tree nodes, optionally including those in all subtrees, assigned to the control.</returns>
    public int GetNodeCount(bool includeSubTrees) => _treeView.GetNodeCount(includeSubTrees);

    /// <summary>
    /// Provides node information, given a point.
    /// </summary>
    /// <param name="pt">The Point at which to retrieve node information.</param>
    /// <returns>A TreeViewHitTestInfo.</returns>
    public TreeViewHitTestInfo HitTest(Point pt) => _treeView.HitTest(pt);

    /// <summary>
    /// Provides node information, given x- and y-coordinates.
    /// </summary>
    /// <param name="x">The x-coordinate at which to retrieve node information.</param>
    /// <param name="y">The y-coordinate at which to retrieve node information.</param>
    /// <returns>A TreeViewHitTestInfo.</returns>
    public TreeViewHitTestInfo HitTest(int x, int y) => _treeView.HitTest(x, y);

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
    public bool IsActive => _fixedActive ?? DesignMode || AlwaysActive || ContainsFocus || _mouseOver || _treeView.MouseOver;

    /// <summary>
    /// Sets input focus to the control.
    /// </summary>
    /// <returns>true if the input focus request was successful; otherwise, false.</returns>
    public new bool Focus() => TreeView.Focus();

    /// <summary>
    /// Activates the control.
    /// </summary>
    public new void Select() => TreeView.Select();
    #endregion

    #region Protected
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
    #endregion

    #region Protected Virtual

    /// <summary>
    /// Raises the AfterCheck event.
    /// </summary>
    /// <param name="e">An TreeViewEventArgs that contains the event data.</param>
    /// <remarks>If overriden directly, will fire when palette changes</remarks>
    protected virtual void OnAfterCheck(TreeViewEventArgs e)
    {
        if (!_isRecreating)
        {
            AfterCheck?.Invoke(this, e);
        }
    }

    /// <summary>
    /// Raises the AfterCollapse event.
    /// </summary>
    /// <param name="e">An TreeViewEventArgs that contains the event data.</param>
    /// <remarks>If overriden directly, will fire when palette changes</remarks>
    protected virtual void OnAfterCollapse(TreeViewEventArgs e)
    {
        if (!_isRecreating)
        {
            AfterCollapse?.Invoke(this, e);
        }
    }

    /// <summary>
    /// Raises the AfterExpand event.
    /// </summary>
    /// <param name="e">An TreeViewEventArgs that contains the event data.</param>
    /// <remarks>If overriden directly, will fire when palette changes</remarks>
    protected virtual void OnAfterExpand(TreeViewEventArgs e)
    {
        if (!_isRecreating)
        {
            AfterExpand?.Invoke(this, e);
        }
    }

    /// <summary>
    /// Raises the AfterLabelEdit event.
    /// </summary>
    /// <param name="e">An NodeLabelEditEventArgs that contains the event data.</param>
    /// <remarks>If overriden directly, will fire when palette changes</remarks>
    protected virtual void OnAfterLabelEdit(NodeLabelEditEventArgs e)
    {
        if (!_isRecreating)
        {
            AfterLabelEdit?.Invoke(this, e);
        }
    }

    /// <summary>
    /// Raises the AfterSelect event.
    /// </summary>
    /// <param name="e">An TreeViewEventArgs that contains the event data.</param>
    /// <remarks>If overriden directly, will fire when palette changes</remarks>
    protected virtual void OnAfterSelect(TreeViewEventArgs e)
    {
        if (!_isRecreating)
        {
            if (_multiSelect && e.Node is not null)
            {
                e.Node.Checked = !e.Node.Checked;
            }

            AfterSelect?.Invoke(this, e);
        }
    }

    /// <summary>
    /// Raises the BeforeCheck event.
    /// </summary>
    /// <param name="e">An TreeViewCancelEventArgs that contains the event data.</param>
    /// <remarks>If overriden directly, will fire when palette changes</remarks>
    protected virtual void OnBeforeCheck(TreeViewCancelEventArgs e)
    {
        if (!_isRecreating)
        {
            BeforeCheck?.Invoke(this, e);
        }
    }

    /// <summary>
    /// Raises the BeforeCollapse event.
    /// </summary>
    /// <param name="e">An TreeViewCancelEventArgs that contains the event data.</param>
    /// <remarks>If overriden directly, will fire when palette changes</remarks>
    protected virtual void OnBeforeCollapse(TreeViewCancelEventArgs e)
    {
        if (!_isRecreating)
        {
            BeforeCollapse?.Invoke(this, e);
        }
    }

    /// <summary>
    /// Raises the BeforeExpand event.
    /// </summary>
    /// <param name="e">An TreeViewCancelEventArgs that contains the event data.</param>
    /// <remarks>If overriden directly, will fire when palette changes</remarks>
    protected virtual void OnBeforeExpand(TreeViewCancelEventArgs e)
    {
        if (!_isRecreating)
        {
            BeforeExpand?.Invoke(this, e);
        }
    }

    /// <summary>
    /// Raises the BeforeLabelEdit event.
    /// </summary>
    /// <param name="e">An NodeLabelEditEventArgs that contains the event data.</param>
    /// <remarks>If overriden directly, will fire when palette changes</remarks>
    protected virtual void OnBeforeLabelEdit(NodeLabelEditEventArgs e)
    {
        if (!_isRecreating)
        {
            BeforeLabelEdit?.Invoke(this, e);
        }
    }

    /// <summary>
    /// Raises the BeforeSelect event.
    /// </summary>
    /// <param name="e">An TreeViewCancelEventArgs that contains the event data.</param>
    /// <remarks>If overriden directly, will fire when palette changes</remarks>
    protected virtual void OnBeforeSelect(TreeViewCancelEventArgs e)
    {
        if (!_isRecreating)
        {
            BeforeSelect?.Invoke(this, e);
        }
    }

    /// <summary>
    /// Raises the ItemDrag event.
    /// </summary>
    /// <param name="e">An ItemDragEventArgs that contains the event data.</param>
    /// <remarks>If overriden directly, will fire when palette changes</remarks>
    protected virtual void OnItemDrag(ItemDragEventArgs e)
    {
        if (!_isRecreating)
        {
            ItemDrag?.Invoke(this, e);
        }
    }

    /// <summary>
    /// Raises the NodeMouseClick event.
    /// </summary>
    /// <param name="e">An TreeNodeMouseClickEventArgs that contains the event data.</param>
    /// <remarks>If overriden directly, will fire when palette changes</remarks>
    protected virtual void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
    {
        if (!_isRecreating)
        {
            NodeMouseClick?.Invoke(this, e);
        }
    }

    /// <summary>
    /// Raises the NodeMouseDoubleClick event.
    /// </summary>
    /// <param name="e">An TreeNodeMouseClickEventArgs that contains the event data.</param>
    /// <remarks>If overriden directly, will fire when palette changes</remarks>
    protected virtual void OnNodeMouseDoubleClick(TreeNodeMouseClickEventArgs e)
    {
        if (!_isRecreating)
        {
            NodeMouseDoubleClick?.Invoke(this, e);
        }
    }

    /// <summary>
    /// Raises the NodeMouseHover event.
    /// </summary>
    /// <param name="e">An TreeNodeMouseHoverEventArgs that contains the event data.</param>
    /// <remarks>If overriden directly, will fire when palette changes</remarks>
    protected virtual void OnNodeMouseHover(TreeNodeMouseHoverEventArgs e)
    {
        if (!_isRecreating)
        {
            NodeMouseHover?.Invoke(this, e);
        }
    }

    /// <summary>
    /// Raises the RightToLeftLayoutChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    /// <remarks>If overriden directly, will fire when palette changes</remarks>
    protected virtual void OnRightToLeftLayoutChanged(EventArgs e)
    {
        if (!_isRecreating)
        {
            RightToLeftLayoutChanged?.Invoke(this, e);
        }
    }

    #endregion

    #region Protected Override
    /// <summary>
    /// Creates a new instance of the control collection for the KryptonTreeView.
    /// </summary>
    /// <returns>A new instance of Control.ControlCollection assigned to the control.</returns>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected override ControlCollection CreateControlsInstance() => new KryptonReadOnlyControls(this);

    /// <summary>
    /// Raises the PaletteChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnPaletteChanged(EventArgs e)
    {
        _treeView.Recreate();
        UpdateItemHeight();
        _treeView.Invalidate();
        base.OnPaletteChanged(e);
    }

    /// <summary>
    /// Processes a notification from palette of a paint and optional layout required.
    /// </summary>
    /// <param name="sender">Source of notification.</param>
    /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
    protected override void OnPaletteNeedPaint(object? sender, NeedLayoutEventArgs e)
    {
        UpdateItemHeight();
        base.OnPaletteChanged(e);
    }

    /// <summary>
    /// Raises the CreateControl event.
    /// </summary>
    protected override void OnCreateControl()
    {
        UpdateItemHeight();
        base.OnCreateControl();
    }

    /// <summary>
    /// Raises the EnabledChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnEnabledChanged(EventArgs e)
    {
        // Change in enabled state requires a layout and repaint
        UpdateStateAndPalettes();
        PerformNeedPaint(true);

        // Let base class fire standard event
        base.OnEnabledChanged(e);
    }

    /// <summary>
    /// Raises the BackColorChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnBackColorChanged(EventArgs e) => BackColorChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the BackgroundImageChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnBackgroundImageChanged(EventArgs e) => BackgroundImageChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the BackgroundImageLayoutChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnBackgroundImageLayoutChanged(EventArgs e) => BackgroundImageLayoutChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the ForeColorChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnForeColorChanged(EventArgs e) => ForeColorChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the PaddingChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnPaddingChanged(EventArgs e) => PaddingChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the TabStop event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnTabStopChanged(EventArgs e)
    {
        TreeView.TabStop = TabStop;
        base.OnTabStopChanged(e);
    }

    /// <summary>
    /// Raises the CausesValidationChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnCausesValidationChanged(EventArgs e)
    {
        TreeView.CausesValidation = CausesValidation;
        base.OnCausesValidationChanged(e);
    }

    /// <summary>
    /// Raises the Paint event.
    /// </summary>
    /// <param name="e">An PaintEventArgs that contains the event data.</param>
    protected override void OnPaint(PaintEventArgs? e)
    {
        if ( Paint is not null && e is not null)
        {
            Paint.Invoke(this, e);
        }

        base.OnPaint(e);
    }

    /// <summary>
    /// Raises the TextChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnTextChanged(EventArgs e) => TextChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the TrackMouseEnter event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnTrackMouseEnter(EventArgs e) => TrackMouseEnter?.Invoke(this, e);

    /// <summary>
    /// Raises the TrackMouseLeave event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnTrackMouseLeave(EventArgs e) => TrackMouseLeave?.Invoke(this, e);

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
    /// Processes a notification from palette storage of a paint and optional layout required.
    /// </summary>
    /// <param name="sender">Source of notification.</param>
    /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
    protected override void OnNeedPaint(object? sender, NeedLayoutEventArgs e)
    {
        if (IsHandleCreated && !e.NeedLayout)
        {
            _treeView.Invalidate();
        }
        else
        {
            ForceControlLayout();
        }

        // Update palette to reflect latest state
        UpdateItemHeight();
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
            _treeView.SetBounds(fillRect.X, fillRect.Y, fillRect.Width, fillRect.Height);
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
        _treeView.Invalidate();
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
        _treeView.Invalidate();
        base.OnMouseLeave(e);
    }

    /// <summary>
    /// Raises the MouseDown event.
    /// </summary>
    /// <param name="e">A MouseEventArgs that contains the event data.</param>
    protected override void OnMouseDown(MouseEventArgs e)
    {
        _mouseOver = false;

        PerformNeedPaint(true);

        _treeView.Invalidate();

        base.OnMouseDown(e);
    }

    /// <summary>
    /// Gets the default size of the control.
    /// </summary>
    protected override Size DefaultSize => new Size(120, 96);

    protected override void CreateHandle()
    {
        base.CreateHandle();

        PI.SetWindowTheme(Handle, @"DarkMode_Explorer", null);
    }

    //protected override onh
    #endregion

    #region Implementation

    private void UpdateItemHeight()
    {
        if (!IsDisposed && !Disposing)
        {
            UpdateContentFromNode(null);

            // Ask the view element to layout in given space, needs this before a render call
            using var context = new ViewLayoutContext(this, Renderer);
            // For calculating the item height we always assume normal state
            _drawButton.ElementState = PaletteState.Normal;

            // Find required size to show a node (only interested in the height)
            Size size = _drawButton.GetPreferredSize(context);
            size.Height += 1;

            // If we have images defined then adjust to reflect image height
            if (ImageList != null)
            {
                size.Height = Math.Max(size.Height, ImageList.ImageSize.Height);
            }

            // Update the item height to match height of a single node
            if (size.Height != ItemHeight)
            {
                if (_itemHeightDefault)
                {
                    _treeView.ItemHeight = size.Height;
                }
            }
        }
    }

    private void UpdateContentFromNode(TreeNode? node)
    {
        _overrideNormalNode.TreeNode = node;

        if (_contentValues is not null)
        {
            if (node is not null)
            {
                // Get information from the node
                _contentValues.ShortText = node.Text;
                _contentValues.LongText = string.Empty;
                _contentValues.Image = null;
                _contentValues.ImageTransparentColor = GlobalStaticValues.EMPTY_COLOR;

                if (node is KryptonTreeNode kryptonNode)
                {
                    // Get long text from the Krypton extension
                    _contentValues.LongText = kryptonNode.LongText;
                }
            }
            else
            {
                // Get the text string for the item
                _contentValues.ShortText = @"A";
                _contentValues.LongText = string.Empty;
                _contentValues.Image = null;
                _contentValues.ImageTransparentColor = GlobalStaticValues.EMPTY_COLOR;
            }
        }
    }

    private void UpdateStateAndPalettes()
    {
        if (!IsDisposed)
        {
            // Get the correct palette settings to use
            IPaletteDouble doubleState = GetDoubleState();
            _treeView.ViewDrawPanel.SetPalettes(doubleState.PaletteBack);
            _drawDockerOuter.SetPalettes(doubleState.PaletteBack, doubleState.PaletteBorder!);
            _drawDockerOuter.Enabled = Enabled;

            // Find the new state of the main view element
            PaletteState state = Enabled ? (IsActive ? PaletteState.Tracking : PaletteState.Normal) : PaletteState.Disabled;

            _treeView.ViewDrawPanel.ElementState = state;
            _drawDockerOuter.ElementState = state;
            _treeView.Font = StateCommon.Node.Content.ShortText.Font;
        }
    }

    private IPaletteDouble GetDoubleState() => Enabled ? (IsActive ? StateActive : StateNormal) : StateDisabled;

    private int NodeIndent(TreeNode node)
    {
        var depth = 0;

        // Count depth of our node in tree
        TreeNode? current = node;
        while (current is not null)
        {
            depth++;
            current = current.Parent;
        }

        // Do we need the root level indent?
        if (!ShowRootLines)
        {
            depth--;
        }

        return depth * _treeView.Indent;
    }

    private void OnTreeViewDrawNode(object? sender, DrawTreeNodeEventArgs e)
    {
        // We cannot do anything without a valid node
        if (e.Node == null)
        {
            return;
        }

        // Update our content object with values from the node
        UpdateContentFromNode(e.Node);

        // Do we need an image?
        if (ImageList != null)
        {
            _layoutImageStack.Visible = true;
            _layoutImage.SeparatorSize = ImageList.ImageSize;
        }
        else
        {
            _layoutImageStack.Visible = false;
        }

        var kryptonNode = e.Node as KryptonTreeNode;

        // Work out if we need to draw a state image
        Image? drawStateImage = null;
        if (StateImageList != null)
        {
            try
            {
                // If showing check boxes then used fixed entries from the state image list
                if (CheckBoxes)
                {
                    if (kryptonNode?.IsCheckBoxVisible != false)
                    {
                        drawStateImage = e.Node.Checked ? StateImageList.Images[1] : StateImageList.Images[0];
                    }
                }
                else
                {
                    // Check node values before tree level values
                    if (!string.IsNullOrEmpty(e.Node.StateImageKey))
                    {
                        drawStateImage = StateImageList.Images[e.Node.StateImageKey];
                    }
                    else if ((e.Node.StateImageIndex >= 0) && (e.Node.StateImageIndex < StateImageList.Images.Count))
                    {
                        drawStateImage = StateImageList.Images[e.Node.StateImageIndex];
                    }
                }
            }
            catch
            {
                // ignored
            }
        }

        _layoutImageCenterState.Visible = drawStateImage != null;

        // Do we need the checkbox?
        _layoutCheckBoxStack.Visible = (StateImageList == null)
                                       && CheckBoxes
                                       && (kryptonNode?.IsCheckBoxVisible != false);
        if (_layoutCheckBoxStack.Visible)
        {
            _drawCheckBox.CheckState = e.Node.Checked ? CheckState.Checked : CheckState.Unchecked;
        }

        // By default, the button is in the normal state
        PaletteState buttonState;

        // Is this item disabled
        if ((e.State & TreeNodeStates.Grayed) == TreeNodeStates.Grayed)
        {
            buttonState = PaletteState.Disabled;
        }
        else
        {
            // If selected then show as a checked item
            if ((e.State & TreeNodeStates.Selected) == TreeNodeStates.Selected)
            {
                _drawButton.Checked = true;

                if ((e.State & TreeNodeStates.Hot) == TreeNodeStates.Hot)
                {
                    buttonState = PaletteState.CheckedTracking;
                }
                else if (e.Node.Checked)
                {
                    buttonState = _layoutCheckBoxStack.Visible
                        ? PaletteState.CheckedPressed
                        : PaletteState.Pressed;
                }
                else
                {
                    buttonState = PaletteState.CheckedNormal;
                }
            }
            else
            {
                _drawButton.Checked = false;

                if ((e.State & TreeNodeStates.Hot) == TreeNodeStates.Hot)
                {
                    buttonState = PaletteState.Tracking;
                }
                else if (e.Node.Checked)
                {
                    buttonState = _layoutCheckBoxStack.Visible
                        ? PaletteState.CheckedPressed
                        : PaletteState.Pressed;
                }
                else
                {
                    buttonState = PaletteState.Normal;
                }
            }

            // Do we need to show item as having the focus
            var hasFocus = (e.State & TreeNodeStates.Focused) == TreeNodeStates.Focused;

            _overrideNormal.Apply = hasFocus;
            _overrideTracking.Apply = hasFocus;
            _overrideMultiSelect.Apply = hasFocus;
            _overrideCheckedTracking.Apply = hasFocus;
            _overrideCheckedNormal.Apply = hasFocus;
            _overrideCheckedMultiSelect.Apply = hasFocus;
        }

        // Update the view with the calculated state
        _drawButton.ElementState = buttonState;

        // Grab the raw device context for the graphics instance
        var hdc = e.Graphics.GetHdc();

        try
        {
            Rectangle bounds = e.Bounds;
            var indent = _treeView.Indent;

            // Create indent rectangle and adjust bounds for remainder
            var nodeIndent = NodeIndent(e.Node) + 2;
            var indentBounds = bounds with { X = bounds.X + nodeIndent - indent, Width = indent };
            bounds.X += nodeIndent;
            bounds.Width -= nodeIndent;

            // Create bitmap that all drawing occurs onto, then we can blit it later to remove flicker
            var hBitmap = PI.CreateCompatibleBitmap(hdc, bounds.Right, bounds.Bottom);

            // If we managed to get a compatible bitmap
            if (hBitmap != IntPtr.Zero)
            {
                try
                {
                    // Must use the screen device context for the bitmap when drawing into the
                    // bitmap otherwise the Opacity and RightToLeftLayout will not work correctly.
                    PI.SelectObject(_screenDC, hBitmap);

                    // Easier to draw using a graphics instance than a DC!
                    using Graphics g = Graphics.FromHdc(_screenDC);
                    using (var context = new ViewLayoutContext(this, Renderer))
                    {
                        context.DisplayRectangle = e.Bounds;
                        _treeView.ViewDrawPanel.Layout(context);
                        context.DisplayRectangle = bounds;

                        // If no using full row selection, then layout using only required width
                        Size prefSize = _layoutDocker.GetPreferredSize(context);
                        if (!FullRowSelect)
                        {
                            if (prefSize.Width < bounds.Width)
                            {
                                bounds.Width = prefSize.Width;
                            }
                        }

                        // Always ensure we have enough space for drawing all the elements
                        if (bounds.Width < prefSize.Width)
                        {
                            bounds.Width = prefSize.Width;
                        }

                        context.DisplayRectangle = bounds;

                        _layoutDocker.Layout(context);
                    }

                    using (var context = new RenderContext(this, g, e.Bounds, Renderer))
                    {
                        _treeView.ViewDrawPanel.Render(context);
                    }

                    // Do we have an indent area for drawing plus/minus/lines?
                    if (indentBounds.X >= 0)
                    {
                        // Do we draw lines between nodes?
                        if (ShowLines
                            && (Redirector.GetMetricBool(PaletteState.Normal, PaletteMetricBool.TreeViewLines) != InheritBool.False))
                        {
                            // Find center points
                            var hCenter = indentBounds.X + (indentBounds.Width / 2) - 1;
                            var vCenter = indentBounds.Y + (indentBounds.Height / 2);
                            vCenter -= (vCenter + 1) % 2;

                            // Default to showing full line height
                            var top = indentBounds.Y;
                            top -= (top + 1) % 2;
                            var bottom = indentBounds.Bottom;

                            // If the first root node then do not show top half of line
                            if ((e.Node.Parent == null) && (e.Node.PrevNode == null))
                            {
                                top = vCenter;
                            }

                            // If the last node in collection then do not show bottom half of line
                            if (e.Node.NextNode == null)
                            {
                                bottom = vCenter;
                            }

                            // Draw the horizontal and vertical lines
                            Color lineColor = Redirector.GetContentShortTextColor1(PaletteContentStyle.InputControlStandalone, PaletteState.Normal);
                            using var linePen = new Pen(lineColor);
                            linePen.DashStyle = DashStyle.Dot;
                            linePen.DashOffset = indent % 2;
                            g.DrawLine(linePen, hCenter, top, hCenter, bottom);
                            g.DrawLine(linePen, hCenter - 1, vCenter - 1, indentBounds.Right, vCenter - 1);
                            hCenter -= indent;

                            // Draw the vertical lines for previous node levels
                            while (hCenter >= 0)
                            {
                                var begin = indentBounds.Y;
                                begin -= (begin + 1) % 2;
                                g.DrawLine(linePen, hCenter, begin, hCenter, indentBounds.Bottom);
                                hCenter -= indent;
                            }
                        }

                        // Do we draw any plus/minus images in indent bounds?
                        if (ShowPlusMinus && (e.Node.Nodes.Count > 0))
                        {
                            Image? drawImage = _redirectImages!.GetTreeViewImage(e.Node.IsExpanded);
                            if (drawImage != null)
                            {
                                float dpiFactor = g.DpiX / 96F;
                                drawImage = CommonHelper.ScaleImageForSizedDisplay(drawImage,
                                    drawImage.Width * dpiFactor,
                                    drawImage.Height * dpiFactor, false)!;
                                g.DrawImage(drawImage, new Rectangle(indentBounds.X + ((indentBounds.Width - drawImage.Width) / 2) - 1,
                                    indentBounds.Y + ((indentBounds.Height - drawImage.Height) / 2),
                                    drawImage.Width, drawImage.Height));
                            }
                        }
                    }

                    using (var context = new RenderContext(this, g, bounds, Renderer))
                    {
                        _layoutDocker.Render(context);
                    }

                    // Do we draw an image for the node?
                    if (ImageList != null)
                    {
                        Image? drawImage = null;
                        var imageCount = ImageList.Images.Count;

                        try
                        {
                            if (e.Node.IsSelected)
                            {
                                // Check node values before tree level values
                                if (!string.IsNullOrEmpty(e.Node.SelectedImageKey))
                                {
                                    drawImage = ImageList.Images[e.Node.SelectedImageKey];
                                }
                                else if ((e.Node.SelectedImageIndex >= 0) && (e.Node.SelectedImageIndex < imageCount))
                                {
                                    drawImage = ImageList.Images[e.Node.SelectedImageIndex];
                                }
                                else if (!string.IsNullOrEmpty(SelectedImageKey))
                                {
                                    drawImage = ImageList.Images[SelectedImageKey];
                                }
                                else if ((SelectedImageIndex >= 0) && (SelectedImageIndex < imageCount))
                                {
                                    drawImage = ImageList.Images[SelectedImageIndex];
                                }
                            }
                            else
                            {
                                // Check node values before tree level values
                                if (!string.IsNullOrEmpty(e.Node.ImageKey))
                                {
                                    drawImage = ImageList.Images[e.Node.ImageKey];
                                }
                                else if ((e.Node.ImageIndex >= 0) && (e.Node.ImageIndex < imageCount))
                                {
                                    drawImage = ImageList.Images[e.Node.ImageIndex];
                                }
                                else if (!string.IsNullOrEmpty(ImageKey))
                                {
                                    drawImage = ImageList.Images[ImageKey];
                                }
                                else if ((ImageIndex >= 0) && (ImageIndex < imageCount))
                                {
                                    drawImage = ImageList.Images[ImageIndex];
                                }
                            }

                            if (drawImage != null)
                            {
                                float dpiFactor = g.DpiX / 96F;
                                drawImage = CommonHelper.ScaleImageForSizedDisplay(drawImage,
                                    drawImage.Width * dpiFactor,
                                    drawImage.Height * dpiFactor, false)!;
                                g.DrawImage(drawImage, _layoutImage.ClientRectangle);
                            }
                        }
                        catch
                        {
                            // ignored
                        }
                    }

                    // Draw a node state image?
                    if (_layoutImageCenterState.Visible)
                    {
                        if (drawStateImage != null)
                        {
                            float dpiFactor = g.DpiX / 96F;
                            drawStateImage = CommonHelper.ScaleImageForSizedDisplay(drawStateImage,
                                drawStateImage.Width * dpiFactor,
                                drawStateImage.Height * dpiFactor, false)!;
                            g.DrawImage(drawStateImage, _layoutImageState.ClientRectangle);
                        }
                    }

                    // Now blit from the bitmap from the screen to the real dc
                    PI.BitBlt(hdc, e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height, _screenDC, e.Bounds.X, e.Bounds.Y, PI.SRCCOPY);
                }
                finally
                {
                    // Delete the temporary bitmap
                    PI.DeleteObject(hBitmap);
                }
            }
        }
        finally
        {
            // Must reserve the GetHdc() call before
            e.Graphics.ReleaseHdc();
        }
    }

    private void OnTreeViewGotFocus(object? sender, EventArgs e)
    {
        UpdateStateAndPalettes();
        _treeView.Invalidate();
        PerformNeedPaint(true);
        OnGotFocus(e);
    }

    private void OnTreeViewLostFocus(object? sender, EventArgs e)
    {
        UpdateStateAndPalettes();
        _treeView.Invalidate();
        PerformNeedPaint(true);
        OnLostFocus(e);
    }

    private void OnTreeViewKeyPress(object? sender, KeyPressEventArgs e) => OnKeyPress(e);

    private void OnTreeViewKeyUp(object? sender, KeyEventArgs e) => OnKeyUp(e);

    private void OnTreeViewKeyDown(object? sender, KeyEventArgs e) => OnKeyDown(e);

    private void OnTreeViewPreviewKeyDown(object? sender, PreviewKeyDownEventArgs e) => OnPreviewKeyDown(e);

    private void OnTreeViewValidated(object? sender, EventArgs e) => OnValidated(e);

    private void OnTreeViewValidating(object? sender, CancelEventArgs e) => OnValidating(e);

    private void OnTreeViewNodeMouseHover(object? sender, TreeNodeMouseHoverEventArgs e) => OnNodeMouseHover(e);

    private void OnTreeViewNodeMouseDoubleClick(object? sender, TreeNodeMouseClickEventArgs e) => OnNodeMouseDoubleClick(e);

    private void OnTreeViewNodeMouseClick(object? sender, TreeNodeMouseClickEventArgs e) => OnNodeMouseClick(e);

    private void OnTreeViewItemDrag(object? sender, ItemDragEventArgs e) => OnItemDrag(e);

    private void OnTreeViewBeforeSelect(object? sender, TreeViewCancelEventArgs e) => OnBeforeSelect(e);

    private void OnTreeViewBeforeLabelEdit(object? sender, NodeLabelEditEventArgs e) => OnBeforeLabelEdit(e);

    private void OnTreeViewBeforeExpand(object? sender, TreeViewCancelEventArgs e) => OnBeforeExpand(e);

    private void OnTreeViewBeforeCollapse(object? sender, TreeViewCancelEventArgs e) => OnBeforeCollapse(e);

    private void OnTreeViewBeforeCheck(object? sender, TreeViewCancelEventArgs e) => OnBeforeCheck(e);

    private void OnTreeViewAfterSelect(object? sender, TreeViewEventArgs e) => OnAfterSelect(e);

    private void OnTreeViewAfterLabelEdit(object? sender, NodeLabelEditEventArgs e) => OnAfterLabelEdit(e);

    private void OnTreeViewAfterExpand(object? sender, TreeViewEventArgs e) => OnAfterExpand(e);

    private void OnTreeViewAfterCollapse(object? sender, TreeViewEventArgs e) => OnAfterCollapse(e);

    private void OnTreeViewAfterCheck(object? sender, TreeViewEventArgs e) => OnAfterCheck(e);

    private void OnTreeViewMouseChange(object? sender, EventArgs e)
    {
        // Change in tracking state?
        if (_treeView.MouseOver != _trackingMouseEnter)
        {
            _trackingMouseEnter = _treeView.MouseOver;

            // Raise appropriate event
            if (_trackingMouseEnter)
            {
                OnTrackMouseEnter(EventArgs.Empty);
                OnMouseEnter(e);
            }
            else
            {
                OnTrackMouseLeave(EventArgs.Empty);
                OnMouseLeave(e);
            }
        }
    }

    private void OnDoubleClick(object? sender, EventArgs e) => base.OnDoubleClick(e);

    private void OnMouseDoubleClick(object? sender, MouseEventArgs e) => base.OnMouseDoubleClick(e);

    #endregion

}
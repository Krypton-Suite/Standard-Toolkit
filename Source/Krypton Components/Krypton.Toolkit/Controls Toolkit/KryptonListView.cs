#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2022. All rights reserved. 
 *
 */
#endregion

// ReSharper disable MemberCanBeProtected.Global
namespace Krypton.Toolkit
{
    /// <summary>
    /// Provide a ListView with Krypton styling applied.
    /// </summary>
    /// <seealso cref="ListView" />
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(ListView))]
    [DefaultEvent("AfterSelect")]
    [DefaultProperty("Nodes")]
    [Designer("Krypton.Toolkit.KryptonTreeViewDesigner, Krypton.Toolkit")]
    [DesignerCategory("code")]
    [Description("A Kryptonised listview. Does not support the `List or Details View` types")]
    public class KryptonListView : ListView
    {
        #region Variables
        private IPalette _localPalette;
        private IPalette _palette;
        private PaletteMode _paletteMode;
        private bool _layoutDirty;
        private bool _refreshAll;
        private readonly IntPtr _screenDC;

        private readonly PaletteTripleOverride _overrideNormal;
        private readonly PaletteTripleOverride _overrideTracking;
        private readonly PaletteTripleOverride _overridePressed;
        private readonly PaletteTripleOverride _overrideCheckedNormal;
        private readonly PaletteTripleOverride _overrideCheckedTracking;
        private readonly PaletteTripleOverride _overrideCheckedPressed;
        private readonly PaletteRedirectCheckBox _paletteCheckBoxImages;
        private readonly ViewLayoutDocker _drawDockerInner;
        private readonly ViewDrawDocker _drawDockerOuter;
        private readonly ViewLayoutCenter _layoutCheckBox;
        private readonly ViewLayoutSeparator _layoutCheckBoxAfter;
        private readonly ViewLayoutStack _layoutCheckBoxStack;
        private readonly ViewLayoutDocker _layoutDockerTile;
        private readonly ViewLayoutDocker _layoutDockerSmall;
        private readonly ViewLayoutDocker _layoutDockerCheckLarge;
        private readonly ViewLayoutStack _layoutImageStack;
        private readonly ViewLayoutCenter _layoutImageCenter;
        private readonly ViewLayoutCenter _layoutImageCenterState;
        private readonly ViewLayoutSeparator _layoutImage;
        private readonly ViewLayoutSeparator _layoutImageState;
        private readonly ViewLayoutSeparator _layoutImageAfter;
        private readonly ViewDrawCheckBox _drawCheckBox;
        private readonly ViewLayoutFill _layoutFill;
        private readonly ViewDrawButton _drawButton;
        private readonly ShortTextValue _contentValues;
        private bool _mouseOver;
        private bool _alwaysActive;
        private ButtonStyle _style;
        private bool? _fixedActive;
        private KryptonContextMenu _kryptonContextMenu;

        #endregion

        #region Constructor
        public KryptonListView()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint
                     | ControlStyles.OptimizedDoubleBuffer
                     | ControlStyles.SupportsTransparentBackColor // Cannot get thi sto work (Code removed)!!
                     | ControlStyles.EnableNotifyMessage
                , true);

            base.OwnerDraw = true;
            // We need to repaint entire control whenever resized
            SetStyle(ControlStyles.ResizeRedraw, true);
            // Yes, we want to be drawn double buffered by default
            DoubleBuffered = true;

            // Default fields
            _alwaysActive = true;
            _style = ButtonStyle.ListItem;
            Padding = new Padding(1);
            base.BorderStyle = System.Windows.Forms.BorderStyle.None;

            // We need to create and cache a device context compatible with the display
            _screenDC = PI.CreateCompatibleDC(IntPtr.Zero);

            // Set the palette and renderer to the defaults as specified by the manager
            Redirector = new PaletteRedirect(null);
            CacheNewPalette(KryptonManager.CurrentGlobalPalette);

            KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;

            NeedPaintDelegate = OnNeedPaint;

            // Create the palette storage
            Images = new CheckBoxImages(NeedPaintDelegate);
            _paletteCheckBoxImages = new PaletteRedirectCheckBox(Redirector, Images);
            StateCommon = new PaletteListStateRedirect(Redirector, PaletteBackStyle.InputControlStandalone, PaletteBorderStyle.InputControlStandalone, NeedPaintDelegate);
            OverrideFocus = new PaletteListItemTripleRedirect(Redirector, PaletteBackStyle.ButtonListItem, PaletteBorderStyle.ButtonListItem, PaletteContentStyle.ButtonListItem, NeedPaintDelegate);
            StateDisabled = new PaletteListState(StateCommon, NeedPaintDelegate);
            StateActive = new PaletteDouble(StateCommon, NeedPaintDelegate);
            StateNormal = new PaletteListState(StateCommon, NeedPaintDelegate);
            StateTracking = new PaletteListItemTriple(StateCommon.Item, NeedPaintDelegate);
            StatePressed = new PaletteListItemTriple(StateCommon.Item, NeedPaintDelegate);
            StateCheckedNormal = new PaletteListItemTriple(StateCommon.Item, NeedPaintDelegate);
            StateCheckedTracking = new PaletteListItemTriple(StateCommon.Item, NeedPaintDelegate);
            StateCheckedPressed = new PaletteListItemTriple(StateCommon.Item, NeedPaintDelegate);
            // Create manager and view for drawing the background
            ViewDrawPanel = new ViewDrawPanel(StateNormal.Back);

            // Create the override handling classes
            _overrideNormal = new PaletteTripleOverride(OverrideFocus.Item, StateNormal.Item, PaletteState.FocusOverride);
            _overrideTracking = new PaletteTripleOverride(OverrideFocus.Item, StateTracking.Item, PaletteState.FocusOverride);
            _overridePressed = new PaletteTripleOverride(OverrideFocus.Item, StatePressed.Item, PaletteState.FocusOverride);
            _overrideCheckedNormal = new PaletteTripleOverride(OverrideFocus.Item, StateCheckedNormal.Item, PaletteState.FocusOverride);
            _overrideCheckedTracking = new PaletteTripleOverride(OverrideFocus.Item, StateCheckedTracking.Item, PaletteState.FocusOverride);
            _overrideCheckedPressed = new PaletteTripleOverride(OverrideFocus.Item, StateCheckedPressed.Item, PaletteState.FocusOverride);

            // Create the check box image drawer and place inside element so it is always centered
            _drawCheckBox = new ViewDrawCheckBox(_paletteCheckBoxImages);
            _layoutCheckBox = new ViewLayoutCenter
            {
                _drawCheckBox
            };
            _layoutCheckBoxAfter = new ViewLayoutSeparator(3, 0);
            _layoutCheckBoxStack = new ViewLayoutStack(true)
            {
                _layoutCheckBox,
                _layoutCheckBoxAfter
            };
            // Stack used to layout the location of the node image
            _layoutImage = new ViewLayoutSeparator(0, 0);
            _layoutImageAfter = new ViewLayoutSeparator(3, 0);
            _layoutImageCenter = new ViewLayoutCenter(_layoutImage);
            _layoutImageStack = new ViewLayoutStack(true)
            {
                _layoutImageCenter,
                _layoutImageAfter
            };
            _layoutImageState = new ViewLayoutSeparator(16, 16);
            _layoutImageCenterState = new ViewLayoutCenter(_layoutImageState);
            // Create the draw element for owner drawing individual items
            _contentValues = new ShortTextValue();
            _drawButton = new ViewDrawButton(StateDisabled.Item, _overrideNormal,
                _overrideTracking, _overridePressed,
                _overrideCheckedNormal, _overrideCheckedTracking,
                _overrideCheckedPressed,
                new PaletteMetricRedirect(Redirector),
                _contentValues, VisualOrientation.Top, false);

            // Place check box on the left and the label in the remainder
            _layoutDockerTile = new ViewLayoutDocker
            {
                { _layoutImageStack, ViewDockStyle.Left },
                { _layoutImageCenterState, ViewDockStyle.Left },
                { _layoutCheckBoxStack, ViewDockStyle.Left },
                { _drawButton, ViewDockStyle.Fill }
            };

            _layoutDockerSmall = new ViewLayoutDocker
            {
                { _drawButton, ViewDockStyle.Left },
                { _layoutImageStack, ViewDockStyle.Left },
                { _layoutImageCenterState, ViewDockStyle.Left },
                { _layoutCheckBoxStack, ViewDockStyle.Left }
            };

            // Place check box on the left and the text to match the width
            _layoutDockerCheckLarge = new ViewLayoutDocker
            {
                { _layoutImageStack, ViewDockStyle.Left },
                { _layoutImageCenterState, ViewDockStyle.Left },
                { _layoutCheckBoxStack, ViewDockStyle.Left },
                { _drawButton, ViewDockStyle.Bottom }
            };

            // Create the element that fills the remainder space and remembers fill rectangle
            _layoutFill = new ViewLayoutFill(this)
            {
                DisplayPadding = new Padding(1)
            };

            // Create inner view for placing inside the drawing docker
            _drawDockerInner = new ViewLayoutDocker
            {
                { _layoutFill, ViewDockStyle.Fill }
            };

            // Create view for the control border and background
            _drawDockerOuter = new ViewDrawDocker(StateNormal.Back, StateNormal.Border)
            {
                { _drawDockerInner, ViewDockStyle.Fill }
            };

            // Create the view manager instance
            ViewManager = new ViewManager(this, _drawDockerOuter);
            // We need to create and cache a device context compatible with the display
            _screenDC = PI.CreateCompatibleDC(IntPtr.Zero);
            StateCommon.Item.Content.ShortText.MultiLine = InheritBool.True;
            StateCommon.Item.Content.ShortText.MultiLineH = PaletteRelativeAlign.Center;
            StateCommon.Item.Content.ShortText.TextH = PaletteRelativeAlign.Center;

        }

        /// <summary>
        /// Gets access to the contained view draw panel instance.
        /// </summary>
        public ViewDrawPanel ViewDrawPanel { get; }

        /// <summary>
        /// Gets access to the palette redirector.
        /// </summary>
        protected PaletteRedirect Redirector
        {
            [DebuggerStepThrough]
            get;
        }
        /// <summary>
        /// Gets and sets the ViewManager instance.
        /// </summary>
        public ViewManager ViewManager
        {
            [DebuggerStepThrough]
            get;
        }
        /// <summary>
        /// Gets access to the current renderer.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IRenderer Renderer
        {
            [DebuggerStepThrough]
            get;
            private set;
        }

        #endregion

        #region public

        /// <summary>Gets and sets the custom palette implementation.</summary>
        [Category("Visuals")]
        [Description("Custom palette applied to drawing.")]
        [DefaultValue(null)]
        public IPalette Palette
        {
            [DebuggerStepThrough] get => this._localPalette;
            set
            {
                if (this._localPalette == value)
                {
                    return;
                }

                CacheNewPalette(value);
                if (value == null)
                {
                    _paletteMode = PaletteMode.Global;
                    _localPalette = (IPalette) null;
                    CacheNewPalette(KryptonManager.GetPaletteForMode(this._paletteMode));
                }
                else
                {
                    _localPalette = value;
                    _paletteMode = PaletteMode.Custom;
                }
                PerformLayout();
            }
        }

        /// <summary>Resets the Palette property to its default value.</summary>
        public void ResetPalette() => this.PaletteMode = PaletteMode.Global;

        /// <summary>
        /// Gets and sets Determines if the control is always active or only when the mouse is over the control or has focus.
        /// </summary>
        [Category("Visuals")]
        [Description("Determines if the control is always active or only when the mouse is over the control or has focus.")]
        [DefaultValue(true)]
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

        public new bool OwnerDraw
        {
            get => base.OwnerDraw; // Should be true!
            // ReSharper disable once ValueParameterNotUsed
            set { /*Do nothing*/}
        }

        /// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ContextMenuStrip" /> associated with this control.</summary>
        /// <returns>The <see cref="T:System.Windows.Forms.ContextMenuStrip" /> for this control, or <see langword="null" /> if there is no <see cref="T:System.Windows.Forms.ContextMenuStrip" />. The default is <see langword="null" />.</returns>
        [Category("Behavior")]
        [Description("Consider using KryptonContextMenu within the behaviors section.\nThe Winforms shortcut menu to show when the user right-clicks the page.\nNote: The ContextMenu will be rendered.")]
        [DefaultValue(null)]
        public override ContextMenuStrip ContextMenuStrip
        {
            [DebuggerStepThrough]
            get => base.ContextMenuStrip;

            set
            {
                // Unhook from any current menu strip
                if (base.ContextMenuStrip != null)
                {
                    base.ContextMenuStrip.Opening -= OnContextMenuStripOpening;
                    base.ContextMenuStrip.Closed -= OnContextMenuClosed;
                }

                // Let parent handle actual storage
                base.ContextMenuStrip = value;

                // Hook into the strip being shown (so we can set the correct renderer)
                if (base.ContextMenuStrip != null)
                {
                    base.ContextMenuStrip.Opening += OnContextMenuStripOpening;
                    base.ContextMenuStrip.Closed += OnContextMenuClosed;
                }
            }
        }


        /// <summary>
        /// Gets and sets the KryptonContextMenu to show when right clicked.
        /// </summary>
        [Category("Behavior")]
        [Description("The KryptonContextMenu to show when the user right-clicks the Control.")]
        [DefaultValue(null)]
        public virtual KryptonContextMenu KryptonContextMenu
        {
            get => _kryptonContextMenu;

            set
            {
                if (_kryptonContextMenu != value)
                {
                    if (_kryptonContextMenu != null)
                    {
                        _kryptonContextMenu.Closed -= OnContextMenuClosed;
                        _kryptonContextMenu.Disposed -= OnKryptonContextMenuDisposed;
                    }

                    _kryptonContextMenu = value;

                    if (_kryptonContextMenu != null)
                    {
                        _kryptonContextMenu.Closed += OnContextMenuClosed;
                        _kryptonContextMenu.Disposed += OnKryptonContextMenuDisposed;
                    }
                }
            }
        }
        #endregion public

        #region DrawItem and SubItem
        private void UpdateStateAndPalettes()
        {
            if (!IsDisposed)
            {
                // Get the correct palette settings to use
                IPaletteDouble doubleState = GetDoubleState();
                ViewDrawPanel.SetPalettes(doubleState.PaletteBack);
                _drawDockerOuter.SetPalettes(doubleState.PaletteBack, doubleState.PaletteBorder);
                _drawDockerOuter.Enabled = Enabled;

                // Find the new state of the main view element
                PaletteState state;
                if (IsActive)
                {
                    state = PaletteState.Tracking;
                }
                else
                {
                    state = Enabled ? PaletteState.Normal : PaletteState.Disabled;
                }

                ViewDrawPanel.ElementState = state;
                _drawDockerOuter.ElementState = state;
            }
        }

        private IPaletteDouble GetDoubleState() => Enabled ? IsActive ? StateActive : StateNormal : StateDisabled;

        /// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.DrawItem">DrawItem</see> event.</summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DrawListViewItemEventArgs">DrawListViewItemEventArgs</see> that contains the event data.</param>
        protected override void OnDrawItem(DrawListViewItemEventArgs e)
        {
            // We cannot do anything without a valid node
            if (e.Item == null)
            {
                return;
            }

            // Do we need an image?
            ImageList imgList = View switch
            {
                View.LargeIcon => LargeImageList,
                View.Tile => LargeImageList,
                View.SmallIcon => SmallImageList,
                _ => null
            };
            if (imgList != null)
            {
                _layoutImageStack.Visible = true;
                _layoutImage.SeparatorSize = imgList.ImageSize;
            }
            else
            {
                _layoutImageStack.Visible = false;
            }

            // Work out if we need to draw a state image
            Image drawStateImage = null;
            if (StateImageList != null)
            {
                try
                {
                    // If showing check boxes then used fixed entries from the state image list
                    if (CheckBoxes)
                    {
                        drawStateImage = e.Item.Checked ? StateImageList.Images[1] : StateImageList.Images[0];
                    }
                    else
                    {
                        // Check node values 
                        if ((e.Item.StateImageIndex >= 0)
                            && (e.Item.StateImageIndex < StateImageList.Images.Count)
                            )
                        {
                            drawStateImage = StateImageList.Images[e.Item.StateImageIndex];
                        }
                    }
                }
                catch
                {
                    // ignored
                }
            }

            _layoutImageCenterState.Visible = drawStateImage != null;
            _layoutCheckBoxStack.Visible = (drawStateImage == null) && CheckBoxes && (View != View.Tile);
            if (_layoutCheckBoxStack.Visible)
            {
                _drawCheckBox.CheckState = e.Item.Checked ? CheckState.Checked : CheckState.Unchecked;
            }
            _contentValues.ShortText = View switch
            {
                View.LargeIcon => e.Item.Text,
                View.Tile => e.Item.Text,
                View.SmallIcon => e.Item.Text + @"     ", // Hack to get the button to "Surround" the text
                _ => null
            };

            // By default the button is in the normal state
            PaletteState buttonState;

            if (e.State.HasFlag(ListViewItemStates.Grayed))
            {
                buttonState = PaletteState.Disabled;
            }
            else
            {
                // If selected then show as a checked item
                if (e.Item.Selected)
                {
                    _drawButton.Checked = true;

                    buttonState = e.State.HasFlag(ListViewItemStates.Hot)
                        ? PaletteState.CheckedTracking
                        : PaletteState.CheckedNormal;
                }
                else
                {
                    _drawButton.Checked = false;

                    buttonState = e.State.HasFlag(ListViewItemStates.Hot)
                        ? PaletteState.Tracking
                        : PaletteState.Normal;
                }

                // Do we need to show item as having the focus
                var hasFocus = e.Item.Focused;

                _overrideNormal.Apply = hasFocus;
                _overrideTracking.Apply = hasFocus;
                _overridePressed.Apply = hasFocus;
                _overrideCheckedTracking.Apply = hasFocus;
                _overrideCheckedNormal.Apply = hasFocus;
                _overrideCheckedPressed.Apply = hasFocus;
            }

            // Update the view with the calculated state
            _drawButton.ElementState = buttonState;

            // Grab the raw device context for the graphics instance
            IntPtr hdc = e.Graphics.GetHdc();

            try
            {
                Rectangle bounds = e.Bounds;
                ViewLayoutDocker layoutDocker = View switch
                {
                    View.LargeIcon => _layoutDockerCheckLarge,
                    View.SmallIcon => _layoutDockerSmall,
                    View.Tile => _layoutDockerTile,
                    _ => throw new ArgumentOutOfRangeException()
                };

                using (ViewLayoutContext context = new(this, Renderer))
                {
                    context.DisplayRectangle = e.Bounds;
                    ViewDrawPanel.Layout(context);
                    layoutDocker.Layout(context);
                }

                // Create bitmap that all drawing occurs onto, then we can blit it later to remove flicker
                IntPtr hBitmap = PI.CreateCompatibleBitmap(hdc, bounds.Right, bounds.Bottom);

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
                        using (RenderContext context = new(this, g, e.Bounds, Renderer))
                        {
                            ViewDrawPanel.Render(context);
                        }

                        using (RenderContext context = new(this, g, bounds, Renderer))
                        {
                            layoutDocker.Render(context);
                        }

                        // Do we draw an image for the node?
                        if (imgList != null)
                        {
                            Image drawImage = null;
                            var imageCount = imgList.Images.Count;

                            try
                            {
                                // Check node values before tree level values
                                if (!string.IsNullOrEmpty(e.Item.ImageKey))
                                {
                                    drawImage = imgList.Images[e.Item.ImageKey];
                                }
                                else if ((e.Item.ImageIndex >= 0) && (e.Item.ImageIndex < imageCount))
                                {
                                    drawImage = imgList.Images[e.Item.ImageIndex];
                                }

                                if (drawImage != null)
                                {
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
                                g.DrawImage(drawStateImage, _layoutImageState.ClientRectangle);
                            }
                        }

                        // Now blit from the bitmap from the screen to the real dc
                        PI.BitBlt(hdc, e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height, _screenDC,
                            e.Bounds.X, e.Bounds.Y, PI.SRCCOPY);
                    }
                    finally
                    {
                        // Delete the temporary bitmap
                        PI.DeleteObject(hBitmap);
                    }
                }
            }
            catch
            {
                e.DrawDefault = true;
            }
            finally
            {
                // Must reserve the GetHdc() call before
                e.Graphics.ReleaseHdc();
            }

        }

        #endregion


        #region Others Overrides
        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            UpdateStateAndPalettes();
            Invalidate();
            base.OnSelectedIndexChanged(e);
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
                        Point mousePt = new(PI.LOWORD(m.LParam), PI.HIWORD(m.LParam));

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

        protected override void OnGotFocus(EventArgs e)
        {
            UpdateStateAndPalettes();
            PerformNeedPaint(true);
            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            UpdateStateAndPalettes();
            PerformNeedPaint(true);
            base.OnLostFocus(e);
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            UpdateStateAndPalettes();
            PerformNeedPaint(true);
            base.OnEnabledChanged(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (!_mouseOver)
            {
                _mouseOver = true;
                PerformNeedPaint(true);
                Invalidate();
            }
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (_mouseOver)
            {
                _mouseOver = false;
                PerformNeedPaint(true);
                Invalidate();
            }
            base.OnMouseLeave(e);
        }

        /// <summary>
        /// Releases all resources used by the Control. 
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_screenDC != IntPtr.Zero)
                {
                    PI.DeleteDC(_screenDC);
                }
                // Unhook from the palette events
                if (_palette != null)
                {
                    _palette.PalettePaint -= OnPalettePaint;
                    _palette = null;
                }

                // Unhook from the static events, otherwise we cannot be garbage collected
                KryptonManager.GlobalPaletteChanged -= OnGlobalPaletteChanged;
                this._localPalette = (IPalette) null;
            }

            base.Dispose(disposing);
        }

        protected override void OnNotifyMessage(Message m)
        {
            if (m.Msg != 0x14)
            {
                base.OnNotifyMessage(m);
            }
        }
        #endregion

        #region Krypton

        /// <summary>
        /// Gets and sets the background style.
        /// </summary>
        [Category("Visuals")]
        [Description("Style used to draw the background.")]
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

        private void ResetBackStyle()
        {
            BackStyle = PaletteBackStyle.InputControlStandalone;
        }

        private bool ShouldSerializeBackStyle() => BackStyle != PaletteBackStyle.InputControlStandalone;

        /// <summary>
        /// Gets and sets the border style.
        /// </summary>
        [Category("Visuals")]
        [Description("Style used to draw the border.")]
        public new PaletteBorderStyle BorderStyle
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

        private void ResetBorderStyle()
        {
            BorderStyle = PaletteBorderStyle.InputControlStandalone;
        }

        private bool ShouldSerializeBorderStyle() => BorderStyle != PaletteBorderStyle.InputControlStandalone;

        /// <summary>
        /// Gets access to the item appearance when it has focus.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining item appearance when it has focus.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteListItemTripleRedirect OverrideFocus { get; }

        private bool ShouldSerializeOverrideFocus() => !OverrideFocus.IsDefault;

        /// <summary>
        /// Gets access to the check box image value overrides.
        /// </summary>
        [Category("Visuals")]
        [Description("CheckBox image value overrides.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public CheckBoxImages Images { get; }

        [Category("Appearance")]
        [DefaultValue(View.LargeIcon)]
        [Description("Selects a subset of the view types that can be shown.")]
        public new View View
        {
            get => base.View;
            set
            {
                if (DesignMode)
                {
                    if (value is not (View.Details or View.List))
                    {
                        base.View = value;
                    }
                    return;
                }

                switch (value)
                {
                    case View.Details:
                        throw new NotSupportedException(@"Use the Krypton DataGrid for this view type");
                    case View.List:
                        throw new NotSupportedException(@"Use the Krypton ListBox for this view type");
                    case View.LargeIcon:
                        StateCommon.Item.Content.ShortText.MultiLineH = PaletteRelativeAlign.Center;
                        StateCommon.Item.Content.ShortText.TextH = PaletteRelativeAlign.Center;
                        break;
                    case View.SmallIcon:
                    case View.Tile:
                        StateCommon.Item.Content.ShortText.MultiLineH = PaletteRelativeAlign.Inherit;
                        StateCommon.Item.Content.ShortText.TextH = PaletteRelativeAlign.Inherit;
                        break;
                }
                base.View = value;
            }
        }
        private bool ShouldSerializeImages() => !Images.IsDefault;

        /// <summary>
        /// Gets access to the common appearance entries that other states can override.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining common appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteListStateRedirect StateCommon { get; }

        private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

        /// <summary>
        /// Gets access to the disabled appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining disabled appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteListState StateDisabled { get; }

        private bool ShouldSerializeStateDisabled() => !StateDisabled.IsDefault;

        /// <summary>
        /// Gets access to the normal appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining normal appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteListState StateNormal { get; }

        private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;

        /// <summary>
        /// Gets access to the active appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining active appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteDouble StateActive { get; }

        private bool ShouldSerializeStateActive() => !StateActive.IsDefault;

        /// <summary>
        /// Gets access to the hot tracking item appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining hot tracking item appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteListItemTriple StateTracking { get; }

        private bool ShouldSerializeStateTracking() => !StateTracking.IsDefault;

        /// <summary>
        /// Gets access to the pressed item appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining pressed item appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteListItemTriple StatePressed { get; }

        private bool ShouldSerializeStatePressed() => !StatePressed.IsDefault;

        /// <summary>
        /// Gets access to the normal checked item appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining normal checked item appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteListItemTriple StateCheckedNormal { get; }

        private bool ShouldSerializeStateCheckedNormal() => !StateCheckedNormal.IsDefault;

        /// <summary>
        /// Gets access to the hot tracking checked item appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining hot tracking checked item appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteListItemTriple StateCheckedTracking { get; }

        private bool ShouldSerializeStateCheckedTracking() => !StateCheckedTracking.IsDefault;

        /// <summary>
        /// Gets access to the pressed checked item appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining pressed checked item appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteListItemTriple StateCheckedPressed { get; }

        private bool ShouldSerializeStateCheckedPressed() => !StateCheckedPressed.IsDefault;

        /// <summary>
        /// Gets access to the need paint delegate.
        /// </summary>
        protected NeedPaintHandler NeedPaintDelegate { get; }

        /// <summary>
        /// Fires the NeedPaint event.
        /// </summary>
        /// <param name="needLayout">Does the palette change require a layout.</param>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void PerformNeedPaint(bool needLayout) => OnNeedPaint(this, new NeedLayoutEventArgs(needLayout));

        /// <summary>
        /// Gets or sets the palette to be applied.
        /// </summary>
        [Category("Visuals")]
        [Description("Palette applied to drawing.")]
        public PaletteMode PaletteMode
        {
            [DebuggerStepThrough]
            get => _paletteMode;

            set
            {
                if (_paletteMode != value)
                {
                    // Action depends on new value
                    switch (value)
                    {
                        case PaletteMode.Custom:
                            // Do nothing, you must assign a palette to the 
                            // 'Palette' property in order to get the custom mode
                            break;
                        default:
                            // Use the new value
                            _paletteMode = value;

                            // Get a reference to the standard palette from its name
                            CacheNewPalette(KryptonManager.GetPaletteForMode(_paletteMode));

                            // Need to layout again use new palette
                            PerformNeedPaint(true);
                            Invalidate();
                            break;
                    }
                }
            }
        }

        private bool ShouldSerializePaletteMode() => PaletteMode != PaletteMode.Global;

        /// <summary>
        /// Resets the PaletteMode property to its default value.
        /// </summary>
        public void ResetPaletteMode()
        {
            PaletteMode = PaletteMode.Global;
        }

        private void OnGlobalPaletteChanged(object sender, EventArgs e)
        {
            if (this.PaletteMode != PaletteMode.Global)
            {
                return;
            }
            // Unhook events from old palette
            if (_palette != null)
            {
                _palette.PalettePaint -= OnPalettePaint;
            }

            CacheNewPalette(KryptonManager.CurrentGlobalPalette);

            // Change of palette means we should repaint to show any changes
            Invalidate();
        }

        private void CacheNewPalette(IPalette palette)
        {
            if (palette != _palette)
            {
                // Unhook from current palette events
                if (_palette != null)
                {
                    _palette.PalettePaint -= OnPalettePaint;
                }

                // Remember the new palette
                _palette = palette;
            }

            // Hook into events for the new palette
            if (_palette != null)
            {
                // Get the renderer associated with the palette
                Renderer = _palette.GetRenderer();
                Redirector.Target = _palette;
                _palette.PalettePaint += OnPalettePaint;
            }
        }

        private void OnPalettePaint(object sender, PaletteLayoutEventArgs e) => Invalidate();

        /// <summary>
        /// Processes a notification from palette storage of a paint and optional layout required.
        /// </summary>
        /// <param name="sender">Source of notification.</param>
        /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
        /// <exception cref="ArgumentNullException"></exception>
        protected virtual void OnNeedPaint(object sender, NeedLayoutEventArgs e)
        {
            Debug.Assert(e != null);

            // Validate incoming reference
            if (e == null)
            {
                throw new ArgumentNullException(nameof(e));
            }

            // Never try and redraw or layout when disposed are trying to dispose
            if (!IsDisposed && !Disposing)
            {

                // If required, layout the control
                if (e.NeedLayout && !_layoutDirty)
                {
                    _layoutDirty = true;
                }

                if (IsHandleCreated && (!_refreshAll || !e.InvalidRect.IsEmpty))
                {
                    // Always request the repaint immediately
                    if (e.InvalidRect.IsEmpty)
                    {
                        _refreshAll = true;
                        Invalidate();
                    }
                    else
                    {
                        Invalidate(e.InvalidRect);
                    }

                }
                // Update palette to reflect latest state
                UpdateStateAndPalettes();
            }
        }

        /// <summary>
        /// Raises the Layout event.
        /// </summary>
        /// <param name="levent">A LayoutEventArgs that contains the event data.</param>
        protected override void OnLayout(LayoutEventArgs levent)
        {
            // Cannot process a message for a disposed control
            if (!IsDisposed && !Disposing)
            {
                // Do we have a manager to use for laying out?
                if (ViewManager != null)
                {
                    // Prevent infinite loop by looping a maximum number of times
                    var max = 5;

                    do
                    {
                        // Layout cannot now be dirty
                        _layoutDirty = false;

                        // Ask the view to perform a layout
                        ViewManager.Layout(Renderer);

                    } while (_layoutDirty && (max-- > 0));
                }
            }

            // Let base class layout child controls
            base.OnLayout(levent);
        }


        /// <summary>
        /// Gets and sets the button style.
        /// </summary>
        [Category("Visuals")]
        [Description("Item style.")]
        public ButtonStyle ItemStyle
        {
            get => _style;

            set
            {
                if (_style != value)
                {
                    _style = value;
                    StateCommon.Item.SetStyles(_style);
                    OverrideFocus.Item.SetStyles(_style);
                    PerformNeedPaint(true);
                }
            }
        }

        private void OnContextMenuStripOpening(object sender, CancelEventArgs e)
        {
            // Get the actual strip instance
            ContextMenuStrip cms = base.ContextMenuStrip;

            // Make sure it has the correct renderer
            cms.Renderer = Renderer.RenderToolStrip(_palette);
        }

        private void OnKryptonContextMenuDisposed(object sender, EventArgs e)
        {
            // When the current krypton context menu is disposed, we should remove 
            // it to prevent it being used again, as that would just throw an exception 
            // because it has been disposed.
            KryptonContextMenu = null;
        }

        private void OnContextMenuClosed(object sender, ToolStripDropDownClosedEventArgs e) => ContextMenuClosed();

        /// <summary>
        /// Called when a context menu has just been closed.
        /// </summary>
        protected virtual void ContextMenuClosed()
        {
        }

        #endregion
    }
}
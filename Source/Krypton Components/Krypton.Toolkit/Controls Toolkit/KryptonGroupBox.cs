﻿#region BSD License
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

namespace Krypton.Toolkit
{
    /// <summary>
    /// Display frame around a group of related controls with an optional caption.
    /// </summary>
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(KryptonGroupBox), "ToolboxBitmaps.KryptonGroupBox.bmp")]
    [DefaultEvent(nameof(Paint))]
    [DefaultProperty("ValuesPrimary")]
    [Designer(typeof(KryptonGroupBoxDesigner))]
    [DesignerCategory(@"code")]
    [Description(@"Display frame around a group of related controls with an optional caption.")]
    [Docking(DockingBehavior.Ask)]
    public class KryptonGroupBox : VisualControlContainment
    {
        #region Instance Fields
        private LabelStyle _captionStyle;
        private VisualOrientation _captionEdge;
        private ButtonOrientation _captionOrientation;
        private readonly ViewDrawGroupBoxDocker _drawDocker;
        private readonly ViewDrawContent _drawContent;
        private readonly ViewLayoutFill _layoutFill;
        private ScreenObscurer? _obscurer;
        private readonly EventHandler? _removeObscurer;
        private bool _forcedLayout;
        private bool _captionVisible;
        private readonly bool _ignoreLayout;
        private bool _layingOut;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonGroupBox class.
        /// </summary>
        public KryptonGroupBox()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer, true);

            // Set default values
            _captionStyle = LabelStyle.GroupBoxCaption;
            _captionEdge = VisualOrientation.Top;
            _captionOrientation = ButtonOrientation.Auto;
            _captionVisible = true;

            // Create storage objects
            Values = new CaptionValues(NeedPaintDelegate, GetDpiFactor);
            Values.TextChanged += OnValuesTextChanged;

            // Create the palette storage
            StateCommon = new PaletteGroupBoxRedirect(Redirector, NeedPaintDelegate);
            StateDisabled = new PaletteGroupBox(StateCommon, NeedPaintDelegate);
            StateNormal = new PaletteGroupBox(StateCommon, NeedPaintDelegate);

            // Create the internal panel used for containing content
            Panel = new KryptonGroupBoxPanel(this, StateCommon, StateDisabled, StateNormal, OnGroupPanelPaint!)
            {
                // Make sure the panel back style always mimics our back style
                PanelBackStyle = PaletteBackStyle.ControlGroupBox
            };

            _drawContent = new ViewDrawContent(StateNormal.Content, Values, VisualOrientation.Top);

            // Create view for the control border and background
            _drawDocker = new ViewDrawGroupBoxDocker(StateNormal.Back, StateNormal.Border);

            // Create the element that fills the remainder space and remembers fill rectangle
            _layoutFill = new ViewLayoutFill(Panel);

            // Add caption into the docker with initial dock edges defined
            _drawDocker.Add(_drawContent, ViewDockStyle.Top);
            _drawDocker.Add(_layoutFill, ViewDockStyle.Fill);

            // Create the view manager instance
            ViewManager = new ViewManager(this, _drawDocker);

            // We want to default to shrinking and growing (base class defaults to GrowOnly)
            AutoSizeMode = AutoSizeMode.GrowAndShrink;

            // Create the delegate used when we need to ensure obscurer is removed
            _removeObscurer = OnRemoveObscurer;

            // Need to prevent the AddInternal from causing a layout, otherwise the
            // layout will probably try to measure text which causes the handle for the
            // control to be created which means the handle is created at the wrong time
            // and so child controls are not added properly in the future! (for the TabControl
            // at the very least).
            _ignoreLayout = true;

            // Add panel to the controls collection
            ((KryptonReadOnlyControls)Controls).AddInternal(Panel);

            _ignoreLayout = false;
        }

        private float GetDpiFactor()
        {
#if NET462
                return PI.GetDpiForWindow(Panel.Handle) / 96F;
#else
                return Panel.DeviceDpi / 96F;
#endif
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Remove any cached obscurer
                if (_obscurer != null!)
                {
                    try
                    {
                        _obscurer.Uncover();
                        _obscurer.Dispose();
                        _obscurer = null!;
                    }
                    catch
                    {
                        // Ignored
                    }
                }
            }

            base.Dispose(disposing);
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the name of the control.
        /// </summary>
        [Browsable(false)]
        [AllowNull]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new string Name
        {
            get => base.Name;

            set
            {
                base.Name = value;
                Panel.Name = $"{value}.Panel";
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the control is automatically resized to display its entire contents.
        /// </summary>
        [Browsable(true)]
        [DefaultValue(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override bool AutoSize
        {
            get => base.AutoSize;
            set => base.AutoSize = value;
        }

        /// <summary>
        /// Gets and sets the internal padding space.
        /// </summary>
        [Browsable(false)]
        [Localizable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Padding Padding
        {
            get => base.Padding;
            set => base.Padding = value;
        }

        /// <summary>
        /// Gets and sets the auto size mode.
        /// </summary>
        [Category(@"Layout")]
        [Description(@"Specifies if the control grows and shrinks to fit the contents exactly.")]
        [DefaultValue(AutoSizeMode.GrowAndShrink)]
        public AutoSizeMode AutoSizeMode
        {
            // ReSharper disable RedundantBaseQualifier
            get => base.GetAutoSizeMode();
            // ReSharper restore RedundantBaseQualifier

            set
            {
                // ReSharper disable RedundantBaseQualifier
                if (value != base.GetAutoSizeMode())
                {
                    base.SetAutoSizeMode(value);
                    // ReSharper restore RedundantBaseQualifier

                    // Only perform an immediate layout if
                    // currently performing auto size operations
                    if (AutoSize)
                    {
                        PerformNeedPaint(true);
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the text associated with this control. 
        /// </summary>
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        [AllowNull]
        public override string Text
        {
            get => Values.Heading;

            set => Values.Heading = value;
        }

        // Never serialize, let the values serialize instead
        private bool ShouldSerializeText() => false;

        /// <summary>
        /// Resets the Text property to its default value.
        /// </summary>
        // Map onto the heading property from the values
        public override void ResetText() => Values.ResetHeading();

        /// <summary>
        /// Gets access to the internal panel that contains group content.
        /// </summary>
        [Localizable(false)]
        [Category(@"Appearance")]
        [Description(@"The internal panel that contains group content.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonGroupBoxPanel Panel { get; }

        /// <summary>
        /// Gets and the sets the percentage of overlap for the caption and group area.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"The percentage the caption should overlap the group area.")]
        [TypeConverter(typeof(OpacityConverter))]
        [DefaultValue(0.5)]
        public double CaptionOverlap
        {
            get => _drawDocker.CaptionOverlap;

            set
            {
                if ( _drawDocker.CaptionOverlap != value)
                {
                    // Enforce limits on the value between 0 and 1 (0% and 100%)
                    value = Math.Max(Math.Min(value, 1.0), 0.0);
                    _drawDocker.CaptionOverlap = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Gets and sets the border style.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Border style.")]
        [DefaultValue(PaletteBorderStyle.ControlGroupBox)]
        public PaletteBorderStyle GroupBorderStyle
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

        private void ResetGroupBorderStyle() => GroupBorderStyle = PaletteBorderStyle.ControlGroupBox;

        private bool ShouldSerializeGroupBorderStyle() => GroupBorderStyle != PaletteBorderStyle.ControlGroupBox;

        /// <summary>
        /// Gets and sets the background style.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Background style.")]
        [DefaultValue(PaletteBackStyle.ControlGroupBox)]
        public PaletteBackStyle GroupBackStyle
        {
            get => StateCommon.BackStyle;

            set
            {
                if (StateCommon.BackStyle != value)
                {
                    StateCommon.BackStyle = value;
                    Panel.PanelBackStyle = value;
                    PerformNeedPaint(true);
                }
            }
        }

        private void ResetGroupBackStyle() => GroupBackStyle = PaletteBackStyle.ControlGroupBox;

        private bool ShouldSerializeGroupBackStyle() => GroupBackStyle != PaletteBackStyle.ControlGroupBox;

        /// <summary>
        /// Gets and sets the caption style.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Caption style.")]
        [DefaultValue(LabelStyle.GroupBoxCaption)]
        public LabelStyle CaptionStyle
        {
            get => _captionStyle;

            set
            {
                if (_captionStyle != value)
                {
                    _captionStyle = value;
                    StateCommon.ContentStyle = CommonHelper.ContentStyleFromLabelStyle(_captionStyle);
                    PerformNeedPaint(true);
                }
            }
        }

        private void ResetCaptionStyle() => CaptionStyle = LabelStyle.GroupBoxCaption;

        private bool ShouldSerializeCaptionStyle() => CaptionStyle != LabelStyle.GroupBoxCaption;

        /// <summary>
        /// Gets and sets the position of the caption.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Edge position of the caption.")]
        [DefaultValue(VisualOrientation.Top)]
        public VisualOrientation CaptionEdge
        {
            get => _captionEdge;

            set
            {
                if (_captionEdge != value)
                {
                    _captionEdge = value;
                    switch (_captionEdge)
                    {
                        case VisualOrientation.Top:
                            if (_captionOrientation == ButtonOrientation.Auto)
                            {
                                _drawContent.Orientation = VisualOrientation.Top;
                            }

                            _drawDocker.SetDock(_drawContent, ViewDockStyle.Top);
                            break;
                        case VisualOrientation.Bottom:
                            if (_captionOrientation == ButtonOrientation.Auto)
                            {
                                _drawContent.Orientation = VisualOrientation.Top;
                            }

                            _drawDocker.SetDock(_drawContent, ViewDockStyle.Bottom);
                            break;
                        case VisualOrientation.Left:
                            if (_captionOrientation == ButtonOrientation.Auto)
                            {
                                _drawContent.Orientation = VisualOrientation.Left;
                            }

                            _drawDocker.SetDock(_drawContent, ViewDockStyle.Left);
                            break;
                        case VisualOrientation.Right:
                            if (_captionOrientation == ButtonOrientation.Auto)
                            {
                                _drawContent.Orientation = VisualOrientation.Right;
                            }

                            _drawDocker.SetDock(_drawContent, ViewDockStyle.Right);
                            break;
                    }

                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Gets and sets the orientation of the caption.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Orientation of the caption.")]
        [DefaultValue(ButtonOrientation.Auto)]
        public ButtonOrientation CaptionOrientation
        {
            get => _captionOrientation;

            set
            {
                if (_captionOrientation != value)
                {
                    _captionOrientation = value;
                    switch (_captionOrientation)
                    {
                        case ButtonOrientation.FixedTop:
                            _drawContent.Orientation = VisualOrientation.Top;
                            break;
                        case ButtonOrientation.FixedBottom:
                            _drawContent.Orientation = VisualOrientation.Bottom;
                            break;
                        case ButtonOrientation.FixedLeft:
                            _drawContent.Orientation = VisualOrientation.Left;
                            break;
                        case ButtonOrientation.FixedRight:
                            _drawContent.Orientation = VisualOrientation.Right;
                            break;
                        case ButtonOrientation.Auto:
                            switch (_captionEdge)
                            {
                                case VisualOrientation.Top:
                                case VisualOrientation.Bottom:
                                    _drawContent.Orientation = VisualOrientation.Top;
                                    break;
                                case VisualOrientation.Left:
                                    _drawContent.Orientation = VisualOrientation.Left;
                                    break;
                                case VisualOrientation.Right:
                                    _drawContent.Orientation = VisualOrientation.Right;
                                    break;
                            }
                            break;
                    }

                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Gets and sets the caption visibility.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Caption visibility.")]
        [DefaultValue(true)]
        public bool CaptionVisible
        {
            get => _captionVisible;

            set
            {
                if (_captionVisible != value)
                {
                    _captionVisible = value;
                    ReapplyVisible();
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Gets access to the common header group appearance that other states can override.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Overrides for defining common header group appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteGroupBoxRedirect StateCommon { get; }

        private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

        /// <summary>
        /// Gets access to the disabled header group appearance entries.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Overrides for defining disabled header group appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteGroupBox? StateDisabled { get; }

        private bool ShouldSerializeStateDisabled() => !StateDisabled!.IsDefault;

        /// <summary>
        /// Gets access to the normal header group appearance entries.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Overrides for defining normal header group appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteGroupBox? StateNormal { get; }

        private bool ShouldSerializeStateNormal() => !StateNormal!.IsDefault;

        /// <summary>
        /// Gets access to the caption content.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Caption values")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public CaptionValues Values { get; }

        private bool ShouldSerializeValues() => !Values.IsDefault;

        /// <summary>
        /// Get the preferred size of the control based on a proposed size.
        /// </summary>
        /// <param name="proposedSize">Starting size proposed by the caller.</param>
        /// <returns>Calculated preferred size.</returns>
        public override Size GetPreferredSize(Size proposedSize)
        {
            // Do we have a manager to ask for a preferred size?
            if (ViewManager != null)
            {
                // Ask the view to perform a layout
                Size retSize = ViewManager.GetPreferredSize(Renderer, proposedSize);

                // Apply the maximum sizing
                if (MaximumSize.Width > 0)
                {
                    retSize.Width = Math.Min(MaximumSize.Width, retSize.Width);
                }

                if (MaximumSize.Height > 0)
                {
                    retSize.Height = Math.Min(MaximumSize.Height, retSize.Height);
                }

                // Apply the minimum sizing
                if (MinimumSize.Width > 0)
                {
                    retSize.Width = Math.Max(MinimumSize.Width, retSize.Width);
                }

                if (MinimumSize.Height > 0)
                {
                    retSize.Height = Math.Max(MinimumSize.Height, retSize.Height);
                }

                return retSize;
            }
            else
            {
                // Fall back on default control processing
                return base.GetPreferredSize(proposedSize);
            }
        }

        /// <summary>
        /// Gets the rectangle that represents the display area of the control.
        /// </summary>
        public override Rectangle DisplayRectangle
        {
            get
            {
                // Ensure that the layout is calculated in order to know the remaining display space
                ForceViewLayout();

                // The inside panel is the client rectangle size
                return new Rectangle(Panel.Location, Panel.Size);
            }
        }

        /// <summary>
        /// Fix the control to a particular palette state.
        /// </summary>
        /// <param name="state">Palette state to fix.</param>
        public virtual void SetFixedState(PaletteState state)
        {
            // Request fixed state from the view
            _drawDocker.FixedState = state;
            Panel.SetFixedState(state);
        }
        #endregion

        #region Protected
        /// <summary>
        /// Force the layout logic to size and position the panels.
        /// </summary>
        protected void ForceControlLayout()
        {
            // Usually the layout will not occur if currently initializing but
            // we need to force the layout processing because otherwise the size
            // of the panel controls will not have been calculated when controls
            // are added to the panels. That would then cause problems with
            // anchor controls as they would then resize incorrectly.
            if (!IsInitialized)
            {
                _forcedLayout = true;
                OnLayout(new LayoutEventArgs(null, null));
                _forcedLayout = false;
            }
        }
        #endregion

        #region Protected Overrides
        /// <summary>
        /// Creates a new instance of the control collection for the KryptonHeaderGroup.
        /// </summary>
        /// <returns>A new instance of Control.ControlCollection assigned to the control.</returns>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected override ControlCollection CreateControlsInstance() => new KryptonReadOnlyControls(this);

        /// <summary>
        /// Raises the HandleCreated event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected override void OnHandleCreated(EventArgs e)
        {
            // Let base class do standard stuff
            base.OnHandleCreated(e);

            // We need a layout to occur before any painting
            InvokeLayout();
        }

        /// <summary>
        /// Raises the Initialized event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnInitialized(EventArgs e)
        {
            // Let base class raise events
            base.OnInitialized(e);

            // Force a layout now that initialization is complete
            OnLayout(new LayoutEventArgs(null, null));
        }

        /// <summary>
        /// Raises the Layout event.
        /// </summary>
        /// <param name="levent">A LayoutEventArgs containing the event data.</param>
        protected override void OnLayout(LayoutEventArgs levent)
        {
            // Remember if we are inside a layout cycle
            _layingOut = true;

            // Must ignore any layout caused by the AddInternal in the constructor,
            // otherwise layout processing causes the controls handle to be constructed
            // (because it tries to measure text for the headers) and so child controls
            // are not added correctly when inside a TabControl. Bonkers but true.
            if (!_ignoreLayout)
            {
                // Let base class calculate fill rectangle
                base.OnLayout(levent);

                // Only use layout logic if control is fully initialized or if being forced
                // to allow a relayout or if in design mode.
                if ((Panel != null))
                {
                    if (IsInitialized || _forcedLayout || DesignMode)
                    {
                        Rectangle fillRect = _layoutFill.FillRect;

                        Panel.SetBounds(fillRect.X,
                            fillRect.Y,
                            fillRect.Width,
                            fillRect.Height);
                    }
                }
            }

            _layingOut = false;
        }

        /// <summary>
        /// Raises the EnabledChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnEnabledChanged(EventArgs e)
        {
            // Push correct palettes into the view
            if (Enabled)
            {
                _drawContent.SetPalette(StateNormal!.Content);
                _drawDocker.SetPalettes(StateNormal.Back, StateNormal.Border);
            }
            else
            {
                _drawContent.SetPalette(StateDisabled!.Content);
                _drawDocker.SetPalettes(StateDisabled.Back, StateNormal!.Border);
            }

            _drawContent.Enabled = Enabled;
            _drawDocker.Enabled = Enabled;

            // Change in enabled state requires a layout and repaint
            PerformNeedPaint(true);

            // Let base class fire standard event
            base.OnEnabledChanged(e);
        }

        /// <summary>
        /// Raises the Resize event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnResize(EventArgs e)
        {
            // Let base class raise events
            base.OnResize(e);

            // We must have a layout calculation
            ForceControlLayout();
        }

        /// <summary>
        /// Processes a notification from palette storage of a paint and optional layout required.
        /// </summary>
        /// <param name="sender">Source of notification.</param>
        /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
        protected override void OnNeedPaint(object? sender, NeedLayoutEventArgs e)
        {
            if (IsInitialized || !e.NeedLayout)
            {
                // As the contained group panel is using our palette storage
                // we also need to pass on any paint request to it as well
                Panel.PerformNeedPaint(e.NeedLayout);
            }
            else
            {
                ForceControlLayout();
            }

            base.OnNeedPaint(sender, e);
        }

        /// <summary>
        /// Process Windows-based messages.
        /// </summary>
        /// <param name="m">A Windows-based message.</param>
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case PI.WM_.WINDOWPOSCHANGING:
                {
                    // First time around we need to create the obscurer
                    _obscurer ??= new ScreenObscurer();

                    // Obscure the display area of the control
                    if (!IsDisposed && IsHandleCreated && !DesignMode)
                    {
                        _obscurer.Cover(this);
                    }

                    // Just in case the WM_WINDOWPOSCHANGED does not occur we can 
                    // ensure the obscurer is removed using this async delegate call
                    if (_removeObscurer != null)
                    {
                        BeginInvoke(_removeObscurer);
                    }

                    break;
                }
                case PI.WM_.WINDOWPOSCHANGED:
                    // Uncover from the covered area
                    _obscurer?.Uncover();
                    break;
            }

            base.WndProc(ref m);
        }
        #endregion

        #region Internal
        internal Component? DesignerComponentFromPoint(Point pt) =>
            // Ignore call as view builder is already destructed
            IsDisposed ? null : ViewManager?.ComponentFromPoint(pt);

        // Ask the current view for a decision
        // Simulate the mouse leaving the control so that the tracking
        // element that thinks it has the focus is informed it does not
        internal void DesignerMouseLeave() => OnMouseLeave(EventArgs.Empty);

        #endregion

        #region Implementation
        private void OnRemoveObscurer(object? sender, EventArgs e) => _obscurer?.Uncover();

        private void OnValuesTextChanged(object? sender, EventArgs e) => OnTextChanged(EventArgs.Empty);

        private void OnGroupPanelPaint(object sender, NeedLayoutEventArgs e)
        {
            // If the child panel is layout out but not because we are, then it must be
            // laying out because a child has changed visibility/size/etc. If we are an
            // AutoSize control then we need to ensure we layout as well to change size.
            if (e.NeedLayout && !_layingOut && AutoSize)
            {
                PerformNeedPaint(true);
            }
        }

        private void ReapplyVisible() => _drawContent.Visible = _captionVisible;
        #endregion

        #region Implementation Static
        private static int PaddingEdgeNeeded(int padding, int client)
        {
            // If the padding value is less than that allocated to children
            if (padding < client)
            {
                // Then no additional padding is needed, because the children
                // overlap all of the padding edge anyway
                return 0;
            }
            else
            {
                // Then we need only the extra space between the client and the
                // padding edge, as the rest is overlaped by the children
                return padding - client;
            }
        }
        #endregion
    }
}

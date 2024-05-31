#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2024. All rights reserved. 
 *  
 */
#endregion

// ReSharper disable MemberCanBePrivate.Global

namespace Krypton.Toolkit
{
    /// <summary>
    /// Combines button functionality with the styling features of the Krypton Toolkit.
    /// </summary>
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(KryptonButton), "ToolboxBitmaps.KryptonButton.bmp")]
    [DefaultEvent(nameof(Click))]
    [DefaultProperty(nameof(Text))]
    [DesignerCategory(@"code")]
    [Description(@"Raises an event when the user clicks it.")]
    [Designer(typeof(KryptonButtonDesigner))]
    public class KryptonButton : VisualSimpleBase, IButtonControl, IContentValues
    {
        #region Static Fields

        private const int DEFAULT_PUSH_BUTTON_WIDTH = 14;

        private static readonly int BORDER_SIZE = SystemInformation.Border3DSize.Width * 2;

        #endregion

        #region Instance Fields

        private readonly ViewDrawButton _drawButton;
        private ButtonStyle _style;
        private readonly ButtonController _buttonController;
        private VisualOrientation _orientation;
        private readonly PaletteTripleOverride _overrideFocus;
        private readonly PaletteTripleOverride _overrideNormal;
        private readonly PaletteTripleOverride _overrideTracking;
        private readonly PaletteTripleOverride _overridePressed;
        private IKryptonCommand? _command;
        private bool _isDefault;
        private bool _useMnemonic;
        private bool _wasEnabled;
        private bool _skipNextOpen;
        //private bool _showSplitOption;
        //private bool _useOSUACShieldIcon;
        private Size _customUACShieldSize;
        private UACShieldIconSize _uacShieldIconSize;
        private Rectangle _dropDownRectangle;

        #endregion

        #region Events
        /// <summary>
        /// Occurs when the value of the KryptonCommand property changes.
        /// </summary>
        [Category(@"Property Changed")]
        [Description(@"Occurs when the value of the KryptonCommand property changes.")]
        public event EventHandler? KryptonCommandChanged;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonButton class.
        /// </summary>
        public KryptonButton()
        {
            // We generate click events manually, suppress default
            // production of them by the base Control class
            SetStyle(ControlStyles.StandardClick |
                     ControlStyles.StandardDoubleClick, false);
            SetStyle(ControlStyles.UseTextForAccessibility, true);
            // Set default button properties
            _style = ButtonStyle.Standalone;
            DialogResult = DialogResult.None;
            _orientation = VisualOrientation.Top;
            _useMnemonic = true;

            // Create content storage
            Values = CreateButtonValues(NeedPaintDelegate);
            Values.TextChanged += OnButtonTextChanged;

            // Create the palette storage
            StateCommon = new PaletteTripleRedirect(Redirector, PaletteBackStyle.ButtonStandalone, PaletteBorderStyle.ButtonStandalone, PaletteContentStyle.ButtonStandalone, NeedPaintDelegate);
            StateDisabled = new PaletteTriple(StateCommon, NeedPaintDelegate);
            StateNormal = new PaletteTriple(StateCommon, NeedPaintDelegate);
            StateTracking = new PaletteTriple(StateCommon, NeedPaintDelegate);
            StatePressed = new PaletteTriple(StateCommon, NeedPaintDelegate);
            OverrideDefault = new PaletteTripleRedirect(Redirector, PaletteBackStyle.ButtonStandalone, PaletteBorderStyle.ButtonStandalone, PaletteContentStyle.ButtonStandalone, NeedPaintDelegate);
            OverrideFocus = new PaletteTripleRedirect(Redirector, PaletteBackStyle.ButtonStandalone, PaletteBorderStyle.ButtonStandalone, PaletteContentStyle.ButtonStandalone, NeedPaintDelegate);

            // Create the override handling classes
            _overrideFocus = new PaletteTripleOverride(OverrideFocus, StateNormal, PaletteState.FocusOverride);
            _overrideNormal = new PaletteTripleOverride(OverrideDefault, _overrideFocus, PaletteState.NormalDefaultOverride);
            _overrideTracking = new PaletteTripleOverride(OverrideFocus, StateTracking, PaletteState.FocusOverride);
            _overridePressed = new PaletteTripleOverride(OverrideFocus, StatePressed, PaletteState.FocusOverride);

            // Create the view button instance
            _drawButton = new ViewDrawButton(StateDisabled,
                                             _overrideNormal,
                                             _overrideTracking,
                                             _overridePressed,
                                             new PaletteMetricRedirect(Redirector),
                                             this,
                                             Orientation,
                                             UseMnemonic)
            {

                // Only draw a focus rectangle when focus cues are needed in the top level form
                TestForFocusCues = true
            };

            // Create a button controller to handle button style behaviour
            _buttonController = new ButtonController(_drawButton, NeedPaintDelegate);

            // Assign the controller to the view element to treat as a button
            _drawButton.MouseController = _buttonController;
            _drawButton.KeyController = _buttonController;
            _drawButton.SourceController = _buttonController;

            // Need to know when user clicks the button view or mouse selects it
            _buttonController.Click += OnButtonClick;
            _buttonController.MouseSelect += OnButtonSelect;

            // Create the view manager instance
            ViewManager = new ViewManager(this, _drawButton);

            _uacShieldIconSize = GlobalStaticValues.DEFAULT_UAC_SHIELD_ICON_SIZE;

            //_useOSUACShieldIcon = false;

            _customUACShieldSize = GlobalStaticValues.DEFAULT_UAC_SHIELD_ICON_CUSTOM_SIZE;

            _skipNextOpen = false;

            _dropDownRectangle = new Rectangle();
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the automatic resize of the control to fit contents.
        /// </summary>
        [Browsable(true)]
        [Localizable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
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
        /// Gets or sets the text associated with this control. 
        /// </summary>
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        [AllowNull]
        public override string Text
        {
            get => Values.Text;

            set => Values.Text = value;
        }

        private bool ShouldSerializeText() =>
            // Never serialize, let the button values serialize instead
            false;

        /// <summary>
        /// Resets the Text property to its default value.
        /// </summary>
        public override void ResetText() =>
            // Map onto the button property from the values
            Values.ResetText();

        /// <summary>
        /// Gets and sets the visual orientation of the control.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Visual orientation of the control.")]
        [DefaultValue(VisualOrientation.Top)]
        public virtual VisualOrientation Orientation
        {
            get => _orientation;

            set
            {
                if (_orientation != value)
                {
                    _orientation = value;

                    // Update the associated visual elements that are effected
                    _drawButton.Orientation = value;

                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Gets and sets the button style.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Button style.")]
        public ButtonStyle ButtonStyle
        {
            get => _style;

            set
            {
                if (_style != value)
                {
                    _style = value;
                    SetStyles(_style);
                    PerformNeedPaint(true);
                }
            }
        }

        private bool ShouldSerializeButtonStyle() => ButtonStyle != ButtonStyle.Standalone;

        private void ResetButtonStyle() => ButtonStyle = ButtonStyle.Standalone;



        /// <summary>
        /// Gets access to the button content.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Button values")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ButtonValues Values { get; }

        private bool ShouldSerializeValues() => !Values.IsDefault;

        //[Category(@"Visuals"), Description(@"UAC Shield Values"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        //public UACShieldValues UACShieldValues { get; }
            
        //private bool ShouldSerializeUACShieldValues() => !UACShieldValues.IsDefault;

        /// <summary>
        /// Gets access to the common button appearance that other states can override.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Overrides for defining common button appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTripleRedirect StateCommon { get; }

        private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

        /// <summary>
        /// Gets access to the disabled button appearance entries.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Overrides for defining disabled button appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTriple StateDisabled { get; }

        private bool ShouldSerializeStateDisabled() => !StateDisabled.IsDefault;

        /// <summary>
        /// Gets access to the normal button appearance entries.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Overrides for defining normal button appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTriple StateNormal { get; }

        private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;

        /// <summary>
        /// Gets access to the hot tracking button appearance entries.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Overrides for defining hot tracking button appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTriple StateTracking { get; }

        private bool ShouldSerializeStateTracking() => !StateTracking.IsDefault;

        /// <summary>
        /// Gets access to the pressed button appearance entries.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Overrides for defining pressed button appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTriple StatePressed { get; }

        private bool ShouldSerializeStatePressed() => !StatePressed.IsDefault;

        /// <summary>
        /// Gets access to the normal button appearance when default.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Overrides for defining normal button appearance when default.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTripleRedirect OverrideDefault { get; }

        private bool ShouldSerializeOverrideDefault() => !OverrideDefault.IsDefault;

        /// <summary>
        /// Gets access to the button appearance when it has focus.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Overrides for defining button appearance when it has focus.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTripleRedirect OverrideFocus { get; }

        private bool ShouldSerializeOverrideFocus() => !OverrideFocus.IsDefault;

        /// <summary>
        /// Gets or sets the value returned to the parent form when the button is clicked.
        /// </summary>
        [Category(@"Behavior")]
        [Description(@"The dialog-box result produced in a modal form by clicking the button.")]
        [DefaultValue(DialogResult.None)]
        public DialogResult DialogResult { get; set; }

        /// <summary>
        /// Gets and sets the associated KryptonCommand.
        /// </summary>
        [Category(@"Behavior")]
        [Description(@"Command associated with the button.")]
        [DefaultValue(null)]
        public virtual IKryptonCommand? KryptonCommand
        {
            get => _command;

            set
            {
                if (_command == value)
                {
                    return;
                }

                if (_command != null)
                {
                    _command.PropertyChanged -= OnCommandPropertyChanged;
                }
                else
                {
                    _wasEnabled = Enabled;
                }

                _command = value;
                OnKryptonCommandChanged(EventArgs.Empty);

                if (_command != null)
                {
                    _command.PropertyChanged += OnCommandPropertyChanged;
                }
                else
                {
                    Enabled = _wasEnabled;
                }
            }
        }

        /// <summary>
        /// Notifies a control that it is the default button so that its appearance and behavior is adjusted accordingly. 
        /// </summary>
        /// <param name="value">true if the control should behave as a default button; otherwise false.</param>
        public void NotifyDefault(bool value)
        {
            if (!ViewDrawButton.IsFixed && (_isDefault != value))
            {
                // Remember new default status
                _isDefault = value;

                // Decide if the default overrides should be applied
                _overrideNormal.Apply = value;

                // Change in default state requires a layout and repaint
                PerformNeedPaint(true);
            }
        }

        /// <summary>
        /// Generates a Click event for the control.
        /// </summary>
        public void PerformClick()
        {
            if (CanSelect)
            {
                OnClick(EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether an ampersand is included in the text of the control. 
        /// </summary>
        [Category(@"Appearance")]
        [Description(@"When true the first character after an ampersand will be used as a mnemonic.")]
        [DefaultValue(true)]
        public bool UseMnemonic
        {
            get => _useMnemonic;

            set
            {
                if (_useMnemonic != value)
                {
                    _useMnemonic = value;
                    _drawButton.UseMnemonic = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Fix the control to a particular palette state.
        /// </summary>
        /// <param name="state">Palette state to fix.</param>
        public virtual void SetFixedState(PaletteState state)
        {
            if (state == PaletteState.NormalDefaultOverride)
            {
                // Setup the overrides correctly to match state
                _overrideFocus.Apply = true;
                _overrideNormal.Apply = true;

                // Must pass a proper drawing state to the view
                state = PaletteState.Normal;
            }

            // Request fixed state from the view
            _drawButton.FixedState = state;
        }

        /// <summary>
        /// Determines the IME status of the object when selected.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new ImeMode ImeMode
        {
            get => base.ImeMode;
            set => base.ImeMode = value;
        }

        /*/// <summary>Gets or sets a value indicating whether [show split option].</summary>
        /// <value><c>true</c> if [show split option]; otherwise, <c>false</c>.</value>
        [Category(@"Visuals")]
        [DefaultValue(false)]
        [Description(@"Displays the split/dropdown option.")]
        public bool ShowSplitOption
        {
            get => _showSplitOption;

            set
            {
                if (value != _showSplitOption)
                {
                    _showSplitOption = value;

                    Invalidate();

                    Parent?.PerformLayout();
                }
            }
        }*/

        #endregion

        #region IContentValues
        /// <summary>
        /// Gets the content short text.
        /// </summary>
        /// <returns>String value.</returns>
        public string GetShortText() => KryptonCommand?.Text ?? Values.GetShortText();

        /// <summary>
        /// Gets the content long text.
        /// </summary>
        /// <returns>String value.</returns>
        public string GetLongText() => KryptonCommand?.ExtraText ?? Values.GetLongText();

        /// <summary>
        /// Gets the content image.
        /// </summary>
        /// <param name="state">The state for which the image is needed.</param>
        /// <returns>Image value.</returns>
        public Image? GetImage(PaletteState state) => KryptonCommand?.ImageSmall ?? Values.GetImage(state);

        /// <summary>
        /// Gets the image colour that should be transparent.
        /// </summary>
        /// <param name="state">The state for which the image is needed.</param>
        /// <returns>Colour value.</returns>
        public Color GetImageTransparentColor(PaletteState state) =>
            KryptonCommand?.ImageTransparentColor ?? Values.GetImageTransparentColor(state);
        #endregion

        #region Public Overrides

        /// <inheritdoc />
        public override Size GetPreferredSize(Size proposedSize)
        {
            Size preferredSize = base.GetPreferredSize(proposedSize);

            if (Values.ShowSplitOption && !string.IsNullOrWhiteSpace(Text) && TextRenderer.MeasureText(Text, Font).Width + DEFAULT_PUSH_BUTTON_WIDTH > preferredSize.Width)
            {
                return preferredSize + new Size(DEFAULT_PUSH_BUTTON_WIDTH + BORDER_SIZE * 2, 0);
            }

            return preferredSize;
        }

        #endregion

        #region Protected Overrides
        /// <summary>
        /// Gets the default size of the control.
        /// </summary>
        protected override Size DefaultSize => new Size(90, 25);

        /// <summary>
        /// Gets the default Input Method Editor (IME) mode supported by this control.
        /// </summary>
        protected override ImeMode DefaultImeMode => ImeMode.Disable;

        /// <summary>
        /// Raises the EnabledChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnEnabledChanged(EventArgs e)
        {
            // Change in enabled state requires a layout and repaint
            PerformNeedPaint(true);

            // Let base class fire standard event
            base.OnEnabledChanged(e);
        }

        /// <summary>
        /// Raises the GotFocus event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnGotFocus(EventArgs e)
        {
            if (!ViewDrawButton.IsFixed)
            {
                // Apply the focus overrides
                _overrideFocus.Apply = true;
                _overrideTracking.Apply = true;
                _overridePressed.Apply = true;

                // Change in focus requires a repaint
                PerformNeedPaint(false);
            }

            // Let base class fire standard event
            base.OnGotFocus(e);
        }

        /// <summary>
        /// Raises the LostFocus event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnLostFocus(EventArgs e)
        {
            if (!ViewDrawButton.IsFixed)
            {
                // Apply the focus overrides
                _overrideFocus.Apply = false;
                _overrideTracking.Apply = false;
                _overridePressed.Apply = false;

                // Change in focus requires a repaint
                PerformNeedPaint(false);
            }

            // Let base class fire standard event
            base.OnLostFocus(e);
        }

        /// <summary>
        /// Raises the Click event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnClick(EventArgs e)
        {
            // Find the form this button is on
            Form? owner = FindForm();

            // If we find a valid owner
            if (owner != null)
            {
                // Update owner with our dialog result setting
                try
                {
                    owner.DialogResult = DialogResult;
                }
                catch (InvalidEnumArgumentException)
                {
                    // Is it https://github.com/Krypton-Suite/Standard-Toolkit/issues/728
                    if (owner is VisualMessageBoxForm)
                    {
                        // need to gain access to `dialogResult` and set it forcefully
                        FieldInfo? fi = typeof(Form).GetField("dialogResult", BindingFlags.NonPublic | BindingFlags.Instance);
                        if (fi != null)
                        {
                            fi.SetValue(owner, DialogResult);
                        }
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            // Let base class fire standard event
            base.OnClick(e);

            // If we have an attached command then execute it
            KryptonCommand?.PerformExecute();

            if (Values.UseAsUACElevationButton)
            {
                var rawUACShield = SystemIcons.Shield.ToBitmap();

                // Resize rawUACShield down to 16 x 16 to make it fit
                var resizedUACShield = new Bitmap(rawUACShield, new Size(16, 16));

                if (Values.Image == null)
                {
                    Values.Image = resizedUACShield;
                }
                else if (Values.Image != null)
                {
                    // TODO: If Values.Image is set, and then image becomes null, to then display the UAC icon
                }
            }
        }

        /// <summary>
        /// Processes a mnemonic character.
        /// </summary>
        /// <param name="charCode">The mnemonic character entered.</param>
        /// <returns>true if the mnemonic was processed; otherwise, false.</returns>
        protected override bool ProcessMnemonic(char charCode)
        {
            // Are we allowed to process mnemonics?
            if (UseMnemonic && CanProcessMnemonic())
            {
                // Does the button primary text contain the mnemonic?
                if (IsMnemonic(charCode, Values.Text))
                {
                    // Perform default action for a button, click it!
                    PerformClick();
                    return true;
                }
            }

            // No match found, let base class do standard processing
            return base.ProcessMnemonic(charCode);
        }

        /// <summary>
        /// Called when a context menu has just been closed.
        /// </summary>
        protected override void ContextMenuClosed() => _buttonController.RemoveFixed();

        /// <inheritdoc />
        protected override void OnPaint(PaintEventArgs? e)
        {
            if (Values.UseAsADialogButton)
            {
                Text = DialogResult switch
                {
                    DialogResult.Abort => KryptonManager.Strings.GeneralStrings.Abort,
                    DialogResult.Cancel => KryptonManager.Strings.GeneralStrings.Cancel,
                    DialogResult.OK => KryptonManager.Strings.GeneralStrings.OK,
                    DialogResult.Yes => KryptonManager.Strings.GeneralStrings.Yes,
                    DialogResult.No => KryptonManager.Strings.GeneralStrings.No,
                    DialogResult.Retry => KryptonManager.Strings.GeneralStrings.Retry,
                    DialogResult.Ignore => KryptonManager.Strings.GeneralStrings.Ignore,
                    _ => Text
                };
            }

            if (Values.UseAsUACElevationButton)
            {
                switch (Values.UACShieldIconSize)
                {
                    case UACShieldIconSize.ExtraSmall:
                        break;
                    case UACShieldIconSize.Small:
                        break;
                    case UACShieldIconSize.Medium:
                        break;
                    case UACShieldIconSize.Large:
                        break;
                    case UACShieldIconSize.ExtraLarge:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            base.OnPaint(e);

            #region Split Code

            if (!Values.ShowSplitOption)
            {
                return;
            }

            Graphics g = e?.Graphics!;

            Rectangle bounds = ClientRectangle;

            _dropDownRectangle = new Rectangle(bounds.Right - DEFAULT_PUSH_BUTTON_WIDTH - 1, BORDER_SIZE, DEFAULT_PUSH_BUTTON_WIDTH, bounds.Height - BORDER_SIZE * 2);

            var internalBorder = BORDER_SIZE;

            var focusRectangle = new Rectangle(internalBorder, internalBorder,
                bounds.Width - _dropDownRectangle.Width - internalBorder, bounds.Height - (internalBorder * 2));

            PaletteBase palette = KryptonManager.CurrentGlobalPalette;

            var shadow = new Pen(palette.ColorTable.GripDark);

            var face = new Pen(palette.ColorTable.GripLight);

            if (RightToLeft == RightToLeft.Yes)
            {
                _dropDownRectangle.X = bounds.Left + 1;

                focusRectangle.X = _dropDownRectangle.Right;

                g.DrawLine(shadow, bounds.Left + DEFAULT_PUSH_BUTTON_WIDTH, BORDER_SIZE, bounds.Left + DEFAULT_PUSH_BUTTON_WIDTH, bounds.Bottom - BORDER_SIZE);

                g.DrawLine(face, bounds.Left + DEFAULT_PUSH_BUTTON_WIDTH + 1, BORDER_SIZE, bounds.Left + DEFAULT_PUSH_BUTTON_WIDTH + 1, bounds.Bottom - BORDER_SIZE);
            }
            else
            {
                // draw two lines at the edge of the dropdown button 
                g.DrawLine(shadow, bounds.Right - DEFAULT_PUSH_BUTTON_WIDTH, BORDER_SIZE, bounds.Right - DEFAULT_PUSH_BUTTON_WIDTH, bounds.Bottom - BORDER_SIZE);

                g.DrawLine(face, bounds.Right - DEFAULT_PUSH_BUTTON_WIDTH - 1, BORDER_SIZE, bounds.Right - DEFAULT_PUSH_BUTTON_WIDTH - 1, bounds.Bottom - BORDER_SIZE);
            }

            // Draw an arrow in the correct location 
            PaintArrow(Values.DropDownArrowColor, g, _dropDownRectangle);

            #endregion
        }

        /// <inheritdoc />
        protected override bool IsInputKey(Keys keyData)
        {
            if (keyData.Equals(Keys.Down) && Values.ShowSplitOption)
            {
                return true;
            }
            else
            {
                return base.IsInputKey(keyData);
            }
        }

        /// <inheritdoc />
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (Values.ShowSplitOption && e.KeyCode.Equals(Keys.Down))
            {
                ShowContextMenuStrip();
            }

            base.OnKeyDown(e);
        }

        /// <inheritdoc />
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!Values.ShowSplitOption)
            {
                base.OnMouseDown(e);
                return;
            }

            if (_dropDownRectangle.Contains(e.Location))
            {
                ShowContextMenuStrip();
            }
            else
            {
                base.OnMouseDown(e);
            }
        }

        /// <inheritdoc />
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (!Values.ShowSplitOption)
            {
                base.OnMouseUp(e);

                return;
            }

            if (ContextMenuStrip == null || !ContextMenuStrip.Visible)
            {
                base.OnMouseUp(e);
            }
        }

        #endregion

        #region Protected Virtual
        // ReSharper disable VirtualMemberNeverOverridden.Global
        /// <summary>
        /// Update the state objects to reflect the new button style.
        /// </summary>
        /// <param name="buttonStyle">New button style.</param>
        protected virtual void SetStyles(ButtonStyle buttonStyle)
        {
            StateCommon.SetStyles(buttonStyle);
            OverrideDefault.SetStyles(buttonStyle);
            OverrideFocus.SetStyles(buttonStyle);
        }

        /// <summary>
        /// Creates a values storage object appropriate for control.
        /// </summary>
        /// <returns>Set of button values.</returns>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        protected virtual ButtonValues CreateButtonValues(NeedPaintHandler needPaint) => new ButtonValues(needPaint);

        /// <summary>
        /// Raises the KryptonCommandChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnKryptonCommandChanged(EventArgs e)
        {
            KryptonCommandChanged?.Invoke(this, e);

            // Use the values from the new command
            if (KryptonCommand != null)
            {
                Enabled = KryptonCommand.Enabled;
            }

            // Redraw to update the text/extratext/image properties
            PerformNeedPaint(true);
        }

        /// <summary>
        /// Handles a change in the property of an attached command.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">A PropertyChangedEventArgs that contains the event data.</param>
        protected virtual void OnCommandPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Enabled):
                    Enabled = KryptonCommand!.Enabled;
                    break;
                case nameof(Text):
                case @"ExtraText":
                case @"ImageSmall":
                case @"ImageTransparentColor":
                    PerformNeedPaint(true);
                    break;
            }
        }

        /// <summary>
        /// Gets access to the view element for the button.
        /// </summary>
        protected virtual ViewDrawButton ViewDrawButton => _drawButton;
        // ReSharper restore VirtualMemberNeverOverridden.Global
        #endregion

        #region Implementation
        private void OnButtonTextChanged(object sender, EventArgs e) => OnTextChanged(EventArgs.Empty);

        private void OnButtonClick(object sender, MouseEventArgs e)
        {
            // Raise the standard click event
            OnClick(EventArgs.Empty);

            // Raise event to indicate it was a mouse activated click
            OnMouseClick(e);
        }

        private void OnButtonSelect(object sender, MouseEventArgs e)
        {
            // Take the focus if allowed
            if (CanFocus)
            {
                Focus();
            }
        }

        #region Splitter Stuff

        /// <summary>Paints the drop-down arrow.</summary>
        /// <param name="graphics">The drop-down arrow graphics.</param>
        /// <param name="rectangle">The drop-down rectangle area.</param>
        /// <param name="dropDownArrowColor">The color of the drop-down arrow.</param>
        private static void PaintArrow(Color? dropDownArrowColor, Graphics graphics, Rectangle rectangle)
        {
            var midPoint = new Point(Convert.ToInt32(rectangle.Left + rectangle.Width / 2),
                Convert.ToInt32(rectangle.Top + rectangle.Height / 2));

            midPoint.X += (rectangle.Width % 2);

            // Testing for null and EMPTY_COLOR, makes the arrow color react to theme changes, 
            // otherwise a custom color is in effect.
            Color? color = dropDownArrowColor is not null && dropDownArrowColor != GlobalStaticValues.EMPTY_COLOR
                ? dropDownArrowColor
                : KryptonManager.CurrentGlobalPalette.GetContentShortTextColor1(PaletteContentStyle.ButtonStandalone, PaletteState.Normal);

            SolidBrush dropDownBrush = new SolidBrush(color.Value);

            var arrow = new Point[]
            {
                new Point(midPoint.X - 2, midPoint.Y - 1),
                new Point(midPoint.X + 3, midPoint.Y - 1),
                midPoint with { Y = midPoint.Y + 2 }
            };

            graphics.FillPolygon(dropDownBrush, arrow);
        }

        private void ShowContextMenuStrip()
        {
            if (_skipNextOpen)
            {
                // we were called because we're closing the context menu strip 
                // when clicking the dropdown button. 
                _skipNextOpen = false;

                return;
            }

            if (KryptonContextMenu != null)
            {
                KryptonContextMenu.Show(FindForm()!.PointToScreen(Location) + new Size(0, Height));

                KryptonContextMenu.Closed += KryptonContextMenu_Closed;
            }
            else if (ContextMenuStrip != null)
            {
                ContextMenuStrip.Closing += ContextMenuStrip_Closing;

                ContextMenuStrip.Show(this, new Point(0, Height), ToolStripDropDownDirection.BelowRight);
            }
        }

        #endregion

        #endregion

        #region Event Handlers

        private void KryptonContextMenu_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            if (sender is KryptonContextMenu kcm)
            {
                kcm.Closed -= KryptonContextMenu_Closed;
            }

            //if (e.CloseReason == ToolStripDropDownCloseReason.AppClicked) 
            //{ 
            _skipNextOpen = (_dropDownRectangle.Contains(PointToClient(Cursor.Position)));
            //} 
        }

        private void ContextMenuStrip_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            if (sender is ContextMenuStrip cms)
            {
                cms.Closing -= ContextMenuStrip_Closing;
            }

            if (e.CloseReason == ToolStripDropDownCloseReason.AppClicked)
            {
                _skipNextOpen = (_dropDownRectangle.Contains(PointToClient(Cursor.Position)));
            }
        }

        #endregion
    }
}

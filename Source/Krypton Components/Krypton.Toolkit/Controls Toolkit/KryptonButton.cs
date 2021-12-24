#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2022. All rights reserved. 
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
    [DefaultEvent(@"Click")]
    [DefaultProperty(@"Text")]
    [DesignerCategory(@"code")]
    [Description(@"Raises an event when the user clicks it.")]
    [Designer(@"Krypton.Toolkit.KryptonButtonDesigner, Krypton.Toolkit")]

    public class KryptonButton : VisualSimpleBase, IButtonControl, IContentValues
    {
        #region Instance Fields
        private readonly ViewDrawButton _drawButton;
        private ButtonStyle _style;
        private readonly ButtonController _buttonController;
        private VisualOrientation _orientation;
        private readonly PaletteTripleOverride _overrideFocus;
        private readonly PaletteTripleOverride _overrideNormal;
        private readonly PaletteTripleOverride _overrideTracking;
        private readonly PaletteTripleOverride _overridePressed;
        private IKryptonCommand _command;
        private bool _useAsDialogButton, _isDefault, _useMnemonic, _wasEnabled, _useAsUACElevationButton;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the value of the KryptonCommand property changes.
        /// </summary>
        [Category(@"Property Changed")]
        [Description(@"Occurs when the value of the KryptonCommand property changes.")]
        public event EventHandler KryptonCommandChanged;
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

            _useAsDialogButton = false;
            
            _useAsUACElevationButton = false;
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
        [Editor(@"System.ComponentModel.Design.MultilineStringEditor", typeof(UITypeEditor))]
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
        [DefaultValue(typeof(VisualOrientation), "Top")]
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

        private void ResetButtonStyle()
        {
            ButtonStyle = ButtonStyle.Standalone;
        }

        [DefaultValue(false), 
         Description(@"If set to true, the text will pair up with the equivalent KryptonManager's dialog button text result. (Note: You'll lose any previous text)")]
        public bool UseAsADialogButton 
        { 
            get => _useAsDialogButton; 
            set => _useAsDialogButton = value; 
        }

        [DefaultValue(false), 
         Description(@"Transforms the button into a UAC elevated button.")]
        public bool UseAsUACElevationButton 
        { 
            get => _useAsUACElevationButton; 
            set 
            { 
                _useAsUACElevationButton = value; 
                ShowUACShield(value); 
            } 
        }

        /// <summary>
        /// Gets access to the button content.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Button values")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ButtonValues Values { get; }

        private bool ShouldSerializeValues() => !Values.IsDefault;

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
        [DefaultValue(typeof(DialogResult), "None")]
        public DialogResult DialogResult { get; set; }

        /// <summary>
        /// Gets and sets the associated KryptonCommand.
        /// </summary>
        [Category(@"Behavior")]
        [Description(@"Command associated with the button.")]
        [DefaultValue(null)]
        public virtual IKryptonCommand KryptonCommand
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
        public Image GetImage(PaletteState state) => KryptonCommand?.ImageSmall ?? Values.GetImage(state);

        /// <summary>
        /// Gets the image colour that should be transparent.
        /// </summary>
        /// <param name="state">The state for which the image is needed.</param>
        /// <returns>Colour value.</returns>
        public Color GetImageTransparentColor(PaletteState state) =>
            KryptonCommand?.ImageTransparentColor ?? Values.GetImageTransparentColor(state);
        #endregion

        #region Protected Overrides
        /// <summary>
        /// Gets the default size of the control.
        /// </summary>
        protected override Size DefaultSize => new(90, 25);

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
            Form owner = FindForm();

            // If we find a valid owner
            if (owner != null)
            {
                // Update owner with our dialog result setting
                owner.DialogResult = DialogResult;
            }

            // Let base class fire standard event
            base.OnClick(e);

            // If we have an attached command then execute it
            KryptonCommand?.PerformExecute();

            if (_useAsUACElevationButton)
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

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_useAsDialogButton)
            {
                if (DialogResult == DialogResult.Abort)
                {
                    Text = KryptonManager.Strings.Abort;
                }

                if (DialogResult == DialogResult.Cancel)
                {
                    Text = KryptonManager.Strings.Cancel;
                }

                if (DialogResult == DialogResult.OK)
                {
                    Text = KryptonManager.Strings.OK;
                }

                if (DialogResult == DialogResult.Yes)
                {
                    Text = KryptonManager.Strings.Yes;
                }

                if (DialogResult == DialogResult.No)
                {
                    Text = KryptonManager.Strings.No;
                }

                if (DialogResult == DialogResult.Retry)
                {
                    Text = KryptonManager.Strings.Retry;
                }

                if (DialogResult == DialogResult.Ignore)
                {
                    Text = KryptonManager.Strings.Ignore;
                }
            }

            base.OnPaint(e);
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
        protected virtual ButtonValues CreateButtonValues(NeedPaintHandler needPaint) => new (needPaint);

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
                case @"Enabled":
                    Enabled = KryptonCommand.Enabled;
                    break;
                case @"Text":
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

        private void ShowUACShield(bool showUACShield)
        {
            if (showUACShield)
            {
                Values.Image = IconExtractor.LoadIcon(IconExtractor.IconType.Shield, SystemInformation.SmallIconSize).ToBitmap();

                Invalidate();
            }
            else
            {
                Values.Image = null;
            }
        }
        #endregion
    }
}

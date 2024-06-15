#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2024. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    [ToolboxBitmap(typeof(KryptonComboBox))]
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
    //[DefaultEvent(nameof(SelectedIndexChanged))]
    //[DefaultProperty(nameof(Text))]
    public class KryptonToolStripComboBox : ToolStripControlHostFixed
    {
        #region Instance Fields



        #endregion

        #region Events
        /*/// <summary>This event is not relevant for this class.</summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler DoubleClick
        {
            add => KryptonComboBoxControl!.DoubleClick += value;
            remove => KryptonComboBoxControl!.DoubleClick -= value;
        }

        /*
        /// <summary>This event is not relevant for this class.</summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event MouseEventHandler MouseDoubleClick
        {
            add => KryptonComboBoxControl!.MouseDoubleClick += value;
            remove => KryptonComboBoxControl!.MouseDoubleClick -= value;
        }*

        /// <summary>
        /// Occurs when [draw item].
        /// </summary>
        [Category(@"Behavior")]
        [Description(@"Occurs when an item needs to be Drawn.")]
        public event DrawItemEventHandler? DrawItem;

        /// <summary>
        /// Occurs when the control is initialized.
        /// </summary>
        [Category(@"Behavior")]
        [Description(@"Occurs when the control has been fully initialized.")]
        public event EventHandler? Initialized;

        /// <summary>
        /// Occurs when the drop-down portion of the KryptonComboBox is shown.
        /// </summary>
        [Description(@"Occurs when the drop-down portion of the KryptonComboBox is shown.")]
        [Category(@"Behavior")]
        public event EventHandler? DropDown;

        /// <summary>
        /// Indicates that the drop-down portion of the KryptonComboBox has closed.
        /// </summary>
        [Description(@"Indicates that the drop-down portion of the KryptonComboBox has closed.")]
        [Category(@"Behavior")]
        public event EventHandler? DropDownClosed;

        /// <summary>
        /// Occurs when the value of the DropDownStyle property changed.
        /// </summary>
        [Description(@"Occurs when the value of the DropDownStyle property changed.")]
        [Category(@"Behavior")]
        public event EventHandler? DropDownStyleChanged;

        /// <summary>
        /// Occurs when the value of the SelectedIndex property changes.
        /// </summary>
        [Description(@"Occurs when the value of the SelectedIndex property changes.")]
        [Category(@"Behavior")]
        public event EventHandler? SelectedIndexChanged;

        /// <summary>
        /// Occurs when an item is chosen from the drop-down list and the drop-down list is closed.
        /// </summary>
        [Description(@"Occurs when an item is chosen from the drop-down list and the drop-down list is closed.")]
        [Category(@"Behavior")]
        public event EventHandler? SelectionChangeCommitted;

        /// <summary>
        /// Occurs when the value of the DataSource property changed.
        /// </summary>
        [Description(@"Occurs when the value of the DataSource property changed.")]
        [Category(@"PropertyChanged")]
        public event EventHandler? DataSourceChanged;

        /// <summary>
        /// Occurs when the value of the DisplayMember property changed.
        /// </summary>
        [Description(@"Occurs when the value of the DisplayMember property changed.")]
        [Category(@"PropertyChanged")]
        public event EventHandler? DisplayMemberChanged;

        /// <summary>
        /// Occurs when the list format has changed.
        /// </summary>
        [Description(@"Occurs when the list format has changed.")]
        [Category(@"PropertyChanged")]
        public event ListControlConvertEventHandler? Format;

        /// <summary>
        /// Occurs when the value of the FormatInfo property changed.
        /// </summary>
        [Description(@"Occurs when the value of the FormatInfo property changed.")]
        [Category(@"PropertyChanged")]
        public event EventHandler? FormatInfoChanged;

        /// <summary>
        /// Occurs when the value of the FormatString property changed.
        /// </summary>
        [Description(@"Occurs when the value of the FormatString property changed.")]
        [Category(@"PropertyChanged")]
        public event EventHandler? FormatStringChanged;

        /// <summary>
        /// Occurs when the value of the FormattingEnabled property changed.
        /// </summary>
        [Description(@"Occurs when the value of the FormattingEnabled property changed.")]
        [Category(@"PropertyChanged")]
        public event EventHandler? FormattingEnabledChanged;

        /// <summary>
        /// Occurs when the value of the SelectedValue property changed.
        /// </summary>
        [Description(@"Occurs when the value of the SelectedValue property changed.")]
        [Category(@"PropertyChanged")]
        public event EventHandler? SelectedValueChanged;

        /// <summary>
        /// Occurs when the value of the ValueMember property changed.
        /// </summary>
        [Description(@"Occurs when the value of the ValueMember property changed.")]
        [Category(@"PropertyChanged")]
        public event EventHandler? ValueMemberChanged;

        /// <summary>
        /// Occurs when the KryptonComboBox text has changed.
        /// </summary>
        [Description(@"Occurs when the KryptonComboBox text has changed.")]
        [Category(@"Behavior")]
        public event EventHandler? TextUpdate;

        /// <summary>
        /// Occurs when the hovered selection changed.
        /// </summary>
        [Description(@"Occurs when the hovered selection changed.")]
        [Category(@"Behavior")]
        public event EventHandler<HoveredSelectionChangedEventArgs>? HoveredSelectionChanged;

        /// <summary>
        /// Occurs when the <see cref="KryptonComboBox"/> wants to display a tooltip.
        /// </summary>
        [Description(@"Occurs when the KryptonComboBox wants to display a tooltip.")]
        [Category(@"Behavior")]
        public event EventHandler<ToolTipNeededEventArgs>? ToolTipNeeded;

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
        public event EventHandler? BackgroundImageChanged;

        /// <summary>
        /// Occurs when the value of the BackgroundImageLayout property changes.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public event EventHandler? BackgroundImageLayoutChanged;

        /// <summary>
        /// Occurs when the value of the ForeColor property changes.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler? ForeColorChanged;

        /// <summary>
        /// Occurs when the value of the Paint property changes.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler? Paint;

        /// <summary>
        /// Occurs when the value of the PaddingChanged property changes.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public event EventHandler? PaddingChanged;*/
        #endregion

        #region Host Control

        [RefreshProperties(RefreshProperties.All)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonComboBox? KryptonComboBoxControl => Control as KryptonComboBox;

        #endregion

        #region Public

        /// <summary>
        /// Gets access to the common textbox appearance entries that other states can override.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Set a watermark/prompt message for the user.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteCueHintText CueHint { get; }

        private bool ShouldSerializeCueHint() => !CueHint.IsDefault;

        /// <summary>
        /// Signals the object that initialization is starting.
        /// </summary>
        public virtual void BeginInit() =>
            // Remember that fact we are inside a BeginInit/EndInit pair
            IsInitializing = true;

        /// <summary>
        /// Signals the object that initialization is complete.
        /// </summary>
        public virtual void EndInit()
        {
            // We are now initialized
            IsInitialized = true;

            // We are no longer initializing
            IsInitializing = false;

            // Force calculation of the drop down items again so they are sized correctly
            KryptonComboBoxControl!.DrawMode = DrawMode.OwnerDrawVariable;
        }

        /// <summary>
        /// Gets a value indicating if the control is initialized.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsInitialized
        {
            [DebuggerStepThrough]
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating if the control is initialized.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsInitializing
        {
            [DebuggerStepThrough]
            get;
            private set;
        }

        /// <summary>
        /// Gets and sets if the control is in the tab chain.
        /// </summary>
        public bool TabStop
        {
            get => KryptonComboBoxControl!.TabStop;
            set => KryptonComboBoxControl!.TabStop = value;
        }

        /// <summary>Gets or sets the draw mode of the combobox.</summary>
        /// <value>The draw mode of the combobox.</value>
        [Description(@"Gets or sets the draw mode of the combobox.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public DrawMode DrawMode
        {
            get => KryptonComboBoxControl!.DrawMode;
            set
            {
                KryptonComboBoxControl!.DrawMode = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets and sets if the control is in the ribbon design mode.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public bool InRibbonDesignMode { get; set; }

        /// <summary>
        /// Gets access to the contained ComboBox instance.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(false)]
        public ComboBox ComboBox => KryptonComboBoxControl!.ComboBox;

        /// <summary>
        /// Gets access to the contained input control.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(false)]
        public Control ContainedControl => ComboBox;

        /// <summary>
        /// Gets a value indicating whether the control has input focus.
        /// </summary>
        [Browsable(false)]
        public override bool Focused => ComboBox.Focused;

        /// <summary>
        /// Gets or sets the background color for the control.
        /// </summary>
        [Browsable(false)]
        [Bindable(false)]
        public override Color BackColor
        {
            get => KryptonComboBoxControl!.BackColor;
            set => KryptonComboBoxControl!.BackColor = value;
        }

        /// <summary>
        /// Gets or sets the font of the text Displayed by the control.
        /// </summary>
        [Browsable(false)]
        [Bindable(false)]
        [AllowNull]
        public override Font Font
        {
            get => KryptonComboBoxControl!.Font;

            set => KryptonComboBoxControl!.Font = value!;
        }

        /// <summary>
        /// Gets or sets the foreground color for the control.
        /// </summary>
        [Browsable(false)]
        [Bindable(false)]
        public override Color ForeColor
        {
            get => KryptonComboBoxControl!.ForeColor;
            set => KryptonComboBoxControl!.ForeColor = value;
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
            get => KryptonComboBoxControl!.Padding;
            set => KryptonComboBoxControl!.Padding = value;
        }

        /// <summary>
        /// Gets and sets the text associated with the control.
        /// </summary>
        [AllowNull]
        public override string Text
        {
            get => KryptonComboBoxControl!.Text;
            set => KryptonComboBoxControl!.Text = value;
        }

        /// <summary>
        /// Gets and sets the selected item.
        /// </summary>
        [Bindable(true)]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object? SelectedItem
        {
            get => KryptonComboBoxControl!.SelectedItem;
            set => KryptonComboBoxControl!.SelectedItem = value;
        }

        /// <summary>
        /// Gets and sets the text that is selected in the editable portion of the control.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string? SelectedText
        {
            get => KryptonComboBoxControl!.SelectedText;
            set => KryptonComboBoxControl!.SelectedText = value;
        }

        /// <summary>
        /// Gets and sets the selected index.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedIndex
        {
            get => KryptonComboBoxControl!.SelectedIndex;
            set => KryptonComboBoxControl!.SelectedIndex = value;
        }

        /// <summary>
        /// Gets and sets the selected value.
        /// </summary>
        [Bindable(true)]
        [Browsable(false)]
        [DefaultValue(null)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object? SelectedValue
        {
            get => KryptonComboBoxControl!.SelectedValue;
            //null forgiving operator used, to remove the warning
            set => KryptonComboBoxControl!.SelectedValue = value!;
        }

        /// <summary>
        /// Gets and sets a value indicating whether the control is displaying its drop-down portion.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool DroppedDown
        {
            get => KryptonComboBoxControl!.DroppedDown;
            set => KryptonComboBoxControl!.DroppedDown = value;
        }

        /*/// <summary>
        /// Gets and sets the associated context menu strip.
        /// </summary>
        public override ContextMenuStrip? ContextMenuStrip
        {
            get => KryptonComboBoxControl!.ContextMenuStrip;

            set
            {
                KryptonComboBoxControl!.ContextMenuStrip = value;
                KryptonComboBoxControl!.ContextMenuStrip = value;
            }
        }*/

        /// <summary>
        /// Gets and sets the value member.
        /// </summary>
        [Category(@"Data")]
        [Description(@"Indicates the property to use as the actual value of the items in the control.")]
        [Editor(@"System.Windows.Forms.Design.DataMemberFieldEditor", typeof(UITypeEditor))]
        [DefaultValue(@"")]
        public string ValueMember
        {
            get => KryptonComboBoxControl!.ValueMember;
            set => KryptonComboBoxControl!.ValueMember = value;
        }

        /// <summary>
        /// Gets and sets the list that this control will use to gets its items.
        /// </summary>
        [Category(@"Data")]
        [Description(@"Indicates the list that this control will use to gets its items.")]
        [AttributeProvider(typeof(IListSource))]
        [RefreshProperties(RefreshProperties.Repaint)]
        [DefaultValue(null)]
        [AllowNull]
        public object? DataSource
        {
            get => KryptonComboBoxControl!.DataSource;
            set => KryptonComboBoxControl!.DataSource = value;
        }

        /// <summary>
        /// Gets and sets the property to display for the items in this control.
        /// </summary>
        [Category(@"Data")]
        [Description(@"Indicates the property to display for the items in this control.")]
        [TypeConverter(@"System.Windows.Forms.Design.DataMemberFieldConverter")]
        [Editor(@"System.Windows.Forms.Design.DataMemberFieldEditor", typeof(UITypeEditor))]
        [DefaultValue(@"")]
        public string DisplayMember
        {
            get => KryptonComboBoxControl!.DisplayMember;
            set => KryptonComboBoxControl!.DisplayMember = value;
        }

        /// <summary>
        /// Gets and sets the formatting provider.
        /// </summary>
        [DefaultValue(null)]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public IFormatProvider? FormatInfo
        {
            get => KryptonComboBoxControl!.FormatInfo;
            set => KryptonComboBoxControl!.FormatInfo = value;
        }

        /// <summary>
        /// Gets and sets the number of characters selected in the editable portion of the control.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionLength
        {
            get => KryptonComboBoxControl!.SelectionLength;
            set => KryptonComboBoxControl!.SelectionLength = value;
        }

        /// <summary>
        /// Gets and sets the starting index of selected text in the control.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionStart
        {
            get => KryptonComboBoxControl!.SelectionStart;
            set => KryptonComboBoxControl!.SelectionStart = value;
        }

        /*/// <summary>
        /// Gets or sets a value indicating whether mnemonics will fire button spec buttons.
        /// </summary>
        [Category(@"Appearance")]
        [Description(@"Defines if mnemonic characters generate click events for button specs.")]
        [DefaultValue(true)]
        public bool UseMnemonic
        {
            get => _buttonManager!.UseMnemonic;

            set
            {
                if (_buttonManager!.UseMnemonic != value)
                {
                    _buttonManager.UseMnemonic = value;
                    PerformNeedPaint(true);
                }
            }
        }*/

        /*/// <summary>
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
        }*/

        /// <summary>
        /// Gets and sets the appearance and functionality of the KryptonComboBox.
        /// </summary>
        [Category(@"Appearance")]
        [Description(@"Controls the appearance and functionality of the KryptonComboBox.")]
        [Editor(typeof(OverrideComboBoxStyleDropDownStyle), typeof(UITypeEditor))]
        [DefaultValue(ComboBoxStyle.DropDown)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public ComboBoxStyle DropDownStyle
        {
            get => KryptonComboBoxControl!.DropDownStyle;

            set
            {
                if (KryptonComboBoxControl!.DropDownStyle != value)
                {
                    if (value == ComboBoxStyle.Simple)
                    {
                        throw new ArgumentOutOfRangeException(nameof(KryptonComboBoxControl.DropDownStyle), @"KryptonComboBox does not support the DropDownStyle.Simple style.");
                    }

                    KryptonComboBoxControl!.DropDownStyle = value;
                    KryptonComboBoxControl?.UpdateEditControl();
                }
            }
        }

        /// <summary />
        [Category(@"Behavior")]
        [DefaultValue(true)]
        [Description(@"Determines if the ComboBox Items are shown in full")]
        public bool IntegralHeight
        {
            get => KryptonComboBoxControl!.IntegralHeight;
            set => KryptonComboBoxControl!.IntegralHeight = value;
        }

        /// <summary>
        /// Gets and sets the height, in pixels, of the drop down box in a KryptonComboBox.
        /// </summary>
        [Category(@"Behavior")]
        [Description(@"The height, in pixels, of the drop down box in a KryptonComboBox.")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(200)]
        [Browsable(true)]
        public int DropDownHeight
        {
            get => KryptonComboBoxControl!.DropDownHeight;
            set => KryptonComboBoxControl!.DropDownHeight = value;
        }

        /// <summary>
        /// Gets and sets the width, in pixels, of the drop down box in a KryptonComboBox.
        /// </summary>
        [Category(@"Behavior")]
        [Description(@"The width, in pixels, of the drop down box in a KryptonComboBox.")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        public int DropDownWidth
        {
            get => KryptonComboBoxControl!.DropDownWidth;
            set => KryptonComboBoxControl!.DropDownWidth = value;
        }

        /// <summary>
        /// Gets and sets the height, in pixels, of items in an owner-draw KryptonComboBox.
        /// </summary>
        [Category(@"Behavior")]
        [Description(@"Do not use this property, it is provided for backwards compatability only.")]
        [Localizable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public int ItemHeight
        {
            get => KryptonComboBoxControl!.ItemHeight;

            set
            {
                // Do nothing, we set the ItemHeight internally to match the font 
            }
        }

        /// <summary>
        /// Gets and sets the maximum number of entries to display in the drop-down list.
        /// </summary>
        [Category(@"Behavior")]
        [Description(@"The maximum number of entries to display in the drop-down list.")]
        [Localizable(true)]
        [DefaultValue(8)]
        public int MaxDropDownItems
        {
            get => KryptonComboBoxControl!.MaxDropDownItems;
            set => KryptonComboBoxControl!.MaxDropDownItems = value;
        }

        /// <summary>
        /// Gets or sets the maximum number of characters that can be entered into the edit control.
        /// </summary>
        [Category(@"Behavior")]
        [Description(@"Specifies the maximum number of characters that can be entered into the edit control.")]
        [DefaultValue(0)]
        [Localizable(true)]
        public int MaxLength
        {
            get => KryptonComboBoxControl!.MaxLength;
            set => KryptonComboBoxControl!.MaxLength = value;
        }

        /// <summary>
        /// Gets or sets whether the items in the list portion of the KryptonComboBox are sorted.
        /// </summary>
        [Category(@"Behavior")]
        [Description(@"Specifies whether the items in the list portion of the KryptonComboBox are sorted.")]
        [DefaultValue(false)]
        public bool Sorted
        {
            get => KryptonComboBoxControl!.Sorted;
            set => KryptonComboBoxControl!.Sorted = value;
        }

        /// <summary>
        /// Gets or sets the items in the KryptonComboBox.
        /// </summary>
        [Category(@"Data")]
        [Description(@"The items in the KryptonComboBox.")]
        [Editor(@"System.Windows.Forms.Design.ListControlStringCollectionEditor", typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [MergableProperty(false)]
        [Localizable(true)]
        public ComboBox.ObjectCollection Items => KryptonComboBoxControl!.Items;

        /*/// <summary>
        /// Gets and sets the input control style.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Input control style.")]
        public InputControlStyle InputControlStyle
        {
            get => _inputControlStyle;

            set
            {
                if (_inputControlStyle != value)
                {
                    _inputControlStyle = value;
                    StateCommon.SetStyles(value);
                    PerformNeedPaint(true);
                }
            }
        }

        private void ResetInputControlStyle() => InputControlStyle = InputControlStyle.Standalone;

        private bool ShouldSerializeInputControlStyle() => InputControlStyle != InputControlStyle.Standalone;

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
                    StateCommon.SetStyles(value);
                    PerformNeedPaint(true);
                }
            }
        }

        private void ResetItemStyle() => ItemStyle = ButtonStyle.ListItem;

        private bool ShouldSerializeItemStyle() => ItemStyle != ButtonStyle.ListItem;

        /// <summary>
        /// Gets and sets the drop button style.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"DropButton style.")]
        public ButtonStyle DropButtonStyle
        {
            get => _dropButtonStyle;

            set
            {
                if (_dropButtonStyle != value)
                {
                    _dropButtonStyle = value;
                    PerformNeedPaint(true);
                }
            }
        }

        private void ResetDropButtonStyle() => DropButtonStyle = ButtonStyle.InputControl;

        private bool ShouldSerializeDropButtonStyle() => DropButtonStyle != ButtonStyle.InputControl;

        /// <summary>
        /// Gets and sets the drop button style.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"DropButton style.")]
        public PaletteBackStyle DropBackStyle
        {
            get => _dropBackStyle;

            set
            {
                if (_dropBackStyle != value)
                {
                    _dropBackStyle = value;
                    StateCommon.SetStyles(value);
                    PerformNeedPaint(true);
                }
            }
        }

        private void ResetDropBackStyle() => DropBackStyle = PaletteBackStyle.ControlClient;

        private bool ShouldSerializeDropBackStyle() => DropBackStyle != PaletteBackStyle.ControlClient;*/

        /// <summary>
        /// Gets and sets a value indicating if tooltips should be Displayed for button specs.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Should tooltips be Displayed for button specs.")]
        [DefaultValue(false)]
        public bool AllowButtonSpecToolTips { get; set; }

        /// <summary>
        /// Gets and sets a value indicating if button spec tooltips should remove the parent tooltip.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Should button spec tooltips should remove the parent tooltip")]
        [DefaultValue(false)]
        public bool AllowButtonSpecToolTipPriority { get; set; }

        /*/// <summary>
        /// Gets the collection of button specifications.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Collection of button specifications.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ComboBoxButtonSpecCollection ButtonSpecs { get; }*/

        /// <summary>
        /// Gets or sets the StringCollection to use when the AutoCompleteSource property is set to CustomSource.
        /// </summary>
        [Description(@"The StringCollection to use when the AutoCompleteSource property is set to CustomSource.")]
        [Editor(@"System.Windows.Forms.Design.ListControlStringCollectionEditor", typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Localizable(true)]
        [Browsable(true)]
        public AutoCompleteStringCollection AutoCompleteCustomSource
        {
            get => KryptonComboBoxControl!.AutoCompleteCustomSource;
            set => KryptonComboBoxControl!.AutoCompleteCustomSource = value;
        }

        /// <summary>
        /// Gets or sets the text completion behavior of the combobox.
        /// </summary>
        [Description(@"Indicates the text completion behavior of the combobox.")]
        [DefaultValue(AutoCompleteMode.None)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        public AutoCompleteMode AutoCompleteMode
        {
            get => KryptonComboBoxControl!.AutoCompleteMode;
            set => KryptonComboBoxControl!.AutoCompleteMode = value;
        }

        /// <summary>
        /// Gets or sets the autocomplete source, which can be one of the values from AutoCompleteSource enumeration.
        /// </summary>
        [Description(@"The autocomplete source, which can be one of the values from AutoCompleteSource enumeration.")]
        [DefaultValue(AutoCompleteSource.None)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        public AutoCompleteSource AutoCompleteSource
        {
            get => KryptonComboBoxControl!.AutoCompleteSource;
            set => KryptonComboBoxControl!.AutoCompleteSource = value;
        }

        /// <summary>
        /// Gets or sets the format specifier characters that indicate how a value is to be Displayed.
        /// </summary>
        [Description(@"The format specifier characters that indicate how a value is to be Displayed.")]
        [Editor(@"System.Windows.Forms.Design.FormatStringEditor", typeof(UITypeEditor))]
        [MergableProperty(false)]
        [DefaultValue(@"")]
        public string FormatString
        {
            get => KryptonComboBoxControl!.FormatString;
            set => KryptonComboBoxControl!.FormatString = value;
        }

        /// <summary>
        /// Gets or sets if this property is true, the value of FormatString is used to convert the value of DisplayMember into a value that can be Displayed.
        /// </summary>
        [Description(@"If this property is true, the value of FormatString is used to convert the value of DisplayMember into a value that can be Displayed.")]
        [DefaultValue(false)]
        public bool FormattingEnabled
        {
            get => KryptonComboBoxControl!.FormattingEnabled;
            set => KryptonComboBoxControl!.FormattingEnabled = value;
        }

        /*/// <summary>
        /// Gets access to the common combobox appearance entries that other states can override.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Overrides for defining common combobox appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteComboBoxRedirect StateCommon { get; }

        private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

        /// <summary>
        /// Gets access to the disabled combobox appearance entries.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Overrides for defining disabled combobox appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteComboBoxStates StateDisabled { get; }

        private bool ShouldSerializeStateDisabled() => !StateDisabled.IsDefault;

        /// <summary>
        /// Gets access to the normal combobox appearance entries.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Overrides for defining normal combobox appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteComboBoxStates StateNormal { get; }

        private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;

        /// <summary>
        /// Gets access to the active combobox appearance entries.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Overrides for defining active combobox appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteComboBoxJustComboStates StateActive { get; }

        private bool ShouldSerializeStateActive() => !StateActive.IsDefault;

        /// <summary>
        /// Gets access to the tracking combobox appearance entries.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Overrides for defining tracking combobox appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteComboBoxJustItemStates StateTracking { get; }

        private bool ShouldSerializeStateTracking() => !StateTracking.IsDefault;*/

        /// <summary>
        /// Finds the first item in the combo box that starts with the specified string.
        /// </summary>
        /// <param name="str">The String to search for.</param>
        /// <returns>The zero-based index of the first item found; returns -1 if no match is found.</returns>
        public int FindString(string str) => KryptonComboBoxControl!.FindString(str);

        /// <summary>
        /// Finds the first item after the given index which starts with the given string. The search is not case sensitive.
        /// </summary>
        /// <param name="str">The String to search for.</param>
        /// <param name="startIndex">The zero-based index of the item before the first item to be searched. Set to -1 to search from the beginning of the control.</param>
        /// <returns>The zero-based index of the first item found; returns -1 if no match is found, or 0 if the s parameter specifies Empty.</returns>
        public int FindString(string str, int startIndex) => KryptonComboBoxControl!.FindString(str, startIndex);

        /// <summary>
        /// Finds the first item in the combo box that matches the specified string.
        /// </summary>
        /// <param name="str">The String to search for.</param>
        /// <returns>The zero-based index of the first item found; returns -1 if no match is found.</returns>
        public int FindStringExact(string str) => KryptonComboBoxControl!.FindStringExact(str);

        /// <summary>
        /// Finds the first item after the specified index that matches the specified string.
        /// </summary>
        /// <param name="str">The String to search for.</param>
        /// <param name="startIndex">The zero-based index of the item before the first item to be searched. Set to -1 to search from the beginning of the control.</param>
        /// <returns>The zero-based index of the first item found; returns -1 if no match is found, or 0 if the s parameter specifies Empty.</returns>
        public int FindStringExact(string str, int startIndex) => KryptonComboBoxControl!.FindStringExact(str, startIndex);

        /// <summary>
        /// Returns the height of an item in the ComboBox.
        /// </summary>
        /// <param name="index">The index of the item to return the height of.</param>
        /// <returns>The height, in pixels, of the item at the specified index.</returns>
        public int GetItemHeight(int index) => KryptonComboBoxControl!.GetItemHeight(index);

        /// <summary>
        /// Returns the text representation of the specified item.
        /// </summary>
        /// <param name="item">The object from which to get the contents to display.</param>
        /// <returns>If the DisplayMember property is not specified, the value returned by GetItemText is the value of the item's ToString method. Otherwise, the method returns the string value of the member specified in the DisplayMember property for the object specified in the item parameter.</returns>
        public string? GetItemText(object? item) => KryptonComboBoxControl!.GetItemText(item);

        /// <summary>
        /// Selects a range of text in the control.
        /// </summary>
        /// <param name="start">The position of the first character in the current text selection within the text box.</param>
        /// <param name="length">The number of characters to select.</param>
        public void Select(int start, int length) => KryptonComboBoxControl!.Select(start, length);

        /// <summary>
        /// Selects all text in the control.
        /// </summary>
        public void SelectAll() => KryptonComboBoxControl!.SelectAll();

        /// <summary>
        /// Maintains performance when items are added to the ComboBox one at a time.
        /// </summary>
        public void BeginUpdate() => KryptonComboBoxControl!.BeginUpdate();

        /// <summary>
        /// Resumes painting the ComboBox control after painting is suspended by the BeginUpdate method. 
        /// </summary>
        public void EndUpdate() => KryptonComboBoxControl!.EndUpdate();

        /// <summary>
        /// Gets access to the ToolTipManager used for displaying tool tips.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToolTipManager ToolTipManager { get; }

        /// <summary>
        /// Sets input focus to the control.
        /// </summary>
        /// <returns>true if the input focus request was successful; otherwise, false.</returns>
        public new bool Focus() => ComboBox.Focus();

        /// <summary>
        /// Activates the control.
        /// </summary>
        public new void Select() => ComboBox.Select();

        // Ask the current view for a decision
        /// <summary>
        /// Internal designing mode method.
        /// </summary>
        public void DesignerMouseLeave() =>
            // Simulate the mouse leaving the control so that the tracking
            // element that thinks it has the focus is informed it does not
            OnMouseLeave(EventArgs.Empty);

        /// <summary>Gets or sets the height and width of the control.</summary>
        [DefaultValue(typeof(Size), "121, 21")]
        public new Size Size
        {
            get => KryptonComboBoxControl!.Size;

            set
            {
                KryptonComboBoxControl!.Size = value;

                KryptonComboBoxControl!.UpdateDropDownWidth(value);
            }
        }

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="KryptonToolStripComboBox" /> class.</summary>
        public KryptonToolStripComboBox() : base(new KryptonComboBox())
        {
            AutoSize = false;
        }

        #endregion

        #region Protected Overrides

        /// <inheritdoc />
        protected override void OnSubscribeControlEvents(Control? control)
        {
            base.OnSubscribeControlEvents(control);
        }

        /// <inheritdoc />
        protected override void OnUnsubscribeControlEvents(Control? control)
        {
            base.OnUnsubscribeControlEvents(control);
        }

        #endregion
    }
}
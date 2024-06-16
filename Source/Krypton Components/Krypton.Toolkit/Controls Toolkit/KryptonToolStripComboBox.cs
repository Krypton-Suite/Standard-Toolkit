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

        /// <summary>Gets access to the krypton ComboBox control.</summary>
        /// <value>The krypton ComboBox control.</value>
        [RefreshProperties(RefreshProperties.All)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Description(@"Access to the hosted KryptonComboBox.")]
        public KryptonComboBox? KryptonComboBoxControl => Control as KryptonComboBox;

        #endregion

        #region Public

        /// <inheritdoc />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Color BackColor { get; set; }

        /// <inheritdoc />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Color ForeColor { get; set; }

        /// <inheritdoc />
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Image BackgroundImage { get; set; }

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
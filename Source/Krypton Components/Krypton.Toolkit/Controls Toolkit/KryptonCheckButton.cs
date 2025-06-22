#region BSD License
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

namespace Krypton.Toolkit;

/// <summary>
/// Presents the user with a binary choice such as Yes/No or True/False.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonCheckButton), "ToolboxBitmaps.KryptonCheckButton.bmp")]
[DefaultEvent(nameof(Click))]
[DefaultProperty(nameof(Text))]
[Designer(typeof(KryptonCheckButtonDesigner))]
[DesignerCategory(@"code")]
[Description(@"Toggles checked state when user clicks button.")]
public class KryptonCheckButton : KryptonButton
{
    #region Instance Fields

    private readonly PaletteTripleOverride _overrideCheckedFocus;
    private readonly PaletteTripleOverride _overrideCheckedNormal;
    private readonly PaletteTripleOverride _overrideCheckedTracking;
    private readonly PaletteTripleOverride _overrideCheckedPressed;
    private CheckButtonValues _checkedValues;
    private bool _wasChecked;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when the value of the Checked property is about to change.
    /// </summary>
    [Category(@"Property Changing")]
    [Description(@"Occurs whenever the Checked property is about to change.")]
    public event CancelEventHandler? CheckedChanging;

    /// <summary>
    /// Occurs when the value of the Checked property has changed.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs whenever the Checked property has changed.")]
    public event EventHandler? CheckedChanged;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonCheckButton class.
    /// </summary>
    public KryptonCheckButton()
    {
        // Create the extra state needed for the checked additions the the base button
        StateCheckedNormal = new PaletteTriple(StateCommon, NeedPaintDelegate);
        StateCheckedTracking = new PaletteTriple(StateCommon, NeedPaintDelegate);
        StateCheckedPressed = new PaletteTriple(StateCommon, NeedPaintDelegate);

        // Create the override handling classes
        _overrideCheckedFocus = new PaletteTripleOverride(OverrideFocus, StateCheckedNormal, PaletteState.FocusOverride);
        _overrideCheckedNormal = new PaletteTripleOverride(OverrideDefault, _overrideCheckedFocus, PaletteState.NormalDefaultOverride);
        _overrideCheckedTracking = new PaletteTripleOverride(OverrideFocus, StateCheckedTracking, PaletteState.FocusOverride);
        _overrideCheckedPressed = new PaletteTripleOverride(OverrideFocus, StateCheckedPressed, PaletteState.FocusOverride);

        // Add the checked specific palettes to the existing view button
        ViewDrawButton.SetCheckedPalettes(_overrideCheckedNormal,
            _overrideCheckedTracking,
            _overrideCheckedPressed);
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets access to the normal checked button appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal checked button appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StateCheckedNormal { get; }

    private bool ShouldSerializeStateCheckedNormal() => !StateCheckedNormal.IsDefault;

    /// <summary>
    /// Gets access to the hot tracking checked button appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining hot tracking checked button appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StateCheckedTracking { get; }

    private bool ShouldSerializeStateCheckedTracking() => !StateCheckedTracking.IsDefault;

    /// <summary>
    /// Gets access to the pressed checked button appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining pressed checked button appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StateCheckedPressed { get; }

    private bool ShouldSerializeStateCheckedPressed() => !StateCheckedPressed.IsDefault;

    /// <summary>
    /// Gets or sets a value indicating whether the KryptonCheckButton is in the checked state. 
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Indicates whether the control is in the checked state.")]
    [DefaultValue(false)]
    [Bindable(true)]
    public bool Checked
    {
        get => ViewDrawButton.Checked;

        set
        {
            if (value != ViewDrawButton.Checked)
            {
                // Generate a pre-change event allowing it to be cancelled
                var ce = new CancelEventArgs();
                OnCheckedChanging(ce);

                // If the change is allowed to occur
                if (!ce.Cancel)
                {
                    // Use new checked state
                    ViewDrawButton.Checked = value;

                    // Generate the change event
                    OnCheckedChanged(EventArgs.Empty);

                    // Need to repaint to reflect change in visual state
                    PerformNeedPaint(true);
                }
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the user can uncheck the button when in the checked state.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether the user can uncheck the button when in the checked state.")]
    [DefaultValue(true)]
    public bool AllowUncheck
    {
        get => ViewDrawButton.AllowUncheck;
        set => ViewDrawButton.AllowUncheck = value;
    }

    /// <summary>
    /// Gets and sets the associated KryptonCommand.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Command associated with the check button.")]
    [DefaultValue(null)]
    public override IKryptonCommand? KryptonCommand
    {
        get => base.KryptonCommand;

        set
        {
            if (base.KryptonCommand != value)
            {
                if (base.KryptonCommand == null)
                {
                    _wasChecked = Checked;
                }

                base.KryptonCommand = value;

                if (base.KryptonCommand == null)
                {
                    Checked = _wasChecked;
                }
            }
        }
    }
    #endregion

    #region Protected Overrides
    /// <summary>
    /// Raises the GotFocus event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnGotFocus(EventArgs e)
    {
        if (!ViewDrawButton.IsFixed)
        {
            // Apply the focus overrides
            _overrideCheckedFocus.Apply = true;
            _overrideCheckedTracking.Apply = true;
            _overrideCheckedPressed.Apply = true;
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
            _overrideCheckedFocus.Apply = false;
            _overrideCheckedTracking.Apply = false;
            _overrideCheckedPressed.Apply = false;
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
        // Toggle the checked state of the button
        if (!Checked || AllowUncheck)
        {
            Checked = !Checked;
        }

        // Let base class fire standard event
        base.OnClick(e);
    }

    /// <summary>
    /// Raises the KryptonCommandChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected override void OnKryptonCommandChanged(EventArgs e)
    {
        // Let base class update with base button properties
        base.OnKryptonCommandChanged(e);

        // Update the check button specific properties from the command
        if (KryptonCommand != null)
        {
            Checked = KryptonCommand.Checked;
        }
    }

    /// <summary>
    /// Handles a change in the property of an attached command.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A PropertyChangedEventArgs that contains the event data.</param>
    protected override void OnCommandPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        Checked = e.PropertyName switch
        {
            nameof(CheckState) => KryptonCommand!.Checked,
            _ => Checked
        };

        base.OnCommandPropertyChanged(sender, e);
    }

    /// <summary>
    /// Creates a values storage object appropriate for control.
    /// </summary>
    /// <returns>Set of button values.</returns>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    protected override ButtonValues CreateButtonValues(NeedPaintHandler needPaint)
    {
        // Create a version of button values with checked entries
        _checkedValues = new CheckButtonValues(needPaint);
        return _checkedValues;
    }
    #endregion

    #region Protected Virtual
    /// <summary>
    /// Raises the CheckedChanging event.
    /// </summary>
    /// <param name="e">A CancelEventArgs containing the event data.</param>
    protected virtual void OnCheckedChanging(CancelEventArgs e) => CheckedChanging?.Invoke(this, e);

    /// <summary>
    /// Raises the CheckedChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnCheckedChanged(EventArgs e)
    {
        CheckedChanged?.Invoke(this, e);

        // If there is a command associated then update with new state
        if (KryptonCommand != null)
        {
            KryptonCommand.Checked = Checked;
        }
    }
    #endregion
}
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
/// Display text and images with the styling features of the Krypton Toolkit
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonLinkLabel), "ToolboxBitmaps.KryptonLinkLabel.bmp")]
[DefaultEvent(nameof(LinkClicked))]
[DefaultProperty(nameof(Text))]
[DefaultBindingProperty(nameof(Text))]
[Designer(typeof(KryptonLinkLabelDesigner))]
[DesignerCategory(@"code")]
[Description(@"Displays descriptive information as a hyperlink.")]
public class KryptonLinkLabel : KryptonLabel
{
    #region Instance Fields

    private readonly PaletteContentInheritRedirect _stateVisitedRedirect;
    private readonly PaletteContentInheritRedirect _stateNotVisitedRedirect;
    private readonly PaletteContentInheritRedirect _statePressedRedirect;
    private readonly PaletteContentInheritRedirect _stateFocusRedirect;
    private readonly PaletteContentInheritOverride _overrideVisited;
    private readonly PaletteContentInheritOverride _overrideNotVisited;
    private readonly PaletteContentInheritOverride _overrideFocusNotVisited;
    private readonly PaletteContentInheritOverride _overridePressed;
    private readonly PaletteContentInheritOverride _overridePressedFocus;
    private readonly LinkLabelBehaviorInherit _inheritBehavior;
    private readonly LinkLabelController _controller;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when the link is clicked.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the link is clicked.")]
    public event EventHandler? LinkClicked;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonLinkLabel class.
    /// </summary>
    public KryptonLinkLabel()
    {
        // The link label cannot take the focus
        SetStyle(ControlStyles.Selectable, true);

        // Turn off the target functionality present in the base class
        EnabledTarget = false;

        // Create the override states that redirect without inheriting
        _stateVisitedRedirect = new PaletteContentInheritRedirect(Redirector, PaletteContentStyle.LabelNormalPanel);
        _stateNotVisitedRedirect = new PaletteContentInheritRedirect(Redirector, PaletteContentStyle.LabelNormalPanel);
        _statePressedRedirect = new PaletteContentInheritRedirect(Redirector, PaletteContentStyle.LabelNormalPanel);
        _stateFocusRedirect = new PaletteContentInheritRedirect(Redirector, PaletteContentStyle.LabelNormalPanel);
        OverrideVisited = new PaletteContent(_stateVisitedRedirect, NeedPaintDelegate);
        OverrideNotVisited = new PaletteContent(_stateNotVisitedRedirect, NeedPaintDelegate);
        OverrideFocus = new PaletteContent(_stateFocusRedirect, NeedPaintDelegate);
        OverridePressed = new PaletteContent(_statePressedRedirect, NeedPaintDelegate);

        // Override the normal state to implement the underling logic
        _inheritBehavior = new LinkLabelBehaviorInherit(StateNormal, KryptonLinkBehavior.AlwaysUnderline);

        // Create the override handling classes
        _overrideVisited = new PaletteContentInheritOverride(OverrideVisited, _inheritBehavior, PaletteState.LinkVisitedOverride, false);
        _overrideNotVisited = new PaletteContentInheritOverride(OverrideNotVisited, _overrideVisited, PaletteState.LinkNotVisitedOverride, true);
        _overrideFocusNotVisited = new PaletteContentInheritOverride(OverrideFocus, _overrideNotVisited, PaletteState.FocusOverride, false);
        _overridePressed = new PaletteContentInheritOverride(OverridePressed, _inheritBehavior, PaletteState.LinkPressedOverride, false);
        _overridePressedFocus = new PaletteContentInheritOverride(OverrideFocus, _overridePressed, PaletteState.FocusOverride, false);

        // Create controller for updating the view state/click events
        _controller = new LinkLabelController(ViewDrawContent, StateDisabled, _overrideFocusNotVisited, _overrideFocusNotVisited, _overridePressedFocus, _overridePressed, NeedPaintDelegate);
        _controller.Click += OnControllerClick;
        ViewDrawContent.MouseController = _controller;
        ViewDrawContent.KeyController = _controller;
        ViewDrawContent.SourceController = _controller;

        // Set initial palette for drawing the content
        ViewDrawContent.SetPalette(_overrideFocusNotVisited);
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets a value that determines the underline behavior of the link label.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Determines the underline behavior of the link label.")]
    public KryptonLinkBehavior LinkBehavior
    {
        get => _inheritBehavior.LinkBehavior;

        set
        {
            if (_inheritBehavior.LinkBehavior != value)
            {
                _inheritBehavior.LinkBehavior = value;
                PerformNeedPaint(false);
            }
        }
    }

    private void ResetLinkBehavior() => LinkBehavior = KryptonLinkBehavior.AlwaysUnderline;

    private bool ShouldSerializeLinkBehavior() => LinkBehavior != KryptonLinkBehavior.AlwaysUnderline;

    /// <summary>
    /// Gets and sets a value indicating if the label has been visited.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Indicates if the hyperlink has been visited already.")]
    [DefaultValue(false)]
    public bool LinkVisited
    {
        get => _overrideVisited.Apply;

        set
        {
            if (_overrideVisited.Apply != value)
            {
                _overrideVisited.Apply = value;
                _overrideNotVisited.Apply = !value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets access to the pressed label appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining pressed label appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteContent OverridePressed { get; }

    private bool ShouldSerializeOverridePressed() => !OverridePressed.IsDefault;

    /// <summary>
    /// Gets access to the label appearance when it has focus.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining label appearance when it has focus.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteContent OverrideFocus { get; }

    private bool ShouldSerializeOverrideFocus() => !OverrideFocus.IsDefault;

    /// <summary>
    /// Gets access to normal state modifications when label has been visited.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for modifying normal state when label has been visited.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteContent OverrideVisited { get; }

    private bool ShouldSerializeOverrideVisited() => !OverrideVisited.IsDefault;

    /// <summary>
    /// Gets access to normal state modifications when label has not been visited.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for modifying normal state when label has not been visited.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteContent OverrideNotVisited { get; }

    private bool ShouldSerializeOverrideNotVisited() => !OverrideNotVisited.IsDefault;

    /// <summary>
    /// Gets access to the target for mnemonic and click actions.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Control? Target
    {
        get => base.Target;
        set => base.Target = value;
    }

    /// <summary>
    /// Fix the control to a particular palette state.
    /// </summary>
    /// <param name="state">Palette state to fix.</param>
    public override void SetFixedState(PaletteState state)
    {
        // Let base class update state
        base.SetFixedState(state);

        // Update display to reflect change
        _controller.Update(this);
        PerformNeedPaint(true);
    }
    #endregion

    #region Protected Virtual
    /// <summary>
    /// Raises the LinkClicked event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnLinkClicked(LinkClickedEventArgs e)
    {
        LinkClicked?.Invoke(this, e);

        // If we have an attached command then execute it
        KryptonCommand?.PerformExecute();
    }
    #endregion

    #region Protected Overrides
    /// <summary>
    /// Raises the EnabledChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnEnabledChanged(EventArgs e)
    {
        // Let base class fire standard event
        base.OnEnabledChanged(e);

        // Ask controller to update with correct palette to match state
        _controller.Update(this);
    }

    /// <summary>
    /// Raises the GotFocus event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnGotFocus(EventArgs e)
    {
        // Apply the focus overrides
        _overrideFocusNotVisited.Apply = true;
        _overridePressedFocus.Apply = true;

        // Change in focus requires a repaint
        PerformNeedPaint(true);

        // Let base class fire standard event
        base.OnGotFocus(e);
    }

    /// <summary>
    /// Raises the LostFocus event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnLostFocus(EventArgs e)
    {
        // Apply the focus overrides
        _overrideFocusNotVisited.Apply = false;
        _overridePressedFocus.Apply = false;

        // Change in focus requires a repaint
        PerformNeedPaint(true);

        // Let base class fire standard event
        base.OnLostFocus(e);
    }

    /// <summary>
    /// Update the view elements based on the requested label style.
    /// </summary>
    /// <param name="style">New label style.</param>
    protected override void SetLabelStyle(LabelStyle style)
    {
        // Let base class update the standard label style
        base.SetLabelStyle(style);

        PaletteContentStyle contentStyle = CommonHelper.ContentStyleFromLabelStyle(style);

        // Update all redirectors with new style
        _stateVisitedRedirect.Style = contentStyle;
        _stateNotVisitedRedirect.Style = contentStyle;
        _statePressedRedirect.Style = contentStyle;
        _stateFocusRedirect.Style = contentStyle;
    }
    #endregion

    #region Implementation
    private void OnControllerClick(object? sender, MouseEventArgs e) => OnLinkClicked(new LinkClickedEventArgs(Text));

    #endregion
}
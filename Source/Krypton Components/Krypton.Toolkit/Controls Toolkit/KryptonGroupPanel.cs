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
/// Special panel used in the KryptonGroup and KryptonHeaderGroup controls.
/// </summary>
[ToolboxItem(false)]
[DesignerCategory(@"code")]
[ToolboxBitmap(typeof(KryptonGroupPanel), "ToolboxBitmaps.KryptonGroupPanel.bmp")]
[Designer(typeof(KryptonGroupPanelDesigner))]
[Description(@"Enables you to group collections of controls.")]
[Docking(DockingBehavior.Never)]
public class KryptonGroupPanel : KryptonPanel
{
    #region Instance Fields
    private readonly PaletteBackInheritForced _forcedDisabled;
    private readonly PaletteBackInheritForced _forcedNormal;
    private readonly NeedPaintHandler? _layoutHandler;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when the value of the AutoSize property changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new event EventHandler? AutoSizeChanged;

    /// <summary>
    /// Occurs when the value of the Dock property changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new event EventHandler? DockChanged;

    /// <summary>
    /// Occurs when the value of the Location property changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new event EventHandler? LocationChanged;

    /// <summary>
    /// Occurs when the value of the TabIndex property changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new event EventHandler? TabIndexChanged;

    /// <summary>
    /// Occurs when the value of the TabStop property changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new event EventHandler? TabStopChanged;

    /// <summary>
    /// Occurs when the value of the Visible property changes.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new event EventHandler? VisibleChanged;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonGroupPanel class.
    /// </summary>
    /// <param name="alignControl">Container control for alignment.</param>
    /// <param name="stateCommon">Common appearance state to inherit from.</param>
    /// <param name="stateDisabled">Disabled appearance state.</param>
    /// <param name="stateNormal">Normal appearance state.</param>
    /// <param name="layoutHandler">Callback delegate for layout processing.</param>
    public KryptonGroupPanel(Control alignControl,
        [DisallowNull] PaletteDoubleRedirect stateCommon,
        [DisallowNull] PaletteDouble stateDisabled,
        [DisallowNull] PaletteDouble stateNormal,
        NeedPaintHandler? layoutHandler)
        : base(stateCommon, stateDisabled, stateNormal)
    {
        // Remember the delegate used to notify layouts
        _layoutHandler = layoutHandler;

        // Create the forced overrides to enforce the graphics option we want
        _forcedDisabled = new PaletteBackInheritForced(stateDisabled.Back);
        _forcedNormal = new PaletteBackInheritForced(stateNormal.Back);

        // We never allow the anti alias option as it prevent transparent background working
        _forcedDisabled.ForceGraphicsHint = PaletteGraphicsHint.None;
        _forcedNormal.ForceGraphicsHint = PaletteGraphicsHint.None;

        // Set the correct initial palettes
        ViewDrawPanel.SetPalettes(Enabled ? _forcedNormal : _forcedDisabled);

        // Make sure the alignment of the group panel is as that of the parent
        ViewManager!.AlignControl = alignControl;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets or sets how a KryptonSplitterPanel attaches to the edges of the KryptonSplitContainer.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override AnchorStyles Anchor
    {
        get => base.Anchor;
        set { /* Ignore request */ }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the KryptonSplitterPanel is automatically resized to display its entire contents.
    /// </summary>
    [Browsable(false)]
    [Bindable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool AutoSize
    {
        get => base.AutoSize;
        set { /* Ignore request */ }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the control is automatically resized to display its entire contents.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override AutoSizeMode AutoSizeMode
    {
        get => base.AutoSizeMode;
        set { /* Ignore request */ }
    }

    /// <summary>
    /// Gets or sets the border style for the KryptonSplitterPanel. 
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new BorderStyle BorderStyle
    {
        get => base.BorderStyle;
        set { /* Ignore request */ }
    }

    /// <summary>
    /// Gets or sets which edge of the KryptonSplitContainer that the KryptonSplitterPanel is docked to. 
    /// </summary>
    [Browsable(false)]
    [Bindable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override DockStyle Dock
    {
        get => base.Dock;
        set { /* Ignore request */ }
    }

    /// <summary>
    /// Gets the internal spacing between the KryptonSplitterPanel and its edges.
    /// </summary>
    [Browsable(false)]
    [Bindable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new DockPaddingEdges DockPadding => base.DockPadding;

    /// <summary>
    /// Gets or sets the height of the KryptonGroupPanel. 
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new int Height
    {
        get => base.Height;
        set => base.Height = value;
    }

    /// <summary>
    /// Gets or sets the coordinates of the upper-left corner of the KryptonSplitterPanel relative to the upper-left corner of its KryptonSplitContainer. 
    /// </summary>
    [Browsable(false)]
    [Bindable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Point Location
    {
        get => base.Location;
        set => base.Location = value;
    }

    /// <summary>
    /// The name of this KryptonSplitterPanel.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [AllowNull]
    public new string Name
    {
        get => base.Name;
        set => base.Name = value;
    }

    /// <summary>
    /// The name of this KryptonSplitterPanel.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Control? Parent
    {
        get => base.Parent;
        set => base.Parent = value;
    }

    /// <summary>
    /// Gets or sets the height and width of the KryptonSplitterPanel.
    /// </summary>
    [Browsable(false)]
    [Bindable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Size Size
    {
        get => base.Size;
        set => base.Size = value;
    }

    /// <summary>
    /// Gets or sets the tab order of the KryptonSplitterPanel within its KryptonSplitContainer.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new int TabIndex
    {
        get => base.TabIndex;
        set => base.TabIndex = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the user can give the focus to this KryptonSplitterPanel using the TAB key.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new bool TabStop
    {
        get => base.TabStop;
        set => base.TabStop = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the KryptonSplitterPanel is Displayed.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new bool Visible
    {
        get => base.Visible;
        set => base.Visible = value;
    }

    /// <summary>
    /// Gets or sets the width of the KryptonSplitterPanel. 
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new int Width
    {
        get => base.Width;
        set => base.Width = value;
    }

    /// <summary>
    /// Gets or sets the palette to be applied.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new PaletteMode PaletteMode
    {
        get => base.PaletteMode;
        set => base.PaletteMode = value;
    }

    /// <summary>
    /// Gets and sets the custom palette implementation.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new PaletteBase? Palette
    {
        get => base.Palette;
        set => base.Palette = value;
    }

    /// <summary>
    /// Gets and sets the panel style.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new PaletteBackStyle PanelBackStyle
    {
        get => base.PanelBackStyle;
        set => base.PanelBackStyle = value;
    }

    /// <summary>
    /// Gets access to the common panel appearance that other states can override.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new PaletteBack StateCommon => base.StateCommon;

    /// <summary>
    /// Gets access to the disabled panel appearance.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new PaletteBack StateDisabled => base.StateDisabled;

    /// <summary>
    /// Gets access to the normal panel appearance.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new PaletteBack StateNormal => base.StateNormal;

    #endregion

    #region Protected Overrides
    /// <summary>
    /// Gets the space, in pixels, that is specified by default between controls.
    /// </summary>
    protected override Padding DefaultMargin => new Padding(0, 0, 0, 0);

    /// <summary>
    /// Raises the Layout event.
    /// </summary>
    /// <param name="levent">A LayoutEventArgs that contains the event data.</param>
    protected override void OnLayout(LayoutEventArgs levent)
    {
        // Inform anyone interested that we are performing a layout call
        _layoutHandler?.Invoke(this, new NeedLayoutEventArgs(true));

        base.OnLayout(levent);
    }

    /// <summary>
    /// Raises the EnabledChanged event.
    /// </summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnEnabledChanged(EventArgs e)
    {
        // Let base class fire standard event
        base.OnEnabledChanged(e);

        // Update with the correct forced palette entry
        ViewDrawPanel.SetPalettes(Enabled ? _forcedNormal : _forcedDisabled);
    }

    /// <summary>
    /// Gets the control reference that is the parent for transparent drawing.
    /// </summary>
    protected override Control? TransparentParent
    {
        get
        {
            // Just in case there is not a parent yet
            if (Parent == null)
            {
                return null;
            }

            // Just in case the parent does not have a parent
            return Parent.Parent ?? Parent;

            // The KryptonGroupPanel is always a child within another 
            // Krypton control that should be considered the actual 
            // parent for transparent drawing purposes.
        }
    }

    /// <summary>
    /// Raises the AutoSizeChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected override void OnAutoSizeChanged(EventArgs e)
    {
        AutoSizeChanged?.Invoke(this, e);

        base.OnAutoSizeChanged(e);
    }

    /// <summary>
    /// Raises the DockChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected override void OnDockChanged(EventArgs e)
    {
        DockChanged?.Invoke(this, e);

        base.OnDockChanged(e);
    }

    /// <summary>
    /// Raises the LocationChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected override void OnLocationChanged(EventArgs e)
    {
        LocationChanged?.Invoke(this, e);

        base.OnLocationChanged(e);
    }

    /// <summary>
    /// Raises the TabIndexChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected override void OnTabIndexChanged(EventArgs e)
    {
        TabIndexChanged?.Invoke(this, e);

        base.OnTabIndexChanged(e);
    }

    /// <summary>
    /// Raises the TabStopChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected override void OnTabStopChanged(EventArgs e)
    {
        TabStopChanged?.Invoke(this, e);

        base.OnTabStopChanged(e);
    }

    /// <summary>
    /// Raises the VisibleChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected override void OnVisibleChanged(EventArgs e)
    {
        VisibleChanged?.Invoke(this, e);

        base.OnVisibleChanged(e);
    }
    #endregion    
}
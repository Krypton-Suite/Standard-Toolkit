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
/// Special panel used in the KryptonSplitContainer.
/// </summary>
[ToolboxItem(false)]
[DesignerCategory(@"code")]
[ToolboxBitmap(typeof(KryptonSplitterPanel), "ToolboxBitmaps.KryptonGroupPanel.bmp")]
[Designer(typeof(KryptonSplitterPanelDesigner))]
[Description(@"Enables you to group collections of controls.")]
[Docking(DockingBehavior.Never)]
public sealed class KryptonSplitterPanel : KryptonPanel
{
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
    /// Initialize a new instance of the KryptonSplitterPanel class.
    /// </summary>
    /// <param name="container">Reference to owning container.</param>
    public KryptonSplitterPanel([DisallowNull] KryptonSplitContainer container)
    {
        Debug.Assert(container is not null);

        Owner = container ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull(nameof(container)));
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
    /// Gets or sets the height of the KryptonSplitterPanel. 
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new int Height
    {
        get => Collapsed ? 0 : base.Height;

        set => throw new NotSupportedException("Cannot set the Height of a KryptonSplitterPanel");
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
    /// Gets or sets the size that is the upper limit that GetPreferredSize can specify.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Size MaximumSize
    {
        get => base.MaximumSize;
        set => base.MaximumSize = value;
    }

    /// <summary>
    /// Gets or sets the size that is the lower limit that GetPreferredSize can specify.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Size MinimumSize
    {
        get => base.MinimumSize;
        set => base.MinimumSize = value;
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
        get => Collapsed ? Size.Empty : base.Size;

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
        get => Collapsed ? 0 : base.Width;

        set => throw new NotSupportedException("Cannot set the Width of a KryptonSplitterPanel");
    }

    #endregion

    #region Protected Overrides
    /// <summary>
    /// Gets the space, in pixels, that is specified by default between controls.
    /// </summary>
    protected override Padding DefaultMargin => new Padding(0, 0, 0, 0);

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

    #region Internal
    internal KryptonSplitContainer Owner { get; }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal bool Collapsed { get; set; }

    #endregion
}
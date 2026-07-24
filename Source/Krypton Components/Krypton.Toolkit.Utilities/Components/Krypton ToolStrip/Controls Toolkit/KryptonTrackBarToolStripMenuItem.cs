#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

[ToolboxBitmap(typeof(KryptonTrackBar)), ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
public class KryptonTrackBarToolStripMenuItem : KryptonToolStripControlHostFixed
{
    // Attributes ========================================================
    private readonly TrackBarHostValues _values;

    // Properties ========================================================
    /// <summary>
    /// Gets the KryptonTrackBar control.
    /// </summary>
    /// <value>The KryptonTrackBar control.</value>
    [RefreshProperties(RefreshProperties.All),
     DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonTrackBar? KryptonTrackBarControl => Control as KryptonTrackBar;

    /// <summary>
    /// Gets the expandable configuration values mirroring the hosted <see cref="KryptonTrackBarControl"/> settings.
    /// </summary>
    [Category("Behavior")]
    [Description("Value, range, tick, and orientation settings for the hosted track bar.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public TrackBarHostValues TrackBarValues => _values;

    private bool ShouldSerializeTrackBarValues() => !_values.IsDefault;

    private void ResetTrackBarValues() => _values.Reset();

    // Constructor ========================================================
    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonTrackBarToolStripMenuItem"/> class.
    /// </summary>
    public KryptonTrackBarToolStripMenuItem()
        : base(new KryptonTrackBar())
    {
        _values = new TrackBarHostValues(this);

        AutoSize = false;
    }

    /// <summary>
    /// Retrieves the size of a rectangular area into which a control can be fitted.
    /// </summary>
    /// <param name="constrainingSize">The custom-sized area for a control.</param>
    /// <returns>
    /// An ordered pair of type <see cref="T:System.Drawing.Size"></see> representing the width and height of a rectangle.
    /// </returns>
    /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    public override Size GetPreferredSize(Size constrainingSize) =>
        //return base.GetPreferredSize(constrainingSize);
        KryptonTrackBarControl!.GetPreferredSize(constrainingSize);

    /// <summary>
    /// Subscribes events from the hosted control.
    /// </summary>
    /// <param name="control">The control from which to subscribe events.</param>
    protected override void OnSubscribeControlEvents(Control? control)
    {
        base.OnSubscribeControlEvents(control);

        if (control is KryptonTrackBar trackBar)
        {
            trackBar.ValueChanged += OnValueChanged;
        }
    }

    /// <summary>
    /// Unsubscribes events from the hosted control.
    /// </summary>
    /// <param name="control">The control from which to unsubscribe events.</param>
    protected override void OnUnsubscribeControlEvents(Control? control)
    {
        if (control is KryptonTrackBar trackBar)
        {
            trackBar.ValueChanged -= OnValueChanged;
        }

        base.OnUnsubscribeControlEvents(control);
    }

    #region ... exposed properties ...
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int Value
    {
        get => _values.Value;
        set => _values.Value = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int LargeChange
    {
        get => _values.LargeChange;
        set => _values.LargeChange = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SmallChange
    {
        get => _values.SmallChange;
        set => _values.SmallChange = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int Maximum
    {
        get => _values.Maximum;
        set => _values.Maximum = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int Minimum
    {
        get => _values.Minimum;
        set => _values.Minimum = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool VolumeControl
    {
        get => _values.VolumeControl;
        set => _values.VolumeControl = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public PaletteTrackBarSize TrackBarSize
    {
        get => _values.TrackBarSize;
        set => _values.TrackBarSize = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public TickStyle TickStyle
    {
        get => _values.TickStyle;
        set => _values.TickStyle = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int TickFrequency
    {
        get => _values.TickFrequency;
        set => _values.TickFrequency = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Orientation Orientation
    {
        get => _values.Orientation;
        set => _values.Orientation = value;
    }
    #endregion

    #region ... exposed events ...
    public event EventHandler? ValueChanged;
    protected void OnValueChanged(object? sender, EventArgs e) => ValueChanged?.Invoke(this, e);

    #endregion
}
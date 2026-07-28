#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Expandable configuration for the <see cref="KryptonTrackBarToolStripMenuItem"/> tool strip host. Mirrors the
/// settings of the hosted <see cref="KryptonTrackBar"/> (<see cref="KryptonTrackBarToolStripMenuItem.KryptonTrackBarControl"/>).
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class TrackBarHostValues : Storage
{
    #region Constants

    private const int DEFAULT_VALUE = 0;
    private const int DEFAULT_LARGE_CHANGE = 5;
    private const int DEFAULT_SMALL_CHANGE = 1;
    private const int DEFAULT_MAXIMUM = 10;
    private const int DEFAULT_MINIMUM = 0;
    private const bool DEFAULT_VOLUME_CONTROL = false;
    private const PaletteTrackBarSize DEFAULT_TRACK_BAR_SIZE = PaletteTrackBarSize.Medium;
    private const TickStyle DEFAULT_TICK_STYLE = TickStyle.BottomRight;
    private const int DEFAULT_TICK_FREQUENCY = 1;
    private const Orientation DEFAULT_ORIENTATION = Orientation.Horizontal;

    #endregion

    #region Instance Fields

    private readonly KryptonTrackBarToolStripMenuItem _owner;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="TrackBarHostValues"/> class.
    /// </summary>
    /// <param name="owner">Owning track bar host.</param>
    public TrackBarHostValues(KryptonTrackBarToolStripMenuItem owner) =>
        _owner = owner ?? throw new ArgumentNullException(nameof(owner));

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? @"Modified" : string.Empty;

    #endregion

    #region IsDefault

    /// <inheritdoc />
    [Browsable(false)]
    public override bool IsDefault =>
        _owner.KryptonTrackBarControl is null ||
        (_owner.KryptonTrackBarControl.Value == DEFAULT_VALUE &&
         _owner.KryptonTrackBarControl.LargeChange == DEFAULT_LARGE_CHANGE &&
         _owner.KryptonTrackBarControl.SmallChange == DEFAULT_SMALL_CHANGE &&
         _owner.KryptonTrackBarControl.Maximum == DEFAULT_MAXIMUM &&
         _owner.KryptonTrackBarControl.Minimum == DEFAULT_MINIMUM &&
         !_owner.KryptonTrackBarControl.VolumeControl &&
         _owner.KryptonTrackBarControl.TrackBarSize == DEFAULT_TRACK_BAR_SIZE &&
         _owner.KryptonTrackBarControl.TickStyle == DEFAULT_TICK_STYLE &&
         _owner.KryptonTrackBarControl.TickFrequency == DEFAULT_TICK_FREQUENCY &&
         _owner.KryptonTrackBarControl.Orientation == DEFAULT_ORIENTATION);

    #endregion

    #region Public

    /// <summary>Gets or sets the current value of the track bar.</summary>
    [Category(@"Behavior")]
    [DefaultValue(DEFAULT_VALUE)]
    public int Value
    {
        get => _owner.KryptonTrackBarControl!.Value;
        set => _owner.KryptonTrackBarControl!.Value = value;
    }

    /// <summary>Gets or sets the large change amount.</summary>
    [Category(@"Behavior")]
    [DefaultValue(DEFAULT_LARGE_CHANGE)]
    public int LargeChange
    {
        get => _owner.KryptonTrackBarControl!.LargeChange;
        set => _owner.KryptonTrackBarControl!.LargeChange = value;
    }

    /// <summary>Gets or sets the small change amount.</summary>
    [Category(@"Behavior")]
    [DefaultValue(DEFAULT_SMALL_CHANGE)]
    public int SmallChange
    {
        get => _owner.KryptonTrackBarControl!.SmallChange;
        set => _owner.KryptonTrackBarControl!.SmallChange = value;
    }

    /// <summary>Gets or sets the maximum value.</summary>
    [Category(@"Behavior")]
    [DefaultValue(DEFAULT_MAXIMUM)]
    public int Maximum
    {
        get => _owner.KryptonTrackBarControl!.Maximum;
        set => _owner.KryptonTrackBarControl!.Maximum = value;
    }

    /// <summary>Gets or sets the minimum value.</summary>
    [Category(@"Behavior")]
    [DefaultValue(DEFAULT_MINIMUM)]
    public int Minimum
    {
        get => _owner.KryptonTrackBarControl!.Minimum;
        set => _owner.KryptonTrackBarControl!.Minimum = value;
    }

    /// <summary>Gets or sets a value indicating whether the track bar is displayed as a volume control.</summary>
    [Category(@"Appearance")]
    [DefaultValue(DEFAULT_VOLUME_CONTROL)]
    public bool VolumeControl
    {
        get => _owner.KryptonTrackBarControl!.VolumeControl;
        set => _owner.KryptonTrackBarControl!.VolumeControl = value;
    }

    /// <summary>Gets or sets the size of the track bar.</summary>
    [Category(@"Appearance")]
    [DefaultValue(DEFAULT_TRACK_BAR_SIZE)]
    public PaletteTrackBarSize TrackBarSize
    {
        get => _owner.KryptonTrackBarControl!.TrackBarSize;
        set => _owner.KryptonTrackBarControl!.TrackBarSize = value;
    }

    /// <summary>Gets or sets the tick mark style.</summary>
    [Category(@"Appearance")]
    [DefaultValue(DEFAULT_TICK_STYLE)]
    public TickStyle TickStyle
    {
        get => _owner.KryptonTrackBarControl!.TickStyle;
        set => _owner.KryptonTrackBarControl!.TickStyle = value;
    }

    /// <summary>Gets or sets the tick frequency.</summary>
    [Category(@"Appearance")]
    [DefaultValue(DEFAULT_TICK_FREQUENCY)]
    public int TickFrequency
    {
        get => _owner.KryptonTrackBarControl!.TickFrequency;
        set => _owner.KryptonTrackBarControl!.TickFrequency = value;
    }

    /// <summary>Gets or sets the orientation of the track bar.</summary>
    [Category(@"Appearance")]
    [DefaultValue(DEFAULT_ORIENTATION)]
    public Orientation Orientation
    {
        get => _owner.KryptonTrackBarControl!.Orientation;
        set => _owner.KryptonTrackBarControl!.Orientation = value;
    }

    /// <summary>
    /// Resets all values to their defaults.
    /// </summary>
    public void Reset()
    {
        if (_owner.KryptonTrackBarControl is { } trackBar)
        {
            trackBar.Value = DEFAULT_VALUE;
            trackBar.LargeChange = DEFAULT_LARGE_CHANGE;
            trackBar.SmallChange = DEFAULT_SMALL_CHANGE;
            trackBar.Maximum = DEFAULT_MAXIMUM;
            trackBar.Minimum = DEFAULT_MINIMUM;
            trackBar.VolumeControl = DEFAULT_VOLUME_CONTROL;
            trackBar.TrackBarSize = DEFAULT_TRACK_BAR_SIZE;
            trackBar.TickStyle = DEFAULT_TICK_STYLE;
            trackBar.TickFrequency = DEFAULT_TICK_FREQUENCY;
            trackBar.Orientation = DEFAULT_ORIENTATION;
        }
    }

    #endregion
}

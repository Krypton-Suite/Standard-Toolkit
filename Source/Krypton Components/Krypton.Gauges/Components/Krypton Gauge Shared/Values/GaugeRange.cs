#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, tobitege, Lesandro, KamaniAR, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Gauges;

/// <summary>
/// A coloured band on an analogue gauge scale.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class GaugeRange : Storage
{
    #region Instance Fields

    private string _name = string.Empty;
    private double _start;
    private double _end = 100d;
    private GaugeRangeSeverity _severity = GaugeRangeSeverity.Normal;
    private Color _color = Color.Empty;
    private GaugeRangeCollection? _owner;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="GaugeRange"/> class.
    /// </summary>
    public GaugeRange()
    {
    }

    /// <summary>
    /// Initialize a new instance of the <see cref="GaugeRange"/> class.
    /// </summary>
    /// <param name="start">Inclusive start of the band.</param>
    /// <param name="end">Inclusive end of the band.</param>
    /// <param name="severity">Palette severity used when <see cref="Color"/> is empty.</param>
    public GaugeRange(double start, double end, GaugeRangeSeverity severity)
    {
        _start = start;
        _end = end;
        _severity = severity;
    }

    /// <summary>
    /// Returns an empty string when every setting is at the factory default, otherwise the range name or "Modified".
    /// </summary>
    public override string ToString()
    {
        if (IsDefault)
        {
            return string.Empty;
        }

        return string.IsNullOrEmpty(_name) ? @"Modified" : _name;
    }

    #endregion

    #region IsDefault

    /// <inheritdoc />
    public override bool IsDefault =>
        string.IsNullOrEmpty(_name) &&
        _start == 0d &&
        _end == 100d &&
        _severity == GaugeRangeSeverity.Normal &&
        _color.IsEmpty;

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets a designer-friendly name for the band.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Optional name shown in the property grid.")]
    [DefaultValue("")]
    public string Name
    {
        get => _name;
        set
        {
            value ??= string.Empty;
            if (_name == value)
            {
                return;
            }

            _name = value;
            NotifyOwner();
        }
    }

    /// <summary>
    /// Gets or sets the inclusive start of the band on the gauge scale.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Inclusive start of the coloured band.")]
    [DefaultValue(0d)]
    public double Start
    {
        get => _start;
        set
        {
            if (_start == value)
            {
                return;
            }

            _start = value;
            NotifyOwner();
        }
    }

    /// <summary>
    /// Gets or sets the inclusive end of the band on the gauge scale.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Inclusive end of the coloured band.")]
    [DefaultValue(100d)]
    public double End
    {
        get => _end;
        set
        {
            if (_end == value)
            {
                return;
            }

            _end = value;
            NotifyOwner();
        }
    }

    /// <summary>
    /// Gets or sets the palette severity used when <see cref="Color"/> is empty.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Palette severity used when Color is empty.")]
    [DefaultValue(GaugeRangeSeverity.Normal)]
    public GaugeRangeSeverity Severity
    {
        get => _severity;
        set
        {
            if (_severity == value)
            {
                return;
            }

            _severity = value;
            NotifyOwner();
        }
    }

    /// <summary>
    /// Gets or sets an explicit band colour. Empty inherits from <see cref="Severity"/>.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Explicit band colour. Empty inherits from Severity.")]
    [KryptonDefaultColor]
    public Color Color
    {
        get => _color;
        set
        {
            if (_color == value)
            {
                return;
            }

            _color = value;
            NotifyOwner();
        }
    }

    private bool ShouldSerializeColor() => !_color.IsEmpty;

    private void ResetColor() => Color = Color.Empty;

    #endregion

    #region Internal

    internal void SetOwner(GaugeRangeCollection? owner) => _owner = owner;

    private void NotifyOwner() => _owner?.OnRangeChanged();

    #endregion
}

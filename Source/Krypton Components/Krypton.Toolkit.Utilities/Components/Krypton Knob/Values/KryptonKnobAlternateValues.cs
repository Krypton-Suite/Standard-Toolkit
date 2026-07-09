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
/// Groups enhanced knob properties for display in the PropertyGrid.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class KryptonKnobAlternateValues : Storage
{
    #region Instance Fields
    private readonly KryptonKnobAlternate _owner;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonKnobAlternateValues"/> class.
    /// </summary>
    /// <param name="owner">The owning knob control.</param>
    internal KryptonKnobAlternateValues(KryptonKnobAlternate owner)
    {
        _owner = owner;
        Backplate = new KnobAlternateBackplateValues(owner);
        PlateLabels = new KnobPlateLabelsValues(owner);
    }
    #endregion

    #region IsDefault
    /// <inheritdoc />
    [Browsable(false)]
    public override bool IsDefault =>
        ScaleTypefaceAutoSize &&
        !DrawDivInside &&
        !ShowSmallScale &&
        ShowLargeScale &&
        StartAngle.Equals(135f) &&
        EndAngle.Equals(405f) &&
        IndicatorShape == KnobIndicatorShape.Circle &&
        KnobStyle == KnobStyle.Classic &&
        Backplate.IsDefault &&
        PlateLabels.IsDefault &&
        MouseWheelBarPartitions == 10 &&
        ScaleDivisions == 11 &&
        ScaleSubDivisions == 4 &&
        Minimum == 0 &&
        Maximum == 100 &&
        LargeChange == 5 &&
        SmallChange == 1 &&
        Value == 0 &&
        BackStyle == PaletteBackStyle.PanelClient &&
        _owner.StateCommon.Face.Color1 == GlobalStaticVariables.EMPTY_COLOR &&
        _owner.StateCommon.Indicator.Color1 == GlobalStaticVariables.EMPTY_COLOR &&
        _owner.StateCommon.Tick.Color1 == GlobalStaticVariables.EMPTY_COLOR &&
        _owner.ScaleTypefaceMatchesControlFont();
    #endregion

    #region Appearance
    /// <summary>
    /// Gets industrial backplate settings drawn behind the knob.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Industrial mounting backplate shape, colours, and depth effects.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KnobAlternateBackplateValues Backplate { get; }

    private bool ShouldSerializeBackplate() => !Backplate.IsDefault;

    /// <summary>
    /// Gets plate label settings (RUN/STOP, OFF/ON, and custom text around the backplate).
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Text labels drawn on the industrial backplate.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KnobPlateLabelsValues PlateLabels { get; }

    private bool ShouldSerializePlateLabels() => !PlateLabels.IsDefault;

    /// <summary>
    /// Gets or sets how the knob face is rendered.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Visual style of the knob face.")]
    [DefaultValue(KnobStyle.Classic)]
    public KnobStyle KnobStyle
    {
        get => _owner.GetKnobStyle();
        set => _owner.SetKnobStyle(value);
    }

    /// <summary>
    /// Gets or sets the font used for graduation labels.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Font of graduations.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Font ScaleTypeface
    {
        get => _owner.GetScaleTypeface();
        set => _owner.SetScaleTypeface(value);
    }

    /// <summary>
    /// Gets or sets whether the graduation font size is calculated automatically.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Autosize font of graduations.")]
    [DefaultValue(true)]
    public bool ScaleTypefaceAutoSize
    {
        get => _owner.GetScaleTypefaceAutoSize();
        set => _owner.SetScaleTypefaceAutoSize(value);
    }

    /// <summary>
    /// Gets or sets the start angle used to display graduations (minimum 90).
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Set the start angle to display graduations (min 90).")]
    [DefaultValue(135f)]
    public float StartAngle
    {
        get => _owner.GetStartAngle();
        set => _owner.SetStartAngle(value);
    }

    /// <summary>
    /// Gets or sets the end angle used to display graduations (maximum 450).
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Set the end angle to display graduations (max 450).")]
    [DefaultValue(405f)]
    public float EndAngle
    {
        get => _owner.GetEndAngle();
        set => _owner.SetEndAngle(value);
    }

    /// <summary>
    /// Gets or sets the style of the knob pointer.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Set the style of the knob pointer: a circle or a line.")]
    [DefaultValue(KnobIndicatorShape.Circle)]
    public KnobIndicatorShape IndicatorShape
    {
        get => _owner.GetIndicatorShape();
        set => _owner.SetIndicatorShape(value);
    }

    /// <summary>
    /// Gets or sets whether graduation strings are drawn inside the knob circle.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Draw graduation strings inside or outside the knob circle.")]
    [DefaultValue(false)]
    public bool DrawDivInside
    {
        get => _owner.GetDrawDivInside();
        set => _owner.SetDrawDivInside(value);
    }

    /// <summary>
    /// Gets or sets the colour of graduations (maps to <see cref="KryptonKnobAlternate.StateCommon"/> tick colour; defaults to palette text colour).
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Colour of graduations. When empty the palette control label text colour is used.")]
    public Color ScaleColour
    {
        get => _owner.GetScaleColour();
        set => _owner.SetScaleColour(value);
    }

    /// <summary>
    /// Gets or sets the knob face colour (maps to <see cref="KryptonKnobAlternate.StateCommon"/> face colour).
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Colour of knob face.")]
    public Color KnobBackColour
    {
        get => _owner.GetKnobBackColour();
        set => _owner.SetKnobBackColour(value);
    }

    /// <summary>
    /// Gets or sets the pointer colour (maps to <see cref="KryptonKnobAlternate.StateCommon"/> indicator colour).
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Colour of the value pointer.")]
    public Color PointerColour
    {
        get => _owner.GetPointerColour();
        set => _owner.SetPointerColour(value);
    }

    /// <summary>
    /// Gets or sets the number of labelled intervals between minimum and maximum.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Set the number of intervals between minimum and maximum.")]
    [DefaultValue(11)]
    public int ScaleDivisions
    {
        get => _owner.GetScaleDivisions();
        set => _owner.SetScaleDivisions(value);
    }

    /// <summary>
    /// Gets or sets the number of subdivisions between each main graduation.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Set the number of subdivisions between main divisions of graduation.")]
    [DefaultValue(4)]
    public int ScaleSubDivisions
    {
        get => _owner.GetScaleSubDivisions();
        set => _owner.SetScaleSubDivisions(value);
    }

    /// <summary>
    /// Gets or sets whether small scale markings are shown.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Show or hide subdivisions of graduations.")]
    [DefaultValue(false)]
    public bool ShowSmallScale
    {
        get => _owner.GetShowSmallScale();
        set => _owner.SetShowSmallScale(value);
    }

    /// <summary>
    /// Gets or sets whether large scale markings and numeric labels are shown.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Show or hide graduations.")]
    [DefaultValue(true)]
    public bool ShowLargeScale
    {
        get => _owner.GetShowLargeScale();
        set => _owner.SetShowLargeScale(value);
    }

    /// <summary>
    /// Gets or sets the background style.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Background style.")]
    [DefaultValue(PaletteBackStyle.PanelClient)]
    public PaletteBackStyle BackStyle
    {
        get => _owner.GetBackStyle();
        set => _owner.SetBackStyle(value);
    }
    #endregion

    #region Behavior
    /// <summary>
    /// Gets or sets how many parts the value range is divided into when using the mouse wheel.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Set to how many parts the range is divided when using the mouse wheel.")]
    [DefaultValue(10)]
    public int MouseWheelBarPartitions
    {
        get => _owner.GetMouseWheelBarPartitions();
        set => _owner.SetMouseWheelBarPartitions(value);
    }

    /// <summary>
    /// Gets or sets the minimum value.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Set the minimum value for the knob control.")]
    [DefaultValue(0)]
    public int Minimum
    {
        get => _owner.GetMinimum();
        set => _owner.SetMinimum(value);
    }

    /// <summary>
    /// Gets or sets the maximum value.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Set the maximum value for the knob control.")]
    [DefaultValue(100)]
    public int Maximum
    {
        get => _owner.GetMaximum();
        set => _owner.SetMaximum(value);
    }

    /// <summary>
    /// Gets or sets the large change value.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Set the value for large changes.")]
    [DefaultValue(5)]
    public int LargeChange
    {
        get => _owner.GetLargeChange();
        set => _owner.SetLargeChange(value);
    }

    /// <summary>
    /// Gets or sets the small change value.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Set the value for small changes.")]
    [DefaultValue(1)]
    public int SmallChange
    {
        get => _owner.GetSmallChange();
        set => _owner.SetSmallChange(value);
    }

    /// <summary>
    /// Gets or sets the current value.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Set the current value of the knob control.")]
    [DefaultValue(0)]
    public int Value
    {
        get => _owner.GetValue();
        set => _owner.SetValue(value);
    }
    #endregion

    #region Implementation
    /// <inheritdoc />
    public override string ToString() => IsDefault ? string.Empty : @"Modified";

    /// <summary>
    /// Resets all values to their defaults.
    /// </summary>
    public void Reset()
    {
        ScaleTypefaceAutoSize = true;
        DrawDivInside = false;
        ShowSmallScale = false;
        ShowLargeScale = true;
        StartAngle = 135f;
        EndAngle = 405f;
        IndicatorShape = KnobIndicatorShape.Circle;
        KnobStyle = KnobStyle.Classic;
        Backplate.Reset();
        PlateLabels.Reset();
        MouseWheelBarPartitions = 10;
        ScaleDivisions = 11;
        ScaleSubDivisions = 4;
        Minimum = 0;
        Maximum = 100;
        LargeChange = 5;
        SmallChange = 1;
        Value = 0;
        BackStyle = PaletteBackStyle.PanelClient;
        _owner.StateCommon.Face.Color1 = GlobalStaticVariables.EMPTY_COLOR;
        _owner.StateCommon.Face.Color2 = GlobalStaticVariables.EMPTY_COLOR;
        _owner.StateCommon.Indicator.Color1 = GlobalStaticVariables.EMPTY_COLOR;
        _owner.StateCommon.Tick.Color1 = GlobalStaticVariables.EMPTY_COLOR;
        _owner.SetScaleTypeface(_owner.Font);
    }
    #endregion
}

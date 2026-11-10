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
/// Contains appearance settings for a <see cref="KryptonKnob"/>.
/// </summary>
public class KnobAppearanceValues : Storage
{
    #region Instance Fields
    private readonly KryptonKnob _owner;
    private readonly KnobBackplateValues _backplate;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KnobAppearanceValues class.
    /// </summary>
    /// <param name="owner">Reference to owning control.</param>
    public KnobAppearanceValues(KryptonKnob owner)
    {
        _owner = owner;
        _backplate = new KnobBackplateValues(owner);
    }

    #endregion

    #region IsDefault
    /// <inheritdoc />
    public override bool IsDefault => BackStyle == PaletteBackStyle.PanelClient &&
                                      DrawBackground &&
                                      !ShowSmallScale &&
                                      ShowLargeScale &&
                                      SizeLargeScaleMarker == 6 &&
                                      SizeSmallScaleMarker == 3 &&
                                      IndicatorShape == KnobIndicatorShape.Circle &&
                                      IndicatorSize == 6 &&
                                      IndicatorCustomPoints == null &&
                                      IndicatorCustomPath == null &&
                                      KnobStyle == KnobStyle.Classic &&
                                      Backplate.IsDefault;

    #endregion

    #region Public
    /// <summary>
    /// Gets access to industrial backplate settings.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Industrial mounting backplate drawn behind the knob.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KnobBackplateValues Backplate => _backplate;

    private bool ShouldSerializeBackplate() => !Backplate.IsDefault;

    /// <summary>
    /// Gets or sets the background style.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Background style.")]
    [DefaultValue(PaletteBackStyle.PanelClient)]
    public PaletteBackStyle BackStyle
    {
        get => _owner.OverrideFocus.BackStyle;

        set
        {
            if (_owner.OverrideFocus.BackStyle != value)
            {
                _owner.OverrideFocus.BackStyle = value;
                _owner.PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeBackStyle() => BackStyle != PaletteBackStyle.PanelClient;

    private void ResetBackStyle() => BackStyle = PaletteBackStyle.PanelClient;

    /// <summary>
    /// Gets or sets if the control should draw the background.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Draw background (default = true).")]
    [DefaultValue(true)]
    [RefreshProperties(RefreshProperties.Repaint)]
    public bool DrawBackground
    {
        get => !_owner.ViewDrawKnob.IgnoreRender;
        set => _owner.ViewDrawKnob.IgnoreRender = !value;
    }

    private bool ShouldSerializeDrawBackground() => !DrawBackground;

    private void ResetDrawBackground() => DrawBackground = true;

    /// <summary>
    /// Gets or sets a value indicating whether small scale marks are shown.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Shows small scale marking.")]
    [DefaultValue(false)]
    public bool ShowSmallScale
    {
        get => _owner.ViewDrawKnob.ShowSmallScale;
        set
        {
            if (value != _owner.ViewDrawKnob.ShowSmallScale)
            {
                _owner.ViewDrawKnob.ShowSmallScale = value;
                _owner.PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether large scale marks are shown.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Shows large scale marking.")]
    [DefaultValue(true)]
    public bool ShowLargeScale
    {
        get => _owner.ViewDrawKnob.ShowLargeScale;
        set
        {
            if (value != _owner.ViewDrawKnob.ShowLargeScale)
            {
                _owner.ViewDrawKnob.ShowLargeScale = value;
                _owner.PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets or sets the size of the large scale marker.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Size of the large scale marker.")]
    [DefaultValue(6)]
    public int SizeLargeScaleMarker
    {
        get => _owner.ViewDrawKnob.SizeLargeScaleMarker;
        set
        {
            if (value != _owner.ViewDrawKnob.SizeLargeScaleMarker)
            {
                _owner.ViewDrawKnob.SizeLargeScaleMarker = value;
                _owner.PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets or sets the size of the small scale marker.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Size of the small scale marker.")]
    [DefaultValue(3)]
    public int SizeSmallScaleMarker
    {
        get => _owner.ViewDrawKnob.SizeSmallScaleMarker;
        set
        {
            if (value != _owner.ViewDrawKnob.SizeSmallScaleMarker)
            {
                _owner.ViewDrawKnob.SizeSmallScaleMarker = value;
                _owner.PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets or sets the shape of the value indicator.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Shape of the value indicator drawn on the knob face.")]
    [DefaultValue(KnobIndicatorShape.Circle)]
    public KnobIndicatorShape IndicatorShape
    {
        get => _owner.ViewDrawKnob.IndicatorShape;
        set
        {
            if (value != _owner.ViewDrawKnob.IndicatorShape)
            {
                _owner.ViewDrawKnob.IndicatorShape = value;
                _owner.PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets or sets the size of the value indicator in pixels.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Size of the value indicator in pixels.")]
    [DefaultValue(6)]
    public int IndicatorSize
    {
        get => _owner.ViewDrawKnob.IndicatorSize;
        set
        {
            if (value != _owner.ViewDrawKnob.IndicatorSize)
            {
                _owner.ViewDrawKnob.IndicatorSize = value;
                _owner.PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets or sets normalized custom indicator points used when <see cref="IndicatorShape"/> is <see cref="KnobIndicatorShape.Custom"/>.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Normalized custom indicator polygon points (+X is outward; scaled by half of IndicatorSize).")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public PointF[]? IndicatorCustomPoints
    {
        get => _owner.ViewDrawKnob.IndicatorCustomPoints;
        set
        {
            _owner.ViewDrawKnob.IndicatorCustomPoints = value;
            _owner.PerformNeedPaint(true);
        }
    }

    /// <summary>
    /// Gets or sets a custom indicator path used when <see cref="IndicatorShape"/> is <see cref="KnobIndicatorShape.Custom"/>.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public GraphicsPath? IndicatorCustomPath
    {
        get => _owner.ViewDrawKnob.IndicatorCustomPath;
        set
        {
            _owner.ViewDrawKnob.IndicatorCustomPath = value;
            _owner.PerformNeedPaint(true);
        }
    }

    /// <summary>
    /// Gets or sets how the knob face is rendered.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Visual style of the knob face.")]
    [DefaultValue(KnobStyle.Classic)]
    public KnobStyle KnobStyle
    {
        get => _owner.ViewDrawKnob.KnobStyle;
        set
        {
            if (value != _owner.ViewDrawKnob.KnobStyle)
            {
                _owner.ViewDrawKnob.KnobStyle = value;
                _owner.PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Resets appearance values to their defaults.
    /// </summary>
    public void Reset()
    {
        ResetBackStyle();
        ResetDrawBackground();
        Backplate.Reset();
        ShowSmallScale = false;
        ShowLargeScale = true;
        SizeLargeScaleMarker = 6;
        SizeSmallScaleMarker = 3;
        IndicatorShape = KnobIndicatorShape.Circle;
        IndicatorSize = 6;
        IndicatorCustomPoints = null;
        IndicatorCustomPath = null;
        KnobStyle = KnobStyle.Classic;
    }

    #endregion
}

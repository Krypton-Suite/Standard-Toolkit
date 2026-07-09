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
/// Contains behavior settings for a <see cref="KryptonKnob"/>.
/// </summary>
public class KnobBehaviorValues : Storage
{
    #region Instance Fields
    private readonly KryptonKnob _owner;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KnobBehaviorValues class.
    /// </summary>
    /// <param name="owner">Reference to owning control.</param>
    public KnobBehaviorValues(KryptonKnob owner) => _owner = owner;

    #endregion

    #region IsDefault
    /// <inheritdoc />
    public override bool IsDefault => Minimum == 0 &&
                                      Maximum == 100 &&
                                      LargeChange == 20 &&
                                      SmallChange == 5 &&
                                      Value == 0;

    #endregion

    #region Public
    /// <summary>
    /// Gets or sets the minimum value.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Minimum value for the knob control.")]
    [DefaultValue(0)]
    public int Minimum
    {
        get => _owner.ViewDrawKnob.Minimum;
        set
        {
            if (value != _owner.ViewDrawKnob.Minimum)
            {
                _owner.ViewDrawKnob.Minimum = value;
                _owner.PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets or sets the maximum value.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Maximum value for the knob control.")]
    [DefaultValue(100)]
    public int Maximum
    {
        get => _owner.ViewDrawKnob.Maximum;
        set
        {
            if (value != _owner.ViewDrawKnob.Maximum)
            {
                _owner.ViewDrawKnob.Maximum = value;
                _owner.PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets or sets the large change value.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Value set for large change.")]
    [DefaultValue(20)]
    public int LargeChange
    {
        get => _owner.ViewDrawKnob.LargeChange;
        set => _owner.ViewDrawKnob.LargeChange = value;
    }

    /// <summary>
    /// Gets or sets the small change value.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Value set for small change.")]
    [DefaultValue(5)]
    public int SmallChange
    {
        get => _owner.ViewDrawKnob.SmallChange;
        set => _owner.ViewDrawKnob.SmallChange = value;
    }

    /// <summary>
    /// Gets or sets the current value.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Current value of the knob control.")]
    [DefaultValue(0)]
    public int Value
    {
        get => _owner.ViewDrawKnob.Value;
        set => _owner.ViewDrawKnob.Value = value;
    }

    /// <summary>
    /// Resets behavior values to their defaults.
    /// </summary>
    public void Reset()
    {
        Minimum = 0;
        Maximum = 100;
        LargeChange = 20;
        SmallChange = 5;
        Value = 0;
    }

    #endregion
}

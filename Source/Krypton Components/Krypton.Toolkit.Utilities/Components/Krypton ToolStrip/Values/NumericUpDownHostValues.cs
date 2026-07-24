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
/// Expandable configuration for the <see cref="KryptonNumericUpDownToolStripItem"/> tool strip host. Mirrors the
/// settings of the hosted <see cref="KryptonNumericUpDown"/> (<see cref="KryptonNumericUpDownToolStripItem.KryptonNumericUpDownControl"/>).
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class NumericUpDownHostValues : Storage
{
    #region Instance Fields

    private readonly KryptonNumericUpDownToolStripItem _owner;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="NumericUpDownHostValues"/> class.
    /// </summary>
    /// <param name="owner">Owning numeric up-down host.</param>
    public NumericUpDownHostValues(KryptonNumericUpDownToolStripItem owner) =>
        _owner = owner ?? throw new ArgumentNullException(nameof(owner));

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? @"Modified" : string.Empty;

    #endregion

    #region IsDefault

    /// <inheritdoc />
    [Browsable(false)]
    public override bool IsDefault =>
        _owner.KryptonNumericUpDownControl is null || _owner.KryptonNumericUpDownControl.Value == 0;

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the current value.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(typeof(decimal), "0")]
    public decimal Value
    {
        get => _owner.KryptonNumericUpDownControl!.Value;
        set => _owner.KryptonNumericUpDownControl!.Value = value;
    }

    /// <summary>
    /// Resets all values to their defaults.
    /// </summary>
    public void Reset()
    {
        if (_owner.KryptonNumericUpDownControl is { } control)
        {
            control.Value = 0;
        }
    }

    #endregion
}

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
/// Expandable configuration for <see cref="KryptonToolStripProgressBarWithValueText"/>.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class ProgressBarValueTextValues : Storage
{
    #region Instance Fields

    private readonly KryptonToolStripProgressBarWithValueText _owner;
    private bool _displayValue;
    private Color _displayTextColour;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="ProgressBarValueTextValues"/> class.
    /// </summary>
    /// <param name="owner">Owning progress bar.</param>
    public ProgressBarValueTextValues(KryptonToolStripProgressBarWithValueText owner) =>
        _owner = owner ?? throw new ArgumentNullException(nameof(owner));

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? @"Modified" : string.Empty;

    #endregion

    #region IsDefault

    /// <inheritdoc />
    // NOTE: DisplayTextColour is set from the current global palette in the owner's constructor,
    // so it is not gated here; only the display flag participates in IsDefault.
    [Browsable(false)]
    public override bool IsDefault => !_displayValue;

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets a value indicating whether to display the numeric value over the progress bar.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(false)]
    public bool DisplayValue
    {
        get => _displayValue;
        set { _displayValue = value; _owner.Invalidate(); }
    }

    /// <summary>
    /// Gets or sets the colour used to display the numeric value over the progress bar.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(typeof(Color), "")]
    public Color DisplayTextColour
    {
        get => _displayTextColour;
        set => _displayTextColour = value;
    }

    /// <summary>
    /// Resets all values to their defaults.
    /// </summary>
    public void Reset()
    {
        _displayValue = false;
        _owner.Invalidate();
    }

    #endregion
}

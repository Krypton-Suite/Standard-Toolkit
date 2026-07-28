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
/// Expandable configuration for <see cref="KryptonEnhancedToolStripProgressBar"/>.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class EnhancedProgressBarValues : Storage
{
    #region Instance Fields

    private readonly KryptonEnhancedToolStripProgressBar _owner;
    private bool _useKryptonRender;
    private bool _useDisplayText;
    private Color _displayTextColour;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="EnhancedProgressBarValues"/> class.
    /// </summary>
    /// <param name="owner">Owning enhanced progress bar.</param>
    public EnhancedProgressBarValues(KryptonEnhancedToolStripProgressBar owner)
    {
        _owner = owner ?? throw new ArgumentNullException(nameof(owner));
        _displayTextColour = Color.Black;
    }

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? @"Modified" : string.Empty;

    #endregion

    #region IsDefault

    /// <inheritdoc />
    // NOTE: DisplayTextColour is set from the current global palette in the owner's constructor,
    // so it is not gated here; only the two behavioural flags participate in IsDefault.
    [Browsable(false)]
    public override bool IsDefault => !_useKryptonRender && !_useDisplayText;

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets a value indicating whether to render the progress bar using the current Krypton renderer's status strip colour.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(false)]
    public bool UseKryptonRender
    {
        get => _useKryptonRender;
        set { _useKryptonRender = value; _owner.Invalidate(); }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to display text over the progress bar.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(false)]
    public bool UseDisplayText
    {
        get => _useDisplayText;
        set => _useDisplayText = value;
    }

    /// <summary>
    /// Gets or sets the colour used to display text over the progress bar.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(typeof(Color), "Color.Black")]
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
        _useKryptonRender = false;
        _useDisplayText = false;
        _displayTextColour = Color.Black;
        _owner.Invalidate();
    }

    #endregion
}

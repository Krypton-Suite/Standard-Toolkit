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
/// Expandable configuration for <see cref="KryptonEnhancedToolStripSeparator"/>.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class EnhancedSeparatorValues : Storage
{
    #region Instance Fields

    private readonly KryptonEnhancedToolStripSeparator _owner;
    private bool _showSeparatorLine;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="EnhancedSeparatorValues"/> class.
    /// </summary>
    /// <param name="owner">Owning enhanced separator.</param>
    public EnhancedSeparatorValues(KryptonEnhancedToolStripSeparator owner) =>
        _owner = owner ?? throw new ArgumentNullException(nameof(owner));

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? @"Modified" : string.Empty;

    #endregion

    #region IsDefault

    /// <inheritdoc />
    [Browsable(false)]
    public override bool IsDefault => !_showSeparatorLine;

    #endregion

    #region Public

    /// <summary>
    /// If set to <c>true</c>, a separator line is displayed in areas not occupied by text.
    /// Otherwise only text will be displayed.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(false)]
    [Description(@"If set to 'true' separator line will be displayed in areas not occupied by text. Otherwise only text will be displayed.")]
    public bool ShowSeparatorLine
    {
        get => _showSeparatorLine;
        set { _showSeparatorLine = value; _owner.Invalidate(); }
    }

    /// <summary>
    /// Resets all values to their defaults.
    /// </summary>
    public void Reset()
    {
        _showSeparatorLine = false;
        _owner.Invalidate();
    }

    #endregion
}

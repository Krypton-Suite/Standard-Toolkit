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
/// Expandable configuration for <see cref="KryptonEnhancedToolStrip"/>.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class EnhancedToolStripValues : Storage
{
    #region Instance Fields

    private readonly KryptonEnhancedToolStrip _owner;
    private bool _clickThrough;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="EnhancedToolStripValues"/> class.
    /// </summary>
    /// <param name="owner">Owning enhanced tool strip.</param>
    public EnhancedToolStripValues(KryptonEnhancedToolStrip owner) =>
        _owner = owner ?? throw new ArgumentNullException(nameof(owner));

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? @"Modified" : string.Empty;

    #endregion

    #region IsDefault

    /// <inheritdoc />
    [Browsable(false)]
    public override bool IsDefault => !_clickThrough;

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets whether the tool strip honors item clicks when its containing form does not have input focus.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(false)]
    [Description(@"Gets or sets whether the tool strip honors item clicks when its containing form does not have input focus.")]
    public bool ClickThrough
    {
        get => _clickThrough;
        set => _clickThrough = value;
    }

    /// <summary>
    /// Resets all values to their defaults.
    /// </summary>
    public void Reset()
    {
        _clickThrough = false;
    }

    #endregion
}

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
/// Storage for <see cref="KryptonCheckBoxExtended"/> layout spacing.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class CheckBoxExtendedLayoutValues : Storage
{
    #region Instance Fields

    private KryptonCheckBoxExtended? _owner;
    private int _subtextSeparatorHeight;
    private int _textGap;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="CheckBoxExtendedLayoutValues"/> class.
    /// </summary>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public CheckBoxExtendedLayoutValues(NeedPaintHandler needPaint)
    {
        NeedPaint = needPaint;
        _subtextSeparatorHeight = 5;
        _textGap = 0;
    }

    #endregion

    #region Public

    /// <inheritdoc />
    public override bool IsDefault => _subtextSeparatorHeight == 5 && _textGap == 0;

    /// <summary>
    /// Gets or sets the number of pixels separating the main text from the subtext.
    /// </summary>
    [Category(@"Layout")]
    [Description(@"Number of pixels separating the main text from the subtext.")]
    [DefaultValue(5)]
    public int SubtextSeparatorHeight
    {
        get => _subtextSeparatorHeight;

        set
        {
            if (_subtextSeparatorHeight != value)
            {
                _subtextSeparatorHeight = value;
                _owner?.OnLayoutValuesChanged();
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Resets the <see cref="SubtextSeparatorHeight"/> property to its default value.
    /// </summary>
    public void ResetSubtextSeparatorHeight() => SubtextSeparatorHeight = 5;

    private bool ShouldSerializeSubtextSeparatorHeight() => SubtextSeparatorHeight != 5;

    /// <summary>
    /// Gets or sets the number of pixels between the check box glyph and the text.
    /// </summary>
    [Category(@"Layout")]
    [Description(@"Additional spacing between the check box glyph and the text.")]
    [DefaultValue(0)]
    public int TextGap
    {
        get => _textGap;

        set
        {
            if (_textGap != value)
            {
                _textGap = Math.Max(0, value);
                _owner?.OnLayoutValuesChanged();
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Resets the <see cref="TextGap"/> property to its default value.
    /// </summary>
    public void ResetTextGap() => TextGap = 0;

    private bool ShouldSerializeTextGap() => TextGap != 0;

    #endregion

    #region Implementation

    internal void SetOwner(KryptonCheckBoxExtended owner) => _owner = owner;

    /// <summary>
    /// Returns a <see cref="string"/> that represents this instance.
    /// </summary>
    /// <returns>A <see cref="string"/> that represents this instance.</returns>
    public override string ToString() => IsDefault ? string.Empty : "Modified";

    #endregion
}

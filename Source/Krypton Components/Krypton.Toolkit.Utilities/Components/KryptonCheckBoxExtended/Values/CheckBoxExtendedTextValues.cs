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
/// Storage for <see cref="KryptonCheckBoxExtended"/> text content.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class CheckBoxExtendedTextValues : LabelValues
{
    #region Static Fields

    private const string DEFAULT_TEXT = @"Krypton CheckBox Extended";
    private const string DEFAULT_SUBTEXT = @"";

    #endregion

    #region Instance Fields

    private Font? _subtextFont;
    private Color _subtextForeColor;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="CheckBoxExtendedTextValues"/> class.
    /// </summary>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public CheckBoxExtendedTextValues(NeedPaintHandler needPaint)
        : base(needPaint)
    {
        Text = DEFAULT_TEXT;
        ExtraText = DEFAULT_SUBTEXT;
        _subtextForeColor = GlobalStaticVariables.EMPTY_COLOR;
    }

    #endregion

    #region Public

    /// <inheritdoc />
    public override bool IsDefault => base.IsDefault
        && !ShouldSerializeSubtextFont()
        && !ShouldSerializeSubtextForeColor();

    /// <summary>
    /// Gets or sets the secondary descriptive text displayed below the main text.
    /// </summary>
    [Category(@"Text")]
    [Description(@"Secondary descriptive text displayed below the main text.")]
    [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
    [DefaultValue(DEFAULT_SUBTEXT)]
    public string Subtext
    {
        get => ExtraText;
        set => ExtraText = value;
    }

    /// <summary>
    /// Resets the <see cref="Subtext"/> property to its default value.
    /// </summary>
    public void ResetSubtext() => Subtext = DEFAULT_SUBTEXT;

    internal bool ShouldSerializeSubtext() => Subtext != DEFAULT_SUBTEXT;

    /// <summary>
    /// Gets or sets the font used to render the subtext.
    /// </summary>
    [Category(@"Text")]
    [Description(@"The font used to display the subtext.")]
    [DefaultValue(null)]
    public Font? SubtextFont
    {
        get => _subtextFont;
        set
        {
            if (_subtextFont != value)
            {
                _subtextFont = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Resets the <see cref="SubtextFont"/> property to its default value.
    /// </summary>
    public void ResetSubtextFont() => SubtextFont = null;

    internal bool ShouldSerializeSubtextFont() => SubtextFont != null;

    /// <summary>
    /// Gets or sets the foreground color of the subtext.
    /// </summary>
    [Category(@"Text")]
    [Description(@"The color used to display the subtext.")]
    [DefaultValue(typeof(Color), "Empty")]
    public Color SubtextForeColor
    {
        get => _subtextForeColor;
        set
        {
            if (_subtextForeColor != value)
            {
                _subtextForeColor = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Resets the <see cref="SubtextForeColor"/> property to its default value.
    /// </summary>
    public void ResetSubtextForeColor() => SubtextForeColor = GlobalStaticVariables.EMPTY_COLOR;

    internal bool ShouldSerializeSubtextForeColor() => SubtextForeColor != GlobalStaticVariables.EMPTY_COLOR;

    /// <inheritdoc />
    public new void ResetText()
    {
        Text = DEFAULT_TEXT;
        Subtext = DEFAULT_SUBTEXT;
    }

    #endregion

    #region Implementation

    /// <summary>
    /// Returns a <see cref="string"/> that represents this instance.
    /// </summary>
    /// <returns>A <see cref="string"/> that represents this instance.</returns>
    public override string ToString() => IsDefault ? string.Empty : "Modified";

    #endregion
}

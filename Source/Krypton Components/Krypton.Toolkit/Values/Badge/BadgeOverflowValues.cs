#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Storage for badge overflow value information.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class BadgeOverflowValues : Storage
{
    #region Static Fields

    private const string DEFAULT_OVERFLOW_TEXT = "+";

    #endregion

    #region Instance Fields

    private string _overflowText;

    #endregion

    #region Identity

    public BadgeOverflowValues(NeedPaintHandler? needPaint)
    {
        NeedPaint = needPaint;

        Reset();
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets and sets the text to display when the badge value exceeds OverflowNumber.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"The text to display when the badge numeric value exceeds OverflowNumber (e.g., '99+').")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue("+")]
    public string OverflowText
    {
        get => _overflowText ?? GlobalStaticValues.DEFAULT_EMPTY_STRING;
        set
        {
            if (_overflowText != value)
            {
                _overflowText = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeOverflowText() => OverflowText != DEFAULT_OVERFLOW_TEXT;

    #endregion

    #region IsDefault

    [Browsable(false)]
    public override bool IsDefault => OverflowText.Equals(DEFAULT_OVERFLOW_TEXT);

    #endregion

    #region Reset

    public void Reset() => OverflowText = DEFAULT_OVERFLOW_TEXT;

    #endregion
}
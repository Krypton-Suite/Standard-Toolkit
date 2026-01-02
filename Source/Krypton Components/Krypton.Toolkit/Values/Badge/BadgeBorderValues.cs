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
/// Storage for badge border values.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class BadgeBorderValues : Storage
{
    #region Static Fields

    private const int DEFAULT_BADGE_BORDER_SIZE = 0;
    private const BadgeBevelType DEFAULT_BADGE_BORDER_BEVEL = BadgeBevelType.None;

    #endregion

    #region Instance Fields


    private int _badgeBorderSize;
    private BadgeBevelType _badgeBorderBevel;

    #endregion

    #region Identity

    public BadgeBorderValues(NeedPaintHandler? needPaint)
    {
        NeedPaint = needPaint;
        
        Reset();
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets and sets the badge border size (thickness in pixels).
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"The border size (thickness in pixels) of the badge. 0 means no border.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(0)]
    public int BadgeBorderSize
    {
        get => _badgeBorderSize;
        set
        {
            if (value < 0)
            {
                value = 0;
            }

            if (_badgeBorderSize != value)
            {
                _badgeBorderSize = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeBadgeBorderSize() => BadgeBorderSize != DEFAULT_BADGE_BORDER_SIZE;

    /// <summary>
    /// Gets and sets the type of bevel effect for the badge border.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"The type of bevel effect for the badge border (None, Raised, or Inset).")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(BadgeBevelType.None)]
    public BadgeBevelType BadgeBorderBevel
    {
        get => _badgeBorderBevel;
        set
        {
            if (_badgeBorderBevel != value)
            {
                _badgeBorderBevel = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeBadgeBorderBevel() => BadgeBorderBevel != DEFAULT_BADGE_BORDER_BEVEL;

    #endregion

    #region IsDefault

    [Browsable(false)]
    public override bool IsDefault => BadgeBorderSize.Equals(DEFAULT_BADGE_BORDER_SIZE) &&
                                      BadgeBorderBevel.Equals(DEFAULT_BADGE_BORDER_BEVEL);

    #endregion

    #region Reset

    public void Reset()
    {
        _badgeBorderSize = DEFAULT_BADGE_BORDER_SIZE;
        _badgeBorderBevel = DEFAULT_BADGE_BORDER_BEVEL;
        PerformNeedPaint(true);
    }

    #endregion
}
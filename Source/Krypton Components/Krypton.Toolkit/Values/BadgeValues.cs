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
/// Storage for badge value information.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class BadgeValues : Storage
{
    #region Instance Fields

    private readonly BadgeBorderValues _badgeBorderValues;
    private readonly BadgeColorValues _badgeColorValues;
    private readonly BadgeContentValues _badgeContentValues;
    private readonly BadgeOverflowValues _badgeOverflowValues;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the BadgeValues class.
    /// </summary>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public BadgeValues(NeedPaintHandler needPaint)
    {
        // Store the provided paint notification delegate
        NeedPaint = needPaint;

        _badgeBorderValues = new BadgeBorderValues(needPaint);
        _badgeColorValues = new BadgeColorValues(needPaint);
        _badgeContentValues = new BadgeContentValues(needPaint);
        _badgeOverflowValues = new BadgeOverflowValues(needPaint);
    }

    #endregion

    #region IsDefault

    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => BadgeBorderValues.IsDefault &&
                                       BadgeColorValues.IsDefault &&
                                       BadgeContentValues.IsDefault &&
                                       BadgeOverflowValues.IsDefault;


    #endregion

    #region Public

    [Category("Visuals")]
    [Description("Storage for badge border values.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public BadgeBorderValues BadgeBorderValues => _badgeBorderValues;

    private bool ShouldSerializeBadgeBorderValues() => !BadgeBorderValues.IsDefault;

    public void ResetBadgeBorderValues() => BadgeBorderValues.Reset();

    [Category("Visuals")]
    [Description("Storage for badge color values.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public BadgeColorValues BadgeColorValues => _badgeColorValues;

    private bool ShouldSerializeBadgeColorValues() => !BadgeColorValues.IsDefault;

    public void ResetBadgeColorValues() => BadgeColorValues.Reset();

    [Category("Visuals")]
    [Description("Storage for badge content values.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public BadgeContentValues BadgeContentValues => _badgeContentValues;

    private bool ShouldSerializeBadgeContentValues() => !BadgeContentValues.IsDefault;

    public void ResetBadgeContentValues() => BadgeContentValues.Reset();

    [Category("Visuals")]
    [Description("Storage for badge overflow values.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public BadgeOverflowValues BadgeOverflowValues => _badgeOverflowValues;

    private bool ShouldSerializeBadgeOverflowValues() => !BadgeOverflowValues.IsDefault;

    public void ResetBadgeOverflowValues() => BadgeOverflowValues.Reset();

    #endregion

    #region Reset

    public void Reset()
    {
        // Set initial values
        ResetBadgeBorderValues();
        ResetBadgeColorValues();
        ResetBadgeContentValues();
        ResetBadgeOverflowValues();
    }

    #endregion
}
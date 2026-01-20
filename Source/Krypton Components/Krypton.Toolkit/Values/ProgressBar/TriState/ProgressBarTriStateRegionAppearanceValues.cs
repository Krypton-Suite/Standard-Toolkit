#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Holds colours and images for one progress bar threshold region (Low, Medium, or High).
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class ProgressBarTriStateRegionAppearanceValues : Storage
{
    #region Instance Fields

    private readonly ProgressBarTriStateValues _parent;
    private readonly Color _defaultBackColor;
    private readonly ProgressBarTriStateRegionStateValues _stateCommon;
    private readonly ProgressBarTriStateRegionStateValues _stateNormal;
    private readonly ProgressBarTriStateRegionStateValues _stateDisabled;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the ProgressBarTriStateRegionAppearanceValues class.
    /// </summary>
    /// <param name="parent">Reference to owning ProgressBarTriStateValues.</param>
    /// <param name="defaultBackColor">Default background color for this region (e.g. Red, Orange, Green).</param>
    public ProgressBarTriStateRegionAppearanceValues(ProgressBarTriStateValues parent, Color defaultBackColor)
    {
        _parent = parent;
        _defaultBackColor = defaultBackColor;

        // Create states - StateCommon gets the default color, others get Empty (will inherit from Common)
        _stateCommon = new ProgressBarTriStateRegionStateValues(this, defaultBackColor);
        _stateNormal = new ProgressBarTriStateRegionStateValues(this, Color.Empty);
        _stateDisabled = new ProgressBarTriStateRegionStateValues(this, Color.Empty);
    }

    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => _stateCommon.IsDefault && _stateNormal.IsDefault && _stateDisabled.IsDefault;

    /// <summary>
    /// Gets the parent ProgressBarTriStateValues.
    /// </summary>
    internal ProgressBarTriStateValues Parent => _parent;

    #endregion

    #region StateCommon

    /// <summary>
    /// Gets access to the common state appearance entries that other states can override.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ProgressBarTriStateRegionStateValues StateCommon => _stateCommon;

    private bool ShouldSerializeStateCommon() => !_stateCommon.IsDefault;

    /// <summary>
    /// Resets the StateCommon to its default value.
    /// </summary>
    public void ResetStateCommon() => _stateCommon.Reset();

    #endregion

    #region StateNormal

    /// <summary>
    /// Gets access to the normal state appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ProgressBarTriStateRegionStateValues StateNormal => _stateNormal;

    private bool ShouldSerializeStateNormal() => !_stateNormal.IsDefault;

    /// <summary>
    /// Resets the StateNormal to its default value.
    /// </summary>
    public void ResetStateNormal() => _stateNormal.Reset();

    #endregion

    #region StateDisabled

    /// <summary>
    /// Gets access to the disabled state appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ProgressBarTriStateRegionStateValues StateDisabled => _stateDisabled;

    private bool ShouldSerializeStateDisabled() => !_stateDisabled.IsDefault;

    /// <summary>
    /// Resets the StateDisabled to its default value.
    /// </summary>
    public void ResetStateDisabled() => _stateDisabled.Reset();

    #endregion

    #region AssignFrom

    /// <summary>
    /// Copies all colours and images from the common base into this region's StateCommon.
    /// </summary>
    /// <param name="source">The common base to copy from.</param>
    public void AssignFrom(ProgressBarTriStateCommonBaseValues source)
    {
        _stateCommon.Back.Color1 = source.BackColor1;
        _stateCommon.Back.Color2 = source.BackColor2;
        _stateCommon.Back.ColorStyle = source.BackColorStyle;
        _stateCommon.Back.ColorAlign = source.BackColorAlign;
        _stateCommon.Back.ColorAngle = source.BackColorAngle;
        _stateCommon.Back.Image = source.BackImage;
        _stateCommon.Back.ImageStyle = source.BackImageStyle;
        _stateCommon.Back.ImageAlign = source.BackImageAlign;
        _stateCommon.Content.Color1 = source.TextColor1;
        _stateCommon.Content.Color2 = source.TextColor2;
        _stateCommon.Content.ColorStyle = source.TextColorStyle;
        _stateCommon.Content.ColorAlign = source.TextColorAlign;
        _stateCommon.Content.ColorAngle = source.TextColorAngle;
    }

    #endregion

    #region EnableOpposite / Restore

    /// <summary>
    /// Called when UseOppositeTextColors is enabled: stores current text color and sets text to opposite of back color.
    /// </summary>
    internal void EnableOppositeTextColors()
    {
        _stateCommon.EnableOppositeTextColors();
        _stateNormal.EnableOppositeTextColors();
        _stateDisabled.EnableOppositeTextColors();
    }

    /// <summary>
    /// Called when UseOppositeTextColors is disabled: restores the stored text color.
    /// </summary>
    internal void RestoreOriginalTextColor()
    {
        _stateCommon.RestoreOriginalTextColor();
        _stateNormal.RestoreOriginalTextColor();
        _stateDisabled.RestoreOriginalTextColor();
    }

    #endregion

    #region Reset

    /// <summary>
    /// Resets all values to their default for this region.
    /// </summary>
    public void Reset()
    {
        _stateCommon.Reset(); // StateCommon.Back.Color1 is already set to _defaultBackColor in Reset()
        _stateNormal.Reset();
        _stateDisabled.Reset();
    }

    #endregion

    #region Internal

    /// <summary>
    /// Called when a state property changes. Notifies the parent.
    /// </summary>
    internal void OnStateChanged(ProgressBarTriStateRegionStateValues state)
    {
        _parent.NotifyRegionChanged(this);
    }

    #endregion

    #region Public Overrides

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? "Modified" : GlobalStaticValues.DEFAULT_EMPTY_STRING;

    #endregion#
}

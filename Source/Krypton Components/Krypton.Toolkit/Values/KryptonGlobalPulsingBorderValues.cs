#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Application-wide pulsing border defaults grouped by control type.
/// </summary>
/// <remarks>
/// Each group is an independent <see cref="InputPulsingBorderValues"/> instance. Controls inherit
/// unset properties from the matching group:
/// <list type="bullet">
/// <item><see cref="Forms"/> — <see cref="KryptonForm"/></item>
/// <item><see cref="Buttons"/> — <see cref="KryptonButton"/>, <see cref="KryptonDropButton"/>, <see cref="KryptonColorButton"/></item>
/// <item><see cref="Inputs"/> — text, combo, numeric, date, and similar input controls</item>
/// <item><see cref="Other"/> — <see cref="KryptonCheckBox"/>, <see cref="KryptonRadioButton"/>, <see cref="KryptonLabel"/></item>
/// </list>
/// </remarks>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class KryptonGlobalPulsingBorderValues : Storage
{
    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonGlobalPulsingBorderValues"/> class.
    /// </summary>
    /// <param name="needPaint">Delegate invoked when any group value changes.</param>
    internal KryptonGlobalPulsingBorderValues(NeedPaintHandler? needPaint)
    {
        NeedPaint = needPaint;
        Forms = CreateGroup(needPaint, InputPulsingBorderStyle.All, InputPulsingBorderShowWhen.Active);
        Buttons = CreateGroup(needPaint, InputPulsingBorderStyle.All, InputPulsingBorderShowWhen.Active);
        Inputs = CreateGroup(needPaint, InputPulsingBorderStyle.Bottom, InputPulsingBorderShowWhen.Focused);
        Other = CreateGroup(needPaint, InputPulsingBorderStyle.All, InputPulsingBorderShowWhen.Active);
    }

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? @"Modified" : string.Empty;

    #endregion

    #region IsDefault

    /// <inheritdoc />
    public override bool IsDefault => Forms.IsDefault
                                      && Buttons.IsDefault
                                      && Inputs.IsDefault
                                      && Other.IsDefault;

    #endregion

    #region Groups

    /// <summary>
    /// Gets the default pulsing border settings inherited by <see cref="KryptonForm"/>.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Default pulsing border for KryptonForm chrome. Unset form properties inherit these values.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public InputPulsingBorderValues Forms { get; }

    private bool ShouldSerializeForms() => !Forms.IsDefault;

    /// <summary>
    /// Gets the default pulsing border settings inherited by button-style controls.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Default pulsing border for KryptonButton, KryptonDropButton, and KryptonColorButton.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public InputPulsingBorderValues Buttons { get; }

    private bool ShouldSerializeButtons() => !Buttons.IsDefault;

    /// <summary>
    /// Gets the default pulsing border settings inherited by input controls.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Default pulsing border for TextBox, ComboBox, NumericUpDown, DateTimePicker, and similar inputs.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public InputPulsingBorderValues Inputs { get; }

    private bool ShouldSerializeInputs() => !Inputs.IsDefault;

    /// <summary>
    /// Gets the default pulsing border settings inherited by other simple controls.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Default pulsing border for KryptonCheckBox, KryptonRadioButton, and KryptonLabel.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public InputPulsingBorderValues Other { get; }

    private bool ShouldSerializeOther() => !Other.IsDefault;

    #endregion

    #region Public

    /// <summary>
    /// Returns the global values for the specified control category.
    /// </summary>
    /// <param name="category">Control category.</param>
    /// <returns>The matching global <see cref="InputPulsingBorderValues"/>.</returns>
    public InputPulsingBorderValues Get(InputPulsingBorderCategory category) => category switch
    {
        InputPulsingBorderCategory.Buttons => Buttons,
        InputPulsingBorderCategory.Forms => Forms,
        InputPulsingBorderCategory.Other => Other,
        _ => Inputs
    };

    /// <summary>
    /// Restores every group to its factory defaults.
    /// </summary>
    public void Reset()
    {
        Forms.Reset();
        Buttons.Reset();
        Inputs.Reset();
        Other.Reset();
    }

    #endregion

    #region Implementation

    private static InputPulsingBorderValues CreateGroup(NeedPaintHandler? needPaint,
        InputPulsingBorderStyle defaultStyle,
        InputPulsingBorderShowWhen defaultShowWhen) =>
        new InputPulsingBorderValues(needPaint, inheritCategory: null, defaultStyle, defaultShowWhen);

    #endregion
}

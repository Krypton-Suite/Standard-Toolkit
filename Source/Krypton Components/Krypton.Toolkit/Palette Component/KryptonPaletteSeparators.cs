#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Storage for separator palette settings.
/// </summary>
public class KryptonPaletteSeparators : Storage
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonPaletteSeparators class.
    /// </summary>
    /// <param name="redirector">Palette redirector for sourcing inherited values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public KryptonPaletteSeparators([DisallowNull] PaletteRedirect redirector,
        NeedPaintHandler needPaint)
    {
        Debug.Assert(redirector != null);

        // Create the button style specific and common palettes
        SeparatorCommon = new KryptonPaletteSeparator(redirector, PaletteBackStyle.SeparatorLowProfile, PaletteBorderStyle.SeparatorLowProfile, needPaint);
        SeparatorLowProfile = new KryptonPaletteSeparator(redirector, PaletteBackStyle.SeparatorLowProfile, PaletteBorderStyle.SeparatorLowProfile, needPaint);
        SeparatorHighProfile = new KryptonPaletteSeparator(redirector, PaletteBackStyle.SeparatorHighProfile, PaletteBorderStyle.SeparatorHighProfile, needPaint);
        SeparatorHighInternalProfile = new KryptonPaletteSeparator(redirector, PaletteBackStyle.SeparatorHighInternalProfile, PaletteBorderStyle.SeparatorHighInternalProfile, needPaint);
        SeparatorCustom1 = new KryptonPaletteSeparator(redirector, PaletteBackStyle.SeparatorCustom1, PaletteBorderStyle.SeparatorCustom1, needPaint);
        SeparatorCustom2 = new KryptonPaletteSeparator(redirector, PaletteBackStyle.SeparatorCustom2, PaletteBorderStyle.SeparatorCustom2, needPaint);
        SeparatorCustom3 = new KryptonPaletteSeparator(redirector, PaletteBackStyle.SeparatorCustom3, PaletteBorderStyle.SeparatorCustom3, needPaint);

        // Create redirectors for inheriting from style specific to style common
        var redirectCommon = new PaletteRedirectDouble(redirector!, SeparatorCommon.StateDisabled,
            SeparatorCommon.StateNormal, SeparatorCommon.StatePressed, SeparatorCommon.StateTracking);

        // Inform the button style to use the new redirector
        SeparatorLowProfile.SetRedirector(redirectCommon);
        SeparatorHighProfile.SetRedirector(redirectCommon);
        SeparatorHighInternalProfile.SetRedirector(redirectCommon);
        SeparatorCustom1.SetRedirector(redirectCommon);
        SeparatorCustom2.SetRedirector(redirectCommon);
        SeparatorCustom3.SetRedirector(redirectCommon);
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => SeparatorCommon.IsDefault &&
                                      SeparatorLowProfile.IsDefault &&
                                      SeparatorHighProfile.IsDefault &&
                                      SeparatorHighInternalProfile.IsDefault 
                                      && SeparatorCustom1.IsDefault
                                      && SeparatorCustom2.IsDefault
                                      && SeparatorCustom3.IsDefault
    ;

    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    /// <param name="common">Reference to common settings.</param>
    public void PopulateFromBase(KryptonPaletteCommon common)
    {
        // Populate only the designated styles
        common.StateCommon.BackStyle = PaletteBackStyle.SeparatorLowProfile;
        common.StateCommon.BorderStyle = PaletteBorderStyle.SeparatorLowProfile;
        SeparatorLowProfile.PopulateFromBase(PaletteMetricPadding.SeparatorPaddingLowProfile);
        common.StateCommon.BackStyle = PaletteBackStyle.SeparatorHighProfile;
        common.StateCommon.BorderStyle = PaletteBorderStyle.SeparatorHighProfile;
        SeparatorHighProfile.PopulateFromBase(PaletteMetricPadding.SeparatorPaddingHighProfile);
        common.StateCommon.BackStyle = PaletteBackStyle.SeparatorHighInternalProfile;
        common.StateCommon.BorderStyle = PaletteBorderStyle.SeparatorHighInternalProfile;
        SeparatorHighInternalProfile.PopulateFromBase(PaletteMetricPadding.SeparatorPaddingHighInternalProfile);
    }
    #endregion

    #region SeparatorCommon
    /// <summary>
    /// Gets access to the common separator appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common separator appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteSeparator SeparatorCommon { get; }

    private bool ShouldSerializeSeparatorCommon() => !SeparatorCommon.IsDefault;

    #endregion

    #region SeparatorLowProfile
    /// <summary>
    /// Gets access to the low profile separator appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining low profile separator appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteSeparator SeparatorLowProfile { get; }

    private bool ShouldSerializeSeparatorLowProfile() => !SeparatorLowProfile.IsDefault;

    #endregion

    #region SeparatorHighProfile
    /// <summary>
    /// Gets access to the high profile separator appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining high profile separator appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteSeparator SeparatorHighProfile { get; }

    private bool ShouldSerializeSeparatorHighProfile() => !SeparatorHighProfile.IsDefault;

    #endregion

    #region SeparatorHighInternalProfile
    /// <summary>
    /// Gets access to the high profile for internal separator appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining high profile for internal separator appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteSeparator SeparatorHighInternalProfile { get; }

    private bool ShouldSerializeSeparatorHighInternalProfile() => !SeparatorHighInternalProfile.IsDefault;

    #endregion

    #region SeparatorCustom1
    /// <summary>
    /// Gets access to the first custom separator appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining first custom separator appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteSeparator SeparatorCustom1 { get; }

    private bool ShouldSerializeSeparatorCustom1() => !SeparatorCustom1.IsDefault;

    #endregion

    #region SeparatorCustom2
    /// <summary>
    /// Gets access to the first custom separator appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining first custom separator appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteSeparator SeparatorCustom2 { get; }

    private bool ShouldSerializeSeparatorCustom2() => !SeparatorCustom2.IsDefault;

    #endregion

    #region SeparatorCustom3
    /// <summary>
    /// Gets access to the first custom separator appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining third custom separator appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteSeparator SeparatorCustom3 { get; }

    private bool ShouldSerializeSeparatorCustom3() => !SeparatorCustom3.IsDefault;

    #endregion
}
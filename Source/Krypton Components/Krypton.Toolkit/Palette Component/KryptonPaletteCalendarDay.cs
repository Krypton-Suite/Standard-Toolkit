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
/// Storage of palette calendar day states.
/// </summary>
public class KryptonPaletteCalendarDay : Storage
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonPaletteCalendarDay class.
    /// </summary>
    /// <param name="redirect">Redirector to inherit values from.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public KryptonPaletteCalendarDay(PaletteRedirect redirect,
        NeedPaintHandler needPaint) 
    {
        // Create the storage objects
        OverrideFocus = new PaletteTripleRedirect(redirect, PaletteBackStyle.ButtonCalendarDay, PaletteBorderStyle.ButtonCalendarDay, PaletteContentStyle.ButtonCalendarDay, needPaint);
        OverrideBolded = new PaletteTripleRedirect(redirect, PaletteBackStyle.ButtonCalendarDay, PaletteBorderStyle.ButtonCalendarDay, PaletteContentStyle.ButtonCalendarDay, needPaint);
        OverrideToday = new PaletteTripleRedirect(redirect, PaletteBackStyle.ButtonCalendarDay, PaletteBorderStyle.ButtonCalendarDay, PaletteContentStyle.ButtonCalendarDay, needPaint);
        StateCommon = new PaletteTripleRedirect(redirect, PaletteBackStyle.ButtonCalendarDay, PaletteBorderStyle.ButtonCalendarDay, PaletteContentStyle.ButtonCalendarDay, needPaint);
        StateDisabled = new PaletteTriple(StateCommon, needPaint);
        StateNormal = new PaletteTriple(StateCommon, needPaint);
        StateTracking = new PaletteTriple(StateCommon, needPaint);
        StatePressed = new PaletteTriple(StateCommon, needPaint);
        StateCheckedNormal = new PaletteTriple(StateCommon, needPaint);
        StateCheckedTracking = new PaletteTriple(StateCommon, needPaint);
        StateCheckedPressed = new PaletteTriple(StateCommon, needPaint);
    }
    #endregion

    #region SetRedirector
    /// <summary>
    /// Update the redirector with new reference.
    /// </summary>
    /// <param name="redirect">Target redirector.</param>
    public void SetRedirector(PaletteRedirect redirect)
    {
        OverrideFocus.SetRedirector(redirect);
        OverrideBolded.SetRedirector(redirect);
        OverrideToday.SetRedirector(redirect);
        StateCommon.SetRedirector(redirect);
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => StateCommon.IsDefault &&
                                      OverrideFocus.IsDefault &&
                                      OverrideBolded.IsDefault &&
                                      OverrideToday.IsDefault &&
                                      StateDisabled.IsDefault &&
                                      StateNormal.IsDefault &&
                                      StateTracking.IsDefault &&
                                      StatePressed.IsDefault &&
                                      StateCheckedNormal.IsDefault &&
                                      StateCheckedTracking.IsDefault &&
                                      StateCheckedPressed.IsDefault;

    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    public void PopulateFromBase()
    {
        // Populate only the designated styles
        OverrideFocus.PopulateFromBase(PaletteState.FocusOverride);
        OverrideBolded.PopulateFromBase(PaletteState.BoldedOverride);
        OverrideToday.PopulateFromBase(PaletteState.BoldedOverride);
        StateDisabled.PopulateFromBase(PaletteState.Disabled);
        StateNormal.PopulateFromBase(PaletteState.Normal);
        StateTracking.PopulateFromBase(PaletteState.Tracking);
        StatePressed.PopulateFromBase(PaletteState.Pressed);
        StateCheckedNormal.PopulateFromBase(PaletteState.CheckedNormal);
        StateCheckedTracking.PopulateFromBase(PaletteState.CheckedTracking);
        StateCheckedPressed.PopulateFromBase(PaletteState.CheckedPressed);
    }
    #endregion

    #region StateCommon
    /// <summary>
    /// Gets access to the common calendar day appearance that other states can override.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common calendar day appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTripleRedirect StateCommon { get; }

    private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

    #endregion

    #region StateDisabled
    /// <summary>
    /// Gets access to the disabled calendar day appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled calendar day appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StateDisabled { get; }

    private bool ShouldSerializeStateDisabled() => !StateDisabled.IsDefault;

    #endregion

    #region StateNormal
    /// <summary>
    /// Gets access to the normal calendar day appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal calendar day appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StateNormal { get; }

    private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;

    #endregion

    #region StateTracking
    /// <summary>
    /// Gets access to the hot tracking calendar day appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining hot tracking calendar day appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StateTracking { get; }

    private bool ShouldSerializeStateTracking() => !StateTracking.IsDefault;

    #endregion

    #region StatePressed
    /// <summary>
    /// Gets access to the pressed calendar day appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining pressed calendar day appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StatePressed { get; }

    private bool ShouldSerializeStatePressed() => !StatePressed.IsDefault;

    #endregion

    #region StateCheckedNormal
    /// <summary>
    /// Gets access to the normal checked calendar day appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal checked calendar day appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StateCheckedNormal { get; }

    private bool ShouldSerializeStateCheckedNormal() => !StateCheckedNormal.IsDefault;

    #endregion

    #region StateCheckedTracking
    /// <summary>
    /// Gets access to the hot tracking checked calendar day appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining hot tracking checked calendar day appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StateCheckedTracking { get; }

    private bool ShouldSerializeStateCheckedTracking() => !StateCheckedTracking.IsDefault;

    #endregion

    #region StateCheckedPressed
    /// <summary>
    /// Gets access to the pressed checked calendar day appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining pressed checked calendar day appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StateCheckedPressed { get; }

    private bool ShouldSerializeStateCheckedPressed() => !StateCheckedPressed.IsDefault;

    #endregion

    #region OverrideFocus
    /// <summary>
    /// Gets access to the calendar day appearance when it has focus.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining calendar day appearance when it has focus.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTripleRedirect OverrideFocus { get; }

    private bool ShouldSerializeOverrideFocus() => !OverrideFocus.IsDefault;

    #endregion

    #region OverrideBolded
    /// <summary>
    /// Gets access to the calendar day appearance when it has bolded days.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining calendar day appearance when it has bolded days.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTripleRedirect OverrideBolded { get; }

    private bool ShouldSerializeOverrideBolded() => !OverrideBolded.IsDefault;

    #endregion

    #region OverrideToday
    /// <summary>
    /// Gets access to the calendar day appearance when it is today.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining calendar day appearance when it is today.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTripleRedirect OverrideToday { get; }

    private bool ShouldSerializeOverrideToday() => !OverrideToday.IsDefault;

    #endregion
}
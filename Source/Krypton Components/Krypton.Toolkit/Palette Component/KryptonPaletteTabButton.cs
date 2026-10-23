#region BSD License
/*
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Storage of palette tab button states.
/// </summary>
public class KryptonPaletteTabButton : Storage
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonPaletteTabButton class.
    /// </summary>
    /// <param name="redirect">Redirector to inherit values from.</param>
    /// <param name="backStyle">Background style.</param>
    /// <param name="borderStyle">Border style.</param>
    /// <param name="contentStyle">Content style.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public KryptonPaletteTabButton(PaletteRedirect redirect,
        PaletteBackStyle backStyle,
        PaletteBorderStyle borderStyle,
        PaletteContentStyle contentStyle,
        NeedPaintHandler needPaint) 
    {
        // Create the storage objects
        OverrideFocus = new PaletteTabTripleRedirect(redirect, backStyle, borderStyle, contentStyle, needPaint);
        StateCommon = new PaletteTabTripleRedirect(redirect, backStyle, borderStyle, contentStyle, needPaint);
        StateDisabled = new PaletteTabTriple(StateCommon, needPaint);
        StateNormal = new PaletteTabTriple(StateCommon, needPaint);
        StateTracking = new PaletteTabTriple(StateCommon, needPaint);
        StatePressed = new PaletteTabTriple(StateCommon, needPaint);
        StateSelected = new PaletteTabTriple(StateCommon, needPaint);
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
                                      StateDisabled.IsDefault &&
                                      StateNormal.IsDefault &&
                                      StateTracking.IsDefault &&
                                      StatePressed.IsDefault &&
                                      StateSelected.IsDefault;

    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    public void PopulateFromBase()
    {
        // Populate only the designated styles
        OverrideFocus.PopulateFromBase(PaletteState.FocusOverride);
        StateDisabled.PopulateFromBase(PaletteState.Disabled);
        StateNormal.PopulateFromBase(PaletteState.Normal);
        StateTracking.PopulateFromBase(PaletteState.Tracking);
        StatePressed.PopulateFromBase(PaletteState.Pressed);
        StateSelected.PopulateFromBase(PaletteState.CheckedNormal);
    }
    #endregion

    #region StateCommon
    /// <summary>
    /// Gets access to the common tab appearance that other states can override.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common tab appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTabTripleRedirect StateCommon { get; }

    private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

    #endregion

    #region StateDisabled
    /// <summary>
    /// Gets access to the disabled tab appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled tab appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTabTriple StateDisabled { get; }

    private bool ShouldSerializeStateDisabled() => !StateDisabled.IsDefault;

    #endregion

    #region StateNormal
    /// <summary>
    /// Gets access to the normal tab appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal tab appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTabTriple StateNormal { get; }

    private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;

    #endregion

    #region StateTracking
    /// <summary>
    /// Gets access to the hot tracking tab appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining hot tracking tab appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTabTriple StateTracking { get; }

    private bool ShouldSerializeStateTracking() => !StateTracking.IsDefault;

    #endregion

    #region StatePressed
    /// <summary>
    /// Gets access to the pressed tab appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining pressed tab appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTabTriple StatePressed { get; }

    private bool ShouldSerializeStatePressed() => !StatePressed.IsDefault;

    #endregion

    #region StateSelected
    /// <summary>
    /// Gets access to the normal tab appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal tab appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTabTriple StateSelected { get; }

    private bool ShouldSerializeStateSelected() => !StateSelected.IsDefault;

    #endregion

    #region OverrideFocus
    /// <summary>
    /// Gets access to the tab appearance when it has focus.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining tab appearance when it has focus.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTabTripleRedirect OverrideFocus { get; }

    private bool ShouldSerializeOverrideFocus() => !OverrideFocus.IsDefault;

    #endregion
}
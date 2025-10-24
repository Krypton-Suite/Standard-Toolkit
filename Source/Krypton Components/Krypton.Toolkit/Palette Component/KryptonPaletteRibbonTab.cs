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
/// Storage for palette ribbon tab states.
/// </summary>
public class KryptonPaletteRibbonTab : Storage
{
    #region Instance Fields
    private readonly PaletteRibbonDoubleInheritRedirect _stateInherit;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonPaletteRibbonTab class.
    /// </summary>
    /// <param name="redirect">Redirector to inherit values from.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public KryptonPaletteRibbonTab(PaletteRedirect redirect,
        NeedPaintHandler needPaint) 
    {
        // Create the storage objects
        _stateInherit = new PaletteRibbonDoubleInheritRedirect(redirect, PaletteRibbonBackStyle.RibbonTab, PaletteRibbonTextStyle.RibbonTab);
        StateCommon = new PaletteRibbonDouble(_stateInherit, _stateInherit, needPaint);
        StateNormal = new PaletteRibbonDouble(StateCommon, StateCommon, needPaint);
        StateTracking = new PaletteRibbonDouble(StateCommon, StateCommon, needPaint);
        StateCheckedNormal = new PaletteRibbonDouble(StateCommon, StateCommon, needPaint);
        StateCheckedTracking = new PaletteRibbonDouble(StateCommon, StateCommon, needPaint);
        StateContextTracking = new PaletteRibbonDouble(StateCommon, StateCommon, needPaint);
        StateContextCheckedNormal = new PaletteRibbonDouble(StateCommon, StateCommon, needPaint);
        StateContextCheckedTracking = new PaletteRibbonDouble(StateCommon, StateCommon, needPaint);
        OverrideFocus = new PaletteRibbonDouble(_stateInherit, _stateInherit, needPaint);
    }
    #endregion

    #region SetRedirector
    /// <summary>
    /// Update the redirector with new reference.
    /// </summary>
    /// <param name="redirect">Target redirector.</param>
    public void SetRedirector(PaletteRedirect redirect) => _stateInherit.SetRedirector(redirect);

    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => StateCommon.IsDefault &&
                                      StateNormal.IsDefault &&
                                      StateTracking.IsDefault &&
                                      StateCheckedNormal.IsDefault &&
                                      StateCheckedTracking.IsDefault &&
                                      StateContextTracking.IsDefault &&
                                      StateContextCheckedNormal.IsDefault &&
                                      StateContextCheckedTracking.IsDefault &&
                                      OverrideFocus.IsDefault;

    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    public void PopulateFromBase()
    {
        // Populate only the designated styles
        StateNormal.PopulateFromBase(PaletteState.Normal);
        StateTracking.PopulateFromBase(PaletteState.Tracking);
        StateCheckedNormal.PopulateFromBase(PaletteState.CheckedNormal);
        StateCheckedTracking.PopulateFromBase(PaletteState.CheckedTracking);
        StateContextTracking.PopulateFromBase(PaletteState.ContextTracking);
        StateContextCheckedNormal.PopulateFromBase(PaletteState.ContextCheckedNormal);
        StateContextCheckedTracking.PopulateFromBase(PaletteState.ContextCheckedTracking);
        OverrideFocus.PopulateFromBase(PaletteState.FocusOverride);
    }
    #endregion

    #region StateCommon
    /// <summary>
    /// Gets access to the common ribbon tab appearance that other states can override.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common ribbon tab appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteRibbonDouble StateCommon { get; }

    private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

    #endregion
    
    #region StateNormal
    /// <summary>
    /// Gets access to the normal ribbon tab appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal ribbon tab appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteRibbonDouble StateNormal { get; }

    private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;

    #endregion

    #region StateTracking
    /// <summary>
    /// Gets access to the tracking ribbon tab appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining tracking ribbon tab appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteRibbonDouble StateTracking { get; }

    private bool ShouldSerializeStateTracking() => !StateTracking.IsDefault;

    #endregion

    #region StateCheckedNormal
    /// <summary>
    /// Gets access to the checked normal ribbon tab appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining checked normal ribbon tab appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteRibbonDouble StateCheckedNormal { get; }

    private bool ShouldSerializeStateCheckedNormal() => !StateCheckedNormal.IsDefault;

    #endregion

    #region StateCheckedTracking
    /// <summary>
    /// Gets access to the checked tracking ribbon tab appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining checked tracking ribbon tab appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteRibbonDouble StateCheckedTracking { get; }

    private bool ShouldSerializeStateCheckedTracking() => !StateCheckedTracking.IsDefault;

    #endregion

    #region StateContextTracking
    /// <summary>
    /// Gets access to the context tracking ribbon tab appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining context tracking ribbon tab appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteRibbonDouble StateContextTracking { get; }

    private bool ShouldSerializeStateContextTracking() => !StateContextTracking.IsDefault;

    #endregion

    #region StateContextCheckedNormal
    /// <summary>
    /// Gets access to the checked normal ribbon tab appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining checked normal ribbon tab appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteRibbonDouble StateContextCheckedNormal { get; }

    private bool ShouldSerializeStateContextCheckedNormal() => !StateContextCheckedNormal.IsDefault;

    #endregion

    #region StateContextCheckedTracking
    /// <summary>
    /// Gets access to the context checked tracking ribbon tab appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining context checked tracking ribbon tab appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteRibbonDouble StateContextCheckedTracking { get; }

    private bool ShouldSerializeStateContextCheckedTracking() => !StateContextCheckedTracking.IsDefault;

    #endregion

    #region StateContextCheckedTracking
    /// <summary>
    /// Gets access to the focus overrides for ribbon tab appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining focus ribbon tab appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteRibbonDouble OverrideFocus { get; }

    private bool ShouldSerializeOverrideFocus() => !OverrideFocus.IsDefault;

    #endregion
}
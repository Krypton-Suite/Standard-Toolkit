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
/// Storage for palette ribbon group collapsed border states.
/// </summary>
public class KryptonPaletteRibbonGroupCollapsedBorder : Storage
{
    #region Instance Fields
    private readonly PaletteRibbonBackInheritRedirect _stateInherit;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonPaletteRibbonGroupCollapsedBorder class.
    /// </summary>
    /// <param name="redirect">Redirector to inherit values from.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public KryptonPaletteRibbonGroupCollapsedBorder(PaletteRedirect redirect,
        NeedPaintHandler needPaint) 
    {
        // Create the storage objects
        _stateInherit = new PaletteRibbonBackInheritRedirect(redirect, PaletteRibbonBackStyle.RibbonGroupCollapsedBorder);
        StateCommon = new PaletteRibbonBack(_stateInherit, needPaint);
        StateNormal = new PaletteRibbonBack(StateCommon, needPaint);
        StateTracking = new PaletteRibbonBack(StateCommon, needPaint);
        StateContextNormal = new PaletteRibbonBack(StateCommon, needPaint);
        StateContextTracking = new PaletteRibbonBack(StateCommon, needPaint);
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
                                      StateContextNormal.IsDefault &&
                                      StateContextTracking.IsDefault;

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
        StateContextNormal.PopulateFromBase(PaletteState.ContextNormal);
        StateContextTracking.PopulateFromBase(PaletteState.ContextTracking);
    }
    #endregion

    #region StateCommon
    /// <summary>
    /// Gets access to the common ribbon group collapsed border appearance that other states can override.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common ribbon group collapsed border appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteRibbonBack StateCommon { get; }

    private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

    #endregion

    #region StateNormal
    /// <summary>
    /// Gets access to the normal ribbon group collapsed border appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal ribbon group collapsed border appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteRibbonBack StateNormal { get; }

    private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;

    #endregion

    #region StateTracking
    /// <summary>
    /// Gets access to the tracking ribbon group collapsed border appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining tracking ribbon group collapsed border appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteRibbonBack StateTracking { get; }

    private bool ShouldSerializeStateTracking() => !StateTracking.IsDefault;

    #endregion

    #region StateContextNormal
    /// <summary>
    /// Gets access to the context normal ribbon group collapsed border appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining context normal ribbon group collapsed border appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteRibbonBack StateContextNormal { get; }

    private bool ShouldSerializeStateContextNormal() => !StateContextNormal.IsDefault;

    #endregion

    #region StateContextTracking
    /// <summary>
    /// Gets access to the context tracking ribbon group collapsed border appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining context tracking ribbon group collapsed border appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteRibbonBack StateContextTracking { get; }

    private bool ShouldSerializeStateContextTracking() => !StateContextTracking.IsDefault;

    #endregion
}
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
/// Storage for palette ribbon group area states.
/// </summary>
public class KryptonPaletteRibbonGroupArea : Storage
{
    #region Instance Fields
    private readonly PaletteRibbonBackInheritRedirect _stateInherit;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonPaletteRibbonGroupArea class.
    /// </summary>
    /// <param name="redirect">Redirector to inherit values from.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public KryptonPaletteRibbonGroupArea(PaletteRedirect redirect,
        NeedPaintHandler needPaint) 
    {
        // Create the storage objects
        _stateInherit = new PaletteRibbonBackInheritRedirect(redirect, PaletteRibbonBackStyle.RibbonGroupArea);
        StateCommon = new PaletteRibbonBack(_stateInherit, needPaint);
        StateCheckedNormal = new PaletteRibbonBack(StateCommon, needPaint);
        StateContextCheckedNormal = new PaletteRibbonBack(StateCommon, needPaint);
        StateTracking = new PaletteRibbonBack(StateCommon, needPaint);
        StateContextPressed = new PaletteRibbonBack(StateCommon, needPaint);
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
                                      StateCheckedNormal.IsDefault &&
                                      StateContextCheckedNormal.IsDefault &&
                                      StateTracking.IsDefault &&
                                      StateContextPressed.IsDefault &&
                                      StateContextTracking.IsDefault;

    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    public void PopulateFromBase()
    {
        // Populate only the designated styles
        StateCheckedNormal.PopulateFromBase(PaletteState.CheckedNormal);
        StateContextCheckedNormal.PopulateFromBase(PaletteState.ContextCheckedNormal);
        StateTracking.PopulateFromBase(PaletteState.Tracking);
        StateContextPressed.PopulateFromBase(PaletteState.Pressed);
        StateContextTracking.PopulateFromBase(PaletteState.Tracking);
    }
    #endregion

    #region StateCommon
    /// <summary>
    /// Gets access to the common ribbon application button appearance that other states can override.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common ribbon application button appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteRibbonBack StateCommon { get; }

    private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

    #endregion

    #region StateCheckedNormal
    /// <summary>
    /// Gets access to the checked ribbon group area appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining checked ribbon group area appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteRibbonBack StateCheckedNormal { get; }

    private bool ShouldSerializeStateCheckedNormal() => !StateCheckedNormal.IsDefault;

    #endregion

    #region StateContextCheckedNormal
    /// <summary>
    /// Gets access to the context checked ribbon group area appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining context checked ribbon group area appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteRibbonBack StateContextCheckedNormal { get; }

    private bool ShouldSerializeStateContextCheckedNormal() => !StateContextCheckedNormal.IsDefault;

    #endregion

    #region StateTracking
    /// <summary>
    /// Gets access to the tracking ribbon group area appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining tracking ribbon group area appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteRibbonBack StateTracking { get; }

    private bool ShouldSerializeStateTracking() => !StateTracking.IsDefault;

    #endregion

    #region StateContextPressed
    /// <summary>
    /// Gets access to the context pressed ribbon group area appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining context pressed ribbon group area appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteRibbonBack StateContextPressed { get; }

    private bool ShouldSerializeStateContextPressed() => !StateContextPressed.IsDefault;
    #endregion

    #region StateContextTracking
    /// <summary>
    /// Gets access to the context tracking ribbon group area appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining context tracking ribbon group area appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteRibbonBack StateContextTracking { get; }

    private bool ShouldSerializeStateContextTracking() => !StateContextTracking.IsDefault;
    #endregion
}
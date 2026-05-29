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
/// Storage for palette input control states.
/// </summary>
public class KryptonPaletteInputControl : Storage
{
    #region Instance Fields

    internal PaletteTripleRedirect _stateCommon;
    internal PaletteTriple _stateDisabled;
    internal PaletteTriple _stateNormal;
    internal PaletteTriple _stateActive;
    internal PaletteTriple _statePressed;
    internal PaletteTriple _stateContextNormal;
    internal PaletteTriple _stateContextTracking;
    internal PaletteTriple _stateContextPressed;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonPaletteInputControl class.
    /// </summary>
    /// <param name="redirect">Redirector to inherit values from.</param>
    /// <param name="backStyle">Background style.</param>
    /// <param name="borderStyle">Border style.</param>
    /// <param name="contentStyle">Content style.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public KryptonPaletteInputControl(PaletteRedirect redirect,
        PaletteBackStyle backStyle,
        PaletteBorderStyle borderStyle,
        PaletteContentStyle contentStyle,
        NeedPaintHandler needPaint)
    {
        // Create the storage objects
        _stateCommon = new PaletteTripleRedirect(redirect, backStyle, borderStyle, contentStyle, needPaint);
        _stateDisabled = new PaletteTriple(_stateCommon, needPaint);
        _stateNormal = new PaletteTriple(_stateCommon, needPaint);
        _stateActive = new PaletteTriple(_stateCommon, needPaint);
        _statePressed = new PaletteTriple(_stateCommon, needPaint);
        _stateContextNormal = new PaletteTriple(_stateCommon, needPaint);
        _stateContextTracking = new PaletteTriple(_stateCommon, needPaint);
        _stateContextPressed = new PaletteTriple(_stateCommon, needPaint);
    }
    #endregion

    #region SetRedirector
    /// <summary>
    /// Update the redirector with new reference.
    /// </summary>
    /// <param name="redirect">Target redirector.</param>
    public void SetRedirector(PaletteRedirect redirect) => _stateCommon.SetRedirector(redirect);

    #endregion

    #region IsDefault

    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => _stateCommon.IsDefault &&
                                      _stateDisabled.IsDefault &&
                                      _stateNormal.IsDefault &&
                                      _stateActive.IsDefault &&
                                      _statePressed.IsDefault &&
                                      _stateContextNormal.IsDefault &&
                                      _stateContextTracking.IsDefault &&
                                      _stateContextPressed.IsDefault;

    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    public void PopulateFromBase()
    {
        // Populate only the designated styles
        _stateDisabled.PopulateFromBase(PaletteState.Disabled);
        _stateNormal.PopulateFromBase(PaletteState.Normal);
        _stateActive.PopulateFromBase(PaletteState.Tracking);
        _statePressed.PopulateFromBase(PaletteState.Pressed);
        _stateContextNormal.PopulateFromBase(PaletteState.ContextNormal);
        _stateContextPressed.PopulateFromBase(PaletteState.ContextTracking);
        _stateContextTracking.PopulateFromBase(PaletteState.ContextPressed);
    }
    #endregion

    #region StateCommon
    /// <summary>
    /// Gets access to the common input control appearance that other states can override.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common input control appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTripleRedirect StateCommon => _stateCommon;

    private bool ShouldSerializeStateCommon() => !_stateCommon.IsDefault;

    #endregion

    #region StateDisabled
    /// <summary>
    /// Gets access to the disabled input control appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled input control appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StateDisabled => _stateDisabled;

    private bool ShouldSerializeStateDisabled() => !_stateDisabled.IsDefault;

    #endregion

    #region StateNormal
    /// <summary>
    /// Gets access to the normal input control appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal input control appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StateNormal => _stateNormal;

    private bool ShouldSerializeStateNormal() => !_stateNormal.IsDefault;

    #endregion

    #region StateActive
    /// <summary>
    /// Gets access to the active input control appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining active input control appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StateActive => _stateActive;

    private bool ShouldSerializeStateActive() => !_stateActive.IsDefault;

    #endregion

    #region StatePressed

    /// <summary>
    /// Gets access to the pressed input control appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining pressed input control appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StatePressed => _statePressed;

    private bool ShouldSerializeStatePressed() => !_statePressed.IsDefault;

    #endregion

    #region StateContextNormal

    /// <summary>
    /// Gets access to the context normal input control appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining context normal input control appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StateContextNormal => _stateContextNormal;

    private bool ShouldSerializeStateContextNormal() => !_stateContextNormal.IsDefault;

    #endregion

    #region StateContextTracking

    /// <summary>
    /// Gets access to the context tracking input control appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining context tracking input control appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StateContextTracking => _stateContextTracking;

    private bool ShouldSerializeStateContextTracking() => !_stateContextTracking.IsDefault;

    #endregion

    #region StateContextPressed

    /// <summary>
    /// Gets access to the context pressed input control appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining context pressed input control appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple StateContextPressed => _stateContextPressed;

    private bool ShouldSerializeStateContextPressed() => !_stateContextPressed.IsDefault;

    #endregion
}
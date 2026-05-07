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
/// Storage for palette form states.
/// </summary>
public class KryptonPaletteForm : KryptonPaletteDouble3
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonPaletteForm class.
    /// </summary>
    /// <param name="redirect">Redirector to inherit values from.</param>
    /// <param name="backStyle">Background style.</param>
    /// <param name="borderStyle">Border style.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public KryptonPaletteForm(PaletteRedirect redirect,
        PaletteBackStyle backStyle,
        PaletteBorderStyle borderStyle,
        NeedPaintHandler needPaint) 
        : base(redirect, backStyle, borderStyle, needPaint)
    {
    }
    #endregion

    #region StateCommon
    /// <summary>
    /// Gets access to the common control appearance that other states can override.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common control appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteDoubleRedirect StateCommon => _stateCommon;

    private bool ShouldSerializeStateCommon() => !_stateCommon.IsDefault;

    #endregion
    
    #region StateInactive
    /// <summary>
    /// Gets access to the inactive form appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining inactive form appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteDouble StateInactive => _stateDisabled;

    private bool ShouldSerializeStateInactive() => !_stateDisabled.IsDefault;

    #endregion

    #region StateActive
    /// <summary>
    /// Gets access to the active form appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining active form appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteDouble StateActive => _stateNormal;

    private bool ShouldSerializeStateActive() => !_stateNormal.IsDefault;

    #endregion
}
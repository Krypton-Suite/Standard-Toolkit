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
/// Base storage class for palette double (background/border) that expose three states.
/// </summary>
public abstract class KryptonPaletteDouble3 : Storage
{
    #region Instance Fields
    internal PaletteDoubleRedirect _stateCommon;
    internal PaletteDouble _stateDisabled;
    internal PaletteDouble _stateNormal;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of KryptonPaletteDouble3 KryptonPaletteControl class.
    /// </summary>
    /// <param name="redirect">Redirector to inherit values from.</param>
    /// <param name="backStyle">Background style.</param>
    /// <param name="borderStyle">Border style.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    protected KryptonPaletteDouble3(PaletteRedirect redirect,
        PaletteBackStyle backStyle,
        PaletteBorderStyle borderStyle,
        NeedPaintHandler needPaint)
    {
        // Create the storage objects
        _stateCommon = new PaletteDoubleRedirect(redirect, backStyle, borderStyle, needPaint);
        _stateDisabled = new PaletteDouble(_stateCommon, needPaint);
        _stateNormal = new PaletteDouble(_stateCommon, needPaint);
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
                                      _stateNormal.IsDefault;

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
    }
    #endregion
}
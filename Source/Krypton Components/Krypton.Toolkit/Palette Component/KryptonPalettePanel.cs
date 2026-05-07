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
/// Storage for palette panel states.
/// </summary>
public class KryptonPalettePanel : Storage
{
    #region Instance Fields
    private readonly PaletteBackInheritRedirect _stateInherit;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonPalettePanel class.
    /// </summary>
    /// <param name="redirect">Redirector to inherit values from.</param>
    /// <param name="backStyle">Back style.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public KryptonPalettePanel(PaletteRedirect redirect,
        PaletteBackStyle backStyle,
        NeedPaintHandler needPaint) 
    {
        // Create the storage objects
        _stateInherit = new PaletteBackInheritRedirect(redirect, backStyle);
        StateCommon = new PaletteBack(_stateInherit, needPaint);
        StateDisabled = new PaletteBack(StateCommon, needPaint);
        StateNormal = new PaletteBack(StateCommon, needPaint);
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
                                      StateDisabled.IsDefault &&
                                      StateNormal.IsDefault;

    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    public void PopulateFromBase()
    {
        // Populate only the designated styles
        StateDisabled.PopulateFromBase(PaletteState.Disabled);
        StateNormal.PopulateFromBase(PaletteState.Normal);
    }
    #endregion

    #region StateCommon
    /// <summary>
    /// Gets access to the common panel appearance that other states can override.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common panel appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack StateCommon { get; }

    private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

    #endregion
    
    #region StateDisabled
    /// <summary>
    /// Gets access to the disabled panel appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled panel appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack StateDisabled { get; }

    private bool ShouldSerializeStateDisabled() => !StateDisabled.IsDefault;

    #endregion

    #region StateNormal
    /// <summary>
    /// Gets access to the normal panel appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal panel appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack StateNormal { get; }

    private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;

    #endregion
}
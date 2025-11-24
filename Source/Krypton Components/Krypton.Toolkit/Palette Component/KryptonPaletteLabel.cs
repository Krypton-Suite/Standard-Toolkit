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
/// Storage for palette label states.
/// </summary>
public class KryptonPaletteLabel : Storage
{
    #region Instance Fields
    private readonly PaletteContentInheritRedirect _stateInherit;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonPaletteLabel class.
    /// </summary>
    /// <param name="redirect">Redirector to inherit values from.</param>
    /// <param name="contentStyle">Content style.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public KryptonPaletteLabel(PaletteRedirect redirect,
        PaletteContentStyle contentStyle,
        NeedPaintHandler needPaint) 
    {
        // Create the storage objects
        _stateInherit = new PaletteContentInheritRedirect(redirect, contentStyle);
        StateCommon = new PaletteContent(_stateInherit, needPaint);
        StateDisabled = new PaletteContent(StateCommon, needPaint);
        StateNormal = new PaletteContent(StateCommon, needPaint);
        OverrideFocus = new PaletteContent(_stateInherit, needPaint);
        OverrideVisited = new PaletteContent(_stateInherit, needPaint);
        OverrideNotVisited = new PaletteContent(_stateInherit, needPaint);
        OverridePressed = new PaletteContent(_stateInherit, needPaint);
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
                                      StateNormal.IsDefault &&
                                      OverrideFocus.IsDefault &&
                                      OverrideVisited.IsDefault &&
                                      OverrideNotVisited.IsDefault &&
                                      OverridePressed.IsDefault;

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
        OverrideFocus.PopulateFromBase(PaletteState.FocusOverride);
        OverrideVisited.PopulateFromBase(PaletteState.LinkVisitedOverride);
        OverrideNotVisited.PopulateFromBase(PaletteState.LinkNotVisitedOverride);
        OverridePressed.PopulateFromBase(PaletteState.LinkPressedOverride);
    }
    #endregion

    #region StateCommon
    /// <summary>
    /// Gets access to the common label appearance that other states can override.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common label appearance that other states can override.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteContent StateCommon { get; }

    private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

    #endregion
    
    #region StateDisabled
    /// <summary>
    /// Gets access to the disabled label appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining disabled label appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteContent StateDisabled { get; }

    private bool ShouldSerializeStateDisabled() => !StateDisabled.IsDefault;

    #endregion

    #region StateNormal
    /// <summary>
    /// Gets access to the normal label appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining normal label appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteContent StateNormal { get; }

    private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;

    #endregion

    #region OverrideFocus
    /// <summary>
    /// Gets access to the label appearance when it has focus.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining label appearance when it has focus.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteContent OverrideFocus { get; }

    private bool ShouldSerializeOverrideFocus() => !OverrideFocus.IsDefault;

    #endregion

    #region OverrideVisited
    /// <summary>
    /// Gets access to normal state modifications when label has been visited.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for modifying normal state when label has been visited.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteContent OverrideVisited { get; }

    private bool ShouldSerializeOverrideVisited() => !OverrideVisited.IsDefault;

    #endregion

    #region OverrideNotVisited
    /// <summary>
    /// Gets access to normal state modifications when label has not been visited.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for modifying normal state when label has not been visited.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteContent OverrideNotVisited { get; }

    private bool ShouldSerializeOverrideNotVisited() => !OverrideNotVisited.IsDefault;

    #endregion

    #region OverridePressed
    /// <summary>
    /// Gets access to the pressed label appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining pressed label appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteContent OverridePressed { get; }

    private bool ShouldSerializeOverridePressed() => !OverridePressed.IsDefault;

    #endregion
}
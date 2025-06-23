#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Implement storage for a ribbon disabled state.
/// </summary>
public class PaletteRibbonDisabled : Storage
{
    #region Instance Fields
    private readonly PaletteRibbonText _ribbonGroupCheckBoxText;
    private readonly PaletteRibbonText _ribbonGroupButtonText;
    private readonly PaletteRibbonText _ribbonGroupLabelText;
    private readonly PaletteRibbonText _ribbonGroupRadioButtonText;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteRibbonDisabled class.
    /// </summary>
    /// <param name="inherit">Source for inheriting values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteRibbonDisabled(PaletteRibbonRedirect inherit,
        NeedPaintHandler needPaint)
    {
        // Create storage that maps onto the inherit instances
        _ribbonGroupCheckBoxText = new PaletteRibbonText(inherit.RibbonGroupCheckBoxText, needPaint);
        _ribbonGroupButtonText = new PaletteRibbonText(inherit.RibbonGroupButtonText, needPaint);
        _ribbonGroupLabelText = new PaletteRibbonText(inherit.RibbonGroupLabelText, needPaint);
        _ribbonGroupRadioButtonText = new PaletteRibbonText(inherit.RibbonGroupRadioButtonText, needPaint);
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => RibbonGroupCheckBoxText.IsDefault &&
                                      RibbonGroupButtonText.IsDefault &&
                                      RibbonGroupLabelText.IsDefault &&
                                      RibbonGroupRadioButtonText.IsDefault;

    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    /// <param name="state">The palette state to populate with.</param>
    public virtual void PopulateFromBase(PaletteState state)
    {
        _ribbonGroupCheckBoxText.PopulateFromBase(state);
        _ribbonGroupButtonText.PopulateFromBase(state);
        _ribbonGroupLabelText.PopulateFromBase(state);
        _ribbonGroupRadioButtonText.PopulateFromBase(state);
    }
    #endregion

    #region SetInherit
    /// <summary>
    /// Sets the inheritance parent.
    /// </summary>
    public virtual void SetInherit(PaletteRibbonRedirect inherit)
    {
        _ribbonGroupCheckBoxText.SetInherit(inherit.RibbonGroupCheckBoxText);
        _ribbonGroupButtonText.SetInherit(inherit.RibbonGroupButtonText);
        _ribbonGroupLabelText.SetInherit(inherit.RibbonGroupLabelText);
        _ribbonGroupRadioButtonText.SetInherit(inherit.RibbonGroupCheckBoxText);
    }
    #endregion

    #region RibbonGroupCheckBoxText
    /// <summary>
    /// Gets access to the ribbon group check box label palette details.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining ribbon group check box label appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteRibbonText RibbonGroupCheckBoxText => _ribbonGroupCheckBoxText;

    private bool ShouldSerializeRibbonGroupCheckBoxText() => !_ribbonGroupCheckBoxText.IsDefault;

    #endregion

    #region RibbonGroupButtonText
    /// <summary>
    /// Gets access to the ribbon group button text palette details.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining ribbon group button text appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteRibbonText RibbonGroupButtonText => _ribbonGroupButtonText;

    private bool ShouldSerializeRibbonGroupButtonText() => !_ribbonGroupButtonText.IsDefault;

    #endregion

    #region RibbonGroupLabelText
    /// <summary>
    /// Gets access to the ribbon group label label palette details.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining ribbon group label label appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteRibbonText RibbonGroupLabelText => _ribbonGroupLabelText;

    private bool ShouldSerializeRibbonGroupLabelText() => !_ribbonGroupLabelText.IsDefault;

    #endregion

    #region RibbonGroupRadioButtonText
    /// <summary>
    /// Gets access to the ribbon group radio button label palette details.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining ribbon group radio button label appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteRibbonText RibbonGroupRadioButtonText => _ribbonGroupRadioButtonText;

    private bool ShouldSerializeRibbonGroupRadioButtonText() => !_ribbonGroupRadioButtonText.IsDefault;

    #endregion

    #region Implementation
    /// <summary>
    /// Handle a change event from palette source.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="needLayout">True if a layout is also needed.</param>
    protected void OnNeedPaint(object? sender, bool needLayout) =>
        // Pass request from child to our own handler
        PerformNeedPaint(needLayout);
    #endregion
}
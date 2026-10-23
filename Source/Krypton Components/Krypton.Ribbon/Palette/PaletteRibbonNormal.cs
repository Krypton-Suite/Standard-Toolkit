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
/// Implement storage for the normal ribbon state.
/// </summary>
public class PaletteRibbonNormal : PaletteRibbonAppGroupTab
{
    #region Instance Fields
    private readonly PaletteRibbonText _ribbonGroupCheckBoxText;
    private readonly PaletteRibbonText _ribbonGroupButtonText;
    private readonly PaletteRibbonText _ribbonGroupLabelText;
    private readonly PaletteRibbonText _ribbonGroupRadioButtonText;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteRibbonNormal class.
    /// </summary>
    /// <param name="inherit">Source for inheriting values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteRibbonNormal(PaletteRibbonRedirect inherit,
        NeedPaintHandler needPaint)
        : base(inherit, needPaint)
    {
        RibbonFileAppTab = new PaletteRibbonFileAppTab(inherit.RibbonFileAppTab, needPaint);
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
    public override bool IsDefault => base.IsDefault
                                      && RibbonFileAppTab.IsDefault
                                      && RibbonGroupCheckBoxText.IsDefault
                                      && RibbonGroupButtonText.IsDefault
                                      && RibbonGroupLabelText.IsDefault
                                      && RibbonGroupRadioButtonText.IsDefault;

    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    /// <param name="state">The palette state to populate with.</param>
    public override void PopulateFromBase(PaletteState state)
    {
        base.PopulateFromBase(state);
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
    public override void SetInherit(PaletteRibbonRedirect inherit)
    {
        base.SetInherit(inherit);
        _ribbonGroupCheckBoxText.SetInherit(inherit.RibbonGroupCheckBoxText);
        _ribbonGroupButtonText.SetInherit(inherit.RibbonGroupButtonText);
        _ribbonGroupLabelText.SetInherit(inherit.RibbonGroupLabelText);
        _ribbonGroupRadioButtonText.SetInherit(inherit.RibbonGroupRadioButtonText);
    }
    #endregion


    /// <summary>
    /// Gets the set of ribbon application button display strings.
    /// </summary>
    [Category(@"Values")]
    [Description(@"Collection of ribbon 'File app button' settings.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteRibbonFileAppTab RibbonFileAppTab { get; }
    private bool ShouldSerializeRibbonFileAppTab() => !RibbonFileAppTab.IsDefault;

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
    /// Gets access to the ribbon group label text palette details.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining ribbon group label text appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteRibbonText RibbonGroupLabelText => _ribbonGroupLabelText;

    private bool ShouldSerializeRibbonGroupLabelText() => !_ribbonGroupLabelText.IsDefault;

    #endregion

    #region RibbonGroupRadioButtonTetxt
    /// <summary>
    /// Gets access to the ribbon group radio button label palette details.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining ribbon group radio button label appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteRibbonText RibbonGroupRadioButtonText => _ribbonGroupRadioButtonText;

    private bool ShouldSerializeRibbonGroupRadioButtonText() => !_ribbonGroupRadioButtonText.IsDefault;

    #endregion
}
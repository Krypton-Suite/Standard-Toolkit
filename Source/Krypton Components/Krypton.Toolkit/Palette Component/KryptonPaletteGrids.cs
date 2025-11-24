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
/// Storage for grids palette settings.
/// </summary>
public class KryptonPaletteGrids : Storage
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonPaletteGrids class.
    /// </summary>
    /// <param name="redirector">Palette redirector for sourcing inherited values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public KryptonPaletteGrids([DisallowNull] PaletteRedirect redirector,
        NeedPaintHandler needPaint)
    {
        Debug.Assert(redirector! != null);

        GridCommon = new KryptonPaletteGrid(redirector!, GridStyle.List, needPaint);
        GridList = new KryptonPaletteGrid(redirector!, GridStyle.List, needPaint);
        GridSheet = new KryptonPaletteGrid(redirector!, GridStyle.Sheet, needPaint);
        GridCustom1 = new KryptonPaletteGrid(redirector!, GridStyle.Custom1, needPaint);
        GridCustom2 = new KryptonPaletteGrid(redirector!, GridStyle.Custom3, needPaint);
        GridCustom3 = new KryptonPaletteGrid(redirector!, GridStyle.Custom3, needPaint);

        // Create redirectors for inheriting from style specific to style common
        var redirectCommon = new PaletteRedirectGrids(redirector!, GridCommon);

        // Ensure the specific styles inherit to the common grid style
        GridList.SetRedirector(redirectCommon);
        GridSheet.SetRedirector(redirectCommon);
        GridCustom1.SetRedirector(redirectCommon);
        GridCustom2.SetRedirector(redirectCommon);
        GridCustom3.SetRedirector(redirectCommon);
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => GridCommon.IsDefault &&
                                      GridList.IsDefault &&
                                      GridSheet.IsDefault 
                                      &&GridCustom1.IsDefault
                                      && GridCustom2.IsDefault 
                                      && GridCustom3.IsDefault;

    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    /// <param name="common">Reference to common settings.</param>
    public void PopulateFromBase(KryptonPaletteCommon common)
    {
        // Populate only the designated styles
        GridList.PopulateFromBase(common, GridStyle.List);
        GridSheet.PopulateFromBase(common, GridStyle.Sheet);
    }
    #endregion

    #region GridCommon
    /// <summary>
    /// Gets access to the common grid appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining common grid appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteGrid GridCommon { get; }

    private bool ShouldSerializeGridCommon() => !GridCommon.IsDefault;

    #endregion

    #region GridList
    /// <summary>
    /// Gets access to the list grid appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining list grid appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteGrid GridList { get; }

    private bool ShouldSerializeGridList() => !GridList.IsDefault;

    #endregion

    #region GridSheet
    /// <summary>
    /// Gets access to the sheet grid appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining sheet grid appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteGrid GridSheet { get; }

    private bool ShouldSerializeGridSheet() => !GridSheet.IsDefault;

    #endregion

    #region GridCustom1
    /// <summary>
    /// Gets access to the first custom grid appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining the first custom grid appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteGrid GridCustom1 { get; }

    private bool ShouldSerializeGridCustom1() => !GridCustom1.IsDefault;

    #endregion

    #region GridCustom2
    /// <summary>
    /// Gets access to the first custom grid appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining the first custom grid appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteGrid GridCustom2 { get; }

    private bool ShouldSerializeGridCustom2() => !GridCustom2.IsDefault;

    #endregion

    #region GridCustom3
    /// <summary>
    /// Gets access to the first custom grid appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining the third custom grid appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteGrid GridCustom3 { get; }

    private bool ShouldSerializeGridCustom3() => !GridCustom3.IsDefault;

    #endregion
}
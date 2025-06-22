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
/// Implement storage for a KryptonDataGridView tracking/pressed/selected states.
/// </summary>
public class PaletteDataGridViewCells : Storage
{
    #region Instance Fields
    private readonly PaletteDataGridViewTripleStates _dataCell;
    private readonly PaletteDataGridViewTripleStates _headerColumn;
    private readonly PaletteDataGridViewTripleStates _headerRow;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteDataGridViewCells class.
    /// </summary>
    /// <param name="inherit">Source for inheriting values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteDataGridViewCells([DisallowNull] PaletteDataGridViewRedirect inherit,
        NeedPaintHandler needPaint)
    {
        Debug.Assert(inherit != null);

        // Store the provided paint notification delegate
        NeedPaint = needPaint;

        // Create storage that maps onto the inherit instances
        _dataCell = new PaletteDataGridViewTripleStates(inherit!.DataCell, needPaint);
        _headerColumn = new PaletteDataGridViewTripleStates(inherit.HeaderColumn, needPaint);
        _headerRow = new PaletteDataGridViewTripleStates(inherit.HeaderRow, needPaint);
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => DataCell.IsDefault &&
                                      HeaderColumn.IsDefault &&
                                      HeaderRow.IsDefault;

    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    /// <param name="state">The palette state to populate with.</param>
    /// <param name="common">Reference to common settings.</param>
    /// <param name="gridStyle">Grid style to use for populating.</param>
    public virtual void PopulateFromBase(KryptonPaletteCommon common,
        PaletteState state,
        GridStyle gridStyle)
    {
        if (gridStyle == GridStyle.List)
        {
            common.StateCommon.SetStyles(PaletteBackStyle.GridDataCellList, PaletteBorderStyle.GridDataCellList, PaletteContentStyle.GridDataCellList);
        }
        else
        {
            common.StateCommon.SetStyles(PaletteBackStyle.GridDataCellSheet, PaletteBorderStyle.GridDataCellSheet, PaletteContentStyle.GridDataCellSheet);
        }

        _dataCell.PopulateFromBase(state);

        if (gridStyle == GridStyle.List)
        {
            common.StateCommon.SetStyles(PaletteBackStyle.GridHeaderColumnList, PaletteBorderStyle.GridHeaderColumnList, PaletteContentStyle.GridHeaderColumnList);
        }
        else
        {
            common.StateCommon.SetStyles(PaletteBackStyle.GridHeaderColumnSheet, PaletteBorderStyle.GridHeaderColumnSheet, PaletteContentStyle.GridHeaderColumnSheet);
        }

        _headerColumn.PopulateFromBase(state);

        if (gridStyle == GridStyle.List)
        {
            common.StateCommon.SetStyles(PaletteBackStyle.GridHeaderRowList, PaletteBorderStyle.GridHeaderRowList, PaletteContentStyle.GridHeaderRowList);
        }
        else
        {
            common.StateCommon.SetStyles(PaletteBackStyle.GridHeaderRowSheet, PaletteBorderStyle.GridHeaderRowSheet, PaletteContentStyle.GridHeaderRowSheet);
        }

        _headerRow.PopulateFromBase(state);
    }
    #endregion

    #region SetInherit
    /// <summary>
    /// Sets the inheritance parent.
    /// </summary>
    public virtual void SetInherit(PaletteDataGridViewRedirect inherit)
    {
        _dataCell.SetInherit(inherit.DataCell);
        _headerColumn.SetInherit(inherit.HeaderColumn);
        _headerRow.SetInherit(inherit.HeaderRow);
    }
    #endregion

    #region DataCell
    /// <summary>
    /// Gets access to the data cell palette details.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining data cell appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteDataGridViewTripleStates DataCell => _dataCell;

    private bool ShouldSerializeDataCell() => !_dataCell.IsDefault;

    #endregion

    #region HeaderColumn
    /// <summary>
    /// Gets access to the header column cell palette details.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining header column cell appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteDataGridViewTripleStates HeaderColumn => _headerColumn;

    private bool ShouldSerializeHeaderColumn() => !_headerColumn.IsDefault;

    #endregion

    #region HeaderRow
    /// <summary>
    /// Gets access to the header row cell palette details.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining header row cell appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteDataGridViewTripleStates HeaderRow => _headerRow;

    private bool ShouldSerializeHeaderRow() => !_headerRow.IsDefault;

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
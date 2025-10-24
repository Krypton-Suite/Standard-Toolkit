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
/// Implement storage and redirection for a KryptonDataGridView state.
/// </summary>
public class PaletteDataGridViewRedirect : Storage
{
    #region Instance Fields
    private readonly PaletteDoubleRedirect _background;
    private readonly PaletteDataGridViewTripleRedirect _dataCell;
    private readonly PaletteDataGridViewTripleRedirect _headerColumn;
    private readonly PaletteDataGridViewTripleRedirect _headerRow;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteDataGridViewRedirect class.
    /// </summary>
    /// <param name="redirect">Source for inheriting values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteDataGridViewRedirect([DisallowNull] PaletteRedirect redirect,
        NeedPaintHandler? needPaint)
    {
        Debug.Assert(redirect != null);

        // Store the provided paint notification delegate
        NeedPaint = needPaint;

        // Create storage that maps onto the inherit instances
        _background = new PaletteDoubleRedirect(redirect!, PaletteBackStyle.GridBackgroundList, PaletteBorderStyle.GridDataCellList, needPaint!);
        _dataCell = new PaletteDataGridViewTripleRedirect(redirect!, PaletteBackStyle.GridDataCellList, PaletteBorderStyle.GridDataCellList, PaletteContentStyle.GridDataCellList, needPaint!);
        _headerColumn = new PaletteDataGridViewTripleRedirect(redirect!, PaletteBackStyle.GridHeaderColumnList, PaletteBorderStyle.GridHeaderColumnList, PaletteContentStyle.GridHeaderColumnList, needPaint!);
        _headerRow = new PaletteDataGridViewTripleRedirect(redirect!, PaletteBackStyle.GridHeaderRowList, PaletteBorderStyle.GridHeaderRowList, PaletteContentStyle.GridHeaderRowList, needPaint!);
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => Background.IsDefault &&
                                      DataCell.IsDefault &&
                                      HeaderColumn.IsDefault &&
                                      HeaderRow.IsDefault;

    #endregion

    #region SetRedirector
    /// <summary>
    /// Update the redirector with new reference.
    /// </summary>
    /// <param name="redirect">Target redirector.</param>
    public void SetRedirector(PaletteRedirect redirect)
    {
        _background.SetRedirector(redirect);
        _dataCell.SetRedirector(redirect);
        _headerColumn.SetRedirector(redirect);
        _headerRow.SetRedirector(redirect);
    }
    #endregion

    #region BackStyle
    /// <summary>
    /// Gets and sets the back style.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public PaletteBackStyle BackStyle
    {
        get => _background.BackStyle;
        set => _background.BackStyle = value;
    }
    #endregion

    #region GridStyle
    /// <summary>
    /// Update the styles of the headers and data cells.
    /// </summary>
    /// <param name="headerColumn">Style for the column headers.</param>
    /// <param name="headerRow">Style for the row headers.</param>
    /// <param name="dataCell">Style for the data cells.</param>
    public void SetGridStyles(GridStyle headerColumn,
        GridStyle headerRow,
        GridStyle dataCell)
    {
        switch (headerColumn)
        {
            case GridStyle.List:
                _headerColumn.SetStyles(PaletteBackStyle.GridHeaderColumnList, PaletteBorderStyle.GridHeaderColumnList, PaletteContentStyle.GridHeaderColumnList);
                break;
            case GridStyle.Sheet:
                _headerColumn.SetStyles(PaletteBackStyle.GridHeaderColumnSheet, PaletteBorderStyle.GridHeaderColumnSheet, PaletteContentStyle.GridHeaderColumnSheet);
                break;
            case GridStyle.Custom1:
                _headerColumn.SetStyles(PaletteBackStyle.GridHeaderColumnCustom1, PaletteBorderStyle.GridHeaderColumnCustom1, PaletteContentStyle.GridHeaderColumnCustom1);
                break;
            case GridStyle.Custom2:
                _headerColumn.SetStyles(PaletteBackStyle.GridHeaderColumnCustom2, PaletteBorderStyle.GridHeaderColumnCustom2, PaletteContentStyle.GridHeaderColumnCustom2);
                break;
            case GridStyle.Custom3:
                _headerColumn.SetStyles(PaletteBackStyle.GridHeaderColumnCustom3, PaletteBorderStyle.GridHeaderColumnCustom3, PaletteContentStyle.GridHeaderColumnCustom3);
                break;
        }

        switch (headerRow)
        {
            case GridStyle.List:
                _headerRow.SetStyles(PaletteBackStyle.GridHeaderRowList, PaletteBorderStyle.GridHeaderRowList, PaletteContentStyle.GridHeaderRowList);
                break;
            case GridStyle.Sheet:
                _headerRow.SetStyles(PaletteBackStyle.GridHeaderRowSheet, PaletteBorderStyle.GridHeaderRowSheet, PaletteContentStyle.GridHeaderRowSheet);
                break;
            case GridStyle.Custom1:
                _headerRow.SetStyles(PaletteBackStyle.GridHeaderRowCustom1, PaletteBorderStyle.GridHeaderRowCustom1, PaletteContentStyle.GridHeaderRowCustom1);
                break;
            case GridStyle.Custom2:
                _headerRow.SetStyles(PaletteBackStyle.GridHeaderRowCustom2, PaletteBorderStyle.GridHeaderRowCustom2, PaletteContentStyle.GridHeaderRowCustom2);
                break;
            case GridStyle.Custom3:
                _headerRow.SetStyles(PaletteBackStyle.GridHeaderRowCustom3, PaletteBorderStyle.GridHeaderRowCustom3, PaletteContentStyle.GridHeaderRowCustom3);
                break;
        }

        switch (dataCell)
        {
            case GridStyle.List:
                _dataCell.SetStyles(PaletteBackStyle.GridDataCellList, PaletteBorderStyle.GridDataCellList, PaletteContentStyle.GridDataCellList);
                break;
            case GridStyle.Sheet:
                _dataCell.SetStyles(PaletteBackStyle.GridDataCellSheet, PaletteBorderStyle.GridDataCellSheet, PaletteContentStyle.GridDataCellSheet);
                break;
            case GridStyle.Custom1:
                _dataCell.SetStyles(PaletteBackStyle.GridDataCellCustom1, PaletteBorderStyle.GridDataCellCustom1, PaletteContentStyle.GridDataCellCustom1);
                break;
            case GridStyle.Custom2:
                _dataCell.SetStyles(PaletteBackStyle.GridDataCellCustom2, PaletteBorderStyle.GridDataCellCustom2, PaletteContentStyle.GridDataCellCustom2);
                break;
            case GridStyle.Custom3:
                _dataCell.SetStyles(PaletteBackStyle.GridDataCellCustom3, PaletteBorderStyle.GridDataCellCustom3, PaletteContentStyle.GridDataCellCustom3);
                break;
        }
    }
    #endregion

    #region Background
    /// <summary>
    /// Gets access to the data grid view background palette details.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining data grid view background appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteBack Background => _background.Back;

    private bool ShouldSerializeBackground() => !_background.IsDefault;

    internal IPaletteDouble BackgroundDouble => _background;

    #endregion

    #region DataCell
    /// <summary>
    /// Gets access to the data cell palette details.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining data cell appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteDataGridViewTripleRedirect DataCell => _dataCell;

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
    public virtual PaletteDataGridViewTripleRedirect HeaderColumn => _headerColumn;

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
    public virtual PaletteDataGridViewTripleRedirect HeaderRow => _headerRow;

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
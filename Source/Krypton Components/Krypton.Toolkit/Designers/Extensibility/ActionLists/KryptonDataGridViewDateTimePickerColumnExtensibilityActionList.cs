#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, tobitege, Lesandro & KamaniAR et al. 2025 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Action list for the KryptonDataGridViewDateTimePickerColumn control using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonDataGridViewDateTimePickerColumnExtensibilityActionList : KryptonExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonDataGridViewDateTimePickerColumn? _dateTimePickerColumn;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonDataGridViewDateTimePickerColumnExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonDataGridViewDateTimePickerColumnExtensibilityActionList(ComponentDesigner owner)
        : base(owner)
    {
        _dateTimePickerColumn = (KryptonDataGridViewDateTimePickerColumn?)owner.Component;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets and sets the column header text.
    /// </summary>
    public string HeaderText
    {
        get => _dateTimePickerColumn?.HeaderText ?? string.Empty;
        set => SetPropertyValue(_dateTimePickerColumn!, nameof(HeaderText), HeaderText, value, v => _dateTimePickerColumn!.HeaderText = v);
    }

    /// <summary>
    /// Gets and sets the column name.
    /// </summary>
    public string Name
    {
        get => _dateTimePickerColumn?.Name ?? string.Empty;
        set => SetPropertyValue(_dateTimePickerColumn!, nameof(Name), Name, value, v => _dateTimePickerColumn!.Name = v);
    }

    /// <summary>
    /// Gets and sets the column width.
    /// </summary>
    public int Width
    {
        get => _dateTimePickerColumn?.Width ?? 100;
        set => SetPropertyValue(_dateTimePickerColumn!, nameof(Width), Width, value, v => _dateTimePickerColumn!.Width = v);
    }

    /// <summary>
    /// Gets and sets the column minimum width.
    /// </summary>
    public int MinimumWidth
    {
        get => _dateTimePickerColumn?.MinimumWidth ?? 5;
        set => SetPropertyValue(_dateTimePickerColumn!, nameof(MinimumWidth), MinimumWidth, value, v => _dateTimePickerColumn!.MinimumWidth = v);
    }

    /// <summary>
    /// Gets and sets whether the column is visible.
    /// </summary>
    public bool Visible
    {
        get => _dateTimePickerColumn?.Visible ?? true;
        set => SetPropertyValue(_dateTimePickerColumn!, nameof(Visible), Visible, value, v => _dateTimePickerColumn!.Visible = v);
    }

    /// <summary>
    /// Gets and sets whether the column is read-only.
    /// </summary>
    public bool ReadOnly
    {
        get => _dateTimePickerColumn?.ReadOnly ?? false;
        set => SetPropertyValue(_dateTimePickerColumn!, nameof(ReadOnly), ReadOnly, value, v => _dateTimePickerColumn!.ReadOnly = v);
    }

    /// <summary>
    /// Gets and sets whether the column is frozen.
    /// </summary>
    public bool Frozen
    {
        get => _dateTimePickerColumn?.Frozen ?? false;
        set => SetPropertyValue(_dateTimePickerColumn!, nameof(Frozen), Frozen, value, v => _dateTimePickerColumn!.Frozen = v);
    }

    /// <summary>
    /// Gets and sets the column sort mode.
    /// </summary>
    public DataGridViewColumnSortMode SortMode
    {
        get => _dateTimePickerColumn?.SortMode ?? DataGridViewColumnSortMode.Automatic;
        set => SetPropertyValue(_dateTimePickerColumn!, nameof(SortMode), SortMode, value, v => _dateTimePickerColumn!.SortMode = v);
    }
    #endregion

    #region Public Override
    /// <summary>
    /// Returns the collection of DesignerActionItem objects contained in the list.
    /// </summary>
    /// <returns>A DesignerActionItem array that contains the items in this list.</returns>
    public override DesignerActionItemCollection GetSortedActionItems()
    {
        var actions = new DesignerActionItemCollection();

        // This can be null when deleting a control instance at design time
        if (_dateTimePickerColumn != null)
        {
            // Add the list of DataGridViewDateTimePickerColumn specific actions
            actions.Add(new DesignerActionHeaderItem(@"Appearance"));
            actions.Add(new DesignerActionPropertyItem(nameof(HeaderText), @"Header Text", @"Appearance", @"Column header text"));
            actions.Add(new DesignerActionPropertyItem(nameof(Name), @"Name", @"Appearance", @"Column name"));
            actions.Add(new DesignerActionPropertyItem(nameof(Width), @"Width", @"Appearance", @"Column width"));
            actions.Add(new DesignerActionPropertyItem(nameof(MinimumWidth), @"Minimum Width", @"Appearance", @"Column minimum width"));
            actions.Add(new DesignerActionHeaderItem(@"Behavior"));
            actions.Add(new DesignerActionPropertyItem(nameof(Visible), @"Visible", @"Behavior", @"Column visible"));
            actions.Add(new DesignerActionPropertyItem(nameof(ReadOnly), @"Read Only", @"Behavior", @"Column read-only"));
            actions.Add(new DesignerActionPropertyItem(nameof(Frozen), @"Frozen", @"Behavior", @"Column frozen"));
            actions.Add(new DesignerActionPropertyItem(nameof(SortMode), @"Sort Mode", @"Behavior", @"Column sort mode"));
        }

        return actions;
    }
    #endregion
}

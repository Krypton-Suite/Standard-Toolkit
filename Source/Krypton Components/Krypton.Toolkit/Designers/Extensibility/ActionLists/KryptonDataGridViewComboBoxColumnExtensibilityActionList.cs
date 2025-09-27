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
/// Action list for the KryptonDataGridViewComboBoxColumn control using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonDataGridViewComboBoxColumnExtensibilityActionList : KryptonExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonDataGridViewComboBoxColumn? _comboBoxColumn;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonDataGridViewComboBoxColumnExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonDataGridViewComboBoxColumnExtensibilityActionList(ComponentDesigner owner)
        : base(owner)
    {
        _comboBoxColumn = (KryptonDataGridViewComboBoxColumn?)owner.Component;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets and sets the column header text.
    /// </summary>
    public string HeaderText
    {
        get => _comboBoxColumn?.HeaderText ?? string.Empty;
        set => SetPropertyValue(_comboBoxColumn!, nameof(HeaderText), HeaderText, value, v => _comboBoxColumn!.HeaderText = v);
    }

    /// <summary>
    /// Gets and sets the column name.
    /// </summary>
    public string Name
    {
        get => _comboBoxColumn?.Name ?? string.Empty;
        set => SetPropertyValue(_comboBoxColumn!, nameof(Name), Name, value, v => _comboBoxColumn!.Name = v);
    }

    /// <summary>
    /// Gets and sets the column width.
    /// </summary>
    public int Width
    {
        get => _comboBoxColumn?.Width ?? 100;
        set => SetPropertyValue(_comboBoxColumn!, nameof(Width), Width, value, v => _comboBoxColumn!.Width = v);
    }

    /// <summary>
    /// Gets and sets the column minimum width.
    /// </summary>
    public int MinimumWidth
    {
        get => _comboBoxColumn?.MinimumWidth ?? 5;
        set => SetPropertyValue(_comboBoxColumn!, nameof(MinimumWidth), MinimumWidth, value, v => _comboBoxColumn!.MinimumWidth = v);
    }

    /// <summary>
    /// Gets and sets whether the column is visible.
    /// </summary>
    public bool Visible
    {
        get => _comboBoxColumn?.Visible ?? true;
        set => SetPropertyValue(_comboBoxColumn!, nameof(Visible), Visible, value, v => _comboBoxColumn!.Visible = v);
    }

    /// <summary>
    /// Gets and sets whether the column is read-only.
    /// </summary>
    public bool ReadOnly
    {
        get => _comboBoxColumn?.ReadOnly ?? false;
        set => SetPropertyValue(_comboBoxColumn!, nameof(ReadOnly), ReadOnly, value, v => _comboBoxColumn!.ReadOnly = v);
    }

    /// <summary>
    /// Gets and sets whether the column is frozen.
    /// </summary>
    public bool Frozen
    {
        get => _comboBoxColumn?.Frozen ?? false;
        set => SetPropertyValue(_comboBoxColumn!, nameof(Frozen), Frozen, value, v => _comboBoxColumn!.Frozen = v);
    }

    /// <summary>
    /// Gets and sets the column sort mode.
    /// </summary>
    public DataGridViewColumnSortMode SortMode
    {
        get => _comboBoxColumn?.SortMode ?? DataGridViewColumnSortMode.Automatic;
        set => SetPropertyValue(_comboBoxColumn!, nameof(SortMode), SortMode, value, v => _comboBoxColumn!.SortMode = v);
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
        if (_comboBoxColumn != null)
        {
            // Add the list of DataGridViewComboBoxColumn specific actions
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

#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Smart-tag action list for <see cref="KryptonMultiColumnComboBox"/>.
/// </summary>
internal class KryptonMultiColumnComboBoxActionList : DesignerActionList
{
    #region Instance Fields

    private readonly KryptonMultiColumnComboBox _control;
    private readonly IComponentChangeService? _service;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonMultiColumnComboBoxActionList"/> class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonMultiColumnComboBoxActionList(KryptonMultiColumnComboBoxDesigner owner)
        : base(owner.Component)
    {
        _control = (owner.Component as KryptonMultiColumnComboBox)!;
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }

    #endregion

    #region Smart-Tag Properties

    public object? DataSource
    {
        get => _control.DataSource;
        set
        {
            if (!ReferenceEquals(_control.DataSource, value))
            {
                _service?.OnComponentChanged(_control, null, _control.DataSource, value);
                _control.DataSource = value;
            }
        }
    }

    public string DisplayMember
    {
        get => _control.DisplayMember;
        set
        {
            string normalized = value ?? string.Empty;
            if (_control.DisplayMember != normalized)
            {
                _service?.OnComponentChanged(_control, null, _control.DisplayMember, normalized);
                _control.DisplayMember = normalized;
            }
        }
    }

    public string ValueMember
    {
        get => _control.ValueMember;
        set
        {
            string normalized = value ?? string.Empty;
            if (_control.ValueMember != normalized)
            {
                _service?.OnComponentChanged(_control, null, _control.ValueMember, normalized);
                _control.ValueMember = normalized;
            }
        }
    }

    public bool AutoGenerateColumns
    {
        get => _control.AutoGenerateColumns;
        set
        {
            if (_control.AutoGenerateColumns != value)
            {
                _service?.OnComponentChanged(_control, null, _control.AutoGenerateColumns, value);
                _control.AutoGenerateColumns = value;
            }
        }
    }

    public bool ColumnHeadersVisible
    {
        get => _control.ColumnHeadersVisible;
        set
        {
            if (_control.ColumnHeadersVisible != value)
            {
                _service?.OnComponentChanged(_control, null, _control.ColumnHeadersVisible, value);
                _control.ColumnHeadersVisible = value;
            }
        }
    }

    public bool CommitOnRowClick
    {
        get => _control.CommitOnRowClick;
        set
        {
            if (_control.CommitOnRowClick != value)
            {
                _service?.OnComponentChanged(_control, null, _control.CommitOnRowClick, value);
                _control.CommitOnRowClick = value;
            }
        }
    }

    public LeftRightAlignment DropDownAlign
    {
        get => _control.DropDownAlign;
        set
        {
            if (_control.DropDownAlign != value)
            {
                _service?.OnComponentChanged(_control, null, _control.DropDownAlign, value);
                _control.DropDownAlign = value;
            }
        }
    }

    public int DropDownWidth
    {
        get => _control.DropDownWidth;
        set
        {
            if (_control.DropDownWidth != value)
            {
                _service?.OnComponentChanged(_control, null, _control.DropDownWidth, value);
                _control.DropDownWidth = value;
            }
        }
    }

    public int DropDownHeight
    {
        get => _control.DropDownHeight;
        set
        {
            if (_control.DropDownHeight != value)
            {
                _service?.OnComponentChanged(_control, null, _control.DropDownHeight, value);
                _control.DropDownHeight = value;
            }
        }
    }

    public bool DropDownResizable
    {
        get => _control.DropDownResizable;
        set
        {
            if (_control.DropDownResizable != value)
            {
                _service?.OnComponentChanged(_control, null, _control.DropDownResizable, value);
                _control.DropDownResizable = value;
            }
        }
    }

    public bool AutoOpenOnType
    {
        get => _control.AutoOpenOnType;
        set
        {
            if (_control.AutoOpenOnType != value)
            {
                _service?.OnComponentChanged(_control, null, _control.AutoOpenOnType, value);
                _control.AutoOpenOnType = value;
            }
        }
    }

    public bool ReadOnlyEditor
    {
        get => _control.ReadOnlyEditor;
        set
        {
            if (_control.ReadOnlyEditor != value)
            {
                _service?.OnComponentChanged(_control, null, _control.ReadOnlyEditor, value);
                _control.ReadOnlyEditor = value;
            }
        }
    }

    public InputControlStyle InputControlStyle
    {
        get => _control.InputControlStyle;
        set
        {
            if (_control.InputControlStyle != value)
            {
                _service?.OnComponentChanged(_control, null, _control.InputControlStyle, value);
                _control.InputControlStyle = value;
            }
        }
    }

    public PaletteMode PaletteMode
    {
        get => _control.PaletteMode;
        set
        {
            if (_control.PaletteMode != value)
            {
                _service?.OnComponentChanged(_control, null, _control.PaletteMode, value);
                _control.PaletteMode = value;
            }
        }
    }

    #endregion

    #region Public Override

    /// <summary>
    /// Returns the collection of <see cref="DesignerActionItem"/> objects contained in the list.
    /// </summary>
    public override DesignerActionItemCollection GetSortedActionItems()
    {
        DesignerActionItemCollection actions = new DesignerActionItemCollection();

        actions.Add(new DesignerActionHeaderItem("Data"));
        actions.Add(new DesignerActionPropertyItem(nameof(DataSource), "Data source", "Data",
            "List or binding source for rows in the drop-down grid."));
        actions.Add(new DesignerActionPropertyItem(nameof(DisplayMember), "Display member", "Data",
            "Property shown in the editor and used for type-ahead filtering."));
        actions.Add(new DesignerActionPropertyItem(nameof(ValueMember), "Value member", "Data",
            "Property used as the logical SelectedValue."));
        actions.Add(new DesignerActionPropertyItem(nameof(AutoGenerateColumns), "Auto-generate columns", "Data",
            "When true and Columns is empty, generate drop-down columns from the data source."));
        actions.Add(new DesignerActionPropertyItem(nameof(ColumnHeadersVisible), "Column headers", "Data",
            "Whether column headers are shown in the drop-down."));
        actions.Add(new DesignerActionPropertyItem(nameof(CommitOnRowClick), "Commit on row click", "Data",
            "When true, a left-click on a data row commits the selection."));

        actions.Add(new DesignerActionHeaderItem("DropDown"));
        actions.Add(new DesignerActionPropertyItem(nameof(DropDownAlign), "Alignment", "DropDown",
            "Horizontal alignment of the drop-down relative to the editor."));
        actions.Add(new DesignerActionPropertyItem(nameof(DropDownWidth), "Width", "DropDown",
            "Initial width of the drop-down popup."));
        actions.Add(new DesignerActionPropertyItem(nameof(DropDownHeight), "Height", "DropDown",
            "Initial height of the drop-down popup."));
        actions.Add(new DesignerActionPropertyItem(nameof(DropDownResizable), "Resizable", "DropDown",
            "Whether the user can resize the drop-down popup at runtime."));
        actions.Add(new DesignerActionPropertyItem(nameof(ReadOnlyEditor), "Read-only editor", "DropDown",
            "When true, selection happens through the drop-down only (DropDownList style)."));
        actions.Add(new DesignerActionPropertyItem(nameof(AutoOpenOnType), "Open on type", "DropDown",
            "When true, typing in the editor opens the drop-down and filters rows."));

        actions.Add(new DesignerActionHeaderItem("Visuals"));
        actions.Add(new DesignerActionPropertyItem(nameof(InputControlStyle), "Style", "Visuals",
            "Input control style applied to the editor."));
        actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), "Palette", "Visuals",
            "Palette applied to drawing."));

        return actions;
    }

    #endregion
}

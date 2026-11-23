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
/// Smart-tag action list for <see cref="KryptonComboBoxUserControl"/>.
/// </summary>
internal class KryptonComboBoxUserControlActionList : DesignerActionList
{
    #region Instance Fields

    private readonly KryptonComboBoxUserControl _control;
    private readonly IComponentChangeService? _service;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonComboBoxUserControlActionList"/> class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonComboBoxUserControlActionList(KryptonComboBoxUserControlDesigner owner)
        : base(owner.Component)
    {
        _control = (owner.Component as KryptonComboBoxUserControl)!;
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }

    #endregion

    #region Smart-Tag Properties

    /// <summary>Gets or sets the horizontal alignment of the drop-down.</summary>
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

    /// <summary>Gets or sets the desired width of the drop-down popup.</summary>
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

    /// <summary>Gets or sets the desired height of the drop-down popup.</summary>
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

    /// <summary>Gets or sets a value indicating whether the user can resize the drop-down.</summary>
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

    /// <summary>Gets or sets a value indicating whether the editor is read-only.</summary>
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

    /// <summary>Gets or sets the input control style.</summary>
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

    /// <summary>Gets or sets the palette mode.</summary>
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

        if (_control != null)
        {
            actions.Add(new DesignerActionHeaderItem("DropDown"));
            actions.Add(new DesignerActionPropertyItem(nameof(DropDownAlign), "Alignment", "DropDown",
                "Horizontal alignment of the drop-down relative to the editor."));
            actions.Add(new DesignerActionPropertyItem(nameof(DropDownWidth), "Width", "DropDown",
                "Initial width of the drop-down popup."));
            actions.Add(new DesignerActionPropertyItem(nameof(DropDownHeight), "Height", "DropDown",
                "Initial height of the drop-down popup."));
            actions.Add(new DesignerActionPropertyItem(nameof(DropDownResizable), "Resizable", "DropDown",
                "Whether the user can resize the drop-down popup at runtime."));

            actions.Add(new DesignerActionHeaderItem("Behavior"));
            actions.Add(new DesignerActionPropertyItem(nameof(ReadOnlyEditor), "ReadOnly editor", "Behavior",
                "When true the editor cannot be typed into; selection happens through the drop-down only."));

            actions.Add(new DesignerActionHeaderItem("Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(InputControlStyle), "Style", "Visuals",
                "Input control style applied to the editor."));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), "Palette", "Visuals",
                "Palette applied to drawing."));
        }

        return actions;
    }

    #endregion
}

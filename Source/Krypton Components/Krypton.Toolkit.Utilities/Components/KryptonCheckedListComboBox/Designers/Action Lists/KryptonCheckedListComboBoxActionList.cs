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
/// Smart-tag action list for <see cref="KryptonCheckedListComboBox"/>.
/// </summary>
internal class KryptonCheckedListComboBoxActionList : DesignerActionList
{
    #region Instance Fields

    private readonly KryptonCheckedListComboBox _control;
    private readonly IComponentChangeService? _service;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonCheckedListComboBoxActionList"/> class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonCheckedListComboBoxActionList(KryptonCheckedListComboBoxDesigner owner)
        : base(owner.Component)
    {
        _control = (owner.Component as KryptonCheckedListComboBox)!;
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }

    #endregion

    #region Smart-Tag Properties

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

    public string ValueSeparator
    {
        get => _control.ValueSeparator;
        set
        {
            if (_control.ValueSeparator != value)
            {
                _service?.OnComponentChanged(_control, null, _control.ValueSeparator, value);
                _control.ValueSeparator = value;
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

        actions.Add(new DesignerActionHeaderItem("DropDown"));
        actions.Add(new DesignerActionPropertyItem(nameof(DropDownAlign), "Alignment", "DropDown",
            "Horizontal alignment of the drop-down relative to the editor."));
        actions.Add(new DesignerActionPropertyItem(nameof(DropDownWidth), "Width", "DropDown",
            "Initial width of the drop-down popup."));
        actions.Add(new DesignerActionPropertyItem(nameof(DropDownHeight), "Height", "DropDown",
            "Initial height of the drop-down popup."));
        actions.Add(new DesignerActionPropertyItem(nameof(DropDownResizable), "Resizable", "DropDown",
            "Whether the user can resize the drop-down popup at runtime."));

        actions.Add(new DesignerActionHeaderItem("List"));
        actions.Add(new DesignerActionPropertyItem(nameof(ValueSeparator), "Value separator", "List",
            "Separator between checked item texts in the editor summary."));

        actions.Add(new DesignerActionHeaderItem("Visuals"));
        actions.Add(new DesignerActionPropertyItem(nameof(InputControlStyle), "Style", "Visuals",
            "Input control style applied to the editor."));
        actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), "Palette", "Visuals",
            "Palette applied to drawing."));

        return actions;
    }

    #endregion
}

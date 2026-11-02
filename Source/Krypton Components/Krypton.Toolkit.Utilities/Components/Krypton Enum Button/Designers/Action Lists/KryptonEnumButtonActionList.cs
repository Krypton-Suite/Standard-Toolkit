#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Smart-tag action list for <see cref="KryptonEnumButton"/>. Exposes the enum-cycling behaviour
/// toggles (wrap, reverse-on-right-click, description / humanise text, sort order, keyboard / wheel
/// cycling) plus the palette mode.
/// </summary>
internal class KryptonEnumButtonActionList : DesignerActionList
{
    #region Instance Fields
    private readonly KryptonEnumButton _button;
    private readonly IComponentChangeService? _service;
    #endregion

    #region Identity
    /// <summary>Initialize a new instance of the <see cref="KryptonEnumButtonActionList"/> class.</summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonEnumButtonActionList(KryptonEnumButtonDesigner owner)
        : base(owner.Component)
    {
        _button = (owner.Component as KryptonEnumButton)!;
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }
    #endregion

    #region Public
    /// <summary>Gets and sets whether cycling wraps around at the ends.</summary>
    public bool WrapAround
    {
        get => _button.WrapAround;
        set
        {
            if (_button.WrapAround != value)
            {
                _service?.OnComponentChanged(_button, null, _button.WrapAround, value);
                _button.WrapAround = value;
            }
        }
    }

    /// <summary>Gets and sets whether right-clicking cycles backwards.</summary>
    public bool ReverseOnRightClick
    {
        get => _button.ReverseOnRightClick;
        set
        {
            if (_button.ReverseOnRightClick != value)
            {
                _service?.OnComponentChanged(_button, null, _button.ReverseOnRightClick, value);
                _button.ReverseOnRightClick = value;
            }
        }
    }

    /// <summary>Gets and sets whether the DescriptionAttribute text is used for display.</summary>
    public bool UseDescriptionAttribute
    {
        get => _button.UseDescriptionAttribute;
        set
        {
            if (_button.UseDescriptionAttribute != value)
            {
                _service?.OnComponentChanged(_button, null, _button.UseDescriptionAttribute, value);
                _button.UseDescriptionAttribute = value;
            }
        }
    }

    /// <summary>Gets and sets whether PascalCase field names are humanised.</summary>
    public bool HumanizeNames
    {
        get => _button.HumanizeNames;
        set
        {
            if (_button.HumanizeNames != value)
            {
                _service?.OnComponentChanged(_button, null, _button.HumanizeNames, value);
                _button.HumanizeNames = value;
            }
        }
    }

    /// <summary>Gets and sets the order in which enum values are cycled.</summary>
    public EnumButtonSortOrder SortOrder
    {
        get => _button.SortOrder;
        set
        {
            if (_button.SortOrder != value)
            {
                _service?.OnComponentChanged(_button, null, _button.SortOrder, value);
                _button.SortOrder = value;
            }
        }
    }

    /// <summary>Gets and sets whether the arrow keys cycle the value.</summary>
    public bool AllowKeyboardCycling
    {
        get => _button.AllowKeyboardCycling;
        set
        {
            if (_button.AllowKeyboardCycling != value)
            {
                _service?.OnComponentChanged(_button, null, _button.AllowKeyboardCycling, value);
                _button.AllowKeyboardCycling = value;
            }
        }
    }

    /// <summary>Gets and sets whether the mouse wheel cycles the value.</summary>
    public bool AllowMouseWheelCycling
    {
        get => _button.AllowMouseWheelCycling;
        set
        {
            if (_button.AllowMouseWheelCycling != value)
            {
                _service?.OnComponentChanged(_button, null, _button.AllowMouseWheelCycling, value);
                _button.AllowMouseWheelCycling = value;
            }
        }
    }

    /// <summary>Gets and sets the palette mode.</summary>
    public PaletteMode PaletteMode
    {
        get => _button.PaletteMode;
        set
        {
            if (_button.PaletteMode != value)
            {
                _service?.OnComponentChanged(_button, null, _button.PaletteMode, value);
                _button.PaletteMode = value;
            }
        }
    }
    #endregion

    #region Public Override
    /// <summary>Returns the collection of <see cref="DesignerActionItem"/> objects contained in the list.</summary>
    /// <returns>A <see cref="DesignerActionItemCollection"/> that contains the items in this list.</returns>
    public override DesignerActionItemCollection GetSortedActionItems()
    {
        DesignerActionItemCollection actions = new DesignerActionItemCollection();

        if (_button != null)
        {
            actions.Add(new DesignerActionHeaderItem(@"Behavior"));
            actions.Add(new DesignerActionPropertyItem(nameof(WrapAround), @"Wrap around", @"Behavior", @"Cycle back to the first value after the last."));
            actions.Add(new DesignerActionPropertyItem(nameof(ReverseOnRightClick), @"Reverse on right-click", @"Behavior", @"Right-click cycles backwards to the previous value."));
            actions.Add(new DesignerActionPropertyItem(nameof(SortOrder), @"Sort order", @"Behavior", @"The order in which enum values are cycled."));
            actions.Add(new DesignerActionPropertyItem(nameof(AllowKeyboardCycling), @"Keyboard cycling", @"Behavior", @"Arrow keys cycle the value while focused."));
            actions.Add(new DesignerActionPropertyItem(nameof(AllowMouseWheelCycling), @"Mouse wheel cycling", @"Behavior", @"The mouse wheel cycles the value."));
            actions.Add(new DesignerActionHeaderItem(@"Text"));
            actions.Add(new DesignerActionPropertyItem(nameof(UseDescriptionAttribute), @"Use [Description]", @"Text", @"Use the DescriptionAttribute text for each value."));
            actions.Add(new DesignerActionPropertyItem(nameof(HumanizeNames), @"Humanize names", @"Text", @"Show PascalCase field names with spaces when no description is used."));
            actions.Add(new DesignerActionHeaderItem(@"Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), @"Palette", @"Visuals", @"Palette applied to drawing."));
        }

        return actions;
    }
    #endregion
}

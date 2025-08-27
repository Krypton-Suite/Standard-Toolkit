#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal class KryptonCalcInputActionList : DesignerActionList
{
    #region Instance Fields
    private readonly KryptonCalcInput _calcButton;
    private readonly IComponentChangeService? _service;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonCalcInputActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonCalcInputActionList(KryptonCalcInputDesigner owner)
        : base(owner.Component)
    {
        _calcButton = (owner.Component as KryptonCalcInput)!;
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }
    #endregion

    #region Public
    public InputControlStyle InputControlStyle
    {
        get => _calcButton.InputControlStyle;

        set
        {
            if (_calcButton.InputControlStyle != value)
            {
                _service?.OnComponentChanged(_calcButton, null, _calcButton.InputControlStyle, value);
                _calcButton.InputControlStyle = value;
            }
        }
    }

    public decimal Value
    {
        get => _calcButton.Value;

        set
        {
            if (_calcButton.Value != value)
            {
                _service?.OnComponentChanged(_calcButton, null, _calcButton.Value, value);
                _calcButton.Value = value;
            }
        }
    }

    public int DecimalPlaces
    {
        get => _calcButton.DecimalPlaces;

        set
        {
            if (_calcButton.DecimalPlaces != value)
            {
                _service?.OnComponentChanged(_calcButton, null, _calcButton.DecimalPlaces, value);
                _calcButton.DecimalPlaces = value;
            }
        }
    }

    public bool AllowDecimals
    {
        get => _calcButton.AllowDecimals;

        set
        {
            if (_calcButton.AllowDecimals != value)
            {
                _service?.OnComponentChanged(_calcButton, null, _calcButton.AllowDecimals, value);
                _calcButton.AllowDecimals = value;
            }
        }
    }

    public int DropDownWidth
    {
        get => _calcButton.DropDownWidth;

        set
        {
            if (_calcButton.DropDownWidth != value)
            {
                _service?.OnComponentChanged(_calcButton, null, _calcButton.DropDownWidth, value);
                _calcButton.DropDownWidth = value;
            }
        }
    }

    public VisualOrientation PopupSide
    {
        get => _calcButton.PopupSide;

        set
        {
            if (_calcButton.PopupSide != value)
            {
                _service?.OnComponentChanged(_calcButton, null, _calcButton.PopupSide, value);
                _calcButton.PopupSide = value;
            }
        }
    }

    public string Text
    {
        get => _calcButton.Text;

        set
        {
            if (_calcButton.Text != value)
            {
                _service?.OnComponentChanged(_calcButton, null, _calcButton.Text, value);
                _calcButton.Text = value;
            }
        }
    }

    public PaletteMode PaletteMode
    {
        get => _calcButton.PaletteMode;

        set
        {
            if (_calcButton.PaletteMode != value)
            {
                _service?.OnComponentChanged(_calcButton, null, _calcButton.PaletteMode, value);
                _calcButton.PaletteMode = value;
            }
        }
    }
    #endregion

    #region Public Override
    public override DesignerActionItemCollection GetSortedActionItems()
    {
        var actions = new DesignerActionItemCollection();

        if (_calcButton != null)
        {
            actions.Add(new DesignerActionHeaderItem(nameof(Appearance)));
            actions.Add(new DesignerActionPropertyItem(nameof(InputControlStyle), nameof(InputControlStyle), nameof(Appearance), @"Input control style"));
            actions.Add(new DesignerActionPropertyItem(nameof(DecimalPlaces), nameof(DecimalPlaces), nameof(Appearance), @"Number of decimal places"));
            actions.Add(new DesignerActionPropertyItem(nameof(AllowDecimals), nameof(AllowDecimals), nameof(Appearance), @"Allow decimal values"));
            actions.Add(new DesignerActionHeaderItem(@"Layout"));
            actions.Add(new DesignerActionPropertyItem(nameof(DropDownWidth), nameof(DropDownWidth), @"Layout", @"Default width of popup"));
            actions.Add(new DesignerActionPropertyItem(nameof(PopupSide), nameof(PopupSide), @"Layout", @"Side where popup appears"));
            actions.Add(new DesignerActionHeaderItem(@"Values"));
            actions.Add(new DesignerActionPropertyItem(nameof(Text), nameof(Text), @"Values", @"Input text"));
            actions.Add(new DesignerActionPropertyItem(nameof(Value), nameof(Value), @"Values", @"Numeric value"));
            actions.Add(new DesignerActionHeaderItem(@"Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), @"Palette", @"Visuals", @"Palette applied to drawing"));
        }

        return actions;
    }
    #endregion
}

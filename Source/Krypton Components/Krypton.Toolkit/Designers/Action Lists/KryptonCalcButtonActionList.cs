#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal class KryptonCalcButtonActionList : DesignerActionList
{
    #region Instance Fields
    private readonly KryptonCalcButton _calcButton;
    private readonly IComponentChangeService? _service;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonCalcButtonActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonCalcButtonActionList(KryptonCalcButtonDesigner owner)
        : base(owner.Component)
    {
        _calcButton = (owner.Component as KryptonCalcButton)!;
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }
    #endregion

    #region Public
    public ButtonStyle ButtonStyle
    {
        get => _calcButton.ButtonStyle;

        set
        {
            if (_calcButton.ButtonStyle != value)
            {
                _service?.OnComponentChanged(_calcButton, null, _calcButton.ButtonStyle, value);
                _calcButton.ButtonStyle = value;
            }
        }
    }

    public VisualOrientation ButtonOrientation
    {
        get => _calcButton.ButtonOrientation;

        set
        {
            if (_calcButton.ButtonOrientation != value)
            {
                _service?.OnComponentChanged(_calcButton, null, _calcButton.ButtonOrientation, value);
                _calcButton.ButtonOrientation = value;
            }
        }
    }

    public VisualOrientation DropDownPosition
    {
        get => _calcButton.DropDownPosition;

        set
        {
            if (_calcButton.DropDownPosition != value)
            {
                _service?.OnComponentChanged(_calcButton, null, _calcButton.DropDownPosition, value);
                _calcButton.DropDownPosition = value;
            }
        }
    }

    public VisualOrientation DropDownOrientation
    {
        get => _calcButton.DropDownOrientation;

        set
        {
            if (_calcButton.DropDownOrientation != value)
            {
                _service?.OnComponentChanged(_calcButton, null, _calcButton.DropDownOrientation, value);
                _calcButton.DropDownOrientation = value;
            }
        }
    }

    public bool Splitter
    {
        get => _calcButton.Splitter;

        set
        {
            if (_calcButton.Splitter != value)
            {
                _service?.OnComponentChanged(_calcButton, null, _calcButton.Splitter, value);
                _calcButton.Splitter = value;
            }
        }
    }

    public string Text
    {
        get => _calcButton.Values.Text;

        set
        {
            if (_calcButton.Values.Text != value)
            {
                _service?.OnComponentChanged(_calcButton, null, _calcButton.Values.Text, value);
                _calcButton.Values.Text = value;
            }
        }
    }

    public string ExtraText
    {
        get => _calcButton.Values.ExtraText;

        set
        {
            if (_calcButton.Values.ExtraText != value)
            {
                _service?.OnComponentChanged(_calcButton, null, _calcButton.Values.ExtraText, value);
                _calcButton.Values.ExtraText = value;
            }
        }
    }

    public Image? Image
    {
        get => _calcButton.Values.Image;

        set
        {
            if (_calcButton.Values.Image != value)
            {
                _service?.OnComponentChanged(_calcButton, null, _calcButton.Values.Image, value);
                _calcButton.Values.Image = value;
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
    #endregion

    #region Public Override
    public override DesignerActionItemCollection GetSortedActionItems()
    {
        var actions = new DesignerActionItemCollection();

        if (_calcButton != null)
        {
            actions.Add(new DesignerActionHeaderItem(nameof(Appearance)));
            actions.Add(new DesignerActionPropertyItem(nameof(Splitter), nameof(Splitter), nameof(Appearance), @"Splitter of DropDown"));
            actions.Add(new DesignerActionPropertyItem(nameof(ButtonStyle), nameof(ButtonStyle), nameof(Appearance), @"Button style"));
            actions.Add(new DesignerActionPropertyItem(nameof(ButtonOrientation), nameof(ButtonOrientation), nameof(Appearance), @"Button orientation"));
            actions.Add(new DesignerActionPropertyItem(nameof(DropDownPosition), nameof(DropDownPosition), nameof(Appearance), @"DropDown position"));
            actions.Add(new DesignerActionPropertyItem(nameof(DropDownOrientation), nameof(DropDownOrientation), nameof(Appearance), @"DropDown orientation"));
            actions.Add(new DesignerActionHeaderItem(@"Values"));
            actions.Add(new DesignerActionPropertyItem(nameof(Text), nameof(Text), @"Values", @"Button text"));
            actions.Add(new DesignerActionPropertyItem(nameof(ExtraText), nameof(ExtraText), @"Values", @"Button extra text"));
            actions.Add(new DesignerActionPropertyItem(nameof(Image), nameof(Image), @"Values", @"Button image"));
            actions.Add(new DesignerActionHeaderItem(@"Behavior"));
            actions.Add(new DesignerActionPropertyItem(nameof(Value), nameof(Value), @"Behavior", @"Numeric value"));
            actions.Add(new DesignerActionHeaderItem(@"Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), @"Palette", @"Visuals", @"Palette applied to drawing"));
        }

        return actions;
    }
    #endregion
}

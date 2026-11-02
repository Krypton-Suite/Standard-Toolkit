#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

internal class KryptonCheckBoxExtendedActionList : DesignerActionList
{
    #region Instance Fields

    private readonly KryptonCheckBoxExtended _checkBox;
    private readonly IComponentChangeService? _service;

    #endregion

    #region Identity

    public KryptonCheckBoxExtendedActionList(KryptonCheckBoxExtendedDesigner owner)
        : base(owner.Component)
    {
        _checkBox = (owner.Component as KryptonCheckBoxExtended)!;
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }

    #endregion

    #region Public

    public bool Checked
    {
        get => _checkBox.Checked;
        set
        {
            if (_checkBox.Checked != value)
            {
                _service?.OnComponentChanged(_checkBox, null, _checkBox.Checked, value);
                _checkBox.Checked = value;
            }
        }
    }

    public CheckState CheckState
    {
        get => _checkBox.CheckState;
        set
        {
            if (_checkBox.CheckState != value)
            {
                _service?.OnComponentChanged(_checkBox, null, _checkBox.CheckState, value);
                _checkBox.CheckState = value;
            }
        }
    }

    public bool ThreeState
    {
        get => _checkBox.ThreeState;
        set
        {
            if (_checkBox.ThreeState != value)
            {
                _service?.OnComponentChanged(_checkBox, null, _checkBox.ThreeState, value);
                _checkBox.ThreeState = value;
            }
        }
    }

    public VisualOrientation Orientation
    {
        get => _checkBox.Orientation;
        set
        {
            if (_checkBox.Orientation != value)
            {
                _service?.OnComponentChanged(_checkBox, null, _checkBox.Orientation, value);
                _checkBox.Orientation = value;
            }
        }
    }

    public VisualOrientation CheckPosition
    {
        get => _checkBox.CheckPosition;
        set
        {
            if (_checkBox.CheckPosition != value)
            {
                _service?.OnComponentChanged(_checkBox, null, _checkBox.CheckPosition, value);
                _checkBox.CheckPosition = value;
            }
        }
    }

    public string Text
    {
        get => _checkBox.Text;
        set
        {
            if (_checkBox.Text != value)
            {
                _service?.OnComponentChanged(_checkBox, null, _checkBox.Text, value);
                _checkBox.Text = value;
            }
        }
    }

    public string Subtext
    {
        get => _checkBox.Values.Subtext;
        set
        {
            if (_checkBox.Values.Subtext != value)
            {
                _service?.OnComponentChanged(_checkBox, null, _checkBox.Values.Subtext, value);
                _checkBox.Values.Subtext = value;
            }
        }
    }

    public Font? SubtextFont
    {
        get => _checkBox.Values.SubtextFont;
        set
        {
            if (_checkBox.Values.SubtextFont != value)
            {
                _service?.OnComponentChanged(_checkBox, null, _checkBox.Values.SubtextFont, value);
                _checkBox.Values.SubtextFont = value;
            }
        }
    }

    public Color SubtextForeColor
    {
        get => _checkBox.Values.SubtextForeColor;
        set
        {
            if (_checkBox.Values.SubtextForeColor != value)
            {
                _service?.OnComponentChanged(_checkBox, null, _checkBox.Values.SubtextForeColor, value);
                _checkBox.Values.SubtextForeColor = value;
            }
        }
    }

    public int SubtextSeparatorHeight
    {
        get => _checkBox.LayoutValues.SubtextSeparatorHeight;
        set
        {
            if (_checkBox.LayoutValues.SubtextSeparatorHeight != value)
            {
                _service?.OnComponentChanged(_checkBox, null, _checkBox.LayoutValues.SubtextSeparatorHeight, value);
                _checkBox.LayoutValues.SubtextSeparatorHeight = value;
            }
        }
    }

    public int TextGap
    {
        get => _checkBox.LayoutValues.TextGap;
        set
        {
            if (_checkBox.LayoutValues.TextGap != value)
            {
                _service?.OnComponentChanged(_checkBox, null, _checkBox.LayoutValues.TextGap, value);
                _checkBox.LayoutValues.TextGap = value;
            }
        }
    }

    public LabelStyle LabelStyle
    {
        get => _checkBox.LabelStyle;
        set
        {
            if (_checkBox.LabelStyle != value)
            {
                _service?.OnComponentChanged(_checkBox, null, _checkBox.LabelStyle, value);
                _checkBox.LabelStyle = value;
            }
        }
    }

    public PaletteMode PaletteMode
    {
        get => _checkBox.PaletteMode;
        set
        {
            if (_checkBox.PaletteMode != value)
            {
                _service?.OnComponentChanged(_checkBox, null, _checkBox.PaletteMode, value);
                _checkBox.PaletteMode = value;
            }
        }
    }

    #endregion

    #region Public Override

    public override DesignerActionItemCollection GetSortedActionItems()
    {
        var actions = new DesignerActionItemCollection();

        if (_checkBox != null)
        {
            actions.Add(new DesignerActionHeaderItem(@"Appearance"));
            actions.Add(new DesignerActionPropertyItem(nameof(Text), @"Text", @"Appearance", @"Main check box text"));
            actions.Add(new DesignerActionPropertyItem(nameof(Subtext), @"Subtext", @"Appearance", @"Secondary descriptive text"));
            actions.Add(new DesignerActionPropertyItem(nameof(SubtextFont), @"Subtext Font", @"Appearance", @"Font used for the subtext"));
            actions.Add(new DesignerActionPropertyItem(nameof(SubtextForeColor), @"Subtext Color", @"Appearance", @"Foreground color of the subtext"));
            actions.Add(new DesignerActionPropertyItem(nameof(SubtextSeparatorHeight), @"Subtext Spacing", @"Appearance", @"Pixels between main text and subtext"));
            actions.Add(new DesignerActionPropertyItem(nameof(TextGap), @"Text Gap", @"Appearance", @"Extra spacing between the check box glyph and the text"));
            actions.Add(new DesignerActionPropertyItem(nameof(Checked), @"Checked", @"Appearance", @"Checked state"));
            actions.Add(new DesignerActionPropertyItem(nameof(CheckState), @"Check State", @"Appearance", @"Three state check value"));
            actions.Add(new DesignerActionPropertyItem(nameof(ThreeState), @"Three State", @"Appearance", @"Allow indeterminate state"));
            actions.Add(new DesignerActionHeaderItem(@"Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(Orientation), @"Orientation", @"Visuals", @"Control orientation"));
            actions.Add(new DesignerActionPropertyItem(nameof(CheckPosition), @"Check Position", @"Visuals", @"Check box position"));
            actions.Add(new DesignerActionPropertyItem(nameof(LabelStyle), @"Label Style", @"Visuals", @"Label style"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), @"Palette", @"Visuals", @"Palette applied to drawing"));
        }

        return actions;
    }

    #endregion
}
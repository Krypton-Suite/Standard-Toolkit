#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

[ToolboxItem(false)]
internal partial class PaletteCornerRoundingSelector : UserControl
{
    private bool _loadingValue;

    /// <summary>
    /// Initialize a new instance of the PaletteCornerRoundingSelector class.
    /// </summary>
    public PaletteCornerRoundingSelector()
    {
        InitializeComponent();
        UpdateCornerControlStates();
    }

    /// <summary>
    /// Gets and sets the value being edited.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public PaletteCornerRounding Value
    {
        get
        {
            if (checkBoxInherit.Checked)
            {
                return PaletteCornerRounding.Inherit;
            }

            return new PaletteCornerRounding(
                GetCornerValue(checkBoxTopLeft, numericUpDownTopLeft),
                GetCornerValue(checkBoxTopRight, numericUpDownTopRight),
                GetCornerValue(checkBoxBottomRight, numericUpDownBottomRight),
                GetCornerValue(checkBoxBottomLeft, numericUpDownBottomLeft));
        }

        set
        {
            _loadingValue = true;
            try
            {
                if (value.TopLeft == PaletteCornerRounding.InheritValue
                    && value.TopRight == PaletteCornerRounding.InheritValue
                    && value.BottomRight == PaletteCornerRounding.InheritValue
                    && value.BottomLeft == PaletteCornerRounding.InheritValue)
                {
                    checkBoxInherit.Checked = true;
                    ResetCornerOverrides();
                }
                else
                {
                    checkBoxInherit.Checked = false;
                    SetCornerValue(checkBoxTopLeft, numericUpDownTopLeft, value.TopLeft);
                    SetCornerValue(checkBoxTopRight, numericUpDownTopRight, value.TopRight);
                    SetCornerValue(checkBoxBottomRight, numericUpDownBottomRight, value.BottomRight);
                    SetCornerValue(checkBoxBottomLeft, numericUpDownBottomLeft, value.BottomLeft);
                }
            }
            finally
            {
                _loadingValue = false;
                UpdateCornerControlStates();
            }
        }
    }

    private static float GetCornerValue(CheckBox checkBox, NumericUpDown numericUpDown) =>
        checkBox.Checked ? Convert.ToSingle(numericUpDown.Value) : PaletteCornerRounding.InheritValue;

    private static void SetCornerValue(CheckBox checkBox, NumericUpDown numericUpDown, float value)
    {
        if (value == PaletteCornerRounding.InheritValue)
        {
            checkBox.Checked = false;
            numericUpDown.Value = ClampToNumericRange(numericUpDown, 0m);
            return;
        }

        checkBox.Checked = true;
        numericUpDown.Value = ClampToNumericRange(numericUpDown, (decimal)value);
    }

    private static decimal ClampToNumericRange(NumericUpDown numericUpDown, decimal value)
    {
        if (value < numericUpDown.Minimum)
        {
            return numericUpDown.Minimum;
        }

        if (value > numericUpDown.Maximum)
        {
            return numericUpDown.Maximum;
        }

        return value;
    }

    private void ResetCornerOverrides()
    {
        checkBoxTopLeft.Checked = false;
        checkBoxTopRight.Checked = false;
        checkBoxBottomRight.Checked = false;
        checkBoxBottomLeft.Checked = false;
        numericUpDownTopLeft.Value = 0m;
        numericUpDownTopRight.Value = 0m;
        numericUpDownBottomRight.Value = 0m;
        numericUpDownBottomLeft.Value = 0m;
    }

    private void UpdateCornerControlStates()
    {
        bool inheritAll = checkBoxInherit.Checked;
        labelOr.Enabled = !inheritAll;

        UpdateCornerRow(checkBoxTopLeft, numericUpDownTopLeft, inheritAll);
        UpdateCornerRow(checkBoxTopRight, numericUpDownTopRight, inheritAll);
        UpdateCornerRow(checkBoxBottomRight, numericUpDownBottomRight, inheritAll);
        UpdateCornerRow(checkBoxBottomLeft, numericUpDownBottomLeft, inheritAll);
    }

    private static void UpdateCornerRow(CheckBox checkBox, NumericUpDown numericUpDown, bool inheritAll)
    {
        checkBox.Enabled = !inheritAll;
        numericUpDown.Enabled = !inheritAll && checkBox.Checked;
    }

    private void checkBoxInherit_CheckedChanged(object? sender, EventArgs e)
    {
        if (_loadingValue)
        {
            return;
        }

        if (checkBoxInherit.Checked)
        {
            ResetCornerOverrides();
        }

        UpdateCornerControlStates();
    }

    private void CornerOverride_CheckedChanged(object? sender, EventArgs e)
    {
        if (_loadingValue)
        {
            return;
        }

        UpdateCornerControlStates();
    }
}

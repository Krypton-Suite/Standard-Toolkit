#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Comprehensive demonstration of KryptonColorButton custom colours (Issue #776).
/// Shows CustomColors, MaxCustomColors, VisibleCustomColors, and using only a fixed set of colours.
/// </summary>
public partial class KryptonColorButtonDemo : KryptonForm
{
    private static readonly Color[] _demoCustomColors =
    [
        Color.Red, Color.Orange, Color.Yellow, Color.Lime, Color.Cyan,
        Color.Blue, Color.Magenta, Color.Gray, Color.White, Color.Black
    ];

    private static readonly Color[] _extendedCustomColors =
    [
        Color.Red, Color.OrangeRed, Color.Orange, Color.Gold, Color.Yellow,
        Color.Lime, Color.Green, Color.Cyan, Color.DodgerBlue, Color.Blue,
        Color.Magenta, Color.Pink, Color.Gray, Color.DarkGray, Color.White,
        Color.Black
    ];

    public KryptonColorButtonDemo()
    {
        InitializeComponent();
        SetupDemos();
        SetupEventHandlers();
    }

    private void SetupDemos()
    {
        // 1. Default: full theme, standard, recent (no custom colours)
        btnDefault.Values.Text = @"Default (Theme + Standard + Recent)";
        btnDefault.CustomColorPreviewShape = KryptonColorButtonCustomColorPreviewShape.Circle;

        // 2. Only 10 custom colours – no theme/standard/recent
        btnOnlyCustom.CustomColors = _demoCustomColors;
        btnOnlyCustom.VisibleThemes = false;
        btnOnlyCustom.VisibleStandard = false;
        btnOnlyCustom.VisibleRecent = false;
        btnOnlyCustom.Values.Text = @"Only 10 custom colours";
        btnOnlyCustom.CustomColorPreviewShape = KryptonColorButtonCustomColorPreviewShape.Circle;

        // 3. Custom colours section alongside theme and standard
        btnCustomAndBuiltIn.CustomColors = _demoCustomColors;
        btnCustomAndBuiltIn.VisibleThemes = true;
        btnCustomAndBuiltIn.VisibleStandard = true;
        btnCustomAndBuiltIn.VisibleRecent = true;
        btnCustomAndBuiltIn.Values.Text = @"Custom + Theme + Standard + Recent";
        btnCustomAndBuiltIn.CustomColorPreviewShape = KryptonColorButtonCustomColorPreviewShape.Circle;

        // 4. MaxCustomColors = 6 (array has 16; only first 6 shown)
        btnMaxCustom.CustomColors = _extendedCustomColors;
        btnMaxCustom.MaxCustomColors = 6;
        btnMaxCustom.VisibleThemes = false;
        btnMaxCustom.VisibleStandard = false;
        btnMaxCustom.VisibleRecent = false;
        btnMaxCustom.Values.Text = @"MaxCustomColors = 6 (16 in list)";
        btnMaxCustom.CustomColorPreviewShape = KryptonColorButtonCustomColorPreviewShape.Circle;

        // 5. Optional: No Color and More Colors hidden, only custom
        btnNoMoreColors.CustomColors = _demoCustomColors;
        btnNoMoreColors.VisibleThemes = false;
        btnNoMoreColors.VisibleStandard = false;
        btnNoMoreColors.VisibleRecent = false;
        btnNoMoreColors.VisibleNoColor = false;
        btnNoMoreColors.VisibleMoreColors = false;
        btnNoMoreColors.Values.Text = @"Only custom (no No Color / More Colors)";
        btnNoMoreColors.CustomColorPreviewShape = KryptonColorButtonCustomColorPreviewShape.Circle;

        UpdateSelectedLabel(btnDefault.SelectedColor);
    }

    private void SetupEventHandlers()
    {
        btnDefault.SelectedColorChanged += OnAnyColorButtonSelectedColorChanged;
        btnOnlyCustom.SelectedColorChanged += OnAnyColorButtonSelectedColorChanged;
        btnCustomAndBuiltIn.SelectedColorChanged += OnAnyColorButtonSelectedColorChanged;
        btnMaxCustom.SelectedColorChanged += OnAnyColorButtonSelectedColorChanged;
        btnNoMoreColors.SelectedColorChanged += OnAnyColorButtonSelectedColorChanged;
    }

    private void OnAnyColorButtonSelectedColorChanged(object? sender, ColorEventArgs e)
    {
        UpdateSelectedLabel(e.Color);
    }

    private void UpdateSelectedLabel(Color color)
    {
        string text;
        if (color.IsEmpty || color.A == 0)
        {
            text = "Selected: (No colour / transparent)";
        }
        else
        {
            text = $"Selected: #{color.R:X2}{color.G:X2}{color.B:X2} (A={color.A})";
        }

        lblSelected.Values.Text = text;
    }
}

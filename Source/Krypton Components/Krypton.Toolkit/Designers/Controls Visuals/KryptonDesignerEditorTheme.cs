#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Resolves and applies designer-editor palettes (owner → global), and builds a local theme selector.
/// </summary>
/// <remarks>
/// Theme changes from the selector update only the open editor form. They do not modify the
/// edited component or <see cref="KryptonManager.GlobalPaletteMode"/>.
/// </remarks>
public static class KryptonDesignerEditorTheme
{
    private const string ThemeSelectorName = @"kcmbDesignerEditorTheme";

    #region Public
    /// <summary>
    /// Resolves the initial palette for a designer editor from the designer context.
    /// Prefers the owning control's local palette when set; otherwise uses the manager global palette.
    /// </summary>
    /// <param name="context">Designer type descriptor context.</param>
    /// <param name="paletteMode">Resolved palette mode.</param>
    /// <param name="customPalette">Resolved custom palette when mode is <see cref="PaletteMode.Custom"/>.</param>
    public static void ResolveFromContext(
        ITypeDescriptorContext? context,
        out PaletteMode paletteMode,
        out KryptonCustomPaletteBase? customPalette)
    {
        paletteMode = PaletteMode.Global;
        customPalette = null;

        if (TryGetOwnerPalette(context?.Instance, out paletteMode, out customPalette)
            && paletteMode != PaletteMode.Global)
        {
            return;
        }

        paletteMode = KryptonManager.CurrentGlobalPaletteMode;
        customPalette = KryptonManager.CurrentGlobalPalette as KryptonCustomPaletteBase;
    }

    /// <summary>
    /// Applies a palette to a designer editor form and its child Krypton controls.
    /// </summary>
    /// <param name="form">Editor form.</param>
    /// <param name="paletteMode">Palette mode to apply.</param>
    /// <param name="customPalette">Optional custom palette.</param>
    public static void ApplyToForm(KryptonForm form, PaletteMode paletteMode, KryptonCustomPaletteBase? customPalette)
    {
        if (paletteMode == PaletteMode.Global)
        {
            form.PaletteMode = PaletteMode.Global;
            form.LocalCustomPalette = null;
        }
        else if (paletteMode == PaletteMode.Custom)
        {
            form.PaletteMode = PaletteMode.Custom;
            form.LocalCustomPalette = customPalette;
        }
        else
        {
            form.PaletteMode = paletteMode;
            form.LocalCustomPalette = null;
        }

        KryptonDesignerCollectionForm.ApplyPalette(form.Controls, form.PaletteMode, form.LocalCustomPalette);
        SyncThemeSelector(form, form.PaletteMode == PaletteMode.Global
            ? KryptonManager.CurrentGlobalPaletteMode
            : form.PaletteMode);
        form.Invalidate(true);
    }

    /// <summary>
    /// Applies the resolved owner/global palette from the designer context.
    /// </summary>
    /// <param name="form">Editor form.</param>
    /// <param name="context">Designer context.</param>
    public static void ApplyFromContext(KryptonForm form, ITypeDescriptorContext? context)
    {
        ResolveFromContext(context, out var mode, out var custom);
        ApplyToForm(form, mode, custom);
    }

    /// <summary>
    /// Creates a theme combo that changes only the owning editor form's palette.
    /// </summary>
    /// <param name="form">Editor form to restyle.</param>
    /// <param name="initialMode">Initial selection.</param>
    /// <param name="initialCustom">Initial custom palette when mode is Custom.</param>
    /// <returns>Configured theme combo box.</returns>
    public static KryptonComboBox CreateThemeSelector(
        KryptonForm form,
        PaletteMode initialMode,
        KryptonCustomPaletteBase? initialCustom)
    {
        var state = new ThemeSelectorState();
        var combo = new KryptonComboBox
        {
            DropDownStyle = ComboBoxStyle.DropDownList,
            Name = ThemeSelectorName,
            Tag = state
        };
        combo.Items.AddRange(CommonHelperThemeSelectors.GetThemesArray());

        combo.SelectedIndexChanged += (_, _) =>
        {
            if (state.Suppress || combo.SelectedItem is not string themeName || themeName.Length == 0)
            {
                return;
            }

            var mode = ThemeManager.GetThemeManagerMode(themeName);
            if (mode == PaletteMode.Custom)
            {
                ApplyToForm(form, PaletteMode.Custom, initialCustom ?? form.LocalCustomPalette);
                return;
            }

            if (mode == PaletteMode.Global)
            {
                ApplyToForm(form, KryptonManager.CurrentGlobalPaletteMode,
                    KryptonManager.CurrentGlobalPalette as KryptonCustomPaletteBase);
                return;
            }

            ApplyToForm(form, mode, null);
        };

        SyncThemeSelectorCombo(combo, initialMode == PaletteMode.Global
            ? KryptonManager.CurrentGlobalPaletteMode
            : initialMode);
        return combo;
    }
    #endregion

    #region Implementation
    private sealed class ThemeSelectorState
    {
        public bool Suppress;
    }

    private static void SyncThemeSelector(KryptonForm form, PaletteMode mode)
    {
        var combo = FindThemeSelector(form.Controls);
        if (combo is not null)
        {
            SyncThemeSelectorCombo(combo, mode);
        }
    }

    private static void SyncThemeSelectorCombo(KryptonComboBox combo, PaletteMode mode)
    {
        var index = CommonHelperThemeSelectors.GetPaletteIndex(combo.Items, mode);
        if (index < 0 || index >= combo.Items.Count || combo.SelectedIndex == index)
        {
            return;
        }

        var state = combo.Tag as ThemeSelectorState;
        if (state is not null)
        {
            state.Suppress = true;
        }

        try
        {
            combo.SelectedIndex = index;
        }
        finally
        {
            if (state is not null)
            {
                state.Suppress = false;
            }
        }
    }

    private static KryptonComboBox? FindThemeSelector(Control.ControlCollection controls)
    {
        foreach (Control control in controls)
        {
            if (control is KryptonComboBox combo && combo.Name == ThemeSelectorName)
            {
                return combo;
            }

            if (control.HasChildren)
            {
                var nested = FindThemeSelector(control.Controls);
                if (nested is not null)
                {
                    return nested;
                }
            }
        }

        return null;
    }

    private static bool TryGetOwnerPalette(
        object? instance,
        out PaletteMode paletteMode,
        out KryptonCustomPaletteBase? customPalette)
    {
        paletteMode = PaletteMode.Global;
        customPalette = null;

        switch (instance)
        {
            case VisualControlBase visualControl:
                paletteMode = visualControl.PaletteMode;
                customPalette = visualControl.LocalCustomPalette;
                return true;

            case DataGridViewColumn { DataGridView: KryptonDataGridView grid }:
                paletteMode = grid.PaletteMode;
                customPalette = grid.Palette as KryptonCustomPaletteBase;
                return true;

            case KryptonContextMenu contextMenu:
                paletteMode = contextMenu.PaletteMode;
                customPalette = contextMenu.LocalCustomPalette;
                return true;

            default:
                return false;
        }
    }
    #endregion
}

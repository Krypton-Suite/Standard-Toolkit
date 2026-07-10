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
/// Krypton-themed designer editor for multiline <see cref="string"/> properties.
/// </summary>
public class KryptonDesignerMultilineStringEditor : UITypeEditor
{
    #region Identity
    /// <inheritdoc />
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext? context) =>
        context?.Instance != null ? UITypeEditorEditStyle.Modal : base.GetEditStyle(context);

    /// <inheritdoc />
    public override object? EditValue(ITypeDescriptorContext? context, IServiceProvider provider, object? value)
    {
        if (context?.Instance is null)
        {
            return value;
        }

        if (value != null && !(value is string))
        {
            return value;
        }

        if (provider.GetService(typeof(IWindowsFormsEditorService)) is not IWindowsFormsEditorService editorService)
        {
            return value;
        }

        var useRichTextBox = context.Instance is KryptonRichTextBox;

        using var form = new VisualMultilineStringEditorForm(
            null,
            null,
            useRichTextBox,
            @"Enter text:",
            @"Text Editor");

        form.SetEditText((string?)value ?? string.Empty);
        KryptonDesignerEditorPalette.ApplyDesignerPalette(form, context);

        if (editorService.ShowDialog(form) == DialogResult.OK)
        {
            context.OnComponentChanged();
            return form.GetEditedText();
        }

        return value;
    }
    #endregion
}

/// <summary>
/// Krypton-themed designer editor for <see cref="string"/> array properties such as <c>Lines</c>.
/// </summary>
public class KryptonDesignerStringArrayEditor : UITypeEditor
{
    #region Identity
    /// <inheritdoc />
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext? context) =>
        context?.Instance != null ? UITypeEditorEditStyle.Modal : base.GetEditStyle(context);

    /// <inheritdoc />
    public override object? EditValue(ITypeDescriptorContext? context, IServiceProvider provider, object? value)
    {
        if (context?.Instance is null)
        {
            return value;
        }

        if (value != null && !(value is string[]))
        {
            return value;
        }

        if (provider.GetService(typeof(IWindowsFormsEditorService)) is not IWindowsFormsEditorService editorService)
        {
            return value;
        }

        var lines = (string[]?)value ?? Array.Empty<string>();

        using var form = new VisualMultilineStringEditorForm(
            lines,
            null,
            false,
            @"Enter the strings in the collection (one per line):",
            @"String Collection Editor");

        KryptonDesignerEditorPalette.ApplyDesignerPalette(form, context);

        if (editorService.ShowDialog(form) == DialogResult.OK)
        {
            context.OnComponentChanged();
            return form.GetEditedLines();
        }

        return value;
    }
    #endregion
}

/// <summary>
/// Shared palette helpers for Krypton designer text editors.
/// </summary>
internal static class KryptonDesignerEditorPalette
{
    internal static void ApplyDesignerPalette(KryptonForm form, ITypeDescriptorContext context)
    {
        KryptonDesignerEditorTheme.ApplyFromContext(form, context);
        EnsureMultilineButtonBarThemeSelector(form);
    }

    /// <summary>
    /// Injects a local theme selector into the designed MultilineStringEditor button panel when present.
    /// </summary>
    private static void EnsureMultilineButtonBarThemeSelector(KryptonForm form)
    {
        if (!DesignModeHelper.IncludeDesignerEditorThemeSelector)
        {
            return;
        }

        var buttonPanel = FindNamedPanel(form.Controls, @"kpnlButtons");
        if (buttonPanel is null || FindNamedCombo(buttonPanel.Controls, @"kcmbDesignerEditorTheme") is not null)
        {
            return;
        }

        var themeCombo = KryptonDesignerEditorTheme.CreateThemeSelector(
            form,
            form.PaletteMode,
            form.LocalCustomPalette);
        themeCombo.Anchor = AnchorStyles.Top | AnchorStyles.Left;
        themeCombo.Location = new Point(
            KryptonDesignerEditorDpi.Scale(form, 9),
            KryptonDesignerEditorDpi.Scale(form, 13));
        themeCombo.Size = KryptonDesignerEditorDpi.Scale(form, new Size(220, 25));
        buttonPanel.Controls.Add(themeCombo);
        themeCombo.BringToFront();
    }

    private static KryptonPanel? FindNamedPanel(Control.ControlCollection controls, string name)
    {
        foreach (Control control in controls)
        {
            if (control is KryptonPanel panel && panel.Name == name)
            {
                return panel;
            }

            if (control.HasChildren)
            {
                var nested = FindNamedPanel(control.Controls, name);
                if (nested is not null)
                {
                    return nested;
                }
            }
        }

        return null;
    }

    private static KryptonComboBox? FindNamedCombo(Control.ControlCollection controls, string name)
    {
        foreach (Control control in controls)
        {
            if (control is KryptonComboBox combo && combo.Name == name)
            {
                return combo;
            }
        }

        return null;
    }
}

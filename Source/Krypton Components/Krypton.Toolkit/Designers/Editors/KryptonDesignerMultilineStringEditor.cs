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

        form.SetEditText(KryptonDesignerEditorPalette.NormalizeDesignerPlaceholderText(context, (string?)value ?? string.Empty));
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

        var lines = KryptonDesignerEditorPalette.NormalizeDesignerPlaceholderLines(
            context,
            (string[]?)value ?? Array.Empty<string>());

        using var form = new VisualMultilineStringEditorForm(
            lines,
            null,
            context.Instance is KryptonRichTextBox,
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
    /// <summary>
    /// Treats a lone line that matches the component <see cref="Control.Name"/> as empty.
    /// Visual Studio often seeds that placeholder when a control is first dropped on a form.
    /// </summary>
    internal static string[] NormalizeDesignerPlaceholderLines(ITypeDescriptorContext? context, string[] lines)
    {
        if (context?.Instance is Control control
            && !string.IsNullOrEmpty(control.Name)
            && lines.Length == 1
            && lines[0] == control.Name)
        {
            return Array.Empty<string>();
        }

        return lines;
    }

    /// <summary>
    /// Treats text that matches the component <see cref="Control.Name"/> as empty.
    /// </summary>
    internal static string NormalizeDesignerPlaceholderText(ITypeDescriptorContext? context, string text)
    {
        if (context?.Instance is Control control
            && !string.IsNullOrEmpty(control.Name)
            && text == control.Name)
        {
            return string.Empty;
        }

        return text;
    }

    internal static void ApplyDesignerPalette(KryptonForm form, ITypeDescriptorContext context)
    {
        KryptonDesignerEditorTheme.ApplyFromContext(form, context);
        RewireFooterThemeSelector(form);
    }

    private static void RewireFooterThemeSelector(KryptonForm form)
    {
        var buttonBar = FindButtonBar(form.Controls);
        buttonBar?.WireThemeToForm(form);
    }

    private static InternalDesignerEditorButtonBarPanel? FindButtonBar(Control.ControlCollection controls)
    {
        foreach (Control control in controls)
        {
            if (control is InternalDesignerEditorButtonBarPanel buttonBar)
            {
                return buttonBar;
            }

            if (control.HasChildren)
            {
                var nested = FindButtonBar(control.Controls);
                if (nested is not null)
                {
                    return nested;
                }
            }
        }

        return null;
    }
}

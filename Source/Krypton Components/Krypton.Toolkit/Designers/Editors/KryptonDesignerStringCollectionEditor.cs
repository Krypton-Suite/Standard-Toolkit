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
/// Krypton-themed designer editor for <see cref="StringCollection"/> and related string list properties.
/// </summary>
public class KryptonDesignerStringCollectionEditor : UITypeEditor
{
    #region Identity
    /// <inheritdoc />
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext? context) =>
        context?.Instance != null ? UITypeEditorEditStyle.Modal : base.GetEditStyle(context);

    /// <inheritdoc />
    public override object? EditValue(ITypeDescriptorContext? context, IServiceProvider provider, object? value)
    {
        if (context?.Instance is null || value is null)
        {
            return value;
        }

        if (provider.GetService(typeof(IWindowsFormsEditorService)) is not IWindowsFormsEditorService editorService)
        {
            return value;
        }

        ValidateEditContext(context);

        var lines = ConvertToLines(value);

        using var form = new VisualMultilineStringEditorForm(
            lines,
            null,
            false,
            @"Enter the strings in the collection (one per line):",
            @"String Collection Editor");

        KryptonDesignerEditorPalette.ApplyDesignerPalette(form, context);

        if (editorService.ShowDialog(form) == DialogResult.OK)
        {
            ApplyLines(value, form.GetEditedLines());
            context.OnComponentChanged();
        }

        return value;
    }
    #endregion

    #region Protected
    /// <summary>
    /// Validates that the collection can be edited in the current designer context.
    /// </summary>
    /// <param name="context">Designer context.</param>
    protected virtual void ValidateEditContext(ITypeDescriptorContext context)
    {
    }

    #endregion

    #region Implementation
    private static string[] ConvertToLines(object collection)
    {
        switch (collection)
        {
            case StringCollection stringCollection:
            {
                var lines = new string[stringCollection.Count];
                stringCollection.CopyTo(lines, 0);
                return lines;
            }
            case AutoCompleteStringCollection autoCompleteCollection:
            {
                var lines = new string[autoCompleteCollection.Count];
                autoCompleteCollection.CopyTo(lines, 0);
                return lines;
            }
            case IList list:
            {
                var lines = new string[list.Count];
                for (var i = 0; i < list.Count; i++)
                {
                    lines[i] = list[i]?.ToString() ?? string.Empty;
                }

                return lines;
            }
            default:
                return [];
        }
    }

    private static void ApplyLines(object collection, string[] lines)
    {
        switch (collection)
        {
            case StringCollection stringCollection:
                stringCollection.Clear();
                stringCollection.AddRange(lines);
                break;
            case AutoCompleteStringCollection autoCompleteCollection:
                autoCompleteCollection.Clear();
                autoCompleteCollection.AddRange(lines);
                break;
            case IList list:
                list.Clear();
                foreach (var line in lines)
                {
                    list.Add(line);
                }

                break;
        }
    }
    #endregion
}

/// <summary>
/// Krypton-themed designer editor for list-control item collections.
/// </summary>
public class KryptonDesignerListControlStringCollectionEditor : KryptonDesignerStringCollectionEditor
{
    #region Protected
    /// <inheritdoc />
    protected override void ValidateEditContext(ITypeDescriptorContext context)
    {
        if (context.Instance is KryptonComboBox combo && combo.DataSource != null)
        {
            throw new ArgumentException(@"Items cannot be modified when a DataSource is set.");
        }

        if (context.Instance is KryptonListBox listBox && listBox.DataSource != null)
        {
            throw new ArgumentException(@"Items cannot be modified when a DataSource is set.");
        }

        if (context.Instance is KryptonCheckedListBox checkedListBox && checkedListBox.DataSource != null)
        {
            throw new ArgumentException(@"Items cannot be modified when a DataSource is set.");
        }

        if (context.Instance is ListControl listControl && listControl.DataSource != null)
        {
            throw new ArgumentException(@"Items cannot be modified when a DataSource is set.");
        }
    }
    #endregion
}

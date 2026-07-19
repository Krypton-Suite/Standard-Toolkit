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
/// Krypton-themed designer editor for task-dialog command-link button collections.
/// </summary>
public sealed class KryptonTaskDialogCommandLinkButtonsCollectionEditor : KryptonDesignerStandardCollectionEditor
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonTaskDialogCommandLinkButtonsCollectionEditor"/> class.
    /// </summary>
    public KryptonTaskDialogCommandLinkButtonsCollectionEditor()
        : base(typeof(InternalKryptonCommandLinkButton))
    {
    }
    #endregion

    #region Public
    /// <inheritdoc />
    public override object? EditValue(ITypeDescriptorContext? context, IServiceProvider provider, object? value)
    {
        if (context?.Instance is not KryptonTaskDialogElementCommandLinkButtons parent)
        {
            return base.EditValue(context, provider, value);
        }

        parent.CollectionEditorActive = true;
        try
        {
            return base.EditValue(context, provider, value);
        }
        finally
        {
            parent.CollectionEditorActive = false;
            parent.CollectionEditorClosed?.Invoke();
        }
    }
    #endregion

    #region Protected
    /// <inheritdoc />
    protected override object? SetItems(object? editValue, object[]? value)
    {
        if (editValue is ObservableCollection<InternalKryptonCommandLinkButton> collection)
        {
            collection.Clear();
            if (value is not null)
            {
                foreach (InternalKryptonCommandLinkButton item in value)
                {
                    collection.Add(item);
                }
            }

            return editValue;
        }

        return base.SetItems(editValue, value);
    }
    #endregion
}

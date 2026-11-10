#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Public façade for the Krypton System Information window (msinfo32-style).
/// </summary>
[ToolboxItem(false)]
[DesignerCategory(@"code")]
public static class KryptonSystemInformation
{
    /// <summary>Gets the localisable strings for the System Information UI.</summary>
    public static KryptonSystemInformationStrings Strings => KryptonSystemInformationStrings.Current;

    /// <summary>Shows the System Information window modelessly.</summary>
    public static Form Show() => Show(null, default);

    /// <summary>Shows the System Information window modelessly, owned by <paramref name="owner"/>.</summary>
    public static Form Show(IWin32Window? owner) => Show(owner, default);

    /// <summary>Shows the System Information window modelessly using <paramref name="data"/>.</summary>
    /// <returns>The opened form so callers can observe <see cref="Form.FormClosed"/>.</returns>
    public static Form Show(IWin32Window? owner, KryptonSystemInformationData data)
    {
        var form = CreateForm(data);
        form.StartPosition = owner == null ? FormStartPosition.CenterScreen : FormStartPosition.CenterParent;
        if (owner == null)
        {
            form.Show();
        }
        else
        {
            form.Show(owner);
        }

        return form;
    }

    /// <summary>Shows the System Information window as a modal dialog.</summary>
    public static DialogResult ShowDialog(IWin32Window? owner = null) => ShowDialog(owner, default);

    /// <summary>Shows the System Information window as a modal dialog using <paramref name="data"/>.</summary>
    public static DialogResult ShowDialog(IWin32Window? owner, KryptonSystemInformationData data)
    {
        using var form = CreateForm(data);
        form.StartPosition = owner == null ? FormStartPosition.CenterScreen : FormStartPosition.CenterParent;
        return form.ShowDialog(owner);
    }

    /// <summary>Shows the System Information window as a modal dialog asynchronously.</summary>
    public static Task<DialogResult> ShowAsync(IWin32Window? owner = null) => ShowAsync(owner, default);

    /// <summary>Shows the System Information window as a modal dialog asynchronously using <paramref name="data"/>.</summary>
    public static async Task<DialogResult> ShowAsync(IWin32Window? owner, KryptonSystemInformationData data)
    {
        using var form = CreateForm(data);
        form.StartPosition = owner == null ? FormStartPosition.CenterScreen : FormStartPosition.CenterParent;
        return await KryptonFormAsync.ShowDialogAsync(form, owner).ConfigureAwait(false);
    }

    private static VisualSystemInformationForm CreateForm(KryptonSystemInformationData data) =>
        new VisualSystemInformationForm(data);
}

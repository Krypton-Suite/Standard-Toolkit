#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Interactive dialog for building a custom theme from a few seed colours.
/// </summary>
[ToolboxItem(false)]
[DesignerCategory(@"code")]
public static class KryptonCustomThemeBuilder
{
    /// <summary>
    /// Shows the custom theme builder with default seed values.
    /// </summary>
    /// <returns>The dialog result.</returns>
    public static DialogResult Show() => Show(null, new KryptonCustomThemeSeed());

    /// <summary>
    /// Shows the custom theme builder owned by <paramref name="owner"/>.
    /// </summary>
    /// <param name="owner">Owner window, or <c>null</c>.</param>
    /// <returns>The dialog result.</returns>
    public static DialogResult Show(IWin32Window? owner) => Show(owner, new KryptonCustomThemeSeed());

    /// <summary>
    /// Shows the custom theme builder initialised from <paramref name="seed"/>.
    /// </summary>
    /// <param name="seed">Initial seed colours. Cannot be null.</param>
    /// <returns>The dialog result.</returns>
    public static DialogResult Show(KryptonCustomThemeSeed seed) => Show(null, seed);

    /// <summary>
    /// Shows the custom theme builder owned by <paramref name="owner"/> and initialised from <paramref name="seed"/>.
    /// </summary>
    /// <param name="owner">Owner window, or <c>null</c>.</param>
    /// <param name="seed">Initial seed colours. Cannot be null.</param>
    /// <returns>The dialog result.</returns>
    public static DialogResult Show(IWin32Window? owner, KryptonCustomThemeSeed seed)
    {
        ThrowHelper.ThrowIfNull(seed);

        using var form = new VisualCustomThemeBuilderForm(seed.Clone());
        return owner is null ? form.ShowDialog() : form.ShowDialog(owner);
    }
}

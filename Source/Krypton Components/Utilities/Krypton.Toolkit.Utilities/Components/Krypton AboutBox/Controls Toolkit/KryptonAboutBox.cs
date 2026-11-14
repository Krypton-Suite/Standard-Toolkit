#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2023 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Public entry point for the comprehensive About dialog hosted in <c>Krypton.Toolkit.Utilities</c>.
/// Identity is taken from assembly attributes unless overridden on <see cref="KryptonAboutBoxData"/>.
/// </summary>
[ToolboxItem(false)]
[DesignerCategory(@"code")]
public static class KryptonAboutBox
{
    #region Public

    /// <summary>
    /// Shows an About box for the entry assembly, filling name, version, copyright, and description from attributes.
    /// </summary>
    /// <returns>The dialog result from the About box.</returns>
    public static DialogResult Show()
    {
        // GetCallingAssembly must run here so the consumer assembly is used when there is no entry assembly.
        Assembly assembly = Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly();
        return ShowCore(CreateFromAssembly(assembly), new KryptonAboutToolkitData());
    }

    /// <summary>
    /// Shows an About box for the specified assembly.
    /// </summary>
    /// <param name="assembly">The assembly whose attributes populate the dialog. Cannot be null.</param>
    /// <returns>The dialog result from the About box.</returns>
    public static DialogResult Show(Assembly assembly)
    {
        ThrowHelper.ThrowIfNull(assembly);
        return ShowCore(CreateFromAssembly(assembly), new KryptonAboutToolkitData());
    }

    /// <summary>Shows a new <see cref="VisualAboutBoxForm"/>.</summary>
    /// <param name="aboutBoxData">The data to pass through.</param>
    /// <returns>The dialog result from the About box.</returns>
    public static DialogResult Show(KryptonAboutBoxData aboutBoxData) =>
        ShowCore(aboutBoxData, new KryptonAboutToolkitData());

    /// <summary>Shows a new <see cref="VisualAboutBoxForm"/>.</summary>
    /// <param name="aboutBoxData">The about box data.</param>
    /// <param name="aboutToolkitData">The about toolkit data.</param>
    /// <returns>The dialog result from the About box.</returns>
    public static DialogResult Show(KryptonAboutBoxData aboutBoxData, KryptonAboutToolkitData aboutToolkitData) =>
        ShowCore(aboutBoxData, aboutToolkitData);

    /// <summary>
    /// Shows an About box asynchronously for the entry assembly.
    /// </summary>
    /// <returns>A task that produces the dialog result when the about box is closed.</returns>
    public static Task<DialogResult> ShowAsync()
    {
        Assembly assembly = Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly();
        return ShowCoreAsync(CreateFromAssembly(assembly), new KryptonAboutToolkitData());
    }

    /// <summary>
    /// Shows an About box asynchronously for the specified assembly.
    /// </summary>
    /// <param name="assembly">The assembly whose attributes populate the dialog. Cannot be null.</param>
    /// <returns>A task that produces the dialog result when the about box is closed.</returns>
    public static Task<DialogResult> ShowAsync(Assembly assembly)
    {
        ThrowHelper.ThrowIfNull(assembly);
        return ShowCoreAsync(CreateFromAssembly(assembly), new KryptonAboutToolkitData());
    }

    /// <summary>Shows a new <see cref="VisualAboutBoxForm"/> asynchronously.</summary>
    /// <param name="aboutBoxData">The data to pass through.</param>
    /// <returns>A task that produces the dialog result when the about box is closed.</returns>
    public static Task<DialogResult> ShowAsync(KryptonAboutBoxData aboutBoxData) =>
        ShowCoreAsync(aboutBoxData, new KryptonAboutToolkitData());

    /// <summary>Shows a new <see cref="VisualAboutBoxForm"/> asynchronously.</summary>
    /// <param name="aboutBoxData">The about box data.</param>
    /// <param name="aboutToolkitData">The about toolkit data.</param>
    /// <returns>A task that produces the dialog result when the about box is closed.</returns>
    public static Task<DialogResult> ShowAsync(KryptonAboutBoxData aboutBoxData, KryptonAboutToolkitData aboutToolkitData) =>
        ShowCoreAsync(aboutBoxData, aboutToolkitData);

    #endregion

    #region Implementation

    private static KryptonAboutBoxData CreateFromAssembly(Assembly assembly) =>
        KryptonAboutBoxUtilities.CreateDataFromAssembly(assembly);

    private static DialogResult ShowCore(KryptonAboutBoxData aboutBoxData, KryptonAboutToolkitData aboutToolkitData)
    {
        using var kab = new VisualAboutBoxForm(aboutBoxData, aboutToolkitData);

        return kab.ShowDialog();
    }

    private static async Task<DialogResult> ShowCoreAsync(KryptonAboutBoxData aboutBoxData, KryptonAboutToolkitData aboutToolkitData)
    {
        using var kab = new VisualAboutBoxForm(aboutBoxData, aboutToolkitData);

        // Await required so using does not dispose the form before the dialog completes.
        return await KryptonFormAsync.ShowDialogAsync(kab).ConfigureAwait(false);
    }

    #endregion
}

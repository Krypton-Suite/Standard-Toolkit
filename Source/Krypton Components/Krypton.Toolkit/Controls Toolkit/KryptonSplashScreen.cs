#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2024 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>The public interface to the <see cref="VisualSplashScreenForm"/> class.</summary>
[ToolboxItem(false)]
[DesignerCategory(@"code")]
public static class KryptonSplashScreen
{
    #region Public

    /// <summary>Shows the specified splash screen.</summary>
    /// <param name="splashScreenData">The splash screen data.</param>
    /// <returns>
    ///   <br />
    /// </returns>
    public static DialogResult Show(KryptonSplashScreenData splashScreenData) => ShowCore(splashScreenData);

    public static void Show(Assembly entryAssembly, bool showProgressBar, int? timeOut, Image applicationLogo, IWin32Window? nextWindow) =>
        ShowCore(entryAssembly, showProgressBar, timeOut, applicationLogo, nextWindow);

    /// <summary>Shows the specified splash screen asynchronously.</summary>
    /// <param name="splashScreenData">The splash screen data.</param>
    /// <returns>A task that produces the dialog result when the splash screen is closed.</returns>
    public static Task<DialogResult> ShowAsync(KryptonSplashScreenData splashScreenData) => ShowCoreAsync(splashScreenData);

    #endregion

    #region Implementation

    private static DialogResult ShowCore(KryptonSplashScreenData splashScreenData)
    {
        using var kssf = new VisualSplashScreenForm(splashScreenData);

        return kssf.ShowDialog();
    }

    private static async Task<DialogResult> ShowCoreAsync(KryptonSplashScreenData splashScreenData)
    {
        using var kssf = new VisualSplashScreenForm(splashScreenData);

        return await KryptonFormAsync.ShowDialogAsync(kssf).ConfigureAwait(false);
    }

    private static void ShowCore(Assembly entryAssembly, bool showProgressBar, int? timeOut, Image applicationLogo, IWin32Window? nextWindow) => new VisualSplashScreenForm(entryAssembly, showProgressBar, timeOut, applicationLogo, nextWindow).Show();

    #endregion
}
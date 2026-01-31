#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
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

    #endregion

    #region Implementation

    private static DialogResult ShowCore(KryptonSplashScreenData splashScreenData)
    {
        using var kssf = new VisualSplashScreenForm(splashScreenData);

        return kssf.ShowDialog();
    }

    private static void ShowCore(Assembly entryAssembly, bool showProgressBar, int? timeOut, Image applicationLogo, IWin32Window? nextWindow) => new VisualSplashScreenForm(entryAssembly, showProgressBar, timeOut, applicationLogo, nextWindow).Show();

    #endregion
}
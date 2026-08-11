#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2023 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>Public API to display the <see cref="VisualThemeBrowserForm"/>.</summary>
public class KryptonThemeBrowser
{
    #region Public

    public static void Show(KryptonThemeBrowserData themeBrowserData, RightToLeftLayout? rightToLeftLayout = RightToLeftLayout.LeftToRight) => ShowCore(themeBrowserData, rightToLeftLayout);

    /// <summary>Displays the theme browser asynchronously.</summary>
    /// <param name="themeBrowserData">The theme browser data.</param>
    /// <param name="rightToLeftLayout">Optional RTL layout mode.</param>
    /// <returns>A task that completes when the browser is closed.</returns>
    public static Task ShowAsync(KryptonThemeBrowserData themeBrowserData, RightToLeftLayout? rightToLeftLayout = RightToLeftLayout.LeftToRight) =>
        ShowCoreAsync(themeBrowserData, rightToLeftLayout);

    #endregion

    #region Implementation

    private static void ShowCore(KryptonThemeBrowserData themeBrowserData,
        RightToLeftLayout? layout)
    {
        if (layout == RightToLeftLayout.LeftToRight)
        {
            using var ktb = new VisualThemeBrowserForm(themeBrowserData);

            ktb.ShowDialog();
        }
        else
        {
            using var ktbRTL = new VisualThemeBrowserFormRtlAware(themeBrowserData);

            ktbRTL.ShowDialog();
        }
    }

    private static async Task ShowCoreAsync(KryptonThemeBrowserData themeBrowserData,
        RightToLeftLayout? layout)
    {
        if (layout == RightToLeftLayout.LeftToRight)
        {
            using var ktb = new VisualThemeBrowserForm(themeBrowserData);

            await KryptonFormAsync.ShowDialogAsync(ktb).ConfigureAwait(false);
        }
        else
        {
            using var ktbRTL = new VisualThemeBrowserFormRtlAware(themeBrowserData);

            await KryptonFormAsync.ShowDialogAsync(ktbRTL).ConfigureAwait(false);
        }
    }

    #endregion
}
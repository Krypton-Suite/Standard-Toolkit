#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>Public API to display the <see cref="VisualThemeBrowserForm"/>.</summary>
public class KryptonThemeBrowser
{
    #region Public

    public static void Show(KryptonThemeBrowserData themeBrowserData, RightToLeftLayout? rightToLeftLayout = RightToLeftLayout.LeftToRight) => ShowCore(themeBrowserData, rightToLeftLayout);

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

    #endregion
}
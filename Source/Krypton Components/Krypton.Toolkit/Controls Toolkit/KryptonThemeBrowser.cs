#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2023. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    public class KryptonThemeBrowser
    {
        #region Public

        public static void Show(FormStartPosition startPosition) => ShowCore(startPosition, null, null, null, null);

        public static void Show(FormStartPosition startPosition, string windowTitle) => ShowCore(startPosition, null, windowTitle, null, null);

        public static void Show(FormStartPosition startPosition, int startIndex) => ShowCore(startPosition, startIndex, null, null, null);




        public static void Show(FormStartPosition startPosition, int startIndex, string windowTitle, bool showImportButton, bool showSilentOption) => ShowCore(startPosition, startIndex, windowTitle, showImportButton, showSilentOption);


        #endregion

        #region Implementation

        private static void ShowCore(FormStartPosition? startPosition,
                                     int? startIndex, string? windowTitle,
                                     bool? showImportButton, bool? showSilentOption)
        {
            using var ktb = new KryptonThemeBrowserForm(startPosition, startIndex, windowTitle, showImportButton,
                showSilentOption);

            ktb.ShowDialog();
        }

        #endregion
    }
}
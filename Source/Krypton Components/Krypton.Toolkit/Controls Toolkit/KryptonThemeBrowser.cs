#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2023. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>Public API to display the <see cref="KryptonThemeBrowserForm"/>.</summary>
    public class KryptonThemeBrowser
    {
        #region Public

        public static void Show() => ShowCore(null, null, null, null, null);

        /// <summary>Shows the theme browser window.</summary>
        /// <param name="startPosition">The start position.</param>
        public static void Show(FormStartPosition startPosition) => ShowCore(startPosition, null, null, null, null);

        /// <summary>Shows the theme browser window.</summary>
        /// <param name="startPosition">The start position.</param>
        /// <param name="windowTitle">The window title.</param>
        public static void Show(FormStartPosition startPosition, string windowTitle) => ShowCore(startPosition, null, windowTitle, null, null);

        /// <summary>Shows the theme browser window.</summary>
        /// <param name="startPosition">The start position.</param>
        /// <param name="startIndex">The start index.</param>
        public static void Show(FormStartPosition startPosition, int startIndex) => ShowCore(startPosition, startIndex, null, null, null);

        /// <summary>Shows the theme browser window.</summary>
        /// <param name="windowTitle">The window title.</param>
        public static void Show(string windowTitle) => ShowCore(null, null, windowTitle, null, null);

        /// <summary>Shows the theme browser window.</summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="windowTitle">The window title.</param>
        public static void Show(int startIndex, string windowTitle) => ShowCore(null, startIndex, windowTitle, null, null);

        /// <summary>Shows the theme browser window.</summary>
        /// <param name="windowTitle">The window title.</param>
        /// <param name="showImportButton">if set to <c>true</c> [show import button].</param>
        public static void Show(string windowTitle, bool showImportButton) => ShowCore(null, null, windowTitle, showImportButton, null);

        /// <summary>Shows the theme browser window.</summary>
        /// <param name="windowTitle">The window title.</param>
        /// <param name="showImportButton">if set to <c>true</c> [show import button].</param>
        /// <param name="showSilentOption">if set to <c>true</c> [show silent option].</param>
        public static void Show(string windowTitle, bool showImportButton, bool showSilentOption) => ShowCore(null, null, windowTitle, showImportButton, showSilentOption);

        /// <summary>Shows theme browser window.</summary>
        /// <param name="showImportButton">if set to <c>true</c> [show import button].</param>
        /// <param name="showSilentOption">if set to <c>true</c> [show silent option].</param>
        public static void Show(bool showImportButton, bool showSilentOption) => ShowCore(null, null, null, showImportButton, showSilentOption);

        /// <summary>Shows the theme browser window.</summary>
        /// <param name="startPosition">The start position.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="windowTitle">The window title.</param>
        /// <param name="showImportButton">if set to <c>true</c> [show import button].</param>
        /// <param name="showSilentOption">if set to <c>true</c> [show silent option].</param>
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
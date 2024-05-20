#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2024. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>Public API to display the <see cref="VisualThemeBrowserForm"/>.</summary>
    public class KryptonThemeBrowser
    {
        #region Public

        public static void Show(RightToLeftLayout? rightToLeftLayout = RightToLeftLayout.LeftToRight) => ShowCore(FormStartPosition.CenterScreen, 33, null, null, null, rightToLeftLayout);

        /// <summary>Shows the theme browser window.</summary>
        /// <param name="startPosition">The start position.</param>
        /// <param name="rightToLeftLayout">RTL window layout.</param>
        public static void Show(FormStartPosition startPosition, RightToLeftLayout? rightToLeftLayout = RightToLeftLayout.LeftToRight) => ShowCore(startPosition, 33, null, null, null, rightToLeftLayout);

        /// <summary>Shows the theme browser window.</summary>
        /// <param name="startPosition">The start position.</param>
        /// <param name="windowTitle">The window title.</param>
        /// <param name="rightToLeftLayout">RTL window layout.</param>
        public static void Show(FormStartPosition startPosition, string windowTitle, RightToLeftLayout? rightToLeftLayout = RightToLeftLayout.LeftToRight) => ShowCore(startPosition, 33, windowTitle, null, null, rightToLeftLayout);

        /// <summary>Shows the theme browser window.</summary>
        /// <param name="startPosition">The start position.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="rightToLeftLayout">RTL window layout.</param>
        public static void Show(FormStartPosition startPosition, int startIndex, RightToLeftLayout? rightToLeftLayout = RightToLeftLayout.LeftToRight) => ShowCore(startPosition, startIndex, null, null, null, rightToLeftLayout);

        /// <summary>Shows the theme browser window.</summary>
        /// <param name="windowTitle">The window title.</param>
        /// <param name="rightToLeftLayout">RTL window layout.</param>
        public static void Show(string windowTitle, RightToLeftLayout? rightToLeftLayout = RightToLeftLayout.LeftToRight) => ShowCore(FormStartPosition.CenterScreen, 33, windowTitle, null, null, rightToLeftLayout);

        /// <summary>Shows the theme browser window.</summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="windowTitle">The window title.</param>
        /// <param name="rightToLeftLayout">RTL window layout.</param>
        public static void Show(int startIndex, string windowTitle, RightToLeftLayout? rightToLeftLayout = RightToLeftLayout.LeftToRight) => ShowCore(FormStartPosition.CenterScreen, startIndex, windowTitle, null, null, rightToLeftLayout);

        /// <summary>Shows the theme browser window.</summary>
        /// <param name="windowTitle">The window title.</param>
        /// <param name="showImportButton">if set to <c>true</c> [show import button].</param>
        /// <param name="rightToLeftLayout">RTL window layout.</param>
        public static void Show(string windowTitle, bool showImportButton, RightToLeftLayout? rightToLeftLayout = RightToLeftLayout.LeftToRight) => ShowCore(FormStartPosition.CenterScreen, 33, windowTitle, showImportButton, null, rightToLeftLayout);

        /// <summary>Shows the theme browser window.</summary>
        /// <param name="windowTitle">The window title.</param>
        /// <param name="showImportButton">if set to <c>true</c> [show import button].</param>
        /// <param name="showSilentOption">if set to <c>true</c> [show silent option].</param>
        /// <param name="rightToLeftLayout">RTL window layout.</param>
        public static void Show(string windowTitle, bool showImportButton, bool showSilentOption, RightToLeftLayout? rightToLeftLayout = RightToLeftLayout.LeftToRight) => ShowCore(FormStartPosition.CenterScreen, 33, windowTitle, showImportButton, showSilentOption, rightToLeftLayout);

        /// <summary>Shows theme browser window.</summary>
        /// <param name="showImportButton">if set to <c>true</c> [show import button].</param>
        /// <param name="showSilentOption">if set to <c>true</c> [show silent option].</param>
        /// <param name="rightToLeftLayout">RTL window layout.</param>
        public static void Show(bool showImportButton, bool showSilentOption, RightToLeftLayout? rightToLeftLayout = RightToLeftLayout.LeftToRight) => ShowCore(FormStartPosition.CenterScreen, 336, null, showImportButton, showSilentOption, rightToLeftLayout);

        /// <summary>Shows the theme browser window.</summary>
        /// <param name="startPosition">The start position.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="windowTitle">The window title.</param>
        /// <param name="showImportButton">if set to <c>true</c> [show import button].</param>
        /// <param name="showSilentOption">if set to <c>true</c> [show silent option].</param>
        /// <param name="rightToLeftLayout">RTL window layout.</param>
        public static void Show(FormStartPosition startPosition, int startIndex, string windowTitle, bool showImportButton, bool showSilentOption, RightToLeftLayout? rightToLeftLayout = RightToLeftLayout.LeftToRight) => ShowCore(startPosition, startIndex, windowTitle, showImportButton, showSilentOption, rightToLeftLayout);
        
        #endregion

        #region Implementation

        private static void ShowCore(FormStartPosition startPosition,
                                     int startIndex, 
                                     string? windowTitle,
                                     bool? showImportButton, 
                                     bool? showSilentOption,
                                     RightToLeftLayout? layout)
        {
            if (layout == RightToLeftLayout.LeftToRight)
            {
                using var ktb = new VisualThemeBrowserForm(startPosition, startIndex, windowTitle, showImportButton,
                    showSilentOption);

                ktb.ShowDialog();
            }
            else
            {
                using var ktbRTL = new VisualThemeBrowserFormRtlAware(startPosition, startIndex, windowTitle,
                    showImportButton, showSilentOption);

                ktbRTL.ShowDialog();
            }
        }

        #endregion
    }
}
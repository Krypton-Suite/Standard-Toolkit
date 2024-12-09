﻿#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>The public interface to the <see cref="VisualExceptionDialogForm"/> class.</summary>
    [ToolboxItem(false)]
    [DesignerCategory(@"code")]
    public static class KryptonExceptionDialog
    {
        #region Public

        /// <summary>Shows the specified exception.</summary>
        /// <param name="exception">The exception.</param>
        public static void Show(Exception exception) => ShowCore(exception, null);

        /// <summary>Shows the specified exception.</summary>
        /// <param name="exception">The exception.</param>
        /// <param name="showCopyButton">Shows the copy button.</param>
        public static void Show(Exception exception, bool? showCopyButton) => ShowCore(exception, showCopyButton);

        #endregion

        #region Implementation

        private static void ShowCore(Exception exception, bool? showCopyButton) =>
            VisualExceptionDialogForm.Show(exception, showCopyButton);

        #endregion
    }
}
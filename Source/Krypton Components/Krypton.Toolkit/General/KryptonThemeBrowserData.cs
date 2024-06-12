#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2024. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>A structure that contains basic information for <see cref="VisualThemeBrowserForm"/>.</summary>
    public struct KryptonThemeBrowserData
    {
        #region Public

        public bool? ShowImportButton { get; set; }

        public bool? ShowSilentOption { get; set; }

        public FormStartPosition? StartPosition { get; set; }

        public int? StartIndex { get; set; }

        public string? WindowTitle { get; set; }

        #endregion
    }
}

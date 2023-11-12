#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2023. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>The public interface to the <see cref="KryptonAboutBoxForm"/> class.</summary>
    [ToolboxItem(false)]
    [DesignerCategory(@"code")]
    public static class KryptonAboutBox
    {
        #region Implementation

        /// <summary>Shows a new <see cref="KryptonAboutBoxForm"/>.</summary>
        /// <param name="aboutBoxData">The data to pass through.</param>
        /// <returns>A new <see cref="KryptonAboutBoxForm"/> with the specified data.</returns>
        public static DialogResult Show(KryptonAboutBoxData aboutBoxData)
            => ShowCore(aboutBoxData);

        #endregion

        #region Implementation

        private static DialogResult ShowCore(KryptonAboutBoxData aboutBoxData)
        {
            using var kab = new KryptonAboutBoxForm(aboutBoxData);

            return kab.ShowDialog();
        }

        #endregion
    }
}
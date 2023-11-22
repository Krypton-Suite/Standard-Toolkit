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
    /// <summary>The public interface to the <see cref="VisualKryptonAboutToolkitForm"/> class.</summary>
    [ToolboxItem(false)]
    [DesignerCategory(@"code")]
    public static class KryptonAboutToolkit
    {
        #region Implementation

        /// <summary>Shows a new <see cref="VisualKryptonAboutToolkitForm"/>.</summary>
        /// <param name="aboutToolkitData">The data to pass through.</param>
        /// <returns>A new <see cref="VisualKryptonAboutToolkitForm"/> with the specified data.</returns>
        public static DialogResult Show(KryptonAboutToolkitData aboutToolkitData)
            => ShowCore(aboutToolkitData);

        #endregion

        #region Implementation

        private static DialogResult ShowCore(KryptonAboutToolkitData aboutToolkitData)
        {
            using var kat = new VisualKryptonAboutToolkitForm(aboutToolkitData);

            return kat.ShowDialog();
        }

        #endregion
    }
}
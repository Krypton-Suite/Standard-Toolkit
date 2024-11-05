#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>A structure that contains basic information for <see cref="VisualSplashScreenForm"/>.</summary>
    public struct KryptonSplashScreenData
    {
        #region Public

        public Assembly Assembly { set; get; }

        public bool ShowCopyright { set; get; }

        public bool ShowVersion { set; get; }

        public bool ShowProgressBar { set; get; }

        public bool ShowProgressBarPercentage { set; get; }

        public Bitmap ApplicationLogo { set; get; }

        public int Timeout { set; get; }

        public IWin32Window? NextWindow { set; get; }

        #endregion

        #region Identity

        public KryptonSplashScreenData()
        {
            NextWindow = null;
        }

        #endregion
    }
}

#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>A structure that contains basic information for <see cref="VisualSplashScreenForm"/>.</summary>
public struct KryptonSplashScreenData
{
    #region Public

    /// <summary>Gets or sets the assembly.</summary>
    /// <value>The assembly.</value>
    public Assembly Assembly { set; get; } = Assembly.GetExecutingAssembly();

    /// <summary>Gets or sets a value indicating whether [show application name].</summary>
    /// <value><c>true</c> if [show application name]; otherwise, <c>false</c>.</value>
    public bool ShowApplicationName { get; set; }

    /// <summary>Gets or sets a value indicating whether [show copyright].</summary>
    /// <value><c>true</c> if [show copyright]; otherwise, <c>false</c>.</value>
    public bool ShowCopyright { set; get; }

    /// <summary>Gets or sets a value indicating whether [show close button].</summary>
    /// <value><c>true</c> if [show close button]; otherwise, <c>false</c>.</value>
    public bool ShowCloseButton { get; set; }

    /// <summary>Gets or sets a value indicating whether [show minimize button].</summary>
    /// <value><c>true</c> if [show minimize button]; otherwise, <c>false</c>.</value>
    public bool ShowMinimizeButton { get; set; }

    /// <summary>Gets or sets a value indicating whether [show version].</summary>
    /// <value><c>true</c> if [show version]; otherwise, <c>false</c>.</value>
    public bool ShowVersion { set; get; }

    /// <summary>Gets or sets a value indicating whether [show progress bar].</summary>
    /// <value><c>true</c> if [show progress bar]; otherwise, <c>false</c>.</value>
    public bool ShowProgressBar { set; get; }

    /// <summary>Gets or sets a value indicating whether [show progress bar percentage].</summary>
    /// <value><c>true</c> if [show progress bar percentage]; otherwise, <c>false</c>.</value>
    public bool ShowProgressBarPercentage { set; get; }

    /// <summary>Gets or sets the application logo.</summary>
    /// <value>The application logo.</value>
    public Bitmap ApplicationLogo { set; get; }

    /// <summary>Gets or sets the timeout.</summary>
    /// <value>The timeout.</value>
    public int Timeout { set; get; }

    /// <summary>Gets or sets the next window.</summary>
    /// <value>The next window.</value>
    public IWin32Window? NextWindow { set; get; }

    #endregion

    #region Identity

    public KryptonSplashScreenData()
    {
        NextWindow = null;
    }

    #endregion
}
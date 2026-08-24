#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Describes the content and behaviour of a <see cref="KryptonSplashScreenManager"/> splash window.
/// </summary>
/// <remarks>
/// The splash is a non-blocking, non-closeable, non-sizable window shown on a dedicated STA thread
/// so fade, status, and progress keep painting while the owner application continues startup work.
/// Distinct from the modal <see cref="KryptonSplashScreen"/> in <c>Krypton.Toolkit</c>.
/// </remarks>
public class KryptonSplashScreenManagerData
{
    #region Identity

    /// <summary>Initializes a new instance of the <see cref="KryptonSplashScreenManagerData"/> class.</summary>
    public KryptonSplashScreenManagerData()
    {
        Assembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
        ShowApplicationName = true;
        ShowVersion = true;
        ShowCopyright = false;
        ShowProgressBar = true;
        ShowExceptionDialog = true;
        FadeIn = true;
        FadeOut = true;
        FadeSpeed = FadeSpeedChoice.Normal;
        Opacity = 1.0;
        BackgroundImageLayout = ImageLayout.Stretch;
        StartPosition = FormStartPosition.CenterScreen;
        TopMost = true;
        Size = new Size(520, 320);
        MinimumDisplayMilliseconds = 750;
        BorderAnimation = KryptonSplashBorderAnimation.None;
        BorderAnimationThickness = 3;
        BorderAnimationSpeed = 1f;
    }

    #endregion

    #region Public

    /// <summary>Gets or sets the assembly used to read product name, version, and copyright.</summary>
    public Assembly? Assembly { get; set; }

    /// <summary>Gets or sets the splash title. When empty, <see cref="Application.ProductName"/> is used.</summary>
    public string? Title { get; set; }

    /// <summary>Gets or sets the initial status text shown below the title.</summary>
    public string? Status { get; set; }

    /// <summary>Gets or sets a value indicating whether the application name / title is shown.</summary>
    public bool ShowApplicationName { get; set; }

    /// <summary>Gets or sets a value indicating whether the file/assembly version is shown.</summary>
    public bool ShowVersion { get; set; }

    /// <summary>Gets or sets a value indicating whether the copyright string is shown.</summary>
    public bool ShowCopyright { get; set; }

    /// <summary>Gets or sets the application logo drawn above the title.</summary>
    [Editor(typeof(KryptonDesignerImageEditor), typeof(UITypeEditor))]
    public Image? Logo { get; set; }

    /// <summary>Gets or sets the optional background image for the splash client area.</summary>
    [Editor(typeof(KryptonDesignerImageEditor), typeof(UITypeEditor))]
    public Image? BackgroundImage { get; set; }

    /// <summary>Gets or sets how <see cref="BackgroundImage"/> is arranged. Defaults to <see cref="ImageLayout.Stretch"/>.</summary>
    public ImageLayout BackgroundImageLayout { get; set; }

    /// <summary>
    /// Gets or sets the form opacity (0.05–1.0). Values below 1.0 make the splash semi-transparent.
    /// </summary>
    public double Opacity { get; set; }

    /// <summary>Gets or sets the client size of the splash window.</summary>
    public Size Size { get; set; }

    /// <summary>Gets or sets the initial position of the splash window.</summary>
    public FormStartPosition StartPosition { get; set; }

    /// <summary>Gets or sets a value indicating whether the splash stays above other windows.</summary>
    public bool TopMost { get; set; }

    /// <summary>Gets or sets a value indicating whether the splash fades in when shown.</summary>
    public bool FadeIn { get; set; }

    /// <summary>Gets or sets a value indicating whether the splash fades out when closed from the manager.</summary>
    public bool FadeOut { get; set; }

    /// <summary>Gets or sets the fade speed used when <see cref="FadeIn"/> or <see cref="FadeOut"/> is enabled.</summary>
    public FadeSpeedChoice FadeSpeed { get; set; }

    /// <summary>Gets or sets a value indicating whether the progress bar is shown.</summary>
    public bool ShowProgressBar { get; set; }

    /// <summary>
    /// Gets or sets the number of <see cref="KryptonSplashScreenManager.SetStatus(string)"/> calls expected
    /// during startup. When set, each status update advances the progress bar by <c>100 / N</c>.
    /// When null, the bar uses marquee (indeterminate) style until <see cref="KryptonSplashScreenManager.SetProgress(int, int?)"/> is called.
    /// </summary>
    public int? ExpectedStepCount { get; set; }

    /// <summary>
    /// Gets or sets the palette applied to the splash form.
    /// When null, the form uses <see cref="PaletteMode.Global"/> (the current <see cref="KryptonManager"/> theme).
    /// </summary>
    public PaletteMode? PaletteMode { get; set; }

    /// <summary>
    /// Gets or sets an optional logger for status and exception messages.
    /// Adapters can wrap <c>ILogger</c>, NLog, or other sinks via <see cref="IKryptonLogger"/>.
    /// </summary>
    public IKryptonLogger? Logger { get; set; }

    /// <summary>Gets or sets an optional callback invoked with the same messages as <see cref="Logger"/>.</summary>
    public Action<string>? LogCallback { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether splash status and exception messages are written to
    /// <see cref="KryptonLog"/> when <see cref="Logger"/> is null. Defaults to <see langword="false"/>.
    /// </summary>
    public bool UseKryptonLog { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether <see cref="KryptonExceptionDialog"/> is shown on the
    /// caller thread after <see cref="KryptonSplashScreenManager.ReportException(Exception)"/>.
    /// </summary>
    public bool ShowExceptionDialog { get; set; }

    /// <summary>
    /// Gets or sets the minimum time the splash remains visible before <see cref="KryptonSplashScreenManager.Close"/>
    /// completes, so a fast startup does not flash the window. Defaults to 750 milliseconds.
    /// </summary>
    public int MinimumDisplayMilliseconds { get; set; }

    /// <summary>
    /// Gets or sets the optional animated border drawn around the splash.
    /// Default is <see cref="KryptonSplashBorderAnimation.None"/>.
    /// </summary>
    public KryptonSplashBorderAnimation BorderAnimation { get; set; }

    /// <summary>
    /// Gets or sets the border colour. When null, the current palette tracking/button border colour is used.
    /// </summary>
    public Color? BorderAnimationColor { get; set; }

    /// <summary>Gets or sets the animated border thickness in pixels. Defaults to 3. Values below 1 are treated as 1.</summary>
    public int BorderAnimationThickness { get; set; }

    /// <summary>
    /// Gets or sets the animation speed multiplier. Defaults to 1. Values of 0.1–4 are typical; values at or below 0 fall back to 1.
    /// </summary>
    public float BorderAnimationSpeed { get; set; }

    #endregion
}

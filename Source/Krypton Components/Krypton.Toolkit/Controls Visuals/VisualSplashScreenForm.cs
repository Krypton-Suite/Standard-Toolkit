#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal partial class VisualSplashScreenForm : KryptonForm/*, ISplashScreenData*/
{
    #region Instance Fields

    private readonly Assembly _entryAssembly;

    private readonly Image _applicationLogo;

    private readonly bool _showProgressBar;

    private int _timeOut;

    private IWin32Window? _nextWindow;

    private readonly KryptonSplashScreenData _splashScreenData;

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="VisualSplashScreenForm" /> class.</summary>
    /// <param name="splashScreenData">The splash screen data.</param>
    public VisualSplashScreenForm(KryptonSplashScreenData splashScreenData)
    {
        //SetInheritedControlOverride(); // Disabled as part of issue #2296. See the issue for details.
        InitializeComponent();

        _splashScreenData = splashScreenData;
    }

    /// <summary>Initializes a new instance of the <see cref="VisualSplashScreenForm" /> class.</summary>
    /// <param name="entryAssembly">The entry assembly.</param>
    /// <param name="showProgressBar">if set to <c>true</c> [show progress bar].</param>
    /// <param name="timeOut">The time out.</param>
    /// <param name="applicationLogo">The application logo.</param>
    /// <param name="nextWindow">The next window.</param>
    public VisualSplashScreenForm(Assembly entryAssembly, bool showProgressBar, int? timeOut, Image applicationLogo, IWin32Window? nextWindow)
    {
        //SetInheritedControlOverride(); // Disabled as part of issue #2296. See the issue for details.
        InitializeComponent();

        _entryAssembly = entryAssembly;

        _showProgressBar = showProgressBar;

        _timeOut = timeOut ?? 5000;

        _applicationLogo = applicationLogo;

        _nextWindow = nextWindow ?? null;
    }

    #endregion

    #region Implementation

    private void VisualSplashScreenForm_Load(object sender, EventArgs e)
    {
        string location = _splashScreenData.Assembly.Location;
        if (string.IsNullOrEmpty(location))
        {
            location = Assembly.GetEntryAssembly()?.Location ?? string.Empty;
        }

        if (string.IsNullOrEmpty(location))
        {
            location = Assembly.GetExecutingAssembly().Location;
        }

        FileVersionInfo? fvi = null;
        if (!string.IsNullOrEmpty(location))
        {
            try
            {
                fvi = FileVersionInfo.GetVersionInfo(location);
            }
            catch (ArgumentException)
            {
                // Location may be invalid (e.g. in-memory assembly when using dotnet run)
            }
        }

        pbxApplicationIcon.Image = _splashScreenData.ApplicationLogo /*_applicationLogo*/;

        kwlblApplicationName.Text = Application.ProductName;

        string copyright = fvi?.LegalCopyright ?? string.Empty;

        kwlblCopyright.Text = string.IsNullOrEmpty(copyright)
            ? $@"{KryptonManager.Strings.SplashScreenStrings.Copyright}:"
            : $@"{KryptonManager.Strings.SplashScreenStrings.Copyright}: {copyright}";

        string fileVersion = fvi?.FileVersion ?? _splashScreenData.Assembly.GetName().Version?.ToString() ?? string.Empty;
        kwlblVersion.Text = string.IsNullOrEmpty(fileVersion)
            ? $@"{KryptonManager.Strings.SplashScreenStrings.Version}:"
            : $@"{KryptonManager.Strings.SplashScreenStrings.Version}: {fileVersion}";

        kwlblApplicationName.Visible = _splashScreenData.ShowApplicationName;

        kwlblCopyright.Visible = _splashScreenData.ShowCopyright;

        kwlblVersion.Visible = _splashScreenData.ShowVersion;

        kpbProgress.Visible = _splashScreenData.ShowProgressBar;

        kbtnClose.Visible = _splashScreenData.ShowCloseButton;

        kbtnMinimize.Visible = _splashScreenData.ShowMinimizeButton;
    }

    private void VisualSplashScreenForm_FormClosing(object sender, FormClosingEventArgs e)
    {

    }

    private void VisualSplashScreenForm_FormClosed(object sender, FormClosedEventArgs e)
    {
        //_splashScreenData.NextWindow?.Show();
    }

    private void kbtnMinimize_Click(object sender, EventArgs e)
    {
        WindowState = FormWindowState.Minimized;
    }

    private void kbtnClose_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }

    private void tmrCountdown_Tick(object sender, EventArgs e)
    {
        kpbProgress.Increment(1);

        kpbProgress.Text = _splashScreenData.ShowProgressBarPercentage ? $@"{kpbProgress.Value}%" : GlobalStaticValues.DEFAULT_EMPTY_STRING;

        if (kpbProgress.Value == kpbProgress.Maximum)
        {
            Hide();

            //_splashScreenData.NextWindow?.Show();

        }
    }

    private void kbtnClose_MouseEnter(object sender, EventArgs e)
    {
        kbtnClose.StateCommon.Content.ShortText.Color1 = Color.Red;
    }

    private void kbtnClose_MouseHover(object sender, EventArgs e)
    {

    }

    private void kbtnClose_MouseLeave(object sender, EventArgs e)
    {
        kbtnClose.StateCommon.Content.ShortText.Color1 = GlobalStaticValues.EMPTY_COLOR;
    }

    #endregion
}
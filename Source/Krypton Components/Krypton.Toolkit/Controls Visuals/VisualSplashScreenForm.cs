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
    internal partial class VisualSplashScreenForm : KryptonForm/*, ISplashScreenData*/
    {
        #region Instance Fields

        private Assembly _entryAssembly;

        private Image _applicationLogo;

        private bool _showProgressBar;

        private int _timeOut;

        private IWin32Window _nextWindow;

        private ISplashScreenData _splashScreenData;

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="VisualSplashScreenForm" /> class.</summary>
        /// <param name="splashScreenData">The splash screen data.</param>
        public VisualSplashScreenForm(ISplashScreenData splashScreenData)
        {
            InitializeComponent();

            _splashScreenData = splashScreenData;
        }

        public VisualSplashScreenForm(Assembly entryAssembly, bool showProgressBar, int? timeOut, Image applicationLogo)
        {
            InitializeComponent();

            _entryAssembly = entryAssembly;

            _showProgressBar = showProgressBar;

            _timeOut = timeOut ?? 5000;

            _applicationLogo = applicationLogo;
        }

        #endregion

        #region Implementation

        private void VisualSplashScreenForm_Load(object sender, EventArgs e)
        {
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(/*_splashScreenData.Assembly.Location*/ _entryAssembly.Location);

            pbxApplicationIcon.Image = /*_splashScreenData.ApplicationLogo*/ _applicationLogo;

            klblCopyright.Text = $"{KryptonManager.Strings.SplashScreenStrings.Copyright}: {fvi.LegalCopyright}";

            klblVersion.Text = $"{KryptonManager.Strings.SplashScreenStrings.Version}: {fvi.FileVersion}";

            kpbProgress.Visible = /*_splashScreenData.ShowProgressBar*/ _showProgressBar;
        }

        private void VisualSplashScreenForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void VisualSplashScreenForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _splashScreenData.NextWindow?.Show();
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

        }

        #endregion
    }
}

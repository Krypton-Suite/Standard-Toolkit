#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

using System.IO;
using System.Reflection;

namespace TestForm;

public partial class SplashScreenExample : KryptonForm
{
    public SplashScreenExample()
    {
        InitializeComponent();
    }

    private void kbtnShow_Click(object sender, EventArgs e)
    {
        KryptonSplashScreenData splashScreenData = new KryptonSplashScreenData()
        {
            ApplicationLogo = new Bitmap(ktxtLogo.Text),
            Assembly = Assembly.GetExecutingAssembly(), //Assembly.LoadFile(ktxtAssembly.Text),
            NextWindow = this,
            ShowCopyright = kchkShowCopyright.Checked,
            ShowCloseButton = kcbShowCloseButton.Checked,
            ShowMinimizeButton = kcbShowMinimizeButton.Checked,
            ShowApplicationName = kchkShowApplicationName.Checked,
            ShowProgressBar = kchkShowProgressBar.Checked,
            ShowProgressBarPercentage = kchkShowProgressBarPercentage.Checked,
            ShowVersion = kchkShowVersion.Checked,
            Timeout = Convert.ToInt32(knudTimeout.Value)
        };

        KryptonSplashScreen.Show(splashScreenData);
    }

    private void kcmdChosenAssembly_Execute(object sender, EventArgs e)
    {
        OpenFileDialog ofd = new OpenFileDialog();

        if (ofd.ShowDialog() == DialogResult.OK)
        {
            ktxtAssembly.Text = Path.GetFullPath(ofd.FileName);
        }
    }

    private void kcmdChosenLogo_Execute(object sender, EventArgs e)
    {
        OpenFileDialog ofd = new OpenFileDialog();

        if (ofd.ShowDialog() == DialogResult.OK)
        {
            ktxtLogo.Text = Path.GetFullPath(ofd.FileName);
        }
    }
}
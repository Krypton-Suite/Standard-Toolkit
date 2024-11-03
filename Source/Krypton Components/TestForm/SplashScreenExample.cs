using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestForm
{
    public partial class SplashScreenExample : KryptonForm
    {
        public SplashScreenExample()
        {
            InitializeComponent();
        }

        private void kbtnShow_Click(object sender, EventArgs e)
        {
            ISplashScreenData? splashScreenData = null;

            if (splashScreenData != null)
            {
                splashScreenData.ApplicationLogo = Image.FromFile(ktxtLogo.Text);
                splashScreenData.Assembly = Assembly.LoadFile(ktxtAssembly.Text);
                splashScreenData.NextWindow = null;
                splashScreenData.ShowCopyright = kchkShowCopyright.Checked;
                splashScreenData.ShowProgressBar = kchkShowProgressBar.Checked;
                splashScreenData.ShowProgressBarPercentage = kchkShowProgressBarPercentage.Checked;
                splashScreenData.ShowVersion = kchkShowVersion.Checked;
                splashScreenData.Timeout = (int)knudTimeout.Value;

                KryptonSplashScreen.Show(splashScreenData);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Krypton.Toolkit
{
    public partial class VisualToolkitInformationForm : KryptonForm
    {
        #region Instance Fields

        private ToolkitType _toolkitType;
        private string _path;

        #endregion

        #region Idenity

        public VisualToolkitInformationForm(ToolkitType toolkitType)
        {
            InitializeComponent();

            _toolkitType = toolkitType;
        }

        #endregion

        private void kbtnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void VisualToolkitInformationForm_Load(object sender, EventArgs e)
        {
            _path = $"{Application.ExecutablePath}\\{GlobalStaticValues.DEFAULT_TOOLKIT_FILE}";

            if (File.Exists(_path))
            {
                var fileInfo = FileVersionInfo.GetVersionInfo(_path);

                klblToolkitVersion.Text = $"{KryptonManager.Strings.ToolkitInformationBoxStrings.Version}: {fileInfo.FileVersion}";
            }

            krtbLicense.Text = GlobalStaticValues.DEFAULT_LONG_SEED_TEXT;

            kbtnSystemInformation.Text = KryptonManager.Strings.MiscellaneousStrings.SystemInformationText;

            switch (_toolkitType)
            {
                case ToolkitType.Canary:
                    pbxLogo.Image = ToolkitLogoImageResources.Krypton_Canary;

                    klblHeading.Text = @"About Krypton Toolkit (Canary)";
                    break;
                case ToolkitType.Nightly:
                    pbxLogo.Image = ToolkitLogoImageResources.Krypton_Nightly;

                    klblHeading.Text = @"About Krypton Toolkit (Nightly)";
                    break;
                case ToolkitType.LongTermSupport:

                    klblHeading.Text = @"About Krypton Toolkit (Long Term Support)";
                    break;
                case ToolkitType.Stable:
                default:
                    pbxLogo.Image = ToolkitLogoImageResources.Krypton_Stable;

                    klblHeading.Text = @"About Krypton Toolkit (Stable)";
                    break;
            }
        }

        private void kbtnSystemInformation_Click(object sender, EventArgs e) => Process.Start(@"msinfo32.exe");

        private void kbtnMoreDetails_Click(object sender, EventArgs e)
        {
        }
    }
}

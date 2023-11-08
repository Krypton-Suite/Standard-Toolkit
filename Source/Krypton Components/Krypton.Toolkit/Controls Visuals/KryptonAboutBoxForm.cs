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
    public partial class KryptonAboutBoxForm : KryptonForm
    {
        #region Instance Fields

        private readonly KryptonAboutBoxData _aboutBoxData;

        #endregion

        #region Identity

        public KryptonAboutBoxForm(KryptonAboutBoxData aboutBoxData)
        {
            InitializeComponent();

            _aboutBoxData = aboutBoxData;

            Startup(_aboutBoxData);

            kbtnOk.Text = KryptonManager.Strings.GeneralStrings.OK;

            kbtnSystemInformation.Text = KryptonManager.Strings.CustomStrings.SystemInformation;
        }

        #endregion

        #region Implementation

        private void Startup(KryptonAboutBoxData aboutBoxData)
        {
            kryptonHeaderGroup1.ValuesPrimary.Image =
                aboutBoxData.HeaderImage ?? GenericImageResources.InformationSmall;

            kryptonHeaderGroup1.ValuesPrimary.Heading =
                $@"{KryptonManager.Strings.AboutBoxStrings.About} {aboutBoxData.ApplicationName}";
        }

        #endregion
    }
}

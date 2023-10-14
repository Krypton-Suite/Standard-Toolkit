#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2023. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit
{
    public partial class KryptonToolkitPoweredByControl : UserControl
    {
        #region Instance Fields

        private bool _showVersions;

        private bool _showThemeOption;

        private ToolkitType _toolkitType;

        #endregion

        #region Public

        public bool ShowVersions { get => _showVersions; set { _showVersions = value; Invalidate(); } }

        public bool ShowThemeOption { get => _showThemeOption; set { _showThemeOption = value; Invalidate(); } }

        public ToolkitType ToolkitType { get => _toolkitType; set => _toolkitType = value; }

        #endregion

        public KryptonToolkitPoweredByControl()
        {
            InitializeComponent();

            _showVersions = false;

            _showThemeOption = false;

            _toolkitType = ToolkitType.Stable;

            SetLogo(_toolkitType);
        }

        private void SetLogo(ToolkitType toolkitType)
        {
            switch (toolkitType)
            {
                case ToolkitType.Canary:
                    kpbxLogo.Image = ToolkitLogoImageResources.Krypton_Canary;
                    break;
                case ToolkitType.Nightly:
                    kpbxLogo.Image = ToolkitLogoImageResources.Krypton_Nightly;
                    break;
                case ToolkitType.Stable:
                    kpbxLogo.Image = ToolkitLogoImageResources.Krypton_Stable;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(toolkitType), toolkitType, null);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            kwlblVersionInformation.Visible = _showVersions;

            kwlblCurrentTheme.Visible = _showThemeOption;

            ktcmbCurrentTheme.Visible = _showThemeOption;

            base.OnPaint(e);
        }
    }
}

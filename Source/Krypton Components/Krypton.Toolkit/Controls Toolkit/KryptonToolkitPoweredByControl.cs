#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary></summary>
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(KryptonToolkitPoweredByControl), @"ToolboxBitmaps.KryptonLogo.bmp")]
    [Description(@"")]
    public partial class KryptonToolkitPoweredByControl : UserControl
    {
        #region Instance Fields

        private bool _showDockingVersion;

        private bool _showNavigatorVersion;

        private bool _showRibbonVersion;

        private bool _showToolkitVersion;

        private bool _showWorkspaceVersion;

        private bool _showThemeOption;

        private ToolkitType _toolkitType;

        #endregion

        #region Public

        //public bool ShowDockingVersion { get => _showDockingVersion; set { _showDockingVersion = value; Invalidate(); } }

        //public bool ShowNavigatorVersion { get => _showNavigatorVersion; set { _showNavigatorVersion = value; Invalidate(); } }

        //public bool ShowRibbonVersion { get => _showRibbonVersion; set { _showRibbonVersion = value; Invalidate(); } }

        //public bool ShowToolkitVersion { get => _showToolkitVersion; set { _showToolkitVersion = value; Invalidate(); } }

        //public bool ShowWorkspaceVersion { get => _showWorkspaceVersion; set { _showWorkspaceVersion = value; Invalidate(); } }

        /// <summary>Gets or sets a value indicating whether [show theme option].</summary>
        /// <value><c>true</c> if [show theme option]; otherwise, <c>false</c>.</value>
        [DefaultValue(false), Description(@"Allows the user to change the theme.")]
        public bool ShowThemeOption { get => _showThemeOption; set { _showThemeOption = value; Invalidate(); SetLogoSpan(value); } }

        /// <summary>Gets or sets the type of the toolkit.</summary>
        /// <value>The type of the toolkit.</value>
        [DefaultValue(typeof(ToolkitType), @"ToolkitType.Stable"), Description(@"Changes the icon based on the type of toolkit you are using.")]
        public ToolkitType ToolkitType { get => _toolkitType; set { _toolkitType = value; SetLogo(value); } }

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="KryptonToolkitPoweredByControl" /> class.</summary>
        public KryptonToolkitPoweredByControl()
        {
            InitializeComponent();

            _showDockingVersion = false;

            _showNavigatorVersion = false;

            _showRibbonVersion = false;

            _showToolkitVersion = false;

            _showWorkspaceVersion = false;

            _showThemeOption = false;

            kwlblDockingVersion.Text = null;

            kwlblNavigatorVersion.Text = null;

            kwlblRibbonVersion.Text = null;

            kwlblToolkitVersion.Text = null;

            kwlblWorkspaceVersion.Text = null;

            _toolkitType = ToolkitType.Stable;

            SetLogo(_toolkitType);

            Size = new Size(659, 122);
        }

        #endregion

        #region Implementation

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

        private void klwlblDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start(@"https://github.com/Krypton-Suite/Standard-Toolkit");
            }
            catch (Exception exception)
            {
                KryptonExceptionHandler.CaptureException(exception, showStackTrace: GlobalStaticValues.DEFAULT_USE_STACK_TRACE);
            }
        }

        private void GetVersions()
        {
            string dockingLocation =
                $@"{Path.GetDirectoryName(Application.ExecutablePath)}\{GlobalStaticValues.DEFAULT_DOCKING_FILE}";

            string navigatorLocation =
                $@"{Path.GetDirectoryName(Application.ExecutablePath)}\{GlobalStaticValues.DEFAULT_NAVIGATOR_FILE}";

            string ribbonLocation =
                $@"{Path.GetDirectoryName(Application.ExecutablePath)}\{GlobalStaticValues.DEFAULT_RIBBON_FILE}";

            string toolkitLocation =
                $@"{Path.GetDirectoryName(Application.ExecutablePath)}\{GlobalStaticValues.DEFAULT_TOOLKIT_FILE}";

            string workspaceLocation =
                $@"{Path.GetDirectoryName(Application.ExecutablePath)}\{GlobalStaticValues.DEFAULT_WORKSPACE_FILE}";

            FileVersionInfo dockingFileVersionInfo = FileVersionInfo.GetVersionInfo(dockingLocation);

            FileVersionInfo navigatorFileVersionInfo = FileVersionInfo.GetVersionInfo(navigatorLocation);

            FileVersionInfo ribbonFileVersionInfo = FileVersionInfo.GetVersionInfo(ribbonLocation);

            FileVersionInfo toolkitFileVersionInfo = FileVersionInfo.GetVersionInfo(toolkitLocation);

            FileVersionInfo workspaceFileVersionInfo = FileVersionInfo.GetVersionInfo(workspaceLocation);

            if (File.Exists(dockingLocation))
            {
                kwlblDockingVersion.Text = string.Format(kwlblDockingVersion.Text, dockingFileVersionInfo.FileVersion);
            }
            else
            {
                kwlblDockingVersion.Text = $@"Cannot find file: '{GlobalStaticValues.DEFAULT_DOCKING_FILE}'";
            }

            if (File.Exists(navigatorLocation))
            {
                kwlblNavigatorVersion.Text = string.Format(kwlblNavigatorVersion.Text, navigatorFileVersionInfo.FileVersion);
            }
            else
            {
                kwlblNavigatorVersion.Text = $@"Cannot find file: '{GlobalStaticValues.DEFAULT_WORKSPACE_FILE}'";
            }

            if (File.Exists(ribbonLocation))
            {
                kwlblRibbonVersion.Text = string.Format(kwlblRibbonVersion.Text, ribbonFileVersionInfo.FileVersion);
            }
            else
            {
                kwlblRibbonVersion.Text = $@"Cannot find file: '{GlobalStaticValues.DEFAULT_RIBBON_FILE}'";
            }

            if (File.Exists(toolkitLocation))
            {
                kwlblToolkitVersion.Text = string.Format(kwlblToolkitVersion.Text, toolkitFileVersionInfo.FileVersion);
            }
            else
            {
                kwlblToolkitVersion.Text = $@"Cannot find file: '{GlobalStaticValues.DEFAULT_TOOLKIT_FILE}'";
            }

            if (File.Exists(workspaceLocation))
            {
                kwlblWorkspaceVersion.Text = string.Format(kwlblWorkspaceVersion.Text, workspaceFileVersionInfo.FileVersion);
            }
            else
            {
                kwlblWorkspaceVersion.Text = $@"Cannot find file: '{GlobalStaticValues.DEFAULT_WORKSPACE_FILE}'";
            }
        }

        private void SetLogoSpan(bool showThemeOption)
        {
            if (showThemeOption)
            {
                tlpnlContent.SetRowSpan(kpbxLogo, 10);

                TableLayoutPanelCellPosition currentThemeLabelCellPosition =
                    tlpnlContent.GetCellPosition(kwlblCurrentTheme);

                TableLayoutPanelCellPosition currentThemeCellPosition = tlpnlContent.GetCellPosition(ktcmbCurrentTheme);

                int labelRowHeight = tlpnlContent.GetRowHeights()[currentThemeLabelCellPosition.Row];

                int comboBoxRowHeight = tlpnlContent.GetRowHeights()[currentThemeCellPosition.Row];

                int addedHeight = labelRowHeight + comboBoxRowHeight;

                Size = new Size(659, addedHeight);
            }
            else
            {
                tlpnlContent.SetRowSpan(kpbxLogo, 1);

                Size = new Size(659, 122);
            }
        }

        #endregion

        #region Protected Overrides

        protected override void OnPaint(PaintEventArgs e)
        {
            kwlblDockingVersion.Visible = _showDockingVersion;

            kwlblNavigatorVersion.Visible = _showNavigatorVersion;

            kwlblRibbonVersion.Visible = _showRibbonVersion;

            kwlblToolkitVersion.Visible = _showToolkitVersion;

            kwlblWorkspaceVersion.Visible = _showWorkspaceVersion;

            kwlblCurrentTheme.Visible = _showThemeOption;

            ktcmbCurrentTheme.Visible = _showThemeOption;

            base.OnPaint(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            //GetVersions();

            base.OnLoad(e);
        }

        #endregion

        #region Removed Designer Visibility

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Color BackColor { get; set; }

        #endregion
    }
}
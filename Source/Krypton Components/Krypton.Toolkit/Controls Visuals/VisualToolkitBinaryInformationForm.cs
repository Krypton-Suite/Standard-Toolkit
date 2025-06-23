#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV) & Giduac, et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal partial class VisualToolkitBinaryInformationForm : KryptonForm
{
    #region Instance Fields

    private bool? _showChangeLogButton;

    private Dictionary<string, FileVersionInfo> _fileVersionInfos = new Dictionary<string, FileVersionInfo>();

    private static readonly FileVersionInfo[] _fileVersionInfoCache = new FileVersionInfo[GlobalStaticValues.TOOLKIT_DLL_NAMES.Length];

    private readonly ToolkitSupportType _toolkitType;

    private readonly string _applicationBaseInstallPath = AppDomain.CurrentDomain.BaseDirectory;

    #endregion

    #region Identity

    public VisualToolkitBinaryInformationForm(ToolkitSupportType toolkitType, bool? showChangeLogButton)
    {
        //SetInheritedControlOverride();

        InitializeComponent();

        _toolkitType = toolkitType;

        _showChangeLogButton = showChangeLogButton;
    }

    #endregion

    #region Implementation

    private void VisualToolkitBinaryInformationForm_Load(object sender, EventArgs e)
    {
        kbtnChangelog.Visible = _showChangeLogButton ?? false;

        kbtnChangelog.Text = KryptonManager.Strings.MiscellaneousStrings.ChangeLogText;

        PopulateBinaryInformation(_toolkitType);
    }

    private void kbtnOk_Click(object sender, EventArgs e) => DialogResult = DialogResult.OK;

    private void PopulateBinaryInformation(ToolkitSupportType toolkitType)
    {
        DisplayFileInformation();

        UpdateLabelInformation();

        switch (toolkitType)
        {
            case ToolkitSupportType.Canary:
                pbxIcon.Image = ToolkitLogoResources.Krypton_48_x_48_Canary;

                kwlblTitle.Text =
                    $@"{KryptonManager.Strings.MiscellaneousStrings.ToolkitInformationText}: {KryptonManager.Strings.MiscellaneousStrings.CanaryText}";
                break;
            case ToolkitSupportType.Nightly:
                pbxIcon.Image = ToolkitLogoResources.Krypton_48_x_48_Nightly;

                kwlblTitle.Text =
                    $@"{KryptonManager.Strings.MiscellaneousStrings.ToolkitInformationText}: {KryptonManager.Strings.MiscellaneousStrings.NightlyText}";
                break;
            case ToolkitSupportType.LongTermSupport:
                pbxIcon.Image = ToolkitLogoResources.Krypton_48_x_48_LTS;

                kwlblTitle.Text =
                    $@"{KryptonManager.Strings.MiscellaneousStrings.ToolkitInformationText}: {KryptonManager.Strings.MiscellaneousStrings.LongTermStableText}";
                break;
            case ToolkitSupportType.Stable:
            default:
                pbxIcon.Image = ToolkitLogoResources.Krypton_48_x_48_Stable;

                kwlblTitle.Text =
                    $@"{KryptonManager.Strings.MiscellaneousStrings.ToolkitInformationText}: {KryptonManager.Strings.MiscellaneousStrings.StableText}";
                break;
        }
    }

    private void UpdateLabelInformation()
    {
        kwlblDockingTitle.Text = @"Krypton Docking:";
        kwlblNavigatorTitle.Text = @"Krypton Navigator:";
        kwlblRibbonTitle.Text = @"Krypton Ribbon:";
        kwlblToolkitTitle.Text = @"Krypton Toolkit:";
        kwlblWorkspaceTitle.Text = @"Krypton Workspace:";
    }

    private void DisplayFileInformation()
    {
        for (int i = 0; i < GlobalStaticValues.TOOLKIT_DLL_NAMES.Length; i++)
        {
            string path = Path.Combine(_applicationBaseInstallPath, GlobalStaticValues.TOOLKIT_DLL_NAMES[i]);

            _fileVersionInfoCache[i] = (GetFileVersionInfo(path) ?? null)!;
        }

        kwlblDockingFileInformation.Text = _fileVersionInfoCache[0]?.FileVersion ??
                                           $"{KryptonManager.Strings.MiscellaneousStrings.FileNotFoundText}";

        kwlblNavigatorFileInformation.Text = _fileVersionInfoCache[1]?.FileVersion ??
                                             $"{KryptonManager.Strings.MiscellaneousStrings.FileNotFoundText}";

        kwlblRibbonFileInformation.Text = _fileVersionInfoCache[2]?.FileVersion ??
                                          $"{KryptonManager.Strings.MiscellaneousStrings.FileNotFoundText}";

        kwlblToolkitFileInformation.Text = _fileVersionInfoCache[3]?.FileVersion ??
                                           $"{KryptonManager.Strings.MiscellaneousStrings.FileNotFoundText}";

        kwlblWorkspaceFileInformation.Text = _fileVersionInfoCache[4]?.FileVersion ??
                                             $"{KryptonManager.Strings.MiscellaneousStrings.FileNotFoundText}";
    }

    private static FileVersionInfo? GetFileVersionInfo(string filePath) => File.Exists(filePath) 
        ? FileVersionInfo.GetVersionInfo(filePath) 
        : null;

    private void kbtnChangelog_Click(object sender, EventArgs e)
    {
        switch (_toolkitType)
        {
            case ToolkitSupportType.Canary:
                GeneralToolkitUtilities.Start(@"https://github.com/Krypton-Suite/Standard-Toolkit/blob/canary/Documents/Changelog/Changelog.md");
                break;
            case ToolkitSupportType.Nightly:
                GeneralToolkitUtilities.Start(@"https://github.com/Krypton-Suite/Standard-Toolkit/blob/alpha/Documents/Changelog/Changelog.md");
                break;
            case ToolkitSupportType.LongTermSupport:
                GeneralToolkitUtilities.Start(@"https://github.com/Krypton-Suite/Standard-Toolkit/blob/V85-LTS/Documents/Help/Changelog.md");
                break;
            case ToolkitSupportType.Stable:
            default:
                GeneralToolkitUtilities.Start(@"https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/Documents/Changelog/Changelog.md");
                break;
        }
    }

    #endregion
}
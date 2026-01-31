#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV) & Giduac, et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal partial class VisualChangeLogForm : KryptonForm
{
    #region Instance Fields

    private ToolkitSupportType _toolkitType;

    #endregion

    #region Identity

    public VisualChangeLogForm(ToolkitSupportType toolkitType)
    {
        InitializeComponent();

        _toolkitType = toolkitType;
    }

    #endregion

    private void VisualChangeLogForm_Load(object sender, EventArgs e)
    {
        switch (_toolkitType)
        {
            case ToolkitSupportType.Canary:
                break;
            case ToolkitSupportType.Nightly:
                break;
            case ToolkitSupportType.LongTermSupport:
                break;
            case ToolkitSupportType.Stable:
            default:
                kwbChangeLog.Navigate(@"https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/Documents/Changelog/Changelog.md");
                break;
        }
    }
}
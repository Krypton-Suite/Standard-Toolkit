#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2023 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>A <see cref="KryptonCommand"/> for the <see cref="PaletteButtonSpecStyle.PageSetup"/> button spec.</summary>
[Category(@"code")]
[ToolboxItem(false)]
[Description(@"For use with the 'Page Setup' ButtonSpec style.")]
[DesignerCategory(@"code")]
[Obsolete("Use KryptonCommand with CommandType = KryptonCommandType.IntegratedToolBarPageSetupCommand instead. This type will be removed in version 120 LTS.")]
public class KryptonIntegratedToolbarPageSetupCommand : KryptonButtonSpecTypedCommand
{
    [DefaultValue(null)]
    [Description(@"Access to the page setup button spec.")]
    [AllowNull]
    public ButtonSpecAny? ToolBarPageSetupButton
    {
        get => ToolBarButton;
        set => ToolBarButton = value;
    }

    public KryptonIntegratedToolbarPageSetupCommand()
        : base(KryptonCommandType.IntegratedToolBarPageSetupCommand)
    {
    }
}

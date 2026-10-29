#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2023 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>A <see cref="KryptonCommand"/> for the <see cref="PaletteButtonSpecStyle.SaveAs"/> button spec.</summary>
[Category(@"code")]
[ToolboxItem(false)]
[Description(@"For use with the 'Save As' ButtonSpec style.")]
[DesignerCategory(@"code")]
[Obsolete("Use KryptonCommand with CommandType = KryptonCommandType.IntegratedToolBarSaveAsCommand instead. This type will be removed in version 120 LTS.")]
public class KryptonIntegratedToolbarSaveAsCommand : KryptonButtonSpecTypedCommand
{
    [DefaultValue(null)]
    [Description(@"Access to the save as button spec.")]
    [AllowNull]
    public ButtonSpecAny? ToolBarSaveAsButton
    {
        get => ToolBarButton;
        set => ToolBarButton = value;
    }

    public KryptonIntegratedToolbarSaveAsCommand()
        : base(KryptonCommandType.IntegratedToolBarSaveAsCommand)
    {
    }
}

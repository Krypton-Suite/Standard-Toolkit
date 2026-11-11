#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2023 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>A <see cref="KryptonCommand"/> for the <see cref="PaletteButtonSpecStyle.Cut"/> button spec.</summary>
[Category(@"code")]
[ToolboxItem(false)]
[Description(@"For use with the 'Cut' ButtonSpec style.")]
[DesignerCategory(@"code")]
[Obsolete("Use KryptonCommand with CommandType = KryptonCommandType.IntegratedToolBarCutCommand instead. This type will be removed in version 120 LTS.")]
public class KryptonIntegratedToolbarCutCommand : KryptonButtonSpecTypedCommand
{
    [DefaultValue(null)]
    [Description(@"Access to the cut button spec.")]
    [AllowNull]
    public ButtonSpecAny? ToolBarCutButton
    {
        get => ToolBarButton;
        set => ToolBarButton = value;
    }

    public KryptonIntegratedToolbarCutCommand()
        : base(KryptonCommandType.IntegratedToolBarCutCommand)
    {
    }
}

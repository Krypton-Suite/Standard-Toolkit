#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2023 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// A <see cref="KryptonCommand"/> for the <see cref="PaletteButtonSpecStyle.New"/> button spec.
/// </summary>
[Category(@"code")]
[ToolboxItem(false)]
[Description(@"For use with the 'New' ButtonSpec style.")]
[DesignerCategory(@"code")]
[Obsolete("Use KryptonCommand with CommandType = KryptonCommandType.IntegratedToolBarNewCommand instead. This type will be removed in version 120 LTS.")]
public class KryptonIntegratedToolbarNewCommand : KryptonButtonSpecTypedCommand
{
    #region Public

    /// <summary>Gets or sets the new toolbar button spec.</summary>
    [DefaultValue(null)]
    [Description(@"Access to the new button spec.")]
    [AllowNull]
    public ButtonSpecAny? ToolBarNewButton
    {
        get => ToolBarButton;
        set => ToolBarButton = value;
    }

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="KryptonIntegratedToolbarNewCommand"/> class.</summary>
    public KryptonIntegratedToolbarNewCommand()
        : base(KryptonCommandType.IntegratedToolBarNewCommand)
    {
    }

    #endregion
}

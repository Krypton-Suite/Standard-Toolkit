#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>A <see cref="KryptonCommand"/> for the <see cref="PaletteButtonSpecStyle.FormHelp"/> button spec.</summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonHelpCommand), @"ToolboxBitmaps.KryptonHelpCommand.bmp")]
[Description(@"For use with the 'Help' ButtonSpec style.")]
[DesignerCategory(@"code")]
[Obsolete("Use KryptonCommand with CommandType = KryptonCommandType.HelpCommand instead. This type will be removed in version 120 LTS.")]
public class KryptonHelpCommand : KryptonButtonSpecTypedCommand
{
    #region Public

    /// <summary>Gets or sets the help button spec.</summary>
    [DefaultValue(null)]
    [Description(@"Access to the help button spec.")]
    [AllowNull]
    public ButtonSpecAny? HelpButton
    {
        get => AssignedButtonSpec as ButtonSpecAny;
        set => AssignedButtonSpec = value;
    }

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="KryptonHelpCommand"/> class.</summary>
    public KryptonHelpCommand()
        : base(KryptonCommandType.HelpCommand)
    {
    }

    #endregion
}

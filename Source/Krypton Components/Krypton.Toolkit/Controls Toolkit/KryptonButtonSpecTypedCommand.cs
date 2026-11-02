#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Base <see cref="KryptonCommand"/> that configures palette images and text for a <see cref="PaletteButtonSpecStyle"/>.
/// </summary>
public abstract class KryptonButtonSpecTypedCommand : KryptonCommand
{
    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonButtonSpecTypedCommand"/> class.
    /// </summary>
    /// <param name="commandType">The command type that defines the button spec style.</param>
    protected KryptonButtonSpecTypedCommand(KryptonCommandType commandType) => CommandType = commandType;

    #endregion

    #region Protected

    /// <summary>
    /// Gets or sets the linked toolbar button specification.
    /// </summary>
    protected ButtonSpecAny? ToolBarButton
    {
        get => AssignedButtonSpec as ButtonSpecAny;
        set => AssignedButtonSpec = value;
    }

    #endregion

    #region Public

    /// <summary>Gets the active image from the current palette.</summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Image? ActiveImage => GetButtonSpecImage(KryptonManager.CurrentGlobalPalette, PaletteState.Tracking);

    /// <summary>Gets the disabled image from the current palette.</summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Image? DisabledImage => GetButtonSpecImage(KryptonManager.CurrentGlobalPalette, PaletteState.Disabled);

    /// <summary>Gets the normal image from the current palette.</summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Image? NormalImage => GetButtonSpecImage(KryptonManager.CurrentGlobalPalette, PaletteState.Normal);

    /// <summary>Gets the pressed image from the current palette.</summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Image? PressedImage => GetButtonSpecImage(KryptonManager.CurrentGlobalPalette, PaletteState.Pressed);

    #endregion
}

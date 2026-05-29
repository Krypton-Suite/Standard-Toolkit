#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Redirect requests for image/text colors to remap.
/// </summary>
public class ButtonSpecRemapByContentView : ButtonSpecRemapByContentBase
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the ButtonSpecRemapByContentView class.
    /// </summary>
    /// <param name="target">Initial palette target for redirection.</param>
    /// <param name="buttonSpec">Reference to button specification.</param>
    public ButtonSpecRemapByContentView(PaletteBase target,
        [DisallowNull] ButtonSpec buttonSpec)
        : base(target, buttonSpec)
    {
    }
    #endregion

    #region Foreground
    /// <summary>
    /// Gets and sets the foreground to use for color map redirection.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ViewDrawContent? Foreground { get; set; }

    #endregion

    #region PaletteContent
    /// <summary>
    /// Gets the palette content to use for remapping.
    /// </summary>
    public override IPaletteContent? PaletteContent => Foreground?.GetPalette();

    #endregion

    #region PaletteState
    /// <summary>
    /// Gets the state of the remapping area
    /// </summary>
    public override PaletteState PaletteState => Foreground?.State ?? PaletteState.Normal;

    #endregion
}
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
public class ButtonSpecRemapByContentCache : ButtonSpecRemapByContentBase
{
    #region Instance Fields
    private IPaletteContent? _paletteContent;
    private PaletteState _paletteState;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ButtonSpecRemapByContentCache class.
    /// </summary>
    /// <param name="target">Initial palette target for redirection.</param>
    /// <param name="buttonSpec">Reference to button specification.</param>
    public ButtonSpecRemapByContentCache(PaletteBase target,
        ButtonSpec buttonSpec)
        : base(target, buttonSpec)
    {
    }
    #endregion

    #region SetPaletteContent
    /// <summary>
    /// Set the palette content to use for remapping.
    /// </summary>
    /// <param name="paletteContent">Palette for requesting foreground colors.</param>
    public void SetPaletteContent(IPaletteContent? paletteContent) => _paletteContent = paletteContent;
    #endregion

    #region SetPaletteState
    /// <summary>
    /// Set the palette state of the remapping element.
    /// </summary>
    /// <param name="paletteState">Palette state.</param>
    public void SetPaletteState(PaletteState paletteState) => _paletteState = paletteState;
    #endregion

    #region PaletteContent
    /// <summary>
    /// Gets the palette content to use for remapping.
    /// </summary>
    public override IPaletteContent? PaletteContent => _paletteContent;

    #endregion

    #region PaletteState
    /// <summary>
    /// Gets the state of the remapping area
    /// </summary>
    public override PaletteState PaletteState => _paletteState;

    #endregion
}
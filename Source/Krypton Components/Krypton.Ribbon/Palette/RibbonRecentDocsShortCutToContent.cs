#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

namespace Krypton.Ribbon;

internal class RibbonRecentDocsShortcutToContent : RibbonRecentDocsEntryToContent
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the RibbonRecentDocsShortcutToContent class.
    /// </summary>
    /// <param name="ribbonGeneral">Source for general ribbon settings.</param>
    /// <param name="ribbonRecentDocEntryText">Source for ribbon recent document entry settings.</param>
    public RibbonRecentDocsShortcutToContent(PaletteRibbonGeneral ribbonGeneral,
        IPaletteRibbonText ribbonRecentDocEntryText)
        : base(ribbonGeneral, ribbonRecentDocEntryText)
    {
    }
    #endregion

    #region IPaletteContent
    /// <summary>
    /// Gets the prefix drawing setting for short text.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteTextPrefix value.</returns>
    public override PaletteTextHotkeyPrefix GetContentShortTextPrefix(PaletteState state) => PaletteTextHotkeyPrefix.Show;

    #endregion
}
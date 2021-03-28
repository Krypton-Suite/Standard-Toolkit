using Krypton.Toolkit;

namespace Krypton.Ribbon
{
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
        public override PaletteTextHotkeyPrefix GetContentShortTextPrefix(PaletteState state)
        {
            return PaletteTextHotkeyPrefix.Show;
        }
        #endregion
    }
}

namespace Krypton.Toolkit
{
    /// <summary>
    /// Provide inheritance of palette ribbon text properties.
    /// </summary>
    public abstract class PaletteRibbonTextInherit : GlobalId,
                                                     IPaletteRibbonText
    {
        #region IPaletteRibbonText
        /// <summary>
        /// Gets the tab color for the item text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public abstract Color GetRibbonTextColor(PaletteState state);        
        #endregion
    }
}

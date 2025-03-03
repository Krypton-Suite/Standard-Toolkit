namespace Krypton.Toolkit
{
    /// <summary>
    /// Represents a Krypton theme with metadata and style properties.
    /// </summary>
    public class KryptonTheme
    {
        /// <summary>
        /// The internal palette mode name corresponding to the theme.
        /// </summary>
        public string BasePaletteMode { get; set; } = "Office 2007";

        /// <summary>
        /// A human-readable name for the theme.
        /// </summary>
        public string DisplayName { get; set; } = "Office 2007";

        /// <summary>
        /// The hotkey prefix setting that determines how shortcut keys are displayed.
        /// </summary>
        public string HotKeyPrefix { get; set; } = "None";

        /// <summary>
        /// A dictionary storing the color mappings for the theme.
        /// </summary>
        public Dictionary<string, Color> Colors { get; set; } = new();

        /// <summary>
        /// A dictionary storing embedded images for the theme.
        /// </summary>
        public Dictionary<string, Image> Images { get; set; } = new();

        /// <summary>
        /// A dictionary storing font data in a serialized format.
        /// </summary>
        public Dictionary<string, byte[]> Fonts { get; set; } = new();
    }
}
#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV) & Giduac, et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// Provides functionality to load embedded Krypton themes from the Krypton.Base.Themes.dll assembly.
    /// </summary>
    public static class KryptonThemeLoader
    {
        /// <summary>
        /// Retrieves a list of available embedded themes.
        /// </summary>
        /// <returns>List of theme names found in the embedded resources.</returns>
        public static List<string> GetAvailableThemes()
        {
            Assembly assembly = Assembly.Load("Krypton.Base.Palettes");
            List<string> themes = new List<string>();

            foreach (var resource in assembly.GetManifestResourceNames())
            {
                if (resource.EndsWith(".ktheme"))
                {
                    string themeName = resource.Replace($"{GlobalStaticValues.EMBEDDED_THEME_NAMESPACE}.", "").Replace(".ktheme", "");
                    themes.Add(themeName);
                }
            }

            return themes;
        }

        /// <summary>
        /// Loads an embedded theme from the Krypton.Base.Themes.dll assembly.
        /// </summary>
        /// <param name="themeName">The name of the theme to load.</param>
        /// <returns>A KryptonTheme object representing the loaded theme.</returns>
        /// <exception cref="FileNotFoundException">Thrown when the specified theme cannot be found.</exception>
        public static KryptonTheme LoadEmbeddedTheme(string themeName)
        {
            Assembly assembly = Assembly.Load("Krypton.Base.Palettes");
            string resourceName = $"{GlobalStaticValues.EMBEDDED_THEME_NAMESPACE}.{themeName}.ktheme";

            using (Stream? stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new FileNotFoundException($"Theme {themeName} not found in Krypton.Base.Palettes.dll");
                }

                XmlDocument doc = new XmlDocument();
                doc.Load(stream);
                return ParseTheme(doc);
            }
        }

        /// <summary>
        /// Parses an XML document to create a KryptonTheme object.
        /// </summary>
        /// <param name="doc">The XML document representing the theme.</param>
        /// <returns>A KryptonTheme object with parsed properties.</returns>
        /// <exception cref="Exception">Thrown if the XML format is invalid.</exception>
        private static KryptonTheme ParseTheme(XmlDocument doc)
        {
            KryptonTheme theme = new KryptonTheme();
            XmlElement? root = doc.DocumentElement;

            if (root == null)
            {
                throw new Exception("Invalid theme format.");
            }

            // Extract BasePaletteMode
            XmlNode? basePaletteNode = root.SelectSingleNode("BasePaletteMode");
            theme.BasePaletteMode = basePaletteNode?.Attributes?["Value"]?.Value ?? "Unknown";

            // Extract Display Name
            XmlNode? displayNameNode = root.SelectSingleNode("DisplayName");
            theme.DisplayName = displayNameNode?.InnerText ?? theme.BasePaletteMode;

            // Extract Colors
            XmlNodeList? colorNodes = root.SelectNodes("Resources/Colors/Color");
            foreach (XmlNode colorNode in colorNodes)
            {
                string key = colorNode.Attributes["key"].Value;
                Color value = ColorTranslator.FromHtml(colorNode.Attributes["value"].Value);
                theme.Colors[key] = value;
            }

            // Extract Images
            XmlNodeList? imageNodes = root.SelectNodes("Resources/Images/Image");
            foreach (XmlNode imageNode in imageNodes)
            {
                string key = imageNode.Attributes["key"]!.Value;
                byte[] imageData = Convert.FromBase64String(imageNode.InnerText);
                using (MemoryStream ms = new MemoryStream(imageData))
                {
                    theme.Images[key] = Image.FromStream(ms);
                }
            }

            // Extract Fonts
            XmlNodeList fontNodes = root.SelectNodes("Resources/Fonts/Font");
            foreach (XmlNode fontNode in fontNodes)
            {
                string key = fontNode.Attributes["key"].Value;
                byte[] fontData = Convert.FromBase64String(fontNode.InnerText);
                theme.Fonts[key] = fontData;
            }

            return theme;
        }
    }
}

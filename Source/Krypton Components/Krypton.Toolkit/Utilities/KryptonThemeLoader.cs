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
    public static class KryptonThemeLoader
    {
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

            return theme;
        }
    }
}

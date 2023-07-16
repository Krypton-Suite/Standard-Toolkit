#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    internal class PaletteUpgradeUtility
    {
        #region Static Fields

        private const int MINIMUM_SUPPORTED_PALETTE_VERSION = 6;

        private const int MAXMUM_SUPPORTED_PALETTE_VERSION = 19;

        #endregion

        #region Public

        public int InputVersionNumber { get; set; }

        #endregion

        #region Identity

        public PaletteUpgradeUtility()
        {

        }

        #endregion

        #region Implementation

        /// <summary>Upgrades the palette file.</summary>
        /// <param name="originalPaletteFilePath">The original palette file path.</param>
        /// <param name="upgradedPaletteFilePath">The upgraded palette file path.</param>
        public static void UpgradePaletteFile(string originalPaletteFilePath, string? upgradedPaletteFilePath)
        {
            PaletteUpgradeUtility upgradeUtility = new();

            if (string.IsNullOrEmpty(upgradedPaletteFilePath))
            {
                try
                {
                    StreamReader reader = new StreamReader(originalPaletteFilePath);

                    string endOfFile = reader.ReadToEnd();

                    reader.Close();

                    reader.Dispose();

                    if (upgradeUtility.GetInputVersionNumber() < MINIMUM_SUPPORTED_PALETTE_VERSION)
                    {
                        var v6Schema = new XslCompiledTransform();

                        v6Schema.Load(new XmlTextReader(new StringReader(PaletteSchemaResources.Version6PaletteSchema)));

                        endOfFile = TransformXML(v6Schema, endOfFile);
                    }
                    else if (upgradeUtility.GetInputVersionNumber() <= MAXMUM_SUPPORTED_PALETTE_VERSION)
                    {
                        var readCurrentSchema = new StringReader(PaletteSchemaResources.CurrentPaletteSchema);

                        var xmlReader = XmlReader.Create(readCurrentSchema);

                        var currentSchema = new XslCompiledTransform();

                        currentSchema.Load(xmlReader);

                        endOfFile = TransformXML(currentSchema, endOfFile);
                    }

                    string tempPath =
                        $"{Application.ExecutablePath}\\Upgraded Palette {DateTime.Now.ToShortDateString()}.xml";

                    if (!File.Exists(tempPath))
                    {
                        File.Create(tempPath);
                    }

                    var writer = new StreamWriter(tempPath);

                    writer.WriteLine("<?xml version=\"1.0\"?>");

                    writer.Write(endOfFile);

                    writer.Flush();

                    writer.Close();

                    object[] text = { "Input file: ", originalPaletteFilePath, "\nOutput file: ", tempPath, "\n\nUpgrade from version '", upgradeUtility.InputVersionNumber, "' to version '", MAXMUM_SUPPORTED_PALETTE_VERSION.ToString(), "' has succeeded." };

                    KryptonMessageBox.Show(string.Concat(text), "Upgrade Success", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
                }
                catch (Exception e)
                {
                    ExceptionHandler.CaptureException(e);
                }
            }
            else
            {
                try
                {
                    StreamReader reader = new StreamReader(originalPaletteFilePath);

                    string endOfFile = reader.ReadToEnd();

                    reader.Close();

                    if (upgradeUtility.GetInputVersionNumber() < MINIMUM_SUPPORTED_PALETTE_VERSION)
                    {
                        var v6Schema = new XslCompiledTransform();

                        v6Schema.Load(new XmlTextReader(new StringReader(PaletteSchemaResources.Version6PaletteSchema)));

                        endOfFile = TransformXML(v6Schema, endOfFile);
                    }
                    else if (upgradeUtility.GetInputVersionNumber() <= MAXMUM_SUPPORTED_PALETTE_VERSION)
                    {
                        var readCurrentSchema = new StringReader(PaletteSchemaResources.CurrentPaletteSchema);

                        var xmlReader = XmlReader.Create(readCurrentSchema);

                        var currentSchema = new XslCompiledTransform();

                        currentSchema.Load(xmlReader);

                        endOfFile = TransformXML(currentSchema, endOfFile);
                    }

                    var writer = new StreamWriter(upgradedPaletteFilePath);

                    writer.WriteLine("<?xml version=\"1.0\"?>");

                    writer.Write(endOfFile);

                    writer.Flush();

                    writer.Close();

                    if (upgradedPaletteFilePath != null)
                    {
                        object[] text = { "Input file: ", originalPaletteFilePath, "\nOutput file: ", upgradedPaletteFilePath, "\n\nUpgrade from version '", upgradeUtility.InputVersionNumber, "' to version '", MAXMUM_SUPPORTED_PALETTE_VERSION.ToString(), "' has succeeded." };

                        KryptonMessageBox.Show(string.Concat(text), "Upgrade Success", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
                    }
                }
                catch (Exception e)
                {
                    ExceptionHandler.CaptureException(e);
                }
            }
        }

        private static string TransformXML(XslCompiledTransform schema, string output)
        {
            StringReader reader = new StringReader(output);

            StringWriter writer = new();

            XmlTextReader xmlReader = new XmlTextReader(reader);

            XmlTextWriter xmlWriter = new XmlTextWriter(writer)
            {
                Formatting = Formatting.Indented,
                Indentation = 4
            };

            schema.Transform(xmlReader, xmlWriter);

            return writer.ToString();
        }

        #endregion

        #region Setters and Getters
        /// <summary>Sets the InputVersionNumber to the value of value.</summary>
        /// <param name="value">The desired value of InputVersionNumber.</param>
        private void SetInputVersionNumber(int value) => InputVersionNumber = value;

        /// <summary>Returns the value of the InputVersionNumber.</summary>
        /// <returns>The value of the InputVersionNumber.</returns>
        private int GetInputVersionNumber() => InputVersionNumber;

        #endregion
    }
}
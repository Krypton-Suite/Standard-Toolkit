﻿namespace Krypton.Toolkit
{
    /// <summary>
    /// Explicit helpers for XML Data export and import
    /// </summary>
    public static class XmlHelper
    {

#pragma warning disable CS1570 // XML comment has badly formed XML
        /// <summary>
        /// Only persist the provided name/value pair as an Xml attribute if the value is not null/empty and not the default.
        /// </summary>
        /// <param name="xmlWriter">Xml writer to save information into.</param>
        /// <param name=(@"Name")>Attribute name.</param>
        /// <param name="value">Attribute value.</param>
        /// <param name="defaultValue">Default value.</param>
        public static void TextToXmlAttribute(XmlWriter xmlWriter, string name, string value, string defaultValue = @"")
#pragma warning restore CS1570 // XML comment has badly formed XML
        {
            if (!string.IsNullOrEmpty(value) && (value != defaultValue))
            {
                xmlWriter.WriteAttributeString(name, value);
            }
        }


#pragma warning disable CS1570 // XML comment has badly formed XML
        /// <summary>
        /// Read the named attribute value but if no attribute is found then return the provided default.
        /// </summary>
        /// <param name="xmlReader">Xml reader to load information from.</param>
        /// <param name=(@"Name")>Attribute name.</param>
        /// <param name="defaultValue">Default value.</param>
        /// <returns>Returns the provided default.</returns>
        public static string XmlAttributeToText(XmlReader xmlReader, string name, string defaultValue = @"")
#pragma warning restore CS1570 // XML comment has badly formed XML
        {
            try
            {
                var ret = xmlReader.GetAttribute(name) ?? defaultValue;

                return ret;
            }
            catch
            {
                return defaultValue;
            }
        }



#pragma warning disable CS1570 // XML comment has badly formed XML
        /// <summary>
        /// Convert a Image to a culture invariant string value.
        /// </summary>
        /// <param name="xmlWriter">Xml writer to save information into.</param>
        /// <param name=(@"Name")>Name of image to save.</param>
        /// <param name="image">Image to persist.</param>
        public static void ImageToXmlCData(XmlWriter xmlWriter, string name, Bitmap image)
#pragma warning restore CS1570 // XML comment has badly formed XML
        {
            // Only store if we have an actual image to persist
            if (image != null)
            {
                // Convert the Image into base64 so it can be used in xml
                MemoryStream memory = new();
                image.Save(memory, image.RawFormat);
                memory.Position = 0;
                var base64 = Convert.ToBase64String(memory.ToArray());

                // Store the base64 Hex as a CDATA inside the element
                xmlWriter.WriteStartElement(name);
                xmlWriter.WriteCData(base64);
                xmlWriter.WriteEndElement();
            }
        }

        /// <summary>
        /// Convert a culture invariant string value into an Image.
        /// </summary>
        /// <param name="xmlReader">Xml reader to load information from.</param>
        /// <returns>Image that was recreated.</returns>
        public static Bitmap XmlCDataToImage(XmlReader xmlReader)
        {
            // Convert the content of the element into base64
            var bytes = Convert.FromBase64String(xmlReader.ReadContentAsString());

            // Convert the bytes back into an Image
            using MemoryStream memory = new(bytes);
            try
            {
                return new Bitmap(memory);
            }
            catch
            {
                // Do the old image way
                // SYSLIB0011: BinaryFormatter serialization is obsolete
#pragma warning disable SYSLIB0011
                BinaryFormatter formatter = new();
                var img = (Image)formatter.Deserialize(memory);
#pragma warning restore SYSLIB0011
                return new Bitmap(img);
            }
        }
    }
}

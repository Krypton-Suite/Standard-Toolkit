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
/// Explicit helpers for XML Data export and import
/// </summary>
public static class XmlHelper
{
    /// <summary>
    /// Only persist the provided name/value pair as an Xml attribute if the value is not null/empty and not the default.
    /// </summary>
    /// <param name="xmlWriter">Xml writer to save information into.</param>
    /// <param name="name">Attribute name.</param>
    /// <param name="value">Attribute value.</param>
    /// <param name="defaultValue">Default value.</param>
    public static void TextToXmlAttribute(XmlWriter xmlWriter, string name, string? value, string defaultValue = @"")
    {
        if (!string.IsNullOrEmpty(value) && (value != defaultValue))
        {
            xmlWriter.WriteAttributeString(name, value);
        }
    }

    /// <summary>
    /// Read the named attribute value but if no attribute is found then return the provided default.
    /// </summary>
    /// <param name="xmlReader">Xml reader to load information from.</param>
    /// <param name="name">Attribute name.</param>
    /// <param name="defaultValue">Default value.</param>
    /// <returns></returns>
    public static string XmlAttributeToText(XmlReader xmlReader, string name, string defaultValue = @"")
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


    /// <summary>
    /// Convert a Image to a culture invariant string value.
    /// </summary>
    /// <param name="xmlWriter">Xml writer to save information into.</param>
    /// <param name="name">Name of image to save.</param>
    /// <param name="image">Image to persist.</param>
    public static void ImageToXmlCData(XmlWriter xmlWriter, string name, Bitmap? image)
    {
        // Only store if we have an actual image to persist
        if (image != null)
        {
            // Convert the Image into base64 so it can be used in xml
            using var memory = new MemoryStream();
            image.Save(memory, ImageFormat.Png);
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
    public static Bitmap? XmlCDataToImage(XmlReader xmlReader)
    {
        try
        {
            // Convert the content of the element into base64
            var bytes = Convert.FromBase64String(xmlReader.ReadContentAsString());

            // Convert the bytes back into an Image
            using var memory = new MemoryStream(bytes);

            return new Bitmap(memory);
        }
        catch (Exception e)
        {
            KryptonExceptionHandler.CaptureException(e);

            return null;
        }
    }
}
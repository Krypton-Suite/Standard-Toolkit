#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

/// <summary>
/// Helper class for saving and loading floating toolbar states to/from XML files.
/// </summary>
public static class FloatingToolbarStateManager
{
    /// <summary>
    /// Saves a collection of floating toolbar states to an XML file.
    /// </summary>
    /// <param name="states">The collection of toolbar states to save.</param>
    /// <param name="filePath">The path to the XML file.</param>
    /// <returns>True if the save was successful; otherwise, false.</returns>
    public static bool SaveStatesToFile(IEnumerable<FloatingToolbarState> states, string filePath)
    {
        try
        {
            var collection = new FloatingToolbarStateCollection
            {
                ToolbarStates = states.ToList()
            };

            var serializer = new XmlSerializer(typeof(FloatingToolbarStateCollection));
            using var writer = new StreamWriter(filePath);
            serializer.Serialize(writer, collection);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Loads floating toolbar states from an XML file.
    /// </summary>
    /// <param name="filePath">The path to the XML file.</param>
    /// <returns>A collection of toolbar states, or null if loading failed.</returns>
    public static FloatingToolbarStateCollection? LoadStatesFromFile(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                return null;
            }

            var serializer = new XmlSerializer(typeof(FloatingToolbarStateCollection));
            using var reader = new StreamReader(filePath);
            return serializer.Deserialize(reader) as FloatingToolbarStateCollection;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Saves a collection of floating toolbar states to an XML string.
    /// </summary>
    /// <param name="states">The collection of toolbar states to save.</param>
    /// <returns>The XML string representation, or null if serialization failed.</returns>
    public static string? SaveStatesToString(IEnumerable<FloatingToolbarState> states)
    {
        try
        {
            var collection = new FloatingToolbarStateCollection
            {
                ToolbarStates = states.ToList()
            };

            var serializer = new XmlSerializer(typeof(FloatingToolbarStateCollection));
            using var writer = new StringWriter();
            serializer.Serialize(writer, collection);
            return writer.ToString();
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Loads floating toolbar states from an XML string.
    /// </summary>
    /// <param name="xmlString">The XML string to deserialize.</param>
    /// <returns>A collection of toolbar states, or null if loading failed.</returns>
    public static FloatingToolbarStateCollection? LoadStatesFromString(string xmlString)
    {
        try
        {
            var serializer = new XmlSerializer(typeof(FloatingToolbarStateCollection));
            using var reader = new StringReader(xmlString);
            return serializer.Deserialize(reader) as FloatingToolbarStateCollection;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Gets the default path for saving user preferences in the application data folder.
    /// </summary>
    /// <param name="applicationName">The name of the application.</param>
    /// <returns>The path to the user preferences folder.</returns>
    public static string GetUserPreferencesPath(string applicationName)
    {
        string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string preferencesPath = Path.Combine(appDataPath, applicationName, "FloatingToolbars");
        
        if (!Directory.Exists(preferencesPath))
        {
            Directory.CreateDirectory(preferencesPath);
        }
        
        return preferencesPath;
    }

    /// <summary>
    /// Saves floating toolbar states to the user preferences folder.
    /// </summary>
    /// <param name="states">The collection of toolbar states to save.</param>
    /// <param name="applicationName">The name of the application.</param>
    /// <param name="fileName">The name of the preferences file (default: "toolbar_states.xml").</param>
    /// <returns>True if the save was successful; otherwise, false.</returns>
    public static bool SaveStatesToUserPreferences(IEnumerable<FloatingToolbarState> states, string applicationName, string fileName = "toolbar_states.xml")
    {
        try
        {
            string preferencesPath = GetUserPreferencesPath(applicationName);
            string filePath = Path.Combine(preferencesPath, fileName);
            return SaveStatesToFile(states, filePath);
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Loads floating toolbar states from the user preferences folder.
    /// </summary>
    /// <param name="applicationName">The name of the application.</param>
    /// <param name="fileName">The name of the preferences file (default: "toolbar_states.xml").</param>
    /// <returns>A collection of toolbar states, or null if loading failed.</returns>
    public static FloatingToolbarStateCollection? LoadStatesFromUserPreferences(string applicationName, string fileName = "toolbar_states.xml")
    {
        try
        {
            string preferencesPath = GetUserPreferencesPath(applicationName);
            string filePath = Path.Combine(preferencesPath, fileName);
            return LoadStatesFromFile(filePath);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Saves a collection of floating toolbar group states to an XML file.
    /// </summary>
    /// <param name="groupStates">The collection of group states to save.</param>
    /// <param name="filePath">The path to the XML file.</param>
    /// <returns>True if the save was successful; otherwise, false.</returns>
    public static bool SaveGroupStatesToFile(IEnumerable<FloatingToolbarGroupState> groupStates, string filePath)
    {
        try
        {
            var collection = new FloatingToolbarGroupStateCollection
            {
                GroupStates = groupStates.ToList()
            };

            var serializer = new XmlSerializer(typeof(FloatingToolbarGroupStateCollection));
            using var writer = new StreamWriter(filePath);
            serializer.Serialize(writer, collection);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Loads floating toolbar group states from an XML file.
    /// </summary>
    /// <param name="filePath">The path to the XML file.</param>
    /// <returns>A collection of group states, or null if loading failed.</returns>
    public static FloatingToolbarGroupStateCollection? LoadGroupStatesFromFile(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                return null;
            }

            var serializer = new XmlSerializer(typeof(FloatingToolbarGroupStateCollection));
            using var reader = new StreamReader(filePath);
            return serializer.Deserialize(reader) as FloatingToolbarGroupStateCollection;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Saves floating toolbar group states to the user preferences folder.
    /// </summary>
    /// <param name="groupStates">The collection of group states to save.</param>
    /// <param name="applicationName">The name of the application.</param>
    /// <param name="fileName">The name of the preferences file (default: "group_states.xml").</param>
    /// <returns>True if the save was successful; otherwise, false.</returns>
    public static bool SaveGroupStatesToUserPreferences(IEnumerable<FloatingToolbarGroupState> groupStates, string applicationName, string fileName = "group_states.xml")
    {
        try
        {
            string preferencesPath = GetUserPreferencesPath(applicationName);
            string filePath = Path.Combine(preferencesPath, fileName);
            return SaveGroupStatesToFile(groupStates, filePath);
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Loads floating toolbar group states from the user preferences folder.
    /// </summary>
    /// <param name="applicationName">The name of the application.</param>
    /// <param name="fileName">The name of the preferences file (default: "group_states.xml").</param>
    /// <returns>A collection of group states, or null if loading failed.</returns>
    public static FloatingToolbarGroupStateCollection? LoadGroupStatesFromUserPreferences(string applicationName, string fileName = "group_states.xml")
    {
        try
        {
            string preferencesPath = GetUserPreferencesPath(applicationName);
            string filePath = Path.Combine(preferencesPath, fileName);
            return LoadGroupStatesFromFile(filePath);
        }
        catch
        {
            return null;
        }
    }
}

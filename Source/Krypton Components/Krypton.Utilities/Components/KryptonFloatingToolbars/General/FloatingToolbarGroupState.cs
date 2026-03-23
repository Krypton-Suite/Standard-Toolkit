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
/// Represents the state of a floating toolbar group for persistence.
/// </summary>
[Serializable]
[XmlType("FloatingToolbarGroupState")]
public class FloatingToolbarGroupState
{
    /// <summary>
    /// Gets or sets the name of the group.
    /// </summary>
    [XmlAttribute("Name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether the group is tabbed.
    /// </summary>
    [XmlAttribute("IsTabbed")]
    public bool IsTabbed { get; set; }

    /// <summary>
    /// Gets or sets the active tab index.
    /// </summary>
    [XmlAttribute("ActiveTabIndex")]
    public int ActiveTabIndex { get; set; }

    /// <summary>
    /// Gets or sets the list of toolbar names in this group.
    /// </summary>
    [XmlElement("ToolbarName")]
    public List<string> ToolbarNames { get; set; } = [];

    /// <summary>
    /// Gets or sets the list of menu strip names in this group.
    /// </summary>
    [XmlElement("MenuStripName")]
    public List<string> MenuStripNames { get; set; } = [];

    /// <summary>
    /// Gets or sets the group window location (if floating).
    /// </summary>
    [XmlElement("Location")]
    public Point Location { get; set; }

    /// <summary>
    /// Gets or sets the group window size (if floating).
    /// </summary>
    [XmlElement("Size")]
    public Size Size { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the group is floating.
    /// </summary>
    [XmlAttribute("IsFloating")]
    public bool IsFloating { get; set; }

    /// <summary>
    /// Initializes a new instance of the FloatingToolbarGroupState class.
    /// </summary>
    public FloatingToolbarGroupState()
    {
        Location = Point.Empty;
        Size = Size.Empty;
    }
}

/// <summary>
/// Collection of floating toolbar group states for serialization.
/// </summary>
[Serializable]
[XmlRoot("FloatingToolbarGroupStates")]
public class FloatingToolbarGroupStateCollection
{
    /// <summary>
    /// Gets or sets the list of group states.
    /// </summary>
    [XmlElement("GroupState")]
    public List<FloatingToolbarGroupState> GroupStates { get; set; } = [];

    /// <summary>
    /// Initializes a new instance of the FloatingToolbarGroupStateCollection class.
    /// </summary>
    public FloatingToolbarGroupStateCollection()
    {
    }
}

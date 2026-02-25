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
/// Represents the floating state of a toolbar or menu strip, including position and size information.
/// </summary>
[Serializable]
[XmlType("FloatingToolbarState")]
public class FloatingToolbarState
{
    /// <summary>
    /// Gets or sets the name/identifier of the toolbar.
    /// </summary>
    [XmlAttribute("Name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether the toolbar is floating.
    /// </summary>
    [XmlAttribute("IsFloating")]
    public bool IsFloating { get; set; }

    /// <summary>
    /// Gets or sets the floating window location.
    /// </summary>
    [XmlElement("Location")]
    public Point Location { get; set; }

    /// <summary>
    /// Gets or sets the floating window size.
    /// </summary>
    [XmlElement("Size")]
    public Size Size { get; set; }

    /// <summary>
    /// Gets or sets the name of the docked panel (if docked).
    /// </summary>
    [XmlAttribute("DockedPanelName")]
    public string? DockedPanelName { get; set; }

    /// <summary>
    /// Gets or sets the dock style when docked.
    /// </summary>
    [XmlAttribute("DockStyle")]
    public DockStyle DockStyle { get; set; }

    /// <summary>
    /// Initializes a new instance of the FloatingToolbarState class.
    /// </summary>
    public FloatingToolbarState()
    {
        Location = Point.Empty;
        Size = Size.Empty;
    }

    /// <summary>
    /// Initializes a new instance of the FloatingToolbarState class with specified values.
    /// </summary>
    /// <param name="name">The name of the toolbar.</param>
    /// <param name="isFloating">True if floating; otherwise false.</param>
    /// <param name="location">The floating window location.</param>
    /// <param name="size">The floating window size.</param>
    public FloatingToolbarState(string name, bool isFloating, Point location, Size size)
    {
        Name = name;
        IsFloating = isFloating;
        Location = location;
        Size = size;
    }
}

/// <summary>
/// Collection of floating toolbar states for serialization.
/// </summary>
[Serializable]
[XmlRoot("FloatingToolbarStates")]
public class FloatingToolbarStateCollection
{
    /// <summary>
    /// Gets or sets the list of toolbar states.
    /// </summary>
    [XmlElement("ToolbarState")]
    public List<FloatingToolbarState> ToolbarStates { get; set; } = [];

    /// <summary>
    /// Initializes a new instance of the FloatingToolbarStateCollection class.
    /// </summary>
    public FloatingToolbarStateCollection()
    {
    }
}

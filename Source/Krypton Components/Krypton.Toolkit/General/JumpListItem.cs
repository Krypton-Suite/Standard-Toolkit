#region BSD License
/*
 *  
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Represents a jump list item (task or destination).
/// </summary>
public class JumpListItem
{
    #region Public

    /// <summary>
    /// Gets or sets the display title of the jump list item.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the path to the executable or file.
    /// </summary>
    public string Path { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the command line arguments.
    /// </summary>
    public string Arguments { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the working directory.
    /// </summary>
    public string WorkingDirectory { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the icon path.
    /// </summary>
    public string IconPath { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the icon index.
    /// </summary>
    public int IconIndex { get; set; }

    /// <summary>
    /// Gets or sets the description/tooltip text.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    #endregion
}

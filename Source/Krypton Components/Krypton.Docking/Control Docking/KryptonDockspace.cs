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

namespace Krypton.Docking;

/// <summary>
/// Extends the KryptonWorkspace to work within the docking edge of a control.
/// </summary>
[ToolboxItem(false)]
[DesignerCategory("code")]
[DesignTimeVisible(false)]
public class KryptonDockspace : KryptonSpace
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonDockspace class.
    /// </summary>
    /// <remarks>
    /// If Min Size not set in the Embedded control, then will default to 150, 50
    /// </remarks>
    public KryptonDockspace()
        : base(@"Docked") =>
        // Define a sensible default minimum size
        base.MinimumSize = new Size(150, 150);

    /// <summary>
    /// Gets a string representation of the class.
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $@"KryptonDockspace {Dock}";

    #endregion
}
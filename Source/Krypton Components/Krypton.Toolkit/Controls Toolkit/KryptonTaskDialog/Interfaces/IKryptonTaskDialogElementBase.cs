#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 * © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Base and top interface for the KryptonTaskDialogElementBase abstract base class.
/// </summary>
public interface IKryptonTaskDialogElementBase
{
    /// <summary>
    /// First background color and overrides the theme based color.
    /// </summary>
    [DefaultValue(null)]
    [Description("First background color.")]
    public Color BackColor1 { get; set; }

    /// <summary>
    /// Second background color used for the gradient and overrides the theme based color.
    /// </summary>
    [DefaultValue(null)]
    [Description("Second background color used for the gradient and overrides the theme based color.")]
    public Color BackColor2 { get; set; }

    /// <summary>
    /// Show or hide the element in the KryptonTaskDialog.
    /// </summary>
    [Description("Show or hide the element in the KryptonTaskDialog.")]
    public bool Visible { get; set; }

    /// <summary>
    /// Returns the height of the element.
    /// </summary>
    [DefaultValue(100)]
    [Description("Show or hide the element in the KryptonTaskDialog.")]
    public int Height { get; }
}


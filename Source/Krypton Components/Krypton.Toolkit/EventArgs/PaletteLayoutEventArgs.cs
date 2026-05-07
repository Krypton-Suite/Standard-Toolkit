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
/// Details for palette layout events.
/// </summary>
public class PaletteLayoutEventArgs : NeedLayoutEventArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteLayoutEventArgs class.
    /// </summary>
    /// <param name="needLayout">Does the layout need regenerating.</param>
    /// <param name="needColorTable">Have the color table values changed?</param>
    public PaletteLayoutEventArgs(bool needLayout,
        bool needColorTable)
        : base(needLayout) =>
        NeedColorTable = needColorTable;

    #endregion

    #region Public
    /// <summary>
    /// Gets a value indicating if the color table needs to be reprocessed.
    /// </summary>
    public bool NeedColorTable { get; }

    #endregion
}
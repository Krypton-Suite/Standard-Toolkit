#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>Positions the title on a <see cref="KryptonForm"/>.</summary>
public enum KryptonFormTitleStyle
{
    /// <summary>Positions the title to the left (Windows 95 - 7/10/11 style).</summary>
    Classic = 0,

    /// <summary>Positions the title to the center (Windows 8/8.1 style).</summary>
    Modern = 1,

    /// <summary>Positions the title, based on OS settings.</summary>
    Inherit = 2
}
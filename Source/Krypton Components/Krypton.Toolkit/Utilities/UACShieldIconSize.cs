#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>Defines the UAC shield image size for a <see cref="KryptonButton"/>.</summary>
public enum UACShieldIconSize
{
    // <summary>A custom image size.</summary>
    // Custom = 0,
    /// <summary>The extra small image size (16 x 16).</summary>
    ExtraSmall = 1,
    /// <summary>The small image size (32 x 32).</summary>
    Small = 2,
    /// <summary>The medium image size (64 x 64).</summary>
    Medium = 3,
    /// <summary>The large image size (128 x 128).</summary>
    Large = 4,
    /// <summary>The extra large image size (256 x 256).</summary>
    ExtraLarge = 5
}
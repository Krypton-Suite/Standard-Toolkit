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
    /// <summary>The tiny image size (8 x 8).</summary>
    Tiny = 1,
    /// <summary>The extra small image size (16 x 16).</summary>
    ExtraSmall = 2,
    /// <summary>The small image size (24 x 24).</summary>
    Small = 3,
    /// <summary>The medium small image size (32 x 32).</summary>
    MediumSmall = 4,
    /// <summary>The medium image size (48 x 48).</summary>
    Medium = 5,
    /// <summary>The medium large image size (64 x 64).</summary>
    MediumLarge = 6,
    /// <summary>The large image size (96 x 96).</summary>
    Large = 7,
    /// <summary>The extra large image size (128 x 128).</summary>
    ExtraLarge = 8,
    /// <summary>The huge image size (192 x 192).</summary>
    Huge = 9,
    /// <summary>The maximum image size (256 x 256).</summary>
    Maximum = 10
}
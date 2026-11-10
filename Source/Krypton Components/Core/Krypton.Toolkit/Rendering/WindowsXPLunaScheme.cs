#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Windows XP Luna colour scheme. Olive is the HomeStead sheet and Silver is Metallic.
/// </summary>
internal enum WindowsXPLunaScheme
{
    Blue = 0,
    Olive = 1,
    Silver = 2
}

/// <summary>
/// Marks a palette that paints the Luna Blue, Olive, or Silver sprite sheets.
/// </summary>
internal interface IWindowsXPLunaScheme
{
    /// <summary>Gets the Luna colour scheme whose bitmaps should be drawn.</summary>
    WindowsXPLunaScheme Scheme { get; }
}

/// <summary>
/// Sprite-sheet theme. The first three values match <see cref="WindowsXPLunaScheme"/>.
/// </summary>
internal enum WindowsXPSpriteTheme
{
    LunaBlue = 0,
    LunaOlive = 1,
    LunaSilver = 2,
    Royale = 3,
    RoyaleNoir = 4,
    Zune = 5
}

/// <summary>
/// Marks a palette that paints a Windows XP sprite sheet other than the Luna schemes.
/// Luna palettes keep <see cref="IWindowsXPLunaScheme"/>.
/// </summary>
internal interface IWindowsXPSpriteTheme
{
    /// <summary>Gets the sprite theme whose bitmaps should be drawn.</summary>
    WindowsXPSpriteTheme Theme { get; }
}

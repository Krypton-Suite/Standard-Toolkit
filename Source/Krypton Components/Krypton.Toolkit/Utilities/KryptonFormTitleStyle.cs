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
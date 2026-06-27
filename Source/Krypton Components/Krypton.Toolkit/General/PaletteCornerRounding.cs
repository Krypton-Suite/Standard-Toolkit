#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Specifies border corner rounding radii for each corner of a rectangle.
/// </summary>
[TypeConverter(typeof(PaletteCornerRoundingConverter))]
public readonly struct PaletteCornerRounding : IEquatable<PaletteCornerRounding>
{
    /// <summary>Sentinel value indicating a corner should inherit from uniform rounding.</summary>
    public const float InheritValue = -1f;

    /// <summary>
    /// Initialize a new instance of the PaletteCornerRounding structure.
    /// </summary>
    /// <param name="topLeft">Top-left corner rounding.</param>
    /// <param name="topRight">Top-right corner rounding.</param>
    /// <param name="bottomRight">Bottom-right corner rounding.</param>
    /// <param name="bottomLeft">Bottom-left corner rounding.</param>
    public PaletteCornerRounding(float topLeft, float topRight, float bottomRight, float bottomLeft)
    {
        TopLeft = topLeft;
        TopRight = topRight;
        BottomRight = bottomRight;
        BottomLeft = bottomLeft;
    }

    /// <summary>Gets the top-left corner rounding radius.</summary>
    public float TopLeft { get; }

    /// <summary>Gets the top-right corner rounding radius.</summary>
    public float TopRight { get; }

    /// <summary>Gets the bottom-right corner rounding radius.</summary>
    public float BottomRight { get; }

    /// <summary>Gets the bottom-left corner rounding radius.</summary>
    public float BottomLeft { get; }

    /// <summary>Gets a value indicating whether all corners share the same radius.</summary>
    public bool IsUniform =>
        TopLeft == TopRight
        && TopRight == BottomRight
        && BottomRight == BottomLeft;

    /// <summary>Gets the largest corner radius.</summary>
    public float MaxRadius => Math.Max(Math.Max(TopLeft, TopRight), Math.Max(BottomLeft, BottomRight));

    /// <summary>Gets a value indicating whether any corner has a positive radius.</summary>
    public bool HasRounding => MaxRadius > 0f;

    /// <summary>Gets a value indicating whether any corner uses the inherit sentinel.</summary>
    public bool HasInherit =>
        TopLeft == InheritValue
        || TopRight == InheritValue
        || BottomRight == InheritValue
        || BottomLeft == InheritValue;

    /// <summary>Creates a uniform rounding value for all corners.</summary>
    /// <param name="value">Rounding radius to apply to every corner.</param>
    /// <returns>Uniform corner rounding.</returns>
    public static PaletteCornerRounding Uniform(float value) => new(value, value, value, value);

    /// <summary>Creates an inherit-only corner rounding value.</summary>
    /// <returns>Inherit corner rounding.</returns>
    public static PaletteCornerRounding Inherit => new(InheritValue, InheritValue, InheritValue, InheritValue);

    /// <summary>
    /// Merges optional per-corner overrides onto a uniform baseline radius.
    /// </summary>
    /// <param name="uniform">Baseline radius used when a corner is set to inherit.</param>
    /// <param name="topLeft">Top-left override, or <see cref="InheritValue"/>.</param>
    /// <param name="topRight">Top-right override, or <see cref="InheritValue"/>.</param>
    /// <param name="bottomRight">Bottom-right override, or <see cref="InheritValue"/>.</param>
    /// <param name="bottomLeft">Bottom-left override, or <see cref="InheritValue"/>.</param>
    /// <returns>Resolved corner rounding.</returns>
    public static PaletteCornerRounding Merge(float uniform, float topLeft, float topRight, float bottomRight, float bottomLeft) =>
        new(
            topLeft == InheritValue ? uniform : topLeft,
            topRight == InheritValue ? uniform : topRight,
            bottomRight == InheritValue ? uniform : bottomRight,
            bottomLeft == InheritValue ? uniform : bottomLeft);

    /// <inheritdoc />
    public bool Equals(PaletteCornerRounding other) =>
        TopLeft == other.TopLeft
        && TopRight == other.TopRight
        && BottomRight == other.BottomRight
        && BottomLeft == other.BottomLeft;

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is PaletteCornerRounding other && Equals(other);

    /// <inheritdoc />
    public override int GetHashCode() => HashCode.Combine(TopLeft, TopRight, BottomRight, BottomLeft);

    /// <summary>Compares two values for equality.</summary>
    public static bool operator ==(PaletteCornerRounding left, PaletteCornerRounding right) => left.Equals(right);

    /// <summary>Compares two values for inequality.</summary>
    public static bool operator !=(PaletteCornerRounding left, PaletteCornerRounding right) => !left.Equals(right);

    /// <inheritdoc />
    public override string ToString()
    {
        if (TopLeft == InheritValue
            && TopRight == InheritValue
            && BottomRight == InheritValue
            && BottomLeft == InheritValue)
        {
            return @"Inherit";
        }

        return IsUniform ? TopLeft.ToString(CultureInfo.CurrentCulture) : $"TL={TopLeft}, TR={TopRight}, BR={BottomRight}, BL={BottomLeft}";
    }
}

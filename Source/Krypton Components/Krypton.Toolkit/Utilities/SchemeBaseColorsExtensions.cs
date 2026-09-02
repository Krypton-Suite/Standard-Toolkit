#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

public static class SchemeBaseColorsExtensions
{
    /// <summary>
    /// Converts the provided <see cref="KryptonColorSchemeBase"/> into a <see cref="Color"/> array that is
    /// ordered according to the <see cref="SchemeBaseColors"/> enumeration.
    /// </summary>
    /// <param name="scheme">Concrete color scheme instance containing properties that match the enumeration names.</param>
    /// <returns>Array of colors indexed by <see cref="SchemeBaseColors"/> values.</returns>
    public static Color[] ToArray(this KryptonColorSchemeBase scheme)
    {
        if (scheme == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(scheme));
        }

        var names = Enum.GetNames(typeof(SchemeBaseColors));
        var colors = new Color[names.Length];
        var type = scheme.GetType();

        for (int i = 0; i < names.Length; i++)
        {
            var property = type.GetProperty(names[i]);
            colors[i] = property is null
                ? SharedStaticVariables.EMPTY_COLOR
                : (Color)property.GetValue(scheme)!;
        }

        return colors;
    }

    /// <summary>
    /// Returns <paramref name="primary"/> when it is a real colour; otherwise <paramref name="fallback"/>.
    /// Empty scheme slots use this so builtin themes keep their historical ColorTable aliases.
    /// </summary>
    /// <param name="primary">Preferred colour, which may be <see cref="Color.Empty"/>.</param>
    /// <param name="fallback">Colour used when <paramref name="primary"/> is empty.</param>
    /// <returns>The first non-empty colour.</returns>
    public static Color Coalesce(Color primary, Color fallback) =>
        IsEmptySchemeColor(primary) ? fallback : primary;

    /// <summary>
    /// Reads <paramref name="primary"/> from <paramref name="colors"/>, falling back to <paramref name="fallback"/>
    /// when the slot is missing or empty.
    /// </summary>
    /// <param name="colors">Scheme array indexed by <see cref="SchemeBaseColors"/>.</param>
    /// <param name="primary">Preferred scheme slot.</param>
    /// <param name="fallback">Slot used when <paramref name="primary"/> is empty or out of range.</param>
    /// <returns>The resolved colour, or <see cref="Color.Empty"/> when both slots are unavailable.</returns>
    public static Color Resolve(this Color[]? colors, SchemeBaseColors primary, SchemeBaseColors fallback) =>
        Coalesce(Get(colors, primary), Get(colors, fallback));

    /// <summary>
    /// Reads a scheme slot, returning <see cref="Color.Empty"/> when the array is null or too short.
    /// </summary>
    /// <param name="colors">Scheme array indexed by <see cref="SchemeBaseColors"/>.</param>
    /// <param name="index">Slot to read.</param>
    /// <returns>The stored colour, or empty when missing.</returns>
    public static Color Get(this Color[]? colors, SchemeBaseColors index)
    {
        if (colors is null)
        {
            return SharedStaticVariables.EMPTY_COLOR;
        }

        var i = (int)index;
        return i >= 0 && i < colors.Length ? colors[i] : SharedStaticVariables.EMPTY_COLOR;
    }

    /// <summary>
    /// True when <paramref name="value"/> is the scheme empty sentinel.
    /// </summary>
    /// <param name="value">Colour to test.</param>
    /// <returns><see langword="true"/> when the colour should inherit a fallback slot.</returns>
    public static bool IsEmptySchemeColor(Color value) =>
        value.IsEmpty || value == SharedStaticVariables.EMPTY_COLOR;

    /// <summary>
    /// Writes <paramref name="value"/> onto the scheme property whose name matches <paramref name="index"/>.
    /// </summary>
    /// <param name="scheme">Scheme instance to update. Ignored when null.</param>
    /// <param name="index">Slot whose matching property should be written.</param>
    /// <param name="value">Colour to store.</param>
    /// <remarks>
    /// <see cref="PaletteBase.SetSchemeColor"/> updates <see cref="PaletteBase.SchemeColors"/>; several palettes
    /// still read <c>BaseColors.MenuItemText</c> for <c>KryptonContextMenu</c> item text. Keep the scheme object
    /// in step with the array so those reads see the override.
    /// </remarks>
    public static void Set(this KryptonColorSchemeBase? scheme, SchemeBaseColors index, Color value)
    {
        if (scheme is null)
        {
            return;
        }

        var property = scheme.GetType().GetProperty(index.ToString());
        if (property != null && property.CanWrite && property.PropertyType == typeof(Color))
        {
            property.SetValue(scheme, value);
        }
    }

    /// <summary>
    /// Extracts the six TrackBar-related colours from a scheme into the legacy Color array layout expected
    /// by older palette constructors.
    /// </summary>
    /// <param name="scheme">Scheme instance.</param>
    /// <returns>Array of six colours in enum order TickMarks..BorderPosition.</returns>
    public static Color[] ToTrackBarArray(this KryptonColorSchemeBase scheme)
    {
        if (scheme == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(scheme));
        }

        return new[]
        {
            scheme.TrackBarTickMarks,
            scheme.TrackBarTopTrack,
            scheme.TrackBarBottomTrack,
            scheme.TrackBarFillTrack,
            scheme.TrackBarOutsidePosition,
            scheme.TrackBarBorderPosition
        };
    }
}

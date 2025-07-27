#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
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
            throw new ArgumentNullException(nameof(scheme));
        }

        var names = Enum.GetNames(typeof(SchemeBaseColors));
        var colors = new Color[names.Length];
        var type = scheme.GetType();

        for (int i = 0; i < names.Length; i++)
        {
            var property = type.GetProperty(names[i]);
            colors[i] = property is null
                ? GlobalStaticValues.EMPTY_COLOR
                : (Color)property.GetValue(scheme)!;
        }

        return colors;
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
            throw new ArgumentNullException(nameof(scheme));
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

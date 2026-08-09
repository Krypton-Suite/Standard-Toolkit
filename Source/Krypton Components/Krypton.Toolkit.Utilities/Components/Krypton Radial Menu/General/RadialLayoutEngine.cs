#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Geometry helper for equal-angle annular sectors and polar hit-testing.
/// </summary>
internal static class RadialLayoutEngine
{
    /// <summary>
    /// Builds equal-angle sector descriptors for the visible items.
    /// </summary>
    /// <param name="itemCount">Number of visible items.</param>
    /// <param name="outerRadius">Outer radius.</param>
    /// <param name="innerRadius">Inner radius.</param>
    /// <returns>Sector descriptors.</returns>
    public static RadialSectorInfo[] BuildSectors(int itemCount, float outerRadius, float innerRadius)
    {
        if (itemCount <= 0)
        {
            return Array.Empty<RadialSectorInfo>();
        }

        var sweep = 360f / itemCount;
        var sectors = new RadialSectorInfo[itemCount];
        for (var i = 0; i < itemCount; i++)
        {
            // Start at top (-90°) and sweep clockwise in GDI+ (positive angles are clockwise when using DrawPie with standard transforms).
            // GDI+ angles: 0 = right, positive = clockwise.
            var start = -90f + (i * sweep);
            sectors[i] = new RadialSectorInfo(i, start, sweep, outerRadius, innerRadius);
        }

        return sectors;
    }

    /// <summary>
    /// Hit-tests a client point against the radial layout.
    /// </summary>
    /// <param name="clientPoint">Point in client coordinates.</param>
    /// <param name="center">Menu centre.</param>
    /// <param name="outerRadius">Outer radius.</param>
    /// <param name="innerRadius">Inner radius.</param>
    /// <param name="sectors">Sector descriptors.</param>
    /// <param name="editorMode">True when an editor ring is active.</param>
    /// <param name="editorCount">Number of editor elements.</param>
    /// <returns>Hit result.</returns>
    public static RadialHitResult HitTest(
        Point clientPoint,
        PointF center,
        float outerRadius,
        float innerRadius,
        RadialSectorInfo[] sectors,
        bool editorMode,
        int editorCount)
    {
        var dx = clientPoint.X - center.X;
        var dy = clientPoint.Y - center.Y;
        var distance = (float)Math.Sqrt((dx * dx) + (dy * dy));

        if (distance > outerRadius)
        {
            return RadialHitResult.None;
        }

        if (distance <= innerRadius)
        {
            return new RadialHitResult(RadialHitKind.Center, -1, -1);
        }

        var angle = (float)(Math.Atan2(dy, dx) * (180.0 / Math.PI));
        // Convert atan2 (-180..180, 0 = right, CCW positive) to GDI+ style (0 = right, CW positive from -90 start).
        if (angle < 0f)
        {
            angle += 360f;
        }

        // Map to start-at-top degrees: 0 at top, clockwise.
        var fromTop = (angle + 90f) % 360f;

        if (editorMode && editorCount > 0)
        {
            var sweep = 360f / editorCount;
            var index = (int)(fromTop / sweep);
            if (index < 0)
            {
                index = 0;
            }

            if (index >= editorCount)
            {
                index = editorCount - 1;
            }

            return new RadialHitResult(RadialHitKind.Editor, -1, index);
        }

        for (var i = 0; i < sectors.Length; i++)
        {
            var local = fromTop - (i * sectors[i].SweepAngle);
            if (local < 0f)
            {
                local += 360f;
            }

            if (local >= 0f && local < sectors[i].SweepAngle)
            {
                return new RadialHitResult(RadialHitKind.Sector, i, -1);
            }
        }

        return RadialHitResult.None;
    }

    /// <summary>
    /// Gets the centroid of a sector mid-ring for content placement.
    /// </summary>
    /// <param name="center">Menu centre.</param>
    /// <param name="sector">Sector info.</param>
    /// <returns>Content point.</returns>
    public static PointF GetSectorContentPoint(PointF center, RadialSectorInfo sector)
    {
        var midAngle = sector.StartAngle + (sector.SweepAngle / 2f);
        var radians = midAngle * (float)(Math.PI / 180.0);
        var radius = (sector.InnerRadius + sector.OuterRadius) / 2f;
        return new PointF(
            center.X + (radius * (float)Math.Cos(radians)),
            center.Y + (radius * (float)Math.Sin(radians)));
    }

    /// <summary>
    /// Converts a pointer angle (from top, clockwise) into a 0..1 slider fraction.
    /// </summary>
    /// <param name="clientPoint">Client point.</param>
    /// <param name="center">Menu centre.</param>
    /// <returns>Normalised value.</returns>
    public static float AngleToNormalized(Point clientPoint, PointF center)
    {
        var dx = clientPoint.X - center.X;
        var dy = clientPoint.Y - center.Y;
        var angle = (float)(Math.Atan2(dy, dx) * (180.0 / Math.PI));
        if (angle < 0f)
        {
            angle += 360f;
        }

        var fromTop = (angle + 90f) % 360f;
        return fromTop / 360f;
    }
}

/// <summary>
/// Describes one annular sector.
/// </summary>
internal readonly struct RadialSectorInfo
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RadialSectorInfo"/> struct.
    /// </summary>
    public RadialSectorInfo(int index, float startAngle, float sweepAngle, float outerRadius, float innerRadius)
    {
        Index = index;
        StartAngle = startAngle;
        SweepAngle = sweepAngle;
        OuterRadius = outerRadius;
        InnerRadius = innerRadius;
    }

    public int Index { get; }
    public float StartAngle { get; }
    public float SweepAngle { get; }
    public float OuterRadius { get; }
    public float InnerRadius { get; }
}

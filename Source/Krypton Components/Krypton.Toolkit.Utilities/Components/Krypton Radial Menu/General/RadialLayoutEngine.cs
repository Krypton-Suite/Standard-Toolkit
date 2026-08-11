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
    /// <param name="startAngle">Start angle of the first sector in degrees.</param>
    /// <returns>Sector descriptors.</returns>
    public static RadialSectorInfo[] BuildSectors(int itemCount, float outerRadius, float innerRadius, float startAngle = -90f)
    {
        if (itemCount <= 0)
        {
            return [];
        }

        var sweep = 360f / itemCount;
        var sectors = new RadialSectorInfo[itemCount];
        for (var i = 0; i < itemCount; i++)
        {
            // GDI+ angles: 0 = right, positive = clockwise.
            var start = startAngle + (i * sweep);
            sectors[i] = new RadialSectorInfo(i, start, sweep, outerRadius, innerRadius);
        }

        return sectors;
    }

    /// <summary>
    /// Hit-tests a client point against the radial layout, distinguishing centre, sector body, and outer-ring band.
    /// </summary>
    public static RadialHitResult HitTest(
        Point clientPoint,
        PointF center,
        float outerRadius,
        float innerRadius,
        RadialSectorInfo[] sectors,
        bool editorMode,
        int editorCount,
        float startAngle = -90f,
        float hitPadding = 0f,
        float outerRingThickness = 10f)
    {
        var dx = clientPoint.X - center.X;
        var dy = clientPoint.Y - center.Y;
        var distance = (float)Math.Sqrt((dx * dx) + (dy * dy));
        var outer = outerRadius + hitPadding;
        var inner = Math.Max(0f, innerRadius - hitPadding);

        if (distance > outer)
        {
            return RadialHitResult.None;
        }

        if (distance <= inner)
        {
            return new RadialHitResult(RadialHitKind.Center, -1, -1);
        }

        var angle = (float)(Math.Atan2(dy, dx) * (180.0 / Math.PI));
        if (angle < 0f)
        {
            angle += 360f;
        }

        // Degrees clockwise from startAngle.
        var fromStart = (angle - startAngle) % 360f;
        if (fromStart < 0f)
        {
            fromStart += 360f;
        }

        if (editorMode && editorCount > 0)
        {
            var sweep = 360f / editorCount;
            var index = (int)(fromStart / sweep);
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

        var sectorIndex = FindSectorIndex(fromStart, sectors);
        if (sectorIndex < 0)
        {
            return RadialHitResult.None;
        }

        // Outer-ring band: thickness floor, with a touch-friendly minimum using hit padding.
        var band = Math.Max(Math.Max(0f, outerRingThickness), hitPadding + 2f);
        var ringInner = Math.Max(inner, outerRadius - band);
        if (distance >= ringInner)
        {
            return new RadialHitResult(RadialHitKind.OuterRing, sectorIndex, -1);
        }

        return new RadialHitResult(RadialHitKind.Sector, sectorIndex, -1);
    }

    private static int FindSectorIndex(float fromStart, RadialSectorInfo[] sectors)
    {
        for (var i = 0; i < sectors.Length; i++)
        {
            var local = fromStart - (i * sectors[i].SweepAngle);
            if (local < 0f)
            {
                local += 360f;
            }

            if (local >= 0f && local < sectors[i].SweepAngle)
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>
    /// Gets the centroid of a sector mid-ring for content placement.
    /// </summary>
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
    /// Converts a pointer angle into a 0..1 slider fraction relative to <paramref name="startAngle"/>.
    /// </summary>
    public static float AngleToNormalized(Point clientPoint, PointF center, float startAngle = -90f)
    {
        var dx = clientPoint.X - center.X;
        var dy = clientPoint.Y - center.Y;
        var angle = (float)(Math.Atan2(dy, dx) * (180.0 / Math.PI));
        if (angle < 0f)
        {
            angle += 360f;
        }

        var fromStart = (angle - startAngle) % 360f;
        if (fromStart < 0f)
        {
            fromStart += 360f;
        }

        return fromStart / 360f;
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

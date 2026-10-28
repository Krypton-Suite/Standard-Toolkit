#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// One zone inside a snap-layout card, as fractions of the monitor work area.
/// </summary>
internal readonly struct KryptonSnapLayoutZone
{
    /// <summary>
    /// Initializes a zone from work-area fractions in the range 0..1.
    /// </summary>
    /// <param name="x">Left edge.</param>
    /// <param name="y">Top edge.</param>
    /// <param name="width">Width.</param>
    /// <param name="height">Height.</param>
    internal KryptonSnapLayoutZone(float x, float y, float width, float height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    /// <summary>Gets the left edge as a fraction of the work area.</summary>
    internal float X { get; }

    /// <summary>Gets the top edge as a fraction of the work area.</summary>
    internal float Y { get; }

    /// <summary>Gets the width as a fraction of the work area.</summary>
    internal float Width { get; }

    /// <summary>Gets the height as a fraction of the work area.</summary>
    internal float Height { get; }
}

/// <summary>
/// A layout card: the set of zones the user can pick.
/// </summary>
internal sealed class KryptonSnapLayoutCard
{
    /// <summary>
    /// Initializes a card.
    /// </summary>
    /// <param name="zones">Zones in this card. Not mirrored.</param>
    internal KryptonSnapLayoutCard(KryptonSnapLayoutZone[] zones) => Zones = zones;

    /// <summary>Gets the zones drawn in this card.</summary>
    internal KryptonSnapLayoutZone[] Zones { get; }
}

/// <summary>
/// Event data for a chosen snap-layout zone.
/// </summary>
internal sealed class KryptonSnapLayoutZoneEventArgs : EventArgs
{
    /// <summary>
    /// Initializes event data for <paramref name="zone"/>.
    /// </summary>
    /// <param name="zone">Zone the user selected.</param>
    internal KryptonSnapLayoutZoneEventArgs(KryptonSnapLayoutZone zone) => Zone = zone;

    /// <summary>Gets the selected zone.</summary>
    internal KryptonSnapLayoutZone Zone { get; }
}

/// <summary>
/// Builds the snap-layout cards that fit the current monitor and form minimum size.
/// </summary>
internal static class KryptonSnapLayoutCatalog
{
    // Three-column cards need a wide work area; narrower monitors keep the two-column set.
    private const int WideWorkAreaWidth = 1100;

    private static readonly KryptonSnapLayoutZone[] _halves =
    {
        new KryptonSnapLayoutZone(0f, 0f, 0.5f, 1f),
        new KryptonSnapLayoutZone(0.5f, 0f, 0.5f, 1f)
    };

    private static readonly KryptonSnapLayoutZone[] _leftStack =
    {
        new KryptonSnapLayoutZone(0f, 0f, 0.5f, 0.5f),
        new KryptonSnapLayoutZone(0f, 0.5f, 0.5f, 0.5f),
        new KryptonSnapLayoutZone(0.5f, 0f, 0.5f, 1f)
    };

    private static readonly KryptonSnapLayoutZone[] _rightStack =
    {
        new KryptonSnapLayoutZone(0f, 0f, 0.5f, 1f),
        new KryptonSnapLayoutZone(0.5f, 0f, 0.5f, 0.5f),
        new KryptonSnapLayoutZone(0.5f, 0.5f, 0.5f, 0.5f)
    };

    private static readonly KryptonSnapLayoutZone[] _quarters =
    {
        new KryptonSnapLayoutZone(0f, 0f, 0.5f, 0.5f),
        new KryptonSnapLayoutZone(0.5f, 0f, 0.5f, 0.5f),
        new KryptonSnapLayoutZone(0f, 0.5f, 0.5f, 0.5f),
        new KryptonSnapLayoutZone(0.5f, 0.5f, 0.5f, 0.5f)
    };

    private static readonly KryptonSnapLayoutZone[] _thirds =
    {
        new KryptonSnapLayoutZone(0f, 0f, 1f / 3f, 1f),
        new KryptonSnapLayoutZone(1f / 3f, 0f, 1f / 3f, 1f),
        new KryptonSnapLayoutZone(2f / 3f, 0f, 1f / 3f, 1f)
    };

    private static readonly KryptonSnapLayoutZone[] _centerWide =
    {
        new KryptonSnapLayoutZone(0f, 0f, 0.25f, 1f),
        new KryptonSnapLayoutZone(0.25f, 0f, 0.5f, 1f),
        new KryptonSnapLayoutZone(0.75f, 0f, 0.25f, 1f)
    };

    /// <summary>
    /// Gets the cards whose every zone is at least <paramref name="minimumSize"/>.
    /// </summary>
    /// <param name="workArea">Monitor work area the form will snap into.</param>
    /// <param name="minimumSize">Form minimum size. Empty dimensions are not constrained.</param>
    /// <param name="rightToLeft">Mirror zones on the horizontal axis.</param>
    /// <returns>Cards safe to show. May be empty.</returns>
    internal static KryptonSnapLayoutCard[] GetCards(Size workArea, Size minimumSize, bool rightToLeft)
    {
        var templates = new List<KryptonSnapLayoutZone[]>
        {
            _halves,
            _leftStack,
            _rightStack,
            _quarters
        };

        if (workArea.Width >= WideWorkAreaWidth)
        {
            templates.Add(_thirds);
            templates.Add(_centerWide);
        }

        var cards = new List<KryptonSnapLayoutCard>();
        foreach (KryptonSnapLayoutZone[] template in templates)
        {
            KryptonSnapLayoutZone[] zones = rightToLeft ? Mirror(template) : template;
            if (Fits(workArea, minimumSize, zones))
            {
                cards.Add(new KryptonSnapLayoutCard(zones));
            }
        }

        return cards.ToArray();
    }

    /// <summary>
    /// Maps a zone onto <paramref name="workArea"/> in screen pixels.
    /// </summary>
    /// <param name="workArea">Monitor work area.</param>
    /// <param name="zone">Zone fractions.</param>
    /// <returns>Screen rectangle. Edges that share a fraction land on the same pixel.</returns>
    internal static Rectangle ToScreen(Rectangle workArea, KryptonSnapLayoutZone zone)
    {
        int left = workArea.X + (int)Math.Round(workArea.Width * zone.X);
        int top = workArea.Y + (int)Math.Round(workArea.Height * zone.Y);
        int right = workArea.X + (int)Math.Round(workArea.Width * (zone.X + zone.Width));
        int bottom = workArea.Y + (int)Math.Round(workArea.Height * (zone.Y + zone.Height));
        if (right <= left)
        {
            right = left + 1;
        }

        if (bottom <= top)
        {
            bottom = top + 1;
        }

        return Rectangle.FromLTRB(left, top, right, bottom);
    }

    private static bool Fits(Size workArea, Size minimumSize, KryptonSnapLayoutZone[] zones)
    {
        foreach (KryptonSnapLayoutZone zone in zones)
        {
            int width = Math.Max(1, (int)Math.Round(workArea.Width * zone.Width));
            int height = Math.Max(1, (int)Math.Round(workArea.Height * zone.Height));
            if (minimumSize.Width > 0 && width < minimumSize.Width)
            {
                return false;
            }

            if (minimumSize.Height > 0 && height < minimumSize.Height)
            {
                return false;
            }
        }

        return true;
    }

    private static KryptonSnapLayoutZone[] Mirror(KryptonSnapLayoutZone[] zones)
    {
        var mirrored = new KryptonSnapLayoutZone[zones.Length];
        for (int i = 0; i < zones.Length; i++)
        {
            KryptonSnapLayoutZone zone = zones[i];
            mirrored[i] = new KryptonSnapLayoutZone(1f - zone.X - zone.Width, zone.Y, zone.Width, zone.Height);
        }

        return mirrored;
    }
}

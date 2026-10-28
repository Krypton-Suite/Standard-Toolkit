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
/// <remarks>
/// Edges are fractions from 0 to 1. A value that misses that range by more than a small
/// rounding tolerance is rejected. Right-to-left mirroring of thirds can land a fraction
/// of a pixel outside 0 or 1, and that still constructs.
/// </remarks>
public sealed class KryptonSnapLayoutZone
{
    // Float sums such as (2/3 + 1/3) can sit just outside 0..1. This keeps those, and real mistakes still fail.
    private const float EdgeTolerance = 0.0001f;

    private float _x;
    private float _y;
    private float _width;
    private float _height;

    /// <summary>
    /// Initializes an empty zone for the designer. Set <see cref="X"/>, <see cref="Y"/>, <see cref="Width"/>, and <see cref="Height"/> before the menu shows it.
    /// </summary>
    public KryptonSnapLayoutZone()
    {
    }

    /// <summary>
    /// Initializes a zone from work-area fractions in the range 0..1.
    /// </summary>
    /// <param name="x">Left edge, from 0 to 1.</param>
    /// <param name="y">Top edge, from 0 to 1.</param>
    /// <param name="width">Width, greater than zero. The right edge must stay within 1.</param>
    /// <param name="height">Height, greater than zero. The bottom edge must stay within 1.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// An edge is outside 0..1, or a size is not positive, or the zone extends past the work area.
    /// </exception>
    public KryptonSnapLayoutZone(float x, float y, float width, float height)
    {
        RequireEdge(nameof(x), x);
        RequireEdge(nameof(y), y);
        RequireSpan(nameof(width), x, width);
        RequireSpan(nameof(height), y, height);
        _x = x;
        _y = y;
        _width = width;
        _height = height;
    }

    /// <summary>Gets or sets the left edge as a fraction of the work area.</summary>
    /// <exception cref="ArgumentOutOfRangeException">The edge is outside 0..1, or the zone would extend past the work area.</exception>
    [Category(@"Layout")]
    [Description(@"Left edge as a fraction of the work area, from 0 to 1.")]
    [DefaultValue(0f)]
    public float X
    {
        get => _x;
        set
        {
            RequireEdge(nameof(value), value);
            if (_width > 0f)
            {
                RequireSpan(nameof(value), value, _width);
            }

            _x = value;
        }
    }

    /// <summary>Gets or sets the top edge as a fraction of the work area.</summary>
    /// <exception cref="ArgumentOutOfRangeException">The edge is outside 0..1, or the zone would extend past the work area.</exception>
    [Category(@"Layout")]
    [Description(@"Top edge as a fraction of the work area, from 0 to 1.")]
    [DefaultValue(0f)]
    public float Y
    {
        get => _y;
        set
        {
            RequireEdge(nameof(value), value);
            if (_height > 0f)
            {
                RequireSpan(nameof(value), value, _height);
            }

            _y = value;
        }
    }

    /// <summary>Gets or sets the width as a fraction of the work area.</summary>
    /// <exception cref="ArgumentOutOfRangeException">The width is not positive, or the zone would extend past the work area.</exception>
    [Category(@"Layout")]
    [Description(@"Width as a fraction of the work area. The right edge must stay within 1.")]
    [DefaultValue(0f)]
    public float Width
    {
        get => _width;
        set
        {
            RequireSpan(nameof(value), _x, value);
            _width = value;
        }
    }

    /// <summary>Gets or sets the height as a fraction of the work area.</summary>
    /// <exception cref="ArgumentOutOfRangeException">The height is not positive, or the zone would extend past the work area.</exception>
    [Category(@"Layout")]
    [Description(@"Height as a fraction of the work area. The bottom edge must stay within 1.")]
    [DefaultValue(0f)]
    public float Height
    {
        get => _height;
        set
        {
            RequireSpan(nameof(value), _y, value);
            _height = value;
        }
    }

    /// <summary>Returns the four fractions, rounded for the property grid.</summary>
    /// <returns>Left, top, width, and height.</returns>
    public override string ToString() => $"{_x:0.##}, {_y:0.##}, {_width:0.##} x {_height:0.##}";

    private static void RequireEdge(string name, float value)
    {
        if (float.IsNaN(value) || float.IsInfinity(value) || value < -EdgeTolerance || value > 1f + EdgeTolerance)
        {
            throw new ArgumentOutOfRangeException(name, value, @"The edge must be from 0 to 1.");
        }
    }

    private static void RequireSpan(string name, float origin, float size)
    {
        if (float.IsNaN(size) || float.IsInfinity(size) || size <= 0f || origin + size > 1f + EdgeTolerance)
        {
            throw new ArgumentOutOfRangeException(name, size, @"The size must be greater than zero and the zone must stay inside 0..1.");
        }
    }
}

/// <summary>
/// A layout card: the set of zones the user can pick.
/// </summary>
/// <remarks>
/// Zones are stored in left-to-right order. The snap menu mirrors them when the form is right-to-left.
/// Call from the UI thread, the same thread that shows the menu.
/// </remarks>
public sealed class KryptonSnapLayoutCard
{
    /// <summary>
    /// Initializes an empty card for the designer. Add zones to <see cref="Zones"/>.
    /// </summary>
    public KryptonSnapLayoutCard()
    {
        Zones = new KryptonSnapLayoutZoneCollection();
    }

    /// <summary>
    /// Initializes a card from at least one zone.
    /// </summary>
    /// <param name="zones">Zones in this card, in left-to-right order. The card keeps its own copy.</param>
    /// <exception cref="ArgumentNullException"><paramref name="zones"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException"><paramref name="zones"/> is empty.</exception>
    public KryptonSnapLayoutCard(params KryptonSnapLayoutZone[] zones)
        : this(zones, false)
    {
    }

    /// <summary>
    /// Initializes a card and records whether the menu should label it as custom.
    /// </summary>
    /// <param name="zones">Zones in this card, in left-to-right order. The card keeps its own copy.</param>
    /// <param name="custom"><see langword="true"/> when the card came from <see cref="KryptonManager.CustomSnapLayouts"/>.</param>
    /// <exception cref="ArgumentNullException"><paramref name="zones"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException"><paramref name="zones"/> is empty.</exception>
    internal KryptonSnapLayoutCard(KryptonSnapLayoutZone[] zones, bool custom)
        : this()
    {
        if (zones == null)
        {
            throw new ArgumentNullException(nameof(zones));
        }

        if (zones.Length == 0)
        {
            throw new ArgumentException(@"A snap-layout card needs at least one zone.", nameof(zones));
        }

        foreach (KryptonSnapLayoutZone zone in zones)
        {
            if (zone == null)
            {
                throw new ArgumentNullException(nameof(zones));
            }

            Zones.Add(new KryptonSnapLayoutZone(zone.X, zone.Y, zone.Width, zone.Height));
        }

        IsCustom = custom;
    }

    /// <summary>Gets the zones drawn in this card.</summary>
    [Category(@"Layout")]
    [Description(@"Zones in this card, as fractions of the work area.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Editor(typeof(KryptonSnapLayoutZoneCollectionEditor), typeof(UITypeEditor))]
    public KryptonSnapLayoutZoneCollection Zones { get; }

    /// <summary>
    /// Gets a value indicating whether the menu labels this card as a custom layout.
    /// </summary>
    /// <remarks>
    /// The menu sets this on the copy it shows. A card constructed by the caller stays <see langword="false"/>.
    /// </remarks>
    internal bool IsCustom { get; private set; }

    /// <summary>Returns how many zones the card contains.</summary>
    /// <returns>Zone count, for the collection editor list.</returns>
    public override string ToString() => Zones.Count == 0 ? @"Snap layout" : $"{Zones.Count} zones";

    private bool ShouldSerializeZones() => Zones.Count > 0;

    private void ResetZones() => Zones.Clear();
}

/// <summary>
/// Zones that make up one <see cref="KryptonSnapLayoutCard"/>.
/// </summary>
public sealed class KryptonSnapLayoutZoneCollection : TypedCollection<KryptonSnapLayoutZone>
{
    /// <summary>Returns how many zones are in the collection.</summary>
    /// <returns>Zone count.</returns>
    public override string ToString() => Count == 0 ? @"None" : $"{Count} zones";
}

/// <summary>
/// Cards added with <see cref="KryptonManager.CustomSnapLayouts"/>.
/// </summary>
/// <remarks>
/// Empty by default, so the menu shows the builtin cards. Add and clear on the UI thread.
/// The menu reads the list when it opens. The same list is <see cref="KryptonManager.GlobalCustomSnapLayouts"/> in the designer.
/// </remarks>
public sealed class KryptonSnapLayoutCardCollection : TypedCollection<KryptonSnapLayoutCard>
{
    /// <summary>Returns how many custom cards are in the collection.</summary>
    /// <returns>Card count.</returns>
    public override string ToString() => Count == 0 ? @"None" : $"{Count} cards";

    /// <summary>Copies the current cards so the menu can read a stable list.</summary>
    /// <returns>The cards in add order. The array is empty when <see cref="TypedCollection{T}.Count"/> is zero.</returns>
    internal KryptonSnapLayoutCard[] Snapshot()
    {
        var copy = new KryptonSnapLayoutCard[Count];
        CopyTo(copy, 0);
        return copy;
    }
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
    /// <param name="custom">Cards to append after the builtins. <see langword="null"/> or empty adds nothing. Wide-monitor gating does not apply to these.</param>
    /// <param name="includeBuiltin">When <see langword="false"/>, only <paramref name="custom"/> is considered.</param>
    /// <returns>Cards safe to show. May be empty.</returns>
    internal static KryptonSnapLayoutCard[] GetCards(Size workArea, Size minimumSize, bool rightToLeft, KryptonSnapLayoutCard[]? custom, bool includeBuiltin)
    {
        var cards = new List<KryptonSnapLayoutCard>();
        if (includeBuiltin)
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

            foreach (KryptonSnapLayoutZone[] template in templates)
            {
                TryAdd(cards, workArea, minimumSize, rightToLeft, template, false);
            }
        }

        if (custom != null)
        {
            foreach (KryptonSnapLayoutCard card in custom)
            {
                KryptonSnapLayoutZone[]? zones = CopyForMenu(card.Zones);
                if (zones != null)
                {
                    TryAdd(cards, workArea, minimumSize, rightToLeft, zones, true);
                }
            }
        }

        return cards.ToArray();
    }

    /// <summary>
    /// Copies designer zones into validated instances. An unfinished zone drops the whole card.
    /// </summary>
    /// <param name="zones">Zones from a custom card.</param>
    /// <returns>Validated copies, or <see langword="null"/> when the card cannot be shown.</returns>
    private static KryptonSnapLayoutZone[]? CopyForMenu(KryptonSnapLayoutZoneCollection zones)
    {
        if (zones.Count == 0)
        {
            return null;
        }

        var copy = new KryptonSnapLayoutZone[zones.Count];
        for (int i = 0; i < zones.Count; i++)
        {
            KryptonSnapLayoutZone zone = zones[i];
            try
            {
                copy[i] = new KryptonSnapLayoutZone(zone.X, zone.Y, zone.Width, zone.Height);
            }
            catch (ArgumentOutOfRangeException)
            {
                return null;
            }
        }

        return copy;
    }

    private static void TryAdd(List<KryptonSnapLayoutCard> cards, Size workArea, Size minimumSize, bool rightToLeft, KryptonSnapLayoutZone[] template, bool custom)
    {
        KryptonSnapLayoutZone[] zones = rightToLeft ? Mirror(template) : template;
        if (Fits(workArea, minimumSize, zones))
        {
            cards.Add(new KryptonSnapLayoutCard(zones, custom));
        }
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

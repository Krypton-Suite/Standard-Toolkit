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
/// Process-wide registry of toolkit snap-layout memberships for in-process Snap Groups.
/// </summary>
/// <remarks>
/// A group is two or more live <see cref="KryptonForm"/> instances that share a card fingerprint
/// and monitor work area. Other applications are never moved.
/// </remarks>
internal static class KryptonSnapGroupRegistry
{
    private static readonly List<Membership> _memberships = new List<Membership>();

    /// <summary>
    /// Records that <paramref name="form"/> occupies <paramref name="zoneIndex"/> of the card.
    /// </summary>
    /// <param name="form">Form that was just snapped.</param>
    /// <param name="cardZones">Zones of the card, in left-to-right order.</param>
    /// <param name="zoneIndex">Index into <paramref name="cardZones"/>.</param>
    /// <param name="workArea">Monitor work area used for the snap.</param>
    internal static void Register(KryptonForm form, KryptonSnapLayoutZone[] cardZones, int zoneIndex, Rectangle workArea)
    {
        if (form == null || cardZones == null || cardZones.Length == 0
            || zoneIndex < 0 || zoneIndex >= cardZones.Length
            || workArea.Width < 1 || workArea.Height < 1)
        {
            return;
        }

        Unregister(form);
        KryptonSnapLayoutZone[] zones = CloneZones(cardZones);
        _memberships.Add(new Membership(
            new WeakReference(form),
            zones,
            zoneIndex,
            MonitorKey(workArea),
            CardFingerprint(zones)));
    }

    /// <summary>
    /// Removes any membership for <paramref name="form"/>.
    /// </summary>
    /// <param name="form">Form that left its toolkit snap.</param>
    internal static void Unregister(KryptonForm form)
    {
        if (form == null)
        {
            return;
        }

        for (int i = _memberships.Count - 1; i >= 0; i--)
        {
            if (TryGetForm(_memberships[i], out KryptonForm? member) && ReferenceEquals(member, form))
            {
                _memberships.RemoveAt(i);
            }
            else if (!TryGetForm(_memberships[i], out _))
            {
                _memberships.RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// Returns live members of the group for <paramref name="cardZones"/> on <paramref name="workArea"/>.
    /// </summary>
    /// <param name="cardZones">Card zones to match.</param>
    /// <param name="workArea">Monitor work area.</param>
    /// <param name="members">Live members when the return value is <see langword="true"/>.</param>
    /// <returns><see langword="true"/> when two or more live members share that card.</returns>
    internal static bool TryGetGroup(KryptonSnapLayoutZone[] cardZones, Rectangle workArea, out KryptonSnapGroupMember[] members)
    {
        members = Array.Empty<KryptonSnapGroupMember>();
        if (cardZones == null || cardZones.Length == 0 || workArea.Width < 1 || workArea.Height < 1)
        {
            return false;
        }

        string monitor = MonitorKey(workArea);
        string fingerprint = CardFingerprint(cardZones);
        PruneDead();
        var live = new List<KryptonSnapGroupMember>();
        for (int i = 0; i < _memberships.Count; i++)
        {
            Membership membership = _memberships[i];
            if (membership.MonitorKey != monitor || membership.Fingerprint != fingerprint)
            {
                continue;
            }

            if (!TryGetForm(membership, out KryptonForm? form) || form == null)
            {
                continue;
            }

            live.Add(new KryptonSnapGroupMember(form, membership.ZoneIndex, membership.CardZones[membership.ZoneIndex]));
        }

        if (live.Count < 2)
        {
            return false;
        }

        members = live.ToArray();
        return true;
    }

    /// <summary>
    /// Builds the Windows-style group caption from live member titles.
    /// </summary>
    /// <param name="members">Group members.</param>
    /// <returns>Caption such as <c>Group | Title and 1 other</c>.</returns>
    internal static string FormatGroupCaption(KryptonSnapGroupMember[] members)
    {
        if (members == null || members.Length == 0)
        {
            return string.Empty;
        }

        string first = TitleOf(members[0].Form);
        if (members.Length == 1)
        {
            return @"Group | " + first;
        }

        if (members.Length == 2)
        {
            return @"Group | " + first + @" and " + TitleOf(members[1].Form);
        }

        return @"Group | " + first + @" and " + (members.Length - 1) + @" others";
    }

    /// <summary>
    /// Collects other top-level <see cref="KryptonForm"/> instances that can fill remaining zones.
    /// </summary>
    /// <param name="exclude">Form that already snapped.</param>
    /// <param name="remainingZones">Empty zones still to fill.</param>
    /// <param name="workArea">Monitor work area.</param>
    /// <returns>Peer forms whose minimum size fits at least one remaining zone.</returns>
    internal static KryptonForm[] CollectAssistPeers(KryptonForm exclude, KryptonSnapLayoutZone[] remainingZones, Rectangle workArea)
    {
        if (exclude == null || remainingZones == null || remainingZones.Length == 0)
        {
            return Array.Empty<KryptonForm>();
        }

        var peers = new List<KryptonForm>();
        foreach (Form open in Application.OpenForms)
        {
            if (!(open is KryptonForm peer) || peer.IsDisposed || !peer.Visible || ReferenceEquals(peer, exclude))
            {
                continue;
            }

            if (peer.Owner != null && !peer.ShowInTaskbar)
            {
                continue;
            }

            if (!FitsAnyZone(peer.MinimumSize, remainingZones, workArea.Size))
            {
                continue;
            }

            peers.Add(peer);
        }

        return peers.ToArray();
    }

    /// <summary>
    /// Ordered zone fingerprint used as part of the group key.
    /// </summary>
    /// <param name="zones">Card zones.</param>
    /// <returns>Stable fingerprint string.</returns>
    internal static string CardFingerprint(KryptonSnapLayoutZone[] zones)
    {
        if (zones == null || zones.Length == 0)
        {
            return string.Empty;
        }

        var builder = new StringBuilder(zones.Length * 24);
        for (int i = 0; i < zones.Length; i++)
        {
            if (i > 0)
            {
                builder.Append(';');
            }

            KryptonSnapLayoutZone zone = zones[i];
            builder.Append(zone.X.ToString(@"0.####", CultureInfo.InvariantCulture));
            builder.Append(',');
            builder.Append(zone.Y.ToString(@"0.####", CultureInfo.InvariantCulture));
            builder.Append(',');
            builder.Append(zone.Width.ToString(@"0.####", CultureInfo.InvariantCulture));
            builder.Append(',');
            builder.Append(zone.Height.ToString(@"0.####", CultureInfo.InvariantCulture));
        }

        return builder.ToString();
    }

    /// <summary>
    /// Copies <paramref name="zones"/> so the registry owns the snapshot.
    /// </summary>
    /// <param name="zones">Source zones.</param>
    /// <returns>Independent copies.</returns>
    internal static KryptonSnapLayoutZone[] CloneZones(KryptonSnapLayoutZone[] zones)
    {
        var copy = new KryptonSnapLayoutZone[zones.Length];
        for (int i = 0; i < zones.Length; i++)
        {
            KryptonSnapLayoutZone zone = zones[i];
            copy[i] = new KryptonSnapLayoutZone(zone.X, zone.Y, zone.Width, zone.Height);
        }

        return copy;
    }

    /// <summary>
    /// Copies the zones of <paramref name="card"/> into an array.
    /// </summary>
    /// <param name="card">Card shown in the menu.</param>
    /// <returns>Zone array, or empty when the card has no zones.</returns>
    internal static KryptonSnapLayoutZone[] SnapshotCardZones(KryptonSnapLayoutCard card)
    {
        if (card == null || card.Zones.Count == 0)
        {
            return Array.Empty<KryptonSnapLayoutZone>();
        }

        var zones = new KryptonSnapLayoutZone[card.Zones.Count];
        for (int i = 0; i < card.Zones.Count; i++)
        {
            KryptonSnapLayoutZone zone = card.Zones[i];
            zones[i] = new KryptonSnapLayoutZone(zone.X, zone.Y, zone.Width, zone.Height);
        }

        return zones;
    }

    private static string MonitorKey(Rectangle workArea) =>
        workArea.X + @"," + workArea.Y + @"," + workArea.Width + @"," + workArea.Height;

    private static void PruneDead()
    {
        for (int i = _memberships.Count - 1; i >= 0; i--)
        {
            if (!TryGetForm(_memberships[i], out _))
            {
                _memberships.RemoveAt(i);
            }
        }
    }

    private static bool TryGetForm(Membership membership, out KryptonForm? form)
    {
        form = null;
        object? target = membership.Form.Target;
        if (!(target is KryptonForm candidate) || candidate.IsDisposed)
        {
            return false;
        }

        form = candidate;
        return true;
    }

    private static string TitleOf(KryptonForm form)
    {
        string text = form.Text;
        if (string.IsNullOrWhiteSpace(text))
        {
            text = form.Name;
        }

        if (string.IsNullOrWhiteSpace(text))
        {
            text = form.GetType().Name;
        }

        if (text.Length > 48)
        {
            text = text.Substring(0, 45) + @"...";
        }

        return text;
    }

    private static bool FitsAnyZone(Size minimumSize, KryptonSnapLayoutZone[] zones, Size workArea)
    {
        foreach (KryptonSnapLayoutZone zone in zones)
        {
            int width = Math.Max(1, (int)Math.Round(workArea.Width * zone.Width));
            int height = Math.Max(1, (int)Math.Round(workArea.Height * zone.Height));
            if (minimumSize.Width > 0 && width < minimumSize.Width)
            {
                continue;
            }

            if (minimumSize.Height > 0 && height < minimumSize.Height)
            {
                continue;
            }

            return true;
        }

        return false;
    }

    private sealed class Membership
    {
        internal Membership(WeakReference form, KryptonSnapLayoutZone[] cardZones, int zoneIndex, string monitorKey, string fingerprint)
        {
            Form = form;
            CardZones = cardZones;
            ZoneIndex = zoneIndex;
            MonitorKey = monitorKey;
            Fingerprint = fingerprint;
        }

        internal WeakReference Form { get; }

        internal KryptonSnapLayoutZone[] CardZones { get; }

        internal int ZoneIndex { get; }

        internal string MonitorKey { get; }

        internal string Fingerprint { get; }
    }
}

/// <summary>
/// One live form in a toolkit snap group.
/// </summary>
internal sealed class KryptonSnapGroupMember
{
    /// <summary>
    /// Initializes a member for <paramref name="form"/> in <paramref name="zone"/>.
    /// </summary>
    /// <param name="form">Live form.</param>
    /// <param name="zoneIndex">Index of the zone in the card.</param>
    /// <param name="zone">Zone fractions.</param>
    internal KryptonSnapGroupMember(KryptonForm form, int zoneIndex, KryptonSnapLayoutZone zone)
    {
        Form = form;
        ZoneIndex = zoneIndex;
        Zone = zone;
    }

    /// <summary>Gets the form.</summary>
    internal KryptonForm Form { get; }

    /// <summary>Gets the zone index in the card.</summary>
    internal int ZoneIndex { get; }

    /// <summary>Gets the zone fractions.</summary>
    internal KryptonSnapLayoutZone Zone { get; }
}

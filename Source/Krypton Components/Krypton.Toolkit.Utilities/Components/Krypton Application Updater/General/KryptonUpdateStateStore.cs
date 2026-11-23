#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>Postpone, skip, and last-check values for one application, stored beside the user profile.</summary>
internal sealed class KryptonUpdateStateStore
{
    public DateTime? LastCheckUtc { get; set; }

    public Version? PostponedVersion { get; set; }

    public DateTime? PostponeUntilUtc { get; set; }

    public List<Version> SkippedVersions { get; } = new List<Version>();

    public bool IsSkipped(Version version)
    {
        foreach (var skipped in SkippedVersions)
        {
            if (skipped == version)
            {
                return true;
            }
        }

        return false;
    }

    public bool IsPostponeActive(Version version, DateTime utcNow) =>
        PostponedVersion == version && PostponeUntilUtc.HasValue && PostponeUntilUtc.Value > utcNow;

    public void Postpone(Version version, DateTime untilUtc)
    {
        PostponedVersion = version;
        PostponeUntilUtc = untilUtc;
    }

    public void Skip(Version version)
    {
        if (!IsSkipped(version))
        {
            SkippedVersions.Add(version);
        }
    }

    public static KryptonUpdateStateStore Load(string path)
    {
        var state = new KryptonUpdateStateStore();
        if (!File.Exists(path))
        {
            return state;
        }

        foreach (var line in File.ReadAllLines(path))
        {
            var split = line.IndexOf('=');
            if (split <= 0)
            {
                continue;
            }

            var name = line.Substring(0, split);
            var value = line.Substring(split + 1);
            if (name == "lastCheckUtc" && DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var lastCheck))
            {
                state.LastCheckUtc = lastCheck.ToUniversalTime();
            }
            else if (name == "postponedVersion" && KryptonUpdateVersion.TryParse(value, out var postponed))
            {
                state.PostponedVersion = postponed;
            }
            else if (name == "postponeUntilUtc" && DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var until))
            {
                state.PostponeUntilUtc = until.ToUniversalTime();
            }
            else if (name == "skipped" && KryptonUpdateVersion.TryParse(value, out var skipped))
            {
                state.SkippedVersions.Add(skipped);
            }
        }

        return state;
    }

    public void Save(string path)
    {
        var directory = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(directory))
        {
            Directory.CreateDirectory(directory);
        }

        var builder = new StringBuilder();
        if (LastCheckUtc.HasValue)
        {
            builder.Append("lastCheckUtc=").AppendLine(LastCheckUtc.Value.ToUniversalTime().ToString("o", CultureInfo.InvariantCulture));
        }

        if (PostponedVersion != null)
        {
            builder.Append("postponedVersion=").AppendLine(PostponedVersion.ToString());
        }

        if (PostponeUntilUtc.HasValue)
        {
            builder.Append("postponeUntilUtc=").AppendLine(PostponeUntilUtc.Value.ToUniversalTime().ToString("o", CultureInfo.InvariantCulture));
        }

        foreach (var skipped in SkippedVersions)
        {
            builder.Append("skipped=").AppendLine(skipped.ToString());
        }

        File.WriteAllText(path, builder.ToString(), Encoding.UTF8);
    }
}

#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Reads a JSON manifest that is either one package object or <c>{ "packages": [ ... ] }</c>.
/// </summary>
internal static class KryptonUpdateManifestParser
{
    internal static KryptonUpdateCatalog Parse(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            throw new KryptonUpdateException("The update manifest is empty.");
        }

        var raw = ReadRawPackages(json, out var feed);
        var packages = new List<KryptonUpdatePackage>(raw.Count);
        foreach (var item in raw)
        {
            packages.Add(Build(item, feed));
        }

        return new KryptonUpdateCatalog(packages);
    }

    private static KryptonUpdatePackage Build(RawPackage raw, FeedPolicy feed)
    {
        if (!KryptonUpdateVersion.TryParse(raw.Version, out var version))
        {
            throw new KryptonUpdateException("The manifest version is not a System.Version value.");
        }

        var packageUrl = raw.PackageUrl;
        if (packageUrl == null || string.IsNullOrWhiteSpace(packageUrl))
        {
            throw new KryptonUpdateException("The manifest is missing packageUrl.");
        }

        var sha256 = raw.Sha256;
        if (sha256 == null || string.IsNullOrWhiteSpace(sha256))
        {
            throw new KryptonUpdateException("The manifest is missing sha256.");
        }

        DateTime? released = null;
        if (!string.IsNullOrWhiteSpace(raw.ReleasedUtc))
        {
            if (!DateTime.TryParse(raw.ReleasedUtc, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var parsed))
            {
                throw new KryptonUpdateException("The manifest releasedUtc value is not a time.");
            }

            released = parsed.ToUniversalTime();
        }

        return new KryptonUpdatePackage(
            version,
            ParseSeverity(raw.Severity),
            ParseKind(raw.PackageKind, packageUrl),
            packageUrl.Trim(),
            sha256.Trim(),
            raw.CertificateThumbprint,
            raw.ReleaseNotes,
            raw.InstallerArguments,
            released,
            raw.MaximumPostponeDays ?? feed.MaximumPostponeDays,
            raw.AllowPostpone ?? feed.AllowPostpone,
            raw.AllowSkip ?? feed.AllowSkip,
            raw.AllowResume ?? feed.AllowResume,
            raw.AllowRetry ?? feed.AllowRetry);
    }

    private static KryptonUpdateSeverity ParseSeverity(string? text)
    {
        if (text == null || string.IsNullOrWhiteSpace(text))
        {
            return KryptonUpdateSeverity.Recommended;
        }

        if (Enum.TryParse(text.Trim(), true, out KryptonUpdateSeverity severity))
        {
            return severity;
        }

        throw new KryptonUpdateException("Unknown update severity '" + text + "'.");
    }

    private static KryptonUpdatePackageKind ParseKind(string? text, string packageUrl)
    {
        if (text != null && !string.IsNullOrWhiteSpace(text))
        {
            if (text.Equals("archive", StringComparison.OrdinalIgnoreCase) || text.Equals("zip", StringComparison.OrdinalIgnoreCase))
            {
                return KryptonUpdatePackageKind.Archive;
            }

            if (text.Equals("installer", StringComparison.OrdinalIgnoreCase))
            {
                return KryptonUpdatePackageKind.Installer;
            }

            throw new KryptonUpdateException("Unknown packageKind '" + text + "'.");
        }

        return packageUrl.EndsWith(".zip", StringComparison.OrdinalIgnoreCase)
            ? KryptonUpdatePackageKind.Archive
            : KryptonUpdatePackageKind.Installer;
    }

    private static List<RawPackage> ReadRawPackages(string json, out FeedPolicy feed)
    {
#if NET8_0_OR_GREATER
        return ReadWithJsonDocument(json, out feed);
#else
        return ReadWithSerializer(json, out feed);
#endif
    }

#if NET8_0_OR_GREATER
    private static List<RawPackage> ReadWithJsonDocument(string json, out FeedPolicy feed)
    {
        using var document = JsonDocument.Parse(json);
        var root = document.RootElement;
        var list = new List<RawPackage>();
        feed = default;
        if (root.ValueKind == JsonValueKind.Object && root.TryGetProperty("packages", out var packages) && packages.ValueKind == JsonValueKind.Array)
        {
            feed = ReadPolicy(root);
            foreach (var item in packages.EnumerateArray())
            {
                list.Add(ReadElement(item));
            }

            return list;
        }

        list.Add(ReadElement(root));
        return list;
    }

    private static RawPackage ReadElement(JsonElement element)
    {
        if (element.ValueKind != JsonValueKind.Object)
        {
            throw new KryptonUpdateException("The update manifest must be a JSON object.");
        }

        return new RawPackage
        {
            Version = GetString(element, "version"),
            Severity = GetString(element, "severity"),
            PackageKind = GetString(element, "packageKind"),
            PackageUrl = GetString(element, "packageUrl"),
            Sha256 = GetString(element, "sha256"),
            CertificateThumbprint = GetString(element, "certificateThumbprint"),
            ReleaseNotes = GetString(element, "releaseNotes"),
            InstallerArguments = GetString(element, "installerArguments"),
            ReleasedUtc = GetString(element, "releasedUtc"),
            MaximumPostponeDays = GetInt32(element, "maximumPostponeDays"),
            AllowPostpone = GetBool(element, "allowPostpone"),
            AllowSkip = GetBool(element, "allowSkip"),
            AllowResume = GetBool(element, "allowResume"),
            AllowRetry = GetBool(element, "allowRetry")
        };
    }

    private static FeedPolicy ReadPolicy(JsonElement element) =>
        new FeedPolicy
        {
            MaximumPostponeDays = GetInt32(element, "maximumPostponeDays"),
            AllowPostpone = GetBool(element, "allowPostpone"),
            AllowSkip = GetBool(element, "allowSkip"),
            AllowResume = GetBool(element, "allowResume"),
            AllowRetry = GetBool(element, "allowRetry")
        };

    private static int? GetInt32(JsonElement element, string name)
    {
        if (!element.TryGetProperty(name, out var value) || value.ValueKind == JsonValueKind.Null)
        {
            return null;
        }

        if (value.ValueKind == JsonValueKind.Number && value.TryGetInt32(out var number))
        {
            return number;
        }

        if (value.ValueKind == JsonValueKind.String && int.TryParse(value.GetString(), NumberStyles.Integer, CultureInfo.InvariantCulture, out var parsed))
        {
            return parsed;
        }

        throw new KryptonUpdateException("The manifest " + name + " value is not a whole number.");
    }

    private static bool? GetBool(JsonElement element, string name)
    {
        if (!element.TryGetProperty(name, out var value) || value.ValueKind == JsonValueKind.Null)
        {
            return null;
        }

        if (value.ValueKind == JsonValueKind.True)
        {
            return true;
        }

        if (value.ValueKind == JsonValueKind.False)
        {
            return false;
        }

        if (value.ValueKind == JsonValueKind.String && bool.TryParse(value.GetString(), out var parsed))
        {
            return parsed;
        }

        throw new KryptonUpdateException("The manifest " + name + " value is not true or false.");
    }

    private static string? GetString(JsonElement element, string name)
    {
        if (!element.TryGetProperty(name, out var value) || value.ValueKind == JsonValueKind.Null)
        {
            return null;
        }

        return value.ValueKind == JsonValueKind.String ? value.GetString() : value.ToString();
    }
#else
    private static List<RawPackage> ReadWithSerializer(string json, out FeedPolicy feed)
    {
        var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        var data = serializer.DeserializeObject(json);
        var list = new List<RawPackage>();
        feed = default;
        if (data is Dictionary<string, object> root && root.TryGetValue("packages", out var packages))
        {
            feed = ReadPolicy(root);
            foreach (var item in EnumerateObjects(packages))
            {
                list.Add(ReadDictionary(item));
            }

            return list;
        }

        if (data is Dictionary<string, object> single)
        {
            list.Add(ReadDictionary(single));
            return list;
        }

        throw new KryptonUpdateException("The update manifest must be a JSON object.");
    }

    private static RawPackage ReadDictionary(Dictionary<string, object> element) =>
        new RawPackage
        {
            Version = GetString(element, "version"),
            Severity = GetString(element, "severity"),
            PackageKind = GetString(element, "packageKind"),
            PackageUrl = GetString(element, "packageUrl"),
            Sha256 = GetString(element, "sha256"),
            CertificateThumbprint = GetString(element, "certificateThumbprint"),
            ReleaseNotes = GetString(element, "releaseNotes"),
            InstallerArguments = GetString(element, "installerArguments"),
            ReleasedUtc = GetString(element, "releasedUtc"),
            MaximumPostponeDays = GetInt32(element, "maximumPostponeDays"),
            AllowPostpone = GetBool(element, "allowPostpone"),
            AllowSkip = GetBool(element, "allowSkip"),
            AllowResume = GetBool(element, "allowResume"),
            AllowRetry = GetBool(element, "allowRetry")
        };

    private static FeedPolicy ReadPolicy(Dictionary<string, object> element) =>
        new FeedPolicy
        {
            MaximumPostponeDays = GetInt32(element, "maximumPostponeDays"),
            AllowPostpone = GetBool(element, "allowPostpone"),
            AllowSkip = GetBool(element, "allowSkip"),
            AllowResume = GetBool(element, "allowResume"),
            AllowRetry = GetBool(element, "allowRetry")
        };

    private static int? GetInt32(Dictionary<string, object> element, string name)
    {
        if (!element.TryGetValue(name, out var value) || value == null)
        {
            return null;
        }

        try
        {
            return Convert.ToInt32(value, CultureInfo.InvariantCulture);
        }
        catch (Exception exception) when (exception is FormatException || exception is InvalidCastException || exception is OverflowException)
        {
            throw new KryptonUpdateException("The manifest " + name + " value is not a whole number.");
        }
    }

    private static bool? GetBool(Dictionary<string, object> element, string name)
    {
        if (!element.TryGetValue(name, out var value) || value == null)
        {
            return null;
        }

        if (value is bool flag)
        {
            return flag;
        }

        if (value is string text && bool.TryParse(text, out var parsed))
        {
            return parsed;
        }

        throw new KryptonUpdateException("The manifest " + name + " value is not true or false.");
    }

    private static string? GetString(Dictionary<string, object> element, string name)
    {
        if (!element.TryGetValue(name, out var value) || value == null)
        {
            return null;
        }

        return Convert.ToString(value, CultureInfo.InvariantCulture);
    }

    private static List<Dictionary<string, object>> EnumerateObjects(object value)
    {
        var list = new List<Dictionary<string, object>>();
        if (value is object[] array)
        {
            foreach (var item in array)
            {
                if (item is Dictionary<string, object> dictionary)
                {
                    list.Add(dictionary);
                }
            }
        }
        else if (value is ArrayList arrayList)
        {
            foreach (var item in arrayList)
            {
                if (item is Dictionary<string, object> dictionary)
                {
                    list.Add(dictionary);
                }
            }
        }

        return list;
    }
#endif

    private sealed class RawPackage
    {
        public string? Version { get; set; }

        public string? Severity { get; set; }

        public string? PackageKind { get; set; }

        public string? PackageUrl { get; set; }

        public string? Sha256 { get; set; }

        public string? CertificateThumbprint { get; set; }

        public string? ReleaseNotes { get; set; }

        public string? InstallerArguments { get; set; }

        public string? ReleasedUtc { get; set; }

        public int? MaximumPostponeDays { get; set; }

        public bool? AllowPostpone { get; set; }

        public bool? AllowSkip { get; set; }

        public bool? AllowResume { get; set; }

        public bool? AllowRetry { get; set; }
    }

    private struct FeedPolicy
    {
        public int? MaximumPostponeDays;

        public bool? AllowPostpone;

        public bool? AllowSkip;

        public bool? AllowResume;

        public bool? AllowRetry;
    }
}
